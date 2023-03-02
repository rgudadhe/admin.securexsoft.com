Imports System
Imports System.Data
Namespace ets
    Partial Class _Default
        Inherits BasePage
        Public IsSend As Boolean
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Response.Write(ConfigurationManager.AppSettings("InstanceCount").ToString)
            If Session("IsOwner") = "False" Then
                chkIsSub.Enabled = False
                lblConName.Text = "Sub-Contractor Name"
                'lblHeading.Text = "Add New Sub-Contractor"
            End If

            If Not IsPostBack Then
                Dim clsCon As New ets.BL.Contractor
                Dim DT As DataTable = clsCon.CreateContractor_Bind(IIf(Session("IsOwner") = "False", False, True), Session("ContractorID"))
                cmbContractors.DataSource = DT
                cmbContractors.DataTextField = "ContractorName"
                cmbContractors.DataValueField = "ContractorID"
                cmbContractors.DataBind()
                cmbContractors.Items.Insert(0, New ListItem("Please Select", String.Empty))
                cmbContractors.Items(0).Selected = True
                lblContractors.Enabled = False
                cmbContractors.Enabled = False
                If Session("IsOwner") = "True" Then
                    pnlSub.Visible = True
                End If
            End If

        End Sub
        Protected Sub chkIsSub_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            If chkIsSub.Checked = True Then
                lblConName.Text = "Sub-Contractor Name"
                'lblHeading.Text = "Add New Sub-Contractor"
                lblContractors.Enabled = True
                cmbContractors.Enabled = True
            Else
                lblConName.Text = "Contractor Name"
                'lblHeading.Text = "Add New Contractor"
                lblContractors.Enabled = False
                cmbContractors.Enabled = False
            End If
        End Sub

        Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
           
            Dim isSubCon As Boolean
            Dim strContractor As String

            If chkIsSub.Checked = True Then
                isSubCon = True
            Else
                isSubCon = False
            End If
            If Session("IsOwner") = "False" Then
                strContractor = Session("ContractorID")
            Else
                strContractor = Request("cmbContractors")
            End If

            Dim clsCon As New ets.BL.Contractor
            Dim NewConID As String = System.Guid.NewGuid.ToString
            With clsCon
                .ContractorID = NewConID
                If isSubCon Or Session("IsOwner") = "False" Then
                    .ParentID = strContractor
                End If
                .OwnerID = Session("OwnerID").ToString
                .ContractorName = Request("txtConName")
                .Description = Request("txtConDetails")
                .CreateDate = Now
                .InstanceID = DLInstance.SelectedIndex
            End With
            Dim RetVal As Integer = clsCon.CreateContractor_Submit()
            If RetVal = 1 Then
                Dim strMessage As String = Request("txtConName") & "  added Successfully"
                Response.Redirect("addContractor.aspx?ConID=" & NewConID & "&Message=" & strMessage & "&isSubCon=" & isSubCon)
            ElseIf RetVal = 0 Then
                iResponse.Text = Request("txtConName") & " already exist"
            Else
                iResponse.Text = "Adding Contractor Failed"
            End If
           
        End Sub
    End Class
End Namespace