Imports System.Data.SqlClient
Imports System.Data
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
        Dim DSDep As DataSet
        Dim clsDep As New ETS.BL.Department
        With clsDep
            '.ContractorID = Session("ContractorID")
            '._WhereString.Append(" and (deleted is null or deleted =0 ) and Name like '" & TxtDname.Text & "'")
            DSDep = .GetDepartmentLstByWrkGroupID(Session("ContractorID"), Session("WorkGroupID"), " AND Name LIKE '" & TxtDname.Text & "%' ")
        End With
        clsDep = Nothing
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both

        Dim K As Integer
        If DSDep.Tables.Count > 0 Then
            K = 0
            For Each DRRec1 As DataRow In DSDep.Tables(0).Rows
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

            Next

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
        DSDep.Dispose()
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
        Dim j As Integer
        Dim DT As New DataTable
        DT.Columns.Add("SuperVisorID", GetType(System.String))
        DT.Columns.Add("LevelNo", GetType(System.Int32))
        For j = 1 To HSuperID.Value
            SuperVisorID = "SuperVisor" & j
            If Request(SuperVisorID) <> "" Then
                Dim DR As DataRow = DT.NewRow
                DR("SuperVisorID") = Request(SuperVisorID)
                DR("LevelNo") = j
                DT.Rows.Add(DR)
            End If
        Next
        Dim DSDepSup As New DataSet
        Dim clsDepSup As New ETS.BL.DeptSuperVisor
        With clsDepSup
            .DepartmentID = HDeptID.Value
            .SetSuperVisor(DT)
            DSDepSup = .getDepSuperVisorsList(HDeptID.Value)
        End With
        clsDepSup = Nothing


        Dim Header1 As New TableCell
        Dim Header2 As New TableCell
        Dim Header3 As New TableCell
        Dim Row1 As New TableRow

        Header1.Text = "Department Name"
        Header1.CssClass = "alt1"
        Header2.Text = "Supervisor Level"
        Header2.CssClass = "alt1"
        Header3.Text = "Supervisor Name"
        Header3.CssClass = "alt1"
        Row1.Cells.Add(Header1)
        Row1.Cells.Add(Header2)
        Row1.Cells.Add(Header3)
        Table4.Rows.Add(Row1)
        For Each RdSet As DataRow In DSDepSup.Tables(0).Rows
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
        Next
        DSDepSup.Dispose()
    End Sub
    Protected Sub DeptStatus_Click()
        Try
            Panel6.Visible = False
            Panel5.Visible = False
            Panel1.Visible = True

            Dim strConn As String
            Dim t2 As New Table
            t2.Style("width") = "100%"
            t2.BorderWidth = 2
            t2.GridLines = GridLines.Both
            Dim clsDep As New ETS.BL.Department
            With clsDep
                .DepartmentID = HDeptID.Value
                .getDepartmentDetails()
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow

                c.Text = .Name
                c1.Text = .Description

                r.Cells.Add(c)
                r.Cells.Add(c1)

                Table2.Rows.Add(r)
            End With
            clsDep = Nothing
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

            Dim sQuery3 As String
            Dim Level1 As String
            Dim Level2 As String
            Dim Level3 As String

            Level1 = "No"
            Level2 = "No"
            Level3 = "No"
            HSuperID.Value = 0
            Dim DSDepSup As New DataSet
            Dim clsDepSup As New ETS.BL.DeptSuperVisor
            With clsDepSup
                DSDepSup = .getDepSuperVisorsList(HDeptID.Value)
            End With
            clsDepSup = Nothing

            Dim DSUsers As New DataSet
            Dim clsUser As New ETS.BL.Users
            With clsUser
                .ContractorID = Session("ContractorID").ToString
                '.CanApprove = True
                ._WhereString.Append(" AND (IsDeleted IS NULL OR IsDeleted=0)")
                DSUsers = .getUsersList
            End With
            clsUser = Nothing
            DSUsers.Tables(0).Columns.Add(New DataColumn("uname", GetType(System.String), "firstname + ' ' + lastname + ' (' + username + ')'"))
            If DSDepSup.Tables.Count > 0 And DSDepSup.Tables(0).Rows.Count > 0 Then
                For Each RdSet As DataRow In DSDepSup.Tables(0).Rows
                    Dim Supervisor As New DropDownList
                    HSuperID.Value = HSuperID.Value + 1
                    Supervisor.ID = "Supervisor" & HSuperID.Value
                    Dim NLI As New ListItem
                    NLI.Text = RdSet("uname")
                    NLI.Value = RdSet("userid").ToString
                    NLI.Selected = "True"
                    Supervisor.Items.Add(NLI)

                    If DSUsers.Tables.Count > 0 Then
                        For Each DRRec2 As DataRow In DSUsers.Tables(0).Rows
                            If Not RdSet("userid") = DRRec2("userid") Then
                                Dim NLI1 As New ListItem
                                NLI1.Text = DRRec2("uname")
                                NLI1.Value = DRRec2("userid").ToString
                                Supervisor.Items.Add(NLI1)
                            End If
                        Next
                    End If

                    Dim Cell1 As New TableCell
                    Dim Cell2 As New TableCell
                    Dim Row1 As New TableRow
                    Cell1.Text = "Supervisor" & HSuperID.Value
                    Cell2.Controls.Add(Supervisor)
                    Row1.Cells.Add(Cell1)
                    Row1.Cells.Add(Cell2)
                    Table3.Rows.Add(Row1)


                Next
            Else
                Dim Supervisor1 As New DropDownList
                HSuperID.Value = HSuperID.Value + 1
                Supervisor1.ID = "Supervisor" & HSuperID.Value
                Dim NLI2 As New ListItem
                NLI2.Text = "Select Supervisor" & HSuperID.Value
                NLI2.Value = ""
                NLI2.Selected = "True"
                Supervisor1.Items.Add(NLI2)
                If DSUsers.Tables.Count > 0 Then
                    For Each DRRec2 As DataRow In DSUsers.Tables(0).Rows

                        Dim NLI3 As New ListItem
                        NLI3.Text = DRRec2("uname")
                        NLI3.Value = DRRec2("userid").ToString
                        Supervisor1.Items.Add(NLI3)

                    Next
                End If
                Dim Cell5 As New TableCell
                Dim Cell6 As New TableCell
                Dim Row2 As New TableRow
                Cell5.Text = "Supervisor" & HSuperID.Value
                Cell6.Controls.Add(Supervisor1)
                Row2.Cells.Add(Cell5)
                Row2.Cells.Add(Cell6)
                Table3.Rows.Add(Row2)
            End If

            DSUsers.Dispose()
        Catch ex As Exception
            Response.Write("Error: " & ex.Message)
        End Try




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

End Class

