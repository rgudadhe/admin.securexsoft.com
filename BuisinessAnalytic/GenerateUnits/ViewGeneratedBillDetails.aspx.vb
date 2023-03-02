Imports System.Data.SqlClient
Imports System.Data
Partial Class GenLC_New
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Visible = False
        If Not IsPostBack Then
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim SQLCmd1 As New SqlCommand("Select * from AdminETS.dbo.tblaccounts where contractorid ='" & Session("contractorid").ToString & "' order by Accountname", New SqlConnection(strConn))
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
            GetMyMonthList(DLMonth, True)
            GetMyYearList(DLYear, True)
            ' Table1.Visible = False
            'Table2.Visible = False
        End If


    End Sub

    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        strQuery = "Select AccountName, Status, ErrMEssage AS Message, DateUpdate AS 'Updated Date' from tblaccounts A INNER JOIN AdminSecureweb.dbo.tblBillgenerateddetails B ON A.AccountID = B.AccountID where A.AccountID = B.AccountID  and B.BillCycle = '" & DLCycle.SelectedValue & "' and B.BillMonth='" & DLMonth.SelectedValue & "' and B.BillYear = '" & DLYear.SelectedValue & "'     "
        If Not DLAct.SelectedValue = String.Empty Then
            strQuery = strQuery & " and A.ACcountID='" & DLAct.SelectedValue & "' "
        End If
        If Not DLStatus.SelectedValue = String.Empty Then
            strQuery = strQuery & " and status = '" & DLStatus.SelectedValue & "' "
        End If
        strQuery = strQuery & " order by DateUpdate "
       
        Dim SQLCmd1 As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            GridView1.DataSource = DRRec1
            GridView1.DataBind()
            If GridView1.Rows.Count > 0 Then
                Label1.Text = "No Records found"
            Else
                Label1.Text = String.Empty
            End If
        Finally

        End Try

    End Sub
End Class




