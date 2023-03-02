Imports System.Data.Sqlclient
Imports System.Data
Partial Class ViewPlatBillingUnitsReport
    Inherits BasePage
    Protected Sub ShowActDetails()

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
    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click
        ShowActDetails()
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
