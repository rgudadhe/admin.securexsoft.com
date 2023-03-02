Imports System.Data.SqlClient
Imports Encrypass
Partial Class Login
    Inherits System.Web.UI.Page
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        'Response.End()
        LoginCheck(username.Text, Password.Text, True)
    End Sub
    Protected Sub LoginCheck(ByVal xusername As String, ByVal xpassword As String, ByVal flag As Boolean)
        'Response.End()
        'Response.Write(xusername)
        'Response.Write(xpassword)
        'Response.End()
        Try
            Dim strConn As String
            Dim Query As String
            'Dim xusername As String
            Dim AccessLevel As String
            Dim SQLSTR As String
            Dim userpass As String
            Dim EPass As New EncryPass.Encry
            If flag Then
                userpass = EPass.encrypt(xusername, xpassword)
            Else
                userpass = xpassword
            End If
            'Response.Write(userpass)
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            'Response.Write(strConn)
            'xusername = username.Text
            ' SQLSTR = "select U.UserID, U.Password, U.First, U.Last, U.username, A.foldername as 'FolderName', U.password, A.description, U.AccID, U.EmailAddress, U.AccessLevel, A.Mode, U.LocCode from SecureWeb.dbo.tblUsers U, dbo.tblaccounts A where A.AccountID=U.AccID and U.username = '" & xusername & "'"
            'SQLSTR = "select U.UserID, U.Password, U.First, U.Last, U.username, A.foldername as 'FolderName', U.password, A.description, U.AccID, U.EmailAddress, U.AccessLevel, A.mode, U.LocCode, A.VFolder, U.WordEditor, IsNull(U.NRStatus, 'D') as NRStatus, IsNull(U.NRPeriod, 30)  as NRPeriod, (Select Top 1 isnull(DocPassword, 'docpa55') as docpassword from dbo.tblaccdocpass where AccountID = A.AccountID order by effdate desc ) as DocPassword from SecureWeb.dbo.tblUsers U LEFT JOIN dbo.tblaccounts A ON  A.AccountID=U.AccID   LEFT JOIN dbo.tblGrpAccounts A1 ON  A1.GrpActID=U.AccID where U.username = '" & xusername & "'"
            SQLSTR = "select U.UserID, U.Password, U.First, U.Last, U.username, A.foldername as 'FolderName', U.password, A.description, U.AccID, U.EmailAddress, U.AccessLevel, A.mode, U.LocCode, A.VFolder, U.WordEditor, IsNull(U.NRStatus, 'D') as NRStatus, IsNull(U.NRPeriod, 30)  as NRPeriod, A.contractorID from SecureWeb.dbo.tblUsers U LEFT JOIN dbo.tblaccounts A ON  A.AccountID=U.AccID LEFT JOIN dbo.tblGrpAccounts A1 ON  A1.GrpActID=U.AccID where U.username = '" & xusername & "'"
            'Response.Write(SQLSTR)
            Dim SQLCmd As New SqlCommand(SQLSTR, New SqlConnection(strConn))
            Try
                SQLCmd.Connection.Open()
                Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                If DRRec.Read = True Then
                    'Response.Write(userpass & " | " & DRRec("Password"))
                    '        Response.End()

                    If userpass = DRRec("Password").ToString Then
                        AccessLevel = DRRec("AccessLevel").ToString
                        Session("UserID") = DRRec("UserID").ToString
                        'Session("DocPassword") = DRRec("DocPassword").ToString
                        Session("userLogin") = DRRec("username").ToString
                        Session("username") = DRRec("FolderName").ToString
                        Session("FolderName") = DRRec("FolderName").ToString
                        Session("LocCode") = DRRec("LocCode").ToString
                        Session("NRStatus") = DRRec("NRStatus").ToString
                        Session("ContractorID") = DRRec("ContractorID").ToString
                        'Response.Write(DRRec("NRPeriod").ToString)
                        'Response.End()
                        If IsDBNull(DRRec("NRPeriod")) Then
                            Session("NRPeriod") = 30
                        ElseIf Trim(DRRec("NRPeriod").ToString) = "" Then
                            Session("NRPeriod") = 30
                        Else
                            Session("NRPeriod") = DRRec("NRPeriod").ToString
                        End If
                        'Session("NRPeriod") = 30

                        If DRRec("WordEditor").ToString = "TX" Then
                            Session("WordEditor") = "TX"
                        Else
                            Session("WordEditor") = "MSWord"
                        End If
                        Session("Mode") = DRRec("Mode").ToString
                        If DRRec("VFolder").ToString = "True" Then
                            Session("VFolder") = True
                        Else
                            Session("VFolder") = False
                        End If
                        Session("CompanyName") = DRRec("description").ToString
                        Session("EMailAddress") = DRRec("EmailAddress").ToString
                        Session("AccID") = DRRec("AccID").ToString
                        Session("blnValidUser") = True
                        Session("uname") = DRRec("first") & " " & DRRec("last")
                        If AccessLevel = "8888" Then
                            Session("AccessLevel") = "MasterAdmin"
                            Session("MasterAdmin") = "True"
                            Response.Redirect("MAmain.htm")
                            Exit Sub
                        End If
                        If AccessLevel = "9999" Then
                            Session("AccessLevel") = "SuperAdmin"
                            Session("SuperAdmin") = "True"
                            Response.Redirect("SAmain.htm")
                            Exit Sub
                        End If
                        If AccessLevel = "777" Then
                            Session("Admin") = "True"
                        Else
                            Session("Admin") = "False"
                        End If

                        Dim Aname As String
                        Dim i As Integer
                        Query = "SELECT AccessName from SecureWeb.dbo.tblUsersAccess"
                        Dim SQLCmdA As New SqlCommand(Query, New SqlConnection(strConn))
                        Try
                            SQLCmdA.Connection.Open()
                            Dim DRRecA As SqlDataReader = SQLCmdA.ExecuteReader()
                            If DRRecA.HasRows = True Then

                                While (DRRecA.Read)
                                    Aname = DRRecA("AccessName").ToString
                                    Session(Aname) = "False"
                                End While

                            End If
                            DRRecA.Close()
                        Finally
                            If SQLCmdA.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmdA.Connection.Close()
                            End If
                        End Try


                        If AccessLevel = "666" Then
                            Session("AccessLevel") = "User"
                            Query = "SELECT A.AccessName from SecureWeb.dbo.tblUsersAccess A, SecureWeb.dbo.tblUsersAssignment B where A.AccessID=B.AccessID and B.UserID='" & Session("UserID").ToString & "'"
                            Dim SQLCmd1 As New SqlCommand(Query, New SqlConnection(strConn))
                            Try
                                SQLCmd1.Connection.Open()
                                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                                If DRRec1.HasRows = True Then
                                    i = 0
                                    While (DRRec1.Read)
                                        i = i + 1
                                        Aname = DRRec1("AccessName").ToString
                                        Session(Aname) = "True"
                                    End While
                                    Session("MenuItem") = i
                                End If
                                DRRec1.Close()
                            Finally
                                If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                                    SQLCmd1.Connection.Close()
                                End If
                            End Try
                        ElseIf AccessLevel = "777" Then
                            Session("AccessLevel") = "Admin"
                            Query = "SELECT AccessName from SecureWeb.dbo.tblUsersAccess"
                            Dim SQLCmd1 As New SqlCommand(Query, New SqlConnection(strConn))
                            Try
                                SQLCmd1.Connection.Open()
                                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                                If DRRec1.HasRows = True Then
                                    i = 0
                                    While (DRRec1.Read)
                                        i = i + 1
                                        Aname = DRRec1("AccessName").ToString
                                        Session(Aname) = "True"
                                    End While
                                    Session("MenuItem") = i
                                End If
                                DRRec1.Close()
                            Finally
                                If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                                    SQLCmd1.Connection.Close()
                                End If
                            End Try
                        End If
                        'response.end
                        Response.Redirect("mainmenu.htm")
                    End If
                End If
                DRRec.Close()
            Finally
                If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd.Connection.Close()
                End If
            End Try
        Catch ex As Exception
            Response.Write("Error : " & ex.Message & " " & "Please contact E-Dictate Customer Support for more details.")
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim varStrUserID As String = String.Empty
            Dim varStrPwd As String = String.Empty

            varStrUserID = Trim(Request.Form("hdnUserId"))
            varStrPwd = Trim(Request.Form("hdnPwd"))

            If Not String.IsNullOrEmpty(varStrUserID) And Not String.IsNullOrEmpty(varStrPwd) Then
                LoginCheck(varStrUserID, varStrPwd, False)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
