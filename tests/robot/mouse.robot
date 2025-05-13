*** Settings ***
Library     PlatynUI


*** Variables ***
&{CALCULATOR}
...                 button_1=app:Application[@Name='__main__.py']//PushButton[@Name='1']
...                 button_2=app:Application[@Name='__main__.py']//PushButton[@Name='2']
...                 button_3=app:Application[@Name='__main__.py']//PushButton[@Name='3']

${BUTTON1}          app:Application[@Name='__main__.py']//PushButton[@Name='1']


*** Test Cases ***
first
    Sleep    2
    Mouse Click    ${CALCULATOR.button_1}

    # Activate    app:Application[@Name='__main__.py']/Frame
    Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='C']
    Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='1']
    Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='2']
    Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='+']
    Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='3']
    Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='4']

keyboard
    # Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='C']
    Type Keys    app:Application[@Name='__main__.py']//Text[@AccessibleId='display']    12345+6789
    Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='=']
    Activate    app:Application[@Name='__main__.py']//PushButton[@Name='C']

keyboard set text
    Set Text    app:Application[@Name='__main__.py']//Text[@AccessibleId='display']    12345+6789
    Activate    app:Application[@Name='__main__.py']//PushButton[@Name='=']
    ${text}    Get Text    app:Application[@Name='__main__.py']//Text[@AccessibleId='display']
    Log    ${text}
    Get Text    app:Application[@Name='__main__.py']//Text[@AccessibleId='display']    ==    19134
    Get Text    app:Application[@Name='__main__.py']//Text[@AccessibleId='display']    ==    19

    Activate    app:Application[@Name='__main__.py']//PushButton[@Name='C']

second
    Clear Input
    Enter Number    12345
    Select Operator    +
    Enter Number    6789
    Calculate Result
    Result Should Be    19134

third
    Activate    app:Application[@Name='kate']/Frame/MenuBar/MenuItem[@Name='File']
    Activate    app:Application[@Name='kate']/Frame/MenuBar/MenuItem[@Name='File']//MenuItem[@Name='Neu']


*** Keywords ***
Enter Number
    [Arguments]    ${numbers}
    FOR    ${n}    IN    @{{[*$numbers]}}
        Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='${n}']
    END

Clear Input
    Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='C']

Select Operator
    [Arguments]    ${arg1}
    Mouse Click    app:Application[@Name='__main__.py']//PushButton[@Name='${arg1}']

Calculate Result
    # TODO: implement keyword "Calculate Result".
    Activate    app:Application[@Name='__main__.py']//PushButton[@Name='=']

Result Should Be
    [Arguments]    ${arg1}
    Get Text    app:Application[@Name='__main__.py']//Text[@AccessibleId='display']    ==    ${arg1}
