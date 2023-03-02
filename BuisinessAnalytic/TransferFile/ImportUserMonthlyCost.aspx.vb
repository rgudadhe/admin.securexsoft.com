
Imports System.Data.SqlClient
Imports System.Data
Imports Microsoft.Office.Core
Imports System.Runtime.InteropServices.Marshal
Imports system.IO
Imports System.Data.OleDb


Partial Class Billing_FileImport_ImportLines
    Inherits BasePage

    Protected Sub InsertRec(ByVal UserID As String, ByVal target As String, ByVal salary As String, ByVal currency As String)
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            If target <> String.Empty Then
                strQuery = "INSERT INTO [ADMINETS].[dbo].[tblUsersTargetsMonthly] ([userid],[target],[salary],[currency],[procmonth],[procyear],[updatedate] ) VALUES "
                strQuery = strQuery & " ('" & UserID & "' ,'" & target & "' ,'" & salary & "' ,'" & currency & "'  ,'" & DLMonth.SelectedValue & "' ,'" & DLYear.SelectedValue & "' ,'" & Now() & "'   )"

            Else
                strQuery = "INSERT INTO [ADMINETS].[dbo].[tblUsersTargetsMonthly] ([userid],[salary],[currency],[procmonth],[procyear],[updatedate] ) VALUES "
                strQuery = strQuery & " ('" & UserID & "'  ,'" & salary & "' ,'" & currency & "'  ,'" & DLMonth.SelectedValue & "' ,'" & DLYear.SelectedValue & "' ,'" & Now() & "'   )"

            End If
            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
            SQLCmd3.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub

    Protected Sub UpdateRec(ByVal UserID As String, ByVal target As String, ByVal salary As String, ByVal currency As String)
        Dim strConn As String
        Dim strQuery As String = String.Empty
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            If target <> String.Empty Then
                strQuery = "UPDATE [ADMINETS].[dbo].[tblUsersTargetsMonthly]  SET target='" & target & "', salary = '" & salary & "' ,currency =  '" & currency & "', updatedate = '" & Now & "' WHERE userID = '" & UserID & "' and ProcMonth  = '" & DLMonth.SelectedValue & "' and ProcYear  = '" & DLYear.SelectedValue & "'"
            Else
                strQuery = "UPDATE [ADMINETS].[dbo].[tblUsersTargetsMonthly]  SET salary = '" & salary & "' ,currency =  '" & currency & "', updatedate = '" & Now & "' WHERE userID = '" & UserID & "' and ProcMonth  = '" & DLMonth.SelectedValue & "' and ProcYear  = '" & DLYear.SelectedValue & "'"
            End If

            'Response.Write(strQuery)
            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
            SQLCmd3.ExecuteNonQuery()
        Catch ex As Exception
            Response.Write("Err: " & ex.Message)
        Finally
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub

    Protected Sub ShowDetails(ByVal Name As String, ByVal UserID As String, ByVal UserCost As String, ByVal UserCurrency As String, ByVal Target As String, ByVal recstatus As String)
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
        Cell1.Text = Name
        Cell2.Text = UserID
        Cell3.Text = UserCost
        Cell4.Text = UserCurrency
        Cell5.Text = Target
        Cell6.Text = DLMonth.SelectedValue
        Cell7.Text = DLYear.SelectedValue
        Cell8.Text = recstatus

        row1.Cells.Add(Cell1)
        row1.Cells.Add(Cell2)
        row1.Cells.Add(Cell3)
        row1.Cells.Add(Cell4)
        row1.Cells.Add(Cell5)
        row1.Cells.Add(Cell6)
        row1.Cells.Add(Cell7)
        row1.Cells.Add(Cell8)

        TblDetails.Rows.Add(row1)


    End Sub

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
        Dim UserID As String = String.Empty
        Dim UserName As String = String.Empty
        Dim Name As String = String.Empty
        Dim UserCost As String = String.Empty
        Dim Target As String = String.Empty
        Dim UserCurrency As String = String.Empty

        Dim strQuery As String
        Dim recstatus As String = "Not updated"
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        If Not System.IO.Directory.Exists("C:\Uploads") Then
            System.IO.Directory.CreateDirectory("C:\Uploads")
        End If
        filename = "C:\Uploads\" & FileUpload1.FileName
        Dim CNNEXCEL As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source=" + filename + "; Extended Properties=Excel 8.0;")
        Dim SQLCmd1 As New OleDbCommand("SELECT * FROM [Sheet1$]", CNNEXCEL)
        SQLCmd1.Connection.Open()
        Dim DRRec1 As OleDbDataReader = SQLCmd1.ExecuteReader()
        If DRRec1.HasRows Then
            While (DRRec1.Read)
                UserName = DRRec1(0).ToString
                UserCost = DRRec1(1).ToString
                UserCurrency = DRRec1(2).ToString
                Target = DRRec1(3).ToString
                If UserName <> "" Then
                    'strQuery = "Select accountid, accountname, Billactnumber from(Select accountid, accountname, Billactnumber from tblaccounts UNION Select GrpActID as accountid, GrpActName as accountname, BillActNumber from tblgrpaccounts) T where contractorid='" & Session("contractorid").ToString & "' and BillActnumber = '" & ActNumber & "' "
                    strQuery = "Select userid, FirstName + ' ' + LastName as Name from AdminETS.dbo.tblusers where contractorid='" & Session("contractorid").ToString & "' and username = '" & UserName & "' "
                    '  Response.Write(strQuery)
                    Dim SQLCmd2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    SQLCmd2.Connection.Open()
                    Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                    If DRRec2.HasRows Then
                        If DRRec2.Read Then
                            UserID = DRRec2("UserID").ToString
                            Name = DRRec2("Name").ToString
                            strQuery = "Select * from [ADMINETS].[dbo].[tblUsersTargetsMonthly] where  userID = '" & UserID & "' and ProcMonth  = '" & DLMonth.SelectedValue & "' and ProcYear  = '" & DLYear.SelectedValue & "' "
                            ' Response.Write(strQuery)
                            Dim SQLCmd3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                            SQLCmd3.Connection.Open()
                            Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                            If DRRec3.HasRows Then
                                If DRRec3.Read Then
                                    
                                    recstatus = "updated"
                                    UpdateRec(UserID, Target, UserCost, UserCurrency)
                                    ShowDetails(Name, UserName, UserCost, UserCurrency, Target, recstatus)


                                End If
                            Else
                                recstatus = "inserted"

                                InsertRec(UserID, Target, UserCost, UserCurrency)
                                ShowDetails(Name, UserName, UserCost, UserCurrency, Target, recstatus)
                            End If
                            '  Response.Write(recstatus)
                            DRRec3.Close()
                            SQLCmd3.Connection.Close()
                        Else
                            recstatus = "inserted"
                            InsertRec(UserID, Target, UserCost, UserCurrency)
                            ShowDetails(Name, UserName, UserCost, UserCurrency, Target, recstatus)
                        End If

                    Else
                        recstatus = "UserID is not correct. Please contact System Administrator for more details."
                        ShowDetails(Name, UserName, UserCost, UserCurrency, Target, recstatus)
                    End If
                    DRRec2.Close()
                    SQLCmd2.Connection.Close()
                End If

            End While
        End If
        DRRec1.Close()
        SQLCmd1.Connection.Close()
        CNNEXCEL.Close()
        Dim obj As New ETS.BL.BusinessAnalytics
        obj.UpdateUsersMonthlyCost(DLMonth.SelectedValue, DLYear.SelectedValue)
        obj = Nothing
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetMyMonthList(DLMonth, True)
            GetMyYearList(DLYear, True)
            DLMonth.SelectedIndex = DLMonth.Items.IndexOf(DLMonth.Items.FindByValue(Now.AddMonths(-1).Month))
            DLYear.SelectedIndex = DLYear.Items.IndexOf(DLYear.Items.FindByValue(Now.AddMonths(-1).Year))
        End If
    End Sub
End Class
