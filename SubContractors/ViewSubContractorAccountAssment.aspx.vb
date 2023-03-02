Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Response.Write(PrdState.Value)
        MsgDisp.Visible = False


        Table4.Visible = False

        If Not IsPostBack Then



            Panel2.Visible = False
            Panel1.Visible = True
            PrdState.Value = 0

        Else

     
        End If



    End Sub









    Protected Sub UserSearch_Click()

        Panel2.Visible = True
        Panel1.Visible = False




        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            Dim SQLCmd As New SqlCommand("Select * from tblcontractor where ParentID ='" & Session("ContractorID") & "' and ContractorName like '" & TxtUname.Text & "'", oConn)
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            Dim i As Integer
            Dim J As Integer
            J = 0

            If DRRec.HasRows Then
                While (DRRec.Read)
                    Dim c As New TableCell
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim r As New TableRow
                    Dim RB As New RadioButton

                    c.Text = DRRec("contractorname")
                    c1.Text = DRRec("Description")
                    'c2.Text = DRRec("username")
                    form1.Controls.Add(RB)
                    RB.GroupName = "ContrID"
                    RB.ID = DRRec("Contractorid").ToString
                    c3.Controls.Add(RB)
                    J = J + 1
                    RB.Checked = True
                    r.Cells.Add(c3)
                    r.Cells.Add(c)
                    r.Cells.Add(c1)
                    'r.Cells.Add(c2)
                    't2.Rows.Add(r)
                    UserTable.Rows.Add(r)

                End While

                'Dim CB As New Button
                Dim r2 As New TableRow
                Dim c4 As New TableCell
                'CB.ID = "Btnsubmit3"
                'CB.Text = "Submit"
                btnSubmit4.Visible = True
                c4.ColumnSpan = 4
                c4.Style("text-align") = "center"
                c4.Controls.Add(btnSubmit4)
                r2.Cells.Add(c4)
                UserTable.Rows.Add(r2)

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
        PrdState.Value = 1
        PageStatus()
    End Sub


    Protected Sub btnSubmit4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit4.Click
        'HContrID.Value = ""
        PrdState.Value = 2
        'Response.Write(Request("ContrID"))
        HContrID.Value = Request("ContrID")
        PageStatus()
        'Response.Write(HContrID.Value)
        'Dim CTls As Control
        '        'Response.Write(Request("ContrID"))


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

        '    'RB2 = Table1.FindControl("ContrID")
        '    ''Response.Write(RB2.Checked)





        'Next


    End Sub
    
    Protected Function chkLevel(ByVal AdminLevel As Long, ByVal Level As Long) As Boolean
        If (AdminLevel And Level) = Level Then
            chkLevel = True
        Else
            chkLevel = False
        End If
    End Function


    Protected Sub BtnSubmit5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit5.Click
        PrdState.Value = 3
        PageStatus()


    End Sub

   
    
    

    Sub PageStatus()
        If PrdState.Value = 1 Then
            UserSearch_Click()
        ElseIf PrdState.Value = 2 Then
            '    UserStatus_Click()
            'ElseIf PrdState.Value = 3 Then
            ActUserAssign()
        End If

        
    End Sub

    Protected Sub ActUserAssign()
        Panel1.Visible = False
        Panel2.Visible = False

        Dim strConn As String


        
        Dim i As Integer
        Dim j As Integer

        Dim sQuery3 As String
        If HcontrID.Value <> "" Then
            sQuery3 = "Select CU.TrackID, C.ContractorName, C.Description , A.AccountName, A.AccountNO from tblcontractor C, tblSubCont2Accounts CU, tblAccounts A where CU.AccountID = A.AccountID and CU.ContractorID = C.ContractorID and C.ContractorID ='" & HcontrID.Value & "' order by C.ContractorName"
            'Response.Write(sQuery3)
            'Response.End()
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()
                Dim cmdSel As New SqlCommand(sQuery3, oConn)
                Dim RdSet As SqlDataReader = cmdSel.ExecuteReader()
                'If RdSet.Read = True Then
                While (RdSet.Read)
                    Dim c As New TableCell
                    Dim c1 As New TableCell
                    Dim c2 As New TableCell
                    Dim c3 As New TableCell
                    Dim c4 As New TableCell
                    Dim r As New TableRow

                    c.Text = RdSet("Contractorname")
                    c1.Text = RdSet("Description")
                    c2.Text = RdSet("Accountname")
                    c3.Text = RdSet("AccountNo")
                    'c4.Text = "<img src=remove.gif style='cursor:hand;' onclick=poptastic('" & RdSet("TrackID").ToString & "')>"
                    c4.HorizontalAlign = HorizontalAlign.Center
                    c4.Text = "<input id=""Button1"" type=""button"" style='cursor:hand;' value=""Remove"" class=""button"" onclick=poptastic('" & RdSet("TrackID").ToString & "') />"
                    r.Cells.Add(c)
                    r.Cells.Add(c1)
                    r.Cells.Add(c2)
                    r.Cells.Add(c3)
                    r.Cells.Add(c4)
                    Table4.Visible = True

                    Table4.Rows.Add(r)

                    'Response.Write(RdSet("uName") & RdSet("LevelName") & RdSet("AccountName") & RdSet("pName"))
                End While
                'End If
                RdSet.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try

        Else
            Dim c3 As New TableCell
            Dim r As New TableRow
            c3.ColumnSpan = 4
            c3.Text = "No Records Found"
            r.Cells.Add(c3)
            Table4.Visible = True

            Table4.Rows.Add(r)

        End If

    End Sub
End Class

