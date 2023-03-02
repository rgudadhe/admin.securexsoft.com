Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType




Partial Class EditUser
    Inherits BasePage

    Public strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    Public RecFound As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = String.Empty
        If Not IsPostBack Then
            RecFound = False
            BindUserData()
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
    Protected Sub BindPlatform()
        Dim clsM As ETS.BL.Accounts
        Dim Ds As New Data.DataSet
        Try
            clsM = New ETS.BL.Accounts
            clsM.ContractorID = Session("ContractorID").ToString

            clsM._WhereString.Append(" and ISPlatForm =1 ")

            Ds = clsM.getAccountList
            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    DLPlatForm.DataSource = Ds
                    DLPlatForm.DataTextField = "AccountName"
                    DLPlatForm.DataValueField = "AccountID"
                    DLPlatForm.DataBind()
                End If
            End If
        Catch ex As Exception
        Finally
            clsM = Nothing
            Ds = Nothing
        End Try
        DLPlatForm.Items.Insert(0, New ListItem("SecureXSoft", "11111111-1111-1111-1111-111111111111"))


        'Dim clsUsers As ETS.BL.Users
        'Try
        '    clsUsers = New ETS.BL.Users
        '    With clsUsers
        '        .ContractorID = Session("ContractorID")
        '        .IsMentor = True
        '        Dim DSUsers As DataSet = .getUsersList()
        '        DSUsers.Tables(0).Columns.Add("UName", GetType(System.String), "Firstname + ' ' + Lastname")
        '        DLPlatForm.DataSource = DSUsers
        '        DLPlatForm.DataTextField = "UName"
        '        DLPlatForm.DataValueField = "userid"
        '        DLPlatForm.DataBind()
        '        DSUsers.Dispose()
        '        DLPlatForm.Items.Insert(0, New ListItem("Please Select", String.Empty))
        '    End With
        'Catch ex As Exception
        'Finally
        '    clsUsers = Nothing
        'End Try
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

    Sub ShowData()
        Panel1.Visible = True
        DLMode.SelectedIndex = -1
        DLPlatForm.SelectedIndex = -1
        Image2.Attributes.Add("onclick", "poptastic('" & Request("DropDownList1") & "')")
        hdnUserID.Value = Request("DropDownList1").ToString
        'Response.Write(Request("DropDownList1").ToString)
        Dim clsUsr As ETS.BL.UserDetails
        Try
            clsUsr = New ETS.BL.UserDetails
            clsUsr._WhereString.Append(" WHERE userid = '" & Request("DropDownList1").ToString & "'")

            Dim DS As DataSet = clsUsr.getUsersList()

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    HRecFound.Value = True
                    BindPlatform()
                    'Response.Write(RecFound)
                    If DS.Tables(0).Rows(0).Item("PlatAccID").ToString <> "" Then
                        For i As Integer = 0 To DLPlatForm.Items.Count - 1
                            DLPlatForm.Items(i).Selected = False
                            If DLPlatForm.Items(i).Value = DS.Tables(0).Rows(0).Item("PlatAccID").ToString Then
                                DLPlatForm.Items(i).Selected = True
                            End If
                        Next
                    
                    End If

                    If DS.Tables(0).Rows(0).Item("ProdMode").ToString <> "" Then
                        For i As Integer = 0 To DLMode.Items.Count - 1
                            DLMode.Items(i).Selected = False
                            'Response.Write(DLMode.Items(i).Value)
                            If DLMode.Items(i).Value = DS.Tables(0).Rows(0).Item("ProdMode").ToString Then
                                DLMode.Items(i).Selected = True
                            End If
                        Next
                    Else
                        DLMode.Items.Insert(0, New ListItem("Not Set", ""))
                    End If

                Else
                    HRecFound.Value = False
                    BindPlatform()
                    DLPlatForm.Items.Insert(0, New ListItem("Not Set", ""))
                    DLMode.Items.Insert(0, New ListItem("Not Set", ""))
                End If

                
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsr = Nothing
        End Try


        'Panel1.Visible = True
        'Image1.Attributes.Add("onclick", "popupwin();")

        'Dim RecFound As String
        'Dim Categoryid As String
        'Categoryid = ""
        'RecFound = "No"
        'Dim i As Integer
        'i = 0
        ''For i = 0 To txtpCountry.Items.Count - 1
        ''    txtpCountry.Items(i).Selected = False
        ''Next

        ''For i = 0 To txtCountry.Items.Count - 1
        ''    txtCountry.Items(i).Selected = False
        ''Next
        ''For i = 0 To TxtDept.Items.Count - 1
        ''    TxtDept.Items(i).Selected = False
        ''Next

        ''For i = 0 To TxtDesi.Items.Count - 1
        ''    TxtDesi.Items(i).Selected = False
        ''Next

        ''For i = 0 To txtCate.Items.Count - 1
        ''    txtCate.Items(i).Selected = False
        ''Next

        'Image2.Attributes.Add("onclick", "poptastic('" & Request("DropDownList1") & "')")
        'Dim X As Integer
        'X = 0
        'Dim DSUsers As New Data.DataSet

        'Dim clsUsr As ETS.BL.Users
        ''Dim SQLCmd As New SqlCommand("Select * from tblUsers where UserID='" & Request("DropDownList1") & "'", New SqlConnection(strConn))
        'Try
        '    clsUsr = New ETS.BL.Users(Request("DropDownList1"))

        '    'SQLCmd.Connection.Open()
        '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
        '    If DRRec.Read = True Then
        '        LblName.Text = DRRec("firstname").ToString & " " & DRRec("lastname").ToString
        '        TxtFirstName.Text = DRRec("firstname").ToString
        '        TxtMiddleName.Text = DRRec("middlename").ToString
        '        TxtLastName.Text = DRRec("lastname").ToString
        '        TxtEmail.Text = DRRec("OfficialMailID").ToString
        '        TxtNonOEmail.Text = DRRec("OtherMailID").ToString
        '        TxtChatID.Text = DRRec("ChatID").ToString
        '        TxtAdd.Text = DRRec("Address").ToString
        '        TxtCity.Text = DRRec("City").ToString
        '        TxtState.Text = DRRec("State").ToString
        '        TRGLines.Text = DRRec("TRGlines").ToString
        '        If DRRec("IsDeleted").ToString = "True" Then
        '            DLMode.Items(0).Selected = False
        '            DLMode.Items(1).Selected = True
        '        Else
        '            DLMode.Items(1).Selected = False
        '            DLMode.Items(0).Selected = True
        '        End If

        '        TxtpAdd.Text = DRRec("PAddress").ToString
        '        TxtpCity.Text = DRRec("PCity").ToString
        '        TxtpState.Text = DRRec("PState").ToString
        '        For i = 0 To txtCountry.Items.Count - 1
        '            If txtCountry.Items(i).Value = DRRec("Country").ToString Then
        '                txtCountry.Items(i).Selected = True
        '            End If
        '        Next
        '        For i = 0 To DLPlatForm.Items.Count - 1
        '            DLPlatForm.Items(i).Selected = False
        '            If DLPlatForm.Items(i).Value = DRRec("mentorid").ToString Then
        '                DLPlatForm.Items(i).Selected = True
        '            End If
        '        Next


        '        For i = 0 To txtpCountry.Items.Count - 1
        '            If txtpCountry.Items(i).Value = DRRec("pCountry").ToString Then
        '                txtpCountry.Items(i).Selected = True
        '            End If
        '        Next

        '        If Not IsDate(DRRec("DatOfBirth").ToString) Then
        '            TxtDOB.Text = ""
        '        Else
        '            TxtDOB.Text = FormatDateTime(DRRec("DatOfBirth").ToString, DateFormat.ShortDate)
        '        End If
        '        If Not IsDate(DRRec("DateJoined").ToString) Then
        '            TxtJoin.Text = ""
        '        Else
        '            TxtJoin.Text = FormatDateTime(DRRec("DateJoined").ToString, DateFormat.ShortDate)
        '        End If

        '        If Not IsDate(DRRec("DateTerminated").ToString) Then
        '            TxtTerm.Text = ""
        '        Else
        '            TxtTerm.Text = FormatDateTime(DRRec("DateTerminated").ToString, DateFormat.ShortDate)
        '        End If

        '        TxtCell.Text = DRRec("CellNo").ToString
        '        TxtTel.Text = DRRec("PhoneNo").ToString
        '        For i = 0 To TxtDept.Items.Count - 1
        '        Next


        '        For i = 0 To TxtDept.Items.Count - 1
        '            If TxtDept.Items(i).Value = DRRec("DepartmentID").ToString Then
        '                TxtDept.Items(i).Selected = True
        '            End If
        '        Next





        '        TxtDesi.Items.Clear()

        '        Dim SQLCmd2 As New SqlCommand("Select * from tblDeptDesignations where DepartmentID = '" & DRRec("DepartmentID").ToString & "' ", New SqlConnection(strConn))
        '        'Response.Write("Select * from tblUsers where UserID='" & UserId & "'")
        '        'Response.End()
        '        Try
        '            SQLCmd2.Connection.Open()
        '            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
        '            If DRRec2.HasRows = True Then
        '                While DRRec2.Read
        '                    Dim LI As New ListItem
        '                    LI.Text = DRRec2("Name").ToString
        '                    LI.Value = DRRec2("DesignationID").ToString
        '                    If DRRec2("DesignationID").ToString = DRRec("DesignationID").ToString Then
        '                        LI.Selected = True
        '                    End If
        '                    TxtDesi.Items.Add(LI)

        '                End While
        '            End If
        '            DRRec2.Close()
        '        Finally
        '            If SQLCmd2.Connection.State = ConnectionState.Open Then
        '                SQLCmd2.Connection.Close()
        '            End If
        '        End Try
        '        'Response.Write(DRRec("DesignationID").ToString)

        '        'For i = 0 To TxtDesi.Items.Count - 1
        '        '    If TxtDesi.Items(i).Value = DRRec("DesignationID").ToString Then
        '        '        TxtDesi.Items(i).Selected = True
        '        '    End If
        '        'Next

        '        For i = 0 To txtCate.Items.Count - 1
        '            If txtCate.Items(i).Value = DRRec("categoryid").ToString Then
        '                txtCate.Items(i).Selected = True
        '            End If
        '        Next

        '        'Dim DepartmentID = DRRec("DepartmentID").ToString
        '        'Dim DesignationID = DRRec("DesignationID").ToString
        '        Categoryid = DRRec("CategoryID").ToString


        '    End If
        '    DRRec.Close()
        'Finally
        '    If SQLCmd.Connection.State = ConnectionState.Open Then
        '        SQLCmd.Connection.Close()
        '    End If
        'End Try
        ''Response.Write(Categoryid)

        'txtCate.Items.Clear()
        'Dim SQLCmd1 As New SqlCommand("Select * from tblUsersCategory where contractorid='" & Session("contractorid").ToString & "' and (deleted is NULL) or (deleted = 'False') ", New SqlConnection(strConn))
        'Try
        '    SQLCmd1.Connection.Open()
        '    Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
        '    If DRRec1.HasRows Then
        '        While DRRec1.Read
        '            Dim LI As New ListItem
        '            LI.Text = DRRec1("Name").ToString
        '            LI.Value = DRRec1("CategoryID").ToString
        '            txtCate.Items.Add(LI)
        '            If Categoryid <> "" And DRRec1("Categoryid").ToString = Categoryid Then
        '                LI.Selected = True
        '                RecFound = "Yes"
        '            End If
        '        End While
        '    End If
        '    DRRec1.Close()
        'Finally
        '    If SQLCmd1.Connection.State = ConnectionState.Open Then
        '        SQLCmd1.Connection.Close()
        '    End If
        'End Try
        'If RecFound = "No" Then
        '    Dim LI As New ListItem
        '    LI.Text = "Select Category"
        '    LI.Value = ""
        '    txtCate.Items.Add(LI)
        '    LI.Selected = True
        'End If

    End Sub
    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        If DropDownList1.SelectedValue <> "" Then
            ShowData()
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub
    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        'Panel1.Visible = False
        Dim clsUsr As ETS.BL.UserDetails
        Try
            clsUsr = New ETS.BL.UserDetails
            clsUsr.UserID = Request("DropDownList1")

            If Not String.IsNullOrEmpty(Request("DLMode")) Then
                clsUsr.ProdMode = Request("DLMode")
            Else
                If String.IsNullOrEmpty(clsUsr._UpdateString.ToString) Then
                    clsUsr._UpdateString.Append(" DLMode = NULL ")
                Else
                    clsUsr._UpdateString.Append(", DLMode = NULL ")
                End If
            End If
            If Not String.IsNullOrEmpty(Request("DLPlatForm")) Then
                clsUsr.PlatAccID = Request("DLPlatForm")
            Else
                If String.IsNullOrEmpty(clsUsr._UpdateString.ToString) Then
                    clsUsr._UpdateString.Append(" PlatAccID = NULL ")
                Else
                    clsUsr._UpdateString.Append(", PlatAccID = NULL ")
                End If
            End If
            'Response.Write(HRecFound.Value)
            If HRecFound.Value = True Then
                If clsUsr.UpdateUserDetails = True Then
                    'Label1.Text = "User data updated sucessfully"
                    'ShowData()
                End If
            Else
                Dim strConn As String
                Dim squery As String
                strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
               
                squery = "DELETE FROM AdminETS.dbo.tblUsersDetails   Where UserID = '" & Request("DropDownList1") & "'  "
                Dim cmdUp As New SqlCommand(squery, New SqlConnection(strConn))
                Try
                    cmdUp.Connection.Open()
                    cmdUp.ExecuteNonQuery()
                Finally
                    If cmdUp.Connection.State = System.Data.ConnectionState.Open Then
                        cmdUp.Connection.Close()
                    End If
                End Try
                If clsUsr.InsertUserDetails = True Then
                    'HRecFound.Value = True
                    ' Label1.Text = "User data updated sucessfully"

                End If
            End If
            ShowData()
            Label1.Text = "User data updated sucessfully"
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsr = Nothing
        End Try


        'Dim strQuery As String
        'Dim Userid As String
        'Userid = Request("DropDownList1")
        'Dim TRGLines1 As Integer
        'TRGLines1 = 0
        'If TRGLines.Text = "" Then
        '    TRGLines1 = 0
        'Else
        '    TRGLines1 = TRGLines.Text
        'End If

        'strQuery = "Update TblUsers Set FirstName='" & Request("TxtFirstName") & "',MiddleName='" & Request("TxtMiddleName") & "', LastName='" & Request("TxtLastName") & "',OfficialMailID='" & Request("TxtEmail") & "', OtherMailID='" & Request("TxtNonOEmail") & "', ChatID='" & Request("TxtChatID") & "', Address= '" & Request("TxtAdd") & "', City='" & Request("TxtCity") & "', State='" & Request("TxtState") & "', Country='" & Request("TxtCountry") & "', CellNo='" & Request("TxtCell") & "', PhoneNo='" & Request("TxtTel") & "', PAddress= '" & Request("TxtpAdd") & "', PCity='" & Request("TxtpCity") & "', PState='" & Request("TxtpState") & "', PCountry='" & Request("TxtpCountry") & "', TRGLines='" & TRGLines1 & "', Isdeleted = '" & DLMode.SelectedValue & "'"
        'If Not IsDate(Request("TxtDOB")) Then
        '    strQuery = strQuery & ", DatOfBirth=NULL"
        'Else
        '    strQuery = strQuery & ", DatOfBirth='" & Request("TxtDOB") & "'"
        'End If
        'If Not IsDate(Request("TxtJoin")) Then
        '    strQuery = strQuery & ", DateJoined=NULL"
        'Else
        '    strQuery = strQuery & ", DateJoined='" & Request("TxtJoin") & "'"
        'End If

        'If Not IsDate(Request("TxtTerm")) Then
        '    strQuery = strQuery & ", DateTerminated=NULL"
        'Else
        '    strQuery = strQuery & ", DateTerminated='" & Request("TxtTerm") & "'"
        'End If
        'If Request("TxtDept") = "" Then
        '    strQuery = strQuery & ", DepartmentID=NULL"
        'Else
        '    strQuery = strQuery & ", DepartmentID='" & Request("TxtDept") & "'"
        'End If
        'If Request("DLPlatForm") = "" Then
        '    strQuery = strQuery & ", MentorID=NULL"
        'Else
        '    strQuery = strQuery & ", MentorID='" & Request("DLPlatForm") & "'"
        'End If


        'If Request("TxtDesi") = "" Then
        '    strQuery = strQuery & ", DesignationID=NULL"
        'Else
        '    strQuery = strQuery & ", DesignationID= '" & Request("TxtDesi") & "'"
        'End If
        'If Request("txtCate") = "" Then
        '    strQuery = strQuery & ", CategoryID= NULL"
        'Else
        '    strQuery = strQuery & ", CategoryID= '" & Request("txtCate") & "'"
        'End If
        'strQuery = strQuery & " where UserID = '" & Userid & "'"
        ''Response.Write(strQuery)
        ''Response.End()




        'Dim cmdUp As New SqlCommand(strQuery, New SqlConnection(strConn))
        'Try
        '    cmdUp.Connection.Open()
        '    cmdUp.ExecuteNonQuery()
        'Finally
        '    If cmdUp.Connection.State = ConnectionState.Open Then
        '        cmdUp.Connection.Close()
        '    End If
        'End Try

        'strQuery = "Delete from tblUsersAcadInfo where UserID = '" & Userid & "'"
        'Dim cmdUpD1 As New SqlCommand(strQuery, New SqlConnection(strConn))
        'Try
        '    cmdUpD1.Connection.Open()
        '    cmdUpD1.ExecuteNonQuery()
        'Finally
        '    If cmdUpD1.Connection.State = ConnectionState.Open Then
        '        cmdUpD1.Connection.Close()
        '    End If
        'End Try

        'strQuery = "Delete from tblUsersProfInfo where UserID = '" & Userid & "'"
        'Dim cmdUpD2 As New SqlCommand(strQuery, New SqlConnection(strConn))
        'Try
        '    cmdUpD2.Connection.Open()
        '    cmdUpD2.ExecuteNonQuery()
        'Finally
        '    If cmdUpD2.Connection.State = ConnectionState.Open Then
        '        cmdUpD2.Connection.Close()
        '    End If
        'End Try

        'Dim i As Integer

        'ShowData()
    End Sub

End Class
