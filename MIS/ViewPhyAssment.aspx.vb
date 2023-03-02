Imports System.Data.SqlClient

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
        Dim clsPhy As ETS.BL.Physicians
        Dim DS As New Data.DataSet
        Dim DRRec1 As Data.DataTableReader
        Dim DV As New Data.DataView
        Try
            clsPhy = New ETS.BL.Physicians
            'clsPhy.AccountID = Request("Actid")
            DS = clsPhy.getPhysiciansList(Session("ContractorID"), Session("WorkGroupID"), Request("Actid"))
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DS.Tables(0), "(Isdeleted is NULL or Isdeleted = 'False')", "firstname", Data.DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        DRRec1 = DV.ToTable.CreateDataReader

                        Dim K As Integer
                        K = 0
                        If DRRec1.HasRows Then
                            While (DRRec1.Read)
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
                            End While
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
                            Exit Sub
                        End If
                    End If
                End If
            End If
            DRRec1.Close()
        Finally
            clsPhy = Nothing
            DRRec1 = Nothing
            DS = Nothing
            DV = Nothing
        End Try
    End Sub










    Protected Sub btnSubmit4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit4.Click
        'HUserID.Value = ""
        PrdState.Value = 2
        'Response.Write(Request("UserID"))
        HUserID.Value = Request("UserID")
        PageStatus()
        'Response.Write(HUserID.Value)
        'Dim CTls As Control
        '        'Response.Write(Request("UserID"))


        'For Each CTls In Page.FindControl("UserTable").Controls
        '    'Response.Write(CTls.ID)

        '    If TypeOf CTls Is RadioButton Then
        '        'Response.Write(CType(CTls, RadioButton).ID)


        '    End If

        '    'If cTls.Checked Then
        '    '    'Response.Write(cTls.ID)

        '    'End If
        '    '    Dim rdo As RadioButton
        '    '    rdo= cTls.FindControl(
        '    'Dim RB2 As RadioButton

        '    'RB2 = Table1.FindControl("UserID")
        '    ''Response.Write(RB2.Checked)





        'Next


    End Sub

    Protected Function chkLevel(ByVal AdminLevel As Long, ByVal Level As Long) As Boolean
        If (AdminLevel And Level) = Level Then
            chkLevel = True
        Else
            chkLevel = False
        End If
    End Function


    Protected Sub BtnSubmit5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit5.Click

        PrdState.Value = 3
        PnlActSearch.Visible = True
        BtnSubmit6.Focus()
        'Response.Write("Pressed")
        PageStatus()


    End Sub

    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit3.Click
        Panel7.Visible = False
        Panel6.Visible = False
        Table4.Visible = True
        Dim strConn As String
        Dim Phy As String

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim i As Integer
        Dim j As Integer
        Dim ViewPhy As String
        ViewPhy = ""
        Phy = ""
        j = 1
        For i = 1 To TotPhy.Value
            Phy = Request("PhyID" & i)
            If Phy <> "" Then
                'Response.Write(Lvl)
                If j = 1 Then
                    ViewPhy = "'" & Phy & "'"
                    j = j + 1
                Else
                    ViewPhy = ViewPhy & ",'" & Phy & "'"
                End If
            End If


        Next
        Dim sQuery3 As String
        If ViewPhy <> "" Then
            Dim Sqlstring As String
            Sqlstring = "SELECT IntialProduction from DBO.tblRSSStatus"
            Dim CmdRec As New SqlCommand(Sqlstring, New SqlConnection(strConn))
            Dim clsRSSStatus As ETS.BL.RSSStatus
            Dim DS As New Data.DataSet
            Dim DRRec As Data.DataTableReader
            Try
                clsRSSStatus = New ETS.BL.RSSStatus
                DS = clsRSSStatus.getRSSStatusList()
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        DRRec = DS.Tables(0).CreateDataReader

                        If DRRec.HasRows Then
                            If DRRec.Read = True Then
                                HDirLevel.Value = DRRec("IntialProduction").ToString
                            End If
                        End If
                    End If
                End If
                
                DRRec.Close()
            Finally
                clsRSSStatus = Nothing
                DRRec = Nothing
                DS = Nothing
            End Try
            Dim SelLevelNo As String
            Dim SelActName As String
            Dim SelUserID As String
            Dim ColSpan As String
            SelLevelNo = ""
            SelActName = ""
            SelUserID = ""


            Dim clsMTD As ETS.BL.MTDirectDictatorAssignments
            Dim DSRec As New Data.DataSet
            Dim RdSet As Data.DataTableReader
            Try
                clsMTD = New ETS.BL.MTDirectDictatorAssignments
                DSRec = clsMTD.GetMTDirectDictatorAssignList(HDirLevel.Value, Session("ContractorID"), ViewPhy)

                If DSRec.Tables.Count > 0 Then
                    If DSRec.Tables(0).Rows.Count > 0 Then
                        RdSet = DSRec.Tables(0).CreateDataReader

                        If RdSet.HasRows = True Then
                            While (RdSet.Read)
                                'Response.Write(RdSet("LevelNo").ToString)
                                If SelActName = "" Then
                                    SelActName = RdSet("Accountname").ToString
                                    Dim cAct As New TableCell
                                    cAct.HorizontalAlign = HorizontalAlign.Center
                                    cAct.ColumnSpan = 4
                                    Dim rAct As New TableRow
                                    rAct.HorizontalAlign = HorizontalAlign.Center
                                    cAct.Text = "Account Name: " & RdSet("Accountname").ToString
                                    cAct.ForeColor = Drawing.Color.Brown
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
                                    cAct.ForeColor = Drawing.Color.DarkGray
                                    cAct.Font.Bold = True
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
                                    cAct.ForeColor = Drawing.Color.DarkGray
                                    cAct.Font.Bold = True
                                    cAct.Text = "Dictator Name: " & RdSet("pname").ToString & " (" & RdSet("PinNo").ToString & ")"
                                    rAct.Cells.Add(cAct)
                                    Table4.Rows.Add(rAct)

                                    SelLevelNo = RdSet("LevelNo").ToString
                                    Dim cAct1 As New TableCell
                                    cAct1.HorizontalAlign = HorizontalAlign.Center
                                    cAct1.ColumnSpan = 4
                                    Dim rAct1 As New TableRow
                                    'rAct.CssClass = "SMSelected"
                                    rAct1.HorizontalAlign = HorizontalAlign.Center
                                    rAct1.BackColor = Drawing.Color.GhostWhite
                                    cAct1.Text = "Level Name: " & RdSet("levelname").ToString
                                    rAct1.Cells.Add(cAct1)
                                    'Table4.Rows.Add(rAct1)

                                    Dim Cell1 As New TableCell
                                    Dim Cell2 As New TableCell
                                    Dim Cell3 As New TableCell
                                    Dim Cell4 As New TableCell
                                    Dim Cell5 As New TableCell
                                    Dim Row1 As New TableRow
                                    ' Row1.CssClass = "SMSelected"
                                    Cell1.Text = "Name"
                                    Cell2.Text = "UserName"
                                    Cell3.Text = "Remove Record"
                                    Cell4.Text = "Modified Date"
                                    Cell1.HorizontalAlign = HorizontalAlign.Center
                                    Cell2.HorizontalAlign = HorizontalAlign.Center
                                    Cell3.HorizontalAlign = HorizontalAlign.Center
                                    Cell4.HorizontalAlign = HorizontalAlign.Center
                                    Row1.BackColor = Drawing.Color.GhostWhite
                                    Row1.Cells.Add(Cell1)
                                    Row1.Cells.Add(Cell2)

                                    'If HDirLevel.Value = RdSet("LevelNo").ToString Then
                                    '    Row1.Cells.Add(Cell3)
                                    'Else
                                    '    Cell1.ColumnSpan = 2
                                    'End If

                                    Row1.Cells.Add(Cell4)
                                    Row1.Cells.Add(Cell3)
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
                                    'Table4.Rows.Add(rAct)

                                    Dim Cell1 As New TableCell
                                    Dim Cell2 As New TableCell
                                    Dim Cell3 As New TableCell
                                    Dim Cell4 As New TableCell
                                    Dim Cell5 As New TableCell
                                    Dim Row1 As New TableRow
                                    ' Row1.CssClass = "SMSelected"
                                    Cell1.Text = "Name"
                                    Cell2.Text = "UserName"
                                    Cell3.Text = "Remove Record"
                                    Cell4.Text = "Modified Date"
                                    Cell1.HorizontalAlign = HorizontalAlign.Center
                                    Cell2.HorizontalAlign = HorizontalAlign.Center
                                    Cell3.HorizontalAlign = HorizontalAlign.Center
                                    Cell4.HorizontalAlign = HorizontalAlign.Center
                                    Row1.BackColor = Drawing.Color.GhostWhite
                                    Row1.Cells.Add(Cell1)
                                    Row1.Cells.Add(Cell2)

                                    'If HDirLevel.Value = RdSet("LevelNo").ToString Then
                                    '    Row1.Cells.Add(Cell3)
                                    'Else
                                    '    Cell1.ColumnSpan = 2
                                    'End If

                                    Row1.Cells.Add(Cell4)
                                    Row1.Cells.Add(Cell3)
                                    Row1.HorizontalAlign = HorizontalAlign.Center
                                    Table4.Rows.Add(Row1)

                                End If

                                Dim c2 As New TableCell
                                Dim c3 As New TableCell
                                Dim c4 As New TableCell
                                Dim c5 As New TableCell
                                c5.Text = "<img src=remove.gif style='cursor:hand;' onclick=poptastic('" & RdSet("TrackID").ToString & "')>"
                                Dim r As New TableRow
                                c2.Text = RdSet("uname").ToString
                                c3.Text = RdSet("username").ToString
                                'c4.Text = RdSet("direct").ToString
                                c4.Text = RdSet("AssignDate").ToString
                                r.Cells.Add(c2)
                                r.Cells.Add(c3)
                                r.Cells.Add(c4)
                                r.Cells.Add(c5)
                                Table4.Visible = True
                                Table4.Rows.Add(r)
                                'Response.Write(RdSet("uName") & RdSet("LevelName") & RdSet("AccountName") & RdSet("pName"))
                            End While
                        Else
                            Dim c3 As New TableCell
                            Dim r As New TableRow
                            c3.ColumnSpan = 4
                            c3.Text = "No Records Found"
                            r.Cells.Add(c3)
                            Table4.Visible = True

                            Table4.Rows.Add(r)
                        End If
                    End If
                End If
                'RdSet.Close()


            Finally
                RdSet = Nothing
                DSRec = Nothing
                clsMTD = Nothing
            End Try
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

    Protected Sub PhyStatus_Click()
        Dim strConn As String
        Dim sqlstring As String
        Dim i As Integer
        Dim lvl As String
        Dim RecFound As Boolean
        RecFound = False
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        sqlstring = "SELECT IntialProduction from DBO.tblRSSStatus"
        Dim CmdRec As New SqlCommand(sqlstring, New SqlConnection(strConn))
        Try
            CmdRec.Connection.Open()
            Dim DRRec As SqlDataReader = CmdRec.ExecuteReader()
            If DRRec.HasRows Then
                If DRRec.Read = True Then
                    For i = 1 To TotLvl.Value
                        lvl = "LevelNo" & i
                        If Request(lvl) <> "" Then
                            If Request(lvl) = CInt(DRRec("IntialProduction").ToString) Then
                                '                            Response.Write(Request(lvl) & "#" & DRRec("IntialProduction").ToString)
                                RecFound = True
                            End If
                        End If
                    Next
                End If
            End If
            DRRec.Close()
        Finally
            If CmdRec.Connection.State = System.Data.ConnectionState.Open Then
                CmdRec.Connection.Close()
                CmdRec = Nothing
            End If
        End Try
        PnlActSearch.Visible = False
        PnlActSelect.Visible = False
        Panel7.Visible = True
        Panel6.Visible = False
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim PhyId As String
        Dim direct As String
        'Response.Write("Total" & TotPhy.Value)
        Dim K As Integer
        K = 0
        For i = 1 To TotPhy.Value
            PhyId = "PhyID" & i
            direct = "Direct" & i
            Dim SQuery As String
            If Request(PhyId) <> "" Then
                SQuery = "Select * from tblphysicians where physicianid like '%" & Request(PhyId) & "%' "
                ' Response.Write(SQuery)
                Dim CmdRec1 As New SqlCommand(SQuery, New SqlConnection(strConn))
                Try
                    CmdRec1.Connection.Open()
                    Dim DRRec1 As SqlDataReader = CmdRec1.ExecuteReader()
                    If DRRec1.Read Then
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
                        CB1.Checked = True

                        Dim CB2 As New CheckBox
                        CB2.ID = "Direct" & K
                        CB2.InputAttributes.Add("Value", True)
                        If Request("direct") = "True" Then
                            CB2.Checked = True
                        Else
                            CB2.Checked = False
                        End If


                        'TotPhy.Value = K
                        c.Text = DRRec1("firstname")
                        c1.Text = DRRec1("lastname")
                        c2.Text = DRRec1("PinNo")
                        c3.Controls.Add(CB1)
                        c5.Controls.Add(CB2)
                        r.Cells.Add(c3)
                        r.Cells.Add(c)
                        r.Cells.Add(c1)
                        r.Cells.Add(c2)
                        If RecFound = True Then
                            r.Cells.Add(c5)
                        End If

                        Table3.Rows.Add(r)

                    End If
                    DRRec1.Close()

                Finally
                    If CmdRec1.Connection.State = System.Data.ConnectionState.Open Then
                        CmdRec1.Connection.Close()
                        CmdRec1 = Nothing
                    End If
                End Try
            End If

        Next
        TotPhy.Value = K
        If RecFound = False Then
            DirCell.Visible = False
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
        ElseIf PhyState.Value = 3 Then
            PhyStatus_Click()
        End If
        If PrdState.Value = 3 And PhyState.Value = 3 Then
            BtnAssign.Visible = True
            BtnAssign.Focus()
        Else
            BtnAssign.Visible = False
            BtnAssign.Focus()
        End If

    End Sub

    Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAssign.Click

        Panel6.Visible = False
        Panel7.Visible = False
        PnlActSearch.Visible = False
        PnlActSelect.Visible = False

        Dim strConn As String
        Dim Lvl As String
        Dim direct As String
        Dim phyid As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim i As Integer
        Dim j As Integer

        For i = 1 To TotLvl.Value
            Lvl = "LevelNo" & i

            For j = 1 To TotPhy.Value
                phyid = "PhyID" & j
                direct = "Direct" & j

                Dim sQuery1 As String
                sQuery1 = "Select * from tblUserPrLvlMgmt where UserID='" & HUserID.Value & "' and PhysicianId='" & Request(phyid) & "' and LevelNo = '" & Request(Lvl) & "'"

                Dim cmdIns As New SqlCommand(sQuery1, New SqlConnection(strConn))
                Try
                    cmdIns.Connection.Open()
                    Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()

                    If DRRec.Read = False Then
                        Dim sQuery2 As String
                        sQuery2 = "INSERT INTO tblUserPrLvlMgmt (UserID, physicianid, LevelNo, AssignDate, Direct)VALUES('" & HUserID.Value & "','" & Request(phyid) & "','" & Request(Lvl) & "','" & Now & "','" & Request(direct) & "')"
                        Dim cmdUp As New SqlCommand(sQuery2, New SqlConnection(strConn))
                        Try
                            cmdUp.Connection.Open()
                            cmdUp.ExecuteNonQuery()
                        Finally
                            If cmdUp.Connection.State = System.Data.ConnectionState.Open Then
                                cmdUp.Connection.Close()
                                cmdUp = Nothing
                            End If
                        End Try

                    Else

                    End If
                    DRRec.Close()

                Finally
                    If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
                        cmdIns.Connection.Close()
                        cmdIns = Nothing
                    End If
                End Try

            Next
        Next
        Dim sQuery3 As String
        sQuery3 = "Select U.firstname + ' ' + U.lastname as uname, P.firstname + ' ' + P.lastname as pname, Pl.LevelName, A.direct,  At.Accountname from tblProductionLevels PL, tblUsers U, tblUserPrLvlMgmt A, tblPhysicians P, tblAccounts At where A.UserID = U.UserID and A.LevelNo = PL.LevelNo and P.physicianID = A.physicianid and P.accountid = At.AccountID and U.UserID ='" & HUserID.Value & "' and AT.contractorID='" & Session("ContractorID") & "' and U.contractorID='" & Session("ContractorID") & "' order by Pl.Levelname, At.Accountname, U.firstname"
        'Response.Write(sQuery3)
        'Response.End()
        Dim cmdSel As New SqlCommand(sQuery3, New SqlConnection(strConn))
        Try
            cmdSel.Connection.Open()
            Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
            'If RdSet.Read = True Then
            While (RdSet.Read)
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim c4 As New TableCell
                Dim r As New TableRow

                c.Text = RdSet("uname").ToString
                c1.Text = RdSet("levelname").ToString
                c2.Text = RdSet("Accountname").ToString
                c3.Text = RdSet("pname").ToString
                c4.Text = RdSet("direct").ToString
                r.Cells.Add(c)
                r.Cells.Add(c1)
                r.Cells.Add(c2)
                r.Cells.Add(c3)
                r.Cells.Add(c4)
                Table4.Visible = True

                Table4.Rows.Add(r)

                'Response.Write(RdSet("uName") & RdSet("LevelName") & RdSet("AccountName") & RdSet("pName"))
            End While
            'End If
            RdSet.Close()

        Finally
            If cmdSel.Connection.State = System.Data.ConnectionState.Open Then
                cmdSel.Connection.Close()
                cmdSel = Nothing
            End If
        End Try

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
        Dim clsAcc As ETS.BL.Accounts
        Dim Ds As New Data.DataSet
        Dim DV As New Data.DataView
        Dim DRRec1 As Data.DataTableReader

        Try
            clsAcc = New ETS.BL.Accounts
            'clsAcc.ContractorID = Session("ContractorID")
            Ds = clsAcc.getAccountList(Session("WorkGroupID"), Session("ContractorID"), String.Empty)
            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(Ds.Tables(0), "(Isdeleted is NULL or Isdeleted = 'False') and AccountName like '%" & TxtAname.Text & "%'", "accountname", Data.DataViewRowState.CurrentRows)
                    If Not String.IsNullOrEmpty(TXtAnumber.Text) Then
                        DV.RowFilter = " AccountNo=" & TXtAnumber.Text.ToString
                    End If

                    If DV.Count > 0 Then
                        DRRec1 = DV.ToTable.CreateDataReader

                        Dim i As Integer
                        Dim K As Integer



                        K = 0
                        If DRRec1.HasRows Then
                            While (DRRec1.Read)
                                K = K + 1
                                Dim c As New TableCell
                                Dim c1 As New TableCell
                                Dim c2 As New TableCell
                                Dim c3 As New TableCell
                                Dim r As New TableRow
                                'Dim CB1 As New CheckBox
                                'CB1.ID = "AccountID" & K
                                'CB1.InputAttributes.Add("Value", DRRec1("AccountID").ToString)
                                Dim RB As New RadioButton
                                RB.GroupName = "ActID"
                                RB.ID = DRRec1("AccountID").ToString
                                TotAct.Value = K
                                c.Text = DRRec1("AccountName")
                                c1.Text = DRRec1("AccountNo")
                                'c2.Text = DRRec1("PinNo")
                                c3.Controls.Add(RB)
                                r.Cells.Add(c3)
                                r.Cells.Add(c)
                                r.Cells.Add(c1)
                                'r.Cells.Add(c2)
                                TblAccount.Rows.Add(r)

                            End While
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
                    End If
                End If
            End If
            
            Dim CB As New Button
            Dim r2 As New TableRow
            Dim c4 As New TableCell
            'CB.ID = "Btnsubmit3"
            'CB.Text = "Submit"
            c4.ColumnSpan = 3
            c4.Style("text-align") = "center"
            BtnSubmit7.Visible = True

            c4.Controls.Add(BtnSubmit7)


            r2.Cells.Add(c4)
            TblAccount.Rows.Add(r2)


            'Panel6.Controls.Add(t2)
            'Panel6.Controls.Add(CB)
            DRRec1.Close()
        Finally
            clsAcc = Nothing
            Ds = Nothing
            DRRec1 = Nothing
        End Try

    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class


