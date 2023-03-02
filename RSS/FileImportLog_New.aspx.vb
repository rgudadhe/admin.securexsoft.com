
Partial Class RSS_FileImportLog_New
    Inherits BasePage
    Protected Sub rptBindPhy()

        Dim clsFIL As ETS.BL.FileImportLog
        Dim DSFIL As New Data.DataSet
        Try
            clsFIL = New ETS.BL.FileImportLog
	    DSFIL = clsFIL.getFileImportLog(Session("ContractorID").ToString, Request("txtCJNum").ToString, Request("txtMD5").ToString, Request("txtClient").ToString, Request("ddlStatus").ToString, Request("sDate").ToString, Request("eDate").ToString)
	    'DSFIL.WriteXML("c:\test.xml")	
	    'response.write (DSFIL.Tables.Count)
            If DSFIL.Tables.Count > 0 Then
                If DSFIL.Tables(0).Rows.Count > 0 Then
                    'response.write (DSFIL.Tables(0).Rows.Count)	
                    dlist.DataSource = DSFIL
                    dlist.DataBind()

                    dlist.ShowFooter = True
                    dlist.UseAccessibleHeader = True
                    dlist.HeaderRow.TableSection = TableRowSection.TableHeader
                    dlist.FooterRow.TableSection = TableRowSection.TableFooter
                    btnReimport.Visible = True
                    dlistRow.Visible = True
                Else
                    dlist.DataSource = Nothing
                    dlist.DataBind()

                End If
            End If
        Catch ex As exception
            Response.Write("error:" & ex.Message)
        Finally
            clsFIL = Nothing
            DSFIL = Nothing
        End Try
    End Sub
    Public Function getStatus(ByVal blnStatus) As String
        If String.IsNullOrEmpty(blnStatus) Then
            getStatus = "Pending Re-Import"
            'Dim lnk As LinkButton = rptPhy.FindControl("LinkButton1")
            'lnk.Visible = False
        Else
            If blnStatus Then
                getStatus = "Imported"
            Else
                getStatus = "Failed"
            End If
        End If
    End Function
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        rptBindPhy()
    End Sub
    Protected Sub dlist_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles dlist.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkHis As ImageButton
            lnkHis = DirectCast(e.Row.FindControl("btnHistory"), ImageButton)
            Dim lblMD5 As Label
            lblMD5 = DirectCast(e.Row.FindControl("lblMD5Value"), Label)
            'Dim hdnRID As HiddenField
            'hdnRID = DirectCast(e.Row.FindControl("hdnRPID"), HiddenField)
            If Not lnkHis Is Nothing And Not lblMD5 Is Nothing Then
                lnkHis.Attributes.Add("onclick", "javascript:return openPopup('" & lblMD5.Text.ToString & "')")
            End If
        End If
    End Sub

    Protected Sub btnReimport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReimport.Click
        Dim varStr As String = String.Empty
        varStr = "<table border=0><tr><td class=""alt1""><b>FileName</b></td><td class=""alt1""><b>Status</b></td></tr>"

        For rowIndex As Integer = 0 To dlist.Rows.Count - 1 Step 1
            If dlist.Rows(rowIndex).RowType = DataControlRowType.DataRow Then
                Dim Chk As CheckBox = CType(dlist.Rows(rowIndex).FindControl("chkJob"), CheckBox)
                If Chk.Checked Then
                    Dim hdnID As HiddenField = Chk.FindControl("hdnID")
                    Dim varRecID As String = hdnID.Value

                    hdnID = Chk.FindControl("hdnFileName")
                    Dim varFileName = hdnID.Value

                    Dim lbl As Label = Chk.FindControl("lblMD5Value")
                    Dim varMD5 = lbl.Text
                    varStr = varStr & "<tr><td>" & varFileName & "</td><td>" & ReImport(varRecID, varMD5, varFileName.ToString) & "</td></tr>"
                    'varStr = varStr & "<tr><td>" & varFileName & "</td><td>Processed</td></tr>"
                End If
            End If
        Next

        If Not String.IsNullOrEmpty(varStr) Then
            varStr = varStr & "</table>"
        End If
        'Response.End()
        dlistRow.Visible = False
        lblMsgRow.Visible = True
        lblMsg.Text = varStr.ToString
        btnReimport.Visible = False
        Exit Sub
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblMsgRow.Visible = False
            dlistRow.Visible = False
            btnReimport.Visible = False
        End If
        lblMsg.Text = String.Empty
    End Sub
    Protected Function ReImport(ByVal varRecID As String, ByVal varMD5Value As String, ByVal varfilename As String) As String
        Dim clsFIL As ETS.BL.FileImportLog
        Try

            clsFIL = New ETS.BL.FileImportLog
            Return clsFIL.btnReImport_Click(varRecID.ToString, varMD5Value.ToString, Trim(varfilename), Server.MapPath("/ETS_Files"), Server.MapPath("/ETS_Files"), Session("UserID").ToString)
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsFIL = Nothing
        End Try
    End Function
End Class
