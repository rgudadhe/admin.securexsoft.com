Imports System.Data.SqlClient
Imports System.Data
Imports ets.DAL.ETS.DAL
Imports ETS.BL

Partial Class UpUnitDetails
    Inherits BasePage




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        TableCell13.Visible = False

        If Not IsPostBack Then

            Dim objAct As New ETS.BL.Accounts
            With objAct
                .ContractorID = Session("contractorID")
            End With
            Dim DTSet1 As System.Data.DataSet = objAct.getAccountList
            If DTSet1.Tables.Count > 0 Then
                If DTSet1.Tables(0).Rows.Count > 0 Then
                    Dim DV As New Data.DataView(DTSet1.Tables(0))
                    DV.Sort = " AccountName Asc"
                    DLAct.DataSource = DV
                    DLAct.DataTextField = "AccountName"
                    DLAct.DataValueField = "AccountID"
                    DLAct.DataBind()
                Else
                    Response.Write("No account has been created to update unit details.")
                    Response.End()

                End If

            Else
                Response.Write("No account has been created.")
                Response.End()

            End If
            If Request("ActID") <> "" Then
                For I As Integer = 0 To DLAct.Items.Count - 1
                    'Response.Write(DLAct.Items(I).Value.ToLower)
                    If DLAct.Items(I).Value.ToLower = Request("ActID").ToLower Then
                        'Response.Write(DLAct.Items(I).Value & Request("ActID"))
                        DLAct.Items(I).Selected = True
                        Exit For
                    End If
                Next
                Table1.Visible = True
                Table3.Visible = True
                Table4.Visible = False
                SubButton.Visible = True
                ShowDetails()
            Else

                Table1.Visible = False
                Table3.Visible = False
                Table4.Visible = False
                SubButton.Visible = False
            End If
            objAct = Nothing

           
        Else
            Table1.Visible = True
            Table3.Visible = True
            SubButton.Visible = True

        End If


    End Sub

    Protected Sub LCMethods(ByVal DLLCMethod As DropDownList)
        Dim objCountMethod As New ETS.BL.LCMethodlogy
        Dim DTSet2 As System.Data.DataSet = objCountMethod.getCountMethodList
        If DTSet2.Tables.Count > 0 Then
            DLLCMethod.DataSource = DTSet2.Tables(0)
            DLLCMethod.DataTextField = "MethodName"
            DLLCMethod.DataValueField = "TrackID"
            DLLCMethod.DataBind()
        End If
        objCountMethod = Nothing
    End Sub
    Protected Sub DLAct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLAct.SelectedIndexChanged
        If DLAct.Items(0).Value = "" And DLAct.Items(0).Text = "Any" Then
            DLAct.Items.RemoveAt(0)
        End If
        ShowDetails()
    End Sub

    Protected Sub ShowDetails()
        WTRow.Visible = False
        Dim objAct As New ETS.BL.Accounts
        objAct.AccountID = DLAct.SelectedValue
        Dim DTSet1 As System.Data.DataSet = objAct.getAccountList
        If DTSet1.Tables.Count > 0 Then
            For Each DRRec As Data.DataRow In DTSet1.Tables(0).Rows
                HMode.Value = Trim(DRRec("Mode").ToString)
                HAccountID.Value = DRRec("AccountID").ToString
                If IsDBNull(DRRec("MinBilling")) Then
                    TxMBilling.Text = ""
                Else
                    TxMBilling.Text = FormatNumber(DRRec("MinBilling").ToString, 6)
                End If
                If IsDBNull(DRRec("Cycle")) Then
                    DLCycle.Items(0).Selected = False
                    DLCycle.Items(1).Selected = True
                ElseIf DRRec("Cycle") = "True" Then
                    DLCycle.Items(1).Selected = False
                    DLCycle.Items(0).Selected = True
                Else
                    DLCycle.Items(0).Selected = False
                    DLCycle.Items(1).Selected = True
                End If
                'Response.Write("Mode " & DRRec("Mode").ToString)
                'Response.End()

                If HMode.Value = "S" Or HMode.Value = "" Then
                    WTRow.Visible = True
                    'Dim SQLCmd1 As New SqlCommand("Select Top 1 * from SecureWeb.dbo.BillDetails where AccountID='" & DLAct.SelectedValue & "' order by ModDate Desc", New SqlConnection(strConn))
                    Dim objBillDetails As New ETS.BL.BillDetails
                    objBillDetails.AccountID = DLAct.SelectedValue
                    Dim DTSet2 As System.Data.DataSet = objBillDetails.getActBillDetailsList
                   
                    If DTSet2.Tables.Count > 0 Then
                        HRecExist.Value = "Yes"
                        If DTSet2.Tables(0).Rows.Count > 0 Then
                            'Response.Write(DTSet2.Tables(0).Rows.Count & "#" & DLAct.SelectedValue)
                            'Response.End()

                            Dim DRRec1 As Data.DataRow = DTSet2.Tables(0).Rows(0)
                            If IsDBNull(DRRec1("WTMode")) Or DRRec1("WTMode").ToString.ToLower = "false" Then
                                DLWType.Items(0).Selected = False
                                DLWType.Items(1).Selected = True
                                Table1.Visible = True
                                Table4.Visible = False
                                Dim Row1 As New TableRow
                                Dim Cell1 As New TableCell
                                Dim Cell2 As New TableCell
                                Dim Cell3 As New TableCell
                                Dim Cell4 As New TableCell
                                Dim Cell5 As New TableCell
                                Dim DDLC As New DropDownList
                                DDLC.ID = "DLMethodID1"
                                LCMethods(DDLC)
                                Dim X As Integer
                                For X = 0 To DDLC.Items.Count - 1
                                    If DRRec1("LCMethodID").ToString = DDLC.Items(X).Value Then
                                        DDLC.Items(X).Selected = True
                                        Exit For
                                    End If
                                Next
                                Cell5.Controls.Add(DDLC)
                                Row1.Font.Size = "8"
                                Dim HF1 As New HiddenField
                                HF1.ID = "HActID1"
                                HF1.Value = DRRec("AccountID").ToString
                                Form.Controls.Add(HF1)
                                Dim HM1 As New HiddenField
                                HM1.ID = "tblActMode1"
                                HM1.Value = DRRec("Mode").ToString
                                Dim Lbl As New Label
                                Lbl.ID = "lblAct1"
                                Lbl.Font.Name = "Arial"
                                Lbl.Text = DRRec("AccountName").ToString
                                Dim Txt1 As New TextBox
                                Dim Txt2 As New TextBox
                                Dim Txt3 As New TextBox
                                Txt1.ID = "TxRate1"
                                ' Txt1.Text = DRRec1("Rate")
                                Txt2.ID = "TxMRate1"
                                ' Txt2.Text = DRRec1("MiscRate")
                                Txt3.ID = "TxSTRate1"
                                Txt1.Width = "50"
                                Txt2.Width = "50"
                                Txt3.Width = "50"
                                Txt1.Font.Size = "8"
                                Txt1.Font.Name = "Arial"
                                Txt2.Font.Size = "8"
                                Txt2.Font.Name = "Arial"
                                Txt3.Font.Size = "8"
                                Txt3.Font.Name = "Arial"
                                If IsDBNull(DRRec1("Rate")) Then
                                    Txt1.Text = ""
                                Else
                                    Txt1.Text = FormatNumber(DRRec1("Rate").ToString, 6)
                                End If
                                If IsDBNull(DRRec1("MiscRate")) Then
                                    Txt2.Text = ""
                                Else
                                    Txt2.Text = FormatNumber(DRRec1("MiscRate").ToString, 6)
                                End If
                                If IsDBNull(DRRec1("StatRate")) Then
                                    Txt3.Text = ""
                                Else
                                    Txt3.Text = FormatNumber(DRRec1("StatRate").ToString, 6)
                                End If

                                Dim SelMonth As Integer
                                SelMonth = 0
                                Dim SelYear As Integer
                                SelYear = 0


                                Cell1.Controls.Add(Lbl)
                                Cell2.Controls.Add(Txt1)
                                Cell3.Controls.Add(Txt2)
                                Cell4.Controls.Add(Txt3)
                                Row1.Cells.Add(Cell1)
                                Row1.Cells.Add(Cell2)
                                'Row1.Cells.Add(Cell3)
                                Row1.Cells.Add(Cell4)
                                Row1.Cells.Add(Cell5)
                                Table1.Rows.Add(Row1)
                            Else
                                Table4.Visible = True
                                Table1.Visible = False
                                DLWType.Items(1).Selected = False
                                DLWType.Items(0).Selected = True
                                Consult.Text = DRRec1("Consult").ToString
                                HP.Text = DRRec1("HP").ToString
                                Discharge.Text = DRRec1("Discharge").ToString
                                IME.Text = DRRec1("IME").ToString
                                Letter.Text = DRRec1("Letter").ToString
                                PrNote.Text = DRRec1("PrNote").ToString
                                OpNote.Text = DRRec1("OpNote").ToString
                                PsychEval.Text = DRRec1("PsychEval").ToString
                                Rate.Text = DRRec1("Rate").ToString
                                StatRate.Text = DRRec1("statrate").ToString
                                LCMethods(DLDefLC)
                                Dim X As Integer
                                For X = 0 To DLDefLC.Items.Count - 1
                                    If DRRec1("LCMethodID").ToString = DLDefLC.Items(X).Value Then
                                        DLDefLC.Items(X).Selected = True
                                        Exit For
                                    End If
                                Next
                            End If
                            ' Next
                        Else
                            DLWType.Items(0).Selected = False
                            DLWType.Items(1).Selected = True
                            Table1.Visible = True
                            Table4.Visible = False
                            HRecExist.Value = "No"
                            Dim Row1 As New TableRow
                            Dim Cell1 As New TableCell
                            Dim Cell2 As New TableCell
                            Dim Cell3 As New TableCell
                            Dim Cell4 As New TableCell
                            Dim Cell5 As New TableCell
                            Dim Cell6 As New TableCell
                            Dim Cell7 As New TableCell
                            Dim DDLC As New DropDownList
                            DDLC.Font.Size = 8
                            DDLC.Font.Name = "Arial"
                            DDLC.ID = "DLMethodID1"
                            LCMethods(DDLC)
                            DDLC.Items(0).Selected = True
                            Cell5.Controls.Add(DDLC)
                            Dim Lbl As New Label
                            Row1.Font.Size = "8"
                            Lbl.Font.Name = "Arial"
                            Lbl.ID = "lblAct1"
                            Lbl.Text = DRRec("AccountName").ToString
                            'Response.Write(DRRec("AccountName").ToString)

                            Dim Txt1 As New TextBox
                            Dim Txt2 As New TextBox
                            Dim Txt3 As New TextBox
                            Dim HF1 As New HiddenField
                            HF1.ID = "HActID1"
                            HF1.Value = DRRec("AccountID").ToString
                            Form.Controls.Add(HF1)
                            Dim HM1 As New HiddenField
                            HM1.ID = "tblActMode1"
                            HM1.Value = DRRec("Mode").ToString
                            ' Txt1.Style("
                            Txt1.Width = "50"
                            Txt2.Width = "50"
                            Txt3.Width = "50"
                            Txt1.ID = "TxRate1"
                            Txt1.Text = ""
                            Txt2.ID = "TxMRate1"
                            Txt2.Text = ""
                            Txt3.ID = "TxSTRate1"
                            Txt3.Text = ""
                            Txt1.Font.Size = "8"
                            Txt1.Font.Name = "Arial"
                            Txt2.Font.Size = "8"
                            Txt2.Font.Name = "Arial"
                            Txt3.Font.Size = "8"
                            Txt3.Font.Name = "Arial"
                            Cell1.Controls.Add(Lbl)
                            Cell2.Controls.Add(Txt1)
                            Cell3.Controls.Add(Txt2)
                            Cell4.Controls.Add(Txt3)
                            Row1.Cells.Add(Cell1)
                            Row1.Cells.Add(Cell2)
                            'Row1.Cells.Add(Cell3)
                            Row1.Cells.Add(Cell4)
                            Row1.Cells.Add(Cell5)
                            Table1.Rows.Add(Row1)
                        End If
                    Else
                        DLWType.Items(0).Selected = False
                        DLWType.Items(1).Selected = True
                        Table1.Visible = True
                        Table4.Visible = False
                        HRecExist.Value = "No"
                        Dim Row1 As New TableRow
                        Dim Cell1 As New TableCell
                        Dim Cell2 As New TableCell
                        Dim Cell3 As New TableCell
                        Dim Cell4 As New TableCell
                        Dim Cell5 As New TableCell
                        Dim Cell6 As New TableCell
                        Dim Cell7 As New TableCell
                        Dim DDLC As New DropDownList
                        DDLC.Font.Size = 8
                        DDLC.Font.Name = "Arial"
                        DDLC.ID = "DLMethodID1"
                        LCMethods(DDLC)
                        DDLC.Items(0).Selected = True
                        Cell5.Controls.Add(DDLC)
                        Dim Lbl As New Label
                        Row1.Font.Size = "8"
                        Lbl.Font.Name = "Arial"
                        Lbl.ID = "lblAct1"
                        Lbl.Text = DRRec("AccountName").ToString
                        'Response.Write(DRRec("AccountName").ToString)

                        Dim Txt1 As New TextBox
                        Dim Txt2 As New TextBox
                        Dim Txt3 As New TextBox
                        Dim HF1 As New HiddenField
                        HF1.ID = "HActID1"
                        HF1.Value = DRRec("AccountID").ToString
                        Form.Controls.Add(HF1)
                        Dim HM1 As New HiddenField
                        HM1.ID = "tblActMode1"
                        HM1.Value = DRRec("Mode").ToString
                        ' Txt1.Style("
                        Txt1.Width = "50"
                        Txt2.Width = "50"
                        Txt3.Width = "50"
                        Txt1.ID = "TxRate1"
                        Txt1.Text = ""
                        Txt2.ID = "TxMRate1"
                        Txt2.Text = ""
                        Txt3.ID = "TxSTRate1"
                        Txt3.Text = ""
                        Txt1.Font.Size = "8"
                        Txt1.Font.Name = "Arial"
                        Txt2.Font.Size = "8"
                        Txt2.Font.Name = "Arial"
                        Txt3.Font.Size = "8"
                        Txt3.Font.Name = "Arial"
                        Cell1.Controls.Add(Lbl)
                        Cell2.Controls.Add(Txt1)
                        Cell3.Controls.Add(Txt2)
                        Cell4.Controls.Add(Txt3)
                        Row1.Cells.Add(Cell1)
                        Row1.Cells.Add(Cell2)
                        'Row1.Cells.Add(Cell3)
                        Row1.Cells.Add(Cell4)
                        Row1.Cells.Add(Cell5)
                        Table1.Rows.Add(Row1)
                    End If
                    DTSet2.Dispose()
                    objBillDetails = Nothing
                ElseIf HMode.Value = "DV" Then
                    Dim k As Integer = 0
                    TableCell13.Visible = True
                    Dim objBillDetails As New ETS.BL.BillDetails
                    objBillDetails.AccountID = DLAct.SelectedValue
                    Dim DTSet2 As System.Data.DataSet = objBillDetails.getActBillDetailsList
                    If DTSet2.Tables.Count > 0 Then
                        HRecExist.Value = "Yes"
                        If DTSet2.Tables(0).Rows.Count > 0 Then
                            'Response.Write(DTSet2.Tables(0).Rows.Count)
                            'Response.End()

                            For Each DRRec1 As Data.DataRow In DTSet2.Tables(0).Rows
                                k = k + 1
                                Dim Row1 As New TableRow
                                Dim Cell1 As New TableCell
                                Dim Cell2 As New TableCell
                                Dim Cell3 As New TableCell
                                Dim Cell4 As New TableCell
                                Dim Cell5 As New TableCell
                                Dim DDLC As New DropDownList
                                DDLC.ID = "DLMethodID1"
                                LCMethods(DDLC)
                                Dim X As Integer
                                For X = 0 To DDLC.Items.Count - 1
                                    If DRRec1("LCMethodID").ToString = DDLC.Items(X).Value Then
                                        DDLC.Items(X).Selected = True
                                        Exit For
                                    End If
                                Next
                                Cell5.Controls.Add(DDLC)

                                Row1.Font.Size = "8"

                                Dim HF1 As New HiddenField
                                HF1.ID = "HActID" & k
                                HF1.Value = DRRec("AccountID").ToString
                                Form.Controls.Add(HF1)
                                Dim HM1 As New HiddenField
                                HM1.ID = "tblActMode1"
                                HM1.Value = DRRec("Mode").ToString

                                Dim Lbl As New Label
                                Lbl.Font.Name = "Arial"
                                Lbl.ID = "lblAct" & k
                                Lbl.Text = DRRec("AccountName").ToString
                                Dim Txt1 As New TextBox
                                Dim Txt2 As New TextBox
                                Dim Txt3 As New TextBox
                                Txt1.ID = "TxRate1"
                                ' Txt1.Text = DRRec1("Rate")
                                Txt2.ID = "TxMRate1"
                                ' Txt2.Text = DRRec1("MiscRate")
                                Txt3.ID = "TxSTRate1"
                                Txt1.Width = "50"
                                Txt2.Width = "50"
                                Txt3.Width = "50"
                                Txt1.Font.Size = "8"
                                Txt1.Font.Name = "Arial"
                                Txt2.Font.Size = "8"
                                Txt2.Font.Name = "Arial"
                                Txt3.Font.Size = "8"
                                Txt3.Font.Name = "Arial"
                                If IsDBNull(DRRec1("Rate")) Then
                                    Txt1.Text = ""
                                Else
                                    Txt1.Text = FormatNumber(DRRec1("Rate").ToString, 6)
                                End If
                                If IsDBNull(DRRec1("MiscRate")) Then
                                    Txt2.Text = ""
                                Else
                                    Txt2.Text = FormatNumber(DRRec1("MiscRate").ToString, 6)
                                End If
                                If IsDBNull(DRRec1("StatRate")) Then
                                    Txt3.Text = ""
                                Else
                                    Txt3.Text = FormatNumber(DRRec1("StatRate").ToString, 6)
                                End If

                                Dim SelMonth As Integer
                                SelMonth = 0
                                Dim SelYear As Integer
                                SelYear = 0


                                'Cell1.Controls.Add(Lbl)
                                Cell2.Controls.Add(Txt1)
                                Cell3.Controls.Add(Txt2)
                                Cell4.Controls.Add(Txt3)
                                Row1.Cells.Add(Cell1)
                                Row1.Cells.Add(Cell2)
                                Row1.Cells.Add(Cell3)
                                Row1.Cells.Add(Cell4)
                                Row1.Cells.Add(Cell5)
                                Table1.Rows.Add(Row1)

                            Next
                        Else
                            HRecExist.Value = "No"
                            Dim Row1 As New TableRow
                            Dim Cell1 As New TableCell
                            Dim Cell2 As New TableCell
                            Dim Cell3 As New TableCell
                            Dim Cell4 As New TableCell
                            Dim Cell5 As New TableCell
                            Dim Cell6 As New TableCell
                            Dim Cell7 As New TableCell
                            Dim DDLC As New DropDownList
                            DDLC.ID = "DLMethodID1"
                            LCMethods(DDLC)
                            DDLC.Items(0).Selected = True
                            Cell5.Controls.Add(DDLC)
                            Dim Lbl As New Label
                            Row1.Font.Size = "8"
                            Lbl.Font.Name = "Arial"
                            Lbl.ID = "lblAct1"
                            Lbl.Text = DRRec("AccountName").ToString
                            'Response.Write(DRRec("AccountName").ToString)

                            Dim Txt1 As New TextBox
                            Dim Txt2 As New TextBox
                            Dim Txt3 As New TextBox
                            Dim HF1 As New HiddenField
                            HF1.ID = "HActID1"
                            HF1.Value = DRRec("AccountID").ToString
                            Form.Controls.Add(HF1)
                            Dim HM1 As New HiddenField
                            HM1.ID = "tblActMode1"
                            HM1.Value = DRRec("Mode").ToString
                            ' Txt1.Style("
                            Txt1.Width = "50"
                            Txt2.Width = "50"
                            Txt3.Width = "50"
                            Txt1.ID = "TxRate1"
                            Txt1.Text = ""
                            Txt2.ID = "TxMRate1"
                            Txt2.Text = ""
                            Txt3.ID = "TxSTRate1"
                            Txt3.Text = ""
                            Txt1.Font.Size = "8"
                            Txt1.Font.Name = "Arial"
                            Txt2.Font.Size = "8"
                            Txt2.Font.Name = "Arial"
                            Txt3.Font.Size = "8"
                            Txt3.Font.Name = "Arial"
                            Cell1.Controls.Add(Lbl)
                            Cell2.Controls.Add(Txt1)
                            Cell3.Controls.Add(Txt2)
                            Cell4.Controls.Add(Txt3)
                            Row1.Cells.Add(Cell1)
                            Row1.Cells.Add(Cell2)
                            Row1.Cells.Add(Cell3)
                            Row1.Cells.Add(Cell4)
                            Row1.Cells.Add(Cell5)
                            Table1.Rows.Add(Row1)
                        End If
                    End If

                ElseIf HMode.Value = "LC" Then
                    Dim k As Integer
                    k = 0
                    Dim objActLoc As New ETS.BL.AccountsLocations
                    objActLoc.AccountID = DLAct.SelectedValue
                    Dim DT As DataSet = objActLoc.getAcountsLocationList
                    If DT.Tables.Count > 0 Then
                        For Each DRRec2 As DataRow In DT.Tables(0).Rows
                            k = k + 1
                            Dim objBillDetails As New ETS.BL.BillDetails
                            objBillDetails.AccountID = DLAct.SelectedValue
                            objBillDetails.SubActID = DRRec2("TrackID").ToString
                            Dim DTSet2 As System.Data.DataSet = objBillDetails.getActBillDetailsList
                            If DTSet2.Tables.Count > 0 Then
                                HRecExist.Value = "Yes"
                                If DTSet2.Tables(0).Rows.Count > 0 Then
                                    For Each DRRec3 As Data.DataRow In DTSet2.Tables(0).Rows
                                        Dim LRow1 As New TableRow
                                        Dim LCell1 As New TableCell
                                        Dim LCell2 As New TableCell
                                        Dim LCell3 As New TableCell
                                        Dim LCell4 As New TableCell
                                        Dim LCell5 As New TableCell
                                        Dim DDLC As New DropDownList
                                        DDLC.ID = "DLMethodID" & k
                                        LCMethods(DDLC)
                                        Dim X As Integer
                                        For X = 0 To DDLC.Items.Count - 1
                                            If DRRec3("LCMethodID").ToString = DDLC.Items(X).Value Then
                                                DDLC.Items(X).Selected = True
                                                Exit For
                                            End If
                                        Next
                                        LCell5.Controls.Add(DDLC)
                                        LRow1.Font.Size = "8"
                                        'LRow1.CssClass = "DEMO6"
                                        Dim LHF1 As New HiddenField
                                        LHF1.ID = "HActID" & k
                                        LHF1.Value = DRRec2("TrackID").ToString
                                        Form.Controls.Add(LHF1)
                                        Dim LLbl As New Label
                                        LLbl.Font.Name = "Arial"
                                        LLbl.ID = "lblAct" & k
                                        LLbl.Text = DRRec2("LocName").ToString
                                        Dim LTxt1 As New TextBox
                                        Dim LTxt2 As New TextBox
                                        Dim LTxt3 As New TextBox
                                        LTxt1.ID = "TxRate" & k
                                        ' Txt1.Text = DRRec1("Rate")
                                        LTxt2.ID = "TxMRate" & k
                                        ' Txt2.Text = DRRec1("MiscRate")
                                        LTxt3.ID = "TxSTRate" & k
                                        LTxt1.Width = "50"
                                        LTxt2.Width = "50"
                                        LTxt3.Width = "50"
                                        LTxt1.Font.Size = "8"
                                        LTxt1.Font.Name = "Arial"
                                        LTxt2.Font.Size = "8"
                                        LTxt2.Font.Name = "Arial"
                                        LTxt3.Font.Size = "8"
                                        LTxt3.Font.Name = "Arial"
                                        If IsDBNull(DRRec3("Rate")) Then
                                            LTxt1.Text = ""
                                        Else
                                            LTxt1.Text = FormatNumber(DRRec3("Rate").ToString, 6)
                                        End If
                                        If IsDBNull(DRRec3("MiscRate")) Then
                                            LTxt2.Text = ""
                                        Else
                                            LTxt2.Text = FormatNumber(DRRec3("MiscRate").ToString, 6)
                                        End If
                                        If IsDBNull(DRRec3("StatRate")) Then
                                            LTxt3.Text = ""
                                        Else
                                            LTxt3.Text = FormatNumber(DRRec3("StatRate").ToString, 6)
                                        End If


                                        LCell1.Controls.Add(LLbl)
                                        LCell2.Controls.Add(LTxt1)
                                        LCell3.Controls.Add(LTxt2)
                                        LCell4.Controls.Add(LTxt3)
                                        LRow1.Cells.Add(LCell1)
                                        LRow1.Cells.Add(LCell2)
                                        'LRow1.Cells.Add(LCell3)
                                        LRow1.Cells.Add(LCell4)
                                        LRow1.Cells.Add(LCell5)
                                        Table1.Rows.Add(LRow1)
                                    Next
                                Else
                                    HRecExist.Value = "No"
                                    Dim LRow1 As New TableRow
                                    Dim LCell1 As New TableCell
                                    Dim LCell2 As New TableCell
                                    Dim LCell3 As New TableCell
                                    Dim LCell4 As New TableCell
                                    Dim LCell5 As New TableCell
                                    Dim LCell6 As New TableCell
                                    Dim LCell7 As New TableCell
                                    Dim DDLC As New DropDownList
                                    DDLC.ID = "DLMethodID" & k
                                    LCMethods(DDLC)
                                    DDLC.Items(0).Selected = True
                                    LCell5.Controls.Add(DDLC)
                                    LRow1.Font.Size = "8"
                                    Dim LLbl As New Label
                                    ' LRow1.CssClass = "DEMO6"
                                    LLbl.Font.Name = "Arial"
                                    LLbl.ID = "lblAct" & k
                                    LLbl.Text = DRRec2("LocName").ToString
                                    Dim LTxt1 As New TextBox
                                    Dim LTxt2 As New TextBox
                                    Dim LTxt3 As New TextBox
                                    Dim LHF1 As New HiddenField
                                    LHF1.ID = "HActID" & k
                                    LHF1.Value = DRRec2("TrackID").ToString
                                    Form.Controls.Add(LHF1)

                                    ' Txt1.Style("
                                    LTxt1.Width = "50"
                                    LTxt2.Width = "50"
                                    LTxt3.Width = "50"
                                    LTxt1.ID = "TxRate" & k
                                    LTxt1.Text = ""
                                    LTxt2.ID = "TxMRate" & k
                                    LTxt2.Text = ""
                                    LTxt3.ID = "TxSTRate" & k
                                    LTxt3.Text = ""
                                    LTxt1.Font.Size = "8"
                                    LTxt1.Font.Name = "Arial"
                                    LTxt2.Font.Size = "8"
                                    LTxt2.Font.Name = "Arial"
                                    LTxt3.Font.Size = "8"
                                    LTxt3.Font.Name = "Arial"
                                    LCell1.Controls.Add(LLbl)
                                    LCell2.Controls.Add(LTxt1)
                                    LCell3.Controls.Add(LTxt2)
                                    LCell4.Controls.Add(LTxt3)
                                    LRow1.Cells.Add(LCell1)
                                    LRow1.Cells.Add(LCell2)
                                    'LRow1.Cells.Add(LCell3)
                                    LRow1.Cells.Add(LCell4)
                                    LRow1.Cells.Add(LCell5)
                                    Table1.Rows.Add(LRow1)
                                End If
                            End If
                        Next
                        HRecCount.Value = k
                    End If
                ElseIf HMode.Value = "TT" Then
                    Dim k As Integer
                    k = 0
                    Dim objActLoc As New ETS.BL.AccountsTAT
                    objActLoc.AccountID = DLAct.SelectedValue
                    Dim DT As DataSet = objActLoc.getAcountsTATList
                    If DT.Tables.Count > 0 Then
                        For Each DRRec2 As DataRow In DT.Tables(0).Rows
                            k = k + 1
                            Dim objBillDetails As New ETS.BL.BillDetails
                            objBillDetails.AccountID = DLAct.SelectedValue
                            objBillDetails.SubActID = DRRec2("TrackID").ToString
                            Dim DTSet2 As System.Data.DataSet = objBillDetails.getActBillDetailsList
                            If DTSet2.Tables.Count > 0 Then
                                HRecExist.Value = "Yes"
                                If DTSet2.Tables(0).Rows.Count > 0 Then
                                    For Each DRRec3 As Data.DataRow In DTSet2.Tables(0).Rows
                                        Dim LRow1 As New TableRow
                                        Dim LCell1 As New TableCell
                                        Dim LCell2 As New TableCell
                                        Dim LCell3 As New TableCell
                                        Dim LCell4 As New TableCell
                                        Dim LCell5 As New TableCell
                                        Dim DDLC As New DropDownList
                                        DDLC.ID = "DLMethodID" & k
                                        LCMethods(DDLC)
                                        Dim X As Integer
                                        For X = 0 To DDLC.Items.Count - 1
                                            If DRRec3("LCMethodID").ToString = DDLC.Items(X).Value Then
                                                DDLC.Items(X).Selected = True
                                                Exit For
                                            End If
                                        Next
                                        LCell5.Controls.Add(DDLC)
                                        LRow1.Font.Size = "8"
                                        'LRow1.CssClass = "DEMO6"
                                        Dim LHF1 As New HiddenField
                                        LHF1.ID = "HActID" & k
                                        LHF1.Value = DRRec2("TrackID").ToString
                                        Form.Controls.Add(LHF1)
                                        Dim LLbl As New Label
                                        LLbl.Font.Name = "Arial"
                                        LLbl.ID = "lblAct" & k
                                        LLbl.Text = DRRec2("TAT").ToString & " hrs TAT"
                                        Dim LTxt1 As New TextBox
                                        Dim LTxt2 As New TextBox
                                        Dim LTxt3 As New TextBox
                                        LTxt1.ID = "TxRate" & k
                                        ' Txt1.Text = DRRec1("Rate")
                                        LTxt2.ID = "TxMRate" & k
                                        ' Txt2.Text = DRRec1("MiscRate")
                                        LTxt3.ID = "TxSTRate" & k
                                        LTxt1.Width = "50"
                                        LTxt2.Width = "50"
                                        LTxt3.Width = "50"
                                        LTxt1.Font.Size = "8"
                                        LTxt1.Font.Name = "Arial"
                                        LTxt2.Font.Size = "8"
                                        LTxt2.Font.Name = "Arial"
                                        LTxt3.Font.Size = "8"
                                        LTxt3.Font.Name = "Arial"
                                        If IsDBNull(DRRec3("Rate")) Then
                                            LTxt1.Text = ""
                                        Else
                                            LTxt1.Text = FormatNumber(DRRec3("Rate").ToString, 6)
                                        End If
                                        If IsDBNull(DRRec3("MiscRate")) Then
                                            LTxt2.Text = ""
                                        Else
                                            LTxt2.Text = FormatNumber(DRRec3("MiscRate").ToString, 6)
                                        End If
                                        If IsDBNull(DRRec3("StatRate")) Then
                                            LTxt3.Text = ""
                                        Else
                                            LTxt3.Text = FormatNumber(DRRec3("StatRate").ToString, 6)
                                        End If


                                        LCell1.Controls.Add(LLbl)
                                        LCell2.Controls.Add(LTxt1)
                                        LCell3.Controls.Add(LTxt2)
                                        LCell4.Controls.Add(LTxt3)
                                        LRow1.Cells.Add(LCell1)
                                        LRow1.Cells.Add(LCell2)
                                        'LRow1.Cells.Add(LCell3)
                                        LRow1.Cells.Add(LCell4)
                                        LRow1.Cells.Add(LCell5)
                                        Table1.Rows.Add(LRow1)
                                    Next
                                Else
                                    HRecExist.Value = "No"
                                    Dim LRow1 As New TableRow
                                    Dim LCell1 As New TableCell
                                    Dim LCell2 As New TableCell
                                    Dim LCell3 As New TableCell
                                    Dim LCell4 As New TableCell
                                    Dim LCell5 As New TableCell
                                    Dim LCell6 As New TableCell
                                    Dim LCell7 As New TableCell
                                    Dim DDLC As New DropDownList
                                    DDLC.ID = "DLMethodID" & k
                                    LCMethods(DDLC)
                                    DDLC.Items(0).Selected = True
                                    LCell5.Controls.Add(DDLC)
                                    LRow1.Font.Size = "8"
                                    Dim LLbl As New Label
                                    ' LRow1.CssClass = "DEMO6"
                                    LLbl.Font.Name = "Arial"
                                    LLbl.ID = "lblAct" & k
                                    LLbl.Text = DRRec2("TAT").ToString & " hrs TAT"
                                    Dim LTxt1 As New TextBox
                                    Dim LTxt2 As New TextBox
                                    Dim LTxt3 As New TextBox
                                    Dim LHF1 As New HiddenField
                                    LHF1.ID = "HActID" & k
                                    LHF1.Value = DRRec2("TrackID").ToString
                                    Form.Controls.Add(LHF1)

                                    ' Txt1.Style("
                                    LTxt1.Width = "50"
                                    LTxt2.Width = "50"
                                    LTxt3.Width = "50"
                                    LTxt1.ID = "TxRate" & k
                                    LTxt1.Text = ""
                                    LTxt2.ID = "TxMRate" & k
                                    LTxt2.Text = ""
                                    LTxt3.ID = "TxSTRate" & k
                                    LTxt3.Text = ""
                                    LTxt1.Font.Size = "8"
                                    LTxt1.Font.Name = "Arial"
                                    LTxt2.Font.Size = "8"
                                    LTxt2.Font.Name = "Arial"
                                    LTxt3.Font.Size = "8"
                                    LTxt3.Font.Name = "Arial"
                                    LCell1.Controls.Add(LLbl)
                                    LCell2.Controls.Add(LTxt1)
                                    LCell3.Controls.Add(LTxt2)
                                    LCell4.Controls.Add(LTxt3)
                                    LRow1.Cells.Add(LCell1)
                                    LRow1.Cells.Add(LCell2)
                                    'LRow1.Cells.Add(LCell3)
                                    LRow1.Cells.Add(LCell4)
                                    LRow1.Cells.Add(LCell5)
                                    Table1.Rows.Add(LRow1)
                                End If
                            End If
                        Next
                        HRecCount.Value = k
                    End If
                ElseIf HMode.Value = "DC" Then
                    Dim k As Integer = 0
                    Dim objActDict As New ETS.BL.GroupDictators
                    objActDict.AccID = DLAct.SelectedValue
                    Dim DT As DataSet = objActDict.getGroupAccountsList
                    If DT.Tables.Count > 0 Then
                        For Each DRRec2 As DataRow In DT.Tables(0).Rows
                            k = k + 1
                            Dim objBillDetails As New ETS.BL.BillDetails
                            objBillDetails.AccountID = DLAct.SelectedValue
                            objBillDetails.SubActID = DRRec2("GrpDicID").ToString
                            Dim DTSet2 As System.Data.DataSet = objBillDetails.getActBillDetailsList
                            If DTSet2.Tables.Count > 0 Then
                                HRecExist.Value = "Yes"
                                If DTSet2.Tables(0).Rows.Count > 0 Then
                                    For Each DRRec3 As Data.DataRow In DTSet2.Tables(0).Rows
                                        Dim LRow1 As New TableRow
                                        Dim LCell1 As New TableCell
                                        Dim LCell2 As New TableCell
                                        Dim LCell3 As New TableCell
                                        Dim LCell4 As New TableCell
                                        Dim LCell5 As New TableCell
                                        Dim DDLC As New DropDownList
                                        DDLC.ID = "DLMethodID" & k
                                        LCMethods(DDLC)
                                        Dim X As Integer
                                        For X = 0 To DDLC.Items.Count - 1
                                            If DRRec3("LCMethodID").ToString = DDLC.Items(X).Value Then
                                                DDLC.Items(X).Selected = True
                                                Exit For
                                            End If
                                        Next
                                        LCell5.Controls.Add(DDLC)
                                        LRow1.Font.Size = "8"
                                        'LRow1.CssClass = "DEMO6"
                                        Dim LHF1 As New HiddenField
                                        LHF1.ID = "HActID" & k
                                        LHF1.Value = DRRec2("GrpDicID").ToString
                                        Form.Controls.Add(LHF1)


                                        Dim LLbl As New Label
                                        LLbl.Font.Name = "Arial"
                                        LLbl.ID = "lblAct" & k
                                        LLbl.Text = DRRec2("GrpDicName").ToString
                                        Dim LTxt1 As New TextBox
                                        Dim LTxt2 As New TextBox
                                        Dim LTxt3 As New TextBox
                                        LTxt1.ID = "TxRate" & k
                                        ' Txt1.Text = DRRec1("Rate")
                                        LTxt2.ID = "TxMRate" & k
                                        ' Txt2.Text = DRRec1("MiscRate")
                                        LTxt3.ID = "TxSTRate" & k
                                        LTxt1.Width = "50"
                                        LTxt2.Width = "50"
                                        LTxt3.Width = "50"
                                        LTxt1.Font.Size = "8"
                                        LTxt1.Font.Name = "Arial"
                                        LTxt2.Font.Size = "8"
                                        LTxt2.Font.Name = "Arial"
                                        LTxt3.Font.Size = "8"
                                        LTxt3.Font.Name = "Arial"
                                        If IsDBNull(DRRec3("Rate")) Then
                                            LTxt1.Text = ""
                                        Else
                                            LTxt1.Text = FormatNumber(DRRec3("Rate").ToString, 6)
                                        End If
                                        If IsDBNull(DRRec3("MiscRate")) Then
                                            LTxt2.Text = ""
                                        Else
                                            LTxt2.Text = FormatNumber(DRRec3("MiscRate").ToString, 6)
                                        End If
                                        If IsDBNull(DRRec3("StatRate")) Then
                                            LTxt3.Text = ""
                                        Else
                                            LTxt3.Text = FormatNumber(DRRec3("StatRate").ToString, 6)
                                        End If


                                        LCell1.Controls.Add(LLbl)
                                        LCell2.Controls.Add(LTxt1)
                                        LCell3.Controls.Add(LTxt2)
                                        LCell4.Controls.Add(LTxt3)
                                        LRow1.Cells.Add(LCell1)
                                        LRow1.Cells.Add(LCell2)
                                        'LRow1.Cells.Add(LCell3)
                                        LRow1.Cells.Add(LCell4)
                                        LRow1.Cells.Add(LCell5)
                                        Table1.Rows.Add(LRow1)

                                    Next
                                Else
                                    HRecExist.Value = "No"
                                    Dim LRow1 As New TableRow
                                    Dim LCell1 As New TableCell
                                    Dim LCell2 As New TableCell
                                    Dim LCell3 As New TableCell
                                    Dim LCell4 As New TableCell
                                    Dim LCell5 As New TableCell
                                    Dim LCell6 As New TableCell
                                    Dim LCell7 As New TableCell
                                    Dim DDLC As New DropDownList
                                    DDLC.ID = "DLMethodID" & k
                                    LCMethods(DDLC)
                                    DDLC.Items(0).Selected = True
                                    LCell5.Controls.Add(DDLC)

                                    LRow1.Font.Size = "8"
                                    Dim LLbl As New Label
                                    ' LRow1.CssClass = "DEMO6"
                                    LLbl.Font.Name = "Arial"
                                    LLbl.ID = "lblAct" & k
                                    LLbl.Text = DRRec2("GrpDicName").ToString
                                    Dim LTxt1 As New TextBox
                                    Dim LTxt2 As New TextBox
                                    Dim LTxt3 As New TextBox
                                    Dim LHF1 As New HiddenField
                                    LHF1.ID = "HActID" & k
                                    LHF1.Value = DRRec2("GrpDicID").ToString
                                    Form.Controls.Add(LHF1)

                                    ' Txt1.Style("
                                    LTxt1.Width = "50"
                                    LTxt2.Width = "50"
                                    LTxt3.Width = "50"
                                    LTxt1.ID = "TxRate" & k
                                    LTxt1.Text = ""
                                    LTxt2.ID = "TxMRate" & k
                                    LTxt2.Text = ""
                                    LTxt3.ID = "TxSTRate" & k
                                    LTxt3.Text = ""
                                    LTxt1.Font.Size = "8"
                                    LTxt1.Font.Name = "Arial"
                                    LTxt2.Font.Size = "8"
                                    LTxt2.Font.Name = "Arial"
                                    LTxt3.Font.Size = "8"
                                    LTxt3.Font.Name = "Arial"
                                    LCell1.Controls.Add(LLbl)
                                    LCell2.Controls.Add(LTxt1)
                                    LCell3.Controls.Add(LTxt2)
                                    LCell4.Controls.Add(LTxt3)
                                    LRow1.Cells.Add(LCell1)
                                    LRow1.Cells.Add(LCell2)
                                    'LRow1.Cells.Add(LCell3)
                                    LRow1.Cells.Add(LCell4)
                                    LRow1.Cells.Add(LCell5)
                                    Table1.Rows.Add(LRow1)
                                End If
                            End If
                        Next
                        HRecCount.Value = k
                    End If
                ElseIf HMode.Value = "TW" Then
                    Dim k As Integer = 0
                    Dim objActDict As New ETS.BL.GroupTemplates
                    objActDict.AccID = DLAct.SelectedValue
                    Dim DT As DataSet = objActDict.getGroupTemplatesList
                    If DT.Tables.Count > 0 Then
                        For Each DRRec2 As DataRow In DT.Tables(0).Rows
                            k = k + 1
                            Dim objBillDetails As New ETS.BL.BillDetails
                            objBillDetails.AccountID = DLAct.SelectedValue
                            objBillDetails.SubActID = DRRec2("GrpTempID").ToString
                            Dim DTSet2 As System.Data.DataSet = objBillDetails.getActBillDetailsList
                            If DTSet2.Tables.Count > 0 Then
                                HRecExist.Value = "Yes"
                                If DTSet2.Tables(0).Rows.Count > 0 Then
                                    For Each DRRec3 As Data.DataRow In DTSet2.Tables(0).Rows
                                        Dim LRow1 As New TableRow
                                        Dim LCell1 As New TableCell
                                        Dim LCell2 As New TableCell
                                        Dim LCell3 As New TableCell
                                        Dim LCell4 As New TableCell
                                        Dim LCell5 As New TableCell
                                        Dim DDLC As New DropDownList
                                        DDLC.ID = "DLMethodID" & k
                                        LCMethods(DDLC)
                                        Dim X As Integer
                                        For X = 0 To DDLC.Items.Count - 1
                                            If DRRec3("LCMethodID").ToString = DDLC.Items(X).Value Then
                                                DDLC.Items(X).Selected = True
                                                Exit For
                                            End If
                                        Next
                                        LCell5.Controls.Add(DDLC)
                                        LRow1.Font.Size = "8"
                                        'LRow1.CssClass = "DEMO6"
                                        Dim LHF1 As New HiddenField
                                        LHF1.ID = "HActID" & k
                                        LHF1.Value = DRRec2("GrpTempID").ToString
                                        Form.Controls.Add(LHF1)


                                        Dim LLbl As New Label
                                        LLbl.Font.Name = "Arial"
                                        LLbl.ID = "lblAct" & k
                                        LLbl.Text = DRRec2("GrpTempName").ToString
                                        Dim LTxt1 As New TextBox
                                        Dim LTxt2 As New TextBox
                                        Dim LTxt3 As New TextBox
                                        LTxt1.ID = "TxRate" & k
                                        ' Txt1.Text = DRRec1("Rate")
                                        LTxt2.ID = "TxMRate" & k
                                        ' Txt2.Text = DRRec1("MiscRate")
                                        LTxt3.ID = "TxSTRate" & k
                                        LTxt1.Width = "50"
                                        LTxt2.Width = "50"
                                        LTxt3.Width = "50"
                                        LTxt1.Font.Size = "8"
                                        LTxt1.Font.Name = "Arial"
                                        LTxt2.Font.Size = "8"
                                        LTxt2.Font.Name = "Arial"
                                        LTxt3.Font.Size = "8"
                                        LTxt3.Font.Name = "Arial"
                                        If IsDBNull(DRRec3("Rate")) Then
                                            LTxt1.Text = ""
                                        Else
                                            LTxt1.Text = FormatNumber(DRRec3("Rate").ToString, 6)
                                        End If
                                        If IsDBNull(DRRec3("MiscRate")) Then
                                            LTxt2.Text = ""
                                        Else
                                            LTxt2.Text = FormatNumber(DRRec3("MiscRate").ToString, 6)
                                        End If
                                        If IsDBNull(DRRec3("StatRate")) Then
                                            LTxt3.Text = ""
                                        Else
                                            LTxt3.Text = FormatNumber(DRRec3("StatRate").ToString, 6)
                                        End If


                                        LCell1.Controls.Add(LLbl)
                                        LCell2.Controls.Add(LTxt1)
                                        LCell3.Controls.Add(LTxt2)
                                        LCell4.Controls.Add(LTxt3)
                                        LRow1.Cells.Add(LCell1)
                                        LRow1.Cells.Add(LCell2)
                                        'LRow1.Cells.Add(LCell3)
                                        LRow1.Cells.Add(LCell4)
                                        LRow1.Cells.Add(LCell5)
                                        Table1.Rows.Add(LRow1)

                                    Next
                                Else
                                    HRecExist.Value = "No"
                                    Dim LRow1 As New TableRow
                                    Dim LCell1 As New TableCell
                                    Dim LCell2 As New TableCell
                                    Dim LCell3 As New TableCell
                                    Dim LCell4 As New TableCell
                                    Dim LCell5 As New TableCell
                                    Dim LCell6 As New TableCell
                                    Dim LCell7 As New TableCell
                                    Dim DDLC As New DropDownList
                                    DDLC.ID = "DLMethodID" & k
                                    LCMethods(DDLC)
                                    DDLC.Items(0).Selected = True
                                    LCell5.Controls.Add(DDLC)

                                    LRow1.Font.Size = "8"
                                    Dim LLbl As New Label
                                    ' LRow1.CssClass = "DEMO6"
                                    LLbl.Font.Name = "Arial"
                                    LLbl.ID = "lblAct" & k
                                    LLbl.Text = DRRec2("GrpTempName").ToString
                                    Dim LTxt1 As New TextBox
                                    Dim LTxt2 As New TextBox
                                    Dim LTxt3 As New TextBox
                                    Dim LHF1 As New HiddenField
                                    LHF1.ID = "HActID" & k
                                    LHF1.Value = DRRec2("GrpTempID").ToString
                                    Form.Controls.Add(LHF1)

                                    ' Txt1.Style("
                                    LTxt1.Width = "50"
                                    LTxt2.Width = "50"
                                    LTxt3.Width = "50"
                                    LTxt1.ID = "TxRate" & k
                                    LTxt1.Text = ""
                                    LTxt2.ID = "TxMRate" & k
                                    LTxt2.Text = ""
                                    LTxt3.ID = "TxSTRate" & k
                                    LTxt3.Text = ""
                                    LTxt1.Font.Size = "8"
                                    LTxt1.Font.Name = "Arial"
                                    LTxt2.Font.Size = "8"
                                    LTxt2.Font.Name = "Arial"
                                    LTxt3.Font.Size = "8"
                                    LTxt3.Font.Name = "Arial"
                                    LCell1.Controls.Add(LLbl)
                                    LCell2.Controls.Add(LTxt1)
                                    LCell3.Controls.Add(LTxt2)
                                    LCell4.Controls.Add(LTxt3)
                                    LRow1.Cells.Add(LCell1)
                                    LRow1.Cells.Add(LCell2)
                                    'LRow1.Cells.Add(LCell3)
                                    LRow1.Cells.Add(LCell4)
                                    LRow1.Cells.Add(LCell5)
                                    Table1.Rows.Add(LRow1)
                                End If
                            End If
                        Next
                        HRecCount.Value = k
                    End If
                End If
            Next
        End If
        objAct = Nothing
    End Sub



    Protected Sub SubButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubButton.Click
        Dim oConn As New SqlClient.SqlConnection
        Dim oTrans As SqlClient.SqlTransaction
        Dim SQLStr As String
        Try
            oConn.ConnectionString = New DALBASE().GetConnectionString
            oConn.Open()
            oTrans = oConn.BeginTransaction()

            Dim RetVal As Integer = 0
            Dim RetActVal As Integer = 0

            Dim varUpdate As String = String.Empty
            Dim varWhere As String = String.Empty
            varUpdate = "MinBilling = '" & TxMBilling.Text & "', Cycle = '" & DLCycle.SelectedValue & "'"
            varWhere = " where AccountID = '" & DLAct.SelectedValue & "' "
            RetActVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getUpdateQuery(varUpdate, "ETS.DBO.tblAccounts", varWhere))
            If HMode.Value = "S" Or HMode.Value = "" Then
                If HRecExist.Value = "Yes" Then
                    If DLWType.Items(0).Selected = False Then
                        SQLStr = " Rate = '" & Request("TxRate1") & "', "
                        SQLStr = SQLStr & " MiscRate = '" & Request("TxMRate1") & "', "
                        SQLStr = SQLStr & " StatRate = '" & Request("TxSTRate1") & "', "
                        SQLStr = SQLStr & " WTMode = '" & Request("DLWType") & "', "
                        If Request("DLMethodID1") = "" Then
                            SQLStr = SQLStr & " LCMethodID = NULL, "
                        Else
                            SQLStr = SQLStr & " LCMethodID = '" & Request("DLMethodID1") & "', "
                        End If
                    Else
                        SQLStr = SQLStr & " Rate = '" & Request("Rate") & "', "
                        SQLStr = SQLStr & " StatRate = '" & Request("StatRate") & "', "
                        SQLStr = SQLStr & " WTMode = '" & Request("DLWType") & "', "
                        SQLStr = SQLStr & " Consult = '" & Request("Consult") & "', "
                        SQLStr = SQLStr & " HP = '" & Request("HP") & "', "
                        SQLStr = SQLStr & " Discharge = '" & Request("Discharge") & "', "
                        SQLStr = SQLStr & " IME = '" & Request("IME") & "', "
                        SQLStr = SQLStr & " Letter = '" & Request("Letter") & "', "
                        SQLStr = SQLStr & " PrNote = '" & Request("PrNote") & "', "
                        SQLStr = SQLStr & " OpNote = '" & Request("OpNote") & "', "
                        SQLStr = SQLStr & " PsychEval = '" & Request("PsychEval") & "', "
                        If Request("DLDefLC") = "" Then
                            SQLStr = SQLStr & " LCMethodID = NULL, "
                        Else
                            SQLStr = SQLStr & " LCMethodID = '" & Request("DLDefLC") & "', "
                        End If
                    End If
                    SQLStr = SQLStr & " ModDate = '" & Now & "' "

                    varUpdate = SQLStr
                    varWhere = " where AccountID = '" & DLAct.SelectedValue & "' "
                    RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getUpdateQuery(varUpdate, "Secureweb.DBO.BillDetails", varWhere))

                Else
                    If DLWType.Items(0).Selected = False Then

                        If Request("DLMethodID1") = "" Then
                            RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getInsertQuery("Accountid, Rate, MiscRate, statRate, ModDate, WTMode, LCMethodID", "'" & HAccountID.Value & "',  '" & Request("TxRate1") & "',  '" & Request("TxMRate1") & "',  '" & Request("TxSTRate1") & "',   '" & Now & "', '" & Request("DLWType") & "',NULL", "Secureweb.DBO.BillDetails"))
                        Else
                            RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getInsertQuery("Accountid, Rate, MiscRate, statRate, ModDate, WTMode, LCMethodID", "'" & HAccountID.Value & "',  '" & Request("TxRate1") & "',  '" & Request("TxMRate1") & "',  '" & Request("TxSTRate1") & "',   '" & Now & "', '" & Request("DLWType") & "', '" & Request("DLMethodID1") & "' ", "Secureweb.DBO.BillDetails"))
                        End If
                    Else
                        If Request("DLMethodID1") = "" Then
                            RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getInsertQuery("Accountid,Consult,HP,Discharge,IME,Letter,PrNote,OpNote,PsychEval,Rate, statRate, ModDate, WTMode, LCMethodID", "'" & HAccountID.Value & "',  '" & Request("Consult") & "', '" & Request("HP") & "', '" & Request("Discharge") & "', '" & Request("IME") & "', '" & Request("Letter") & "', '" & Request("PrNote") & "', '" & Request("OpNote") & "', '" & Request("PsychEval") & "', '" & Request("rate") & "', '" & Request("statrate") & "',   '" & Now & "', '" & Request("DLWType") & "',NULL", "Secureweb.DBO.BillDetails"))
                        Else
                            RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getInsertQuery("Accountid,Consult,HP,Discharge,IME,Letter,PrNote,OpNote,PsychEval,Rate, statRate, ModDate, WTMode, LCMethodID", "'" & HAccountID.Value & "',  '" & Request("Consult") & "', '" & Request("HP") & "', '" & Request("Discharge") & "', '" & Request("IME") & "', '" & Request("Letter") & "', '" & Request("PrNote") & "', '" & Request("OpNote") & "', '" & Request("PsychEval") & "', '" & Request("rate") & "', '" & Request("statrate") & "',   '" & Now & "', '" & Request("DLWType") & "', '" & Request("DLMethodID1") & "'", "Secureweb.DBO.BillDetails"))
                        End If

                    End If



                End If
            ElseIf HMode.Value = "DV" Then
                If HRecExist.Value = "Yes" Then
                    SQLStr = " Rate = '" & Request("TxRate1") & "', "
                    SQLStr = SQLStr & " MiscRate = '" & Request("TxMRate1") & "', "
                    SQLStr = SQLStr & " StatRate = '" & Request("TxSTRate1") & "', "
                    SQLStr = SQLStr & " WTMode = '" & Request("DLWType") & "', "
                    If Request("DLMethodID1") = "" Then
                        SQLStr = SQLStr & " LCMethodID = NULL, "
                    Else
                        SQLStr = SQLStr & " LCMethodID = '" & Request("DLMethodID1") & "', "
                    End If

                    'SQLStr = SQLStr & " Cycle = '" & Request("DLCycle") & "', "
                    '  SQLStr = SQLStr & " EffMonth = '" & Request("DLMonth") & "', "
                    '  SQLStr = SQLStr & " EffYear = '" & Request("DLYear") & "', "
                    SQLStr = SQLStr & " ModDate = '" & Now & "' "
                    varUpdate = SQLStr
                    varWhere = " where AccountID = '" & DLAct.SelectedValue & "' "
                    RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getUpdateQuery(varUpdate, "Secureweb.DBO.BillDetails", varWhere))


                Else
                    SQLStr = "Insert Into Secureweb.DBO.BillDetails (Accountid, Rate, MiscRate, statRate, ModDate, LCMethodID) Values ('" & HAccountID.Value & "',  '" & Request("TxRate1") & "',  '" & Request("TxMRate1") & "',  '" & Request("TxSTRate1") & "',   '" & Now & "'"
                    If Request("DLMethodID1") = "" Then
                        RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getInsertQuery("Accountid, Rate, MiscRate, statRate, ModDate, LCMethodID", "'" & HAccountID.Value & "',  '" & Request("TxRate1") & "',  '" & Request("TxMRate1") & "',  '" & Request("TxSTRate1") & "',   '" & Now & "',NULL", "Secureweb.DBO.BillDetails"))
                    Else
                        RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getInsertQuery("Accountid, Rate, MiscRate, statRate, ModDate, LCMethodID", "'" & HAccountID.Value & "',  '" & Request("TxRate1") & "',  '" & Request("TxMRate1") & "',  '" & Request("TxSTRate1") & "',   '" & Now & "', '" & Request("DLMethodID1") & "'", "Secureweb.DBO.BillDetails"))
                    End If


                End If
            ElseIf HMode.Value = "LC" Or HMode.Value = "TT" Or HMode.Value = "DC" Or HMode.Value = "TW" Then
                Dim i As Integer
                For i = 1 To HRecCount.Value
                    'If HRecExist.Value = "Yes" Then
                    SQLStr = " Rate = '" & Request("TxRate" & i) & "', "
                    SQLStr = SQLStr & " MiscRate = '" & Request("TxMRate" & i) & "', "
                    SQLStr = SQLStr & " StatRate = '" & Request("TxSTRate" & i) & "', "
                    SQLStr = SQLStr & " MinBilling = '" & Request("TxMBilling") & "', "
                    SQLStr = SQLStr & " Cycle = '" & Request("DLCycle") & "', "
                    If Request("DLMethodID" & i) = "" Then
                        SQLStr = SQLStr & " LCMethodID = NULL, "
                    Else
                        SQLStr = SQLStr & " LCMethodID = '" & Request("DLMethodID" & i) & "', "
                    End If

                    'SQLStr = SQLStr & " LCMethodID = '" & Request("DLMethodID" & i) & "', "
                    '  SQLStr = SQLStr & " EffYear = '" & Request("DLYear") & "', "
                    SQLStr = SQLStr & " ModDate = '" & Now & "' "
                    varUpdate = SQLStr
                    varWhere = "  where AccountID = '" & HAccountID.Value & "' and SubActID='" & Request("HActID" & i) & "'  "
                    RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getUpdateQuery(varUpdate, "Secureweb.DBO.BillDetails", varWhere))
                    'Else
                    If Not RetVal = 1 Then
                        SQLStr = "Insert Into Secureweb.DBO.BillDetails (Accountid, SubActID, Rate, MiscRate, statRate, MinBilling, Cycle, ModDate, LCMethodID) Values ('" & HAccountID.Value & "',  '" & Request("HActID" & i) & "', '" & Request("TxRate" & i) & "',  '" & Request("TxMRate" & i) & "',  '" & Request("TxSTRate" & i) & "', '" & Request("TxMBilling") & "','" & Request("DLCycle") & "',  '" & Now & "'"
                        If Request("DLMethodID" & i) = "" Then
                            RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getInsertQuery("Accountid, SubActID, Rate, MiscRate, statRate, MinBilling, Cycle, ModDate, LCMethodID", "'" & HAccountID.Value & "',  '" & Request("HActID" & i) & "', '" & Request("TxRate" & i) & "',  '" & Request("TxMRate" & i) & "',  '" & Request("TxSTRate" & i) & "', '" & Request("TxMBilling") & "','" & Request("DLCycle") & "',  '" & Now & "',NULL", "Secureweb.DBO.BillDetails"))
                        Else
                            RetVal = New DALBASE().ExecuteDynamicQuery(oConn, oTrans, New DynamicSQL().getInsertQuery("Accountid, SubActID, Rate, MiscRate, statRate, MinBilling, Cycle, ModDate, LCMethodID", "'" & HAccountID.Value & "',  '" & Request("HActID" & i) & "', '" & Request("TxRate" & i) & "',  '" & Request("TxMRate" & i) & "',  '" & Request("TxSTRate" & i) & "', '" & Request("TxMBilling") & "','" & Request("DLCycle") & "',  '" & Now & "', '" & Request("DLMethodID" & i) & "' ", "Secureweb.DBO.BillDetails"))
                        End If
                    End If
                    'Response.Write(SQLStr)
                Next
            End If

            If RetVal > 0 And RetActVal > 0 Then
                oTrans.Commit()
            Else
                oTrans.Rollback()
            End If
        Catch ex As Exception
            oTrans.Rollback()

        Finally
            oTrans.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try

        'Dim SQLStr As String
        'Dim strConn As String
        'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'SQLStr = "update ETS.DBO.tblAccounts set MinBilling = '" & TxMBilling.Text & "', Cycle = '" & DLCycle.SelectedValue & "' where AccountID = '" & DLAct.SelectedValue & "' "
        'Dim cmdup1 As New SqlCommand(SQLStr, New SqlConnection(strConn))
        'Try
        '    cmdup1.Connection.Open()
        '    cmdup1.ExecuteNonQuery()
        'Finally
        '    If cmdup1.Connection.State = System.Data.ConnectionState.Open Then
        '        cmdup1.Connection.Close()
        '        cmdup1 = Nothing
        '    End If
        'End Try

        ''SQLStr = SQLStr & " Accountid = '" & Request("HActID1") & "', "
        ''SQLStr = SQLStr & " Rate = '" & Request("TxRate1") & "', "
        ''SQLStr = SQLStr & " MiscRate = '" & Request("TxMRate1") & "', "
        ''SQLStr = SQLStr & " MinimumBill = '" & Request("TxMBilling") & "', "
        ''SQLStr = SQLStr & " Cycle = '" & Request("DLCycle") & "', "
        ''SQLStr = SQLStr & " ModDate = '" & Now & "' "
        ''  Response.Write(SQLStr)
        'If HMode.Value = "S" Or HMode.Value = "" Then
        '    If HRecExist.Value = "Yes" Then
        '        SQLStr = "Update Secureweb.DBO.BillDetails Set "
        '        If DLWType.Items(0).Selected = False Then
        '            SQLStr = SQLStr & " Rate = '" & Request("TxRate1") & "', "
        '            SQLStr = SQLStr & " MiscRate = '" & Request("TxMRate1") & "', "
        '            SQLStr = SQLStr & " StatRate = '" & Request("TxSTRate1") & "', "
        '            SQLStr = SQLStr & " WTMode = '" & Request("DLWType") & "', "
        '            If Request("DLMethodID1") = "" Then
        '                SQLStr = SQLStr & " LCMethodID = NULL, "
        '            Else
        '                SQLStr = SQLStr & " LCMethodID = '" & Request("DLMethodID1") & "', "
        '            End If
        '        Else
        '            SQLStr = SQLStr & " Rate = '" & Request("Rate1") & "', "
        '            SQLStr = SQLStr & " StatRate = '" & Request("StatRate") & "', "
        '            SQLStr = SQLStr & " WTMode = '" & Request("DLWType") & "', "
        '            SQLStr = SQLStr & " Consult = '" & Request("Consult") & "', "
        '            SQLStr = SQLStr & " HP = '" & Request("HP") & "', "
        '            SQLStr = SQLStr & " Discharge = '" & Request("Discharge") & "', "
        '            SQLStr = SQLStr & " IME = '" & Request("IME") & "', "
        '            SQLStr = SQLStr & " Letter = '" & Request("Letter") & "', "
        '            SQLStr = SQLStr & " PrNote = '" & Request("PrNote") & "', "
        '            SQLStr = SQLStr & " OpNote = '" & Request("OpNote") & "', "
        '            SQLStr = SQLStr & " PsychEval = '" & Request("PsychEval") & "', "
        '            If Request("DLDefLC") = "" Then
        '                SQLStr = SQLStr & " LCMethodID = NULL, "
        '            Else
        '                SQLStr = SQLStr & " LCMethodID = '" & Request("DLDefLC") & "', "
        '            End If

        '        End If



        '        'SQLStr = SQLStr & " Cycle = '" & Request("DLCycle") & "', "
        '        '  SQLStr = SQLStr & " EffMonth = '" & Request("DLMonth") & "', "
        '        '  SQLStr = SQLStr & " EffYear = '" & Request("DLYear") & "', "
        '        SQLStr = SQLStr & " ModDate = '" & Now & "' "
        '        SQLStr = SQLStr & " where AccountID = '" & HAccountID.Value & "' "
        '    Else
        '        If DLWType.Items(0).Selected = False Then
        '            SQLStr = "Insert Into Secureweb.DBO.BillDetails (Accountid, Rate, MiscRate, statRate, ModDate, WTMode, LCMethodID) Values ('" & HAccountID.Value & "',  '" & Request("TxRate1") & "',  '" & Request("TxMRate1") & "',  '" & Request("TxSTRate1") & "',   '" & Now & "', '" & Request("DLWType") & "'"
        '            If Request("DLMethodID1") = "" Then
        '                SQLStr = SQLStr & ", NULL)"
        '            Else
        '                SQLStr = SQLStr & ", '" & Request("DLMethodID1") & "' )"
        '            End If
        '        Else
        '            SQLStr = "Insert Into Secureweb.DBO.BillDetails (Accountid,Consult,HP,Discharge,IME,Letter,PrNote,OpNote,PsychEval,Rate, statRate, ModDate, WTMode, LCMethodID) Values ('" & HAccountID.Value & "',  '" & Request("Consult") & "', '" & Request("HP") & "', '" & Request("Discharge") & "', '" & Request("IME") & "', '" & Request("Letter") & "', '" & Request("PrNote") & "', '" & Request("OpNote") & "', '" & Request("PsychEval") & "', '" & Request("rate") & "', '" & Request("statrate") & "',   '" & Now & "', '" & Request("DLWType") & "'"
        '            If Request("DLMethodID1") = "" Then
        '                SQLStr = SQLStr & ", NULL)"
        '            Else
        '                SQLStr = SQLStr & ", '" & Request("DLMethodID1") & "' )"
        '            End If

        '        End If



        '    End If
        '    Dim cmdIns As New SqlCommand(SQLStr, New SqlConnection(strConn))
        '    Try
        '        cmdIns.Connection.Open()
        '        cmdIns.ExecuteNonQuery()
        '    Finally
        '        If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
        '            cmdIns.Connection.Close()
        '            cmdIns = Nothing
        '        End If
        '    End Try
        'ElseIf HMode.Value = "DV" Then
        '    If HRecExist.Value = "Yes" Then
        '        SQLStr = "Update Secureweb.DBO.BillDetails Set "
        '        SQLStr = SQLStr & " Rate = '" & Request("TxRate1") & "', "
        '        SQLStr = SQLStr & " MiscRate = '" & Request("TxMRate1") & "', "
        '        SQLStr = SQLStr & " StatRate = '" & Request("TxSTRate1") & "', "
        '        SQLStr = SQLStr & " WTMode = '" & Request("DLWType") & "', "
        '        If Request("DLMethodID1") = "" Then
        '            SQLStr = SQLStr & " LCMethodID = NULL, "
        '        Else
        '            SQLStr = SQLStr & " LCMethodID = '" & Request("DLMethodID1") & "', "
        '        End If

        '        'SQLStr = SQLStr & " Cycle = '" & Request("DLCycle") & "', "
        '        '  SQLStr = SQLStr & " EffMonth = '" & Request("DLMonth") & "', "
        '        '  SQLStr = SQLStr & " EffYear = '" & Request("DLYear") & "', "
        '        SQLStr = SQLStr & " ModDate = '" & Now & "' "
        '        SQLStr = SQLStr & " where AccountID = '" & HAccountID.Value & "' "
        '    Else
        '        SQLStr = "Insert Into Secureweb.DBO.BillDetails (Accountid, Rate, MiscRate, statRate, ModDate, LCMethodID) Values ('" & HAccountID.Value & "',  '" & Request("TxRate1") & "',  '" & Request("TxMRate1") & "',  '" & Request("TxSTRate1") & "',   '" & Now & "'"
        '        If Request("DLMethodID1") = "" Then
        '            SQLStr = SQLStr & ", NULL)"
        '        Else
        '            SQLStr = SQLStr & ", '" & Request("DLMethodID1") & "' )"
        '        End If


        '    End If
        '    Dim cmdIns As New SqlCommand(SQLStr, New SqlConnection(strConn))
        '    Try
        '        cmdIns.Connection.Open()
        '        cmdIns.ExecuteNonQuery()
        '    Finally
        '        If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
        '            cmdIns.Connection.Close()
        '            cmdIns = Nothing
        '        End If
        '    End Try
        'ElseIf HMode.Value = "LC" Or HMode.Value = "DC" Then
        '    Dim i As Integer
        '    For i = 1 To HRecCount.Value
        '        If HRecExist.Value = "Yes" Then
        '            SQLStr = "Update Secureweb.DBO.BillDetails Set "
        '            SQLStr = SQLStr & " Rate = '" & Request("TxRate" & i) & "', "
        '            SQLStr = SQLStr & " MiscRate = '" & Request("TxMRate" & i) & "', "
        '            SQLStr = SQLStr & " StatRate = '" & Request("TxSTRate" & i) & "', "
        '            SQLStr = SQLStr & " MinBilling = '" & Request("TxMBilling") & "', "
        '            SQLStr = SQLStr & " Cycle = '" & Request("DLCycle") & "', "
        '            If Request("DLMethodID" & i) = "" Then
        '                SQLStr = SQLStr & " LCMethodID = NULL, "
        '            Else
        '                SQLStr = SQLStr & " LCMethodID = '" & Request("DLMethodID" & i) & "', "
        '            End If

        '            'SQLStr = SQLStr & " LCMethodID = '" & Request("DLMethodID" & i) & "', "
        '            '  SQLStr = SQLStr & " EffYear = '" & Request("DLYear") & "', "
        '            SQLStr = SQLStr & " ModDate = '" & Now & "' "
        '            SQLStr = SQLStr & " where AccountID = '" & HAccountID.Value & "' and SubActID='" & Request("HActID" & i) & "' "
        '        Else
        '            SQLStr = "Insert Into Secureweb.DBO.BillDetails (Accountid, SubActID, Rate, MiscRate, statRate, MinBilling, Cycle, ModDate, LCMethodID) Values ('" & HAccountID.Value & "',  '" & Request("HActID" & i) & "', '" & Request("TxRate" & i) & "',  '" & Request("TxMRate" & i) & "',  '" & Request("TxSTRate" & i) & "', '" & Request("TxMBilling") & "','" & Request("DLCycle") & "',  '" & Now & "'"
        '            If Request("DLMethodID" & i) = "" Then
        '                SQLStr = SQLStr & ", NULL)"
        '            Else
        '                SQLStr = SQLStr & ", '" & Request("DLMethodID" & i) & "' )"
        '            End If
        '        End If
        '        'Response.Write(SQLStr)
        '        Dim cmdIns As New SqlCommand(SQLStr, New SqlConnection(strConn))
        '        Try
        '            cmdIns.Connection.Open()
        '            cmdIns.ExecuteNonQuery()
        '        Finally
        '            If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
        '                cmdIns.Connection.Close()
        '                cmdIns = Nothing
        '            End If
        '        End Try

        '    Next
        'End If

        'Response.Write(HMode.Value)
        'Response.End()

        ShowDetails()


    End Sub

    Protected Sub DLWType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLWType.SelectedIndexChanged
        If DLWType.Items(0).Selected = True Then
            Table1.Visible = False
            Table4.Visible = True
        Else
            Table4.Visible = False
            Table1.Visible = True
        End If
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim objAct As New ETS.BL.Accounts
        objAct.AccountID = DLAct.SelectedValue
        Dim DTSet1 As System.Data.DataSet = objAct.getAccountList

        If DTSet1.Tables.Count > 0 Then
            Response.Write(DTSet1.Tables.Count)
            For Each DRRec As Data.DataRow In DTSet1.Tables(0).Rows
                HMode.Value = Trim(DRRec("Mode").ToString)
                HAccountID.Value = DRRec("AccountID").ToString
                If IsDBNull(DRRec("MinBilling")) Then
                    TxMBilling.Text = ""
                Else
                    TxMBilling.Text = FormatNumber(DRRec("MinBilling").ToString, 6)
                End If
                If IsDBNull(DRRec("Cycle")) Then
                    DLCycle.Items(0).Selected = False
                    DLCycle.Items(1).Selected = True
                ElseIf DRRec("Cycle") = "True" Then
                    DLCycle.Items(1).Selected = False
                    DLCycle.Items(0).Selected = True
                Else
                    DLCycle.Items(0).Selected = False
                    DLCycle.Items(1).Selected = True
                End If
                'Response.Write("Mode " & DRRec("Mode").ToString)
                'Response.End()

                'If HMode.Value = "S" Or HMode.Value = "" Then
                '    WTRow.Visible = True
                'Dim SQLCmd1 As New SqlCommand("Select Top 1 * from SecureWeb.dbo.BillDetails where AccountID='" & DLAct.SelectedValue & "' order by ModDate Desc", New SqlConnection(strConn))
                Dim objBillDetails As New ETS.BL.BillDetails
                objBillDetails.AccountID = DLAct.SelectedValue
                Dim DTSet2 As System.Data.DataSet = objBillDetails.getActBillDetailsList
                'Response.Write(DTSet2.Tables.Count)
                If DTSet2.Tables.Count > 0 Then
                    'Response.Write(DTSet2.Tables(0).Rows.Count & "#" & DLAct.SelectedValue)
                    HRecExist.Value = "Yes"
                    If DTSet2.Tables(0).Rows.Count > 0 Then
                        'Response.Write(DTSet2.Tables(0).Rows.Count & "#" & DLAct.SelectedValue)
                        'Response.End()


                        For Each DRRec1 As Data.DataRow In DTSet2.Tables(0).Rows
                            'Dim SQLCmd As New SqlCommand("Select Mode, AccountName, AccountID, MinBilling, Cycle from tblAccounts where accountid ='" & DLAct.SelectedValue & "'", New SqlConnection(strConn))
                            ''Response.Write("Select Mode, AccountName, AccountID, MinBilling, Cycle from tblAccounts where accountid ='" & DLAct.SelectedValue & "'")

                            'Try
                            '    SQLCmd.Connection.Open()
                            '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                            '    If DRRec.HasRows = True Then
                            '        If DRRec.Read Then
                            '            'Response.Write(DRRec("mode"))
                            '            HMode.Value = Trim(DRRec("Mode").ToString)
                            '            HAccountID.Value = DRRec("AccountID").ToString
                            '            If IsDBNull(DRRec("MinBilling")) Then
                            '                TxMBilling.Text = ""
                            '            Else
                            '                TxMBilling.Text = FormatNumber(DRRec("MinBilling").ToString, 6)
                            '            End If
                            '            If IsDBNull(DRRec("Cycle")) Then
                            '                DLCycle.Items(0).Selected = False
                            '                DLCycle.Items(1).Selected = True
                            '            ElseIf DRRec("Cycle") = "True" Then
                            '                DLCycle.Items(1).Selected = False
                            '                DLCycle.Items(0).Selected = True
                            '            Else
                            '                DLCycle.Items(0).Selected = False
                            '                DLCycle.Items(1).Selected = True
                            '            End If

                            '            Dim SQLCmd1 As New SqlCommand("Select Top 1 * from SecureWeb.dbo.BillDetails where AccountID='" & DLAct.SelectedValue & "' order by ModDate Desc", New SqlConnection(strConn))
                            '            Try
                            '                SQLCmd1.Connection.Open()
                            '                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                            '                If DRRec1.HasRows = True Then
                            '                    HRecExist.Value = "Yes"
                            '                    If DRRec1.Read Then
                            'Response.Write(DLWType.Items(1).Selected)
                            If DLWType.Items(1).Selected = True Then
                                'DLWType.Items(0).Selected = False
                                'DLWType.Items(1).Selected = True
                                Table1.Visible = True
                                Table4.Visible = False

                                Dim Row1 As New TableRow
                                Dim Cell1 As New TableCell
                                Dim Cell2 As New TableCell
                                Dim Cell3 As New TableCell
                                Dim Cell4 As New TableCell
                                Dim Cell5 As New TableCell
                                Dim DDLC As New DropDownList
                                DDLC.ID = "DLMethodID1"
                                LCMethods(DDLC)
                                Dim X As Integer
                                For X = 0 To DDLC.Items.Count - 1
                                    If DRRec1("LCMethodID").ToString = DDLC.Items(X).Value Then
                                        DDLC.Items(X).Selected = True
                                        Exit For
                                    End If
                                Next
                                Cell5.Controls.Add(DDLC)

                                Row1.Font.Size = "8"

                                Dim HF1 As New HiddenField
                                HF1.ID = "HActID1"
                                HF1.Value = DRRec("AccountID").ToString
                                Form.Controls.Add(HF1)
                                Dim HM1 As New HiddenField
                                HM1.ID = "tblActMode1"
                                HM1.Value = DRRec("Mode").ToString

                                Dim Lbl As New Label
                                Lbl.ID = "lblAct1"
                                Lbl.Font.Name = "Arial"
                                Lbl.Text = DRRec("AccountName").ToString
                                Dim Txt1 As New TextBox
                                Dim Txt2 As New TextBox
                                Dim Txt3 As New TextBox
                                Txt1.ID = "TxRate1"
                                ' Txt1.Text = DRRec1("Rate")
                                Txt2.ID = "TxMRate1"
                                ' Txt2.Text = DRRec1("MiscRate")
                                Txt3.ID = "TxSTRate1"
                                Txt1.Width = "50"
                                Txt2.Width = "50"
                                Txt3.Width = "50"
                                Txt1.Font.Size = "8"
                                Txt1.Font.Name = "Arial"
                                Txt2.Font.Size = "8"
                                Txt2.Font.Name = "Arial"
                                Txt3.Font.Size = "8"
                                Txt3.Font.Name = "Arial"
                                If IsDBNull(DRRec1("Rate")) Then
                                    Txt1.Text = ""
                                Else
                                    Txt1.Text = FormatNumber(DRRec1("Rate").ToString, 6)
                                End If
                                If IsDBNull(DRRec1("MiscRate")) Then
                                    Txt2.Text = ""
                                Else
                                    Txt2.Text = FormatNumber(DRRec1("MiscRate").ToString, 6)
                                End If
                                If IsDBNull(DRRec1("StatRate")) Then
                                    Txt3.Text = ""
                                Else
                                    Txt3.Text = FormatNumber(DRRec1("StatRate").ToString, 6)
                                End If

                                Dim SelMonth As Integer
                                SelMonth = 0
                                Dim SelYear As Integer
                                SelYear = 0


                                Cell1.Controls.Add(Lbl)
                                Cell2.Controls.Add(Txt1)
                                Cell3.Controls.Add(Txt2)
                                Cell4.Controls.Add(Txt3)
                                Row1.Cells.Add(Cell1)
                                Row1.Cells.Add(Cell2)
                                'Row1.Cells.Add(Cell3)
                                Row1.Cells.Add(Cell4)
                                Row1.Cells.Add(Cell5)
                                Table1.Rows.Add(Row1)
                            Else
                                Table4.Visible = True
                                Table1.Visible = False
                                DLWType.Items(1).Selected = False
                                DLWType.Items(0).Selected = True
                                'Response.Write("Row")
                                Consult.Text = DRRec1("Consult").ToString
                                HP.Text = DRRec1("HP").ToString
                                Discharge.Text = DRRec1("Discharge").ToString
                                IME.Text = DRRec1("IME").ToString
                                Letter.Text = DRRec1("Letter").ToString
                                PrNote.Text = DRRec1("PrNote").ToString
                                OpNote.Text = DRRec1("OpNote").ToString
                                PsychEval.Text = DRRec1("PsychEval").ToString
                                LCMethods(DLDefLC)
                                Dim X As Integer
                                For X = 0 To DLDefLC.Items.Count - 1
                                    If DRRec1("LCMethodID").ToString = DLDefLC.Items(X).Value Then
                                        DLDefLC.Items(X).Selected = True
                                        Exit For
                                    End If
                                Next
                            End If

                        Next
                    Else
                        'Response.Write("Else" & "#" & DLWType.Items(1).Selected)
                        If DLWType.Items(1).Selected = True Then
                            'DLWType.Items(0).Selected = False
                            'DLWType.Items(1).Selected = True
                            Table1.Visible = True
                            Table4.Visible = False
                            HRecExist.Value = "No"
                            Dim Row1 As New TableRow
                            Dim Cell1 As New TableCell
                            Dim Cell2 As New TableCell
                            Dim Cell3 As New TableCell
                            Dim Cell4 As New TableCell
                            Dim Cell5 As New TableCell
                            Dim Cell6 As New TableCell
                            Dim Cell7 As New TableCell
                            Dim DDLC As New DropDownList
                            DDLC.Font.Size = 8
                            DDLC.Font.Name = "Arial"
                            DDLC.ID = "DLMethodID1"
                            LCMethods(DDLC)
                            DDLC.Items(0).Selected = True
                            Cell5.Controls.Add(DDLC)
                            Dim Lbl As New Label
                            Row1.Font.Size = "8"
                            Lbl.Font.Name = "Arial"
                            Lbl.ID = "lblAct1"
                            Lbl.Text = DRRec("AccountName").ToString

                            Dim Txt1 As New TextBox
                            Dim Txt2 As New TextBox
                            Dim Txt3 As New TextBox
                            Dim HF1 As New HiddenField
                            HF1.ID = "HActID1"
                            HF1.Value = DRRec("AccountID").ToString
                            Form.Controls.Add(HF1)
                            Dim HM1 As New HiddenField
                            HM1.ID = "tblActMode1"
                            HM1.Value = DRRec("Mode").ToString
                            ' Txt1.Style("
                            Txt1.Width = "50"
                            Txt2.Width = "50"
                            Txt3.Width = "50"
                            Txt1.ID = "TxRate1"
                            Txt1.Text = ""
                            Txt2.ID = "TxMRate1"
                            Txt2.Text = ""
                            Txt3.ID = "TxSTRate1"
                            Txt3.Text = ""
                            Txt1.Font.Size = "8"
                            Txt1.Font.Name = "Arial"
                            Txt2.Font.Size = "8"
                            Txt2.Font.Name = "Arial"
                            Txt3.Font.Size = "8"
                            Txt3.Font.Name = "Arial"
                            Cell1.Controls.Add(Lbl)
                            Cell2.Controls.Add(Txt1)
                            Cell3.Controls.Add(Txt2)
                            Cell4.Controls.Add(Txt3)
                            Row1.Cells.Add(Cell1)
                            Row1.Cells.Add(Cell2)
                            'Row1.Cells.Add(Cell3)
                            Row1.Cells.Add(Cell4)
                            Row1.Cells.Add(Cell5)
                            Table1.Rows.Add(Row1)
                        Else
                            LCMethods(DLDefLC)
                            Table4.Visible = True
                            Table1.Visible = False
                        End If
                    End If

                End If
            Next
        End If

        '                                    Finally
        '                    If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
        '                        SQLCmd1.Connection.Close()
        '                        SQLCmd1 = Nothing
        '                    End If
        '                End Try
        '            End If
        '        End If


        '                        Finally
        '    If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
        '        SQLCmd.Connection.Close()
        '        SQLCmd = Nothing
        '    End If
        'End Try
    End Sub
End Class
