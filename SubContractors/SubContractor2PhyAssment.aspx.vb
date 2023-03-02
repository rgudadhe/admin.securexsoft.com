Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       

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
            PhyState.Value = 0
        Else


        End If



    End Sub







    Protected Sub PhySearch_Click()

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
            Dim SQLCmd1 As New SqlCommand("Select * from tblphysicians AS P INNER JOIN tblAccounts AS A ON P.AccountID = A.AccountID where P.PinNo like '" & Txtpname.Text & "' and A.ContractorID='" & Session("ContractorID") & "'", oConn)

            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            Dim i As Integer
            Dim K As Integer



            K = 0
            If DRRec1.HasRows Then
                While (DRRec1.Read)
                    K = K + 1
                    Dim c As New TableCell
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim r As New TableRow
                    Dim CB1 As New CheckBox
                    CB1.ID = "PhyID" & K
                    CB1.InputAttributes.Add("Value", DRRec1("PhysicianId").ToString)
                    TotPhy.Value = K
                    c.Text = IIf(IsDBNull(DRRec1("firstname")), "", DRRec1("firstname").ToString)
                    c1.Text = IIf(IsDBNull(DRRec1("lastname")), "", DRRec1("lastname").ToString)
                    c2.Text = IIf(IsDBNull(DRRec1("PinNo")), "", DRRec1("PinNo").ToString)
                    c3.Controls.Add(CB1)
                    r.Cells.Add(c3)
                    r.Cells.Add(c)
                    r.Cells.Add(c1)
                    r.Cells.Add(c2)
                    Table1.Rows.Add(r)

                End While


                Dim CB As New Button
                Dim r2 As New TableRow
                Dim c4 As New TableCell
                'CB.ID = "Btnsubmit3"
                'CB.Text = "Submit"
                c4.ColumnSpan = 4
                c4.Style("text-align") = "center"
                btnsubmit3.Visible = True

                c4.Controls.Add(btnsubmit3)


                r2.Cells.Add(c4)
                Table1.Rows.Add(r2)

            Else
                DispBox.Text = "No Dictator found"
                PhyState.Value = 0
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
            Dim SQLCmd As New SqlCommand("Select * from tblContractor where Contractorname like '" & TxtSubContractorname.Text & "' and parentid = '" & Session("ContractorID") & "'", oConn)

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


                Dim r2 As New TableRow
                Dim c4 As New TableCell

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
        PhyState.Value = 1
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
        PhyState.Value = 2
        Dim i As Integer
        Dim PhyID As String
        Dim RecFound As String
        RecFound = "No"
        ''Response.Write(TotAct.Value)

        For i = 1 To TotPhy.Value

            PhyID = "PhyID" & i
            If Request(PhyID) <> "" Then
                RecFound = "Yes"
            End If
            '    '    'Response.Write(AccountID)

            '    '    Response.Write(Request(AccountID))


        Next
        If RecFound = "No" Then
            PhyState.Value = 1
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
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            Dim sqlString As String
            sqlString = "select * from tblContractor where ContractorID = '" & HSubcontractorID.Value & "'"
           
            oCommand = New Data.SqlClient.SqlCommand(sqlString, oConn)
            oRec = oCommand.ExecuteReader()
            'Response.End()
            If oRec.Read() Then
                'If oRec.HasRows Then
                Dim c As New TableCell
                Dim r As New TableRow
                Dim c1 As New TableCell
                c.Text = oRec("ContractorName")
                r.Cells.Add(c)
                TblSubcontractorstatus.Rows.Add(r)
                oCommand.Connection.Close()
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
    Protected Sub PhyStatus_Click()

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
            Dim PhyId As String
            'Response.Write("Total" & TotPhy.Value)
            Dim K As Integer
            K = 0

            For i = 1 To TotPhy.Value
                PhyId = "PhyId" & i
                Dim SQuery As String
                If Request(PhyId) <> "" Then
                    SQuery = "Select * from tblphysicians where physicianid like '%" & Request(PhyId) & "%' "
                    ' Response.Write(SQuery)
                    Dim SQLCmd1 As New SqlCommand(SQuery, oConn)

                    Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                    If DRRec1.Read Then
                        K = K + 1
                        Dim c As New TableCell
                        Dim c1 As New TableCell
                        Dim c2 As New TableCell
                        Dim c3 As New TableCell
                        Dim r As New TableRow
                        Dim CB1 As New CheckBox
                        CB1.ID = "PhyID" & K
                        CB1.InputAttributes.Add("Value", DRRec1("PhysicianId").ToString)
                        CB1.Checked = True

                        'TotPhy.Value = K
                        c.Text = DRRec1("firstname")
                        c1.Text = DRRec1("lastname")
                        c2.Text = DRRec1("PinNo")
                        c3.Controls.Add(CB1)
                        r.Cells.Add(c3)
                        r.Cells.Add(c)
                        r.Cells.Add(c1)
                        r.Cells.Add(c2)
                        Table3.Rows.Add(r)
                        DRRec1.Close()
                    End If
                End If

            Next
            TotPhy.Value = K
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

        If PhyState.Value = 0 Then
            Loadpage()
        ElseIf PhyState.Value = 1 Then
            PhySearch_Click()
        ElseIf PhyState.Value = 2 Then
            PhyStatus_Click()
        End If
        If SubContractorState.Value = 2 And PhyState.Value = 2 Then
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
        Dim Lvl As String
        Dim PhyID As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            Dim i As Integer
            Dim j As Integer


            For j = 1 To TotPhy.Value
                PhyID = "phyID" & j

                Dim sQuery1 As String
                sQuery1 = "Select * from tblSubCont2Physicians where ContractorID='" & HSubcontractorID.Value & "' and PhysicianID='" & Request(PhyID) & "'"
                'Response.Write(sQuery1)
                'Response.End()
                Dim cmdIns As New SqlCommand(sQuery1, oConn)
                Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()

                If DRRec.Read = False Then
                    DRRec.Close()
                    Dim sQuery2 As String
                    sQuery2 = "INSERT INTO tblSubCont2Physicians (ContractorID, PhysicianID, CreateDate)VALUES('" & HSubcontractorID.Value & "','" & Request(PhyID) & "','" & Now & "')"
                    'Response.Write(sQuery2)
                    'Response.End()
                    Dim cmdUp As New SqlCommand(sQuery2, oConn)

                    cmdUp.ExecuteNonQuery()
                End If

                If Not DRRec.IsClosed Then
                    DRRec.Close()
                End If
            Next

            Dim sQuery3 As String
            sQuery3 = "Select C.ContractorName, P.firstname + ' ' + P.Lastname as pname, A.Accountname, A.AccountNo, P.PinNo from tblSubCont2Physicians SA, tblContractor C, tblPhysicians P, tblAccounts A where SA.PhysicianID = P.PhysicianID and P.AccountId = A.accountID and SA.ContractorID = C.ContractorID and SA.ContractorID ='" & HSubcontractorID.Value & "' order by A.Accountname"
            'Response.Write(sQuery3)
            'Response.End()
            Dim cmdSel As New SqlCommand(sQuery3, oConn)
            Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
            If RdSet.HasRows Then
                While (RdSet.Read)
                    Dim c As New TableCell
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim c4 As New TableCell
                    Dim r As New TableRow

                    c.Text = RdSet("ContractorName")
                    c2.Text = RdSet("pname")
                    c3.Text = RdSet("PinNO")
                    c4.Text = RdSet("AccountName")
                    r.Cells.Add(c)
                    r.Cells.Add(c2)
                    r.Cells.Add(c3)
                    r.Cells.Add(c4)
                    Table4.Visible = True

                    Table4.Rows.Add(r)

                    'Response.Write(RdSet("uName") & RdSet("LevelName") & RdSet("AccountName") & RdSet("pName"))
                End While
            End If
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

        If PhyState.Value = 0 Then
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

