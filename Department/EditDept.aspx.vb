Imports System.Data
Partial Class Department_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim DSDep As dataset
        Dim clsDep As New ETS.BL.Department

        With clsDep
            '.ContractorID = Session("ContractorID").ToString
            '._WhereString.Append(" and (deleted is null or deleted =0 )")
            'DSDep = .getDepartmentList

            DSDep = .GetDepartmentLstByWrkGroupID(Session("ContractorID"), IIf(Session("WorkGroupID") = Nothing, String.Empty, Session("WorkGroupID")), String.Empty)
        End With
        clsDep = Nothing
        If DSDep.Tables.Count > 0 Then
            DispData.DataSource = DSDep
            DispData.DataBind()
            DSDep.Dispose()
        Else
            Response.Write("No department found.")
            Response.End()
        End If
    End Sub

    Protected Sub DispData_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles DispData.ItemCommand

    End Sub
End Class
