
Imports System.Data

Partial Class RoutingTool_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
     ViewStatus()
    End Sub
  
    Sub ViewStatus()
        Dim clsUsers As New ETS.BL.Users

        'clsUsers.ContractorID = Session("contractorID").ToString
        Dim DSUsers As DataSet = clsUsers.getUsersList(Session("ContractorId"), Session("WorkGroupId"), String.Empty)

        clsUsers = Nothing

        If DSUsers.Tables(0).Rows.Count > 0 Then
            For Each DRRec1 As DataRow In DSUsers.Tables(0).Rows
                If Not IsDBNull(DRRec1("LastLogin")) Then
                    Dim CL1 As New TableCell
                    Dim CL2 As New TableCell
                    Dim CL3 As New TableCell
                    Dim CL4 As New TableCell
                    Dim RW1 As New TableRow
                    Dim CL6 As New TableCell
                    Dim CL7 As New TableCell
                    CL1.Text = DRRec1("FirstName").ToString & " " & DRRec1("LastName").ToString
                    CL2.Text = DRRec1("username").ToString
                    CL3.Text = DRRec1("LastLogin").ToString
                    RW1.Cells.Add(CL1)
                    RW1.Cells.Add(CL2)
                    RW1.Cells.Add(CL3)
                    Table2.Rows.Add(RW1)
                End If
            Next
        End If
       
    End Sub

    
End Class

