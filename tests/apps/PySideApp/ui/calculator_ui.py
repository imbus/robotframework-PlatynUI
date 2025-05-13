from PySide6.QtCore import Qt
from PySide6.QtGui import QFont
from PySide6.QtWidgets import QGridLayout, QLineEdit, QMainWindow, QPushButton, QVBoxLayout, QWidget


class CalculatorUI(QMainWindow):
    def __init__(self):
        super().__init__()
        self.setWindowTitle("Calculator")
        self.setGeometry(100, 100, 300, 400)

        self.central_widget = QWidget()
        self.setCentralWidget(self.central_widget)

        self.main_layout = QVBoxLayout()
        self.central_widget.setLayout(self.main_layout)

        # Display with larger font and right alignment
        self.display = QLineEdit()
        self.display.setObjectName("display")
        self.display.setAccessibleIdentifier("display")
        self.display.setAlignment(Qt.AlignRight)
        self.display.setFont(QFont("Arial", 16))
        self.display.setFixedHeight(50)
        #self.display.setReadOnly(True)
        self.main_layout.addWidget(self.display)

        # Grid layout for buttons
        self.buttons_layout = QGridLayout()
        self.main_layout.addLayout(self.buttons_layout)

        self.create_buttons()

    def create_buttons(self):
        buttons = [
            ("7", 0, 0, self.add_to_display),
            ("8", 0, 1, self.add_to_display),
            ("9", 0, 2, self.add_to_display),
            ("/", 0, 3, self.add_to_display),
            ("4", 1, 0, self.add_to_display),
            ("5", 1, 1, self.add_to_display),
            ("6", 1, 2, self.add_to_display),
            ("*", 1, 3, self.add_to_display),
            ("1", 2, 0, self.add_to_display),
            ("2", 2, 1, self.add_to_display),
            ("3", 2, 2, self.add_to_display),
            ("-", 2, 3, self.add_to_display),
            ("0", 3, 0, self.add_to_display),
            (".", 3, 1, self.add_to_display),
            ("C", 3, 2, self.clear_display),
            ("+", 3, 3, self.add_to_display),
            ("=", 4, 0, self.calculate_result, 1, 4),  # Spans entire row
        ]

        for button_def in buttons:
            if len(button_def) == 4:
                text, row, col, handler = button_def
                rowspan, colspan = 1, 1
            else:
                text, row, col, handler, rowspan, colspan = button_def

            button = QPushButton(text)
            button.setFont(QFont("Arial", 12))
            button.setMinimumSize(50, 50)
            button.setAccessibleIdentifier("btn_" + text)  # Set accessible name for the button
            button.setObjectName("btn_" + text)

            # Direkt den Handler aus der Liste zuweisen
            button.clicked.connect(handler)

            self.buttons_layout.addWidget(button, row, col, rowspan, colspan)

    def add_to_display(self, value):
        current_text = self.display.text()
        new_text = current_text + self.sender().text()
        self.display.setText(new_text)

    def clear_display(self):
        self.display.clear()

    def calculate_result(self, _=None):
        try:
            expression = self.display.text()
            allowed_chars = "0123456789+-*/.()"
            if all(char in allowed_chars for char in expression):
                result = eval(expression)
                self.display.setText(str(result))
            else:
                self.display.setText("Invalid Input")
        except Exception:
            self.display.setText("Error")
