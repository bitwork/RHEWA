Imports System.ComponentModel
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.UI
''' <summary>
''' hilfsklasse. Enthält Informationen und Einstellungen des aktuell angemeldeten Benutzers
''' </summary>
''' <remarks></remarks>
Public Class AktuellerBenutzer

    Private mvarLetztesUpdate As DateTime
    Private mvarAktuelleSprache As String
    Private mvarSynchronisierungsmodus As String
    Private mvarSyncAb As DateTime
    Private mvarSyncBis As DateTime
    Private mvarHoleAlleeigenenEichungenVomServer As Boolean
    Private mvarGridSettings As String
    Private mvarGridSettingsRHEWA As String

    Private Const mvarDefaultGridSettings As String = "PFJhZEdyaWRWaWV3IFNob3dOb0RhdGFUZXh0PSJGYWxzZSIgQXV0b1NpemVSb3dzPSJUcnVlIiBUZXh0PSJSYWRHcmlkVmlldzEiIEF1dG9TY3JvbGw9IlRydWUiIEN1cnNvcj0iRGVmYXVsdCIgVGFiSW5kZXg9IjAiPjxNYXN0ZXJUZW1wbGF0ZSBBbGxvd1Jvd1Jlc2l6ZT0iRmFsc2UiIEVuYWJsZUFsdGVybmF0aW5nUm93Q29sb3I9IlRydWUiIEFsbG93RWRpdFJvdz0iRmFsc2UiIEFsbG93Q29sdW1uQ2hvb3Nlcj0iRmFsc2UiIEFsbG93Q29sdW1uSGVhZGVyQ29udGV4dE1lbnU9IkZhbHNlIiBBbGxvd1Jvd0hlYWRlckNvbnRleHRNZW51PSJGYWxzZSIgQWxsb3dDZWxsQ29udGV4dE1lbnU9IkZhbHNlIiBBbGxvd0RlbGV0ZVJvdz0iRmFsc2UiIEFsbG93QWRkTmV3Um93PSJGYWxzZSIgQWxsb3dTZWFyY2hSb3c9IlRydWUiIFNob3dHcm91cGVkQ29sdW1ucz0iVHJ1ZSIgQXV0b0V4cGFuZEdyb3Vwcz0iVHJ1ZSI+PENvbHVtbnM+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uIFdpZHRoPSIyNTciIEZpZWxkTmFtZT0iU3RhdHVzIiBOYW1lPSJTdGF0dXMiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJTdGF0dXMiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uIFdpZHRoPSIxNDgiIEZpZWxkTmFtZT0iQmVhcmJlaXR1bmdzc3RhdHVzIiBOYW1lPSJCZWFyYmVpdHVuZ3NzdGF0dXMiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJCZWFyYmVpdHVuZ3NzdGF0dXMiPjxDb25kaXRpb25hbEZvcm1hdHRpbmdPYmplY3RMaXN0PjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkNvbmRpdGlvbmFsRm9ybWF0dGluZ09iamVjdCBUVmFsdWUxPSJGZWhsZXJoYWZ0IiBDZWxsRm9yZUNvbG9yPSIiIENlbGxCYWNrQ29sb3I9IiIgUm93Rm9yZUNvbG9yPSIiIFJvd0JhY2tDb2xvcj0iMjU0LCAxMjAsIDExMCIgTmFtZT0iRmVobGVyaGFmdCIgQXBwbHlUb1Jvdz0iVHJ1ZSIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5Db25kaXRpb25hbEZvcm1hdHRpbmdPYmplY3QgVFZhbHVlMT0iSW52YWxpZCIgQ2VsbEZvcmVDb2xvcj0iIiBDZWxsQmFja0NvbG9yPSIiIFJvd0ZvcmVDb2xvcj0iIiBSb3dCYWNrQ29sb3I9IjI1NCwgMTIwLCAxMTAiIE5hbWU9ImludmFsaWQiIEFwcGx5VG9Sb3c9IlRydWUiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuQ29uZGl0aW9uYWxGb3JtYXR0aW5nT2JqZWN0IFRWYWx1ZTE9IkLFgsSZZG5lIiBDZWxsRm9yZUNvbG9yPSIiIENlbGxCYWNrQ29sb3I9IiIgUm93Rm9yZUNvbG9yPSIiIFJvd0JhY2tDb2xvcj0iMjU0LCAxMjAsIDExMCIgTmFtZT0iaW52YWxpZFBsIiBBcHBseVRvUm93PSJUcnVlIiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkNvbmRpdGlvbmFsRm9ybWF0dGluZ09iamVjdCBUVmFsdWUxPSJHZW5laG1pZ3QiIENlbGxGb3JlQ29sb3I9IiIgQ2VsbEJhY2tDb2xvcj0iIiBSb3dGb3JlQ29sb3I9IiIgUm93QmFja0NvbG9yPSIyMDEsIDI1NSwgMTMyIiBOYW1lPSJHZW5laG1pZ3QiIEFwcGx5VG9Sb3c9IlRydWUiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuQ29uZGl0aW9uYWxGb3JtYXR0aW5nT2JqZWN0IFRWYWx1ZTE9IlZhbGlkIiBDZWxsRm9yZUNvbG9yPSIiIENlbGxCYWNrQ29sb3I9IiIgUm93Rm9yZUNvbG9yPSIiIFJvd0JhY2tDb2xvcj0iMjAxLCAyNTUsIDEzMiIgTmFtZT0iVmFsaWQiIEFwcGx5VG9Sb3c9IlRydWUiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuQ29uZGl0aW9uYWxGb3JtYXR0aW5nT2JqZWN0IFRWYWx1ZTE9IlphdHdpZXJkem9ubyIgQ2VsbEZvcmVDb2xvcj0iIiBDZWxsQmFja0NvbG9yPSIiIFJvd0ZvcmVDb2xvcj0iIiBSb3dCYWNrQ29sb3I9IjIwMSwgMjU1LCAxMzIiIE5hbWU9IlphdHdpZXJkem9ubyIgQXBwbHlUb1Jvdz0iVHJ1ZSIgLz48L0NvbmRpdGlvbmFsRm9ybWF0dGluZ09iamVjdExpc3Q+PC9UZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3VGV4dEJveENvbHVtbj48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld0RlY2ltYWxDb2x1bW4gRGF0YVR5cGU9IlN5c3RlbS5JbnQzMiIgRmllbGROYW1lPSJJRCIgTmFtZT0iSUQiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJGYWxzZSIgSGVhZGVyVGV4dD0iSUQiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uIEZpZWxkTmFtZT0iVm9yZ2FuZ3NudW1tZXIiIE5hbWU9IlZvcmdhbmdzbnVtbWVyIiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iRmFsc2UiIEhlYWRlclRleHQ9IlZvcmdhbmdzbnVtbWVyIiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3VGV4dEJveENvbHVtbiBXaWR0aD0iMTAxIiBGaWVsZE5hbWU9IkZhYnJpa251bW1lciIgTmFtZT0iRmFicmlrbnVtbWVyIiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iVHJ1ZSIgSGVhZGVyVGV4dD0iRmFicmlrbnVtbWVyIiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3VGV4dEJveENvbHVtbiBXaWR0aD0iMTIxIiBGaWVsZE5hbWU9Ikxvb2t1cF9XYWVnZXplbGxlIiBOYW1lPSJMb29rdXBfV2FlZ2V6ZWxsZSIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IldaIiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3VGV4dEJveENvbHVtbiBXaWR0aD0iODIiIEZpZWxkTmFtZT0iTG9va3VwX1dhYWdlbnR5cCIgTmFtZT0iTG9va3VwX1dhYWdlbnR5cCIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IldhYWdlbnR5cCIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjExMyIgRmllbGROYW1lPSJMb29rdXBfV2FhZ2VuYXJ0IiBOYW1lPSJMb29rdXBfV2FhZ2VuYXJ0IiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iVHJ1ZSIgSGVhZGVyVGV4dD0iV2FhZ2VuYXJ0IiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3VGV4dEJveENvbHVtbiBXaWR0aD0iNTMiIEZpZWxkTmFtZT0iTG9va3VwX0F1c3dlcnRlZ2VyYWV0IiBOYW1lPSJMb29rdXBfQXVzd2VydGVnZXJhZXQiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJBV0ciIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdDaGVja0JveENvbHVtbiBGaWVsZE5hbWU9IkF1c2dlYmxlbmRldCIgTmFtZT0iQXVzZ2VibGVuZGV0IiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iRmFsc2UiIEhlYWRlclRleHQ9IkF1c2dlYmxlbmRldCIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld0RhdGVUaW1lQ29sdW1uIERhdGFUeXBlPSJTeXN0ZW0uTnVsbGFibGVgMVtbU3lzdGVtLkRhdGVUaW1lLCBtc2NvcmxpYiwgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODldXSIgV2lkdGg9IjEzNCIgRmllbGROYW1lPSJCZWFyYmVpdHVuZ3NkYXR1bSIgTmFtZT0iQmVhcmJlaXR1bmdzZGF0dW0iIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJCZWFyYmVpdHVuZ3NkYXR1bSIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjM0OCIgRmllbGROYW1lPSJCZW1lcmt1bmciIE5hbWU9IkJlbWVya3VuZyIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IkJlbWVya3VuZyIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld0RhdGVUaW1lQ29sdW1uIERhdGFUeXBlPSJTeXN0ZW0uTnVsbGFibGVgMVtbU3lzdGVtLkRhdGVUaW1lLCBtc2NvcmxpYiwgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODldXSIgV2lkdGg9IjExNCIgRmllbGROYW1lPSJJZGVudGlmaWthdGlvbnNkYXRlbl9EYXR1bSIgTmFtZT0iSWRlbnRpZmlrYXRpb25zZGF0ZW5fRGF0dW0iIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJIS0IgRGF0dW0iIC8+PC9Db2x1bW5zPjxHcm91cERlc2NyaXB0b3JzPjxUZWxlcmlrLldpbkNvbnRyb2xzLkRhdGEuR3JvdXBEZXNjcmlwdG9yPjxHcm91cE5hbWVzPjxUZWxlcmlrLldpbkNvbnRyb2xzLkRhdGEuU29ydERlc2NyaXB0b3IgUHJvcGVydHlOYW1lPSJCZWFyYmVpdHVuZ3NzdGF0dXMiIC8+PC9Hcm91cE5hbWVzPjwvVGVsZXJpay5XaW5Db250cm9scy5EYXRhLkdyb3VwRGVzY3JpcHRvcj48L0dyb3VwRGVzY3JpcHRvcnM+PFZpZXdEZWZpbml0aW9uIHhzaTp0eXBlPSJUZWxlcmlrLldpbkNvbnRyb2xzLlVJLlRhYmxlVmlld0RlZmluaXRpb24iIHhtbG5zOnhzaT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiIC8+PC9NYXN0ZXJUZW1wbGF0ZT48L1JhZEdyaWRWaWV3Pg=="
    Private Const mvarDefaultGridSettingsRHEWA As String = "PFJhZEdyaWRWaWV3IFNob3dOb0RhdGFUZXh0PSJGYWxzZSIgQXV0b1NpemVSb3dzPSJUcnVlIiBTaG93SGVhZGVyQ2VsbEJ1dHRvbnM9IlRydWUiIFRleHQ9IlJhZEdyaWRWaWV3MSIgQXV0b1Njcm9sbD0iVHJ1ZSIgQ3Vyc29yPSJEZWZhdWx0IiBUYWJJbmRleD0iNSI+PE1hc3RlclRlbXBsYXRlIEFsbG93Um93UmVzaXplPSJGYWxzZSIgQWxsb3dFZGl0Um93PSJGYWxzZSIgQWxsb3dDZWxsQ29udGV4dE1lbnU9IkZhbHNlIiBBbGxvd0RlbGV0ZVJvdz0iRmFsc2UiIEFsbG93QWRkTmV3Um93PSJGYWxzZSIgRW5hYmxlRmlsdGVyaW5nPSJUcnVlIiBTaG93R3JvdXBlZENvbHVtbnM9IlRydWUiIEF1dG9FeHBhbmRHcm91cHM9IlRydWUiIFNob3dIZWFkZXJDZWxsQnV0dG9ucz0iVHJ1ZSI+PENvbHVtbnM+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uIFdpZHRoPSI2NyIgRmllbGROYW1lPSJBV0ciIE5hbWU9IkFXRyIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IkFXRyIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9Ijc3IiBGaWVsZE5hbWU9IkFuaGFuZ1BmYWQiIE5hbWU9IkFuaGFuZ1BmYWQiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJBbmhhbmciIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uIFdpZHRoPSIxNDgiIEZpZWxkTmFtZT0iQmVhcmJlaXR1bmdzc3RhdHVzIiBOYW1lPSJCZWFyYmVpdHVuZ3NzdGF0dXMiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJCZWFyYmVpdHVuZ3NzdGF0dXMiPjxDb25kaXRpb25hbEZvcm1hdHRpbmdPYmplY3RMaXN0PjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkNvbmRpdGlvbmFsRm9ybWF0dGluZ09iamVjdCBUVmFsdWUxPSJGZWhsZXJoYWZ0IiBDZWxsRm9yZUNvbG9yPSIiIENlbGxCYWNrQ29sb3I9IiIgUm93Rm9yZUNvbG9yPSIiIFJvd0JhY2tDb2xvcj0iMjU0LCAxMjAsIDExMCIgTmFtZT0iRmVobGVyaGFmdCIgQXBwbHlUb1Jvdz0iVHJ1ZSIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5Db25kaXRpb25hbEZvcm1hdHRpbmdPYmplY3QgVFZhbHVlMT0iR2VuZWhtaWd0IiBDZWxsRm9yZUNvbG9yPSIiIENlbGxCYWNrQ29sb3I9IiIgUm93Rm9yZUNvbG9yPSIiIFJvd0JhY2tDb2xvcj0iMjAxLCAyNTUsIDEzMiIgTmFtZT0iR2VuZWhtaWd0IiBBcHBseVRvUm93PSJUcnVlIiAvPjwvQ29uZGl0aW9uYWxGb3JtYXR0aW5nT2JqZWN0TGlzdD48L1RlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3VGV4dEJveENvbHVtbiBXaWR0aD0iMzMxIiBGaWVsZE5hbWU9IkJlbWVya3VuZyIgTmFtZT0iQmVtZXJrdW5nIiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iVHJ1ZSIgSGVhZGVyVGV4dD0iQmVtZXJrdW5nIiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3VGV4dEJveENvbHVtbiBXaWR0aD0iMjkxIiBGaWVsZE5hbWU9IkVpY2hiZXZvbGxtYWVjaHRpZ3RlciIgTmFtZT0iRWljaGJldm9sbG1hZWNodGlndGVyIiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iVHJ1ZSIgSGVhZGVyVGV4dD0iS29uZm9ybWl0w6R0c2Jld2VydHVuZ3NiZXZvbGxtw6RjaHRpZ3RlciIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjI3MCIgRmllbGROYW1lPSJGYWJyaWtudW1tZXIiIE5hbWU9IkZhYnJpa251bW1lciIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IkZhYnJpa251bW1lciIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjEyMiIgRmllbGROYW1lPSJHZXNwZXJydER1cmNoIiBOYW1lPSJHZXNwZXJydER1cmNoIiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iVHJ1ZSIgSGVhZGVyVGV4dD0iR2VzcGVycnQgZHVyY2giIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdEYXRlVGltZUNvbHVtbiBGb3JtYXRTdHJpbmc9InswOmRkLk1NLnl5eXl9IiBXaWR0aD0iMTAxIiBGaWVsZE5hbWU9IkhLQkRhdHVtIiBOYW1lPSJIS0JEYXR1bSIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IkhLQiBEYXR1bSIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjY3IiBGaWVsZE5hbWU9IklEIiBOYW1lPSJJRCIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IkZhbHNlIiBIZWFkZXJUZXh0PSJJRCIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld0NoZWNrQm94Q29sdW1uIFdpZHRoPSIxMDUiIEZpZWxkTmFtZT0iTmV1ZVdaIiBOYW1lPSJOZXVlV1oiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJOZXVlIFdaIiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3VGV4dEJveENvbHVtbiBXaWR0aD0iNDI2IiBGaWVsZE5hbWU9IlBydWVmc2NoZWlubnVtbWVyIiBOYW1lPSJQcnVlZnNjaGVpbm51bW1lciIgSXNBdXRvR2VuZXJhdGVkPSJUcnVlIiBJc1Zpc2libGU9IlRydWUiIEhlYWRlclRleHQ9IlBydWVmc2NoZWlubnVtbWVyIiAvPjxUZWxlcmlrLldpbkNvbnRyb2xzLlVJLkdyaWRWaWV3RGF0ZVRpbWVDb2x1bW4gV2lkdGg9IjExNSIgRmllbGROYW1lPSJVcGxvYWRkYXR1bSIgTmFtZT0iVXBsb2FkZGF0dW0iIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJVcGxvYWRkYXR1bSIgLz48VGVsZXJpay5XaW5Db250cm9scy5VSS5HcmlkVmlld1RleHRCb3hDb2x1bW4gV2lkdGg9IjE0MiIgRmllbGROYW1lPSJWb3JnYW5nc251bW1lciIgTmFtZT0iVm9yZ2FuZ3NudW1tZXIiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJGYWxzZSIgSGVhZGVyVGV4dD0iVm9yZ2FuZ3NudW1tZXIiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uIFdpZHRoPSIxMjciIEZpZWxkTmFtZT0iV1oiIE5hbWU9IldaIiBJc0F1dG9HZW5lcmF0ZWQ9IlRydWUiIElzVmlzaWJsZT0iVHJ1ZSIgSGVhZGVyVGV4dD0iV1oiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uIFdpZHRoPSIxMDMiIEZpZWxkTmFtZT0iV2FhZ2VuYXJ0IiBOYW1lPSJXYWFnZW5hcnQiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJXYWFnZW5hcnQiIC8+PFRlbGVyaWsuV2luQ29udHJvbHMuVUkuR3JpZFZpZXdUZXh0Qm94Q29sdW1uIFdpZHRoPSIxMDAiIEZpZWxkTmFtZT0iV2FhZ2VudHlwIiBOYW1lPSJXYWFnZW50eXAiIElzQXV0b0dlbmVyYXRlZD0iVHJ1ZSIgSXNWaXNpYmxlPSJUcnVlIiBIZWFkZXJUZXh0PSJXYWFnZW50eXAiIC8+PC9Db2x1bW5zPjxHcm91cERlc2NyaXB0b3JzPjxUZWxlcmlrLldpbkNvbnRyb2xzLkRhdGEuR3JvdXBEZXNjcmlwdG9yPjxHcm91cE5hbWVzPjxUZWxlcmlrLldpbkNvbnRyb2xzLkRhdGEuU29ydERlc2NyaXB0b3IgUHJvcGVydHlOYW1lPSJCZWFyYmVpdHVuZ3NzdGF0dXMiIC8+PC9Hcm91cE5hbWVzPjwvVGVsZXJpay5XaW5Db250cm9scy5EYXRhLkdyb3VwRGVzY3JpcHRvcj48L0dyb3VwRGVzY3JpcHRvcnM+PFZpZXdEZWZpbml0aW9uIHhzaTp0eXBlPSJUZWxlcmlrLldpbkNvbnRyb2xzLlVJLlRhYmxlVmlld0RlZmluaXRpb24iIHhtbG5zOnhzaT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS9YTUxTY2hlbWEtaW5zdGFuY2UiIC8+PC9NYXN0ZXJUZW1wbGF0ZT48L1JhZEdyaWRWaWV3Pg=="

    Private mvarObjLizenz As Lizensierung

    ''' <summary>
    ''' Obsolete. Keine Singleton Instanz mehr im eigentlichen Sinne
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared mobjSingletonObject As AktuellerBenutzer
    ''' <summary>
    ''' Gets the Lizenz.
    ''' </summary>
    ''' <value>The  lizenz.</value>
    Public ReadOnly Property Lizenz As Lizensierung
        Get
            Return mvarObjLizenz
        End Get
    End Property

    ''' <summary>
    ''' Gets the  letztes update.
    ''' </summary>
    ''' <value>The  letztes update.</value>
    Public Property LetztesUpdate As DateTime
        Get
            Return mvarLetztesUpdate
        End Get
        Set(value As DateTime)
            mvarLetztesUpdate = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  aktuelle sprache.
    ''' </summary>
    ''' <value>The  aktuelle sprache.</value>
    Public Property AktuelleSprache As String
        Get
            If String.IsNullOrEmpty(mvarAktuelleSprache) Then
                Return "en"
            End If
            Return mvarAktuelleSprache
        End Get
        Set(value As String)
            mvarAktuelleSprache = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the synchronisierungsmodus.
    ''' </summary>
    ''' <value>The  synchronisierungsmodus.</value>
    Public Property Synchronisierungsmodus As String
        Get
            Return mvarSynchronisierungsmodus
        End Get
        Set(value As String)
            'speichern in Konfig DB
            mvarSynchronisierungsmodus = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  sync ab.
    ''' </summary>
    ''' <value>The  sync ab.</value>
    Public Property SyncAb As DateTime
        Get
            Return mvarSyncAb
        End Get
        Set(value As DateTime)
            mvarSyncAb = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  sync bis.
    ''' </summary>
    ''' <value>The  sync bis.</value>
    Public Property SyncBis As DateTime
        Get
            Return mvarSyncBis
        End Get
        Set(value As DateTime)
            mvarSyncBis = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  hole alleeigenen eichungen vom server.
    ''' </summary>
    ''' <value>The  hole alleeigenen eichungen vom server.</value>
    Public Property HoleAlleeigenenEichungenVomServer As Boolean
        Get
            Return mvarHoleAlleeigenenEichungenVomServer
        End Get
        Set(value As Boolean)
            mvarHoleAlleeigenenEichungenVomServer = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  grid settings.
    ''' </summary>
    ''' <value>The  grid settings.</value>
    Public Property GridSettings As String
        Get
            Return mvarGridSettings
        End Get
        Set(value As String)
            mvarGridSettings = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  grid settings rhewa.
    ''' </summary>
    ''' <value>The  grid settings rhewa.</value>
    Public Property GridSettingsRhewa As String
        Get
            Return mvarGridSettingsRHEWA
        End Get
        Set(value As String)
            mvarGridSettingsRHEWA = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the  grid settings.
    ''' </summary>
    ''' <value>The  grid settings.</value>
    Public ReadOnly Property GridDefaultSettings As String
        Get
            Return mvarDefaultGridSettings
        End Get
    End Property

    ''' <summary>
    ''' Gets the  grid settings rhewa.
    ''' </summary>
    ''' <value>The  grid settings rhewa.</value>
    Public ReadOnly Property GridDefaultSettingsRhewa As String
        Get
            Return mvarDefaultGridSettingsRHEWA
        End Get
    End Property

    Public Shared ReadOnly Property Instance As AktuellerBenutzer
        Get
            Return mobjSingletonObject
        End Get
    End Property

    ''' <summary>
    ''' Lädt das Benutzerobjekt
    ''' </summary>
    ''' <param name="pLizenzschluessel"></param>
    ''' <returns></returns>
    Public Shared Function GetNewInstance(ByVal pLizenzschluessel As String)
        mobjSingletonObject = New AktuellerBenutzer
        mobjSingletonObject.mvarObjLizenz = clsDBFunctions.HoleLizenzObjekt(pLizenzschluessel)

        If mobjSingletonObject.mvarObjLizenz Is Nothing Then
            Return Nothing
        End If

        Using Context As New Entities
            Context.Configuration.LazyLoadingEnabled = True
            Dim Konfig = (From Konfiguration In Context.Konfiguration Where Konfiguration.BenutzerLizenz = mobjSingletonObject.mvarObjLizenz.Lizenzschluessel).FirstOrDefault
            If Konfig Is Nothing Then
                mobjSingletonObject.mvarAktuelleSprache = "en"
                mobjSingletonObject.AktuelleSprache = "en"
                mobjSingletonObject.HoleAlleeigenenEichungenVomServer = True
                mobjSingletonObject.LetztesUpdate = New Date(2000, 1, 1)
                mobjSingletonObject.SyncAb = New Date(2000, 1, 1)
                mobjSingletonObject.SyncBis = New Date(9999, 12, 31)
                mobjSingletonObject.Synchronisierungsmodus = "Alles"
                mobjSingletonObject.GridSettings = mvarDefaultGridSettings
                mobjSingletonObject.GridSettingsRhewa = mvarDefaultGridSettingsRHEWA
            Else

                mobjSingletonObject.AktuelleSprache = Konfig.AktuelleSprache
                mobjSingletonObject.GridSettings = Konfig.GridSettings
                mobjSingletonObject.GridSettingsRhewa = Konfig.GridSettingsRHEWA
                mobjSingletonObject.HoleAlleeigenenEichungenVomServer = Konfig.HoleAlleeigenenEichungenVomServer
                mobjSingletonObject.LetztesUpdate = Konfig.LetztesUpdate
                mobjSingletonObject.SyncAb = Konfig.SyncAb
                mobjSingletonObject.SyncBis = Konfig.SyncBis
                mobjSingletonObject.Synchronisierungsmodus = Konfig.Synchronisierungsmodus
            End If

        End Using

        Return mobjSingletonObject

    End Function

    ''' <summary>
    ''' speichert benutzerbezogene Daten in Datenbank. Etwa die Einstellung der Grids
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SaveSettings()
        Using Context As New Entities
            Context.Configuration.LazyLoadingEnabled = True

            Try
                If mobjSingletonObject Is Nothing Then
                    MessageBox.Show("Debug Info -1. Please Report to RHEWA")
                ElseIf mobjSingletonObject.mvarObjLizenz Is Nothing Then
                    MessageBox.Show("Debug Info -2. Please Report to RHEWA")
                ElseIf mobjSingletonObject.mvarObjLizenz.Lizenzschluessel Is Nothing Then
                    MessageBox.Show("Debug Info -3. Please Report to RHEWA")
                End If
            Catch ex As Exception

            End Try

            Dim Konfig = (From Konfiguration In Context.Konfiguration Where Konfiguration.BenutzerLizenz = mobjSingletonObject.mvarObjLizenz.Lizenzschluessel).FirstOrDefault
            If Konfig Is Nothing Then
                MessageBox.Show("Ungültige Konfiguration. Wird neu erstellt")
                Konfig = New Konfiguration
                Context.Konfiguration.Add(Konfig)
            End If

            Try
                Konfig.AktuelleSprache = IIf(String.IsNullOrEmpty(mobjSingletonObject.mvarAktuelleSprache) = True, "en", mobjSingletonObject.AktuelleSprache)
            Catch ex As Exception
                MessageBox.Show("Debug Info 1. Please Report to RHEWA")
            End Try
            Try
                Konfig.GridSettings = mobjSingletonObject.mvarGridSettings
            Catch ex As Exception
                MessageBox.Show("Debug Info 2. Please Report to RHEWA")
            End Try
            Try
                Konfig.GridSettingsRHEWA = mobjSingletonObject.mvarGridSettingsRHEWA
            Catch ex As Exception
                MessageBox.Show("Debug Info 3. Please Report to RHEWA")
            End Try
            Try
                Konfig.HoleAlleeigenenEichungenVomServer = mobjSingletonObject.mvarHoleAlleeigenenEichungenVomServer
            Catch ex As Exception
                MessageBox.Show("Debug Info 4. Please Report to RHEWA")
            End Try
            Try
                Konfig.LetztesUpdate = mobjSingletonObject.mvarLetztesUpdate
            Catch ex As Exception
                MessageBox.Show("Debug Info 5. Please Report to RHEWA")
            End Try
            Try
                Konfig.SyncAb = mobjSingletonObject.mvarSyncAb
            Catch ex As Exception
                MessageBox.Show("Debug Info 6. Please Report to RHEWA")
            End Try
            Try
                Konfig.SyncBis = mobjSingletonObject.mvarSyncBis
            Catch ex As Exception
                MessageBox.Show("Debug Info 7. Please Report to RHEWA")
            End Try
            Try
                Konfig.Synchronisierungsmodus = mobjSingletonObject.mvarSynchronisierungsmodus
            Catch ex As Exception
                MessageBox.Show("Debug Info 8. Please Report to RHEWA")
            End Try

            Try
                Konfig.BenutzerLizenz = mobjSingletonObject.mvarObjLizenz.Lizenzschluessel
            Catch ex As Exception
                MessageBox.Show("Debug Info 9. Please Report to RHEWA")

            End Try

            Try
                Context.SaveChanges()
            Catch ex As Entity.Validation.DbEntityValidationException
                MessageBox.Show(ex.Message)

                For Each validerrorresult In ex.EntityValidationErrors
                    For Each validerror In validerrorresult.ValidationErrors
                        MessageBox.Show(String.Format("Message: {0}  Property: {1}", validerror.ErrorMessage, validerror.PropertyName))

                    Next
                Next
                If Not ex.InnerException Is Nothing Then
                    MessageBox.Show(ex.InnerException.Message)
                    If Not ex.InnerException.InnerException Is Nothing Then
                        MessageBox.Show(ex.InnerException.InnerException.Message)
                    End If
                End If
            End Try
            Return True
        End Using
    End Function

    ''' <summary>
    ''' Speichert Gridlayout als XML Stream, welcher in DB zum aktuellen Benutzer gespeichert wird
    ''' </summary>
    ''' <remarks></remarks>
    Friend Shared Sub SpeichereGridLayout(ByVal uco As ucoEichprozessauswahlliste)
        'speichere Layout der beiden Grids
        If uco.GetType Is GetType(ucoEichprozessauswahlliste) Then
            Dim gridProzesse = CType(uco, ucoEichprozessauswahlliste).RadGridViewAuswahlliste
            Dim gridProzesseRHEWA = CType(uco, ucoEichprozessauswahlliste).RadGridViewRHEWAAlle
            Try
                Using stream As New IO.MemoryStream()
                    '   SetzeGridLayoutString(gridProzesse)

                    gridProzesse.SaveLayout(stream)
                    If Not stream Is Nothing Then
                        stream.Position = 0
                        Dim buffer As Byte() = New Byte(CInt(stream.Length) - 1) {}
                        stream.Read(buffer, 0, buffer.Length)

                        AktuellerBenutzer.Instance.GridSettings = Convert.ToBase64String(buffer)
                    End If
                End Using
            Catch ex As Exception

            End Try

            Try
                Using stream As New IO.MemoryStream()
                    '       SetzeGridLayoutString(gridProzesseRHEWA)
                    gridProzesseRHEWA.SaveLayout(stream)
                    If Not stream Is Nothing Then
                        stream.Position = 0
                        Dim buffer As Byte() = New Byte(CInt(stream.Length) - 1) {}
                        stream.Read(buffer, 0, buffer.Length)
                        AktuellerBenutzer.Instance.GridSettingsRhewa = Convert.ToBase64String(buffer)
                    End If
                End Using
            Catch ex As Exception
            End Try
        End If
        AktuellerBenutzer.SaveSettings()

    End Sub

    ''' <summary>
    ''' laden des Grid Layouts aus User Settings
    ''' </summary>
    ''' <param name="uco"></param>
    Friend Shared Sub LadeGridLayout(uco As ucoEichprozessauswahlliste)
        If AktuellerBenutzer.Instance.GridSettings.ToString.Equals("") Then
            ResetGridSettings()
        End If
        'laden des Grid Layouts aus User Settings
        Try
            If Not AktuellerBenutzer.Instance.GridSettings.ToString.Equals("") Then
                Using stream As New IO.MemoryStream(Convert.FromBase64String(AktuellerBenutzer.Instance.GridSettings))
                    If uco.RadGridViewAuswahlliste.Rows.Count > 0 Then
                        uco.RadGridViewAuswahlliste.LoadLayout(stream)
                    End If

                End Using
            End If
        Catch ex As Exception
            'konnte layout nicht finden
            Debug.WriteLine(ex.ToString)
        End Try

        'laden des RHEWA Grids aus User Settings
        Try
            If Not AktuellerBenutzer.Instance.GridSettingsRhewa.ToString.Equals("") Then
                Using stream As New IO.MemoryStream(Convert.FromBase64String(AktuellerBenutzer.Instance.GridSettingsRhewa))
                    If uco.RadGridViewRHEWAAlle.Rows.Count > 0 Then
                        uco.RadGridViewRHEWAAlle.LoadLayout(stream)
                    End If
                End Using
            End If
        Catch ex As Exception
            'konnte layout nicht finden
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' nimmt die in den Konstanten definierten Default Werte. Wenn es Änderungen am Grid gibt, die alle Betreffen müssen die Strings aktualisiert werden
    ''' </summary>
    Public Shared Sub ResetGridSettings()
        AktuellerBenutzer.Instance.GridSettings = AktuellerBenutzer.Instance.GridDefaultSettings
        AktuellerBenutzer.Instance.GridSettingsRhewa = AktuellerBenutzer.Instance.GridDefaultSettingsRhewa
        AktuellerBenutzer.SaveSettings()

    End Sub
End Class