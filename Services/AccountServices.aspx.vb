Partial Class AccountServices
    Inherits BasePage
    Public uName As String
    Protected Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGO.Click
        If Not String.IsNullOrEmpty(DDLAccounts.SelectedItem.Text) Then
            DoBind()
        End If
    End Sub
    Private Function DoBind()
        hdnAccID.Value = DDLAccounts.SelectedItem.Value
        uName = "Service Assignments for: " & DDLAccounts.SelectedItem.Text
        MultiView1.ActiveViewIndex = 1
        Dim clsSer As ETS.BL.Services
        Dim Ds As New Data.DataSet
        Try
            clsSer = New ETS.BL.Services
            Ds = clsSer.GetServiceLstByAcc(DDLAccounts.SelectedItem.Value)
            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    rptLevels.DataSource = Ds
                    rptLevels.DataBind()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsSer = Nothing
            Ds = Nothing
        End Try
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DDLAccounts.Focus()
        DDLAccounts.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('btnGO').click();return false;}} else {return true}; ")
        'TRFile.Visible = False
        If Not IsPostBack Then
            MultiView1.ActiveViewIndex = 0
            Dim clsAcc As ETS.BL.Accounts
            Dim Ds As New Data.DataSet
            Try
                clsAcc = New ETS.BL.Accounts
                Ds = clsAcc.getAccountList(Session("WorkgroupID"), Session("ContractorID"), String.Empty)
                If Ds.Tables.Count > 0 Then
                    If Ds.Tables(0).Rows.Count > 0 Then
                        DDLAccounts.DataSource = Ds
                        DDLAccounts.DataValueField = "AccountID"
                        DDLAccounts.DataTextField = "AccountName"
                        DDLAccounts.DataBind()
                    End If
                End If
                DDLAccounts.Items.Insert(0, New ListItem("", String.Empty))
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsAcc = Nothing
                Ds = Nothing
            End Try
        End If
        If IsPostBack Then
            If Not String.IsNullOrEmpty(Request("btnSave")) Then
                Try
                    Dim DT As New Data.DataTable
                    DT.Columns.Add("ServiceID", GetType(System.String))
                    DT.Columns.Add("IsDefault", GetType(System.Boolean))


                    For Each rptitem As RepeaterItem In rptLevels.Items
                        Dim chk As CheckBox = DirectCast(rptitem.FindControl("ckSelected"), CheckBox)
                        If chk.Checked Then
                            Dim hdn As HiddenField = chk.Parent.FindControl("ID")
                            Dim ID As String = hdn.Value
                            Dim rdo As RadioButton = chk.Parent.FindControl("rdoDefault")
                            Dim IsDefault As Boolean = rdo.Checked

                            Dim DRow As Data.DataRow = DT.NewRow
                            DRow("ServiceID") = ID
                            DRow("IsDefault") = IsDefault

                            DT.Rows.Add(DRow)
                        End If
                    Next

                    Dim clsSer As ETS.BL.Services
                    Try
                        clsSer = New ETS.BL.Services
                        If clsSer.btn_AssignedServicesTo_Acc_Click(DDLAccounts.SelectedItem.Value, DT) Then
                            MsgBox1.alert("Changes have been saved successfully!")
                        Else
                            MsgBox1.alert("Transaction Failed!")
                        End If
                    Catch ex As Exception
                    Finally
                        clsSer = Nothing
                    End Try

                Catch ex As Exception
                    MsgBox1.alert("Transaction Failed!")
                    Response.Write(ex.Message)
                Finally
                    DoBind()
                End Try
            End If
        End If
    End Sub
   
    Protected Sub btnBack_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        MultiView1.ActiveViewIndex = 0
    End Sub

    Protected Sub rptLevels_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptLevels.ItemDataBound
        If e.Item.ItemType <> ListItemType.Item AndAlso e.Item.ItemType <> ListItemType.AlternatingItem Then
            Exit Sub
        End If

        Dim rdo As RadioButton = DirectCast(e.Item.FindControl("rdoDefault"), RadioButton)
        Dim script As String = "SetUniqueRadioButton('rptLevels.*Default',this)"
        rdo.Attributes.Add("onclick", script)

    End Sub
End Class

