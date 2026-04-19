*** Settings ***
Library         PlatynUI
Library         Collections
Variables       mapping.py
Test Setup      Check Calculator And Zehnertastatur Exist

*** Test Cases ***
Click All Calculator Digits Via Children
    ${digit_buttons}=    Collect Digit Buttons From Children    /Window[@Name='Rechner']//Group[@Name="Zehnertastatur"]
    Length Should Be    ${digit_buttons}    10

    FOR    ${button}    IN    @{digit_buttons}
        Activate    ${button}
    END


Test Get Children
    ${children}=    Get Children    /Window[@Name='Rechner']//Group[@Name="Zehnertastatur"]
    ${count}=    Get Length    ${children}
    
    Log    Found ${count} children in Zehnertastatur group    INFO
    
    # Log properties of first few children
    FOR    ${idx}    ${child}    IN ENUMERATE    @{children}
        ${aid}=    Get Property Value    ${child}    AutomationId
        Log    Child ${idx}: AutomationId=${aid}    INFO
        ${names}=    Get Property Names    ${child}   
        Log    Child ${idx}: Property Names=${names}    INFO
        ${break}=    Evaluate    ${idx} >= 4
        IF    ${break}
            BREAK
        END
    END
    
    # Should find at least the buttons (0-9 plus decimal point)
    Length Should Be    ${children}    11

Test Get Children Locators
    
    # Get Children Locators returns XPath strings
    ${children}=    Get Children Locators    /Window[@Name='Rechner']//Group[@Name="Zehnertastatur"]
    ${count}=    Get Length    ${children}
    Log    Found ${count} children in Zehnertastatur group    INFO

    # Log each child locator
    FOR    ${idx}    ${child}    IN ENUMERATE    @{children}
        Log    ${idx}: ${child}    INFO
    END

    # Should find 11 buttons (0-9 plus decimal point)
    Length Should Be    ${children}    11

    # Each locator is a usable XPath string
    FOR    ${child}    IN    @{children}
        ${aid}=    Get Property Value    ${child}    AutomationId
        Log    ${child} -> AutomationId=${aid}    INFO
    END


*** Keywords ***
Collect Digit Buttons From Children
    [Arguments]    ${parent}

    ${children}=    Get Children    ${parent}
    log  LIST OF CHILDREN: ${children}
    
    VAR    @{digit_buttons}
    ${child_count}=    Get Length    ${children}

    # Build an ordered list num0Button..num9Button from all children.
    FOR    ${digit}    IN RANGE    0    10
        VAR    ${target_id}    num${digit}Button
        FOR    ${child}    IN    @{children}
            ${automation_id}=    Get Property Value    ${child}    AutomationId
            IF    '${automation_id}' == '${target_id}'
                Append To List    ${digit_buttons}    ${child}
                BREAK
            END
        END
    END

    RETURN    ${digit_buttons}


Check Calculator And Zehnertastatur Exist
    Ensure Exists    ${calculator}    
    Ensure Exists    /Window[@Name='Rechner']//Group[@Name="Zehnertastatur"]    
    Activate     /Window[@Name='Rechner']//Button[@Name='Löschen']

