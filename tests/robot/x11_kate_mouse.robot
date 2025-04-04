*** Settings ***
Library     PlatynUI


*** Test Cases ***
Test X11 Mouse Position
    [Documentation]    Simple test case to demonstrate mouse movement in x11.
    ...    Open text editor "kate" in fullscreen before running the test.
    ...    Test shows movement over menubar and clicking on "new document" afterwards
    Move To    .    x=0    y=85
    Move To    .    x=100    y=85
    Move To    .    x=200    y=85
    Move To    .    x=300    y=85
    Move To    .    x=400    y=85
    Move To    .    x=500    y=85
    Move To    .    x=600    y=85
    Move To    .    x=700    y=85
    Move To    .    x=800    y=85
    Move To    .    x=10    y=85
    Click    .    x=10    y=110
