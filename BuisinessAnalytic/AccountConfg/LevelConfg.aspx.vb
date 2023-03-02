Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LocStatus()
        End If
    End Sub
    

    Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAssign.Click
        'Panel7.Visible = False
        Dim ProdLevel As String
        Dim Name As String
        Dim Level As String
        Dim Factor As String
        Dim Weightage As String
        Dim varAccName As String = String.Empty
        Dim j As Integer

        Dim DT As New DataTable
        DT.Columns.Add("Level", GetType(System.Int32))
        DT.Columns.Add("Name", GetType(System.String))
        DT.Columns.Add("ProdLevel", GetType(System.Int32))
        DT.Columns.Add("Factor", GetType(System.Double))
        DT.Columns.Add("Weightage", GetType(System.Double))
        'Response.Write(HLoc.Value)
        For j = 1 To HLoc.Value
            Level = "Level" & j
            Name = "Name" & j
            ProdLevel = "ProdLevel" & j
            Factor = "Factor" & j
            Weightage = "Weightage" & j
            'If Request(Level) <> "" And Request(Level) <> "" Then
            Dim DR As DataRow = DT.NewRow
            DR("Level") = Request(Level)
            DR("Name") = Request(Name)
            'DR("ProdLevel") = Request(ProdLevel)
            DR("Factor") = Request(Factor)
            DR("Weightage") = Request(Weightage)
            'Response.Write(Factor & ":" & Request(Factor))

            'DR("ProdLevel") = IIf(String.IsNullOrEmpty(Request(ProdLevel)), String.Empty, Request(ProdLevel))
            'Response.Write("Name : " & DR("LocName").ToString & " Code :" & DR("LocCode").ToString & " ID :" & DR("LocID").ToString & "<BR>")
            DT.Rows.Add(DR)
            'End If
        Next
        Dim clsLevel As New ETS.BL.Levelconfg
        clsLevel.SetLevelConfg(DT, Session("ContractorID").ToString)
        DT.Dispose()
        'DSActLocs = .getAcountsLocationList()
        'End With
        'clsActLoc = Nothing
        'Dim clsAcc As New ETS.BL.Accounts
        'clsAcc.AccountID = HActID.Value.ToString
        'clsAcc.getAccountDetails()
        'If Not String.IsNullOrEmpty(clsAcc.AccountName) Then
        '    varAccName = clsAcc.AccountName
        'End If
        ''Response.Write("AccName : " & HActName.Value)
        'For Each DR As DataRow In DSActLocs.Tables(0).Rows
        '    Dim c As New TableCell
        '    Dim c1 As New TableCell
        '    Dim c2 As New TableCell
        '    Dim c3 As New TableCell
        '    Dim r As New TableRow
        '    c.Text = varAccName.ToString
        '    c1.Text = DR("LocCode")
        '    c2.Text = DR("Locname")
        '    r.Cells.Add(c)
        '    r.Cells.Add(c1)
        '    r.Cells.Add(c2)
        '    Table4.Visible = True
        '    Table4.Rows.Add(r)
        'Next
        'DSActLocs.Dispose()
        LocStatus()
    End Sub
    Sub LocStatus()
        Dim varStrTempId As String = String.Empty
        Dim DSActLocs As New DataSet
        Dim clsActLoc As New ETS.BL.Levelconfg
        With clsActLoc
            .ContractorID = Session("contractorID").ToString
            DSActLocs = .getAcountsLevelConfgList
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
                Dim c4 As New TableCell
                Dim r As New TableRow
                HLoc.Value = HLoc.Value + 1
                Dim CHLevel As New TextBox
                Dim CHName As New TextBox
                Dim CHProdLevel As New TextBox
                Dim CHFactor As New TextBox
                Dim CHWeightage As New TextBox

                CHLevel.ID = "Level" & HLoc.Value
                CHName.ID = "Name" & HLoc.Value
                CHProdLevel.ID = "ProdLevel" & HLoc.Value
                CHFactor.ID = "Factor" & HLoc.Value
                CHWeightage.ID = "Weightage" & HLoc.Value
                CHLevel.Text = DR("Level").ToString
                CHName.Text = DR("name").ToString
                CHFactor.Text = DR("Factor").ToString
                CHWeightage.Text = DR("Weightage").ToString
                CHProdLevel.Text = DR("ProdLevel").ToString

                c.Controls.Add(CHLevel)
                c1.Controls.Add(CHName)
                c2.Controls.Add(CHProdLevel)
                c3.Controls.Add(CHFactor)
                c4.Controls.Add(CHWeightage)
                r.Cells.Add(c)
                r.Cells.Add(c1)
                r.Cells.Add(c2)
                r.Cells.Add(c3)
                r.Cells.Add(c4)

                Table2.Rows.Add(r)
            Next
            DSActLocs.Dispose()
        End If
        

        clsActLoc = Nothing
    End Sub
End Class

