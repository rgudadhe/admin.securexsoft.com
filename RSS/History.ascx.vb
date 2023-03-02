Imports System
Imports System.Data
Namespace HierarGridDemoVB
    Partial Class Authors
        Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        Public Function getStatus(ByVal blnStatus) As String
            If String.IsNullOrEmpty(blnStatus) Then
                getStatus = "Pending Re-Import"
            Else
                If blnStatus Then
                    getStatus = "Imported"
                Else
                    getStatus = "Failed"
                End If
            End If
        End Function
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Dim CLSFIL As New ETS.BL.FileImportLog
                Dim DSFILHistory As dataset = CLSFIL.getFileImportHistory(Session("ContractorID").ToString, hdnMD5Value.Value)
                CLSFIL = Nothing
                rptHistory.DataSource = DSFILHistory
                rptHistory.DataBind()
                DSFILHistory.Dispose()
            End If
        End Sub
        'Protected Sub rptHistory_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles rptHistory.ItemCommand
        '    Response.Write("Test")
        '    Response.End 
        '    If Trim(UCase(e.CommandName)) = Trim(UCase("btnEd")) Then
        '        Dim btn As Button
        '        btn = CType(source, Button)

        '        If Trim(UCase(btn.Text)) = Trim(UCase("...")) Then
        '            Dim item As RepeaterItem
        '            item = e.Item
        '            Dim txt As TextBox
        '            txt = CType(item.FindControl("txtCJonNumber"), TextBox)

        '            txt.Visible = True
        '            txt.Text = "Testing"

        '            btn.Text = "Save"
        '        End If
        '    End If
        'End Sub
        'Protected Sub OnLetterClicked(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs)
        '    Try
        '        Response.Write("Testing")
        '        Response.End()
        '    Catch ex As Exception
        '        Response.Write(ex.Message)
        '    End Try
        'End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Response.Write("Testing")
            Response.End()
        End Sub
    End Class
End Namespace
