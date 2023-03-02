Imports MainModule
Partial Class ERSSMainNew_PastTickets
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsERSSTicket As ETS.BL.ERSSTicket
            Dim DS As New Data.DataSet
            Try
                clsERSSTicket = New ETS.BL.ERSSTicket
                DS = clsERSSTicket.GetERSSTicketsListByUsr(Session("UserId").ToString, Session("ContractorID").ToString)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        GridViewhistory.DataSource = DS
                        GridViewhistory.DataBind()
                    End If
                End If

                If GridViewhistory.Rows.Count > 0 Then
                    GridViewhistory.ShowFooter = True
                    GridViewhistory.UseAccessibleHeader = True
                    GridViewhistory.HeaderRow.TableSection = TableRowSection.TableHeader
                    GridViewhistory.FooterRow.TableSection = TableRowSection.TableFooter
                End If
            Catch ex As Exception
            Finally
                clsERSSTicket = Nothing
                DS.Dispose()
            End Try
        End If
    End Sub
End Class
