Imports System.Data.SqlClient

Partial Class Billing_LCMethods_LCMethodology
    Inherits BasePage

   

    'Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
    '    Dim strConn As String
    '    Dim strQuery As String
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    strQuery = "Update tblaccounts Set"
    '    strQuery = strQuery & " Description='" & TxtDescr.Text & "',"
    '    strQuery = strQuery & " BillActnumber='" & TxtBillNumber.Text & "',"
    '    strQuery = strQuery & " BillContName='" & TxtContPerson.Text & "',"
    '    strQuery = strQuery & " BillContNO='" & TxtTel.Text & "',"
    '    strQuery = strQuery & " BillEmail='" & TxtEMail.Text & "',"
    '    strQuery = strQuery & " BillFax='" & TxtEFax.Text & "',"
    '    strQuery = strQuery & " BillAddress='" & TxtAddress.Text & "',"
    '    strQuery = strQuery & " BillCity='" & TxtCity.Text & "',"
    '    strQuery = strQuery & " BillState='" & TxtState.Text & "',"
    '    strQuery = strQuery & " BillCntry='" & TxtCntry.Text & "',"
    '    strQuery = strQuery & " BillZip='" & TxtZip.Text & "',"
    '    If DLCategory.SelectedValue = "" Then
    '        strQuery = strQuery & " Category=NULL,"
    '    Else
    '        strQuery = strQuery & " Category='" & DLCategory.SelectedValue & "',"
    '    End If

    '    If DLMode.SelectedValue = "" Then
    '        strQuery = strQuery & " Mode=NULL,"
    '    Else
    '        strQuery = strQuery & " Mode='" & DLMode.SelectedValue & "',"
    '    End If

    '    If DLDelv.SelectedValue = "" Then
    '        strQuery = strQuery & " DelMode=NULL,"
    '    Else
    '        strQuery = strQuery & " DelMode='" & DLDelv.SelectedValue & "',"
    '    End If

    '    If DLTerm.SelectedValue = "" Then
    '        strQuery = strQuery & " PayTerm=NULL,"
    '    Else
    '        strQuery = strQuery & " PayTerm='" & DLTerm.SelectedValue & "',"
    '    End If

    '    If DLGroup.SelectedValue = "" Then
    '        strQuery = strQuery & " GrpActID=NULL,"
    '    Else
    '        strQuery = strQuery & " GrpActID='" & DLGroup.SelectedValue & "',"
    '    End If

    '    If DLLCMethod.SelectedValue = "" Then
    '        strQuery = strQuery & " LCMethodID=NULL"
    '    Else
    '        strQuery = strQuery & " LCMethodID='" & DLLCMethod.SelectedValue & "'"
    '    End If

    '    strQuery = strQuery & " where AccountID='" & DLAct.SelectedValue & "'"

    '    'Response.Write(strQuery)
    '    ' Response.End()

    '    Dim cmdIns As New SqlCommand(strQuery, New SqlConnection(strConn))
    '    cmdIns.Connection.Open()
    '    cmdIns.ExecuteNonQuery()
    '    cmdIns.Connection.Close()
    '    '    Table1.Visible = False
    '    Table2.Visible = False

    '    lblDisp.Text = "Account Details has been updated successfully."


    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Response.Write("Yes")

            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim SQLCmd As New SqlCommand("Select * from AdminETS.dbo.tblaccounts where contractorid ='" & Session("contractorid").ToString & "' AND (Indirect = 'False' OR Indirect IS NULL) order by Accountname", New SqlConnection(strConn))
            Try
                SQLCmd.Connection.Open()
                Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                If DRRec.HasRows = True Then
                    'Response.Write("Yes")
                    While DRRec.Read

                        '  If DRRec.Read Then
                        Dim LI As New ListItem
                        'Response.Write(DRRec("Methodname"))
                        LI.Text = DRRec("Accountname")
                        LI.Value = DRRec("AccountID").ToString
                        DLAct.Items.Add(LI)
                    End While
                End If
                DRRec.Close()
            Finally

                If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd.Connection.Close()
                    SQLCmd = Nothing
                End If
            End Try
            Dim LI1 As New ListItem
            LI1.Text = "Not set"
            LI1.Value = ""
            DLGroup.Items.Add(LI1)
            DLCategory.Items.Add(LI1)
            'DLLCMethod.Items.Add(LI1)
            Dim SQLCmd1 As New SqlCommand("Select * from AdminETS.dbo.tblGrpAccounts where contractorid ='" & Session("contractorid").ToString & "' order by Description", New SqlConnection(strConn))
            Try
                SQLCmd1.Connection.Open()
                Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
                If DRRec1.HasRows = True Then
                    'Response.Write("Yes")
                    While DRRec1.Read

                        '  If DRRec.Read Then
                        Dim LI As New ListItem
                        'Response.Write(DRRec("Methodname"))
                        LI.Text = DRRec1("GrpActName")
                        LI.Value = DRRec1("GrpActID").ToString
                        DLGroup.Items.Add(LI)
                    End While
                End If
                DRRec1.Close()
            Finally
                If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd1.Connection.Close()
                    SQLCmd1 = Nothing
                End If
            End Try


            Dim SQLCmd2 As New SqlCommand("Select * from AdminETS.dbo.tblActCategories where contractorid ='" & Session("contractorid").ToString & "' order by Description", New SqlConnection(strConn))
            Try
                SQLCmd2.Connection.Open()
                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                If DRRec2.HasRows = True Then
                    'Response.Write("Yes")
                    While DRRec2.Read

                        '  If DRRec.Read Then
                        Dim LI As New ListItem
                        'Response.Write(DRRec("Methodname"))
                        LI.Text = DRRec2("Description")
                        LI.Value = DRRec2("Category").ToString
                        DLCategory.Items.Add(LI)
                    End While
                End If
                DRRec2.Close()

            Finally
                If SQLCmd2.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd2.Connection.Close()
                    SQLCmd2 = Nothing
                End If
            End Try



            'Dim SQLCmd3 As New SqlCommand("Select * from AdminETS.dbo.tblLCMethodology where contractorid ='" & Session("contractorid").ToString & "' order by methodname", New SqlConnection(strConn))
            'Try
            '    SQLCmd3.Connection.Open()
            '    Dim DRRec3 As SqlDataReader = SQLCmd3.ExecuteReader()
            '    If DRRec3.HasRows = True Then
            '        'Response.Write("Yes")
            '        While DRRec3.Read
            '            '  If DRRec.Read Then
            '            Dim LI As New ListItem
            '            'Response.Write(DRRec("Methodname"))
            '            LI.Text = DRRec3("methodname")
            '            LI.Value = DRRec3("trackid").ToString
            '            DLLCMethod.Items.Add(LI)
            '        End While
            '    End If
            '    DRRec3.Close()

            'Finally
            '    If SQLCmd3.Connection.State = System.Data.ConnectionState.Open Then
            '        SQLCmd3.Connection.Close()
            '        SQLCmd3 = Nothing
            '    End If
            'End Try



            Table2.Visible = False
            'TableCell3.Enabled = False

            'Table1.Visible = False
            'TableCell7.Enabled = False
            'TableCell8.Enabled = False
            'TableCell10.Enabled = False
            'TableCell11.Enabled = False
            'TableCell12.Enabled = False
            'TableCell13.Enabled = False
            'TableCell14.Enabled = False
            'TableCell15.Enabled = False
            'TXcpl.Text = 65
        End If


    End Sub

    'Protected Sub DLMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLMethod.SelectedIndexChanged
    '    'CHheader.Checked = False
    '    'CHfooter.Checked = False
    '    'CHbody.Checked = False
    '    'CHBIU.Checked = False
    '    'CHspaces.Checked = False
    '    'CHSchars.Checked = False
    '    'CHsrt.Checked = False
    '    'If DLMethod.SelectedValue = "Pages" Then
    '    '    TableCell7.Enabled = False
    '    '    TableCell8.Enabled = False
    '    '    TableCell10.Enabled = False
    '    '    TableCell11.Enabled = False
    '    '    TableCell12.Enabled = False
    '    '    TableCell13.Enabled = False
    '    '    TableCell14.Enabled = False
    '    '    TableCell15.Enabled = False
    '    'ElseIf DLMethod.SelectedValue = "Words" Then
    '    '    TableCell7.Enabled = True
    '    '    TableCell8.Enabled = False
    '    '    TableCell10.Enabled = True
    '    '    TableCell11.Enabled = False
    '    '    TableCell12.Enabled = True
    '    '    TableCell13.Enabled = False
    '    '    TableCell14.Enabled = False
    '    '    TableCell15.Enabled = False
    '    'ElseIf DLMethod.SelectedValue = "GrossLines" Then
    '    '    TableCell7.Enabled = True
    '    '    TableCell8.Enabled = False
    '    '    TableCell10.Enabled = True
    '    '    TableCell11.Enabled = False
    '    '    TableCell12.Enabled = True
    '    '    TableCell13.Enabled = False
    '    '    TableCell14.Enabled = False
    '    '    TableCell15.Enabled = False
    '    'ElseIf DLMethod.SelectedValue = "AllLines" Then
    '    '    TableCell7.Enabled = True
    '    '    TableCell8.Enabled = False
    '    '    TableCell10.Enabled = True
    '    '    TableCell11.Enabled = False
    '    '    TableCell12.Enabled = True
    '    '    TableCell13.Enabled = False
    '    '    TableCell14.Enabled = False
    '    '    TableCell15.Enabled = False
    '    'ElseIf DLMethod.SelectedValue = "CharsPerLine" Then
    '    '    TableCell7.Enabled = True
    '    '    TableCell8.Enabled = True
    '    '    TableCell10.Enabled = True
    '    '    TableCell11.Enabled = True
    '    '    TableCell12.Enabled = True
    '    '    TableCell13.Enabled = True
    '    '    TableCell14.Enabled = True
    '    '    TableCell15.Enabled = True
    '    'End If

    'End Sub

    'Protected Sub CHspaces_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHspaces.CheckedChanged
    '    'If CHspaces.Checked = True Then
    '    '    CHsrt.Checked = False
    '    'End If
    'End Sub

    'Protected Sub CHsrt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHsrt.CheckedChanged
    '    'If CHsrt.Checked = True Then
    '    '    CHspaces.Checked = False
    '    'End If
    'End Sub

    'Protected Sub DLLCMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLLCMethod.SelectedIndexChanged
    '    CHheader.Checked = False
    '    CHfooter.Checked = False
    '    CHbody.Checked = False
    '    CHBIU.Checked = False
    '    CHspaces.Checked = False
    '    CHSchars.Checked = False
    '    CHsrt.Checked = False

    '    If DLLCMethod.Items(0).Value = "" And DLLCMethod.Items(0).Text = "Any" Then
    '        DLLCMethod.Items.RemoveAt(0)
    '    End If
    '    Dim strConn As String
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim SQLCmd As New SqlCommand("Select * from tblLCMethodology where trackid ='" & DLLCMethod.SelectedValue & "'", New SqlConnection(strConn))
    '    SQLCmd.Connection.Open()
    '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
    '    If DRRec.HasRows = True Then
    '        'Response.Write("Yes")
    '        If DRRec.Read Then
    '            Panel1.Visible = True
    '            Table2.Visible = True
    '            'TableCell3.Enabled = True
    '            'TxtDescr.Text = DRRec("description")
    '            If DRRec("CountMethod") = "Pages" Then
    '                DLMethod.Items(1).Selected = False
    '                DLMethod.Items(2).Selected = False
    '                DLMethod.Items(3).Selected = False
    '                DLMethod.Items(4).Selected = False
    '                DLMethod.Items(0).Selected = True
    '            ElseIf DRRec("CountMethod") = "CharsPerLine" Then
    '                DLMethod.Items(0).Selected = False
    '                DLMethod.Items(2).Selected = False
    '                DLMethod.Items(3).Selected = False
    '                DLMethod.Items(4).Selected = False
    '                DLMethod.Items(1).Selected = True
    '            ElseIf DRRec("CountMethod") = "Words" Then
    '                DLMethod.Items(0).Selected = False
    '                DLMethod.Items(1).Selected = False
    '                DLMethod.Items(3).Selected = False
    '                DLMethod.Items(4).Selected = False
    '                DLMethod.Items(2).Selected = True
    '            ElseIf DRRec("CountMethod") = "GrossLines" Then
    '                DLMethod.Items(0).Selected = False
    '                DLMethod.Items(1).Selected = False
    '                DLMethod.Items(2).Selected = False
    '                DLMethod.Items(4).Selected = False
    '                DLMethod.Items(3).Selected = True
    '            ElseIf DRRec("CountMethod") = "AllLines" Then
    '                DLMethod.Items(0).Selected = False
    '                DLMethod.Items(1).Selected = False
    '                DLMethod.Items(2).Selected = False
    '                DLMethod.Items(3).Selected = False
    '                DLMethod.Items(4).Selected = True
    '            End If
    '            CHheader.Checked = DRRec("RptHeader")
    '            CHfooter.Checked = DRRec("RptFooter")
    '            CHbody.Checked = DRRec("RptBody")
    '            CHBIU.Checked = DRRec("RptBIU")
    '            CHspaces.Checked = DRRec("RptSpaces")
    '            CHSchars.Checked = DRRec("RptShifted")
    '            CHsrt.Checked = DRRec("RptSCT")

    '            If DLMethod.SelectedValue = "Pages" Then
    '                TableCell7.Enabled = False
    '                TableCell8.Enabled = False
    '                TableCell10.Enabled = False
    '                TableCell11.Enabled = False
    '                TableCell12.Enabled = False
    '                TableCell13.Enabled = False
    '                TableCell14.Enabled = False
    '                TableCell15.Enabled = False
    '            ElseIf DLMethod.SelectedValue = "Words" Then
    '                TableCell7.Enabled = True
    '                TableCell8.Enabled = False
    '                TableCell10.Enabled = True
    '                TableCell11.Enabled = False
    '                TableCell12.Enabled = True
    '                TableCell13.Enabled = False
    '                TableCell14.Enabled = False
    '                TableCell15.Enabled = False
    '            ElseIf DLMethod.SelectedValue = "GrossLines" Then
    '                TableCell7.Enabled = True
    '                TableCell8.Enabled = False
    '                TableCell10.Enabled = True
    '                TableCell11.Enabled = False
    '                TableCell12.Enabled = True
    '                TableCell13.Enabled = False
    '                TableCell14.Enabled = False
    '                TableCell15.Enabled = False
    '            ElseIf DLMethod.SelectedValue = "AllLines" Then
    '                TableCell7.Enabled = True
    '                TableCell8.Enabled = False
    '                TableCell10.Enabled = True
    '                TableCell11.Enabled = False
    '                TableCell12.Enabled = True
    '                TableCell13.Enabled = False
    '                TableCell14.Enabled = False
    '                TableCell15.Enabled = False
    '            ElseIf DLMethod.SelectedValue = "CharsPerLine" Then
    '                TableCell7.Enabled = True
    '                TableCell8.Enabled = True
    '                TableCell10.Enabled = True
    '                TableCell11.Enabled = True
    '                TableCell12.Enabled = True
    '                TableCell13.Enabled = True
    '                TableCell14.Enabled = True
    '                TableCell15.Enabled = True
    '            End If
    '        End If
    '    End If
    '    SQLCmd.Connection.Close()
    'End Sub



    Protected Sub DLAct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLAct.SelectedIndexChanged
        If DLAct.Items(0).Value = "" And DLAct.Items(0).Text = "Any" Then
            DLAct.Items.RemoveAt(0)
        End If
        Table2.Visible = True
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd As New SqlCommand("Select * from tblAccounts where accountid ='" & DLAct.SelectedValue & "'", New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows = True Then
                'Response.Write("Yes")
                If DRRec.Read Then
                    TxtDescr.Text = DRRec("Description").ToString
                    TxtActNumber.Text = DRRec("AccountNo").ToString
                    TxtBillNumber.Text = DRRec("BillActnumber").ToString
                    TxtContPerson.Text = DRRec("BillContName").ToString
                    TxtTel.Text = DRRec("BillContNO").ToString
                    TxtEMail.Text = DRRec("BillEmail").ToString
                    TxtEFax.Text = DRRec("BillFax").ToString
                    TxtAddress.Text = DRRec("BillAddress").ToString
                    TxtCity.Text = DRRec("BillCity").ToString
                    TxtState.Text = DRRec("BillState").ToString
                    TxtCntry.Text = DRRec("BillCntry").ToString
                    TxtZip.Text = DRRec("BillZip").ToString
                    Dim i As Integer
                    Dim SelCount As Integer
                    SelCount = 0
                    For i = 0 To DLGroup.Items.Count - 1
                        DLGroup.Items(i).Selected = False
                        If DLGroup.Items(i).Value = DRRec("GrpActID").ToString Then
                            SelCount = i
                        End If
                    Next
                    DLGroup.Items(SelCount).Selected = True
                    SelCount = 0
                    For i = 0 To DLSepinvoice.Items.Count - 1
                        DLSepInvoice.Items(i).Selected = False
                        If DLSepInvoice.Items(i).Value = DRRec("Sepinvoice").ToString Then
                            SelCount = i
                        End If
                    Next
                    DLSepInvoice.Items(SelCount).Selected = True

                    'SelCount = 0
                    'For i = 0 To DLGroup.Items.Count - 1
                    '    DLGroup.Items(i).Selected = False
                    '    If DLGroup.Items(i).Value = DRRec("GrpActID").ToString Then
                    '        SelCount = i
                    '    End If
                    'Next
                    'DLGroup.Items(SelCount).Selected = True

                    SelCount = 0
                    For i = 0 To DLCategory.Items.Count - 1
                        DLCategory.Items(i).Selected = False
                        If DLCategory.Items(i).Value = Trim(DRRec("Category").ToString) Then
                            SelCount = i
                        End If
                    Next
                    DLCategory.Items(SelCount).Selected = True
                    ' Response.Write(DRRec("Mode").ToString)
                    SelCount = 0
                    For i = 0 To DLMode.Items.Count - 1
                        DLMode.Items(i).Selected = False
                        If DLMode.Items(i).Value = Trim(DRRec("Mode").ToString) Then
                            SelCount = i
                        End If
                    Next
                    DLMode.Items(SelCount).Selected = True
                    ' Response.Write(SelCount)
                    SelCount = 0
                    For i = 0 To DLDelv.Items.Count - 1
                        DLDelv.Items(i).Selected = False

                        If Trim(DLDelv.Items(i).Value) = Trim(DRRec("DelMode").ToString) Then

                            SelCount = i
                        End If
                    Next
                    DLDelv.Items(SelCount).Selected = True


                    SelCount = 0
                    For i = 0 To DLTerm.Items.Count - 1
                        DLTerm.Items(i).Selected = False
                        If DLTerm.Items(i).Value = Trim(DRRec("PayTerm").ToString) Then
                            SelCount = i
                        End If
                    Next
                    DLTerm.Items(SelCount).Selected = True

                    ' Response.Write(SelCount)
                    ' Response.End()
                    SelCount = 0
                    'For i = 0 To DLLCMethod.Items.Count - 1
                    '    DLLCMethod.Items(i).Selected = False
                    '    If DLLCMethod.Items(i).Value = Trim(DRRec("LCMethodID").ToString) Then
                    '        SelCount = i
                    '    End If
                    'Next
                    'DLLCMethod.Items(SelCount).Selected = True


                End If
            End If
            DRRec.Close()

        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try


    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        strQuery = "Update tblaccounts Set"
        strQuery = strQuery & " Description='" & TxtDescr.Text & "',"
        strQuery = strQuery & " BillActnumber='" & TxtBillNumber.Text & "',"
        strQuery = strQuery & " BillContName='" & TxtContPerson.Text & "',"
        strQuery = strQuery & " BillContNO='" & TxtTel.Text & "',"
        strQuery = strQuery & " BillEmail='" & TxtEMail.Text & "',"
        strQuery = strQuery & " BillFax='" & TxtEFax.Text & "',"
        strQuery = strQuery & " BillAddress='" & TxtAddress.Text & "',"
        strQuery = strQuery & " BillCity='" & TxtCity.Text & "',"
        strQuery = strQuery & " BillState='" & TxtState.Text & "',"
        strQuery = strQuery & " BillCntry='" & TxtCntry.Text & "',"
        strQuery = strQuery & " BillZip='" & TxtZip.Text & "',"
        strQuery = strQuery & " SepInvoice='" & DLSepInvoice.SelectedValue & "',"
        If DLCategory.SelectedValue = "" Then
            strQuery = strQuery & " Category=NULL,"
        Else
            strQuery = strQuery & " Category='" & DLCategory.SelectedValue & "',"
        End If

        If DLMode.SelectedValue = "" Then
            strQuery = strQuery & " Mode=NULL,"
        Else
            strQuery = strQuery & " Mode='" & DLMode.SelectedValue & "',"
        End If

        If DLDelv.SelectedValue = "" Then
            strQuery = strQuery & " DelMode=NULL,"
        Else
            strQuery = strQuery & " DelMode='" & DLDelv.SelectedValue & "',"
        End If

        If DLTerm.SelectedValue = "" Then
            strQuery = strQuery & " PayTerm=NULL,"
        Else
            strQuery = strQuery & " PayTerm='" & DLTerm.SelectedValue & "',"
        End If

        If DLGroup.SelectedValue = "" Then
            strQuery = strQuery & " GrpActID=NULL"
        Else
            strQuery = strQuery & " GrpActID='" & DLGroup.SelectedValue & "'"
        End If

        'If DLLCMethod.SelectedValue = "" Then
        '    strQuery = strQuery & " LCMethodID=NULL"
        'Else
        '    strQuery = strQuery & " LCMethodID='" & DLLCMethod.SelectedValue & "'"
        'End If

        strQuery = strQuery & " where AccountID='" & DLAct.SelectedValue & "'"

        'Response.Write(strQuery)
        ' Response.End()

        Dim cmdIns As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            cmdIns.Connection.Open()
            cmdIns.ExecuteNonQuery()
        Finally
            If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
                cmdIns.Connection.Close()
                cmdIns = Nothing
            End If
        End Try
        '    Table1.Visible = False
        Table2.Visible = False

        lblDisp.Text = "Account Details has been updated successfully."
    End Sub
End Class
