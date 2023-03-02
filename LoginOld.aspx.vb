Imports Encrypass
Imports System.Data.SqlClient
Partial Class Login
    Inherits System.Web.UI.Page
    Private clsUserInfo As ETS.BL.Users
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        setSecureProtocol(True)
    End Sub

#End Region
    Public Shared Sub setSecureProtocol(ByVal bSecure As Boolean)

        Dim redirectUrl As String = Nothing
        Dim context As HttpContext = HttpContext.Current


        ' if we want HTTPS and it is currently HTTP
        If bSecure AndAlso Not context.Request.IsSecureConnection Then
            redirectUrl = context.Request.Url.ToString().Replace("http:", "https:")

            ' if we want HTTP and it is currently HTTPS
        ElseIf Not bSecure AndAlso context.Request.IsSecureConnection Then
            redirectUrl = context.Request.Url.ToString().Replace("https:", "http:")
        End If

        'else

        ' in all other cases we don't need to redirect

        ' check if we need to redirect, and if so use redirectUrl to do the job
        If redirectUrl IsNot Nothing Then
            context.Response.Redirect(redirectUrl)
        End If

    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
	If Not Request.IsSecureConnection Then
            Dim redirectUrl As String = Request.Url.ToString().Replace("http:", "https:")
            Response.Redirect(redirectUrl)
        End If
        name.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
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
                    'If clsUserInfo.UpdateLastLogin() = 1 Then
                    'Dim clsU As New ETS.BL.Users
                    'clsU.Lastlogin = Now
                    'clsU.UserID = Session("UserID")

                    'If clsU.UpdateUserDetails() = 1 Then

                    '    Response.Redirect("main.htm")
                    'Else
                    '    lblMessage.Text = "Login Failed"
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
                    Response.Redirect("main.htm")
                End If
            Else
                lblMessage.Text = "Login Failed"
            End If
        End With
        clsUserInfo = Nothing
        
    End Sub

    'Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    Try
    '        Dim strConn As String
    '        Dim Query As String
    '        Dim xusername As String
    '        'Dim AccessLevel As String
    '        Dim SQLSTR As String
    '        Dim userpass As String
    '        Dim EPass As New EncryPass.Encry
    '        xusername = name.Text.ToLower
    '        userpass = EPass.encrypt(name.Text.ToLower, password.Text)
    '        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '        'xusername = username.Text
    '        ' SQLSTR = "select U.UserID, U.Password, U.First, U.Last, U.username, A.foldername as 'FolderName', U.password, A.description, U.AccID, U.EmailAddress, U.AccessLevel, A.Mode, U.LocCode from SecureWeb.dbo.tblUsers U, DBO.tblaccounts A where A.AccountID=U.AccID and U.username = '" & xusername & "'"
    '        SQLSTR = "select U.UserID, U.Password, U.First, U.Last, U.username, A.foldername as 'FolderName', U.password, A.description, U.AccID, U.EmailAddress, U.AccessLevel, A.mode, U.LocCode, A.VFolder, U.WordEditor, IsNull(U.NRStatus, 'D') as NRStatus, IsNull(U.NRPeriod, 30)  as NRPeriod, (Select Top 1 isnull(DocPassword, 'docpa55') as docpassword from DBO.tblaccdocpass where AccountID = A.AccountID order by effdate desc ) as DocPassword from SecureWeb.dbo.tblUsers U LEFT JOIN DBO.tblaccounts A ON  A.AccountID=U.AccID   LEFT JOIN DBO.tblGrpAccounts A1 ON  A1.GrpActID=U.AccID where U.username = '" & xusername & "'"
    '        'Response.Write(SQLSTR)
    '        'Response.End()
    '        Dim SQLCmd As New SqlCommand(SQLSTR, New SqlConnection(strConn))
    '        Try
    '            SQLCmd.Connection.Open()
    '            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
    '            If DRRec.HasRows = True Then
    '                If DRRec.Read = True Then

    '                    If userpass = DRRec("Password").ToString Then
    '                        'SQLSTR = "SELECT isnull(Acs.ServiceID,'11111111-1111-1111-1111-111111111111') as Assigned,S.ServiceName, S.ServiceDesc, S.ServiceID, S.ServiceURL, S.InfoURL, S.ISActive, AcS.IsDefault FROM tblServices AS S left outer join (select ServiceID, IsDefault from tblAccountsServices WHERE AccountID = '" & DRRec("accid").ToString & "') AS AcS ON AcS.ServiceID = S.ServiceID where Acs.isDefault ='True' "
    '                        'Dim SQLCmd1 As New SqlCommand(SQLSTR, New SqlConnection(strConn))
    '                        'Try
    '                        '    SQLCmd1.Connection.Open()
    '                        '    Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
    '                        '    If DRRec1.HasRows = True Then
    '                        '        If DRRec1.Read = True Then
    '                        Response.Redirect("../physician/loginRed.aspx?xusername=" & xusername)
    '                        '            End If
    '                        '        Else
    '                        '            lblMessage.Text = "No service is assigned on your ID."
    '                        '        End If
    '                        '        DRRec1.Close()
    '                        '                Finally
    '                        '    If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
    '                        '        SQLCmd1.Connection.Close()
    '                        '    End If
    '                        'End Try
    '                    End If
    '                Else
    '                    lblMessage.Text = "Login Failed."
    '                End If
    '            Else
    '                lblMessage.Text = "Login Failed."
    '            End If
    '            DRRec.Close()
    '        Finally
    '            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
    '                SQLCmd.Connection.Close()
    '            End If
    '        End Try
    '    Catch ex As Exception
    '        lblMessage.Text = "Login Failed"
    '    End Try
    'End Sub
End Class


