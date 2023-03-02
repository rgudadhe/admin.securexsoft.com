Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BtnAssign.Visible = False
        Table4.Visible = False
        If Not IsPostBack Then
            BtnAssign.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False
            Panel1.Visible = True
            Panel5.Visible = True
            GrpActState.Value = 0
            ActState.Value = 0
        End If
    End Sub
    Protected Sub ActSearch_Click()
        Panel6.Visible = True
        Panel5.Visible = False
        Panel7.Visible = False
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both

        Dim DSAct As New DataSet
        Dim DRec1 As Data.DataTableReader
        Dim clsAct As ETS.BL.Accounts
        Try
            clsAct = New ETS.BL.Accounts
            DSAct = clsAct.getAccountList(Session("WorkGroupID"), Session("ContractorID"), " AND AccountName like '%" & TxtAname.Text & "%' ")
            If DSAct.Tables.Count > 0 Then
                If DSAct.Tables(0).Rows.Count > 0 Then
                    DRec1 = DSAct.Tables(0).CreateDataReader
                    Dim K As Integer = 0
                    If DRec1.HasRows Then
                        While DRec1.Read
                            K += 1
                            Dim c As New TableCell
                            Dim c1 As New TableCell
                            Dim c2 As New TableCell
                            Dim c3 As New TableCell
                            Dim r As New TableRow
                            Dim CB1 As New CheckBox
                            CB1.ID = "AccountID" & K
                            CB1.InputAttributes.Add("Value", DRec1("AccountID").ToString)
                            TotAct.Value = K
                            c.Text = DRec1("AccountName")
                            c1.Text = DRec1("AccountNo")
                            If HGrpActID.Value = DRec1("GrpBillActID").ToString Then
                                CB1.Checked = True
                            End If
                            c3.Controls.Add(CB1)
                            r.Cells.Add(c3)
                            r.Cells.Add(c)
                            r.Cells.Add(c1)
                            Table1.Rows.Add(r)
                        End While
                    End If

                End If
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

        Catch ex As Exception
        Finally
            clsAct = Nothing
            DSAct = Nothing
            DRec1 = Nothing
        End Try



        'Dim DSActList As New DataSet
        'Dim clsAct As New ETS.BL.Accounts
        'With clsAct
        '    .ContractorID = Session("ContractorID")
        '    ._WhereString.Append(" and isdeleted is null or isdeleted ='False' and AccountName like '%" & TxtAname.Text & "%'")
        '    DSActList = .getAccountList
        'End With
        'clsAct = Nothing
        Try

            'For Each DR As DataRow In DSActList.Tables(0).Rows

            'Next
            'DSActList.Dispose()
            
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub GrpAccountSearch_Click()
        Panel2.Visible = True
        Panel1.Visible = False
        Panel3.Visible = False
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim DSGrpActList As New DataSet
        Dim clsGrpAct As New ETS.BL.GrpAccounts
        With clsGrpAct
            .ContractorID = Session("ContractorID").ToString
            ._WhereString.Append(" and GrpActname like '%" & TxtGrpname.Text & "%'")
            DSGrpActList = .getGrpActList()
        End With
        clsGrpAct = Nothing
        Try
            Dim J As Integer
            J = 0
            For Each DR As DataRow In DSGrpActList.Tables(0).Rows

                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow
                Dim RB As New RadioButton
                c.Text = DR("GrpActName")
                c1.Text = DR("GrpActNo")
                form1.Controls.Add(RB)
                RB.GroupName = "GrpActID"
                RB.ID = DR("GrpActID").ToString
                c3.Controls.Add(RB)
                J = J + 1
                r.Cells.Add(c3)
                r.Cells.Add(c)
                r.Cells.Add(c1)
                r.Cells.Add(c2)
                TblGrpSeach.Rows.Add(r)
            Next
            Dim r2 As New TableRow
            Dim c4 As New TableCell
            btnSubmit4.Visible = True
            c4.ColumnSpan = 3
            c4.Style("text-align") = "center"
            c4.Controls.Add(btnSubmit4)
            r2.Cells.Add(c4)
            TblGrpSeach.Rows.Add(r2)
            DSGrpActList.Dispose()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub BtnSubmit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit1.Click
        GrpActState.Value = 1
        PageStatus()
    End Sub

    Protected Sub BtnSubmit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit2.Click
        ActState.Value = 1
        PageStatus()
    End Sub

    Protected Sub btnSubmit4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit4.Click
        GrpActState.Value = 2
        HGrpActID.Value = Request("GrpActID")
        PageStatus()
    End Sub
    Protected Sub BtnSubmit5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit5.Click
        GrpActState.Value = 3
        PageStatus()
    End Sub

    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit3.Click
        ActState.Value = 2
        PageStatus()
    End Sub
    Protected Sub GrpActStatus_Click()
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = True
        Dim clsGrpAct As New ETS.BL.GrpAccounts
        With clsGrpAct
            .ContractorID = Session("ContractorID").ToString
            .GrpActID = HGrpActID.Value
            .getGrpActDetails()
            Dim c As New TableCell
            Dim r As New TableRow
            Dim c1 As New TableCell
            c.Text = .GrpActName
            c1.Text = .GrpActNo
            r.Cells.Add(c)
            r.Cells.Add(c1)
            TblGrpstatus.Rows.Add(r)
        End With
        clsGrpAct = Nothing
    End Sub
    Protected Sub ActStatus_Click()
        Panel7.Visible = True
        Panel5.Visible = False
        Panel6.Visible = False
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim DSActList As New DataSet
        Dim clsAct As New ETS.BL.Accounts
        With clsAct
            .ContractorID = Session("ContractorID").ToString
            ._WhereString.Append(" and (IsDeleted is NULL or IsDeleted=0)")
            DSActList = .getAccountList()
        End With
        clsAct = Nothing
        Dim i As Integer
        Dim AccountID As String
        Dim K As Integer
        K = 0
        For i = 1 To TotAct.Value
            AccountID = "AccountID" & i
            If Request(AccountID) <> "" Then
                Dim DR() As DataRow = DSActList.Tables(0).Select("AccountID='" & Request(AccountID) & "'")
                Try
                    K = K + 1
                    Dim c As New TableCell
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim r As New TableRow
                    Dim CB1 As New CheckBox
                    CB1.ID = "AccountID" & K
                    CB1.InputAttributes.Add("Value", DR(0).Item("AccountID").ToString)
                    CB1.Checked = True
                    c.Text = DR(0).Item("Accountname")
                    c1.Text = DR(0).Item("AccountNO")
                    c3.Controls.Add(CB1)
                    r.Cells.Add(c3)
                    r.Cells.Add(c)
                    r.Cells.Add(c1)
                    Table3.Rows.Add(r)
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try
            End If

        Next
        TotAct.Value = K
        DSActList.Dispose()
    End Sub

    Sub PageStatus()
        If GrpActState.Value = 1 Then
            GrpAccountSearch_Click()
        ElseIf GrpActState.Value = 2 Then
            GrpActStatus_Click()
        End If

        If ActState.Value = 1 Then
            ActSearch_Click()
        ElseIf ActState.Value = 2 Then
            ActStatus_Click()
        End If
        If GrpActState.Value = 2 And ActState.Value = 2 Then
            BtnAssign.Visible = True
        Else
            BtnAssign.Visible = False
        End If

    End Sub

    Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAssign.Click
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Dim strConn As String
        Dim AccountID As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim j As Integer
        Dim arrActID As String = String.Empty
        For j = 1 To TotAct.Value
            AccountID = "AccountID" & j
            If Not Request(AccountID) = "" Then
                If String.IsNullOrEmpty(arrActID) Then
                    arrActID = "'" & Request(AccountID) & "'"
                Else
                    arrActID = arrActID & ",'" & Request(AccountID) & "'"
                End If
            End If
        Next
        Dim strGrpActNo As String
        Dim strgrpActName As String
        Dim clsGrpAct As New ETS.BL.GrpAccounts
        With clsGrpAct
            .GrpActID = HGrpActID.Value
            .SetAccountsBillingGroup(arrActID)
            .getGrpActDetails()
            strgrpActName = .GrpActName
            strGrpActNo = .GrpActNo
        End With
        clsGrpAct = Nothing
        Dim DSActList As New DataSet
        Dim clsAct As New ETS.BL.Accounts
        With clsAct
            .ContractorID = Session("ContractorID").ToString
            ._WhereString.Append(" and (IsDeleted is NULL or IsDeleted=0)")
            .GrpBillActID = HGrpActID.Value
            DSActList = .getAccountList()
        End With
        clsAct = Nothing

        For Each DR As DataRow In DSActList.Tables(0).Rows
            Dim c As New TableCell
            Dim c1 As New TableCell
            Dim c2 As New TableCell
            Dim c3 As New TableCell
            Dim r As New TableRow
            c.Text = strgrpActName
            c1.Text = strGrpActNo

            c2.Text = DR("Accountname")
            c3.Text = DR("AccountNO")
            r.Cells.Add(c)
            r.Cells.Add(c1)
            r.Cells.Add(c2)
            r.Cells.Add(c3)
            Table4.Visible = True
            Table4.Rows.Add(r)
        Next
        DSActList.Dispose()

    End Sub
End Class

