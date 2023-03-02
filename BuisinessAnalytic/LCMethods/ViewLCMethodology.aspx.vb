Imports System.Data.SqlClient

Partial Class ViewLCMethodology
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Response.Write(Session("contractorid").ToString)
            Dim obj As New ETS.BL.LCMethodlogy
            With obj
                .contractorid = Session("contractorid").ToString
            End With
            Dim DT As System.Data.DataSet = obj.getCountMethodList
            obj = Nothing
            DLLCMethod.DataSource = DT.Tables(0)
            DLLCMethod.DataTextField = "MethodName"
            DLLCMethod.DataValueField = "TrackID"
            DLLCMethod.DataBind()
            Dim LI As New ListItem
            LI.Selected = True
            LI.Text = "Any"
            LI.Value = ""
            DLLCMethod.Items.Add(LI)
            Panel1.Visible = False
            Table2.Visible = False
            TableCell3.Enabled = False
            TableRow1.Visible = False
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
        DisplayControls(DLMethod.SelectedItem.Text)
    End Sub
    Protected Sub DisplayControls(ByVal CountName As String)
        If CountName = "Pages" Or CountName = "PerDictator" Or CountName = "PerReport" Or CountName = "Minutes" Then
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
        ElseIf CountName = "Words" Then
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
            TableCell9.Enabled = False
            TableCell4.Enabled = False
            TableCell5.Enabled = False
            TableCell18.Enabled = False
        ElseIf CountName = "GrossLines" Then
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
            TableCell9.Enabled = False
            TableCell4.Enabled = False
            TableCell5.Enabled = False
            TableCell18.Enabled = False
        ElseIf CountName = "AllLines" Then
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
            TableCell9.Enabled = False
            TableCell4.Enabled = False
            TableCell5.Enabled = False
            TableCell18.Enabled = False
        ElseIf CountName = "CharsPerLine" Then
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

    Protected Sub DLLCMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLLCMethod.SelectedIndexChanged
        Try

      
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
            If DLLCMethod.Items(0).Value = "" And DLLCMethod.Items(0).Text = "Any" Then
                DLLCMethod.Items.RemoveAt(0)
            End If
            Dim obj As New ETS.BL.LCMethodlogy
            With obj
                .TrackID = DLLCMethod.SelectedValue
            End With
            Dim DT As System.Data.DataSet = obj.getCountMethodList
            obj = Nothing
            'Dim strConn As String
            'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            'Dim SQLCmd As New SqlCommand("Select * from tblLCMethodology where trackid ='" & DLLCMethod.SelectedValue & "'", New SqlConnection(strConn))
            'Try
            '    SQLCmd.Connection.Open()
            '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            '    If DRRec.HasRows = True Then
            '        'Response.Write("Yes")
            '        If DRRec.Read Then
            If DT.Tables.Count >= 0 Then
                Panel1.Visible = True
                TableRow1.Visible = True
                Table2.Visible = True
                TableCell3.Enabled = True
                For Each DRRec As System.Data.DataRow In DT.Tables(0).Rows
                    TxtDescr.Text = DRRec("description")
                    If DRRec("CountMethod") = "PerDictator" Then
                        DLMethod.Items(1).Selected = False
                        DLMethod.Items(2).Selected = False
                        DLMethod.Items(3).Selected = False
                        DLMethod.Items(4).Selected = False
                        DLMethod.Items(0).Selected = False
                        DLMethod.Items(5).Selected = True
                        DLMethod.Items(6).Selected = False
                        DLMethod.Items(7).Selected = False
                    ElseIf DRRec("CountMethod") = "PerReport" Then
                        DLMethod.Items(1).Selected = False
                        DLMethod.Items(2).Selected = False
                        DLMethod.Items(3).Selected = False
                        DLMethod.Items(4).Selected = False
                        DLMethod.Items(0).Selected = False
                        DLMethod.Items(5).Selected = False
                        DLMethod.Items(6).Selected = True
                        DLMethod.Items(7).Selected = False
                    ElseIf DRRec("CountMethod") = "Minutes" Then
                        DLMethod.Items(1).Selected = False
                        DLMethod.Items(2).Selected = False
                        DLMethod.Items(3).Selected = False
                        DLMethod.Items(4).Selected = False
                        DLMethod.Items(0).Selected = False
                        DLMethod.Items(5).Selected = False
                        DLMethod.Items(6).Selected = False
                        DLMethod.Items(7).Selected = True
                    ElseIf DRRec("CountMethod") = "Pages" Then
                        DLMethod.Items(1).Selected = False
                        DLMethod.Items(2).Selected = False
                        DLMethod.Items(3).Selected = False
                        DLMethod.Items(4).Selected = False
                        DLMethod.Items(5).Selected = False
                        DLMethod.Items(0).Selected = True
                        DLMethod.Items(6).Selected = False
                        DLMethod.Items(7).Selected = False
                    ElseIf DRRec("CountMethod") = "CharsPerLine" Then
                        DLMethod.Items(0).Selected = False
                        DLMethod.Items(2).Selected = False
                        DLMethod.Items(3).Selected = False
                        DLMethod.Items(4).Selected = False
                        DLMethod.Items(5).Selected = False
                        DLMethod.Items(1).Selected = True
                        DLMethod.Items(6).Selected = False
                        DLMethod.Items(7).Selected = False
                    ElseIf DRRec("CountMethod") = "Words" Then
                        DLMethod.Items(0).Selected = False
                        DLMethod.Items(1).Selected = False
                        DLMethod.Items(3).Selected = False
                        DLMethod.Items(4).Selected = False
                        DLMethod.Items(5).Selected = False
                        DLMethod.Items(2).Selected = True
                        DLMethod.Items(6).Selected = False
                        DLMethod.Items(7).Selected = False
                    ElseIf DRRec("CountMethod") = "GrossLines" Then
                        DLMethod.Items(0).Selected = False
                        DLMethod.Items(1).Selected = False
                        DLMethod.Items(2).Selected = False
                        DLMethod.Items(4).Selected = False
                        DLMethod.Items(5).Selected = False
                        DLMethod.Items(3).Selected = True
                        DLMethod.Items(6).Selected = False
                        DLMethod.Items(7).Selected = False
                    ElseIf DRRec("CountMethod") = "AllLines" Then
                        DLMethod.Items(0).Selected = False
                        DLMethod.Items(1).Selected = False
                        DLMethod.Items(2).Selected = False
                        DLMethod.Items(3).Selected = False
                        DLMethod.Items(5).Selected = False
                        DLMethod.Items(4).Selected = True
                        DLMethod.Items(6).Selected = False
                        DLMethod.Items(7).Selected = False
                    End If
                    '  Response.Write(DRRec("RptHeader").ToString)
                    CHheader.Checked = DRRec("RptHeader")
                    CHfooter.Checked = DRRec("RptFooter")
                    CHbody.Checked = DRRec("RptBody")
                    CHBIU.Checked = DRRec("RptBIU")
                    CHspaces.Checked = DRRec("RptSpaces")
                    CHSchars.Checked = DRRec("RptShifted")
                    CHsrt.Checked = DRRec("RptSCT")
                    CHBIUOnOff.Checked = DRRec("RptBIUOnOff")
                    CHScharsAll.Checked = DRRec("RptShiftedAll")
                    CHDocVariable.Checked = DRRec("RptDocVariable")
                    TXcpl.Text = DRRec("CharsPerLines").ToString
                    txtBIUVal.Text = DRRec("BIUVal").ToString
                    txtBIUShiftedAll.Text = DRRec("BIUShiftedAll").ToString
                    If DLMethod.SelectedValue = "Pages" Or DLMethod.SelectedValue = "PerDictator" Or DLMethod.SelectedValue = "PerReport" Or DLMethod.SelectedValue = "Minutes" Then
                        TableCell7.Enabled = False
                        TableCell8.Enabled = False
                        TableCell10.Enabled = False
                        TableCell11.Enabled = False
                        TableCell12.Enabled = False
                        TableCell13.Enabled = False
                        TableCell14.Enabled = False
                        TableCell15.Enabled = False
                        TableCell5.Enabled = False
                        TableCell18.Enabled = False
                        TableCell4.Enabled = False
                        TableCell6.Enabled = False
                        TableCell16.Enabled = False
                    ElseIf DLMethod.SelectedValue = "Words" Then
                        TableCell7.Enabled = True
                        TableCell8.Enabled = False
                        TableCell10.Enabled = True
                        TableCell11.Enabled = False
                        TableCell12.Enabled = True
                        TableCell13.Enabled = False
                        TableCell14.Enabled = False
                        TableCell15.Enabled = False
                        TableCell5.Enabled = False
                        TableCell18.Enabled = False
                        TableCell4.Enabled = False
                        TableCell6.Enabled = False
                        TableCell16.Enabled = False
                    ElseIf DLMethod.SelectedValue = "GrossLines" Then
                        TableCell7.Enabled = True
                        TableCell8.Enabled = False
                        TableCell10.Enabled = True
                        TableCell11.Enabled = False
                        TableCell12.Enabled = True
                        TableCell13.Enabled = False
                        TableCell14.Enabled = False
                        TableCell15.Enabled = False
                        TableCell5.Enabled = False
                        TableCell18.Enabled = False
                        TableCell4.Enabled = False
                        TableCell6.Enabled = False
                        TableCell16.Enabled = False
                    ElseIf DLMethod.SelectedValue = "AllLines" Then
                        TableCell7.Enabled = True
                        TableCell8.Enabled = False
                        TableCell10.Enabled = True
                        TableCell11.Enabled = False
                        TableCell12.Enabled = True
                        TableCell13.Enabled = False
                        TableCell14.Enabled = False
                        TableCell15.Enabled = False
                        TableCell5.Enabled = False
                        TableCell18.Enabled = False
                        TableCell4.Enabled = False
                        TableCell6.Enabled = False
                        TableCell16.Enabled = False
                    ElseIf DLMethod.SelectedValue = "CharsPerLine" Then
                        TableCell7.Enabled = True
                        TableCell8.Enabled = True
                        TableCell10.Enabled = True
                        TableCell11.Enabled = True
                        TableCell12.Enabled = True
                        TableCell13.Enabled = True
                        TableCell14.Enabled = True
                        TableCell15.Enabled = True
                        TableCell5.Enabled = True
                        TableCell18.Enabled = True
                        TableCell4.Enabled = True
                        TableCell6.Enabled = True
                        TableCell16.Enabled = True
                    End If
                Next
            End If
            '    End If
            'DRRec.Close()
            'Finally
            '    If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
            '        SQLCmd.Connection.Close()
            '        SQLCmd = Nothing
            '    End If
            'End Try
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim strConn As String
        Dim strQuery As String
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
            If CHsrt.Checked = True Then
                RptSCT = True
            Else
                RptSCT = False
            End If
            If TXcpl.Text > 0 Then
                CharsPerLines = TXcpl.Text
            Else
                CharsPerLines = 65
            End If
            If CHBIUOnOff.Checked = True Then
                RptBIUOnOff = True
                BIUVal = txtBIUVal.Text
                BIUShiftedAll = txtBIUShiftedAll.Text
            Else
                RptBIUOnOff = False
            End If
            If CHScharsAll.Checked = True Then
                RptShiftedAll = True
            Else
                RptShiftedAll = False
            End If
            If CHDocVariable.Checked = True Then
                RptDocVariable = True
            Else
                RptDocVariable = False
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
            .TrackID = DLLCMethod.SelectedValue
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
            If .UpdateCountMethodDetails = 1 Then
                lblDisp.Text = "UnitCount method has been updated successfully."
            Else
                lblDisp.Text = "Issue in updating UnitCount method."
            End If

        End With
        obj = Nothing

        'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'strQuery = "Update tblLCMethodology Set Description ='" & TxtDescr.Text & "', CountMethod= '" & DLMethod.SelectedValue & "', RptHeader='" & RptHeader & "', RptFooter= '" & RptFooter & "', RptBody='" & RptBody & "', RptBIU='" & RptBIU & "',  RptShifted= '" & RptShifted & "', RptSpaces=  '" & RptSpaces & "', RptSCT= '" & RptSCT & "', CharsPerLines= '" & CharsPerLines & "',  CreatedDate='" & Now & "' where TrackID='" & DLLCMethod.SelectedValue & "'"
        ''Response.Write(strQuery)
        ''Response.End()

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

    End Sub
End Class
