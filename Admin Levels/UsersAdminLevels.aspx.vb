Imports System
Imports System.Data
Partial Class Admin_Levels_UsersAdminLevels
    Inherits BasePage
    Public SelectedUserLevel As Long
    Public IsSuperAdmin As Boolean
    Private _flash As Boolean = False
    Dim flashThread As Threading.Thread
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Not IsPostBack Then
            hdnCriOption.Value = Request("CriOption")
            hdnCriUser.Value = Request("CriUser")
            lstBind()
        End If
    End Sub
    Private Function lstBind() As Boolean
        Try
            lstAssignLevels.Items.Clear()
            lstAvailLevels.Items.Clear()
            Dim SelectedAdminLevel As Long
            Try
                Dim clsUPL As New ETS.BL.UserLevels
                With clsUPL
                    .UserID = Request("UserID")
                    .getUserLevelDetails()
                    SelectedUserLevel = IIf(IsDBNull(.AdminLevel), 0, .AdminLevel)
                End With
                clsUPL = Nothing
                lblUser.Text = "Access Levels for <b>" & Request("UserName")
                Dim dsAL As New DataSet
                Dim clsAL As New ETS.BL.AdminLevels
                With clsAL
                    .IsDeleted = False
                    dsAL = .getAdminLevelList
                End With
                clsAL = Nothing
                Dim clsAdmLvl As New ETS.BL.UserLevels
                With clsAdmLvl
                    .UserID = Session("UserID")
                    .getUserLevelDetails()
                    SelectedAdminLevel = IIf(IsDBNull(.AdminLevel), 0, .AdminLevel)
                End With
                clsAdmLvl = Nothing
                If SelectedAdminLevel = 2147483647 Then
                    hdnIsSuperAdmin.Value = "True"
                Else
                    hdnIsSuperAdmin.Value = "False"
                End If

                If dsAL.Tables.Count > 0 Then
                    For Each oRec As DataRow In dsAL.Tables(0).Rows
                        If oRec("LevelNo") <> 2147483647 Then
                            Dim LI As New ListItem
                            LI.Text = oRec("LevelName")
                            LI.Value = oRec("LevelNo")
                            If chkLevel(SelectedAdminLevel, CLng(oRec("LevelNo"))) Then
                                If chkLevel(SelectedUserLevel, CLng(oRec("LevelNo"))) Then
                                    lstAssignLevels.Items.Add(LI)
                                Else
                                    lstAvailLevels.Items.Add(LI)
                                End If
                            End If
                        End If
                    Next
                Else
                    Response.Write("No Records Found!")
                End If
            Catch ex As Exception
                'Response.Write(ex.Message)
            End Try
        Catch ex As Exception
            '      Response.Write(ex.Message)
        End Try
    End Function


    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
        Dim intCt As Integer
        For intCt = lstAvailLevels.Items.Count - 1 To 0 Step -1 ' Looping Backwards
            If lstAvailLevels.Items(intCt).Selected Then
                Dim LI As New ListItem
                LI.Text = lstAvailLevels.Items(intCt).Text
                LI.Value = lstAvailLevels.Items(intCt).Value
                lstAssignLevels.Items.Add(LI)
                lstAvailLevels.Items.Remove(lstAvailLevels.Items(intCt))
            End If
        Next
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove.Click
        Dim intCt As Integer
        For intCt = lstAssignLevels.Items.Count - 1 To 0 Step -1 ' Looping Backwards
            If lstAssignLevels.Items(intCt).Selected Then
                Dim LI As New ListItem
                LI.Text = lstAssignLevels.Items(intCt).Text
                LI.Value = lstAssignLevels.Items(intCt).Value
                lstAvailLevels.Items.Add(LI)
                lstAssignLevels.Items.Remove(lstAssignLevels.Items(intCt))
            End If
        Next
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click


        Dim UpdatedLevel As Long
        Dim i As Integer
        For i = 0 To lstAssignLevels.Items.Count - 1
            If IsNumeric(lstAssignLevels.Items(i).Value) Then

                UpdatedLevel = UpdatedLevel + CLng(lstAssignLevels.Items(i).Value)
            End If
        Next
        Dim clsUL As New ETS.BL.UserLevels
        With clsUL
            .UserID = Request("UserID")
            .AdminLevel = UpdatedLevel
            If Not .UpdateUserLevels() >= 1 Then
                If .InsertUserLevels() = 1 Then
                    MsgBox1.alert("Levels Updated Successfully!")
                Else
                    MsgBox1.alert("Updating Levels failed!")
                End If
            Else
                MsgBox1.alert("Levels Updated Successfully!")
            End If
        End With
        clsUL = Nothing
        CleanUserLinks(UpdatedLevel)
        lstBind()
    End Sub
    Private Function CleanUserLinks(ByVal AdminLevels As Long)
        Dim strLvl As New StringBuilder
        Dim clsUL As New ETS.BL.UserLinks
        With clsUL
            .UserID = Request("UserID")
            Dim DSUL As DataSet = .getUserLinkDetails
            If DSUL.Tables.Count > 0 Then
                For Each oRec As DataRow In DSUL.Tables(0).Rows
                    If Not chkLevel(AdminLevels, CLng(oRec("LevelNo"))) Then
                        strLvl.Append(IIf(strLvl.ToString = "", "", ",") & oRec("LevelNo"))
                    End If
                Next
                If Not strLvl.Length <= 0 Then
                    ._WhereString.Append(" and levelno in (" & strLvl.ToString & ")")
                    .DeleteUserLinks()
                End If
            End If
        End With
    End Function


    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("A_LevelAssignments.aspx?CriUser=" & hdnCriUser.Value & "&CriOption=" & hdnCriOption.Value, True)
    End Sub

    Protected Sub lstAssignLevels_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstAssignLevels.SelectedIndexChanged
        lblLinkCaption.Text = "Links for " & lstAssignLevels.SelectedItem.Text
        hdnSelAsiignLevel.Value = lstAssignLevels.SelectedItem.Value
        chkbLinks.Items.Clear()

        Dim LinkLevels As Long
        Dim clsUL As New ETS.BL.UserLinks
        With clsUL
            .UserID = Request("UserID")
            .LevelNo = lstAssignLevels.SelectedItem.Value
            Dim DSUL As DataSet = .getUserLinkDetails
            If DSUL.Tables(0).Rows.Count = 1 Then
                LinkLevels = IIf(IsDBNull(DSUL.Tables(0).Rows(0).Item("LinkNo")), 0, DSUL.Tables(0).Rows(0).Item("LinkNo"))
            End If
        End With
        clsUL = Nothing
        Dim AdminLinkLevels As Long
        Dim clsAL As New ETS.BL.UserLinks
        With clsAL
            .UserID = Session("UserID")
            .LevelNo = lstAssignLevels.SelectedItem.Value
            Dim DSAL As DataSet = .getUserLinkDetails
            If DSAL.Tables(0).Rows.Count = 1 Then
                AdminLinkLevels = IIf(IsDBNull(DSAL.Tables(0).Rows(0).Item("LinkNo")), 0, DSAL.Tables(0).Rows(0).Item("LinkNo"))
            End If
        End With
        clsAL = Nothing
        Dim DSALL As New DataSet
        Dim clsALL As New ETS.BL.AdminLevelLinks
        With clsALL
            .LevelNo = lstAssignLevels.SelectedItem.Value
            DSALL = .getLinkList()
        End With
        clsALL = Nothing
        'Response.Write(IsSuperAdmin)
        If DSALL.Tables.Count > 0 Then
            Dim IsSelected As Boolean = False
            For Each oRec As DataRow In DSALL.Tables(0).Rows
                Dim LI As New ListItem
                LI.Text = oRec("link_caption")
                LI.Value = oRec("linkid")
                LI.Selected = chkLevel(LinkLevels, CLng(IIf(IsDBNull(oRec("linkid")), 1, oRec("linkid"))))

                If hdnIsSuperAdmin.Value = "True" Or chkLevel(AdminLinkLevels, CLng(IIf(IsDBNull(oRec("linkid")), 1, oRec("linkid")))) Then
                    chkbLinks.Items.Add(LI)
                End If
            Next
        End If
        DSALL.Dispose()
    End Sub

    Protected Sub chkbLinks_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkbLinks.SelectedIndexChanged
        UpdateLinkLevel()
    End Sub
    Private Function UpdateLinkLevel()
        Dim SelectedLinkLevels As Long
        For Each Li As ListItem In chkbLinks.Items
            If Li.Selected Then
                SelectedLinkLevels = SelectedLinkLevels + Li.Value
            End If
        Next
        Dim clsUL As New ETS.BL.UserLinks
        With clsUL
            .UserID = Request("UserID")
            .LevelNo = hdnSelAsiignLevel.Value
            .LinkNo = SelectedLinkLevels
            If Not .UpdateUserLink >= 1 Then
                If .InsertUserLink() = 1 Then
                    MsgBox1.alert("Links Updated Successfully!")
                Else
                    MsgBox1.alert("Updating Links failed!")
                End If
            Else
                MsgBox1.alert("Links Updated Successfully!")
            End If
        End With
        clsUL = Nothing
    End Function



    Protected Sub lnkCheckAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCheckAll.Click
        For Each Li As ListItem In chkbLinks.Items
            Li.Selected = True
        Next
        UpdateLinkLevel()
    End Sub

    Protected Sub lnkUnCheckAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkUnCheckAll.Click
        For Each Li As ListItem In chkbLinks.Items
            Li.Selected = False
        Next
        UpdateLinkLevel()
    End Sub
End Class
