Imports System.Data.SqlClient

Partial Class RoutingTool_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '        System.Threading.Thread.Sleep(100)
        lbltime.Text = SysTime
   
        If Not IsPostBack Then
            Panel1.Visible = False
            ViewLevels()
        End If




    End Sub
    Protected Sub ViewLevels()
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        strQuery = "Select Levelname, LevelNo from tblProductionLevels where JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
        Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
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

    End Sub
    Sub ViewStatus()
        Panel1.Visible = True

        'Response.Write(ProcTime)
        'Response.Write(ServStartDate)
        'Response.Write(ProcStartDate)
        ''Response.Write(ServEndDate)
      
        Dim strConn As String
        Dim strQuery As String
        Dim scdMins As Integer
        Dim MinsDone As Integer
        Dim MinsAssn As Integer
        Dim MinsPend As Integer
        Dim DirMins As Integer
        Dim LvlNo As Integer
        Dim COLvlNo As Integer
        Dim LvlName As String
        Dim LevelNo As Integer
        Dim LvlNOAssgn As String
        Dim LvlAssn As String
        Dim LvlA(0) As String
        Dim i As Integer
        Dim j As Integer
        i = 0

        LvlAssn = ""
        LvlName = ""
        LvlNOAssgn = ""
        LvlNo = DLLevel.SelectedValue
        COLvlNo = 100 + LvlNo
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim Userid As String
        Dim LvlCount As Integer
        Dim Lvl(0) As String

      

        strQuery = "Select Levelname, LevelNo from tblProductionLevels where JobsRouting = 'True' and LevelNo not in ('1073741824', '2147483647') "
        Dim CMUsr As New SqlCommand(strQuery, New SqlConnection(strConn))
        CMUsr.Connection.Open()
        Dim DRRec As SqlDataReader = CMUsr.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)
                'Response.Write(DRRec("LevelNo"))



                LevelNo = DRRec("LevelNo") + 100
                If i = 0 Then

                    LDone.Text = DRRec("LevelName").ToString & "</td>"
                    LOut.Text = DRRec("LevelName").ToString & "</td>"
                    LvlAssn = LevelNo

                    LvlNOAssgn = DRRec("LevelNo")

                Else

                    ReDim Preserve Lvl(i)
                    ReDim Preserve LvlA(i)
                    LDone.Text = LDone.Text & "<td>" & DRRec("LevelName").ToString & "</td>"
                    LOut.Text = LOut.Text & "<td>" & DRRec("LevelName").ToString & "</td>"
                    LvlAssn = LvlAssn & "," & LevelNo
                    LvlNOAssgn = LvlNOAssgn & "," & DRRec("LevelNo")
                End If
                LvlA(i) = LevelNo
                Lvl(i) = DRRec("LevelNo")
                i = i + 1
            End While
        End If
        DRRec.Close()
        CMUsr.Connection.Close()
        LDone.Text = LDone.Text & "<td>Total"
        LOut.Text = LOut.Text & "<td>Total"
 
        CellMdone.ColumnSpan = i + 1
        CellCout.ColumnSpan = i + 1

        strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.SchMins, L.RecFound from (Select * from tblUsers where (Isdeleted is NULL) or (Isdeleted = 'False')) U, tblUsersLevels UL, tblUsersSchMins S, (Select 'Yes' as RecFound, userID, startdate, enddate from tblLeave) L where   dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'  and L.startDate <= '" & ProcStartDate & "' and L.endDate >= '" & ProcStartDate & "' and S.SchDate = '" & ProcStartDate & "' and  UL.UserID = U.USerID and U.UserID*=S.USerID  and U.UserID*=L.USerID order by RecFound, uname"
        Response.Write(strQuery)

        Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
        CMUser.Connection.Open()
        Dim DRRec1 As SqlDataReader = CMUser.ExecuteReader()
        If DRRec1.HasRows Then
            While (DRRec1.Read)
                Dim CL1 As New TableCell
                Dim CL2 As New TableCell
                Dim CL3 As New TableCell
                Dim CL4 As New TableCell
                Dim RW1 As New TableRow
                Dim CL6 As New TableCell
                Dim CL7 As New TableCell
                Userid = DRRec1("UserID").ToString


                If DRRec1("RecFound").ToString <> "" Then
                    RW1.BackColor = Drawing.Color.LightGray
                    RW1.ForeColor = Drawing.Color.White
                    CL1.Text = DRRec1("uname")
                Else
                    CL1.Text = "<a href='UserRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlNo & " '  Target=_Blank>" & DRRec1("uname") & "</a>"
                End If


                CL2.Text = DRRec1("username")
                If IsDBNull(DRRec1("SchMins")) Then
                    scdMins = 0
                    CL3.Text = 0
                Else
                    scdMins = DRRec1("SchMins").ToString
                    CL3.Text = DRRec1("SchMins").ToString
                End If


                strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where M.Submitdate >= '" & ServStartDate & "' and M.Submitdate >= '" & ServEndDate & "' and  L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "'  and L.Userlevel in (" & LvlNOAssgn & ")  and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & LvlNOAssgn & ")  )) as DT"
                Dim CMUser1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                CMUser1.Connection.Open()
                Dim DRRec2 As SqlDataReader = CMUser1.ExecuteReader()
                If DRRec2.HasRows Then
                    While (DRRec2.Read)
                        If IsDBNull(DRRec2("Mins")) Then
                            MinsDone = 0
                            CL4.Text = 0
                        Else
                            MinsDone = FormatNumber((DRRec2("Mins").ToString / 60), 0)
                            CL4.Text = FormatNumber((DRRec2("Mins").ToString / 60), 0)
                        End If


                        'Response.Write(DRRec2("Mins").ToString)
                    End While
                End If
                DRRec2.Close()
                CMUser1.Connection.Close()





                strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration) as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "' and M.status = L.status and M.status in (" & LvlAssn & ") ) as DT"
                'Response.Write(strQuery)
                Dim CL5 As New TableCell
                Dim CMUser2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                CMUser2.Connection.Open()
                Dim DRRec3 As SqlDataReader = CMUser2.ExecuteReader()
                If DRRec3.HasRows Then
                    While (DRRec3.Read)

                        If IsDBNull(DRRec3("Mins")) Then
                            MinsAssn = 0
                            CL5.Text = 0
                        Else
                            MinsAssn = FormatNumber((DRRec3("Mins").ToString / 60), 0)
                            CL5.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlAssn & " '  Target=_Blank>" & FormatNumber((DRRec3("Mins").ToString / 60), 0) & "</a>"
                        End If


                        'Response.Write(DRRec2("Mins").ToString)
                    End While
                End If
                DRRec3.Close()
                CMUser2.Connection.Close()

                If scdMins > 0 Then
                    MinsPend = scdMins - (MinsDone + MinsAssn)
                    If MinsPend < 0 Then
                        MinsPend = 0
                    End If
                Else
                    MinsPend = 0
                End If

                strQuery = "Select sum(Mins) as Mins from (select distinct M.TranscriptionID, datediff(s,0,M.duration)  as Mins from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and L.userid='" & Userid & "' and Direct='True' and M.status=L.LevelNo and M.status=" & LvlNo & " and submitdate >= '1/9/2008' and submitdate < '1/10/2008' ) as DT"

                'Response.Write(strQuery)
                Dim CMUser3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                CMUser3.Connection.Open()
                Dim DRRec4 As SqlDataReader = CMUser3.ExecuteReader()
                If DRRec4.HasRows Then
                    While (DRRec4.Read)

                        If IsDBNull(DRRec4("Mins")) Then
                            DirMins = 0
                            CL7.Text = 0
                        Else
                            DirMins = FormatNumber((DRRec4("Mins").ToString / 60), 0)
                            CL7.Text = FormatNumber((DRRec4("Mins").ToString / 60), 0)
                        End If

                    End While
                End If

                DRRec4.Close()
                CMUser3.Connection.Close()


                CL7.Text = DirMins
                CL6.Text = MinsPend


                RW1.Cells.Add(CL1)
                RW1.Cells.Add(CL2)
                CL3.BorderColor = Drawing.Color.DimGray
                RW1.Cells.Add(CL3)
                For j = 0 To i - 1
                    strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where M.Submitdate >= '" & ServStartDate & "' and M.Submitdate >= '" & ServEndDate & "' and  L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "'  and L.Userlevel in (" & Lvl(j) & ")  and L.status in (Select LevelNo from tblProductionlevels where levelno not in (" & Lvl(j) & ")  )) as DT"
                    Dim CellD As New TableCell
                    Dim CMUserD As New SqlCommand(strQuery, New SqlConnection(strConn))
                    CMUserD.Connection.Open()
                    Dim DRRecD As SqlDataReader = CMUserD.ExecuteReader()
                    If DRRecD.HasRows Then
                        While (DRRecD.Read)
                            If IsDBNull(DRRecD("Mins")) Then
                                CellD.Text = 0
                            Else
                                CellD.Text = FormatNumber((DRRecD("Mins").ToString / 60), 0)
                            End If

                            'Response.Write(DRRec2("Mins").ToString)
                        End While
                    Else
                        CellD.Text = 0
                    End If
                    RW1.Cells.Add(CellD)
                    DRRecD.Close()
                    CMUserD.Connection.Close()
                Next
                CL4.BorderColor = Drawing.Color.DimGray
                RW1.Cells.Add(CL4)
                For j = 0 To i - 1
                    Dim CellC As New TableCell
                    strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration) as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "' and M.status = L.status and M.status in (" & LvlA(j) & ") ) as DT"
                    Dim CMUserC As New SqlCommand(strQuery, New SqlConnection(strConn))
                    CMUserC.Connection.Open()
                    Dim DRRecC As SqlDataReader = CMUserC.ExecuteReader()
                    If DRRecC.HasRows Then
                        While (DRRecC.Read)

                            If IsDBNull(DRRecC("Mins")) Then

                                CellC.Text = 0
                            Else
                                CellC.Text = "<a href='JobsRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlA(j) & " '  Target=_Blank>" & FormatNumber((DRRecC("Mins").ToString / 60), 0) & "</a>"
                            End If


                            'Response.Write(DRRec2("Mins").ToString)
                        End While
                    Else
                        CellC.Text = 0
                    End If
                    RW1.Cells.Add(CellC)

                    DRRecC.Close()
                    CMUserC.Connection.Close()
                Next
                CL5.BorderColor = Drawing.Color.DimGray
                CL6.BorderColor = Drawing.Color.DimGray
                'CL7.BorderColor = Drawing.Color.DimGray
                RW1.Cells.Add(CL5)
                RW1.Cells.Add(CL6)
                RW1.Cells.Add(CL7)


                Table2.Rows.Add(RW1)

            End While
        End If
    End Sub

    'Sub QAAssignUser()
    '    Dim strConn As String
    '    Dim strQuery As String
    '    Dim scdMins As Integer
    '    Dim MinsDone As Integer
    '    Dim MinsAssn As Integer
    '    Dim MinsPend As Integer
    '    Dim DirMins As Integer
    '    Dim LvlNo As Integer
    '    Dim COLvlNo As Integer
    '    LvlNo = 4
    '    COLvlNo = 100 + LvlNo
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim Userid As String

    '    'strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.SchMins from tblUsers U, tblAccountUserAssgn A, tblUsersLevels UL, tblUsersSchMins S where dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'  and   UL.UserID = U.USerID and A.UserID=U.USerID  and U.UserID*=S.USerID and S.schDate = '1/9/2008' "
    '    strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.SchMins from tblUsers U, tblUsersLevels UL, tblUsersSchMins S where dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'  and   UL.UserID = U.USerID  and U.UserID*=S.USerID and S.schDate = '1/9/2008' "
    '    Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    CMUser.Connection.Open()
    '    Dim DRRec1 As SqlDataReader = CMUser.ExecuteReader()
    '    If DRRec1.HasRows Then
    '        While (DRRec1.Read)
    '            Dim CL1 As New TableCell
    '            Dim CL2 As New TableCell
    '            Dim CL3 As New TableCell
    '            Dim CL4 As New TableCell
    '            Dim RW1 As New TableRow
    '            Dim CL6 As New TableCell
    '            Dim CL7 As New TableCell

    '            Userid = DRRec1("UserID").ToString
    '            CL1.Text = "<a href='UserRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlNo & " '  Target=_Blank>" & DRRec1("uname") & "</a>"
    '            CL2.Text = DRRec1("username")
    '            If IsDBNull(DRRec1("SchMins")) Then
    '                scdMins = 0
    '                CL3.Text = 0
    '            Else
    '                scdMins = DRRec1("SchMins").ToString
    '                CL3.Text = DRRec1("SchMins").ToString
    '            End If


    '            strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)/60  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "'  and L.Userlevel = " & LvlNo & "  and L.status in (Select LevelNo from tblProductionlevels where levelno <> " & LvlNo & "  )) as DT"
    '            Dim CMUser1 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '            CMUser1.Connection.Open()
    '            Dim DRRec2 As SqlDataReader = CMUser1.ExecuteReader()
    '            If DRRec2.HasRows Then
    '                While (DRRec2.Read)
    '                    If IsDBNull(DRRec2("Mins")) Then
    '                        MinsDone = 0
    '                        CL4.Text = 0
    '                    Else
    '                        MinsDone = DRRec2("Mins").ToString
    '                        CL4.Text = DRRec2("Mins").ToString
    '                    End If


    '                    'Response.Write(DRRec2("Mins").ToString)
    '                End While
    '            End If
    '            DRRec2.Close()
    '            CMUser1.Connection.Close()





    '            strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)/60  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "' and L.status = " & COLvlNo & " ) as DT"
    '            'Response.Write(strQuery)
    '            Dim CL5 As New TableCell
    '            Dim CMUser2 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '            CMUser2.Connection.Open()
    '            Dim DRRec3 As SqlDataReader = CMUser2.ExecuteReader()
    '            If DRRec3.HasRows Then
    '                While (DRRec3.Read)

    '                    If IsDBNull(DRRec3("Mins")) Then
    '                        MinsAssn = 0
    '                        CL5.Text = 0
    '                    Else
    '                        MinsAssn = DRRec3("Mins").ToString
    '                        CL5.Text = DRRec3("Mins").ToString
    '                    End If
    '                    'Response.Write(DRRec2("Mins").ToString)
    '                End While
    '            End If
    '            DRRec3.Close()
    '            CMUser2.Connection.Close()

    '            If scdMins > 0 Then
    '                MinsPend = scdMins - (MinsDone + MinsAssn)
    '                If MinsPend < 0 Then
    '                    MinsPend = 0
    '                End If
    '            Else
    '                MinsPend = 0
    '            End If

    '            strQuery = "Select sum(Mins) as Mins from (select distinct M.TranscriptionID, datediff(s,0,M.duration)/60  as Mins from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and L.userid='" & Userid & "' and Direct='True' and M.status=L.LevelNo and M.status=" & LvlNo & " and submitdate >= '1/9/2008' and submitdate < '1/10/2008' ) as DT"

    '            'Response.Write(strQuery)
    '            Dim CMUser3 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '            CMUser3.Connection.Open()
    '            Dim DRRec4 As SqlDataReader = CMUser3.ExecuteReader()
    '            If DRRec4.HasRows Then
    '                While (DRRec4.Read)

    '                    If IsDBNull(DRRec4("Mins")) Then
    '                        DirMins = 0
    '                        CL7.Text = 0
    '                    Else
    '                        DirMins = DRRec4("Mins").ToString
    '                        CL7.Text = DRRec4("Mins").ToString
    '                    End If

    '                End While
    '            End If

    '            DRRec4.Close()
    '            CMUser3.Connection.Close()


    '            CL7.Text = DirMins
    '            CL6.Text = MinsPend


    '            RW1.Cells.Add(CL1)
    '            RW1.Cells.Add(CL2)
    '            RW1.Cells.Add(CL3)
    '            RW1.Cells.Add(CL4)
    '            RW1.Cells.Add(CL5)
    '            RW1.Cells.Add(CL6)
    '            RW1.Cells.Add(CL7)


    '            Table4.Rows.Add(RW1)

    '        End While
    '    End If
    'End Sub

    'Sub BBAssignUser()
    '    Dim strConn As String
    '    Dim strQuery As String
    '    Dim scdMins As Integer
    '    Dim MinsDone As Integer
    '    Dim MinsAssn As Integer
    '    Dim MinsPend As Integer
    '    Dim DirMins As Integer
    '    Dim LvlNo As Integer
    '    Dim COLvlNo As Integer
    '    LvlNo = 8
    '    COLvlNo = 100 + LvlNo
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim Userid As String

    '    strQuery = "Select distinct U.FirstName +' ' + U.LastName as uname, U.Username, U.UserID, S.SchMins from tblUsers U, tblUsersLevels UL, tblUsersSchMins S where dbo.chkLevel(UL.ProductionLevel, " & LvlNo & ")='True'  and   UL.UserID = U.USerID and  U.UserID*=S.USerID and S.schDate = '1/9/2008' "
    '    Dim CMUser As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    CMUser.Connection.Open()
    '    Dim DRRec1 As SqlDataReader = CMUser.ExecuteReader()
    '    If DRRec1.HasRows Then
    '        While (DRRec1.Read)
    '            Dim CL1 As New TableCell
    '            Dim CL2 As New TableCell
    '            Dim CL3 As New TableCell
    '            Dim CL4 As New TableCell
    '            Dim RW1 As New TableRow
    '            Dim CL6 As New TableCell
    '            Dim CL7 As New TableCell

    '            Userid = DRRec1("UserID").ToString
    '            CL1.Text = "<a href='UserRouting.aspx?Userid=" & DRRec1("Userid").ToString & "&ProLevel=" & LvlNo & " '  Target=_Blank>" & DRRec1("uname") & "</a>"
    '            CL2.Text = DRRec1("username")
    '            If IsDBNull(DRRec1("SchMins")) Then
    '                scdMins = 0
    '                CL3.Text = 0
    '            Else
    '                scdMins = DRRec1("SchMins").ToString
    '                CL3.Text = DRRec1("SchMins").ToString
    '            End If


    '            strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)/60  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "'  and L.Userlevel = " & LvlNo & "  and L.status in (Select LevelNo from tblProductionlevels where levelno <> " & LvlNo & "  )) as DT"
    '            Dim CMUser1 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '            CMUser1.Connection.Open()
    '            Dim DRRec2 As SqlDataReader = CMUser1.ExecuteReader()
    '            If DRRec2.HasRows Then
    '                While (DRRec2.Read)
    '                    If IsDBNull(DRRec2("Mins")) Then
    '                        MinsDone = 0
    '                        CL4.Text = 0
    '                    Else
    '                        MinsDone = DRRec2("Mins").ToString
    '                        CL4.Text = DRRec2("Mins").ToString
    '                    End If


    '                    'Response.Write(DRRec2("Mins").ToString)
    '                End While
    '            End If
    '            DRRec2.Close()
    '            CMUser1.Connection.Close()





    '            strQuery = "Select sum(Mins) as Mins from (select distinct L.TranscriptionID, datediff(s,0,M.duration)/60  as Mins from tbltranscriptionlog L, tbltranscriptionMain M where L.TranscriptionID= M.TranscriptionID and L.userid='" & Userid & "' and L.status = " & COLvlNo & " ) as DT"
    '            'Response.Write(strQuery)
    '            Dim CL5 As New TableCell
    '            Dim CMUser2 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '            CMUser2.Connection.Open()
    '            Dim DRRec3 As SqlDataReader = CMUser2.ExecuteReader()
    '            If DRRec3.HasRows Then
    '                While (DRRec3.Read)

    '                    If IsDBNull(DRRec3("Mins")) Then
    '                        MinsAssn = 0
    '                        CL5.Text = 0
    '                    Else
    '                        MinsAssn = DRRec3("Mins").ToString
    '                        CL5.Text = DRRec3("Mins").ToString
    '                    End If
    '                    'Response.Write(DRRec2("Mins").ToString)
    '                End While
    '            End If
    '            DRRec3.Close()
    '            CMUser2.Connection.Close()

    '            If scdMins > 0 Then
    '                MinsPend = scdMins - (MinsDone + MinsAssn)
    '                If MinsPend < 0 Then
    '                    MinsPend = 0
    '                End If
    '            Else
    '                MinsPend = 0
    '            End If

    '            strQuery = "Select sum(Mins) as Mins from (select distinct M.TranscriptionID, datediff(s,0,M.duration)/60  as Mins from tblUserPrLvlMgmt L, tbltranscriptionMain M where L.PhysicianID= M.DictatorID and L.userid='" & Userid & "' and Direct='True' and M.status=L.LevelNo and M.status=" & LvlNo & " and submitdate >= '1/9/2008' and submitdate < '1/10/2008' ) as DT"

    '            'Response.Write(strQuery)
    '            Dim CMUser3 As New SqlCommand(strQuery, New SqlConnection(strConn))
    '            CMUser3.Connection.Open()
    '            Dim DRRec4 As SqlDataReader = CMUser3.ExecuteReader()
    '            If DRRec4.HasRows Then
    '                While (DRRec4.Read)

    '                    If IsDBNull(DRRec4("Mins")) Then
    '                        DirMins = 0
    '                        CL7.Text = 0
    '                    Else
    '                        DirMins = DRRec4("Mins").ToString
    '                        CL7.Text = DRRec4("Mins").ToString
    '                    End If

    '                End While
    '            End If

    '            DRRec4.Close()
    '            CMUser3.Connection.Close()


    '            CL7.Text = DirMins
    '            CL6.Text = MinsPend


    '            RW1.Cells.Add(CL1)
    '            RW1.Cells.Add(CL2)
    '            RW1.Cells.Add(CL3)
    '            RW1.Cells.Add(CL4)
    '            RW1.Cells.Add(CL5)
    '            RW1.Cells.Add(CL6)
    '            RW1.Cells.Add(CL7)


    '            Table6.Rows.Add(RW1)

    '        End While
    '    End If
    'End Sub
    
    Protected Function chkLevel(ByVal AdminLevel As Long, ByVal Level As Long) As Boolean
        If (AdminLevel And Level) = Level Then
            chkLevel = True
        Else
            chkLevel = False
        End If
    End Function

   
    Protected Sub DLLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLLevel.SelectedIndexChanged
        ViewStatus()
    End Sub
End Class

