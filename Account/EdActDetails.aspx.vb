Imports System
Imports System.Data
Partial Class Account_EdActDetails
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            getAccounts(True, CInt(DLInstance.SelectedValue))
        End If
    End Sub
    Private Sub getAccounts(ByVal Active As Boolean, ByVal InstanceID As Integer)
        MyDataGrid.DataSource = Nothing
        MyDataGrid.DataBind()

        Dim clsAcc As New ETS.BL.Accounts
        Dim DSAcc As New DataSet
        With clsAcc

            If Active Then
                DSAcc = .getAccountList(Session("WorkGroupID"), Session("ContractorID"), " AND InstanceID=" & InstanceID & " ")
            Else
                DSAcc = .getInactiveAccountList(Session("WorkGroupID"), Session("ContractorID"), " AND InstanceID=" & InstanceID & " ")
            End If

            If DSAcc.Tables.Count > 0 Then
                If DSAcc.Tables(0).Rows.Count > 0 Then


                    MyDataGrid.DataSource = DSAcc
                    MyDataGrid.DataBind()
                End If
            End If
        End With
        clsAcc = Nothing
        DSAcc = Nothing
    End Sub
    Protected Sub DLActive_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLActive.SelectedIndexChanged
        If DLActive.SelectedValue = "Active" Then
            getAccounts(True, CInt(DLInstance.SelectedValue))
        Else
            getAccounts(False, CInt(DLInstance.SelectedValue))
        End If
    End Sub

    Protected Sub DLInstance_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLInstance.SelectedIndexChanged
        If DLActive.SelectedValue = "Active" Then
            getAccounts(True, CInt(DLInstance.SelectedValue))
        Else
            getAccounts(False, CInt(DLInstance.SelectedValue))
        End If
    End Sub
End Class
