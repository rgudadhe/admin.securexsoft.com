Imports System.Data.SqlClient
Partial Class CategoryDet
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim clsNavBar As New ETS.BL.NavBar(Request("CatID").ToString)
            With clsNavBar
                TxtNavBar.Text = .Details
                CategoryID.Value = .NavBarID
            End With
            clsNavBar = Nothing
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim clsNavBar As New ETS.BL.NavBar
        With clsNavBar
            .NavBarID = CategoryID.Value
            .Details = TxtNavBar.Text
            Dim RetVal As Integer = .UpdateNavBarDetails()
            If RetVal = 1 Then
                MsgDisp.Text = "Record has been udpated successfully."
            Else
                MsgDisp.Text = "Update failed!"
            End If
        End With
        clsNavBar = Nothing
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim clsNavBar As New ETS.BL.NavBar
        With clsNavBar
            .NavBarID = CategoryID.Value
            .deleted = True
            Dim RetVal As Integer = .UpdateNavBarDetails()
            If RetVal = 1 Then
                MsgDisp.Text = "Record has been deleted successfully."
            Else
                MsgDisp.Text = "Deletion failed!"
            End If
        End With
        clsNavBar = Nothing
    End Sub
End Class
