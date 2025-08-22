*** Settings ***
Documentation       Comprehensive X11 keyboard implementation tests for German QWERTZ layout
...                 Tests character input, special characters, German umlauts, and control keys

Library             PlatynUI

Test Tags           wip


*** Variables ***
${CALC_INPUT}       app:Application[@Name="kcalc"]//Text[@AccessibleId="QApplication.MainWindow#1.KCalculator.calc_display_frame.input_display"]


*** Test Cases ***
Test Basic Characters and Y/Z Fix
    [Documentation]    Tests basic alphabetic input including Y/Z swap fix for German QWERTZ
    Mouse Click    ${CALC_INPUT}
    Type Keys    .    qwertzuiopasdfghjklyxcvbnm
    Get Text    ${CALC_INPUT}    equals    qwertzuiopasdfghjklyxcvbnm
    Type Keys    .    <Ctrl+a><Ctrl+x>

Test Uppercase and Mixed Case
    [Documentation]    Tests uppercase letters and mixed case text input
    Mouse Click    ${CALC_INPUT}
    Type Keys    .    HELLO WORLD YAK ZEBRA
    Get Text    ${CALC_INPUT}    equals    HELLO WORLD YAK ZEBRA
    Type Keys    .    <Ctrl+a><Ctrl+x>

Test Special Characters
    [Documentation]    Tests special characters requiring Shift modifier
    Mouse Click    ${CALC_INPUT}
    Type Keys    .    !"#$%^&*(){}[]|\\:;"'<>?~
    Get Text    ${CALC_INPUT}    equals    !"#$%^&*(){}[]|\\:;"'<>?~
    Type Keys    .    <Ctrl+a><Ctrl+x>

Test German Characters
    [Documentation]    Tests German umlauts, sharp S, Euro, and degree symbols
    Mouse Click    ${CALC_INPUT}
    Type Keys    .    <Ctrl+a><Ctrl+x>    # Clear field first
    Type Keys    .    äöüßÄÖÜ€°
    Get Text    ${CALC_INPUT}    equals    äöüßÄÖÜ€°
    Type Keys    .    <Ctrl+a><Ctrl+x>

Test Real World Text
    [Documentation]    Tests practical text scenarios with mixed content
    Mouse Click    ${CALC_INPUT}
    Type Keys    .    Price: €49,99 @ Zürich 25°C
    Get Text    ${CALC_INPUT}    equals    Price: €49,99 @ Zürich 25°C
    Type Keys    .    <Ctrl+a><Ctrl+x>

Test Control Key Operations
    [Documentation]    Tests copy, cut, paste operations with control keys
    Mouse Click    ${CALC_INPUT}
    Type Keys    .    Sample text for editing
    Type Keys    .    <Ctrl+a><Ctrl+c><Ctrl+x>
    Get Text    ${CALC_INPUT}    equals    ${EMPTY}
    Type Keys    .    <Ctrl+v>
    Get Text    ${CALC_INPUT}    equals    Sample text for editing
    Type Keys    .    <Ctrl+a><Ctrl+x>
