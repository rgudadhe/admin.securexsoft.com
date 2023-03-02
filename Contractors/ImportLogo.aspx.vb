Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.Office.Core
Imports System.Runtime.InteropServices.Marshal
Imports system.IO
Imports System.Data.OleDb



Partial Class Login_CreateUser
    Inherits System.Web.UI.Page

    Protected myRepeater As Repeater
    Protected IDList As ArrayList = New ArrayList
    Protected DisplayChanged As Boolean = False
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim filename1 As String
        Dim filename2 As String
        Dim Uplfilename As String

        Try
            If Not FileUpload2.HasFile And Not FileUpload1.HasFile Then
                Label2.Text = "You have not specified a file."
            End If
            If FileUpload1.HasFile And FileUpload2.HasFile Then

                If Not System.IO.Directory.Exists("C:\Uploads") Then
                    System.IO.Directory.CreateDirectory("C:\Uploads")
                End If
                Uplfilename = FileUpload1.FileName

                If File.Exists("C:\Uploads\" & FileUpload1.FileName) Then
                    File.Delete("C:\Uploads\" & FileUpload1.FileName)
                End If

                FileUpload1.SaveAs("C:\Uploads\" & _
                   FileUpload1.FileName)

                Label2.Text = "File name: " & _
                   FileUpload1.PostedFile.FileName & "<br>" & _
                   "File Size: " & _
                   FileUpload1.PostedFile.ContentLength & " kb<br>" & _
                   "Content type: " & _
                   FileUpload1.PostedFile.ContentType
                filename1 = "C:\Uploads\" & FileUpload1.FileName
                If File.Exists(filename1) Then
                    Uplfilename = FileUpload2.FileName
                    If File.Exists("C:\Uploads\" & FileUpload2.FileName) Then
                        File.Delete("C:\Uploads\" & FileUpload2.FileName)
                    End If
                    FileUpload2.SaveAs("C:\Uploads\" & _
                       FileUpload2.FileName)
                    Label3.Text = "File name: " & _
                       FileUpload2.PostedFile.FileName & "<br>" & _
                       "File Size: " & _
                       FileUpload2.PostedFile.ContentLength & " kb<br>" & _
                       "Content type: " & _
                       FileUpload2.PostedFile.ContentType
                    filename2 = "C:\Uploads\" & FileUpload2.FileName
                    If File.Exists(filename2) Then
                        Dim LID1 As String = Guid.NewGuid().ToString
                        Dim LID2 As String = Guid.NewGuid().ToString
                        Dim clsCon As New ETS.BL.ContractorLogo
                        With clsCon
                            .LogoID1 = LID1
                            .LogoID2 = LID2
                            .filename1 = filename1
                            .filename2 = filename2
                            .ContractorID = DLContractor.SelectedValue
                            .updatedate = Now
                            If .ContractorLogo_Submit(Server.MapPath("../ETS_Files/")) Then
                                Label1.Text = "Updated successfuly!"
                            Else
                                Label1.Text = "Update failed!"
                            End If
                        End With
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write(Server.MapPath("../ETS_Files"))

        If Not IsPostBack Then
            Dim clsCon As New ETS.BL.Contractor
            
            DLContractor.DataSource = clsCon.getContractorList
            DLContractor.DataTextField = "ContractorName"
            DLContractor.DataValueField = "ContractorID"
            DLContractor.DataBind()

            clsCon = Nothing

        End If
        'If Not IsPostBack Then
        '    Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        '    Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        '    Try
        '        oConn.Open()
        '        Dim SQLCmd As New SqlCommand("Select * from dbo.tblcontractor", oConn)

        '        Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
        '        If DRRec.HasRows = True Then
        '            While DRRec.Read
        '                Dim LI As New ListItem
        '                LI.Text = DRRec("ContractorName").ToString
        '                LI.Value = DRRec("ContractorID").ToString
        '                DLContractor.Items.Add(LI)
        '            End While
        '        End If
        '        DRRec.Close()

        '    Catch ex As Exception
        '        Response.Write(ex.Message)
        '    Finally
        '        If oConn.State <> Data.ConnectionState.Open Then
        '            oConn.Close()
        '            oConn = Nothing
        '        End If
        '    End Try
        'End If
    End Sub
End Class

