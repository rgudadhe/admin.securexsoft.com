
Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.Office.Core
Imports System.Runtime.InteropServices.Marshal
Imports system.IO
Imports System.Data.OleDb


Partial Class Billing_FileImport_ImportLines
    Inherits BasePage

   

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
       
        Dim obj As New ETS.BL.BusinessAnalytics
        obj.UpdateUsersMonthlyCost(DLMonth.SelectedValue, DLYear.SelectedValue)
        obj = Nothing
        Label2.Text = "Linecount has been generated successfully."
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetMyMonthList(DLMonth, True)
            GetMyYearList(DLYear, True)
        End If

    End Sub
End Class
