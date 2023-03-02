Imports MainModule
Imports System.Data
Partial Class TicketHistory
    Inherits BasePage
    Dim objMainModule As New MainModule
    Dim varStrTicketID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varStrQuery As String
        Dim varStrActionType As String
        Dim clsERSS As ETS.BL.ERSS
        Dim DS As New DataSet
        Dim DS1 As New DataSet
        Dim objRecTicketDetails As DataTableReader
        Dim objRecActionDetails As DataTableReader

        If Not Page.IsPostBack Then
            Try
                clsERSS = New ETS.BL.ERSS()

                varStrTicketID = Trim(Request.QueryString("TID")).ToString
                varStrQuery = "SELECT U.FirstName,U.LastName,U.UserName FROM DBO.tblUsers U INNER JOIN DBO.tblTickets T ON U.UserID=T.UserID WHERE TicketID='" & varStrTicketID & "' AND U.ContractorID='" & Session("ContractorID").ToString & "' "
                DS = clsERSS.GetERSSTicketInfoByTicketID(varStrTicketID, Session("ContractorID").ToString)
                objRecTicketDetails = DS.Tables(0).CreateDataReader
                If objRecTicketDetails.HasRows Then
                    While objRecTicketDetails.Read
                        lblCustName.Text = objRecTicketDetails.GetString(objRecTicketDetails.GetOrdinal("FirstName")) & " " & objRecTicketDetails.GetString(objRecTicketDetails.GetOrdinal("LastName"))
                        lblUserName.Text = objRecTicketDetails.GetString(objRecTicketDetails.GetOrdinal("UserNAme"))
                        lblTicketNo.Text = objRecTicketDetails.GetValue(objRecTicketDetails.GetOrdinal("TicketNo")).ToString
                        lblIssueName.Text = objRecTicketDetails.GetString(objRecTicketDetails.GetOrdinal("IssueName")).ToString
                        lblDatePosted.Text = objRecTicketDetails.GetDateTime(objRecTicketDetails.GetOrdinal("DatePosted")).ToString
                        lblPriority.Text = objRecTicketDetails.GetString(objRecTicketDetails.GetOrdinal("Priority")).ToString
                        tblCellIssueDetails.Text = objRecTicketDetails.GetString(objRecTicketDetails.GetOrdinal("Description")).ToString
                    End While
                End If
                objRecTicketDetails.Close()


                DS1 = clsERSS.GetERSSTicketActionDetailsByTicketID(varStrTicketID, Session("ContractorID").ToString)
                objRecActionDetails = DS1.Tables(0).CreateDataReader
                If objRecActionDetails.HasRows Then
                    tblResponses.Visible = True
                    While objRecActionDetails.Read
                        Dim varTempStr As String
                        Dim varTempStrTable As String
                        Dim varTblRowResponse As New TableRow
                        Dim varTblRowDesc As New TableRow
                        Dim vartblResponseByCell As New TableCell
                        Dim vartblResponseTimeCell As New TableCell
                        Dim vartblResponseDescCell As New TableCell

                        If Trim(UCase(objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionType")))) = Trim(UCase("Modified Ticket")) Then
                            varTempStrTable = "<TABLE width=100%><TR><TD align=RIGHT width=20% valign=top style=border:0><div style=text-align:right>Description :</div></TD><TD style=border:0>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionDetails")).ToString & "</TD></TR><TR><TD align=RIGHT width=20% valign=top style=border:0><div style=text-align:right>Group Assigned :</div></TD><TD style=border:0>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("Name")) & "</TD></TR><TR><TD align=RIGHT width=20% valign=top style=border:0><div style=text-align:right>User Assigned :</div></TD><TD style=border:0>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("UserName")) & "</TD></TR></TABLE>"
                            varStrActionType = "Responded By : "
                        ElseIf Trim(UCase(objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionType")))) = Trim(UCase("Added Comments")) Then
                            varTempStrTable = "<TABLE width=100%><TR><TD align=RIGHT width=20% valign=top style=border:0><div style=text-align:right>Description :</div></TD><TD style=border:0>" & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("Comments")).ToString & "</TD></TR></TABLE>"
                            varStrActionType = "Comments Added : "
                        End If



                        varTempStr = objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("FirstName")) & " " & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("LastName")) & " on " & objRecActionDetails.GetDateTime(objRecActionDetails.GetOrdinal("ActionTime")) & ""
                        vartblResponseByCell.Text = "<div style=text-align:left><b>" & varStrActionType & varTempStr & "(EST)</b></div>"
                        vartblResponseByCell.ColumnSpan = 2
                        'vartblResponseDescCell.Text = "Description : " & objRecActionDetails.GetString(objRecActionDetails.GetOrdinal("ActionDetails")).ToString
                        vartblResponseDescCell.Text = varTempStrTable

                        varTblRowResponse.Font.Size = 8
                        varTblRowResponse.HorizontalAlign = HorizontalAlign.Left
                        varTblRowResponse.Cells.Add(vartblResponseByCell)
                        vartblResponseByCell.BackColor = Drawing.Color.LightYellow
                        tblResponses.Rows.Add(varTblRowResponse)

                        varTblRowDesc.Font.Size = 10
                        varTblRowDesc.HorizontalAlign = HorizontalAlign.Left
                        varTblRowDesc.Cells.Add(vartblResponseDescCell)
                        tblResponses.Rows.Add(varTblRowDesc)

                        varTempStrTable = ""
                        varStrActionType = ""
                    End While
                End If
                objRecActionDetails.Close()


            Catch ex As Exception
            Finally
                clsERSS = Nothing
                DS.Dispose()
                DS1.Dispose()
                objRecActionDetails = Nothing
                objRecTicketDetails = Nothing
            End Try
        End If
    End Sub
End Class
