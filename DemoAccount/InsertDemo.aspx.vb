Imports System.Data.SqlClient
Imports System.Data

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Assign.Visible = False
        DispBox.Visible = False
        Table4.Visible = False
        If Not IsPostBack Then
            Assign.Visible = False
            Table4.Visible = False
            Panel6.Visible = False
            'Panel1.Visible = False
            Panel2.Visible = False
            HDictCode.Value = 1
            Panel5.Visible = True
            ActState.Value = 0
        Else
        End If
    End Sub







    Protected Sub ActSearch_Click()
        Label1.Visible = False


        Panel6.Visible = True
        Panel5.Visible = False
        'Panel1.Visible = False
        Panel2.Visible = False
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        'Dim clsAcc As New ETS.BL.Accounts
        'With clsAcc
        '    .ContractorID = Session("ContractorID").ToString
        '    ._WhereString.Append(" and AccountName like '%" & TxtAname.Text & "%'")
        'End With
        'Dim DSAccList As DataSet = clsAcc.getAccountList()
        'clsAcc = Nothing

        Dim DSAct As New DataSet
        Dim DRec1 As Data.DataTableReader
        Dim clsAct As ETS.BL.Accounts
        Try
            clsAct = New ETS.BL.Accounts
            DSAct = clsAct.getAccountList(Session("WorkGroupID"), Session("ContractorID"), " AND AccountName like '%" & TxtAname.Text & "%'")
            If DSAct.Tables.Count > 0 Then
                If DSAct.Tables(0).Rows.Count > 0 Then
                    
                    DRec1 = DSAct.Tables(0).CreateDataReader
                    If DRec1.HasRows Then
                        While DRec1.Read
                            Dim c As New TableCell
                            Dim c1 As New TableCell
                            Dim c2 As New TableCell
                            Dim c3 As New TableCell
                            Dim r As New TableRow
                            Dim RB As New RadioButton
                            RB.GroupName = "AccountID"
                            RB.ID = DRec1("accountid").ToString
                            RB.Checked = "True"
                            c3.Controls.Add(RB)

                            c.Text = DRec1("AccountName")
                            c1.Text = DRec1("AccountNo")



                            r.Cells.Add(c3)
                            r.Cells.Add(c)
                            r.Cells.Add(c1)
                            Table1.Rows.Add(r)
                        End While
                    End If
                Else
                    ActState.Value = 0
                    DispBox.Visible = True

                    DispBox.Text = "No Records Found"
                    PageStatus()
                    Exit Sub
                End If
            Else
                ActState.Value = 0
                DispBox.Visible = True

                DispBox.Text = "No Records Found"
                PageStatus()
                Exit Sub
            End If
        Catch ex As Exception
        Finally
            clsAct = Nothing
            DRec1 = Nothing
            DSAct = Nothing
        End Try

        Dim CB As New Button
        Dim r2 As New TableRow
        Dim c4 As New TableCell
        c4.ColumnSpan = 3
        c4.Style("text-align") = "center"
        submit3.Visible = True

        c4.Controls.Add(submit3)


        r2.Cells.Add(c4)
        Table1.Rows.Add(r2)


        Panel6.Controls.Add(t2)
        'Panel6.Controls.Add(CB)
        

    End Sub
    Protected Sub BtnSubmit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit2.Click
        ActState.Value = 1
        HActID.Value = Request("AccountID")
        PageStatus()
    End Sub


    Sub PageStatus()

        If ActState.Value = 0 Then
            LoadPage()
        ElseIf ActState.Value = 1 Then
            ActSearch_Click()

        ElseIf ActState.Value = 3 Then
            ActStatus_Click()
            'LocStatus()
        End If
        If ActState.Value = 3 Then
            Assign.Visible = True
        Else
            Assign.Visible = False
        End If

    End Sub



    
    Protected Sub ActStatus_Click()
        ' Response.Write(HActID.Value)


        Panel6.Visible = False
        Panel5.Visible = False
        'Panel1.Visible = False
        Panel2.Visible = True

        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both






        'Response.Write("Total" & TotPhy.Value)
        Dim K As Integer
        K = 0
        Dim Filename As String
        Dim clsAcc As New ETS.BL.Accounts
        With clsAcc
            .ContractorID = Session("ContractorID").ToString
            .AccountID = HActID.Value
        End With
        clsAcc.getAccountDetails()




        Dim c As New TableCell
        Dim c1 As New TableCell
        Dim c2 As New TableCell
        Dim c3 As New TableCell
        Dim r As New TableRow




        'TotPhy.Value = K
        c.Text = clsAcc.AccountName
        c1.Text = clsAcc.Description
        c2.Text = clsAcc.AccountNo
        HFoldName.Value = clsAcc.foldername
        Filename = clsAcc.Description
        clsAcc = Nothing
        r.Cells.Add(c)
        r.Cells.Add(c1)
        r.Cells.Add(c2)
        Table5.Rows.Add(r)



        Dim clsDemo As New ETS.BL.Demographics
        Dim arrDemoFields() As String = Split(clsDemo.getAcctDemoFields(HActID.Value), ",")
        clsDemo = Nothing
        
        If UBound(arrDemoFields) > 0 Then

            Dim Cell1 As New TableCell
            Dim Cell2 As New TableCell
            Dim Row1 As New TableRow
            Dim Cell3 As New TableCell
            Dim Cell7 As New TableCell
            Cell1.Text = "DemoField Name"
            Cell7.Text = "Type"
            Cell2.Text = "Size"
            Cell3.Text = "Data"
            Cell1.CssClass = "alt1"
            Cell7.CssClass = "alt1"
            Cell2.CssClass = "alt1"
            Cell3.CssClass = "alt1"
            Row1.Cells.Add(Cell1)
            Row1.Cells.Add(Cell7)
            Row1.Cells.Add(Cell2)
            Row1.Cells.Add(Cell3)
            Table3.Rows.Add(Row1)

            For i As Integer = 0 To UBound(arrDemoFields)

                DemoFieldText.Value = i
                Dim Cell4 As New TableCell
                Dim Cell5 As New TableCell
                Dim Cell6 As New TableCell
                Dim Cell8 As New TableCell
                Dim Row2 As New TableRow
                Dim TX As New TextBox
                TX.ID = "TXTValue" & i
                If i = 1 Then
                    DemoFieldValue.Value = arrDemoFields(i)
                Else
                    DemoFieldValue.Value = DemoFieldValue.Value & "," & arrDemoFields(i)
                End If



                Cell4.Text = arrDemoFields(i)
                clsDemo = New ETS.BL.Demographics
                With clsDemo
                    .AccountID = HActID.Value
                    .DemoFieldName = Trim(arrDemoFields(i))

                    .getDemoAccDetails()


                    Cell5.Text = .DemoFieldType
                    Cell6.Text = .DemoFieldSize
                    TX.MaxLength = .DemoFieldSize
                    If .DemoFieldType = "Text" Then
                        TX.ToolTip = "Please enter Text"

                    ElseIf .DemoFieldType = "Integer" Then
                        TX.ToolTip = "Please enter numeric value"
                       
                    ElseIf .DemoFieldType = "date" Then
                        TX.ToolTip = "Please enter Date"
                       
                    ElseIf .DemoFieldType = "Integer" Then
                        TX.ToolTip = "Please enter boolean value 1 or 0"

                    End If

                End With
                clsDemo = Nothing
                Cell8.Controls.Add(TX)
                Row2.Cells.Add(Cell4)

                Row2.Cells.Add(Cell5)
                Row2.Cells.Add(Cell6)
                Row2.Cells.Add(Cell8)
                Table3.Rows.Add(Row2)


            Next





        Else

            Dim Cell1 As New TableCell
            Dim Row1 As New TableRow
            Dim Cell3 As New TableCell
            Table3.GridLines = GridLines.None

            Cell1.Text = "This account was not configured for Demos"

            Row1.Cells.Add(Cell1)

            Table3.Rows.Add(Row1)

            Assign.Visible = False

        End If
        









    End Sub

    

   
    Sub LoadPage()
        Assign.Visible = False
        Table4.Visible = False
        Panel6.Visible = False
        'Panel1.Visible = False
        Panel2.Visible = False
        HDictCode.Value = 1
        Panel5.Visible = True
        ActState.Value = 0
    End Sub

    Protected Sub submit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit3.Click
        ActState.Value = 3
        HActID.Value = Request("AccountID")
        PageStatus()
    End Sub

    Protected Sub Assign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Assign.Click
       Dim j As Integer
        Dim TXTValue As String
        Dim DemoTextValue As String
        DemoTextValue = ""
        For j = 1 To DemoFieldText.Value
            TXTValue = "TXTValue" & j
            If j = 1 Then
                DemoTextValue = "'" & Request(TXTValue) & "'"
            Else
                DemoTextValue = DemoTextValue & "," & "'" & Request(TXTValue) & "'"
            End If
        Next
        Dim clsDemo As New ETS.BL.Demographics
        Label1.Visible = True
        If clsDemo.setAcctDemos(DemoFieldValue.Value, DemoTextValue, HFoldName.Value) = 1 Then
            Label1.Text = "The record has been updated successfully."
        Else
            Label1.Text = "Updating Record Failed."
        End If
        

        PageStatus()
    End Sub
End Class

