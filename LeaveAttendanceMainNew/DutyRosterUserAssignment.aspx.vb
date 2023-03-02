
Partial Class LeaveAttendanceMainNew_DutyRosterUserAssignment
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblStatus.Text = String.Empty
        If Not Page.IsPostBack Then
            Dim hdnTemp As New HiddenField
            hdnTemp = DirectCast(PreviousPage.FindControl("hdnUserIds"), HiddenField)
            If Not hdnTemp Is Nothing Then
                hdnSelectedUsers.Value = hdnTemp.Value.ToString
                BindSelectedUsers()
            End If
        End If
    End Sub
    Protected Sub BindSelectedUsers()
        GrdSelUsers.DataSource = Nothing
        GrdSelUsers.DataBind()
        Dim varWhere As String = String.Empty
        varWhere = "'" & Replace(hdnSelectedUsers.Value.ToString, ",", "','") & "'"


        If Not String.IsNullOrEmpty(varWhere) Then
            Dim varSearch As String = " AND U.UserID IN (" & varWhere.ToString & ")"
            Dim clsUsr As ETS.BL.Users
            Dim DS As New Data.DataSet
            Dim DV As New Data.DataView
            Try
                clsUsr = New ETS.BL.Users
                DS = clsUsr.getUsersList(Session("ContractorID").ToString, Session("WorkGroupID").ToString, varSearch)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        DS.Tables(0).Columns.Add(New Data.DataColumn("EName", GetType(System.String), "FirstName +' '+LastName"))
                        GrdSelUsers.DataSource = DS.Tables(0)
                        GrdSelUsers.DataBind()
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsUsr = Nothing
            End Try
        End If
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
    Protected Sub btnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        Dim varSDate As Date = CDate(sDate.Text.ToString)
        Dim varEDate As Date = CDate(eDate.Text.ToString)

        If DateDiff(DateInterval.Day, varSDate, varEDate) < 0 Then
            lblStatus.Text = "startdate should be less than end date"
            Exit Sub
        Else
            If Trim(UCase(ddlWOff1.SelectedValue.ToString)) = Trim(UCase(ddlWOff2.SelectedValue.ToString)) Then
                lblStatus.Text = "Both weekly off should be on different day"
                Exit Sub
            Else
                Try
                    Dim DT As New Data.DataTable
                    DT.Columns.Add(New Data.DataColumn("UserID"))
                    Dim varStrIds As String = hdnSelectedUsers.Value.ToString
                    If Not String.IsNullOrEmpty(varStrIds) Then
                        Dim varStrSplit() = Split(varStrIds, ",")
                        For i As Integer = 0 To UBound(varStrSplit)
                            Dim DR As Data.DataRow = DT.NewRow
                            DR("UserID") = varStrSplit(i).ToString
                            DT.Rows.Add(DR)
                        Next
                    End If
                    If DT.Rows.Count > 0 Then
                        Dim clsDR As ETS.BL.DutyRoster
                        Try
                            clsDR = New ETS.BL.DutyRoster
                            If clsDR.btn_UploadDutyRoster(DT, ddlWOff1.SelectedValue.ToString, ddlWOff2.SelectedValue.ToString, ddlShift.SelectedValue.ToString, sDate.Text.ToString, eDate.Text.ToString, Session("UserID").ToString) = True Then
                                lblStatus.Text = "Duty Roster updated..."
                            Else
                                lblStatus.Text = "Duty Roster updation failed!!!"
                            End If
                        Catch ex As Exception
                            Response.Write(ex.Message)
                        Finally
                            clsDR = Nothing
                        End Try
                    End If
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub
End Class
