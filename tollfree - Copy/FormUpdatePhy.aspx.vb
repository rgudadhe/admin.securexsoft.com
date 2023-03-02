Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.UI.ScriptManager
Partial Class FormUpdatePhy
    Inherits System.Web.UI.Page
    Public acname As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        con.Open()
        Dim hcmd As New SqlCommand("Select distinct accname, custid from tblaccounts order by accname", con)
        Dim hds As New DataSet()
        Dim hda As New SqlDataAdapter(hcmd)
        hda.Fill(hds)
        acct.DataSource = hds
        acct.DataTextField = "accname"
        acct.DataValueField = "custid"
        acct.DataBind()
        con.Close()

    End Sub

    Protected Sub Add(ByVal sender As Object, ByVal e As EventArgs)
        Dim control As Control = Nothing
        If (Not (GridView1.FooterRow) Is Nothing) Then
            control = GridView1.FooterRow
        Else
            control = GridView1.Controls(0).Controls(0)
        End If
        Dim accname As String = CType(control.FindControl("txtaccname"), TextBox).Text
        Dim lname As String = CType(control.FindControl("txtlname"), TextBox).Text
        Dim fname As String = CType(control.FindControl("txtfname"), TextBox).Text
        Dim keypad As String = CType(control.FindControl("txtkeypad"), TextBox).Text
        Dim id As String = CType(control.FindControl("txtid"), TextBox).Text
        Dim password As String = CType(control.FindControl("txtpassword"), TextBox).Text
        Dim ct As String = CType(control.FindControl("txtctype"), TextBox).Text
        Dim cid As String = CType(control.FindControl("txtcustid"), TextBox).Text
        Dim part As String = CType(control.FindControl("txtpartition"), TextBox).Text
        Dim pno As String = CType(control.FindControl("txtpno"), TextBox).Text

        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)
        Try
            con.Open()
            Dim cmd As New SqlCommand("Insert into tbltollfree values('" + accname + "','" + lname + "','" + fname + "','ChartVox','" + keypad + "','" + id + "','" + password + "','" + ct + "','" + cid + "','" + part + "','" + pno + "')", con)

            cmd.ExecuteNonQuery()
            Dim ds As dataset()

            con.Close()
            Response.Write("Record has been updated Successfully")

        Catch ex As Exception

            Response.Write(ex)
        End Try
	Binddata()
    End Sub

    Protected Sub acct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles acct.SelectedIndexChanged

        Dim strcon1 As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con1 As New SqlConnection(strcon1)

        Dim custid As String
        acname = acct.SelectedItem.Text
        'Response.Write(acname)
        con1.Open()
        Dim acmd As New SqlCommand("Select * from tbltollfree where accname='" & acname & "'", con1)
        Dim ads As New DataSet()
        Dim ada As New SqlDataAdapter(acmd)
        ada.Fill(ads)
        GridView1.DataSource = ads
        GridView1.DataBind()
        con1.close()
		BindData()
		GridView1.ShowFooter = true
    End Sub

    
    Protected Sub DeleteCustomer(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs) Handles GridView1.RowDeleting
		Dim varDDL As HiddenField
        Dim varid As String
        varDDL = DirectCast(GridView1.Rows(e.RowIndex).FindControl("iddel"), HiddenField)
        varid = varDDL.Value
        Response.Write(varid)
        
    End Sub
    Private Sub BindData()
        Dim strcon1 As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con1 As New SqlConnection(strcon1)
        acname = acct.SelectedItem.Text
        con1.Open()
        Dim acmd As New SqlCommand("Select * from tbltollfree where accname='" & acname & "'", con1)
        Dim ads As New DataSet()
        Dim ada As New SqlDataAdapter(acmd)
        ada.Fill(ads)
        GridView1.DataSource = ads
        GridView1.DataBind()
        ' con1.Close()
    End Sub
	
	Protected Sub GridView1_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridView1.RowEditing
        GridView1.EditIndex = e.NewEditIndex
        BindData()
    End Sub
	
	Protected Sub GridView1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridView1.RowUpdating
        Dim varlname As String
        Dim varfname As String
        Dim varkeypad As String
        Dim varid As Integer
        Dim varpassword As Integer
        Dim varpartition As String
        Dim varpno As Integer
        Dim sqlq As String

        Dim vlname As TextBox
        Dim vfname As TextBox
        Dim vkeypad As TextBox
        Dim vid As TextBox
        Dim vpassword As TextBox
        Dim vpartition As TextBox
        Dim vpno As TextBox

        vlname = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtlname"), TextBox)
        vfname = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtfname"), TextBox)
        vkeypad = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtkeypad"), TextBox)
        vid = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtid"), TextBox)
        vpassword = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtpassword"), TextBox)
        vpartition = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtpartition"), TextBox)
        vpno = DirectCast(GridView1.Rows(e.RowIndex).FindControl("txtpno"), TextBox)

        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Dim con As New SqlConnection(strcon)


        sqlq = "Update tbltollfree set diclname='" & vlname.Text & "',dicfname='" & vfname.Text & "',keypad='" & vkeypad.Text & "',id='" & vid.Text & "',password='" & vpassword.Text & "',partition='" & vpartition.Text & "',partitionno='" & vpno.Text & "' where id= '" & vid.Text & "'"
        con.Open()
        Dim cmd As New SqlCommand(sqlq, con)
        cmd.ExecuteNonQuery()
		GridView1.EditIndex = -1
		Response.write ("Record Updated Successfully!")
        BindData()
    End Sub
	
	Protected Sub GridView1_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridView1.RowCancelingEdit
        GridView1.EditIndex = -1
        BindData()
    End Sub
	
	
End Class
