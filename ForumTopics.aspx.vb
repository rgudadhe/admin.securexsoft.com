Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class testets_ForumTopics
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            forumid.Value = Request("forumid").ToString
            '        If Not IsPostBack Then
            ShowData()
        End If

        'End If
    End Sub


    Sub ShowData()

        Dim RecFound As String
        RecFound = "No"
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd2 As New SqlCommand("Select * from tblForum  where Forumid ='" & Request("forumid").ToString & "'  ", New SqlConnection(strConn))
        Try
            SQLCmd2.Connection.Open()
            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
            If DRRec2.HasRows Then
                If DRRec2.Read Then
                    LblRoot.Text = "<table><tr><td><img src='images/Folder.png'> <a href='forums.aspx'>All Forums</a></td></tr><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;<img src='images/Folder_share.png'> " & DRRec2("Details").ToString & "</td></tr></table>"
                End If
            End If
            DRRec2.Close()
        Finally
            If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd2.Connection.Close()
                SQLCmd2 = Nothing
            End If
        End Try


        Dim SQLCmd1 As New SqlCommand("Select F.TopicID, F.LastDiscID, F.Topic, F.CreatedDate, F.ModifiedDate, U.FirstName + ' ' + U.LastName as AuthorName, U1.FirstName + ' ' + U1.LastName as UName  from tblForumTopic F LEFT OUTER JOIN tblUsers U ON U.UserID=F.AuthorID  LEFT OUTER JOIN tblUsers U1 ON U1.UserID=F.UserID where Forumid ='" & Request("forumid").ToString & "' order by CreatedDate Desc ", New SqlConnection(strConn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then
                While DRRec1.Read
                    Dim Row1 As New TableRow
                    Dim Cell1 As New TableCell
                    Dim Cell2 As New TableCell
                    Dim Cell3 As New TableCell
                    Dim Cell4 As New TableCell
                    Cell1.Text = "<a href='forumdiscussions.aspx?topicid=" & DRRec1("TopicID").ToString & "&forumid=" & forumid.Value & "'>" & DRRec1("Topic").ToString & "</a>"
                    Cell2.Text = DRRec1("AuthorName").ToString
                    Cell3.Text = "10"
                    Dim SQLCmd As New SqlCommand("Select F.Details,  F.ModifiedDate, U.FirstName + ' ' + U.LastName as AuthorName  from tblDiscussions F LEFT OUTER JOIN tblUsers U ON U.UserID=F.UserID   where DiscussionID = '" & DRRec1("LastDiscID").ToString & "' ", New SqlConnection(strConn))
                    SQLCmd.Connection.Open()
                    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                    If DRRec.HasRows Then
                        If DRRec.Read Then
                            Cell4.Text = Left(DRRec("Details").ToString, 30) & "<BR>" & DRRec("ModifiedDate").ToString & "<BR>" & DRRec("authorname").ToString
                        End If
                    Else
                        Cell4.Text = ""

                    End If

                    Row1.Cells.Add(Cell1)
                    Row1.Cells.Add(Cell2)
                    Row1.Cells.Add(Cell3)
                    Row1.Cells.Add(Cell4)
                    Table1.Rows.Add(Row1)



                End While

            End If
            DRRec1.Close()
        Finally
            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd1.Connection.Close()
                SQLCmd1 = Nothing
            End If
        End Try
    End Sub


    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
        Dim ConString As String
        Dim SQLString As String
        Dim RowsAfected As Integer
        Dim strTopicGuid As String = System.Guid.NewGuid().ToString()
        Dim strDiscGuid As String = System.Guid.NewGuid().ToString()
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "Insert Into tblForumTopic (ForumID, TopicID, LastDiscID, Topic, CreatedDate, AuthorID) " & _
            "Values ('" & forumid.Value & "', '" & strTopicGuid & "', '" & strDiscGuid & "', '" & TxtTopic.Text & "', '" & Now() & "', '" & Session("Userid").ToString & "')"
            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)

            oCommand.ExecuteNonQuery()

            SQLString = "Insert Into tblDiscussions (TopicID, DiscussionID, Details, ModifiedDate, UserID) " & _
                        "Values ('" & strTopicGuid & "', '" & strDiscGuid & "', '" & TxtDescr.Text & "', '" & Now() & "', '" & Session("Userid").ToString & "')"
            Dim oCommand1 As New Data.SqlClient.SqlCommand(SQLString, oConn)

            oCommand1.ExecuteNonQuery()

            SQLString = "update tblForum set LastTopicID = '" & strTopicGuid & "', Topics = Topics + 1, Posts = Posts + 1 where ForumID = '" & forumid.Value & "'"
            Dim oCommand2 As New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand2.ExecuteNonQuery()


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State = System.Data.ConnectionState.Open Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Response.Redirect("ForumDiscussions.aspx?topicID=" & strTopicGuid & "&forumid=" & forumid.Value)

    End Sub
End Class
