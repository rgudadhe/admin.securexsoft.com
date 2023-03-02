Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.Office.Core
Imports System.Runtime.InteropServices.Marshal
Imports system.IO
Imports System.Data.OleDb



Partial Class Login_CreateUser
    Inherits BasePage

    Protected myRepeater As Repeater
    Protected IDList As ArrayList = New ArrayList
    Protected DisplayChanged As Boolean = False
    Protected SrNo As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub


    Public Function InsertRec(ByVal JobNumber As String, ByVal UserLevel As String, ByVal Lines As String, ByVal PostDate As String) As Boolean
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            strQuery = "INSERT INTO AdminETS.dbo.tblBillingPlatLines (PlatActID,JobNumber, UserLevel, Lines, BLines,  PostDate, updatedate) VALUES "
            strQuery = strQuery & " ('0E8D4B01-188A-4047-8816-91406C6FC275','" & JobNumber & "' ,'" & UserLevel & "' ,'" & Lines & "' ,'" & Lines & "','" & PostDate & "', '" & Now() & "')"
            'Response.Write(strQuery)

            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
            SQLCmd3.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Return False
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function

    Public Function GetSXFID(ByVal ID As String, ByVal Level As String) As String
        Dim strConn As String
        Dim strQuery As String
        Dim RetID As String = String.Empty
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            strQuery = "SELECT [UserName] FROM [ADMINETS].[dbo].[tblUsersIDMappings] "
            If Level = "MTID" Then
                strQuery = strQuery & " WHERE MTID='" & ID & "'"
            ElseIf Level = "QAID" Then
                strQuery = strQuery & " WHERE QAID='" & ID & "'"
            ElseIf Level = "QABID" Then
                strQuery = strQuery & " WHERE QABID='" & ID & "'"
            End If
            Dim SQLCmd2 As New SqlCommand(strQuery, oConn)
            'Response.Write(strQuery)
            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
            If DRRec2.HasRows Then
                If DRRec2.Read Then
                    RetID = DRRec2("UserName").ToString
                End If
            End If
            Return RetID
        Catch ex As Exception
            Return Nothing
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try

    End Function

    Protected Sub ShowDetails(ByVal JobNumber As String, ByVal UserLevel As String, ByVal Lines As String, ByVal PostDate As String, ByVal Status As String)
        Dim row1 As New TableRow
        Dim Cell1 As New TableCell
        Dim Cell2 As New TableCell
        Dim Cell3 As New TableCell
        Dim Cell4 As New TableCell
        Dim Cell5 As New TableCell
        Dim Cell6 As New TableCell
        Dim Cell7 As New TableCell
        Dim Cell8 As New TableCell
        Dim Cell9 As New TableCell

        Cell2.Text = JobNumber
        Cell3.Text = UserLevel
        Cell4.Text = Lines

        Cell7.Text = PostDate
        SrNo = SrNo + 1
        Cell8.Text = SrNo
        Cell9.Text = Status
        row1.Cells.Add(Cell8)

        row1.Cells.Add(Cell2)
        row1.Cells.Add(Cell3)
        row1.Cells.Add(Cell4)

        row1.Cells.Add(Cell7)
        row1.Cells.Add(Cell9)

        TblDetails.Rows.Add(row1)


    End Sub
    Public Function GetExcelSheetNames(ByVal excelFileName As String) As String()
        Dim con As OleDbConnection = Nothing
        Dim dt As DataTable = Nothing
        '  excelFileName = "D:\SecureWeb\Demo"
        Dim conStr As String = ("Provider=Microsoft.Jet.OLEDB.4.0;" & "Data Source=") + excelFileName & ";Extended Properties=Excel 8.0;"
        con = New OleDbConnection(conStr)
        con.Open()
        dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        If dt Is Nothing Then
            Return Nothing
        End If
        Dim excelSheetNames As String() = New String(dt.Rows.Count - 1) {}
        Dim i As Integer = 0
        For Each row As DataRow In dt.Rows
            excelSheetNames(i) = row("TABLE_NAME").ToString()
            i += 1
        Next
        con.Close()
        Return excelSheetNames
    End Function
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        
        Dim MDPFilePath As String
        
        
        If FileUpload1.HasFile Then
            Try
                If Not System.IO.Directory.Exists("C:\Uploads") Then
                    System.IO.Directory.CreateDirectory("C:\Uploads")
                End If
                FileUpload1.SaveAs("C:\Uploads\" & _
                   FileUpload1.FileName)
                Label2.Text = "File name: " & _
                   FileUpload1.PostedFile.FileName & "<br>" & _
                   "File Size: " & _
                   FileUpload1.PostedFile.ContentLength & " kb<br>" & _
                   "Content type: " & _
                   FileUpload1.PostedFile.ContentType
            Catch ex As Exception
                Label2.Text = "ERROR: " & ex.Message.ToString()
            End Try
        Else
            Label2.Text = "You have not specified a file."
        End If

        MDPFilePath = "C:\Uploads\" & _
               FileUpload1.FileName
        Dim MDPFLPath As New FileInfo(MDPFilePath)
       
        Dim UserName As String = String.Empty
        Dim JobNumber As String = String.Empty
        Dim UserLevel As String = String.Empty
        Dim Lines As String = String.Empty
        Dim MTID As String = String.Empty
        Dim QAID As String = String.Empty
        Dim UserMT As String
        Dim UserQA As String
        Dim PostDate As String = String.Empty
        If MDPFLPath.Extension.ToLower = ".xls" Then
            Dim Filepath As New FileInfo(MDPFilePath)
            Dim FolderPath As DirectoryInfo
            Dim fileName As String
            fileName = Filepath.Name
            FolderPath = Filepath.Directory
            Dim CNNEXCEL As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source=" + MDPFilePath + "; Extended Properties=""EXCEL 8.0;HDR=YES;IMEX=1;ImportMixedTypes=Text""")
            Dim ExcelFileName() As String
            ExcelFileName = GetExcelSheetNames(MDPFilePath)
            'Response.Write(ExcelFileName(0).ToString.Replace("'", "''"))
            'Response.End()
            '  If ExcelFileName(0).ToString.Replace("'", "''") <> "" Then
            Dim SQLCmd1 As New OleDbCommand("SELECT * FROM [" & ExcelFileName(0).ToString.Replace("'", "''") & "]", CNNEXCEL)
            Try
                SQLCmd1.Connection.Open()
                Dim DRRec1 As OleDbDataReader = SQLCmd1.ExecuteReader()
                If DRRec1.HasRows Then
                    While (DRRec1.Read)
                        If DRRec1(0).ToString.Replace("'", "''") <> "" And DRRec1(1).ToString.Replace("'", "''") <> "" Then
                            'UserMT = GetSXFID(DRRec1(2).ToString.Replace("'", "''"), "MTID")
                            JobNumber = DRRec1(0).ToString.Replace("'", "''")
                            Lines = DRRec1(4).ToString.Replace("'", "''")
                            MTID = DRRec1(2).ToString.Replace("'", "''")
                            QAID = DRRec1(3).ToString.Replace("'", "''")
                            PostDate = DRRec1(1).ToString.Replace("'", "''")
                            If MTID = QAID Then
                                UserLevel = 2
                                If InsertRec(JobNumber, UserLevel, Lines, PostDate) = True Then
                                    ShowDetails(JobNumber, UserLevel, Lines, PostDate, "Imported")
                                End If
                            Else


                                UserLevel = 1
                                If InsertRec(JobNumber, UserLevel, Lines, PostDate) = True Then
                                    ShowDetails(JobNumber, UserLevel, Lines, PostDate, "Imported")
                                End If

                                UserLevel = 3
                                If InsertRec(JobNumber, UserLevel, Lines, PostDate) = True Then
                                    ShowDetails(JobNumber, UserLevel, Lines, PostDate, "Imported")
                                End If
                                
                            End If

                        End If

                    End While
                End If

            Finally
                If SQLCmd1.Connection.State = ConnectionState.Open Then
                    SQLCmd1.Connection.Close()
                End If
            End Try

        End If


    End Sub

End Class

