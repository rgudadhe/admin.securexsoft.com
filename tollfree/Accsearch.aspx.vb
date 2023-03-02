Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Partial Class tollfree_Accsearch
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim conString As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
            Dim query As String = "Select distinct accname, custid from tbltollfree"
            Using con As SqlConnection = New SqlConnection(conString)
                Dim cmd As SqlCommand = New SqlCommand(query)
                Using sda As SqlDataAdapter = New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dt As DataTable = New DataTable()
                        sda.Fill(dt)

                        ddlCustomers.DataTextField = "accname"
                        ddlCustomers.DataValueField = "custid"
                        ddlCustomers.DataSource = dt
                        ddlCustomers.DataBind()
                    End Using
                End Using
            End Using
        End If
    End Sub

    'Protected Sub OnSubmit(ByVal sender As Object, ByVal e As EventArgs)
    '    ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "alert('CustomerID : " & ddlCustomers.Value & "')", True)
    'End Sub

End Class
