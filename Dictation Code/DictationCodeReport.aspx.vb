Imports System
Imports System.Data
Partial Class Dictation_Code_DictationCodeReport
    Inherits BasePage

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        GetResult()
    End Sub
    Protected Sub GetResult()
        Dim DSPhyDC As New DataSet
        Dim DV As New Data.DataView
        Dim clsPhy As ETS.BL.Physicians

        Try
            clsPhy = New ETS.BL.Physicians
            DSPhyDC = clsPhy.getPhyDCodewithActDetails(Session("contractorid").ToString, Session("WorkGroupID"), "", txtAccName.Text, txtFirst.Text, txtLast.Text)

            If DSPhyDC.Tables.Count > 0 Then
                If DSPhyDC.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DSPhyDC.Tables(0), " DictationCode LIKE '" & txtDictCode.Text.ToString & "%' ", String.Empty, DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        rptDetails.DataSource = DV
                        rptDetails.DataBind()


                        If rptDetails.Items.Count > 0 Then
                            Panel1.Visible = True
                            Panel2.Visible = False
                        Else
                            Panel2.Visible = True
                            Panel1.Visible = False
                            lblMsg.Text = "No Records found"
                            Exit Sub
                        End If
                    Else
                        Panel2.Visible = True
                        Panel1.Visible = False
                        lblMsg.Text = "No Records found"
                        Exit Sub
                    End If
                Else
                    Panel2.Visible = True
                    Panel1.Visible = False
                    lblMsg.Text = "No Records found"
                    Exit Sub
                End If
            Else
                Panel2.Visible = True
                Panel1.Visible = False
                lblMsg.Text = "No Records found"
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsPhy = Nothing
            DSPhyDC = Nothing
        End Try




        


        

    End Sub
    Protected Sub LnkExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkExport.Click
        Dim stringWrite As New System.IO.StringWriter()                 'create a string writer
        Dim htmlWrite As New System.Web.UI.HtmlTextWriter(stringWrite)  'create an htmltextwriter which uses the stringwriter
        Response.Clear()                                                'clean up the response.object
        Response.Charset = ""

        Dim filename = "Dictation Code Report " & Now & " .xls"
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        GetResult()
        Response.ContentType = "application/vnd.ms-excel"               'set the response mime type for excel
        rptDetails.RenderControl(htmlWrite)                                     'render datagrid to htmltextwriter
        Response.Write(stringWrite.ToString())
        'response.Write("-- end of report --")
        Response.End()
    End Sub
End Class
