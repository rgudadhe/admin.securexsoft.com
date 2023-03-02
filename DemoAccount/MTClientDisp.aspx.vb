Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common

Partial Class DemoAccount_ConfgDemo
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HActID.Value = Request("Accountid")
            DispdemoDetails()
        End If
    End Sub



    Sub DispdemoDetails()
        If Not IsPostBack Then
            HCHBx.Value = 0
        End If
        Dim clsDemo As New ETS.BL.Demographics
        Dim DSFieldList As DataSet = clsDemo.getAcctDemoFieldsDataSet(Request("AccountID"))
        clsDemo = Nothing
        If DSFieldList.Tables.Count > 0 Then
            For Each DR As DataRow In DSFieldList.Tables(0).Rows
                HCHBx.Value = HCHBx.Value + 1
                Dim TABL As Table = Page.FindControl("Table2")
                Dim c As New TableCell
                Dim c1 As New TableCell
                Dim c2 As New TableCell
                Dim c3 As New TableCell
                Dim r As New TableRow
                Dim CH As New CheckBox
                CH.ID = "CH" & HCHBx.Value
                'CH.Attributes.Add "value"
                CH.InputAttributes.Add("Value", DR("RecordID").ToString)
                If IsDBNull(DR("MtClientDisp")) Then
                    CH.Checked = "False"
                Else
                    If DR("MtClientDisp") Then
                        CH.Checked = "True"
                    Else
                        CH.Checked = "False"
                    End If

                End If
                c2.Controls.Add(CH)
                'TotPhy.Value = K
                c.Text = DR("DemofieldName") & " (" & DR("DemoFieldType") & ")"
                c1.Text = DR("DemoFieldSize")
                c3.Text = "<img src=../images/p_remove1.jpg style='cursor:hand;' onclick=poptastic('" & DR("RecordID").ToString & "','" & HActID.Value & "')>"

                'c.Width = "80"
                'c1.Width = "20"
                r.Cells.Add(c)
                r.Cells.Add(c1)
                ' r.Cells.Add(c3)
                r.Cells.Add(c2)
                TABL.Rows.Add(r)

            Next
        End If
        DSFieldList.Dispose()


    End Sub
    Protected Sub btnConfigure_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfigure.Click
        Dim chkValue As Integer
        chkValue = HCHBx.Value
        Dim k As Integer
        Dim CHList As String
        Dim strRecValues As String = String.Empty
        For k = 1 To chkValue
            CHList = "CH" & k
            If Request(CHList) <> "" Then
                If String.IsNullOrEmpty(strRecValues) Then
                    strRecValues = Request(CHList)
                Else
                    strRecValues = strRecValues & "," & Request(CHList)
                End If
            End If
        Next
        Dim clsdemo As New ETS.BL.Demographics
        clsdemo.AccountID = HActID.Value
        clsdemo.UpdateMTCStatus(strRecValues)
        clsdemo = Nothing
        DispdemoDetails()
    End Sub
    
End Class
