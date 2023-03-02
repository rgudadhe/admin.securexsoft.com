
Partial Class Services_MoveUnrecognized
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Page.IsPostBack Then
        'Dim objConn As New Data.SqlClient.SqlConnection
        'objConn = OpenConnection(objConn)

        'Dim objCmdAcc As New Data.SqlClient.SqlCommand("SELECT A.AccountID,AccountName FROM tblAccounts A INNER JOIN tblAccountsServices SA ON A.AccountID=SA.AccountID INNER JOIN tblServices S ON SA.ServiceID=S.ServiceID WHERE ServiceName='SecureFax'", objConn)
        'Dim objRecAcc As Data.SqlClient.SqlDataReader = objCmdAcc.ExecuteReader

        If Not Page.IsPostBack Then
            tblRes.Visible = False
            tblAcc.Visible = False
        Else
            GetData(ViewState("Query"))
        End If

        'If objRecAcc.HasRows Then
        '    ddlAcc.Items.Clear()
        '    While objRecAcc.Read
        '        Dim varLstItem As New ListItem
        '        Dim varStrAccID As String = String.Empty
        '        Dim varStrAccName As String = String.Empty

        '        If Not objRecAcc.IsDBNull(objRecAcc.GetOrdinal("AccountID")) Then
        '            varStrAccID = objRecAcc("AccountID").ToString
        '        End If
        '        If Not objRecAcc.IsDBNull(objRecAcc.GetOrdinal("AccountName")) Then
        '            varStrAccName = objRecAcc("AccountName")
        '        End If

        '        If Not String.IsNullOrEmpty(varStrAccID) And Not String.IsNullOrEmpty(varStrAccName) Then
        '            varLstItem.Value = varStrAccID
        '            varLstItem.Text = varStrAccName
        '            ddlAcc.Items.Add(varLstItem)
        '        End If

        '    End While
        '    Dim varLstItemM As New ListItem
        '    varLstItemM.Value = ""
        '    varLstItemM.Text = "Please Select"
        '    ddlAcc.Items.Insert(0, varLstItemM)
        'End If
        'objRecAcc.Close()
        'objRecAcc = Nothing
        'objCmdAcc = Nothing
        'End If
    End Sub
    Public Function OpenConnection(ByRef Conn As Data.SqlClient.SqlConnection) As Data.SqlClient.SqlConnection
        'Conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Conn.ConnectionString = "Server=sqlmain\one;Database=ETS;UID=usersqlbkp;Pwd=y0u4@209#"
        Conn.Open()
        Return Conn
    End Function
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim varWhere As String = String.Empty
            varWhere = " WHERE RecID IS NOT NULL AND MoveDate IS NULL "

            Dim varStrSenderFaxNo As String = String.Empty
            Dim StartDate As String
            Dim EndDate As String

            varStrSenderFaxNo = Request("txtFaxNo")
            StartDate = Request("txtStartDate")
            EndDate = Request("txtEndDate")

            If Not String.IsNullOrEmpty(varStrSenderFaxNo) Then
                varWhere = varWhere & " AND SenderFaxNo = " + varStrSenderFaxNo
            End If

            If String.IsNullOrEmpty(StartDate) = False And String.IsNullOrEmpty(EndDate) = True Then
                varWhere = varWhere & " AND RecievedDate >='" + StartDate + "'"
            End If

            If String.IsNullOrEmpty(StartDate) = True And String.IsNullOrEmpty(EndDate) = False Then
                varWhere = varWhere & " AND RecievedDate <='" + EndDate + "'"
            End If

            If String.IsNullOrEmpty(StartDate) = False And String.IsNullOrEmpty(EndDate) = False Then
                varWhere = varWhere & " AND RecievedDate BETWEEN '" + StartDate + "' AND '" + EndDate + "'"
            End If

            GetData("SELECT RecID,FileName,SenderFaxNo,RecievedDate FROM Securefax.dbo.tblFAXUnrecognizedInbound  " & varWhere & "")

            tblRes.Visible = True
            tblAcc.Visible = True
        Catch ex As Exception
            Response.Write("Error:" & ex.Message)
        Finally
        End Try
    End Sub
    Protected Sub GetData(ByVal Str As String)
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            If String.IsNullOrEmpty(ViewState("Query")) Then
                ViewState("Query") = Str
            End If

            Dim objCmd As New Data.SqlClient.SqlCommand(Str, objConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader

            If objRec.HasRows Then
                tblRes.Rows.Clear()

                Dim vartblRowf As New TableRow
                Dim varTblCellRecIDf As New TableCell
                Dim varTblCellFileNamef As New TableCell
                Dim varTblCellSenderFaxNof As New TableCell
                Dim varTblCellRecievedDatef As New TableCell
                vartblRowf.CssClass = "HeaderDiv"
                vartblRowf.Font.Name = "Trebuchet MS"
                vartblRowf.Font.Size = "9"

                Dim varctrlChkAll As New CheckBox
                varctrlChkAll.ID = "ChkAll"
                varctrlChkAll.EnableViewState = True
                varctrlChkAll.Attributes.Add("OnClick", "javascript:chkALL(this)")

                varTblCellRecIDf.Controls.Add(varctrlChkAll)

                varTblCellFileNamef.Text = "File Name"
                varTblCellFileNamef.HorizontalAlign = HorizontalAlign.Center
                varTblCellSenderFaxNof.Text = "Sender FaxNo"
                varTblCellSenderFaxNof.HorizontalAlign = HorizontalAlign.Center
                varTblCellRecievedDatef.Text = "Recieved Date"
                varTblCellRecievedDatef.HorizontalAlign = HorizontalAlign.Center

                vartblRowf.Cells.Add(varTblCellRecIDf)
                vartblRowf.Cells.Add(varTblCellFileNamef)
                vartblRowf.Cells.Add(varTblCellSenderFaxNof)
                vartblRowf.Cells.Add(varTblCellRecievedDatef)

                tblRes.Rows.Add(vartblRowf)

                While objRec.Read
                    Dim varRecID As String = String.Empty
                    Dim varFileName As String = String.Empty
                    Dim varSenderFaxNo As String = String.Empty
                    Dim varRecievedDate As String = String.Empty

                    If Not objRec.IsDBNull(objRec.GetOrdinal("RecID")) Then
                        varRecID = objRec("RecID").ToString
                    End If
                    If Not objRec.IsDBNull(objRec.GetOrdinal("FileName")) Then
                        varFileName = objRec("FileName")
                    End If
                    If Not objRec.IsDBNull(objRec.GetOrdinal("SenderFaxNo")) Then
                        varSenderFaxNo = objRec("SenderFaxNo")
                    End If
                    If Not objRec.IsDBNull(objRec.GetOrdinal("RecievedDate")) Then
                        varRecievedDate = objRec("RecievedDate")
                    End If

                    Dim vartblRow As New TableRow
                    Dim varTblCellRecID As New TableCell
                    Dim varTblCellFileName As New TableCell
                    Dim varTblCellSenderFaxNo As New TableCell
                    Dim varTblCellRecievedDate As New TableCell

                    Dim varctrlChk As New CheckBox
                    varctrlChk.ID = varRecID.ToString
                    varctrlChk.EnableViewState = True
                    varctrlChk.AutoPostBack = False

                    varTblCellRecID.Controls.Add(varctrlChk)
                    varTblCellFileName.Text = varFileName
                    varTblCellSenderFaxNo.Text = varSenderFaxNo
                    varTblCellRecievedDate.Text = varRecievedDate

                    vartblRow.Cells.Add(varTblCellRecID)
                    vartblRow.Cells.Add(varTblCellFileName)
                    vartblRow.Cells.Add(varTblCellSenderFaxNo)
                    vartblRow.Cells.Add(varTblCellRecievedDate)

                    tblRes.Rows.Add(vartblRow)
                End While
            End If

            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

        Catch ex As Exception
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub btnMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMove.Click
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            For i As Integer = 1 To tblRes.Rows.Count - 1
                Dim vartblRow As TableRow
                vartblRow = tblRes.Rows(i)
                For Each varctrl As Control In vartblRow.Cells(0).Controls
                    Dim varStatus As CheckBox = DirectCast(vartblRow.Cells(0).FindControl(varctrl.ID.ToString), CheckBox)
                    If varStatus.Checked Then
                        '\\win11617\d$\ETS\FAXDocuments\Unrecognized\
                        System.IO.File.Move("\\win11617\d$\ETS\FAXDocuments\Unrecognized\" & vartblRow.Cells(1).Text, "\\win11617\d$\ETS\FAXDocuments\tobeimported\" & vartblRow.Cells(1).Text)
                        If System.IO.File.Exists("\\win11617\d$\ETS\FAXDocuments\tobeimported\" & vartblRow.Cells(1).Text) Then
                            Dim objCmdUpdate As New Data.SqlClient.SqlCommand("UPDATE Securefax.dbo.tblFAXUnrecognizedInbound SET MoveDate='" & Now() & "' WHERE RecID='" & varStatus.ID.ToString & "' ", objConn)
                            If objCmdUpdate.ExecuteNonQuery() < 0 Then
                                System.IO.File.Move("\\win11617\d$\ETS\FAXDocuments\tobeimported\" & vartblRow.Cells(1).Text, "\\win11617\d$\ETS\FAXDocuments\Unrecognized\" & vartblRow.Cells(1).Text)
                            End If
                        End If
                    End If
                Next
            Next
            'Response.Write("RowCount:" & tblRes.Rows.Count)
            Response.Write("<script language=javascript>alert('Files moved successfully');window.location.href='MoveUnrecognized.aspx';</script>")
        Catch ex As Exception
            Response.Write("Error:" & ex.Message)
        Finally
            If objConn.State <> Data.ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
