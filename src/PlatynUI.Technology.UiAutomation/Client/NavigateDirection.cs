// SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
//
// SPDX-License-Identifier: Apache-2.0

namespace PlatynUI.Technology.UiAutomation.Client;

public enum NavigateDirection
{
    NavigateDirection_Parent,
    NavigateDirection_NextSibling,
    NavigateDirection_PreviousSibling,
    NavigateDirection_FirstChild,
    NavigateDirection_LastChild,
}
