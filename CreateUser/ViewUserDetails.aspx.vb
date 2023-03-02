Imports FILEMGRLib
Imports System
Imports System.Data
Imports System.IO

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    'Public DDLCate As New DropDownList
    'Public DDLDes As New DropDownList
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Panel1.Visible = True
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim clsDep As New ETS.BL.Department
            With clsDep
                '.ContractorID = Session("ContractorID")
                '._WhereString.Append(" and (deleted is NULL or deleted = 'False')")
                Dim DSDep As DataSet = .GetDepartmentLstByWrkGroupID(Session("ContractorID"), String.Empty, String.Empty)
                DeptList.DataSource = DSDep
                DeptList.DataTextField = "Name"
                DeptList.DataValueField = "DepartmentID"
                DeptList.DataBind()
                DSDep.Dispose()
                Dim LI1 As New ListItem
                LI1.Text = "Any"
                LI1.Value = ""
                DeptList.Items.Insert(0, LI1)
                LI1 = Nothing
            End With
            clsDep = Nothing
            'Dim clsUsers As New ETS.BL.Users
            'With clsUsers
            '    '.ContractorID = Session("ContractorID")
            '    '.IsMentor = True
            '    Dim DSUsers As DataSet = .getUsersList(Session("ContractorID"), Session("WorkGroupID"), " AND U.IsMentor = 1 ")

            '    If DSUsers.Tables.Count > 0 Then
            '        If DSUsers.Tables(0).Rows.Count > 0 Then
            '            DSUsers.Tables(0).Columns.Add("UName", GetType(System.String), "Firstname + ' ' + Lastname")
            '            DLmentor.DataSource = DSUsers
            '            DLmentor.DataTextField = "UName"
            '            DLmentor.DataValueField = "userid"
            '            DLmentor.DataBind()
            '        End If
            '    End If
            '    DSUsers.Dispose()
            '    Dim LI1 As New ListItem
            '    LI1.Text = "Any"
            '    LI1.Value = ""
            '    DLmentor.Items.Insert(0, LI1)
            '    LI1 = Nothing
            'End With
            'clsUsers = Nothing
            Dim clsMentors As New ETS.BL.Mentors
            With clsMentors
                '.ContractorID = Session("ContractorID")
                '.IsMentor = True
                Dim DsMentors As DataSet = .getMentorsListByWrkGrpID(Session("ContractorID").ToString, String.Empty)

                If DsMentors.Tables.Count > 0 Then
                    If DsMentors.Tables(0).Rows.Count > 0 Then
                        'DsMentors.Tables(0).Columns.Add("UName", GetType(System.String), "Firstname + ' ' + Lastname")
                        DLmentor.DataSource = DsMentors
                        DLmentor.DataTextField = "Mentor"
                        DLmentor.DataValueField = "userid"
                        DLmentor.DataBind()
                    End If
                End If
                DsMentors.Dispose()
                Dim LI1 As New ListItem
                LI1.Text = "Any"
                LI1.Value = ""
                DLmentor.Items.Insert(0, LI1)
                LI1 = Nothing
            End With
            clsMentors = Nothing
            tblMain.Visible = False
        End If
    End Sub
    Public Function GetMentorName(ByVal MentorID As String) As String
        Dim varReturn As String = String.Empty
        If IsDBNull(MentorID.ToString) Then
            If String.IsNullOrEmpty(MentorID.ToString) Then
                varReturn = String.Empty
            Else
                varReturn = String.Empty
            End If
        Else
            If Not String.IsNullOrEmpty(MentorID.ToString) Then
                Dim clsUsr As ETS.BL.Users
                Dim varTemp As New StringBuilder
                Try
                    clsUsr = New ETS.BL.Users(MentorID.ToString)
                    varTemp.Append(clsUsr.FirstName.ToString & " ")
                    varTemp.Append(clsUsr.LastName.ToString)
                    varReturn = varTemp.ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsUsr = Nothing
                End Try
            End If
        End If

        Return varReturn.ToString
    End Function
    Protected Function GetCategoryName(ByVal CategoryID As String) As String
        Dim varReturn As String = String.Empty
        If Not String.IsNullOrEmpty(CategoryID) Then
            Dim clsCategory As ETS.BL.UserCategories
            Try
                clsCategory = New ETS.BL.UserCategories(CategoryID.ToString)
                varReturn = clsCategory.Name
            Catch ex As Exception
                Response.Write("From Cate:" & ex.Message)
            Finally
                clsCategory = Nothing
            End Try
        End If
        Return varReturn
    End Function
    Protected Function GetDesignationName(ByVal DesignationID As String) As String
        Dim varReturn As String = String.Empty
        If Not String.IsNullOrEmpty(DesignationID) Then
            Dim clsDesignation As ETS.BL.Designations
            Try
                clsDesignation = New ETS.BL.Designations(DesignationID.ToString)
                varReturn = clsDesignation.Name
            Catch ex As Exception
            Finally
                clsDesignation = Nothing
            End Try
        End If
        Return varReturn
    End Function
    Protected Function GetDeptName(ByVal DeptID As String) As String
        Dim varReturn As String = String.Empty
        If Not String.IsNullOrEmpty(DeptID) Then
            Dim clsDept As ETS.BL.Department
            Try
                clsDept = New ETS.BL.Department
                clsDept.DepartmentID = DeptID
                clsDept.getDepartmentDetails()
                varReturn = clsDept.Name
            Catch ex As Exception
            Finally
                clsDept = Nothing
            End Try
        End If
        Return varReturn
    End Function
    Sub SQLQuery()
        'Dim clsUC As New ETS.BL.UserCategories
        'With clsUC
        '    .ContractorID = Session("ContractorID")
        '    Dim DSCate As DataSet = .getCategoryList
        '    DDLCate.DataSource = DSCate
        '    DDLCate.DataTextField = "Name"
        '    DDLCate.DataValueField = "CategoryID"
        '    DDLCate.DataBind()
        '    DSCate.Dispose()
        'End With
        'clsUC = Nothing
        'Dim clsUD As New ETS.BL.Designations
        'With clsUD
        '    .Deleted = False
        '    Dim DSDes As DataSet = .getDesignationList

        '    DDLDes.DataSource = DSDes
        '    DDLDes.DataTextField = "Name"
        '    DDLDes.DataValueField = "DesignationID"
        '    DDLDes.DataBind()
        '    DSDes.Dispose()
        'End With
        'clsUD = Nothing
        Panel1.Visible = True
        Dim ColSearch1 As Boolean
        Dim ColSearch2 As Boolean
        Dim ColSearch3 As Boolean
        Dim ColSearch4 As Boolean
        Dim ColSearch5 As Boolean
        ColSearch1 = False
        ColSearch2 = False
        ColSearch3 = False
        ColSearch4 = False
        ColSearch5 = False
        Dim clsUser As New ETS.BL.Users
        Dim varStrWhere As New StringBuilder
        With clsUser
            If request("DeptList") <> "" Then
                .DepartmentID = request("DeptList")
                varStrWhere.Append(" AND U.DepartmentID='" & request("DeptList") & "' ")
            End If
            If Username.Text <> "" Then
                varStrWhere.Append(" AND U.Username like '" & Username.Text & "' ")
            End If
            If FirstName.Text <> "" Then
                varStrWhere.Append(" AND U.Firstname like '" & FirstName.Text & "' ")
            End If
            If LastName.Text <> "" Then
                varStrWhere.Append(" AND U.Lastname like '" & LastName.Text & "' ")
            End If
            If DLmentor.SelectedValue <> "" Then
                varStrWhere.Append(" ANd U.MentorID = '" & DLmentor.SelectedValue & "' ")
            End If
            'Response.Write(varStrWhere.ToString)
            'Response.End()
            '.ContractorID = Session("ContractorID")
            '._WhereString.Append(" order by firstname ")
            Dim DSUsers As DataSet = .getUsersListWithInactiveUsrs(Session("ContractorID"), String.Empty, varStrWhere.ToString)
            DSUsers.Tables(0).Columns.Add("DesID", GetType(System.String), "ISNULL(DesignationID,'')")
            MyDataGrid.DataSource = DSUsers
            MyDataGrid.DataBind()

            If MyDataGrid.Rows.Count > 0 Then
                MyDataGrid.ShowFooter = True
                MyDataGrid.UseAccessibleHeader = True
                MyDataGrid.HeaderRow.TableSection = TableRowSection.TableHeader
                MyDataGrid.FooterRow.TableSection = TableRowSection.TableFooter
            End If
            DSUsers.Dispose()
        End With
        clsUser = Nothing
        tblMain.Visible = True
    End Sub
    Public Function SetStatus(ByVal flag As Boolean) As String
        If IsDBNull(flag) Then
            Return "Active"
        Else
            If flag = True Then
                Return "Inactive"
            Else
                Return "Active"
            End If
        End If
    End Function
    'Protected Sub MyDataGrid_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyDataGrid.DataBinding

    'End Sub

    'Protected Sub MyDataGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles MyDataGrid.PageIndexChanging
    '    SQLQuery()
    'End Sub

    'Protected Sub MyDataGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles MyDataGrid.Sorting
    '    SQLQuery()
    'End Sub
   
    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim filename As String
    '    filename = "User Details " & Month(Now) & Day(Now) & Year(Now) & ".xls"
    '    Dim attachment As String = "attachment; filename=" & filename
    '    Response.ClearContent()
    '    Response.AddHeader("content-disposition", attachment)
    '    Response.ContentType = "application/ms-excel"
    '    Dim sw As New StringWriter()
    '    Dim htw As New HtmlTextWriter(sw)
    '    SQLQuery()

    '    MyDataGrid.DataSourceID = "SqlDataSource1"
    '    If RBPage.SelectedValue = "AP" Then
    '        MyDataGrid.AllowPaging = False
    '    ElseIf RBPage.SelectedValue = "CP" Then
    '        MyDataGrid.AllowPaging = True
    '    Else
    '        MyDataGrid.AllowPaging = False
    '    End If
    '    MyDataGrid.AllowSorting = False
    '    MyDataGrid.DataBind()
    '    MyDataGrid.AutoGenerateColumns = True
    '    Dim Table1 As New Table
    '    Table1.GridLines = GridLines.Both
    '    Table1.Font.Name = "Trebuchet MS"
    '    Table1.Font.Italic = True
    '    Dim x As Integer
    '    If (Not (MyDataGrid.HeaderRow) Is Nothing) Then
    '        Dim TRow1 As New TableRow
    '        For x = 0 To MyDataGrid.HeaderRow.Cells.Count - 1
    '            If MyDataGrid.Columns(x).Visible = True Then
    '                Dim TCell1 As New TableCell
    '                TCell1.Text = MyDataGrid.HeaderRow.Cells(x).Text
    '                TCell1.Font.Bold = True
    '                TCell1.BackColor = Drawing.Color.Gray
    '                TCell1.ForeColor = Drawing.Color.White
    '                TRow1.Cells.Add(TCell1)
    '            End If
    '        Next
    '        Table1.Rows.Add(TRow1)
    '    End If
    '    Dim i As Integer
    '    Dim k As Integer
    '    k = 0
    '    For Each row As GridViewRow In MyDataGrid.Rows
    '        k = k + 1
    '        Dim TRow1 As New TableRow
    '        For i = 0 To row.Cells.Count - 1
    '            'If MyDataGrid.Columns(i).Visible = True Then
    '            Dim TCell1 As New TableCell
    '            TCell1.Text = row.Cells(i).Text
    '            TRow1.Cells.Add(TCell1)
    '            'End If
    '        Next
    '        Table1.Rows.Add(TRow1)
    '        If MyDataGrid.AllowPaging = True And MyDataGrid.PageSize = k Then
    '            Exit For
    '        End If
    '    Next
    '    Table1.RenderControl(htw)
    '    'MyDataGrid.RenderControl(htw)
    '    Response.Write(sw.ToString())
    '    Response.[End]()
    '    MyDataGrid.AutoGenerateColumns = False
    '    'Dim filename As String
    '    'filename = "View Transcription Log " & Month(Now) & Day(Now) & Year(Now) & ".xls"
    '    'Dim attachment As String = "attachment; filename=" & filename
    '    'Response.ClearContent()
    '    'Response.AddHeader("content-disposition", attachment)
    '    'Response.ContentType = "application/ms-excel"
    '    'Dim sw As New StringWriter()
    '    'Dim htw As New HtmlTextWriter(sw)
    '    'If RBPage.SelectedValue = "AP" Then
    '    '    MyDataGrid.AllowPaging = False
    '    'ElseIf RBPage.SelectedValue = "CP" Then
    '    '    MyDataGrid.AllowPaging = True
    '    'Else
    '    '    MyDataGrid.AllowPaging = False
    '    'End If
    '    'MyDataGrid.AllowSorting = False
    '    'MyDataGrid.ShowCount = False
    '    'MyDataGrid.DataBind()
    '    'MyDataGrid.RenderControl(htw)
    '    'Dim Table1 As New Table
    '    'Table1.GridLines = GridLines.Both
    '    'Table1.Font.Name = "Trebuchet MS"
    '    'Table1.Font.Italic = True
    '    'Dim x As Integer
    '    'If (Not (MyDataGrid.HeaderRow) Is Nothing) Then
    '    '    Dim TRow1 As New TableRow
    '    '    For x = 4 To MyDataGrid.HeaderRow.Cells.Count - 1
    '    '        If MyDataGrid.Columns(x).Visible = True Then
    '    '            Dim TCell1 As New TableCell
    '    '            TCell1.Text = MyDataGrid.HeaderRow.Cells(x).Text
    '    '            TCell1.Font.Bold = True
    '    '            TCell1.BackColor = Drawing.Color.Gray
    '    '            TCell1.ForeColor = Drawing.Color.White
    '    '            TRow1.Cells.Add(TCell1)
    '    '        End If
    '    '    Next
    '    '    Table1.Rows.Add(TRow1)
    '    'End If
    '    'Dim i As Integer
    '    'Dim k As Integer
    '    'k = 0
    '    'For Each row As GridViewRow In MyDataGrid.Rows
    '    '    k = k + 1
    '    '    Dim TRow1 As New TableRow
    '    '    For i = 4 To row.Cells.Count - 1
    '    '        If MyDataGrid.Columns(i).Visible = True Then
    '    '            Dim TCell1 As New TableCell
    '    '            TCell1.Text = row.Cells(i).Text
    '    '            TRow1.Cells.Add(TCell1)
    '    '        End If
    '    '    Next
    '    '    Table1.Rows.Add(TRow1)
    '    '    If MyDataGrid.AllowPaging = True And MyDataGrid.PageSize = k Then
    '    '        Exit For
    '    '    End If
    '    'Next
    '    'Table1.RenderControl(htw)
    '    'Response.Write(sw.ToString())
    '    'Response.[End]()
    'End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        SQLQuery()
    End Sub
    Protected Sub LnlExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnlExport.Click
        Response.Clear()
        SQLQuery()
        Dim filename = "Search User Report " & Now & " .xls"
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        Me.EnableViewState = False
        Dim tw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        MyDataGrid.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
End Class

