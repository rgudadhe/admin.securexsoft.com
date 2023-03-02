Imports System.Data
Imports System.Data.OleDb
Partial Class FaxPlus_ImportLog
    Inherits BasePage
    Dim sFileDir As String = Server.MapPath("/ETS_Files").ToString & "\"
    Dim lMaxFileSize As Long = 1000000


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub DeleteFile(ByVal strFileName As String)
        Try
            If strFileName.Trim().Length > 0 Then
                Dim fi As New IO.FileInfo(strFileName)
                If (fi.Exists) Then    'if file exists, delete it
                    fi.Delete()
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdUpload_Click(ByVal sender As System.Object, _
       ByVal e As System.EventArgs) Handles cmdUpload.Click

        'check that the file has been selected and it's a valid file
        If (Not File1.PostedFile Is Nothing) _
           And (File1.PostedFile.ContentLength > 0) Then

            'determine file name
            Dim sFileName As String = _
               System.IO.Path.GetFileName(File1.PostedFile.FileName)
            Try
                If File1.PostedFile.ContentLength <= lMaxFileSize Then
                    'save file on disk
                    If IO.File.Exists(sFileDir + sFileName) Then
                        IO.File.Delete(sFileDir + sFileName)
                    End If
                    If IO.File.Exists(sFileDir + sFileName) Then
                        DeleteFile(sFileDir + sFileName)
                    End If

                    File1.PostedFile.SaveAs(sFileDir + sFileName)
                    
                    If IO.File.Exists(sFileDir + sFileName) Then
                        If ImportXLS(sFileDir + sFileName) Then
                            DeleteFile(sFileDir + sFileName)
                        End If
                    End If
                    lblMessage.Visible = True
                    'lblMessage.Text = "File: " + sFileDir + sFileName + _
                    '   " Uploaded Successfully"
                Else    'reject file
                    lblMessage.Visible = True
                    lblMessage.Text = "File Size if Over the Limit of " + lMaxFileSize.ToString
                End If
            Catch exc As Exception    'in case of an error
                'Response.Write(exc.Message)
                lblMessage.Visible = True
                lblMessage.Text = "An Error Occured. Please Try Again! " & exc.Message
                'DeleteFile(sFileDir + sFileName)
            Finally
                If IO.File.Exists(sFileDir + sFileName) Then
                    DeleteFile(sFileDir + sFileName)
                End If
            End Try
        Else
            lblMessage.Visible = True
            lblMessage.Text = "Nothing to upload. Please Try Again!"
        End If
    End Sub
    Private Function ImportXLS(ByVal iFilePath As String) As Boolean
        Dim myConnectionString As String
        Dim myConnection As OleDbConnection
        Dim myCommand As OleDbCommand
        Dim myDataReader As OleDbDataReader
        Try
            myConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & iFilePath & ";Extended Properties=Excel 8.0;"
            myConnection = New OleDbConnection(myConnectionString)
            myConnection.Open()
            myCommand = New OleDbCommand("SELECT Recipient, Job, Sent, Pages, Status FROM [Sheet1$]", myConnection)

            myDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            myDataReader.Read()

            Dim varStrStatus As String = String.Empty

            Dim recCount, TotalRec As Integer
            If myDataReader.HasRows Then
                Dim DTSend As New Data.DataTable
                DTSend.Columns.Add(New DataColumn("FaxNumber", GetType(System.String)))
                DTSend.Columns.Add(New DataColumn("JobNumber", GetType(System.String)))
                DTSend.Columns.Add(New DataColumn("Sent", GetType(System.String)))
                DTSend.Columns.Add(New DataColumn("Pages", GetType(System.String)))
                DTSend.Columns.Add(New DataColumn("Status", GetType(System.String)))

                Do While myDataReader.Read

                    Dim DRRec As DataRow = DTSend.NewRow
                    DRRec("FaxNumber") = IIf(myDataReader("Recipient").ToString.Length = 11, Right(myDataReader("Recipient").ToString, 10), myDataReader("Recipient").ToString)
                    DRRec("JobNumber") = myDataReader("Job")
                    DRRec("Sent") = myDataReader("Sent")
                    DRRec("Pages") = myDataReader("Pages")
                    DRRec("Status") = Trim(myDataReader("Status"))

                    DTSend.Rows.Add(DRRec)
                    TotalRec = TotalRec + 1

                Loop

                If DTSend.Rows.Count > 0 Then
                    Dim clsFAX As ETS.BL.FaxPlus
                    Dim DTRecStatus As New Data.DataTable
                    Try
                        clsFAX = New ETS.BL.FaxPlus
                        DTRecStatus = clsFAX.btn_ImportLog_click(DTSend, Session("WorkGroupID"))
                        If DTRecStatus.Rows.Count > 0 Then
                            For Each DRTemp As Data.DataRow In DTRecStatus.Rows
                                If String.IsNullOrEmpty(DRTemp("RecStatus")) Then
                                    varStrStatus = DRTemp("RecStatus")
                                Else
                                    varStrStatus = varStrStatus & "<BR>" & DRTemp("RecStatus")
                                End If

                                If DRTemp("RecStatus").ToString.IndexOf("Record updated") > 0 Then
                                    recCount = recCount + 1
                                End If
                            Next
                        End If

                    Catch ex As Exception
                        Response.Write(ex.Message)
                    Finally
                        clsFAX = Nothing
                        DTRecStatus = Nothing
                    End Try
                End If
            End If

            lblMessage.Text = recCount & " records updated out of " & TotalRec & " records! <BR> " & varStrStatus
            myDataReader.Close()
            myConnection.Close()
            Return True
        Catch ex As Exception
            If myConnection.State Then
                myConnection.Close()
            End If
            Return False
        Finally
            If Not myDataReader Is Nothing Then
                myDataReader.Close()
            End If
            If Not myConnection Is Nothing Then
                myConnection.Close()
            End If
        End Try
    End Function
End Class
