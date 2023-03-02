Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.Common
Partial Class DemoAccount_ConfgDemo
    Inherits BasePage


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblStatus.Text = String.Empty
        If Not IsPostBack Then
            btnclose.Visible = False
            HActID.Value = Request("Accountid")

            ACount.Value = 3
            Dim i As Integer
            For i = ACount.Value To 34
                Table1.Rows(i).Visible = False
            Next
            Dim j As Integer
            j = ACount.Value - 2


            Dim clsAttributes As ETS.BL.Attributes
            Dim DSAtt As New DataSet
            Try
                clsAttributes = New ETS.BL.Attributes
                DSAtt = clsAttributes.getEditAttributes(Session("ContractorID").ToString)
                Dim d1 As DropDownList = Page.FindControl("DropDownList" & j)
                Dim I1 As New ListItem
                I1.Text = "Select Attribute"
                I1.Value = ""
                I1.Selected = "True"
                d1.Items.Add(I1)


                If DSAtt.Tables.Count > 0 Then
                    If DSAtt.Tables(0).Rows.Count > 0 Then
                        Dim DV As Data.DataView = New Data.DataView(DSAtt.Tables(0), String.Empty, String.Empty, DataViewRowState.CurrentRows)
                        If DV.Count > 0 Then
                            For Each DR As DataRow In DV.ToTable.Rows
                                If Not String.IsNullOrEmpty(DR("Type")) Then
                                    Dim DemoType As String
                                    DemoType = "Text"
                                    If DR("Type").ToString = 0 Then
                                        DemoType = "Text"
                                    ElseIf DR("Type").ToString = 1 Then
                                        DemoType = "Integer"
                                    ElseIf DR("Type").ToString = 2 Then
                                        DemoType = "Date"
                                    ElseIf DR("Type").ToString = 3 Then
                                        DemoType = "Boolean"
                                    End If
                                    Dim I2 As New ListItem
                                    I2.Text = DR("Name") & " (" & DemoType & ")"
                                    I2.Value = DR("AttributeId").ToString
                                    d1.Items.Add(I2)

                                End If

                            Next
                        End If
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally

                clsAttributes = Nothing
                DSAtt.Dispose()
            End Try

            GetAccData()
        End If
    End Sub
    Protected Function GetAccData()
        Dim clsAcc As ETS.BL.Accounts
        Dim DsAcc As Data.DataSet
        Dim DRRec1 As DataTableReader
        Try
            clsAcc = New ETS.BL.Accounts
            DsAcc = clsAcc.getAccountList(Session("WorkgroupID").ToString, Session("ContractorID").ToString, " AND AccountID =  '" & Request("AccountID") & "' ")
            If DsAcc.Tables.Count > 0 Then
                If DsAcc.Tables(0).Rows.Count > 0 Then
                    DRRec1 = DsAcc.Tables(0).CreateDataReader
                    If DRRec1.HasRows Then
                        While (DRRec1.Read)
                            Dim c As New TableCell
                            Dim c1 As New TableCell
                            Dim c2 As New TableCell
                            Dim c3 As New TableCell
                            Dim r As New TableRow
                            Dim fldname As String

                            fldname = DRRec1("foldername")
                            HFoldName.Value = DRRec1("FolderName")
                            c.Text = DRRec1("AccountName")
                            c1.Text = DRRec1("Description")
                            c2.Text = DRRec1("AccountNo")

                            r.Cells.Add(c)
                            r.Cells.Add(c1)
                            r.Cells.Add(c2)
                            Table5.Rows.Add(r)
                            'Response.Write(fldname.ToString)
                            If Not DRRec1("DemoConfg").ToString = "True" Then

                                Dim ws As com.securexsoft.sxf1.WebService
                                Try
                                    ws = New com.securexsoft.sxf1.WebService
                                    If ws.CreateDemoTableForAcc(Trim(fldname)) = True Then
                                        Dim clsAct As ETS.BL.Accounts
                                        Try
                                            clsAct = New ETS.BL.Accounts
                                            clsAct.AccountID = HActID.Value
                                            clsAct.DemoConfg = True
                                            clsAct.UpdateAccountDetails()
                                        Catch ex As Exception
                                            Response.Write(ex.Message)
                                        Finally
                                            clsAct = Nothing
                                        End Try
                                    End If
                                Catch ex As Exception
                                    Response.Write(ex.Message)
                                Finally
                                    ws = Nothing
                                End Try
                                'Dim clsDemo As ETS.BL.Demographics
                                'Try
                                '    clsDemo = New ETS.BL.Demographics
                                '    clsDemo.CreateDemoTbl(Trim(fldname), HActID.Value, Session("ContractorID").ToString)

                                'Catch ex As Exception
                                '    Response.Write(ex.Message)
                                'Finally
                                '    clsDemo = Nothing
                                'End Try
                            End If
                        End While
                    End If
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsAcc = Nothing
            DsAcc = Nothing
        End Try
    End Function
    Protected Sub AnyClicked(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim b As DropDownList = CType(sender, DropDownList)
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
        GetAccData()
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

    'Protected Sub AddImage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles AddImage.Click


    'End Sub

    'Protected Sub ConfigureDemo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ConfigureDemo.Click


    '    'Dim strconn As String
    '    'strconn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    'Dim SQuery As String
    '    'Dim SQuery1 As String
    '    'SQuery = "Select * from tblattributes where AttributeID ='" & Request(DLList) & "'"
    '    'Dim CmdAttr As New SqlCommand(SQuery, New SqlConnection(strconn))
    '    'Try
    '    '    CmdAttr.Connection.Open()
    '    '    Dim DRAttr As SqlDataReader = CmdAttr.ExecuteReader()
    '    '    If DRAttr.HasRows Then
    '    '        While DRAttr.Read
    '    '            DemoField = DRAttr("Name")
    '    '            If DRAttr("Type") = 0 Then
    '    '                DemoSize = Request(TxtList)
    '    '                DemoType = "Text"

    '    '            ElseIf DRAttr("Type") = 1 Then
    '    '                DemoSize = Request(TxtList)
    '    '                DemoType = "Integer"

    '    '            ElseIf DRAttr("Type") = 2 Then
    '    '                DemoSize = 16
    '    '                DemoType = "date"

    '    '            ElseIf DRAttr("Type") = 3 Then
    '    '                DemoSize = 1
    '    '                DemoType = "Boolean"

    '    '            End If
    '    '            SQuery1 = "Alter Table ETSDemos.dbo." & HFoldName.Value & " Add " & DemoField & " nvarchar(" & DemoSize & ")"

    '    '            Dim CmdUp As New SqlCommand(SQuery1, New SqlConnection(strconn))
    '    '            Try
    '    '                CmdUp.Connection.Open()
    '    '                CmdUp.ExecuteNonQuery()
    '    '            Finally
    '    '                If CmdUp.Connection.State = ConnectionState.Open Then
    '    '                    CmdUp.Connection.Close()
    '    '                End If
    '    '            End Try

    '    '            SQuery1 = "Insert Into dbo.tblActDemos (AccountID, DemoFieldName, DemoFieldType, DemoFieldSize, UpdateDate) Values ('" & HActID.Value & "', '" & DemoField & "', '" & DemoType & "', '" & DemoSize & "' , '" & Now & "')"
    '    '            'responsse.write()
    '    '            Dim CmdUp1 As New SqlCommand(SQuery1, New SqlConnection(strconn))
    '    '            Try
    '    '                CmdUp1.Connection.Open()
    '    '                CmdUp1.ExecuteNonQuery()
    '    '            Finally
    '    '                If CmdUp1.Connection.State = ConnectionState.Open Then
    '    '                    CmdUp1.Connection.Close()
    '    '                End If
    '    '            End Try


    '    '        End While
    '    '    End If
    '    '    DRAttr.Close()
    '    'Finally
    '    '    If CmdAttr.Connection.State = ConnectionState.Open Then
    '    '        CmdAttr.Connection.Close()
    '    '    End If
    '    'End Try
    '    'End If



    '    'Dim strconn As String
    '    'strconn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    'Dim SQuery As String

    '    'SQuery = "Select * from tblattributes order by name"
    '    'Dim CmdAttr As New SqlCommand(SQuery, New SqlConnection(strconn))
    '    'CmdAttr.Connection.Open()
    '    'Dim DRAttr As SqlDataReader = CmdAttr.ExecuteReader()
    '    'If DRAttr.HasRows Then
    '    '    While DRAttr.Read
    '    '        Dim DemoType As String
    '    '        DemoType = "Text"
    '    '        If DRAttr("Type") = 0 Then
    '    '            DemoType = "Text"
    '    '        ElseIf DRAttr("Type") = 1 Then
    '    '            DemoType = "Integer"
    '    '        ElseIf DRAttr("Type") = 2 Then
    '    '            DemoType = "Date"
    '    '        ElseIf DRAttr("Type") = 3 Then
    '    '            DemoType = "Boolean"
    '    '        End If
    '    '        Dim I2 As New ListItem
    '    '        I2.Text = DRAttr("Name") & " (" & DemoType & ")"
    '    '        I2.Value = DRAttr("AttributeId").ToString
    '    '        d1.Items.Add(I2)

    '    '    End While
    '    'End If


    '    'DRAttr.Close()
    '    'CmdAttr.Connection.Close()
    'End Sub

    Protected Sub BtnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        Table1.Rows(ACount.Value).Visible = True
        ACount.Value = ACount.Value + 1
        'Response.Write(ACount.Value)
        'Dim strconn As String
        'strconn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'Dim SQuery As String

        Dim j As Integer
        j = ACount.Value - 2
        'For j = 1 To 33
        Dim d1 As DropDownList = Page.FindControl("DropDownList" & j)
        Dim I1 As New ListItem
        I1.Text = "Select Attribute"
        I1.Value = ""
        I1.Selected = "True"
        d1.Items.Add(I1)

        Dim clsAttributes As ETS.BL.Attributes
        Dim DSAtt As New DataSet

        Try
            clsAttributes = New ETS.BL.Attributes
            DSAtt = clsAttributes.getEditAttributes(Session("ContractorID").ToString)


            If DSAtt.Tables.Count > 0 Then
                If DSAtt.Tables(0).Rows.Count > 0 Then
                    Dim DV As Data.DataView = New Data.DataView(DSAtt.Tables(0), String.Empty, String.Empty, DataViewRowState.CurrentRows)
                    If DV.Count > 0 Then
                        For Each DR As DataRow In DV.ToTable.Rows
                            If Not String.IsNullOrEmpty(DR("Type")) Then
                                Dim DemoType As String
                                DemoType = "Text"
                                If DR("Type").ToString = 0 Then
                                    DemoType = "Text"
                                ElseIf DR("Type").ToString = 1 Then
                                    DemoType = "Integer"
                                ElseIf DR("Type").ToString = 2 Then
                                    DemoType = "Date"
                                ElseIf DR("Type").ToString = 3 Then
                                    DemoType = "Boolean"
                                End If
                                Dim I2 As New ListItem
                                I2.Text = DR("Name") & " (" & DemoType & ")"
                                I2.Value = DR("AttributeId").ToString
                                d1.Items.Add(I2)

                            End If

                        Next
                    End If
                End If
            End If
        Catch ex As Exception
        Finally
            clsAttributes = Nothing
            DSAtt.Dispose()
        End Try
        GetAccData()
    End Sub

    Protected Sub btnConfgDemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfgDemo.Click
        Dim chkValue As Integer
        chkValue = ACount.Value - 2
        Dim k As Integer
        Dim DLList As String
        Dim TxtList As String
        Dim DT As New DataTable
        DT = New DataTable

        Try
            DT.Columns.Add(New DataColumn("FieldName"))
            DT.Columns.Add(New DataColumn("FieldSize"))
            DT.Columns.Add(New DataColumn("FieldType"))

            For k = 1 To chkValue
                DLList = "DropDownList" & k
                TxtList = "TextBox" & k

                If Request(DLList) <> "" Then
                    Dim DemoField As String
                    Dim DemoSize As String
                    Dim DemoType As String
                    DemoType = "Text"
                    DemoSize = 50

                    Dim DR As Data.DataRow = DT.NewRow
                    Dim clsAtt As New ETS.BL.Attributes
                    Dim DsAtt As New Data.DataSet
                    DsAtt = clsAtt.getEditAttributes(Session("ContractorID").ToString)


                    If DsAtt.Tables.Count > 0 Then
                        If DsAtt.Tables(0).Rows.Count > 0 Then
                            Dim DV As Data.DataView = New Data.DataView(DsAtt.Tables(0), "AttributeID='" & Request(DLList).ToString & "'", String.Empty, DataViewRowState.CurrentRows)
                            If DV.Count > 0 Then
                                For Each DRTemp As DataRow In DV.ToTable.Rows
                                    If Not String.IsNullOrEmpty(DRTemp("Type")) Then

                                        DemoField = DRTemp("Name")
                                        If DRTemp("Type") = 0 Then
                                            DemoSize = Request(TxtList)
                                            DemoType = "Text"
                                        ElseIf DRTemp("Type") = 1 Then
                                            DemoSize = Request(TxtList)
                                            DemoType = "Integer"
                                        ElseIf DRTemp("Type") = 2 Then
                                            DemoSize = 16
                                            DemoType = "date"
                                        ElseIf DRTemp("Type") = 3 Then
                                            DemoSize = 1
                                            DemoType = "Boolean"
                                        End If

                                        DR("FieldName") = DemoField
                                        DR("FieldSize") = DemoSize
                                        DR("FieldType") = DemoType

                                        DT.Rows.Add(DR)

                                    End If

                                Next
                            End If
                        End If
                    End If
                    clsAtt = Nothing
                End If
            Next
            Dim varBol As Boolean = True
            If DT.Rows.Count > 0 Then
                Dim ws As com.securexsoft.sxf1.WebService
                Dim clsDemo As ETS.BL.Demographics
                Try
                    ws = New com.securexsoft.sxf1.WebService
                    clsDemo = New ETS.BL.Demographics
                    For Each DR As DataRow In DT.Rows
                        If ws.AlterDemoTableForAcc(HFoldName.Value.ToString, DR("FieldName").ToString, DR("FieldSize").ToString) = True Then
                            Try
                                If clsDemo.UpdateSingleDemoField(HActID.Value.ToString, DR("FieldName").ToString, DR("FieldType").ToString, DR("FieldSize").ToString) = True Then
                                    varBol = True
                                Else
                                    varBol = False
                                    Exit For
                                End If
                            Catch ex As Exception
                                Response.Write(ex.Message)
                            End Try
                        Else
                            varBol = False
                            Exit For
                        End If
                    Next


                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    ws = Nothing
                    clsDemo = Nothing
                End Try


                'Dim clsDemo As ETS.BL.Demographics
                'Try
                '    clsDemo = New ETS.BL.Demographics
                '    If clsDemo.UpdateDemoField(HFoldName.Value, HActID.Value, DT) = True Then
                '        lblStatus.Text = "Demo Configured Sucessfully"
                '        btnclose.Visible = True
                '        Table5.Visible = False
                '        Table1.Visible = False

                '        BtnAdd.Visible = False
                '        btnConfgDemo.Visible = False
                '    End If


                'Catch ex As Exception
                '    Response.Write("DT" & ex.Message)
                'Finally
                '    clsDemo = Nothing
                'End Try
            End If
            If varBol = True Then
                lblStatus.Text = "Demo Configured Sucessfully"
                btnclose.Visible = True
                Table5.Visible = False
                Table1.Visible = False

                BtnAdd.Visible = False
                btnConfgDemo.Visible = False
            End If


        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            DT.Dispose()

        End Try
    End Sub
End Class
