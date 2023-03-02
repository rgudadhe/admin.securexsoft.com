Imports System.Data.SqlClient
Imports System
Imports System.Data
Partial Class Department_Default
    Inherits BasePage


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim clsNavBar As New ETS.BL.NavBar
        With clsNavBar
            .Details = TxtNavBar.Text
            Dim DSNavBar As DataSet = .getNavBarList()
            If DSNavBar.Tables(0).Compute("count(Details)", "Details = '" & TxtNavBar.Text & "'") = 0 Then
                .updatedate = Now
                Dim RetVal As Integer = .InserNewNavBar
                If RetVal = 1 Then
                    MsgDisp.Text = "Record has been added successfully."
                Else
                    MsgDisp.Text = "Failed adding NavBar"
                End If
            Else
                TxtNavBar.Focus()
                MsgDisp.Text = "Record already exists."
            End If
        End With
        clsNavBar = Nothing

    End Sub

    
End Class
