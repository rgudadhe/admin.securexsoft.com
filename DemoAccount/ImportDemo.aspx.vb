Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data



Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton

    'Public ProcFolder As String = Server.MapPath("../ETS_Files")
    Public ProcFolder As String = Server.MapPath("/ETS_Files/")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Assign.Visible = False
        DispBox.Visible = False
        Table4.Visible = False
        If Not IsPostBack Then
            Assign.Visible = False
            Table4.Visible = False
            Panel6.Visible = False

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

        Panel2.Visible = False

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
        Dim DVAct As New DataView
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
            DVAct = Nothing
        End Try

        Dim CB As New Button
        Dim r2 As New TableRow
        Dim c4 As New TableCell
        c4.ColumnSpan = 3
        c4.Style("text-align") = "center"
        Submit3.Visible = True

        c4.Controls.Add(Submit3)


        r2.Cells.Add(c4)
        Table1.Rows.Add(r2)


        Panel6.Controls.Add(t2)
        'Panel6.Controls.Add(CB)
        


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
        

        Try
            Dim oApp As Excel.Application
            Dim oWB As Excel.Workbook
            oApp = New Excel.Application
            oApp.Visible = False
            oWB = oApp.Workbooks.Add

            Filename = ""
            Dim FilePath As String
            FilePath = ""


            Dim clsDemo As New ETS.BL.Demographics
            Dim arrDemoFields() As String = Split(clsDemo.getAcctDemoFields(HActID.Value), ",")
            clsDemo = Nothing

            For K = 1 To UBound(arrDemoFields) + 1
                oWB.Sheets(1).Cells(1, K).Value = arrDemoFields(K - 1)
            Next

            Dim strFolder As String
            Filename = Filename & " " & Format(Now(), "yyyymmdd_HHmmss") & ".xls"

            strFolder = ProcFolder & "secureweb\DemoTemplate\"
            FilePath = strFolder & Filename
          
            Dim fso
            fso = CreateObject("Scripting.FileSystemObject")
            If Not fso.FolderExists(strFolder) Then
                'Create the file
                FileSystem.MkDir(strFolder)
            End If


            If IO.File.Exists(FilePath) Then
                IO.File.Delete(FilePath)
            End If
            oWB.SaveAs(FilePath)

            oWB.Close(False)
           
            ReleaseComObject(oWB)
            oApp.Quit()
            ReleaseComObject(oApp)
           

            HyperLink1.NavigateUrl = WebAddress & "/ETS_Files/secureweb/DemoTemplate/" & Filename
            
        Catch ex As Exception
            Response.Write("Error : " & ex.Message & " " & "Please contact E-Dictate Customer Support for more details.")
        End Try



    End Sub
    Private Sub ReleaseComObject(ByRef Reference As Object)
        Try
            Do Until _
             System.Runtime.InteropServices.Marshal.ReleaseComObject(Reference) <= 0
            Loop
        Catch
        Finally
            Reference = Nothing
        End Try
    End Sub


    
    Sub LoadPage()
        Assign.Visible = False
        Table4.Visible = False
        Panel6.Visible = False

        Panel2.Visible = False
        HDictCode.Value = 1
        Panel5.Visible = True
        ActState.Value = 0
    End Sub




    
    Protected Sub btnSubmit2_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit2.Click
        ActState.Value = 1
        HActID.Value = Request("AccountID")
        PageStatus()
    End Sub

    Protected Sub Assign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Assign.Click
        Dim strFolder As String
        strFolder = ProcFolder & "\secureweb\DemoTemplate\"
        If FileUpload1.HasFile Then
            Try
                FileUpload1.SaveAs(strFolder & _
                   FileUpload1.FileName)
                Label2.Text = "File name: " & _
                   FileUpload1.PostedFile.FileName & "<br>" & _
                   "File Size: " & _
                   FileUpload1.PostedFile.ContentLength & " kb<br>" & _
                   "Content type: " & _
                   FileUpload1.PostedFile.ContentType
                FileUpload1.Visible = False
            Catch ex As Exception
                Label2.Text = "ERROR: " & ex.Message.ToString()
                Exit Sub
            End Try
        Else
            Label2.Text = "You have not specified a file."
            Exit Sub
        End If
        Dim filename As String
        
        Dim DFNAme As String


        filename = strFolder & FileUpload1.FileName






        Dim CNNEXCEL As New OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source=" + filename + "; Extended Properties=Excel 8.0;")
        Dim SQLCmd1 As New OleDbCommand("SELECT * FROM [Sheet1$]", CNNEXCEL)
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As OleDbDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then
                Dim FieldCount As String
                FieldCount = DRRec1.FieldCount
                '  Response.Write(FieldCount)
                '  Response.Write(DRRec1.GetName(0))
                '  Response.Write(DRRec1.GetName(1))
                Dim FCount(FieldCount - 1) As String
                Dim FldName As String
                Dim FldType(FieldCount - 1) As String
                Dim FldSize(FieldCount - 1) As String
                Dim FldValue As String
                FldValue = ""
                FldName = ""
                Dim i As Integer
                Dim k As Integer
                k = 0
                If FieldCount > 0 Then
                    For i = 0 To FieldCount - 1
                        'Response.Write(DRRec1.GetName(i))
                        If i = 0 Then
                            FldName = DRRec1.GetName(i)
                        Else
                            FldName = FldName & "," & DRRec1.GetName(i)
                        End If

                        DFNAme = DRRec1.GetName(i)
                        Dim clsDemo As New ETS.BL.Demographics
                        With clsDemo
                            .AccountID = HActID.Value
                            .DemoFieldName = DRRec1.GetName(i)
                            .getDemoAccDetails()
                            FldType(i) = .DemoFieldType
                            FldSize(i) = .DemoFieldSize
                        End With
                        clsDemo = Nothing
                    Next
                End If
                While (DRRec1.Read)
                    k = 0
                    For k = 0 To FieldCount - 1
                        If k = 0 Then
                            FldValue = "'" & DRRec1(k) & "'"
                        Else
                            FldValue = FldValue & "," & "'" & DRRec1(k) & "'"
                        End If

                    Next
                   
                    Dim clsDemo As New ETS.BL.Demographics
                    Label1.Visible = True
                    If clsDemo.setAcctDemos(FldName, FldValue, HFoldName.Value) = 1 Then
                        Label1.Text = "The record has been updated successfully."
                    Else
                        Label1.Text = "Updating Record Failed."
                    End If
                    clsDemo = Nothing
                End While
            End If
            DRRec1.Close()
        Finally
            If SQLCmd1.Connection.State = ConnectionState.Open Then
                SQLCmd1.Connection.Close()
            End If
        End Try

        CNNEXCEL.Close()

    End Sub

    Protected Sub Submit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit3.Click
        ActState.Value = 3
        HActID.Value = Request("AccountID")
        PageStatus()
    End Sub
End Class

