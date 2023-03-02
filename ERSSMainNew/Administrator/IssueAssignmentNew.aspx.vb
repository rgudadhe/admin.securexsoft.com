Imports MainModule
Imports System.Data
Imports System.Web.UI.Page
Partial Class IssueAssignmentNew
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Session("UserID") = "11111111-1111-1111-1111-111111111111"
        If Session("DID") <> "" And Session("IID") <> "" Then
            AddDepartments(Session("DID"))
            AddIssueType(Session("IID"))
            AddSearchResultTable()
        End If
        lblResule.Text = ""
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varListItem As New ListItem
        varListItem.Text = "Please select"
        varListItem.Value = ""
        If Not Page.IsPostBack Then
            Try
                AddIssueType(String.Empty)
                AddDepartments(String.Empty)
            Catch ex As Exception
            End Try
        End If
        lblResule.Text = ""
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        AddSearchResultTable()
        ExistingAssignment()
        If Accordion.SelectedIndex >= 0 Then
            Accordion.SelectedIndex = -1
        End If
        If BtnSubmitInTicketManagement.Visible = False Then
            BtnSubmitInTicketManagement.Visible = True
        End If
    End Sub
    Protected Sub AddDepartments(ByVal DeptID As String)
        Dim varLstItemZeroIndex As New ListItem
        DropDownDept.Items.Clear()
        varLstItemZeroIndex.Text = "Please Select"
        varLstItemZeroIndex.Value = ""

        Dim clsDept As ETS.BL.Department
        Dim DS As Data.DataSet
        Dim DV As Data.DataView

        Try
            clsDept = New ETS.BL.Department
            'clsDept.ContractorID = Session("ContractorID").ToString
            DS = clsDept.GetDepartmentLstByWrkGroupID(Session("ContractorID"), Session("WorkGroupID"), String.Empty)
            DV = New Data.DataView(DS.Tables(0))
            DV.RowFilter = "(Deleted IS NULL OR Deleted=0)"
            DV.Sort = "Name"

            DropDownDept.DataSource = DV
            DropDownDept.DataTextField = "Name"
            DropDownDept.DataValueField = "DepartmentID"
            DropDownDept.DataBind()

            If Not String.IsNullOrEmpty(DeptID) Then
                Dim TempLstItem As ListItem = DropDownDept.Items.FindByValue(DeptID.ToString)
                If Not TempLstItem Is Nothing Then
                    TempLstItem.Selected = True
                End If
            End If

            DropDownDept.Items.Insert(0, varLstItemZeroIndex)

        Catch ex As Exception
        Finally
            clsDept = Nothing
            DS = Nothing
            DV = Nothing
        End Try
        Session("DID") = ""
    End Sub
    Protected Sub AddIssueType(ByVal IssueID As String)
        Dim varLstItemZeroIndex As New ListItem
        DropDownIssues.Items.Clear()
        varLstItemZeroIndex.Text = "Please Select"
        varLstItemZeroIndex.Value = ""

        Dim clsERSSIT As ETS.BL.ERSSIssueType
        Dim DS As Data.DataSet
        Dim DV As Data.DataView

        Try
            clsERSSIT = New ETS.BL.ERSSIssueType()
            clsERSSIT.ContractorID = Session("ContractorID").ToString
            DS = clsERSSIT.getIssueTypeList
            DV = New Data.DataView(DS.Tables(0))
            DV.RowFilter = "(IsDeleted IS NULL OR IsDeleted=0)"
            DV.Sort = "IssueName"

            DropDownIssues.DataSource = DV
            DropDownIssues.DataTextField = "IssueName"
            DropDownIssues.DataValueField = "IssueID"
            DropDownIssues.DataBind()

            If Not String.IsNullOrEmpty(IssueID) Then
                Dim TempLstItem As ListItem = DropDownIssues.Items.FindByValue(IssueID.ToString)
                If Not TempLstItem Is Nothing Then
                    TempLstItem.Selected = True
                End If
            End If

            DropDownIssues.Items.Insert(0, varLstItemZeroIndex)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsERSSIT = Nothing
            DS = Nothing
            DV = Nothing
        End Try
        Session("IID") = ""
    End Sub
    Protected Sub AddSearchResultTable()
        Dim clsUsrs As ETS.BL.Users
        Dim DS As Data.DataSet
        Dim objRecGetEmpData As Data.DataTableReader
        Dim objConn As New Data.SqlClient.SqlConnection
        Try
            Dim varStrQuery As String
            Dim varStrDeptID As String
            Dim varStrIssueID As String

            Dim varStrLookforQuery As String
            Dim varStrDeptQuery As String
            Dim varIntI As Integer
            Dim varIntJ As Integer
            Dim varIntRowCountI As Integer

            Dim varArrUserID As New ArrayList
            Dim varArrUserName As New ArrayList
            Dim varArrEmpName As New ArrayList
            Dim varArrDeptName As New ArrayList
            'varIntJ = 0
            tblSearchResultInTicketManagement.Visible = True
            varIntRowCountI = tblSearchResultInTicketManagement.Rows.Count - 1
            If varIntRowCountI > 1 And Session("DID") <> "" And Session("IID") <> "" Then
                For varIntJ = 1 To varIntRowCountI
                    tblSearchResultInTicketManagement.Rows.RemoveAt(varIntRowCountI)
                    varIntRowCountI = varIntRowCountI - 1
                Next
            ElseIf varIntRowCountI = 1 Then
                tblSearchResultInTicketManagement.Rows.RemoveAt(1)
            End If

            varStrDeptID = DropDownDept.Items(DropDownDept.SelectedIndex).Value.ToString
            varStrIssueID = DropDownIssues.Items(DropDownIssues.SelectedIndex).Value.ToString

            objConn = objMainModule.OpenConnection(objConn)

            clsUsrs = New ETS.BL.Users
            DS = clsUsrs.getUsersListByDeptName(Session("ContractorID").ToString, varStrDeptID.ToString)

            objRecGetEmpData = DS.Tables(0).CreateDataReader
            If objRecGetEmpData.HasRows Then
                While objRecGetEmpData.Read
                    Dim varStrTempName As String
                    If Not objRecGetEmpData.IsDBNull(objRecGetEmpData.GetOrdinal("UserID")) Then
                        varArrUserID.Add(objRecGetEmpData.GetGuid(objRecGetEmpData.GetOrdinal("UserID")).ToString)
                    End If
                    If Not objRecGetEmpData.IsDBNull(objRecGetEmpData.GetOrdinal("UserName")) Then
                        varArrUserName.Add(objRecGetEmpData.GetString(objRecGetEmpData.GetOrdinal("UserName")))
                    Else
                        varArrUserName.Add("")
                    End If
                    If Not objRecGetEmpData.IsDBNull(objRecGetEmpData.GetOrdinal("FirstName")) Then
                        varStrTempName = objRecGetEmpData.GetString(objRecGetEmpData.GetOrdinal("FirstName"))
                    End If
                    If Not objRecGetEmpData.IsDBNull(objRecGetEmpData.GetOrdinal("LastName")) Then
                        varStrTempName = varStrTempName & " " & objRecGetEmpData.GetString(objRecGetEmpData.GetOrdinal("LastName"))
                    End If
                    If varStrTempName <> "" Then
                        varArrEmpName.Add(varStrTempName)
                    Else
                        varArrEmpName.Add("")
                    End If
                    If Not objRecGetEmpData.IsDBNull(objRecGetEmpData.GetOrdinal("Name")) Then
                        varArrDeptName.Add(objRecGetEmpData.GetString(objRecGetEmpData.GetOrdinal("Name")))
                    Else
                        varArrDeptName.Add("")
                    End If
                End While
            End If
            objRecGetEmpData.Close()

            Dim varIntRowCount As Integer
            varIntRowCount = 1

            Dim varStrAccessStatus As String
            'varStrAccessStatus = RadioButtonAccess.Items(RadioButtonAccess.SelectedIndex).Value.ToString
            varStrAccessStatus = "Read"

            Dim varCounter As Integer
            For varCounter = 0 To varArrUserID.Count - 1
                Dim varTblRow As New TableRow
                Dim varTblCellUserName As New TableCell
                Dim varTblCellName As New TableCell
                Dim varTblCellDepartment As New TableCell
                Dim varTblCellAccess As New TableCell
                Dim varTblCellChkBox As New TableCell
                'Dim varCtrlChkBox As New CheckBox


                Dim varTblCellNewAccess As New TableCell
                Dim varCtrlChkBoxRead As New CheckBox
                Dim varCtrlChkBoxUpdate As New CheckBox
                Dim varCtrlChkBoxRemove As New CheckBox
                Dim varCtrllblRead As New Label
                Dim varCtrllblUpdate As New Label
                Dim varCtrllblRemove As New Label

                varCtrllblRead.ID = "lblRead" & varArrUserID(varCounter)
                varCtrllblRead.EnableViewState = True
                varCtrllblRead.Text = "Read"

                varCtrllblUpdate.ID = "lblUpdate" & varArrUserID(varCounter)
                varCtrllblUpdate.EnableViewState = True
                varCtrllblUpdate.Text = "Update"

                varCtrllblRemove.ID = "lblRemove" & varArrUserID(varCounter)
                varCtrllblRemove.EnableViewState = True
                varCtrllblRemove.Text = "Remove"
                Dim varStrTempName As String
                Dim varStrAccess As String = String.Empty
                varStrAccess = ""

                'varCtrlChkBox.AutoPostBack = False
                'varCtrlChkBox.ID = "Chk_" & varArrUserID(varCounter)
                'varCtrlChkBox.EnableViewState = True
                Dim clsERSSTA As ETS.BL.ERSSTicketsAccess

                Try
                    clsERSSTA = New ETS.BL.ERSSTicketsAccess
                    clsERSSTA.UserID = varArrUserID(varCounter).ToString
                    clsERSSTA.IssueID = varStrIssueID
                    clsERSSTA.getTicketAccessDetails()
                    If String.IsNullOrEmpty(clsERSSTA.Access) Then
                        varStrAccess = String.Empty
                    Else
                        varStrAccess = clsERSSTA.Access
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsERSSTA = Nothing
                End Try

                'varTblCellChkBox.Controls.Add(varCtrlChkBox)
                'varTblCellChkBox.Width = 20

                varCtrlChkBoxRead.AutoPostBack = False
                varCtrlChkBoxRead.ID = "Chk_Read#" & varArrUserID(varCounter)
                varCtrlChkBoxRead.EnableViewState = True
                varCtrlChkBoxRead.Attributes.Add("onclick", "javascript:ReadChk('" & varArrUserID(varCounter) & "');")

                varCtrlChkBoxUpdate.AutoPostBack = False
                varCtrlChkBoxUpdate.ID = "Chk_Update#" & varArrUserID(varCounter)
                varCtrlChkBoxUpdate.EnableViewState = True
                varCtrlChkBoxUpdate.Attributes.Add("onclick", "javascript:UpdateChk('" & varArrUserID(varCounter) & "');")

                varCtrlChkBoxRemove.AutoPostBack = False
                varCtrlChkBoxRemove.ID = "Chk_Remove#" & varArrUserID(varCounter)
                varCtrlChkBoxRemove.EnableViewState = True
                varCtrlChkBoxRemove.Attributes.Add("onclick", "javascript:RemoveChk('" & varArrUserID(varCounter) & "');")

                If varStrAccess <> "" Then
                    If Trim(UCase(varStrAccess)) = Trim(UCase("Update")) Then
                        varCtrlChkBoxRead.Checked = True
                        varCtrlChkBoxUpdate.Checked = True
                    ElseIf Trim(UCase(varStrAccess)) = Trim(UCase("Read")) And Trim(UCase(varStrAccess)) = Trim(UCase("Read")) Then
                        varCtrlChkBoxRead.Checked = True
                    End If
                End If

                varTblCellNewAccess.Controls.Add(varCtrlChkBoxRead)
                varTblCellNewAccess.Controls.Add(varCtrllblRead)
                varTblCellNewAccess.Controls.Add(varCtrlChkBoxUpdate)
                varTblCellNewAccess.Controls.Add(varCtrllblUpdate)
                If varStrAccess <> "" Then
                    varTblCellNewAccess.Controls.Add(varCtrlChkBoxRemove)
                    varTblCellNewAccess.Controls.Add(varCtrllblRemove)
                End If


                varTblCellUserName.Text = varArrUserName(varCounter)
                varTblCellName.Text = varArrEmpName(varCounter)
                varTblCellDepartment.Text = varArrDeptName(varCounter)

                varTblCellAccess.Text = varStrAccess
                If String.IsNullOrEmpty(varTblCellAccess.Text.ToString) Then
                    varTblCellAccess.Text = "&nbsp;"
                End If
                'varTblRow.Cells.Add(varTblCellChkBox)
                varTblRow.Cells.Add(varTblCellUserName)
                varTblRow.Cells.Add(varTblCellName)
                varTblRow.Cells.Add(varTblCellDepartment)
                varTblRow.Cells.Add(varTblCellAccess)

                varTblRow.Cells.Add(varTblCellNewAccess)

                If varIntI Mod 2 = 0 Then
                    varTblRow.BackColor = Drawing.Color.FloralWhite
                Else
                    varTblRow.BackColor = Drawing.Color.OldLace
                End If
                tblSearchResultInTicketManagement.Rows.AddAt(varIntRowCount, varTblRow)
                varStrTempName = ""
                varIntI = varIntI + 1
                varIntRowCount = varIntRowCount + 1
            Next
            Session("DID") = varStrDeptID
            Session("IID") = varStrIssueID
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If objConn.State <> ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
            clsUsrs = Nothing
            DS = Nothing
            objRecGetEmpData = Nothing
        End Try
    End Sub
    Protected Sub BtnSubmitInTicketManagement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmitInTicketManagement.Click
        Dim objConn As New Data.SqlClient.SqlConnection
        Dim clsERSSTA As ETS.BL.ERSSTicketsAccess

        Try
            Dim varStrRadioButton As String
            Dim varCtrl As Control
            Dim varIntI As Integer
            Dim varStrTemp As String
            Dim varStrIssueID As String
            Dim varCounter As Integer
            varCounter = 0
            varStrIssueID = DropDownIssues.Items(DropDownIssues.SelectedIndex).Value.ToString
            'varStrRadioButton = RadioButtonAccess.Items(RadioButtonAccess.SelectedIndex).Value.ToString
            'varStrRadioButton = "Read"
            objConn = objMainModule.OpenConnection(objConn)
            Dim varStrQuery As String = String.Empty
            If tblSearchResultInTicketManagement.Rows.Count > 1 Then
                Dim DTTemp As New Data.DataTable
                DTTemp.Columns.Add(New DataColumn("UserID"))
                DTTemp.Columns.Add(New DataColumn("Access"))
                DTTemp.Columns.Add(New DataColumn("Existing"))
                clsERSSTA = New ETS.BL.ERSSTicketsAccess
                For varIntI = 1 To tblSearchResultInTicketManagement.Rows.Count - 1
                    Dim varStrOprQuery As String = String.Empty
                    Dim varTblRow As New TableRow
                    Dim varTblCell As New TableCell

                    varTblRow = tblSearchResultInTicketManagement.Rows(varIntI)

                    varTblCell = varTblRow.Cells(4)
                    Dim varStrUserID As String = String.Empty
                    Dim varBolChkRead As Boolean = False
                    Dim varBolChkUpdate As Boolean = False
                    Dim varBolChkRemove As Boolean = False
                    Dim varCtrlCheckBoxRead As New CheckBox
                    Dim varCtrlCheckBoxUpdate As New CheckBox
                    Dim varCtrlCheckBoxRemove As New CheckBox
                    Dim varStrAccess As String = String.Empty


                    For Each varCtrl In varTblCell.Controls
                        If varCtrl.ID.ToString.IndexOf("Chk_Read") > -1 Then
                            varCtrlCheckBoxRead = DirectCast(varTblCell.FindControl(varCtrl.ID.ToString), CheckBox)
                            varBolChkRead = varCtrlCheckBoxRead.Checked
                        End If

                        If varCtrl.ID.ToString.IndexOf("Chk_Update") > -1 Then
                            varCtrlCheckBoxUpdate = DirectCast(varTblCell.FindControl(varCtrl.ID.ToString), CheckBox)
                            varBolChkUpdate = varCtrlCheckBoxUpdate.Checked
                        End If

                        If varCtrl.ID.ToString.IndexOf("Chk_Remove") > -1 Then
                            varCtrlCheckBoxRemove = DirectCast(varTblCell.FindControl(varCtrl.ID.ToString), CheckBox)
                            varBolChkRemove = varCtrlCheckBoxRemove.Checked
                        End If

                        If varStrUserID = "" Then
                            Dim varStrSplit() As String
                            varStrSplit = varCtrl.ID.ToString.Split("#")
                            If varStrSplit(1) <> "" Then
                                varStrUserID = varStrSplit(1)
                            End If
                        End If

                        'varStrUserID = Mid(varCtrl.ID.ToString, 5)

                        'If varBolChk Then

                        '    Dim objGetAccess As New Data.SqlClient.SqlCommand("SELECT Access FROM dbo.tblERSSTicketsAccess WHERE UserID='" & varStrUserID.ToString & "' AND IssueID='" & varStrIssueID & "'", objMainModule.oConn)
                        '    Dim objRecGetAccess As Data.SqlClient.SqlDataReader = objGetAccess.ExecuteReader()

                        '    If objRecGetAccess.HasRows Then
                        '        While objRecGetAccess.Read
                        '            varStrOprQuery = "UPDATE dbo.tblERSSTicketsAccess SET Access='" & varStrRadioButton & "',ModifiedBy='" & Session("UserID") & "',ModifiedOn='" & Now() & "' WHERE UserID='" & varStrUserID & "' AND IssueID='" & varStrIssueID & "'"
                        '        End While
                        '    Else
                        '        varStrOprQuery = "INSERT INTO dbo.tblERSSTicketsAccess (UserID,IssueID,Access,ModifiedBy,ModifiedOn) VALUES('" & varStrUserID & "','" & varStrIssueID & "','" & varStrRadioButton & "','" & Session("UserID") & "','" & Now() & "')"
                        '    End If
                        '    objRecGetAccess.Close()
                        '    objRecGetAccess = Nothing
                        '    objGetAccess = Nothing
                        '    Response.Write(varStrOprQuery)
                        '    Dim StrCmd As New Data.SqlClient.SqlCommand
                        '    StrCmd.Connection = objMainModule.oConn
                        '    StrCmd.CommandType = Data.CommandType.Text
                        '    StrCmd.CommandText = varStrOprQuery
                        '    StrCmd.ExecuteNonQuery()
                        '    StrCmd = Nothing
                        '    varCounter = varCounter + 1
                        '    varStrQuery = varStrQuery & varStrOprQuery
                        'End If
                    Next

                    If varBolChkRead Then
                        If varBolChkUpdate Then
                            varStrAccess = "Update"
                        Else
                            varStrAccess = "Read"
                        End If
                    ElseIf varBolChkRemove Then
                        varStrAccess = "Remove"
                    End If
                    Dim varBolExisting As Boolean
                    Dim clsTempERSSTA As ETS.BL.ERSSTicketsAccess
                    Try
                        clsTempERSSTA = New ETS.BL.ERSSTicketsAccess
                        clsTempERSSTA.UserID = varStrUserID
                        clsTempERSSTA.IssueID = varStrIssueID.ToString
                        clsTempERSSTA.getTicketAccessDetails()
                        If String.IsNullOrEmpty(clsTempERSSTA.Access) Then
                            varBolExisting = False
                        Else
                            varBolExisting = True
                        End If

                        'lblResule.Text = lblResule.Text & varStrUserID & " " & varStrIssueID & " " & varBolExisting & " Acc : " & clsTempERSSTA.Access & "<BR>"

                    Catch ex As Exception
                        lblResule.Text = ex.Message
                    Finally
                        clsTempERSSTA = Nothing
                    End Try

                    Dim DR As Data.DataRow = DTTemp.NewRow

                    DR("UserID") = varStrUserID
                    DR("Access") = varStrAccess
                    DR("Existing") = varBolExisting

                    DTTemp.Rows.Add(DR)



                    'varStrQuery = varStrQuery & varStrAccess
                    'If varStrAccess <> String.Empty Then
                    '    If Trim(UCase(varStrAccess)) = Trim(UCase("Remove")) Then
                    '        varStrOprQuery = "DELETE ETS.dbo.tblERSSTicketsAccess WHERE UserID='" & varStrUserID & "' AND IssueID='" & varStrIssueID & "'"
                    '    Else
                    '        Dim objGetAccess As New Data.SqlClient.SqlCommand("SELECT Access FROM ETS.dbo.tblERSSTicketsAccess WHERE UserID='" & varStrUserID.ToString & "' AND IssueID='" & varStrIssueID & "'", objConn)
                    '        Dim objRecGetAccess As Data.SqlClient.SqlDataReader = objGetAccess.ExecuteReader()

                    '        If objRecGetAccess.HasRows Then
                    '            While objRecGetAccess.Read
                    '                varStrOprQuery = "UPDATE dbo.tblERSSTicketsAccess SET Access='" & varStrAccess & "',ModifiedBy='" & Session("UserID") & "',ModifiedOn='" & Now() & "' WHERE UserID='" & varStrUserID & "' AND IssueID='" & varStrIssueID & "'"
                    '            End While
                    '        Else
                    '            varStrOprQuery = "INSERT INTO dbo.tblERSSTicketsAccess (UserID,IssueID,Access,ModifiedBy,ModifiedOn) VALUES('" & varStrUserID & "','" & varStrIssueID & "','" & varStrAccess & "','" & Session("UserID") & "','" & Now() & "')"
                    '        End If
                    '        objRecGetAccess.Close()
                    '        objRecGetAccess = Nothing
                    '        objGetAccess = Nothing
                    '    End If

                    '    Dim StrCmd As New Data.SqlClient.SqlCommand
                    '    StrCmd.Connection = objConn
                    '    StrCmd.CommandType = Data.CommandType.Text
                    '    StrCmd.CommandText = varStrOprQuery
                    '    StrCmd.ExecuteNonQuery()
                    '    StrCmd = Nothing
                    '    varCounter = varCounter + 1
                    '    varStrQuery = varStrQuery & "<BR>" & varStrOprQuery
                    'End If
                Next


                With clsERSSTA
                    .IssueID = varStrIssueID
                    .ModifiedBy = Session("UserID")
                    .ModifiedOn = Now()
                End With

                Dim RetVal As Boolean = clsERSSTA.btn_Submit_Assignment(DTTemp)
                If RetVal Then
                    lblResule.Text = "Update ERSS Tickets Access Sucessfully !!!"
                Else
                    lblResule.Text = "ERSS Tickets Access updation failed !!!"
                End If
            Else
                lblResule.Text = "Please select records to update the access."
            End If
        Catch ex As Exception
            lblResule.Text = ex.Message.ToString
        Finally
            If objConn.State <> ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try

        ExistingAssignment()
        tblExistingAssignment.Visible = True
        tblSearchResultInTicketManagement.Visible = False
        Accordion.SelectedIndex = 0
        BtnSubmitInTicketManagement.Visible = False
    End Sub
    'Protected Sub BtnRemoveInTicketManagement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRemoveInTicketManagement.Click
    '    Dim varStrRadioButton As String
    '    Dim varCtrl As Control
    '    Dim varIntI As Integer
    '    Dim varStrIssueID As String
    '    Dim varCounter As Integer
    '    varCounter = 0
    '    varStrIssueID = DropDownIssues.Items(DropDownIssues.SelectedIndex).Value.ToString
    '    'varStrRadioButton = RadioButtonAccess.Items(RadioButtonAccess.SelectedIndex).Value.ToString
    '    varStrRadioButton = "Read"
    '    For varIntI = 2 To tblSearchResultInTicketManagement.Rows.Count - 1
    '        Dim varTblRow As New TableRow
    '        Dim varTblCell As New TableCell
    '        varTblRow = tblSearchResultInTicketManagement.Rows(varIntI)
    '        varTblCell = varTblRow.Cells(0)
    '        For Each varCtrl In varTblCell.Controls

    '            Dim varStrUserID As String
    '            Dim varCtrlCheckBox As New CheckBox
    '            Dim varBolChk As String
    '            varBolChk = ""
    '            varCtrlCheckBox = DirectCast(varTblCell.FindControl(varCtrl.ID.ToString), CheckBox)
    '            varBolChk = varCtrlCheckBox.Checked
    '            varStrUserID = Mid(varCtrl.ID.ToString, 5)

    '            If varBolChk Then
    '                Dim StrDelCmd As New Data.SqlClient.SqlCommand
    '                StrDelCmd.Connection = objMainModule.oConn
    '                StrDelCmd.CommandType = Data.CommandType.Text
    '                StrDelCmd.CommandText = "DELETE dbo.tblERSSTicketsAccess WHERE UserID='" & varStrUserID & "' AND IssueID='" & varStrIssueID & "'"
    '                StrDelCmd.ExecuteNonQuery()
    '                StrDelCmd = Nothing
    '                varCounter = varCounter + 1
    '            End If
    '        Next
    '    Next
    '    If varCounter > 0 Then
    '        lblResule.Text = "Remove ERSS Tickets Access Sucessfully !!!"
    '    End If
    'End Sub
    Protected Sub DropDownIssues_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownIssues.SelectedIndexChanged
        ExistingAssignment()
    End Sub
    Public Sub ExistingAssignment()
        Dim clsERSSTA As ETS.BL.ERSSTicketsAccess
        Dim DS As New Data.DataSet
        Dim objRec As Data.DataTableReader

        Try
            Dim varStrIssueID As String = String.Empty
            Dim varArrDeptID As New ArrayList
            Dim varArrDeptName As New ArrayList
            Dim varArrCounter As New ArrayList
            varStrIssueID = DropDownIssues.Items(DropDownIssues.SelectedIndex).Value.ToString
            If varStrIssueID <> "" Then
                For i As Integer = 2 To tblExistingAssignment.Rows.Count - 1
                    tblExistingAssignment.Rows.RemoveAt(i)
                Next
                'tblExistingAssignment.Visible = True

                clsERSSTA = New ETS.BL.ERSSTicketsAccess
                DS = clsERSSTA.GetTicketAccessDeptListByIssueId(varStrIssueID.ToString)

                objRec = DS.Tables(0).CreateDataReader
                If objRec.HasRows Then
                    While objRec.Read
                        If Not objRec.IsDBNull(objRec.GetOrdinal("DepartmentID")) Then
                            varArrDeptID.Add(objRec.GetGuid(objRec.GetOrdinal("DepartmentID")).ToString())
                        End If
                        If Not objRec.IsDBNull(objRec.GetOrdinal("Name")) Then
                            varArrDeptName.Add(objRec.GetString(objRec.GetOrdinal("Name"))).ToString()
                        End If
                        If Not objRec.IsDBNull(objRec.GetOrdinal("RecordCount")) Then
                            varArrCounter.Add(objRec("RecordCount"))
                        End If
                    End While
                End If
                objRec.Close()

                Dim varIntI As Integer = 0
                For varIntI = 0 To varArrDeptID.Count - 1
                    Dim varBolFirst As Boolean = False
                    Dim DS1 As New Data.DataSet
                    Dim objRecAccess As Data.DataTableReader

                    Try
                        DS1 = clsERSSTA.GetTicketAccessUsrsByDept(varStrIssueID.ToString, varArrDeptID(varIntI))
                        objRecAccess = DS1.Tables(0).CreateDataReader

                        If objRecAccess.HasRows Then
                            While objRecAccess.Read
                                Dim varTblRow As New TableRow
                                If Not varBolFirst Then
                                    Dim varTblCellDept As New TableCell
                                    varTblCellDept.Text = varArrDeptName(varIntI)
                                    varTblCellDept.RowSpan = varArrCounter(varIntI)
                                    varTblRow.Cells.Add(varTblCellDept)
                                    varBolFirst = True
                                End If

                                Dim varTblCellEmp As New TableCell
                                Dim varTblCellAccess As New TableCell
                                Dim varStrName As String = String.Empty
                                If Not objRecAccess.IsDBNull(objRecAccess.GetOrdinal("FirstName")) Then
                                    varStrName = objRecAccess.GetString(objRecAccess.GetOrdinal("FirstName"))
                                End If
                                If Not objRecAccess.IsDBNull(objRecAccess.GetOrdinal("LastName")) Then
                                    varStrName = varStrName & " " & objRecAccess.GetString(objRecAccess.GetOrdinal("LastName"))
                                End If
                                varTblCellEmp.Text = varStrName
                                If Not objRecAccess.IsDBNull(objRecAccess.GetOrdinal("Access")) Then
                                    varTblCellAccess.Text = objRecAccess.GetString(objRecAccess.GetOrdinal("Access"))
                                Else
                                    varTblCellAccess.Text = "&nbsp"
                                End If
                                varTblRow.Cells.Add(varTblCellEmp)
                                varTblRow.Cells.Add(varTblCellAccess)
                                tblExistingAssignment.Rows.Add(varTblRow)
                            End While
                        End If
                        objRecAccess.Close()
                    Catch ex As Exception
                        Response.Write(ex.Message)
                    Finally
                        objRecAccess = Nothing
                        DS1 = Nothing
                    End Try
                Next
            Else
                tblExistingAssignment.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsERSSTA = Nothing
            DS = Nothing
            objRec = Nothing
        End Try
    End Sub
End Class
