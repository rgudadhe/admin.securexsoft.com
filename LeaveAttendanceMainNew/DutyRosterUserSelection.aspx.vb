Partial Class LeaveAttendanceMainNew_DutyRosterUserSelection
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblStatus.Text = String.Empty
        If Not Page.IsPostBack Then
            Panel2.Visible = False
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GrdViewUsers.DataSource = Nothing
        GrdViewUsers.DataBind()
        Dim varDeptID As String = String.Empty
        varDeptID = GetDeptID()

        Dim varStrSuper As String = String.Empty
        Dim varTemp As String = String.Empty
        If Not String.IsNullOrEmpty(varDeptID) Then
            Dim clsDS As ETS.BL.DeptSuperVisor
            Dim DSSup As New Data.DataSet
            Dim DVSup As New Data.DataView
            Dim DVSupervisor As New Data.DataView
            Try
                clsDS = New ETS.BL.DeptSuperVisor
                DSSup = clsDS.getDepSuperVisorsList()

                If DSSup.Tables.Count > 0 Then
                    If DSSup.Tables(0).Rows.Count > 0 Then
                        DVSup = New Data.DataView(DSSup.Tables(0), "SuperVisorID='" & Session("UserID").ToString & "' AND LevelNo > 1", String.Empty, Data.DataViewRowState.CurrentRows)
                        If DVSup.Count > 0 Then
                            For Each drv As Data.DataRowView In DVSup
                                If String.IsNullOrEmpty(varTemp.ToString) Then
                                    varTemp = "'" & drv("DepartmentID").ToString & "'"
                                Else
                                    varTemp = varTemp & ",'" & drv("DepartmentID").ToString & "'"
                                End If
                                DVSupervisor = New Data.DataView(DSSup.Tables(0), "DepartmentID='" & drv("DepartmentID").ToString & "' AND LevelNo <" & drv("LevelNo") & "", String.Empty, Data.DataViewRowState.CurrentRows)
                                If DVSupervisor.Count > 0 Then
                                    For Each drvs As Data.DataRowView In DVSupervisor
                                        If String.IsNullOrEmpty(varStrSuper) Then
                                            varStrSuper = "'" & drvs("SupervisorID").ToString & "'"
                                        Else
                                            varStrSuper = varStrSuper & ",'" & drvs("SupervisorID").ToString & "'"
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    End If
                End If


            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsDS = Nothing
                DSSup.Dispose()
                DVSup.Dispose()
                DVSupervisor.Dispose()
            End Try

            If Not String.IsNullOrEmpty(varDeptID.ToString) Then
                varDeptID = "'" & varDeptID.ToString & "'," & varTemp.ToString
            End If

            Dim varSearch As String = String.Empty

            If Not String.IsNullOrEmpty(varStrSuper) Then
                varSearch = "  AND (U.DepartmentID IN (" & varDeptID & ") OR U.UserID IN (" & varStrSuper.ToString & ")) AND U.FirstName +' '+U.LastName LIKE'" & Request.Form("txtEName") & "%' AND (U.UserID NOT IN ('" & Session("UserID").ToString & "')) "
            Else
                varSearch = "  AND (U.DepartmentID IN (" & varDeptID & ")) AND U.FirstName +' '+U.LastName LIKE'" & Request.Form("txtEName") & "%' AND (U.UserID NOT IN ('" & Session("UserID").ToString & "')) "
            End If

            'Response.Write(varSearch.ToString)
            Dim clsUsr As ETS.BL.Users
            Dim DS As New Data.DataSet
            Dim DV As New Data.DataView
            Try
                clsUsr = New ETS.BL.Users
                clsUsr.ContractorID = Session("ContractorID").ToString
                clsUsr.DepartmentID = varDeptID.ToString
                DS = clsUsr.getUsersList(Session("ContractorID").ToString, Session("WorkGroupID").ToString, varSearch.ToString)
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        DS.Tables(0).Columns.Add(New Data.DataColumn("EName", GetType(System.String), "FirstName +' '+LastName"))
                        GrdViewUsers.DataSource = DS.Tables(0)
                        GrdViewUsers.DataBind()
                        Panel2.Visible = True
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsUsr = Nothing
            End Try
        Else
            lblStatus.Text = "You have no any department assigned!!"
            Exit Sub
        End If
    End Sub
    Protected Function GetDeptID() As String
        'Session("UserId") = "76D1FBAD-499D-466D-A56E-AE22FB509C21"
        Dim varReturn As String = String.Empty
        Dim clsUsr As ETS.BL.Users
        Try
            clsUsr = New ETS.BL.Users(Session("UserID").ToString)
            varReturn = clsUsr.DepartmentID.ToString
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsr = Nothing
        End Try
        Return varReturn
    End Function
    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim varStrIds As String = String.Empty
        For Each gvr As GridViewRow In GrdViewUsers.Rows
            Dim chk As CheckBox = DirectCast(gvr.FindControl("ChkUsr"), CheckBox)
            Dim hdnUser As HiddenField
            If Not chk Is Nothing Then
                If chk.Checked = True Then
                    hdnUser = DirectCast(gvr.FindControl("hdnUserID"), HiddenField)
                    If Not hdnUser Is Nothing Then
                        If String.IsNullOrEmpty(varStrIds) Then
                            varStrIds = hdnUser.Value.ToString
                        Else
                            varStrIds = varStrIds & "," & hdnUser.Value.ToString
                        End If
                    End If
                End If
            End If
        Next
        hdnUserIds.Value = varStrIds.ToString
        Server.Transfer("DutyRosterUserAssignment.aspx")
    End Sub
End Class
