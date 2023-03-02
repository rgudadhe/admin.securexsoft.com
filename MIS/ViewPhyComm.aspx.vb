Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data



Partial Class UserPhyAssgn_Default
    Inherits BasePage




    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DispBox.Text = ""
        If Not Page.IsPostBack Then
            Dim LI1 As New ListItem
            LI1.Value = ""
            LI1.Text = "All Accounts"
            LI1.Selected = True
            ActList.Items.Add(LI1)

            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim Cmd As New SqlCommand("Select accountid, accountname from tblaccounts order by accountname", New SqlConnection(strConn))
            Try
                Cmd.Connection.Open()
                Dim DRRec1 As SqlDataReader = Cmd.ExecuteReader()
                If DRRec1.HasRows Then
                    While (DRRec1.Read)
                        Dim LI As New ListItem
                        LI.Value = DRRec1("accountid").ToString
                        LI.Text = DRRec1("accountname").ToString
                        ActList.Items.Add(LI)
                    End While
                End If
                DRRec1.Close()
            Finally
                If Cmd.Connection.State = System.Data.ConnectionState.Open Then
                    Cmd.Connection.Close()
                    Cmd = Nothing
                End If
            End Try
        Else

        End If

    End Sub

    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnsubmit3.Click
        Dim strConn As String
        Dim Lvl As String
        If Not IsDate(sDate.Text) Then
            DispBox.Text = "Please enter proper start date."
            sDate.Focus()
            Exit Sub
        ElseIf Not IsDate(eDate.Text) Then
            DispBox.Text = "Please enter proper end date."
            eDate.Focus()
            Exit Sub
        End If
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim i As Integer
        Dim j As Integer

        Dim sQuery3 As String
        Dim JobNumber As String
        Dim accountname As String
        Dim uname As String
        Dim dateupdated As String
        Dim comments As String
        JobNumber = ""

        Dim Sqlstring As String

        sQuery3 = "Select A.AccountName, U.First + ' ' + U.Last as uname, C.comments, T.JobNumber, C.dateupdated from DBO.tblPhyComments C INNER JOIN DBO.tbltranscriptionmain T on C.transcriptionID = T.transcriptionID INNER JOIN SecureWeb.DBO.tblusers U ON U.UserID = C.UserID INNER JOIN DBO.tblAccounts A ON U.AccID = A.AccountID where C.dateupdated between '" & sDate.Text & "' and '" & eDate.Text & "'   "
        If ActList.SelectedValue <> "" Then
            sQuery3 = sQuery3 & "  and A.AccountID ='" & ActList.SelectedValue & "' "
        End If
        sQuery3 = sQuery3 & "  order by T.jobnumber desc, C.dateupdated desc"

        Dim cmdSel As New SqlCommand(sQuery3, New SqlConnection(strConn))
        Try
            cmdSel.Connection.Open()
            Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
            If RdSet.HasRows = True Then
                Dim Cell1 As New TableCell
                Dim Cell2 As New TableCell
                Dim Cell3 As New TableCell
                Dim Cell4 As New TableCell
                Dim Cell5 As New TableCell
                Dim Row1 As New TableRow
                ' Row1.CssClass = "SMSelected"

                Cell1.Text = "Account Name"
                Cell2.Text = "Job Number"
                Cell3.Text = "Account User"
                Cell4.Text = "Comments"
                Cell5.Text = "Submit Date"
                Cell1.HorizontalAlign = HorizontalAlign.Center
                Cell2.HorizontalAlign = HorizontalAlign.Center
                Cell3.HorizontalAlign = HorizontalAlign.Center
                Cell4.HorizontalAlign = HorizontalAlign.Center
                Row1.BackColor = Drawing.Color.GhostWhite
                Row1.Cells.Add(Cell1)
                Row1.Cells.Add(Cell2)
                Row1.Cells.Add(Cell3)
                Row1.Cells.Add(Cell4)
                Row1.Cells.Add(Cell5)
                'Row1.BackColor = Drawing.Color.Navy
                'Row1.ForeColor = Drawing.Color.AntiqueWhite
                Row1.CssClass = "SMSelected"
                Cell1.HorizontalAlign = HorizontalAlign.Left
                Cell2.HorizontalAlign = HorizontalAlign.Left
                Cell3.HorizontalAlign = HorizontalAlign.Left
                Cell4.HorizontalAlign = HorizontalAlign.Left
                Cell5.HorizontalAlign = HorizontalAlign.Left
                Table4.Rows.Add(Row1)

                While (RdSet.Read)

                    If JobNumber <> "" And JobNumber = RdSet("jobnumber").ToString Then
                        JobNumber = ""
                        accountname = ""
                    Else
                        JobNumber = RdSet("jobnumber").ToString
                        accountname = RdSet("AccountName").ToString
                    End If


                    uname = RdSet("uname").ToString
                    dateupdated = RdSet("dateupdated").ToString
                    comments = RdSet("comments").ToString
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim c4 As New TableCell
                    Dim c5 As New TableCell
                    Dim c6 As New TableCell
                    Dim r As New TableRow

                    c2.Text = accountname
                    c3.Text = JobNumber
                    c4.Text = uname
                    c5.Text = comments
                    c6.Text = dateupdated
                    r.Cells.Add(c2)
                    r.Cells.Add(c3)
                    r.Cells.Add(c4)
                    r.Cells.Add(c5)
                    r.Cells.Add(c6)
                    r.HorizontalAlign = HorizontalAlign.Left

                    Table4.Rows.Add(r)

                    Table4.Visible = True
                    'Response.Write(RdSet("uName") & RdSet("LevelName") & RdSet("AccountName") & RdSet("pName"))
                End While
            Else
                DispBox.Text = "No Records Found."
            End If
            RdSet.Close()

        Finally
            If cmdSel.Connection.State = System.Data.ConnectionState.Open Then
                cmdSel.Connection.Close()
                cmdSel = Nothing
            End If
        End Try
        'Else
        'Dim c3 As New TableCell
        'Dim r As New TableRow
        'c3.ColumnSpan = 4
        'c3.Text = "No Records Found"
        'r.Cells.Add(c3)
        'Table4.Visible = True

        'Table4.Rows.Add(r)

        'End If

    End Sub


    
   


 
End Class

