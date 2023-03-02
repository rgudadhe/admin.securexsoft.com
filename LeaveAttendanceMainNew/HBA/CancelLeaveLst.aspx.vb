
Partial Class LeaveAttendanceMainNew_HBA_CancelLeaveLst
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsLeave As ETS.BL.Leave
            Dim Ds As New Data.DataSet
            Try
                clsLeave = New ETS.BL.Leave
                Ds = clsLeave.getLeaveCancellstByUsr(Session("UserID").ToString)
                If Ds.Tables.Count > 0 Then
                    If Ds.Tables(0).Rows.Count > 0 Then
                        GridViewCancel.DataSource = Ds
                        GridViewCancel.DataBind()
                    End If
                End If
                If GridViewCancel.Rows.Count > 0 Then
                    GridViewCancel.ShowFooter = True
                    GridViewCancel.UseAccessibleHeader = True
                    GridViewCancel.HeaderRow.TableSection = TableRowSection.TableHeader
                    GridViewCancel.FooterRow.TableSection = TableRowSection.TableFooter
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsLeave = Nothing
                Ds.Dispose()
            End Try
        End If
    End Sub
End Class
