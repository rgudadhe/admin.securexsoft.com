Imports MainModule
Imports System.Data
Imports System.Web.Script.Services
Partial Class ChkProd
    Inherits BasePage
    Dim Query As String
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim varDtTodayDate As Date
            If objMainModule.CheckDayLightSavings(Now) = True Then
                varDtTodayDate = DateAdd(DateInterval.Hour, 9, Now)
                varDtTodayDate = DateAdd(DateInterval.Minute, 30, varDtTodayDate)
            Else
                varDtTodayDate = DateAdd(DateInterval.Hour, 10, Now)
                varDtTodayDate = DateAdd(DateInterval.Minute, 30, varDtTodayDate)
            End If

            Dim varTdateAdd As Date
            varTdateAdd = DateAdd(DateInterval.Day, -14, DateAdd(DateInterval.Day, -GetDayNo(WeekdayName(Weekday(varDtTodayDate))), varDtTodayDate))

            Dim varTdateAdd1 As Date
            varTdateAdd1 = DateAdd(DateInterval.Day, 20, DateAdd(DateInterval.Day, -GetDayNo(WeekdayName(Weekday(varDtTodayDate))), varDtTodayDate))

            While varTdateAdd1 >= varTdateAdd
                If Trim(UCase(WeekdayName(Weekday(varTdateAdd)))) = Trim(UCase("Monday")) Then
                    WS.Items.Add(varTdateAdd.ToShortDateString)
                ElseIf Trim(UCase(WeekdayName(Weekday(varTdateAdd)))) = Trim(UCase("Sunday")) Then
                    WE.Items.Add(varTdateAdd.ToShortDateString)
                End If
                varTdateAdd = DateAdd(DateInterval.Day, 1, varTdateAdd)
            End While
            DisplayRecord()
        End If
    End Sub
    Public Sub DisplayRecord()
        Dim d As DateTime
        Dim d1 As Date

        Dim varPrevUserName As String
        Dim varPrevWeekStart As String
        Dim varDtTempDate
        Dim varDtSchDate
        Dim varTempMins(6)
        Dim varTempSchDate(6)
        Dim varBolFlag As Boolean

        Dim i As Int16
        Dim j As Int16
        Dim m As Integer

        Dim DTSearchParam As New DataTable
        Dim DS As New Data.DataSet
        Dim clsERSS As ETS.BL.ERSS
        Dim Reader As DataTableReader
        Try
            'd = Convert.ToDateTime(Date.Now())
            'd1 = d.AddDays(-(Day(Now.Date)))
            d1 = WS.Items(WS.Items.Count - 3).Value.ToString

		
            clsERSS = New ETS.BL.ERSS()
            DTSearchParam = New DataTable
            DTSearchParam.Columns.Add(New DataColumn("ContractorID"))
            DTSearchParam.Columns.Add(New DataColumn("StartDate"))
            DTSearchParam.Columns.Add(New DataColumn("EndDate"))
            DTSearchParam.Columns.Add(New DataColumn("UserName"))
            DTSearchParam.Columns.Add(New DataColumn("WorkGroupID"))

            Dim DR As Data.DataRow = DTSearchParam.NewRow

            DR("ContractorID") = Session("ContractorID").ToString
            DR("StartDate") = Request.Form("WS").ToString
            DR("EndDate") = Request.Form("WE").ToString
            DR("UserName") = txtID.Text.ToString
            DR("WorkGroupID") = Session("WorkGroupID").ToString


            DTSearchParam.Rows.Add(DR)

            DS = clsERSS.GetProductionSceduleReport(DTSearchParam)

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    Reader = DS.Tables(0).CreateDataReader
                    If Reader.HasRows Then
                        m = 0
                        j = 0
                        varBolFlag = True
                        varPrevUserName = ""
                        varPrevWeekStart = ""
                        While Reader.Read
                            Dim tblrow As New TableRow

                            Dim cell1 As New TableCell
                            Dim cell2 As New TableCell
                            Dim cell3 As New TableCell
                            Dim cell4 As New TableCell
                            Dim cell5 As New TableCell
                            Dim cell6 As New TableCell
                            Dim cell7 As New TableCell
                            Dim cell8 As New TableCell
                            Dim cell9 As New TableCell
                            Dim cell10 As New TableCell
                            Dim cell11 As New TableCell
                            Dim cell12 As New TableCell
                            Dim cell13 As New TableCell
                            Dim cell14 As New TableCell
                            Dim cell15 As New TableCell
                            'Dim cell16 As New TableCell

                            cell1.Text = Reader.GetString(Reader.GetOrdinal("UserName"))
                            'cell16.Text = Reader.GetString(Reader.GetOrdinal("FirstName")) & " " & Reader.GetString(Reader.GetOrdinal("LastName"))

                            varTempMins(m) = Reader.GetDecimal(Reader.GetOrdinal("Mins"))
                            varTempSchDate(m) = Reader.GetDateTime(Reader.GetOrdinal("ScheduleDate"))

                            'cell2.Text = Reader.GetDecimal(2)
                            'cell3.Text = Reader.GetDecimal(3)
                            'cell4.Text = Reader.GetDecimal(4)
                            'cell5.Text = Reader.GetDecimal(5)
                            'cell6.Text = Reader.GetDecimal(6)
                            'cell7.Text = Reader.GetDecimal(7)
                            'cell8.Text = Reader.GetDecimal(8)

                            cell9.Text = Reader.GetString(Reader.GetOrdinal("startTime"))
                            cell10.Text = Reader.GetString(Reader.GetOrdinal("endTime"))
                            'cell11.Text = Reader.GetDateTime(Reader.GetOrdinal("WeekStart"))
                            'cell12.Text = Reader.GetDateTime(Reader.GetOrdinal("WeekEnd"))
                            If Not Reader.IsDBNull(Reader.GetOrdinal("Description")) Then
                                cell14.Text = Reader.GetString(Reader.GetOrdinal("Description"))
                            Else
                                cell14.Text = "&nbsp"
                            End If

                            cell15.Text = Reader.GetString(Reader.GetOrdinal("Platform"))

                            If Not Reader.IsDBNull(Reader.GetOrdinal("lastUpdate")) Then
                                varDtTempDate = Reader.GetDateTime(Reader.GetOrdinal("lastUpdate"))
                                If objMainModule.CheckDayLightSavings(varDtTempDate) = True Then
                                    varDtTempDate = DateAdd(DateInterval.Hour, 9, varDtTempDate)
                                    varDtTempDate = DateAdd(DateInterval.Minute, 30, varDtTempDate)
                                Else
                                    varDtTempDate = DateAdd(DateInterval.Hour, 10, varDtTempDate)
                                    varDtTempDate = DateAdd(DateInterval.Minute, 30, varDtTempDate)
                                End If

                                cell13.Text = varDtTempDate
                            Else
                                cell13.Text = "&nbsp"
                            End If


                            cell1.HorizontalAlign = HorizontalAlign.Center


                            cell2.HorizontalAlign = HorizontalAlign.Center


                            cell3.HorizontalAlign = HorizontalAlign.Center




                            cell4.HorizontalAlign = HorizontalAlign.Center



                            cell5.HorizontalAlign = HorizontalAlign.Center


                            cell6.HorizontalAlign = HorizontalAlign.Center


                            cell7.HorizontalAlign = HorizontalAlign.Center


                            cell8.HorizontalAlign = HorizontalAlign.Center


                            cell9.HorizontalAlign = HorizontalAlign.Center



                            cell10.HorizontalAlign = HorizontalAlign.Center



                            cell11.HorizontalAlign = HorizontalAlign.Center


                            cell12.HorizontalAlign = HorizontalAlign.Center


                            cell13.HorizontalAlign = HorizontalAlign.Center


                            cell14.HorizontalAlign = HorizontalAlign.Center


                            cell15.HorizontalAlign = HorizontalAlign.Center



                            m = m + 1



                            If m = 7 Then
                                tblrow.Cells.Add(cell1)
                                'tblrow.Cells.Add(cell16)
                                tblrow.Cells.Add(cell14)

                                cell2.Text = varTempMins(0)
                                tblrow.Cells.Add(cell2)
                                cell3.Text = varTempMins(1)
                                tblrow.Cells.Add(cell3)
                                cell4.Text = varTempMins(2)
                                tblrow.Cells.Add(cell4)
                                cell5.Text = varTempMins(3)
                                tblrow.Cells.Add(cell5)
                                cell6.Text = varTempMins(4)
                                tblrow.Cells.Add(cell6)
                                cell7.Text = varTempMins(5)
                                tblrow.Cells.Add(cell7)
                                cell8.Text = varTempMins(6)
                                tblrow.Cells.Add(cell8)

                                cell11.Text = varTempSchDate(0)
                                cell12.Text = varTempSchDate(6)

                                tblrow.Cells.Add(cell9)
                                tblrow.Cells.Add(cell10)
                                tblrow.Cells.Add(cell11)
                                tblrow.Cells.Add(cell12)
                                tblrow.Cells.Add(cell13)
                                tblrow.Cells.Add(cell15)

                                Table2.Rows.Add(tblrow)
                                m = 0


                            End If

                            If varBolFlag = True Then
                                varBolFlag = False
                            End If

                            j = j + 1


                            'varPrevUserName = Reader.GetString(Reader.GetOrdinal("UserName"))
                            'varPrevWeekStart = Reader.GetDateTime(Reader.GetOrdinal("WeekStart")).ToShortDateString
                        End While

                    End If
                End If
            End If

            
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsERSS = Nothing
            DS = Nothing
            Reader = Nothing
            DTSearchParam = Nothing
        End Try
    End Sub
    Protected Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        Dim sDate As Date
        Dim eDate As Date
        Dim tempS()
        Dim tempE()
        tempS = Split(Trim(WS.Text), "/")
        tempE = Split(Trim(WE.Text), "/")

        sDate = DateSerial(tempS(2), tempS(0), tempS(1))
        eDate = DateSerial(tempE(2), tempE(0), tempE(1))
        If DateDiff(DateInterval.Day, sDate, eDate) < 0 Then
            Response.Write("<script type=""text/javascript"" language=javascript> alert(""WeekStart should be greater than WeekEnd !!"");window.location.href='chkProd.aspx';</script>")
        End If
        DisplayRecord()
    End Sub
    'Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
    '    Response.Write("test")
    '    Try
    '        Response.Clear()
    '        ' Set the content type to Excel.
    '        Response.ContentType = "application/vnd.ms-excel"
    '        ' Remove the charset from the Content-Type header.
    '        Response.Charset = ""
    '        ' Turn off the view state.
    '        Me.EnableViewState = False
    '        DisplayRecord()
    '        Dim tw As New System.IO.StringWriter()
    '        Dim hw As New System.Web.UI.HtmlTextWriter(tw)

    '        ' Get the HTML for the control.
    '        Table2.RenderControl(hw)

    '        ' Write the HTML back to the browser.
    '        Response.Write(tw.ToString())
    '        ' End the response.
    '        Response.End()
    '    Catch ex As Exception
    '    End Try

    'End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        Exit Sub
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Response.Clear()
            ' Set the content type to Excel.
            Dim filename = "Schedule Report.xls"
            Response.AddHeader("content-disposition", "attachment;filename=" & filename)


            Response.ContentType = "application/vnd.ms-excel"

            ' Remove the charset from the Content-Type header.
            Response.Charset = ""
            ' Turn off the view state.
            Me.EnableViewState = False
            DisplayRecord()
            Dim tw As New System.IO.StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(tw)

            ' Get the HTML for the control.
            Table2.RenderControl(hw)

            ' Write the HTML back to the browser.
            Response.Write(tw.ToString())
            ' End the response.
            Response.End()
        Catch ex As Exception
        End Try
    End Sub
    Protected Function GetDayNo(ByVal Str As String) As Integer
        Select Case Str
            Case "Monday"
                Return 0
            Case "Tuesday"
                Return 1
            Case "Wednesday"
                Return 2
            Case "Thursday"
                Return 3
            Case "Friday"
                Return 4
            Case "Saturday"
                Return 5
            Case "Sunday"
                Return 6
        End Select
    End Function
End Class
