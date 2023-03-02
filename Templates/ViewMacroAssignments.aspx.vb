Imports System.Data
Imports System.Data.SqlClient
Partial Class MacroAssignments
    Inherits BasePage
   
    Protected Sub BindAccountData()
        Dim DS As New Data.DataSet

        Dim clsAccount As ETS.BL.Accounts
        Try
            clsAccount = New ETS.BL.Accounts
            clsAccount.ContractorID = Session("contractorID").ToString
            DS = clsAccount.getAccountList


            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DDLAcc.DataTextField = "AccountName"
                    DDLAcc.DataValueField = "AccountID"
                    DDLAcc.DataSource = DS
                    DDLAcc.DataBind()
                End If
            End If
            DDLAcc.Items.Insert(0, New ListItem("Please Select", String.Empty))
        Catch ex As exception
            lblMsg.Text = String.Empty
            lblMsg.Text = "Err :" & ex.Message
        Finally
            DS.Dispose()
            clsAccount = Nothing
        End Try
    End Sub
    Protected Sub BindDMData()
        Try
            'Response.Write("started")
            Dim strConn As String
            Dim d As DataSet
            Dim t As New DataTable
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            '' Dim cmdIns As New SqlCommand("SELECT Distinct M.McId,M.McName from tblMModal_Macros M INNER JOIN tblPhysicians P ON M.DictatorID = P.PhysicianID WHERE P.AccountID='" & DDLAcc.SelectedValue & "' Order by M.McName ", New SqlConnection(strConn))
            'Dim cmdIns As New SqlCommand("SELECT Distinct M.McId,IsNull(M.Description, M.McName) as Description from tblMModal_Macros M Order by Description ", New SqlConnection(strConn))
            'cmdIns.Connection.Open()
            'Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
            'd = DRRec
            'DRRec.Close()
            Dim queryString As String = " SELECT P.FirstName + ' ' + P.LastName as 'Physician', IsNull(M.Description, M.McName) as Description from tblMModal_Macros M, tblMModal_DictatorMacros M1, tblphysicians P where M.McID = M1.McID and p.PhysicianID = M1.DictatorID AND P.AccountID ='" & DDLAcc.SelectedValue & "'   Order by P.FirstName, P.LastName, M.Description    "
            Dim oconn As New SqlConnection(strConn)
            oconn.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(queryString, oconn)

            Dim customers As New DataSet

            adapter.Fill(customers, "Customers")
            Response.Write(customers.Tables(0).Rows.Count)
            MyDataGrid.DataSource = customers.Tables(0)
            MyDataGrid.DataBind()
            oconn.Close()
        Catch ex As exception
            Response.Write(ex.message)
        End Try

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindAccountData()
        End If
    End Sub
    
    Protected Sub ddlAcc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLAcc.SelectedIndexChanged
       
        BindDMData()
    End Sub
   
End Class
