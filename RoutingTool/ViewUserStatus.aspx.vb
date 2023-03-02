Imports System.Data.SqlClient
Imports System.Data
Partial Class RoutingTool_Default
    Inherits BasePage
    Private hourdiff As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    
        If Not IsPostBack Then
            Panel1.Visible = False
            ViewLevels()
            DLLevel.SelectedIndex = 0
            If DLLevel.Items.Count > 0 Then
                ViewStatus(DLLevel.SelectedValue)
            End If

        End If
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)


    End Sub
    Protected Sub ViewLevels()
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        strQuery = "Select Levelname, LevelNo from tblProductionLevels where contractorid='" & Session("contractorid") & "' and  JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
        Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUser.Connection.Open()
            Dim DRRec1 As SqlDataReader = CMUser.ExecuteReader()
            If DRRec1.HasRows Then
                While (DRRec1.Read)
                    Dim LI As New ListItem
                    LI.Text = DRRec1("LevelNAme").ToString
                    LI.Value = DRRec1("LevelNo").ToString
                    DLLevel.Items.Add(LI)
                End While
            End If
            DRRec1.Close()
        Finally

            If CMUser.Connection.State = ConnectionState.Open Then
                CMUser.Connection.Close()
            End If
        End Try
    End Sub
    Sub ViewStatus(ByVal LvlNo As Integer)
        Panel1.Visible = True
        hourdiff = DateDiff(DateInterval.Hour, ServStartDate, Now)
        Dim strConn As String
        Dim strQuery As String
        Dim scdMins As Integer
        Dim MinsDone As Integer
        Dim MinsAssn As Integer
        Dim MinsPend As Integer
        Dim DirMins As Integer
        Dim COLvlNo As Integer
        Dim LvlName As String
        Dim LevelNo As Integer
        Dim LvlNOAssgn As String
        Dim LvlAssn As String
        Dim LvlA(0) As String
        Dim i As Integer = 0
        Dim j As Integer
        LvlAssn = ""
        LvlName = ""
        LvlNOAssgn = ""
        COLvlNo = 100 + LvlNo
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim Userid As String
        Dim Lvl(0) As String
        Dim SelQuery As String = String.Empty
        Dim JoinQuery As String = String.Empty
        Dim Incr As Integer = 0
        strQuery = "Select Levelname, LevelNo from tblProductionLevels where contractorid='" & Session("contractorid") & "' and  JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
        Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUsr.Connection.Open()
            Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
            If DRRec.HasRows Then
                While (DRRec.Read)
                    Incr = Incr + 1
                    LevelNo = DRRec("LevelNo") + 100
                    If i = 0 Then
                        SelQuery = "DN" & Incr & ".DNmins" & Incr & ", DN" & Incr & ".CntDN" & Incr & ","
                        SelQuery = SelQuery & "CH" & Incr & ".CHmins" & Incr & ", CH" & Incr & ".CntCH" & Incr & ","
                        JoinQuery = "LEFT OUTER JOIN (select count(*) as CntDN" & Incr & ",sum(datediff(s,0,M.duration)) as DNMins" & Incr & ", L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & "   and L.TranscriptionID= M.TranscriptionID and M.ContractorID='" & Session("ContractorID") & "' and L.Userlevel in (" & DRRec("LevelNo").ToString & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & DRRec("LevelNo").ToString & ")) group by L.UserID) DN" & Incr & " ON DN" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select count(*) as CntCH" & Incr & ",sum(datediff(s,0,M.duration)) as CHMins" & Incr & ", L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.ContractorID='" & Session("ContractorID") & "' and M.status in (" & LevelNo & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and M.ContractorID='" & Session("ContractorID") & "' and status in (" & LevelNo & ") )  group by L.UserID) CH" & Incr & " ON CH" & Incr & ".userid = U.USerID "
                        LDone.Text = "FN[" & DRRec("LevelName").ToString & "]</td>"
                        LOut.Text = "CHK[" & DRRec("LevelName").ToString & "]</td>"
                        LvlAssn = LevelNo
                        LvlNOAssgn = DRRec("LevelNo").ToString
                    Else
                        SelQuery = SelQuery & "DN" & Incr & ".DNmins" & Incr & ", DN" & Incr & ".CntDN" & Incr & ","
                        SelQuery = SelQuery & "CH" & Incr & ".CHmins" & Incr & ", CH" & Incr & ".CntCH" & Incr & ","
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select count(*) as CntDN" & Incr & ",sum(datediff(s,0,M.duration)) as DNMins" & Incr & ", L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & "  and L.TranscriptionID= M.TranscriptionID and M.ContractorID='" & Session("ContractorID") & "' and L.Userlevel in (" & DRRec("LevelNo").ToString & ") and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & DRRec("LevelNo").ToString & ")) group by L.UserID) DN" & Incr & " ON DN" & Incr & ".userid = U.USerID "
                        JoinQuery = JoinQuery & "LEFT OUTER JOIN (select count(*) as CntCH" & Incr & ",sum(datediff(s,0,M.duration)) as CHMins" & Incr & ", L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.ContractorID='" & Session("ContractorID") & "' and M.status in (" & LevelNo & ") and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LevelNo & ") ) group by L.UserID) CH" & Incr & " ON CH" & Incr & ".userid = U.USerID "
                        ReDim Preserve Lvl(i)
                        ReDim Preserve LvlA(i)
                        LDone.Text = LDone.Text & "<td>FN[" & DRRec("LevelName").ToString & "]</td>"
                        LOut.Text = LOut.Text & "<td>CHK[" & DRRec("LevelName").ToString & "]</td>"
                        LvlAssn = LvlAssn & "," & LevelNo
                        LvlNOAssgn = LvlNOAssgn & "," & DRRec("LevelNo").ToString
                    End If
                    LvlA(i) = LevelNo
                    Lvl(i) = DRRec("LevelNo")
                    i = i + 1
                End While
            End If
            DRRec.Close()
        Finally
            If CMUsr.Connection.State = ConnectionState.Open Then
                CMUsr.Connection.Close()
            End If
        End Try
        LDone.Text = LDone.Text & "<td class=""alt1"">FN[Total]"
        LOut.Text = LOut.Text & "<td class=""alt1"">CHK[Total]"
        strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, " & SelQuery & " DN.DNMins, CH.CHMins, DR.Mins as DIRMins, U.UserID, S.Mins as SCHMins, S.startTime, S.EndTime, L.RecFound, L.TypeOfLeave from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False') and ContractorID='" & Session("ContractorID") & "') U INNER JOIN tblUsersLevels UL ON  UL.UserID = U.USerID LEFT OUTER JOIN (Select * from tblSchedule where ScheduleDate = '" & ProcStartDate & "' ) S ON U.UserID=S.USerID LEFT OUTER  JOIN (Select 'Yes' as RecFound, userID, startdate, enddate,TypeOfLeave from tblLeave where (Isdeleted is NULL Or IsDeleted = 'False') and startDate <= '" & ProcStartDate & "' and endDate >= '" & ProcStartDate & "') L ON U.UserID=L.USerID " & JoinQuery & " LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as DNMins, L.userid from tbltranscriptionstatus L, tbltranscriptionMain M where  datediff(hh, L.Datemodified, getdate()) <= " & hourdiff & "   and L.TranscriptionID= M.TranscriptionID  and L.Userlevel in (" & LvlNOAssgn & ") and L.status in (Select LevelNo from tblProductionlevels ) and M.ContractorID='" & Session("ContractorID") & "' group by L.UserID) DN ON DN.userid = U.USerID " & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as CHMins, L.userid from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID  and M.status = L.status and M.status in (" & LvlAssn & ")  and L.datemodified=(select max(datemodified)from tbltranscriptionlog where TranscriptionID= M.TranscriptionID and M.status = status and status in (" & LvlAssn & ") and M.ContractorID='" & Session("ContractorID") & "') group by L.UserID) CH ON CH.userid = U.USerID " & "LEFT OUTER JOIN (select sum(datediff(s,0,M.duration)) as Mins, L.userid from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and  Direct='True' and M.status=L.LevelNo and M.status=" & LvlNo & " and M.ContractorID='" & Session("ContractorID") & "' group by L.UserID) DR ON DR.userid = U.USerID " & " where     (U.Isdeleted is null or U.isdeleted = 'False') and  U.ContractorID='" & Session("ContractorID") & "' and dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'   order by CHMins desc "
        'Response.Write(strQuery)
        Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            CMUser.Connection.Open()
            CMUser.CommandTimeout = 1200
            Dim DRRec1 As SqlDataReader = CMUser.ExecuteReader()
            If DRRec1.HasRows Then
                While (DRRec1.Read)
                    Dim CL1 As New TableCell
                    Dim CL2 As New TableCell
                    Dim CL3 As New TableCell
                    Dim CL4 As New TableCell
                    Dim CL5 As New TableCell
                    Dim RW1 As New TableRow
                    Dim CL6 As New TableCell
                    Dim CL7 As New TableCell
                    Userid = DRRec1("UserID").ToString
                    RW1.BorderWidth = "1"
                    If DRRec1("RecFound").ToString <> "" Then
                        If DRRec1("TypeOfLeave").ToString = "HL" Or DRRec1("TypeOfLeave").ToString = "LWPHL" Then
                            RW1.BackColor = Drawing.Color.LightBlue
                            RW1.ForeColor = Drawing.Color.White
                            CL1.Text = DRRec1("uname")
                        Else
                            RW1.BackColor = Drawing.Color.LightGray
                            RW1.ForeColor = Drawing.Color.White
                            CL1.Text = DRRec1("uname")
                        End If
                    Else
                        CL1.Text = "<a href='UserRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlNo & " '  Target=_Blank>" & DRRec1("uname") & "</a>"
                    End If
                    CL1.Font.Bold = True
                    CL1.HorizontalAlign = HorizontalAlign.Left
                    CL2.Text = DRRec1("username")
                    If IsDBNull(DRRec1("SchMins")) Then
                        scdMins = 0
                        CL3.Text = 0
                    Else
                        scdMins = DRRec1("SchMins").ToString
                        CL3.Text = DRRec1("SchMins").ToString
                    End If
                    If IsDBNull(DRRec1("DNMins")) Then
                        MinsDone = 0
                        CL4.Text = 0
                    Else
                        MinsDone = FormatNumber((DRRec1("DNMins").ToString / 60), 0)
                        CL4.Text = FormatNumber((DRRec1("DNMins").ToString / 60), 0)
                    End If
                    If IsDBNull(DRRec1("CHMins")) Then
                        MinsAssn = 0
                        CL5.Text = 0
                    Else
                        MinsAssn = FormatNumber((DRRec1("CHMins").ToString / 60), 0)
                        CL5.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlAssn & " '  Target=_Blank>" & FormatNumber((DRRec1("CHMins").ToString / 60), 0) & "</a>"
                    End If
                    If scdMins > 0 Then
                        MinsPend = scdMins - (MinsDone + MinsAssn)
                        If MinsPend < 0 Then
                            MinsPend = 0
                        End If
                    Else
                        MinsPend = 0
                    End If
                    If IsDBNull(DRRec1("DirMins")) Then
                        DirMins = 0
                        CL7.Text = 0
                    Else
                        DirMins = FormatNumber((DRRec1("DirMins").ToString / 60), 0)
                        CL7.Text = FormatNumber((DRRec1("DirMins").ToString / 60), 0)
                    End If
                    CL7.Text = DirMins
                    CL6.Text = MinsPend
                    Dim STime As New TableCell
                    Dim ETime As New TableCell
                    If DRRec1("starttime").ToString = "" Then
                        STime.Text = "-"
                    Else
                        STime.Text = DRRec1("starttime").ToString
                    End If
                    If DRRec1("endtime").ToString = "" Then
                        ETime.Text = "-"
                    Else
                        ETime.Text = DRRec1("endtime").ToString
                    End If
                    'STime.BorderColor = Drawing.Color.DimGray
                    'ETime.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL1)
                    RW1.Cells.Add(CL2)
                    'CL3.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL3)
                    RW1.Cells.Add(STime)
                    RW1.Cells.Add(ETime)
                    For j = 1 To Incr
                        Dim CellD As New TableCell
                        'CellD.BorderColor = Drawing.Color.DimGray
                        If IsDBNull(DRRec1("DNMins" & j)) Then
                            CellD.Text = 0
                        Else
                            CellD.Text = FormatNumber((DRRec1("DNMins" & j).ToString / 60), 0) & "(" & DRRec1("CntDN" & j).ToString & ")"
                        End If
                        RW1.Cells.Add(CellD)
                    Next
                    'CL4.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL4)
                    For j = 1 To Incr
                        Dim CellC As New TableCell
                        'CellC.BorderColor = Drawing.Color.DimGray
                        If IsDBNull(DRRec1("CHMins" & j)) Then
                            CellC.Text = 0
                        Else
                            CellC.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlA(j - 1) & " '  Target=_Blank>" & FormatNumber((DRRec1("CHMins" & j).ToString / 60), 0) & "(" & DRRec1("CntCH" & j).ToString & ")" & "</a>"
                        End If
                        RW1.Cells.Add(CellC)
                    Next
                    'CL1.BorderColor = Drawing.Color.DimGray
                    'CL2.BorderColor = Drawing.Color.DimGray
                    'CL3.BorderColor = Drawing.Color.DimGray
                    'CL4.BorderColor = Drawing.Color.DimGray
                    'CL5.BorderColor = Drawing.Color.DimGray
                    'CL6.BorderColor = Drawing.Color.DimGray
                    'CL7.BorderColor = Drawing.Color.DimGray
                    'CL7.BorderColor = Drawing.Color.DimGray
                    RW1.Cells.Add(CL5)
                    RW1.Cells.Add(CL6)
                    RW1.Cells.Add(CL7)
                    RW1.BorderStyle = BorderStyle.Solid
                    Table2.Rows.Add(RW1)
                    'Exit Sub
                End While
            End If
            DRRec1.Close()
        Finally
            If CMUser.Connection.State = ConnectionState.Open Then
                CMUser.Connection.Close()
            End If
        End Try
        'Response.Write(strQuery)
    End Sub

    
    
    Protected Function chkLevel(ByVal AdminLevel As Long, ByVal Level As Long) As Boolean
        If (AdminLevel And Level) = Level Then
            chkLevel = True
        Else
            chkLevel = False
        End If
    End Function

   
    Protected Sub DLLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLLevel.SelectedIndexChanged
        ViewStatus(DLLevel.SelectedValue)
    End Sub
End Class

