*** Settings ***
Library     PlatynUI
Test Tags    wip

*** Variables ***
${ROOT_PATH}        /app:Application[@Name="explorer"]

${SEARCH_BUTTON}    ${ROOT_PATH}//Button[@AutomationId="SearchButton"]


*** Test Cases ***
zero
    # Ensure Exists    /Pane[ends-with(@Name, 'Word - \\\\Remote') and @ClassName='Transparent Windows Client']    30s
    Log    ${ROOT_PATH}
#    Type Keys    /Pane[ends-with(@Name, 'Word - \\\\Remote') and @ClassName='Transparent Windows Client']    <Alt+F4>
#    Type Keys
#    ...    /Pane[ends-with(@Name, 'Word - \\\\Remote') and @ClassName='Transparent Windows Client']
#    ...    <Escape>

first
    Ensure Exists    Window[@Name='Rechner']    30s

    # Type Keys    Window[@Name='Rechner']    <Alt+F4>

    # Is Active    Window[@Name='Rechner']    should be    true

    Activate    Window[@Name='Rechner']//Button[@AutomationId="num1Button"]

    ${isactive}    Is Active    Window[@Name='Rechner']

    Activate    Window[@Name='Rechner']//Button[@AutomationId="num2Button"]
    Activate    Window[@Name='Rechner']//Button[@AutomationId="num3Button"]
    Activate    Window[@Name='Rechner']//Button[@AutomationId="num4Button"]
    # Activate    Window[@Name='Rechner']//Button[@AutomationId="MemRecall"]
    Activate    Window[@Name='Rechner']//Button[@AutomationId="num5Button"]
    Activate    Window[@Name='Rechner']//Button[@AutomationId="num6Button"]
    Activate    Window[@Name='Rechner']//Button[@AutomationId="num7Button"]
    Activate    Window[@Name='Rechner']//Button[@AutomationId="num8Button"]
    Activate    Window[@Name='Rechner']//Button[@AutomationId="num9Button"]
    Activate    Window[@Name='Rechner']//Button[@AutomationId="num0Button"]

second
    # Ensure Exists    Window[@Name='Rechner']    30s

    # Type Keys    Window[@Name='Rechner']    <Alt+F4>

    Set Root Element    Window[@Name='Rechner']
    TRY
        Is Active    .
    EXCEPT    AdapterNotFoundError.*    type=regexp
        Log    AdapterNotFoundError
    END

    do something

    Activate    .//Button[@AutomationId="num1Button"]

    ${isactive}    Is Active    .

    Activate    .//Button[@AutomationId="num2Button"]
    Activate    .//Button[@AutomationId="num3Button"]
    Activate    .//Button[@AutomationId="num4Button"]
    # Activate    //Button[@AutomationId="MemRecall"]
    Activate    .//Button[@AutomationId="num5Button"]
    Activate    .//Button[@AutomationId="num6Button"]
    Activate    .//Button[@AutomationId="num7Button"]
    Activate    .//Button[@AutomationId="num8Button"]
    Activate    .//Button[@AutomationId="num9Button"]
    Activate    .//Button[@AutomationId="num0Button"]

third
    Set Root Element    Window[@AutomationId='Shell' and @ProcessName='WpfTestApp']

    Activate    .//Button[@AutomationId='DoSomething']

    Activate    ./Window//Button


*** Keywords ***
do something
    Log Variables
    Set Root Element    Window[@AutomationId='Shell' and @ProcessName='WpfTestApp']

    Activate    .//Button[@AutomationId='DoSomething']
    Activate    ./Window//Button
