Imports System.Data.SqlClient
Imports System.Data
Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MsgDisp.Visible = False
        Table4.Visible = False
        If Not IsPostBack Then
            Panel2.Visible = False
            Panel3.Visible = False
            Panel1.Visible = True
            PrdState.Value = 0
        End If
    End Sub

    Protected Sub UserSearch_Click()
        Panel2.Visible = True
        Panel1.Visible = False
        Panel3.Visible = False

        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim DSUsers As New DataSet
        Dim clsUsers As New ETS.BL.Users
        Dim varWhere As New StringBuilder
        With clsUsers
            '.ContractorID = Session("ContractorID").ToString
            '._WhereString.Append(" and (Isdeleted is NULL or Isdeleted = 'False') ")
            If Not String.IsNullOrEmpty(TxtUname.Text) Then
                '._WhereString.Append(" and UserName like '" & TxtUname.Text & "'")
                varWhere.Append(" AND UserName like '" & TxtUname.Text & "'")
            End If
            If Not String.IsNullOrEmpty(TxtFname.Text) Then
                '._WhereString.Append(" and FirstName like '" & TxtFname.Text & "'")
                varWhere.Append(" AND FirstName like '" & TxtFname.Text & "' ")
            End If
            If Not String.IsNullOrEmpty(TxtLname.Text) Then
                '._WhereString.Append(" and LastName like '" & TxtLname.Text & "'")
                varWhere.Append(" AND LastName like '" & TxtLname.Text & "'")
            End If
            DSUsers = .getUsersList(Session("ContractorID"), Session("WorkGroupID"), varWhere.ToString)
        End With
        clsUsers = Nothing
        Dim i As Integer
        Dim J As Integer
        J = 0
        If DSUsers.Tables.Count > 0 Then
            For Each DRRec As DataRow In DSUsers.Tables(0).Rows
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow
                Dim RB As New RadioButton
                c.Text = DRRec("firstname")
                c1.Text = DRRec("lastname")
                c2.Text = DRRec("username")
                form1.Controls.Add(RB)
                RB.GroupName = "UserID"
                RB.ID = DRRec("userid").ToString
                c3.Controls.Add(RB)
                J = J + 1
                RB.Checked = True
                r.Cells.Add(c3)
                r.Cells.Add(c)
                r.Cells.Add(c1)
                r.Cells.Add(c2)
                't2.Rows.Add(r)
                UserTable.Rows.Add(r)

            Next
            Dim r2 As New TableRow
            Dim c4 As New TableCell
            btnSubmit4.Visible = True
            c4.ColumnSpan = 4
            c4.Style("text-align") = "center"
            c4.Controls.Add(btnSubmit4)
            r2.Cells.Add(c4)
            UserTable.Rows.Add(r2)

        Else
            MsgDisp.Visible = True
            MsgDisp.Text = "No user found. Please try again."
            Panel2.Visible = False
            Panel3.Visible = False
            Panel1.Visible = True

        End If
        DSUsers.Dispose()
    End Sub


    Protected Sub BtnSubmit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit1.Click
        PrdState.Value = 1
        PageStatus()
    End Sub


    Protected Sub btnSubmit4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit4.Click
        PrdState.Value = 2
        HUserID.Value = Request("UserID")
        PageStatus()
    End Sub
    Protected Sub UserStatus_Click()

        Panel3.Visible = True
        Panel1.Visible = False
        Panel2.Visible = False


        Dim SelectedUserLevel As Long
        Dim LevelNo As Long
        Dim LevelName As String


        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim clsUL As New ETS.BL.UserLevels
        With clsUL
            .UserID = Session("UserID")
            .getUserLevelDetails()
            SelectedUserLevel = .ProductionLevel
        End With
        clsUL = Nothing

        
        Dim i As Integer
        Dim J As Integer
        Dim RecFound As String
        RecFound = "No"

        J = 0
       
        

        If SelectedUserLevel > 0 Then
            Dim DSPL As New DataSet
            Dim clsPL As New ETS.BL.ProductionLevels
            With clsPL
                .ContractorID = IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString)
                .Type = Session("IsContractor")
                .IsDeleted = False
                DSPL = .getPLevelList
            End With
            clsPL = Nothing

            Dim m As Integer
            m = 0
            If DSPL.Tables.Count > 0 Then
                For Each oRec As DataRow In DSPL.Tables(0).Rows
                    LevelNo = oRec("LevelNo")
                    LevelName = oRec("LevelName")

                    If chkLevel(SelectedUserLevel, LevelNo) Then
                        m = m + 1
                        Dim CB As New CheckBox
                        Dim c As New TableCell
                        Dim r As New TableRow
                        Dim c1 As New TableCell
                        CB.ID = "LevelNo" & m

                        c.Controls.Add(CB)
                        c1.Text = LevelName
                        CB.InputAttributes.Add("Value", LevelNo)
                        r.Cells.Add(c)
                        r.Cells.Add(c1)
                        TblUserstatus.Rows.Add(r)
                        TotLvl.Value = m
                        RecFound = "Yes"
                    End If

                Next
                If RecFound = "Yes" Then
                    Dim r2 As New TableRow
                    Dim c4 As New TableCell
                    BtnSubmit5.Visible = True
                    c4.ColumnSpan = 2
                    c4.Style("text-align") = "center"
                    c4.Controls.Add(BtnSubmit5)
                    r2.Cells.Add(c4)
                    TblUserstatus.Rows.Add(r2)
                End If
           
            End If
            DSPL.Dispose()
        Else
            PrdState.Value = 1
            MsgDisp.Visible = True
            MsgDisp.Text = "No Production Level found for this user"
            PageStatus()
            SelectedUserLevel = 0
            Exit Sub
        End If

    End Sub
    


    Protected Sub BtnSubmit5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit5.Click
        PrdState.Value = 3
        PageStatus()


    End Sub





    Sub PageStatus()
        If PrdState.Value = 1 Then
            UserSearch_Click()
        ElseIf PrdState.Value = 2 Then
            UserStatus_Click()
        ElseIf PrdState.Value = 3 Then
            ActUserAssign()
        End If


    End Sub

    Protected Sub ActUserAssign()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Dim Lvl As String

        Dim i As Integer
        Dim j As Integer
        Dim ViewLvl As String
        ViewLvl = ""
        Lvl = ""
        j = 1
        For i = 1 To TotLvl.Value
            Lvl = Request("LevelNo" & i)
            If Lvl <> "" Then
                If j = 1 Then
                    ViewLvl = Lvl
                    j = j + 1
                Else
                    ViewLvl = ViewLvl & "," & Lvl
                End If
            End If


        Next
        If ViewLvl <> "" Then
            Dim SelLevelNo As String
            Dim SelActName As String
            Dim SelUserID As String
            SelLevelNo = ""
            SelActName = ""
            SelUserID = ""
            Dim DSUPA As New DataSet
            Dim clsUPLM As New ETS.BL.UserPrLvlMgmt
            With clsUPLM
                DSUPA = .getViewUsersPhyAssignedList("", HUserID.Value, IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString), Session("IntialProductionLevel").ToString)
            End With
            clsUPLM = Nothing


            If DSUPA.Tables.Count > 0 Then
                For Each RdSet As DataRow In DSUPA.Tables(0).Rows
                    If InStr(ViewLvl, RdSet("LevelNo")) > 0 Then
                        If SelLevelNo = "" Then

                            SelLevelNo = RdSet("levelname").ToString
                            Dim cAct As New TableCell
                            cAct.HorizontalAlign = HorizontalAlign.Center
                            cAct.ColumnSpan = 4
                            Dim rAct As New TableRow
                            rAct.HorizontalAlign = HorizontalAlign.Center
                            cAct.CssClass = "HeaderDiv"
                            cAct.Text = "User Name: " & RdSet("uname").ToString & " | User Role: " & RdSet("levelname").ToString
                            rAct.Cells.Add(cAct)
                            Table4.Rows.Add(rAct)
                        End If
                        If SelLevelNo <> RdSet("levelname").ToString Then
                            Dim CEmp As New TableCell
                            Dim REmp As New TableRow
                            CEmp.Text = "-"
                            CEmp.ColumnSpan = 4
                            REmp.Cells.Add(CEmp)
                            Table4.Rows.Add(REmp)
                            SelLevelNo = RdSet("levelname").ToString
                            Dim cAct As New TableCell
                            cAct.HorizontalAlign = HorizontalAlign.Center
                            cAct.ColumnSpan = 4
                            Dim rAct As New TableRow
                            'rAct.CssClass = "SMSelected"
                            rAct.HorizontalAlign = HorizontalAlign.Center
                            cAct.CssClass = "HeaderDiv"
                            cAct.Text = "User Name: " & RdSet("uname").ToString & " | User Role: " & RdSet("levelname").ToString
                            rAct.Cells.Add(cAct)
                            Table4.Rows.Add(rAct)
                        End If

                        If SelActName = "" Then
                            SelActName = RdSet("Accountname").ToString
                            Dim cAct As New TableCell
                            cAct.HorizontalAlign = HorizontalAlign.Center
                            cAct.ColumnSpan = 4
                            Dim rAct As New TableRow
                            ' rAct.CssClass = "SMSelected"
                            rAct.HorizontalAlign = HorizontalAlign.Center
                            cAct.Text = "Account Name: " & RdSet("Accountname").ToString
                            cAct.CssClass = "alt2"
                            rAct.Cells.Add(cAct)
                            Table4.Rows.Add(rAct)
                        End If
                        If SelActName <> RdSet("Accountname").ToString Then
                            SelActName = RdSet("Accountname").ToString
                            Dim cAct As New TableCell
                            cAct.HorizontalAlign = HorizontalAlign.Center
                            cAct.ColumnSpan = 4
                            Dim rAct As New TableRow
                            ' rAct.CssClass = "SMSelected"
                            rAct.HorizontalAlign = HorizontalAlign.Center
                            cAct.Text = "Account Name: " & RdSet("Accountname").ToString
                            cAct.CssClass = "alt2"
                            rAct.Cells.Add(cAct)
                            Table4.Rows.Add(rAct)
                        End If



                        Dim c2 As New TableCell
                        Dim c3 As New TableCell
                        Dim c4 As New TableCell
                        Dim c5 As New TableCell
                        If Not IsDBNull(RdSet("TrackID")) Then
                            c5.Text = "<img src=remove.gif style='cursor:hand;' onclick=poptastic('" & RdSet("TrackID").ToString & "')>"
                        End If
                        Dim r As New TableRow

                        c2.Text = RdSet("pname").ToString
                        c3.Text = RdSet("PINNo").ToString
                        If Not IsDBNull(RdSet("TrackID")) Then
                            c4.Text = RdSet("direct").ToString
                        Else
                            c4.Text = False
                        End If
                        r.Cells.Add(c2)
                        r.Cells.Add(c3)

                        If HDirLevel.Value = RdSet("LevelNo").ToString Then

                            r.Cells.Add(c4)
                        Else
                            c2.ColumnSpan = 2
                        End If
                        r.Cells.Add(c5)
                        Table4.Visible = True

                        Table4.Rows.Add(r)
                    End If
                Next
            End If
            DSUPA.Dispose()
        Else
            Dim c3 As New TableCell
            Dim r As New TableRow
            c3.ColumnSpan = 4
            c3.Text = "No Records Found"
            r.Cells.Add(c3)
            Table4.Visible = True

            Table4.Rows.Add(r)

        End If

    End Sub
End Class

