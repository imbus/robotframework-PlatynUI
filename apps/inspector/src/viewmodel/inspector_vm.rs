//! ViewModel: Overall application state for the Inspector.

use crate::model::tree_data::{DisplayAttribute, SearchResultItem, UiNodeData};
use crate::viewmodel::{async_tasks, tree_vm::TreeViewModel};
use eframe::egui;
use egui_async::Bind;
use platynui_core::ui::UiNode;
use platynui_runtime::Runtime;
use std::sync::Arc;
use std::sync::atomic::{AtomicBool, Ordering};
use std::time::Instant;

/// Top-level ViewModel holding the complete inspector state.
pub struct InspectorViewModel {
    /// Tree view model (expand/collapse, flattened rows).
    pub tree: TreeViewModel,
    /// Currently selected row index (mouse click or keyboard).
    pub selected_index: Option<usize>,
    /// Currently focused row index (keyboard navigation).
    pub focused_index: usize,
    /// XPath search text.
    pub search_text: String,
    /// Whether the window should stay on top.
    pub always_on_top: bool,
    /// Whether the selected node should be refreshed automatically every 15 s.
    pub auto_refresh: bool,
    /// Cached attributes for the currently selected node.
    pub selected_attributes: Vec<DisplayAttribute>,
    /// Label for the currently selected node.
    pub selected_label: String,
    /// Results from XPath evaluation.
    pub results: Vec<SearchResultItem>,
    /// Status / error message for the results panel.
    pub result_status: Option<String>,
    /// Full search error message shown below the search field.
    search_error_hint: Option<String>,
    /// Focused row index in the results panel (keyboard cursor).
    pub result_focused_index: usize,
    /// When true, the tree view should scroll to the focused row on the next frame.
    /// Consumed (set to false) after rendering.
    pub scroll_to_focused: bool,
    /// PlatynUI runtime (kept alive for the entire application).
    runtime: Arc<Runtime>,
    /// Root node kept for lazy start of initial tree loading on first frame.
    init_root: Option<Arc<dyn UiNode>>,
    /// Number of frames to wait before starting initial tree iteration.
    init_defer_frames: u8,
    /// Initial tree loading state handled by egui-async.
    init_load_task: Bind<Vec<Arc<UiNodeData>>, String>,
    /// XPath search state handled by egui-async.
    search_task: Bind<async_tasks::SearchResult, String>,
    /// Shared in-flight search progress for incremental UI updates.
    search_progress: Option<async_tasks::SharedSearchProgress>,
    /// Number of progress items already appended to `results`.
    search_progress_seen: usize,
    /// Cancellation flag for in-flight search task.
    search_cancel_flag: Option<Arc<AtomicBool>>,
    /// Start time of in-flight search for live status.
    search_started_at: Option<Instant>,
    /// Background reveal (tree sync) state handled by egui-async.
    reveal_task: Bind<async_tasks::RevealResult, String>,
    /// Background selected-node details state handled by egui-async.
    selection_task: Bind<async_tasks::SelectionResult, String>,
    /// Highlight clear/show task handled by egui-async.
    highlight_task: Bind<(), String>,
    /// Monotonic request id to ignore stale selection results.
    selection_request_id: u64,
    /// Frame counter for spinner animation.
    spinner_frame: usize,
}

impl InspectorViewModel {
    /// Create a new inspector ViewModel backed by the given runtime.
    pub fn new(runtime: Arc<Runtime>, preloaded_root_children: Vec<Arc<UiNodeData>>) -> Self {
        let rt_for_root = Arc::clone(&runtime);
        let desktop_node = std::thread::Builder::new()
            .name("inspector-root-node".to_string())
            .spawn(move || rt_for_root.desktop_node())
            .expect("failed to spawn inspector-root-node thread")
            .join()
            .expect("inspector-root-node thread panicked");
        let root_data = Arc::new(UiNodeData::new(Arc::clone(&desktop_node)));
        root_data.init_children_cache();

        for child in preloaded_root_children {
            root_data.push_cached_child(Arc::clone(&child));
        }

        let mut tree = TreeViewModel::new(Arc::clone(&root_data));
        if root_data.cached_children().is_some_and(|children| !children.is_empty()) {
            tree.expand_root();
        }

        Self {
            tree,
            selected_index: None,
            focused_index: 0,
            search_text: String::new(),
            always_on_top: false,
            auto_refresh: false,
            selected_attributes: Vec::new(),
            selected_label: String::new(),
            results: Vec::new(),
            result_status: None,
            search_error_hint: None,
            result_focused_index: 0,
            scroll_to_focused: false,
            runtime,
            init_root: if root_data.cached_children().is_some_and(|children| !children.is_empty()) {
                None
            } else {
                Some(desktop_node)
            },
            // Ensure one frame is presented before touching potentially slow
            // UIA root-child iteration.
            init_defer_frames: 1,
            init_load_task: Bind::new(false),
            search_task: Bind::new(false),
            search_progress: None,
            search_progress_seen: 0,
            search_cancel_flag: None,
            search_started_at: None,
            reveal_task: Bind::new(false),
            selection_task: Bind::new(false),
            highlight_task: Bind::new(false),
            selection_request_id: 0,
            spinner_frame: 0,
        }
    }

    /// Poll the initial tree load state. Call this every frame.
    pub fn poll_initial_load(&mut self, ctx: &egui::Context) {
        if self.init_defer_frames > 0 {
            self.init_defer_frames -= 1;
            ctx.request_repaint();
            return;
        }

        if self.init_load_task.is_idle()
            && let Some(root) = self.init_root.take()
        {
            self.init_load_task.refresh(async_tasks::initial_load_task(root));
        }

        if let Some(result) = self.init_load_task.take() {
            match result {
                Ok(loaded_batch) => {
                    if loaded_batch.is_empty() {
                        return;
                    }

                    let root = Arc::clone(self.tree.root());
                    for node_data in loaded_batch {
                        root.push_cached_child(Arc::clone(&node_data));
                    }

                    if self.tree.row_count() == 0 {
                        self.tree.expand_root();
                    } else {
                        self.tree.rebuild_rows();
                    }

                    ctx.request_repaint();
                }
                Err(err) => {
                    tracing::warn!(%err, "initial load task failed");
                }
            }
        } else if self.init_load_task.is_pending() {
            ctx.request_repaint();
        }

        if self.init_load_task.is_idle() && self.init_root.is_none() {
            let root = Arc::clone(self.tree.root());
            if root.cached_children().is_some_and(|children| !children.is_empty()) && self.tree.row_count() == 0 {
                self.tree.expand_root();
            }
        }
    }

    /// Select a tree node by index, updating the attributes panel and highlighting.
    pub fn select_node(&mut self, index: usize) {
        // Ignore redundant re-selection of the same row to avoid repeatedly
        // clearing attributes and replacing in-flight background loads.
        if self.selected_index == Some(index) {
            self.focused_index = index;
            self.scroll_to_focused = true;
            return;
        }

        self.selected_index = Some(index);
        self.focused_index = index;
        self.scroll_to_focused = true;

        if let Some(row) = self.tree.rows().get(index) {
            self.selected_label = row.label.clone();

            self.selection_request_id = self.selection_request_id.wrapping_add(1);
            let request_id = self.selection_request_id;
            let selected_label = row.label.clone();
            let node_data = Arc::clone(&row.data);

            self.selection_task.refresh(async_tasks::selection_task(request_id, selected_label, node_data));
        }
    }

    /// Poll selected-node details loading. Call this every frame.
    pub fn poll_selection(&mut self, ctx: &egui::Context) {
        if let Some(result) = self.selection_task.take() {
            match result {
                Ok(async_tasks::SelectionResult {
                    request_id,
                    selected_label,
                    attributes,
                    is_root,
                    bounds,
                    node_id,
                }) => {
                    // Ignore stale result from an older selection.
                    if request_id != self.selection_request_id {
                        return;
                    }

                    self.selected_label = selected_label;
                    self.selected_attributes = attributes;

                    if is_root {
                        self.clear_highlight();
                    } else {
                        self.highlight_bounds(node_id, bounds);
                    }

                    ctx.request_repaint();
                }
                Err(err) => {
                    tracing::warn!(%err, "selection task failed");
                }
            }
        } else if self.selection_task.is_pending() {
            ctx.request_repaint();
        }
    }

    /// Poll highlight task state. Call this every frame.
    pub fn poll_highlight(&mut self, ctx: &egui::Context) {
        if let Some(result) = self.highlight_task.take() {
            if let Err(err) = result {
                tracing::warn!(%err, "highlight task failed");
            }
        } else if self.highlight_task.is_pending() {
            ctx.request_repaint();
        }
    }

    /// Return true when any egui-async background task is currently running.
    pub fn has_pending_background_work(&mut self) -> bool {
        self.init_load_task.is_pending()
            || self.search_task.is_pending()
            || self.reveal_task.is_pending()
            || self.selection_task.is_pending()
            || self.highlight_task.is_pending()
    }

    /// Current status text for the global status bar.
    pub fn status_bar_text(&self) -> Option<&str> {
        self.result_status.as_deref()
    }

    /// Full search error hint shown under the search field.
    pub fn search_error_hint(&self) -> Option<&str> {
        self.search_error_hint.as_deref()
    }

    /// Clear search error hint when the query text changes.
    pub fn on_search_text_changed(&mut self) {
        self.search_error_hint = None;
        if self.result_status.as_deref().is_some_and(|status| status.starts_with("Error:")) {
            self.result_status = None;
        }
    }

    /// Navigate up one row.
    pub fn navigate_up(&mut self) {
        if self.focused_index > 0 {
            self.focused_index -= 1;
            self.select_node(self.focused_index);
        }
    }

    /// Navigate down one row.
    pub fn navigate_down(&mut self) {
        if self.focused_index + 1 < self.tree.row_count() {
            self.focused_index += 1;
            self.select_node(self.focused_index);
        }
    }

    /// Navigate left: collapse or go to parent.
    pub fn navigate_left(&mut self) {
        let idx = self.focused_index;
        if let Some(row) = self.tree.rows().get(idx) {
            if row.has_children && row.is_expanded {
                self.tree.collapse(idx);
            } else if let Some(parent) = self.tree.parent_index(idx) {
                self.focused_index = parent;
                self.select_node(parent);
            }
        }
    }

    /// Navigate right: expand or go to first child.
    pub fn navigate_right(&mut self) {
        let idx = self.focused_index;
        let count = self.tree.row_count();
        if let Some(row) = self.tree.rows().get(idx).cloned() {
            if row.has_children && !row.is_expanded {
                self.tree.expand(idx);
            } else if row.has_children && row.is_expanded && idx + 1 < count {
                self.focused_index = idx + 1;
                self.select_node(idx + 1);
            }
        }
    }

    /// Navigate to the first row.
    pub fn navigate_home(&mut self) {
        if self.tree.row_count() > 0 {
            self.focused_index = 0;
            self.select_node(0);
        }
    }

    /// Navigate to the last row.
    pub fn navigate_end(&mut self) {
        let count = self.tree.row_count();
        if count > 0 {
            self.focused_index = count - 1;
            self.select_node(count - 1);
        }
    }

    /// Navigate up by a page.
    pub fn navigate_page_up(&mut self, page_size: usize) {
        self.focused_index = self.focused_index.saturating_sub(page_size);
        self.select_node(self.focused_index);
    }

    /// Navigate down by a page.
    pub fn navigate_page_down(&mut self, page_size: usize) {
        let count = self.tree.row_count();
        if count > 0 {
            self.focused_index = (self.focused_index + page_size).min(count - 1);
            self.select_node(self.focused_index);
        }
    }

    /// Refresh a specific tree row.
    pub fn refresh_row(&mut self, index: usize) {
        self.tree.refresh_row(index);
    }

    /// Refresh a tree row and its entire subtree.
    pub fn refresh_subtree(&mut self, index: usize) {
        self.tree.refresh_subtree(index);
    }

    fn highlight_bounds(&mut self, node_id: String, bounds: Option<platynui_core::types::Rect>) {
        if let Some(bounds) = bounds {
            let rt = Arc::clone(&self.runtime);
            self.highlight_task.refresh(async_tasks::highlight_bounds_task(rt, node_id, bounds));
            return;
        }

        self.clear_highlight();
    }

    fn clear_highlight(&mut self) {
        let rt = Arc::clone(&self.runtime);
        self.highlight_task.refresh(async_tasks::clear_highlight_task(rt));
    }

    /// Clear current search results and status.
    pub fn clear_results(&mut self) {
        self.results.clear();
        self.result_status = None;
        self.search_error_hint = None;
        self.result_focused_index = 0;
    }

    /// Refresh the currently selected tree row, if any.
    pub fn refresh_selected_row(&mut self) {
        if let Some(index) = self.selected_index {
            self.refresh_row(index);
        }
    }

    /// Refresh the currently selected tree row subtree, if any.
    pub fn refresh_selected_subtree(&mut self) {
        if let Some(index) = self.selected_index {
            self.refresh_subtree(index);
        }
    }

    /// Highlight the currently selected tree row, if any.
    pub fn highlight_selected_row(&mut self) {
        if let Some(index) = self.selected_index {
            self.highlight_row(index);
        }
    }

    /// Highlight a specific tree row, if possible.
    pub fn highlight_row(&mut self, index: usize) {
        if let Some(row) = self.tree.rows().get(index) {
            let rt = Arc::clone(&self.runtime);
            let node_data = Arc::clone(&row.data);
            self.highlight_task.refresh(async_tasks::highlight_node_task(rt, node_data));
        }
    }

    /// Highlight a specific search result, if it is backed by a UI node.
    pub fn highlight_result(&mut self, index: usize) {
        if let Some(result) = self.results.get(index)
            && let Some(node) = result.ui_node().cloned()
        {
            let rt = Arc::clone(&self.runtime);
            let node_data = Arc::new(UiNodeData::new(node));
            self.highlight_task.refresh(async_tasks::highlight_node_task(rt, node_data));
        }
    }

    /// Expand the currently selected tree row, if any.
    pub fn expand_selected_row(&mut self) {
        if let Some(index) = self.selected_index {
            self.tree.expand(index);
        }
    }

    /// Collapse the currently selected tree row, if any.
    pub fn collapse_selected_row(&mut self) {
        if let Some(index) = self.selected_index {
            self.tree.collapse(index);
        }
    }

    /// Evaluate the current `search_text` as an XPath expression (non-blocking).
    ///
    /// Cancels any in-progress search, then starts an egui-async background task.
    pub fn evaluate_xpath(&mut self) {
        // Cancel any running search first.
        self.cancel_search();

        let xpath = self.search_text.trim().to_string();
        if xpath.is_empty() {
            self.results.clear();
            self.result_status = None;
            self.search_error_hint = None;
            self.result_focused_index = 0;
            return;
        }

        // Clear previous results.
        self.results.clear();
        self.result_status = Some("Searching\u{2026}".to_string());
        self.search_error_hint = None;
        self.result_focused_index = 0;

        let cancel_flag = Arc::new(AtomicBool::new(false));
        let progress = async_tasks::new_search_progress();
        self.search_progress = Some(Arc::clone(&progress));
        self.search_progress_seen = 0;
        self.search_cancel_flag = Some(Arc::clone(&cancel_flag));
        self.search_started_at = Some(Instant::now());
        self.search_task.refresh(async_tasks::search_task(Arc::clone(&self.runtime), xpath, cancel_flag, progress));
    }

    /// Poll the background search for new results. Call this every frame.
    ///
    /// While a search is active, requests a repaint so the next frame polls again.
    pub fn poll_search(&mut self, ctx: &egui::Context) {
        if let Some(result) = self.search_task.take() {
            self.search_progress = None;
            self.search_progress_seen = 0;
            self.search_cancel_flag = None;
            self.search_started_at = None;

            match result {
                Ok(async_tasks::SearchResult { results, elapsed, cancelled }) => {
                    self.results = results;
                    let count = self.results.len();
                    if cancelled {
                        self.result_status = Some(format!(
                            "Cancelled \u{2014} {count} result{} ({:.1}ms)",
                            if count == 1 { "" } else { "s" },
                            elapsed.as_secs_f64() * 1000.0,
                        ));
                    } else {
                        self.result_status = Some(format!(
                            "{count} result{} ({:.1}ms)",
                            if count == 1 { "" } else { "s" },
                            elapsed.as_secs_f64() * 1000.0,
                        ));
                    }
                }
                Err(err) => {
                    self.search_error_hint = Some(err.clone());
                    self.result_status = Some(format!("Error: {}", short_error_summary(&err)));
                }
            }
        } else if self.search_task.is_pending() {
            if let Some(progress) = &self.search_progress
                && let Ok(state) = progress.lock()
            {
                let len = state.results.len();
                if self.search_progress_seen > len {
                    self.search_progress_seen = 0;
                    self.results.clear();
                }
                if self.search_progress_seen < len {
                    self.results.extend_from_slice(&state.results[self.search_progress_seen..]);
                    self.search_progress_seen = len;
                }
            }

            // Update live status while search is in progress.
            self.spinner_frame = self.spinner_frame.wrapping_add(1);
            let elapsed = self.search_started_at.map_or(0.0, |start| start.elapsed().as_secs_f64());
            let count = self.results.len();
            self.result_status =
                Some(format!("Searching\u{2026} {count} result{} ({elapsed:.1}s)", if count == 1 { "" } else { "s" },));
            // Request repaint so next frame continues polling.
            ctx.request_repaint();
        }
    }

    /// Cancel the current background search, if any.
    pub fn cancel_search(&mut self) {
        if self.search_task.is_pending() {
            if let Some(cancel_flag) = &self.search_cancel_flag {
                cancel_flag.store(true, Ordering::Relaxed);
            }
            self.search_task.abort();

            let count = self.results.len();
            let elapsed_ms = self.search_started_at.map_or(0.0, |start| start.elapsed().as_secs_f64() * 1000.0);
            self.result_status = Some(format!(
                "Cancelled \u{2014} {count} result{} ({elapsed_ms:.1}ms)",
                if count == 1 { "" } else { "s" },
            ));

            self.search_progress = None;
            self.search_progress_seen = 0;
            self.search_cancel_flag = None;
            self.search_started_at = None;
        }
    }

    /// Returns `true` if a background search is currently running.
    pub fn is_searching(&mut self) -> bool {
        self.search_task.is_pending()
    }

    /// When a result is selected, reveal its node in the tree (non-blocking).
    ///
    /// Spawns a background task that pre-loads the ancestor path into the
    /// `UiNodeData` cache (expensive AT-SPI / UIA calls). Once ready, the
    /// UI thread performs the cheap expand + rebuild + select.
    ///
    /// If a previous reveal is still in progress it is cancelled.
    pub fn reveal_and_select_result(&mut self, result_index: usize) {
        // Cancel any in-flight reveal and start a fresh request.
        self.reveal_task.abort();

        let item = match self.results.get(result_index) {
            Some(item) => item.clone(),
            None => return,
        };

        let Some(target_node) = item.ui_node().cloned() else {
            return;
        };

        let target_id = target_node.runtime_id().as_str().to_string();

        // Quick path: node is already visible in the cached tree.
        if self.tree.reveal_node_cached(&target_id) {
            self.select_node_if_visible(&target_id);
            return;
        }

        // Slow path: spawn egui-async task to pre-load ancestor caches.
        let root = Arc::clone(self.tree.root());
        self.reveal_task.refresh(async_tasks::reveal_task(root, target_node));
    }

    /// Helper: select a node if it's visible in the tree.
    fn select_node_if_visible(&mut self, target_id: &str) {
        if let Some(tree_index) = self.tree.rows().iter().position(|row| row.data.id() == target_id) {
            self.select_node(tree_index);
        } else {
            tracing::warn!(target_id, "reveal_node: not found in visible rows after expand");
        }
    }

    /// Poll the background reveal operation via egui-async. Call this every frame.
    pub fn poll_reveal(&mut self, ctx: &egui::Context) {
        if let Some(result) = self.reveal_task.take() {
            match result {
                Ok(async_tasks::RevealResult::Ready { target_id }) => {
                    // Now find_and_expand is cheap (children are cached).
                    if self.tree.reveal_node_cached(&target_id) {
                        self.select_node_if_visible(&target_id);
                    } else {
                        tracing::warn!(target_id, "reveal_node: not found after background preload");
                    }
                }
                Ok(async_tasks::RevealResult::Cancelled) => {}
                Err(err) => {
                    tracing::error!(%err, "reveal task failed");
                }
            }
        } else if self.reveal_task.is_pending() {
            // Still working — request repaint for next poll.
            ctx.request_repaint();
        }
    }
}

fn short_error_summary(error: &str) -> String {
    let first_line = error.lines().find(|line| !line.trim().is_empty()).unwrap_or("XPath evaluation failed").trim();
    let summary_with_location = if let Some((line, column)) = extract_line_column(error) {
        format!("L{line}:C{column} {first_line}")
    } else {
        first_line.to_string()
    };
    const MAX_CHARS: usize = 96;
    if summary_with_location.chars().count() <= MAX_CHARS {
        summary_with_location
    } else {
        let shortened: String = summary_with_location.chars().take(MAX_CHARS - 1).collect();
        format!("{shortened}\u{2026}")
    }
}

fn extract_line_column(error: &str) -> Option<(usize, usize)> {
    let line = number_after_keyword(error, "line")?;
    let column = number_after_keyword(error, "column")
        .or_else(|| number_after_keyword(error, "col"))
        .or_else(|| number_after_keyword(error, "position"))?;
    Some((line, column))
}

fn number_after_keyword(input: &str, keyword: &str) -> Option<usize> {
    let lower = input.to_ascii_lowercase();
    let idx = lower.find(keyword)?;
    let rest = &lower[idx + keyword.len()..];

    let mut digits = String::new();
    let mut seen_digit = false;
    for ch in rest.chars() {
        if ch.is_ascii_digit() {
            digits.push(ch);
            seen_digit = true;
        } else if seen_digit {
            break;
        }
    }

    if digits.is_empty() { None } else { digits.parse::<usize>().ok() }
}
