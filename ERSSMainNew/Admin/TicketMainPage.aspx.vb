Imports MainModule
Imports System.Data
Partial Class TicketMainPage
    Inherits BasePage
    Dim objMainModule As New MainModule
    Dim gvUniqueID As String
    Dim gvSortExpr As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                BindData("Open")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub GridViewTickets_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewTickets.RowCommand
        Try
            If e.CommandName = "ActionTicket" Then
                Dim varStrSplit() As String
                varStrSplit = Split(Trim(e.CommandArgument), "#")

                Dim varStrTicketID As String
                varStrTicketID = Trim(varStrSplit(0))

                Dim varStrForward As String
                varStrForward = Trim(varStrSplit(1))

                Response.Redirect("ActionTicket.aspx?ID=" & varStrTicketID & "&From=Active&Forward=" & varStrForward & "")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub BindData(ByVal Status As String)
        Dim clsERSS As ETS.BL.ERSS
        Dim DS As New Data.DataSet
        Dim DV As Data.DataView
        Try
            Dim varStrStatus As String = String.Empty
            varStrStatus = Status

            If Trim(UCase(Status)) = Trim(UCase("Open")) Then
                varStrStatus = " AND T.Status='Open' "
            ElseIf Trim(UCase(Status)) = Trim(UCase("Close")) Then
                varStrStatus = " AND T.Status='Close' "
            End If

            ViewState("Status") = Status

            clsERSS = New ETS.BL.ERSS
            DS = clsERSS.GetERSSActiveTickets(Session("UserID").ToString, Session("ContractorID").ToString)
            DV = New Data.DataView(DS.Tables(0))
            If Not String.IsNullOrEmpty(Status) Then
                DV.RowFilter = " Status='" & Status & "' "
            End If


            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    GridViewTickets.DataSource = DV
                    GridViewTickets.DataBind()
                    If GridViewTickets.Rows.Count < 0 Then
                        lblTickets.Text = "No Tickets availble !!!"
                        lblTickets.Visible = True
                    Else
                        lblTickets.Text = ""
                        lblTickets.Visible = False
                        GridViewTickets.ShowFooter = True
                        GridViewTickets.UseAccessibleHeader = True
                        GridViewTickets.HeaderRow.TableSection = TableRowSection.TableHeader
                        GridViewTickets.FooterRow.TableSection = TableRowSection.TableFooter
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsERSS = Nothing
            DS.Dispose()
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim varstrValue As String = String.Empty
        varstrValue = ddlStatus.Items(ddlStatus.SelectedIndex).Value.ToString
        ViewState("Status") = varstrValue

        BindData(varstrValue)
    End Sub
    Protected Function ValidateString(ByVal [String] As Object) As String
        If Not String.IsNullOrEmpty([String].ToString) Then
            If ([String].ToString().Length > 100) Then
                Dim varStr As String = String.Empty
                varStr = CStr([String].ToString())
                varStr = varStr.Replace(Convert.ToChar(34), " ")
                Return [String].ToString().Substring(0, 100) + " <label style=""font-family:Trebuchet MS; color:Blue; cursor:hand ""  onmouseover=""tip_it('ToolTip','Description','" & varStr.ToString & "'); "" onmouseout=""hideIt('ToolTip');""> more >></label>"
            Else
                Return [String].ToString()
            End If
        Else
            Return String.Empty
        End If
    End Function
End Class
