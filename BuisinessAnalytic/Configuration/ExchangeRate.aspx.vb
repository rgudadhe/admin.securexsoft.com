
Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.Office.Core
Imports System.Runtime.InteropServices.Marshal
Imports system.IO
Imports System.Data.OleDb


Partial Class Billing_FileImport_ImportLines
    Inherits BasePage

    

    Protected Sub ShowDetails()
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        strQuery = "Select * from [ADMINETS].[dbo].[tblExchRate] where  Month  = '" & DLMonth.SelectedValue & "' and Year  = '" & DLYear.SelectedValue & "' and contractorid ='" & Session("contractorid").ToString & "' "
        ' Response.Write(strQuery)
        Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
        SQLCmd3.Connection.Open()
        Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
        If DRRec3.HasRows Then
            If DRRec3.Read Then
                TXTExchRate.Text = DRRec3("ExchRate").ToString
            End If
        Else
            TXTExchRate.Text = 0.0
        End If
        '  Response.Write(recstatus)
        DRRec3.Close()
        SQLCmd3.Connection.Close()



    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            strQuery = "DELETE FROM AdminETS.dbo.tblExchRate WHERE Month  = '" & DLMonth.SelectedValue & "' and Year  = '" & DLYear.SelectedValue & "' and contractorID='" & Session("ContractorID").ToString & "'"
            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
            SQLCmd3.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> ConnectionState.Open Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Dim oConn1 As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn1.Open()
            strQuery = "INSERT INTO AdminETS.dbo.tblExchRate ([ExchRate],[month],[year],[ContractorID] ) VALUES ('" & TXTExchRate.Text & "','" & DLMonth.SelectedValue & "','" & DLYear.SelectedValue & "','" & Session("contractorID").ToString & "')"
            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
            SQLCmd3.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn1.State <> ConnectionState.Open Then
                oConn1.Close()
                oConn1 = Nothing
            End If
        End Try


                      

    End Sub

    Protected Sub DLMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLMonth.SelectedIndexChanged
        ShowDetails()
    End Sub

    Protected Sub DLYear_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLYear.SelectedIndexChanged
        ShowDetails()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ShowDetails()
        End If
    End Sub
End Class
