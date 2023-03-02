Imports System
Imports System.Data
Partial Class _Default
    Inherits BasePage
    Dim sb As New StringBuilder
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		'On page_load event this code will fire,
        'which enables the first view of MultiView Control

        If Not IsPostBack Then

            mvAdminLevels.ActiveViewIndex = 0
        End If
    End Sub

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext1.Click
        Dim goAhead As Boolean = True
        Dim strMessage As String
        If String.IsNullOrEmpty(txtLevelName.Text) Then
            strMessage = "Admin Level-Name can not be blank"
            goAhead = False
        End If
        If goAhead Then
            Dim ClsAL As New ETS.BL.AdminLevels

            Dim DSAL As DataSet = ClsAL.getAdminLevelList()
            Dim MaxLevel As Integer = DSAL.Tables(0).Compute("MAX(LevelNo)", "LevelNo<>2147483647")
            If MaxLevel >= 1073741824 Then
                strMessage = "Admin Level limits reached!"
            Else
                If DSAL.Tables(0).Compute("count(LevelName)", "LevelName = '" & txtLevelName.Text & "'") = 0 Then
                    With ClsAL
                        .LevelName = txtLevelName.Text
                        .LevelNo = MaxLevel + MaxLevel
                        .Description = txtLevelDesc.Text
                    End With

                    Dim RetVal As Integer = ClsAL.InsertNewLevel
                    ClsAL = Nothing
                    If RetVal = 1 Then
                        strMessage = "Admin Level " & txtLevelName.Text & " added successfully"
                        mvAdminLevels.ActiveViewIndex += 1
                    Else
                        strMessage = "Adding Admin Level " & txtLevelName.Text & " failed"
                    End If
                Else
                    strMessage = "Admin Level with name " & txtLevelName.Text & " is alreadt exist"
                End If
            End If

        End If
        Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
    End Sub

    

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Dim goAhead As Boolean = True
        Dim strMessage As String = String.Empty
        If String.IsNullOrEmpty(hdnNewLevel.Value) Then
            strMessage = "Adding Admin Level-Link  failed"
            Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
            goAhead = False
        End If
        If String.IsNullOrEmpty(txtLinkCap.Text) Then
            strMessage = "Admin Level-Link Caption can not be blank"
            Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
            goAhead = False
        End If
        If String.IsNullOrEmpty(txtLinkPath.Text) Then
            strMessage = "Admin Level-Link Path can not be blank"
            Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
            goAhead = False
        Else
            Dim iFileInfo As New System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory & txtLinkPath.Text)
            If Not iFileInfo.Exists Then
                strMessage = "Please specify correct path and file name"
                Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
                goAhead = False
            End If
        End If
        If goAhead Then

            Dim clsALL As New ETS.BL.AdminLevelLinks
            clsALL.LevelNo = CInt(hdnNewLevel.Value)
            Dim DSALL As DataSet = clsALL.getLinkList
            If DSALL.Tables(0).Compute("count(Link_Caption)", "Link_Caption = '" & txtLinkCap.Text & "'") = 0 Then
                With clsALL
                    .Link_Caption = txtLinkCap.Text
                    .Link_Path = txtLinkPath.Text
                End With
                Dim RetVal As Integer = clsALL.InsertNewLink()
                clsALL = Nothing
                If RetVal = 1 Then
                    strMessage = "Link " & txtLinkCap.Text & " added successfully"
                    Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
                    Response.Redirect("EditAdminLevelLinks.aspx?lvlNo=" & hdnNewLevel.Value, True)
                End If
            Else
                strMessage = "Admin Level-Link with Caption " & txtLinkCap.Text & " is alreadt exist"
                Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
            End If
            
        End If
    End Sub


    Protected Sub btnFinish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        Response.Redirect("EditAdminLevelLinks.aspx?lvlNo=" & hdnNewLevel.Value, True)
    End Sub
End Class
