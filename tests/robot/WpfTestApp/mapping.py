from typing import Optional

from PlatynUI.ui import Button, Window, locator


@locator(AutomationId="Shell", ProcessName="WpfTestApp")
class ShellWindow(Window):
    @property
    @locator(AutomationId="DoSomethingOther")
    def DoSomethingOther(self) -> Optional[Button]:
        pass


Shell = ShellWindow()
