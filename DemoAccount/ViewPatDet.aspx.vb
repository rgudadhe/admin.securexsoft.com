Imports System.Data.SqlClient
Partial Class DemoAccount_ViewPatDet
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        HActID.Value = Request("AccID")
        HFoldName.Value = Request("Tabname")
        DemoActStatus_Click()
    End Sub

    Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnAssign.Click
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim sQuery1 As String
        Dim j As Integer
        Dim TXTValue As String
        Dim TXTFValue As String
        'Dim DemoTextValue As String
        Dim strFldName() As String = Split(DemoFieldValue.Value, ",")
        'DemoTextValue = ""
        'Response.Write(DemoFieldText.Value & "#" & UBound(strFldName))
        sQuery1 = "Update ETSDemos.dbo." & HFoldName.Value & " Set "

        For j = 0 To DemoFieldText.Value - 1
            TXTValue = "TXTValue" & j
            If DemoFieldText.Value - 1 = 0 Or DemoFieldText.Value - 1 = j Then
                sQuery1 = sQuery1 & strFldName(j) & "='" & Request(TXTValue) & "'"
            Else
                sQuery1 = sQuery1 & strFldName(j) & "='" & Request(TXTValue) & "',"

            End If

            'TXTFValue = "TXTFValue" & j
            'Response.Write(strFldName(j) & ": " & Request(TXTValue))
            'If j = 1 Then
            '    DemoTextValue = "'" & Request(TXTValue) & "'"
            'Else
            '    DemoTextValue = DemoTextValue & "," & "'" & Request(TXTValue) & "'"
            'End If
        Next
        sQuery1 = sQuery1 & " where Lookupid = '" & HLookupID.Value & "' "
        'sQuery1 = "Insert Into ETSDemos.dbo." & HFoldName.Value & " (" & DemoFieldValue.Value & ") values " & "(" & DemoTextValue & ")"
        'Response.Write(sQuery1)
        'Response.End()

        Label1.Visible = True
        Label1.Text = "The record has beed updated successfully"
        Dim cmdUp As New SqlCommand(sQuery1, New SqlConnection(strConn))
        Try
            cmdUp.Connection.Open()
            cmdUp.ExecuteNonQuery()

        Finally
            If cmdUp.Connection.State = System.Data.ConnectionState.Open Then
                cmdUp.Connection.Close()
            End If
        End Try


    End Sub



    Protected Sub DemoActStatus_Click()
        If HLookupID.Value = "" Then
            HLookupID.Value = Request("LookupID").ToString
        End If
        Dim strConn As String
        Dim SelFields As String
        SelFields = ""
        Dim i As Integer
        Dim K As Integer
        i = 0
        K = 0
        Dim sqlQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim Cmd As New SqlCommand("Select * from tblActDemos where AccountID = '" & HActID.Value & "' ", New SqlConnection(strConn))
        Try
            Cmd.Connection.Open()
            Dim DRRec1 As SqlDataReader = Cmd.ExecuteReader()
            If DRRec1.HasRows Then
                While (DRRec1.Read)
                    K = K + 1
                    Dim Row1 As New TableRow
                    Dim Cell1 As New TableCell
                    Dim Cell2 As New TableCell
                    Cell1.Style("text-align") = "right"
                    'Cell1.Width = "200"
                    'Cell2.Width = "200"
                    Dim TX As New TextBox
                    Dim TXF As New HiddenField
                    TX.ID = "TXTValue" & i
                    TXF.ID = "TXTFValue" & i
                    TXF.Value = DRRec1("DemoFieldName")
                    'Response.Write(DRRec1("DemoFieldName"))
                    TX.Width = "150"
                    Cell1.CssClass = "DEMO4"
                    Cell2.CssClass = "DEMO4"
                    Cell1.Text = DRRec1("DemoFieldName")
                    Cell2.Controls.Add(TX)
                    Row1.Cells.Add(Cell1)
                    Row1.Cells.Add(Cell2)
                    sqlQuery = "Select " & DRRec1("DemoFieldName") & " from ETSDemos.DBO." & HFoldName.Value & " where LookupID='" & Request("LookupID").ToString & "'"
                    'Response.Write(sqlQuery)
                    'Response.End()
                    '  Dim MyConn As New OleDbConnection(strConn)
                    Dim Cmd3 As New SqlCommand(sqlQuery, New SqlConnection(strConn))
                    Cmd3.Connection.Open()
                    Dim DRRec4 As SqlDataReader = Cmd3.ExecuteReader()
                    If DRRec4.HasRows Then
                        While (DRRec4.Read)
                            If IsDBNull(DRRec4(DRRec1("DemoFieldName"))) Then
                                TX.Text = ""
                            Else
                                TX.Text = DRRec4(DRRec1("DemoFieldName"))
                            End If
                            TX.Enabled = True

                        End While
                    End If
                    DRRec4.Close()
                    Cmd3.Connection.Close()
                    If i = 0 Then
                        SelFields = DRRec1("DemoFieldName")
                    Else
                        SelFields = SelFields & "," & DRRec1("DemoFieldName")
                    End If
                    viewTable.Rows.Add(Row1)
                    i = i + 1
                End While
            End If
            DRRec1.Close()

        Finally
            If Cmd.Connection.State = System.Data.ConnectionState.Open Then
                Cmd.Connection.Close()
            End If
        End Try
        DemoFieldText.Value = i
        DemoFieldValue.Value = SelFields
        'Response.Write("DF" & DemoFieldValue.Value)
        'Row1.CssClass = "SMSelected"
        'Row1.Height = "22"
        'viewTable.Rows.Add(Row1)
        '  Response.Write(i)
        ' Response.End()
        'Dim TableName As String
        'TableName = ""
        'Dim Cmd2 As New SqlCommand("Select FolderName from tblAccounts where AccountID = '" & Session("AccID").ToString & "' ", New SqlConnection(strConn))
        'Cmd2.Connection.Open()
        'Dim DRRec3 As SqlDataReader = Cmd2.ExecuteReader()
        'If DRRec3.HasRows Then
        '    While (DRRec3.Read)
        '        TableName = DRRec3("FolderName")
        '    End While
        'End If
        'DRRec3.Close()
        'Cmd2.Connection.Close()
        'Dim sqlQuery As String
        'Dim k As Integer

        'sqlQuery = "Select LookupID, " & SelFields & " from ETSDemos.DBO." & TableName
        ''Response.Write(sqlQuery)
        ''  Dim MyConn As New OleDbConnection(strConn)
        'Dim Cmd3 As New SqlCommand(sqlQuery, New SqlConnection(strConn))
        'Cmd3.Connection.Open()
        'Dim DRRec4 As SqlDataReader = Cmd3.ExecuteReader()
        'If DRRec4.HasRows Then
        '    While (DRRec4.Read)
        '        Dim Row2 As New TableRow
        '        Row2.CssClass = "DEMO"
        '        Row2.Attributes.Add("onDblClick", "poptastic('" & DRRec4("LookupID").ToString & "');")
        '        For k = 1 To i
        '            Dim Cell2 As New TableCell
        '            Cell2.Text = DRRec4(DRRec4.GetName(k)).ToString
        '            Row2.Cells.Add(Cell2)
        '        Next
        '        viewTable.Rows.Add(Row2)
        '        'Response.Write(DRRec4.GetName(0))
        '    End While
        'End If
        'DRRec4.Close()
        'Cmd3.Connection.Close()


    End Sub



End Class
