
Partial Class FaxPlus_SummarySentFAX
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMessage.Text = String.Empty
        LnkExport.Visible = False
        If Not Page.IsPostBack Then
            LoadAcc()
        End If
    End Sub
    Public Function ReplaceSubject(ByVal Str As String) As String
        If String.IsNullOrEmpty(Str.ToString) Then
            Return String.Empty
        Else
            If Len(Str.ToString) > 0 Then
                If Str.ToString.Contains("vbCrLfNote") Then
                    If Str.ToString.IndexOf("vbCrLfNote") > 0 Then
                        Return Left(Str.ToString, Str.ToString.IndexOf("vbCrLfNote") - 1)
                    Else
                        Return Str.ToString
                    End If
                Else
                    Return Str.ToString
                End If
            End If
        End If
    End Function
    Public Function SetNote(ByVal str As String, ByVal RecID As String) As String
        If String.IsNullOrEmpty(str.ToString) Then
            Return String.Empty
        Else
            If Len(str.ToString) > 0 Then
                If InStr(Eval("Subject"), "vbCrLfNote") > 0 Then
                    If Len(Mid(str.ToString, InStr(str.ToString, "vbCrLfNote") + 11, Len(str.ToString))) > 25 Then
                        Return Left(Mid(str.ToString, InStr(str.ToString, "vbCrLfNote") + 11, Len(str.ToString)), 25) & "<a id=" & RecID.ToString & " name=" & RecID.ToString & " onmouseover=""this.style.cursor='hand'"" onclick=""javascript:return Chkmore('" & RecID.ToString & "');"" > <u>more..</u><a>"
                    Else
                        Return Mid(str.ToString, InStr(str.ToString, "vbCrLfNote") + 11, Len(str.ToString))
                    End If
                Else
                    Return str.ToString
                End If
            Else
                Return String.Empty
            End If
        End If
    End Function
    Public Function ChkFaxNo1(ByVal Str As String) As String
        If String.IsNullOrEmpty(Str) Then
            Return String.Empty
        Else
            If Len(Str) = 10 And Str <> "" Then
                'Return Format(Str, Left(Str, 3) & "-" & Mid(Str, 4, 3) & "-" & Right(Str, 4))
                Return Mid(Str, Len(Str) - 9, 3) & "-" & Mid(Str, Len(Str) - 6, 3) & "-" & Right(Str, 4)
            ElseIf Len(Str) > 10 And Str <> "" Then
                Return Left(Str, Len(Str) - 10) & "-" & Mid(Str, Len(Str) - 9, 3) & "-" & Mid(Str, Len(Str) - 6, 3) & "-" & Right(Str, 4)
            Else
                Return Str
            End If
        End If
    End Function
    Public Sub LoadAcc()
        Dim clsAcc As ETS.BL.Accounts
        Dim DS As New Data.DataSet
        Dim DV As New Data.DataView
        Try
            clsAcc = New ETS.BL.Accounts

            DS = clsAcc.getAccountList(Session("WorkGroupID").ToString, Session("ContractorID").ToString, String.Empty)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DV = New Data.DataView(DS.Tables(0), String.Empty, " AccountName ", Data.DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        ddlAccounts.DataSource = DV
                        ddlAccounts.DataTextField = "AccountName"
                        ddlAccounts.DataValueField = "AccountID"
                        ddlAccounts.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsAcc = Nothing
            DS = Nothing
        End Try
        ddlAccounts.Items.Insert(0, New ListItem("Any", String.Empty))
    End Sub
    Public Sub LoadData()
        Dim varAccID As String = String.Empty
        varAccID = ddlAccounts.SelectedValue.ToString

        Dim varsDate As String = String.Empty
        varsDate = txtStartDate.Text.ToString

        Dim vareDate As String = String.Empty
        vareDate = txtEndDate.Text.ToString

        Dim DS As New Data.DataSet
        Dim clsFAX As ETS.BL.FaxPlus

        Try
            clsFAX = New ETS.BL.FaxPlus

            DS = clsFAX.GetSummarySentFAXRecords(1, varAccID.ToString, varsDate.ToString, vareDate.ToString)

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    MyDataGrid.DataSource = DS.Tables(0)
                    MyDataGrid.DataBind()
                    LnkExport.Visible = True
                Else
                    MyDataGrid.DataSource = Nothing
                    MyDataGrid.DataBind()
                    lblMessage.Text = "No sent faxes for this period"
                    Exit Sub
                End If
            Else
                MyDataGrid.DataSource = Nothing
                MyDataGrid.DataBind()
                lblMessage.Text = "No sent faxes for this period"
                Exit Sub
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsFAX = Nothing
            DS = Nothing
        End Try

        If MyDataGrid.Rows.Count > 0 Then
            MyDataGrid.ShowFooter = True
            MyDataGrid.UseAccessibleHeader = True
            MyDataGrid.HeaderRow.TableSection = TableRowSection.TableHeader
            MyDataGrid.FooterRow.TableSection = TableRowSection.TableFooter

        End If
    End Sub
    Protected Sub LnkExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkExport.Click
        Response.Clear()
        Dim filename = "Sent Faxes " & Now & " .xls"
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        Me.EnableViewState = False
        Dim tw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        MyDataGrid.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '     server control at run time. 

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        LoadData()
    End Sub

   
End Class
