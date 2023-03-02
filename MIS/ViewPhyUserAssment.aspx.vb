Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Write(PrdState.Value)

        MsgDisp.Visible = False

        Table4.Visible = False

        If Not IsPostBack Then



            Panel2.Visible = False
            Panel3.Visible = False
            Panel1.Visible = True
            PrdState.Value = 0

        Else

     
        End If



    End Sub









    Protected Sub UserSearch_Click()

        Panel2.Visible = True
        Panel1.Visible = False
        Panel3.Visible = False



        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim Sqlstring As String
        Sqlstring = "SELECT IntialProduction from DBO.tblRSSStatus"
        Dim CmdRecI As New SqlCommand(Sqlstring, New SqlConnection(strConn))
        Try
            CmdRecI.Connection.Open()
            Dim DRRecI As SqlDataReader = CmdRecI.ExecuteReader()
            If DRRecI.HasRows Then
                If DRRecI.Read = True Then
                    HDirLevel.Value = DRRecI("IntialProduction").ToString
                    'Response.Write("HDIrLevel:" & HDirLevel.Value)
                End If
            End If
            DRRecI.Close()
        Finally
            If CmdRecI.Connection.State = System.Data.ConnectionState.Open Then
                CmdRecI.Connection.Close()
                CmdRecI = Nothing
            End If
        End Try

        'Response.Write("Select U.* from tblUsers U INNER JOIN tblUsersLEvels L ON L.UserID = U.UserID where L.ProductionLevel Is Not NULL and L.ProductionLevel > 0 and (U.Isdeleted is NULL or U.Isdeleted = 'False')  and U.Username like '%" & TxtUname.Text & "%'  and  U.firstname like '%" & TxtFname.Text & "%'  and  U.Lastname like '%" & TxtLname.Text & "%'   order by U.FirstName")
        Dim SQLCmd As New SqlCommand("Select U.* from tblUsers U INNER JOIN tblUsersLEvels L ON L.UserID = U.UserID where L.ProductionLevel Is Not NULL and    dbo.chkLevel(L.ProductionLevel, " & HDirLevel.Value & ")='True'  and  L.ProductionLevel > 0 and (U.Isdeleted is NULL or U.Isdeleted = 'False')  and U.Username like '%" & TxtUname.Text & "%'  and  U.firstname like '%" & TxtFname.Text & "%'  and  U.Lastname like '%" & TxtLname.Text & "%'   order by U.FirstName", New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            Dim i As Integer
            Dim J As Integer
            J = 0

            If DRRec.HasRows = True Then
                While (DRRec.Read)
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

                End While

                'Dim CB As New Button
                Dim r2 As New TableRow
                Dim c4 As New TableCell
                'CB.ID = "Btnsubmit3"
                'CB.Text = "Submit"
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
            ' Panel2.Controls.Add(t2)
            ' Panel2.DataBind()

            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try



    End Sub


    Protected Sub BtnSubmit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit1.Click
        PrdState.Value = 1
        PageStatus()
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
    Protected Sub UserStatus_Click()

        Panel3.Visible = True
        Panel1.Visible = False
        Panel2.Visible = False

        'Panel3.Controls.Clear()


        Dim SelectedUserLevel As Long
        Dim LevelNo As Long
        Dim LevelName As String

        Dim strConn As String
        Dim oRec As Data.SqlClient.SqlDataReader
        Dim oCommand As New Data.SqlClient.SqlCommand

        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim sqlString As String

        sqlString = "SELECT UL.ProductionLevel, U.FirstName + ' ' + U.LastName + ' (' + U.UserName + ')' AS uName FROM tblUsersLevels UL INNER JOIN tblUsers U ON UL.UserID = U.UserID where UL.UserID='" & HUserID.Value & "' order by U.FirstName"
        'Response.Write(sqlString)
        'Response.End()



        Dim SQLCmd As New SqlCommand(sqlString, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            Dim i As Integer
            Dim J As Integer
            Dim RecFound As String
            RecFound = "No"

            J = 0
            DRRec.Read()
            If DRRec.HasRows Then

                Dim uname As String
                uname = DRRec("uName")
                hUname.Value = uname

                'Dim c3 As New TableCell
                'Dim r1 As New TableRow
                'Dim c5 As New TableCell
                'c3.Text = "User Name"
                'c5.Text = uname
                ''r1.Cells.Add(c3)
                'r1.Cells.Add(c5)
                'TblUserstatus.Rows.Add(r1)

                If Not IsDBNull(DRRec("ProductionLevel")) Then

                    SelectedUserLevel = DRRec("ProductionLevel")
                    sqlString = "select LevelName,LevelNo from tblProductionLevels "
                    'Response.Write(sqlString)
                    oCommand = New Data.SqlClient.SqlCommand(sqlString, New SqlConnection(strConn))
                    Try
                        oCommand.Connection.Open()
                        oRec = oCommand.ExecuteReader()
                        Dim m As Integer
                        m = 0
                        If oRec.HasRows Then

                            While (oRec.Read)
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

                            End While
                            If RecFound = "Yes" Then
                                Dim r2 As New TableRow
                                Dim c4 As New TableCell
                                'CB.ID = "Btnsubmit3"
                                'CB.Text = "Submit"
                                BtnSubmit5.Visible = True
                                c4.ColumnSpan = 2
                                c4.Style("text-align") = "center"
                                c4.Controls.Add(BtnSubmit5)
                                r2.Cells.Add(c4)
                                TblUserstatus.Rows.Add(r2)
                            End If
                        Else
                            'Response.Write("No Records Found!")
                        End If
                        oRec.Close()
                    Finally
                        If oCommand.Connection.State = System.Data.ConnectionState.Open Then
                            oCommand.Connection.Close()
                            oCommand = Nothing
                        End If
                    End Try


                Else
                    SelectedUserLevel = 0
                End If
            Else
                PrdState.Value = 1
                MsgDisp.Visible = True
                MsgDisp.Text = "No Production Level found for this user"
                PageStatus()
                SelectedUserLevel = 0
                Exit Sub
            End If

            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try



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
        PageStatus()


    End Sub





    Sub PageStatus()
        If PrdState.Value = 1 Then
            UserSearch_Click()
        ElseIf PrdState.Value = 2 Then
            '    UserStatus_Click()
            'ElseIf PrdState.Value = 3 Then
            ActUserAssign()
        End If


    End Sub

    Protected Sub ActUserAssign()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Dim strConn As String
        Dim Lvl As String

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim i As Integer
        Dim j As Integer
        'Dim ViewLvl As String
        'ViewLvl = ""
        'Lvl = ""
        'j = 1
        'For i = 1 To TotLvl.Value
        '    Lvl = Request("LevelNo" & i)
        '    If Lvl <> "" Then
        '        'Response.Write(Lvl)
        '        If j = 1 Then
        '            ViewLvl = Lvl
        '            j = j + 1
        '        Else
        '            ViewLvl = ViewLvl & "," & Lvl
        '        End If
        '    End If


        'Next
        Dim sQuery3 As String
        ' If ViewLvl <> "" Then

        Dim SelLevelNo As String
        Dim SelActName As String
        Dim SelUserID As String
        Dim ColSpan As String
        SelLevelNo = ""
        SelActName = ""
        SelUserID = ""
        sQuery3 = "Select A.TrackID, U.firstname + ' ' + U.lastname as uname, P.firstname + ' ' + P.lastname as pname, Pl.LevelName, At.Accountname, P.PINNo, A.direct, PL.LevelNo, P.category, A.assigndate from tblProductionLevels PL, tblUsers U, tblUserPrLvlMgmt A, tblPhysicians P, tblAccounts At where  A.direct='True' and  A.LevelNo = '" & HDirLevel.Value & "' and A.UserID = U.UserID and A.LevelNo = PL.LevelNo and P.physicianID = A.physicianid and P.accountid = At.AccountID and  U.UserID ='" & HUserID.Value & "' order by Pl.Levelname, At.Accountname, U.firstname"
        'Response.Write(sQuery3)
        'Response.End()
        Dim cmdSel As New SqlCommand(sQuery3, New SqlConnection(strConn))
        Try
            cmdSel.Connection.Open()
            Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
            If RdSet.HasRows = True Then
                While (RdSet.Read)
                    If SelLevelNo = "" Then

                        SelLevelNo = RdSet("levelname").ToString
                        Dim cAct As New TableCell
                        cAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.ColumnSpan = 5
                        Dim rAct As New TableRow
                        rAct.CssClass = "SMSelected"
                        rAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.Text = "User Name: " & RdSet("uname").ToString & " | Level: " & RdSet("levelname").ToString
                        rAct.Cells.Add(cAct)
                        Table4.Rows.Add(rAct)
                    End If
                    If SelLevelNo <> RdSet("levelname").ToString Then
                        Dim CEmp As New TableCell
                        Dim REmp As New TableRow
                        CEmp.Text = "-"
                        CEmp.ColumnSpan = 5
                        REmp.Cells.Add(CEmp)
                        Table4.Rows.Add(REmp)
                        SelLevelNo = RdSet("levelname").ToString
                        Dim cAct As New TableCell
                        cAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.ColumnSpan = 5
                        Dim rAct As New TableRow
                        rAct.CssClass = "SMSelected"
                        rAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.Text = "User Name: " & RdSet("uname").ToString & " | Level: " & RdSet("levelname").ToString
                        rAct.Cells.Add(cAct)
                        Table4.Rows.Add(rAct)
                    End If

                    If SelActName = "" Then
                        SelActName = RdSet("Accountname").ToString
                        Dim cAct As New TableCell
                        cAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.ColumnSpan = 5
                        Dim rAct As New TableRow
                        ' rAct.CssClass = "SMSelected"
                        rAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.Text = "Account Name: " & RdSet("Accountname").ToString
                        rAct.Cells.Add(cAct)
                        Table4.Rows.Add(rAct)
                        Dim Cell1 As New TableCell
                        Dim Cell2 As New TableCell
                        Dim Cell3 As New TableCell
                        Dim Cell4 As New TableCell
                        Dim Cell5 As New TableCell
                        Dim Row1 As New TableRow
                        ' Row1.CssClass = "SMSelected"
                        Cell1.Text = "Diactator Name"
                        Cell2.Text = "UserName"
                        Cell3.Text = "Category"
                        Cell4.Text = "Modified Date"
                        Cell5.Text = "Remove Record"
                        Cell1.HorizontalAlign = HorizontalAlign.Center
                        Cell2.HorizontalAlign = HorizontalAlign.Center
                        Cell3.HorizontalAlign = HorizontalAlign.Center
                        Cell4.HorizontalAlign = HorizontalAlign.Center
                        Row1.BackColor = Drawing.Color.GhostWhite
                        Row1.Cells.Add(Cell1)
                        Row1.Cells.Add(Cell2)
                        Row1.Cells.Add(Cell3)
                        Row1.Cells.Add(Cell4)
                        Row1.Cells.Add(Cell5)
                        Row1.HorizontalAlign = HorizontalAlign.Center
                        Table4.Rows.Add(Row1)

                    End If
                    If SelActName <> RdSet("Accountname").ToString Then
                        SelActName = RdSet("Accountname").ToString
                        Dim cAct As New TableCell
                        cAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.ColumnSpan = 5
                        Dim rAct As New TableRow
                        ' rAct.CssClass = "SMSelected"
                        rAct.HorizontalAlign = HorizontalAlign.Center
                        cAct.Text = "Account Name: " & RdSet("Accountname").ToString
                        rAct.Cells.Add(cAct)
                        Table4.Rows.Add(rAct)
                        Dim Cell1 As New TableCell
                        Dim Cell2 As New TableCell
                        Dim Cell3 As New TableCell
                        Dim Cell4 As New TableCell
                        Dim Cell5 As New TableCell
                        Dim Row1 As New TableRow
                        ' Row1.CssClass = "SMSelected"
                        Cell1.Text = "Dictator Name"
                        Cell2.Text = "UserName"
                        Cell3.Text = "Category"
                        Cell4.Text = "Modified Date"
                        Cell5.Text = "Remove Record"
                        Cell1.HorizontalAlign = HorizontalAlign.Center
                        Cell2.HorizontalAlign = HorizontalAlign.Center
                        Cell3.HorizontalAlign = HorizontalAlign.Center
                        Cell4.HorizontalAlign = HorizontalAlign.Center
                        Row1.BackColor = Drawing.Color.GhostWhite
                        Row1.Cells.Add(Cell1)
                        Row1.Cells.Add(Cell2)
                        Row1.Cells.Add(Cell3)
                        Row1.Cells.Add(Cell4)
                        Row1.Cells.Add(Cell5)
                        Row1.HorizontalAlign = HorizontalAlign.Center
                        Table4.Rows.Add(Row1)

                    End If



                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim c4 As New TableCell
                    Dim c5 As New TableCell
                    Dim c6 As New TableCell

                    c5.Text = "<img src=remove.gif style='cursor:hand;' onclick=poptastic('" & RdSet("TrackID").ToString & "')>"
                    Dim r As New TableRow

                    c2.Text = RdSet("pname").ToString
                    c3.Text = RdSet("PINNo").ToString
                    c6.Text = RdSet("AssignDate").ToString

                    If RdSet("category").ToString = "A" Then
                        c4.Text = "A"
                    Else
                        c4.Text = "B"

                    End If
                    r.Cells.Add(c2)
                    r.Cells.Add(c3)
                    r.Cells.Add(c4)
                    r.Cells.Add(c6)
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
            RdSet.Close()

        Finally
            If cmdSel.Connection.State = System.Data.ConnectionState.Open Then
                cmdSel.Connection.Close()
                cmdSel = Nothing
            End If
        End Try
        'Else
        'Dim c3 As New TableCell
        'Dim r As New TableRow
        'c3.ColumnSpan = 4
        'c3.Text = "No Records Found"
        'r.Cells.Add(c3)
        'Table4.Visible = True

        'Table4.Rows.Add(r)

        'End If

    End Sub
End Class

