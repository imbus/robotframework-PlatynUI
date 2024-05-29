from ..core import context
from . import Control, strategies

__all__ = ["Text"]


@context
class Text(Control):

    @property
    def text(self) -> str:
        self.ensure_that(self._application_is_ready)

        return self.adapter.get_strategy(strategies.Text).text
