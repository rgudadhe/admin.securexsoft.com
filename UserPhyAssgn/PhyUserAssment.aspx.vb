Imports System.Data.SqlClient
Imports SASMTPLib
Imports System.Data
Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Public DirLevelNo As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MsgDisp.Text = ""
        BtnAssign.Visible = False
        Table4.Visible = False

        If Not IsPostBack Then
            BtnSubmit1.Focus()
            ChkAll.Attributes.Add("onclick", "changeAll();")
            ChkDirect.Attributes.Add("onclick", "changeAllDirect();")
            BtnAssign.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
            Panel1.Visible = True
            PnlActSearch.Visible = False
            PnlActSelect.Visible = False
            PrdState.Value = 0
            PhyState.Value = 0
        End If
    End Sub
    Protected Sub ShowSelAccounts()
        HActID.Value = Request("ActID")
        PnlActSearch.Visible = False
        PnlActSelect.Visible = False
        Panel6.Visible = True
        Panel7.Visible = False
        Dim NotChecked As Boolean
        NotChecked = False
        Dim clsRSSS As New ETS.BL.RSSStatus
        With clsRSSS
            .ContractorID = Session("ContractorID")
            HDirLevel.Value = .IntialProduction
        End With
        clsRSSS = Nothing
        Dim clsAUA As New ETS.BL.AccountUserAssgn
        With clsAUA
            .userid = HUserID.Value
            .LevelNo = Request("LevelNo")
            .AccountID = Request("Actid")
            .getAccountUsersDetails()
            If Len(.TrackID) = 36 Then
                ChkAll.Checked = True
            Else
                ChkAll.Checked = False
            End If
        End With
        clsAUA = Nothing
        Dim DSUP As New DataSet
        Dim clsUPLM As New ETS.BL.UserPrLvlMgmt
        With clsUPLM
            If Session("IsContractor") = "1" Then
                DSUP = .getUsersPhySearch(HUserID.Value, Request("LevelNo"), Request("ActID"))
            Else
                DSUP = .SubConPhySearch(HUserID.Value, Request("LevelNo"), Request("ActID"))
            End If
        End With
        clsUPLM = Nothing


        Dim K As Integer
        K = 0
        If DSUP.Tables.Count > 0 Then
            NotChecked = True
            For Each DRRec1 As DataRow In DSUP.Tables(0).Rows
                K = K + 1
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim c5 As New TableCell
                Dim r As New TableRow
                If ChkAll.Checked Then
                    c3.Text = "<input id=PhyID type=checkbox name=PhyID onclick=highlightRow(this); Value=" & DRRec1("PhysicianId").ToString & "  checked=checked>"
                Else
                    If DRRec1("APhysicianId").ToString <> "" Then
                        c3.Text = "<input id=PhyID type=checkbox name=PhyID onclick=highlightRow(this); Value=" & DRRec1("PhysicianId").ToString & "  checked=checked>"
                    Else
                        c3.Text = "<input id=PhyID type=checkbox name=PhyID onclick=highlightRow(this); Value=" & DRRec1("PhysicianId").ToString & " >"
                        NotChecked = False
                    End If
                End If

                If DRRec1("Direct").ToString = "True" Then
                    c5.Text = "<input id=Direct type=checkbox name=Direct onclick=highlightRowDir(this); Value=" & DRRec1("PhysicianId").ToString & "  checked=checked>"

                Else
                    c5.Text = "<input id=Direct type=checkbox name=Direct onclick=highlightRowDir(this); Value=" & DRRec1("PhysicianId").ToString & " >"
                End If

                Dim CB2 As New CheckBox
                CB2.ID = "Direct" & K

                CB2.InputAttributes.Add("Value", "True")
                TotPhy.Value = K
                c.Text = DRRec1("firstname")
                c1.Text = DRRec1("lastname")
                c2.Text = DRRec1("PinNo")

                r.Cells.Add(c3)
                r.Cells.Add(c)
                r.Cells.Add(c1)
                r.Cells.Add(c2)
                c3.Attributes.Add("style", "text-align:center;")
                c5.Attributes.Add("style", "text-align:center;")
                If IsNumeric(HDirLevel.Value) Then
                    r.Cells.Add(c5)
                End If
                Table1.Rows.Add(r)

            Next
        Else
            PhyState.Value = 0
            PageStatus()
            Exit Sub
        End If

        DSUP.Dispose()
        If IsNumeric(HDirLevel.Value) = False Then
            DirCell.Visible = False
        End If
    End Sub
    Protected Sub UserSearch_Click()

        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim DSUsers As New DataSet
        Dim clsUsers As New ETS.BL.Users
        Dim varWhere As New StringBuilder
        With clsUsers
            '.ContractorID = Session("ContractorID").ToString
            ' ._WhereString.Append(" and (Isdeleted is NULL or Isdeleted = 'False') ")
            If Not String.IsNullOrEmpty(TxtUname.Text) Then
                varWhere.Append(" and UserName like '" & TxtUname.Text & "'")
            End If
            If Not String.IsNullOrEmpty(TxtFname.Text) Then
                varWhere.Append(" and FirstName like '" & TxtFname.Text & "'")
            End If
            If Not String.IsNullOrEmpty(TxtLname.Text) Then
                varWhere.Append(" and LastName like '" & TxtLname.Text & "'")
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
                c3.Attributes.Add("style", "text-align:center;")
                J = J + 1
                RB.Checked = True
                r.Cells.Add(c3)
                r.Cells.Add(c)
                r.Cells.Add(c1)
                r.Cells.Add(c2)
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
            Panel2.Visible = True
            Panel1.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
        Else
            MsgDisp.Text = "No user found. Please try again."
            Panel2.Visible = False
            Panel1.Visible = True
            Panel3.Visible = False
            Panel4.Visible = False
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
        Panel4.Visible = False

        Dim SelectedUserLevel As Long
        Dim LevelNo As Long
        Dim LevelName As String

        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim clsUL As New ETS.BL.UserLevels
        With clsUL
            .UserID = HUserID.Value.ToString
            .getUserLevelDetails()
            SelectedUserLevel = .ProductionLevel
        End With
        clsUL = Nothing

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
                        Dim CB As New RadioButton
                        Dim c As New TableCell
                        Dim r As New TableRow
                        Dim c1 As New TableCell
                        CB.ID = "LevelNo" & m
                        c.Text = "<input id=LevelNo type=radio name=LevelNo Value=" & LevelNo & ">"
                        c1.Text = LevelName
                        CB.InputAttributes.Add("Value", LevelNo)
                        c.Attributes.Add("style", "text-align:center;")
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
            SelectedUserLevel = 0
        End If

    End Sub
    Protected Sub BtnSubmit5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit5.Click
        PrdState.Value = 3
        PnlActSearch.Visible = True
        BtnSubmit6.Focus()
        PageStatus()
    End Sub

    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit3.Click
        PhyState.Value = 3
        PageStatus()
    End Sub
    Protected Sub UserFStatus_Click()

        Panel4.Visible = True
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False

        Dim LevelNo As Long
        Dim LevelName As String

        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both

        Dim L As Integer
        L = 0
       
        Dim clsPL As New ETS.BL.ProductionLevels
        With clsPL
            .ContractorID = IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString)
            .Type = Session("IsContractor")
            .IsDeleted = False
            ._WhereString.Append(" and (LevelNo<>1073741824 and LevelNo<>5 and LevelNo<>3)")
            .LevelNo = Request("LevelNo")
            .getPLevelDetails()

            L = L + 1
            LevelNo = .LevelNo
            LevelName = .LevelName


            Dim CB As New RadioButton
            Dim c As New TableCell
            Dim r As New TableRow
            Dim c1 As New TableCell
            LvlAssgned.Value = L
            CB.ID = "LevelNo" & L
            CB.Checked = True
            'c.Controls.Add(CB)
            c.Text = "<input id=LevelNo type=radio name=LevelNo Value=" & LevelNo & "  checked=checked>"
            c1.Text = LevelName
            CB.InputAttributes.Add("Value", LevelNo)
            c.Attributes.Add("style", "text-align:center;")
            r.Cells.Add(c)
            r.Cells.Add(c1)
            Table2.Rows.Add(r)
        End With
        clsPL = Nothing
    End Sub

    Sub PageStatus()
        If PrdState.Value = 0 Then
            BtnSubmit1.Focus()
            Loadpage()
        ElseIf PrdState.Value = 1 Then
            btnSubmit4.Focus()
            UserSearch_Click()
        ElseIf PrdState.Value = 2 Then
            BtnSubmit5.Focus()
            UserStatus_Click()
        ElseIf PrdState.Value = 3 Then
            UserFStatus_Click()
        End If

        If PhyState.Value = 0 Then
            Loadpage()
        ElseIf PhyState.Value = 1 Then
            BtnSubmit7.Focus()
            ActSearch_Click()
        ElseIf PhyState.Value = 2 Then
            btnsubmit3.Focus()
            ShowSelAccounts()
        
        End If
        If PrdState.Value = 3 And PhyState.Value = 2 Then
            BtnAssign.Visible = True
            BtnAssign.Focus()
        Else
            BtnAssign.Visible = False
            BtnAssign.Focus()
        End If

    End Sub

    Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAssign.Click

        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False

        Panel6.Visible = False
        Panel7.Visible = False
        PnlActSearch.Visible = False
        PnlActSelect.Visible = False

        Dim strConn As String
       
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim clsActUserAsiign As New ETS.BL.AccountUserAssgn
        With clsActUserAsiign
            .userid = HUserID.Value
            .AccountID = HActID.Value
            .LevelNo = Request("LevelNo")
            .SetUserAssignments(ChkAll.Checked, Request("PhyID"))
        End With
        clsActUserAsiign = Nothing

        Dim sQuery3 As String
        Dim SelLevelNo As String
        Dim SelActName As String
        Dim SelUserID As String
        SelLevelNo = ""
        SelActName = ""
        SelUserID = ""

        Dim DSUPA As New DataSet
        Dim clsUPLM As New ETS.BL.UserPrLvlMgmt
        With clsUPLM
            DSUPA = .getViewUsersPhyAssignedList("", HUserID.Value, IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString), Request("LevelNo"))
        End With
        clsUPLM = Nothing


        If DSUPA.Tables.Count > 0 Then
            For Each RdSet As DataRow In DSUPA.Tables(0).Rows
                
                If SelLevelNo = "" Then

                    SelLevelNo = RdSet("levelname").ToString
                    Dim cAct As New TableCell
                    cAct.HorizontalAlign = HorizontalAlign.Center
                    cAct.ColumnSpan = 4
                    Dim rAct As New TableRow

                    rAct.HorizontalAlign = HorizontalAlign.Center
                    cAct.Text = "User Name: " & RdSet("uname").ToString & " | User Role: " & RdSet("levelname").ToString
                    cAct.CssClass = "SMSelected"
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
                    rAct.CssClass = "SMSelected"
                    rAct.HorizontalAlign = HorizontalAlign.Center
                    cAct.CssClass = "SMSelected"
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
                    cAct.CssClass = "SMSelected"
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
                    cAct.CssClass = "SMSelected"
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
                If RdSet("direct").ToString = "True" Then
                    c4.Text = "Direct"
                Else
                    c4.Text = "Indirect"
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
            Next
            DSUPA.Dispose()
        End If
        

    End Sub

    Sub Loadpage()
        If PrdState.Value = 0 Then
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel1.Visible = True
        End If
        If PhyState.Value = 0 Then
            'PnlActSearch.Visible = False
            PnlActSelect.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False

        End If

    End Sub

    Protected Sub BtnSubmit6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit6.Click
        PhyState.Value = 1
        PageStatus()
    End Sub
    Protected Sub BtnSubmit7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit7.Click
        PhyState.Value = 2
        PageStatus()
    End Sub

    Protected Sub ActSearch_Click()
       
        Panel6.Visible = False

        Panel7.Visible = False

        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both

        Dim clsRSSS As New ETS.BL.RSSStatus
        With clsRSSS
            .ContractorID = Session("ContractorID")
            HDirLevel.Value = .IntialProduction
        End With
        clsRSSS = Nothing

        Dim DSAct As New DataSet
        Dim clsAct As New ETS.BL.Accounts
        With clsAct
            If Session("IsContractor") = 1 Then
                '.ContractorID = Session("ContractorID")
                '._WhereString.Append(" and (Isdeleted is NULL or Isdeleted = 'False') and AccountName like '%" & TxtAname.Text & "%'")
                DSAct = .getAccountList(Session("WorkGroupID"), Session("ContractorID"), " AND AccountName like '%" & TxtAname.Text & "%' ")
            Else
                DSAct = .getSubConAccounts(Session("ParentID").ToString, Session("ContractorID"))
            End If
        End With
        clsAct = Nothing
       
        Dim DR() As DataRow
        If Session("IsContractor") = 1 Then
            DR = DSAct.Tables(0).Select("AccountName is not NULL")
        Else
            DR = DSAct.Tables(0).Select("AccountName like '%" & TxtAname.Text & "%'")
        End If
        DSAct.Dispose()
        Dim K As Integer
        K = 0
        If UBound(DR) > 0 Then
            For Each DRRec1 As DataRow In DR
                K = K + 1
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow
                Dim RB As New RadioButton
                RB.GroupName = "ActID"
                RB.ID = DRRec1("AccountID").ToString
                TotAct.Value = K
                c.Text = DRRec1("AccountName")
                c1.Text = DRRec1("AccountNo")
                c3.Controls.Add(RB)
                r.Cells.Add(c3)
                r.Cells.Add(c)
                r.Cells.Add(c1)
                c3.Attributes.Add("style", "text-align:center;")
                'r.Cells.Add(c2)
                TblAccount.Rows.Add(r)

            Next
            DR = Nothing
            Dim CB As New Button
            Dim r2 As New TableRow
            Dim c4 As New TableCell
            c4.ColumnSpan = 3
            c4.Style("text-align") = "center"
            BtnSubmit7.Visible = True
            c4.Controls.Add(BtnSubmit7)
            r2.Cells.Add(c4)
            TblAccount.Rows.Add(r2)
            PnlActSearch.Visible = False
            PnlActSelect.Visible = True
        Else
            MsgDisp.Text = "No records found, Please try another search."
            PnlActSearch.Visible = True
            PnlActSelect.Visible = False
        End If

    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class


