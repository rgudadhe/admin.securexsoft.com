Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BtnAssign.Visible = False
        DispBox.Visible = False
        Table4.Visible = False
        If Not IsPostBack Then
            BtnAssign.Visible = False
            Table4.Visible = False
            Panel6.Visible = False
            Panel1.Visible = False

            HSuperID.Value = 1
            Panel5.Visible = True
            DeptState.Value = 0
        Else
        End If
    End Sub







    Protected Sub DeptSearch_Click()

        Panel6.Visible = True
        Panel5.Visible = False
        Panel1.Visible = False

        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd1 As New SqlCommand("Select * from tblDepartments where Name like '%" & TxtDname.Text & "%' ", New SqlConnection(strConn))
        SQLCmd1.Connection.Open()
        Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()

        Dim K As Integer

        If DRRec1.HasRows Then

            K = 0
            While (DRRec1.Read)
                K = K + 1
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow
                Dim CB1 As New RadioButton
                CB1.ID = DRRec1("DepartmentID").ToString
                CB1.GroupName = "DepartmentID"
                CB1.Checked = "True"
                TotAct.Value = K
                c.Text = DRRec1("Name")
                c3.Controls.Add(CB1)
                r.Cells.Add(c3)
                r.Cells.Add(c)
                Table1.Rows.Add(r)

            End While

        Else
            DeptState.Value = 0
            DispBox.Visible = True

            DispBox.Text = "No Records Found"
            PageStatus()
            Exit Sub

        End If


        Dim CB As New Button
        Dim r2 As New TableRow
        Dim c4 As New TableCell
        c4.ColumnSpan = 3
        c4.Style("text-align") = "center"
        btnsubmit3.Visible = True

        c4.Controls.Add(btnsubmit3)


        r2.Cells.Add(c4)
        Table1.Rows.Add(r2)


        'Panel6.Controls.Add(t2)
        'Panel6.Controls.Add(CB)
        DRRec1.Close()
        SQLCmd1.Connection.Close()

    End Sub






    Protected Sub BtnSubmit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit2.Click
        DeptState.Value = 1
        HDeptID.Value = Request("DepartmentID")
        PageStatus()
    End Sub






    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit3.Click

        DeptState.Value = 2
        HDeptID.Value = Request("DepartmentID")
        PageStatus()
    End Sub


    Sub PageStatus()

        If DeptState.Value = 0 Then
            LoadPage()
        ElseIf DeptState.Value = 1 Then
            DeptSearch_Click()
        ElseIf DeptState.Value = 2 Then
            DeptStatus_Click()
        End If
        If DeptState.Value = 2 Then
            BtnAssign.Visible = True
        Else
            BtnAssign.Visible = False
        End If

    End Sub

    Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAssign.Click

        Panel5.Visible = False
        Panel6.Visible = False
        Panel1.Visible = False

        Dim strConn As String
        Dim SuperVisorID As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim j As Integer
        Dim sQuery1 As String
        Dim sQuery2 As String
        sQuery1 = "Delete from tblDeptSuperVisorAssign where DepartmentID='" & HDeptID.Value & "' "
        Dim cmdDel As New SqlCommand(sQuery1, New SqlConnection(strConn))
        cmdDel.Connection.Open()
        cmdDel.ExecuteNonQuery()
        cmdDel.Connection.Close()
        For j = 1 To HSuperID.Value
            SuperVisorID = "SuperVisor" & j
            If Request(SuperVisorID) <> "" Then

                sQuery2 = "INSERT INTO tblDeptSuperVisorAssign (DepartmentID,SuperVisorID, LevelNo, CreateDate)VALUES('" & HDeptID.Value & "','" & Request(SuperVisorID) & "', '" & j & "','" & Now & "')"
                Dim cmdUp As New SqlCommand(sQuery2, New SqlConnection(strConn))
                cmdUp.Connection.Open()
                cmdUp.ExecuteNonQuery()
                cmdUp.Connection.Close()
            End If
        Next



        Dim sQuery3 As String
        sQuery3 = "Select D.name, u.firstname + ' ' + u.lastname + ' (' + u.username + ')' as uname, DS.LevelNo from tblDeptSuperVisorAssign DS, tblDepartments D, tblUsers U where DS.DepartmentID=D.DepartmentID and DS.SuperVisorID=U.userid and D.DepartmentID ='" & HDeptID.Value & "' order by DS.LevelNo"
        Dim cmdSel As New SqlCommand(sQuery3, New SqlConnection(strConn))
        cmdSel.Connection.Open()
        Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
        Dim Header1 As New TableCell
        Dim Header2 As New TableCell
        Dim Header3 As New TableCell
        Dim Row1 As New TableRow
        Row1.CssClass = "SMSelected"
        Row1.Style("text-align") = "Center"


        Header1.Text = "Department Name"
        Header2.Text = "Supervisor Level"
        Header3.Text = "Supervisor Name"
        Row1.Cells.Add(Header1)
        Row1.Cells.Add(Header2)
        Row1.Cells.Add(Header3)
        Table4.Rows.Add(Row1)
        'If RdSet.Read = True Then
        While (RdSet.Read)
            Dim c As New TableCell
            Dim c1 As New TableCell
            Dim c2 As New TableCell
            Dim c3 As New TableCell
            Dim r As New TableRow
            c.Text = RdSet("name")
            c1.Text = RdSet("LevelNo")
            c2.Text = RdSet("uname")
            r.Cells.Add(c)
            r.Cells.Add(c1)
            r.Cells.Add(c2)
            Table4.Visible = True
            Table4.Rows.Add(r)
            'Response.Write(RdSet("uName") & RdSet("LevelName") & RdSet("AccountName") & RdSet("pName"))
        End While
        'End If
        RdSet.Close()

        cmdSel.Connection.Close()

    End Sub


    Protected Sub DeptStatus_Click()

        Panel6.Visible = False
        Panel5.Visible = False
        Panel1.Visible = True

        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd1 As New SqlCommand("Select * from tblDepartments where DepartmentID like '%" & HDeptID.Value & "%' ", New SqlConnection(strConn))
        SQLCmd1.Connection.Open()
        Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
        Dim K As Integer
        If DRRec1.HasRows Then
            K = 0
            While (DRRec1.Read)
                K = K + 1
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow
                c.Text = DRRec1("name")
                c1.Text = DRRec1("Description")

                r.Cells.Add(c)
                r.Cells.Add(c1)

                Table2.Rows.Add(r)

            End While
        End If

        Dim sQuery3 As String
        Dim Level1 As String
        Dim Level2 As String
        Dim Level3 As String
        Dim Level1Text As String
        Dim Level2Text As String
        Dim Level3Text As String
        Dim Level1Value As String
        Dim Level2Value As String
        Dim Level3Value As String
        Level1 = "No"
        Level2 = "No"
        Level3 = "No"
        HSuperID.Value = 0
        sQuery3 = "Select u.userid,  u.firstname + ' ' + u.lastname + ' (' + u.username + ')' as uname, DS.LevelNo from tblDeptSuperVisorAssign DS, tblDepartments D, tblUsers U where DS.DepartmentID=D.DepartmentID and DS.SuperVisorID=U.userid and D.DepartmentID ='" & HDeptID.Value & "' order by DS.LevelNo"
        Dim cmdSel As New SqlCommand(sQuery3, New SqlConnection(strConn))
        cmdSel.Connection.Open()
        Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
        If RdSet.HasRows Then


            While (RdSet.Read)
                Dim Supervisor As New DropDownList
                HSuperID.Value = HSuperID.Value + 1
                Supervisor.ID = "Supervisor" & HSuperID.Value
                Dim NLI As New ListItem
                NLI.Text = RdSet("uname")
                NLI.Value = RdSet("userid").ToString
                NLI.Selected = "True"
                Supervisor.Items.Add(NLI)
                Dim SQLCmd2 As New SqlCommand("Select firstname + ' ' + lastname + ' (' + username + ')' as uname, userid from tblUsers where CanApprove='True' order by firstname", New SqlConnection(strConn))
                SQLCmd2.Connection.Open()
                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                If DRRec2.HasRows Then
                    While DRRec2.Read
                        If Not RdSet("userid") = DRRec2("userid") Then
                            Dim NLI1 As New ListItem
                            NLI1.Text = DRRec2("uname")
                            NLI1.Value = DRRec2("userid").ToString
                            Supervisor.Items.Add(NLI1)
                            'Supervisor2.Items.Add(NLI)
                            'Supervisor3.Items.Add(NLI)
                        End If
                    End While
                End If
                DRRec2.Close()
                SQLCmd2.Connection.Close()
                Dim Cell1 As New TableCell
                Dim Cell2 As New TableCell
                Dim Row1 As New TableRow
                Cell1.Text = "Supervisor" & HSuperID.Value
                Cell2.Controls.Add(Supervisor)
                Row1.Cells.Add(Cell1)
                Row1.Cells.Add(Cell2)
                Table3.Rows.Add(Row1)


            End While
        Else
            Dim Supervisor1 As New DropDownList
            HSuperID.Value = HSuperID.Value + 1
            Supervisor1.ID = "Supervisor" & HSuperID.Value
            Dim NLI2 As New ListItem
            NLI2.Text = "Select Supervisor" & HSuperID.Value
            NLI2.Value = ""
            NLI2.Selected = "True"
            Supervisor1.Items.Add(NLI2)
            Dim SQLCmd2 As New SqlCommand("Select firstname + ' ' + lastname + ' (' + username + ')' as uname, userid from tblUsers where CanApprove='True' order by firstname", New SqlConnection(strConn))
            SQLCmd2.Connection.Open()
            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
            If DRRec2.HasRows Then
                While DRRec2.Read

                    Dim NLI3 As New ListItem
                    NLI3.Text = DRRec2("uname")
                    NLI3.Value = DRRec2("userid").ToString
                    Supervisor1.Items.Add(NLI3)

                End While
            End If
            DRRec2.Close()
            SQLCmd2.Connection.Close()
            Dim Cell5 As New TableCell
            Dim Cell6 As New TableCell
            Dim Row2 As New TableRow
            Cell5.Text = "Supervisor" & HSuperID.Value
            Cell6.Controls.Add(Supervisor1)
            Row2.Cells.Add(Cell5)
            Row2.Cells.Add(Cell6)
            Table3.Rows.Add(Row2)
        End If

        RdSet.Close()

        cmdSel.Connection.Close()


        'Supervisor2.Items.Add(LI2)
        'Supervisor3.Items.Add(LI3)


    End Sub





    Sub LoadPage()
        BtnAssign.Visible = False
        Table4.Visible = False
        Panel6.Visible = False
        Panel1.Visible = False
        HSuperID.Value = 1
        Panel5.Visible = True
        DeptState.Value = 0
    End Sub

    'Protected Sub Supervisor1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Supervisor1.SelectedIndexChanged

    '    'Response.Write(Supervisor2.Items.Count)
    '    If Supervisor1.SelectedValue <> "" Then
    '        Dim LI1 As ListItem = Supervisor2.Items.FindByValue(Supervisor1.SelectedValue)
    '        Supervisor2.Items.Remove(LI1)
    '        Dim LI2 As ListItem = Supervisor3.Items.FindByValue(Supervisor1.SelectedValue)
    '        Supervisor3.Items.Remove(LI2)
    '    End If




    '    'Response.End()
    '    'Dim i As Integer
    '    'For i = 1 To Supervisor2.Items.Count - 1
    '    '    If Supervisor2..Value = Supervisor1.SelectedValue Then
    '    '        Supervisor2.Items.RemoveAt(i)
    '    '    End If
    '    'Next
    'End Sub

   

    'Protected Sub Supervisor2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Supervisor2.SelectedIndexChanged
    '    If Supervisor2.SelectedValue <> "" Then
    '        Dim LI1 As ListItem = Supervisor1.Items.FindByValue(Supervisor2.SelectedValue)
    '        Supervisor1.Items.Remove(LI1)
    '        Dim LI2 As ListItem = Supervisor3.Items.FindByValue(Supervisor2.SelectedValue)
    '        Supervisor3.Items.Remove(LI2)
    '    End If

    'End Sub

    'Protected Sub Supervisor3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Supervisor3.SelectedIndexChanged
    '    If Supervisor3.SelectedValue <> "" Then
    '        Dim LI1 As ListItem = Supervisor1.Items.FindByValue(Supervisor3.SelectedValue)
    '        Supervisor1.Items.Remove(LI1)
    '        Dim LI2 As ListItem = Supervisor2.Items.FindByValue(Supervisor3.SelectedValue)
    '        Supervisor2.Items.Remove(LI2)
    '    End If
    'End Sub

    
End Class

