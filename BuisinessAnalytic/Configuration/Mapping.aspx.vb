Imports System.Data.Sqlclient
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO

Partial Class Transcend_Mapping
    Inherits BasePage
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If Not Page.IsPostBack Then
            DoSearch()
            'LoadData(ddlAccounts)
        End If
        
    End Sub
   
    Private Function DoSearch()
        lblResponse.Text = String.Empty


        Try

            Dim obj As New ETS.BL.UsersIDMappings
            With obj
                .ContractorID = Session("contractorid").ToString
            End With
            obj._WhereString.Append(" Order by username ")
            Dim objDS As System.Data.DataSet = obj.getUsersIDMappingsList
            Dim objDataTable As New System.Data.DataTable
            objDataTable = objDS.Tables(0)
            'Response.Write(objDataTable.Rows.Count)

            If objDataTable.Rows.Count > 0 Then
                GrdViewData.DataSource = objDataTable
                GrdViewData.DataBind()
            Else
                'Dim t As Data.DataTable = objDS.Tables(0).Clone
                'For Each c As Data.DataColumn In t.Columns
                '    c.AllowDBNull = True
                'Next
                't.Rows.Add(t.NewRow())
                'GrdViewData.DataSource = t
                'GrdViewData.DataBind()
                'GrdViewData.Rows(0).Visible = False
                'GrdViewData.Rows(0).Controls.Clear()
            End If

        Catch ex As Exception
            'Response.Write(ex.Message)
        Finally

        End Try
    End Function
   
   
    
    Protected Sub GrdViewData_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GrdViewData.RowCancelingEdit
        GrdViewData.EditIndex = -1
        DoSearch()
    End Sub
    Protected Sub GrdViewData_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GrdViewData.RowCommand
        If Trim(UCase(e.CommandName)) = Trim(UCase("Insert")) Then
            Dim objCmd As New ETS.BL.UsersIDMappings
            Try
                Dim varInsert As New System.Text.StringBuilder
                Dim varValues As New System.Text.StringBuilder



                Dim varUserName As String = String.Empty
                varUserName = DirectCast(GrdViewData.FooterRow.FindControl("FtxtUserName"), TextBox).Text.ToString

                If String.IsNullOrEmpty(varUserName) Then
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "UserName should not be empty "
                    Exit Sub
                End If

                If Not CheckUserName(varUserName) Then
                    'If Not String.IsNullOrEmpty(varUserName.ToString()) Then
                    '    If Not String.IsNullOrEmpty(varInsert.ToString) Then
                    '        objCmd.UserName = varUserName.ToString
                    '        varInsert.Append(",UserName")
                    '        varValues.Append(",'" & varUserName.ToString & "'")
                    '    Else
                    '        varInsert.Append("UserName")
                    '        varValues.Append("'" & varUserName.ToString & "'")
                    '    End If
                    'End If

                    Dim varMTID As String = String.Empty
                    varMTID = DirectCast(GrdViewData.FooterRow.FindControl("FtxtMTID"), TextBox).Text.ToString

                    'If Not String.IsNullOrEmpty(varMTID.Text.ToString) Then
                    '    If Not String.IsNullOrEmpty(varInsert.ToString) Then
                    '        varInsert.Append(",MTIDt")
                    '        varValues.Append(",'" & varMTID.Text.ToString & "'")
                    '    Else
                    '        varInsert.Append("MTID")
                    '        varValues.Append("'" & varMTID.Text.ToString & "'")
                    '    End If
                    'End If

                    Dim varQAID As String = String.Empty
                    varQAID = DirectCast(GrdViewData.FooterRow.FindControl("FtxtQAID"), TextBox).Text.ToString

                    'If Not String.IsNullOrEmpty(varQAID.ToString()) Then
                    '    If Not String.IsNullOrEmpty(varInsert.ToString) Then
                    '        varInsert.Append(",QAID")
                    '        varValues.Append(",'" & varQAID.ToString & "'")
                    '    Else
                    '        varInsert.Append("QAID")
                    '        varValues.Append("'" & varQAID.ToString & "'")
                    '    End If
                    'End If

                    Dim varQABID As String = String.Empty
                    varQABID = DirectCast(GrdViewData.FooterRow.FindControl("FtxtQABID"), TextBox).Text.ToString

                    'If Not String.IsNullOrEmpty(varQABID.ToString()) Then
                    '    If Not String.IsNullOrEmpty(varInsert.ToString) Then
                    '        varInsert.Append(",QABID")
                    '        varValues.Append(",'" & varQABID.ToString & "'")
                    '    Else
                    '        varInsert.Append("QABID")
                    '        varValues.Append("'" & varQABID.ToString & "'")
                    '    End If
                    'End If

                    'Response.Write(varMTID.ToString)
                    objCmd.UserName = varUserName.ToString
                    objCmd.MTID = varMTID.ToString
                    objCmd.QAID = varQAID.ToString
                    objCmd.QABID = varQABID.ToString
                    objCmd.ContractorID = Session("ContractorID").ToString
                    'Response.Write(objCmd.InsertUsersIDMappings)
                    If objCmd.InsertUsersIDMappings = 1 Then
                        GrdViewData.EditIndex = -1
                        DoSearch()
                    End If
                Else
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "Transcend id already assigned "
                    Exit Sub
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                objCmd = Nothing
            End Try
            'Else
            '    lblResponse.Text = String.Empty
            '    lblResponse.Text = "Please search only user wise and then assign transcend ids"
            '    Exit Sub
            'End If
        ElseIf Trim(UCase(e.CommandName)) = Trim(UCase("Delete")) Then
            Dim varAutoID As String = e.CommandArgument
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

            Try
                oConn.Open()

                Dim varQuery As String = String.Empty
                varQuery = "DELETE FROM AdminETS.dbo.tblUsersIDMappings WHERE AutoID='" & varAutoID.ToString & "'"

                Dim objCmd As New Data.SqlClient.SqlCommand(varQuery, oConn)
                If objCmd.ExecuteNonQuery = 1 Then
                    GrdViewData.EditIndex = -1
                    DoSearch()
                End If
                objCmd = Nothing
            Catch ex As Exception
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub
    Protected Sub GrdViewData_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GrdViewData.RowDeleting
    End Sub
    Protected Sub GrdViewData_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GrdViewData.RowEditing
        GrdViewData.EditIndex = e.NewEditIndex
        DoSearch()
    End Sub
    Protected Sub GrdViewData_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GrdViewData.RowUpdating
        If GrdViewData.Rows.Count > 0 Then
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Try
                Dim varUpdate As New System.Text.StringBuilder
                Dim varValues As New System.Text.StringBuilder



                Dim varUserName As String = String.Empty
                varUserName = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtUserName"), TextBox).Text.ToString
                'Response.Write("username")

                If String.IsNullOrEmpty(varUserName) Then
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "UserName should not be empty "
                    Exit Sub
                End If

                If Not CheckUserName(varUserName) Then

                    varUpdate.Append("UserName='" & varUserName.ToString & "'")
                    Dim varMTID As TextBox
                    varMTID = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtMTID"), TextBox)
                    varUpdate.Append(",MTID='" & varMTID.Text.ToString & "'")
                    Dim varQAID As String = String.Empty
                    varQAID = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtQAID"), TextBox).Text.ToString
                    varUpdate.Append(",QAID='" & varQAID.ToString & "'")

                    Dim varQABID As String = String.Empty
                    varQABID = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("txtQABID"), TextBox).Text.ToString
                    varUpdate.Append(",QABID='" & varQABID.ToString & "'")

                    oConn.Open()
                    Dim varhdnAutoID As New HiddenField
                    varhdnAutoID = DirectCast(GrdViewData.Rows(e.RowIndex).FindControl("hdnAutoID"), HiddenField)
                    Dim varQuery As String = String.Empty
                    varQuery = "UPDATE AdminETS.dbo.tblUsersIDMappings SET " & varUpdate.ToString & " WHERE AutoID='" & varhdnAutoID.Value.ToString & "'"
                    'Response.Write(varQuery.ToString)
                    Dim objCmd As New Data.SqlClient.SqlCommand(varQuery, oConn)

                    If objCmd.ExecuteNonQuery = 1 Then
                        GrdViewData.EditIndex = -1
                        DoSearch()
                    End If
                Else
                    lblResponse.Text = String.Empty
                    lblResponse.Text = "Username does not exist. "
                    Exit Sub
                End If
            Catch ex As Exception
                'Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub


    Protected Sub GrdViewData_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdViewData.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'Dim hdnUserId As HiddenField
                'hdnUserId = DirectCast(e.Row.FindControl("hdnUserID"), HiddenField)

                'Dim objDDL As DropDownList
                'objDDL = DirectCast(e.Row.FindControl("ddlSID"), DropDownList)

                'If Not objDDL Is Nothing Then
                '    BindData(objDDL, False)

                '    If Not hdnUserId Is Nothing Then
                '        If Not String.IsNullOrEmpty(hdnUserId.Value.ToString) Then
                '            objDDL.SelectedValue = hdnUserId.Value.ToString
                '        End If
                '    End If
                'End If

                ''Dim hdnAccId As HiddenField
                ''hdnAccId = DirectCast(e.Row.FindControl("hdnAccID"), HiddenField)

                ''Dim ddlA As DropDownList
                ''ddlA = DirectCast(e.Row.FindControl("GddlAccounts"), DropDownList)

                ''If Not ddlA Is Nothing Then
                ''    LoadData(ddlA)
                ''    If Not hdnAccId Is Nothing Then
                ''        If Not String.IsNullOrEmpty(hdnAccId.Value.ToString) Then
                ''            ddlA.SelectedValue = hdnAccId.Value.ToString
                ''        End If
                ''    End If
                ''End If

                'Dim objClearance As DropDownList
                'objClearance = DirectCast(e.Row.FindControl("ddlCS"), DropDownList)

                'If Not objClearance Is Nothing Then
                '    If String.IsNullOrEmpty(objClearance.SelectedValue) Then
                '        DirectCast(e.Row.FindControl("ddlAType"), DropDownList).Enabled = False
                '    Else
                '        If Trim(UCase(objClearance.SelectedValue)) = Trim(UCase("Yes")) Then
                '            DirectCast(e.Row.FindControl("ddlAType"), DropDownList).Enabled = True
                '        Else
                '            DirectCast(e.Row.FindControl("ddlAType"), DropDownList).Enabled = False
                '        End If
                '    End If
                '    objClearance.Attributes.Add("onchange", "javascript:StatusGridDropdown('" & objClearance.ClientID & "','" & DirectCast(e.Row.FindControl("ddlAType"), DropDownList).ClientID & "')")
                'End If


            ElseIf e.Row.RowType = DataControlRowType.Footer Then
                'Dim objDDL1 As New DropDownList
                'objDDL1 = DirectCast(e.Row.FindControl("FddlSID"), DropDownList)

                'If Not objDDL1 Is Nothing Then
                '    BindData(objDDL1, True)
                'End If

                'Dim ddlA1 As DropDownList
                'ddlA1 = DirectCast(e.Row.FindControl("GFddlAccounts"), DropDownList)

                'If Not ddlA1 Is Nothing Then
                '    LoadData(ddlA1)
                'End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Function CheckUserName(ByVal UserName As String) As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim varReturn As Boolean = False
        Try
            oConn.Open()

            Dim objCmd As New Data.SqlClient.SqlCommand("SELECT Count(*) AS 'Count' FROM AdminETS.dbo.tblUsersIDMappings WHERE UserName='" & UserName.ToString & "' AND IsDeleted IS NULL ", oConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader
            If objRec.HasRows Then
                While objRec.Read
                    If objRec("Count") > 0 Then
                        varReturn = True
                    End If
                End While
            End If
            objRec.Close()
            objRec = Nothing
            objCmd = Nothing

        Catch ex As Exception

        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        Return varReturn
    End Function

    'Protected Sub LnlExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnlExport.Click
    '    Dim filename As String
    '    filename = "Daily Productivity " & Month(Now) & Day(Now) & Year(Now) & ".xls"
    '    Dim attachment As String = "attachment; filename=" & filename
    '    Response.ClearContent()
    '    Response.AddHeader("content-disposition", attachment)
    '    Response.ContentType = "application/ms-excel"
    '    Dim sw As New StringWriter()
    '    Dim htw As New HtmlTextWriter(sw)
    '    'If RBPage.SelectedValue = "AP" Then
    '    '    GrdViewData.AllowPaging = False
    '    'ElseIf RBPage.SelectedValue = "CP" Then
    '    '    GrdViewData.AllowPaging = True
    '    'Else
    '    '    GrdViewData.AllowPaging = False
    '    'End If
    '    'GrdViewData.AllowSorting = False
    '    'BindData(Hsort.Value, Horder.Value)
    '    'GrdViewData.ShowCount = False
    '    DoSearch()
    '    Dim Table1 As New Table
    '    Table1.GridLines = GridLines.Both
    '    Table1.Font.Name = "Trebuchet MS"
    '    Table1.Font.Size = 8
    '    'Table1.CssClass = "common"
    '    'Table1.Font.Italic = True
    '    Dim x As Integer
    '    If (Not (GrdViewData.HeaderRow) Is Nothing) Then
    '        Dim TRow1 As New TableRow
    '        For x = 0 To GrdViewData.HeaderRow.Cells.Count - 3
    '            'If GrdViewData.Columns(x).Visible = True Then
    '            Dim TCell1 As New TableCell
    '            TCell1.Text = GrdViewData.HeaderRow.Cells(x).Text
    '            TCell1.Font.Bold = True
    '            TCell1.BackColor = Drawing.Color.Gray
    '            TCell1.ForeColor = Drawing.Color.White
    '            TRow1.Cells.Add(TCell1)
    '            'End If
    '        Next
    '        Table1.Rows.Add(TRow1)
    '    End If
    '    Dim i As Integer
    '    Dim k As Integer
    '    Dim AltRec As Boolean = True
    '    k = 0
    '    For Each row As GridViewRow In GrdViewData.Rows
    '        k = k + 1
    '        Dim TRow1 As New TableRow
    '        If row.RowIndex = 0 Then
    '            row.Font.Bold = True
    '            row.BackColor = Drawing.Color.Navy
    '            row.ForeColor = Drawing.Color.White
    '        ElseIf AltRec = True Then
    '            row.CssClass = "gridalt1"
    '            AltRec = False
    '        ElseIf AltRec = False Then
    '            row.CssClass = "gridalt2"
    '            AltRec = True
    '        End If
    '        Response.Write(row.Cells(i).Text)
    '        For i = 0 To row.Cells.Count - 3
    '            ' If GrdViewData.Columns(i).Visible = True Then
    '            Dim TCell1 As New TableCell
    '            TCell1.Text = row.Cells(i).Text
    '            TRow1.Cells.Add(TCell1)
    '            'End If
    '        Next
    '        Table1.Rows.Add(TRow1)
    '        'If GrdViewData.AllowPaging = True And GrdViewData.PageSize = k Then
    '        '    Exit For
    '        'End If
    '    Next
    '    Table1.RenderControl(htw)
    '    Response.Write(sw.ToString())
    '    Response.[End]()
    'End Sub
End Class
