Imports System.Data.SqlClient

Partial Class Billing_LCMethods_LCMethodology
    Inherits BasePage




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'TableCell13.Visible = False

        'If Not IsPostBack Then

        '    'Dim month As DateTime = Convert.ToDateTime(Today)
        '    'For i As Integer = 0 To 11
        '    '    Dim NextMont As DateTime = month.AddMonths(i)
        '    '    Dim list As New ListItem()
        '    '    list.Text = NextMont.ToString("MMMM")
        '    '    list.Value = NextMont.Month.ToString()
        '    '    DLMonth.Items.Add(list)
        '    'Next
        '    'DLMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = True
        '    'Dim Year As DateTime = Convert.ToDateTime(Today)
        '    'For i As Integer = 0 To 11
        '    '    Dim NextMont As DateTime = Year.AddYears(i)
        '    '    Dim list As New ListItem()
        '    '    list.Text = NextMont.Year
        '    '    list.Value = NextMont.Year
        '    '    DLYear.Items.Add(list)
        '    'Next
        '    'DLYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = True

        '    Dim strConn As String
        '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        '    Dim SQLCmd1 As New SqlCommand("Select * from AdminETS.dbo.tblaccounts order by Accountname", New SqlConnection(strConn))
        '    Try
        '        SQLCmd1.Connection.Open()
        '        Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
        '        If DRRec1.HasRows = True Then
        '            While DRRec1.Read
        '                Dim LI As New ListItem
        '                LI.Text = DRRec1("Accountname")
        '                LI.Value = DRRec1("AccountID").ToString
        '                DLAct.Items.Add(LI)
        '            End While
        '        End If
        '        DRRec1.Close()

        '    Finally
        '        If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
        '            SQLCmd1.Connection.Close()
        '            SQLCmd1 = Nothing
        '        End If
        '    End Try

        '    Table1.Visible = False
        '    Table3.Visible = False
        '    Table4.Visible = False
        '    SubButton.Visible = False
        'Else
        '    Table1.Visible = True
        '    Table3.Visible = True
        '    SubButton.Visible = True

        'End If

        ShowDetails()
    End Sub




    Protected Sub ShowDetails()
        'WTRow.Visible = False
        Dim strConn As String
        Dim Minbilling As String
        Dim strCycle As String
        Dim strActname As String
        Dim strMode As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd As New SqlCommand("Select Mode, AccountName, BillActNumber, AccountID, MinBilling, Cycle from tblAccounts where (IsDeleted is null or isdeleted = 'False') and (Indirect = 'False' OR Indirect IS NULL) order by accountname", New SqlConnection(strConn))
        ' Response.Write("Select Mode, AccountName, AccountID, MinBilling, Cycle from tblAccounts where accountid ='" & DRRec("AccountID").ToString & "'")

        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows = True Then
                While DRRec.Read

                    'Dim Cell1 As New TableCell
                    'Dim Cell2 As New TableCell
                    'Dim Cell3 As New TableCell
                    'Dim Cell4 As New TableCell
                    'Dim Cell5 As New TableCell
                    'Dim Cell6 As New TableCell
                    'Dim Cell7 As New TableCell
                    'Dim Cell8 As New TableCell

                    'Response.Write(DRRec("mode"))
                    HMode.Value = Trim(DRRec("Mode").ToString)
                    'HAccountID.Value = DRRec("AccountID").ToString
                    If IsDBNull(DRRec("MinBilling")) Then
                        Minbilling = ""
                    Else
                        Minbilling = FormatNumber(DRRec("MinBilling").ToString, 3)
                    End If
                    If IsDBNull(DRRec("Cycle")) Then
                        strCycle = "No"
                    ElseIf DRRec("Cycle") = "True" Then
                        strCycle = "Yes"
                    Else
                        strCycle = "No"
                    End If
                    If HMode.Value = "S" Or HMode.Value = "" Then
                        strMode = "Standard"
                    ElseIf HMode.Value = "DV" Then
                        strMode = "DeviceWise"
                    ElseIf HMode.Value = "LC" Then
                        strMode = "LocationWise"
                    ElseIf HMode.Value = "DC" Then
                        strMode = "DictatorWise"
                    End If



                    If HMode.Value = "S" Or HMode.Value = "" Then

                        'WTRow.Visible = True
                        Dim SQLCmd1 As New SqlCommand("Select Top 1 B.*, L.MethodName from AdminSecureweb.dbo.BillDetails B LEFT OUTER JOIN AdminETS.dbo.tbllcmethodology L ON B.LCMethodID = L.TrackID where B.AccountID='" & DRRec("AccountID").ToString & "' order by B.ModDate Desc", New SqlConnection(strConn))
                        'Response.Write("Select Top 1 B.*, L.MethodName from AdminSecureweb.dbo.BillDetails B LEFT OUTER JOIN AdminETS.dbo.tbllcmethodology L ON B.LCMethodID = L.TrackID where B.AccountID='" & DRRec("AccountID").ToString & "' order by B.ModDate Desc")
                        Try
                            SQLCmd1.Connection.Open()
                            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                            If DRRec1.HasRows = True Then
                                HRecExist.Value = "Yes"
                                If DRRec1.Read Then
                                    If IsDBNull(DRRec1("WTMode")) Or DRRec1("WTMode").ToString.ToLower = "false" Then
                                        'DLWType.Items(0).Selected = False
                                        'DLWType.Items(1).Selected = True
                                        'Table1.Visible = True
                                        'Table4.Visible = False

                                        Dim Row1 As New TableRow
                                        Dim Cell1 As New TableCell
                                        Dim Cell1N As New TableCell
                                        Dim Cell2 As New TableCell
                                        Dim Cell3 As New TableCell
                                        Dim Cell4 As New TableCell
                                        Dim Cell5 As New TableCell
                                        Dim Cell6 As New TableCell
                                        Dim Cell7 As New TableCell
                                        Dim Cell8 As New TableCell
                                        Dim Cell9 As New TableCell
                                        Dim Cell10 As New TableCell
                                        Dim DDLC As New DropDownList
                                        DDLC.ID = "DLMethodID1"
                                        Row1.CssClass = "tblbg2"
                                        'Dim X As Integer
                                        'For X = 0 To DDLC.Items.Count - 1
                                        '    If DRRec1("LCMethodID").ToString = DDLC.Items(X).Value Then
                                        '        DDLC.Items(X).Selected = True
                                        '        Exit For
                                        '    End If
                                        'Next
                                        Cell8.Text = DRRec1("MethodName").ToString
                                        ' Response.Write(DRRec1("MethodName").ToString)

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
                                        Cell1.Text = "<a href='../LCMethods/upunitdetails.aspx?actid=" & DRRec("AccountID").ToString & "'>" & DRRec("AccountName").ToString & "</a>"
                                        Cell1N.Text = DRRec("BillActNumber").ToString
                                        Cell2.Text = Minbilling
                                        Cell3.Text = strCycle

                                        Cell4.Text = DRRec1("WTMode").ToString
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
                                            Cell5.Text = ""
                                        Else
                                            Cell5.Text = FormatNumber(DRRec1("Rate").ToString, 3)
                                        End If
                                        If IsDBNull(DRRec1("MiscRate")) Then
                                            Cell6.Text = ""
                                        Else
                                            Cell6.Text = FormatNumber(DRRec1("MiscRate").ToString, 3)
                                        End If
                                        If IsDBNull(DRRec1("StatRate")) Then
                                            Cell7.Text = ""
                                        Else
                                            Cell7.Text = FormatNumber(DRRec1("StatRate").ToString, 3)
                                        End If

                                        Dim SelMonth As Integer
                                        SelMonth = 0
                                        Dim SelYear As Integer
                                        SelYear = 0



                                        Row1.Cells.Add(Cell1)
                                        Row1.Cells.Add(Cell1N)
                                        Cell9.Text = strMode
                                        Row1.Cells.Add(Cell9)
                                        Row1.Cells.Add(Cell10)
                                        Row1.Cells.Add(Cell2)
                                        Row1.Cells.Add(Cell3)
                                        Row1.Cells.Add(Cell4)
                                        Row1.Cells.Add(Cell5)
                                        Row1.Cells.Add(Cell6)
                                        Row1.Cells.Add(Cell7)
                                        Row1.Cells.Add(Cell8)
                                        Table1.Rows.Add(Row1)
                                        'Else
                                        '    Table4.Visible = True
                                        '    Table1.Visible = False
                                        '    DLWType.Items(1).Selected = False
                                        '    DLWType.Items(0).Selected = True
                                        '    Consult.Text = DRRec1("Consult").ToString
                                        '    HP.Text = DRRec1("HP").ToString
                                        '    Discharge.Text = DRRec1("Discharge").ToString
                                        '    IME.Text = DRRec1("IME").ToString
                                        '    Letter.Text = DRRec1("Letter").ToString
                                        '    PrNote.Text = DRRec1("PrNote").ToString
                                        '    OpNote.Text = DRRec1("OpNote").ToString
                                        '    PsychEval.Text = DRRec1("PsychEval").ToString
                                        '    Rate.Text = DRRec1("Rate").ToString
                                        '    StatRate.Text = DRRec1("statrate").ToString
                                        '    LCMethods(DLDefLC)
                                        '    Dim X As Integer
                                        '    For X = 0 To DLDefLC.Items.Count - 1
                                        '        If DRRec1("LCMethodID").ToString = DLDefLC.Items(X).Value Then
                                        '            DLDefLC.Items(X).Selected = True
                                        '            Exit For
                                        '        End If
                                        '    Next
                                    Else
                                        Dim Row1 As New TableRow
                                        Dim Cell1 As New TableCell
                                        Dim Cell1N As New TableCell
                                        Dim Cell2 As New TableCell
                                        Dim Cell3 As New TableCell
                                        Dim Cell4 As New TableCell
                                        Dim Cell5 As New TableCell
                                        Dim Cell6 As New TableCell
                                        Dim Cell7 As New TableCell
                                        Dim Cell8 As New TableCell
                                        Dim Cell9 As New TableCell
                                        Dim Cell10 As New TableCell
                                        Row1.Cells.Add(Cell1)
                                        Row1.Cells.Add(Cell1N)
                                        Row1.CssClass = "tblbg2"
                                        Row1.Font.Size = "8"
                                        Cell9.Text = strMode
                                        Row1.Cells.Add(Cell9)
                                        Row1.Cells.Add(Cell10)
                                        Row1.Cells.Add(Cell2)
                                        Row1.Cells.Add(Cell3)
                                        Row1.Cells.Add(Cell4)
                                        Row1.Cells.Add(Cell5)
                                        Row1.Cells.Add(Cell6)
                                        Row1.Cells.Add(Cell7)
                                        Row1.Cells.Add(Cell8)
                                        Table1.Rows.Add(Row1)
                                    End If

                                End If
                            Else

                                Dim Row1 As New TableRow
                                Dim Cell1 As New TableCell
                                Dim Cell1N As New TableCell
                                Dim Cell2 As New TableCell
                                Dim Cell3 As New TableCell
                                Dim Cell4 As New TableCell
                                Dim Cell5 As New TableCell
                                Dim Cell6 As New TableCell
                                Dim Cell7 As New TableCell
                                Dim Cell8 As New TableCell
                                Dim Cell9 As New TableCell
                                Dim Cell10 As New TableCell
                                Dim DDLC As New DropDownList
                                Row1.Font.Size = "8"
                                Cell1.Text = "<a href='../LCMethods/upunitdetails.aspx?actid=" & DRRec("AccountID").ToString & "'>" & DRRec("AccountName").ToString & "</a>"
                                Cell1N.Text = DRRec("BillActNumber").ToString
                                Row1.CssClass = "tblbg2"
                                Row1.Font.Size = "8"
                                Row1.Cells.Add(Cell1)
                                Row1.Cells.Add(Cell1N)
                                Cell9.Text = strMode
                                Row1.Cells.Add(Cell9)
                                Row1.Cells.Add(Cell10)
                                Row1.Cells.Add(Cell2)
                                Row1.Cells.Add(Cell3)
                                Row1.Cells.Add(Cell4)
                                Row1.Cells.Add(Cell5)
                                Row1.Cells.Add(Cell6)
                                Row1.Cells.Add(Cell7)
                                Row1.Cells.Add(Cell8)
                                Table1.Rows.Add(Row1)
                            End If
                        Finally
                            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd1.Connection.Close()
                                SQLCmd1 = Nothing
                            End If
                        End Try
                    ElseIf HMode.Value = "DV" Then

                        'TableCell13.Visible = True
                        Dim SQLCmd1 As New SqlCommand("Select Top 1 B.*, L.MethodName from AdminSecureweb.dbo.BillDetails B LEFT OUTER JOIN AdminETS.dbo.tbllcmethodology L ON B.LCMethodID = L.TrackID  where AccountID='" & DRRec("AccountID").ToString & "' order by ModDate Desc", New SqlConnection(strConn))
                        Try
                            SQLCmd1.Connection.Open()
                            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                            If DRRec1.HasRows = True Then
                                HRecExist.Value = "Yes"
                                If DRRec1.Read Then
                                    Dim Row1 As New TableRow
                                    Dim Cell1 As New TableCell
                                    Dim Cell1N As New TableCell
                                    Dim Cell2 As New TableCell
                                    Dim Cell3 As New TableCell
                                    Dim Cell4 As New TableCell
                                    Dim Cell5 As New TableCell
                                    Dim Cell6 As New TableCell
                                    Dim Cell7 As New TableCell
                                    Dim Cell8 As New TableCell
                                    Dim Cell9 As New TableCell
                                    Dim Cell10 As New TableCell
                                    Dim DDLC As New DropDownList
                                    DDLC.ID = "DLMethodID1"
                                    Row1.CssClass = "tblbg2"
                                    'Dim X As Integer
                                    'For X = 0 To DDLC.Items.Count - 1
                                    '    If DRRec1("LCMethodID").ToString = DDLC.Items(X).Value Then
                                    '        DDLC.Items(X).Selected = True
                                    '        Exit For
                                    '    End If
                                    'Next
                                    Cell8.Text = DRRec1("MethodName").ToString

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
                                    Cell1.Text = "<a href='../LCMethods/upunitdetails.aspx?actid=" & DRRec("AccountID").ToString & "'>" & DRRec("AccountName").ToString & "</a>"
                                    Cell1N.Text = DRRec("BillActNumber").ToString
                                    Cell2.Text = Minbilling
                                    Cell3.Text = strCycle
                                    Cell4.Text = DRRec1("WTMode").ToString
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
                                        Cell5.Text = ""
                                    Else
                                        Cell5.Text = FormatNumber(DRRec1("Rate").ToString, 3)
                                    End If
                                    If IsDBNull(DRRec1("MiscRate")) Then
                                        Cell6.Text = ""
                                    Else
                                        Cell6.Text = FormatNumber(DRRec1("MiscRate").ToString, 3)
                                    End If
                                    If IsDBNull(DRRec1("StatRate")) Then
                                        Cell7.Text = ""
                                    Else
                                        Cell7.Text = FormatNumber(DRRec1("StatRate").ToString, 3)
                                    End If

                                    Dim SelMonth As Integer
                                    SelMonth = 0
                                    Dim SelYear As Integer
                                    SelYear = 0



                                    Row1.Cells.Add(Cell1)
                                    Row1.Cells.Add(Cell1N)
                                    Cell9.Text = strMode
                                    Row1.Cells.Add(Cell9)
                                    Row1.Cells.Add(Cell10)
                                    Row1.Cells.Add(Cell2)
                                    Row1.Cells.Add(Cell3)
                                    Row1.Cells.Add(Cell4)
                                    Row1.Cells.Add(Cell5)
                                    Row1.Cells.Add(Cell6)
                                    Row1.Cells.Add(Cell7)
                                    Row1.Cells.Add(Cell8)
                                    Table1.Rows.Add(Row1)

                                End If
                            Else
                                HRecExist.Value = "No"
                                Dim Row1 As New TableRow
                                Dim Cell1 As New TableCell
                                Dim Cell1N As New TableCell
                                Dim Cell2 As New TableCell
                                Dim Cell3 As New TableCell
                                Dim Cell4 As New TableCell
                                Dim Cell5 As New TableCell
                                Dim Cell6 As New TableCell
                                Dim Cell7 As New TableCell
                                Dim Cell8 As New TableCell
                                Dim Cell9 As New TableCell
                                Dim Cell10 As New TableCell
                                Dim DDLC As New DropDownList
                                Row1.CssClass = "tblbg2"
                                Row1.Font.Size = "8"
                                DDLC.ID = "DLMethodID1"
                                'LCMethods(DDLC)
                                DDLC.Items(0).Selected = True
                                Cell5.Controls.Add(DDLC)
                                Dim Lbl As New Label
                                Row1.Font.Size = "8"
                                Lbl.Font.Name = "Arial"
                                Lbl.ID = "lblAct1"
                                Cell1.Text = "<a href='../LCMethods/upunitdetails.aspx?actid=" & DRRec("AccountID").ToString & "'>" & DRRec("AccountName").ToString & "</a>"
                                Cell1N.Text = DRRec("BillActNumber").ToString
                                'Response.Write(DRRec("AccountName").ToString)

                                'Dim Txt1 As New TextBox
                                'Dim Txt2 As New TextBox
                                'Dim Txt3 As New TextBox
                                'Dim HF1 As New HiddenField
                                'HF1.ID = "HActID1"
                                'HF1.Value = DRRec("AccountID").ToString
                                'Form.Controls.Add(HF1)
                                'Dim HM1 As New HiddenField
                                'HM1.ID = "tblActMode1"
                                'HM1.Value = DRRec("Mode").ToString
                                '' Txt1.Style("
                                'Txt1.Width = "50"
                                'Txt2.Width = "50"
                                'Txt3.Width = "50"
                                'Txt1.ID = "TxRate1"
                                'Txt1.Text = ""
                                'Txt2.ID = "TxMRate1"
                                'Txt2.Text = ""
                                'Txt3.ID = "TxSTRate1"
                                'Txt3.Text = ""
                                'Txt1.Font.Size = "8"
                                'Txt1.Font.Name = "Arial"
                                'Txt2.Font.Size = "8"
                                'Txt2.Font.Name = "Arial"
                                'Txt3.Font.Size = "8"
                                'Txt3.Font.Name = "Arial"
                                'Cell1.Controls.Add(Lbl)
                                'Cell2.Controls.Add(Txt1)
                                'Cell3.Controls.Add(Txt2)
                                'Cell4.Controls.Add(Txt3)
                                Row1.Cells.Add(Cell1)
                                Row1.Cells.Add(Cell1N)
                                Row1.Cells.Add(Cell2)
                                Row1.Cells.Add(Cell3)
                                Row1.Cells.Add(Cell4)
                                Row1.Cells.Add(Cell5)
                                Table1.Rows.Add(Row1)
                            End If
                        Finally
                            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd1.Connection.Close()
                                SQLCmd1 = Nothing
                            End If
                        End Try
                    ElseIf HMode.Value = "LC" Then
                        'Response.Write("Found Account")

                        'Dim i As Integer
                        'For i = 0 To DLMonth.Items.Count - 1
                        '    DLMonth.Items(i).Selected = False
                        '    DLYear.Items(i).Selected = False
                        '    If DRRec1("EffMonth") = DLMonth.Items(i).Value Then
                        '        LSelMonth = i
                        '    End If
                        '    If DRRec1("EffYear") = DLYear.Items(i).Value Then
                        '        LSelYear = i
                        '    End If
                        'Next
                        'DLMonth.Items(LSelMonth).Selected = True
                        'DLYear.Items(LSelYear).Selected = True
                        'Response.Write("Select A.Accountname, LA.LocCode, LA.LocName, LA.TrackID from tblAccountsLocations LA, tblAccounts A where LA.AccountID=A.AccountID and LA.AccountID ='" & DRRec("AccountID").ToString & "' order by LA.LocCode")
                        Dim k As Integer
                        k = 0
                        Dim SQLCmd2 As New SqlCommand("Select A.Accountname, LA.LocCode, LA.LocName, LA.TrackID from tblAccountsLocations LA, tblAccounts A where LA.AccountID=A.AccountID and LA.AccountID ='" & DRRec("AccountID").ToString & "' order by LA.LocCode", New SqlConnection(strConn))
                        'Response.Write("1")
                        Try
                            SQLCmd2.Connection.Open()
                            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                            If DRRec2.HasRows Then
                                While (DRRec2.Read)
                                    k = k + 1
                                    Dim SQLCmd3 As New SqlCommand("Select B.*, L.MethodName from AdminSecureweb.dbo.BillDetails B LEFT OUTER JOIN AdminETS.dbo.tbllcmethodology L ON B.LCMethodID = L.TrackID where AccountID='" & DRRec("AccountID").ToString & "' AND SubActID ='" & DRRec2("TrackID").ToString & "'order by ModDate Desc", New SqlConnection(strConn))
                                    ' Response.Write("Select * from AdminSecureweb.dbo.BillDetails where AccountID='" & DRRec("AccountID").ToString & "' AND SubActID ='" & DRRec2("TrackID").ToString & "'order by ModDate Desc")
                                    Try
                                        SQLCmd3.Connection.Open()
                                        Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                        If DRRec3.HasRows = True Then
                                            '  Response.Write("2")
                                            HRecExist.Value = "Yes"
                                            If DRRec3.Read Then
                                                Dim Row1 As New TableRow
                                                Dim Cell1 As New TableCell
                                                Dim Cell1N As New TableCell
                                                Dim Cell2 As New TableCell
                                                Dim Cell3 As New TableCell
                                                Dim Cell4 As New TableCell
                                                Dim Cell5 As New TableCell
                                                Dim Cell6 As New TableCell
                                                Dim Cell7 As New TableCell
                                                Dim Cell8 As New TableCell
                                                Dim Cell9 As New TableCell
                                                Dim Cell10 As New TableCell
                                                Dim DDLC As New DropDownList
                                                Row1.CssClass = "tblbg2"
                                                DDLC.ID = "DLMethodID1"

                                                'Dim X As Integer
                                                'For X = 0 To DDLC.Items.Count - 1
                                                '    If DRRec1("LCMethodID").ToString = DDLC.Items(X).Value Then
                                                '        DDLC.Items(X).Selected = True
                                                '        Exit For
                                                '    End If
                                                'Next
                                                Cell8.Text = DRRec3("MethodName").ToString

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
                                                Cell1.Text = "<a href='../LCMethods/upunitdetails.aspx?actid=" & DRRec("AccountID").ToString & "'>" & DRRec("AccountName").ToString & "</a>"
                                                Cell1N.Text = DRRec("BillActNumber").ToString
                                                Cell2.Text = Minbilling
                                                Cell3.Text = strCycle
                                                Cell4.Text = DRRec3("WTMode").ToString
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
                                                If IsDBNull(DRRec3("Rate")) Then
                                                    Cell5.Text = ""
                                                Else
                                                    Cell5.Text = FormatNumber(DRRec3("Rate").ToString, 3)
                                                End If
                                                If IsDBNull(DRRec3("MiscRate")) Then
                                                    Cell6.Text = ""
                                                Else
                                                    Cell6.Text = FormatNumber(DRRec3("MiscRate").ToString, 3)
                                                End If
                                                If IsDBNull(DRRec3("StatRate")) Then
                                                    Cell7.Text = ""
                                                Else
                                                    Cell7.Text = FormatNumber(DRRec3("StatRate").ToString, 3)
                                                End If

                                                Dim SelMonth As Integer
                                                SelMonth = 0
                                                Dim SelYear As Integer
                                                SelYear = 0



                                                Row1.Cells.Add(Cell1)
                                                Row1.Cells.Add(Cell1N)
                                                Cell9.Text = strMode
                                                Cell10.Text = DRRec2("LocName").ToString
                                                Row1.Cells.Add(Cell9)
                                                Row1.Cells.Add(Cell10)
                                                Row1.Cells.Add(Cell2)
                                                Row1.Cells.Add(Cell3)
                                                Row1.Cells.Add(Cell4)
                                                Row1.Cells.Add(Cell5)
                                                Row1.Cells.Add(Cell6)
                                                Row1.Cells.Add(Cell7)
                                                Row1.Cells.Add(Cell8)
                                                Table1.Rows.Add(Row1)

                                            End If
                                        Else
                                            HRecExist.Value = "No"
                                            Dim LRow1 As New TableRow
                                            Dim LCell1 As New TableCell
                                            Dim LCell1N As New TableCell
                                            Dim LCell2 As New TableCell
                                            Dim LCell3 As New TableCell
                                            Dim LCell4 As New TableCell
                                            Dim LCell5 As New TableCell
                                            Dim LCell6 As New TableCell
                                            Dim LCell7 As New TableCell
                                            Dim lCell8 As New TableCell
                                            Dim lCell9 As New TableCell
                                            Dim lCell10 As New TableCell
                                            LCell1.Text = "<a href='../LCMethods/upunitdetails.aspx?actid=" & DRRec("AccountID").ToString & "'>" & DRRec("AccountName").ToString & "</a>"
                                            LCell1N.Text = DRRec("BillActNumber").ToString
                                            LRow1.CssClass = "tblbg2"
                                            LRow1.Font.Size = "8"
                                            LRow1.Cells.Add(LCell1)
                                            LRow1.Cells.Add(LCell1N)
                                            lCell9.Text = strMode
                                            lCell10.Text = DRRec2("LocName").ToString
                                            LRow1.Cells.Add(lCell9)
                                            LRow1.Cells.Add(lCell10)
                                            LRow1.Cells.Add(LCell2)
                                            LRow1.Cells.Add(LCell3)
                                            LRow1.Cells.Add(LCell4)
                                            LRow1.Cells.Add(LCell5)
                                            LRow1.Cells.Add(LCell6)
                                            LRow1.Cells.Add(LCell7)
                                            LRow1.Cells.Add(lCell8)
                                            Table1.Rows.Add(LRow1)
                                        End If

                                    Finally
                                        If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                            SQLCmd3.Connection.Close()
                                            SQLCmd3 = Nothing
                                        End If
                                    End Try
                                End While

                                HRecCount.Value = k
                            Else

                            End If
                        Finally
                            If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd2.Connection.Close()
                                SQLCmd2 = Nothing
                            End If
                        End Try

                        '    'Dim Row1 As New TableRow
                        '    'Dim Cell1 As New TableCell
                        '    'Dim Cell2 As New TableCell
                        '    'Dim Cell3 As New TableCell
                        '    'Dim Cell4 As New TableCell
                        '    'Dim Cell5 As New TableCell
                        '    'Row1.Font.Size = "8"
                        '    'Dim HF1 As New HiddenField
                        '    'HF1.ID = "HActID1"
                        '    'HF1.Value = DRRec("AccountID").ToString
                        '    'Form.Controls.Add(HF1)
                        '    'Dim HM1 As New HiddenField
                        '    'HM1.ID = "tblActMode1"
                        '    'HM1.Value = DRRec("Mode").ToString

                        '    'Dim Lbl As New Label
                        '    'Lbl.ID = "lblAct1"
                        '    'Lbl.Text = DRRec("AccountName").ToString
                        '    'Dim Txt1 As New TextBox
                        '    'Dim Txt2 As New TextBox
                        '    'Dim Txt3 As New TextBox
                        '    'Txt1.ID = "TxRate1"
                        '    '' Txt1.Text = DRRec1("Rate")
                        '    'Txt2.ID = "TxMRate1"
                        '    '' Txt2.Text = DRRec1("MiscRate")
                        '    'Txt3.ID = "TxSTRate1"
                        '    'Txt1.Width = "50"
                        '    'Txt2.Width = "50"
                        '    'Txt3.Width = "50"
                        '    'If IsDBNull(DRRec1("Rate")) Then
                        '    '    Txt1.Text = ""
                        '    'Else
                        '    '    Txt1.Text = DRRec1("Rate")
                        '    'End If
                        '    'If IsDBNull(DRRec1("MiscRate")) Then
                        '    '    Txt2.Text = ""
                        '    'Else
                        '    '    Txt2.Text = DRRec1("MiscRate")
                        '    'End If
                        '    'If IsDBNull(DRRec1("StatRate")) Then
                        '    '    Txt3.Text = ""
                        '    'Else
                        '    '    Txt3.Text = DRRec1("StatRate")
                        '    'End If
                        '    'If IsDBNull(DRRec1("MinBilling")) Then
                        '    '    TxMBilling.Text = ""
                        '    'Else
                        '    '    TxMBilling.Text = DRRec1("MinBilling")
                        '    'End If
                        '    'If IsDBNull(DRRec1("Cycle")) Then
                        '    '    DLCycle.Items(0).Selected = False
                        '    '    DLCycle.Items(1).Selected = True
                        '    'ElseIf DRRec1("Cycle") = "True" Then
                        '    '    DLCycle.Items(1).Selected = False
                        '    '    DLCycle.Items(0).Selected = True
                        '    'Else
                        '    '    DLCycle.Items(0).Selected = False
                        '    '    DLCycle.Items(1).Selected = True
                        '    'End If
                        '    'Dim SelMonth As Integer
                        '    'SelMonth = 0
                        '    'Dim SelYear As Integer
                        '    'SelYear = 0
                        '    'Dim i As Integer
                        '    'For i = 0 To DLMonth.Items.Count - 1
                        '    '    DLMonth.Items(i).Selected = False
                        '    '    DLYear.Items(i).Selected = False
                        '    '    If DRRec1("EffMonth") = DLMonth.Items(i).Value Then
                        '    '        SelMonth = i
                        '    '    End If
                        '    '    If DRRec1("EffYear") = DLYear.Items(i).Value Then
                        '    '        SelYear = i
                        '    '    End If
                        '    'Next
                        '    'DLMonth.Items(SelMonth).Selected = True
                        '    'DLYear.Items(SelYear).Selected = True

                        '    'Cell1.Controls.Add(Lbl)
                        '    'Cell2.Controls.Add(Txt1)
                        '    'Cell3.Controls.Add(Txt2)
                        '    'Cell4.Controls.Add(Txt3)
                        '    'Row1.Cells.Add(Cell1)
                        '    'Row1.Cells.Add(Cell2)
                        '    'Row1.Cells.Add(Cell3)
                        '    'Row1.Cells.Add(Cell4)
                        '    'Table1.Rows.Add(Row1)



                    ElseIf HMode.Value = "DC" Then

                        Dim k As Integer
                        k = 0
                        Dim SQLCmd2 As New SqlCommand("Select A.Accountname, GD.GrpDicName, GD.GrpDicID from AdminSecureweb.dbo.GrpDictators GD, tblAccounts A where GD.AccID=A.AccountID and GD.AccID ='" & DRRec("AccountID").ToString & "' order by GD.GrpDicName", New SqlConnection(strConn))
                        'Response.Write("Select A.Accountname, GD.GrpDicName, GD.GrpDicID from AdminSecureweb.dbo.GrpDictators GD, tblAccounts A where GD.AccID=A.AccountID and GD.AccID ='" & DRRec("AccountID").ToString & "' order by GD.GrpDicName")

                        Try
                            SQLCmd2.Connection.Open()
                            Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                            If DRRec2.HasRows Then
                                While (DRRec2.Read)
                                    k = k + 1
                                    Dim SQLCmd3 As New SqlCommand("Select B.*, L.MethodName from AdminSecureweb.dbo.BillDetails B LEFT OUTER JOIN AdminETS.dbo.tbllcmethodology L ON B.LCMethodID = L.TrackID where AccountID='" & DRRec("AccountID").ToString & "' AND SubActID ='" & DRRec2("GrpDicID").ToString & "'order by ModDate Desc", New SqlConnection(strConn))
                                    Try
                                        SQLCmd3.Connection.Open()
                                        Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
                                        If DRRec3.HasRows = True Then
                                            HRecExist.Value = "Yes"
                                            If DRRec3.Read Then
                                                Dim Row1 As New TableRow
                                                Dim Cell1 As New TableCell
                                                Dim Cell1N As New TableCell
                                                Dim Cell2 As New TableCell
                                                Dim Cell3 As New TableCell
                                                Dim Cell4 As New TableCell
                                                Dim Cell5 As New TableCell
                                                Dim Cell6 As New TableCell
                                                Dim Cell7 As New TableCell
                                                Dim Cell8 As New TableCell
                                                Dim Cell9 As New TableCell
                                                Dim Cell10 As New TableCell
                                                Dim DDLC As New DropDownList
                                                DDLC.ID = "DLMethodID1"


                                                Cell8.Text = DRRec3("MethodName").ToString

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
                                                Cell1.Text = "<a href='../LCMethods/upunitdetails.aspx?actid=" & DRRec("AccountID").ToString & "'>" & DRRec("AccountName").ToString & "</a>"
                                                Cell1N.Text = DRRec("BillActNumber").ToString
                                                Cell2.Text = Minbilling
                                                Cell3.Text = strCycle
                                                Cell4.Text = DRRec3("WTMode").ToString
                                                Dim Txt1 As New TextBox
                                                Dim Txt2 As New TextBox
                                                Dim Txt3 As New TextBox
                                                Txt1.ID = "TxRate1"
                                                Txt1.Text = DRRec3("Rate")
                                                Txt2.ID = "TxMRate1"
                                                Txt2.Text = DRRec3("MiscRate")
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
                                                If IsDBNull(DRRec3("Rate")) Then
                                                    Cell5.Text = ""
                                                Else
                                                    Cell5.Text = FormatNumber(DRRec3("Rate").ToString, 3)
                                                End If
                                                If IsDBNull(DRRec3("MiscRate")) Then
                                                    Cell6.Text = ""
                                                Else
                                                    Cell6.Text = FormatNumber(DRRec3("MiscRate").ToString, 3)
                                                End If
                                                If IsDBNull(DRRec3("StatRate")) Then
                                                    Cell7.Text = ""
                                                Else
                                                    Cell7.Text = FormatNumber(DRRec3("StatRate").ToString, 3)
                                                End If

                                                Dim SelMonth As Integer
                                                SelMonth = 0
                                                Dim SelYear As Integer
                                                SelYear = 0

                                                Row1.CssClass = "tblbg2"

                                                Row1.Cells.Add(Cell1)
                                                Row1.Cells.Add(Cell1N)
                                                Cell9.Text = strMode
                                                Cell10.Text = DRRec2("GrpDicName").ToString
                                                Row1.Cells.Add(Cell9)
                                                Row1.Cells.Add(Cell10)
                                                Row1.Cells.Add(Cell2)
                                                Row1.Cells.Add(Cell3)
                                                Row1.Cells.Add(Cell4)
                                                Row1.Cells.Add(Cell5)
                                                Row1.Cells.Add(Cell6)
                                                Row1.Cells.Add(Cell7)
                                                Row1.Cells.Add(Cell8)
                                                Table1.Rows.Add(Row1)

                                            End If
                                        Else
                                            HRecExist.Value = "No"
                                            Dim LRow1 As New TableRow
                                            Dim LCell1 As New TableCell
                                            Dim LCell1N As New TableCell
                                            Dim LCell2 As New TableCell
                                            Dim LCell3 As New TableCell
                                            Dim LCell4 As New TableCell
                                            Dim LCell5 As New TableCell
                                            Dim LCell6 As New TableCell
                                            Dim LCell7 As New TableCell
                                            Dim lCell8 As New TableCell
                                            Dim lCell9 As New TableCell
                                            Dim lCell10 As New TableCell
                                            LCell1.Text = "<a href='../LCMethods/upunitdetails.aspx?actid=" & DRRec("AccountID").ToString & "'>" & DRRec("AccountName").ToString & "</a>"
                                            LCell1N.Text = DRRec("BillActNumber").ToString
                                            LRow1.CssClass = "tblbg2"
                                            LRow1.Cells.Add(LCell1)
                                            LRow1.Cells.Add(LCell1N)
                                            lCell9.Text = strMode
                                            lCell10.Text = DRRec2("GrpDicName").ToString
                                            LRow1.Cells.Add(lCell9)
                                            LRow1.Cells.Add(lCell10)
                                            LRow1.Cells.Add(LCell2)
                                            LRow1.Cells.Add(LCell3)
                                            LRow1.Cells.Add(LCell4)
                                            LRow1.Cells.Add(LCell5)
                                            LRow1.Cells.Add(LCell6)
                                            LRow1.Cells.Add(LCell7)
                                            LRow1.Cells.Add(lCell8)
                                            Table1.Rows.Add(LRow1)
                                        End If
                                    Finally
                                        If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
                                            SQLCmd3.Connection.Close()
                                            SQLCmd3 = Nothing
                                        End If
                                    End Try
                                End While
                                HRecCount.Value = k
                            End If
                        Finally
                            If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                                SQLCmd2.Connection.Close()
                                SQLCmd2 = Nothing
                            End If
                        End Try

                        'Dim Row1 As New TableRow
                        'Dim Cell1 As New TableCell
                        'Dim Cell2 As New TableCell
                        'Dim Cell3 As New TableCell
                        'Dim Cell4 As New TableCell
                        'Dim Cell5 As New TableCell
                        'Row1.Font.Size = "8"
                        'Dim HF1 As New HiddenField
                        'HF1.ID = "HActID1"
                        'HF1.Value = DRRec("AccountID").ToString
                        'Form.Controls.Add(HF1)
                        'Dim HM1 As New HiddenField
                        'HM1.ID = "tblActMode1"
                        'HM1.Value = DRRec("Mode").ToString

                        'Dim Lbl As New Label
                        'Lbl.ID = "lblAct1"
                        'Lbl.Text = DRRec("AccountName").ToString
                        'Dim Txt1 As New TextBox
                        'Dim Txt2 As New TextBox
                        'Dim Txt3 As New TextBox
                        'Txt1.ID = "TxRate1"
                        '' Txt1.Text = DRRec1("Rate")
                        'Txt2.ID = "TxMRate1"
                        '' Txt2.Text = DRRec1("MiscRate")
                        'Txt3.ID = "TxSTRate1"
                        'Txt1.Width = "50"
                        'Txt2.Width = "50"
                        'Txt3.Width = "50"
                        'If IsDBNull(DRRec1("Rate")) Then
                        '    Txt1.Text = ""
                        'Else
                        '    Txt1.Text = DRRec1("Rate")
                        'End If
                        'If IsDBNull(DRRec1("MiscRate")) Then
                        '    Txt2.Text = ""
                        'Else
                        '    Txt2.Text = DRRec1("MiscRate")
                        'End If
                        'If IsDBNull(DRRec1("StatRate")) Then
                        '    Txt3.Text = ""
                        'Else
                        '    Txt3.Text = DRRec1("StatRate")
                        'End If
                        'If IsDBNull(DRRec1("MinBilling")) Then
                        '    TxMBilling.Text = ""
                        'Else
                        '    TxMBilling.Text = DRRec1("MinBilling")
                        'End If
                        'If IsDBNull(DRRec1("Cycle")) Then
                        '    DLCycle.Items(0).Selected = False
                        '    DLCycle.Items(1).Selected = True
                        'ElseIf DRRec1("Cycle") = "True" Then
                        '    DLCycle.Items(1).Selected = False
                        '    DLCycle.Items(0).Selected = True
                        'Else
                        '    DLCycle.Items(0).Selected = False
                        '    DLCycle.Items(1).Selected = True
                        'End If
                        'Dim SelMonth As Integer
                        'SelMonth = 0
                        'Dim SelYear As Integer
                        'SelYear = 0
                        'Dim i As Integer
                        'For i = 0 To DLMonth.Items.Count - 1
                        '    DLMonth.Items(i).Selected = False
                        '    DLYear.Items(i).Selected = False
                        '    If DRRec1("EffMonth") = DLMonth.Items(i).Value Then
                        '        SelMonth = i
                        '    End If
                        '    If DRRec1("EffYear") = DLYear.Items(i).Value Then
                        '        SelYear = i
                        '    End If
                        'Next
                        'DLMonth.Items(SelMonth).Selected = True
                        'DLYear.Items(SelYear).Selected = True

                        'Cell1.Controls.Add(Lbl)
                        'Cell2.Controls.Add(Txt1)
                        'Cell3.Controls.Add(Txt2)
                        'Cell4.Controls.Add(Txt3)
                        'Row1.Cells.Add(Cell1)
                        'Row1.Cells.Add(Cell2)
                        'Row1.Cells.Add(Cell3)
                        'Row1.Cells.Add(Cell4)
                        'Table1.Rows.Add(Row1)


                    Else

                    End If
                End While
            End If


        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
    End Sub


End Class
