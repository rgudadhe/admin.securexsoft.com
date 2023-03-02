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
            Dim queryString As String = "SELECT Distinct M.McID,  A.AccountName, M.FileName ,   u.First + ' ' + u.Last AS UName, M.DateUpdated, convert(varchar(50),M.mcid)  + '|' + convert(varchar(50),a.AccountID) AS McDet from tblMacroNewUpload M,  tblAccounts A, secureweb.dbo.tblusers U where M.userid=U.userid AND (M.Processed = 0 OR M.Processed IS NULL) AND M.AccountID =A.AccountID  Order by A.AccountName   "
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
