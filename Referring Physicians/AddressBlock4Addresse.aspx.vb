Imports System
Imports System.Data
Partial Class AddressBlock4Addresse
    Inherits BasePage



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim DSAct As DataSet
            Dim clsAct As New ETS.BL.Accounts
            With clsAct
                '.ContractorID = Session("ContractorID").ToString
                '._WhereString.Append(" and (IsDeleted is null or IsDeleted=0)")
                DSAct = .getAccountList(Session("WorkGroupID"), Session("ContractorID"), String.Empty)
            End With
            clsAct = Nothing
            DDLAccounts.DataSource = DSAct
            DDLAccounts.DataTextField = "AccountName"
            DDLAccounts.DataValueField = "AccountID"
            DDLAccounts.DataBind()
            DSAct.Dispose()
            Dim LI As New ListItem("", Guid.NewGuid.ToString)
            DDLAccounts.Items.Insert(0, LI)
            LI = Nothing

        End If
    End Sub

    Protected Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGO.Click
        DoBind()
        Pan1.Visible = True
        lblResponse.Visible = False
        lblHeader.Text = "Address Block settings for: " & DDLAccounts.SelectedItem.Text
    End Sub
    Private Function DoBind() As Boolean
        Dim clsAB As New ETS.BL.AddressBlock4ADC
        With clsAB
            .AccID = DDLAccounts.SelectedValue.ToString
            .getABDetails()
            If Len(.AccID) = 36 Then
                chkFName.Checked = .PhyName
                chkMName.Checked = .PhymName
                chkLName.Checked = .PhylName
                chkdgree.Checked = .PhyDegree
                chkAdd.Checked = .Address
                chkCity.Checked = .PhyCity
                chkState.Checked = .PhyState
                chkZip.Checked = .PhyCode
                chkPhone.Checked = .PhoneNO
                chkFax.Checked = .FaxNO
            Else
                chkFName.Checked = False
                chkMName.Checked = False
                chkLName.Checked = False
                chkdgree.Checked = False
                chkAdd.Checked = False
                chkCity.Checked = False
                chkState.Checked = False
                chkZip.Checked = False
                chkPhone.Checked = False
                chkFax.Checked = False
            End If
        End With
        clsAB = Nothing
    End Function


    Protected Sub btnSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSet.Click
        Dim clsAB As New ETS.BL.AddressBlock4ADC
        With clsAB
            .PhyName = chkFName.Checked
            .PhymName = chkMName.Checked
            .PhylName = chkLName.Checked
            .PhyDegree = chkdgree.Checked
            .Address = chkAdd.Checked
            .PhyCity = chkCity.Checked
            .PhyState = chkState.Checked
            .PhyCode = chkZip.Checked
            .PhoneNO = chkPhone.Checked
            .FaxNO = chkFax.Checked
        End With

        If clsAB.setAccountsAB(DDLAccounts.SelectedValue.ToString) Then
            lblResponse.Text = "Address Block has been updated successfully"
            lblResponse.Visible = True
        Else
            lblResponse.Text = "Updating Address Block failed"
            lblResponse.Visible = True
        End If
        clsAB = Nothing
    End Sub

    Protected Sub DDLAccounts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLAccounts.SelectedIndexChanged
        Try
            Pan1.Visible = False
            lblResponse.Visible = False
            lblHeader.Text = ""
        Catch ex As Exception
            lblResponse.Text = ex.Message.ToString
            lblResponse.Visible = False
        End Try

    End Sub
End Class
