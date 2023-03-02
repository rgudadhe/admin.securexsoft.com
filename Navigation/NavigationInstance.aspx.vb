
Partial Class Navigation_NavigationInstance
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim varUID As String = String.Empty
        varUID = Request.Form("hdnUserID").ToString

        If Not String.IsNullOrEmpty(varUID.ToString) Then
            CheckUser(varUID.ToString)
        Else
            lblMessage.Text = "Login failed"
        End If
    End Sub
    Protected Sub CheckUser(ByVal UserID As String)
        If Not String.IsNullOrEmpty(UserID.ToString) Then
            Dim clsUsr As ETS.BL.Users
            Try
                clsUsr = New ETS.BL.Users(UserID.ToString)

                Session("UserID") = clsUsr.UserID.ToString
                Session("clsUserInfo") = clsUsr
                With clsUsr
                    Session("UserName") = .FirstName & " " & .LastName
                    Session("ContractorID") = .ContractorID
                    Session("AdminLevel") = .AdminLevel
                    Session("OwnerID") = .OwnerID
                    Session("LoginID") = .UserName
                    Session("DeptID") = .DepartmentID
                    Session("ParentID") = IIf(String.IsNullOrEmpty(.ParentID), .ContractorID, .ParentID)
                    Session("IsOwner") = .IsOwner
                    Session("WorkGroupID") = .WorkGroupID
                    If .IsOwner Then
                        Session("OwnerID") = .UserID
                    End If
                    If String.IsNullOrEmpty(.ParentID) Then
                        Session("IsContractor") = 1
                    Else
                        Session("IsContractor") = 0
                    End If
                End With
                Response.Redirect("../main.htm")
                'If clsUsr.UpdateUserDetails() = 1 Then
                '    Response.Redirect("main.htm")
                'Else
                '    lblMessage.Text = "Login Failed"
                'End If

            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsUsr = Nothing
            End Try
        End If
    End Sub
End Class
