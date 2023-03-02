Imports System.Data.Sqlclient
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.Security

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim clsMTU As ETS.BL.MTDirectUserAssignments
            Dim Ds As New Data.DataSet

            Try
                clsMTU = New ETS.BL.MTDirectUserAssignments
                Ds = clsMTU.GetUsrLstForUsrAssignment(Session("WorkGroupID"), Session("ContractorID"), Session("IntialProductionLevel").ToString)
                If Ds.Tables.Count > 0 Then
                    If Ds.Tables(0).Rows.Count > 0 Then
                        Ds.Tables(0).Columns.Add("uname", Type.GetType("System.String"), "FirstName + ' '+ LastNAme")
                        DLUser.DataSource = Ds.Tables(0)
                        DLUser.DataTextField = "uname"
                        DLUser.DataValueField = "Userid"
                        DLUser.DataBind()
                    End If
                End If
                DLUser.Items.Insert(0, New ListItem("All Users", String.Empty))
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsMTU = Nothing
                Ds = Nothing
            End Try
        End If
    End Sub
    Protected Sub btnsubmit6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit6.Click
        Dim clsMTU As ETS.BL.MTDirectUserAssignments
        Dim Ds As New Data.DataSet
        Try
            clsMTU = New ETS.BL.MTDirectUserAssignments
            If DLShow.SelectedValue = "A" Then
                If DLUser.SelectedValue = "" Then
                    Ds = clsMTU.GetAllUsrAssignment(Session("WorkGroupID"), Session("ContractorID"), Session("IntialProductionLevel").ToString)
                Else
                    Ds = clsMTU.GetAllUsrAssignmentForUsr(DLUser.SelectedValue.ToString, Session("ContractorID"), Session("IntialProductionLevel").ToString)
                End If
            Else
                If DLUser.SelectedValue = "" Then
                    Ds = clsMTU.GetAllUsrAssignmentGroupByAcc(Session("WorkGroupID"), Session("ContractorID"), Session("IntialProductionLevel").ToString)
                Else
                    Ds = clsMTU.GetAllUsrAssignmentGroupByAccForUsr(DLUser.SelectedValue.ToString, Session("ContractorID"), Session("IntialProductionLevel").ToString)
                End If
            End If

            GridView1.DataSource = Ds.Tables(0)
            GridView1.DataBind()
            If DLShow.SelectedValue = "G" Then
                For Each R As GridViewRow In GridView1.Rows
                    R.Attributes.Add("ondblClick", "javascript:window.open('ViewUAssignment.aspx?accid=" & R.Cells(0).Text & "&userid=" & R.Cells(1).Text & "','OpenRep','width=600,height=500, top=200, left=200, scrollbars=1,menubar=0,toolbar=0,location=0,status=0')")
                Next
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsMTU = Nothing
            Ds = Nothing
        End Try

        If RB.SelectedValue = "E" Then
            ExpResult()
        End If
    End Sub
 
    Sub ExpResult()
        Dim filename As String
        filename = "View Account Assignment " & Month(Now) & Day(Now) & Year(Now) & ".xls"
        Dim attachment As String = "attachment; filename=" & filename
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)

        GridView1.RenderControl(htw)
        'MyDataGrid.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET 
        ' server control at run time. 
    End Sub

    
    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If DLShow.SelectedValue = "G" Then
            If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header OrElse e.Row.RowType = DataControlRowType.Footer Then
                e.Row.Cells(1).Visible = False
                e.Row.Cells(0).Visible = False
            End If
        End If
    End Sub
End Class


