Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.Office.Core
Imports System.Runtime.InteropServices.Marshal
Imports System.IO
Imports System.Data.OleDb



Partial Class ImportInvoice
    Inherits BasePage

    Protected myRepeater As Repeater
    Protected IDList As ArrayList = New ArrayList
    Protected DisplayChanged As Boolean = False

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

        
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
            Dim item As String
            Dim itemid As String
            Dim desc As String
            Dim qty As String
            Dim amount As String
            Dim total As String
            Dim strQuery As String
            Dim sdate As String

            ActName = ""

            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            filename = "C:\Uploads\" & FileUpload1.FileName
            Dim CNNEXCEL As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source=" + filename + "; Extended Properties=Excel 8.0;")
            Dim SQLCmd1 As New OleDbCommand("SELECT * FROM [Sheet1$]", CNNEXCEL)
            SQLCmd1.Connection.Open()
            Dim DRRec1 As OleDbDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then
                While (DRRec1.Read)

                    ActName = DRRec1(0).ToString
                    ActNumber = DRRec1(1).ToString
                    item = DRRec1(2).ToString
                    desc = DRRec1(3).ToString
                    qty = DRRec1(4).ToString
                    amount = DRRec1(5).ToString
                    total = DRRec1(6).ToString
                    sdate = DRRec1(8).ToString
                    Try

                        If ActNumber <> "" Then
                            'strQuery = "Select accountid, accountname, Billactnumber from(Select accountid, accountname, Billactnumber from tblaccounts UNION Select GrpActID as accountid, GrpActName as accountname, BillActNumber from tblgrpaccounts) T where contractorid='" & Session("contractorid").ToString & "' and BillActnumber = '" & ActNumber & "' "
                            strQuery = "Select accountid, accountname, Billactnumber from(Select accountid, accountname, Billactnumber,contractorID from tblaccounts UNION Select GrpActID as accountid, GrpActName as accountname, BillActNumber,contractorID from tblgrpaccounts) T where contractorid='" & Session("contractorid").ToString & "' and BillActnumber = '" & ActNumber & "' "
                            '  Response.Write(strQuery)
                            Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                            SQLCmd2.Connection.Open()
                            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()

                            'Response.End()
                            If DRRec2.HasRows Then
                                If DRRec2.Read Then
                                    AccountID = DRRec2("AccountID").ToString
                                    ActName = DRRec2("AccountName").ToString

                                    strQuery = "Select * from AdminSecureweb.dbo.ItemDetails where Item='" & item.ToString & "'"
                                    'Response.Write(strQuery)

                                    Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                                    SQLCmd3.Connection.Open()
                                    Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                    If DRRec3.HasRows Then
                                        If DRRec3.Read Then
                                            itemid = DRRec3("itemid").ToString
                                            'Response.Write(itemid)
                                            InsertRec(AccountID, "VAS", desc, itemid, qty, amount, total, sdate)
                                            ShowDetails(ActName, item, desc, sdate, qty, amount, total)
                                        End If

                                    End If
                                    '  Response.Write(recstatus)
                                    DRRec3.Close()
                                    SQLCmd3.Connection.Close()

                                End If
                            Else
                                Label2.Text = "Billing Account Number is not correct. Please contact System Administrator for more details."
                                ShowDetails(ActName, item, desc, sdate, qty, amount, total)
                            End If
                            DRRec2.Close()
                            SQLCmd2.Connection.Close()
                        End If

                    Catch ex As Exception
                        Response.Write(ex.Message)
                    End Try

                End While
            End If
            DRRec1.Close()
            SQLCmd1.Connection.Close()
            CNNEXCEL.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub InsertRec(ByVal AccID As String, ByVal mode As String, ByVal descr As String, ByVal itemid As String, ByVal qty As Integer, ByVal amount As Double, ByVal totamount As Double, ByVal sdate As Date)
        Dim strConn As String
        Dim strQuery As String
        Dim invoiceid As String = "11111111-1111-1111-1111-111111111111"
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        strQuery = "INSERT INTO [ADMINSecureweb].[dbo].[tblinvItemdet] ([InvoiceID] ,[AccountID] ,[Mode] ,[Descr] ,[itemid] ,[quantity] ,[amount],[totamount] ,[servicedate],[dateupdate]) VALUES ('" & invoiceid & "' ,'" & AccID & "' ,'" & mode & "' ,'" & descr & "' ,'" & itemid & "' ,'" & qty & "'  ,'" & amount & "' ,'" & totamount & "' ,'" & sdate & "' , '" & Now() & "'  )"
        '   Response.Write(strQuery)

        Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
        SQLCmd3.Connection.Open()
        SQLCmd3.ExecuteNonQuery()
        SQLCmd3.Connection.Close()
    End Sub

    'Protected Sub UpdateRec(ByVal InvoiceID As String, ByVal InvCode As String, ByVal InvDate As Date, ByVal Amount As Double, ByVal Comments As String)
    '    Dim strConn As String
    '    Dim strQuery As String
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    strQuery = "UPDATE [ADMINSecureweb].[dbo].[InvUpData]  SET [InvCode] = '" & InvCode & "' ,[InvDate] = '" & InvDate & "' ,[Amount] =  '" & Amount & "' ,[Comments] = '" & Comments & "', [updatedate] = '" & Now & "' WHERE [TrackID] = '" & InvoiceID & "'"
    '    '   Response.Write(strQuery)
    '    Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    SQLCmd3.Connection.Open()
    '    SQLCmd3.ExecuteNonQuery()
    '    SQLCmd3.Connection.Close()
    'End Sub

    Protected Sub ShowDetails(ByVal Account As String, ByVal Item As String, ByVal Desc As String, ByVal vasdate As Date, ByVal qty As String, ByVal amount As String, ByVal total As String)
        Dim row1 As New TableRow
        Dim Cell1 As New TableCell
        Dim Cell2 As New TableCell
        Dim Cell3 As New TableCell
        Dim Cell4 As New TableCell
        Dim Cell5 As New TableCell
        Dim Cell6 As New TableCell
        Dim Cell7 As New TableCell

        Cell1.Text = Account
        Cell2.Text = Item
        Cell3.Text = Desc
        Cell4.Text = vasdate
        Cell5.Text = qty
        Cell6.Text = amount
        Cell7.Text = total

        row1.Cells.Add(Cell1)
        row1.Cells.Add(Cell2)
        row1.Cells.Add(Cell3)
        row1.Cells.Add(Cell4)
        row1.Cells.Add(Cell5)
        row1.Cells.Add(Cell6)
        row1.Cells.Add(Cell7)

        TblDetails.Rows.Add(row1)


    End Sub

End Class

