Imports MainModule
Partial Class EditDayOff
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varStrID As String
        If Not Page.IsPostBack Then
            varStrID = Request.QueryString("ID")
            Dim clsOff As ETS.BL.NationalOffDays

            Try
                clsOff = New ETS.BL.NationalOffDays
                clsOff.ID = varStrID
                clsOff.getOffDaysDetails()
                txtDate.Text = CDate(clsOff.OffDate).ToShortDateString
                txtDescription.InnerText = clsOff.Description

            Catch ex As Exception
            Finally
                clsOff = Nothing
            End Try
        End If
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim varStrOffDate As String
        Dim varStrDescription As String
        Dim varStrID As String

        varStrOffDate = Trim(Request.Form("txtDate"))
        varStrDescription = Trim(Request.Form("txtDescription"))
        varStrID = Request.QueryString("ID")

        Dim clsOff As ETS.BL.NationalOffDays
        Try
            clsOff = New ETS.BL.NationalOffDays
            clsOff.OffDate = varStrOffDate
            clsOff.Description = varStrDescription
            clsOff.ID = varStrID
            If clsOff.UpdateOffDayDetails = 1 Then
                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Off Date updated sucessfully !!!</font></center>")
                Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            End If
        Catch ex As Exception
        Finally
            clsOff = Nothing
        End Try
    End Sub
    Protected Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Dim varStrID As String

        varStrID = Request.QueryString("ID")

        Dim clsOff As ETS.BL.NationalOffDays
        Try
            clsOff = New ETS.BL.NationalOffDays
            clsOff.ID = varStrID
            If clsOff.DeleteOffDayDetails = 1 Then
                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Off Date deleted sucessfully !!!</font></center>")
                Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            End If
        Catch ex As Exception
        Finally
            clsOff = Nothing

        End Try
    End Sub
End Class
