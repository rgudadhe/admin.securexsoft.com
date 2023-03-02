Imports System.Data.SqlClient

Partial Class Excess_Minutes_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    
        
    End Sub
    Private Sub Excmins(ByVal SrvStDate As Date)
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim ProtocolMins As Integer
        Dim FreshMins As Integer
        Dim PFreshMins As Integer
        Dim DoneMins As Integer
        Dim PDoneMins As Integer
        Dim NotFinishedMins As Integer
        Dim PNotFinishedMins As Integer
        Dim PendingMins As Integer
        Dim ExcMins As Integer
        Dim SrvEnddate As Date
        SrvEnddate = SrvStDate.AddDays(1)
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins, T.FreshMins, T1.PFreshMins, T2.NotFinished, T3.PNotFinished, T4.DoneMins, T5.PDoneMins, T6.pendingmins from tblaccounts AS A LEFT JOIN (Select AccountID, sum(datediff(s,0,duration))/60 as FreshMins  from tbltranscriptionmain where   submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEndDate & "' group by AccountID) AS T ON T.accountid = A.accountid  LEFT JOIN (Select AccountID, sum(datediff(s,0,duration))/60 as PFreshMins  from  tbltranscriptionmain  where  submitdate < '" & SrvStDate & "' and  duedate >= '" & SrvEndDate & "' and duedate < '1/11/2008' group by AccountID) AS T1 ON T1.accountid = A.accountid LEFT JOIN (Select AccountID, sum(datediff(s,0,duration))/60 as NotFinished  from  tbltranscriptionmain  where   status not in(1073741824)  and submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEndDate & "' group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select AccountID, sum(datediff(s,0,duration))/60 as PNotFinished  from  tbltranscriptionmain  where   status not in(1073741824)  and submitdate  < '" & SrvStDate & "' and  duedate >= '" & SrvEndDate & "' and duedate < '1/11/2008'   group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration))/60 as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEndDate & "' group by AccountID) AS T4 ON T4.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration))/60 as PDoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate < '" & SrvStDate & "' and  duedate >= '" & SrvEndDate & "' and duedate < '1/11/2008'  group by AccountID) AS T5 ON T5.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration))/60 as PendingMins  from tbltranscriptionmain  where   status not in(1073741824)  and submitdate < '" & SrvStDate & "' and duedate < '" & SrvEndDate & "' group by AccountID) AS T6 ON T6.accountid = A.accountid  order by Accountname"
        strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins, T.FreshMins,  T2.NotFinished, T4.DoneMins, T6.pendingmins from tblaccounts AS A LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)) as FreshMins  from tbltranscriptionmain where   submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEnddate & "' and ContractorID='" & Session("ContractorID") & "' group by AccountID) AS T ON T.accountid = A.accountid  LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)) as NotFinished  from  tbltranscriptionmain  where   status not in('1073741824', '3','5')  and submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEnddate & "' and ContractorID='" & Session("ContractorID") & "' group by AccountID) AS T2 ON T2.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEnddate & "' and ContractorID='" & Session("ContractorID") & "' group by AccountID) AS T4 ON T4.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)) as PDoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate < '" & SrvStDate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '" & SrvEnddate.AddDays(1) & "' and ContractorID='" & Session("ContractorID") & "'  group by AccountID) AS T5 ON T5.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)) as PendingMins  from tbltranscriptionmain  where   status not in('1073741824', '3','5')  and submitdate < '" & SrvStDate & "' and duedate < '" & SrvEnddate & "' and ContractorID='" & Session("ContractorID") & "' group by AccountID) AS T6 ON T6.accountid = A.accountid where A.ContractorID='" & Session("ContractorID") & "' order by Accountname"
        'Response.Write(strQuery)
        Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
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
                    ' Dim Actname As New String
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

                    If IsDBNull(DRRec("FreshMins")) Then
                        FreshMins = 0
                    Else

                        FreshMins = FormatNumber(DRRec("FreshMins").ToString / 60, 0)
                    End If


                    'If IsDBNull(DRRec("PNotFinished")) Then
                    '    PNotFinishedMins = 0
                    'Else
                    '    PNotFinishedMins = DRRec("PNotFinished").ToString
                    'End If


                    If IsDBNull(DRRec("NotFinished")) Then
                        NotFinishedMins = 0
                    Else
                        NotFinishedMins = FormatNumber(DRRec("NotFinished").ToString / 60, 0)
                    End If

                    'If IsDBNull(DRRec("PDoneMins")) Then
                    '    PDoneMins = 0
                    'Else
                    '    PDoneMins = DRRec("PDoneMins").ToString
                    'End If

                    If IsDBNull(DRRec("DoneMins")) Then
                        DoneMins = 0
                    Else
                        DoneMins = FormatNumber(DRRec("DoneMins").ToString / 60, 0)
                    End If


                    If IsDBNull(DRRec("PendingMins")) Then
                        PendingMins = 0
                    Else
                        PendingMins = FormatNumber(DRRec("PendingMins").ToString / 60, 0)
                    End If
                    'Response.Write(DoneMins)
                    NotFinishedMins = FreshMins - DoneMins
                    c2.Text = ProtocolMins
                    c3.Text = FreshMins
                    c4.Text = DoneMins
                    c5.Text = NotFinishedMins
                    c6.Text = PendingMins
                    If ProtocolMins > DoneMins Then
                        ExcMins = FreshMins - ProtocolMins
                    Else
                        ExcMins = FreshMins - DoneMins
                    End If

                    If ExcMins > 0 And ProtocolMins > 0 Then
                        c7.Text = "<a href='UpExcMins.aspx?AccID=" & DRRec("AccountID").ToString & "&SrvStDate=" & SrvStDate & "' Target=_Blank>" & ExcMins & "</a>"
                    Else
                        c7.Text = 0
                    End If

                    r.Cells.Add(c1)
                    r.Cells.Add(c2)
                    r.Cells.Add(c3)
                    r.Cells.Add(c4)
                    r.Cells.Add(c5)
                    r.Cells.Add(c6)

                    r.Cells.Add(c7)
                    If FreshMins > 0 Then
                        Table2.Rows.Add(r)
                    End If

                End While
            End If
            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Date1.Text = "" Then
            Lbl1.Text = "Please enter date."
            Date1.Focus()
            Exit Sub
        ElseIf Not IsDate(Date1.Text) Then
            Lbl1.Text = "Please enter date."
            Date1.Focus()
            Exit Sub
        Else
            Excmins(Date1.Text)
        End If
    End Sub
End Class
