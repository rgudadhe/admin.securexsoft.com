Imports MainModule
Partial Class LeavePolicyAddEdit
    Inherits System.Web.UI.Page
    Dim objMainModule As New MainModule
    Dim varStrOpr As String
    Dim varStrTrackID As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Request.QueryString("TrackID") <> "" Then
                Dim varStrTrackIDQS As String
                varStrTrackIDQS = Request.QueryString("TrackID")
                ViewState("varStrTrackID") = Request.QueryString("TrackID")
                lblPolicy.Text = "EDIT Policy"
                ViewState("varStrOpr") = "Edit"
                BtnDelete.Visible = True
                Dim clsLP As ETS.BL.LeavePolicy
                Try
                    clsLP = New ETS.BL.LeavePolicy
                    clsLP.TrackID = varStrTrackIDQS
                    clsLP.getLeavePolicyDetails()
                    txtDay.Text = CInt(clsLP.Day)
                    DropDownMonth.Items.FindByValue(Trim(clsLP.Month.ToString)).Selected = True
                    txtCL.Text = clsLP.CL
                    txtEL.Text = clsLP.EL

                Catch ex As Exception

                Finally
                    clsLP = Nothing
                End Try
            Else
                lblPolicy.Text = "ADD Policy"
                ViewState("varStrOpr") = "Add"
            End If
        End If
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim clsLP As ETS.BL.LeavePolicy
        Dim Ret_Val As Integer = 0
        Try
            clsLP = New ETS.BL.LeavePolicy

            If Trim(UCase(ViewState("varStrOpr"))) = Trim(UCase("Add")) Then
                clsLP.Day = txtDay.Text
                clsLP.Month = DropDownMonth.Items(DropDownMonth.SelectedIndex).Value.ToString
                clsLP.CL = txtCL.Text
                clsLP.EL = txtEL.Text
                clsLP.ContractorID = Session("ContractorID").ToString
                clsLP.UpdatedBy = Session("UserID").ToString
                clsLP.UpdatedOn = Now
                Ret_Val = clsLP.InsertLeavePolicyDetails()
            ElseIf Trim(UCase(ViewState("varStrOpr"))) = Trim(UCase("Edit")) Then
                clsLP.Day = txtDay.Text
                clsLP.Month = DropDownMonth.Items(DropDownMonth.SelectedIndex).Value.ToString
                clsLP.CL = txtCL.Text
                clsLP.EL = txtEL.Text
                clsLP.ContractorID = Session("ContractorID").ToString
                clsLP.UpdatedBy = Session("UserID").ToString
                clsLP.UpdatedOn = Now
                clsLP._WhereString.Remove(0, clsLP._WhereString.Length)
                clsLP._WhereString.Append(" WHERE TrackID='" & ViewState("varStrTrackID") & "' ")
                Ret_Val = clsLP.UpdateLeavePolicyDetails()
            End If

            If Ret_Val = 1 Then
                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Leave Policy updated sucessfully. !!!</font></center>")
                Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            End If
        Catch ex As Exception
        Finally
            clsLP = Nothing
        End Try
    End Sub
    Protected Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Dim clsLP As ETS.BL.LeavePolicy
        Try
            clsLP = New ETS.BL.LeavePolicy
            clsLP.TrackID = ViewState("varStrTrackID")
            If clsLP.DeleteLeavePolicyDetails() = 1 Then
                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Leave Policy deleted sucessfully. !!!</font></center>")
                Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            End If
        Catch ex As Exception
        Finally
            clsLP = Nothing
        End Try
    End Sub
End Class
