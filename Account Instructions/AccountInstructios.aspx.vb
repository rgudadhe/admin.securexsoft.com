Imports System
Imports System.Data
Partial Class AccountInstructions
    Inherits BasePage

    Protected Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGO.Click
        If Not String.IsNullOrEmpty(DDLAccounts.SelectedItem.Text) Then
            DoBind()
        End If
    End Sub
    Private Function DoBind()
        hdnAccID.Value = DDLAccounts.SelectedItem.Value
        lblAccName.Text = "Account Instructions for: " & DDLAccounts.SelectedItem.Text
        MultiView1.ActiveViewIndex = 1

        Try

            Dim clsAI As New ETS.BL.AccountInstructions
            With clsAI
                .AccountID = DDLAccounts.SelectedItem.Value
                .getAIDetails()

                If Len(.UserID) = 36 Then
                    If Not IsDBNull(.Format) Then
                        Select Case .Format.ToString.ToLower
                            Case ".doc"
                                lblType.Text = "Word Document"
                            Case ".xls"
                                lblType.Text = "Excel  Worksheet"
                            Case ".ppt"
                                lblType.Text = "PowerPoint Presentation"
                        End Select
                    Else
                        lblType.Text = "-"
                    End If
                    lblDateMod.Text = IIf(String.IsNullOrEmpty(.DateModified), "-", .DateModified.ToString)
                    Dim URL As String = System.Configuration.ConfigurationManager.AppSettings("URL")
                    hdnReportPath.Value = EncryptText(URL & "/ETS_Files/Instructions/" & DDLAccounts.SelectedItem.Value & .Format.ToString, "webpath")
                    btndelete.Text = IIf(.IsDeleted = True, "Un-Delete", "Delete")
                    btndelete.ToolTip = "Click here to " & btndelete.Text & " Account Instructions"
                Else
                    TR.Visible = True
                    lblResponse.Visible = True
                    lblResponse.Text = "No Account Instructions found for " & DDLAccounts.SelectedItem.Text
                    lblType.Visible = False
                    btndelete.Text = "Delete"
                    btndelete.Enabled = False
                End If
            End With
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
    Function EncryptText(ByVal strText, ByVal strPwd)
        Dim i, c
        Dim strBuff
        If strPwd <> "" And strText <> "" Then
            strPwd = UCase(strPwd)
            If Len(strPwd) Then
                For i = 1 To Len(strText)
                    c = Asc(Mid(strText, i, 1))
                    c = c + Asc(Mid(strPwd, (i Mod Len(strPwd)) + 1, 1))
                    strBuff = strBuff & Chr(c And &HFF)
                Next
            Else
                strBuff = strText
            End If
            EncryptText = strBuff
        Else
            EncryptText = ""
        End If
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        hdnURL.Value = WebAddress
        TR.Visible = False
        lblResponse.Visible = False
        DDLAccounts.Focus()
        DDLAccounts.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('btnGO').click();return false;}} else {return true}; ")
        'TRFile.Visible = False
        If Not IsPostBack Then
            MultiView1.ActiveViewIndex = 0
            Dim DSAcc As New dataset
            Dim clsAcc As New ETS.BL.Accounts
            With clsAcc
                '.ContractorID = Session("ContractorID").ToString
                DSAcc = .getAccountList(Session("WorkGroupID").ToString, Session("ContractorID").ToString, String.Empty)
                DDLAccounts.DataSource = DSAcc
                DDLAccounts.DataValueField = "AccountID"
                DDLAccounts.DataTextField = "AccountName"
                DDLAccounts.DataBind()
                DSAcc.Dispose()
                Dim LI As New ListItem("", Guid.NewGuid().ToString)
                DDLAccounts.Items.Insert(0, LI)
                LI.Selected = True
                LI = Nothing
            End With
            clsAcc = Nothing
        Else
            If Not String.IsNullOrEmpty(DDLAccounts.SelectedItem.Text) Then
                DoBind()
            End If
        End If
        If Request.Form("hdnDelete") = "1" Then
            Request.Form("hdnDelete").Replace("1", "0")
            
            Try
               
                Dim TODO As Boolean
                If btndelete.Text = "Delete" Then
                    TODO = True
                Else
                    TODO = False
                End If
                Dim clsAI As New ETS.BL.AccountInstructions
                With clsAI
                    .AccountID = DDLAccounts.SelectedItem.Value
                    .IsDeleted = TODO
                    If .UpdateAIDetails() > 0 Then
                        MsgBox1.alert("Changes have been saved successfully!")
                        DoBind()
                    End If
                End With
                
            Catch ex As Exception
                Response.Write(ex.Message)
            
            End Try
        End If
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        MsgBox1.confirm("Are you sure to " & btn.Text & " Account Intructions for " & DDLAccounts.SelectedItem.Text & "?", "hdnDelete")
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        MultiView1.ActiveViewIndex = 0
    End Sub



End Class

