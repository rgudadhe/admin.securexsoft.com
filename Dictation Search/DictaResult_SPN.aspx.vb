Imports System
Imports System.Data
Namespace ets
    Partial Class Dictation_Search_DictaResult
        Inherits BasePage
        Private WhereClause As StringBuilder
        Private Sub DBind()
            WhereClause = New StringBuilder
            Dim OrderByClause As String
            Dim IsUserCri As Boolean
            If Session("IsContractor") <> 1 Then
                lstOptions.Items.RemoveAt(5)
                lstOptions.Items.RemoveAt(4)
                lstOptions.Items.RemoveAt(3)
            End If

            If IsPostBack = False And String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
                Dim Track As String = Request("Track").ToString()
                Dim Cust As String = Request("Cust").ToString
                Dim PFirst As String = Request("PFirst").ToString
                Dim PLast As String = Request("PLast").ToString

                Dim sDate As String = Request("sDate").ToString
                Dim eDate As String = Request("eDate").ToString

                Dim DCode As String = Request("DCode").ToString
                Dim AccName As String = Request("AccName").ToString
                Dim ACCNum As String = String.Empty
                Dim PIN As String = String.Empty
                Dim PatientF As String = String.Empty
                Dim PatientL As String = String.Empty
                Dim DOS As String = String.Empty
                Dim DOB As String = String.Empty
                Dim MRN As String = String.Empty
                Dim TType As String = String.Empty
                Dim TName As String = String.Empty

                Dim i As Integer = 1

                Do While Not i = 4
                    Dim ifield As String = Request("lblOp" & i).ToString
                    Select Case ifield
                        Case "Account#"
                            ACCNum = Request("valOp" & i).ToString
                        Case "PIN"
                            PIN = Request("valOp" & i).ToString
                        Case "Patient First"
                            PatientF = Request("valOp" & i).ToString
                        Case "Patient Last"
                            PatientL = Request("valOp" & i).ToString
                        Case "Date Of Service"
                            DOS = Request("valOp" & i).ToString
                        Case "Date of Birth"
                            DOB = Request("valOp" & i).ToString
                        Case "MRN"
                            MRN = Request("valOp" & i).ToString
                        Case "Template Type"
                            TType = Request("valOp" & i).ToString
                        Case "Template Name"
                            TName = Request("valOp" & i).ToString
                    End Select
                    i = i + 1
                Loop

                Dim Status As String = Request("UStatus")

                Dim Level As String = String.Empty
                If Not String.IsNullOrEmpty(Request("Level")) Then
                    Level = Request("Level").ToString
                End If
                Dim UserID As String = String.Empty
                If Not String.IsNullOrEmpty(Request("UserID")) Then
                    UserID = Request("UserID").ToString
                End If
                Dim UserName As String = String.Empty
                If Not String.IsNullOrEmpty(Request("UserName")) Then
                    UserName = Request("UserName").ToString
                End If
                If String.IsNullOrEmpty(sDate) = False And String.IsNullOrEmpty(eDate) = False Then
                    If DateDiff(DateInterval.Day, CDate(sDate), CDate(eDate)) < 0 Then
                        iMain.Visible = False
                        lblMessage.Text = "Start date cannot be greater than end date!"
                        iMessage.Visible = True
                        Exit Sub
                    End If
                End If
                If String.IsNullOrEmpty(UserID) = False Or String.IsNullOrEmpty(UserName) = False Then
                    IsUserCri = True
                End If
                WhereClause = New ets.BL.Dictations().getDictationSearchWhereClause(Track, Status, Cust, PFirst, PLast, PIN, sDate, eDate, DCode, AccName, ACCNum, TName, TType, PatientL, DOB, DOS, MRN, UserID, UserName, Level, Session("IsContractor").ToString, Session("ParentID").ToString, Session("ContractorID").ToString)
            End If

            OrderByClause = " ORDER BY SubmitDate "


            Dim objDS As System.Data.DataSet = New ets.BL.Dictations().DictationSearch(WhereClause.ToString, OrderByClause.ToString, IsUserCri, 1)

            If String.IsNullOrEmpty(intRecordCount.Value) Then
                intRecordCount.Value = CStr(objDS.Tables(0).Rows.Count)
            End If
            If intRecordCount.Value <= 0 Then

                iMain.Visible = False
                lblMessage.Text = "No Records Found!"
                iMessage.Visible = True
            End If


            dlist.DataSource = objDS    '.Tables(0).DefaultView
            dlist.DataBind()
            objDS.Dispose()


            If dlist.Rows.Count > 0 Then
                dlist.ShowFooter = True
                dlist.UseAccessibleHeader = True
                dlist.HeaderRow.TableSection = TableRowSection.TableHeader
                dlist.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        End Sub
        Function datediffToMe(ByVal d1 As Double, ByVal d2 As Date) As String
            Dim DueDate As Date
            DueDate = DateAdd(DateInterval.Hour, d1, d2)
            Return DateDiff(DateInterval.Hour, Now(), DueDate).ToString
        End Function

        Protected Sub btnStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            ' Try
            Dim SQLString As String = String.Empty
            Dim strID As New ArrayList
            Dim JobNumber As New ArrayList
            Dim CustJobID As New ArrayList
            Dim btn As Button = CType(sender, Button)
            Dim ddlOp As DropDownList = btn.Parent.FindControl("lstOptions")


            Dim DT As New DataTable
            DT.Columns.Add("ActNo", GetType(System.Int32))
            DT.Columns.Add("CurrentStatus", GetType(System.Int32))
            DT.Columns.Add("TranscriptionID", GetType(System.String))
            DT.Columns.Add("JobNo", GetType(System.Int32))
            For Each DR As GridViewRow In dlist.Rows
                Dim chk As CheckBox = DR.FindControl("chkJob")

                If Not chk Is Nothing Then
                    If chk.Checked Then
                        Dim DRow As DataRow = DT.NewRow

                        Dim hdn As HiddenField = chk.Parent.FindControl("hdnTransID")
                        DRow("TranscriptionID") = hdn.Value.ToString

                        hdn = chk.Parent.FindControl("hdnAccNo")
                        DRow("ActNo") = hdn.Value

                        hdn = chk.Parent.FindControl("hdnStatus")
                        DRow("CurrentStatus") = hdn.Value

                        Dim hdnJobNo As HiddenField = chk.Parent.FindControl("hdnJobNo")
                        DRow("JobNo") = hdnJobNo.Value

                        DT.Rows.Add(DRow)
                        strID.Add(hdn.Value.ToString)
                    End If
                End If
            Next
            Dim clsDic As New ets.BL.Dictations

            Dim varStatus As String = String.Empty
            If ddlOp.SelectedValue = "1" Then
                Dim ddlSt As DropDownList = btn.Parent.FindControl("lstStatus")
                iMain.Visible = False
                Dim pnl As Panel = btn.Parent.FindControl("iMessage")
                pnl.Visible = True

                Dim lbl As Label = btn.Parent.FindControl("lblMessage")
                lbl.Visible = True
                DT = clsDic.setDictationStatus(ddlSt.SelectedValue, Session("UserID"), Request.UserHostAddress(), DT)
                'lblMessage.Text = DT.Rows(0).Item("Result").ToString
                lblMessage.Text = "Job Status updated : "
            ElseIf ddlOp.SelectedValue = "2" Then
                Dim lstUser As TextBox = btn.Parent.FindControl("txtUser")
                Dim lstLevel As DropDownList = btn.Parent.FindControl("lstLevel")
                Dim UserLevel As Integer = CInt(lstLevel.SelectedValue)
                Dim UserID As String = String.Empty

                Dim clsusers As New ets.BL.Users
                With clsusers
                    .ContractorID = Session("contractorID")
                    ._WhereString.Append(" AND Firstname + ' ' + Lastname + '(' + UserName + ')'='" & lstUser.Text & "'")
                    UserID = .getUserID
                    lstUser.Text = UserID
                End With
                clsusers = Nothing

                DT = clsDic.AssignDictations(UserID, UserLevel + 100, Session("UserID"), False, Request.UserHostAddress(), DT)
                txtUser.Text = DT.Rows(2).Item("Result").ToString
                lblMessage.Text = "Job Assigned to users : "
            ElseIf ddlOp.SelectedValue = "3" Then
                Dim txtTAT As TextBox = btn.Parent.FindControl("txtTAT")
                DT = clsDic.setDictationTAT(txtTAT.Text, DT)
                txtTAT.Text = DT.Rows(2).Item("Result").ToString & DT.Rows.Count & DT.Rows(0).Item("JobNo").ToString
                lblMessage.Text = "Job TAT updated : "
            ElseIf ddlOp.SelectedValue = "4" Then
                DT = clsDic.setDictationSamples(Session("UserID"), DT)
                lblMessage.Text = DT.Rows(0).Item("Result").ToString
                lblMessage.Text = "Job Set for samples : "
            ElseIf ddlOp.SelectedValue = "5" Then
                Dim txt As TextBox = btn.Parent.FindControl("txtPhy")
                Dim DSPhy As New DataSet
                Dim clsPhy As New ets.BL.Physicians
                With clsPhy
                    DSPhy = .getPhywithActDetails(Session("contractorID"), Session("WorkGroupID"))
                End With
                clsPhy = Nothing
                Dim DR() As DataRow = DSPhy.Tables(0).Select("FirstName+' '+LastName+'('+AccountNo+')'='" & txt.Text & "'")
                DSPhy.Dispose()
                DT = clsDic.setDictationDictator(DR(0).Item("PhysicianID").ToString, DR(0).Item("AccountNo").ToString, DT)
                txt.Text = DT.Rows(0).Item("Result").ToString
                lblMessage.Text = "Physicians changes for Jobs : "
            End If
            Dim varForloopCount As Integer = 0
            If DT.Rows.Count > 0 Then

                Dim varTemp As String = String.Empty
                varTemp = "<table><tr><td class=alt1>Job#</td><td class=alt1>Updated</td></tr>"
                For Each DR1 As Data.DataRow In DT.Rows
                    varTemp = varTemp & "<tr><td>" & DR1("JobNo").ToString & "</td><td>" & DR1("Result").ToString & "</td></tr>"
                    varForloopCount = varForloopCount + 1
                Next
                If varForloopCount > 0 Then
                    varTemp = varTemp & "</table>"
                    lblMessage.Text = varTemp.ToString
                End If
                'TextArea1.InnerText = varTemp
            End If

            txtTAT.Text = txtTAT.Text & " Count Rows : " & varForloopCount


            DT.Dispose()
            clsDic = Nothing


            'If ddlOp.SelectedValue = "1" Then
            '    Dim ddlSt As DropDownList = btn.Parent.FindControl("lstStatus")
            '    If ddlSt.SelectedValue <> "" Then
            '        If strID.Count > 0 Then
            '            Dim CountUpdated, i As Integer
            '            Dim ConString As String
            '            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            '            Dim tr As New TableRow
            '            tr.CssClass = "SMParentSelected"
            '            Dim tc1 As New TableCell
            '            tc1.Text = "Tracking#"
            '            Dim tc2 As New TableCell
            '            tc2.Text = "Client Job#"
            '            Dim tc3 As New TableCell
            '            tc3.Text = "Status"
            '            Dim tc4 As New TableCell
            '            tc4.Text = "User Name"
            '            Dim tc5 As New TableCell
            '            tc5.Text = "Transaction"
            '            tr.Cells.Add(tc1)
            '            tr.Cells.Add(tc2)
            '            tr.Cells.Add(tc3)
            '            tr.Cells.Add(tc4)
            '            tr.Cells.Add(tc5)
            '            'tblUp.Rows.Add(tr)
            '            For Each val As String In strID
            '                Dim oConn As New Data.SqlClient.SqlConnection
            '                Dim oCommand As New Data.SqlClient.SqlCommand
            '                Dim thisTransaction As Data.SqlClient.SqlTransaction
            '                oConn.ConnectionString = ConString
            '                oConn.Open()
            '                thisTransaction = oConn.BeginTransaction()
            '                Try
            '                    SQLString = "update tblTranscriptionMain set Status = " & ddlSt.SelectedValue & ", cstatus = NULL, FReview=NULL, Review=NULL, modified=NUll, downloaded = NULL, printed = NULL, lcstatus=NULL where TranscriptionID='" & val & "'"
            '                    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                    oCommand.Transaction = thisTransaction
            '                    Dim RowsMain As Integer = oCommand.ExecuteNonQuery()


            '                    SQLString = "insert into  tblTranscriptionLog(TranscriptionID,AssignedBy,Status,DateModified,IP) " & _
            '                    "values('" & val & "','" & Session("UserID") & "'," & ddlSt.SelectedValue & ",'" & Now() & "','" & Request.UserHostAddress() & "')"

            '                    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                    oCommand.Transaction = thisTransaction
            '                    Dim RowsLog As Integer = oCommand.ExecuteNonQuery()

            '                    SQLString = "SELECT FxStatus.Status as C3, TM.CustJobID as C2, TM.JobNumber as C1, U.UserName as C4 FROM tblTranscriptionMain AS TM INNER JOIN " & _
            '                                "(SELECT     UserID,transcriptionid FROM tblTranscriptionLog AS TLog " & _
            '                                "WHERE (DateModified = (SELECT     MAX(DateModified) AS DateModified FROM tblTranscriptionLog  " & _
            '                                "where TLog.TranscriptionID = TranscriptionID))) AS TL ON TM.TranscriptionID = TL.transcriptionid INNER JOIN " & _
            '                                "dbo.getStatus() as FxStatus on Fxstatus.LevelNo=TM.Status INNER JOIN tblUsers AS U ON TL.UserID = U.UserID " & _
            '                                "WHERE TM.TranscriptionID = '" & val & "'"
            '                    '"union  " & _
            '                    '"SELECT 'CheckedOut '+ +PL.LevelName as C3, TM.CustJobID as  C2, TM.JobNumber as C1, U.UserName as C4 FROM tblTranscriptionMain AS TM INNER JOIN " & _
            '                    '"(SELECT     UserID,transcriptionid FROM tblTranscriptionLog AS TLog " & _
            '                    '"WHERE (DateModified = (SELECT     MAX(DateModified) AS DateModified FROM tblTranscriptionLog " & _
            '                    '"where TLog.TranscriptionID = TranscriptionID))) AS TL ON TM.TranscriptionID = TL.transcriptionid INNER JOIN " & _
            '                    '"tblProductionLevels AS PL ON TM.Status = PL.LevelNo+100 INNER JOIN tblUsers AS U ON TL.UserID = U.UserID " & _
            '                    '"WHERE TM.TranscriptionID = '" & val & "'"
            '                    'lblMessage.Text = SQLString
            '                    If RowsLog > 0 And RowsMain > 0 Then
            '                        thisTransaction.Commit()
            '                        writeStatus(SQLString, "Successfull")
            '                        CountUpdated = CountUpdated + 1
            '                    Else
            '                        thisTransaction.Rollback()
            '                        writeStatus(SQLString, "Failed")
            '                    End If
            '                Catch ex As Exception
            '                    thisTransaction.Rollback()
            '                    writeStatus(SQLString, "Failed")
            '                Finally
            '                    If oConn.State <> Data.ConnectionState.Closed Then
            '                        oConn.Close()
            '                    End If
            '                End Try
            '                i = i + 1
            '            Next

            '            lblMessage.Text = "Total Records updated :" & CountUpdated & " Failed :" & i - CountUpdated
            '            iMain.Visible = False

            '            iMessage.Visible = True
            '        End If
            '    End If
            'ElseIf ddlOp.SelectedValue = "3" Then
            '    Dim txt As TextBox = btn.Parent.FindControl("txtTAT")
            '    Dim CountUpdated, i As Integer
            '    If txt.Text <> "" Then
            '        If strID.Count > 0 Then
            '            Dim ConString As String
            '            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            '            Dim tr As New TableRow
            '            tr.CssClass = "SMParentSelected"
            '            Dim tc1 As New TableCell
            '            tc1.Text = "Tracking#"
            '            Dim tc2 As New TableCell
            '            tc2.Text = "Client Job#"
            '            Dim tc3 As New TableCell
            '            tc3.Text = "Status"
            '            Dim tc4 As New TableCell
            '            tc4.Text = "TAT"
            '            Dim tc5 As New TableCell
            '            tc5.Text = "Transaction"
            '            tr.Cells.Add(tc1)
            '            tr.Cells.Add(tc2)
            '            tr.Cells.Add(tc3)
            '            tr.Cells.Add(tc4)
            '            tr.Cells.Add(tc5)
            '            'tblUp.Rows.Add(tr)
            '            For Each val As String In strID
            '                Dim oConn As New Data.SqlClient.SqlConnection
            '                Dim oCommand As New Data.SqlClient.SqlCommand
            '                Dim thisTransaction As Data.SqlClient.SqlTransaction
            '                oConn.ConnectionString = ConString
            '                oConn.Open()
            '                thisTransaction = oConn.BeginTransaction()
            '                Try
            '                    SQLString = "update tblTranscriptionMain set TAT = " & CInt(txt.Text) & ", DueDate = DateAdd(hour," & CInt(txt.Text) & ", SubmitDate) where TranscriptionID='" & val & "'"
            '                    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                    oCommand.Transaction = thisTransaction
            '                    SQLString = "SELECT FxStatus.Status AS C3, TM.CustJobID AS C2, TM.JobNumber AS C1, TM.TAT AS C4 FROM tblTranscriptionMain AS TM INNER JOIN " & _
            '                    "dbo.getStatus() as FxStatus on Fxstatus.LevelNo=TM.Status WHERE TM.TranscriptionID = '" & val & "'"
            '                    '"union " & _
            '                    '"SELECT 'CheckedOut '+ +PL.LevelName AS C3, TM.CustJobID AS C2, TM.JobNumber AS C1, TM.TAT AS C4 FROM tblTranscriptionMain AS TM INNER JOIN " & _
            '                    '"tblProductionLevels AS PL ON TM.Status = PL.LevelNo+100 WHERE TM.TranscriptionID = '" & val & "'"

            '                    If oCommand.ExecuteNonQuery > 0 Then
            '                        thisTransaction.Commit()
            '                        writeStatus(SQLString, "Successfull")
            '                        CountUpdated = CountUpdated + 1
            '                    Else
            '                        thisTransaction.Rollback()
            '                        writeStatus(SQLString, "Failed")
            '                    End If
            '                    oConn.Close()
            '                Catch ex As Exception
            '                    thisTransaction.Rollback()
            '                    writeStatus(SQLString, "Failed")
            '                Finally
            '                    If oConn.State = Data.ConnectionState.Open Then oConn.Close()
            '                End Try
            '                i = i + 1
            '            Next
            '            lblMessage.Text = "Total Records updated :" & CountUpdated & " Failed :" & i - CountUpdated
            '            iMain.Visible = False
            '            iMessage.Visible = True
            '        End If
            '    End If
            'ElseIf ddlOp.SelectedValue = "2" Then
            '    'Dim lstUser As DropDownList = btn.Parent.FindControl("lstUser")
            '    Dim lstUser As TextBox = btn.Parent.FindControl("txtUser")
            '    Dim UserID As String = String.Empty 'lstUser.SelectedValue.ToString
            '    Dim lstLevel As DropDownList = btn.Parent.FindControl("lstLevel")
            '    Dim UserLevel As Integer = CInt(lstLevel.SelectedValue)
            '    'If UserID <> "" Then
            '    If lstUser.Text <> "" Then
            '        If strID.Count > 0 Then
            '            Dim tr As New TableRow
            '            tr.CssClass = "SMParentSelected"
            '            Dim tc1 As New TableCell
            '            tc1.Text = "Tracking#"
            '            Dim tc2 As New TableCell
            '            tc2.Text = "Client Job#"
            '            Dim tc3 As New TableCell
            '            tc3.Text = "Status"
            '            Dim tc4 As New TableCell
            '            tc4.Text = "User Name"
            '            Dim tc5 As New TableCell
            '            tc5.Text = "Transaction"
            '            tr.Cells.Add(tc1)
            '            tr.Cells.Add(tc2)
            '            tr.Cells.Add(tc3)
            '            tr.Cells.Add(tc4)
            '            tr.Cells.Add(tc5)
            '            'tblUp.Rows.Add(tr)
            '            Dim ConString As String
            '            Dim sQuery As String
            '            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

            '            Dim i, CountUpdated As Integer
            '            For Each val As String In strID

            '                Dim strMessage As String = String.Empty
            '                Dim oConn As New Data.SqlClient.SqlConnection
            '                Dim oCommand As New Data.SqlClient.SqlCommand
            '                Dim thisTransaction As Data.SqlClient.SqlTransaction
            '                oConn.ConnectionString = ConString
            '                oConn.Open()
            '                thisTransaction = oConn.BeginTransaction()
            '                Try
            '                    SQLString = "Select UserID from tblUsers where Firstname + ' ' + Lastname + '(' + UserName + ')'='" & lstUser.Text & "'"
            '                    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                    oCommand.Transaction = thisTransaction
            '                    Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
            '                    oRec.Read()
            '                    UserID = oRec("UserID").ToString
            '                    oRec.Close()
            '                    oRec = Nothing
            '                    'Response.Write(UserID)
            '                    If Not String.IsNullOrEmpty(UserID) Then
            '                        SQLString = "update tblTranscriptionMain set Status = Status + 100 , cstatus = NULL, FReview=NULL, Review=NULL, modified=NUll, downloaded = NULL, printed = NULL, lcstatus=NULL  where TranscriptionID='" & val & "' and Status=" & UserLevel
            '                        oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                        oCommand.Transaction = thisTransaction
            '                        Dim rowsMain As Integer = oCommand.ExecuteNonQuery()

            '                        SQLString = "insert into  tblTranscriptionLog(TranscriptionID,UserID,UserLevel,Status,DateModified,IP,AssignedBy) " & _
            '                        "values('" & val & "','" & UserID & "'," & UserLevel & "," & UserLevel + 100 & ",'" & Now() & "','" & Request.UserHostAddress() & "','" & Session("UserID") & "')"
            '                        oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                        oCommand.Transaction = thisTransaction
            '                        Dim rowsLog As Integer = oCommand.ExecuteNonQuery()

            '                        SQLString = "DELETE FROM tblTranscriptionCKDStatus WHERE TranscriptionID='" & val & "' and Status= " & UserLevel + 100
            '                        oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                        oCommand.Transaction = thisTransaction
            '                        oCommand.ExecuteNonQuery()
            '                        SQLString = "Insert Into tblTranscriptionCKDStatus(TranscriptionID,UserID,Status,DateModified) Values('" & val & "','" & UserID & "'," & UserLevel + 100 & ",'" & Now() & "')"
            '                        oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                        oCommand.Transaction = thisTransaction
            '                        Dim rowsCKStatus As Integer = oCommand.ExecuteNonQuery()

            '                        sQuery = "SELECT FxStatus.Status as C3, TM.CustJobID as C2, TM.JobNumber as C1, U.UserName as C4 FROM tblTranscriptionMain AS TM INNER JOIN " & _
            '                                    "(SELECT     UserID,transcriptionid FROM tblTranscriptionLog AS TLog " & _
            '                                    "WHERE (UserLevel IS NOT NULL) AND (DateModified = (SELECT     MAX(DateModified) AS DateModified FROM tblTranscriptionLog  " & _
            '                                    "where TLog.TranscriptionID = TranscriptionID))) AS TL ON TM.TranscriptionID = TL.transcriptionid INNER JOIN " & _
            '                                    "dbo.getStatus() as FxStatus on Fxstatus.LevelNo=TM.Status INNER JOIN tblUsers AS U ON TL.UserID = U.UserID " & _
            '                                    "WHERE TM.TranscriptionID = '" & val & "'"
            '                        '"union  " & _
            '                        '"SELECT 'CheckedOut '+ +PL.LevelName as C3, TM.CustJobID as  C2, TM.JobNumber as C1, U.UserName as C4 FROM tblTranscriptionMain AS TM INNER JOIN " & _
            '                        '"(SELECT     UserID,transcriptionid FROM tblTranscriptionLog AS TLog " & _
            '                        '"WHERE (UserLevel IS NOT NULL) AND (DateModified = (SELECT     MAX(DateModified) AS DateModified FROM tblTranscriptionLog " & _
            '                        '"where TLog.TranscriptionID = TranscriptionID))) AS TL ON TM.TranscriptionID = TL.transcriptionid INNER JOIN " & _
            '                        '"tblProductionLevels AS PL ON TM.Status = PL.LevelNo+100 INNER JOIN tblUsers AS U ON TL.UserID = U.UserID " & _
            '                        '"WHERE TM.TranscriptionID = '" & val & "'"

            '                        If rowsMain > 0 And rowsLog > 0 And rowsCKStatus > 0 Then
            '                            thisTransaction.Commit()
            '                            writeStatus(sQuery, "Successfull")
            '                            CountUpdated = CountUpdated + 1
            '                        Else
            '                            thisTransaction.Rollback()
            '                            writeStatus(sQuery, "Failed")
            '                        End If
            '                    Else
            '                        lblMessage.Text = "User not found"
            '                        iMain.Visible = False
            '                        iMessage.Visible = True
            '                        Exit Sub
            '                    End If
            '                Catch ex As Exception
            '                    thisTransaction.Rollback()
            '                    writeStatus(sQuery, "Failed")
            '                Finally
            '                    If oConn.State <> Data.ConnectionState.Closed Then
            '                        oConn.Close()
            '                    End If
            '                End Try

            '                i = i + 1
            '            Next
            '            lblMessage.Text = "Total Records updated :" & CountUpdated & " Failed :" & i - CountUpdated
            '            iMain.Visible = False
            '            iMessage.Visible = True
            '        End If
            '    End If
            'ElseIf ddlOp.SelectedValue = "4" Then

            '    If strID.Count > 0 Then
            '        Dim CountUpdated, i As Integer
            '        Dim ConString As String
            '        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            '        Dim tr As New TableRow
            '        tr.CssClass = "SMParentSelected"
            '        Dim tc1 As New TableCell
            '        tc1.Text = "Tracking#"
            '        Dim tc2 As New TableCell
            '        tc2.Text = "Client Job#"
            '        Dim tc3 As New TableCell
            '        tc3.Text = "Status"
            '        Dim tc4 As New TableCell
            '        tc4.Text = "Submit Date"
            '        Dim tc5 As New TableCell
            '        tc5.Text = "Transaction"
            '        tr.Cells.Add(tc1)
            '        tr.Cells.Add(tc2)
            '        tr.Cells.Add(tc3)
            '        tr.Cells.Add(tc4)
            '        tr.Cells.Add(tc5)
            '        'tblUp.Rows.Add(tr)
            '        For Each val As String In strID
            '            Dim oConn As New Data.SqlClient.SqlConnection
            '            Dim oCommand As New Data.SqlClient.SqlCommand
            '            Dim thisTransaction As Data.SqlClient.SqlTransaction
            '            Dim sQuery As String
            '            oConn.ConnectionString = ConString
            '            oConn.Open()
            '            thisTransaction = oConn.BeginTransaction()
            '            Try
            '                SQLString = "select count(*) as IsExists from tblSetSamples where TranscriptionID = '" & val & "'"
            '                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                oCommand.Transaction = thisTransaction
            '                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
            '                oRec.Read()
            '                sQuery = "SELECT TM.JobNumber AS C1, TM.CustJobID as C2, FxStatus.LevelName as C3, TM.DateDictated as C4 " & _
            '                                "FROM tblTranscriptionMain AS TM INNER JOIN dbo.getStatus() as FxStatus on Fxstatus.LevelNo=TM.Status " & _
            '                                "where TM.TranscriptionID='" & val & "'"
            '                If Not oRec("IsExists") > 0 Then
            '                    oRec.Close()
            '                    SQLString = "INSERT INTO tblSetSamples(TranscriptionID,SuggestedBy,DateAvailable,Status) " & _
            '                                                              "Values('" & val & "','" & Session("UserID") & "','" & Now() & "',0)"

            '                    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                    oCommand.Transaction = thisTransaction
            '                    Dim RowsMain As Integer = oCommand.ExecuteNonQuery()
            '                    If RowsMain > 0 Then
            '                        thisTransaction.Commit()
            '                        writeStatus(sQuery, "Successfull")
            '                        CountUpdated = CountUpdated + 1
            '                    Else
            '                        thisTransaction.Rollback()
            '                        writeStatus(sQuery, "Failed")
            '                    End If
            '                Else
            '                    writeStatus(sQuery, "Already Exists")
            '                End If
            '            Catch ex As Exception
            '                thisTransaction.Rollback()
            '                writeStatus(sQuery, "Failed")
            '            Finally
            '                If oConn.State <> Data.ConnectionState.Closed Then
            '                    oConn.Close()
            '                End If
            '            End Try
            '            i = i + 1
            '        Next

            '        lblMessage.Text = "Total Records updated :" & CountUpdated & " Failed :" & i - CountUpdated
            '        iMain.Visible = False
            '        iMessage.Visible = True
            '    End If
            'ElseIf ddlOp.SelectedValue = "5" Then
            '    Dim txt As TextBox = btn.Parent.FindControl("txtPhy")
            '    Dim PhyID As String = String.Empty
            '    Dim CountUpdated, i As Integer
            '    Dim SQLQuery As String
            '    If txt.Text <> "" Then
            '        If strID.Count > 0 Then
            '            Dim ConString As String
            '            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            '            Dim tr As New TableRow
            '            tr.CssClass = "SMParentSelected"
            '            Dim tc1 As New TableCell
            '            tc1.Text = "Tracking#"
            '            Dim tc2 As New TableCell
            '            tc2.Text = "Client Job#"
            '            Dim tc3 As New TableCell
            '            tc3.Text = "Status"
            '            Dim tc4 As New TableCell
            '            tc4.Text = "Physician Name"
            '            Dim tc5 As New TableCell
            '            tc5.Text = "Transaction"
            '            tr.Cells.Add(tc1)
            '            tr.Cells.Add(tc2)
            '            tr.Cells.Add(tc3)
            '            tr.Cells.Add(tc4)
            '            tr.Cells.Add(tc5)
            '            'tblUp.Rows.Add(tr)
            '            For Each val As String In strID
            '                Dim oConn As New Data.SqlClient.SqlConnection
            '                Dim oCommand As New Data.SqlClient.SqlCommand
            '                Dim thisTransaction As Data.SqlClient.SqlTransaction
            '                oConn.ConnectionString = ConString
            '                oConn.Open()
            '                thisTransaction = oConn.BeginTransaction()
            '                SQLQuery = "SELECT FxStatus.Status AS C3, TM.CustJobID AS C2, TM.JobNumber AS C1, P.FirstName + ' ' + P.LastName AS C4 FROM tblTranscriptionMain AS TM INNER JOIN " & _
            '                        "dbo.getStatus() as FxStatus on Fxstatus.LevelNo=TM.Status INNER JOIN tblPhysicians AS P ON TM.DictatorID = P.PhysicianID WHERE TM.TranscriptionID = '" & val & "'"
            '                Try
            '                    SQLString = "SELECT P.PhySicianID FROM tblPhysicians AS P INNER JOIN tblAccounts AS A ON P.AccountID = A.AccountID where P.FirstName+' '+P.LastName+'('+convert(varchar(10),A.AccountNo,4)+')'='" & txt.Text & "' and A.Accountid=(select accountid from tbltranscriptionmain where transcriptionid='" & val & "')"
            '                    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                    oCommand.Transaction = thisTransaction
            '                    Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
            '                    oRec.Read()
            '                    PhyID = oRec("PhySicianID").ToString
            '                    oRec.Close()
            '                    oRec = Nothing
            '                    If Not String.IsNullOrEmpty(PhyID) Then
            '                        SQLString = "update tblTranscriptionMain set dictatorID ='" & PhyID & "' where TranscriptionID='" & val & "'"
            '                        oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            '                        oCommand.Transaction = thisTransaction

            '                        If oCommand.ExecuteNonQuery > 0 Then
            '                            thisTransaction.Commit()
            '                            writeStatus(SQLQuery, "Successfull")
            '                            CountUpdated = CountUpdated + 1
            '                        Else
            '                            thisTransaction.Rollback()
            '                            writeStatus(SQLQuery, "Failed")
            '                        End If
            '                    Else
            '                        'txt.Text = SQLString
            '                        thisTransaction.Rollback()
            '                        writeStatus(SQLQuery, "Failed")
            '                    End If
            '                    oConn.Close()
            '                Catch ex As Exception
            '                    thisTransaction.Rollback()
            '                    writeStatus(SQLQuery, "Failed")
            '                Finally
            '                    If oConn.State = Data.ConnectionState.Open Then oConn.Close()
            '                End Try
            '                i = i + 1
            '            Next
            '            lblMessage.Text = "Total Records updated :" & CountUpdated & " Failed :" & i - CountUpdated
            '            iMain.Visible = False
            '            iMessage.Visible = True
            '        End If
            '    End If
            'End If

            'Catch ex As Exception
            '    'Response.Write(ex.Message)
            'End Try
        End Sub
        Private Function writeStatus(ByVal Message As String)
            'Dim oConn As New Data.SqlClient.SqlConnection
            'Try
            '    Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

            '    Dim oCommand As New Data.SqlClient.SqlCommand
            '    oConn.ConnectionString = ConString
            '    oConn.Open()
            '    oCommand = New Data.SqlClient.SqlCommand(sQuery, oConn)
            '    Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
            '    oRec.Read()
            '    If oRec.HasRows Then

            Dim tr As TableRow = New TableRow
            tr.CssClass = "even"
            'Dim tc1 As TableCell = New TableCell
            'Dim tc2 As TableCell = New TableCell
            'Dim tc3 As TableCell = New TableCell
            'Dim tc4 As TableCell = New TableCell
            Dim tc5 As TableCell = New TableCell
            'tc1.Text = oRec("C1")
            'tc2.Text = oRec("C2")
            'tc3.Text = oRec("C3")
            'tc4.Text = oRec("C4")
            tc5.Text = Message
            'tr.Cells.Add(tc1)
            'tr.Cells.Add(tc2)
            'tr.Cells.Add(tc3)
            'tr.Cells.Add(tc4)
            tr.Cells.Add(tc5)
            tblUp.Rows.Add(tr)
            '    End If
            'oRec.Close()
            'oConn.Close()

            'Catch ex As Exception
            '    'txtRecPP.Text = ex.Message
            'Finally
            '    If oConn.State <> Data.ConnectionState.Closed Then
            '        oConn.Close()
            '    End If
            'End Try
        End Function
        Private Sub ToggleCheckState(ByVal checkState As Boolean)
            For Each DR As GridViewRow In dlist.Rows
                Dim cb As CheckBox = DR.FindControl("chkJob")

                If cb IsNot Nothing Then
                    cb.Checked = checkState
                End If
            Next

            'For Each Ditem As DataGridItem In dlist.Items
            '    Dim cb As CheckBox = Ditem.FindControl("chkJob")
            '    If cb IsNot Nothing Then
            '        cb.Checked = checkState
            '    End If
            'Next
        End Sub

        Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Response.Redirect("DictaResult_SP.aspx?SortItem=" & hdnSortItem.Value.ToString & "&SortOrder=" & hdnSortOrder.Value & "&intCurrIndex=" & intCurrIndex.Value & "&chk=True" & "&WC=" & hdnWhereClause.Value, True)
            'Dim chk As CheckBox = CType(sender, CheckBox)
            'If chk.Checked Then
            '    ToggleCheckState(True)

            'Else
            '    ToggleCheckState(False)
            'End If

        End Sub

        Protected Sub lstOptions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim LST As DropDownList = CType(sender, DropDownList)
                If LST.SelectedValue = "1" Then
                    Dim txt As TextBox = LST.Parent.FindControl("txtTAT")
                    txt.Visible = False
                    Dim lstStatus As DropDownList = LST.Parent.FindControl("lstStatus")
                    lstStatus.Visible = True
                    Dim txtUser As TextBox = LST.Parent.FindControl("txtUser")
                    txtUser.Visible = False
                    Dim lstLevel As DropDownList = LST.Parent.FindControl("lstLevel")
                    lstLevel.Visible = False
                ElseIf LST.SelectedValue = "3" Then
                    Dim lstStatus As DropDownList = LST.Parent.FindControl("lstStatus")
                    lstStatus.Visible = False
                    Dim txt As TextBox = LST.Parent.FindControl("txtTAT")
                    txt.Visible = True
                    Dim txtUser As TextBox = LST.Parent.FindControl("txtUser")
                    txtUser.Visible = False
                    Dim lstLevel As DropDownList = LST.Parent.FindControl("lstLevel")
                    lstLevel.Visible = False
                ElseIf LST.SelectedValue = "2" Then
                    Dim lstStatus As DropDownList = LST.Parent.FindControl("lstStatus")
                    lstStatus.Visible = False
                    Dim txt As TextBox = LST.Parent.FindControl("txtTAT")
                    txt.Visible = False
                    Dim txtUser As TextBox = LST.Parent.FindControl("txtUser")
                    txtUser.Visible = False
                    Dim clsPL As New ets.BL.ProductionLevels
                    Dim DSPL As DataSet = clsPL.getProductionLevelsByContractorType(Session("ContractorID"), Session("ParentID"), Session("IsContractor"), IIf(Session("IsContractor"), 0, 1))
                    clsPL = Nothing
                    Dim lstLevel As DropDownList = LST.Parent.FindControl("lstLevel")
                    lstLevel.DataSource = DSPL
                    lstLevel.DataTextField = "LevelName"
                    lstLevel.DataValueField = "LevelNo"
                    lstLevel.DataBind()
                    lstLevel.Visible = True
                ElseIf LST.SelectedValue = "4" Then
                    Dim lstStatus As DropDownList = LST.Parent.FindControl("lstStatus")
                    lstStatus.Visible = False
                    Dim txt As TextBox = LST.Parent.FindControl("txtTAT")
                    txt.Visible = False
                    Dim txtUser As TextBox = LST.Parent.FindControl("txtUser")
                    txtUser.Visible = False
                    Dim lstLevel As DropDownList = LST.Parent.FindControl("lstLevel")
                    lstLevel.Visible = False
                ElseIf LST.SelectedValue = "5" Then
                    Dim lstStatus As DropDownList = LST.Parent.FindControl("lstStatus")
                    lstStatus.Visible = False
                    Dim txt As TextBox = LST.Parent.FindControl("txtTAT")
                    txt.Visible = False
                    Dim txtUser As TextBox = LST.Parent.FindControl("txtUser")
                    txtUser.Visible = False
                    Dim txtPhy As TextBox = LST.Parent.FindControl("txtPhy")
                    txtPhy.Visible = True
                    Dim AutoCompletePhy As AjaxControlToolkit.AutoCompleteExtender
                    AutoCompletePhy = New AjaxControlToolkit.AutoCompleteExtender
                    With AutoCompletePhy
                        .MinimumPrefixLength = "1"
                        .CompletionSetCount = "10"
                        .TargetControlID = "txtPhy"
                        .ServicePath = "../users/autocomplete.asmx"
                        .EnableCaching = "true"
                        .ServiceMethod = "GetPhyNames"
                    End With
                    Update.ContentTemplateContainer.Controls.Add(AutoCompletePhy)
                    Dim lstLevel As DropDownList = LST.Parent.FindControl("lstLevel")
                    lstLevel.Visible = False
                End If
            Catch ex As Exception
                lblMessage.Text = ex.Message
                iMain.Visible = False
            End Try
        End Sub
        Protected Sub lstLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstLevel.SelectedIndexChanged
            
            txtUser.Visible = True
            Dim LST As DropDownList = CType(sender, DropDownList)
            If LST.SelectedValue <> 0 Then
                Dim AutoCompleteSearch As AjaxControlToolkit.AutoCompleteExtender
                AutoCompleteSearch = New AjaxControlToolkit.AutoCompleteExtender
                With AutoCompleteSearch
                    .MinimumPrefixLength = "1"
                    .CompletionSetCount = "10"
                    .TargetControlID = "txtUser"
                    .ServicePath = "../users/autocomplete.asmx"
                    .EnableCaching = "true"
                    .ServiceMethod = "GetUserName" & LST.SelectedValue.ToString
                End With
                Update.ContentTemplateContainer.Controls.Add(AutoCompleteSearch)

               
            End If
            
        End Sub

        'Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        '    iMain.Visible = True
        '    iMessage.Visible = False
        '    Response.Redirect("DictaResult_SP.aspx?SortItem=" & hdnSortItem.Value.ToString & "&SortOrder=" & hdnSortOrder.Value & "&intCurrIndex=" & intCurrIndex.Value & "&WC=" & hdnWhereClause.Value, True)
        '    'DBind()
        'End Sub
        'Protected Sub SortMe(ByVal sender As Object, ByVal e As System.EventArgs)
        '    Dim lnk As LinkButton = CType(sender, LinkButton)
        '    hdnSortItem.value = lnk.ID.ToString
        '    'dlist.CurrentPageIndex = 0
        '    If String.IsNullOrEmpty(hdnSortOrder.value) Then
        '        hdnSortOrder.value = " ASC"
        '    ElseIf hdnSortOrder.value.ToString = " ASC" Then
        '        hdnSortOrder.value = " DESC"
        '    Else
        '        hdnSortOrder.value = " ASC"
        '    End If
        '    DBind()
        'End Sub




        'Protected Sub dlist_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dlist.SortCommand
        '    hdnSortItem.value = e.SortExpression.ToString()
        '    If String.IsNullOrEmpty(hdnSortOrder.value) Then
        '        hdnSortOrder.value = " ASC"
        '    ElseIf hdnSortOrder.value.ToString = " ASC" Then
        '        hdnSortOrder.value = " DESC"
        '    Else
        '        hdnSortOrder.value = " ASC"
        '    End If
        '    Response.Redirect("DictaResult_SP.aspx?SortItem=" & hdnSortItem.Value.ToString & "&SortOrder=" & hdnSortOrder.Value & "&intCurrIndex=" & intCurrIndex.Value & "&WC=" & hdnWhereClause.Value, True)
        '    DBind()
        'End Sub

        'Protected Sub dlist_TemplateSelection(ByVal sender As Object, ByVal e As DBauer.Web.UI.WebControls.HierarGridTemplateSelectionEventArgs) Handles dlist.TemplateSelection
        '    e.TemplateFilename = "History.ascx"
        'End Sub
        Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        End Sub
        Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
            'Response.Redirect("ExportXLS.aspx?WC=" & hdnWhereClause.Value & "&IsUserCri=" & hdnIsUserCri.Value.ToString & "&OrderBy=" & hdnOrderBy.Value.ToString, True)
            Response.Clear()
            Dim filename = "Dictation Status Report " & Now & " .xls"
            Response.AddHeader("content-disposition", "attachment;filename=" & filename)
            Response.ContentType = "application/vnd.ms-excel"
            Response.Charset = ""
            Me.EnableViewState = False
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)
            dlist.RenderControl(hw)
            Response.Write(tw.ToString())
            Response.End()
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            'getRecordsPP()
            ClientScript.RegisterClientScriptInclude("SelectAllCheckboxes", "JScript.js")
            If String.IsNullOrEmpty(Request.QueryString("WC")) = False Or String.IsNullOrEmpty(Request.QueryString("intCurrIndex")) = False Or String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then

                iMain.Visible = True
                DBind()
            ElseIf IsPostBack Then
                iMain.Visible = True
            Else
                iMain.Visible = False
            End If

        End Sub


        'Protected Sub btnRecPP_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '    'Response.End()
        '    Dim ConString As String
        '    Dim SQLString As String
        '    ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        '    Dim oConn As New Data.SqlClient.SqlConnection
        '    Try
        '        oConn.ConnectionString = ConString
        '        oConn.Open()

        '        SQLString = "delete from tblPageSize where UserID='" & Session("UserID") & "'"
        '        Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
        '        oCommand.ExecuteNonQuery()

        '        SQLString = "insert into tblPageSize(UserID,Records) values('" & Session("UserID") & "'," & Request("txtRecPP") & ")"
        '        oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)

        '        If oCommand.ExecuteNonQuery() > 0 Then
        '            intPageSize.Value = Request("txtRecPP")
        '            Response.Redirect("DictaResult_SP.aspx?SortItem=" & hdnSortItem.Value.ToString & "&SortOrder=" & hdnSortOrder.Value & "&intCurrIndex=" & intCurrIndex.Value & "&PageSize=" & intPageSize.Value, True)
        '        End If

        '    Catch ex As Exception
        '        lblMessage.Text = ex.Message
        '        iMain.Visible = False
        '    Finally
        '        If oConn.State <> Data.ConnectionState.Closed Then
        '            oConn.Close()
        '        End If
        '    End Try
        'End Sub
        'Private Function getRecordsPP()
        '    Dim ConString As String
        '    Dim SQLString As String
        '    ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        '    Dim oConn As New Data.SqlClient.SqlConnection
        '    Try
        '        oConn.ConnectionString = ConString
        '        oConn.Open()

        '        SQLString = "select * from getPageSize('" & Session("UserID") & "')"
        '        Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
        '        Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
        '        oRec.Read()
        '        If oRec.HasRows Then
        '            intPageSize.Value = oRec("Records")
        '        Else
        '            intPageSize.Value = "10"
        '        End If
        '        ' Response.Write(intPageSize.Value)
        '        oRec.Close()
        '        oConn.Close()
        '    Catch ex As Exception
        '        lblMessage.Text = ex.Message
        '        iMain.Visible = False
        '    Finally
        '        If oConn.State <> Data.ConnectionState.Closed Then
        '            oConn.Close()
        '        End If
        '    End Try
        'End Function
        Protected Sub dlist_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dlist.RowDataBound
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim lnkHis As ImageButton
                lnkHis = DirectCast(e.Row.FindControl("btnHistory"), ImageButton)
                Dim hdntID As HiddenField
                hdntID = DirectCast(e.Row.FindControl("hdnTransID"), HiddenField)
                Dim hdnStatus As HiddenField
                hdnStatus = DirectCast(e.Row.FindControl("hdnStatus"), HiddenField)
                Dim var_hdnAccName As HiddenField
                var_hdnAccName = DirectCast(e.Row.FindControl("hdnAccName"), HiddenField)
                Dim var_hdnAccNo As HiddenField
                var_hdnAccNo = DirectCast(e.Row.FindControl("hdnAccNo"), HiddenField)
                Dim var_hdnContractorName As HiddenField
                var_hdnContractorName = DirectCast(e.Row.FindControl("hdnContractorName"), HiddenField)
                Dim var_hdnDictatorName As HiddenField
                var_hdnDictatorName = DirectCast(e.Row.FindControl("hdnDictatorName"), HiddenField)
                Dim var_hdnPinNo As HiddenField
                var_hdnPinNo = DirectCast(e.Row.FindControl("hdnPinNo"), HiddenField)
                Dim var_hdnSignedName As HiddenField
                var_hdnSignedName = DirectCast(e.Row.FindControl("hdnSignedName"), HiddenField)

                Dim var_hdnJobNo As HiddenField
                var_hdnJobNo = DirectCast(e.Row.FindControl("hdnJobNo"), HiddenField)

                Dim var_hdnCustJobNo As HiddenField
                var_hdnCustJobNo = DirectCast(e.Row.FindControl("hdnCustJobNo"), HiddenField)

                Dim var_hdnDtCreated As HiddenField
                var_hdnDtCreated = DirectCast(e.Row.FindControl("hdnDtCreated"), HiddenField)

                Dim var_hdnTAT As HiddenField
                var_hdnTAT = DirectCast(e.Row.FindControl("hdnTAT"), HiddenField)

                Dim var_hdnDtDictated As HiddenField
                var_hdnDtDictated = DirectCast(e.Row.FindControl("hdnDtDictated"), HiddenField)

                Dim var_hdnRemaining As HiddenField
                var_hdnRemaining = DirectCast(e.Row.FindControl("hdnRemaining"), HiddenField)

                If Not lnkHis Is Nothing And Not hdntID Is Nothing And Not hdnStatus Is Nothing Then
                    lnkHis.Attributes.Add("onclick", "javascript:return openPopup('" & hdntID.Value.ToString & "','" & hdnStatus.Value.ToString & "','" & var_hdnAccName.Value.ToString & "','" & var_hdnAccNo.Value.ToString & "','" & var_hdnContractorName.Value.ToString & "','" & var_hdnDictatorName.Value.ToString & "','" & var_hdnPinNo.Value.ToString & "','" & var_hdnSignedName.Value.ToString & "','" & var_hdnJobNo.Value.ToString & "','" & var_hdnCustJobNo.Value.ToString & "','" & var_hdnDtCreated.Value.ToString & "','" & var_hdnTAT.Value.ToString & "','" & var_hdnDtDictated.Value.ToString & "','" & var_hdnRemaining.Value.ToString & "')")
                End If
            End If
        End Sub

        
    End Class
End Namespace