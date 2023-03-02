Imports System
Imports System.Data
Partial Class FaxPlus_Popup
    Inherits BasePage
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Page.RegisterStartupScript("refresh", "<script language=javascript>var url = window.opener.location.href; window.opener.location.href = url+'?Result=1';</script>")
        Page.RegisterStartupScript("close", "<script language=javascript>window.close();</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("SortOrder") = " ASC"
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            DoBind("Physician" & ViewState("SortOrder").ToString)

        Catch ex As Exception
            txtF.Text = ex.Message
        End Try
    End Sub
    Private Function DoBind(ByVal SortBy As String)
        If String.IsNullOrEmpty(txtF.Text) = True And String.IsNullOrEmpty(txtL.Text) = True Then
            Exit Function
        End If
        Dim clsPhy As New ETS.BL.Physicians
        clsPhy.getPhysicianDetails(Request.QueryString("TransID").ToString)
        Dim AccID As String = clsPhy.AccountID
        clsPhy = Nothing
        Dim clsRP As New ETS.BL.RefPhysician
        With clsRP
            .AccID = AccID
            If String.IsNullOrEmpty(txtF.Text) = False And String.IsNullOrEmpty(txtL.Text) = True Then
                ._WhereString.Append(" AND PhyName like '" & txtF.Text & "'")
            End If
            If String.IsNullOrEmpty(txtF.Text) = True And String.IsNullOrEmpty(txtL.Text) = False Then
                ._WhereString.Append(" AND PhylName like '" & txtL.Text & "'")
            End If
            If String.IsNullOrEmpty(txtF.Text) = False And String.IsNullOrEmpty(txtL.Text) = False Then
                ._WhereString.Append(" AND PhyName like '" & txtF.Text & "' AND PhylName like '" & txtL.Text & "'")
            End If
        End With
        Dim DSRPList As DataSet = clsRP.getRPList()
        DSRPList.Tables(0).Columns.Add(New DataColumn("Physician", GetType(System.String), "ISNULL(PhyName,'')+' '+ISNULL(PhymName,'')+' '+ISNULL(PhylName,'')"))

        If DSRPList.Tables(0).Rows.Count > 0 Then
            RPlist.DataSource = DSRPList
            RPlist.DataBind()
            RPlist.ShowFooter = True
            RPlist.UseAccessibleHeader = True
            RPlist.HeaderRow.TableSection = TableRowSection.TableHeader
            RPlist.FooterRow.TableSection = TableRowSection.TableFooter
        End If

        DSRPList.Dispose()
        clsRP = Nothing
        
    End Function

    Protected Sub Attache(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim btn As Button = CType(sender, Button)
        Dim hdn As HiddenField = btn.FindControl("hdnPhyID")
        Dim OldRPID As String = Request.QueryString("RPID").ToString

        Dim clsFP As New ETS.BL.FaxPlus
        With clsFP
            .TranscriptionID = Request.QueryString("TransID").ToString
            .RPID = hdn.Value.ToString
            .Status = 0
            .DateAvailable = Now()
           
            If .AttachPhysician(Session("UserID").ToString, OldRPID) Then
                lit1.Visible = True
                RPlist.Visible = False
                lit1.Text = "Referring Physician has been set successfully"
            Else
                lit1.Visible = True
                lit1.Text = "Failed setting Referring Physician"
            End If
        End With
        clsFP = Nothing
        
    End Sub
    

    'Protected Sub RPlist_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles RPlist.SortCommand
    '    If String.IsNullOrEmpty(ViewState("SortOrder")) Then
    '        ViewState("SortOrder") = " ASC"
    '    ElseIf ViewState("SortOrder").ToString = " ASC" Then
    '        ViewState("SortOrder") = " DESC"
    '    Else
    '        ViewState("SortOrder") = " ASC"
    '    End If
    '    DoBind(e.SortExpression.ToString & ViewState("SortOrder").ToString)
    'End Sub
End Class
