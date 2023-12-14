Module Module1
    Public Sub CloseAllOtherForms(excludedForm As Form)
        Dim mainForm As Form = Application.OpenForms(0)

        For i As Integer = Application.OpenForms.Count - 1 To 0 Step -1
            Dim frm As Form = Application.OpenForms(i)

            If frm IsNot mainForm Then
                frm.Close()
            End If
        Next

        Application.Exit()
    End Sub
End Module
