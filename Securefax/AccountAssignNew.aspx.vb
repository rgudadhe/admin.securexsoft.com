Imports System.Data
Imports System.Data.SqlClient
Partial Class AccountAssignNew
    Inherits BasePage
    Public Function OpenConnection(ByRef Conn As Data.SqlClient.SqlConnection) As Data.SqlClient.SqlConnection
        'Conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Conn.ConnectionString = "Server=sqlone;Database=ETS;UID=usersqlbkp;Pwd=y0u4@209#"
        Conn.Open()
        Return Conn
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                FillAccounts("Service")
                'RadFAX.Checked = True
            Else
                'If Request.QueryString("AccID") <> "" Then
                '    DropDownAccount.Items.FindByValue(Request.QueryString("AccId")).Selected = True
                '    DropDownAccount_SelectedIndexChanged()
                'End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub
    Protected Sub FillAccounts(ByVal str As String)
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Dim objDataSet As DataSet
        Dim objCmd1 As Data.SqlClient.SqlCommand
        Dim objDAtaAdapter As Data.SqlClient.SqlDataAdapter
        'If Trim(UCase(str)) = Trim(UCase("Service")) Or str = "" Then
        '    objCmd1 = New Data.SqlClient.SqlCommand("SELECT DISTINCT A.AccID AS 'AccID',Description FROM MDONE.DBO.TBLACCDETAILS A INNER JOIN DEMOGRAPHICS.DBO.Users FA ON A.AccNumber=FA.AccID WHERE A.ACCID IS NOT NULL AND FA.FAXService=1 ORDER BY DESCRIPTION ", objConn)
        'ElseIf Trim(UCase(str)) = Trim(UCase("ALL")) Then
        '    objCmd1 = New Data.SqlClient.SqlCommand("SELECT AccID,Description FROM MDONE.DBO.TBLACCDETAILS WHERE ACCID IS NOT NULL ORDER BY DESCRIPTION ", objConn)
        'End If
        objCmd1 = New Data.SqlClient.SqlCommand("SELECT AccountID,AccountName FROM tblAccounts where (Isdeleted is null or Isdeleted=0) order by AccountName", objConn)
        'A INNER JOIN tblAccountsServices SA ON A.AccountID=SA.AccountID INNER JOIN tblServices S ON SA.ServiceID=S.ServiceID WHERE ServiceName='Secure-Fax'
        objDAtaAdapter = New Data.SqlClient.SqlDataAdapter(objCmd1)
        objDataSet = New DataSet
        objDAtaAdapter.Fill(objDataSet)
        If objDataSet.Tables(0).Rows.Count > 0 Then
            DropDownAccount.Items.Clear()
            DropDownAccount.DataSource = objDataSet.Tables(0)
            DropDownAccount.DataTextField = objDataSet.Tables(0).Columns(1).ToString
            DropDownAccount.DataValueField = objDataSet.Tables(0).Columns(0).ToString
            DropDownAccount.DataBind()


        End If
        Dim varLstItem As New ListItem
        varLstItem.Text = "SELECT"
        varLstItem.Value = ""
        DropDownAccount.Items.Insert(0, varLstItem)
        objConn.Close()
        objConn = Nothing
    End Sub
    'Protected Sub RadAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadAll.CheckedChanged
    '    'Response.Write("Test")
    '    FillAccounts("ALL")
    '    RadAll.Checked = True
    '    chkFAXService.Checked = False
    '    lstAssigned.Items.Clear()
    'End Sub
    'Protected Sub RadFAX_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadFAX.CheckedChanged
    '    FillAccounts("Service")
    '    RadFAX.Checked = True
    '    lstAssigned.Items.Clear()
    '    chkFAXService.Checked = False
    'End Sub
    Protected Sub DropDownAccount_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownAccount.SelectedIndexChanged

        'chkFAXService.Checked = False
        Dim varStrAccID As String = DropDownAccount.Items(DropDownAccount.SelectedIndex).Value.ToString
        'If varStrAccID <> "" Then
        '    Dim objConn As New Data.SqlClient.SqlConnection
        '    objConn = OpenConnection(objConn)

        '    Dim varIntCount As Integer = 0
        '    'Get the status of account for the fax service assigned or not 
        '    Dim objCmdChk As New Data.SqlClient.SqlCommand("SELECT count(*) AS 'Count' FROM DEMOGRAPHICS.DBO.Users WHERE AccID=(SELECT AccNumber FROM MDONE.DBO.TBLACCDETAILS WHERE AccID='" & varStrAccID & "') AND FAXService=1 ", objConn)
        '    Dim objRecChk As Data.SqlClient.SqlDataReader = objCmdChk.ExecuteReader

        '    If objRecChk.HasRows Then
        '        While objRecChk.Read
        '            If Not objRecChk.IsDBNull(objRecChk.GetOrdinal("Count")) Then
        '                varIntCount = objRecChk("Count")
        '            End If
        '        End While
        '    End If
        '    objRecChk.Close()
        '    objRecChk = Nothing
        '    objCmdChk = Nothing
        '    'Response.Write(varIntCount)
        '    If varIntCount > 0 Then
        '        chkFAXService.Checked = True
        '    End If

        '    objConn.Close()
        '    objConn = Nothing

        FillFaxNo(varStrAccID)
        'End If
    End Sub
    Protected Sub FillFaxNo(ByVal AccID As String)
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        lstAssigned.Items.Clear()

        Dim objCmdAssign As New Data.SqlClient.SqlCommand("SELECT AssignID,FAXNo FROM securefax.dbo.tblFAXAccountAssignedFAXNo WHERE AccID='" & AccID & "'", objConn)
        Dim objRecAssign As Data.SqlClient.SqlDataReader = objCmdAssign.ExecuteReader

        If objRecAssign.HasRows Then

            While objRecAssign.Read
                Dim varFAXNo As String = String.Empty
                Dim varAssignID As String = String.Empty
                Dim varLstItem As New ListItem
                If Not objRecAssign.IsDBNull(objRecAssign.GetOrdinal("AssignID")) Then
                    varAssignID = objRecAssign.GetGuid(objRecAssign.GetOrdinal("AssignID")).ToString
                End If
                If Not objRecAssign.IsDBNull(objRecAssign.GetOrdinal("FAXNo")) Then
                    varFAXNo = objRecAssign.GetString(objRecAssign.GetOrdinal("FAXNo"))
                End If

                If varAssignID <> "" And varFAXNo <> "" Then
                    varLstItem.Value = varAssignID
                    varLstItem.Text = varFAXNo

                    lstAssigned.Items.Add(varLstItem)
                End If
            End While
        End If

        objRecAssign.Close()
        objRecAssign = Nothing
        objCmdAssign = Nothing

    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
        Try
            Dim varFaxNo
            varFaxNo = Trim(txtNo.Text)

            If Not String.IsNullOrEmpty(varFaxNo) Then
                'Dim varRet = "alert('" & IsFaxNoExist(varFaxNo) & "')"
                'ScriptManager.RegisterClientScriptBlock(btnAdd, btnAdd.GetType(), "msg", varRet, True)
                If IsFaxNoExist(varFaxNo) = False Then
                    Dim varStrAccId As String = String.Empty
                    varStrAccId = DropDownAccount.Items(DropDownAccount.SelectedIndex).Value.ToString
                    Dim objConn As New Data.SqlClient.SqlConnection
                    objConn = OpenConnection(objConn)

                    'Delete the old entry of the fax no
                    Dim DeleteCmd As Data.SqlClient.SqlCommand
                    DeleteCmd = New Data.SqlClient.SqlCommand
                    DeleteCmd.CommandType = Data.CommandType.Text
                    DeleteCmd.CommandText = "DELETE FROM securefax.dbo.tblFAXAccountAssignedFAXNo WHERE AccID = '" & varStrAccId & "' AND FaxNo='" & varFaxNo & "'"
                    DeleteCmd.Connection = objConn
                    DeleteCmd.ExecuteNonQuery()

                    'insert the fax no in blocked list
                    Dim InsertCmd As Data.SqlClient.SqlCommand
                    InsertCmd = New Data.SqlClient.SqlCommand
                    InsertCmd.CommandType = Data.CommandType.Text
                    InsertCmd.CommandText = "INSERT INTO securefax.dbo.tblFAXAccountAssignedFAXNo (FaxNo,AccID,ModifiedBy,ModifiedOn) VALUES('" & varFaxNo & "','" & varStrAccId & "','" & Session("UserID") & "','" & Now() & "')"
                    InsertCmd.Connection = objConn
                    InsertCmd.ExecuteNonQuery()

                    'end the deletion of old fax no entry

                    objConn.Close()
                    objConn = Nothing

                    Dim varStrMsg As String
                    varStrMsg = "alert('PGI Code assigned successfully');"

                    FillFaxNo(varStrAccId)
                    txtNo.Text = ""
                    Accordion.SelectedIndex = -1
                    ScriptManager.RegisterClientScriptBlock(btnAdd, btnAdd.GetType(), "msg", varStrMsg, True)
                Else
                    ScriptManager.RegisterClientScriptBlock(btnAdd, btnAdd.GetType(), "msg", "alert('Fax number already set for any other account,please try again...');", True)
                End If
            End If
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(btnAdd, btnAdd.GetType(), "msg", ex.Message, True)
        End Try

        



        'ScriptManager.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('Fax no assigned sucessfully');</script>", , True)
        'ClientScript.RegisterClientScriptBlock(Page.GetType, "Script", "<script language='javascript'>alert('Fax no assigned sucessfully');</script>")

    End Sub
    Protected Function IsFaxNoExist(ByVal FaxNo) As Boolean
        Dim objConn As New Data.SqlClient.SqlConnection
        Dim varCount As Integer = 0
        Try
            objConn = OpenConnection(objConn)

            Dim objCmdNoAssign As New Data.SqlClient.SqlCommand("select Count(*) as 'Counter' from securefax.dbo.tblFAXAccountAssignedFAXNo where FaxNo='" & FaxNo & "'", objConn)
            Dim objRecNoAssign As Data.SqlClient.SqlDataReader = objCmdNoAssign.ExecuteReader

            If objRecNoAssign.HasRows Then
                While objRecNoAssign.Read
                    If Not objRecNoAssign.IsDBNull(objRecNoAssign.GetOrdinal("Counter")) Then
                        varCount = CInt(objRecNoAssign("Counter").ToString)
                    End If
                End While
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If objConn.State <> ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try

        If varCount > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove.Click
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Dim varStrAccId As String = String.Empty
        varStrAccId = DropDownAccount.Items(DropDownAccount.SelectedIndex).Value.ToString

        Dim varIndex As String
        varIndex = CStr(hdnIndex.Value)
        Dim varStrTemp()
        'Response.Write("varIndex" & varIndex)
        'Response.End()
        varStrTemp = Split(varIndex, ",")
        UBound(varStrTemp)
        For i As Integer = 0 To UBound(varStrTemp)
            If i > -1 Then
                Dim varAssignID = varStrTemp(i)

                Dim varStrQuery As String = String.Empty
                varStrQuery = "DELETE FROM securefax.dbo.tblFAXAccountAssignedFAXNo WHERE AssignID='" & varAssignID & "'"
                Dim objCmd As New Data.SqlClient.SqlCommand(varStrQuery, objConn)
                objCmd.ExecuteNonQuery()
            End If
        Next
        'Response.End()
        'Response.Write("<script type=""text/javascript"" language=javascript> alert(""Contact removed sucessfully!!!"");</script>")

        Dim varStrMsg As String
        varStrMsg = "alert('PGI Code removed successfully');"

        FillFaxNo(varStrAccId)
        ScriptManager.RegisterClientScriptBlock(btnRemove, btnRemove.GetType(), "msg", varStrMsg, True)


    End Sub
    'Protected Sub chkFAXService_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkFAXService.CheckedChanged
    '    Dim varStrAccID As String = String.Empty
    '    varStrAccID = DropDownAccount.Items(DropDownAccount.SelectedIndex).Value.ToString

    '    Dim varStrStatus As Boolean = False
    '    varStrStatus = chkFAXService.Checked
    '    'Response.Write(varStrStatus)
    '    'Response.End()
    '    If varStrAccID <> "" Then
    '        Dim objConn As New Data.SqlClient.SqlConnection
    '        objConn = OpenConnection(objConn)

    '        Dim varStrQuery As String = String.Empty
    '        If varStrStatus Then
    '            varStrQuery = "UPDATE DEMOGRAPHICS.DBO.Users SET FAXService=1 WHERE AccID=(SELECT AccNumber FROM MDONE.DBO.TBLACCDETAILS WHERE AccID='" & varStrAccID & "')"
    '            'chkFAXService.Checked = True
    '            'Response.Write("If" & varStrQuery)
    '        Else
    '            varStrQuery = "UPDATE DEMOGRAPHICS.DBO.Users SET FAXService = NULL WHERE AccID=(SELECT AccNumber FROM MDONE.DBO.TBLACCDETAILS WHERE AccID='" & varStrAccID & "')"
    '            'chkFAXService.Checked = False
    '            'Response.Write("Else" & varStrQuery)
    '        End If
    '        'Response.End()
    '        Dim objCmd As New Data.SqlClient.SqlCommand(varStrQuery, objConn)
    '        objCmd.ExecuteNonQuery()

    '        objConn.Close()
    '        objConn = Nothing
    '        objCmd = Nothing
    '    End If
    'End Sub
End Class
