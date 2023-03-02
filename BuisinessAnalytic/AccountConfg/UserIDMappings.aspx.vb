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

        DT.Columns.Add("UserName", GetType(System.String))
        DT.Columns.Add("MTID", GetType(System.String))
        DT.Columns.Add("QAID", GetType(System.String))
        DT.Columns.Add("QABID", GetType(System.String))


        'Response.Write(HLoc.Value)
        For j = 1 To HLoc.Value

            Name = "UserName" & j
            Name = "MTID" & j
            Name = "QAID" & j
            Name = "QABID" & j
            'If Request(Level) <> "" And Request(Level) <> "" Then
            Dim DR As DataRow = DT.NewRow

            DR("UserName") = Request(Name)
            DR("MTID") = Request(Name)
            DR("QAID") = Request(Name)
            DR("QABID") = Request(Name)
            'Response.Write(Factor & ":" & Request(Factor))

            'DR("ProdLevel") = IIf(String.IsNullOrEmpty(Request(ProdLevel)), String.Empty, Request(ProdLevel))
            'Response.Write("Name : " & DR("LocName").ToString & " Code :" & DR("LocCode").ToString & " ID :" & DR("LocID").ToString & "<BR>")
            DT.Rows.Add(DR)
            'End If
        Next
        Dim clsLevel As New ETS.BL.UsersIDMappings
        clsLevel.SetUsersIDMappings(DT, Session("ContractorID").ToString)
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
        Dim clsActLoc As New ETS.BL.UsersIDMappings
        With clsActLoc
            .ContractorID = Session("contractorID").ToString
            DSActLocs = .getUsersIDMappingsList
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

                Dim CHUserName As New TextBox
                Dim CHMTID As New TextBox
                Dim CHQAID As New TextBox
                Dim CHQABID As New TextBox
                CHUserName.ID = "UserName" & HLoc.Value
                CHMTID.ID = "MTID" & HLoc.Value
                CHQAID.ID = "QAID" & HLoc.Value
                CHQABID.ID = "QABID" & HLoc.Value


                CHUserName.Text = DR("UserName").ToString
                CHMTID.Text = DR("MTID").ToString
                CHQAID.Text = DR("QAID").ToString
                CHQABID.Text = DR("QABID").ToString

                c1.Controls.Add(CHUserName)
                c2.Controls.Add(CHMTID)
                c3.Controls.Add(CHQAID)
                c4.Controls.Add(CHQABID)

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

