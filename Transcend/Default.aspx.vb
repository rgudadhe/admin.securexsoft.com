Imports System.Data.OleDb
Partial Class Transcend_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim myConnectionString As String
        Dim varBolFormat As Boolean = True
        Dim varCountO As Long
        Dim varCount As Long
        Dim varFileName As String

        varFileName = "d:\test.xls"
        Dim CNNEXCEL As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source=" + varFileName + "; Extended Properties=""EXCEL 8.0;HDR=YES;IMEX=1;ImportMixedTypes=Text""")
        Dim SQLCmd1 As New OleDbCommand("SELECT * FROM [Sheet1]", CNNEXCEL)
        SQLCmd1.Connection.Open()
        'Dim DRRec1 As OleDbDataReader = SQLCmd1.ExecuteReader()
        'If DRRec1.HasRows Then

        'End If
        'SQLCmd1.Connection.Close()
        'Dim myDataAdapter As New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", myConnectionString)
        'Dim myDataSet As New System.Data.DataSet()
        'myDataAdapter.Fill(myDataSet, "ExcelInfo")
        'myDataSet.WriteXml("\\win11617\d$\ets\test.xml")
    End Sub
End Class
