Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class testets_AddTopic
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ShowData()
        End If
    End Sub


    Sub ShowData()

        Dim RecFound As String
        RecFound = "No"
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd1 As New SqlCommand("Select F.* from tblForum F where  contractorid='" & Session("contractorid").ToString & "' order by details", New SqlConnection(strConn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then
                While DRRec1.Read
                    Dim LI As New ListItem
                    LI.Text = DRRec1("Details").ToString
                    LI.Value = DRRec1("ForumID").ToString
                    DLForum.Items.Add(LI)
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
            "Values ('" & DLForum.SelectedValue & "', '" & strTopicGuid & "', '" & strDiscGuid & "', '" & TxtTopic.Text & "', '" & Now() & "', '" & Session("Userid").ToString & "')"
            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)

            oCommand.ExecuteNonQuery()

            SQLString = "Insert Into tblDiscussions (TopicID, DiscussionID, Details, ModifiedDate, UserID) " & _
                        "Values ('" & strTopicGuid & "', '" & strDiscGuid & "', '" & TxtDescr.Text & "', '" & Now() & "', '" & Session("Userid").ToString & "')"
            Dim oCommand1 As New Data.SqlClient.SqlCommand(SQLString, oConn)

            oCommand1.ExecuteNonQuery()

            SQLString = "update tblForum set LastTopicID = '" & strTopicGuid & "', Topics = Topics + 1, Posts = Posts + 1 where ForumID = '" & DLForum.SelectedValue & "'"
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
        Response.Redirect("ForumDiscussions.aspx?topicID=" & strTopicGuid & "&forumid=" & DLForum.SelectedValue)
    End Sub
End Class
