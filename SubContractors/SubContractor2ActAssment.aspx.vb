Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      
        DispBox.Text = ""
        BtnAssign.Visible = False
        Table4.Visible = False

        If Not IsPostBack Then


            BtnAssign.Visible = False
            Panel2.Visible = False
            Panel3.Visible = False
            Panel6.Visible = False
            Panel7.Visible = False

            Panel1.Visible = True
            Panel5.Visible = True
            SubContractorState.Value = 0
            ActState.Value = 0
        Else


        End If



    End Sub







    Protected Sub ActSearch_Click()

        Panel6.Visible = True
        Panel5.Visible = False
        Panel7.Visible = False
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            Dim SQLCmd1 As New SqlCommand("Select * from tblAccounts where AccountName like '" & TxtAname.Text & "' and ContractorID='" & Session("ContractorID") & "' ", oConn)
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            Dim K As Integer
            If DRRec1.HasRows Then
                K = 0
                While (DRRec1.Read)
                    K = K + 1
                    Dim c As New TableCell
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim r As New TableRow
                    Dim CB1 As New CheckBox
                    CB1.ID = "AccountID" & K
                    CB1.InputAttributes.Add("Value", DRRec1("AccountID").ToString)
                    TotAct.Value = K
                    c.Text = DRRec1("AccountName")
                    c1.Text = DRRec1("AccountNo")
                    'c2.Text = DRRec1("PinNo")
                    c3.Controls.Add(CB1)
                    r.Cells.Add(c3)
                    r.Cells.Add(c)
                    r.Cells.Add(c1)
                    'r.Cells.Add(c2)
                    Table1.Rows.Add(r)

                End While

                Dim CB As New Button
                Dim r2 As New TableRow
                Dim c4 As New TableCell
                'CB.ID = "Btnsubmit3"
                'CB.Text = "Submit"
                c4.ColumnSpan = 3
                c4.Style("text-align") = "center"
                btnsubmit3.Visible = True

                c4.Controls.Add(btnsubmit3)


                r2.Cells.Add(c4)
                Table1.Rows.Add(r2)

            Else
                DispBox.Text = "No Account found"
                ActState.Value = 0
                PageStatus()


            End If
            'Panel6.Controls.Add(t2)
            'Panel6.Controls.Add(CB)
            DRRec1.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try

    End Sub


    Protected Sub SubContractorSearch_Click()

        Panel2.Visible = True
        Panel1.Visible = False
        Panel3.Visible = False
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            Dim SQLCmd As New SqlCommand("Select * from tblContractor where Contractorname like '%" & TxtSubContractorname.Text & "%' and parentid = '" & Session("ContractorID") & "'", oConn)
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            Dim J As Integer
            J = 0

            If DRRec.HasRows = True Then
                While (DRRec.Read)
                    Dim c As New TableCell
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim r As New TableRow
                    Dim RB As New RadioButton
                    c.Text = DRRec("ContractorName")
                    RB.GroupName = "SubContractorID"
                    RB.ID = DRRec("ContractorID").ToString
                    c3.Controls.Add(RB)
                    J = J + 1
                    RB.Checked = True
                    r.Cells.Add(c3)
                    r.Cells.Add(c)

                    TblSubContractorSeach.Rows.Add(r)

                End While

                'Dim CB As New Button
                Dim r2 As New TableRow
                Dim c4 As New TableCell
                'CB.ID = "Btnsubmit3"
                'CB.Text = "Submit"
                btnSubmit4.Visible = True
                c4.ColumnSpan = 2
                c4.Style("text-align") = "center"
                c4.Controls.Add(btnSubmit4)
                r2.Cells.Add(c4)
                TblSubContractorSeach.Rows.Add(r2)
            Else
                DispBox.Text = "No Subcontractor found"
                SubContractorState.Value = 0
                PageStatus()

            End If
            ' Panel2.Controls.Add(t2)
            ' Panel2.DataBind()

            DRRec.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub


    Protected Sub BtnSubmit1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit1.Click
        SubContractorState.Value = 1
        PageStatus()
    End Sub

    Protected Sub BtnSubmit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit2.Click
        ActState.Value = 1
        PageStatus()
    End Sub

    Protected Sub btnSubmit4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit4.Click
        'HSubContractorActID.Value = ""
        SubContractorState.Value = 2
        'Response.Write(Request("SubContractorActID"))
        HSubcontractorID.Value = Request("SubContractorID")
        PageStatus()
        'Response.Write(HSubContractorActID.Value)
        'Dim CTls As Control
        '        'Response.Write(Request("SubContractorActID"))


        'For Each CTls In Page.FindControl("UserTable").Controls
        '    'Response.Write(CTls.ID)

        '    If TypeOf CTls Is RadioButton Then
        '        'Response.Write(CType(CTls, RadioButton).ID)


        '    End If

        '    'If cTls.Checked Then
        '    '    'Response.Write(cTls.ID)

        '    'End If
        '    '    Dim rdo As RadioButton
        '    '    rdo= cTls.FindControl(
        '    'Dim RB2 As RadioButton

        '    'RB2 = Table1.FindControl("SubContractorActID")
        '    ''Response.Write(RB2.Checked)





        'Next


    End Sub




    Protected Sub BtnSubmit5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit5.Click
        SubContractorState.Value = 3
        PageStatus()


    End Sub

    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit3.Click
        ActState.Value = 2
        Dim i As Integer
        Dim AccountID As String
        Dim RecFound As String
        RecFound = "No"
        ''Response.Write(TotAct.Value)

        For i = 1 To TotAct.Value

            AccountID = "AccountID" & i
            If Request(AccountID) <> "" Then
                RecFound = "Yes"
            End If
            '    '    'Response.Write(AccountID)

            '    '    Response.Write(Request(AccountID))


        Next
        If RecFound = "No" Then
            ActState.Value = 1
        End If
        PageStatus()

    End Sub
    Protected Sub SubContractorActStatus_Click()


        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = True
        'Panel3.Controls.Clear()



        Dim strConn As String
        Dim oRec As Data.SqlClient.SqlDataReader
        Dim oCommand As New Data.SqlClient.SqlCommand
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim sqlString As String
        sqlString = "select * from tblContractor where ContractorID = '" & HSubcontractorID.Value & "'"
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            oCommand = New Data.SqlClient.SqlCommand(sqlString, oConn)
            oRec = oCommand.ExecuteReader()
            'Response.End()
            If oRec.HasRows Then
                If oRec.Read() Then

                    Dim c As New TableCell
                    Dim r As New TableRow
                    Dim c1 As New TableCell
                    c.Text = oRec("ContractorName")
                    r.Cells.Add(c)
                    TblSubcontractorstatus.Rows.Add(r)
                End If
            End If
            oRec.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
    Protected Sub ActStatus_Click()

        Panel7.Visible = True
        Panel5.Visible = False
        Panel6.Visible = False
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both



        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            Dim i As Integer
            Dim AccountID As String
            'Response.Write("Total" & TotAct.Value)
            Dim K As Integer
            K = 0

            For i = 1 To TotAct.Value
                AccountID = "AccountID" & i
                Dim SQuery As String
                If Request(AccountID) <> "" Then
                    SQuery = "Select * from tblaccounts where accountid like '%" & Request(AccountID) & "%' "
                    ' Response.Write(SQuery)
                    Dim SQLCmd1 As New SqlCommand(SQuery, oConn)
                    Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                    If DRRec1.HasRows Then
                        If DRRec1.Read Then
                            K = K + 1
                            Dim c As New TableCell
                            Dim c1 As New TableCell
                            Dim c2 As New TableCell
                            Dim c3 As New TableCell
                            Dim r As New TableRow
                            Dim CB1 As New CheckBox
                            CB1.ID = "AccountID" & K
                            CB1.InputAttributes.Add("Value", DRRec1("AccountID").ToString)
                            CB1.Checked = True

                            'TotAct.Value = K
                            c.Text = DRRec1("Accountname")
                            c1.Text = DRRec1("AccountNO")
                            'c2.Text = DRRec1("PinNo")
                            c3.Controls.Add(CB1)
                            r.Cells.Add(c3)
                            r.Cells.Add(c)
                            r.Cells.Add(c1)
                            'r.Cells.Add(c2)
                            Table3.Rows.Add(r)
                            DRRec1.Close()
                        End If
                    End If

                End If

            Next
            TotAct.Value = K
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub

    Sub PageStatus()
        If SubContractorState.Value = 0 Then
            Loadpage()
        ElseIf SubContractorState.Value = 1 Then
            SubContractorSearch_Click()
        ElseIf SubContractorState.Value = 2 Then
            SubContractorActStatus_Click()
        End If

        If ActState.Value = 0 Then
            Loadpage()
        ElseIf ActState.Value = 1 Then
            ActSearch_Click()
        ElseIf ActState.Value = 2 Then
            ActStatus_Click()
        End If
        If SubContractorState.Value = 2 And ActState.Value = 2 Then
            BtnAssign.Visible = True
        Else
            BtnAssign.Visible = False
        End If

    End Sub

    Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAssign.Click
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False

        Panel5.Visible = False
        Panel6.Visible = False
        Panel7.Visible = False
        Dim strConn As String
        Dim AccountID As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            Dim j As Integer
            For j = 1 To TotAct.Value
                AccountID = "AccountID" & j
                Dim sQuery1 As String
                sQuery1 = "Select * from tblSubCont2Accounts where ContractorID='" & HSubcontractorID.Value & "' and AccountID='" & Request(AccountID) & "'"
                Dim cmdIns As New SqlCommand(sQuery1, oConn)
                Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
                If DRRec.Read = False Then
                    DRRec.Close()
                    Dim sQuery2 As String
                    sQuery2 = "INSERT INTO tblSubCont2Accounts (ContractorID, AccountID, CreateDate)VALUES('" & HSubcontractorID.Value & "','" & Request(AccountID) & "','" & Now & "')"
                    Dim cmdUp As New SqlCommand(sQuery2, oConn)
                    cmdUp.ExecuteNonQuery()
                Else
                End If
                DRRec.Close()
            Next

            Dim sQuery3 As String
            sQuery3 = "Select C.ContractorName, A.Accountname, A.AccountNo from tblSubCont2Accounts SA, tblContractor C, tblAccounts A where SA.AccountID = A.AccountID and SA.ContractorID = C.ContractorID and SA.ContractorID ='" & HSubcontractorID.Value & "' order by A.Accountname"
            Dim cmdSel As New SqlCommand(sQuery3, oConn)
            Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
            'If RdSet.Read = True Then
            While (RdSet.Read)
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow
                c.Text = RdSet("ContractorName")
                c2.Text = RdSet("Accountname")
                c3.Text = RdSet("AccountNO")
                r.Cells.Add(c)
                r.Cells.Add(c2)
                r.Cells.Add(c3)
                Table4.Visible = True
                Table4.Rows.Add(r)
            End While
            RdSet.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub
    Sub Loadpage()
        BtnAssign.Visible = False
      
        If ActState.Value = 0 Then
            Panel6.Visible = False
            Panel7.Visible = False
            Panel5.Visible = True
        End If
        If SubContractorState.Value = 0 Then
            Panel2.Visible = False
            Panel3.Visible = False
            Panel1.Visible = True
        End If


        'SubContractorState.Value = 0
        'ActState.Value = 0
    End Sub
End Class

