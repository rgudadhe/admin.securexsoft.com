Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data



Partial Class UserPhyAssgn_Default
    Inherits BasePage




    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            TxtDate1.Text = Request("TXTDate1")
            TxtDate2.Text = Request("TXTDate2")
            
        Else
            Dim Date1 As Date
            Dim Date2 As Date
            Date1 = Now.AddDays(-3).ToShortDateString
            Date2 = Now.AddDays(1).ToShortDateString
            TxtDate1.Text = Date1
            TxtDate2.Text = Date2
            
        End If
        'Session.Abandon()
        lblDetails.Text = ""

        Button1.Visible = False
        If Not Page.IsPostBack Then
            Dim clsAcc As ETS.BL.Accounts
            Dim dsAccList As Data.DataSet
            'clsAcc.ContractorID = Session("ContractorID").ToString
            'clsAcc.DemoConfg = True
            'clsAcc._WhereString.Append(" and (IsDeleted is null or IsDeleted =0)")

            Try
                clsAcc = New ETS.BL.Accounts
                dsAccList = clsAcc.getAccountList(Session("WorkGroupID"), Session("ContractorID"), String.Empty)
                If dsAccList.Tables.Count > 0 Then
                    If dsAccList.Tables(0).Rows.Count > 0 Then
                        ActList.DataSource = dsAccList
                        ActList.DataTextField = "AccountName"
                        ActList.DataValueField = "AccountID"
                        ActList.DataBind()
                    End If
                End If
            Catch ex As Exception
            Finally
                clsAcc = Nothing
                dsAccList = Nothing
            End Try


            
            Dim LI As New ListItem
            LI.Text = "Please select"
            LI.Value = ""
            ActList.Items.Insert(0, LI)
            LI.Selected = True
        End If

    End Sub

    Sub BindSQL()
        Dim SubDate1 As String
        Dim SubDate2 As String
        If TxtDate1.Text <> "" And IsDate(TxtDate1.Text) Then
            SubDate1 = TxtDate1.Text
        Else
            SubDate1 = "1/1/2006"
        End If
        If TxtDate2.Text <> "" And IsDate(TxtDate2.Text) Then
            SubDate2 = TxtDate2.Text
        Else
            SubDate2 = Now()
        End If
        Dim clsDemo As New ETS.BL.Demographics
        Dim DSDemoStatus As DataSet = clsDemo.getAccountsDemoStatus(ActList.SelectedValue.ToString, DLstatus.SelectedValue.ToString, SubDate1, SubDate2, Session("ContractorID").ToString)

        MyDataGrid.DataSource = DSDemoStatus
        MyDataGrid.DataBind() '
    End Sub
    Protected Sub MyDataGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles MyDataGrid.PageIndexChanging
        BindSQL()
    End Sub
    Protected Sub MyDataGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles MyDataGrid.Sorting
        BindSQL()
    End Sub
    Protected Sub Submit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit3.Click
        BindSQL()
    End Sub
End Class

