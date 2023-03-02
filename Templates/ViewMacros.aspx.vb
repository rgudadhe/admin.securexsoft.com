Imports System.Data
Imports System.Data.SqlClient
Partial Class ViewMacro
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            Dim queryString As String = "SELECT Distinct M.McID,  A.AccountName, M.McName ,  IsNull(M.Description, M.McName) as Description, u.First + ' ' + u.Last AS UName, MS.DateUpdated, convert(varchar(50),MS.mcid) + '|' + convert(varchar(10), MS.version) + '|' + convert(varchar(50),a.AccountID) AS McDet from tblMacroFinalStatus MS, tblMModal_Macros M, tblMModal_DictatorMacros M1, tblphysicians P, tblAccounts A, secureweb.dbo.tblusers U where MS.userid=U.userid and M.McID = M1.McID and p.PhysicianID = M1.DictatorID AND MS.McID = M.McID and (MS.Processed = 0 OR MS.Processed IS NULL) AND P.AccountID =A.AccountID  Order by A.AccountName   "
            Dim oconn As New SqlConnection(strConn)
            oconn.Open()
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(queryString, oconn)

            Dim customers As New DataSet

            adapter.Fill(customers, "Customers")

            DispData.DataSource = customers.Tables(0)
            DispData.DataBind()
            oconn.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
