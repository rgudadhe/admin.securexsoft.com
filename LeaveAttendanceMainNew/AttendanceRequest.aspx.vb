Imports System.Web.Mail
Imports System.Net.Mail.SmtpClient
Imports System.Net.Mail
Imports System.Net.NetworkCredential
Imports MainModule
Partial Class AttendanceRequest
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtInTime.Items.Clear()
            txtOutTime.Items.Clear()
            For i As Integer = 1 To 12
                Dim varLstItem As New ListItem
                varLstItem.Text = i
                varLstItem.Value = i
                txtInTime.Items.Add(varLstItem)
                txtOutTime.Items.Add(varLstItem)
            Next
            Dim varLstItemm As New ListItem
            varLstItemm.Text = "-- Select --"
            varLstItemm.Value = ""
            txtInTime.Items.Insert(0, varLstItemm)
            txtOutTime.Items.Insert(0, varLstItemm)
        End If
    End Sub
    Protected Sub SendRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SendRequest.Click
        Dim clsAttReq As ETS.BL.AttendanceRequest

        Try
            clsAttReq = New ETS.BL.AttendanceRequest()
            With clsAttReq
                .UserID = Session("UserID").ToString
                .ContractorID = Session("ContractorID").ToString
                .Status = "Pending"
                .SignIn = Request("txtInTime") & " " & Request("InTime")
                .SignOut = Request("txtOutTime") & " " & Request("OutTime")
                .Reason = Replace(Request("txtReason"), "'", "''")
                .AttDate = DateSerial(Year(Request("txtAttDate")), Month(Request("txtAttDate")), Day(Request("txtAttDate")))
            End With
            lblMsg.Text = clsAttReq.btn_SendAttendanceRequest()
        Catch ex As Exception
        Finally
            clsAttReq = Nothing
        End Try
    End Sub
    Function CheckTimeFormat(ByVal txtTime, ByVal txtControl)
        Dim varStrSpaceSplit
        Dim varStrDotSplit
        Dim varTimeFormat As String

        varStrSpaceSplit = Split(Trim(txtTime), " ")
        If UBound(varStrSpaceSplit) = 1 Then
            varTimeFormat = UCase(varStrSpaceSplit(1))
            varStrDotSplit = Split(Trim(varStrSpaceSplit(0)), ":")

            If Trim(UCase(varTimeFormat)) = Trim(UCase("AM")) Or Trim(UCase(varTimeFormat)) = Trim(UCase("PM")) Then

                If CInt(varStrDotSplit(0)) > 12 Then
                    If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");</script>")
                        Return False
                    End If
                    If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                        Return False
                    End If
                End If

                If UBound(varStrDotSplit) = 2 Then
                    If CInt(varStrDotSplit(1)) > 59 Then
                        If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");</script>")
                            Return False
                        End If
                        If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                            Return False
                        End If
                    End If

                    If CInt(varStrDotSplit(2)) > 59 Then
                        If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");</script>")
                            Return False
                        End If
                        If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                            Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                            Return False
                        End If
                    End If
                Else
                    If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");</script>")
                        Return False
                    End If
                    If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                        Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                        Return False
                    End If
                End If
            Else
                If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");</script>")
                    Return False
                End If
                If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                    Return False
                End If
            End If
        Else
            If Trim(UCase(txtControl)) = Trim(UCase("In-Time")) Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid In-Time"");txtInTime.focus();</script>")
                Return False
            End If
            If Trim(UCase(txtControl)) = Trim(UCase("Out-Time")) Then
                Response.Write("<script type=""text/javascript"" language=javascript> alert(""Please enter valid Out-Time"");</script>")
                Return False
            End If
        End If
    End Function
End Class
