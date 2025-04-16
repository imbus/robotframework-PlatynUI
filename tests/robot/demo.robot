*** Settings ***
Library         PlatynUI
Variables       mapping.py
Test Tags    wip

*** Test Cases ***
erster
    Click    /Window[@Name='Rechner']//Button[@AutomationId='num1Button']
    Click    /Window[@Name='Rechner']//Button[@AutomationId='num2Button']
    Click    /Window[@Name='Rechner']//Button[@AutomationId='num3Button']
    Click    /Window[@Name='Rechner']//Button[@AutomationId='num4Button']

zweiter
    Activate    ${calculator}
    Is Active    ${calculator}    should be    true

    Activate    ${calculator.n1}
    ${isactive}    Is Active    ${calculator}

    Activate    ${calculator.n2}
    Activate    ${calculator.n3}
    Activate    ${calculator.n4}
    Activate    ${calculator.n5}
    Activate    ${calculator.plus}
    Activate    ${calculator.n7}
    Activate    ${calculator.n8}
    Activate    ${calculator.n9}
    Activate    ${calculator.n0}
    Get Text    ${calculator.result}    should be    1234567890

vierter
    Activate    Window[@AutomationId='Shell' and @ProcessName='WpfTestApp']//Button[@AutomationId='DoSomething']

fünfter
    Set Root Element    ${calculator}
    Activate    .//Button[@AutomationId='num1Button']
    Activate    .//Button[@AutomationId='num2Button']
