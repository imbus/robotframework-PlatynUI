# SPDX-FileCopyrightText: 2024 Daniel Biehl <daniel.biehl@imbus.de>
#
# SPDX-License-Identifier: Apache-2.0

from typing import List

from robotlibcore import keyword

from ..core.contextbase import ContextBase
from .types import ElementDescriptor


class Elements:
    @keyword
    def get_children(self, descriptor: ElementDescriptor) -> List[ContextBase]:
        return descriptor.context().get_children(None)

    @keyword
    def get_children_locators(self, descriptor: ElementDescriptor) -> List[str]:
        """Return XPath locator strings for each direct child of the element."""
        parent_context = descriptor.context()
        parent_locator = parent_context.locator
        parent_path = getattr(parent_locator, "path", None) or parent_locator.get_path(
            parent_context.context_parent, context_type=type(parent_context)
        )

        result: List[str] = []
        for child in parent_context.get_children(None):
            adapter = child._adapter
            if adapter is None:
                continue

            role = adapter.role or "*"
            attr = (
                "@AutomationId='%s'" % adapter.id if adapter.id
                else "@Name='%s'" % adapter.name if adapter.name
                else None
            )
            segment = "%s[%s]" % (role, attr) if attr else role
            result.append("%s/%s" % (parent_path, segment) if parent_path else segment)

        return result
