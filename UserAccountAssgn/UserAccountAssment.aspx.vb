Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Write(PrdState.Value)

        BtnAssign.Visible = False
        Table4.Visible = False

        If Not IsPostBack Then

            ChkAll.Attributes.Add("onclick", "changeAll();")
            BtnAssign.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False

            Panel1.Visible = True
            Panel5.Visible = False
            PrdState.Value = 0
            ActState.Value = 0
        Else


        End If



    End Sub







    Protected Sub ActSearch_Click()

        Panel6.Visible = True
        Panel5.Visible = False
        Panel7.Visible = False




        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both




        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim SQLCmd1 As New SqlCommand("Select * from tblAccounts where   (Isdeleted is NULL) or (Isdeleted = 'False') and AccountName like '%" & TxtAname.Text & "%' ", New SqlConnection(strConn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
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
                    Dim CB1 As New CheckBox
                    CB1.ID = "AccountID" & K
                    CB1.InputAttributes.Add("Value", DRRec1("AccountID").ToString)
                    CB1.InputAttributes.Add("onclick", "highlightRow(this);")
                    TotAct.Value = K
                    c.Text = DRRec1("AccountName")
                    c1.Text = DRRec1("AccountNo")
                    'c2.Text = DRRec1("PinNo")
                    c3.Controls.Add(CB1)
                    r.Cells.Add(c3)
                    r.Cells.Add(c)
                    r.Cells.Add(c1)
                    'r.Cells.Add(c2)
                    Table1.Rows.Add(r)

                End While
            End If



            Dim CB As New Button
            Dim r2 As New TableRow
            Dim c4 As New TableCell
            'CB.ID = "Btnsubmit3"
            'CB.Text = "Submit"
            c4.ColumnSpan = 3
            c4.Style("text-align") = "center"
            btnsubmit3.Visible = True

            c4.Controls.Add(btnsubmit3)


            r2.Cells.Add(c4)
            Table1.Rows.Add(r2)


            'Panel6.Controls.Add(t2)
            'Panel6.Controls.Add(CB)
            DRRec1.Close()
        Finally
            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd1.Connection.Close()
                SQLCmd1 = Nothing
            End If
        End Try

    End Sub


    Protected Sub UserSearch_Click()

        Panel2.Visible = True
        Panel1.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False


        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCMD As New SqlCommand("Select U.* from tblUsers U INNER JOIN tblUsersLEvels L ON L.UserID = U.UserID where L.ProductionLevel Is Not NULL and L.ProductionLevel > 0 and (U.Isdeleted is NULL or U.Isdeleted = 'False') and U.Username like '%" & TxtUname.Text & "%'  order by U.FirstName", New SqlConnection(strConn))
        Try
            SQLCMD.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCMD.ExecuteReader()
            Dim i As Integer
            Dim J As Integer
            J = 0
            If DRRec.HasRows Then
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
                    'RB.Checked = True
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

            End If
            ' Panel2.Controls.Add(t2)
            ' Panel2.DataBind()

            DRRec.Close()
        Finally
            If SQLCMD.Connection.State = System.Data.ConnectionState.Open Then
                SQLCMD.Connection.Close()
                SQLCMD = Nothing
            End If
        End Try

    End Sub


    Protected Sub BtnSubmit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit1.Click
        PrdState.Value = 1
        PageStatus()
    End Sub

    Protected Sub BtnSubmit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit2.Click
        ActState.Value = 1
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
        Panel4.Visible = False
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
                    ''Response.Write(sqlString)
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
                SelectedUserLevel = 0
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
        Panel5.Visible = True
        PrdState.Value = 3
        PageStatus()


    End Sub

    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit3.Click
        ActState.Value = 2
        'Dim i As Integer
        'Dim AccountID As String
        ''Response.Write(TotAct.Value)

        'For i = 1 To TotAct.Value

        '    AccountID = "AccountID" & i
        '    Session(AccountID) = Request(AccountID)
        '    '    'Response.Write(AccountID)

        '    '    Response.Write(Request(AccountID))


        'Next
        PageStatus()

    End Sub
    Protected Sub UserFStatus_Click()

        Panel4.Visible = True
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        'Panel3.Controls.Clear()


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

        Dim Lvl As String
        Dim i As Integer
        Dim L As Integer
        L = 0
        For i = 1 To TotLvl.Value
            Lvl = "LevelNo" & i
            If Request(Lvl) <> "" Then
                sqlString = "select LevelName,LevelNo from tblProductionLevels where LevelNo = '" & Request(Lvl) & "'"


                oCommand = New Data.SqlClient.SqlCommand(sqlString, New SqlConnection(strConn))
                Try
                    oCommand.Connection.Open()
                    oRec = oCommand.ExecuteReader()
                    'Response.End()
                    If oRec.HasRows Then
                        If oRec.Read() Then

                            L = L + 1
                            LevelNo = oRec("LevelNo")
                            LevelName = oRec("LevelName")


                            Dim CB As New CheckBox
                            Dim c As New TableCell
                            Dim r As New TableRow
                            Dim c1 As New TableCell
                            CB.ID = "LevelNo" & L
                            CB.Checked = True
                            c.Controls.Add(CB)
                            c1.Text = LevelName
                            CB.InputAttributes.Add("Value", LevelNo)
                            r.Cells.Add(c)
                            r.Cells.Add(c1)
                            Table2.Rows.Add(r)
                            oCommand.Connection.Close()
                        End If
                    End If
                    oRec.Close()
                Finally
                    If oCommand.Connection.State = System.Data.ConnectionState.Open Then
                        oCommand.Connection.Close()
                        oCommand = Nothing
                    End If
                End Try
            End If

        Next
        TotLvl.Value = L

    End Sub
    Protected Sub ActStatus_Click()

        Panel7.Visible = True
        Panel5.Visible = False
        Panel6.Visible = False
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both



        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim i As Integer
        Dim AccountID As String
        'Response.Write("Total" & TotAct.Value)
        Dim K As Integer
        K = 0

        For i = 1 To TotAct.Value
            AccountID = "AccountID" & i
            Dim SQuery As String
            If Request(AccountID) <> "" Then
                SQuery = "Select * from tblaccounts where accountid like '%" & Request(AccountID) & "%' "
                ' Response.Write(SQuery)
                Dim SQLCmd1 As New SqlCommand(SQuery, New SqlConnection(strConn))
                Try
                    SQLCmd1.Connection.Open()
                    Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                    If DRRec1.HasRows Then
                        If DRRec1.Read Then
                            K = K + 1
                            Dim c As New TableCell
                            Dim c1 As New TableCell
                            Dim c2 As New TableCell
                            Dim c3 As New TableCell
                            Dim r As New TableRow
                            Dim CB1 As New CheckBox
                            CB1.ID = "AccountID" & K
                            CB1.InputAttributes.Add("Value", DRRec1("AccountID").ToString)
                            CB1.InputAttributes.Add("onclick", "highlightRow(this);")
                            CB1.Checked = True

                            'TotAct.Value = K
                            c.Text = DRRec1("Accountname")
                            c1.Text = DRRec1("AccountNO")
                            'c2.Text = DRRec1("PinNo")
                            c3.Controls.Add(CB1)
                            r.Cells.Add(c3)
                            r.Cells.Add(c)
                            r.Cells.Add(c1)
                            'r.Cells.Add(c2)
                            Table3.Rows.Add(r)

                        End If
                    End If
                    DRRec1.Close()

                Finally
                    If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                        SQLCmd1.Connection.Close()
                        SQLCmd1 = Nothing
                    End If
                End Try

            End If

        Next
        TotAct.Value = K





    End Sub

    Sub PageStatus()
        If PrdState.Value = 1 Then
            UserSearch_Click()
        ElseIf PrdState.Value = 2 Then
            UserStatus_Click()
        ElseIf PrdState.Value = 3 Then
            UserFStatus_Click()
        End If

        If ActState.Value = 1 Then
            ActSearch_Click()
        ElseIf ActState.Value = 2 Then
            ActStatus_Click()
        End If
        If PrdState.Value = 3 And ActState.Value = 2 Then
            BtnAssign.Visible = True
        Else
            BtnAssign.Visible = False
        End If

    End Sub

    Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAssign.Click
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Dim strConn As String
        Dim Lvl As String
        Dim AccountID As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim i As Integer
        Dim j As Integer

        For i = 1 To TotLvl.Value
            Lvl = "LevelNo" & i

            For j = 1 To TotAct.Value
                AccountID = "AccountID" & j

                Dim sQuery1 As String
                sQuery1 = "Select * from tblAccountUserAssgn where UserID='" & HUserID.Value & "' and AccountID='" & Request(AccountID) & "' and LevelNo = '" & Request(Lvl) & "'"

                Dim cmdIns As New SqlCommand(sQuery1, New SqlConnection(strConn))
                Try
                    cmdIns.Connection.Open()
                    Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()

                    If DRRec.HasRows = False Then
                        Dim sQuery2 As String
                        sQuery2 = "INSERT INTO tblAccountUserAssgn (UserID, AccountID, LevelNo, AssignDate)VALUES('" & HUserID.Value & "','" & Request(AccountID) & "','" & Request(Lvl) & "','" & Now & "')"
                        ' Response.Write(sQuery2)
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
        sQuery3 = "Select U.Firstname + ' ' + U.Lastname as uname, Pl.LevelName, A.AccountName, A.AccountNO from tblProductionLevels PL, tblUsers U, tblAccountUserAssgn AU, tblAccounts A where AU.AccountID = A.AccountID and AU.LevelNo = PL.LevelNo and AU.UserID = U.UserID and U.UserID ='" & HUserID.Value & "' order by Pl.Levelname, A.Accountname, U.firstname"
        'Response.Write(sQuery3)
        'Response.End()
        Dim cmdSel As New SqlCommand(sQuery3, New SqlConnection(strConn))
        Try
            cmdSel.Connection.Open()
            Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
            If RdSet.HasRows = True Then
                While (RdSet.Read)
                    Dim c As New TableCell
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim r As New TableRow

                    c.Text = RdSet("uname")
                    c1.Text = RdSet("levelname")
                    c2.Text = RdSet("Accountname")
                    c3.Text = RdSet("AccountNO")

                    r.Cells.Add(c)
                    r.Cells.Add(c1)
                    r.Cells.Add(c2)
                    r.Cells.Add(c3)
                    Table4.Visible = True

                    Table4.Rows.Add(r)

                    'Response.Write(RdSet("uName") & RdSet("LevelName") & RdSet("AccountName") & RdSet("pName"))
                End While
            End If
            RdSet.Close()



        Finally
            If cmdSel.Connection.State = System.Data.ConnectionState.Open Then
                cmdSel.Connection.Close()
                cmdSel = Nothing
            End If
        End Try

    End Sub
End Class

