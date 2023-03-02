Imports System.Data.SqlClient
Imports Encrypass
Partial Class Login_Login
    Inherits System.Web.UI.Page
    Private clsUserInfo As ETS.BL.Users
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        name.Focus()
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        clsUserInfo = New ETS.BL.Users(name.Text, password.Text)

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

                '****
                If password.Text.Length < 8 Then
                    lblMessage.Text = "Change password."
                    If (Not ClientScript.IsStartupScriptRegistered("OpenPopup")) Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "OpenPopup", "OpenModalPopup();", True)
                    End If
                    Exit Sub
                Else
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
                    Response.Write("<script language='javascript'>window.opener.history.back();</script>")
                    Response.Write("<script language='javascript'>window.opener.parent.STATUS.history.go(0);</script>")
                    'Response.End()
                    Response.Write("<script>window.close();</script>")
                    'If clsUserInfo.UpdateLastLogin() = 1 Then
                    
                End If
            Else
                lblMessage.Text = "Login Failed"
            End If
        End With
        clsUserInfo = Nothing

        'Comment by anil on 19th Aug 2011
        ''On Error GoTo err1
        'Dim ConString As String
        'Dim EPass As New EncryPass.Encry
        'Dim userpass As String
        'Dim SQLString As String
        'userpass = EPass.encrypt(name.Text.ToLower, password.Text)

        'ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'Dim oConn As New Data.SqlClient.SqlConnection
        'oConn.ConnectionString = ConString
        'oConn.Open()
        'SQLString = "SELECT U.UserID, U.username, U.FirstName, U.LastName, U.ContractorID, U.departmentid, UL.AdminLevel, O.OwnerID, OID.IsOwner,c.ParentID,WGD.WorkGroupID  " & _
        '            "FROM dbo.tblOwner O RIGHT OUTER JOIN " & _
        '            "tblContractor C ON O.OwnerID = C.OwnerID RIGHT OUTER JOIN " & _
        '            "tblUsers U LEFT OUTER JOIN " & _
        '            "tblUsersLevels UL ON U.UserID = UL.UserID ON C.ContractorID = U.ContractorID LEFT OUTER JOIN " & _
        '            "tblWorkGroupDepartments as WGD on U.departmentid=WGD.departmentid  LEFT OUTER JOIN " & _
        '            "(SELECT OwnerID, 1 AS IsOwner " & _
        '            "FROM tblOwner) OID ON OID.OwnerID = U.UserID " & _
        '            "WHERE (U.Isdeleted is NULL or U.Isdeleted = 'False') and (U.UserName = '" & name.Text & "') AND (U.Password = '" & userpass & "')"
        'Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
        'Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
        'If oRec.Read Then

        '    Session("UserID") = oRec("UserID").ToString
        '    Session("UserName") = oRec("FirstName") & " " & oRec("LastName")
        '    Session("ContractorID") = oRec("ContractorID").ToString
        '    Session("AdminLevel") = oRec("AdminLevel").ToString
        '    'If oRec("AdminLevel").ToString = "2147483647" Then
        '    '    Session("SuperAdmin") = True
        '    'Else
        '    '    Session("SuperAdmin") = False
        '    'End If
        '    Session("OwnerID") = oRec("OwnerID").ToString
        '    Session("LoginID") = oRec("userName").ToString
        '    Session("DeptID") = oRec("DepartmentID").ToString
        '    Session("ParentID") = IIf(IsDBNull(oRec("ParentID")), oRec("ContractorID"), oRec("ParentID"))
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

        '    'Response.Write("<script language='javascript'>window.opener.history.go(-2); </script>")
        '    Response.Write("<script language='javascript'>window.opener.history.back();</script>")
        '    Response.Write("<script language='javascript'>window.opener.parent.STATUS.history.go(0);</script>")
        '    'Response.End()
        '    Response.Write("<script>window.close();</script>")

        'Else
        'lblMessage.Text = "Login Failed"
        'End If

        'oConn.Close()
        ''err1:
        ''If Err.Number > 0 Then
        ''Response.Write(Err.Description)
        ''End If
    End Sub
End Class

