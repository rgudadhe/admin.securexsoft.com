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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim LI2 As New ListItem
            LI2.Text = "Select Platform"
            LI2.Value = ""
            DLPlatform.Items.Add(LI2)
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()
                Dim SQLCmd2 As New SqlCommand("Select AccountName, AccountID from tblaccounts where isplatform = 'True' ", oConn)
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
                If oConn.State <> ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub

    Protected Sub DLPlatform_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLPlatform.SelectedIndexChanged

    End Sub

    Protected Sub InsertRec(ByVal AccountID As String, ByVal MTLines As String, ByVal MTRate As String, ByVal MTPLines As String, ByVal MTPRate As String, ByVal QALines As String, ByVal QARate As String, ByVal MTSLines As String, ByVal MTSRate As String, ByVal MTPSLines As String, ByVal MTPSRate As String, ByVal QASLines As String, ByVal QASRate As String, ByVal CPL As String, ByVal STATLines As String, ByVal BillCycle As String, ByVal BillMonth As String, ByVal BillYear As String)
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            strQuery = "INSERT INTO AdminSecureweb.dbo.tblInDirActDetails (AccountID, MTLines,MTRate,MTPLines, MTPRate,QALines,QARate,MTSLines,MTSRate,MTPSLines,MTPSRate,QASLines,QASRate,CPL,STATLines,BillCycle,BillMonth,BillYear,updatedate ) VALUES "
            strQuery = strQuery & " ('" & AccountID & "' ,'" & MTLines & "', '" & MTRate & "', '" & MTPLines & "', '" & MTPRate & "', '" & QALines & "', '" & QARate & "', '" & MTSLines & "', '" & MTSRate & "', '" & MTPSLines & "', '" & MTPSRate & "', '" & QASLines & "', '" & QASRate & "', '" & CPL & "', '" & STATLines & "', '" & BillCycle & "', '" & BillMonth & "', '" & BillYear & "','" & Now() & "'   )"
            '    Response.Write(strQuery)

            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
            SQLCmd3.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub

    Protected Sub UpdateRec(ByVal AutoID As String, ByVal MTLines As String, ByVal MTRate As String, ByVal MTPLines As String, ByVal MTPRate As String, ByVal QALines As String, ByVal QARate As String, ByVal MTSLines As String, ByVal MTSRate As String, ByVal MTPSLines As String, ByVal MTPSRate As String, ByVal QASLines As String, ByVal QASRate As String, ByVal CPL As String, ByVal STATLines As String, ByVal BillCycle As String, ByVal BillMonth As String, ByVal BillYear As String)
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            strQuery = "UPDATE AdminSecureweb.dbo.tblInDirActDetails SET MTLines = '" & MTLines & "', MTRate = '" & MTRate & "', MTPLines = '" & MTPLines & "', MTPRate = '" & MTPRate & "', QALines = '" & QALines & "', QARate = '" & QARate & "', MTSLines = '" & MTSLines & "', MTSRate = '" & MTSRate & "', MTPSLines = '" & MTPSLines & "', MTPSRate = '" & MTPSRate & "', QASLines = '" & QASLines & "', QASRate = '" & QASRate & "', CPL = '" & CPL & "', STATLines = '" & STATLines & "', BillCycle = '" & BillCycle & "', BillMonth = '" & BillMonth & "', BillYear = '" & BillYear & "', updatedate = '" & Now & "' WHERE AutoID  = '" & AutoID & "'"
            '    Response.Write(strQuery)
            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
            SQLCmd3.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try

    End Sub

    Protected Sub ShowDetails(ByVal Platform As String, ByVal MTLines As String, ByVal MTRate As String, ByVal MTPLines As String, ByVal MTPRate As String, ByVal QALines As String, ByVal QARate As String, ByVal MTSLines As String, ByVal MTSRate As String, ByVal MTPSLines As String, ByVal MTPSRate As String, ByVal QASLines As String, ByVal QASRate As String, ByVal CPL As String, ByVal STATLines As String, ByVal BillCycle As String, ByVal BillMonth As String, ByVal BillYear As String, ByVal recstatus As String)
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
        Dim Cell12 As New TableCell
        Dim Cell13 As New TableCell
        Dim Cell14 As New TableCell
        Dim Cell15 As New TableCell
        Dim Cell16 As New TableCell
        Dim Cell17 As New TableCell
        Dim Cell18 As New TableCell
        Dim Cell19 As New TableCell
        Cell1.Text = Platform
        Cell2.Text = MTLines
        Cell3.Text = MTRate
        Cell4.Text = MTPLines
        Cell5.Text = MTPRate
        Cell6.Text = QALines
        Cell7.Text = QARate
        Cell8.Text = MTSLines
        Cell9.Text = MTSRate
        Cell10.Text = MTPSRate
        Cell11.Text = QASLines
        Cell12.Text = QASRate
        Cell13.Text = CPL
        Cell14.Text = STATLines
        Cell15.Text = BillCycle
        Cell16.Text = BillMonth
        Cell17.Text = BillYear
        Cell18.Text = recstatus
        Cell19.Text = MTPSLines

        row1.Cells.Add(Cell1)
        row1.Cells.Add(Cell2)
        row1.Cells.Add(Cell3)
        row1.Cells.Add(Cell4)
        row1.Cells.Add(Cell5)
        row1.Cells.Add(Cell6)
        row1.Cells.Add(Cell7)
        row1.Cells.Add(Cell9)
        row1.Cells.Add(Cell19)
        row1.Cells.Add(Cell10)
        row1.Cells.Add(Cell11)
        row1.Cells.Add(Cell12)
        row1.Cells.Add(Cell13)
        row1.Cells.Add(Cell14)
        row1.Cells.Add(Cell15)
        row1.Cells.Add(Cell16)
        row1.Cells.Add(Cell17)
        row1.Cells.Add(Cell18)
        TblDetails.Rows.Add(row1)


    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DLPlatform.SelectedValue = "" Then

        Else
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()
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

                Dim PostDate As String
                Dim JobNumber As String
                Dim Level As String
                Dim Lines As String
                Dim Username As String
                Dim Dictator As String
                Dim recstatus As String
                Dim strQuery As String
                Dim MTLines As String
                Dim MTRate As String
                Dim MTPLines As String
                Dim MTPRate As String
                Dim QALines As String
                Dim QARate As String
                Dim MTSLines As String
                Dim MTSRate As String
                Dim MTPSLines As String
                Dim MTPSRate As String
                Dim QASLines As String
                Dim QASRate As String
                Dim CPL As String
                Dim STATLines As String
                Dim BillCycle As String
                Dim BillMonth As String
                Dim BillYear As String

                recstatus = "Not updated"
                'Response.Write(DLPlatform.SelectedItem)

                filename = "C:\Uploads\" & FileUpload1.FileName
                Dim CNNEXCEL As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source=" + filename + "; Extended Properties=Excel 8.0;")
                Dim SQLCmd1 As New OleDbCommand("SELECT * FROM [Sheet1$]", CNNEXCEL)
                SQLCmd1.Connection.Open()
                Dim DRRec1 As OleDbDataReader = SQLCmd1.ExecuteReader()
                If DRRec1.HasRows Then
                    While (DRRec1.Read)

                        MTLines = DRRec1(0).ToString
                        MTRate = DRRec1(1).ToString
                        MTPLines = DRRec1(2).ToString
                        MTPRate = DRRec1(3).ToString
                        QALines = DRRec1(4).ToString
                        QARate = DRRec1(5).ToString
                        MTSLines = DRRec1(6).ToString
                        MTSRate = DRRec1(7).ToString
                        MTPSLines = DRRec1(8).ToString
                        MTPSRate = DRRec1(9).ToString
                        QASLines = DRRec1(10).ToString
                        QASRate = DRRec1(11).ToString
                        CPL = DRRec1(12).ToString
                        STATLines = DRRec1(13).ToString
                        BillCycle = DRRec1(14).ToString
                        BillMonth = DRRec1(15).ToString
                        BillYear = DRRec1(16).ToString
                        If BillYear <> "" Then

                            strQuery = "Select * from AdminSecureweb.dbo.tblInDirActDetails where AccountID='" & DLPlatform.SelectedValue & "' and BillCycle='" & BillCycle & "' and BillYear='" & BillYear & "' and BillMonth='" & BillMonth & "'  "
                            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)

                            Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                            If DRRec3.HasRows Then
                                If DRRec3.Read Then

                                    recstatus = "updated"
                                    UpdateRec(DRRec3("AutoID").ToString, MTLines, MTRate, MTPLines, MTPRate, QALines, QARate, MTSLines, MTSRate, MTPSLines, MTPSRate, QASLines, QASRate, CPL, STATLines, BillCycle, BillMonth, BillYear)
                                    ShowDetails(DLPlatform.SelectedItem.ToString, MTLines, MTRate, MTPLines, MTPRate, QALines, QARate, MTSLines, MTSRate, MTPSLines, MTPSRate, QASLines, QASRate, CPL, STATLines, BillCycle, BillMonth, BillYear, recstatus)


                                End If
                            Else
                                recstatus = "inserted"
                                InsertRec(DLPlatform.SelectedValue, MTLines, MTRate, MTPLines, MTPRate, QALines, QARate, MTSLines, MTSRate, MTPSLines, MTPSRate, QASLines, QASRate, CPL, STATLines, BillCycle, BillMonth, BillYear)
                                ShowDetails(DLPlatform.SelectedItem.ToString, MTLines, MTRate, MTPLines, MTPRate, QALines, QARate, MTSLines, MTSRate, MTPSLines, MTPSRate, QASLines, QASRate, CPL, STATLines, BillCycle, BillMonth, BillYear, recstatus)
                            End If
                            DRRec3.Close()
                        End If


                    End While
                End If
                DRRec1.Close()
                CNNEXCEL.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If

    End Sub

End Class

