Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports EncryPass
Imports System.Security.Cryptography
Partial Class authorization_userquestionreset
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(sender As Object, e As System.EventArgs) Handles form1.Load
        Dim blSecurity As New ETS.BL.BALSecurity
        Dim dt As DataTable = blSecurity.getQuestionBank
        'Response.Write(dt.Rows.Count)
        DropDownList1.DataSource = dt
        DropDownList1.DataTextField = "question"
        DropDownList1.DataValueField = "question"
        DropDownList1.DataBind()


        'Dim uid As String = Request.QueryString("uid")
        Dim uid As String = "a164cb3f-c29b-4f8f-afc8-67f626b5f05d"
        Dim DT1 As DataTable = blSecurity.getUserSecretQuestions(uid)

        If DT1.Rows.Count > 0 Then
            Dim DR As DataRow = DT1.Rows(0)
            DropDownList1.SelectedItem.Text = DR.Item("q1").ToString
            txtans1.Text = DR.Item("s1").ToString
            Dim dt2 As DataTable = blSecurity.getQuestionBankForOtherDD(DropDownList1.Text)
            DropDownList2.DataSource = dt2
            DropDownList2.DataTextField = "question"
            DropDownList2.DataValueField = "question"
            DropDownList2.DataBind()
            DropDownList2.SelectedItem.Text = DR.Item("q2").ToString
            txtans2.Text = DR.Item("s2").ToString
            Dim dt3 As DataTable = blSecurity.getQuestionBankForOtherDD(DropDownList2.Text)
        End If
    End Sub
End Class
