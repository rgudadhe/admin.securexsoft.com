Imports System.Data
Imports System.Data.SqlClient
Imports MainModule
Partial Class TechReports_Shift
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim objConn As New Data.SqlClient.SqlConnection
            objConn = objMainModule.OpenConnection(objConn)

            Try
                Dim oCommand As New Data.SqlClient.SqlCommand("SELECT * FROM DBO.tblShift WHERE (IsDeleted IS NULL)", objConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
                If oRec.HasRows Then
                    While oRec.Read
                        Dim varRow As New TableRow
                        Dim varCellPrefix As New TableCell
                        Dim varCellShiftName As New TableCell
                        Dim varCellEdit As New TableCell

                        varRow.Font.Name = "Trebuchet MS"
                        varRow.Font.Size = 10
                        varCellPrefix.Text = oRec.GetString(oRec.GetOrdinal("ShiftPrefix"))
                        varCellShiftName.Text = oRec.GetString(oRec.GetOrdinal("ShiftName"))
                        varCellEdit.Text = "<a href="""" OnClick=""window.open('EditShift.aspx?ID=" & oRec.GetGuid(oRec.GetOrdinal("TrackID")).ToString & "','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >EDIT</a>"

                        varRow.Cells.Add(varCellPrefix)
                        varRow.Cells.Add(varCellShiftName)
                        varRow.Cells.Add(varCellEdit)
                        tblMain.Rows.Add(varRow)
                    End While
                End If
                oRec.Close()
                oRec = Nothing
                oCommand = Nothing
            Catch ex As Exception
            Finally
                If objConn.State <> ConnectionState.Closed Then
                    objConn.Close()
                    objConn = Nothing
                End If
            End Try
        End If
        Dim varRowAddState As New TableRow
        Dim varCellAddState As New TableCell
        varCellAddState.ColumnSpan = 3
        varRowAddState.Font.Size = 10
        varRowAddState.Font.Name = "Trebuchet MS"
        varCellAddState.HorizontalAlign = HorizontalAlign.Right
        varCellAddState.Text = "<a href="""" OnClick=""window.open('AddShift.aspx','', 'width=450,height=240,status=1,scrollbars=1');return false;"" >ADD</a>"
        varRowAddState.Cells.Add(varCellAddState)
        tblMain.Rows.Add(varRowAddState)
    End Sub
End Class
