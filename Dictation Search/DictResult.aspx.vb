Imports System.Data
Partial Class Dictation_Search_DictResult
    Inherits System.Web.UI.Page
    Private WhereClause As StringBuilder
    Private Sub DBind()
        WhereClause = New StringBuilder
        Dim OrderByClause As String
        Dim IsUserCri As Boolean
        Dim NeedUnion As Integer
        If Session("IsContractor") <> 1 Then
            lstOptions.Items.RemoveAt(5)
            lstOptions.Items.RemoveAt(4)
            lstOptions.Items.RemoveAt(3)
        End If

        If IsPostBack = False And String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
            Dim Track As String = Request("Track").ToString()
            Dim Cust As String = Request("Cust").ToString
            Dim PFirst As String = Request("PFirst").ToString
            Dim PLast As String = Request("PLast").ToString

            Dim sDate As String = Request("sDate").ToString
            Dim eDate As String = Request("eDate").ToString

            Dim DCode As String = Request("DCode").ToString
            Dim AccName As String = Request("AccName").ToString
            Dim ACCNum As String = String.Empty
            Dim PIN As String = String.Empty
            Dim PatientF As String = String.Empty
            Dim PatientL As String = String.Empty
            Dim DOS As String = String.Empty
            Dim DOB As String = String.Empty
            Dim MRN As String = String.Empty
            Dim TType As String = String.Empty
            Dim TName As String = String.Empty

            Dim i As Integer = 1

            Do While Not i = 4
                Dim ifield As String = Request("lblOp" & i).ToString
                Select Case ifield
                    Case "Account#"
                        ACCNum = Request("valOp" & i).ToString
                    Case "PIN"
                        PIN = Request("valOp" & i).ToString
                    Case "Patient First"
                        PatientF = Request("valOp" & i).ToString
                    Case "Patient Last"
                        PatientL = Request("valOp" & i).ToString
                    Case "Date Of Service"
                        DOS = Request("valOp" & i).ToString
                    Case "Date of Birth"
                        DOB = Request("valOp" & i).ToString
                    Case "MRN"
                        MRN = Request("valOp" & i).ToString
                    Case "Template Type"
                        TType = Request("valOp" & i).ToString
                    Case "Template Name"
                        TName = Request("valOp" & i).ToString
                End Select
                i = i + 1
            Loop

            Dim Status As String = Request("UStatus")

            Dim Level As String = String.Empty
            If Not String.IsNullOrEmpty(Request("Level")) Then
                Level = Request("Level").ToString
            End If
            Dim UserID As String = String.Empty
            If Not String.IsNullOrEmpty(Request("UserID")) Then
                UserID = Request("UserID").ToString
            End If
            Dim UserName As String = String.Empty
            If Not String.IsNullOrEmpty(Request("UserName")) Then
                UserName = Request("UserName").ToString
            End If
            If String.IsNullOrEmpty(sDate) = False And String.IsNullOrEmpty(eDate) = False Then
                If DateDiff(DateInterval.Day, CDate(sDate), CDate(eDate)) < 0 Then
                    'iMain.Visible = False
                    'lblMessage.Text = "Start date cannot be greater than end date!"
                    'iMessage.Visible = True
                    Exit Sub
                End If
            End If
            If String.IsNullOrEmpty(UserID) = False Or String.IsNullOrEmpty(UserName) = False Then
                IsUserCri = True
            End If

            If String.IsNullOrEmpty(Status) Then
                NeedUnion = 1
            ElseIf Status = "1073741824" Then
                NeedUnion = 2
            End If
            'WhereClause = New ETS.BL.Dictations().getDictationSearchWhereClause(Track, Status, Cust, PFirst, PLast, PIN, sDate, eDate, DCode, AccName, ACCNum, TName, TType, PatientF, PatientL, DOB, DOS, MRN, UserID, UserName, Level, Session("IsContractor").ToString, Session("ParentID").ToString, Session("ContractorID").ToString)
            WhereClause = New ETS.BL.Dictations().getDictationSearchWhereClause(Track, Status, Cust, PFirst, PLast, PIN, sDate, eDate, DCode, AccName, ACCNum, TName, TType, PatientL, DOB, DOS, MRN, UserID, UserName, Level, Session("IsContractor").ToString, Session("ParentID").ToString, Session("ContractorID").ToString)
        End If

        OrderByClause = " ORDER BY SubmitDate "


        'Dim objDS As System.Data.DataSet = New ETS.BL.Dictations().DictationSearch(WhereClause.ToString, OrderByClause.ToString, IsUserCri)
        Dim objDS As System.Data.DataSet = New ETS.BL.Dictations().DictationSearch(WhereClause.ToString, OrderByClause.ToString, IsUserCri, NeedUnion)

        'If String.IsNullOrEmpty(intRecordCount.Value) Then
        '    intRecordCount.Value = CStr(objDS.Tables(0).Rows.Count)
        'End If
        'If intRecordCount.Value <= 0 Then
        '    iMain.Visible = False
        '    lblMessage.Text = "No Records Found!"
        '    iMessage.Visible = True
        'End If


        dlist.DataSource = objDS    '.Tables(0).DefaultView
        dlist.DataBind()
        objDS.Dispose()


        If dlist.Rows.Count > 0 Then
            dlist.ShowFooter = True
            dlist.UseAccessibleHeader = True
            dlist.HeaderRow.TableSection = TableRowSection.TableHeader
            dlist.FooterRow.TableSection = TableRowSection.TableFooter
            Opr.Visible = True
        End If
    End Sub
    Function datediffToMe(ByVal d1 As Double, ByVal d2 As Date) As String
        Dim DueDate As Date
        DueDate = DateAdd(DateInterval.Hour, d1, d2)
        Return DateDiff(DateInterval.Hour, Now(), DueDate).ToString
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack And String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
            DBind()
            BindPL()
            LinkButton1.Visible = False
        End If
    End Sub
    Protected Sub BindPL()
        Dim clsPL As ETS.BL.ProductionLevels
        Dim DSPL As New DataSet
        Try
            clsPL = New ETS.BL.ProductionLevels
            DSPL = clsPL.getProductionLevelsByContractorType(Session("ContractorID"), Session("ParentID"), Session("IsContractor"), IIf(Session("IsContractor"), 0, 1))

            lstLevel.DataSource = DSPL
            lstLevel.DataTextField = "LevelName"
            lstLevel.DataValueField = "LevelNo"
            lstLevel.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsPL = Nothing
            DSPL.Dispose()
        End Try
    End Sub
    Protected Sub lstLevel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstLevel.SelectedIndexChanged
        Try
            Dim LST As DropDownList = CType(sender, DropDownList)
            If LST.SelectedValue <> 0 Then
                Dim AutoCompleteSearch As AjaxControlToolkit.AutoCompleteExtender
                AutoCompleteSearch = New AjaxControlToolkit.AutoCompleteExtender
                With AutoCompleteSearch
                    .MinimumPrefixLength = "1"
                    .CompletionSetCount = "10"
                    .TargetControlID = "txtUser"
                    .ServicePath = "../users/autocomplete.asmx"
                    .EnableCaching = "true"
                    .ServiceMethod = "GetUserName" & LST.SelectedValue.ToString
                End With
                UpdatePanel2.ContentTemplateContainer.Controls.Add(AutoCompleteSearch)
                txtUser.Visible = True
            End If
        Catch ex As Exception
            txtUser.Text = ex.Message
        End Try
    End Sub
    Protected Sub lstOptions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstOptions.SelectedIndexChanged
        Try
            Dim LST As DropDownList = CType(sender, DropDownList)
            'Response.Write(LST)
            If LST.SelectedValue = "1" Then
                Dim txt As TextBox = LST.Parent.FindControl("txtTAT")
                txt.Visible = False
                Dim lstStatus As DropDownList = LST.Parent.FindControl("lstStatus")
                lstStatus.Visible = True
                Dim txtUser As TextBox = LST.Parent.FindControl("txtUser")
                txtUser.Visible = False
                Dim lstLevel As DropDownList = LST.Parent.FindControl("lstLevel")
                lstLevel.Visible = False
                Dim txtP As TextBox = LST.Parent.FindControl("txtPhy")
                txtP.Visible = False
            ElseIf LST.SelectedValue = "3" Then
                Dim lstStatus As DropDownList = LST.Parent.FindControl("lstStatus")
                lstStatus.Visible = False
                Dim txt As TextBox = LST.Parent.FindControl("txtTAT")
                txt.Visible = True
                Dim txtUser As TextBox = LST.Parent.FindControl("txtUser")
                txtUser.Visible = False
                Dim lstLevel As DropDownList = LST.Parent.FindControl("lstLevel")
                lstLevel.Visible = False
                Dim txtP As TextBox = LST.Parent.FindControl("txtPhy")
                txtP.Visible = False
            ElseIf LST.SelectedValue = "2" Then
                Dim lstStatus As DropDownList = LST.Parent.FindControl("lstStatus")
                lstStatus.Visible = False
                Dim txt As TextBox = LST.Parent.FindControl("txtTAT")
                txt.Visible = False
                Dim txtP As TextBox = LST.Parent.FindControl("txtPhy")
                txtP.Visible = False
                Dim txtUser As TextBox = LST.Parent.FindControl("txtUser")
                txtUser.Visible = False
                Dim clsPL As New ETS.BL.ProductionLevels
                Dim DSPL As DataSet = clsPL.getProductionLevelsByContractorType(Session("ContractorID"), Session("ParentID"), Session("IsContractor"), IIf(Session("IsContractor"), 0, 1))
                clsPL = Nothing
                Dim lstLevel As DropDownList = LST.Parent.FindControl("lstLevel")
                lstLevel.DataSource = DSPL
                lstLevel.DataTextField = "LevelName"
                lstLevel.DataValueField = "LevelNo"
                lstLevel.DataBind()
                lstLevel.Visible = True
            ElseIf LST.SelectedValue = "4" Then
                Dim lstStatus As DropDownList = LST.Parent.FindControl("lstStatus")
                lstStatus.Visible = False
                Dim txt As TextBox = LST.Parent.FindControl("txtTAT")
                txt.Visible = False
                Dim txtUser As TextBox = LST.Parent.FindControl("txtUser")
                txtUser.Visible = False
                Dim lstLevel As DropDownList = LST.Parent.FindControl("lstLevel")
                lstLevel.Visible = False
                Dim txtP As TextBox = LST.Parent.FindControl("txtPhy")
                txtP.Visible = False
            ElseIf LST.SelectedValue = "5" Then
                Dim lstStatus As DropDownList = LST.Parent.FindControl("lstStatus")
                lstStatus.Visible = False
                Dim txt As TextBox = LST.Parent.FindControl("txtTAT")
                txt.Visible = False
                Dim txtUser As TextBox = LST.Parent.FindControl("txtUser")
                txtUser.Visible = False
                Dim txtPhy As TextBox = LST.Parent.FindControl("txtPhy")
                txtPhy.Visible = True
                Dim lstLevel As DropDownList = LST.Parent.FindControl("lstLevel")
                lstLevel.Visible = False
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub UpdateStatus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SQLString As String = String.Empty
        Dim strID As New ArrayList
        Dim JobNumber As New ArrayList
        Dim CustJobID As New ArrayList
        Dim btn As Button = CType(sender, Button)
        Dim ddlOp As DropDownList = btn.Parent.FindControl("lstOptions")


        Dim DT As New DataTable
        DT.Columns.Add("ActNo", GetType(System.Int32))
        DT.Columns.Add("CurrentStatus", GetType(System.Int32))
        DT.Columns.Add("TranscriptionID", GetType(System.String))
        DT.Columns.Add("JobNo", GetType(System.Int32))
        For Each DR As GridViewRow In dlist.Rows
            Dim chk As CheckBox = DR.FindControl("chkJob")

            If Not chk Is Nothing Then
                If chk.Checked Then
                    Dim DRow As DataRow = DT.NewRow

                    Dim hdn As HiddenField = chk.Parent.FindControl("hdnTransID")
                    DRow("TranscriptionID") = hdn.Value.ToString

                    hdn = chk.Parent.FindControl("hdnAccNo")
                    DRow("ActNo") = hdn.Value

                    hdn = chk.Parent.FindControl("hdnStatus")
                    DRow("CurrentStatus") = hdn.Value

                    Dim hdnJobNo As HiddenField = chk.Parent.FindControl("hdnJobNo")
                    DRow("JobNo") = hdnJobNo.Value

                    DT.Rows.Add(DRow)
                    strID.Add(hdn.Value.ToString)
                End If
            End If
        Next
        Dim clsDic As New ETS.BL.Dictations

        Dim varStatus As String = String.Empty
        If ddlOp.SelectedValue = "1" Then
            Dim ddlSt As DropDownList = btn.Parent.FindControl("lstStatus")
            Dim pnl As Panel = btn.Parent.FindControl("iMessage")
            pnl.Visible = True

            Dim lbl As Label = btn.Parent.FindControl("lblMessage")
            lbl.Visible = True
            DT = clsDic.setDictationStatus(ddlSt.SelectedValue, Session("UserID"), Request.UserHostAddress(), DT)
            'lblMessage.Text = DT.Rows(0).Item("Result").ToString
            Label1.Text = "<b>Job Status updated : </b>"
        ElseIf ddlOp.SelectedValue = "2" Then
            Dim lstUser As TextBox = btn.Parent.FindControl("txtUser")
            Dim lstLevel As DropDownList = btn.Parent.FindControl("lstLevel")
            Dim UserLevel As Integer = CInt(lstLevel.SelectedValue)
            Dim UserID As String = String.Empty

            Dim clsusers As New ETS.BL.Users
            With clsusers
                .ContractorID = Session("contractorID")
                ._WhereString.Append(" AND Firstname + ' ' + Lastname + '(' + UserName + ')'='" & lstUser.Text & "'")
                UserID = .getUserID
                lstUser.Text = UserID
            End With
            clsusers = Nothing

            DT = clsDic.AssignDictations(UserID, UserLevel + 100, Session("UserID"), False, Request.UserHostAddress(), DT)
            'txtUser.Text = DT.Rows(2).Item("Result").ToString
            Label1.Text = "<b>Job Assigned to users : </b>"
        ElseIf ddlOp.SelectedValue = "3" Then
            Dim txtTAT As TextBox = btn.Parent.FindControl("txtTAT")
            DT = clsDic.setDictationTAT(txtTAT.Text, DT)
            Label1.Text = "<b>Job TAT updated : </b>"
        ElseIf ddlOp.SelectedValue = "4" Then
            DT = clsDic.setDictationSamples(Session("UserID"), DT)
            'lblMessage.Text = DT.Rows(0).Item("Result").ToString
            Label1.Text = "<b>Job Set for samples : </b>"
        ElseIf ddlOp.SelectedValue = "5" Then
            Dim txt As TextBox = btn.Parent.FindControl("txtPhy")
            Dim DSPhy As New DataSet
            Dim clsPhy As New ETS.BL.Physicians
            With clsPhy
                DSPhy = .getPhywithActDetails(Session("contractorID"), Session("WorkgroupID"))
            End With
            clsPhy = Nothing
            Dim DR() As DataRow = DSPhy.Tables(0).Select("FirstName+' '+LastName+'('+AccountNo+')'='" & txt.Text & "'")
            DSPhy.Dispose()
            DT = clsDic.setDictationDictator(DR(0).Item("PhysicianID").ToString, DR(0).Item("AccountNo").ToString, DT)
            'txt.Text = DT.Rows(0).Item("Result").ToString

            Label1.Text = "<b>Physicians changes for Jobs : </b>"
        End If
        Dim varForloopCount As Integer = 0
        If DT.Rows.Count > 0 Then

            Dim varTemp As String = String.Empty
            varTemp = "<table><tr><td class=alt1>Job#</td><td class=alt1>Updated</td></tr>"
            For Each DR1 As Data.DataRow In DT.Rows
                If Trim(UCase(DR1("Result").ToString)) = Trim(UCase("True")) Then
                    varTemp = varTemp & "<tr><td>" & DR1("JobNo").ToString & "</td><td align=""center""><img src=""../App_Themes/Images/check.png"" alt=""Record updated""></td></tr>"
                Else
                    varTemp = varTemp & "<tr><td>" & DR1("JobNo").ToString & "</td><td align=""center""><img src=""../App_Themes/Images/close.png"" alt=""Record not updated""></td></tr>"
                End If
                varForloopCount = varForloopCount + 1
            Next
            If varForloopCount > 0 Then
                Panel1.Visible = False
                'DivMain.Visible = False
                'LinkButton1.Visible = False
                'dlist.Visible = False
                varTemp = varTemp & "</table><BR><a href=""#"" style=""cursor:pointer"" onclick=""javascript:window.location.reload();"">Reload the Page</a>"
                Label2.Text = varTemp.ToString

                dlist.DataSource = Nothing     '.Tables(0).DefaultView
                dlist.DataBind()
            End If
        End If
        DT.Dispose()
        clsDic = Nothing
    End Sub
End Class
