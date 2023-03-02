
Partial Class PrevJobsSearch
    Inherits BasePage
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
            Server.Transfer("PrevJobsResult.aspx")
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsAcc As New ETS.BL.Accounts
        'clsAcc.ContractorID = Session("ContractorID").ToString
        'clsAcc._WhereString.Append(" and (IsDeleted is null or IsDeleted =0)")

        Dim dsAccList As Data.DataSet = clsAcc.getAccountList(Session("WorkGroupID"), Session("ContractorId"), String.Empty)
        DDLAccounts.DataSource = dsAccList
        DDLAccounts.DataTextField = "AccountName"
        DDLAccounts.DataValueField = "AccountID"
        DDLAccounts.DataBind()
        Dim LI As New ListItem
        LI.Text = "Please select"
        LI.Value = ""
        DDLAccounts.Items.Insert(0, LI)
        LI.Selected = True
    End Sub

    
End Class
