Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class Uprofile
    Inherits BasePage
    Private clsUserInfo As ETS.BL.Users
    Protected Function IsUsrEmailExist(ByVal emailID As String, ByVal UserID As String) As Boolean
        Dim clsUsr As ETS.BL.Users
        Dim DSExistUser As New Data.DataSet
        Dim varReturn As Boolean = False
        Try
            clsUsr = New ETS.BL.Users
            'clsUsr.ContractorID = IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString)
            clsUsr.OfficialMailID = emailID.ToString

            DSExistUser = clsUsr.getUsersList
            If DSExistUser.Tables.Count > 0 Then
                If DSExistUser.Tables(0).Rows.Count > 0 Then
                    For Each DR As DataRow In DSExistUser.Tables(0).Rows
                        If DR("UserID").ToString <> UserID Then
                            varReturn = True
                        End If
                    Next

                End If
            End If

        Catch ex As exception
        Finally
            DSExistUser.Dispose()
            clsUsr = Nothing
        End Try
        Return varReturn
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strURL As String
        strURL = "userphoto.aspx?UserID=" & Session("UserID").ToString

        Image1.ImageUrl = strURL
        ShowData()
        If Not IsPostBack Then
            TxtFirstName.Enabled = False
            TxtLastName.Enabled = False
            TxtEmail.Enabled = False
            TxtDOB.Enabled = False
            TxtJoin.Enabled = False
            TxtNonOEmail.Enabled = False
            TxtChatID.Enabled = False
            TxtAdd.Enabled = False
            TxtCity.Enabled = False
            TxtState.Enabled = False
            TxtCountry.Enabled = False
            TxtpAdd.Enabled = False
            TxtpCity.Enabled = False
            TxtpState.Enabled = False
            TxtpCountry.Enabled = False
            TxtCell.Enabled = False
            TxtTel.Enabled = False
            TxtDept.Enabled = False
            TxtDesn.Enabled = False
            TxtCategory.Enabled = False
            Button2.Visible = True
            btnSubmit.Visible = False
            'Response.Write(Request("DropDownList1"))
            'Response.End()


        End If

    End Sub


    Sub ShowData()


        Image2.Visible = False
        TxtFirstName.Enabled = False
        TxtLastName.Enabled = False
        TxtEmail.Enabled = False
        TxtDOB.Enabled = False
        TxtJoin.Enabled = False
        TxtNonOEmail.Enabled = False
        TxtChatID.Enabled = False
        TxtAdd.Enabled = False
        TxtCity.Enabled = False
        TxtState.Enabled = False
        TxtCountry.Enabled = False
        TxtpAdd.Enabled = False
        TxtpCity.Enabled = False
        TxtpState.Enabled = False
        TxtpCountry.Enabled = False
        TxtCell.Enabled = False
        TxtTel.Enabled = False
        TxtDept.Enabled = False
        TxtDesn.Enabled = False
        TxtCategory.Enabled = False

        clsUserInfo = CType(Session("clsUserInfo"), ETS.BL.Users)
       
        With clsUserInfo
            LblName.Text = UCase(.FirstName & " " & .LastName)
            TxtFirstName.Text = UCase(.FirstName)
            TxtLastName.Text = UCase(.LastName)
            TxtEmail.Text = UCase(.OfficialMailID)
            TxtNonOEmail.Text = UCase(.OtherMailID)
            TxtChatID.Text = UCase(.ChatID)
            TxtAdd.Text = .Address
            TxtCity.Text = .City
            TxtState.Text = .State
            TxtCountry.Text = Trim(.Country)
            TxtpAdd.Text = .pAddress
            TxtpCity.Text = .pCity
            TxtpState.Text = .pState
            TxtpCountry.Text = Trim(.pCountry)
            If Not IsDate(.DateOfBirth) Then
                TxtDOB.Text = ""
            Else
                TxtDOB.Text = FormatDateTime(.DateOfBirth, DateFormat.ShortDate)
            End If
            If Not IsDate(.DateJoined) Then
                TxtJoin.Text = ""
            Else
                TxtJoin.Text = FormatDateTime(.DateJoined, DateFormat.ShortDate)
            End If

            TxtCell.Text = .CellNo
            TxtTel.Text = .PhoneNo
            Dim clsDep As New ETS.BL.Department(.DepartmentID)
            TxtDept.Text = clsDep.Name
            clsDep = Nothing
            Dim clsDes As New ETS.BL.Designations(.DesignationID)
            TxtDesn.Text = clsDes.Name
            clsDes = Nothing
            Dim clsCat As New ETS.BL.UserCategories(.CategoryID)
            TxtCategory.Text = clsCat.Name
            clsCat = Nothing
        End With

        clsUserInfo = Nothing
        


    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Image2.Visible = True
        Image2.Attributes.Add("onclick", "popupPage()")
        TxtNonOEmail.Enabled = True
        TxtEmail.Enabled = True
        TxtChatID.Enabled = True
        TxtAdd.Enabled = True
        TxtCity.Enabled = True
        TxtState.Enabled = True
        TxtCountry.Enabled = True
        TxtpAdd.Enabled = True
        TxtpCity.Enabled = True
        TxtpState.Enabled = True
        TxtpCountry.Enabled = True
        TxtCell.Enabled = True
        TxtTel.Enabled = True
        Button2.Visible = False
        btnSubmit.Visible = True
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If IsUsrEmailExist(Request("TxtEmail").ToString, Session("UserID")) Then
            Response.Write("Email ID is already in use")
            Button2_Click(sender, e)
            TxtEmail.Text = ""
            Exit Sub
        End If
        Dim clsU As New ETS.BL.Users
        With clsU


            .UserID = Session("UserID")
            .OtherMailID = Request("TxtNonOEmail")
            .OfficialMailID = Request("TxtEmail")
            .ChatID = Request("TxtChatID")
            .Address = Request("TxtAdd")
            .City = Request("TxtCity")
            .State = Request("TxtState")
            .Country = Request("TxtCountry")
            .CellNo = Request("TxtCell")
            .PhoneNo = Request("TxtTel")
            .PAddress = Request("TxtpAdd")
            .PCity = Request("TxtpCity")
            .PState = Request("TxtpState")
            .PCountry = Request("TxtpCountry")
            If .UpdateUserDetails() = 1 Then
                Session("clsUserInfo") = New ETS.BL.Users(Session("UserID").ToString)
                ShowData()
            Else
                Response.Write("Failed")
            End If
        End With
        clsU = Nothing

        TxtFirstName.Enabled = False
        TxtLastName.Enabled = False
        TxtEmail.Enabled = False
        TxtDOB.Enabled = False
        TxtJoin.Enabled = False
        TxtNonOEmail.Enabled = False
        TxtChatID.Enabled = False
        TxtAdd.Enabled = False
        TxtCity.Enabled = False
        TxtState.Enabled = False
        TxtCountry.Enabled = False
        TxtpAdd.Enabled = False
        TxtpCity.Enabled = False
        TxtpState.Enabled = False
        TxtpCountry.Enabled = False
        TxtCell.Enabled = False
        TxtTel.Enabled = False
        TxtDept.Enabled = False
        TxtDesn.Enabled = False
        TxtCategory.Enabled = False
        btnSubmit.Visible = False
        Button2.Visible = True
        Image2.Visible = False

    End Sub
End Class
