Imports System.Data.SqlClient
Imports SASMTPLib
Imports System.Data
Partial Class Templates_TemplateAssment
    Inherits BasePage
    Dim RB As RadioButton
    Public DirLevelNo As String
    Protected WithEvents RB1 As System.Web.UI.WebControls.RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        MsgDisp.Text = ""
        BtnAssign.Visible = False
        Table4.Visible = False

        If Not IsPostBack Then
            'BtnSubmit1.Focus()
            ''ChkAll.Attributes.Add("onclick", "changeAll();")
            ''ChkDirect.Attributes.Add("onclick", "changeAllDirect();")
            'ChTmpAll.Attributes.Add("onclick", "changeTmpAll();")
            'ChPhyAll.Attributes.Add("onclick", "changePhyAll();")
            BtnAssign.Visible = False
            'Panel2.Visible = False
            'Panel3.Visible = False
            'Panel4.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
            'Panel1.Visible = False
            PnlActSearch.Visible = True
            PnlActSelect.Visible = False
            PrdState.Value = 0
            PhyState.Value = 0
        Else
            PageStatus()
        End If



    End Sub







    Protected Sub PhySearch_Click()
        If Request("ActID") <> "" Then
            HActID.Value = Request("ActID")
        End If
        PnlActSearch.Visible = False
        PnlActSelect.Visible = False
        Panel6.Visible = True
        Panel7.Visible = False

        Dim clsPhy As New ETS.BL.Physicians
        Dim DSPhy As DataSet = clsPhy.getPhysiciansList(Session("ContractorID"), Session("WorkGroupID"), HActID.Value)
        clsPhy = Nothing
        Dim DRRec1 As DataTableReader
        Dim NotChecked As Boolean
        NotChecked = False
        If DSPhy.Tables.Count > 0 Then
            If DSPhy.Tables(0).Rows.Count > 0 Then
                DRRec1 = DSPhy.Tables(0).CreateDataReader
                Dim K As Integer
                K = 0

                If DRRec1.HasRows Then
                    NotChecked = True
                    While (DRRec1.Read)
                        K = K + 1
                        Dim c As New TableCell
                        Dim c1 As New TableCell
                        Dim c2 As New TableCell
                        Dim c3 As New TableCell
                        Dim c5 As New TableCell
                        Dim r As New TableRow
                        RB1 = New RadioButton
                        RB1.AutoPostBack = True


                        c3.Text = "<a href='viewassignment.aspx?phyid=" & DRRec1("PhysicianId").ToString & "' target='myframe'>View </a>"

                        TotPhy.Value = K
                        c.HorizontalAlign = HorizontalAlign.Left
                        c1.HorizontalAlign = HorizontalAlign.Left
                        c2.HorizontalAlign = HorizontalAlign.Left
                        c.Text = DRRec1("firstname")
                        c1.Text = DRRec1("lastname")
                        c2.Text = DRRec1("PinNo")

                        r.Cells.Add(c3)
                        r.Cells.Add(c)
                        r.Cells.Add(c1)
                        r.Cells.Add(c2)

                        Table1.Rows.Add(r)

                    End While
                Else
                    PhyState.Value = 0
                    PageStatus()
                    DRRec1.Close()
                    Exit Sub
                End If
                DRRec1.Close()
            End If
        End If
    End Sub






    'Protected Sub BtnSubmit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit1.Click
    '    PrdState.Value = 1
    '    PageStatus()
    'End Sub



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


        PnlActSearch.Visible = True
        BtnSubmit6.Focus()
        'Response.Write("Pressed")
        PageStatus()


    End Sub

    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit3.Click
        PhyState.Value = 3
        'Panel1.Visible = True
        'Dim i As Integer
        'Dim PhyId As String
        ''Response.Write(TotPhy.Value)

        'For i = 1 To TotPhy.Value

        '    PhyId = "PhyID" & i
        '    Session(PhyId) = Request(PhyId)
        '    '    'Response.Write(PhyId)

        '    '    Response.Write(Request(PhyId))


        'Next
        PageStatus()

    End Sub
    'Protected Sub TemplateSelect_Click()


    '    'Panel3.Controls.Clear()
    '    'PnlActSearch.Visible = True

    '    Dim TemplateID As String
    '    Dim TemplateName As String

    '    Dim strConn As String
    '    Dim oRec As Data.SqlClient.SqlDataReader
    '    Dim oCommand As New Data.SqlClient.SqlCommand

    '    Dim t2 As New Table
    '    t2.Style("width") = "100%"
    '    t2.BorderWidth = 2
    '    t2.GridLines = GridLines.Both
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim sqlString As String
    '    Dim L As Integer
    '    L = 0
    '    'For i = 1 To TotLvl.Value
    '    '    Lvl = "LevelNo" & i
    '    '    If Request(Lvl) <> "" Then
    '    'sqlString = "select TemplateID,TemplateName from tblTemplates where TemplateName like '" & TxtTmpname.Text & "'"
    '    sqlString = "SELECT T.TemplateID , T.TemplateName FROM tblTemplates T where  T.TemplateName like '" & TxtTmpname.Text & "' "
    '    'Response.Write(sqlString)
    '    oCommand = New Data.SqlClient.SqlCommand(sqlString, New SqlConnection(strConn))
    '    Try
    '        oCommand.Connection.Open()
    '        oRec = oCommand.ExecuteReader()
    '        'Response.End()
    '        If oRec.HasRows Then
    '            While oRec.Read()
    '                TemplateID = oRec("TemplateID").ToString
    '                TemplateName = oRec("TemplateName").ToString
    '                Dim c As New TableCell
    '                Dim r As New TableRow
    '                Dim c1 As New TableCell
    '                'If IsDBNull(oRec("Physicianid")) Then
    '                c.Text = "<input id=TemplateID type=checkbox onclick=highlightRow(this); name=TemplateID Value=" & TemplateID & "  >"
    '                'Else
    '                'c.Text = "<input id=TemplateID type=checkbox onclick=highlightRow(this);  name=TemplateID Value=" & TemplateID & "  checked=checked>"
    '                'End If


    '                c1.Text = TemplateName
    '                r.Cells.Add(c)
    '                r.Cells.Add(c1)
    '                Table2.Rows.Add(r)
    '            End While
    '            Panel4.Visible = True
    '            Panel1.Visible = False
    '            Panel2.Visible = False
    '            Panel3.Visible = False

    '        Else
    '            Panel4.Visible = False
    '            Panel1.Visible = True
    '            Panel2.Visible = False
    '            Panel3.Visible = False
    '            oRec.Close()
    '            'If oCommand.Connection.State = System.Data.ConnectionState.Open Then
    '            '    oCommand.Connection.Close()
    '            '    oCommand = Nothing
    '            'End If
    '            PrdState.Value = 0
    '            Exit Sub
    '        End If

    '        oRec.Close()
    '    Finally
    '        If oCommand.Connection.State = System.Data.ConnectionState.Open Then
    '            oCommand.Connection.Close()
    '            oCommand = Nothing
    '        End If
    '    End Try
    '    Dim C11 As New TableCell
    '    'Dim CB As New Button
    '    Dim r2 As New TableRow
    '    'Dim c4 As New TableCell
    '    'c4.ColumnSpan = 5
    '    'c4.Style("text-align") = "center"
    '    C11.ColumnSpan = 4
    '    BtnSubmit8.Visible = True
    '    C11.Controls.Add(BtnSubmit8)

    '    'c4.Controls.Add(btnsubmit3)
    '    r2.Cells.Add(C11)
    '    Table2.Rows.Add(r2)
    '    '    End If
    '    'Next
    '    'TotLvl.Value = L
    'End Sub

    'Protected Sub TemplateStatus_Click()
    '    ChTmpAll.Visible = False
    '    Panel4.Visible = True
    '    Panel1.Visible = False
    '    Panel2.Visible = False
    '    Panel3.Visible = False
    '    'Panel3.Controls.Clear()
    '    PnlActSearch.Visible = True

    '    Dim TemplateID As String
    '    Dim TemplateName As String
    '    If Request("TemplateID") <> "" Then
    '        HTmpID.Value = Request("TemplateID")
    '    End If
    '    Dim strConn As String
    '    Dim oRec As Data.SqlClient.SqlDataReader
    '    Dim oCommand As New Data.SqlClient.SqlCommand

    '    Dim t2 As New Table
    '    t2.Style("width") = "100%"
    '    t2.BorderWidth = 2
    '    t2.GridLines = GridLines.Both
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim sqlString As String
    '    Dim L As Integer
    '    Dim I As Integer
    '    Dim TmpArr() As String = Split(HTmpID.Value, ",")
    '    L = 0
    '    'For i = 1 To TotLvl.Value
    '    '    Lvl = "LevelNo" & i
    '    '    If Request(Lvl) <> "" Then
    '    'sqlString = "select TemplateID,TemplateName from tblTemplates where TemplateName like '" & TxtTmpname.Text & "'"
    '    For I = 0 To UBound(TmpArr)

    '        sqlString = "SELECT T.TemplateID , T.TemplateName FROM tblTemplates T where  T.TemplateID = '" & TmpArr(I) & "' "
    '        'Response.Write(sqlString)
    '        oCommand = New Data.SqlClient.SqlCommand(sqlString, New SqlConnection(strConn))
    '        Try
    '            oCommand.Connection.Open()
    '            oRec = oCommand.ExecuteReader()
    '            'Response.End()
    '            If oRec.Read() Then
    '                TemplateID = oRec("TemplateID").ToString
    '                TemplateName = oRec("TemplateName").ToString
    '                Dim c As New TableCell
    '                Dim r As New TableRow
    '                Dim c1 As New TableCell
    '                'If IsDBNull(oRec("Physicianid")) Then
    '                c.Text = "<input id=TemplateID disabled type=checkbox name=TemplateID Value=" & TemplateID & "  checked=checked >"
    '                'Else
    '                'c.Text = "<input id=TemplateID type=checkbox onclick=highlightRow(this);  name=TemplateID Value=" & TemplateID & "  checked=checked>"
    '                'End If


    '                c1.Text = TemplateName
    '                r.Cells.Add(c)
    '                r.Cells.Add(c1)
    '                Table2.Rows.Add(r)
    '            End If
    '            oRec.Close()
    '        Finally
    '            If oCommand.Connection.State = System.Data.ConnectionState.Open Then
    '                oCommand.Connection.Close()
    '                oCommand = Nothing
    '            End If
    '        End Try
    '    Next
    '    PnlActSearch.Visible = True
    '    '    End If
    '    'Next
    '    'TotLvl.Value = L
    'End Sub

    Protected Sub PhyStatus_Click()
        PnlActSearch.Visible = False
        PnlActSelect.Visible = False
        Panel7.Visible = True
        Panel6.Visible = False
        Dim strConn As String
        Dim sqlstring As String
        Dim i As Integer
        Dim lvl As String
        Dim RecFound As Boolean
        RecFound = False
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        If Request("PhyId") <> "" Then
            HPhyID.Value = Request("PhyId")
        End If
        Dim ArrPhy() As String = Split(HPhyID.Value, ",")
        'Dim i As Integer
        Dim SQuery As String
        For i = 0 To UBound(ArrPhy)
            SQuery = "Select * from tblphysicians where physicianid = '" & ArrPhy(i) & "' "
            'Response.Write(SQuery)
            Dim CmdRec1 As New SqlCommand(SQuery, New SqlConnection(strConn))
            Try
                CmdRec1.Connection.Open()
                Dim DRRec1 As SqlDataReader = CmdRec1.ExecuteReader()
                If DRRec1.Read Then
                    'HPhyID.Value = DRRec1("PhysicianId").ToString
                    Dim c As New TableCell
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim c5 As New TableCell
                    Dim r As New TableRow



                    'TotPhy.Value = K

                    c.Text = DRRec1("firstname")
                    c1.Text = DRRec1("lastname")
                    c2.Text = DRRec1("PinNo")
                    c3.Text = "<input id=PhyID type=checkbox checked=checked disabled name=PhyID Value=" & DRRec1("PhysicianId").ToString & " >"
                    r.Cells.Add(c3)
                    r.Cells.Add(c)
                    r.Cells.Add(c1)
                    r.Cells.Add(c2)

                    Table3.Rows.Add(r)
                End If
                DRRec1.Close()
            Finally
                If CmdRec1.Connection.State = System.Data.ConnectionState.Open Then
                    CmdRec1.Connection.Close()
                    CmdRec1 = Nothing
                End If
            End Try

        Next



    End Sub

    Sub PageStatus()
        'If PrdState.Value = 1 Then
        '    TemplateSelect_Click()
        '    'PrdState.Value = 2
        'ElseIf PrdState.Value = 2 Then
        '    TemplateStatus_Click()
        '    'PrdState.Value = 2
        'End If

        If PhyState.Value = 1 Then
            BtnSubmit7.Focus()
            ActSearch_Click()
        ElseIf PhyState.Value = 2 Then
            btnsubmit3.Focus()
            PhySearch_Click()
        ElseIf PhyState.Value = 3 Then
            PhyStatus_Click()
        End If
        If PrdState.Value = 2 And PhyState.Value = 3 Then
            BtnAssign.Visible = True
            BtnAssign.Focus()
        Else
            BtnAssign.Visible = False
            BtnAssign.Focus()
        End If

    End Sub

    'Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAssign.Click
    '    'Response.Write(HTmpID.Value)
    '    'Response.Write(HPhyID.Value)
    '    'Response.End()
    '    Panel1.Visible = False
    '    Panel2.Visible = False
    '    Panel3.Visible = False
    '    Panel4.Visible = False
    '    Panel6.Visible = False
    '    Panel7.Visible = False
    '    PnlActSearch.Visible = False
    '    PnlActSelect.Visible = False
    '    Dim strConn As String
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim sQuery1 As String
    '    Dim sQuery2 As String
    '    Dim ArrPhy() As String = Split(HPhyID.Value, ",")
    '    Dim Arrtmp() As String = Split(HTmpID.Value, ",")
    '    Dim i As Integer = 0
    '    Dim j As Integer = 0
    '    Dim SQLString As String
    '    Dim Seq As Integer
    '    Dim strTAT As Integer
    '    Dim strSTAT As Integer
    '    For i = 0 To UBound(ArrPhy)
    '        For j = 0 To UBound(Arrtmp)
    '            sQuery1 = "Select * from tblPhysiciansTemplate where PhysicianID='" & ArrPhy(i) & "' and TemplateId='" & Arrtmp(j) & "'"
    '            Dim cmdIns As New SqlCommand(sQuery1, New SqlConnection(strConn))
    '            Try
    '                cmdIns.Connection.Open()
    '                Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
    '                If DRRec.HasRows = False Then
    '                    SQLString = "SELECT max(worktype) as numRec from tblPhysiciansTemplate where PhysicianID='" & ArrPhy(i) & "'"
    '                    Dim oCommand1 As New Data.SqlClient.SqlCommand(SQLString, New SqlConnection(strConn))
    '                    Try
    '                        oCommand1.Connection.Open()
    '                        Dim oRec As Data.SqlClient.SqlDataReader = oCommand1.ExecuteReader()
    '                        If oRec.HasRows Then
    '                            If oRec.Read Then
    '                                Seq = IIf(IsDBNull(oRec("numRec")), 0, oRec("numRec"))
    '                            End If
    '                        End If
    '                        oRec.Close()
    '                    Finally
    '                        If oCommand1.Connection.State = System.Data.ConnectionState.Open Then
    '                            oCommand1.Connection.Close()
    '                            oCommand1 = Nothing
    '                        End If
    '                    End Try


    '                    SQLString = "SELECT A.TAT, A.STAT FROM tblAccounts AS A INNER JOIN tblPhysicians AS P ON A.AccountID = P.AccountID where P.PhysicianID='" & ArrPhy(i) & "'"
    '                    'Response.Write(SQLString)
    '                    Dim oCommand2 As New Data.SqlClient.SqlCommand(SQLString, New SqlConnection(strConn))
    '                    Try
    '                        oCommand2.Connection.Open()
    '                        Dim DRRec1 As SqlDataReader = oCommand2.ExecuteReader()

    '                        If DRRec1.HasRows Then
    '                            If DRRec1.Read Then
    '                                strTAT = IIf(IsDBNull(DRRec1("TAT")), 0, DRRec1("TAT"))
    '                                strSTAT = IIf(IsDBNull(DRRec1("STAT")), 0, DRRec1("STAT"))
    '                            End If
    '                        Else
    '                            strTAT = 0
    '                            strSTAT = 0
    '                        End If
    '                        DRRec1.Close()
    '                    Finally
    '                        If oCommand2.Connection.State = System.Data.ConnectionState.Open Then
    '                            oCommand2.Connection.Close()
    '                            oCommand2 = Nothing
    '                        End If
    '                    End Try

    '                    SQLString = "insert into tblPhysiciansTemplate(TemplateID,PhysicianID,TAT,STAT,worktype) values('" & Arrtmp(j) & "','" & ArrPhy(i) & "'," & strTAT & "," & strSTAT & "," & Seq + 1 & ")"
    '                    Dim oCommand3 As New Data.SqlClient.SqlCommand(SQLString, New SqlConnection(strConn))
    '                    Try
    '                        oCommand3.Connection.Open()
    '                        oCommand3.ExecuteNonQuery()
    '                    Finally
    '                        If oCommand3.Connection.State = System.Data.ConnectionState.Open Then
    '                            oCommand3.Connection.Close()
    '                            oCommand3 = Nothing
    '                        End If
    '                    End Try
    '                End If
    '                DRRec.Close()
    '                MsgDisp.Text = "Template(s) have been assigned successfully."
    '            Finally
    '                If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
    '                    cmdIns.Connection.Close()
    '                    cmdIns = Nothing
    '                End If
    '            End Try
    '        Next
    '    Next
    'End Sub

    Sub Loadpage()

        If PhyState.Value = 0 Then
            'PnlActSearch.Visible = False
            PnlActSearch.Visible = True
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
        Dim DSAct As New DataSet
        Dim DRRec1 As Data.DataTableReader
        Dim clsAct As New ETS.BL.Accounts
        DSAct = clsAct.getAccountList(Session("WorkGroupID"), Session("ContractorID"), " AND AccountName like '%" & TxtAname.Text & "%' AND (IsDeleted is null or IsDeleted=0) ")

        clsAct = Nothing
        If DSAct.Tables.Count > 0 Then
            If DSAct.Tables(0).Rows.Count > 0 Then
                DRRec1 = DSAct.Tables(0).CreateDataReader

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
                        c.HorizontalAlign = HorizontalAlign.Left
                        c1.HorizontalAlign = HorizontalAlign.Left
                        'c2.Text = DRRec1("PinNo")
                        c3.Controls.Add(RB)
                        r.Cells.Add(c3)
                        r.Cells.Add(c)
                        r.Cells.Add(c1)
                        'r.Cells.Add(c2)
                        TblAccount.Rows.Add(r)

                    End While
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
                    PnlActSearch.Visible = False
                    PnlActSelect.Visible = True
                Else
                    MsgDisp.Text = "No records found, Please try another search."
                    PnlActSearch.Visible = True
                    PnlActSelect.Visible = False
                End If

                DRRec1.Close()
            End If
        End If
    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub BtnSubmit8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit8.Click
        PrdState.Value = 2
        'PhyState.Value = 2
        PageStatus()

    End Sub



    'Protected Sub ShowDetails(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Response.Write("Physician ID:" & Request("PhyID"))
    'End Sub
    Private Sub RB1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RB1.CheckedChanged
        Response.Write("clicked")
        'Dim txtBoxSender As TextBox
        'Dim strTextBoxID As String

        'txtBoxSender = CType(sender, TextBox)
        'strTextBoxID = txtBoxSender.ID

        'Select Case strTextBoxID
        '    Case "TextBox1"
        '        Label3.Text = "TextBox1 text was changed"

        '    Case "TextBox2"
        '        Label4.Text = "TextBox2 text was changed"
        'End Select
    End Sub
End Class
