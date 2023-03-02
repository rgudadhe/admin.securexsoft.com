Imports System.Data.SqlClient

Partial Class Billing_LCMethods_LCMethodology
    Inherits BasePage

  

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Panel1.Enabled = False
            'Table2.Enabled = False
            TableCell7.Enabled = False
            TableCell8.Enabled = False
            TableCell10.Enabled = False
            TableCell11.Enabled = False
            TableCell12.Enabled = False
            TableCell13.Enabled = False
            TableCell14.Enabled = False
            TableCell15.Enabled = False
            TableCell6.Enabled = False
            TableCell16.Enabled = False
            TableCell9.Enabled = False
            TableCell4.Enabled = False
            TableCell5.Enabled = False
            TableCell18.Enabled = False
            TXcpl.Text = 65
            txtBIUVal.Text = 1
            txtBIUShiftedAll.Text = 1
        End If


    End Sub

    Protected Sub DLMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLMethod.SelectedIndexChanged
        CHheader.Checked = False
        CHfooter.Checked = False
        CHbody.Checked = False
        CHBIU.Checked = False
        CHspaces.Checked = False
        CHSchars.Checked = False
        CHsrt.Checked = False
        CHBIUOnOff.Checked = False
        CHScharsAll.Checked = False
        CHDocVariable.Checked = False

        If DLMethod.SelectedValue = "Pages" Or DLMethod.SelectedValue = "PerDictator" Or DLMethod.SelectedValue = "PerReport" Or DLMethod.SelectedValue = "Minutes" Then
            TableCell7.Enabled = False
            TableCell8.Enabled = False
            TableCell10.Enabled = False
            TableCell11.Enabled = False
            TableCell12.Enabled = False
            TableCell13.Enabled = False
            TableCell14.Enabled = False
            TableCell15.Enabled = False
            TableCell6.Enabled = False
            TableCell16.Enabled = False
            TableCell9.Enabled = True
            TableCell4.Enabled = False
            TableCell5.Enabled = False
            TableCell18.Enabled = False
        ElseIf DLMethod.SelectedValue = "Words" Then
            TableCell7.Enabled = True
            TableCell8.Enabled = False
            TableCell10.Enabled = True
            TableCell11.Enabled = False
            TableCell12.Enabled = True
            TableCell13.Enabled = False
            TableCell14.Enabled = False
            TableCell15.Enabled = False
            TableCell6.Enabled = False
            TableCell16.Enabled = False
            TableCell9.Enabled = True
            TableCell4.Enabled = False
            TableCell5.Enabled = False
            TableCell18.Enabled = False
        ElseIf DLMethod.SelectedValue = "GrossLines" Then
            TableCell7.Enabled = True
            TableCell8.Enabled = False
            TableCell10.Enabled = True
            TableCell11.Enabled = False
            TableCell12.Enabled = True
            TableCell13.Enabled = False
            TableCell14.Enabled = False
            TableCell15.Enabled = False
            TableCell6.Enabled = False
            TableCell16.Enabled = False
            TableCell9.Enabled = True
            TableCell4.Enabled = False
            TableCell5.Enabled = False
            TableCell18.Enabled = False
        ElseIf DLMethod.SelectedValue = "AllLines" Then
            TableCell7.Enabled = True
            TableCell8.Enabled = False
            TableCell10.Enabled = True
            TableCell11.Enabled = False
            TableCell12.Enabled = True
            TableCell13.Enabled = False
            TableCell14.Enabled = False
            TableCell15.Enabled = False
            TableCell6.Enabled = False
            TableCell16.Enabled = False
            TableCell9.Enabled = True
            TableCell4.Enabled = False
            TableCell5.Enabled = False
            TableCell18.Enabled = False
        ElseIf DLMethod.SelectedValue = "CharsPerLine" Then
            TableCell7.Enabled = True
            TableCell8.Enabled = True
            TableCell10.Enabled = True
            TableCell11.Enabled = True
            TableCell12.Enabled = True
            TableCell13.Enabled = True
            TableCell14.Enabled = True
            TableCell15.Enabled = True
            TableCell6.Enabled = True
            TableCell16.Enabled = True
            TableCell9.Enabled = True
            TableCell4.Enabled = True
            TableCell5.Enabled = True
            TableCell18.Enabled = True
        End If

    End Sub

    Protected Sub CHspaces_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHspaces.CheckedChanged
        If CHspaces.Checked = True Then
            CHsrt.Checked = False
        End If
    End Sub

    Protected Sub CHsrt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CHsrt.CheckedChanged
        If CHsrt.Checked = True Then
            CHspaces.Checked = False
        End If
    End Sub

   

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Dim strConn As String
        'Dim strQuery As String
        Dim RptHeader As Boolean
        Dim RptFooter As Boolean
        Dim RptBody As Boolean
        Dim RptBIU As Boolean
        Dim RptBIUOnOff As Boolean
        Dim RptSpaces As Boolean
        Dim RptShifted As Boolean
        Dim RptShiftedAll As Boolean
        Dim RptDocVariable As Boolean
        Dim RptSCT As Boolean
        Dim CharsPerLines As Integer
        Dim BIUVal As Integer = 1
        Dim BIUShiftedAll As Integer = 1
        If DLMethod.SelectedValue = "CharsPerLine" Then
            If CHheader.Checked = True Then
                RptHeader = True
            Else
                RptHeader = False
            End If
            If CHfooter.Checked = True Then
                RptFooter = True
            Else
                RptFooter = False
            End If
            If CHbody.Checked = True Then
                RptBody = True
            Else
                RptBody = False
            End If
            If CHBIU.Checked = True Then
                RptBIU = True
            Else
                RptBIU = False
            End If
            If CHBIUOnOff.Checked = True Then
                RptBIUOnOff = True
                BIUVal = txtBIUVal.Text
                BIUShiftedAll = txtBIUShiftedAll.Text
            Else
                RptBIUOnOff = False
            End If
            If CHspaces.Checked = True Then
                RptSpaces = True
            Else
                RptSpaces = False
            End If
            If CHSchars.Checked = True Then
                RptShifted = True
            Else
                RptShifted = False
            End If
            If CHScharsAll.Checked = True Then
                RptShiftedAll = True
            Else
                RptShiftedAll = False
            End If
            If CHsrt.Checked = True Then
                RptSCT = True
            Else
                RptSCT = False
            End If
            If CHDocVariable.Checked = True Then
                RptDocVariable = True
            Else
                RptDocVariable = False
            End If
            If TXcpl.Text > 0 Then
                CharsPerLines = TXcpl.Text
            Else
                CharsPerLines = 65
            End If
        ElseIf DLMethod.SelectedValue = "Words" Or DLMethod.SelectedValue = "AllLines" Or DLMethod.SelectedValue = "GrossLines" Then
            If CHheader.Checked = True Then
                RptHeader = True
            Else
                RptHeader = False
            End If
            If CHfooter.Checked = True Then
                RptFooter = True
            Else
                RptFooter = False
            End If
            If CHbody.Checked = True Then
                RptBody = True
            Else
                RptBody = False
            End If
        Else
            RptHeader = False
            RptFooter = False
            RptBody = False
            RptBIU = False
            RptSpaces = False
            RptShifted = False
            RptSCT = False
            RptBIUOnOff = False
            RptShiftedAll = False
            RptDocVariable = False
            CharsPerLines = 0

        End If
        Dim obj As New ETS.BL.LCMethodlogy
        With obj
            .contractorid = Session("contractorid").ToString
            .MethodName = TxtName.Text
            .Description = TxtDescr.Text
            .CountMethod = DLMethod.SelectedValue
            .RptHeader = RptHeader
            .RptFooter = RptFooter
            .RptBody = RptBody
            .RptBIU = RptBIU
            .RptShifted = RptShifted
            .RptSpaces = RptSpaces
            .RptSCT = RptSCT
            .RptBIUOnOff = RptBIUOnOff
            .RptShiftedAll = RptShiftedAll
            .RptDocVariable = RptDocVariable
            .CharsPerLines = CharsPerLines
            .BIUVal = BIUVal
            .BIUShiftedAll = BIUShiftedAll
            .CreatedDate = Now
            If .InsertCountMethodDetails = 1 Then
                Response.Write("updated")
            Else
                Response.Write("Not updated")
            End If
        End With
        obj = Nothing
        'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'strQuery = "Insert Into tblLCMethodology (MethodName, Description, CountMethod, RptHeader, RptFooter, RptBody, RptBIU, RptShifted, RptSpaces, RptSCT, CharsPerLines, CreatedDate, contractorid)  Values ('" & TxtName.Text & "', '" & TxtDescr.Text & "', '" & DLMethod.SelectedValue & "', '" & RptHeader & "', '" & RptFooter & "', '" & RptBody & "', '" & RptBIU & "', '" & RptShifted & "', '" & RptSpaces & "', '" & RptSCT & "', '" & CharsPerLines & "', '" & Now & "', '" & Session("contractorid").ToString & "')"
        'Dim cmdIns As New SqlCommand(strQuery, New SqlConnection(strConn))
        'Try
        '    cmdIns.Connection.Open()
        '    cmdIns.ExecuteNonQuery()
        'Finally
        '    If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
        '        cmdIns.Connection.Close()
        '        cmdIns = Nothing
        '    End If
        'End Try
        Table1.Visible = False
        Table2.Visible = False
        Panel1.Visible = False
        lblDisp.Text = "UnitCount method has been added successfully."
    End Sub
End Class
