@echo off
Rem **********************************************************************
Rem FILE NAME    : GenerateFixSQLScriptSub.sql
Rem AUTHOR       : Ganesan. K
Rem CREATED      : 06-Jul-2007
Rem DESCRIPTION  : This batch file will be called from GenerateFixSQLScriptMain
Rem		    batch file to generate the Fix SQL Script
Rem 
Rem **********************************************************************

Rem **********************************************************************
Rem Parameters details
Rem 
Rem 1. Table To Be Synchronized
Rem 
Rem **********************************************************************

Rem Retrieve parameters and assign them into local variables
set TableName=%1

Rem echo ****************************************
Rem echo Listing Input parameters
Rem echo ****************************************
Rem echo Table To Be Synchronized = %TableName%
Rem echo ****************************************

Rem Declare and initialize local variables
set Command=
set WHITESPACE= 

Rem Delete the FixSQLTempFile if it already exists
if exist %FixSQLTempFile% del %FixSQLTempFile%

Rem Construct tablediff command with dynamic parameter values
set Command=%TableDiff%%WHITESPACE%-sourcetable%WHITESPACE%%TableName%
set command=%command%%WHITESPACE%-destinationtable%WHITESPACE%%TableName%

Rem execute the tablediff command
%command%

Rem if an error found then exit this batch file with errorlevel
if %ERRORLEVEL%==1 exit /B 1

Rem Generate the SQL script by appending the FixSQL file with temporary FixSQL file
set Command=type%WHITESPACE%%FixSQLTempFile%%WHITESPACE%
%command%>>%FixSQLFile%

Rem Exit this batch file with return 0
exit /B 0
