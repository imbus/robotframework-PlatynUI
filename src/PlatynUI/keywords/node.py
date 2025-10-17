from robotlibcore import keyword

from ..ui.runtime.dotnet_interface import DotNetInterface

__all__ = ["NodeKeywords"]


class NodeKeywords:

    @keyword
    def invalidate_node(self, xpath: str) -> None:
        """Invalidates the cached node information, so that the next access fetches fresh data."""
        DotNetInterface.finder().InvalidateNode(xpath, False)