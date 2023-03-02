Imports System.Data.SqlClient
Partial Class DicWise
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Table1.Visible = False
            Table2.Visible = False
            ListBox1.Items.Clear()
            ListBox2.Items.Clear()
            TxtGrpDicName.Text = ""
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            If Request("DelGrp") = "True" Then
                Dim SQLCmd As New SqlCommand("Delete from AdminSecureweb.dbo.GrpDictators where GrpDicID = '" & Request("GrpDicID") & "'", New SqlConnection(strConn))
                Try
                    SQLCmd.Connection.Open()
                    SQLCmd.ExecuteNonQuery()
                Finally
                    If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                        SQLCmd.Connection.Close()
                        SQLCmd = Nothing
                    End If
                End Try

                Dim SQLCmd3 As New SqlCommand("Delete from AdminSecureweb.dbo.AssignDic where GrpDicID = '" & Request("GrpDicID") & "'", New SqlConnection(strConn))
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

            Dim SQLCmd1 As New SqlCommand("Select * from AdminETS.dbo.tblaccounts where contractorid ='" & Session("contractorid").ToString & "' AND Mode = 'DC' and (isdeleted is NULL OR isdeleted=0) order by Accountname", New SqlConnection(strConn))
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



            Dim sqlcmd2 As New SqlCommand("Select a.AccountName, P.FirstName + ' ' + P.LastName as UserName from AdminETS.dbo.tblPhysicians P,AdminETS.dbo.tblAccounts A Where P.AccountID = A.AccountID and a.Mode='DC' and a.IsDeleted=0 and p.IsDeleted=0 and p.FirstName+ ' '+p.LastName <> 'Test Test' and P.PhysicianID not in (Select UserID from AdminSecureweb.dbo.AssignDic) order by a.AccountName asc", New SqlConnection(strConn))
            Try
                sqlcmd2.Connection.Open()
                Dim DRRec2 As SqlDataReader = sqlcmd2.ExecuteReader()
                If DRRec2.HasRows = True Then
                    While DRRec2.Read
                        hactname.Value = DRRec2("AccountName")
                        hphyname.Value = DRRec2("Username")
                        ShowDetails(hactname.Value, hphyname.Value)
                    End While
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            End Try
        End If


    End Sub

    Protected Sub DLAct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLAct.SelectedIndexChanged
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        TxtGrpDicName.Text = ""
        Dim SQLSTR As String
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        SQLSTR = "Select P.FirstName + ' ' + P.LastName as UserName, P.PhysicianID from AdminETS.dbo.tblPhysicians P,AdminETS.dbo.tblAccounts A Where P.AccountID = A.AccountID and A.accountID = '" & DLAct.SelectedValue & "' and P.PhysicianID not in (Select UserID from AdminSecureweb.dbo.AssignDic where AccID='" & DLAct.SelectedValue & "')"
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

        Dim j As Integer
        Dim AssignDict As String
        AssignDict = ""
        j = 0
        SQLSTR = "Select * from AdminSecureweb.dbo.GrpDictators  Where accID = '" & DLAct.SelectedValue & "' order by GrpDicName"
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
                    Cell1.Text = "<a href='EditDicWise.aspx?AccountID=" & DLAct.SelectedValue & "&GrpDicID=" & DRRec("GrpDicID").ToString & "'>" & DRRec("GrpDicName").ToString & "</a>"
                    Cell4.Text = DRRec("Description").ToString
                    j = 0
                    AssignDict = ""
                    SQLSTR = "Select P.FirstName + ' ' + P.LastName as UserName from AdminSecureweb.dbo.AssignDic A, AdminETS.dbo.tblphysicians P where A.UserID = P.PhysicianID and A.GrpDicID = '" & DRRec("GrpDicID").ToString & "' order by firstname"
                    'Response.Write(SQLSTR)
                    Dim SQLCmd1 As New SqlCommand(SQLSTR, New SqlConnection(strConn))
                    Try
                        SQLCmd1.Connection.Open()
                        Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                        If DRRec1.HasRows = True Then
                            While (DRRec1.Read)
                                j = j + 1
                                AssignDict = AssignDict & j & ". " & DRRec1("Username").ToString & "<BR>"
                            End While
                        End If
                        'Response.Write(AssignDict)

                        DRRec1.Close()
                    Finally
                        If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                            SQLCmd1.Connection.Close()
                            SQLCmd1 = Nothing
                        End If
                    End Try
                    Cell3.Text = "<a href='DicWise.aspx?DelGrp=True&GrpDicID=" & DRRec("GrpDicID").ToString & "'>Remove Details</a>"
                    Cell2.Text = AssignDict
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
        SQLSTR = "select * from AdminSecureweb.dbo.GrpDictators where AccID='" & DLAct.SelectedValue & "' and GrpDicName='" & TxtGrpDicName.Text & "'"
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

        Dim GrpDicID As String = System.Guid.NewGuid().ToString()

        SQLSTR = "Insert Into AdminSecureweb.dbo.GrpDictators (GrpDicID, AccID, GrpDicName, Description, DateUpdated) Values ('" & GrpDicID & "','" & DLAct.SelectedValue & "','" & TxtGrpDicName.Text & "', '" & TxtDesc.Text & "','" & Now & "') "
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
            SQLSTR = "Insert Into AdminSecureweb.dbo.AssignDic (AccID, GrpDicID, UserID, DateUpdated) Values ('" & DLAct.SelectedValue & "','" & GrpDicID & "','" & ListBox2.Items(i).Value & "','" & Now & "')"
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

    End Sub

    Protected Sub ShowDetails(ByVal Account As String, ByVal Dictator As String)
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
