﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.18444
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("EichsoftwareSyncClientVB.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Call GenerateFixSQLScriptMain^
        ''' h2223265.stratoserver.net,^
        ''' Herstellerersteichung,^
        ''' Eichen,^
        ''' Eichen2013,^
        ''' WIN7MOBDEV01,^
        ''' Herstellerersteichung,^
        ''' sa,^
        ''' Test1234,^
        ''' 1,^
        ''' 1,^
        ''' 3,^
        ''' &quot;ServerVerbindungsprotokoll,ServerLookupVertragspartnerFirma,ServerKonfiguration,Servereichmarkenverwaltung,Firmen,ServerFirmenZusatzdaten,Benutzer,ServerLizensierung,ServerLookup_Waagenart,ServerKompatiblitaetsnachweis,ServerBeschaffenheitspruefung,ServerLookup_Vorgangsstatus,ServerLookup_Auswertegeraet,ServerLook [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property Call_DTS() As String
            Get
                Return ResourceManager.GetString("Call_DTS", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to @echo off
        '''Rem **********************************************************************
        '''Rem FILE NAME    : GenerateFixSQLScriptMain.sql
        '''Rem AUTHOR       : Ganesan. K
        '''Rem CREATED      : 06-Jul-2007
        '''Rem DESCRIPTION  :  This batch file will generate SQL script for 
        '''Rem		    offline synchronization
        '''Rem 
        '''Rem **********************************************************************
        '''
        '''Rem **********************************************************************
        '''Rem Parameters details
        '''Rem 
        '''Rem 1. Source Server
        ''' [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property GenerateFixSQLScriptMain() As String
            Get
                Return ResourceManager.GetString("GenerateFixSQLScriptMain", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to @echo off
        '''Rem **********************************************************************
        '''Rem FILE NAME    : GenerateFixSQLScriptSub.sql
        '''Rem AUTHOR       : Ganesan. K
        '''Rem CREATED      : 06-Jul-2007
        '''Rem DESCRIPTION  : This batch file will be called from GenerateFixSQLScriptMain
        '''Rem		    batch file to generate the Fix SQL Script
        '''Rem 
        '''Rem **********************************************************************
        '''
        '''Rem **********************************************************************
        '''Rem Parameters detail [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property GenerateFixSQLScriptSub() As String
            Get
                Return ResourceManager.GetString("GenerateFixSQLScriptSub", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to @echo off
        '''Rem **********************************************************************
        '''Rem FILE NAME    : offline_synchronization.bat
        '''Rem AUTHOR       : Ganesan. K
        '''Rem CREATED      : 06-Jul-2007
        '''Rem DESCRIPTION  : This batch file will perform synchronization
        '''Rem 
        '''Rem **********************************************************************
        '''
        '''Rem **********************************************************************
        '''Rem Parameters details
        '''Rem 
        '''Rem 1. Destination Server
        '''Rem 2. Destination Database
        '''Rem [rest of string was truncated]&quot;;.
        '''</summary>
        Friend ReadOnly Property Offline_Synchronization() As String
            Get
                Return ResourceManager.GetString("Offline_Synchronization", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized resource of type System.Byte[].
        '''</summary>
        Friend ReadOnly Property tablediff() As Byte()
            Get
                Dim obj As Object = ResourceManager.GetObject("tablediff", resourceCulture)
                Return CType(obj,Byte())
            End Get
        End Property
    End Module
End Namespace