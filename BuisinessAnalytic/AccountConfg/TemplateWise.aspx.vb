Imports System.Data.SqlClient
Partial Class TemplateWise
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Table1.Visible = False
            Table2.Visible = False
            ListBox1.Items.Clear()
            ListBox2.Items.Clear()
            TxtGrpTempName.Text = ""
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            If Request("DelGrp") = "True" Then
                Dim SQLCmd As New SqlCommand("Delete from AdminSecureweb.dbo.tblGrpTemplates where GrpTempID = '" & Request("GrpTempID") & "'", New SqlConnection(strConn))
                Try
                    SQLCmd.Connection.Open()
                    SQLCmd.ExecuteNonQuery()
                Finally
                    If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                        SQLCmd.Connection.Close()
                        SQLCmd = Nothing
                    End If
                End Try

                Dim SQLCmd3 As New SqlCommand("Delete from AdminSecureweb.dbo.tblAssignTemplates where GrpTempID = '" & Request("GrpTempID") & "'", New SqlConnection(strConn))
                Try
                    SQLCmd3.Connection.Open()
                    SQLCmd3.ExecuteNonQuery()
                Finally
                    If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                        SQLCmd3.Connection.Close()
                        SQLCmd3 = Nothing
                    End If
                End Try

            End If

            Dim SQLCmd1 As New SqlCommand("Select * from AdminETS.dbo.tblaccounts where contractorid ='" & Session("contractorid").ToString & "' AND Mode = 'TW' order by Accountname", New SqlConnection(strConn))
            'Response.Write("Select * from AdminETS.dbo.tblaccounts where contractorid ='" & Session("contractorid").ToString & "' AND Mode = 'TL' order by Accountname")
            Try
                SQLCmd1.Connection.Open()
                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                If DRRec1.HasRows = True Then

                    While DRRec1.Read
                        Dim LI As New ListItem
                        LI.Text = DRRec1("Accountname")
                        LI.Value = DRRec1("AccountID").ToString
                        DLAct.Items.Add(LI)
                    End While
                End If
                DRRec1.Close()
            Finally
                If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd1.Connection.Close()
                    SQLCmd1 = Nothing
                End If
            End Try

            Dim sqlcmd2 As New SqlCommand("Select Distinct  a.AccountName, T.TemplateName from AdminETS.dbo.tblPhysiciansTemplate PT, AdminETS.dbo.tblTemplates T, AdminETS.dbo.tblPhysicians P,AdminETS.dbo.tblAccounts A Where P.PhysicianID=PT.PhysicianID and PT.TemplateID = T.TemplateID and P.AccountID = A.AccountID and (a.IsDeleted=0 or a.IsDeleted is NULL) and a.Mode='TW'  and PT.TemplateID not in (Select TemplateID from AdminSecureweb.dbo.tblAssignTemplates ) order by a.AccountName asc", New SqlConnection(strConn))
            Try
                sqlcmd2.Connection.Open()
                Dim DRRec2 As SqlDataReader = sqlcmd2.ExecuteReader()
                If DRRec2.HasRows = True Then
                    While DRRec2.Read
                        showallactemplates(DRRec2("AccountName"), DRRec2("TemplateName"))
                    End While
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try






        End If


    End Sub

    Protected Sub DLAct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLAct.SelectedIndexChanged

        ShowDet()
    End Sub
    Protected Sub ShowDet()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        TxtGrpTempName.Text = ""
        Dim SQLSTR As String
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        SQLSTR = "Select Distinct T.TemplateName, T.TemplateID from AdminETS.dbo.tblPhysiciansTemplate PT, AdminETS.dbo.tblTemplates T, AdminETS.dbo.tblPhysicians P,AdminETS.dbo.tblAccounts A Where P.PhysicianID=PT.PhysicianID and PT.TemplateID = T.TemplateID and P.AccountID = A.AccountID and A.accountID = '" & DLAct.SelectedValue & "' and PT.TemplateID not in (Select TemplateID from AdminSecureweb.dbo.tblAssignTemplates where AccID='" & DLAct.SelectedValue & "') order by T.TemplateName"
        'Response.Write(SQLSTR)
        Dim SQLCmd2 As New SqlCommand(SQLSTR, New SqlConnection(strConn))
        Try
            SQLCmd2.Connection.Open()
            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
            If DRRec2.HasRows = True Then

                While (DRRec2.Read)
                    Dim LI As New ListItem
                    LI.Text = DRRec2("TemplateName")
                    LI.Value = DRRec2("TemplateID").ToString
                    ListBox1.Items.Add(LI)
                End While
            End If
            DRRec2.Close()
        Finally
            If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd2.Connection.Close()
                SQLCmd2 = Nothing
            End If
        End Try

        Dim j As Integer
        Dim tblAssignTemplatest As String
        tblAssignTemplatest = ""
        j = 0
        SQLSTR = "Select * from AdminSecureweb.dbo.tblGrpTemplates  Where accID = '" & DLAct.SelectedValue & "' order by GrpTempName"
        'Response.Write(SQLSTR)
        Dim SQLCmd As New SqlCommand(SQLSTR, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows = True Then
                While (DRRec.Read)
                    Dim Row1 As New TableRow
                    Row1.VerticalAlign = VerticalAlign.Top
                    Row1.Font.Size = "10"
                    Row1.Font.Name = "Arial"
                    Row1.Font.Size = "8"
                    Row1.HorizontalAlign = HorizontalAlign.Left
                    Dim Cell1 As New TableCell
                    Dim Cell2 As New TableCell
                    Dim Cell3 As New TableCell
                    Dim Cell4 As New TableCell
                    Cell1.Text = "<a href='EditTempWise.aspx?AccountID=" & DLAct.SelectedValue & "&GrpTempID=" & DRRec("GrpTempID").ToString & "'>" & DRRec("GrpTempName").ToString & "</a>"
                    Cell4.Text = DRRec("Description").ToString
                    j = 0
                    tblAssignTemplatest = ""
                    SQLSTR = "Select TemplateName from AdminSecureweb.dbo.tblAssignTemplates A, AdminETS.dbo.tblTemplates T where A.TemplateID = T.TemplateID and A.GrpTempID = '" & DRRec("GrpTempID").ToString & "' order by TemplateName"
                    'Response.Write(SQLSTR)
                    Dim SQLCmd1 As New SqlCommand(SQLSTR, New SqlConnection(strConn))
                    Try
                        SQLCmd1.Connection.Open()
                        Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                        If DRRec1.HasRows = True Then
                            While (DRRec1.Read)
                                j = j + 1
                                tblAssignTemplatest = tblAssignTemplatest & j & ". " & DRRec1("TemplateName").ToString & "<BR>"
                            End While
                        End If
                        'Response.Write(tblAssignTemplatest)

                        DRRec1.Close()
                    Finally
                        If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                            SQLCmd1.Connection.Close()
                            SQLCmd1 = Nothing
                        End If
                    End Try
                    Cell3.Text = "<a href='TemplateWise.aspx?DelGrp=True&GrpTempID=" & DRRec("GrpTempID").ToString & "'>Remove Details</a>"
                    Cell2.Text = tblAssignTemplatest
                    Row1.Cells.Add(Cell1)
                    Row1.Cells.Add(Cell4)
                    Row1.Cells.Add(Cell2)
                    Row1.Cells.Add(Cell3)
                    Table1.Rows.Add(Row1)




                End While
            End If
            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try

        Table1.Visible = True
        Table2.Visible = True
    End Sub
    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click

        Dim intCt As Integer
        For intCt = ListBox2.Items.Count - 1 To 0 Step -1 ' Looping Backwards
            If ListBox2.Items(intCt).Selected Then
                Dim LI As New ListItem
                LI.Text = ListBox2.Items(intCt).Text
                LI.Value = ListBox2.Items(intCt).Value
                'Response.Write(LI.Text)

                ListBox1.Items.Add(LI)
                ListBox2.Items.Remove(ListBox2.Items(intCt))
            End If
        Next
        'ShowDet()
        'Dim i As Integer
        'Dim icount As Integer
        'icount = ListBox1.Items.Count - 1
        'Response.Write(icount)
        ''Do Until icount = 0

        ''Loop
        'For Each L As ListItem In ListBox1.Items
        '    If L.Selected Then
        '        ListBox2.Items.Add(L)
        '        ListBox1.Items.Remove(L)
        '    End If
        'Next

        ''For i = 1 To icount

        ''    Response.Write(ListBox1.Items(i).Selected)


        ''    'If ListBox1.Items(i).Selected Then
        'Dim LI As New ListItem
        'LI.Text = ListBox1.Items(i).Text
        'LI.Value = ListBox1.Items(i).Value
        ''    '    ListBox2.Items.Add(LI)
        ''    '    ListBox1.Items.RemoveAt(i)
        ''    '    'ListBox1.Items(i).re()
        ''    'End If
        ''Next

    End Sub


    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        Dim intCt As Integer
        For intCt = ListBox1.Items.Count - 1 To 0 Step -1 ' Looping Backwards
            If ListBox1.Items(intCt).Selected Then
                Dim LI As New ListItem
                LI.Text = ListBox1.Items(intCt).Text
                LI.Value = ListBox1.Items(intCt).Value
                'Response.Write(LI.Text)

                ListBox2.Items.Add(LI)
                ListBox1.Items.Remove(ListBox1.Items(intCt))
            End If
        Next
        'ShowDet()
        'Dim i As Integer
        'Dim icount As Integer
        'icount = ListBox1.Items.Count - 1
        'Response.Write(icount)
        ''Do Until icount = 0

        ''Loop
        'For Each L As ListItem In ListBox1.Items
        '    If L.Selected Then
        '        ListBox2.Items.Add(L)
        '        ListBox1.Items.Remove(L)
        '    End If
        'Next

        ''For i = 1 To icount

        ''    Response.Write(ListBox1.Items(i).Selected)


        ''    'If ListBox1.Items(i).Selected Then
        'Dim LI As New ListItem
        'LI.Text = ListBox1.Items(i).Text
        'LI.Value = ListBox1.Items(i).Value
        ''    '    ListBox2.Items.Add(LI)
        ''    '    ListBox1.Items.RemoveAt(i)
        ''    '    'ListBox1.Items(i).re()
        ''    'End If
        ''Next
    End Sub

    Protected Sub btnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        Dim Query As String
        Dim SQLSTR As String
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        SQLSTR = "select * from AdminSecureweb.dbo.tblGrpTemplates where AccID='" & DLAct.SelectedValue & "' and GrpTempName='" & TxtGrpTempName.Text & "'"
        'Response.Write(SQLSTR)
        Dim SQLCmd2 As New SqlCommand(SQLSTR, New SqlConnection(strConn))
        Try
            SQLCmd2.Connection.Open()
            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
            If DRRec2.HasRows = True Then


            End If
            DRRec2.Close()
        Finally
            If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd2.Connection.Close()
                SQLCmd2 = Nothing
            End If
        End Try

        Dim GrpTempID As String = System.Guid.NewGuid().ToString()

        SQLSTR = "Insert Into AdminSecureweb.dbo.tblGrpTemplates (GrpTempID, AccID, GrpTempName, Description, DateUpdated) Values ('" & GrpTempID & "','" & DLAct.SelectedValue & "','" & TxtGrpTempName.Text & "', '" & TxtDesc.Text & "','" & Now & "') "
        Dim SQLCmd As New SqlCommand(SQLSTR, New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            SQLCmd.ExecuteNonQuery()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try

        Dim i As Integer
        For i = 0 To ListBox2.Items.Count - 1
            SQLSTR = "Insert Into AdminSecureweb.dbo.tblAssignTemplates (AccID, GrpTempID, TemplateID, DateUpdated) Values ('" & DLAct.SelectedValue & "','" & GrpTempID & "','" & ListBox2.Items(i).Value & "','" & Now & "')"
            'response.write sql
            Dim SQLCmd1 As New SqlCommand(SQLSTR, New SqlConnection(strConn))
            Try
                SQLCmd1.Connection.Open()
                SQLCmd1.ExecuteNonQuery()
            Finally
                If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd1.Connection.Close()
                    SQLCmd1 = Nothing
                End If
            End Try

        Next
        ShowDet()
    End Sub

    Protected Sub showallactemplates(ByVal Account As String, ByVal Dictator As String)
        Dim row1 As New TableRow
        Dim Cell1 As New TableCell
        Dim Cell2 As New TableCell


        Cell1.Text = Account
        Cell2.Text = Dictator

        row1.Cells.Add(Cell1)
        row1.Cells.Add(Cell2)

        TblDetails.Rows.Add(row1)

    End Sub

End Class
