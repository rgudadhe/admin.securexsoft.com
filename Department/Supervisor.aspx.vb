
Imports System.Data
Partial Class Department_Supervisor
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim DSDep As New DataSet
            Dim clsDep As New ETS.BL.Department
            With clsDep
                '.ContractorID = Session("ContractorID").ToString
                DSDep = .GetDepartmentLstByWrkGroupID(Session("ContractorID").ToString, Session("WorkGroupID").ToString, String.Empty)
            End With
            clsDep = Nothing
            DropDownDept.DataSource = DSDep
            DropDownDept.DataValueField = "DepartmentID"
            DropDownDept.DataTextField = "Name"
            DropDownDept.DataBind()
            Dim LI As New ListItem("Please Select", "")
            DropDownDept.Items.Insert(0, LI)
            LI.Selected = True
            FillTable()
        End If


    End Sub
    
    Protected Sub FillTable()
        Dim DSUsers As New DataSet
        Dim clsUser As New ETS.BL.Users
        With clsUser
            '.ContractorID = Session("ContractorID").ToString
            '.CanApprove = True
            '._WhereString.Append(" AND (IsDeleted IS NULL OR IsDeleted=0)")
            DSUsers = .getUsersList(Session("ContractorID").ToString, Session("WorkGroupID").ToString, " AND CanApprove = 1 ")
        End With
        clsUser = Nothing
        
        If DSUsers.Tables.Count > 0 Then
            Dim DSDep As New DataSet
            Dim clsDep As New ETS.BL.Department
            With clsDep
                .ContractorID = Session("ContractorID").ToString
                DSDep = .getDepartmentList
            End With
            clsDep = Nothing
            For Each objRec As DataRow In DSUsers.Tables(0).Rows
                If String.IsNullOrEmpty(hdnSupList.Value) Then
                    hdnSupList.Value = "'" & objRec("UserID").ToString & "'"
                Else
                    hdnSupList.Value = hdnSupList.Value & ",'" & objRec("UserID").ToString & "'"
                End If
                Dim varTblRow As New TableRow
                Dim vartblCellID As New TableCell
                Dim vartblCellName As New TableCell
                Dim varTblCellDept As New TableCell
                Dim varTblCellRemove As New TableCell
                Dim varStrTempName As String = String.Empty
                If Not IsDBNull(objRec("FirstName")) Then
                    varStrTempName = objRec("FirstName")
                End If
                If Not IsDBNull(objRec("LastName")) Then
                    varStrTempName = varStrTempName & " " & objRec("LastName")
                End If
                vartblCellID.Text = objRec("UserName")
                vartblCellName.Text = varStrTempName

                Dim DR() As DataRow = DSDep.Tables(0).Select("DepartmentID='" & objRec("DepartmentID").ToString & "'")
                If UBound(DR) >= 0 Then
                    varTblCellDept.Text = DR(0).Item("Name")
                End If
                Dim btnRemove As New Button
                btnRemove.Font.Name = "Trebuchet MS"
                btnRemove.Font.Size = 10
                btnRemove.Text = "Remove"
                btnRemove.CausesValidation = False
                btnRemove.CssClass = "button"

                btnRemove.CommandName = "ID"
                btnRemove.CommandArgument = objRec("UserId").ToString()
                AddHandler btnRemove.Command, AddressOf myFunction

                varTblCellRemove.Controls.Add(btnRemove)
                varTblRow.Cells.Add(vartblCellID)
                varTblRow.Cells.Add(vartblCellName)
                varTblRow.Cells.Add(varTblCellDept)
                varTblRow.Cells.Add(varTblCellRemove)
                tblList.Rows.Add(varTblRow)
            Next
        End If
        DSUsers.Dispose()
        
    End Sub
    Private Sub myFunction(ByVal sender As Object, ByVal e As CommandEventArgs)
        Dim RecordId As String = e.CommandArgument.ToString()
        If RecordId <> "" Then
            Dim clsDS As ETS.BL.DeptSuperVisor
            Try
                clsDS = New ETS.BL.DeptSuperVisor
                If clsDS.ISSuperVisorsAssign(RecordId.ToString) = True Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""User set as supervisor you can not remove from this list "");</script>")
                    Exit Sub
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsDS = Nothing
            End Try
            Dim clsUser As New ETS.BL.Users
            With clsUser
                .UserID = RecordId
                ' .CanApprove = False
                If .UpdateUserDetails > 0 Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""User removed from Supervisor list"");window.location.href='Supervisor.aspx';</script>")
                End If
            End With
            clsUser = Nothing
        End If
    End Sub
    Protected Sub DropDownDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownDept.SelectedIndexChanged
        Dim varStrDeptID As String
        varStrDeptID = DropDownDept.Items(DropDownDept.SelectedIndex).Value.ToString
        DropDownUsers.Items.Clear()
        If varStrDeptID <> "" Then
            Dim DSUsers As New DataSet
            Dim clsUser As New ETS.BL.Users
            With clsUser
                '.ContractorID = Session("ContractorID").ToString
                '.DepartmentID = varStrDeptID
                '._WhereString.Append(" AND (IsDeleted IS NULL OR IsDeleted=0)  AND UserID NOT IN (" & hdnSupList.Value & ")")
                
                Dim varTempWhere As String = String.Empty
                If Not String.IsNullOrEmpty(hdnSupList.Value.ToString) Then
                    varTempWhere = " AND U.DepartmentID='" & varStrDeptID & "' AND  UserID NOT IN (" & hdnSupList.Value & ") "
                Else
                    varTempWhere = " AND U.DepartmentID='" & varStrDeptID & "' "
                End If

                DSUsers = .getUsersList(Session("ContractorID").ToString, Session("WorkGroupID").ToString, varTempWhere.ToString)
            End With
            clsUser = Nothing
            If DSUsers.Tables.Count > 0 Then
                If DSUsers.Tables(0).Rows.Count > 0 Then
                    DSUsers.Tables(0).Columns.Add(New DataColumn("BindName", GetType(System.String), "ISNULL(FirstName,'')+' '+ISNULL(LastName,'')"))
                    DropDownUsers.DataSource = DSUsers
                    DropDownUsers.DataTextField = "BindName"
                    DropDownUsers.DataValueField = "UserID"
                    DropDownUsers.DataBind()
                End If
            End If
            DSUsers.Dispose()
            Dim varLstEmpty As New ListItem
            varLstEmpty.Text = "Please Select"
            varLstEmpty.Value = ""
            DropDownUsers.Items.Insert(0, varLstEmpty)
        End If
        
    End Sub
    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Dim varStrDeptID As String = String.Empty
        Dim varStrUserId As String = String.Empty
        varStrDeptID = DropDownDept.Items(DropDownDept.SelectedIndex).Value.ToString
        varStrUserId = DropDownUsers.Items(DropDownUsers.SelectedIndex).Value.ToString
        If varStrDeptID <> "" And varStrUserId <> "" Then
            Dim clsUser As New ETS.BL.Users
            With clsUser
                .UserID = varStrUserId
                '.CanApprove = True
                If .UpdateUserDetails > 0 Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""User added to Supervisor list"");window.location.href='Supervisor.aspx';</script>")
                End If
            End With
            clsUser = Nothing
        End If
    End Sub
End Class
