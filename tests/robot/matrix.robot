*** Settings ***
Documentation       Suite for testing the workflow matrix configuration


*** Variables ***
${PLATFORM}             ${{ platform.system() }}
${PLATFORM_VERSION}     ${{ platform.version() }}
${PYTHON_VERSION}       ${{ platform.python_version() }}
${ROBOT_VERSION}        ${{ robot.version.get_version() }}


*** Test Cases ***
Log Environment Information
    [Documentation]    Logs the OS, Python version, and Robot Framework version.
    Log    OS: ${PLATFORM} Version: ${PLATFORM_VERSION}
    Log    Python Version: ${PYTHON_VERSION}
    Log    Robot Framework Version: ${ROBOT_VERSION}
