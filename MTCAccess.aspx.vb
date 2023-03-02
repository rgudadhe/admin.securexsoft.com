Imports Encrypass
Partial Class MTCAccess
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsUserInfo As ETS.BL.Users
        Try
            clsUserInfo = New ETS.BL.Users(Request("UserName").ToString, Request("PassWord").ToString)
            Dim redirectPage As String = Request("PageURL")
            With clsUserInfo
                If .IsExists Then
                    Session("UserID") = clsUserInfo.UserID.ToString
                    Session("clsUserInfo") = clsUserInfo
                    '***

                    Session("UserName") = .FirstName & " " & .LastName
                    Session("ContractorID") = .ContractorID
                    Session("AdminLevel") = .AdminLevel
                    Session("OwnerID") = .OwnerID
                    Session("LoginID") = .UserName
                    Session("DeptID") = .DepartmentID
                    Session("ParentID") = IIf(String.IsNullOrEmpty(.ParentID), .ContractorID, .ParentID)
                    Session("IsOwner") = .IsOwner
                    Session("WorkGroupID") = .WorkGroupID
                    Session("IntialProductionLevel") = .IntialProduction
                    If .IsOwner Then
                        Session("OwnerID") = .UserID
                    End If
                    If String.IsNullOrEmpty(.ParentID) Then
                        Session("IsContractor") = 1
                    Else
                        Session("IsContractor") = 0
                    End If


                    'Dim clsU As New ETS.BL.Users
                    'clsU.Lastlogin = Now
                    'clsU.UserID = Session("UserID")
                    ''Response.Write("Function Called")

                    'If clsU.UpdateUserDetails() = 1 Then

                    '    Response.Redirect("main.htm")
                    'Else
                    '    Response.Redirect("MTCAccess.aspx")
                    'End If
                    'clsU = Nothing
                    Dim clsU As New ETS.BL.UsersLastLogin
                    clsU._WhereString.Append(" Where userid ='" & Session("UserID") & "' ")
                    Dim DSUsers As System.Data.DataSet = clsU.getUsersLastLogin()
                    If DSUsers.Tables(0).Rows.Count > 0 Then
                        clsU.UpdateLastLogin(Session("UserID"))
                    Else
                        clsU.Lastlogin = Now
                        clsU.UserID = Session("UserID")
                        clsU.InsertUserLastLogin()
                    End If
                    clsU = Nothing
                    If redirectPage Is Nothing Then
                        Response.Redirect("main.htm")
                    Else
                        Response.Redirect(redirectPage)
                    End If

                Else
                    Response.Redirect("MTCAccess.aspx")
                End If
            End With
        Catch ex As exception
            Response.Write(ex.Message)
        Finally
            clsUserInfo = Nothing
        End Try
        'Dim ConString As String
        'Dim SQLString As String
        'Dim EPass As New EncryPass.Encry
        'Dim txtUserName As String = Request("UserName").ToString
        'Dim txtPassword As String = Request("PassWord").ToString
        'Dim userpass As String = EPass.encrypt(txtUserName.ToLower, txtPassword)
        'Session("ClientPath") = IIf(String.IsNullOrEmpty(Request("ClientPath")), "", Request("ClientPath"))
        'Session("ClientPath") = Replace(Session("ClientPath"), "\", "\\")
        'ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'Dim oConn As New Data.SqlClient.SqlConnection
        'oConn.ConnectionString = ConString
        'oConn.Open()
        'SQLString = "SELECT U.UserID, U.Username, U.FirstName, U.LastName, U.ContractorID, U.departmentid,  UL.AdminLevel, O.OwnerID, OID.IsOwner,c.ParentID " & _
        '                "FROM dbo.tblOwner O RIGHT OUTER JOIN " & _
        '                "tblContractor C ON O.OwnerID = C.OwnerID RIGHT OUTER JOIN " & _
        '                "tblUsers U LEFT OUTER JOIN " & _
        '                "tblUsersLevels UL ON U.UserID = UL.UserID ON C.ContractorID = U.ContractorID LEFT OUTER JOIN " & _
        '                "(SELECT OwnerID, 1 AS IsOwner " & _
        '                "FROM tblOwner) OID ON OID.OwnerID = U.UserID " & _
        '                "WHERE (U.Isdeleted is NULL or U.Isdeleted = 'False') and (U.UserName = '" & txtUserName & "') AND (U.Password = '" & userpass & "')"
        'Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
        'Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
        'If oRec.HasRows Then
        '    oRec.Read()
        '    Session("UserID") = oRec("UserID").ToString
        '    Session("UserName") = oRec("FirstName") & " " & oRec("LastName")
        '    Session("ContractorID") = oRec("ContractorID")
        '    Session("AdminLevel") = oRec("AdminLevel")
        '    Session("OwnerID") = oRec("OwnerID")
        '    Session("LoginID") = oRec("userName").ToString
        '    Session("DeptID") = oRec("DepartmentID").ToString
        '    If IsDBNull(oRec("IsOwner")) Then
        '        Session("IsOwner") = "False"
        '    Else
        '        Session("IsOwner") = "True"
        '        Session("OwnerID") = oRec("UserID").ToString
        '    End If
        '    If IsDBNull(oRec("ParentID")) Then
        '        Session("IsContractor") = 1
        '    Else
        '        Session("IsContractor") = 0
        '    End If
        '    Response.Redirect("main.htm")

        'Else
        '    Response.Redirect("MTCAccess.aspx")
        'End If

        'oConn.Close()
    End Sub
End Class
