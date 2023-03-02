Imports System
Imports System.Data
Imports MainModule
Partial Class GetIssueTypes
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varQuery As String
        If Not Page.IsPostBack Then
            Dim objConn As New Data.SqlClient.SqlConnection
            Try
                objConn = objMainModule.OpenConnection(objConn)

                Dim varCategoryID As String
                Dim varString As String
                Dim varIssueID As String
                varString = ""
                varCategoryID = Request.QueryString("Cate")
                varIssueID = Request.QueryString("IssueID")
                If varIssueID = "" Then
                    varQuery = "SELECT IssueName,Description FROM dbo.tblIssueType WHERE CategoryID='" & Replace(varCategoryID, "'", "") & "' AND IsDeleted IS NULL "

                    Dim oCommand As New Data.SqlClient.SqlCommand(varQuery, objConn)
                    Dim Reader As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()

                    If Reader.HasRows Then
                        While Reader.Read
                            varString = varString & Reader.GetString(Reader.GetOrdinal("IssueName")) & "#"
                            varString = varString & Reader.GetString(Reader.GetOrdinal("Description")) & "$"
                        End While
                    End If
                    Reader.Close()
                    Reader = Nothing
                    oCommand = Nothing
                    Response.Write(varString)
                Else
                    varQuery = "SELECT IssueName,Description,IssueID FROM dbo.tblIssueType WHERE CategoryID='" & Replace(varCategoryID, "'", "") & "' AND IsDeleted IS NULL "

                    Dim oCommand As New Data.SqlClient.SqlCommand(varQuery, objConn)
                    Dim Reader As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()

                    If Reader.HasRows Then
                        While Reader.Read
                            varString = varString & Reader.GetString(Reader.GetOrdinal("IssueName")) & "#"
                            varString = varString & Reader.GetString(Reader.GetOrdinal("Description")) & "#"
                            varString = varString & Reader.GetGuid(Reader.GetOrdinal("IssueID")).ToString & "$"
                        End While
                    End If
                    Reader.Close()
                    Reader = Nothing
                    oCommand = Nothing
                    Response.Write(varString)
                End If
            Catch ex As Exception
            Finally
                If objConn.State <> ConnectionState.Closed Then
                    objConn.Close()
                    objConn = Nothing
                End If
            End Try
        End If
    End Sub
End Class
