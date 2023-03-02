Imports System.Data.SqlClient

Partial Class RoutingTool_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim ProtocolMins As Integer
        Dim DoneMins As Integer
        Dim AwaitingEntry As Integer
        Dim CheckedOut As Integer
        Dim NotFinished As Integer
        Dim PrvPendingMins As Integer
        Dim PendingMins As Integer

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim strQuery As String

        Dim Cell1 As New TableCell
        Dim Cell2 As New TableCell
        Dim Cell3 As New TableCell
        Dim Cell4 As New TableCell
        Dim Cell5 As New TableCell
        Dim AccID As String
        Dim ClmMins() As Integer
        Dim ACount As Integer
        Dim Row1 As New TableRow
        ' Row1.CssClass = "SMSelected"
        Row1.Style("text-align") = "Center"
        Row1.CssClass = "noScroll"
        Dim ClmName() As String
        Dim ClmCount As Integer
        Cell1.Text = "Account Name"
        Cell2.Text = "Protocol Mins"
        Cell4.Text = "Pending Mins"

        Row1.Cells.Add(Cell1)
        Row1.Cells.Add(Cell2)
        Row1.Cells.Add(Cell4)
        ClmCount = 0
        strQuery = "Select DISTINCT  LevelName, LevelNo from tblproductionlevels where JobsRouting='True' order by LevelNo"
        'Response.Write(strQuery)

        Dim CommPL As New SqlCommand(strQuery, New SqlConnection(strConn))
        CommPL.Connection.Open()
        Dim DRPL As SqlDataReader = CommPL.ExecuteReader()
        If DRPL.HasRows Then
            While (DRPL.Read)
                ClmCount = ClmCount + 1
                If ClmCount = 1 Then
                    ReDim ClmName(ClmCount)
                Else
                    ReDim Preserve ClmName(ClmCount)
                End If
                ReDim ClmMins(ClmCount)
                ClmMins(ClmCount) = 0
                ClmName(ClmCount) = DRPL("LevelNO")
                Dim Ncell As New TableCell
                ' Ncell.BackColor = Drawing.Color.DarkOrange
                ' Ncell.ForeColor = Drawing.Color.AntiqueWhite

                Ncell.Text = "Pending " & DRPL("LevelName") & " Mins"
                Row1.Cells.Add(Ncell)
            End While
        End If
        '  Cell3.BackColor = Drawing.Color.DarkOrange
        '  Cell3.ForeColor = Drawing.Color.AntiqueWhite
        Cell3.Text = "Jobs In Other Status"

        Cell5.Text = "Previous Pending Mins"

        Row1.Cells.Add(Cell3)
        Row1.Cells.Add(Cell5)
        DRPL.Close()

        Table2.Rows.Add(Row1)

        CommPL.Connection.Close()

        'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,   T2.AwaitingEntry, T3.Checkedout, T4.DoneMins, T6.pendingmins from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824)  and submitdate >= '1/9/2008' and submitdate < '1/10/2008' group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as CheckedOut  from  tbltranscriptionmain TM   where  TM.status not in (Select LevelNo from tblProductionLevels)  and TM.status not in(1073741824)  and submitdate >= '1/9/2008' and submitdate < '1/10/2008' group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '1/9/2008' and submitdate < '1/10/2008' group by AccountID) AS T4 ON T4.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PendingMins  from tbltranscriptionmain  where   status not in(1073741824)  and submitdate < '1/9/2008'  group by AccountID) AS T6 ON T6.accountid = A.accountid  order by Accountname"
        'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,   T2.AwaitingEntry, T3.Checkedout, T4.DoneMins, T6.pendingmins from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824)   and (L.ForcedRouting is null or L.ForcedRouting = 'False') group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)) as CheckedOut  from  tbltranscriptionmain TM   where  TM.status not in (Select LevelNo from tblProductionLevels)  and TM.status not in(1073741824)   group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate > '3/1/2009' group by AccountID) AS T4 ON T4.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)) as PendingMins  from tbltranscriptionmain  where   status not in(1073741824) group by AccountID) AS T6 ON T6.accountid = A.accountid  order by Accountname"
        strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,   T2.AwaitingEntry, T3.Checkedout, T4.DoneMins, T6.pendingmins from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824)   and (L.ForcedRouting is null or L.ForcedRouting = 'False') and TM.submitdate > '3/1/2009'  group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)) as CheckedOut  from  tbltranscriptionmain TM   where  TM.status not in (Select LevelNo from tblProductionLevels)  and TM.status not in(1073741824)   and TM.submitdate > '3/1/2009'  group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate > '3/1/2009' group by AccountID) AS T4 ON T4.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)) as PendingMins  from tbltranscriptionmain  where   status not in(1073741824) and submitdate > '3/1/2009'  group by AccountID) AS T6 ON T6.accountid = A.accountid  order by Accountname"
        'Response.Write(strQuery)

        'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins,   T2.AwaitingEntry, T3.Checkedout, T4.DoneMins, T6.pendingmins from tblaccounts AS A   LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824) and (L.ForcedRouting is null or L.ForcedRouting = 'False')  and submitdate >= '1/9/2008' and submitdate < '1/10/2008' group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select TM.AccountID, sum(datediff(s,0,TM.duration)/60) as CheckedOut  from  tbltranscriptionmain TM   where  TM.status not in (Select LevelNo from tblProductionLevels where )  and TM.status not in(1073741824)  and submitdate >= '1/9/2008' and submitdate < '1/10/2008' group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '1/9/2008' and submitdate < '1/10/2008' group by AccountID) AS T4 ON T4.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PendingMins  from tbltranscriptionmain  where   status not in(1073741824)  and submitdate < '1/9/2008'  group by AccountID) AS T6 ON T6.accountid = A.accountid  order by Accountname"
        Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
        SQLCmd.Connection.Open()
        Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim c4 As New TableCell
                Dim c5 As New TableCell
                Dim c6 As New TableCell
                Dim c7 As New TableCell
                Dim r As New TableRow
                NotFinished = 0
                ' Dim Actname As New String
                AccID = DRRec("AccountID").ToString
                c1.Text = DRRec("Accountname").ToString
                If IsDBNull(DRRec("ProtocolMins")) Then
                    ProtocolMins = 0
                Else
                    ProtocolMins = DRRec("ProtocolMins").ToString
                End If

                'If IsDBNull(DRRec("PFreshMins")) Then
                '    PFreshMins = 0
                'Else
                '    PFreshMins = DRRec("PFreshMins").ToString
                'End If

                If IsDBNull(DRRec("AwaitingEntry")) Then
                    AwaitingEntry = 0
                Else
                    AwaitingEntry = FormatNumber((DRRec("AwaitingEntry").ToString / 60), 0)
                End If


                'If IsDBNull(DRRec("PNotFinished")) Then
                '    PNotFinishedMins = 0
                'Else
                '    PNotFinishedMins = DRRec("PNotFinished").ToString
                'End If


                If IsDBNull(DRRec("CheckedOut")) Then
                    CheckedOut = 0
                Else
                    CheckedOut = FormatNumber((DRRec("CheckedOut").ToString / 60), 0)
                End If

                'If IsDBNull(DRRec("PDoneMins")) Then
                '    PDoneMins = 0
                'Else
                '    PDoneMins = DRRec("PDoneMins").ToString
                'End If

                If IsDBNull(DRRec("DoneMins")) Then
                    DoneMins = 0
                Else
                    DoneMins = FormatNumber((DRRec("DoneMins").ToString / 60), 0)
                End If


                If IsDBNull(DRRec("PendingMins")) Then
                    PrvPendingMins = 0
                Else
                    PrvPendingMins = FormatNumber((DRRec("PendingMins").ToString / 60), 0)
                End If

                strQuery = "Select L.LevelNo, sum(datediff(s,0,TM.duration)) as AwaitingEntry  from  tbltranscriptionmain TM, tblProductionLevels L  where  TM.status=L.LevelNo and TM.status not in(1073741824)   and submitdate > '3/1/2009'  and L.JobsRouting = 'True' and TM.AccountID = '" & AccID & "' group by L.LevelNo"

                Dim CommPL1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                CommPL1.Connection.Open()
                Dim DRPL1 As SqlDataReader = CommPL1.ExecuteReader()
                If DRPL1.HasRows Then
                    While (DRPL1.Read)

                        For ACount = 1 To ClmCount
                            If ClmName(ACount) = DRPL1("LevelNO") Then
                                If IsDBNull(DRPL1("AwaitingEntry")) Then
                                    ClmMins(ACount) = 0
                                Else
                                    ClmMins(ACount) = FormatNumber((DRPL1("AwaitingEntry") / 60), 0)
                                End If
                            End If
                        Next
                        NotFinished = NotFinished + FormatNumber((DRPL1("AwaitingEntry") / 60), 0)
                    End While

                End If
                CommPL1.Connection.Close()

                NotFinished = NotFinished + AwaitingEntry + CheckedOut
                c2.Text = ProtocolMins
                'Response.Write(AwaitingEntry + CheckedOut)

                c3.Text = AwaitingEntry + CheckedOut
                'c4.Text = CheckedOut
                c6.Text = PrvPendingMins

                If ProtocolMins > DoneMins Then
                    PendingMins = ProtocolMins - DoneMins
                    If NotFinished < PendingMins Then
                        PendingMins = NotFinished
                    End If
                Else
                    PendingMins = 0
                End If
                c5.Text = PendingMins
                'If ExcMins > 0 Then
                '    c7.Text = "<a href=UpExcMins.aspx?AccID=" & DRRec("AccountID").ToString & " Target=_Blank>" & ExcMins & "</a>"
                'Else
                '    c7.Text = 0
                'End If

                r.Cells.Add(c1)
                r.Cells.Add(c2)
                r.Cells.Add(c5)
                For ACount = 1 To ClmCount
                    Dim NewCell As New TableCell
                    If ClmMins(ACount) > 0 Then
                        NewCell.Text = "<a href='Empstatus.aspx?AccID=" & DRRec("AccountID").ToString & "&ProLevel=" & ClmName(ACount) & "' Target=_Blank>" & ClmMins(ACount) & "</a>"
                    Else
                        NewCell.Text = ClmMins(ACount)
                    End If
                    ClmMins(ACount) = 0
                    r.Cells.Add(NewCell)
                Next

                r.Cells.Add(c3)
                'r.Cells.Add(c4)

                r.Cells.Add(c6)

                Table2.Rows.Add(r)
            End While
        End If
    End Sub
End Class
