Imports System.Data.SqlClient

Partial Class RoutingTool_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
     ViewStatus()
    End Sub
  
    Sub ViewStatus()

        Dim clsUsrs As ETS.BL.Users
        Dim Ds As New Data.DataSet
        Dim DRRec1 As Data.DataTableReader
        Try
            clsUsrs = New ETS.BL.Users
            Ds = clsUsrs.GetUsersPasswordChanged(Session("ContractorID"), Session("WorkGroupID"))

            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    DRRec1 = Ds.Tables(0).CreateDataReader
                    If DRRec1.HasRows Then
                        While (DRRec1.Read)
                            Dim CL1 As New TableCell
                            Dim CL2 As New TableCell
                            Dim CL3 As New TableCell
                            Dim CL4 As New TableCell
                            Dim RW1 As New TableRow
                            Dim CL6 As New TableCell
                            Dim CL7 As New TableCell
                            CL1.Text = DRRec1("uname").ToString
                            CL2.Text = DRRec1("username").ToString
                            CL3.Text = DRRec1("PassChanged").ToString
                            RW1.Cells.Add(CL1)
                            RW1.Cells.Add(CL2)
                            RW1.Cells.Add(CL3)
                            Table2.Rows.Add(RW1)

                        End While
                    End If
                    DRRec1.Close()

                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsrs = Nothing
            Ds = Nothing
        End Try
    End Sub
End Class

