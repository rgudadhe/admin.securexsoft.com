Imports System.Data
Imports System.Data.SqlClient

Partial Class EditCustomAttributes
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ' Response.Write("loaded")

            'Response.Write(Session("ContractorID"))
            If Not IsPostBack Then
                reBind()
            Else

            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
    Private Sub reBind()

        'Dim clsAtt As New ETS.BL.Attributes
        'Dim DSAtt As Data.DataSet = clsAtt.getEditAttributes(Session("ContractorID"))
        'clsAtt = Nothing
        'rptCon.DataSource = DSAtt
        'rptCon.DataBind()
        'DSAtt.Dispose()
        Dim strConn As String

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DT1n As New DataTable
        Dim cmdIns1n As New SqlCommand("select [Attributeid],[Name],[Caption],[ControlType],[ControlSize],IsNULL(Deleted,0) AS Deleted from [ADMINETS].[dbo].[tblCustomAttributes] ", New SqlConnection(strConn))
        cmdIns1n.Connection.Open()
        Dim DRRec1n As SqlDataReader = cmdIns1n.ExecuteReader()
        DT1n.Load(DRRec1n)
        DRRec1n.Close()
        If cmdIns1n.Connection.State = ConnectionState.Open Then
            cmdIns1n.Connection.Close()
        End If
        rptCon.DataSource = DT1n
        rptCon.DataBind()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button
        Dim hdn As HiddenField
        Dim txt As TextBox
        Dim chk As CheckBox
        Dim AttribID, AttribName, AttribCap As String
        Dim AttribType As Integer
        Dim isDeleted As Boolean
        btn = CType(sender, Button)
        btn.Enabled = False
        hdn = btn.Parent.FindControl("hdnAttribID")
        AttribID = hdn.Value
        hdn = btn.Parent.FindControl("hdnTypeNo")
        AttribType = hdn.Value
        txt = btn.Parent.FindControl("txtAttribName")
        AttribName = txt.Text
        txt = btn.Parent.FindControl("txtCaption")
        AttribCap = txt.Text
        chk = btn.Parent.FindControl("chkDelete")
        If chk.Checked = True Then
            isDeleted = True
        Else
            isDeleted = False
        End If

        If AttribID <> "" Then
            Dim ClsAtt As New ETS.BL.Attributes
            With ClsAtt
                .AttributeID = AttribID
                If isDeleted Then
                    If .DeleteAttribute Then
                        reBind()
                    End If
                Else
                    .Name = AttribName
                    .Caption = AttribCap
                    .Type = AttribType

                    'Dim DLAT As New DALAuditTrail.DALAuditLog

                    'Dim res = DLAT.InsertAuditRecord(Session("LoginID"), "Edit Attribute", "Admin Portal - Attribute " & AttribName & " updated successfully", "Attribute", AttribName, Session("ipaddress"))

                    If .UpdateAttributeDetails Then
                        reBind()
                    End If
                End If
            End With
        End If
    End Sub
    Protected Sub DDType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddList As DropDownList = CType(sender, DropDownList)
        If ddList.SelectedValue <> "" Then
            Dim lbl As Label = ddList.Parent.FindControl("lblType")
            lbl.Text = ddList.SelectedItem.Text
            lbl.Visible = True
            Dim hdn As HiddenField = ddList.Parent.FindControl("hdnTypeNo")
            hdn.Value = ddList.SelectedItem.Value
            Dim btn As Button = ddList.FindControl("Button1")
            btn.Enabled = True
            ddList.SelectedIndex = 0
            ddList.Visible = False
            btn = ddList.FindControl("iPopUpOp")
            If ddList.SelectedItem.Value = "5" Then
                btn.Visible = True
            Else
                btn.Visible = False
            End If
        End If
    End Sub
    Protected Sub iPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn As Button = CType(sender, Button)
        Dim ddlist As DropDownList = btn.Parent.FindControl("DDType")
        Dim lbl As Label = btn.Parent.FindControl("lblType")
        If Not ddlist.Visible Then
            ddlist.Visible = True
            lbl.Visible = False
            btn.ToolTip = "Click to reset"
        Else
            ddlist.Visible = False
            lbl.Visible = True
            btn.ToolTip = "Click here to change Data Type"
        End If
    End Sub
    Protected Sub iPopUpOp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

            Dim btn As Button = CType(sender, Button)
            Dim hdn As HiddenField = btn.Parent.FindControl("hdnAttribID")
            Dim HidCaption As HiddenField = btn.Parent.FindControl("HidCaption")
            ' Response.Write(hdn.Value)
            Dim url As String = "UpdateCustomOptions.aspx?AttributeID=" & hdn.Value & "&Caption=" & HidCaption.Value
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "newpage", "customOpen('" + url + "');", True)


        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "newpage", "alert('hello');", True)
            '   ClientScript.RegisterStartupScript(Me.GetType(), "pop", "<Script>alert('hello')</script>", True)
        End Try

    End Sub
    Function getDataType(ByVal TypeID As Integer) As String
        Select Case TypeID
            Case 0
                getDataType = "Text"
            Case 1
                getDataType = "Numeric"
            Case 2
                getDataType = "Date"
            Case 3
                getDataType = "Boolean"
            Case 4
                getDataType = "Raw"
            Case 5
                getDataType = "Options"
        End Select
    End Function
    Function getCanDelete(ByVal ID As Integer) As Boolean
        Select Case ID
            Case 0
                getCanDelete = False
            Case 1
                getCanDelete = True
        End Select
    End Function
    Sub change(ByVal sender As Object, ByVal e As EventArgs)
        Dim txt As TextBox
        txt = CType(sender, TextBox)
        Dim btn As Button = txt.Parent.FindControl("Button1")
        btn.Enabled = True
    End Sub
    Sub changeCHK(ByVal sender As Object, ByVal e As EventArgs)
        Dim chk As CheckBox
        chk = CType(sender, CheckBox)
       
        Dim hdn As HiddenField
       
        Dim AttribID, AttribName, AttribCap As String
       
        hdn = chk.Parent.FindControl("hdnAttribID")
        AttribID = hdn.Value
        Dim strConn As String

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DT1n As New DataTable
        Dim cmdIns1n As New SqlCommand("UPDATE [ADMINETS].[dbo].[tblCustomAttributes] SET DELETED ='" & chk.Checked & "' WHERE attributeid = '" & AttribID & "'  ", New SqlConnection(strConn))
        cmdIns1n.Connection.Open()
        cmdIns1n.ExecuteNonQuery()
        ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", "<script>alert('Record has been updated')</script>")
        If cmdIns1n.Connection.State = ConnectionState.Open Then
            cmdIns1n.Connection.Close()
        End If
    End Sub
End Class
