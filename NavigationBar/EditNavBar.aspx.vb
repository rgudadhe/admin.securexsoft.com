Imports System
Imports System.Data
Imports System.Data.SqlClient
Partial Class Department_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsNavBar As New ETS.BL.NavBar
        With clsNavBar
            Dim DSNavBar As DataSet = .getNavBarList()

            Dim sExpr As String
            Dim drRows() As DataRow
            sExpr = "(deleted is null or deleted=false)"
            drRows = DSNavBar.Tables(0).Select(sExpr)
            Dim DT As DataTable = DSNavBar.Tables(0).Clone
            For Each dr As DataRow In drRows
                DT.ImportRow(dr)
            Next
            DispData.DataSource = DT
            DispData.DataBind()
        End With
    End Sub
End Class
