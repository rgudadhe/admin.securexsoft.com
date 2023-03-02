Imports TaskScheduler
Partial Class Sch
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Dim FSO
            'FSO = Server.CreateObject("Scripting.FileSystemObject")

            'Dim folder As New System.IO.DirectoryInfo("\\t-server\c$\windows\tasks")
            'Dim files
            'Dim file

            'If folder.Exists Then
            '    folder = FSO.GetFolder()

            '    files = folder.GetFiles

            '    For Each file In files
            '        If "job" = Mid(file.name, InStr(1, file.name, ".") + 1) Then
            '            Response.Write(file.Name & "&&nbsp" & file.DateLastModified & "&&nbsp" & DateDiff("n", file.DateLastModified, Now()))
            '            Response.Write("<BR>")
            '        End If
            '    Next
            'End If

            'FSO = Nothing
            'folder = Nothing
            'files = Nothing

            If Not Page.IsPostBack Then
                GetData(Request.QueryString("Server"))
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Sub GetData(ByVal ServerName As String)
        Try
            'Response.Clear()
            'Response.Write("<b><i><font face=""Trebuchet MS"" size=""2pt"" color=""Crimson"" style=""font-weight:bold"">ServerName : dbServer<BR>DateTime:" & Now & " (EST)</font></i></b><BR>")

            Dim st As ScheduledTasks = Nothing
            st = New ScheduledTasks("\\" & ServerName & "\")
            lblServer.Text = ServerName
            'lblDate.Text = Now & " (EST) "

            Dim taskNames As String() = st.GetTaskNames()
            ' Open each task, dump info to console 
            If taskNames.Length > 0 Then
                'Response.Write("<table border=1 width=100% style=""font-family:Trebuchet MS; font-size:8pt ""><tr class=""SMSelectedGrid""><td>SchedularName</td><td width=""30%"">Schedule</td><td>LastRun</td><td>NextRun</td><td>Difference</td><td>State</td><td>Exit-Code</td><td>Comment</td></tr>")
                Dim i = 0
                For Each name As String In taskNames
                    Dim t As Task = st.OpenTask(name)
                    Dim varLstRun As String = String.Empty
                    Dim varNxtRun As String = String.Empty
                    Dim varState As String = String.Empty
                    Dim varExit As String = String.Empty
                    Dim varComment As String = String.Empty
                    Dim varStrColor As String = String.Empty
                    Dim varBolDiff As Boolean = False

                    Dim vartblRow As New TableRow
                    Dim vartblCell1 As New TableCell
                    Dim vartblCell2 As New TableCell
                    Dim vartblCell3 As New TableCell
                    Dim vartblCell4 As New TableCell
                    Dim vartblCell5 As New TableCell
                    Dim vartblCell6 As New TableCell
                    Dim vartblCell7 As New TableCell
                    Dim vartblCell8 As New TableCell

                    varLstRun = t.MostRecentRunTime.ToString
                    varNxtRun = t.NextRunTime.ToString
                    varState = t.Status.ToString()

                    Dim varDt As Date
                    varDt = varLstRun

                    Dim varDt1 As Date
                    varDt1 = varNxtRun

                    Dim Diff
                    If Trim(UCase(varState)) <> Trim(UCase("Disabled")) Then
                        Diff = DateDiff(DateInterval.Minute, varDt, varDt1)
                        varBolDiff = True
                    End If

                    Try
                        varExit = t.ExitCode
                    Catch ex As Exception
                        'Exception for schedular
                        varExit = ex.Message
                    End Try

                    If Not String.IsNullOrEmpty(t.Comment.ToString) Then
                        varComment = t.Comment.ToString
                    Else
                        varComment = "&nbsp"
                    End If



                    vartblCell1.Text = t.Name.ToString
                    vartblCell2.Text = t.Triggers(0).ToString()
                    vartblCell3.Text = varLstRun
                    vartblCell4.Text = varNxtRun
                    vartblCell5.Text = IIf(varBolDiff, Diff, "&nbsp")
                    vartblCell6.Text = varState
                    vartblCell7.Text = varExit
                    vartblCell8.Text = varComment

                    vartblRow.Cells.Add(vartblCell1)
                    vartblRow.Cells.Add(vartblCell2)
                    vartblRow.Cells.Add(vartblCell3)
                    vartblRow.Cells.Add(vartblCell4)
                    vartblRow.Cells.Add(vartblCell5)
                    vartblRow.Cells.Add(vartblCell6)
                    vartblRow.Cells.Add(vartblCell7)
                    vartblRow.Cells.Add(vartblCell8)


                    If Trim(UCase(varState)) <> Trim(UCase("Ready")) And Trim(UCase(varState)) <> Trim(UCase("Running")) Then
                        vartblRow.ForeColor = Drawing.Color.Chocolate
                    ElseIf Trim(UCase(varState)) = Trim(UCase("Running")) Then
                        vartblRow.ForeColor = Drawing.Color.Green
                    End If

                    If varExit <> 0 And varExit <> 1 Then
                        vartblRow.ForeColor = Drawing.Color.Red
                    End If

                    If i Mod 2 = 0 Then
                        vartblRow.BackColor = Drawing.Color.WhiteSmoke
                        'Response.Write("<tr style=""background-color:#F7F6F3 " & varStrColor & """><td>" & t.Name & "</td><td>" & t.Triggers(0).ToString() & "</td><td>" & varLstRun & "</td><td>" & varNxtRun & "</td><td>" & IIf(varBolDiff, Diff, "&nbsp") & "</td><td>" & varState & "</td><td>" & varExit & "</td><td>" & varComment & "</td></tr>")
                    Else
                        'Response.Write("<tr style=""background-color:white " & varStrColor & """><td>" & t.Name & "</td><td>" & t.Triggers(0).ToString() & "</td><td>" & varLstRun & "</td><td>" & varNxtRun & "</td><td>" & IIf(varBolDiff, Diff, "&nbsp") & "</td><td>" & varState & "</td><td>" & varExit & "</td><td>" & varComment & "</td></tr>")
                        vartblRow.BackColor = Drawing.Color.White
                    End If

                    tblSch.Rows.Add(vartblRow)

                    t.Close()
                    i = i + 1
                Next
            End If

            st.Dispose()
            st = Nothing
            'Response.Write("</table>")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub UpdateTimer_Tick(ByVal sender As Object, ByVal e As EventArgs)
        'DateStampLabel.Text = DateTime.Now.ToString()
        Try
            GetData(Request.QueryString("Server"))
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
