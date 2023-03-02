Imports System.Data.SqlClient
Partial Class Excess_Minutes_Default
    Inherits BasePage
    Private Sub Excmins(ByVal SrvStDate As Date)
        Dim ProtocolMins As Integer
        Dim FreshMins As Integer
        Dim DoneMins As Integer
        Dim NotFinishedMins As Integer
        Dim PendingMins As Integer
        Dim ExcMins As Integer
        Dim SrvEnddate As Date
        SrvEnddate = SrvStDate.AddDays(1)

        Dim clsRo As ETS.BL.Routing
        Dim Ds As New Data.DataSet
        Try
            clsRo = New ETS.BL.Routing
            Ds = clsRo.RoutingToolExcessMinutesReport(Session("ContractorID"), Session("WorkgroupID"), SrvStDate, SrvEnddate)

            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    For Each DRRec As Data.DataRow In Ds.Tables(0).Rows
                        Dim c1 As New TableCell
                        Dim c2 As New TableCell
                        Dim c3 As New TableCell
                        Dim c4 As New TableCell
                        Dim c5 As New TableCell
                        Dim c6 As New TableCell
                        Dim c7 As New TableCell
                        Dim r As New TableRow

                        c1.Text = DRRec("Accountname").ToString
                        If IsDBNull(DRRec("ProtocolMins")) Then
                            ProtocolMins = 0
                        Else
                            ProtocolMins = DRRec("ProtocolMins").ToString
                        End If

                        If IsDBNull(DRRec("FreshMins")) Then
                            FreshMins = 0
                        Else

                            FreshMins = FormatNumber(DRRec("FreshMins").ToString / 60, 0)
                        End If

                        If IsDBNull(DRRec("NotFinished")) Then
                            NotFinishedMins = 0
                        Else
                            NotFinishedMins = FormatNumber(DRRec("NotFinished").ToString / 60, 0)
                        End If

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
                            c7.Text = "<a href='UpExcMinsNew.aspx?AccID=" & DRRec("AccountID").ToString & "&SrvStDate=" & SrvStDate & "' Target=_Blank>" & ExcMins & "</a>"
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
                    Next
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsRo = Nothing
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
