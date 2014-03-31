@echo off
Rem **********************************************************************
Rem FILE NAME    : GenerateFixSQLScriptMain.sql
Rem AUTHOR       : Ganesan. K
Rem CREATED      : 06-Jul-2007
Rem DESCRIPTION  :  This batch file will generate SQL script for 
Rem		    offline synchronization
Rem 
Rem **********************************************************************

Rem **********************************************************************
Rem Parameters details
Rem 
Rem 1. Source Server
Rem 2. Source Database
Rem 3. Source User
Rem 4. Source Password
Rem 5. Destination Server
Rem 6. Destination Database
Rem 7. Destination User
Rem 8. Destination Password
Rem 9. Number Of Retries
Rem 10.Retrty Interval
Rem 11.Connection Timeouts
Rem 12.Tables To Be Synchronized
Rem 
Rem **********************************************************************

Rem Retrieve parameters and assign them into local variables
set SourceServer=%1
set SourceDatabase=%2
set SourceUser=%3
set SourcePassword=%4
set DestinationServer=%5
set DestinationDatabase=%6
set DestinationUser=%7
set DestinationPassword=%8
set NumberOfRetries=%9
shift
set RetrtyInterval=%9
shift
set ConnectionTimeouts=%9
shift
set TablesToBeSynchronized=%9

Rem echo ****************************************
Rem echo Listing Input parameters
Rem echo ****************************************
Rem echo Source Server = %SourceServer%
Rem echo Source Database = %SourceDatabase%
Rem echo Source User = %SourceUser%
Rem echo Source Password = %SourcePassword%
Rem echo Destination Server = %DestinationServer%
Rem echo Destination Database = %DestinationDatabase%
Rem echo Destination User = %DestinationUser%
Rem echo Destination Password = %DestinationPassword%
Rem echo Number Of Retries = %NumberOfRetries%
Rem echo Retrty Interval = %RetrtyInterval%
Rem echo Connection Timeouts = %ConnectionTimeouts%
Rem echo Tables To Be Synchronized = %TablesToBeSynchronized%
Rem echo ****************************************

Rem Declare and initialize local variables
set FixSQLTempFile=FixSQLTempFile.sql
set FixSQLFile=FixSQLFile.sql
Set SynchronizationLogFile=OfflineSync.log
set TableDiff=tablediff.exe
set Command=
set WHITESPACE= 
set GenerateFixSQLScript=GenerateFixSQLScriptSub.bat

Rem delete the FixSQL & log file if it already exists
if exist %FixSQLFile% del %FixSQLFile%
if exist %SynchronizationLogFile% del %SynchronizationLogFile%

Rem Construct tablediff command with static parameter values
set TableDiff=%TableDiff%%WHITESPACE%-sourceserver%WHITESPACE%%SourceServer%
set TableDiff=%TableDiff%%WHITESPACE%-sourcedatabase%WHITESPACE%%SourceDatabase%
set TableDiff=%TableDiff%%WHITESPACE%-sourceuser%WHITESPACE%%SourceUser%
set TableDiff=%TableDiff%%WHITESPACE%-sourcepassword%WHITESPACE%%SourcePassword%
set TableDiff=%TableDiff%%WHITESPACE%-sourcelocked
set TableDiff=%TableDiff%%WHITESPACE%-destinationserver%WHITESPACE%%DestinationServer%
set TableDiff=%TableDiff%%WHITESPACE%-destinationdatabase%WHITESPACE%%DestinationDatabase%
set TableDiff=%TableDiff%%WHITESPACE%-destinationuser%WHITESPACE%%DestinationUser%
set TableDiff=%TableDiff%%WHITESPACE%-destinationpassword%WHITESPACE%%DestinationPassword%
set TableDiff=%TableDiff%%WHITESPACE%-destinationlocked
set TableDiff=%TableDiff%%WHITESPACE%-f%WHITESPACE%%FixSQLTempFile%
set TableDiff=%TableDiff%%WHITESPACE%-o%WHITESPACE%%SynchronizationLogFile%
set TableDiff=%TableDiff%%WHITESPACE%-rc%WHITESPACE%%NumberOfRetries%
set TableDiff=%TableDiff%%WHITESPACE%-ri%WHITESPACE%%RetrtyInterval%
set TableDiff=%TableDiff%%WHITESPACE%-t%WHITESPACE%%ConnectionTimeouts%
Rem set TableDiff=%TableDiff%%WHITESPACE%-strict

Rem Remove double quote from the varibale TablesToBeSynchronized
for /F "delims=" %%v in (%TablesToBeSynchronized%) do set TablesToBeSynchronizedWithoutQuote=%%v

Rem Generate the Fix SQL script for all tables to be syncrhonized
Rem Abort generating the Fix SQL Script and return to called procedure when an error(||) encountered at any point of time
for %%v in (%TablesToBeSynchronizedWithoutQuote%) do CALL %GenerateFixSQLScript%%WHITESPACE%%%v || (@echo on & echo 1 & exit /B 1)

Rem delete the temporary FixSQL file 
if exist %FixSQLTempFile% del %FixSQLTempFile%

Rem Return success
@echo on & echo 0 & exit /B 0
