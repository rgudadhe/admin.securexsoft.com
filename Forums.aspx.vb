Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class testets_Forums
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '        If Not IsPostBack Then
        ShowData()
        'End If
    End Sub


    Sub ShowData()

        Dim RecFound As String
        RecFound = "No"
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")



        Dim SQLCmd1 As New SqlCommand("Select F.* from tblForum F where  contractorid='" & Session("contractorid").ToString & "' and (isdeleted is null or isdeleted ='False')    order by CreatedDate Desc ", New SqlConnection(strConn))
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
                    Cell1.Text = "<a href='forumtopics.aspx?forumid=" & DRRec1("ForumID").ToString & "'>" & DRRec1("Details").ToString & "</a>"
                    Cell2.Text = DRRec1("Topics").ToString
                    Cell3.Text = DRRec1("Posts").ToString
                    Dim SQLCmd As New SqlCommand("Select F.Topic, F.CreatedDate, F.ModifiedDate, U.FirstName + ' ' + U.LastName as AuthorName, U1.FirstName + ' ' + U1.LastName as UName  from tblForumTopic F LEFT OUTER JOIN tblUsers U ON U.UserID=F.AuthorID  LEFT OUTER JOIN tblUsers U1 ON U1.UserID=F.UserID  where TopicID = '" & DRRec1("LastTopicID").ToString & "' ", New SqlConnection(strConn))
                    SQLCmd.Connection.Open()
                    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                    If DRRec.HasRows Then
                        If DRRec.Read Then
                            Cell4.Text = "<a href='Forumdiscussions.aspx?topicid=" & DRRec1("LastTopicID").ToString & "&forumid=" & DRRec1("ForumID").ToString & "'>" & Left(DRRec("Topic").ToString, 30) & "</a>" & "<BR>" & DRRec("CreatedDate").ToString & "<BR>" & DRRec("authorname").ToString
                        End If
                    Else
                        Cell4.Text = ""
                    End If
                    'Cell4.Text = DRRec1("UName").ToString
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


End Class
