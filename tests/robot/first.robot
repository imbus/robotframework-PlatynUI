*** Settings ***
Library         PlatynUI
Variables       mapping.py


*** Test Cases ***
first
    Ensure Exists    Window[contains(@Name, "Rechner")]    30s

    # Activate    ${calculator}
    # Type Keys    ${calculator}    <ALT+1>
    # Clear
    # Activate    ${calculator.n1}
    # Activate    ${calculator.n2}
    # Activate    ${calculator.n3}
    # Activate    ${calculator.n4}
    # Activate    ${calculator.plus}
    # Activate    ${calculator.n5}
    # Activate    ${calculator.n6}
    # Activate    ${calculator.n7}
    # Activate    ${calculator.n8}

    # Activate    ${calculator.equal}

    # Type Keys    ${calculator}    <ALT+1>
    # Type Keys    ${calculator}    <ALT+2>
    # Type Keys    ${calculator}    <ALT+3>
    # Type Keys    ${calculator}    <ALT+4>
    # Type Keys    ${calculator}    <ALT+1>
    # Type Keys    ${calculator}    <ESCAPE>
    # Type Keys    ${calculator}    12345
    # Type Keys    ${calculator}    <ESCAPE>
    # Type Keys    ${calculator}    12345    delay=1
    # Type Keys    ${calculator}    <CONTROL+c>
    # Type Keys    ${calculator}    <ESCAPE>
    # Type Keys    ${calculator}    <CONTROL+v>
    # Type Keys    ${calculator}    +
    # Type Keys    ${calculator}    67890
    # Type Keys    ${calculator}    <ENTER>
    # Type Keys    .    <ALT+F4>

    # Set Text    ${calculator}    12345
    # ${text}    Get Text    ${calculator.n1}
    # Get Text    ${calculator.n1}    ==    ein text123445

    Click    ${calculator.n1}    button=right
    Click    ${calculator.results}    button=right
    Click    ${calculator.calculator_results}    button=right


*** Keywords ***
Clear
    Activate    ${calculator.clear}
