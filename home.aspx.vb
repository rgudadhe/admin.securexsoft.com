Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class ets_home
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim RecFound As String


        RecFound = "No"
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'Response.Write(strConn)

        'Dim SQLCmd As New SqlCommand("Select Top 1 Details, DateDisp, TrackID from tblCOO order by DateDisp DESC", New SqlConnection(strConn))
        'Try
        '    SQLCmd.Connection.Open()
        '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
        '    If DRRec.HasRows = True Then
        '        While (DRRec.Read)
        '            LblCOO.Text = "<span style=color: #ff3333><img src='images/flag.gif'>  <b>Posted on " & FormatDateTime(DRRec("DateDisp").ToString, DateFormat.ShortDate) & "</b></span><BR>"
        '            LblCOO.Text = LblCOO.Text & Left(Replace(DRRec("Details").ToString, vbCrLf, " "), 200)
        '            'LblCOO.Text = LblCOO.Text & "<a href='news.aspx?trackid=" & DRRec("TrackID").ToString & "'>Posted on " & FormatDateTime(DRRec("DateDisp").ToString, DateFormat.ShortDate) & "</a>"
        '            'LblCOO.Text = LblCOO.Text & "<BR>"
        '        End While
        '    End If
        '    DRRec.Close()
        'Finally
        '    If SQLCmd.Connection.State = ConnectionState.Open Then
        '        SQLCmd.Connection.Close()
        '    End If
        'End Try


        Dim clsHome As ETS.BL.HomePage
        Dim DSNews As New Data.DataSet
        Dim DRRec1 As Data.DataTableReader
        Try
            clsHome = New ETS.BL.HomePage
            DSNews = clsHome.GetNewsListByContractorID(Session("ContractorID").ToString)
            If DSNews.Tables.Count > 0 Then
                If DSNews.Tables(0).Rows.Count > 0 Then
                    DRRec1 = DSNews.Tables(0).CreateDataReader
                    If DRRec1.HasRows = True Then
                        While (DRRec1.Read)
                            LblNews.Text = LblNews.Text & "<img src='images/flag.gif'>  <a href='ednews.aspx?trackid=" & DRRec1("trackid").ToString & "'>" & DRRec1("SubText").ToString & " - " & DRRec1("uname").ToString & "</a>" & "<br />"
                        End While
                    End If
                    DRRec1.Close()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsHome = Nothing
            DRRec1 = Nothing
            DSNews = Nothing
        End Try

        'Dim SQLCmd1 As New SqlCommand("Select Top 3  N.*, U.firstname + ' ' + U.LastName as uname   from tblNews  N LEFT OUTER JOIN TBLUSERS U ON N.USERID = U.USERID order by DateDisp DESC", New SqlConnection(strConn))
        'Try
        '    SQLCmd1.Connection.Open()
        '    Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
        '    If DRRec1.HasRows = True Then
        '        While (DRRec1.Read)
        '            LblNews.Text = LblNews.Text & "<img src='images/flag.gif'>  <a href='ednews.aspx?trackid=" & DRRec1("trackid").ToString & "'>" & DRRec1("SubText").ToString & " - " & DRRec1("uname").ToString & "</a>" & "<br />"
        '        End While

        '    End If
        '    DRRec1.Close()
        'Finally
        '    If SQLCmd1.Connection.State = ConnectionState.Open Then
        '        SQLCmd1.Connection.Close()
        '    End If
        'End Try




        Dim clsHome1 As ETS.BL.HomePage
        Dim DSForums As New Data.DataSet
        Dim DRRec2 As Data.DataTableReader
        Try
            clsHome1 = New ETS.BL.HomePage
            DSForums = clsHome1.GetForumListByContractorID(Session("ContractorID").ToString)
            If DSForums.Tables.Count > 0 Then
                If DSForums.Tables(0).Rows.Count > 0 Then
                    DRRec2 = DSForums.CreateDataReader
                    If DRRec2.HasRows = True Then
                        While (DRRec2.Read)
                            LblForum.Text = LblForum.Text & "<img src='images/flag.gif'>  <a href='forumdiscussions.aspx?topicid=" & DRRec2("TopicID").ToString & "&forumid=" & DRRec2("ForumID").ToString & "'>" & DRRec2("Topic").ToString & " - " & DRRec2("AuthorName").ToString & "</a>" & "<br />"
                        End While

                    End If
                    DRRec2.Close()
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsHome1 = Nothing
            DRRec2 = Nothing
            DSForums = Nothing
        End Try

        'Dim SQLCmd2 As New SqlCommand("Select Top 5 F.ForumID, F.TopicID, F.LastDiscID, F.Topic, F.CreatedDate, F.ModifiedDate, U.FirstName + ' ' + U.LastName as AuthorName, U1.FirstName + ' ' + U1.LastName as UName  from tblForumTopic F INNER JOIN tblForum F1 ON F.ForumID = F1.ForumID LEFT OUTER JOIN tblUsers U ON U.UserID=F.AuthorID  LEFT OUTER JOIN tblUsers U1 ON U1.UserID=F.UserID where (F1.isdeleted is Null or F1.isDeleted = 'False') order by CreatedDate Desc ", New SqlConnection(strConn))
        'Try

        '    SQLCmd2.Connection.Open()
        '    Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
        '    If DRRec2.HasRows = True Then
        '        While (DRRec2.Read)
        '            LblForum.Text = LblForum.Text & "<img src='images/flag.gif'>  <a href='forumdiscussions.aspx?topicid=" & DRRec2("TopicID").ToString & "&forumid=" & DRRec2("ForumID").ToString & "'>" & DRRec2("Topic").ToString & " - " & DRRec2("AuthorName").ToString & "</a>" & "<br />"
        '        End While

        '    End If
        '    DRRec2.Close()
        'Finally
        '    If SQLCmd2.Connection.State = ConnectionState.Open Then
        '        SQLCmd2.Connection.Close()
        '    End If
        'End Try




        Dim clsHome3 As ETS.BL.HomePage
        Dim DSUpdates As New Data.DataSet
        Dim DRRec3 As Data.DataTableReader

        Try
            clsHome3 = New ETS.BL.HomePage
            DSUpdates = clsHome3.GetUpdatesListByContractorID(Session("ContractorID").ToString)
            If DSUpdates.Tables.Count > 0 Then
                If DSUpdates.Tables(0).Rows.Count > 0 Then
                    DRRec3 = DSUpdates.Tables(0).CreateDataReader
                    If DRRec3.HasRows = True Then
                        While (DRRec3.Read)
                            Lblupdate.Text = Lblupdate.Text & "<img src='images/flag.gif'>  <a href='updates.aspx?trackid=" & DRRec3("trackid").ToString & "'>" & DRRec3("SubText").ToString & " - " & DRRec3("uname").ToString & "</a>" & "<br />"
                        End While

                    End If
                    DRRec3.Close()
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsHome3 = Nothing
            DSUpdates = Nothing
            DRRec3 = Nothing
        End Try


        'Dim SQLCmd3 As New SqlCommand("Select Top 3  N.*, U.firstname + ' ' + U.LastName as uname   from tblupdates  N LEFT OUTER JOIN TBLUSERS U ON N.USERID = U.USERID order by DateDisp DESC", New SqlConnection(strConn))
        'Try
        '    SQLCmd3.Connection.Open()
        '    Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
        '    If DRRec3.HasRows = True Then
        '        While (DRRec3.Read)
        '            Lblupdate.Text = Lblupdate.Text & "<img src='images/flag.gif'>  <a href='updates.aspx?trackid=" & DRRec3("trackid").ToString & "'>" & DRRec3("SubText").ToString & " - " & DRRec3("uname").ToString & "</a>" & "<br />"
        '        End While

        '    End If
        '    DRRec3.Close()
        'Finally
        '    If SQLCmd3.Connection.State = ConnectionState.Open Then
        '        SQLCmd3.Connection.Close()
        '    End If
        'End Try





        LblNews.Text = "<i><font size='1'>" & LblNews.Text & "</font></i>"
        'LblCOO.Text = "<i><font size='2' >" & LblCOO.Text & "</font></i>"
        LblForum.Text = "<i><font size='1' >" & LblForum.Text & "</font></i>"
        Lblupdate.Text = "<i><font size='1' >" & Lblupdate.Text & "</font></i>"
    End Sub
End Class
