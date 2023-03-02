Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class testets_ForumDiscussions
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '        If Not IsPostBack Then

        If Not IsPostBack Then
            forumid.Value = Request("forumid").ToString
            HTopicID.Value = Request("TopicID").ToString
            ShowData()
        End If

        'End If
    End Sub


    Sub ShowData()

        Dim RecFound As String
        RecFound = "No"
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

  

        Dim SQLCmd3 As New SqlCommand("Select T.Topic, F.Details, T.topicID, F.ForumID from tblForumTopic T LEFT OUTER JOIN tblForum F ON F.ForumID = T.ForumID  where T.Forumid ='" & Request("forumid").ToString & "' AND T.topicid = '" & Request("TopicID").ToString & "' ", New SqlConnection(strConn))
        Try
            SQLCmd3.Connection.Open()
            Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
            If DRRec3.HasRows Then
                If DRRec3.Read Then
                    LblRoot.Text = "<table><tr><td Colspan=2><img src='images/Folder.png'> <a href='forums.aspx'>All Forums</a></td></tr><tr><td Colspan=2>&nbsp;&nbsp;&nbsp;&nbsp;<img src='images/Folder.png'> <a href='forumtopics.aspx?forumid=" & DRRec3("ForumID").ToString & "'>" & DRRec3("Details").ToString & "</a></td></tr><tr><td Colspan=2>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src='images/Folder_share.png'> " & DRRec3("Topic").ToString & "</td></tr></table>"
                End If
            End If
            DRRec3.Close()
        Finally
            If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd3.Connection.Close()
                SQLCmd3 = Nothing
            End If
        End Try

        Label1.Text = "<table width=100% cellPadding=2 cellspacing=2 style='font-family:Trebuchet MS;font-size:Small;width:100%;'><tr style='color:#FF8000;'><td vAlign='top' align='middle' width='15%'>Author</td><td vAlign='top' align='middle' width='85%'>Topic</td></tr>"
        Dim SQLCmd1 As New SqlCommand("Select F.*, U.FirstName + ' ' + U.LastName as AuthorName from tblDiscussions F LEFT OUTER JOIN tblUsers U ON U.UserID=F.UserID where topicid ='" & Request("topicid").ToString & "' and SubDiscID is NULL order by ModifiedDate Desc ", New SqlConnection(strConn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then
                While DRRec1.Read
                    Label1.Text = Label1.Text & "<tr><td vAlign='top' align='middle' width='10%'><table border='2' cellpadding='2' style='margin-top:0px; margin-left:0px;'><tr><td style='background-color: #336699'><span id='LblName' style='color:White;font-family:Trebuchet MS;font-size:x-Small;'>" & DRRec1("AuthorName").ToString & "</span></td></tr><tr><td style='background-color: gainsboro;' ><img id='Image1' src='userphoto.aspx?UserID=" & DRRec1("UserID").ToString & "' style='border-width:2px;border-style:Groove;height:70px;width:70px;' /></td> </tr></table>"
                    Label1.Text = Label1.Text & "</td><td vAlign='top' width='90%'><h2><span style='color: #ff9933; font-size: 10pt;'><em>" & DRRec1("Modifieddate").ToString & "</em></span></h2><p class='post-by'>Posted By <a href='profile.aspx?userid=" & DRRec1("userid").ToString & "' target=_blank>" & DRRec1("AuthorName").ToString & "</a></p><p>" & DRRec1("Details").ToString & "</p><br /></td></tr>"
                    Dim SQLCmd2 As New SqlCommand("Select F.*, U.FirstName + ' ' + U.LastName as AuthorName from tblDiscussions F LEFT OUTER JOIN tblUsers U ON U.UserID=F.UserID where SubDiscID ='" & DRRec1("discussionid").ToString & "' order by ModifiedDate Desc ", New SqlConnection(strConn))
                    SQLCmd2.Connection.Open()
                    Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                    If DRRec2.HasRows Then
                        While DRRec2.Read
                            Label1.Text = Label1.Text & "<tr><td vAlign='top' align='middle' width='15%'><img id='Image2' src='images/reply.jpg'><br><br><table border='2' cellpadding='2' style='margin-top:0px; margin-left:5%;'><tr><td style='background-color: #336699'><span id='LblName' style='color:White;font-family:Trebuchet MS;font-size:x-Small;font-weight:bold;'>" & DRRec2("AuthorName").ToString & "</span></td></tr><tr><td style='background-color: gainsboro;' ><img id='Image1' src='userphoto.aspx?UserID=" & DRRec2("UserID").ToString & "' style='border-width:2px;border-style:Groove;height:70px;width:70px;' /></td> </tr></table>"
                            Label1.Text = Label1.Text & "</td><td vAlign='top' width='85%'><h2><span style='color: #ff9933; font-size: 10pt;'><em>" & DRRec2("Modifieddate").ToString & "</em></span></h2><p class='post-by'>Posted By <a href='profile.aspx?userid=" & DRRec2("userid").ToString & "' target=_blank>" & DRRec2("AuthorName").ToString & "</a></p><p>" & DRRec2("Details").ToString & "</p><br /></td></tr>"

                        End While

                    End If
                    DRRec2.Close()
                    SQLCmd2.Connection.Close()
                    Label1.Text = Label1.Text & "<tr><td colspan=2 style='text-align:right;'><a href='addpost.aspx?topicid=" & DRRec1("topicid").ToString & "&discussionid=" & DRRec1("discussionid").ToString & "' class='button'>Reply to this post</a></td></tr>"
                    Label1.Text = Label1.Text & "<tr><td colspan=2><h1></h1></td></tr>"
                End While

            End If
            DRRec1.Close()
        Finally
            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd1.Connection.Close()
                SQLCmd1 = Nothing
            End If
        End Try
        Label1.Text = Label1.Text & "</table>"
    End Sub


    Protected Sub Submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit.Click
        Dim ConString As String
        Dim SQLString As String

        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim strDiscGuid As String = System.Guid.NewGuid().ToString()
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "Insert Into tblDiscussions (DiscussionID, TopicID,Details, ModifiedDate, UserID) " & _
            "Values ('" & strDiscGuid & "',  '" & HTopicID.Value & "', '" & TxtDescr.Text & "', '" & Now() & "', '" & Session("Userid").ToString & "')"
            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand.ExecuteNonQuery()
            SQLString = "update tblForum set Posts = Posts + 1 where ForumID = '" & forumid.Value & "'"
            Dim oCommand2 As New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand2.ExecuteNonQuery()

            SQLString = "update tblForumTopic set LastDiscID = '" & strDiscGuid & "' where TopicID = '" & HTopicID.Value & "'"
            Dim oCommand3 As New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand3.ExecuteNonQuery()

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State = System.Data.ConnectionState.Open Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try

        ShowData()
    End Sub

End Class
