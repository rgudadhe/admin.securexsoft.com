
Partial Class Account_ViewAccAssignmentToUsr
    Inherits BasePage
    Protected Sub BindUsrs()
        Dim clsUsers As ETS.BL.Users
        Dim DSUsers As New Data.DataSet
        Dim DV As Data.DataView
        Try
            clsUsers = New ETS.BL.Users
            With clsUsers
                .ContractorID = Session("ContractorID")
                DSUsers = .getUsersList()
                If DSUsers.Tables.Count > 0 Then
                    If DSUsers.Tables(0).Rows.Count > 0 Then
                        DSUsers.Tables(0).Columns.Add("UName", GetType(System.String), "Firstname + ' ' + Lastname + ' ('+ UserName +')'")
                        DV = New Data.DataView(DSUsers.Tables(0), String.Empty, "FirstName,LastNAme", Data.DataViewRowState.CurrentRows)
                        If DV.Count > 0 Then
                            ddlUsrs.DataSource = DV
                            ddlUsrs.DataTextField = "UName"
                            ddlUsrs.DataValueField = "userid"
                            ddlUsrs.DataBind()
                        End If
                    End If
                End If
                ddlUsrs.Items.Insert(0, New ListItem("Please Select", String.Empty))
            End With
        Catch ex As Exception
        Finally
            clsUsers = Nothing
            DSUsers.Dispose()
            DV = Nothing
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblMsg.Text = String.Empty
            BindUsrs()
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMsg.Text = String.Empty
        Dim varUsrID As String = String.Empty
        varUsrID = ddlUsrs.Items(ddlUsrs.SelectedIndex).Value.ToString
        If String.IsNullOrEmpty(varUsrID) Then
            lblMsg.Text = "Please Select User"
            Exit Sub
        Else
            BindViewData(varUsrID)
        End If
    End Sub
    Protected Sub BindViewData(ByVal varUsrID As String)
        Dim clsAcc As ETS.BL.Accounts
        Dim DS As New Data.DataSet
        Try
            clsAcc = New ETS.BL.Accounts
            DS = clsAcc.GetAccAssignedToUsr(Session("ContractorID").ToString, varUsrID)
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    GridView1.DataSource = DS
                    GridView1.DataBind()

                    If GridView1.Rows.Count > 0 Then
                        GridView1.ShowFooter = True
                        GridView1.UseAccessibleHeader = True
                        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader
                        GridView1.FooterRow.TableSection = TableRowSection.TableFooter
                    End If
                    lblMsg.Text = String.Empty
                Else
                    lblMsg.Text = String.Empty
                    lblMsg.Text = "No Records found"
                    GridView1.DataSource = Nothing
                    GridView1.DataBind()
                    Exit Sub
                End If
            Else
                lblMsg.Text = String.Empty
                lblMsg.Text = "No Records found"
                GridView1.DataSource = Nothing
                GridView1.DataBind()
                Exit Sub
            End If
        Catch ex As Exception
        Finally
            clsAcc = Nothing
            DS.Dispose()
        End Try
    End Sub
End Class
