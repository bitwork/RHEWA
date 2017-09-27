Public Interface IRhewaEditingDialog
    Sub LoadFromDatabase()
    Sub OverwriteIstSoll()
    Function ValidateControls() As Boolean
    Sub SaveObjekt()
    Sub AktualisiereStatus()
    'Sub SaveWithoutValidation()
    Sub Lokalisiere()
    Sub UpdateObjekt()
    Sub Entsperrung()
    Sub Versenden()
    Sub FillControls()
    Function CheckDialogModus() As Boolean
    Function ValidationNeeded() As Boolean
    Sub SetzeUeberschrift()
End Interface
Public Interface IRhewaPruefungDialog
    Sub LadePruefungen()
    Sub LadePruefungenRHEWAKorrekturModus()
    Sub LadePruefungenBearbeitungsModus()
End Interface

Public Interface IRhewaEditingDialogEvents
    Sub LokalisierungNeeded(ByVal UserControl As UserControl)
    Sub SaveWithoutValidationNeeded(ByVal usercontrol As UserControl)
    Sub UpdateNeeded(ByVal UserControl As UserControl)
    Sub SaveNeeded(ByVal UserControl As UserControl)
    Sub EntsperrungNeeded()
    Sub VersendenNeeded(TargetUserControl As UserControl)
End Interface
