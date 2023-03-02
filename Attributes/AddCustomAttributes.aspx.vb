Imports System
Imports System.Data
Imports System.Data.SqlClient
Namespace ets
    Partial Class Attributes_Add
        Inherits BasePage
        '  Private DLAT As New DALAuditTrail.DALAuditLog
        Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
            Dim strMessage As String
            If String.IsNullOrEmpty(txtAttribName.Text) Then
                valAttribName.IsValid = False
                valAttribName.ErrorMessage = "Name can not have white space"
            ElseIf InStr(Trim(txtAttribName.Text), " ") > 0 Then
                valAttribName.IsValid = False
                valAttribName.ErrorMessage = "Name can not have white space"
            Else
                Dim strConn As String

                strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim cmdIns1 As New SqlCommand("SELECT * FROM [AdminETS].[dbo].[tblCustomAttributes] Where Caption='" & txtAttribCaption.Text & "' ", New SqlConnection(strConn))
                cmdIns1.CommandType = CommandType.Text
                cmdIns1.Connection.Open()
                Dim DT1 As New DataTable
                Dim DRRec1 As SqlDataReader = cmdIns1.ExecuteReader()
                DT1.Load(DRRec1)
                If DT1.Rows.Count = 0 Then
                    Dim DT As New DataTable
                    Dim cmdIns As New SqlCommand("INSERT INTO [AdminETS].[dbo].[tblCustomAttributes] ([Name], [Caption],[ControlType]) Values ('" & txtAttribName.Text & "', '" & txtAttribCaption.Text & "', '" & ddType.SelectedValue & "')", New SqlConnection(strConn))
                    ' Response.Write(" SELECT Top 1 PhyID from secureweb.dbo.tblphyassignment where userid ='" & Session("userID").ToString & "' ")

                    cmdIns.Connection.Open()
                    cmdIns.ExecuteNonQuery()

                    If cmdIns.Connection.State = ConnectionState.Open Then
                        cmdIns.Connection.Close()
                    End If
                    txtAttribCaption.Text = String.Empty
                    txtAttribName.Text = String.Empty
                    strMessage = "Attribute has been added"
                Else
                    strMessage = "Attribute already exits"
                End If

                Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
            End If
        End Sub


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then

            End If
        End Sub



    End Class
End Namespace