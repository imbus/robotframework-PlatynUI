if __name__ == "__main__":
    import sys

    from PySide6.QtWidgets import QApplication
    from ui.calculator_ui import CalculatorUI

    app = QApplication(sys.argv)

    ui = CalculatorUI()
    ui.show()

    sys.exit(app.exec())
