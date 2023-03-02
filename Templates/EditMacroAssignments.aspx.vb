Imports System
Imports System.data
Namespace ets
    Partial Class Templates_EditTemplateAssignments
        Inherits BasePage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Dim scriptManager__1 As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            'scriptManager__1.RegisterPostBackControl(Me.btnSave)
            Try

                If Not IsPostBack Then
                    lblCaption.Text = "Macro assignments for " & Request("PhyName") & Request("iRes")
                    hdnPhyID.Value = Request("PhyID")
                    Dim clsPT As New ets.BL.MModalDMAssignments
                    Dim DSPT As DataSet = clsPT.GetAssignedMacrosByPhyID(Request("PhyID"))
                    clsPT = Nothing
                    rptPhyTemp.DataSource = DSPT
                    rptPhyTemp.DataBind()

                    
                    DSPT.Dispose()
                End If

            Catch ex As exception
                lblCaption.Text = ex.StackTrace.ToString
                'Response.Write(ex.InnerException.ToString)
            End Try
        End Sub


        Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim btn As Button
            Dim hdn As HiddenField
            Dim strTempID As String
            btn = CType(sender, Button)
            hdn = btn.Parent.FindControl("McID")
            strTempID = hdn.Value
            If Len(strTempID) = 36 Then
                Dim clsPT As New ets.BL.MModalDMAssignments
                With clsPT
                    If .DeleteMacroDetails(hdnPhyID.Value, hdn.Value) > 0 Then
                        Response.Redirect("EditMacroAssignments.aspx?PhyID=" & hdnPhyID.Value & "&PhyName=" & Request("PhyName") & "&iRes=" & " removed Successfully", True)
                    End If
                End With
                clsPT = Nothing
            End If
        End Sub
    End Class
End Namespace