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
            Dim LI As New ListItem
            LI.Text = "All Accounts"
            LI.Value = String.Empty
            DLAct.Items.Add(LI)
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim CmdRec1 As New SqlCommand("SELALLACCOUNTS", New SqlConnection(strConn))
            CmdRec1.CommandType = CommandType.StoredProcedure
            Try
                CmdRec1.Connection.Open()
                Dim DRRec1 As SqlDataReader = CmdRec1.ExecuteReader()
                Dim K As Integer
                K = 0
                If DRRec1.HasRows Then
                    While (DRRec1.Read)
                        Dim LI1 As New ListItem
                        LI1.Text = DRRec1("accountname").ToString
                        LI1.Value = DRRec1("accountid").ToString
                        DLAct.Items.Add(LI1)
                    End While
                End If
                DRRec1.Close()
            Catch ex As Exception
                If CmdRec1.Connection.State = ConnectionState.Open Then
                    CmdRec1.Connection.Close()
                End If
            End Try
        End If
    End Sub







   

    Protected Sub btnsubmit6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit6.Click

        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim sQuery3 As String
        If DLShow.SelectedValue = "A" Then
            If DLAct.SelectedValue = "" Then
                sQuery3 = "SELALLRECORDS1"
            Else
                sQuery3 = "SELALLRECORDS2"
            End If
        Else
            If DLAct.SelectedValue = "" Then
                sQuery3 = "SELALLRECORDS3"
            Else
                sQuery3 = "SELALLRECORDS4"
            End If
        End If


        'Response.Write(sQuery3)
        'Response.End()
        Dim cmdSel As New SqlCommand(sQuery3, New SqlConnection(strConn))
        cmdSel.CommandType = CommandType.StoredProcedure
        If DLAct.SelectedValue <> "" Then
            cmdSel.Parameters.Add("@accid", SqlDbType.UniqueIdentifier)
            cmdSel.Parameters("@accid").Value = New System.Guid(DLAct.SelectedValue)
        End If
        Try

        cmdSel.Connection.Open()
            Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
            GridView1.DataSource = RdSet
            GridView1.DataBind()
            If DLShow.SelectedValue = "G" Then
                For Each R As GridViewRow In GridView1.Rows
                    ' R.Cells(2).Text = "<a href='ViewUAssignment.aspx?accid=" & R.Cells(0).Text & "&userid=" & R.Cells(1).Text & "'>" & R.Cells(2).Text & "</a>"
                    'R.Cells(2).Text = "<a href='#' onclick='window.open('ViewUAssignment.aspx?accid=" & R.Cells(0).Text & "&userid=" & R.Cells(1).Text & "','OpenRep','width='+screen.width+',height='+screen.height+', scrollbars=1,menubar=0,toolbar=0,location=0,status=0');'>" & R.Cells(2).Text & "</a>"
                    R.Attributes.Add("ondblClick", "javascript:window.open('ViewUAssignment.aspx?accid=" & R.Cells(0).Text & "&userid=" & R.Cells(1).Text & "','OpenRep','width=600,height=500, top=200, left=200, scrollbars=1,menubar=0,toolbar=0,location=0,status=0')")
                Next
                'GridView1.DataBind()
                'GridView1.Columns(0).Visible = True
            End If
        Catch ex As Exception
            If cmdSel.Connection.State = ConnectionState.Open Then
                cmdSel.Connection.Close()
            End If
        End Try

        'RdSet.Close()
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


