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

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
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
        Dim strConn As String
        Dim AccountID As String
        Dim ActName As String
        Dim ActNumber As String
        Dim InvType As String
        Dim InvDate As String
        Dim InvNumber As String
        Dim InvAmount As String
        Dim InvComments As String
        Dim InvMonth As String
        Dim InvYear As String
        Dim InvCycle As String
        Dim recstatus As String
        Dim strQuery As String
        ActName = ""
        recstatus = "Not updated"

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        filename = "C:\Uploads\" & FileUpload1.FileName
        Dim CNNEXCEL As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source=" + filename + "; Extended Properties=Excel 8.0;")
        Dim SQLCmd1 As New OleDbCommand("SELECT * FROM [Sheet1$]", CNNEXCEL)
        SQLCmd1.Connection.Open()
        Dim DRRec1 As OleDbDataReader = SQLCmd1.ExecuteReader()
        If DRRec1.HasRows Then
            While (DRRec1.Read)

                ActNumber = DRRec1(0).ToString
                InvType = DRRec1(1).ToString
                InvDate = DRRec1(2).ToString
                InvNumber = DRRec1(3).ToString
                InvAmount = DRRec1(4).ToString
                InvComments = DRRec1(5).ToString
                InvMonth = DRRec1(6).ToString
                InvYear = DRRec1(7).ToString
                InvCycle = DRRec1(8).ToString

                If ActNumber <> "" Then
                    strQuery = "Select * from tblaccounts where BillActnumber = '" & ActNumber & "' "
                    Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    SQLCmd2.Connection.Open()
                    Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                    If DRRec2.HasRows Then
                        If DRRec2.Read Then
                            AccountID = DRRec2("AccountID").ToString
                            ActName = DRRec2("AccountName").ToString
                            If InvType = "Invoice" Then
                                strQuery = "Select * from AdminSecureweb.dbo.InvUpdata where InvType='" & InvType & "' and AccID = '" & AccountID & "' and BillYear = '" & InvYear & "' and BillMonth = '" & InvMonth & "' and BillCycle = '" & InvCycle & "' "
                                Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                SQLCmd3.Connection.Open()
                                Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                If DRRec3.HasRows Then
                                    If DRRec3.Read Then
                                        If DRRec3("status").ToString = "Posted" Then
                                        Else
                                            recstatus = "updated"
                                            UpdateRec(DRRec3("TrackID").ToString, InvNumber, InvDate, InvAmount, InvComments)
                                            ShowDetails(ActName, ActNumber, InvType, InvDate, InvNumber, InvAmount, InvComments, InvMonth, InvYear, InvCycle, recstatus)

                                        End If
                                    End If
                                Else
                                    recstatus = "inserted"
                                    InsertRec(AccountID, InvType, InvNumber, InvDate, InvAmount, InvComments, InvMonth, InvYear, InvCycle)
                                    ShowDetails(ActName, ActNumber, InvType, InvDate, InvNumber, InvAmount, InvComments, InvMonth, InvYear, InvCycle, recstatus)
                                End If
                                DRRec3.Close()
                                SQLCmd3.Connection.Close()
                            Else
                                recstatus = "inserted"
                                InsertRec(AccountID, InvType, InvNumber, InvDate, InvAmount, InvComments, InvMonth, InvYear, InvCycle)
                                ShowDetails(ActName, ActNumber, InvType, InvDate, InvNumber, InvAmount, InvComments, InvMonth, InvYear, InvCycle, recstatus)
                            End If
                        End If
                    Else
                        recstatus = "Billing Account Number is not correct. Please contact System Administrator for more details."
                        ShowDetails(ActName, ActNumber, InvType, InvDate, InvNumber, InvAmount, InvComments, InvMonth, InvYear, InvCycle, recstatus)
                    End If
                    DRRec2.Close()
                    SQLCmd2.Connection.Close()
                End If

            End While
        End If
        DRRec1.Close()
        SQLCmd1.Connection.Close()
        CNNEXCEL.Close()

    End Sub

    Protected Sub InsertRec(ByVal AccID As String, ByVal InvType As String, ByVal InvCode As String, ByVal InvDate As Date, ByVal Amount As Double, ByVal Comments As String, ByVal BillMonth As String, ByVal BillYear As String, ByVal BillCycle As String)
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        strQuery = "INSERT INTO [ADMINSecureweb].[dbo].[InvUpData] ([AccID] ,[InvType] ,[InvCode] ,[InvDate] ,[Amount] ,[Comments],[BillMonth] ,[BillYear] ,[BillCycle] ,[updatedate] ) VALUES ('" & AccID & "' ,'" & InvType & "' ,'" & InvCode & "' ,'" & InvDate & "'  ,'" & Amount & "' ,'" & Comments & "'  ,'" & BillMonth & "' ,'" & BillYear & "' ,'" & BillCycle & "' ,'" & Now() & "'   )"
        ' Response.Write(strQuery)

        Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
        SQLCmd3.Connection.Open()
        SQLCmd3.ExecuteNonQuery()
        SQLCmd3.Connection.Close()
    End Sub

    Protected Sub UpdateRec(ByVal InvoiceID As String, ByVal InvCode As String, ByVal InvDate As Date, ByVal Amount As Double, ByVal Comments As String)
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        strQuery = "UPDATE [ADMINSecureweb].[dbo].[InvUpData]  SET [InvCode] = '" & InvCode & "' ,[InvDate] = '" & InvDate & "' ,[Amount] =  '" & Amount & "' ,[Comments] = '" & Comments & "', [updatedate] = '" & Now & "' WHERE [TrackID] = '" & InvoiceID & "'"
        'Response.Write(strQuery)
        Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
        SQLCmd3.Connection.Open()
        SQLCmd3.ExecuteNonQuery()
        SQLCmd3.Connection.Close()
    End Sub

    Protected Sub ShowDetails(ByVal Account As String, ByVal ActNumber As String, ByVal InvType As String, ByVal Invdate As Date, ByVal InvNumber As String, ByVal Invamount As String, ByVal InvComments As String, ByVal Invmonth As String, ByVal invyear As String, ByVal Invcycle As String, ByVal status As String)
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
        Cell1.Text = Account
        Cell2.Text = ActNumber
        Cell3.Text = InvType
        Cell4.Text = Invdate
        Cell5.Text = InvNumber
        Cell6.Text = Invamount
        Cell7.Text = InvComments
        Cell8.Text = Invmonth
        Cell9.Text = invyear
        Cell10.Text = Invcycle
        Cell11.Text = status
        row1.Cells.Add(Cell1)
        row1.Cells.Add(Cell2)
        row1.Cells.Add(Cell3)
        row1.Cells.Add(Cell4)
        row1.Cells.Add(Cell5)
        row1.Cells.Add(Cell6)
        row1.Cells.Add(Cell7)
        row1.Cells.Add(Cell8)
        row1.Cells.Add(Cell9)
        row1.Cells.Add(Cell10)
        row1.Cells.Add(Cell11)
        TblDetails.Rows.Add(row1)


    End Sub

End Class

