Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType




Partial Class EditUserEmail
    Inherits BasePage

    Public strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TxtEmail.ReadOnly = True
        lblMsg.Text = String.Empty
        If Not IsPostBack Then
            DropDownList1.Attributes.Add("onchange", "hide();")
            BindUserData()
            TxtDesi.Items.Clear()
            Panel1.Visible = False
            For Each c As Control In Panel1.Controls 'LOOP THROUGHN ALL CONTROLS 
                If c.GetType() Is GetType(TextBox) Then 'IF ITS A TEXTBOX THEN EMPTY IT 
                    CType(c, TextBox).Text = String.Empty
                    '    ElseIf c.GetType Is GetType(DropDownList) Then 'ELSE IF ITS A DROPDOWN LIST SET SELECTED VALUE TO -1     
                    '        CType(c, DropDownList).SelectedIndex = 0
                End If
            Next
        End If
        Dim strURL As String = "userphoto.aspx?UserID=" & Request("DropDownList1")
        Image1.ImageUrl = strURL
    End Sub
    Protected Sub BindMentor()
        DLmentor.Items.Clear()
        Dim clsM As ETS.BL.Mentors
        Dim Ds As New Data.DataSet
        Try
            clsM = New ETS.BL.Mentors
            Ds = clsM.getMentorsListByWrkGrpID(Session("ContractorID").ToString, Session("WorkGroupID").ToString)
            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    DLmentor.DataSource = Ds
                    DLmentor.DataTextField = "Mentor"
                    DLmentor.DataValueField = "userid"
                    DLmentor.DataBind()
                End If
            End If
        Catch ex As Exception
        Finally
            clsM = Nothing
            Ds = Nothing
        End Try
        DLmentor.Items.Insert(0, New ListItem("Please Select", String.Empty))


        'Dim clsUsers As ETS.BL.Users
        'Try
        '    clsUsers = New ETS.BL.Users
        '    With clsUsers
        '        .ContractorID = Session("ContractorID")
        '        .IsMentor = True
        '        Dim DSUsers As DataSet = .getUsersList()
        '        DSUsers.Tables(0).Columns.Add("UName", GetType(System.String), "Firstname + ' ' + Lastname")
        '        DLmentor.DataSource = DSUsers
        '        DLmentor.DataTextField = "UName"
        '        DLmentor.DataValueField = "userid"
        '        DLmentor.DataBind()
        '        DSUsers.Dispose()
        '        DLmentor.Items.Insert(0, New ListItem("Please Select", String.Empty))
        '    End With
        'Catch ex As Exception
        'Finally
        '    clsUsers = Nothing
        'End Try
    End Sub
    Protected Sub BindDept()
        TxtDept.Items.Clear()
        Dim clsDep As ETS.BL.Department
        Try
            clsDep = New ETS.BL.Department

            With clsDep
                .ContractorID = Session("ContractorID")
                ._WhereString.Append(" and (deleted is NULL or deleted = 'False')")
                Dim DSDep As DataSet = .getDepartmentList()
                TxtDept.DataSource = DSDep
                TxtDept.DataTextField = "Name"
                TxtDept.DataValueField = "DepartmentID"
                TxtDept.DataBind()
                DSDep.Dispose()
            End With
            TxtDept.Items.Insert(0, New ListItem("Please Select", String.Empty))
        Catch ex As Exception
        Finally
            clsDep = Nothing
        End Try
    End Sub
    Protected Sub BindUserData()
        DropDownList1.Items.Clear()
        Dim clsUsrs As ETS.BL.Users
        Dim DV As Data.DataView
        Dim DS As Data.DataSet
        Try
            clsUsrs = New ETS.BL.Users
            'clsUsrs.ContractorID = Session("ContractorID")
            'clsUsrs._WhereString.Append(" AND (IsDeleted is NULL or Isdeleted = 'False')")
            DS = clsUsrs.getUsersListWithInactiveUsrs(Session("ContractorID"), Session("WorkGroupID"), String.Empty)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DS.Tables(0).Columns.Add("BindName", GetType(System.String), "FirstName + ' '+ LastName + ' (' + UserName + ')'")
                    DV = New Data.DataView(DS.Tables(0), String.Empty, "FirstName,LastName", DataViewRowState.CurrentRows)

                    DropDownList1.DataSource = DV
                    DropDownList1.DataValueField = "UserID"
                    DropDownList1.DataTextField = "BindName"
                    DropDownList1.DataBind()
                End If
            End If
            DropDownList1.Items.Insert(0, New ListItem("Please Select", String.Empty))
        Catch ex As Exception
        Finally
            clsUsrs = Nothing
            DS = Nothing
            DV = Nothing
        End Try
    End Sub
    Protected Sub BindUserCategories()
        txtCate.Items.Clear()
        Dim clsUC As ETS.BL.UserCategories
        Try
            clsUC = New ETS.BL.UserCategories

            With clsUC
                .ContractorID = Session("ContractorID")
                Dim DSCate As DataSet = .getCategoryList
                txtCate.DataSource = DSCate
                txtCate.DataTextField = "Name"
                txtCate.DataValueField = "CategoryID"
                txtCate.DataBind()
                DSCate.Dispose()
                txtCate.Items.Insert(0, New ListItem("Please Select", String.Empty))
            End With
        Catch ex As Exception
        Finally
            clsUC = Nothing
        End Try
    End Sub
    Protected Sub BindDeptDesignation(ByVal DeptID As String)
        TxtDesi.Items.Clear()
        Dim clsUD As ETS.BL.Designations
        Try
            clsUD = New ETS.BL.Designations
            clsUD.Deleted = False
            clsUD.DepartmentID = DeptID
            Dim DSDes As DataSet = clsUD.getDesignationList
            TxtDesi.DataSource = DSDes
            TxtDesi.DataTextField = "Name"
            TxtDesi.DataValueField = "DesignationID"
            TxtDesi.DataBind()
            DSDes.Dispose()
            TxtDesi.Items.Insert(0, New ListItem("Please Select", String.Empty))
        Catch ex As Exception
        Finally
            clsUD = Nothing
        End Try
    End Sub
    Sub ShowData()

        'Panel1.Visible = True
        Image1.Attributes.Add("onclick", "popupwin();")
        txtCountry.SelectedIndex = -1
        txtpCountry.SelectedIndex = -1
        TxtDept.SelectedIndex = -1
        TxtDesi.SelectedIndex = -1
        txtCate.SelectedIndex = -1
        DLStatus.SelectedIndex = -1
        DLmentor.SelectedIndex = -1
        Image2.Attributes.Add("onclick", "poptastic('" & DropDownList1.SelectedValue & "')")

        'hdnUserID.Value = Request("DropDownList1").ToString

        Dim clsUsr As ETS.BL.Users
        Try
            clsUsr = New ETS.BL.Users()
            clsUsr.getUserInfoIncludingDeletedUsrFromEditUsr(DropDownList1.SelectedValue)
            hdnUserID.Value = clsUsr.UserID.ToString
            LblName.Text = clsUsr.FirstName.ToString & " " & clsUsr.LastName.ToString()
            TxtFirstName.Text = clsUsr.FirstName.ToString
            TxtMiddleName.Text = clsUsr.MiddleName.ToString
            TxtLastName.Text = clsUsr.LastName.ToString
            TxtEmail.Text = clsUsr.OfficialMailID.ToString
            TxtNonOEmail.Text = clsUsr.OtherMailID.ToString
            TxtChatID.Text = clsUsr.ChatID.ToString
            TxtAdd.Text = clsUsr.Address.ToString
            TxtCity.Text = clsUsr.City.ToString
            TxtState.Text = clsUsr.State.ToString
            'TRGLines.Text = clsUsr.TRGLines.ToString
            DLStatus.SelectedIndex = -1
            If clsUsr.IsDeleted.ToString = "True" Then
                DLStatus.Items(0).Selected = False
                DLStatus.Items(1).Selected = True
            Else
                DLStatus.Items(1).Selected = False
                DLStatus.Items(0).Selected = True
            End If

            TxtpAdd.Text = clsUsr.PAddress.ToString
            TxtpCity.Text = clsUsr.PCity.ToString
            TxtpState.Text = clsUsr.PState.ToString
            Dim i As Integer
            For i = 0 To txtCountry.Items.Count - 1
                If txtCountry.Items(i).Value = clsUsr.Country.ToString Then
                    txtCountry.Items(i).Selected = True
                End If
            Next

            BindMentor()

            If DLmentor.Items.Count > 0 Then
                For i = 0 To DLmentor.Items.Count - 1
                    DLmentor.Items(i).Selected = False
                    If DLmentor.Items(i).Value = clsUsr.MentorID.ToString Then
                        DLmentor.Items(i).Selected = True
                    End If
                Next
            End If

            For i = 0 To txtpCountry.Items.Count - 1
                If txtpCountry.Items(i).Value = clsUsr.PCountry.ToString Then
                    txtpCountry.Items(i).Selected = True
                End If
            Next

            If Not IsDate(clsUsr.DateOfBirth.ToString) Then
                TxtDOB.Text = ""
            Else
                TxtDOB.Text = FormatDateTime(clsUsr.DateOfBirth.ToString, DateFormat.ShortDate)
            End If
            If Not IsDate(clsUsr.DateJoined.ToString) Then
                TxtJoin.Text = ""
            Else
                TxtJoin.Text = FormatDateTime(clsUsr.DateJoined.ToString, DateFormat.ShortDate)
            End If

            If Not IsDate(clsUsr.DateTerminated.ToString) Then
                TxtTerm.Text = ""
            Else
                TxtTerm.Text = FormatDateTime(clsUsr.DateTerminated.ToString, DateFormat.ShortDate)
            End If

            TxtCell.Text = clsUsr.CellNo.ToString
            TxtTel.Text = clsUsr.PhoneNo.ToString
            'Dim I1 As Integer = "str"
            BindDept()
            For i = 0 To TxtDept.Items.Count - 1
                If TxtDept.Items(i).Value = clsUsr.DepartmentID.ToString Then
                    TxtDept.Items(i).Selected = True
                End If
            Next

            If Not String.IsNullOrEmpty(TxtDept.SelectedValue) Then
                TxtDesi.SelectedIndex = -1
                BindDeptDesignation(TxtDept.SelectedValue.ToString)
                For i = 0 To TxtDesi.Items.Count - 1
                    If TxtDesi.Items(i).Value = clsUsr.DesignationID.ToString Then
                        TxtDesi.Items(i).Selected = True
                    End If
                Next
            End If

            BindUserCategories()
            For i = 0 To txtCate.Items.Count - 1
                If txtCate.Items(i).Value = clsUsr.CategoryID.ToString Then
                    txtCate.Items(i).Selected = True
                End If
            Next
            Panel1.Visible = True
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsr = Nothing
        End Try



    End Sub
    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue <> "" Then
            Panel1.Visible = False
            ShowData()
        Else
            Panel1.Visible = False
        End If
    End Sub
    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click

        Dim clsUsr As ETS.BL.Users
        Try
            clsUsr = New ETS.BL.Users
            clsUsr.UserID = hdnUserID.Value.ToString

            If Not String.IsNullOrEmpty(Request("TxtFirstName")) Then
                clsUsr.FirstName = Request("TxtFirstName")
            Else
                clsUsr.FirstName = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtMiddleName")) Then
                clsUsr.MiddleName = Request("TxtMiddleName")
            Else
                clsUsr.MiddleName = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtLastName")) Then
                clsUsr.LastName = Request("TxtLastName")
            Else
                clsUsr.LastName = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtEmail")) Then
                clsUsr.OfficialMailID = Request("TxtEmail")
            Else
                clsUsr.OfficialMailID = String.Empty
            End If
            ' Response.Write(String.IsNullOrEmpty(Request("TxtEmail")))
            If Not String.IsNullOrEmpty(Request("TxtNonOEmail")) Then
                clsUsr.OtherMailID = Request("TxtNonOEmail")
            Else
                clsUsr.OtherMailID = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtChatID")) Then
                clsUsr.ChatID = Request("TxtChatID")
            Else
                clsUsr.ChatID = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtAdd")) Then
                clsUsr.Address = Request("TxtAdd")
            Else
                clsUsr.Address = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtCity")) Then
                clsUsr.City = Request("TxtCity")
            Else
                clsUsr.City = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtState")) Then
                clsUsr.State = Request("TxtState")
            Else
                clsUsr.State = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtCountry")) Then
                clsUsr.Country = Request("TxtCountry")
            Else
                clsUsr.Country = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtCell")) Then
                clsUsr.CellNo = Request("TxtCell")
            Else
                clsUsr.CellNo = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtTel")) Then
                clsUsr.PhoneNo = Request("TxtTel")
            Else
                clsUsr.PhoneNo = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtpAdd")) Then
                clsUsr.PAddress = Request("TxtpAdd")
            Else
                clsUsr.PAddress = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtpCity")) Then
                clsUsr.PCity = Request("TxtpCity")
            Else
                clsUsr.PCity = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtpState")) Then
                clsUsr.PState = Request("TxtpState")
            Else
                clsUsr.PState = String.Empty
            End If

            If Not String.IsNullOrEmpty(Request("TxtpCountry")) Then
                clsUsr.PCountry = Request("TxtpCountry")
            Else
                clsUsr.PCountry = String.Empty
            End If


            'If Not String.IsNullOrEmpty(Request("TRGLines")) Then
            '    clsUsr.TRGLines = Request("TRGLines")
            'Else
            '    clsUsr.TRGLines = String.Empty
            'End If

            If Not String.IsNullOrEmpty(Request("DLStatus")) Then
                clsUsr.IsDeleted = Request("DLStatus")
            End If

            If Not String.IsNullOrEmpty(Request("TxtDOB").ToString) Then
                If IsDate(Request("TxtDOB")) Then
                    clsUsr.DateOfBirth = Trim(Request("TxtDOB").ToString)
                Else
                    lblMsg.Text = "Invalid DateOfBirth"
                    Exit Sub
                End If
            Else
                If String.IsNullOrEmpty(clsUsr._UpdateString.ToString) Then
                    clsUsr._UpdateString.Append(" DateOfBirth = NULL ")
                Else
                    clsUsr._UpdateString.Append(", DateOfBirth = NULL ")
                End If
            End If


            If Not String.IsNullOrEmpty(Request("TxtJoin").ToString) Then
                If IsDate(Request("TxtJoin")) Then
                    clsUsr.DateJoined = Trim(Request("TxtJoin").ToString)
                Else
                    lblMsg.Text = "Invalid Date of Joining"
                    Exit Sub
                End If
            Else
                If String.IsNullOrEmpty(clsUsr._UpdateString.ToString) Then
                    clsUsr._UpdateString.Append(" DateJoined = NULL ")
                Else
                    clsUsr._UpdateString.Append(", DateJoined = NULL ")
                End If
            End If



            If Not String.IsNullOrEmpty(Request("TxtTerm").ToString) Then
                If IsDate(Request("TxtTerm").ToString) Then
                    clsUsr.DateTerminated = Trim(Request("TxtTerm").ToString)
                Else
                    lblMsg.Text = "Invalid Date of termination"
                    Exit Sub
                End If
            Else
                If String.IsNullOrEmpty(clsUsr._UpdateString.ToString) Then
                    clsUsr._UpdateString.Append(" DateTerminated = NULL ")
                Else
                    clsUsr._UpdateString.Append(", DateTerminated = NULL ")
                End If
            End If

            If Not String.IsNullOrEmpty(Request("TxtDept")) Then
                clsUsr.DepartmentID = Request("TxtDept")
            Else
                If String.IsNullOrEmpty(clsUsr._UpdateString.ToString) Then
                    clsUsr._UpdateString.Append(" DepartmentID = NULL ")
                Else
                    clsUsr._UpdateString.Append(", DepartmentID = NULL ")
                End If
            End If

            If Not String.IsNullOrEmpty(Request("DLmentor")) Then
                clsUsr.MentorID = Request("DLmentor")
            Else
                If String.IsNullOrEmpty(clsUsr._UpdateString.ToString) Then
                    clsUsr._UpdateString.Append(" MentorID = NULL ")
                Else
                    clsUsr._UpdateString.Append(", MentorID = NULL ")
                End If
            End If

            If Not String.IsNullOrEmpty(Request("TxtDesi")) Then
                clsUsr.DesignationID = Request("TxtDesi")
            Else
                If String.IsNullOrEmpty(clsUsr._UpdateString.ToString) Then
                    clsUsr._UpdateString.Append(" DesignationID = NULL ")
                Else
                    clsUsr._UpdateString.Append(", DesignationID = NULL ")
                End If
            End If

            If Not String.IsNullOrEmpty(Request("txtCate")) Then
                clsUsr.CategoryID = Request("txtCate")
            Else
                If String.IsNullOrEmpty(clsUsr._UpdateString.ToString) Then
                    clsUsr._UpdateString.Append(" CategoryID = NULL ")
                Else
                    clsUsr._UpdateString.Append(", CategoryID = NULL ")
                End If
            End If


            If clsUsr.btn_EditUsr() = True Then
                lblMsg.Text = "User data updated sucessfully"
                ShowData()
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsr = Nothing
        End Try

    End Sub
    Protected Sub TxtDept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDept.SelectedIndexChanged
        Panel1.Visible = True
        BindDeptDesignation(TxtDept.SelectedValue)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        lblMsg.Text = ""
        'Response.Write(DLStatus.SelectedValue)
        If DLStatus.SelectedValue = True Then
            TxtEmail.Text = TxtEmail.Text & "_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second
            lblMsg.Text = "E-Mail id is now changed, please click Submit button to commit the changes."
        Else
            lblMsg.Text = "User must be Inactive before you rename the user email id"
        End If
        ' ShowData()
    End Sub
End Class
