Imports MainModule
Partial Class LeaveAttendanceMainNew_Supervisor_LeaveApproval
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Response.Write(Session("UserID").ToString)
            Dim clsLeave As ETS.BL.Leave
            Dim DS As New Data.DataSet
            Try
                clsLeave = New ETS.BL.Leave
                DS = clsLeave.GetLeaveApprovalLstBySupervisorID(Session("UserID").ToString, Session("ContractorID").ToString)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        GridViewLApprove.DataSource = DS
                        GridViewLApprove.DataBind()
                    End If
                End If
                If GridViewLApprove.Rows.Count > 0 Then
                    GridViewLApprove.ShowFooter = True
                    GridViewLApprove.UseAccessibleHeader = True
                    GridViewLApprove.HeaderRow.TableSection = TableRowSection.TableHeader
                    GridViewLApprove.FooterRow.TableSection = TableRowSection.TableFooter
                End If
            Catch ex As Exception
            Finally
                clsLeave = Nothing
                DS.Dispose()
            End Try
        End If
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        objMainModule = Nothing
    End Sub
End Class
