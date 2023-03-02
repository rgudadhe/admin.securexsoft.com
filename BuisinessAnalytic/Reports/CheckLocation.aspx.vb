Imports System.Data.Sqlclient
Imports System.Data
Partial Class MIS_DailyMins
    Inherits BasePage

    Protected Sub ShowActDetails()
        Dim strConn As String
        Dim strDate As String
        
        Dim InpDate As Date
        strDate = DLMonth.SelectedValue & "/1/" & DLYear.SelectedValue
        InpDate = Date.Parse(strDate)
        Dim C1SDate As Date
        Dim C1EDate As Date
        Dim C2SDate As Date
        Dim C2EDate As Date
        Dim minDate As Date
        Dim strQuery As String
        C1SDate = Month(InpDate) & "/1/" & Year(InpDate)
        C2EDate = DateAdd(DateInterval.Month, 1, C1SDate)
        'If DLCycle.SelectedValue = "1" Then
        '    tblDtls.Text = "Billing Report (" & C1SDate & " - " & C1EDate & ")"
        '    minDate = C1EDate
        'Else
        '    tblDtls.Text = "Billing Report (" & C1SDate & " - " & C2EDate & ")"
        '    minDate = C2EDate
        'End If

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        
        strQuery = " Select  * from AdminSecureweb.dbo.tblTranscriptionclientMain where datemodified between '" & C1SDate & "' and '" & C2EDate & "' and  AccountID = '" & DLAct.SelectedValue & "' and location not in (Select LocCode from AdminETS.dbo.tblaccountslocations where AccountID = '" & DLAct.SelectedValue & "' ) Order by  jobnumber"
        Response.Write(strQuery)
        ''      Response.End()
        'Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
        'Try
        '    SQLCmd.Connection.Open()
        '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
        '    If DRRec.HasRows Then
        '        MyDataGrid.DataSource = DRRec
        '        MyDataGrid.DataBind()
        '    End If
        'Finally
        '    If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
        '        SQLCmd.Connection.Close()
        '        SQLCmd = Nothing
        '    End If
        'End Try
        HStrQuery.Value = strQuery
        SqlDataSource1.SelectCommand = strQuery

    End Sub




    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click
        ShowActDetails()
    End Sub

    Function WorkingDays(ByVal StartDate As Date, ByVal EndDate As Date) As Integer
        Dim intCount As Integer
        intCount = 0
        Do While StartDate <= EndDate
            Select Case Weekday(StartDate)
                Case "1"
                    intCount = intCount
                Case "7"
                    intCount = intCount
                Case "2"
                    intCount = intCount + 1
                Case "3"
                    intCount = intCount + 1
                Case "4"
                    intCount = intCount + 1
                Case "5"
                    intCount = intCount + 1
                Case "6"
                    intCount = intCount + 1
            End Select
            StartDate = DateAdd(DateInterval.Day, 1, StartDate)
        Loop
        If intCount = 0 Then
            intCount = 1
        End If
        WorkingDays = intCount
    End Function

    Function InsRecord(ByVal AccID As String, ByVal ItemID As String, ByVal Descr As String, ByVal Quantity As Integer, ByVal amount As Double, ByVal Totamount As Double, ByVal ServiceDate As String) As Boolean
        Try

            Dim strConn As String
            Dim strQuery As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            strQuery = "Insert Into AdminSecureweb.dbo.tblinvItemdet (AccountID,InvoiceID, itemid, Descr, quantity, Amount,totamount,mode,servicedate, dateupdate) Values ('" & AccID & "', '11111111-1111-1111-1111-111111111111', '" & ItemID & "', '" & Descr & "', '" & Quantity & "', convert(money," & amount & "),  convert(money," & Totamount & "), 'VAS', '" & ServiceDate & "', '" & Now & "')"
            '            Response.Write(strQuery)
            Dim cmdIns As New SqlCommand(strQuery, New SqlConnection(strConn))
            Try
                cmdIns.Connection.Open()
                cmdIns.ExecuteNonQuery()
            Finally
                If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
                    cmdIns.Connection.Close()
                    cmdIns = Nothing
                End If
            End Try
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim SQLCmd1 As New SqlCommand("Select * from AdminETS.dbo.tblaccounts where mode='LC' and contractorid ='" & Session("contractorid").ToString & "' order by Accountname", New SqlConnection(strConn))
            Try
                SQLCmd1.Connection.Open()
                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                If DRRec1.HasRows = True Then
                    While DRRec1.Read
                        Dim LI As New ListItem
                        LI.Text = DRRec1("Accountname")
                        LI.Value = DRRec1("AccountID").ToString
                        DLAct.Items.Add(LI)
                    End While
                End If
                DRRec1.Close()
            Finally
                If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd1.Connection.Close()
                    SQLCmd1 = Nothing
                End If
            End Try

        End If
    End Sub

    Protected Sub MyDataGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles MyDataGrid.RowDataBound
        SqlDataSource1.SelectCommand = HStrQuery.Value
        'Response.Write("strquery1: " & HStrQuery.Value)
    End Sub

    Protected Sub MyDataGrid_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles MyDataGrid.RowEditing
        SqlDataSource1.SelectCommand = HStrQuery.Value
        'Response.Write("strquery2: " & HStrQuery.Value)
    End Sub

    Protected Sub MyDataGrid_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles MyDataGrid.RowUpdating
        SqlDataSource1.SelectCommand = HStrQuery.Value
        'Response.Write("strquery3: " & HStrQuery.Value)
    End Sub
End Class
