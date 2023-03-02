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
                    lblCaption.Text = "Template assignments for " & Request("PhyName") & Request("iRes")
                    hdnPhyID.Value = Request("PhyID")
                    Dim clsPT As New ets.BL.PhysiciansTempaltes
                    Dim DSPT As DataSet = clsPT.getPhysiciansTemplatesList(Request("PhyID"))
                    clsPT = Nothing
                    rptPhyTemp.DataSource = DSPT
                    rptPhyTemp.DataBind()

                    For Each DR As DataRow In DSPT.Tables(0).Rows
                        For Each ctl As RepeaterItem In rptPhyTemp.Items
                            Dim ddl As DropDownList
                            Dim hdn As HiddenField = ctl.FindControl("TemplateID")
                            If hdn.Value = DR("TemplateID").ToString Then
                                ddl = New DropDownList
                                ddl = hdn.Parent.FindControl("DDLTAT")
                                ddl.Items.FindByValue(DR("TAT").ToString).Selected = True
                                If Not IsDBNull(DR("TZDifference")) Then
                                    ddl = New DropDownList
                                    ddl = hdn.Parent.FindControl("DDLTZ")
                                    ddl.Items.FindByValue(DR("TZDifference").ToString).Selected = True
                                End If
                            End If
                        Next
                    Next
                    DSPT.Dispose()
                End If

            Catch ex As exception
                lblCaption.Text = ex.StackTrace.ToString
                'Response.Write(ex.InnerException.ToString)
            End Try
        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim ires As Integer
            For Each ctl As RepeaterItem In rptPhyTemp.Items
                Dim txt As TextBox
                Dim ddl As DropDownList
                Dim strTempID As String
                Dim intTAT As Integer
                Dim intTime As Integer
                Dim intTZDeference As Integer
                Dim intSTAT As Integer
                Dim intWT As Integer
                Dim hdn As HiddenField = ctl.FindControl("TemplateID")
                strTempID = hdn.Value
                ddl = hdn.Parent.FindControl("DDLTAT")
                If Not String.IsNullOrEmpty(ddl.SelectedValue) Then
                    If IsNumeric(ddl.SelectedValue) Then
                        intTAT = CInt(ddl.SelectedValue)
                    End If
                End If
                txt = hdn.Parent.FindControl("txtTime")
                If Not String.IsNullOrEmpty(txt.Text) Then
                    If IsNumeric(txt.Text) Then
                        intTime = CInt(txt.Text)
                    End If
                End If
                txt = hdn.Parent.FindControl("txtSTAT")
                If Not String.IsNullOrEmpty(txt.Text) Then
                    If IsNumeric(txt.Text) Then
                        intSTAT = CInt(txt.Text)
                    End If
                End If
                ddl = hdn.Parent.FindControl("DDLTZ")
                If Not String.IsNullOrEmpty(ddl.SelectedValue) Then
                    If IsNumeric(ddl.SelectedValue) Then
                        intTZDeference = CInt(ddl.SelectedValue)
                    End If
                End If
                txt = hdn.Parent.FindControl("txtWT")
                If Not String.IsNullOrEmpty(txt.Text) Then
                    If IsNumeric(txt.Text) Then
                        intWT = CInt(txt.Text)
                    End If
                End If
                If Len(strTempID) = 36 Then
                    Dim clsPT As New ets.BL.PhysiciansTempaltes
                    With clsPT
                        .PhysicianID = hdnPhyID.Value
                        .TemplateID = strTempID
                        .TAT = intTAT
                        .Time = intTime
                        .STAT = intSTAT
                        .TZDifference = intTZDeference
                        .WorkType = intWT
                        ires = .UpdatePhysicianTemplates()
                    End With
                    clsPT = Nothing
                End If
            Next
            Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & hdnPhyID.Value & "&PhyName=" & Request("PhyName") & "&iRes=" & IIf(ires > 0, " saved Successfully", " failed"), True)
        End Sub

        Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim btn As Button
            Dim hdn As HiddenField
            Dim strTempID As String
            btn = CType(sender, Button)
            hdn = btn.Parent.FindControl("TemplateID")
            strTempID = hdn.Value
            If Len(strTempID) = 36 Then
                Dim clsPT As New ets.BL.PhysiciansTempaltes
                With clsPT
                    .PhysicianID = hdnPhyID.Value
                    .TemplateID = strTempID
                    If .DeletePhysicianTemplate() > 0 Then
                        Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & hdnPhyID.Value & "&PhyName=" & Request("PhyName") & "&iRes=" & " removed Successfully", True)
                    End If
                End With
                clsPT = Nothing
            End If
        End Sub
    End Class
End Namespace