Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data



Partial Class UserPhyAssgn_Default
    Inherits BasePage




    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DispBox.Text = ""
        If Not Page.IsPostBack Then
            Dim LI1 As New ListItem
            LI1.Value = ""
            LI1.Text = "All Users"
            LI1.Selected = True
            UserList.Items.Add(LI1)

            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim Cmd As New SqlCommand("Select userid, firstname + ' ' + lastname + ' (' + username +')' as uname from tblusers where (IsDeleted=0 or IsDeleted is null) and contractorID='" & Session("ContractorID") & "' order by firstname", New SqlConnection(strConn))
            Try
                Cmd.Connection.Open()
                Dim DRRec1 As SqlDataReader = Cmd.ExecuteReader()
                If DRRec1.HasRows Then
                    While (DRRec1.Read)
                        Dim LI As New ListItem
                        LI.Value = DRRec1("userid").ToString
                        LI.Text = IIf(IsDBNull(DRRec1("uname")), "", DRRec1("uname").ToString)
                        UserList.Items.Add(LI)
                    End While
                End If
                DRRec1.Close()
            Finally
                If Cmd.Connection.State = System.Data.ConnectionState.Open Then
                    Cmd.Connection.Close()
                    Cmd = Nothing
                End If
            End Try
        Else

        End If

    End Sub

    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnsubmit3.Click
        Dim strConn As String
        Dim Lvl As String
        If Not IsDate(sDate.Text) Then
            DispBox.Text = "Please enter proper start date."
            sDate.Focus()
            Exit Sub
        ElseIf Not IsDate(eDate.Text) Then
            DispBox.Text = "Please enter proper end date."
            eDate.Focus()
            Exit Sub
        End If
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim i As Integer
        Dim j As Integer

        Dim sQuery3 As String

        Dim Sqlstring As String
        Sqlstring = "SELECT IntialProduction from DBO.tblRSSStatus"
        Dim CmdRec As New SqlCommand(Sqlstring, New SqlConnection(strConn))
        Try
            CmdRec.Connection.Open()
            Dim DRRec As SqlDataReader = CmdRec.ExecuteReader()
            If DRRec.HasRows Then
                If DRRec.Read = True Then
                    HDirLevel.Value = DRRec("IntialProduction").ToString
                    'Response.Write("HDIrLevel:" & HDirLevel.Value)
                End If
            End If
            DRRec.Close()
        Finally
            If CmdRec.Connection.State = System.Data.ConnectionState.Open Then
                CmdRec.Connection.Close()
                CmdRec = Nothing
            End If
        End Try
        Dim SelLevelNo As String
        Dim SelActName As String
        Dim SelUserID As String

        SelLevelNo = ""
        SelActName = ""
        SelUserID = ""
        sQuery3 = "Select U.firstname + ' ' + U.lastname as uname, U.username, SUM(CASE WHEN Isnull(P.category, 'B') ='B' THEN 1 ELSE 0 END) as CateB, SUM(CASE WHEN P.category ='A' THEN 1 ELSE 0 END) as CateA  from tblProductionLevels PL, tblUsers U, tblUserPrLvlMgmt A, tblPhysicians P, tblAccounts At where A.direct='True' and A.LevelNo = '1' and A.UserID = U.UserID and A.LevelNo = PL.LevelNo and P.physicianID = A.physicianid and P.accountid = At.AccountID and AT.contractorID='" & Session("ContractorID") & "' and U.contractorID='" & Session("ContractorID") & "'"
        If UserList.SelectedValue <> "" Then
            sQuery3 = sQuery3 & "  and U.UserID ='" & UserList.SelectedValue & "' "
        End If
        sQuery3 = sQuery3 & " and  A.assigndate between '" & sDate.Text & "' and '" & eDate.Text & "' group by U.firstname + ' ' + U.lastname, username order by U.firstname + ' ' + U.lastname"

        Dim cmdSel As New SqlCommand(sQuery3, New SqlConnection(strConn))
        Try
            cmdSel.Connection.Open()
            Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
            If RdSet.HasRows = True Then
                Dim Cell1 As New TableCell
                Dim Cell2 As New TableCell
                Dim Cell3 As New TableCell
                Dim Cell4 As New TableCell
                Dim Cell5 As New TableCell
                Dim Row1 As New TableRow
                ' Row1.CssClass = "SMSelected"
                Cell1.Text = "Name"
                Cell2.Text = "Username"
                Cell3.Text = "Category A"
                Cell4.Text = "Category B"
                Cell5.Text = "Total"
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
                'Row1.BackColor = Drawing.Color.Navy
                'Row1.ForeColor = Drawing.Color.AntiqueWhite
                Row1.CssClass = "SMSelected"
                Row1.HorizontalAlign = HorizontalAlign.Center
                Cell5.HorizontalAlign = HorizontalAlign.Center
                Table4.Rows.Add(Row1)

                While (RdSet.Read)
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim c4 As New TableCell
                    Dim c5 As New TableCell
                    Dim c6 As New TableCell
                    Dim r As New TableRow
                    c2.Text = RdSet("uname").ToString
                    c3.Text = RdSet("username").ToString
                    c4.Text = RdSet("cateA").ToString
                    c5.Text = RdSet("cateB").ToString
                    c6.Text = CInt(RdSet("cateA").ToString) + CInt(RdSet("cateB").ToString)
                    r.Cells.Add(c2)
                    r.Cells.Add(c3)
                    r.Cells.Add(c4)
                    r.Cells.Add(c5)
                    r.Cells.Add(c6)


                    Table4.Rows.Add(r)

                    Table4.Visible = True
                    'Response.Write(RdSet("uName") & RdSet("LevelName") & RdSet("AccountName") & RdSet("pName"))
                End While
            Else
                DispBox.Text = "No Records Found."
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

