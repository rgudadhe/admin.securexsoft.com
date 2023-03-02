Imports MainModule
Partial Class AddDayOff
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim varStrOffDate As String
        Dim varStrDescription As String

        varStrOffDate = Trim(Request.Form("txtDate"))
        varStrDescription = Trim(Request.Form("txtDescription"))

        Dim clsOff As ETS.BL.NationalOffDays
        Try
            clsOff = New ETS.BL.NationalOffDays
            clsOff.ContractorID = Session("ContractorID").ToString
            clsOff.OffDate = varStrOffDate
            clsOff.Description = varStrDescription
            If clsOff.InsertOffDayDetails = 1 Then
                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Off Date added sucessfully !!!</font></center>")
                Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            End If
        Catch ex As Exception
        Finally
            clsOff = Nothing
        End Try
    End Sub
End Class
