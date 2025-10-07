*** Settings ***
Library     PlatynUI


*** Test Cases ***

Check Dynamic Content
    Mouse Click    app:Application[@Name="AvaloniaTestApp"]/MainWindow//Button[@AutomationId='AddButtonCommand']
    Sleep    1s
    Mouse Click    app:Application[@Name="AvaloniaTestApp"]/MainWindow//Button[@AutomationId='DynamicRemoveButton']