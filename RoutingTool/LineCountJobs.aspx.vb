Imports System.Data.SqlClient

Partial Class RoutingTool_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        'AssignUser()
        DictStatus()
     
        
   
        




    End Sub
  

    Sub DictStatus()
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim intExcMins As Integer
        intExcMins = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        TotJobs.Value = 0
        Dim ActName As String
        Dim TransID As String
        Dim SubmitDate As String
        Dim DueDate As Date
        Dim Jobnumber As String
        Dim TemplateName As String
        Dim uName As String
        Dim duration As String
        Dim Mins As Integer
        Dim dirStatus As String
        Dim DirMins As Integer
        Dim LevelNo As Integer
        Dim LvlAssn As String
        Dim Priority As Boolean
        Dim TAT As String
        Dim i As Integer
        Dim strJobnumber As String
        Dim Lvlchk As String = ProLevel.Value
        Dim LvlFound As Boolean = False
        Dim k As Integer
        i = 0
        LvlAssn = ""
        
        'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration)/60 as Mins, P.uname, TL.TemplateName from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.submitdate >= '1/9/2008' and T.submitdate < '1/10/2008' and T.status =" & Request("ProLevel") & " and T.AccountID = '" & HAccID.Value & "'  order by DueDate Desc"
        'strQuery = "Select T.TranscriptionID,  T.Jobnumber, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PU.direct from tbltranscriptionmain T LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID  INNER JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where userid='" & HUserID.Value & "' and LevelNo=" & ProLevel.Value & " and direct='True') AS PU ON PU.physicianID=T.dictatorID  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where T.AccountID='" & HAccID.Value & "' and T.status =" & Request("ProLevel") & " order by  DueDate  Asc"
        'strQuery = "Select distinct T.TranscriptionID,  T.Jobnumber, T.TAT, T.Priority, T.submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, PL.LevelName, U.username, PU.direct from tbltranscriptionmain T INNER JOIN (select * from tbltranscriptionlog order by datemodified) AS L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select userid, username from tblusers) AS U ON U.Userid=L.UserID LEFT JOIN tblProductionLevels PL ON PL.LevelNo = (T.status-100) LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where LevelNo in (" & ProLevel.Value & ") and direct='True') AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID   LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where  T.AccountID = '" & HAccID.Value & "' and T.status in (" & ProLevel.Value & ") order by  DueDate  Asc"
        strQuery = "Select distinct T.TranscriptionID,  T.Jobnumber, T.TAT, T.status, T.Priority, T.datecreated as submitDate, T.DueDate, A.AccountName, T.duration, datediff(s,0,T.duration) as Mins, P.uname, TL.TemplateName, isnull(PL.LevelName,PL1.LevelName) as LevelName, U.username, PU.direct, l.datemodified, DATEDIFF(hour, getdate(), T.Duedate) as RemTAT, T.datemodified  from tbltranscriptionmain T LEFT JOIN tblTranscriptionLog  L  ON L.TranscriptionID= T.TranscriptionID and T.status = L.status LEFT JOIN (Select userid, username from tblusers) AS U ON U.Userid=L.UserID LEFT JOIN (Select 'Pending ' + levelname as levelname, Levelno from tblProductionLevels) PL1 ON PL1.LevelNo = T.status LEFT JOIN (Select 'CheckedOut ' + levelname as levelname, Levelno from tblProductionLevels) PL ON PL.LevelNo = (T.status-100) LEFT JOIN (Select AccountID, AccountName from tblAccounts) AS A ON A.accountID=T.AccountID  LEFT JOIN (Select PhysicianID, firstname + ' ' + lastname as uname from tblphysicians) AS P ON P.physicianID=T.dictatorID     LEFT JOIN (Select userid, PhysicianID, LevelNo, 'Yes' as Direct from tblUserPrLvlMgmt where  direct='True') AS PU ON PU.userid=L.UserID and PU.physicianID=T.dictatorID and PU.Levelno = (T.status-100)  LEFT JOIN (Select TemplateID, TemplateName from tblTemplates) AS TL ON TL.TemplateID=T.TemplateID where  (T.LCStatus is null or T.LCStatus = 'False') and T.datemodified > '11/1/2009' and T.status = '1073741824' and T.accountid not in ('1B410F55-42D0-4329-83B3-7FBBD04A5196', 'DEA2179E-530E-4063-A37D-6754AF5C3B73')     order by  T.datemodified  asc"
        Response.Write(strQuery)
        Response.End()
        Dim DRExc As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            DRExc.Connection.Open()
            Dim JobDR As SqlDataReader = DRExc.ExecuteReader()
            If JobDR.HasRows Then
                While (JobDR.Read)


                    If strJobnumber = JobDR("jobnumber").ToString Then
                    Else
                        strJobnumber = JobDR("jobnumber").ToString

                        'Response.Write(JobDR("Direct"))
                        Mins = 0
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
                        Dim c12 As New TableCell
                        Dim c13 As New TableCell
                        Dim c14 As New TableCell
                        Dim r As New TableRow

                        If IsDBNull(JobDR("AccountName")) Then
                            ActName = ""
                        Else
                            ActName = JobDR("AccountName").ToString
                        End If

                        If IsDBNull(JobDR("TranscriptionID")) Then
                            TransID = ""
                        Else
                            TransID = JobDR("TranscriptionID").ToString
                        End If



                        If IsDBNull(JobDR("SubmitDate")) Then
                            SubmitDate = ""
                        Else
                            SubmitDate = JobDR("SubmitDate").ToString
                        End If

                        If IsDBNull(JobDR("DueDate")) Then
                            DueDate = Now
                            'r.BackColor = Drawing.Color.White
                            r.ForeColor = Drawing.Color.Black
                        Else
                            DueDate = JobDR("DueDate").ToString
                            If IsDate(DueDate) Then
                                If DueDate < Now() Then
                                    'r.BackColor = Drawing.Color.Maroon
                                    r.ForeColor = Drawing.Color.Maroon
                                End If
                            End If
                        End If

                        If IsDBNull(JobDR("Jobnumber")) Then
                            Jobnumber = ""
                        Else
                            Jobnumber = "<a href='jobdetails.aspx?status=" & JobDR("status").ToString() & "&transid=" & JobDR("transcriptionid").ToString() & "'>" & JobDR("Jobnumber").ToString & "</a>"
                        End If

                        If IsDBNull(JobDR("TemplateName")) Then
                            TemplateName = ""
                        Else
                            TemplateName = JobDR("TemplateName").ToString
                        End If

                        If IsDBNull(JobDR("uName")) Then
                            uName = ""
                        Else
                            uName = JobDR("uName").ToString
                        End If

                        If IsDBNull(JobDR("Duration")) Then
                            duration = ""
                        Else
                            duration = JobDR("Duration").ToString
                        End If
                        'Response.Write(JobDR("Mins").ToString)

                        If IsDBNull(JobDR("Mins")) Then
                            Mins = ""
                        Else
                            Mins = JobDR("Mins").ToString
                        End If

                        If JobDR("Priority").ToString = "True" Then
                            'r.BackColor = Drawing.Color.Yellow
                            r.ForeColor = Drawing.Color.BurlyWood
                            Priority = True
                        Else
                            Priority = False
                        End If


                        TAT = JobDR("TAT").ToString


                        If IsDBNull(JobDR("direct")) Then
                            dirStatus = "No"
                        Else
                            dirStatus = JobDR("direct").ToString
                            r.ForeColor = Drawing.Color.Black

                            DirMins = DirMins + Mins

                        End If


                        intExcMins = intExcMins + Mins


                        'If intExcMins < ExcMins Then
                        k = k + 1
                        Dim CB1 As New CheckBox



                        CB1.ID = "TransID" & k
                        CB1.InputAttributes.Add("Value", TransID & "#" & Mins)
                        'CB1.Attributes.Add("Checked", "No")
                        CB1.EnableViewState = "False"
                        CB1.InputAttributes.Add("onclick", "highlightRow(this)")
                        'CB1.Checked = "False"

                        c1.Controls.Add(CB1)

                        c2.Text = Jobnumber
                        c3.Text = duration
                        c4.Text = SubmitDate
                        c5.Text = DueDate
                        c6.Text = ActName
                        c7.Text = uName
                        c8.Text = TemplateName
                        c9.Text = JobDR("LevelName").ToString
                        c10.Text = TAT
                        c11.Text = Priority
                        c12.Text = dirStatus
                        c13.Text = JobDR("username").ToString
                        c14.Text = JobDR("RemTAT").ToString
                        r.Cells.Add(c1)
                        r.Cells.Add(c2)
                        r.Cells.Add(c9)
                        r.Cells.Add(c13)
                        r.Cells.Add(c10)
                        r.Cells.Add(c11)
                        r.Cells.Add(c3)
                        r.Cells.Add(c4)
                        r.Cells.Add(c5)
                        r.Cells.Add(c14)
                        r.Cells.Add(c6)
                        r.Cells.Add(c7)
                        r.Cells.Add(c12)

                        ' r.Cells.Add(c8)
                        r.Font.Size = "9"
                        r.Font.Italic = False

                        Table4.Rows.Add(r)

                        'Response.Write(CB1.Checked)


                        'Else
                        '    Exit While
                        'End If
                    End If
                End While
                'Response.Write(intExcMins)

                LblTMins.Text = FormatNumber((intExcMins / 60), 0)
                lblTotJobs.Text = k
                LblDMins.Text = FormatNumber((DirMins / 60), 0)
                TotJobs.Value = k
            Else

                ' LblTotmins.Text = intExcMins
                LblDMins.Text = DirMins
                lblTotJobs.Text = k
                Table4.Visible = False
                'submit.Visible = False


            End If
            JobDR.Close()
        Finally
            If DRExc.Connection.State = System.Data.ConnectionState.Open Then
                DRExc.Connection.Close()
                DRExc = Nothing
            End If
        End Try

    End Sub



  


    
End Class
