Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common



Partial Class DemoAccount_ConfgDemo
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HActID.Value = Request("Accountid")
            DispdemoDetails()
        End If
        
        If Not IsPostBack Then
            ACount.Value = 3
            Dim i As Integer
            For i = ACount.Value To 35
                Table1.Rows(i).Visible = False
            Next
            Dim j As Integer
            j = ACount.Value - 2
            'For j = 1 To 33
            Dim d1 As DropDownList = Page.FindControl("DropDownList" & j)
            Dim I1 As New ListItem
            I1.Text = "Select Attribute"
            I1.Value = ""
            I1.Selected = "True"
            d1.Items.Add(I1)
            Dim clsDemo As New ETS.BL.Demographics
            Dim strfieldList As String = clsDemo.getAcctDemoFields(Request("AccountID").ToString)
            clsDemo = Nothing
            Dim clsAttributes As New ETS.BL.Attributes
            'clsAttributes.ContractorID = Session("ContractorId").ToString
            'clsAttributes._WhereString.Append(" and name not in ('" & Replace(strfieldList, ",", "','") & "') ")
            'Response.Write(clsAttributes._WhereString.ToString)

            'Dim DSAtt As DataSet = clsAttributes.getAttributeList()
            Dim DSAtt As DataSet = clsAttributes.getEditAttributes(Session("ContractorID").ToString)

            If DSAtt.Tables.Count > 0 Then
                If DSAtt.Tables(0).Rows.Count > 0 Then
                    Dim DV As Data.DataView = New Data.DataView(DSAtt.Tables(0), " name not in ('" & Replace(strfieldList, ",", "','") & "') ", String.Empty, DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        For Each DR As DataRow In DV.ToTable.Rows
                            Dim DemoType As String
                            DemoType = "Text"
                            If DR("Type") = 0 Then
                                DemoType = "Text"
                            ElseIf DR("Type") = 1 Then
                                DemoType = "Integer"
                            ElseIf DR("Type") = 2 Then
                                DemoType = "Date"
                            ElseIf DR("Type") = 3 Then
                                DemoType = "Boolean"
                            End If
                            Dim I2 As New ListItem
                            I2.Text = DR("Name") & " (" & DemoType & ")"
                            I2.Value = DR("AttributeId").ToString
                            d1.Items.Add(I2)

                        Next
                    End If
                End If
            End If
            clsAttributes = Nothing
            DSAtt.Dispose()

        End If
        Dim clsAcc As New ETS.BL.Accounts
        With clsAcc
            .AccountID = Request("AccountID")
            .getAccountDetails()

            


            Dim c As New TableCell
            Dim c1 As New TableCell
            Dim c2 As New TableCell
            Dim c3 As New TableCell
            Dim r As New TableRow
            Dim fldname As String

            fldname = .foldername
            HFoldName.Value = .foldername
            hdnDemoConfg.Value = .DemoConfg
            'TotPhy.Value = K
            c.Text = .AccountName
            c1.Text = .Description
            c2.Text = .AccountNo

            r.Cells.Add(c)
            r.Cells.Add(c1)
            r.Cells.Add(c2)
            Table5.Rows.Add(r)
            clsAcc = Nothing
        End With

    End Sub



    Sub DispdemoDetails()
        Dim clsDemo As New ETS.BL.Demographics
        Dim DSFieldList As DataSet = clsDemo.getAcctDemoFieldsDataSet(Request("AccountID"))
        clsDemo = Nothing
        
        
        If DSFieldList.Tables.Count > 0 Then
            For Each DR As DataRow In DSFieldList.Tables(0).Rows
                Dim TABL As Table = Page.FindControl("Table2")
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow

                c.Text = DR("DemofieldName") & " (" & DR("DemoFieldType") & ")"
                c1.Text = DR("DemoFieldSize")
                c3.Text = "<input id=""Button1"" type=""button"" value=""Remove Demo Field"" style='cursor:hand;' class=""button"" onclick=poptastic('" & DR("RecordID").ToString & "','" & HActID.Value & "') />"
                r.Cells.Add(c)
                r.Cells.Add(c1)
                r.Cells.Add(c3)

                TABL.Rows.Add(r)

            Next

        End If
        DSFieldList.Dispose()
    End Sub



    Protected Sub AnyClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim b As DropDownList = CType(sender, DropDownList)
        'Response.Write(b.ID)

        If b.SelectedValue <> "" Then
            Dim SelIndex As Integer
            Dim j As Integer
            Dim k As Integer
            SelIndex = b.SelectedIndex
            j = ACount.Value - 2
            For k = 1 To j
                Dim d1 As DropDownList = Page.FindControl("DropDownList" & k)
                If d1.ID <> b.ID Then
                    If d1.SelectedValue = b.SelectedValue Then
                        UserMsgBox("You have already selected this attribute")
                        b.ClearSelection()
                        DispdemoDetails()
                        Exit Sub
                    End If
                End If
            Next

            Dim DemoType As String
            Dim DRList As String

            DRList = Right(b.ID, Len(b.ID) - 12)
            'Response.Write(DRList)

            DemoType = Mid(b.SelectedItem.Text, InStr(b.SelectedItem.Text, "(") + 1, (Len(b.SelectedItem.Text) - 1) - (InStr(b.SelectedItem.Text, "(")))

            Dim Tx As TextBox = Page.FindControl("Textbox" & DRList)
            If DemoType = "Text" Then
                Tx.Text = "50"
                Tx.Enabled = True
            ElseIf DemoType = "Date" Then
                Tx.Text = "8"
                Tx.Enabled = False
            ElseIf DemoType = "Integer" Then
                Tx.Text = "50"
                Tx.Enabled = True
            ElseIf DemoType = "Boolean" Then
                Tx.Text = "1"
                Tx.Enabled = False
            End If
        End If
        DispdemoDetails()

    End Sub

    Public Sub UserMsgBox(ByVal sMsg As String)
        Dim sb As New StringBuilder()
        Dim oFormObject As System.Web.UI.Control

        sMsg = sMsg.Replace("'", "\'")
        sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
        sMsg = sMsg.Replace(vbCrLf, "\n")
        sMsg = "<script language=javascript>alert(""" & sMsg & """)</script>"
        sb = New StringBuilder()
        sb.Append(sMsg)
        For Each oFormObject In Me.Controls
            If TypeOf oFormObject Is HtmlForm Then
                Exit For
            End If
        Next
        ' Add the javascript after the form object so that the 
        ' message doesn't appear on a blank screen.
        oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))

    End Sub
    Protected Sub btnConfgDemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfgDemo.Click
        Dim chkValue As Integer
        chkValue = ACount.Value - 2
        Dim k As Integer
        Dim DLList As String
        Dim TxtList As String
        For k = 1 To chkValue
            DLList = "DropDownList" & k
            TxtList = "TextBox" & k

            If Request(DLList) <> "" Then
                Dim DemoField As String
                Dim DemoSize As String
                Dim DemoType As String
                DemoType = "Text"
                DemoSize = 50
                Dim clsAtt As New ETS.BL.Attributes
                With clsAtt
                    .AttributeID = Request(DLList)
                    .getAttributeDetails()

                    DemoField = .Name
                    If .Type = 0 Then
                        DemoSize = Request(TxtList)
                        DemoType = "Text"
                    ElseIf .Type = 1 Then
                        DemoSize = Request(TxtList)
                        DemoType = "Integer"
                    ElseIf .Type = 2 Then
                        DemoSize = 16
                        DemoType = "date"
                    ElseIf .Type = 3 Then
                        DemoSize = 1
                        DemoType = "Boolean"
                    End If
                    Dim clsDemo As New ETS.BL.Demographics
                    clsDemo.ConfigAccDemo(HFoldName.Value, HActID.Value, DemoField, DemoType, DemoSize, IIf(hdnDemoConfg.Value, 1, 0))

                    clsDemo = Nothing
                End With
                clsAtt = Nothing
            End If
        Next

        DispdemoDetails()
        Table1.Visible = False
        btnConfgDemo.Visible = False
        btnAddAttribute.Visible = False
    End Sub
    
    Protected Sub btnAddAttribute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddAttribute.Click
        Table1.Rows(ACount.Value).Visible = True
        ACount.Value = ACount.Value + 1
        Dim j As Integer
        j = ACount.Value - 2
        Dim d1 As DropDownList = Page.FindControl("DropDownList" & j)
        Dim I1 As New ListItem
        I1.Text = "Select Attribute"
        I1.Value = ""
        I1.Selected = "True"
        d1.Items.Add(I1)
        Dim clsDemo As New ETS.BL.Demographics
        Dim strFields As String = clsDemo.getAcctDemoFields(Request("AccountID"))
        clsDemo = Nothing
        Dim clsAtt As New ETS.BL.Attributes
        'clsAtt.ContractorID = Session("ContractorID").ToString
        'clsAtt._WhereString.Append(" and name not in ('" & Replace(strFields, ",", "','") & "')")
        'Dim DSAtt As DataSet = clsAtt.getAttributeList
        Dim DSAtt As DataSet = clsAtt.getEditAttributes(Session("ContractorID").ToString)
        If DSAtt.Tables.Count > 0 Then
            If DSAtt.Tables(0).Rows.Count > 0 Then
                Dim DV As Data.DataView = New Data.DataView(DSAtt.Tables(0), " name not in ('" & Replace(strFields, ",", "','") & "') ", String.Empty, DataViewRowState.CurrentRows)
                If DV.Count > 0 Then
                    For Each DR As DataRow In DV.ToTable.Rows

                        Dim DemoType As String
                        DemoType = "Text"
                        If DR("Type") = 0 Then
                            DemoType = "Text"
                        ElseIf DR("Type") = 1 Then
                            DemoType = "Integer"
                        ElseIf DR("Type") = 2 Then
                            DemoType = "Date"
                        ElseIf DR("Type") = 3 Then
                            DemoType = "Boolean"
                        End If
                        'Response.Write(DR("Name") & " (" & DemoType & ")<BR>")
                        Dim I2 As New ListItem
                        I2.Text = DR("Name") & " (" & DemoType & ")"
                        I2.Value = DR("AttributeId").ToString

                        d1.Items.Add(I2)
                    Next
                End If
            End If
        End If


        clsAtt = Nothing
        
        DSAtt.Dispose()
        DispdemoDetails()
    End Sub
End Class
