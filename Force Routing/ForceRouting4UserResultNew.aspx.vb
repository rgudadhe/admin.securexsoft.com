
Partial Class FRUResult
    Inherits BasePage
    Public strUserName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
                iMain.Visible = True
                DBind()
            ElseIf IsPostBack Then
                iMain.Visible = True
            Else
                iMain.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message & "hh")
        End Try
    End Sub
    Private Sub DBind()
        Try
            If IsPostBack = False And String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
                iMain.Visible = True
                Session("WorkgroupID") = "3BC5ABE9-8EDC-4CCD-8FD3-C8A001ED8083"
                Dim varUName As String = String.Empty
                Dim varUID As String = String.Empty
                Dim varULevel As String = String.Empty

                If Not String.IsNullOrEmpty(Request("UName")) Then
                    varUName = Request("UName").ToString
                End If

                If Not String.IsNullOrEmpty(Request("UID")) Then
                    varUID = Request("UID").ToString
                End If

                If Not String.IsNullOrEmpty(Request("ULevel")) Then
                    hdnSelUserLevel.Value = Request("ULevel").ToString
                    varULevel = Request("ULevel").ToString
                End If

                Dim clsCR As ETS.BL.CustomRouting
                Dim DS As New Data.DataSet
                Try
                    clsCR = New ETS.BL.CustomRouting
                    DS = clsCR.ForceRoutingSearchUser(Session("ContractorID"), Session("WorkGroupID"), varUName.ToString, varUID.ToString, varULevel.ToString)
                    If DS.Tables.Count > 0 Then
                        If DS.Tables(0).Rows.Count > 0 Then
                            dlist.DataSource = DS
                            dlist.DataBind()
                        Else
                            iMain.Visible = False
                        End If
                    Else
                        iMain.Visible = False
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try

            End If
        Catch ex As Exception
            Response.Write(ex.Message & " bb")
        End Try
    End Sub
    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        iMain.Visible = False
        idetails.Visible = True
        rptHistory.DataSource = Nothing
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Dim hdn As HiddenField = lnk.Parent.FindControl("hdnUser")
        hdnSelUserID.Value = hdn.Value.ToString
        hdn = lnk.Parent.FindControl("hdnLevels")
        HDNSelUserFRLvl.Value = CLng(hdn.Value.ToString)
        Dim lbl As Label = lnk.Parent.FindControl("lblUser")
        strUserName = lbl.Text


        Dim clsPL As ETS.BL.ProductionLevels
        Dim DS As New Data.DataSet
        Dim DV As New Data.DataView
        Try
            clsPL = New ETS.BL.ProductionLevels
            clsPL.Type = Session("IsContractor")
            clsPL.ForcedRouting = True
            DS = clsPL.getPLevelList

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DS.Tables(0).Columns.Add("LevelNameNew", System.Type.GetType("System.String"), " LevelName+' ('+ Description+')' ")
                    DV = New Data.DataView(DS.Tables(0), String.Empty, "Sequence", Data.DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        rptHistory.DataSource = DV.ToTable
                        rptHistory.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsPL = Nothing
            DS = Nothing
            DV = Nothing
        End Try
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim UpdatedLevel As Long
        Try
            Dim btn As Button = CType(sender, Button)
            For Each item As RepeaterItem In rptHistory.Items
                Dim chk As CheckBox = DirectCast(item.FindControl("ckSelected"), CheckBox)
                If chk.Checked Then
                    Dim hdn As HiddenField = chk.Parent.FindControl("hdnLvlNO")
                    If IsNumeric(hdn.Value) Then
                        UpdatedLevel = UpdatedLevel + CLng(hdn.Value)
                    End If
                End If
            Next

            Dim clsCR As ETS.BL.CustomRouting
            Try
                clsCR = New ETS.BL.CustomRouting
                If clsCR.btn_SubmitClick_From_User(hdnSelUserID.Value.ToString, CLng(hdnSelUserLevel.Value.ToString), UpdatedLevel) Then
                    iMain.Visible = True
                    idetails.Visible = False
                    Response.Write("<script>alert('Force Routing Levels have been successfully set');</script>")
                    DBind()
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsCR = Nothing
            End Try
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        iMain.Visible = True
        idetails.Visible = False
        DBind()
    End Sub
End Class
