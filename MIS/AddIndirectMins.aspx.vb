Imports System.Data.SqlClient
Partial Class MIS_AddIndirectMins
    Inherits BasePage

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit.Click
        Label1.Text = ""
        Dim subdate As Date
        Dim strconn As String
        If DLAct.SelectedValue = "" Then
            Label1.Text = "Please select account from the list."
            DLAct.Focus()
            Exit Sub
        End If
        If txtUnits.Text = "" Then
            Label1.Text = "Please enter units. "
            txtUnits.Focus()
            Exit Sub
        End If
        strconn = System.Configuration.ConfigurationManager.AppSettings("ETSConOne")
        Dim SQuery As String
        If IsDate(txtDate.Text) And txtDate.Text <> "" Then
            subdate = txtDate.Text
            SQuery = "Select * from DBO.tblIndirectActUnits where  accountid ='" & DLAct.SelectedValue & "' and submitdate = '" & subdate & "'"
            Dim CmdRecChk As New SqlCommand(SQuery, New SqlConnection(strconn))
            Try
                CmdRecChk.Connection.Open()
                Dim DRRecChk As SqlDataReader = CmdRecChk.ExecuteReader()
                If DRRecChk.HasRows Then
                    If DLMode.SelectedValue = "Submit" Then
                        SQuery = "Update DBO.tblIndirectActUnits set sunits ='" & txtUnits.Text & "' where accountid ='" & DLAct.SelectedValue & "' and submitdate = '" & subdate & "'"
                    Else
                        SQuery = "Update DBO.tblIndirectActUnits set punits ='" & txtUnits.Text & "' where accountid ='" & DLAct.SelectedValue & "' and submitdate = '" & subdate & "'"
                    End If
                    Dim CmdUnits As New SqlCommand(SQuery, New SqlConnection(strconn))
                    Try
                        CmdUnits.Connection.Open()
                        CmdUnits.ExecuteNonQuery()
                    Finally
                        If CmdUnits.Connection.State = System.Data.ConnectionState.Open Then
                            CmdUnits.Connection.Close()
                            CmdUnits = Nothing
                        End If
                    End Try
                Else
                    If DLMode.SelectedValue = "Submit" Then
                        SQuery = "Insert Into DBO.tblIndirectActUnits (AccountID, SubmitDate, sunits, Dateupdated) Values ('" & DLAct.SelectedValue & "','" & subdate & "','" & txtUnits.Text & "','" & Now & "')"
                    Else
                        SQuery = "Insert Into DBO.tblIndirectActUnits (AccountID, SubmitDate, punits, Dateupdated) Values ('" & DLAct.SelectedValue & "','" & subdate & "','" & txtUnits.Text & "','" & Now & "')"
                    End If

                    Dim CmdUnits As New SqlCommand(SQuery, New SqlConnection(strconn))
                    Try
                        CmdUnits.Connection.Open()
                        CmdUnits.ExecuteNonQuery()
                    Finally
                        If CmdUnits.Connection.State = System.Data.ConnectionState.Open Then
                            CmdUnits.Connection.Close()
                            CmdUnits = Nothing
                        End If
                    End Try
                End If

            Finally
                If CmdRecChk.Connection.State = System.Data.ConnectionState.Open Then
                    CmdRecChk.Connection.Close()
                    CmdRecChk = Nothing
                End If
            End Try
            'SQuery = "Insert Into DBO.tblIndirectActUnits where submitedate = '" & subdate & "'"
            'Dim CmdUnits1 As New SqlCommand(SQuery, New SqlConnection(strconn))
            'CmdUnits1.Connection.Open()
            'CmdUnits1.ExecuteNonQuery()
            'CmdUnits1.Connection.Close()
            Label1.Text = "Units have been updated successfully. "
        Else
            Label1.Text = "Please enter proper date in text box. "
            txtDate.Focus()
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSConOne")
            Dim SQLCmd1 As New SqlCommand("Select * from DBO.tblAccounts where Indirect='True' and (isdeleted is null or isdeleted = 'false') and contractorID='" & Session("ContractorID") & "' order by AccountName", New SqlConnection(strConn))
            Try
                SQLCmd1.Connection.Open()
                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                If DRRec1.HasRows Then
                    While DRRec1.Read
                        Dim LI As New ListItem
                        LI.Text = DRRec1("AccountName").ToString
                        LI.Value = DRRec1("AccountID").ToString
                        DLAct.Items.Add(LI)
                    End While
                End If
                DRRec1.Close()
            Finally
                If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd1.Connection.Close()
                    SQLCmd1 = Nothing
                End If
            End Try

        End If
    End Sub
End Class
