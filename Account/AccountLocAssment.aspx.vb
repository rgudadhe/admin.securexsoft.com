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
            Panel6.Visible = False
            Panel7.Visible = False
            HLoc.Value = 1
            Panel5.Visible = True
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
        Dim DSActList As New DataSet
        Dim DSAct As New DataSet
        Dim DRec1 As Data.DataTableReader
        Dim clsAct As ETS.BL.Accounts
        Try
            clsAct = New ETS.BL.Accounts
            DSAct = clsAct.getAccountList(Session("WorkGroupID"), Session("ContractorID"), " AND AccountName like '%" & TxtAname.Text & "%' AND Mode = 'LC' ")

            If DSAct.Tables.Count > 0 Then
                If DSAct.Tables(0).Rows.Count > 0 Then
                    
                    Dim K As Integer
                    K = 0
                    DRec1 = DSAct.Tables(0).CreateDataReader
                    If DRec1.HasRows Then
                        While DRec1.Read
                            K = K + 1
                            Dim c As New TableCell
                            Dim c1 As New TableCell
                            Dim c2 As New TableCell
                            Dim c3 As New TableCell
                            Dim r As New TableRow
                            Dim CB1 As New RadioButton
                            CB1.ID = DRec1("AccountID").ToString
                            CB1.GroupName = "AccountID"
                            CB1.Checked = "True"
                            TotAct.Value = K
                            c.Text = DRec1("AccountName")
                            c1.Text = DRec1("AccountNo")
                            c3.Controls.Add(CB1)
                            r.Cells.Add(c3)
                            r.Cells.Add(c)
                            r.Cells.Add(c1)
                            Table1.Rows.Add(r)
                        End While
                    End If

                End If
            End If
        Catch ex As Exception
        Finally
            clsAct = Nothing
            DSAct = Nothing
            DRec1 = Nothing
        End Try

        Dim CB As New Button
        Dim r2 As New TableRow
        Dim c4 As New TableCell
        c4.ColumnSpan = 3
        c4.Style("text-align") = "center"
        btnsubmit3.Visible = True
        c4.Controls.Add(btnsubmit3)
        r2.Cells.Add(c4)
        Table1.Rows.Add(r2)

        ''Dim DSActList As New DataSet
        ''Dim clsAct As New ETS.BL.Accounts
        ''With clsAct
        ''    .ContractorID = Session("ContractorID").ToString
        ''    .Mode = "LC"
        ''    ._WhereString.Append(" and AccountName like '%" & TxtAname.Text & "%'")
        ''    DSActList = .getAccountList()
        ''End With
        ''clsAct = Nothing
        'Try
        '    Dim i As Integer

        '    For Each DRRec1 As DataRow In DSActList.Tables(0).Rows


        '    Next
        '    DSActList.Dispose()



        'Catch ex As Exception
        '    Response.Write(ex.Message)

        'End Try


    End Sub






    Protected Sub BtnSubmit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit2.Click
        ActState.Value = 1
        HActID.Value = Request("AccountID")
        HActName.Value = Request("AccountName")
        PageStatus()
    End Sub






    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit3.Click
        ActState.Value = 2
        HActID.Value = Request("AccountID")
        HActName.Value = Request("AccountName")
        PageStatus()

    End Sub
    Protected Sub ActStatus_Click()
        Panel7.Visible = True
        Panel5.Visible = False
        Panel6.Visible = False
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim clsAct As New ETS.BL.Accounts
        With clsAct
            .AccountID = Request("AccountID").ToString
            .getAccountDetails()
            Dim c As New TableCell
            Dim c1 As New TableCell
            Dim c2 As New TableCell
            Dim c3 As New TableCell
            Dim r As New TableRow
            Dim CB1 As New RadioButton
            CB1.ID = .AccountID
            CB1.GroupName = "AccountID"
            CB1.Checked = True
            c.Text = .AccountName
            c1.Text = .AccountNo
            c3.Controls.Add(CB1)
            r.Cells.Add(c)
            r.Cells.Add(c1)
            Table3.Rows.Add(r)
        End With
        clsAct = Nothing
    End Sub

    Sub PageStatus()
        If ActState.Value = 1 Then
            ActSearch_Click()
        ElseIf ActState.Value = 2 Then
            ActStatus_Click()
            LocStatus()
        End If
        If ActState.Value = 2 Then
            BtnAssign.Visible = True
        Else
            BtnAssign.Visible = False
        End If

    End Sub

    Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAssign.Click
        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Dim LocCode As String
        Dim LocName As String
        Dim LocID As String
        Dim varAccName As String = String.Empty
        Dim j As Integer

        Dim DT As New DataTable
        DT.Columns.Add("LocID", GetType(System.String))
        DT.Columns.Add("LocName", GetType(System.String))
        DT.Columns.Add("LocCode", GetType(System.Int32))
        For j = 1 To HLoc.Value
            LocCode = "LocCode" & j
            LocName = "LocName" & j
            LocID = "LocID" & j
            If Request(LocCode) <> "" And Request(LocName) <> "" Then
                Dim DR As DataRow = DT.NewRow
                DR("LocName") = Request(LocName)
                DR("LocCode") = Request(LocCode)
                DR("LocID") = IIf(String.IsNullOrEmpty(Request(LocID)), String.Empty, Request(LocID))
                'Response.Write("Name : " & DR("LocName").ToString & " Code :" & DR("LocCode").ToString & " ID :" & DR("LocID").ToString & "<BR>")
                DT.Rows.Add(DR)
            End If
        Next
        Dim DSActLocs As New DataSet
        Dim clsActLoc As New ETS.BL.AccountsLocations
        With clsActLoc
            .SetAccountsLocations(DT, HActID.Value, hdnExistingIDs.Value.ToString)
            DT.Dispose()
            DSActLocs = .getAcountsLocationList()
        End With
        clsActLoc = Nothing
        Dim clsAcc As New ETS.BL.Accounts
        clsAcc.AccountID = HActID.Value.ToString
        clsAcc.getAccountDetails()
        If Not String.IsNullOrEmpty(clsAcc.AccountName) Then
            varAccName = clsAcc.AccountName
        End If
        'Response.Write("AccName : " & HActName.Value)
        For Each DR As DataRow In DSActLocs.Tables(0).Rows
            Dim c As New TableCell
            Dim c1 As New TableCell
            Dim c2 As New TableCell
            Dim c3 As New TableCell
            Dim r As New TableRow
            c.Text = varAccName.ToString
            c1.Text = DR("LocCode")
            c2.Text = DR("Locname")
            r.Cells.Add(c)
            r.Cells.Add(c1)
            r.Cells.Add(c2)
            Table4.Visible = True
            Table4.Rows.Add(r)
        Next
        DSActLocs.Dispose()
    End Sub
    Sub LocStatus()
        Dim varStrTempId As String = String.Empty
        Dim DSActLocs As New DataSet
        Dim clsActLoc As New ETS.BL.AccountsLocations
        With clsActLoc
            .AccountID = HActID.Value
            DSActLocs = .getAcountsLocationList()
        End With
        clsActLoc = Nothing
        If DSActLocs.Tables(0).Rows.Count > 0 Then
            Table2.Rows.Remove(Table2.Rows(1))
            HLoc.Value = 0
            For Each DR As DataRow In DSActLocs.Tables(0).Rows
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow
                HLoc.Value = HLoc.Value + 1
                Dim CHLocCode As New TextBox
                Dim CHLocName As New TextBox
                Dim hdnLocID As New HiddenField
                CHLocCode.ID = "LocCode" & HLoc.Value
                CHLocName.ID = "LocName" & HLoc.Value
                hdnLocID.ID = "LocID" & HLoc.Value
                CHLocCode.Text = DR("LocCode")
                CHLocName.Text = DR("Locname")
                hdnLocID.Value = DR("TrackID").ToString
                c1.Controls.Add(CHLocCode)
                c1.Controls.Add(hdnLocID)
                c2.Controls.Add(CHLocName)
                If String.IsNullOrEmpty(varStrTempId.ToString) Then
                    varStrTempId = DR("TrackID").ToString
                Else
                    varStrTempId = varStrTempId & "|" & DR("TrackID").ToString
                End If

                r.Cells.Add(c1)
                r.Cells.Add(c2)
                Table2.Rows.Add(r)
            Next
            DSActLocs.Dispose()
        End If
        If Not String.IsNullOrEmpty(varStrTempId) Then
            hdnExistingIDs.Value = varStrTempId
        End If

        clsActLoc = Nothing
    End Sub
End Class

