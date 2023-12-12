Module Module1
    Public Sub CloseAllOtherForms(excludedForm As Form)
        For Each frm As Form In Application.OpenForms
            If frm IsNot excludedForm AndAlso frm.Visible Then
                frm.Close()
            End If
        Next
    End Sub
End Module
