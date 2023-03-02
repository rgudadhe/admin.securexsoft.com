Imports MainModule
Partial Class LeaveAttendanceMainNew_Supervisor_CancelApproval
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsLeave As ETS.BL.Leave
            Dim DS As New Data.DataSet
            Try
                clsLeave = New ETS.BL.Leave
                DS = clsLeave.GetCancelLeaveApprovalLstBySupervisorID(Session("UserID"))
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        GridViewCApprove.DataSource = DS
                        GridViewCApprove.DataBind()
                    End If
                End If
                If GridViewCApprove.Rows.Count > 0 Then
                    GridViewCApprove.ShowFooter = True
                    GridViewCApprove.UseAccessibleHeader = True
                    GridViewCApprove.HeaderRow.TableSection = TableRowSection.TableHeader
                    GridViewCApprove.FooterRow.TableSection = TableRowSection.TableFooter
                End If
            Catch ex As Exception
            Finally
                clsLeave = Nothing
                DS.Dispose()
            End Try
        End If
    End Sub
End Class
