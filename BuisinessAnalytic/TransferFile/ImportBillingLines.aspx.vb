Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.Office.Core
Imports System.Runtime.InteropServices.Marshal
Imports system.IO
Imports System.Data.OleDb


Partial Class Billing_FileImport_ImportLines
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim LI2 As New ListItem
            LI2.Text = "SecureXFlow"
            LI2.Value = "11111111-1111-1111-1111-111111111111"
            DLPlatform.Items.Add(LI2)
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()
                Dim SQLCmd2 As New SqlCommand("Select AccountName, AccountID from tblaccounts where IsPlatForm = 'True' and contractorID='" & Session("ContractorID") & "' and (IsDeleted is null or IsDeleted=0)", oConn)
                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                If DRRec2.HasRows = True Then
                    While DRRec2.Read
                        Dim LI As New ListItem
                        LI.Text = DRRec2("accountname").ToString
                        LI.Value = DRRec2("accountid").ToString
                        DLPlatform.Items.Add(LI)
                    End While
                End If
                DRRec2.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> ConnectionState.Open Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub

    Protected Sub DLPlatform_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLPlatform.SelectedIndexChanged

    End Sub

    Protected Sub InsertRec(ByVal PlatformID As String, ByVal JobNumber As String, ByVal Level As String, ByVal Lines As Double, ByVal Dictator As String, ByVal Postdate As Date)
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            strQuery = "INSERT INTO [ADMINETS].[dbo].tblBillingPlatLines] (PlatActID, JobNumber, UserLevel, Lines, Dictator, PostDate,updatedate ) VALUES "
            strQuery = strQuery & " ('" & PlatformID & "'  ,'" & JobNumber & "' ,'" & Level & "'  ,'" & Lines & "' ,'" & Dictator & "'  ,'" & Postdate & "' ,'" & Now() & "'   )"
            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
            SQLCmd3.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> ConnectionState.Open Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub

    Protected Sub UpdateRec(ByVal Lines As Double, ByVal Dictator As String, ByVal PostDate As Date, ByVal JobNumber As String, ByVal Level As String)
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            strQuery = "UPDATE [ADMINETS].[dbo].tblBillingPlatLines]  SET Lines = '" & Lines & "' ,Dictator =  '" & Dictator & "', updatedate = '" & Now & "' WHERE JobNumber = '" & JobNumber & "' and userLevel  = '" & Level & "'"
            'Response.Write(strQuery)
            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
            SQLCmd3.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> ConnectionState.Open Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try

    End Sub

    Protected Sub ShowDetails(ByVal Platform As String, ByVal JobNumber As String, ByVal Level As String, ByVal Lines As Double, ByVal Dictator As String, ByVal PostDate As Date, ByVal recstatus As String)
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
        Dim Cell10 As New TableCell
        Dim Cell11 As New TableCell
        Cell1.Text = Platform
        'Cell2.Text = Username
        Cell3.Text = JobNumber
        Cell4.Text = Level
        Cell5.Text = Lines
        Cell6.Text = Dictator
        Cell7.Text = PostDate
        Cell8.Text = recstatus

        row1.Cells.Add(Cell1)
        'row1.Cells.Add(Cell2)
        row1.Cells.Add(Cell3)
        row1.Cells.Add(Cell4)
        row1.Cells.Add(Cell5)
        row1.Cells.Add(Cell6)
        row1.Cells.Add(Cell7)
        row1.Cells.Add(Cell8)

        TblDetails.Rows.Add(row1)


    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Response.Write("'{" & DLPlatform.SelectedValue & "}'")
        'Exit Sub

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
        Dim filename As String

        filename = "C:\Uploads\" & FileUpload1.FileName
        Dim excelConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;" & vbCr & vbLf & "Data Source=" & filename & ";" & vbCr & vbLf & "Extended Properties=""Excel 8.0;HDR=YES;"""

        'Create Connection to Excel work book
        Dim excelConnection As New OleDbConnection(excelConnectionString)

        'Create OleDbCommand to fetch data from Excel

        Dim cmd As New OleDbCommand("Select {" & DLPlatform.SelectedValue & "} as [PlatActID],[UserLevel], [JobNumber],[PostDate],[Lines],[BLines],[Dictator], '" & Now & "' as [updatedate]  from [Sheet1$]", excelConnection)

        excelConnection.Open()
        Dim dReader As OleDbDataReader
        dReader = cmd.ExecuteReader()
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        oConn.Open()
        Dim sqlBulk As New SqlBulkCopy(oConn)
        sqlBulk.DestinationTableName = "AdminETS.dbo.tblBillingPlatLines"
        sqlBulk.ColumnMappings.Add(0, 1)

        ',[PostDate],[UserLevel],[Lines],[Username],[Dictator]
        sqlBulk.ColumnMappings.Add(1, 3)
        sqlBulk.ColumnMappings.Add(2, 2)
        sqlBulk.ColumnMappings.Add(3, 8)
        sqlBulk.ColumnMappings.Add(4, 4)
        'sqlBulk.ColumnMappings.Add(5, 2)
        sqlBulk.ColumnMappings.Add(5, 5)
        sqlBulk.ColumnMappings.Add(6, 7)
        sqlBulk.ColumnMappings.Add(7, 9)
        'sqlBulk.ColumnMappings.Add("Lines", "Lines")
        'sqlBulk.ColumnMappings.Add("Username", "Username")
        'sqlBulk.ColumnMappings.Add("Dictator", "Dictator")
        sqlBulk.WriteToServer(dReader)

        If oConn.State = ConnectionState.Open Then
            oConn.Close()
        End If
        If excelConnection.State = ConnectionState.Open Then
            excelConnection.Close()
        End If
        '                    oConn.Close()
        '                    oConn = Nothing
        '                End If
        'If DLPlatform.SelectedValue = "" Then

        'Else
        '    Dim strConn As String
        '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        '    Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        '    Try
        '        'oConn.Open()
        '        If FileUpload1.HasFile Then
        '            Try
        '                If Not System.IO.Directory.Exists("C:\Uploads") Then
        '                    System.IO.Directory.CreateDirectory("C:\Uploads")
        '                End If
        '                FileUpload1.SaveAs("C:\Uploads\" & _
        '                   FileUpload1.FileName)
        '                Label2.Text = "File name: " & _
        '                   FileUpload1.PostedFile.FileName & "<br>" & _
        '                   "File Size: " & _
        '                   FileUpload1.PostedFile.ContentLength & " kb<br>" & _
        '                   "Content type: " & _
        '                   FileUpload1.PostedFile.ContentType
        '            Catch ex As Exception
        '                Label2.Text = "ERROR: " & ex.Message.ToString()
        '            End Try
        '        Else
        '            Label2.Text = "You have not specified a file."
        '        End If
        '        Dim filename As String

        '        Dim PostDate As String
        '        Dim JobNumber As String
        '        Dim Level As String
        '        Dim Lines As String
        '        Dim Username As String
        '        Dim Dictator As String
        '        Dim recstatus As String
        '        Dim strQuery As String

        '        recstatus = "Not updated"


        '        filename = "C:\Uploads\" & FileUpload1.FileName
        '        Dim CNNEXCEL As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source=" + filename + "; Extended Properties=Excel 8.0;")
        '        Dim SQLCmd1 As New OleDbCommand("SELECT * FROM [Sheet1$]", CNNEXCEL)
        '        SQLCmd1.Connection.Open()
        '        Dim DRRec1 As OleDbDataReader = SQLCmd1.ExecuteReader()
        '        If DRRec1.HasRows Then
        '            While (DRRec1.Read)

        '                PostDate = DRRec1(0).ToString
        '                JobNumber = DRRec1(1).ToString
        '                Level = DRRec1(2).ToString
        '                Lines = DRRec1(3).ToString
        '                Username = DRRec1(4).ToString
        '                Dictator = DRRec1(5).ToString.Replace("'", "")
        '                strQuery = "Select * from AdminETS.dbo.tblUserPlatLines where PlatActID='" & DLPlatform.SelectedValue & "' and JobNumber='" & JobNumber & "' and UserLevel = '" & Level & "' "
        '                oConn.Open()
        '                Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
        '                Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
        '                If DRRec3.HasRows Then
        '                    If DRRec3.Read Then
        '                        recstatus = "updated"
        '                        UpdateRec(Username, Lines, Dictator, PostDate, JobNumber, Level)
        '                        ShowDetails(DLPlatform.Text, Username, JobNumber, Level, Lines, Dictator, PostDate, recstatus)
        '                    End If
        '                Else
        '                    recstatus = "inserted"
        '                    InsertRec(DLPlatform.SelectedValue, Username, JobNumber, Level, Lines, Dictator, PostDate)
        '                    ShowDetails(DLPlatform.Text, Username, JobNumber, Level, Lines, Dictator, PostDate, recstatus)
        '                End If
        '                DRRec3.Close()
        '                If oConn.State <> ConnectionState.Open Then
        '                    oConn.Close()
        '                    oConn = Nothing
        '                End If
        '            End While
        '        End If
        '        DRRec1.Close()
        '        CNNEXCEL.Close()
        '    Catch ex As Exception
        '        Response.Write(ex.Message)
        '    Finally

        '    End Try
        'End If

    End Sub
End Class
