Imports System
Imports System.Data
Imports SASMTPLib
Partial Class Department_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Response.Write(Session("WorkgroupId").ToString & "<BR>" & Session("ContractorID").ToString)
        If Not IsPostBack Then
            Dim DSAct As New DataSet
            Dim clsAct As New ETS.BL.Accounts
            With clsAct
                '.ContractorID = Session("ContractorID").ToString
                '._WhereString.Append(" AND (IsDeleted is null or IsDeleted=0)")
                DSAct = .getAccountList(Session("WorkgroupID"), Session("ContractorID"), String.Empty)
                ActID.DataSource = DSAct
                ActID.DataTextField = "AccountName"
                ActID.DataValueField = "AccountID"
                ActID.DataBind()
                DSAct.Dispose()
                Dim LI As New ListItem
                LI.Value = ""
                LI.Text = "Please Select"
                ActID.Items.Insert(0, LI)
            End With
            clsAct = Nothing
            DSAct = Nothing

        End If
    End Sub






    Protected Function PlanText(ByVal html As String) As String
        Return html.Replace("<", "&lt;").Replace(">", "&gt;").ToString
    End Function


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        
        'Dim text As String
        'text = txtExSignedName.Text.ToString
        'Response.Write(text.Replace("\n", "<br/>") & "<BR>")


        'Response.Write(txtExSignedName.Text.ToString)
        'Response.Write(txtExSignedName.Text)



        'Response.End()

        Try
            Dim RecFound As String
            Dim PINNo As String
            PINNo = ""

            '        PINNo = "103001"
            RecFound = "No"
            Dim ActName As String = String.Empty
            Dim AccNo As String = String.Empty
            AccNo = ""
            Dim DSPhy As New DataSet
            Dim clsPhy As New ETS.BL.Physicians
            With clsPhy
                DSPhy = .getPhywithActDetails(Session("ContractorID").ToString, Session("WorkGroupID").ToString, ActID.SelectedValue)
            End With
            Dim NoOfPhy As Integer = 0
            If DSPhy.Tables.Count > 0 Then
                If DSPhy.Tables(0).Rows.Count > 0 Then
                    'ActName = DSPhy.Tables(0).Rows(0).Item("accountname")
                    'AccNo = DSPhy.Tables(0).Rows(0).Item("AccountNo")
                    NoOfPhy = DSPhy.Tables(0).Compute("Count(PINNO)", "")
                End If
            End If



            Dim clsAcc As ETS.BL.Accounts
            Try
                clsAcc = New ETS.BL.Accounts
                clsAcc.AccountID = ActID.SelectedValue.ToString
                clsAcc.getAccountDetails()
                If Not String.IsNullOrEmpty(clsAcc.AccountName.ToString) Then
                    ActName = clsAcc.AccountName.ToString
                End If
                If Not String.IsNullOrEmpty(clsAcc.AccountNo.ToString) Then
                    AccNo = clsAcc.AccountNo.ToString
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsAcc = Nothing
            End Try

            NoOfPhy = NoOfPhy + 1
            If NoOfPhy < 10 Then
                PINNo = "00" & NoOfPhy
            ElseIf NoOfPhy < 100 Then
                PINNo = "0" & NoOfPhy
            Else
                PINNo = NoOfPhy
            End If

            PINNo = AccNo & PINNo


            If Not String.IsNullOrEmpty(ActName.ToString) And Not String.IsNullOrEmpty(AccNo.ToString) Then
                With clsPhy
                    .PhysicianID = Guid.NewGuid().ToString
                    .FirstName = TxtFirstName.Text
                    .MiddleName = TxtMiddleName.Text
                    .LastName = TxtLastName.Text
                    .SignedName = Replace(TxtSignedName.Text, "'", "''")
                    .Speciality = TxtSpeciality.Text
                    .Email = TxtEmail.Text
                    .PhoneNo = TxtPhoneno.Text
                    .Fax = txtFax.Text
                    .PINNo = PINNo
                    .AccountID = ActID.SelectedValue
                    .ProviderID = TxProvID.Text
                    .sendfax = chkFax.Checked
                    If Not String.IsNullOrEmpty(txtExSignedName.Text.ToString) Then
                        .ExSignedName = Replace(txtExSignedName.Text.ToString, "'", "''")
                    End If
                    .FaxPlus = DDLAutoFax.SelectedValue
                    .CreateDate = Now
                    If .InsertPhysicianDetails() = 1 Then

                        Dim varBody As String = "<font face='Trebuchet MS' size='2' color='#000080'>Dear Team</font><p><font color='#000080'><font face='Trebuchet MS' size='2'>New dictator has been setup on SecureXSoft.</font><br><font face='Trebuchet MS' size='2'>Dictator Name : " & TxtFirstName.Text & " " & TxtLastName.Text & "</font><br><font face='Trebuchet MS' size='2'>Account Name : " & ActName & "</font><br><font face='Trebuchet MS' size='2'>Pin number : " & PINNo & "</font><br><font face='Trebuchet MS' size='2'>Signed Name : " & Replace(TxtSignedName.Text, "'", "''") & "</font><br><br><br></font></p><p><font color='#000080'><font face='Trebuchet MS' size='2'>Thanks,</font><br><font face='Trebuchet MS' size='2'>E-Dictate Support Team</font><br>"
                        Dim varSubject As String = "SecureXSoft - New Dictator : " & TxtFirstName.Text & " " & TxtLastName.Text & "( Account Name : " & ActName & ")"
                        Dim obj As MainModule
                        Try
                            obj = New MainModule
                            'If obj.NewAccPhyCreationMail("techsupport@edictate.com", "support@medofficepro.com", "caloysius@edictate.com,billing@medofficepro.com,mtsupport@edictate.com,sanjaykr@edictate.com,suman@edictate.com,kalpana@edictate.com", varSubject, varBody) = True Then
                            MsgDisp.Text = "The dictator has been added successfully."
                            'End If
                        Catch ex As Exception
        Finally
            obj = Nothing
        End Try
                    Else
                        MsgDisp.Text = "Failed adding dictator."
                    End If
                End With
            End If

            clsPhy = Nothing


            TxtFirstName.Text = ""
            TxtMiddleName.Text = ""
            TxtLastName.Text = ""

            TxtSignedName.Text = ""
            TxtSpeciality.Text = ""

            TxtEmail.Text = ""
            TxtPhoneno.Text = ""
            txtFax.Text = ""

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
        

    End Sub


End Class
