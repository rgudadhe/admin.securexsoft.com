Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.Office.Core
Imports System.Runtime.InteropServices.Marshal
Imports system.IO


Partial Class DemoAccount_DemoTemplate
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.ContentType = "application/ms-excel"
        Dim oApp As Excel.Application
        Dim oWB As Excel.Workbook
        oApp = New Excel.Application
        oApp.Visible = False
        oWB = oApp.Workbooks.Add
        Dim Filename As String
        Filename = ""
        Dim FilePath As String
        FilePath = ""
        Dim strconn As String
        Dim SQuery As String

        strconn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        SQuery = "Select * from tblAccounts where AccountID =  '" & Request("AccountID") & "'"
        Dim SQLCmd1 As New SqlCommand(SQuery, New SqlConnection(strconn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then
                While (DRRec1.Read)
                    Filename = DRRec1("description")
                End While
            End If
            DRRec1.Close()
        Finally
            If SQLCmd1.Connection.State = ConnectionState.Closed Then
                SQLCmd1.Connection.Close()
            End If
        End Try
        Dim k As Integer
        k = 0
        'DRRec1.Close()
        'SQLCmd1.Connection.Close()
        Dim row1 As New TableRow
        SQuery = "Select * from tblActDemos where AccountID =  '" & Request("AccountID") & "'"
        Dim SQLCmd2 As New SqlCommand(SQuery, New SqlConnection(strconn))
        Try
            SQLCmd2.Connection.Open()
            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
            If DRRec2.HasRows Then
                While (DRRec2.Read)
                    k = k + 1
                    oWB.Sheets(1).Cells(1, k).Value = DRRec2("DemoFieldName")
                    'ObjWS.Cells(1, k) = DRRec2("DemoFieldName")
                    'ObjWS.Columns.AutoFit()
                End While
            End If
            DRRec2.Close()
        Finally
            If SQLCmd2.Connection.State = ConnectionState.Closed Then
                SQLCmd2.Connection.Close()
            End If
        End Try
        Dim strFolder As String
        strFolder = Server.MapPath("ETS_Files") & "\Secureweb\DemoTemplate"

        FilePath = strFolder & Filename & Format(Now(), "yyyymmdd_HHmmss") & ".xls"
        '  Response.Write(FilePath)
        '  Response.End()

        Dim fso
        fso = CreateObject("Scripting.FileSystemObject")
        If Not fso.FolderExists(strFolder) Then
            'Create the file
            FileSystem.MkDir(strFolder)
        End If

        oWB.Close(SaveChanges:=True, Filename:=FilePath)
        oWB = Nothing
        oApp.Quit()
        oApp = Nothing
        HyperLink1.NavigateUrl = "http://ets.edictate.com/users/DemoTemplate/" & Filename & Format(Now(), "yyyymmdd_HHmmss") & ".xls"
        'Open the file after export to excel
        'Shell("EXCEL.EXE C:\filename.xls", vbNormalFocus)


    End Sub

    Private Sub ReleaseComObject(ByRef Reference As Object)
        Try
            Do Until _
             System.Runtime.InteropServices.Marshal.ReleaseComObject(Reference) <= 0
            Loop
        Catch
        Finally
            Reference = Nothing
        End Try
    End Sub




End Class
