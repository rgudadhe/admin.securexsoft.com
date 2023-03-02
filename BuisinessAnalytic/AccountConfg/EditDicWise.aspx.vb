Imports System.Data.SqlClient
Partial Class EditDicWise
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Table1.Visible = False
            'Table2.Visible = False
            'ListBox1.Items.Clear()
            'ListBox2.Items.Clear()
            'TxtGrpDicName.Text = ""
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            'If Request("DelGrp") = "True" Then
            '    Dim SQLCmd As New SqlCommand("Delete from AdminSecureweb.dbo.GrpDictators where GrpDicID = '" & Request("GrpDicID") & "'", New SqlConnection(strConn))
            '    Try
            '        SQLCmd.Connection.Open()
            '        SQLCmd.ExecuteNonQuery()
            '    Finally
            '        If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
            '            SQLCmd.Connection.Close()
            '            SQLCmd = Nothing
            '        End If
            '    End Try

            '    Dim SQLCmd3 As New SqlCommand("Delete from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & Request("GrpDicID") & "'", New SqlConnection(strConn))
            '    Try
            '        SQLCmd3.Connection.Open()
            '        SQLCmd3.ExecuteNonQuery()
            '    Finally
            '        If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
            '            SQLCmd3.Connection.Close()
            '            SQLCmd3 = Nothing
            '        End If
            '    End Try

            'End If

            Dim SQLCmd1 As New SqlCommand("Select GrpDicName, Description, IsNull(SepInvoice, 'False') as SepInvoice from AdminSecureweb.dbo.GrpDictators where GrpDicID = '" & Request("GrpDicID") & "'", New SqlConnection(strConn))
            'Response.Write("Select * from AdminSecureweb.dbo.GrpDictators where GrpDicID = '" & Request("GrpDicID") & "'")
            Dim SelCount As Integer
            Try
                SQLCmd1.Connection.Open()
                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                If DRRec1.HasRows = True Then
                    If DRRec1.Read Then
                        TxtGrpDicName.Text = DRRec1("GrpDicName").ToString
                        TxtDesc.Text = DRRec1("Description").ToString

                        If DRRec1("Sepinvoice").ToString Then
                            DLSepInvoice.Items(0).Selected = False
                            DLSepInvoice.Items(1).Selected = True
                        Else
                            DLSepInvoice.Items(1).Selected = False
                            DLSepInvoice.Items(0).Selected = True
                        End If

                    End If
                End If
                DRRec1.Close()
            Finally
                If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd1.Connection.Close()
                    SQLCmd1 = Nothing
                End If
            End Try
            ShowDetails()
        End If


    End Sub

    Protected Sub ShowDetails()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        Dim SQLSTR As String
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        SQLSTR = "Select P.FirstName + ' ' + P.LastName as UserName, P.PhysicianID from AdminETS.dbo.tblPhysicians P,AdminETS.dbo.tblAccounts A Where P.AccountID = A.AccountID and A.accountID = '" & Request("AccountID") & "' and P.PhysicianID not in (Select UserID from AdminSecureweb.dbo.AssignDic where AccID='" & Request("AccountID") & "')"
        'Response.Write(SQLSTR)
        Dim SQLCmd2 As New SqlCommand(SQLSTR, New SqlConnection(strConn))
        Try
            SQLCmd2.Connection.Open()
            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
            If DRRec2.HasRows = True Then

                While (DRRec2.Read)
                    Dim LI As New ListItem
                    LI.Text = DRRec2("Username")
                    LI.Value = DRRec2("PhysicianID").ToString
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

        SQLSTR = "Select P.FirstName + ' ' + P.LastName as UserName, P.PhysicianID from AdminETS.dbo.tblPhysicians P,AdminETS.dbo.tblAccounts A Where P.AccountID = A.AccountID and A.accountID = '" & Request("AccountID") & "' and P.PhysicianID  in (Select UserID from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & Request("GrpDicID") & "')"

        Dim SQLCmd3 As New SqlCommand(SQLSTR, New SqlConnection(strConn))
        Try
            SQLCmd3.Connection.Open()
            Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
            If DRRec3.HasRows = True Then

                While (DRRec3.Read)
                    Dim LI As New ListItem
                    LI.Text = DRRec3("Username")
                    LI.Value = DRRec3("PhysicianID").ToString
                    ListBox2.Items.Add(LI)
                End While
            End If
            DRRec3.Close()
        Finally
            If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd3.Connection.Close()
                SQLCmd3 = Nothing
            End If
        End Try



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
        SQLSTR = "select * from AdminSecureweb.dbo.GrpDictators where GrpDicID Not In ('" & Request("GrpDicID") & "') and AccID='" & Request("AccountID") & "' and GrpDicName='" & TxtGrpDicName.Text & "'"
        'Response.Write(SQLSTR)
        Dim SQLCmd2 As New SqlCommand(SQLSTR, New SqlConnection(strConn))
        Try
            SQLCmd2.Connection.Open()
            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
            If DRRec2.HasRows = True Then
                Label1.Text = "Group name already exists."
                Exit Sub
            End If
            DRRec2.Close()
        Finally
            If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd2.Connection.Close()
                SQLCmd2 = Nothing
            End If
        End Try
        SQLSTR = "Update AdminSecureweb.dbo.GrpDictators Set GrpDicName = '" & TxtGrpDicName.Text & "', Description = '" & TxtDesc.Text & "', SepInvoice = '" & DLSepInvoice.SelectedValue & "'  where AccID='" & Request("AccountID") & "' and GrpDicID='" & Request("GrpDicID") & "'"
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

        SQLSTR = "DELETE FROM AdminSecureweb.dbo.AssignDic  where AccID='" & Request("AccountID") & "' and GrpDicID='" & Request("GrpDicID") & "'"
        Dim SQLCmdD As New SqlCommand(SQLSTR, New SqlConnection(strConn))
        Try
            SQLCmdD.Connection.Open()
            SQLCmdD.ExecuteNonQuery()
        Finally
            If SQLCmdD.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmdD.Connection.Close()
                SQLCmdD = Nothing
            End If
        End Try
        Dim i As Integer
        For i = 0 To ListBox2.Items.Count - 1
            SQLSTR = "Insert Into AdminSecureweb.dbo.AssignDic (AccID, GrpDicID, UserID, DateUpdated) Values ('" & Request("AccountID") & "','" & Request("GrpDicID") & "','" & ListBox2.Items(i).Value & "','" & Now & "')"
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
        Label1.Text = "Record has been updated."
    End Sub
End Class
