Imports System.Data.Sqlclient
Imports System.Data
Partial Class MIS_DailyMins
    Inherits BasePage
    Protected Sub ShowActDetails()
        Dim strConn As String
      
        Dim t2 As New Table
        t2.Style("width") = "100%"
        't2.BorderWidth = 2
        't2.GridLines = GridLines.Both
        Dim I As Integer
        I = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim InvBillAmount As Double
        InvBillAmount = 0.0
        Dim strQuery As String
        Dim DLines As String
        Dim DSLines As String
        Dim BillUnits As String
        Dim BillSUnits As String
        Dim BillAmt As String
        Dim BillOtherAmt As String
        Dim BillTotAmount As String
        DLines = 0
        DSLines = 0
        BillUnits = 0
        BillSUnits = 0
        BillAmt = 0
        BillOtherAmt = 0
        BillTotAmount = 0

        Dim STSTDLines As String
        Dim STSTDSLines As String
        Dim STBillUnits As String
        Dim STBillSUnits As String
        Dim STBillAmt As String
        Dim STBillOtherAmt As String
        Dim STBillTotAmount As String
        Dim InvCode As String
        Dim minbilling As Double
        Dim RemBalance As Double
        RemBalance = "0.00"
        minbilling = "0.00"
        STSTDLines = 0
        STSTDSLines = 0
        STBillUnits = 0
        STBillSUnits = 0
        STBillAmt = 0
        STBillOtherAmt = 0
        STBillTotAmount = 0
        Dim TSTDLines As String
        Dim TSTDSLines As String
        Dim TBillUnits As String
        Dim TBillSUnits As String
        Dim TBillAmt As String
        Dim TBillOtherAmt As String
        Dim TBillTotAmount As String
        Dim InvoiceID As String

        TSTDLines = 0
        TSTDSLines = 0
        TBillUnits = 0
        TBillSUnits = 0
        TBillAmt = 0
        TBillOtherAmt = 0
        TBillTotAmount = 0
        strQuery = "Select A.AccountName, Sum(I.amount) as balance from AdminSecureweb.dbo.invupdata I, AdminETS.dbo.tblaccounts A where  A.contractorid = '" & Session("contractorid").ToString & "' and A.AccountID = I.AccID and (A.Isdeleted is null OR A.isdeleted = 'False') Group by A.AccountName"
        ' Response.Write(strQuery)
        '      Response.End()
        Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows Then
                While DRRec.Read

                    Dim ARow1 As New TableRow
                    Dim ACell1 As New TableCell
                    Dim ACell2 As New TableCell
                    Dim ACell3 As New TableCell
                    Dim ACell4 As New TableCell
                    Dim ACell5 As New TableCell
                    Dim ACell6 As New TableCell
                    Dim ACell7 As New TableCell
                    Dim ACell8 As New TableCell
                    Dim ACell9 As New TableCell
                    Dim ACell10 As New TableCell
                    Dim ACell11 As New TableCell
                    Dim ACell12 As New TableCell
                    Dim ACell13 As New TableCell
                    Dim ACell14 As New TableCell
                    Dim ACell15 As New TableCell
                    Dim ACell16 As New TableCell
                    Dim ACell17 As New TableCell
                    Dim ACell18 As New TableCell

                    'ARow1.CssClass = "tblbg2"
                    ARow1.HorizontalAlign = HorizontalAlign.Left
                    'ARow1.Font.Bold = True
                    ARow1.Font.Size = "8"
                    ACell1.HorizontalAlign = HorizontalAlign.Left
                    ACell2.HorizontalAlign = HorizontalAlign.Left

                    ACell1.Text = DRRec("AccountName").ToString
                    ACell2.Text = FormatNumber(DRRec("balance").ToString, 2)


                    ARow1.Cells.Add(ACell1)
                    ARow1.Cells.Add(ACell2)

                    tblMins.Rows.Add(ARow1)

                End While

            End If
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try

    End Sub

 

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ShowActDetails()
    End Sub
End Class
