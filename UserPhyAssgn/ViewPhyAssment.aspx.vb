Imports System.Data.SqlClient
Imports System.Data
Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        BtnAssign.Visible = False
        Table4.Visible = False

        If Not IsPostBack Then
            ChkAll.Attributes.Add("onclick", "changeAll();")
            BtnAssign.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
            PnlActSearch.Visible = True
            PnlActSelect.Visible = False
            PrdState.Value = 0
            PhyState.Value = 0
        End If

    End Sub

    Protected Sub PhySearch_Click()
        PnlActSearch.Visible = False
        PnlActSelect.Visible = False
        Panel6.Visible = True
        Panel7.Visible = False
        Dim DSPhy As New DataSet
        Dim clsPhy As New ETS.BL.Physicians
        With clsPhy
            .AccountID = Request("Actid")
            ._WhereString.Append(" and   (Isdeleted is NULL or Isdeleted = 'False')")
            DSPhy = .getPhysiciansList
        End With
        clsPhy = Nothing
        
        Dim K As Integer
        K = 0
        If DSPhy.Tables.Count > 0 Then
            For Each DRRec1 As DataRow In DSPhy.Tables(0).Rows
                K = K + 1
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim c5 As New TableCell
                Dim r As New TableRow
                Dim CB1 As New CheckBox
                CB1.ID = "PhyID" & K
                CB1.InputAttributes.Add("Value", DRRec1("PhysicianId").ToString)
                CB1.InputAttributes.Add("onclick", "highlightRow(this);")
                Dim CB2 As New CheckBox
                CB2.ID = "Direct" & K
                CB2.InputAttributes.Add("Value", "True")
                TotPhy.Value = K
                c.Text = DRRec1("firstname")
                c1.Text = DRRec1("lastname")
                c2.Text = DRRec1("PinNo")
                c3.Controls.Add(CB1)
                c5.Controls.Add(CB2)
                r.Cells.Add(c3)
                r.Cells.Add(c)
                r.Cells.Add(c1)
                r.Cells.Add(c2)
                'r.Cells.Add(c5)
                Table1.Rows.Add(r)

            Next
            Dim CB As New Button
            Dim r2 As New TableRow
            Dim c4 As New TableCell
            c4.ColumnSpan = 5
            c4.Style("text-align") = "center"
            btnsubmit3.Visible = True
            c4.Controls.Add(btnsubmit3)
            r2.Cells.Add(c4)
            Table1.Rows.Add(r2)
        Else
            PhyState.Value = 0
            PageStatus()
        End If

        DSPhy.Dispose()

    End Sub

    Protected Sub btnSubmit4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit4.Click
        PrdState.Value = 2
        HUserID.Value = Request("UserID")
        PageStatus()
    End Sub
    
    


    Protected Sub BtnSubmit5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit5.Click
        PrdState.Value = 3
        PnlActSearch.Visible = True
        BtnSubmit6.Focus()
        PageStatus()
    End Sub

    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit3.Click
        Panel7.Visible = False
        Panel6.Visible = False
        Table4.Visible = True
        Dim Phy As String

        Dim i As Integer
        Dim j As Integer
        Dim ViewPhy As String
        ViewPhy = ""
        Phy = ""
        j = 1
        For i = 1 To TotPhy.Value
            Phy = Request("PhyID" & i)
            If Phy <> "" Then
                If j = 1 Then
                    ViewPhy = "'" & Phy & "'"
                    j = j + 1
                Else
                    ViewPhy = ViewPhy & ",'" & Phy & "'"
                End If
            End If


        Next
        If ViewPhy <> "" Then
            Dim clsRSSS As New ETS.BL.RSSStatus
            With clsRSSS
                .ContractorID = Session("ContractorID")
                HDirLevel.Value = .IntialProduction
            End With
            clsRSSS = Nothing
            Dim DSPhyUsersList As New DataSet
            Dim clsUPLM As New ETS.BL.UserPrLvlMgmt
            With clsUPLM
                DSPhyUsersList = .getViewUsersPhyAssignedList(ViewPhy, "", Session("ContractorID"), Session("IntialProductionLevel").ToString)
            End With
            clsUPLM = Nothing
            Dim SelLevelNo As String
            Dim SelActName As String
            Dim SelUserID As String
            SelLevelNo = ""
            SelActName = ""
            SelUserID = ""

            If DSPhyUsersList.Tables.Count > 0 Then
                For Each RdSet As DataRow In DSPhyUsersList.Tables(0).Rows
                    'Response.Write(RdSet("LevelNo").ToString)
                    If SelActName = "" Then
                        SelActName = RdSet("Accountname").ToString
                        Dim cAct As New TableCell
                        cAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.ColumnSpan = 4
                        Dim rAct As New TableRow
                        rAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.Text = "Account Name: " & RdSet("Accountname").ToString
                        'cAct.ForeColor = Drawing.Color.Brown
                        cAct.CssClass = "HeaderDiv"
                        cAct.Font.Bold = True
                        rAct.Cells.Add(cAct)
                        Table4.Rows.Add(rAct)
                    End If
                    If SelUserID = "" Then
                        SelUserID = RdSet("PhysicianID").ToString
                        Dim cAct As New TableCell
                        cAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.ColumnSpan = 4
                        Dim rAct As New TableRow
                        'rAct.CssClass = "HeaderDiv"
                        rAct.HorizontalAlign = HorizontalAlign.Center
                        'cAct.ForeColor = Drawing.Color.DarkGray
                        cAct.Font.Bold = True
                        cAct.CssClass = "alt1"
                        cAct.Text = "Dictator Name: " & RdSet("pname").ToString & " (" & RdSet("PinNo").ToString & ")"
                        rAct.Cells.Add(cAct)
                        Table4.Rows.Add(rAct)
                    End If
                    If SelUserID <> RdSet("PhysicianID").ToString Then
                        Dim CEmp As New TableCell
                        Dim REmp As New TableRow
                        CEmp.Text = "-"
                        CEmp.ColumnSpan = 4
                        REmp.Cells.Add(CEmp)
                        Table4.Rows.Add(REmp)
                        SelUserID = RdSet("PhysicianID").ToString
                        Dim cAct As New TableCell
                        cAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.ColumnSpan = 4
                        Dim rAct As New TableRow
                        'rAct.CssClass = "HeaderDiv"
                        rAct.HorizontalAlign = HorizontalAlign.Center
                        'cAct.ForeColor = Drawing.Color.DarkGray
                        cAct.Font.Bold = True
                        cAct.CssClass = "alt1"
                        cAct.Text = "Dictator Name: " & RdSet("pname").ToString & " (" & RdSet("PinNo").ToString & ")"
                        rAct.Cells.Add(cAct)
                        Table4.Rows.Add(rAct)

                        SelLevelNo = RdSet("LevelNo").ToString
                        Dim cAct1 As New TableCell
                        cAct1.HorizontalAlign = HorizontalAlign.Center
                        cAct1.ColumnSpan = 4
                        Dim rAct1 As New TableRow
                        'rAct1.CssClass = "SMSelected"
                        rAct1.HorizontalAlign = HorizontalAlign.Center
                        'rAct1.BackColor = Drawing.Color.GhostWhite
                        cAct1.CssClass = "alt1"
                        cAct1.Text = "User Role: " & RdSet("levelname").ToString
                        rAct1.Cells.Add(cAct1)
                        Table4.Rows.Add(rAct1)

                        Dim Cell1 As New TableCell
                        Dim Cell2 As New TableCell
                        Dim Cell3 As New TableCell
                        Dim Cell4 As New TableCell
                        Dim Cell5 As New TableCell
                        Dim Row1 As New TableRow
                        Row1.CssClass = "SMSelected"
                        Cell1.Text = "Name"
                        Cell2.Text = "UserName"
                        Cell3.Text = "Direct"
                        Cell4.Text = "Remove"
                        Cell1.HorizontalAlign = HorizontalAlign.Center
                        Cell2.HorizontalAlign = HorizontalAlign.Center
                        Cell3.HorizontalAlign = HorizontalAlign.Center
                        Cell4.HorizontalAlign = HorizontalAlign.Center
                        Cell1.CssClass = "alt1"
                        Cell2.CssClass = "alt1"
                        Cell3.CssClass = "alt1"
                        Cell4.CssClass = "alt1"
                        Row1.BackColor = Drawing.Color.GhostWhite
                        Row1.Cells.Add(Cell1)
                        Row1.Cells.Add(Cell2)

                        If HDirLevel.Value = RdSet("LevelNo").ToString Then
                            Row1.Cells.Add(Cell3)
                        Else
                            Cell1.ColumnSpan = 2
                        End If

                        Row1.Cells.Add(Cell4)
                        Row1.HorizontalAlign = HorizontalAlign.Center
                        Table4.Rows.Add(Row1)
                    End If

                    If SelLevelNo = "" Or SelLevelNo <> RdSet("LevelNo").ToString Then
                        SelLevelNo = RdSet("LevelNo").ToString
                        Dim cAct As New TableCell
                        cAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.ColumnSpan = 4
                        Dim rAct As New TableRow
                        'rAct.CssClass = "SMSelected"
                        rAct.HorizontalAlign = HorizontalAlign.Center
                        rAct.BackColor = Drawing.Color.GhostWhite
                        cAct.Text = "Level Name: " & RdSet("levelname").ToString
                        rAct.Cells.Add(cAct)
                        Table4.Rows.Add(rAct)

                        Dim Cell1 As New TableCell
                        Dim Cell2 As New TableCell
                        Dim Cell3 As New TableCell
                        Dim Cell4 As New TableCell
                        Dim Cell5 As New TableCell
                        Dim Row1 As New TableRow
                        ' Row1.CssClass = "SMSelected"
                        Cell1.Text = "Name"
                        Cell2.Text = "UserName"
                        Cell3.Text = "Direct"
                        Cell4.Text = "Remove"
                        Cell1.HorizontalAlign = HorizontalAlign.Center
                        Cell2.HorizontalAlign = HorizontalAlign.Center
                        Cell3.HorizontalAlign = HorizontalAlign.Center
                        Cell4.HorizontalAlign = HorizontalAlign.Center
                        Cell1.CssClass = "alt1"
                        Cell2.CssClass = "alt1"
                        Cell3.CssClass = "alt1"
                        Cell4.CssClass = "alt1"
                        Row1.BackColor = Drawing.Color.GhostWhite
                        Row1.Cells.Add(Cell1)
                        Row1.Cells.Add(Cell2)

                        If HDirLevel.Value = RdSet("LevelNo").ToString Then
                            Row1.Cells.Add(Cell3)
                        Else
                            Cell1.ColumnSpan = 2
                        End If

                        Row1.Cells.Add(Cell4)
                        Row1.HorizontalAlign = HorizontalAlign.Center
                        Table4.Rows.Add(Row1)

                    End If

                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim c4 As New TableCell
                    Dim c5 As New TableCell
                    'c5.Text = "<img src=remove.gif style='cursor:hand;' onclick=poptastic('" & RdSet("TrackID").ToString & "')>"
                    c5.Text = "<input id=""Button1"" style='cursor:hand;' type=""button"" value=""Remove"" class=""button"" onclick=poptastic('" & RdSet("TrackID").ToString & "') />"
                    Dim r As New TableRow
                    c2.Text = RdSet("uname").ToString
                    c3.Text = RdSet("username").ToString
                    c4.Text = RdSet("direct").ToString
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
                    'Response.Write(RdSet("uName") & RdSet("LevelName") & RdSet("AccountName") & RdSet("pName"))
                Next
                DSPhyUsersList.Dispose()
            End If

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
    
    Sub PageStatus()
        If PhyState.Value = 0 Then
            Loadpage()
        ElseIf PhyState.Value = 1 Then
            BtnSubmit7.Focus()
            ActSearch_Click()
        ElseIf PhyState.Value = 2 Then
            btnsubmit3.Focus()
            PhySearch_Click()
        End If
    End Sub
    Sub Loadpage()
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
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim DSAct As New DataSet
        Dim clsAct As New ETS.BL.Accounts
        With clsAct
            '.ContractorID = Session("ContractorID").ToString
            '._WhereString.Append(" and (Isdeleted is NULL or Isdeleted = 'False') and AccountName like '%" & TxtAname.Text & "%' and AccountNo like '%" & TXtAnumber.Text & "%'")
            DSAct = .getAccountList(Session("WorkGroupID").ToString, Session("ContractorID").ToString, " AND AccountName like '%" & TxtAname.Text & "%' AND AccountNo like '%" & TXtAnumber.Text & "%' ")
        End With
        clsAct = Nothing
        Dim i As Integer
        Dim K As Integer
        K = 0
        If DSAct.Tables.Count > 0 Then
            For Each DRRec1 As DataRow In DSAct.Tables(0).Rows
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
                TblAccount.Rows.Add(r)

            Next
            DSAct.Dispose()
            PnlActSearch.Visible = False
            PnlActSelect.Visible = True
            Panel6.Visible = False
            Panel7.Visible = False
        Else
            PnlActSearch.Visible = True
            PnlActSelect.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
            MsgDisp.Text = "No account found."
        End If
        Dim CB As New Button
        Dim r2 As New TableRow
        Dim c4 As New TableCell
        c4.ColumnSpan = 3
        c4.Style("text-align") = "center"
        BtnSubmit7.Visible = True
        c4.Controls.Add(BtnSubmit7)
        r2.Cells.Add(c4)
        TblAccount.Rows.Add(r2)

    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class


