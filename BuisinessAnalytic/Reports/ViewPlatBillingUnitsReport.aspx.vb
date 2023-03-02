Imports System.Data.Sqlclient
Imports System.Data
Partial Class ViewPlatBillingUnitsReport
    Inherits BasePage
    Protected Sub DeleteActDetails()
        Dim objInvVas As New ETS.BL.BillingPlatUnits
        Dim strBL As New StringBuilder

        Dim str As String = " WHERE Datediff(day, PostDate, '" & TxtEDate.Text & "') Between 0 and  Datediff(day, '" & TxtSDate.Text & "', '" & TxtEDate.Text & "') and platactID ='" & DLAct.SelectedValue & "'"
        'Response.Write(str)
        'Response.End()

        strBL.Append(str)
        objInvVas._WhereString = strBL
        objInvVas.DeleteBillingPlatUnitsinBatch()
        objInvVas = Nothing
    End Sub
    Protected Sub ShowActDetails()

        Dim objInvVas As New ETS.BL.BillingPlatUnits
        Dim strBL As New StringBuilder
        Dim str As String = " WHERE Datediff(day, PostDate, '" & TxtEDate.Text & "') Between 0 and  Datediff(day, '" & TxtSDate.Text & "', '" & TxtEDate.Text & "') and platactID ='" & DLAct.SelectedValue & "'"
        ' Dim str As String = " WHERE PostDate Between '" & TxtSDate.Text & "' and '" & TxtEDate.Text & "' and platactID ='" & DLAct.SelectedValue & "'"
        'Response.Write(str)
        'Response.End()

        strBL.Append(Str)
        objInvVas._WhereString = strBL
        Dim DTSet As DataSet = objInvVas.getBillingPlatUnits
        If DTSet.Tables.Count > 0 Then
            ' Response.Write(DTSet.Tables(0).Rows.Count)

            Dim Source As DataView = DTSet.Tables(0).DefaultView
            'Source.Sort = sortExpression + direction
            'If Source.Table.Rows.Count > 0 Then
            '    MenuPnl.Visible = True
            '    PLPage.Visible = True
            '    pnl1.Visible = True
            'End If
            For I As Integer = 0 To DTSet.Tables(0).Rows.Count - 1
                Dim Drrec4 As DataRow = DTSet.Tables(0).Rows(I)
                ' RecCount = RecCount + 1
                Dim Row2 As New TableRow
                Dim FCEll As New TableCell
                FCEll.Text = "<input type=checkbox name=DemoRec onclick=highlightRow(this); value=" & Drrec4("AutoID").ToString & ">"
                Row2.Cells.Add(FCEll)
                For J As Integer = 2 To DTSet.Tables(0).Columns.Count - 3
                    Dim Cell2 As New TableCell
                    'Cell2.Attributes.Add("ondblclick", "EditDet('" & DRRec4("LookupID").ToString & "','" & TableName & "','" & HActID.Value & "');")
                    'Response.Write(Drrec4(J).ToString)
                    Cell2.Text = Drrec4(J).ToString
                    Cell2.HorizontalAlign = HorizontalAlign.Center
                    Row2.Cells.Add(Cell2)
                Next
                Table1.Rows.Add(Row2)
            Next
            'Source.Sort = sortExpression + direction
            'MyDataGrid.DataSource = Source
            'MyDataGrid.DataBind()
        End If
    End Sub
    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        ShowActDetails()
    End Sub

    Protected Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteActDetails()
    End Sub
    Function WorkingDays(ByVal StartDate As Date, ByVal EndDate As Date) As Integer
        Dim intCount As Integer
        intCount = 0
        Do While StartDate <= EndDate
            Select Case Weekday(StartDate)
                Case "1"
                    intCount = intCount
                Case "7"
                    intCount = intCount
                Case "2"
                    intCount = intCount + 1
                Case "3"
                    intCount = intCount + 1
                Case "4"
                    intCount = intCount + 1
                Case "5"
                    intCount = intCount + 1
                Case "6"
                    intCount = intCount + 1
            End Select
            StartDate = DateAdd(DateInterval.Day, 1, StartDate)
        Loop
        If intCount = 0 Then
            intCount = 1
        End If
        WorkingDays = intCount
    End Function
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strPlatRec() As String
        strPlatRec = Split(Request("DemoRec"), ",")
        Dim i As Integer
        For i = 0 To strPlatRec.Length - 1
            Dim objPlatUnits As New ETS.BL.BillingPlatUnits
            objPlatUnits.AutoID = strPlatRec(i)
            objPlatUnits.DeleteBillingPlatUnits()
        Next


    End Sub
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ObjAct As New ETS.BL.Accounts
            ObjAct.IsPlatForm = True
            Dim DTSet As DataSet = ObjAct.getAccountList
            If DTSet.Tables.Count > 0 Then
                DLAct.DataSource = DTSet.Tables(0)
                DLAct.DataTextField = "AccountName"
                DLAct.DataValueField = "AccountID"
                DLAct.DataBind()
            End If
            Dim LI1 As New ListItem
            LI1.Text = "SecureXFlow"
            LI1.Value = "11111111-1111-1111-1111-111111111111"
            LI1.Selected = True
            DLAct.Items.Add(LI1)
        End If
    End Sub
    'Protected Sub chkSelectAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim chkAll As CheckBox = DirectCast(MyDataGrid.HeaderRow.FindControl("chkSelectAll"), CheckBox)
    '    If chkAll.Checked = True Then
    '        For Each gvRow As GridViewRow In MyDataGrid.Rows
    '            Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
    '            chkSel.Checked = True
    '        Next
    '    Else
    '        For Each gvRow As GridViewRow In MyDataGrid.Rows
    '            Dim chkSel As CheckBox = DirectCast(gvRow.FindControl("chkSelect"), CheckBox)
    '            chkSel.Checked = False
    '        Next
    '    End If
    'End Sub
    'Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim chkTest As CheckBox = DirectCast(sender, CheckBox)
    '    If chkTest.Checked = False Then
    '        Dim chkAll As CheckBox = DirectCast(MyDataGrid.HeaderRow.FindControl("chkSelectAll"), CheckBox)
    '        chkAll.Checked = False

    '    End If
    'End Sub

    'Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
    '    If e.Row.RowType = DataControlRowType.Header Then
    '        'Find the checkbox control in header and add an attribute
    '        'CType(e.Row.FindControl("cbSelectAll"), CheckBox).Attributes.Add("onclick", "javascript:SelectAll('" + (CType(e.Row.FindControl("cbSelectAll"), CheckBox)).ClientID + "')")
    '    End If
    'End Sub


End Class
