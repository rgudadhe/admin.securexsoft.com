
Partial Class CreateUser_OtherInfo
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            BindUsers()
        End If
    End Sub

    Protected Sub GetUsrData()
        Dim varUsrID As String = String.Empty
        varUsrID = ddlUsrs.Items(ddlUsrs.SelectedIndex).Value.ToString
        If Not String.IsNullOrEmpty(varUsrID) Then
            Dim clsUPro As ETS.BL.Users_ProfessionalInfo
            Dim clsUPer As ETS.BL.Users_PersonalAndAcademicsInfo
            Dim clsUSup As ETS.BL.Users_SupportiveInfo
            Try
                clsUPro = New ETS.BL.Users_ProfessionalInfo
                clsUPro.UserID = varUsrID.ToString
                clsUPro.getUsers_ProfessionalInfoDetails()
                With clsUPro
                    txtDtCon.Text = .ConfirmDt
                    txtConfirmStatus.Text = .ConfirmStatus.ToString
                    txtSSalary.Text = .StartSalary
                    txtICount.Text = .IncrementsEnjoyed
                    txtIncrementsDesc.Text = .IncrementsDesc.ToString
                    txtCSalary.Text = .CurrentSalary
                    txtInsStatus.Text = .InsuranceStatus.ToString
                    txtInsCode.Text = .InsuranceCode
                    txtInsDetails.Text = .FamilyInsuranceDetails.ToString
                    txtPF.Text = .PFNumber
                    txtPAN.Text = .PANNumber
                    txtPassport.Text = .PassportNumber
                    txtBankAccNo.Text = .BankAccNumber
                    txtBankName.Text = .BankName.ToString
                    txtIFSC.Text = .BranchIFSCCode
                    txtInductionStatus.Text = .InductionStatus.ToString
                    txtInductionBy.Text = .InductionBy.ToString
                    txtInductionOn.Text = .InductionOn.ToString
                    If Not String.IsNullOrEmpty(.DocVerifyStatus.ToString) Then
                        If .DocVerifyStatus Then
                            ddlVerify.SelectedIndex = 1
                        Else
                            ddlVerify.SelectedIndex = 0
                        End If
                    End If
                    txtDocVerify.Text = .DocVerifyBy.ToString
                    If Not String.IsNullOrEmpty(.AccessCard.ToString) Then
                        If .AccessCard Then
                            ddlAccessCard.SelectedIndex = 1
                        Else
                            ddlAccessCard.SelectedIndex = 0
                        End If
                    End If

                    If Not String.IsNullOrEmpty(.EmpCard.ToString) Then
                        If .EmpCard Then
                            ddlEmpICard.SelectedIndex = 1
                        Else
                            ddlEmpICard.SelectedIndex = 0
                        End If
                    End If

                    txtEmploymentStatus.Text = .EmploymentStatus.ToString
                End With

                clsUPer = New ETS.BL.Users_PersonalAndAcademicsInfo
                clsUPer.UserID = varUsrID.ToString
                clsUPer.getUsers_PersonalAndAcademicsInfoDetails()
                With clsUPer
                    txtHEdu.Text = .HighestEdu.ToString
                    txtSpec.Text = .Specialization.ToString
                    txtBGroup.Text = .BloodGroup.ToString
                    If Not String.IsNullOrEmpty(.MaritalStatus) Then
                        If Trim(UCase(.MaritalStatus.ToString)) = Trim(UCase("Single")) Then
                            ddlMaritalStatus.SelectedIndex = 0
                        ElseIf Trim(UCase(.MaritalStatus.ToString)) = Trim(UCase("Married")) Then
                            ddlMaritalStatus.SelectedIndex = 1
                        Else
                            ddlMaritalStatus.SelectedIndex = 0
                        End If
                    End If
                    txtHobbies.Text = .Hobbies.ToString
                End With

                clsUSup = New ETS.BL.Users_SupportiveInfo
                clsUSup.UserId = varUsrID.ToString
                clsUSup.getUsers_SupportiveInfoDetails()
                With clsUSup
                    txtResReqNo.Text = .ResReqNumber.ToString
                    txtResIdeDt.Text = .ResIdentificationDt.ToString
                    txtResJoinDt.Text = .ResJoiningDate.ToString
                    txtSrcOfRec.Text = .RecruitmentSrc.ToString
                    txtRefBy.Text = .RefferedBy.ToString
                    txtSpeAch.Text = .AchiInEdictate.ToString
                End With


            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsUPro = Nothing
                clsUPer = Nothing
                clsUSup = Nothing
            End Try
        End If
    End Sub
    Protected Sub BindUsers()
        Dim clsUsrs As ETS.BL.Users
        Dim Ds As New Data.DataSet

        Try
            clsUsrs = New ETS.BL.Users
            Ds = clsUsrs.getUsersList(Session("ContractorID").ToString, Session("WorkGroupID").ToString, String.Empty)
            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    Ds.Tables(0).Columns.Add("EName", GetType(System.String), "FirstName + ' '+ LastName + ' (' + UserName + ')'")

                    ddlUsrs.DataSource = Ds
                    ddlUsrs.DataTextField = "EName"
                    ddlUsrs.DataValueField = "UserID"
                    ddlUsrs.DataBind()
                End If
            End If
            ddlUsrs.Items.Insert(0, New ListItem("Please Select", String.Empty))
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsrs = Nothing
            Ds = Nothing
        End Try
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If Validation() Then
            Dim varUsrId As String = String.Empty
            varUsrId = ddlUsrs.Items(ddlUsrs.SelectedIndex).Value.ToString
            If Not String.IsNullOrEmpty(varUsrId.ToString) Then
                Dim clsUPro As ETS.BL.Users_ProfessionalInfo
                Dim clsUPer As ETS.BL.Users_PersonalAndAcademicsInfo
                Dim clsUSup As ETS.BL.Users_SupportiveInfo
                Dim clsUsr As ETS.BL.Users

                Try
                    clsUPro = New ETS.BL.Users_ProfessionalInfo
                    With clsUPro
                        .UserID = varUsrId.ToString
                        .ConfirmDt = txtDtCon.Text.ToString

                        .ConfirmStatus = txtConfirmStatus.Text.ToString
                        .StartSalary = txtSSalary.Text.ToString
                        .IncrementsEnjoyed = txtICount.Text.ToString
                        .IncrementsDesc = txtIncrementsDesc.Text.ToString
                        .CurrentSalary = txtCSalary.Text.ToString
                        .InsuranceStatus = txtInsStatus.Text.ToString
                        .InsuranceCode = txtInsCode.Text.ToString
                        .FamilyInsuranceDetails = txtInsDetails.Text.ToString
                        .PFNumber = txtPF.Text.ToString
                        .PANNumber = txtPAN.Text.ToString
                        .PassportNumber = txtPassport.Text.ToString
                        .BankAccNumber = txtBankAccNo.Text
                        .BankName = txtBankName.Text.ToString
                        .BranchIFSCCode = txtIFSC.Text.ToString
                        .InductionStatus = txtInductionStatus.Text.ToString
                        .InductionBy = txtInductionBy.Text.ToString
                        .InductionOn = txtInductionOn.Text.ToString
                        .DocVerifyStatus = IIf(ddlVerify.SelectedIndex = 1, True, False)
                        .DocVerifyBy = txtDocVerify.Text.ToString
                        .AccessCard = IIf(ddlAccessCard.SelectedIndex = 1, True, False)
                        .EmpCard = IIf(ddlEmpICard.SelectedIndex = 1, True, False)
                        .EmploymentStatus = txtEmploymentStatus.Text.ToString
                        .InfoUpdatedBy = Session("UserID").ToString
                        .InfoUpdatedOn = Now
                    End With

                    clsUPer = New ETS.BL.Users_PersonalAndAcademicsInfo
                    With clsUPer
                        .UserID = varUsrId.ToString
                        .HighestEdu = txtHEdu.Text.ToString
                        .Specialization = txtSpec.Text.ToString
                        .BloodGroup = txtBGroup.Text
                        .MaritalStatus = ddlMaritalStatus.SelectedValue.ToString
                        .Hobbies = txtHobbies.Text.ToString
                        .InfoUpdatedBy = Session("UserID").ToString
                        .InfoUpdatedOn = Now
                    End With


                    clsUSup = New ETS.BL.Users_SupportiveInfo
                    With clsUSup
                        .UserId = varUsrId.ToString
                        .ResReqNumber = txtResReqNo.Text.ToString
                        .ResIdentificationDt = txtResIdeDt.Text.ToString
                        .ResJoiningDate = txtResJoinDt.Text.ToString
                        .RecruitmentSrc = txtSrcOfRec.Text.ToString
                        .RefferedBy = txtRefBy.Text.ToString
                        .AchiInEdictate = txtSpeAch.Text.ToString
                        .InfoUpdatedBy = Session("UserID").ToString
                        .InfoUpdatedOn = Now
                    End With

                    clsUsr = New ETS.BL.Users

                    'Response.Write(clsUsr.btnUpdate_Clicked_From_UserOtherInfo(clsUPro, clsUPer, clsUSup))
                    If clsUsr.btnUpdate_Clicked_From_UserOtherInfo(clsUPro, clsUPer, clsUSup) = True Then
                        lblMsg.Text = "Information updated...."
                    Else
                        lblMsg.Text = "Updation failed..."
                    End If

                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsUPro = Nothing
                    clsUPer = Nothing
                    clsUSup = Nothing
                    clsUsr = Nothing
                End Try
            End If
        End If
    End Sub
    Protected Function Validation() As Boolean
        lblMsg.Text = String.Empty
        If String.IsNullOrEmpty(ddlUsrs.SelectedValue.ToString) Then
            lblMsg.Text = "Please select user"
            ddlUsrs.Focus()
            Return False
        End If
        If String.IsNullOrEmpty(txtSSalary.Text.ToString) Then
            lblMsg.Text = "Please enter start salary"
            txtSSalary.Focus()
            Return False
        End If
        If String.IsNullOrEmpty(txtCSalary.Text.ToString) Then
            lblMsg.Text = "Please enter current salary"
            txtCSalary.Focus()
            Return False
        End If
        If String.IsNullOrEmpty(txtPF.Text.ToString) Then
            lblMsg.Text = "Please enter PF number"
            txtPF.Focus()
            Return False
        End If
        If String.IsNullOrEmpty(txtPAN.Text.ToString) Then
            lblMsg.Text = "Please enter PAN number"
            txtPAN.Focus()
            Return False
        End If
        If String.IsNullOrEmpty(txtBankAccNo.Text.ToString) Then
            lblMsg.Text = "Please enter Bank A/C number"
            txtBankAccNo.Focus()
            Return False
        End If
        If String.IsNullOrEmpty(txtBankName.Text.ToString) Then
            lblMsg.Text = "Please enter Bank name"
            txtBankName.Focus()
            Return False
        End If
        If String.IsNullOrEmpty(txtIFSC.Text.ToString) Then
            lblMsg.Text = "Please enter Branch IFSC Code"
            txtIFSC.Focus()
            Return False
        End If
        If Not String.IsNullOrEmpty(txtDtCon.Text.ToString) Then
            If IsDate(txtDtCon.Text.ToString) = False Then
                lblMsg.Text = "Invalid date...for confirmation date"
                txtDtCon.Focus()
                Return False
            End If
        End If

        If Not String.IsNullOrEmpty(txtInductionOn.Text.ToString) Then
            If IsDate(txtInductionOn.Text.ToString) = False Then
                lblMsg.Text = "Invalid date...for induction on date"
                txtInductionOn.Focus()
                Return False
            End If
        End If

        If Not String.IsNullOrEmpty(txtResIdeDt.Text.ToString) Then
            If IsDate(txtResIdeDt.Text.ToString) = False Then
                lblMsg.Text = "Invalid date...for Resource Identification Date"
                txtResIdeDt.Focus()
                Return False
            End If
        End If

        If Not String.IsNullOrEmpty(txtResJoinDt.Text.ToString) Then
            If IsDate(txtResJoinDt.Text.ToString) = False Then
                lblMsg.Text = "Invalid date...for Resource Joining Date"
                txtResJoinDt.Focus()
                Return False
            End If
        End If
        Return True
    End Function
    Protected Sub ClearData()
        txtDtCon.Text = String.Empty
        txtConfirmStatus.Text = String.Empty
        txtSSalary.Text = String.Empty
        txtICount.Text = String.Empty
        txtIncrementsDesc.Text = String.Empty
        txtCSalary.Text = String.Empty
        txtInsStatus.Text = String.Empty
        txtInsCode.Text = String.Empty
        txtInsDetails.Text = String.Empty
        txtPF.Text = String.Empty
        txtPAN.Text = String.Empty
        txtPassport.Text = String.Empty
        txtBankAccNo.Text = String.Empty
        txtBankName.Text = String.Empty
        txtIFSC.Text = String.Empty
        txtInductionStatus.Text = String.Empty
        txtInductionBy.Text = String.Empty
        txtInductionOn.Text = String.Empty
        txtEmploymentStatus.Text = String.Empty
        ddlAccessCard.SelectedIndex = 0
        ddlVerify.SelectedIndex = 0
        ddlEmpICard.SelectedIndex = 0

        txtHEdu.Text = String.Empty
        txtSpec.Text = String.Empty
        txtBGroup.Text = String.Empty
        ddlMaritalStatus.SelectedIndex = 0
        txtHobbies.Text = String.Empty

        txtResReqNo.Text = String.Empty
        txtResIdeDt.Text = String.Empty
        txtResJoinDt.Text = String.Empty
        txtSrcOfRec.Text = String.Empty
        txtRefBy.Text = String.Empty
        txtSpeAch.Text = String.Empty
    End Sub
    Protected Sub ddlUsrs_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUsrs.SelectedIndexChanged
        ClearData()
        If ddlUsrs.SelectedIndex > 0 Then
            GetUsrData()
        End If
    End Sub

    Protected Sub LnkExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkExport.Click
        Dim clsUsr As ETS.BL.Users
        Dim DS As New Data.DataSet
        Try
            clsUsr = New ETS.BL.Users
            DS = clsUsr.getUsersListForOtherInfoByWrkGroupID(Session("ContractorID").ToString, Session("WorkGroupID").ToString)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    GrdViewData.DataSource = DS
                    GrdViewData.DataBind()
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

        Response.Clear()
        Dim filename = "User Information Report " & Now & " .xls"
        Response.AddHeader("content-disposition", "attachment;filename=" & filename)
        Response.ContentType = "application/vnd.ms-excel"
        Response.Charset = ""
        Me.EnableViewState = False
        Dim tw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        GrdViewData.RenderControl(hw)
        Response.Write(tw.ToString())
        Response.End()
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
    End Sub
End Class
