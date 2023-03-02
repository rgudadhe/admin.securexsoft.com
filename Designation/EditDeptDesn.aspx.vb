Imports System.Data
Partial Class Department_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       

        Dim DSDes As New DataSet
        Dim clsDes As New ETS.BL.Designations
        With clsDes
            DSDes = .getContractorsDesignationListByWorkGroupID(Session("ContractorID").ToString, Session("WorkGroupID").ToString, "", "", String.Empty)
        End With
        clsDes = Nothing
        If DSDes.Tables.Count > 0 Then
            DispData.DataSource = DSDes
            DispData.DataBind()
            DSDes.Dispose()
        Else
            Response.Write("No designation found.")
            DSDes.Dispose()
            Response.End()
        End If
    End Sub
End Class
