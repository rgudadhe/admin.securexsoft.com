
Partial Class LeaveAttendanceMainNew_DutyRosterTemplateAssignment
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Panel2.Visible = False
            For i As Integer = Year(Now) - 3 To Year(Now) + 3
                ddlYear.Items.Add(New ListItem(i, i))
            Next
            ddlYear.Items.Insert(0, New ListItem("--Select--", String.Empty))
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim varSearchText As String = Request.Form("txtTemplateName").ToString & "%"
        Dim clsDT As ETS.BL.DutyRosterTemplates
        Dim DS As New Data.DataSet
        Dim DV As New Data.DataView
        Try
            GrdViewMain.DataSource = Nothing
            GrdViewMain.DataBind()
            Panel2.Visible = False
            clsDT = New ETS.BL.DutyRosterTemplates
            clsDT.ContractorID = Session("ContractorID").ToString
            DS = clsDT.getDutyRosterTemplatesList
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DS.Tables(0), " (IsDeleted IS NULL OR IsDeleted=0) AND Name LIKE '" & varSearchText.ToString & "'", "", Data.DataViewRowState.CurrentRows)

                    If DV.Count > 0 Then
                        GrdViewMain.DataSource = DV
                        GrdViewMain.DataBind()
                        Panel2.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsDT = Nothing
        End Try
    End Sub
    Protected Function GetWeeklyOffs(ByVal WO As Integer) As String
        Dim varWeeklyOffs As String = String.Empty
        Dim varLst As New ListBox
        varLst.Items.Add(New ListItem("Monday", 1))
        varLst.Items.Add(New ListItem("Tuesday", 2))
        varLst.Items.Add(New ListItem("Wenesday", 4))
        varLst.Items.Add(New ListItem("Thrusday", 8))
        varLst.Items.Add(New ListItem("Friday", 16))
        varLst.Items.Add(New ListItem("Saturday", 32))
        varLst.Items.Add(New ListItem("Sunday", 64))

        If WO > 0 Then
            For i As Integer = 0 To varLst.Items.Count - 1
                If (WO And varLst.Items(i).Value) = varLst.Items(i).Value Then
                    If String.IsNullOrEmpty(varWeeklyOffs) Then
                        varWeeklyOffs = varLst.Items(i).Text.ToString
                    Else
                        varWeeklyOffs = varWeeklyOffs & "," & varLst.Items(i).Text.ToString
                    End If
                End If
            Next
        End If
        Return varWeeklyOffs
    End Function
    Protected Sub BtnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNext.Click
        If String.IsNullOrEmpty(Request.Form("gvradio")) Then
            lblStatus.Text = "Please select template duty roster"
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlMonth.SelectedValue) Then
            lblStatus.Text = "Please select month for duty roster"
            Exit Sub
        End If
        If String.IsNullOrEmpty(ddlYear.SelectedValue) Then
            lblStatus.Text = "Please select year for duty roster"
            Exit Sub
        End If

        Server.Transfer("DutyRosterUserAssignment.aspx")
    End Sub
End Class
