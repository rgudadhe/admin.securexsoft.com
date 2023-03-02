Imports MainModule
Partial Class LeaveAttendanceMainNew_Supervisor_AttendanceApproval
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsAtt As ETS.BL.AttendanceRequest
            Dim DS As New Data.DataSet
            Try
                clsAtt = New ETS.BL.AttendanceRequest
                DS = clsAtt.GetAttendnaceApprovalLstBySupervisorID(Session("UserID"))
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        GridViewAApprove.DataSource = DS
                        GridViewAApprove.DataBind()
                    End If
                End If
                If GridViewAApprove.Rows.Count > 0 Then
                    GridViewAApprove.ShowFooter = True
                    GridViewAApprove.UseAccessibleHeader = True
                    GridViewAApprove.HeaderRow.TableSection = TableRowSection.TableHeader
                    GridViewAApprove.FooterRow.TableSection = TableRowSection.TableFooter
                End If
            Catch ex As Exception
            Finally
                clsAtt = Nothing
                DS.Dispose()
            End Try
        End If
    End Sub
End Class
