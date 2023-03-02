Namespace ets
    Partial Class Dictation_Search_ExportResult
        Inherits PageBase
        Private Function GridDataGet() As Boolean
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection

            Dim ds As New System.Data.DataSet
            Try

                oConn.ConnectionString = ConString
                oConn.Open()
                Dim oCommand As New Data.SqlClient.SqlCommand
                oCommand = New Data.SqlClient.SqlCommand("SP_DictationSearch", oConn)

                Dim oParam As New Data.SqlClient.SqlParameter("@WhereClause", Data.SqlDbType.VarChar, 500)
                oParam.Value = Request("WC").ToString
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@OrderByClause", Data.SqlDbType.VarChar, 500)
                oParam.Value = Request("OrderBy").ToString
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@IsUserCri", Data.SqlDbType.Bit, 1)
                oParam.Value = IIf(Request("IsUserCri").ToString = "1", 1, 0)
                oCommand.Parameters.Add(oParam)

                oCommand.CommandType = Data.CommandType.StoredProcedure

                Dim objDA As New System.Data.SqlClient.SqlDataAdapter
                objDA.SelectCommand = oCommand


                objDA.Fill(ds, "MyDataTable1")

            Catch ex As Exception
                Response.Write(ex.Message)
                Exit Function
            Finally
                If Not oConn Is Nothing Then oConn.Close()
            End Try
            dgResultsData.DataSource = ds
            dgResultsData.DataBind()
            ds.Dispose()
            GridDataGet = True
        End Function

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not GridDataGet() Then Exit Sub
            Dim mHeader As String = "Dictation Search"
            Dim mSubHead As String = "Printed by: " & Session("UserName") & "<br>Data as at: " & Now()
            DataGridToExcel(dgResultsData, Response, mHeader, mSubHead)
            Response.End()
        End Sub
        Public Shared Sub DataGridToExcel(ByVal dgExport As DataGrid, ByVal response As HttpResponse, Optional ByVal argHeader As String = "", Optional ByVal argSubHead As String = "")

            Dim stringWrite As New System.IO.StringWriter()                 'create a string writer
            Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)  'create an htmltextwriter which uses the stringwriter
            Dim dg As New DataGrid()
            response.Clear()                                                'clean up the response.object
            response.Charset = ""
            response.ContentType = "application/vnd.ms-excel"               'set the response mime type for excel
            dg = dgExport                                                   'set the input datagrid = to the new dg grid
            dg.GridLines = GridLines.None                                   'no gridlines
            dg.HeaderStyle.Font.Bold = True                                 'header text bold
            dg.HeaderStyle.ForeColor = System.Drawing.Color.Black           'change colors etc...
            dg.ItemStyle.ForeColor = System.Drawing.Color.Black
            dg.DataBind()                                                   'bind modified grid
            dg.RenderControl(htmlWrite)                                     'render datagrid to htmltextwriter
            response.Write("<h4>" & argHeader & "</h4>")                    'output the html with header and footer
            response.Write("<b>" & argSubHead & "</b>")
            response.Write(stringWrite.ToString())
            response.Write("-- end of report --")
            response.End()
        End Sub
    End Class
End Namespace