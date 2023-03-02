Imports System.Data.SqlClient
Imports System.Data

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DispBox.Visible = False

        If Not IsPostBack Then
            Panel6.Visible = False
            HDictCode.Value = 1
            Panel5.Visible = True
            ActState.Value = 0
        Else
        End If
    End Sub







    Protected Sub ActSearch_Click()
      

        Panel6.Visible = True
        Panel5.Visible = False
        Dim strConn As String
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both

        'Dim clsAcc As New ETS.BL.Accounts
        'With clsAcc
        '    .ContractorID = Session("ContractorID").ToString
        '    ._WhereString.Append(" and AccountName like '%" & TxtAname.Text & "%' and (IsDeleted is null or IsDeleted =0) order by accountname")
        'End With
        'Dim DSAccList As DataSet = clsAcc.getAccountList()
        'clsAcc = Nothing

        Dim DSAct As New DataSet
        Dim DRec1 As Data.DataTableReader
        Dim clsAct As ETS.BL.Accounts
        Try
            clsAct = New ETS.BL.Accounts
            DSAct = clsAct.getAccountList(Session("WorkGroupID"), Session("ContractorID"), " AND AccountName like '%" & TxtAname.Text & "%' ")
            If DSAct.Tables.Count > 0 Then
                If DSAct.Tables(0).Rows.Count > 0 Then
                    DRec1 = DSAct.Tables(0).CreateDataReader
                    If DRec1.HasRows Then
                        While DRec1.Read
                            Dim c As New TableCell
                            Dim c1 As New TableCell
                            Dim c2 As New TableCell
                            Dim c4 As New TableCell
                            Dim c3 As New TableCell
                            Dim r As New TableRow
                            c.Text = DRec1("AccountName")
                            c1.Text = DRec1("AccountNo")
                            If DRec1("MapDemoAccID").ToString <> "" Then
                                Dim clsMAcc As New ETS.BL.Accounts
                                clsMAcc.AccountID = DRec1("MapDemoAccID").ToString
                                clsMAcc.getAccountDetails()
                                If clsMAcc.AccountID <> "" Then
                                    If clsMAcc.DemoConfg Then
                                        c4.Text = IIf(IsDBNull(clsMAcc.AccountName) Or String.IsNullOrEmpty(clsMAcc.AccountName.ToString), "&nbsp;", clsMAcc.AccountName.ToString)
                                        c3.Text = "<input type=""button"" class=""button"" value=""Edit Configuration"" style='cursor:hand; width:140px'  onclick=editDemo('" & clsMAcc.AccountID & "') />"
                                        c2.Text = "<input type=""button"" class=""button"" value=""MT Client Configuration"" style='cursor:hand; width:140px' onclick=MTClientStatus('" & clsMAcc.AccountID & "') />"
                                    Else
                                        c4.Text = "-"
                                        c3.Text = "<input type=""button"" class=""button"" value=""Demo Configuration"" style='cursor:hand; width:140px' onclick=confgDemo('" & DRec1("AccountID").ToString & "') />"
                                        c2.Text = "Not Configured"
                                    End If
                                Else
                                    c4.Text = "-"
                                    c3.Text = "<input type=""button"" class=""button"" value=""Demo Configuration"" style='cursor:hand; width:140px' onclick=confgDemo('" & DRec1("AccountID").ToString & "') />"
                                    c2.Text = "Not Configured"
                                End If
                                clsMAcc = Nothing
                            Else
                                c4.Text = "-"
                                If DRec1("DemoConfg").ToString = "True" Then
                                    c3.Text = "<input type=""button"" class=""button"" value=""Edit Configuration"" style='cursor:hand; width:140px' onclick=editDemo('" & DRec1("AccountID").ToString & "') />"
                                    c2.Text = "<input type=""button"" class=""button"" value=""MT Client Configuration"" style='cursor:hand; width:140px' onclick=MTClientStatus('" & DRec1("AccountID").ToString & "') />"
                                Else
                                    c3.Text = "<input type=""button"" class=""button"" value=""Demo Configuration"" style='cursor:hand; width:140px' onclick=confgDemo('" & DRec1("AccountID").ToString & "') />"
                                    c2.Text = "Not Configured"
                                End If
                            End If



                            r.Cells.Add(c)
                            r.Cells.Add(c1)
                            r.Cells.Add(c4)
                            r.Cells.Add(c3)
                            r.Cells.Add(c2)
                            mytable.Rows.Add(r)
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


        

        'If DSAccList.Tables.Count > 0 Then
        '    For Each DR As DataRow In DSAccList.Tables(0).Rows


        '    Next
        'Else


        'End If

        'DSAccList.Dispose()
        

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
        ElseIf ActState.Value = 2 Then
            'PhySearch_Click()
        ElseIf ActState.Value = 3 Then
            'PhyStatus_Click()
            ' LocStatus()
        End If

    End Sub
    Sub LoadPage()
        Panel6.Visible = False
        HDictCode.Value = 1
        Panel5.Visible = True
        ActState.Value = 0
    End Sub
End Class

