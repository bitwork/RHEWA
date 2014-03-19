@echo off
Rem **********************************************************************
Rem FILE NAME    : offline_synchronization.bat
Rem AUTHOR       : Ganesan. K
Rem CREATED      : 06-Jul-2007
Rem DESCRIPTION  : This batch file will perform synchronization
Rem 
Rem **********************************************************************

Rem **********************************************************************
Rem Parameters details
Rem 
Rem 1. Destination Server
Rem 2. Destination Database
Rem 3. Destination User
Rem 4. Destination Password
Rem 5. Connection Timeouts
Rem 
Rem **********************************************************************

Rem Retrieve parameters and assign them into local variables
set DestinationServer=%1
set DestinationDatabase=%2
set DestinationUser=%3
set DestinationPassword=%4
set ConnectionTimeouts=%5

Rem echo ****************************************
Rem echo Listing Input parameters
Rem echo ****************************************
Rem echo Destination Server = %DestinationServer%
Rem echo Destination Database = %DestinationDatabase%
Rem echo Destination User = %DestinationUser%
Rem echo Destination Password = %DestinationPassword%
Rem echo Connection Timeouts = %ConnectionTimeouts%
Rem echo ****************************************

Rem Declare and initialize local variables
set FixSQLFile=FixSQLFile.sql
Set SynchronizationLogFile=OfflineSync.log
set Command=
set WHITESPACE= 

Rem Construct the SQL command to perform offliine synchronization
set command=sqlcmd%WHITESPACE%-U%WHITESPACE%%DestinationUser%
set command=%command%%WHITESPACE%-P%WHITESPACE%%DestinationPassword%
set command=%command%%WHITESPACE%-S%WHITESPACE%%DestinationServer%
set command=%command%%WHITESPACE%-d%WHITESPACE%%DestinationDatabase%
set command=%command%%WHITESPACE%-i%WHITESPACE%%FixSQLFile%
set command=%command%%WHITESPACE%-l%WHITESPACE%%ConnectionTimeouts%
set command=%command%%WHITESPACE%-o%WHITESPACE%%SynchronizationLogFile%

Rem Perform offline synchronization
%command%

Rem Return the Errorlevel to the called procedure
@echo on & echo %errorlevel% & exit /B  %errorlevel%

