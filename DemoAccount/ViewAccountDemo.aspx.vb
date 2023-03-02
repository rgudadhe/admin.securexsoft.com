Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Data



Partial Class UserPhyAssgn_Default
    Inherits BasePage




    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Button1.Visible = False
        If Not Page.IsPostBack Then
            Panel1.Visible = True
            Dim clsAcc As ETS.BL.Accounts
            Dim dsAccList As Data.DataSet
            Dim DV As New Data.DataView
            'clsAcc.ContractorID = Session("ContractorID").ToString
            'clsAcc.DemoConfg = True
            'clsAcc._WhereString.Append(" and (IsDeleted is null or IsDeleted =0)")

            Try
                clsAcc = New ETS.BL.Accounts
                dsAccList = clsAcc.getAccountList(Session("WorkGroupID"), Session("ContractorID"), String.Empty)

                If dsAccList.Tables.Count > 0 Then
                    If dsAccList.Tables(0).Rows.Count > 0 Then
                        ActList.DataSource = dsAccList
                        ActList.DataTextField = "AccountName"
                        ActList.DataValueField = "AccountID"
                        ActList.DataBind()
                    End If
                End If
            Catch ex As Exception
        Finally
            clsAcc = Nothing
            dsAccList = Nothing
            DV = Nothing
        End Try

            Dim LI As New ListItem
            LI.Text = "Please select"
            LI.Value = ""
            ActList.Items.Insert(0, LI)
            LI.Selected = True
        Else
            Panel1.Visible = False
        End If

    End Sub

    Sub BindSQL(ByVal SortField As String)
        Panel1.Visible = True

        Dim SelFields As String = String.Empty
        Dim ColSearch1 As Boolean
        Dim ColSearch2 As Boolean
        Dim ColSearch3 As Boolean
        Dim ColSearch4 As Boolean
        Dim ColSearch5 As Boolean
        ColSearch1 = False
        ColSearch2 = False
        ColSearch3 = False
        ColSearch4 = False
        ColSearch5 = False

        Dim clsDemo As New ETS.BL.Demographics
        SelFields = clsDemo.getAcctDemoFields(HActID.Value)
        clsDemo = Nothing

        Dim Row1 As New TableHeaderRow

        If Not String.IsNullOrEmpty(SelFields) Then
            Dim FCell As New TableCell
            FCell.Text = "<input type=checkbox name=ChkAll onclick=changeAll();>"
            Row1.Cells.Add(FCell)
            Dim DRRec1() As String = Split(SelFields, ",")
            For i As Integer = 0 To UBound(DRRec1)
                Dim Cell1 As New TableCell
                Cell1.Text = DRRec1(i).ToString
                Cell1.HorizontalAlign = HorizontalAlign.Center
                Row1.Cells.Add(Cell1)

                If DRRec1(i).ToString = "DTOfServ" Then
                    ColSearch1 = True
                ElseIf DRRec1(i).ToString = "PFirstName" Then
                    ColSearch2 = True
                ElseIf DRRec1(i).ToString = "PLastName" Then
                    ColSearch3 = True
                ElseIf DRRec1(i).ToString = "MedRN" Then
                    ColSearch4 = True
                ElseIf DRRec1(i).ToString = "APhyName" Then
                    ColSearch5 = True
                End If
                i = i + 1
            Next
        Else
            DispBox.Text = "No attributes are assigned to this account. Please contact System Administrator for more details."
            Exit Sub
        End If


        Row1.CssClass = "SMSelected"
        Row1.Height = "22"
        Table1.Rows.Add(Row1)

        Dim TableName As String
        TableName = ""
        Dim clsAcc As New ETS.BL.Accounts
        With clsAcc
            .AccountID = HActID.Value.ToString
            .getAccountDetails()
            TableName = .foldername
            HTabName.Value = .foldername
        End With
        clsAcc = Nothing
        

        Dim sdate As String
        Dim edate As String
        If DTOfServ1.Text <> "" Then
            sdate = DTOfServ1.Text
            sdate = CDate(sdate).ToShortDateString
        Else
            sdate = ""
        End If
        If DTOfServ2.Text <> "" Then
            edate = DTOfServ2.Text
            edate = CDate(edate).AddDays(1).ToShortDateString
        Else
            edate = ""
        End If
     
        clsDemo = New ETS.BL.Demographics
        Dim DSDemo As DataSet = clsDemo.getAcctDemos(SelFields, TableName, sdate, edate, PFirstName.Text, PLastName.Text, MedRN.Text, APhyName.Text)

        clsDemo = Nothing



        Dim RecCount As Long
        RecCount = 0
        If DSDemo.Tables.Count > 0 Then
            Button1.Visible = True
            For Each DRRec4 As DataRow In DSDemo.Tables(0).Rows
                RecCount = RecCount + 1
                Dim Row2 As New TableRow
                Dim FCEll As New TableCell
                FCEll.Text = "<input type=checkbox name=DemoRec onclick=highlightRow(this); value=" & DRRec4("LookupID").ToString & ">"
                Row2.Cells.Add(FCEll)
                For Each DC As DataColumn In DSDemo.Tables(0).Columns
                    Dim Cell2 As New TableCell
                    Cell2.Attributes.Add("ondblclick", "EditDet('" & DRRec4("LookupID").ToString & "','" & TableName & "','" & HActID.Value & "');")
                    Cell2.Text = DRRec4(DC.ColumnName).ToString
                    Cell2.HorizontalAlign = HorizontalAlign.Center
                    Row2.Cells.Add(Cell2)
                Next
                Table1.Rows.Add(Row2)
            Next
            DispBox.Text = "Number Of Records Found: " & RecCount
        Else
            DispBox.Text = "No Records Found."
        End If


    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'Response.Write(Request("DemoRec"))
        Dim strDemoRec() As String
        Dim sQuery1 As String
        strDemoRec = Split(Request("DemoRec"), ",")
        Dim i As Integer
        For i = 0 To strDemoRec.Length - 1
            sQuery1 = "Delete from ETSDemos.DBO." & HTabName.Value & " where LookupID='" & strDemoRec(i) & "'"
            'Response.Write(sQuery1)
            Dim cmdUp As New SqlCommand(sQuery1, New SqlConnection(strConn))
            Try
                cmdUp.Connection.Open()
                cmdUp.ExecuteNonQuery()

            Finally
                If cmdUp.Connection.State = System.Data.ConnectionState.Open Then
                    cmdUp.Connection.Close()
                End If
            End Try
        Next

        BindSQL("DtOFServ")
    End Sub

    Protected Sub Submit3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Submit3.Click
        HActID.Value = ActList.SelectedValue
        BindSQL("DtOFServ")
    End Sub
End Class

