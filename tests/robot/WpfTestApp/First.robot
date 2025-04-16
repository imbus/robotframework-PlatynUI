*** Settings ***
Library         PlatynUI
Variables       mapping.py
Test Tags    wip

*** Test Cases ***
first
    Activate    Window[@AutomationId='Shell' and @ProcessName='WpfTestApp']//Button[@AutomationId='DoSomethingOther']
    Activate
    ...    Window[@AutomationId='Shell' and @ProcessName='WpfTestApp']//Window[@ClassName='#32770']//Button[@Name='OK']
    Click    Window[@AutomationId='Shell' and @ProcessName='WpfTestApp']//Button[@AutomationId='DoSomethingOther']

second
    Activate    ${Shell}
    Activate    ${Shell.DoSomethingOther}
