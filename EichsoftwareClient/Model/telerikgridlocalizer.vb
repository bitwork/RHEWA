Imports Telerik.WinControls.UI.Localization

Public Class telerikgridlocalizerEN

    Inherits RadGridLocalizationProvider

    Public Overrides Function GetLocalizedString(id As String) As string
        Select Case id
            Case RadGridStringId.ConditionalFormattingPleaseSelectValidCellValue
                Return "Please select valid cell value"
            Case RadGridStringId.ConditionalFormattingPleaseSetValidCellValue
                Return "Please set a valid cell value"
            Case RadGridStringId.ConditionalFormattingPleaseSetValidCellValues
                Return "Please set a valid cell values"
            Case RadGridStringId.ConditionalFormattingPleaseSetValidExpression
                Return "Please set a valid expression"
            Case RadGridStringId.ConditionalFormattingItem
                Return "Item"
            Case RadGridStringId.ConditionalFormattingInvalidParameters
                Return "Invalid parameters"
            Case RadGridStringId.FilterFunctionBetween
                Return "Between"
            Case RadGridStringId.FilterFunctionContains
                Return "Contains"
            Case RadGridStringId.FilterFunctionDoesNotContain
                Return "Does not contain"
            Case RadGridStringId.FilterFunctionEndsWith
                Return "Ends with"
            Case RadGridStringId.FilterFunctionEqualTo
                Return "Equals"
            Case RadGridStringId.FilterFunctionGreaterThan
                Return "Greater than"
            Case RadGridStringId.FilterFunctionGreaterThanOrEqualTo
                Return "Greater than or equal to"
            Case RadGridStringId.FilterFunctionIsEmpty
                Return "Is empty"
            Case RadGridStringId.FilterFunctionIsNull
                Return "Is null"
            Case RadGridStringId.FilterFunctionLessThan
                Return "Less than"
            Case RadGridStringId.FilterFunctionLessThanOrEqualTo
                Return "Less than or equal to"
            Case RadGridStringId.FilterFunctionNoFilter
                Return "No filter"
            Case RadGridStringId.FilterFunctionNotBetween
                Return "Not between"
            Case RadGridStringId.FilterFunctionNotEqualTo
                Return "Not equal to"
            Case RadGridStringId.FilterFunctionNotIsEmpty
                Return "Is not empty"
            Case RadGridStringId.FilterFunctionNotIsNull
                Return "Is not null"
            Case RadGridStringId.FilterFunctionStartsWith
                Return "Starts with"
            Case RadGridStringId.FilterFunctionCustom
                Return "Custom"
            Case RadGridStringId.FilterOperatorBetween
                Return "Between"
            Case RadGridStringId.FilterOperatorContains
                Return "Contains"
            Case RadGridStringId.FilterOperatorDoesNotContain
                Return "NotContains"
            Case RadGridStringId.FilterOperatorEndsWith
                Return "EndsWith"
            Case RadGridStringId.FilterOperatorEqualTo
                Return "Equals"
            Case RadGridStringId.FilterOperatorGreaterThan
                Return "GreaterThan"
            Case RadGridStringId.FilterOperatorGreaterThanOrEqualTo
                Return "GreaterThanOrEquals"
            Case RadGridStringId.FilterOperatorIsEmpty
                Return "IsEmpty"
            Case RadGridStringId.FilterOperatorIsNull
                Return "IsNull"
            Case RadGridStringId.FilterOperatorLessThan
                Return "LessThan"
            Case RadGridStringId.FilterOperatorLessThanOrEqualTo
                Return "LessThanOrEquals"
            Case RadGridStringId.FilterOperatorNoFilter
                Return "No filter"
            Case RadGridStringId.FilterOperatorNotBetween
                Return "NotBetween"
            Case RadGridStringId.FilterOperatorNotEqualTo
                Return "NotEquals"
            Case RadGridStringId.FilterOperatorNotIsEmpty
                Return "NotEmpty"
            Case RadGridStringId.FilterOperatorNotIsNull
                Return "NotNull"
            Case RadGridStringId.FilterOperatorStartsWith
                Return "StartsWith"
            Case RadGridStringId.FilterOperatorIsLike
                Return "Like"
            Case RadGridStringId.FilterOperatorNotIsLike
                Return "NotLike"
            Case RadGridStringId.FilterOperatorIsContainedIn
                Return "ContainedIn"
            Case RadGridStringId.FilterOperatorNotIsContainedIn
                Return "NotContainedIn"
            Case RadGridStringId.FilterOperatorCustom
                Return "Custom"
            Case RadGridStringId.CustomFilterMenuItem
                Return "Custom"
            Case RadGridStringId.CustomFilterDialogCaption
                Return "RadGridView Filter Dialog [{0}]"
            Case RadGridStringId.CustomFilterDialogLabel
                Return "Show rows where:"
            Case RadGridStringId.CustomFilterDialogRbAnd
                Return "And"
            Case RadGridStringId.CustomFilterDialogRbOr
                Return "Or"
            Case RadGridStringId.CustomFilterDialogBtnOk
                Return "OK"
            Case RadGridStringId.CustomFilterDialogBtnCancel
                Return "Cancel"
            Case RadGridStringId.CustomFilterDialogCheckBoxNot
                Return "Not"
            Case RadGridStringId.CustomFilterDialogTrue
                Return "True"
            Case RadGridStringId.CustomFilterDialogFalse
                Return "False"
            Case RadGridStringId.FilterMenuBlanks
                Return "Empty"
            Case RadGridStringId.FilterMenuAvailableFilters
                Return "Available Filters"
            Case RadGridStringId.FilterMenuSearchBoxText
                Return "Search..."
            Case RadGridStringId.FilterMenuClearFilters
                Return "Clear Filter"
            Case RadGridStringId.FilterMenuButtonOK
                Return "OK"
            Case RadGridStringId.FilterMenuButtonCancel
                Return "Cancel"
            Case RadGridStringId.FilterMenuSelectionAll
                Return "All"
            Case RadGridStringId.FilterMenuSelectionAllSearched
                Return "All Search Result"
            Case RadGridStringId.FilterMenuSelectionNull
                Return "Null"
            Case RadGridStringId.FilterMenuSelectionNotNull
                Return "Not Null"
            Case RadGridStringId.FilterFunctionSelectedDates
                Return "Filter by specific dates:"
            Case RadGridStringId.FilterFunctionToday
                Return "Today"
            Case RadGridStringId.FilterFunctionYesterday
                Return "Yesterday"
            Case RadGridStringId.FilterFunctionDuringLast7days
                Return "During last 7 days"
            Case RadGridStringId.FilterLogicalOperatorAnd
                Return "AND"
            Case RadGridStringId.FilterLogicalOperatorOr
                Return "OR"
            Case RadGridStringId.FilterCompositeNotOperator
                Return "NOT"
            Case RadGridStringId.DeleteRowMenuItem
                Return "Delete Row"
            Case RadGridStringId.SortAscendingMenuItem
                Return "Sort Ascending"
            Case RadGridStringId.SortDescendingMenuItem
                Return "Sort Descending"
            Case RadGridStringId.ClearSortingMenuItem
                Return "Clear Sorting"
            Case RadGridStringId.ConditionalFormattingMenuItem
                Return "Conditional Formatting"
            Case RadGridStringId.GroupByThisColumnMenuItem
                Return "Group by this column"
            Case RadGridStringId.UngroupThisColumn
                Return "Ungroup this column"
            Case RadGridStringId.ColumnChooserMenuItem
                Return "Column Chooser"
            Case RadGridStringId.HideMenuItem
                Return "Hide Column"
            Case RadGridStringId.HideGroupMenuItem
                Return "Hide Group"
            Case RadGridStringId.UnpinMenuItem
                Return "Unpin Column"
            Case RadGridStringId.UnpinRowMenuItem
                Return "Unpin Row"
            Case RadGridStringId.PinMenuItem
                Return "Pinned state"
            Case RadGridStringId.PinAtLeftMenuItem
                Return "Pin at left"
            Case RadGridStringId.PinAtRightMenuItem
                Return "Pin at right"
            Case RadGridStringId.PinAtBottomMenuItem
                Return "Pin at bottom"
            Case RadGridStringId.PinAtTopMenuItem
                Return "Pin at top"
            Case RadGridStringId.BestFitMenuItem
                Return "Best Fit"
            Case RadGridStringId.PasteMenuItem
                Return "Paste"
            Case RadGridStringId.EditMenuItem
                Return "Edit"
            Case RadGridStringId.ClearValueMenuItem
                Return "Clear Value"
            Case RadGridStringId.CopyMenuItem
                Return "Copy"
            Case RadGridStringId.CutMenuItem
                Return "Cut"
            Case RadGridStringId.AddNewRowString
                Return "Click here to add a new row"
            Case RadGridStringId.SearchRowResultsOfLabel
                Return "of"
            Case RadGridStringId.SearchRowMatchCase
                Return "Match case"
            Case RadGridStringId.ConditionalFormattingSortAlphabetically
                Return "Sort columns alphabetically"
            Case RadGridStringId.ConditionalFormattingCaption
                Return "Conditional Formatting Rules Manager"
            Case RadGridStringId.ConditionalFormattingLblColumn
                Return "Format only cells with"
            Case RadGridStringId.ConditionalFormattingLblName
                Return "Rule name"
            Case RadGridStringId.ConditionalFormattingLblType
                Return "Cell value"
            Case RadGridStringId.ConditionalFormattingLblValue1
                Return "Value 1"
            Case RadGridStringId.ConditionalFormattingLblValue2
                Return "Value 2"
            Case RadGridStringId.ConditionalFormattingGrpConditions
                Return "Rules"
            Case RadGridStringId.ConditionalFormattingGrpProperties
                Return "Rule Properties"
            Case RadGridStringId.ConditionalFormattingChkApplyToRow
                Return "Apply this formatting to entire row"
            Case RadGridStringId.ConditionalFormattingChkApplyOnSelectedRows
                Return "Apply this formatting if the row is selected"
            Case RadGridStringId.ConditionalFormattingBtnAdd
                Return "Add new rule"
            Case RadGridStringId.ConditionalFormattingBtnRemove
                Return "Remove"
            Case RadGridStringId.ConditionalFormattingBtnOK
                Return "OK"
            Case RadGridStringId.ConditionalFormattingBtnCancel
                Return "Cancel"
            Case RadGridStringId.ConditionalFormattingBtnApply
                Return "Apply"
            Case RadGridStringId.ConditionalFormattingRuleAppliesOn
                Return "Rule applies to"
            Case RadGridStringId.ConditionalFormattingCondition
                Return "Condition"
            Case RadGridStringId.ConditionalFormattingExpression
                Return "Expression"
            Case RadGridStringId.ConditionalFormattingChooseOne
                Return "[Choose one]"
            Case RadGridStringId.ConditionalFormattingEqualsTo
                Return "equals to [Value1]"
            Case RadGridStringId.ConditionalFormattingIsNotEqualTo
                Return "is not equal to [Value1]"
            Case RadGridStringId.ConditionalFormattingStartsWith
                Return "starts with [Value1]"
            Case RadGridStringId.ConditionalFormattingEndsWith
                Return "ends with [Value1]"
            Case RadGridStringId.ConditionalFormattingContains
                Return "contains [Value1]"
            Case RadGridStringId.ConditionalFormattingDoesNotContain
                Return "does not contain [Value1]"
            Case RadGridStringId.ConditionalFormattingIsGreaterThan
                Return "is greater than [Value1]"
            Case RadGridStringId.ConditionalFormattingIsGreaterThanOrEqual
                Return "is greater than or equal [Value1]"
            Case RadGridStringId.ConditionalFormattingIsLessThan
                Return "is less than [Value1]"
            Case RadGridStringId.ConditionalFormattingIsLessThanOrEqual
                Return "is less than or equal to [Value1]"
            Case RadGridStringId.ConditionalFormattingIsBetween
                Return "is between [Value1] and [Value2]"
            Case RadGridStringId.ConditionalFormattingIsNotBetween
                Return "is not between [Value1] and [Value1]"
            Case RadGridStringId.ConditionalFormattingLblFormat
                Return "Format"
            Case RadGridStringId.ConditionalFormattingBtnExpression
                Return "Expression editor"
            Case RadGridStringId.ConditionalFormattingTextBoxExpression
                Return "Expression"
            Case RadGridStringId.ConditionalFormattingPropertyGridCaseSensitive
                Return "CaseSensitive"
            Case RadGridStringId.ConditionalFormattingPropertyGridCellBackColor
                Return "CellBackColor"
            Case RadGridStringId.ConditionalFormattingPropertyGridCellForeColor
                Return "CellForeColor"
            Case RadGridStringId.ConditionalFormattingPropertyGridEnabled
                Return "Enabled"
            Case RadGridStringId.ConditionalFormattingPropertyGridRowBackColor
                Return "RowBackColor"
            Case RadGridStringId.ConditionalFormattingPropertyGridRowForeColor
                Return "RowForeColor"
            Case RadGridStringId.ConditionalFormattingPropertyGridRowTextAlignment
                Return "RowTextAlignment"
            Case RadGridStringId.ConditionalFormattingPropertyGridTextAlignment
                Return "TextAlignment"
            Case RadGridStringId.ConditionalFormattingPropertyGridCaseSensitiveDescription
                Return "Determines whether case-sensitive comparisons will be made when evaluating string values."
            Case RadGridStringId.ConditionalFormattingPropertyGridCellBackColorDescription
                Return "Enter the background color to be used for the cell."
            Case RadGridStringId.ConditionalFormattingPropertyGridCellForeColorDescription
                Return "Enter the foreground color to be used for the cell."
            Case RadGridStringId.ConditionalFormattingPropertyGridEnabledDescription
                Return "Determines whether the condition is enabled (can be evaluated and applied)."
            Case RadGridStringId.ConditionalFormattingPropertyGridRowBackColorDescription
                Return "Enter the background color to be used for the entire row."
            Case RadGridStringId.ConditionalFormattingPropertyGridRowForeColorDescription
                Return "Enter the foreground color to be used for the entire row."
            Case RadGridStringId.ConditionalFormattingPropertyGridRowTextAlignmentDescription
                Return "Enter the alignment to be used for the cell values, when ApplyToRow is true."
            Case RadGridStringId.ConditionalFormattingPropertyGridTextAlignmentDescription
                Return "Enter the alignment to be used for the cell values."
            Case RadGridStringId.ColumnChooserFormCaption
                Return "Column Chooser"
            Case RadGridStringId.ColumnChooserFormMessage
                Return "Drag a column header from the" & vbLf & "grid here to remove it from" & vbLf & "the current view."
            Case RadGridStringId.GroupingPanelDefaultMessage
                Return "Drag a column here to group by this column."
            Case RadGridStringId.GroupingPanelHeader
                Return "Group by:"
            Case RadGridStringId.PagingPanelPagesLabel
                Return "Page"
            Case RadGridStringId.PagingPanelOfPagesLabel
                Return "of"
            Case RadGridStringId.NoDataText
                Return "No data to display"
            Case RadGridStringId.CompositeFilterFormErrorCaption
                Return "Filter Error"
            Case RadGridStringId.CompositeFilterFormInvalidFilter
                Return "The composite filter descriptor is not valid."
            Case RadGridStringId.ExpressionMenuItem
                Return "Expression"
            Case RadGridStringId.ExpressionFormTitle
                Return "Expression Builder"
            Case RadGridStringId.ExpressionFormFunctions
                Return "Functions"
            Case RadGridStringId.ExpressionFormFunctionsText
                Return "Text"
            Case RadGridStringId.ExpressionFormFunctionsAggregate
                Return "Aggregate"
            Case RadGridStringId.ExpressionFormFunctionsDateTime
                Return "Date-Time"
            Case RadGridStringId.ExpressionFormFunctionsLogical
                Return "Logical"
            Case RadGridStringId.ExpressionFormFunctionsMath
                Return "Math"
            Case RadGridStringId.ExpressionFormFunctionsOther
                Return "Other"
            Case RadGridStringId.ExpressionFormOperators
                Return "Operators"
            Case RadGridStringId.ExpressionFormConstants
                Return "Constants"
            Case RadGridStringId.ExpressionFormFields
                Return "Fields"
            Case RadGridStringId.ExpressionFormDescription
                Return "Description"
            Case RadGridStringId.ExpressionFormResultPreview
                Return "Result preview"
            Case RadGridStringId.ExpressionFormTooltipPlus
                Return "Plus"
            Case RadGridStringId.ExpressionFormTooltipMinus
                Return "Minus"
            Case RadGridStringId.ExpressionFormTooltipMultiply
                Return "Multiply"
            Case RadGridStringId.ExpressionFormTooltipDivide
                Return "Divide"
            Case RadGridStringId.ExpressionFormTooltipModulo
                Return "Modulo"
            Case RadGridStringId.ExpressionFormTooltipEqual
                Return "Equal"
            Case RadGridStringId.ExpressionFormTooltipNotEqual
                Return "Not Equal"
            Case RadGridStringId.ExpressionFormTooltipLess
                Return "Less"
            Case RadGridStringId.ExpressionFormTooltipLessOrEqual
                Return "Less Or Equal"
            Case RadGridStringId.ExpressionFormTooltipGreaterOrEqual
                Return "Greater Or Equal"
            Case RadGridStringId.ExpressionFormTooltipGreater
                Return "Greater"
            Case RadGridStringId.ExpressionFormTooltipAnd
                Return "Logical ""AND"""
            Case RadGridStringId.ExpressionFormTooltipOr
                Return "Logical ""OR"""
            Case RadGridStringId.ExpressionFormTooltipNot
                Return "Logical ""NOT"""
            Case RadGridStringId.ExpressionFormAndButton
                Return String.Empty
                'if empty, default button image is used
            Case RadGridStringId.ExpressionFormOrButton
                Return String.Empty
                'if empty, default button image is used
            Case RadGridStringId.ExpressionFormNotButton
                Return String.Empty
                'if empty, default button image is used
            Case RadGridStringId.ExpressionFormOKButton
                Return "OK"
            Case RadGridStringId.ExpressionFormCancelButton
                Return "Cancel"
        End Select
        Return String.Empty
    End Function

End Class

Public Class telerikgridlocalizerDE

    Inherits RadGridLocalizationProvider

    Public Overrides Function GetLocalizedString(id As String) As string
        Select Case id
            Case RadGridStringId.ConditionalFormattingPleaseSelectValidCellValue
                Return "Please select valid cell value"
            Case RadGridStringId.ConditionalFormattingPleaseSetValidCellValue
                Return "Please set a valid cell value"
            Case RadGridStringId.ConditionalFormattingPleaseSetValidCellValues
                Return "Please set a valid cell values"
            Case RadGridStringId.ConditionalFormattingPleaseSetValidExpression
                Return "Please set a valid expression"
            Case RadGridStringId.ConditionalFormattingItem
                Return "Item"
            Case RadGridStringId.ConditionalFormattingInvalidParameters
                Return "Invalid parameters"
            Case RadGridStringId.FilterFunctionBetween
                Return "Zwischen"
            Case RadGridStringId.FilterFunctionContains
                Return "Enthält"
            Case RadGridStringId.FilterFunctionDoesNotContain
                Return "Enthält nicht"
            Case RadGridStringId.FilterFunctionEndsWith
                Return "Endet mit"
            Case RadGridStringId.FilterFunctionEqualTo
                Return "Gleich"
            Case RadGridStringId.FilterFunctionGreaterThan
                Return "Größer als"
            Case RadGridStringId.FilterFunctionGreaterThanOrEqualTo
                Return "Größer als oder gleich"
            Case RadGridStringId.FilterFunctionIsEmpty
                Return "leer"
            Case RadGridStringId.FilterFunctionIsNull
                Return "Ist null"
            Case RadGridStringId.FilterFunctionLessThan
                Return "Kleiner als"
            Case RadGridStringId.FilterFunctionLessThanOrEqualTo
                Return "Kleiner als oder gleich"
            Case RadGridStringId.FilterFunctionNoFilter
                Return "kein Filter"
            Case RadGridStringId.FilterFunctionNotBetween
                Return "nicht zwischen"
            Case RadGridStringId.FilterFunctionNotEqualTo
                Return "ungleich"
            Case RadGridStringId.FilterFunctionNotIsEmpty
                Return "Ist nicht leer"
            Case RadGridStringId.FilterFunctionNotIsNull
                Return "Ist nicht leer"
            Case RadGridStringId.FilterFunctionStartsWith
                Return "Startet mit"
            Case RadGridStringId.FilterFunctionCustom
                Return "Custom"
            Case RadGridStringId.FilterOperatorBetween
                Return "Zwischen"
            Case RadGridStringId.FilterOperatorContains
                Return "Enthält"
            Case RadGridStringId.FilterOperatorDoesNotContain
                Return "Enthält nicht"
            Case RadGridStringId.FilterOperatorEndsWith
                Return "Endet mit"
            Case RadGridStringId.FilterOperatorEqualTo
                Return "Gleich"
            Case RadGridStringId.FilterOperatorGreaterThan
                Return "Größer als"
            Case RadGridStringId.FilterOperatorGreaterThanOrEqualTo
                Return "Größer als oder gleich"
            Case RadGridStringId.FilterOperatorIsEmpty
                Return "Ist leer"
            Case RadGridStringId.FilterOperatorIsNull
                Return "IsNull"
            Case RadGridStringId.FilterOperatorLessThan
                Return "Kleiner als"
            Case RadGridStringId.FilterOperatorLessThanOrEqualTo
                Return "Kleiner als oder gleich"
            Case RadGridStringId.FilterOperatorNoFilter
                Return "kein Filter"
            Case RadGridStringId.FilterOperatorNotBetween
                Return "nicht zwischen"
            Case RadGridStringId.FilterOperatorNotEqualTo
                Return "nicht gleich"
            Case RadGridStringId.FilterOperatorNotIsEmpty
                Return "nicht leer"
            Case RadGridStringId.FilterOperatorNotIsNull
                Return "nicht null"
            Case RadGridStringId.FilterOperatorStartsWith
                Return "Startet mit"
            Case RadGridStringId.FilterOperatorIsLike
                Return "Wie"
            Case RadGridStringId.FilterOperatorNotIsLike
                Return "nicht Wie"
            Case RadGridStringId.FilterOperatorIsContainedIn
                Return "ContainedIn"
            Case RadGridStringId.FilterOperatorNotIsContainedIn
                Return "NotContainedIn"
            Case RadGridStringId.FilterOperatorCustom
                Return "Custom"
            Case RadGridStringId.CustomFilterMenuItem
                Return "Custom"
            Case RadGridStringId.CustomFilterDialogCaption
                Return "RadGridView Filter Dialog [{0}]"
            Case RadGridStringId.CustomFilterDialogLabel
                Return "Show rows where:"
            Case RadGridStringId.CustomFilterDialogRbAnd
                Return "And"
            Case RadGridStringId.CustomFilterDialogRbOr
                Return "Or"
            Case RadGridStringId.CustomFilterDialogBtnOk
                Return "OK"
            Case RadGridStringId.CustomFilterDialogBtnCancel
                Return "Cancel"
            Case RadGridStringId.CustomFilterDialogCheckBoxNot
                Return "Not"
            Case RadGridStringId.CustomFilterDialogTrue
                Return "True"
            Case RadGridStringId.CustomFilterDialogFalse
                Return "False"
            Case RadGridStringId.FilterMenuBlanks
                Return "Empty"
            Case RadGridStringId.FilterMenuAvailableFilters
                Return "Available Filters"
            Case RadGridStringId.FilterMenuSearchBoxText
                Return "Search..."
            Case RadGridStringId.FilterMenuClearFilters
                Return "Clear Filter"
            Case RadGridStringId.FilterMenuButtonOK
                Return "OK"
            Case RadGridStringId.FilterMenuButtonCancel
                Return "Cancel"
            Case RadGridStringId.FilterMenuSelectionAll
                Return "All"
            Case RadGridStringId.FilterMenuSelectionAllSearched
                Return "All Search Result"
            Case RadGridStringId.FilterMenuSelectionNull
                Return "Null"
            Case RadGridStringId.FilterMenuSelectionNotNull
                Return "Not Null"
            Case RadGridStringId.FilterFunctionSelectedDates
                Return "Filter by specific dates:"
            Case RadGridStringId.FilterFunctionToday
                Return "Today"
            Case RadGridStringId.FilterFunctionYesterday
                Return "Yesterday"
            Case RadGridStringId.FilterFunctionDuringLast7days
                Return "During last 7 days"
            Case RadGridStringId.FilterLogicalOperatorAnd
                Return "AND"
            Case RadGridStringId.FilterLogicalOperatorOr
                Return "OR"
            Case RadGridStringId.FilterCompositeNotOperator
                Return "NOT"
            Case RadGridStringId.DeleteRowMenuItem
                Return "Delete Row"
            Case RadGridStringId.SortAscendingMenuItem
                Return "Sort Ascending"
            Case RadGridStringId.SortDescendingMenuItem
                Return "Sort Descending"
            Case RadGridStringId.ClearSortingMenuItem
                Return "Clear Sorting"
            Case RadGridStringId.ConditionalFormattingMenuItem
                Return "Conditional Formatting"
            Case RadGridStringId.GroupByThisColumnMenuItem
                Return "Nach dieser Spalte gruppieren"
            Case RadGridStringId.UngroupThisColumn
                Return "Gruppierung aufheben"
            Case RadGridStringId.ColumnChooserMenuItem
                Return "Column Chooser"
            Case RadGridStringId.HideMenuItem
                Return "Hide Column"
            Case RadGridStringId.HideGroupMenuItem
                Return "Hide Group"
            Case RadGridStringId.UnpinMenuItem
                Return "Unpin Column"
            Case RadGridStringId.UnpinRowMenuItem
                Return "Unpin Row"
            Case RadGridStringId.PinMenuItem
                Return "Pinned state"
            Case RadGridStringId.PinAtLeftMenuItem
                Return "Pin at left"
            Case RadGridStringId.PinAtRightMenuItem
                Return "Pin at right"
            Case RadGridStringId.PinAtBottomMenuItem
                Return "Pin at bottom"
            Case RadGridStringId.PinAtTopMenuItem
                Return "Pin at top"
            Case RadGridStringId.BestFitMenuItem
                Return "Best Fit"
            Case RadGridStringId.PasteMenuItem
                Return "Paste"
            Case RadGridStringId.EditMenuItem
                Return "Edit"
            Case RadGridStringId.ClearValueMenuItem
                Return "Clear Value"
            Case RadGridStringId.CopyMenuItem
                Return "Copy"
            Case RadGridStringId.CutMenuItem
                Return "Cut"
            Case RadGridStringId.AddNewRowString
                Return "Click here to add a new row"
            Case RadGridStringId.SearchRowResultsOfLabel
                Return "von "
            Case RadGridStringId.SearchRowMatchCase
                Return "Groß/Kleinschreibung berücksichtigen"
            Case RadGridStringId.ConditionalFormattingSortAlphabetically
                Return "Sort columns alphabetically"
            Case RadGridStringId.ConditionalFormattingCaption
                Return "Conditional Formatting Rules Manager"
            Case RadGridStringId.ConditionalFormattingLblColumn
                Return "Format only cells with"
            Case RadGridStringId.ConditionalFormattingLblName
                Return "Rule name"
            Case RadGridStringId.ConditionalFormattingLblType
                Return "Cell value"
            Case RadGridStringId.ConditionalFormattingLblValue1
                Return "Value 1"
            Case RadGridStringId.ConditionalFormattingLblValue2
                Return "Value 2"
            Case RadGridStringId.ConditionalFormattingGrpConditions
                Return "Rules"
            Case RadGridStringId.ConditionalFormattingGrpProperties
                Return "Rule Properties"
            Case RadGridStringId.ConditionalFormattingChkApplyToRow
                Return "Apply this formatting to entire row"
            Case RadGridStringId.ConditionalFormattingChkApplyOnSelectedRows
                Return "Apply this formatting if the row is selected"
            Case RadGridStringId.ConditionalFormattingBtnAdd
                Return "Add new rule"
            Case RadGridStringId.ConditionalFormattingBtnRemove
                Return "Remove"
            Case RadGridStringId.ConditionalFormattingBtnOK
                Return "OK"
            Case RadGridStringId.ConditionalFormattingBtnCancel
                Return "Cancel"
            Case RadGridStringId.ConditionalFormattingBtnApply
                Return "Apply"
            Case RadGridStringId.ConditionalFormattingRuleAppliesOn
                Return "Rule applies to"
            Case RadGridStringId.ConditionalFormattingCondition
                Return "Condition"
            Case RadGridStringId.ConditionalFormattingExpression
                Return "Expression"
            Case RadGridStringId.ConditionalFormattingChooseOne
                Return "[Choose one]"
            Case RadGridStringId.ConditionalFormattingEqualsTo
                Return "equals to [Value1]"
            Case RadGridStringId.ConditionalFormattingIsNotEqualTo
                Return "is not equal to [Value1]"
            Case RadGridStringId.ConditionalFormattingStartsWith
                Return "starts with [Value1]"
            Case RadGridStringId.ConditionalFormattingEndsWith
                Return "ends with [Value1]"
            Case RadGridStringId.ConditionalFormattingContains
                Return "contains [Value1]"
            Case RadGridStringId.ConditionalFormattingDoesNotContain
                Return "does not contain [Value1]"
            Case RadGridStringId.ConditionalFormattingIsGreaterThan
                Return "is greater than [Value1]"
            Case RadGridStringId.ConditionalFormattingIsGreaterThanOrEqual
                Return "is greater than or equal [Value1]"
            Case RadGridStringId.ConditionalFormattingIsLessThan
                Return "is less than [Value1]"
            Case RadGridStringId.ConditionalFormattingIsLessThanOrEqual
                Return "is less than or equal to [Value1]"
            Case RadGridStringId.ConditionalFormattingIsBetween
                Return "is between [Value1] and [Value2]"
            Case RadGridStringId.ConditionalFormattingIsNotBetween
                Return "is not between [Value1] and [Value1]"
            Case RadGridStringId.ConditionalFormattingLblFormat
                Return "Format"
            Case RadGridStringId.ConditionalFormattingBtnExpression
                Return "Expression editor"
            Case RadGridStringId.ConditionalFormattingTextBoxExpression
                Return "Expression"
            Case RadGridStringId.ConditionalFormattingPropertyGridCaseSensitive
                Return "CaseSensitive"
            Case RadGridStringId.ConditionalFormattingPropertyGridCellBackColor
                Return "CellBackColor"
            Case RadGridStringId.ConditionalFormattingPropertyGridCellForeColor
                Return "CellForeColor"
            Case RadGridStringId.ConditionalFormattingPropertyGridEnabled
                Return "Enabled"
            Case RadGridStringId.ConditionalFormattingPropertyGridRowBackColor
                Return "RowBackColor"
            Case RadGridStringId.ConditionalFormattingPropertyGridRowForeColor
                Return "RowForeColor"
            Case RadGridStringId.ConditionalFormattingPropertyGridRowTextAlignment
                Return "RowTextAlignment"
            Case RadGridStringId.ConditionalFormattingPropertyGridTextAlignment
                Return "TextAlignment"
            Case RadGridStringId.ConditionalFormattingPropertyGridCaseSensitiveDescription
                Return "Determines whether case-sensitive comparisons will be made when evaluating string values."
            Case RadGridStringId.ConditionalFormattingPropertyGridCellBackColorDescription
                Return "Enter the background color to be used for the cell."
            Case RadGridStringId.ConditionalFormattingPropertyGridCellForeColorDescription
                Return "Enter the foreground color to be used for the cell."
            Case RadGridStringId.ConditionalFormattingPropertyGridEnabledDescription
                Return "Determines whether the condition is enabled (can be evaluated and applied)."
            Case RadGridStringId.ConditionalFormattingPropertyGridRowBackColorDescription
                Return "Enter the background color to be used for the entire row."
            Case RadGridStringId.ConditionalFormattingPropertyGridRowForeColorDescription
                Return "Enter the foreground color to be used for the entire row."
            Case RadGridStringId.ConditionalFormattingPropertyGridRowTextAlignmentDescription
                Return "Enter the alignment to be used for the cell values, when ApplyToRow is true."
            Case RadGridStringId.ConditionalFormattingPropertyGridTextAlignmentDescription
                Return "Enter the alignment to be used for the cell values."
            Case RadGridStringId.ColumnChooserFormCaption
                Return "Column Chooser"
            Case RadGridStringId.ColumnChooserFormMessage
                Return "Drag a column header from the" & vbLf & "grid here to remove it from" & vbLf & "the current view."
            Case RadGridStringId.GroupingPanelDefaultMessage
                Return "Spaltenkopf hierher ziehen um zu gruppieren"
            Case RadGridStringId.GroupingPanelHeader
                Return "Gruppieren nach:"
            Case RadGridStringId.PagingPanelPagesLabel
                Return "Page"
            Case RadGridStringId.PagingPanelOfPagesLabel
                Return "of"
            Case RadGridStringId.NoDataText
                Return "No data to display"
            Case RadGridStringId.CompositeFilterFormErrorCaption
                Return "Filter Error"
            Case RadGridStringId.CompositeFilterFormInvalidFilter
                Return "The composite filter descriptor is not valid."
            Case RadGridStringId.ExpressionMenuItem
                Return "Expression"
            Case RadGridStringId.ExpressionFormTitle
                Return "Expression Builder"
            Case RadGridStringId.ExpressionFormFunctions
                Return "Functions"
            Case RadGridStringId.ExpressionFormFunctionsText
                Return "Text"
            Case RadGridStringId.ExpressionFormFunctionsAggregate
                Return "Aggregate"
            Case RadGridStringId.ExpressionFormFunctionsDateTime
                Return "Date-Time"
            Case RadGridStringId.ExpressionFormFunctionsLogical
                Return "Logical"
            Case RadGridStringId.ExpressionFormFunctionsMath
                Return "Math"
            Case RadGridStringId.ExpressionFormFunctionsOther
                Return "Other"
            Case RadGridStringId.ExpressionFormOperators
                Return "Operators"
            Case RadGridStringId.ExpressionFormConstants
                Return "Constants"
            Case RadGridStringId.ExpressionFormFields
                Return "Fields"
            Case RadGridStringId.ExpressionFormDescription
                Return "Description"
            Case RadGridStringId.ExpressionFormResultPreview
                Return "Result preview"
            Case RadGridStringId.ExpressionFormTooltipPlus
                Return "Plus"
            Case RadGridStringId.ExpressionFormTooltipMinus
                Return "Minus"
            Case RadGridStringId.ExpressionFormTooltipMultiply
                Return "Multiply"
            Case RadGridStringId.ExpressionFormTooltipDivide
                Return "Divide"
            Case RadGridStringId.ExpressionFormTooltipModulo
                Return "Modulo"
            Case RadGridStringId.ExpressionFormTooltipEqual
                Return "Equal"
            Case RadGridStringId.ExpressionFormTooltipNotEqual
                Return "Not Equal"
            Case RadGridStringId.ExpressionFormTooltipLess
                Return "Less"
            Case RadGridStringId.ExpressionFormTooltipLessOrEqual
                Return "Less Or Equal"
            Case RadGridStringId.ExpressionFormTooltipGreaterOrEqual
                Return "Greater Or Equal"
            Case RadGridStringId.ExpressionFormTooltipGreater
                Return "Greater"
            Case RadGridStringId.ExpressionFormTooltipAnd
                Return "Logical ""AND"""
            Case RadGridStringId.ExpressionFormTooltipOr
                Return "Logical ""OR"""
            Case RadGridStringId.ExpressionFormTooltipNot
                Return "Logical ""NOT"""
            Case RadGridStringId.ExpressionFormAndButton
                Return String.Empty
                'if empty, default button image is used
            Case RadGridStringId.ExpressionFormOrButton
                Return String.Empty
                'if empty, default button image is used
            Case RadGridStringId.ExpressionFormNotButton
                Return String.Empty
                'if empty, default button image is used
            Case RadGridStringId.ExpressionFormOKButton
                Return "OK"
            Case RadGridStringId.ExpressionFormCancelButton
                Return "Cancel"
        End Select
        Return String.Empty
    End Function

End Class