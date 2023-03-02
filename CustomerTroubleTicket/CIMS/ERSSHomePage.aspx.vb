Imports MainModule
Partial Class ERSSHomePage
    Inherits System.Web.UI.Page
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        objMainModule.oConn.Open()
        Session("UserID") = "cbc338d9-7cf8-4130-bf0f-cfbc7491474c"
        If Not Page.IsPostBack Then
            Dim varStrDeptName As String
            Dim varStrDesignation As String
            Dim objGetDeptName As New Data.SqlClient.SqlCommand("SELECT D.Name,DS.Name FROM dbo.tblUsers U INNER JOIN dbo.tblDepartments D ON U.DepartmentID=D.DepartmentID INNER JOIN dbo.tblDeptDesignations DS ON U.DesignationID=DS.DesignationID  WHERE USerID='" & Session("UserID") & "'", objMainModule.oConn)
            Dim objRecGetDeptName As Data.SqlClient.SqlDataReader = objGetDeptName.ExecuteReader
            If objRecGetDeptName.HasRows Then
                While objRecGetDeptName.Read
                    varStrDeptName = objRecGetDeptName.GetString(objRecGetDeptName.GetOrdinal("Name"))
                    varStrDesignation = objRecGetDeptName.GetString(1)
                End While
            End If
            objRecGetDeptName.Close()
            objRecGetDeptName = Nothing
            objGetDeptName = Nothing

            If Trim(UCase(varStrDeptName)) = Trim(UCase("Production")) And Trim(UCase(varStrDesignation)) = Trim(UCase("HBA")) Then
                Dim varTblRowRaiseTicket As New TableRow
                Dim varTblCellRaiseTicket As New TableCell

                Dim varTblRowTicketHistory As New TableRow
                Dim varTblCellTicketHistory As New TableCell


                varTblCellRaiseTicket.Text = "<a href=""ERSS/NewTicket.aspx"">New Ticket</a>"
                varTblRowRaiseTicket.Cells.Add(varTblCellRaiseTicket)
                tblMain.Rows.Add(varTblRowRaiseTicket)

                varTblCellTicketHistory.Text = "<a href=""PastTickets.aspx"">Ticket History</a>"
                varTblRowTicketHistory.Cells.Add(varTblCellTicketHistory)
                tblMain.Rows.Add(varTblRowTicketHistory)

            End If
        End If
    End Sub
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        objMainModule.oConn.Close()
        objMainModule.oConn = Nothing
    End Sub
End Class
