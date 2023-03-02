Imports System.Data.SqlClient

Partial Class Excess_Minutes_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
            submit_Click()
            Excmins(SrvStDate.Value)
        Else
            SrvStDate.Value = Request("SrvStDate")
            HAccID.Value = Request("AccID")
            Excmins(SrvStDate.Value)
        End If

    End Sub
    Private Sub Excmins(ByVal SrvStDate As Date)
        Try
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
            Dim intExcMins As Integer
            Dim intExcSecs As Integer
            intExcMins = 0
            intExcSecs = 0
            Dim SrvEnddate As Date
            SrvEnddate = SrvStDate.AddDays(1)
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim strQuery As String
            '        strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins, T.FreshMins, T1.PFreshMins, T2.NotFinished, T3.PNotFinished, T4.DoneMins, T5.PDoneMins, T6.pendingmins from tblaccounts AS A LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as FreshMins  from tbltranscriptionmain where   submitdate >= '" & SrvStdate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T ON T.accountid = A.accountid  LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as PFreshMins  from  tbltranscriptionmain  where  submitdate < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008' group by AccountID) AS T1 ON T1.accountid = A.accountid LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as NotFinished  from  tbltranscriptionmain  where   status not in(1073741824)  and submitdate >= '" & SrvStdate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as PNotFinished  from  tbltranscriptionmain  where   status not in(1073741824)  and submitdate  < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008'   group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '" & SrvStdate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T4 ON T4.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PDoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008'  group by AccountID) AS T5 ON T5.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PendingMins  from tbltranscriptionmain  where   status not in(1073741824)  and submitdate < '" & SrvStdate & "' and duedate < '" & SrvEnddate & "' group by AccountID) AS T6 ON T6.accountid = A.accountid  where A.AccountID = '" & HAccID.Value & "'  order by Accountname"
            'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins, T.FreshMins, T1.PFreshMins, T2.NotFinished, T3.PNotFinished, T4.DoneMins, T5.PDoneMins, T6.pendingmins from tblaccounts AS A LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as FreshMins  from tbltranscriptionmain where   submitdate >= '" & SrvStdate & "' and submitdate < '" & SrvEnddate & "' and duedate < '1/11/2008'  group by AccountID) AS T ON T.accountid = A.accountid  LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as PFreshMins  from  tbltranscriptionmain  where  submitdate < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008' group by AccountID) AS T1 ON T1.accountid = A.accountid LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as NotFinished  from  tbltranscriptionmain  where   status not in(1073741824)  and submitdate >= '" & SrvStdate & "' and submitdate < '" & SrvEnddate & "' and duedate < '1/11/2008'  group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as PNotFinished  from  tbltranscriptionmain  where   status not in(1073741824)  and submitdate  < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008'   group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '" & SrvStdate & "' and submitdate < '" & SrvEnddate & "' and duedate < '1/11/2008'  group by AccountID) AS T4 ON T4.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PDoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008'  group by AccountID) AS T5 ON T5.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PendingMins  from tbltranscriptionmain  where   status not in(1073741824)  and submitdate < '" & SrvStdate & "' and duedate < '" & SrvEnddate & "' group by AccountID) AS T6 ON T6.accountid = A.accountid  where A.AccountID = '" & HAccID.Value & "'  order by Accountname"
            'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins, T.FreshMins,  T2.NotFinished, T4.DoneMins, T6.pendingmins from tblaccounts AS A LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as FreshMins  from tbltranscriptionmain where   submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T ON T.accountid = A.accountid  LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as NotFinished  from  tbltranscriptionmain  where   status not in('1073741824', '3','5')  and submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T2 ON T2.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T4 ON T4.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PDoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate < '" & SrvStDate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008'  group by AccountID) AS T5 ON T5.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PendingMins  from tbltranscriptionmain  where   status not in('1073741824', '3','5')  and submitdate < '" & SrvStDate & "' and duedate < '" & SrvEnddate & "' group by AccountID) AS T6 ON T6.accountid = A.accountid  where A.AccountID = '" & HAccID.Value & "'   order by Accountname"
            strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins, T.FreshMins,  T2.NotFinished, T4.DoneMins, T6.pendingmins from tblaccounts AS A LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)) as FreshMins  from tbltranscriptionmain where   submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T ON T.accountid = A.accountid  LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)) as NotFinished  from  tbltranscriptionmain  where   status not in('1073741824', '3','5')  and submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T2 ON T2.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '" & SrvStDate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T4 ON T4.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)) as PDoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate < '" & SrvStDate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '" & SrvEnddate.AddDays(1) & "'   group by AccountID) AS T5 ON T5.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)) as PendingMins  from tbltranscriptionmain  where   status not in('1073741824', '3','5')  and submitdate < '" & SrvStDate & "' and duedate < '" & SrvEnddate & "' group by AccountID) AS T6 ON T6.accountid = A.accountid  where A.AccountID = '" & HAccID.Value & "'  order by Accountname"
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
            TotJobs.Value = 0
            Dim ActName As String
            Dim TransID As String
            Dim SubmitDate As String
            Dim DueDate As String
            Dim Jobnumber As String
            Dim TemplateName As String
            Dim uName As String
            Dim duration As String
            Dim Mins As Integer
            Dim k As Integer
            k = 0
            If ExcMins > 0 Then
                'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration)/60 as Mins, P.uname, TL.TemplateName from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.submitdate >= '" & SrvStdate & "' and T.submitdate < '" & SrvEnddate & "' and T.status not in(1073741824) and T.AccountID = '" & HAccID.Value & "'  UNION Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration)/60 as Mins, P.uname, TL.TemplateName from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where submitdate  < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008'  and T.status not in(1073741824)  and T.AccountID = '" & HAccID.Value & "' order by DueDate Desc"
                'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration)/60 as Mins, P.uname, TL.TemplateName from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.submitdate >= '" & SrvStdate & "' and T.submitdate < '" & SrvEnddate & "' and duedate < '1/11/2008' and T.status not in(1073741824) and T.AccountID = '" & HAccID.Value & "'  UNION Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration)/60 as Mins, P.uname, TL.TemplateName from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where submitdate  < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008'  and T.status not in(1073741824)  and T.AccountID = '" & HAccID.Value & "' order by DueDate Desc"

                strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Secs,  P.uname, TL.TemplateName, T.TAT , isnull(PL.LevelName,PL1.LevelName)  as Lname, T.Priority from tbltranscriptionmain T  LEFT JOIN (Select 'Pending ' + levelname as levelname, Levelno from tblProductionLevels) PL1 ON PL1.LevelNo = T.status LEFT JOIN (Select 'CheckedOut ' + levelname as levelname, Levelno from tblProductionLevels) PL ON PL.LevelNo = (T.status-100) LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.submitdate >= '" & SrvStDate & "' and T.submitdate < '" & SrvEnddate & "' and T.status not in(1073741824, 5, 3) and T.AccountID = '" & HAccID.Value & "'  order by DueDate Desc"
                'strQuery = "Select DISTINCT  A.AccountID, A.Accountname, A.ProtocolMins, T.FreshMins, T1.PFreshMins, T2.NotFinished, T3.PNotFinished, T4.DoneMins, T5.PDoneMins, T6.pendingmins from tblaccounts AS A LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as FreshMins  from tbltranscriptionmain where   submitdate >= '" & SrvStdate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T ON T.accountid = A.accountid  LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as PFreshMins  from  tbltranscriptionmain  where  submitdate < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008' group by AccountID) AS T1 ON T1.accountid = A.accountid LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as NotFinished  from  tbltranscriptionmain  where   status not in(1073741824)  and submitdate >= '" & SrvStdate & "' and submitdate < '" & SrvEnddate & "' and duedate < '1/11/2008' group by AccountID) AS T2 ON T2.accountid = A.accountid LEFT JOIN (Select AccountID, sum(datediff(s,0,duration)/60) as PNotFinished  from  tbltranscriptionmain  where   status not in(1073741824)  and submitdate  < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008'   group by AccountID) AS T3 ON T3.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as DoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate >= '" & SrvStdate & "' and submitdate < '" & SrvEnddate & "' group by AccountID) AS T4 ON T4.accountid = A.accountid Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PDoneMins  from  tbltranscriptionmain  where   status in(1073741824)  and submitdate < '" & SrvStdate & "' and  duedate >= '" & SrvEnddate & "' and duedate < '1/11/2008'  group by AccountID) AS T5 ON T5.accountid = A.accountid  Left Join  (Select AccountID, sum(datediff(s,0,duration)/60) as PendingMins  from tbltranscriptionmain  where   status not in(1073741824)  and submitdate < '" & SrvStdate & "' and duedate < '" & SrvEnddate & "' group by AccountID) AS T6 ON T6.accountid = A.accountid  where A.AccountID = '" & HAccID.Value & "'  order by Accountname"
                'Response.Write(strQuery)
                'Response.End()


                Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
                Try
                    DRExc.Connection.Open()
                    Dim ExcDataReader As SqlDataReader = DRExc.ExecuteReader()
                    If ExcDataReader.HasRows Then
                        While (ExcDataReader.Read)


                            If IsDBNull(ExcDataReader("AccountName")) Then
                                ActName = ""
                            Else
                                ActName = ExcDataReader("AccountName").ToString
                            End If

                            If IsDBNull(ExcDataReader("TranscriptionID")) Then
                                TransID = ""
                            Else
                                TransID = ExcDataReader("TranscriptionID").ToString
                            End If

                            If IsDBNull(ExcDataReader("SubmitDate")) Then
                                SubmitDate = ""
                            Else
                                SubmitDate = ExcDataReader("SubmitDate").ToString
                            End If

                            If IsDBNull(ExcDataReader("DueDate")) Then
                                DueDate = ""
                            Else
                                DueDate = ExcDataReader("DueDate").ToString
                            End If

                            If IsDBNull(ExcDataReader("Jobnumber")) Then
                                Jobnumber = ""
                            Else
                                Jobnumber = ExcDataReader("Jobnumber").ToString
                            End If

                            If IsDBNull(ExcDataReader("TemplateName")) Then
                                TemplateName = ""
                            Else
                                TemplateName = ExcDataReader("TemplateName").ToString
                            End If

                            If IsDBNull(ExcDataReader("uName")) Then
                                uName = ""
                            Else
                                uName = ExcDataReader("uName").ToString
                            End If

                            If IsDBNull(ExcDataReader("Duration")) Then
                                duration = ""
                            Else
                                duration = ExcDataReader("Duration").ToString
                            End If

                            'If IsDBNull(ExcDataReader("Mins")) Then
                            '    Mins = ""
                            'Else
                            '    Mins = ExcDataReader("Mins").ToString
                            'End If
                            intExcSecs = intExcSecs + ExcDataReader("Secs").ToString
                            intExcMins = (intExcSecs / 60)
                            If intExcMins < ExcMins Then
                                k = k + 1
                                Dim CB1 As New CheckBox
                                lblTotMins.Text = intExcMins
                                lblTotJobs.Text = k

                                Dim c1 As New TableCell
                                Dim c2 As New TableCell
                                Dim c3 As New TableCell
                                Dim c4 As New TableCell
                                Dim c5 As New TableCell
                                Dim c6 As New TableCell
                                Dim c7 As New TableCell
                                Dim c8 As New TableCell
                                Dim c9 As New TableCell
                                Dim c10 As New TableCell
                                Dim c11 As New TableCell

                                Dim r As New TableRow
                                CB1.ID = "TransID" & k
                                CB1.Checked = False
                                'Response.Write(CB1.Checked)


                                CB1.InputAttributes.Add("Value", TransID)
                                CB1.InputAttributes.Add("onclick", "highlightRow(this)")
                                TotJobs.Value = k
                                c1.Controls.Add(CB1)
                                c2.Text = Jobnumber
                                c3.Text = duration
                                c4.Text = SubmitDate
                                c5.Text = DueDate
                                c6.Text = ActName
                                c7.Text = uName
                                c8.Text = TemplateName
                                'If InStr(ExcDataReader("lname").ToString, "CheckedOut") > 0 Then
                                c9.Text = ExcDataReader("lname").ToString
                                'Else
                                '    c9.Text = "Pending " & ExcDataReader("lname").ToString
                                'End If

                                c10.Text = ExcDataReader("TAT").ToString
                                c11.Text = ExcDataReader("priority").ToString

                                r.Cells.Add(c1)
                                r.Cells.Add(c2)
                                r.Cells.Add(c3)
                                r.Cells.Add(c9)
                                r.Cells.Add(c8)
                                r.Cells.Add(c4)
                                r.Cells.Add(c10)
                                r.Cells.Add(c5)
                                r.Cells.Add(c11)
                                r.Cells.Add(c6)
                                r.Cells.Add(c7)
                                r.Cells.Add(c8)
                                Table3.Rows.Add(r)
                            Else
                                Exit While
                            End If

                        End While
                    End If
                    ExcDataReader.Close()
                Finally
                    If DRExc.Connection.State = System.Data.ConnectionState.Open Then
                        DRExc.Connection.Close()
                        DRExc = Nothing
                    End If
                End Try
            Else
                lblTotMins.Text = intExcMins
                lblTotJobs.Text = k
                Table3.Visible = False
                submit.Visible = False

            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Sub submit_Click()
        Dim TransID As String
        Dim I As Integer

        For I = 1 To TotJobs.Value
            TransID = "TransID" & I
            If Request(TransID) <> "" Then
                Dim strConn As String
                strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim strQuery As String
                strQuery = "update tbltranscriptionMain set TAT=TAT+24, submitdate=DateAdd([day], 1, submitdate), duedate = DateAdd([day], 1, duedate) Where TranscriptionID ='" & Request(TransID) & "' "
                'strQuery = "update tbltranscriptionMain set TAT=TAT+24, duedate = DateAdd([day], 1, duedate) Where TranscriptionID ='" & Request(TransID) & "' "
                'Response.Write(strQuery)
                'Response.End()

                Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
                Try
                    DRExc.Connection.Open()
                    DRExc.ExecuteNonQuery()
                Finally
                    If DRExc.Connection.State = System.Data.ConnectionState.Open Then
                        DRExc.Connection.Close()
                        DRExc = Nothing
                    End If
                End Try
            End If

        Next

    End Sub


End Class
