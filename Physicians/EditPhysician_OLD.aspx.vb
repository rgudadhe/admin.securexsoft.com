Imports System
Imports System.Data

Partial Class Department_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        BtnAssign.Visible = False
        DispBox.Visible = False
        Table4.Visible = False
        If Not IsPostBack Then
            BtnAssign.Visible = False
            Table4.Visible = False
            Panel6.Visible = False
            Panel1.Visible = False
            Panel2.Visible = False
            HDictCode.Value = 1
            Panel5.Visible = True
            ActState.Value = 0
            DLPhyID.Attributes.Add("onChange", "OnSelectedIndexChange();")
        Else
        End If
    End Sub
    Protected Sub ActSearch_Click()
        Panel6.Visible = True
        Panel5.Visible = False
        Panel1.Visible = False
        Panel2.Visible = False
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both

        Dim DSAct As New DataSet
        Dim DRec1 As Data.DataTableReader
        Dim clsAct As New ETS.BL.Accounts
        DSAct = clsAct.getAccountList(Session("WorkGroupID"), Session("ContractorID"), " AND AccountName like '%" & TxtAname.Text & "%' ")

        'With clsAct
        '    .ContractorID = Session("ContractorID").ToString
        '    ._WhereString.Append(" and AccountName like '%" & TxtAname.Text & "%' AND (IsDeleted is null or IsDeleted=0)")
        '    DSAct = .getAccountList()
        'End With
        clsAct = Nothing
        Dim K As Integer
        If DSAct.Tables.Count > 0 Then
            If DSAct.Tables(0).Rows.Count > 0 Then
                DRec1 = DSAct.Tables(0).CreateDataReader
                K = 0
                If DRec1.HasRows Then
                    While DRec1.Read
                        K = K + 1
                        Dim c As New TableCell
                        Dim c1 As New TableCell
                        Dim c2 As New TableCell
                        Dim c3 As New TableCell
                        Dim r As New TableRow
                        Dim CB1 As New RadioButton
                        r.CssClass = "common"
                        CB1.ID = DRec1("AccountID").ToString
                        CB1.GroupName = "AccountID"
                        CB1.Checked = "True"
                        TotAct.Value = K
                        c.Text = DRec1("AccountName")
                        c1.Text = DRec1("AccountNo")
                        c3.Controls.Add(CB1)
                        r.Cells.Add(c3)
                        r.Cells.Add(c)
                        r.Cells.Add(c1)
                        Table1.Rows.Add(r)
                    End While
                End If
            End If
        Else
            ActState.Value = 0
            DispBox.Visible = True

            DispBox.Text = "No Records Found"
            PageStatus()
            Exit Sub

        End If
        DSAct.Dispose()
        DRec1 = Nothing

        Dim CB As New Button
        Dim r2 As New TableRow
        Dim c4 As New TableCell
        c4.ColumnSpan = 3
        c4.Style("text-align") = "center"
        btnsubmit3.Visible = True

        c4.Controls.Add(btnsubmit3)

        r2.Cells.Add(c4)
        Table1.Rows.Add(r2)
    End Sub

    Protected Sub BtnSubmit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit2.Click
        ActState.Value = 1
        HActID.Value = Request("AccountID")
        PageStatus()
    End Sub

    Protected Sub btnsubmit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsubmit3.Click
        ActState.Value = 2
        HActID.Value = Request("AccountID")
        HDictID.Value = Request("PhyID")
        PageStatus()
    End Sub


    Sub PageStatus()

        If ActState.Value = 0 Then
            LoadPage()
        ElseIf ActState.Value = 1 Then
            ActSearch_Click()
        ElseIf ActState.Value = 2 Then
            PhySearch_Click()
        ElseIf ActState.Value = 3 Then
            ShowData(Request("PhyID"))
        End If

    End Sub
    Protected Sub PhySearch_Click()

        Panel6.Visible = False
        Panel5.Visible = False
        Panel1.Visible = True
        Panel2.Visible = False

        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both

        Dim clsPhy As New ETS.BL.Physicians
        Dim DSPhy As DataSet = clsPhy.getPhysiciansList(Session("ContractorID"), Session("WorkgroupID"), HActID.Value)
        clsPhy = Nothing
        Dim K As Integer
        If DSPhy.Tables.Count > 0 Then
            K = 0
            For Each DRRec1 As DataRow In DSPhy.Tables(0).Rows

                K = K + 1
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow
                r.CssClass = "common"
                Dim RB As New RadioButton
                RB.ID = DRRec1("PhysicianId").ToString
                RB.GroupName = "PhyID"
                RB.Checked = True
                c.Text = DRRec1("firstname").ToString
                c1.Text = DRRec1("lastname").ToString
                c2.Text = DRRec1("PinNo").ToString
                c3.Controls.Add(RB)
                r.Cells.Add(c3)
                r.Cells.Add(c)
                r.Cells.Add(c1)
                r.Cells.Add(c2)
                Table2.Rows.Add(r)

            Next
        Else
            ActState.Value = 1
            DispBox.Visible = True
            DispBox.Text = "No dictator is assigned to this account"
            PageStatus()
            Exit Sub
        End If
        DSPhy.Dispose()

        Dim CB As New Button
        Dim r2 As New TableRow
        Dim c4 As New TableCell

        c4.ColumnSpan = 4
        c4.Style("text-align") = "center"
        btnSubmit4.Visible = True

        c4.Controls.Add(btnSubmit4)


        r2.Cells.Add(c4)
        Table2.Rows.Add(r2)

    End Sub


    Sub ShowData(ByVal PhyID As String)

        Panel6.Visible = False
        Panel5.Visible = False
        Panel1.Visible = False
        Panel2.Visible = True

        Dim RecFound As String


        RecFound = "No"
        DLPhyID.Items.Clear()
        Dim clsPhy As New ETS.BL.Physicians
        With clsPhy
            .AccountID = HActID.Value
            Dim DSPhy As DataSet = .getPhysiciansList(Session("ContractorID"), Session("WorkGroupID").ToString, HActID.Value)
            DSPhy.Tables(0).Columns.Add("PhyName", GetType(System.String), "ISNULL(FirstName,'')+' '+ISNULL(LastName,'')+'('+PinNo+')'")
            DLPhyID.DataSource = DSPhy
            DLPhyID.DataValueField = "PhysicianID"
            DLPhyID.DataTextField = "PhyName"
            DLPhyID.DataBind()
            DSPhy.Dispose()
            DLPhyID.Items.FindByValue(PhyID).Selected = True
            .PhysicianID = PhyID
            .getPhysicianDetails()
            Dim dsQS As DataSet = .getPhysiciansExpectedQScore()
            If Not dsQS Is Nothing Then
                If dsQS.Tables(0).Rows.Count > 0 Then
                    If Convert.IsDBNull(dsQS.Tables(0).Rows(0).Item("ExpQScore")) Then
                        txtExpQS.Text = "0.75"
                    Else
                        txtExpQS.Text = dsQS.Tables(0).Rows(0).Item("ExpQScore")
                    End If
                Else
                    txtExpQS.Text = "0.75"
                End If
            Else
                txtExpQS.Text = "0.75"
            End If
            'txtExpQS.text = iif(dsQS.Tables(0).Rows(0).Item("ExpQScore") = DBNull, 0.75, dsQS.Tables(0).Rows(0).Item("ExpQScore"))
            HDictID.Value = .PhysicianID
            TxtFirstName.Text = .FirstName
            TxtMiddleName.Text = .MiddleName
            TxtLastName.Text = .LastName
            TxtEmail.Text = .Email
            TxtPhoneno.Text = .PhoneNo
            TxtSpeciality.Text = .Speciality
            TxtSignedName.Text = .SignedName
            TxProvID.Text = .ProviderID
            txtFax.Text = .Fax
            Dim ProcFolder As String = Server.MapPath("../ETS_Files")
            Dim Signfile As String = ProcFolder & "\" & "ESignature1\" & HDictID.Value & ".jpg"
            'Response.Write(Signfile)
            If Not IO.File.Exists(Signfile) Then
                Image1.Visible = False
            Else
                Image1.Visible = True
                Image1.ImageUrl = WebAddress & "\ets_files\ESignature1\" & HDictID.Value & ".jpg"
            End If


            chkFax.Checked = .sendfax
            'If .ForcedRouting Then
            '    Dropdownlist1.Items(1).Selected = False
            '    Dropdownlist1.Items(0).Selected = True
            'Else
            '    Dropdownlist1.Items(0).Selected = False
            '    Dropdownlist1.Items(1).Selected = True
            'End If
            'Response.Write(.SignatureLevel)
            If .SignatureLevel = 1 Then
                DropDownList2.Items(0).Selected = True
                DropDownList2.Items(1).Selected = False
            Else
                DropDownList2.Items(1).Selected = True
                DropDownList2.Items(0).Selected = False
            End If
            If .IsDeleted Then
                DLStatus.Items(0).Selected = False
                DLStatus.Items(1).Selected = True
            Else
                DLStatus.Items(1).Selected = False
                DLStatus.Items(0).Selected = True
            End If
            If .FaxPlus Then
                DDLAutoFax.Items(0).Selected = False
                DDLAutoFax.Items(1).Selected = True
                If .FaxPlusMode Then
                    DDLFaxmode.Items(1).Selected = False
                    DDLFaxmode.Items(0).Selected = True
                Else
                    DDLFaxmode.Items(0).Selected = False
                    DDLFaxmode.Items(1).Selected = True
                End If
            Else
                DDLAutoFax.Items(1).Selected = False
                DDLAutoFax.Items(0).Selected = True
                DDLFaxmode.Items(0).Selected = False
                DDLFaxmode.Items(1).Selected = True
            End If
            If .DemoBusting Then
                DLDBStatus.Items(0).Selected = True
                DLDBStatus.Items(1).Selected = False
            Else
                DLDBStatus.Items(1).Selected = True
                DLDBStatus.Items(0).Selected = False
            End If

            If Trim(UCase(.Category)) = "A" Then
                DLCategory.SelectedIndex = 1
            ElseIf Trim(UCase(.Category)) = "B" Then
                DLCategory.SelectedIndex = 2
            Else
                DLCategory.SelectedIndex = 0
            End If
            'For Each c As Char In .ExSignedName.ToString
            '    Response.Write("Char : " & c & "-->" & System.Convert.ToInt32(c) & "<BR>")
            'Next

            'For i As Integer = 0 To Len(.ExSignedName.ToString) - 1

            '    Response.Write("Char : " & .ExSignedName.Chars(i).ToString & " " & .ExSignedName.Chars(i).ToString & "<BR>")
            'Next
            txtExSignedName.Text = .ExSignedName.ToString
        End With
        clsPhy = Nothing
        Dim DSDC As New DataSet
        Dim clsDC As New ETS.BL.DictationCodes
        With clsDC
            .PhysicianID = PhyID
            DSDC = .getPhysiciansDCList()
        End With
        clsDC = Nothing

        Dim icount As Integer
        If DSDC.Tables.Count > 0 Then
            tblcodes.Visible = True
            For Each DRRec2 As DataRow In DSDC.Tables(0).Rows
                icount = icount + 1
                Dim Cell1 As New TableCell
                Dim Cell2 As New TableCell
                Cell1.CssClass = "ADACCESS1"
                Cell2.CssClass = "ADACCESS1"
                Dim Row1 As New TableRow
                Cell1.Text = icount
                Cell2.Text = DRRec2("DictationCode").ToString
                Row1.Cells.Add(Cell1)
                Row1.Cells.Add(Cell2)
                tblcodes.Rows.Add(Row1)
            Next
        Else
            tblcodes.Visible = False
        End If
        DSDC.Dispose()
        Dim DSAct As New DataSet
        Dim clsAct As New ETS.BL.Accounts
        With clsAct
            .ContractorID = Session("ContractorID")
            ._WhereString.Append(" and (ISDeleted is null or IsDeleted=0)")
            DSAct = .getAccountList()
        End With
        clsAct = Nothing
        ActID.DataSource = DSAct
        ActID.DataValueField = "AccountID"
        ActID.DataTextField = "AccountName"
        ActID.DataBind()
        DSAct.Dispose()
        ActID.Items.FindByValue(HActID.Value).Selected = True

    End Sub

    Protected Sub btnSubmit4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit4.Click
        If Request("PhyID") <> "" Then
            ActState.Value = 3
            HDictID.Value = Request("PhyID")
            PageStatus()
        Else
            DispBox.Visible = True
            DispBox.Text = "Please select dictator ID"
            PageStatus()
        End If

    End Sub


    Sub LoadPage()
        BtnAssign.Visible = False
        Table4.Visible = False
        Panel6.Visible = False
        Panel1.Visible = False
        Panel2.Visible = False
        HDictCode.Value = 1
        Panel5.Visible = True
        ActState.Value = 0
    End Sub

    Protected Sub DLPhyID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLPhyID.SelectedIndexChanged
        'HDictID.Value = DLPhyID.SelectedValue
        ShowData(DLPhyID.SelectedValue)
    End Sub

    Protected Sub ImageButton1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImageButton1.Click
        Try
            If FileUpload1.HasFile Then
                If UCase(Right(FileUpload1.FileName, 3)) = "JPG" Then
                    Dim ProcFolder As String = Server.MapPath("../ETS_Files")
                    Dim SignFolder As String = ProcFolder & "\" & "ESignature1"
                    If Not IO.Directory.Exists(SignFolder) Then
                        IO.Directory.CreateDirectory(SignFolder)
                    End If
                    Dim SignFile As String = SignFolder & "\" & HDictID.Value & ".jpg"
                    If IO.File.Exists(SignFile) Then
                        IO.File.Delete(SignFile)
                    End If
                    FileUpload1.SaveAs(SignFile)

                End If
            End If
            Dim clsPhy As New ETS.BL.Physicians

            With clsPhy
                .PhysicianID = HDictID.Value
                .FirstName = Replace(Request("TxtFirstName"), "'", "''")
                .MiddleName = Request("TxtMiddleName")
                .LastName = Replace(Request("TxtLastName"), "'", "''")
                .Email = Request("TxtEmail")
                .PhoneNo = Request("TxtPhoneno")
                .Speciality = Request("TxtSpeciality")
                .SignedName = Replace(Request("TxtSignedName"), "'", "''")
                If Request("TxProvID") <> "" Then
                    .ProviderID = Replace(Request("TxProvID"), "'", "''")
                Else
                    .ProviderID = Request("TxProvID")
                End If

                .Fax = Request("TxtFax")
                .IsDeleted = DLStatus.SelectedValue
                .DemoBusting = DLDBStatus.SelectedValue
                .Category = DLCategory.SelectedValue
                .sendfax = chkFax.Checked
                .FaxPlus = DDLAutoFax.SelectedValue
                If chkFax.Checked Then
                    .FaxPlusMode = DDLFaxmode.SelectedValue
                End If
                '.ForcedRouting = DropDownList1.SelectedValue
                .SignatureLevel = DropDownList2.SelectedValue
                If Not String.IsNullOrEmpty(txtExSignedName.Text.ToString) Then
                    .ExSignedName = Replace(txtExSignedName.Text.ToString, "'", "''")
                Else
                    If Not String.IsNullOrEmpty(._UpdateString.ToString) Then
                        ._UpdateString.Append(", ExSignedName = NULL ")
                    Else
                        ._UpdateString.Append(" ExSignedName = NULL ")
                    End If
                End If

                If .UpdatePhysicianDetails > 0 Then
                    If DLDBStatus.SelectedValue Then
                        .UpdatePhysiciansExpectedQScore(txtExpQS.Text)
                    End If
                    DispBox.Text = "The dictator has been updated successfully."
                Else
                    DispBox.Text = "Updating Physician Data failed"
                End If
                DispBox.Visible = True
                ShowData(HDictID.Value)
            End With
            clsPhy = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
End Class
