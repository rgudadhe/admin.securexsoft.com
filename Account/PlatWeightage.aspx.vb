
Imports System.Data.SqlClient

Partial Class Account_PlatWeightage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()
                Dim LI2 As New ListItem
                LI2.Text = "Select Platform"
                LI2.Value = ""
                DLPlatform.Items.Add(LI2)
                Dim LI3 As New ListItem
                LI3.Text = "Direct Accounts"
                LI3.Value = "11111111-1111-1111-1111-111111111111"
                DLPlatform.Items.Add(LI3)
                Dim SQLCmd2 As New SqlCommand("Select AccountName, AccountID from tblaccounts where indirect = 'True' ", oConn)
                'Response.Write("Select * from tblUsers where UserID='" & UserId & "'")
                'Response.End()

                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                If DRRec2.HasRows = True Then
                    While DRRec2.Read
                        Dim LI As New ListItem
                        LI.Text = DRRec2("accountname").ToString
                        LI.Value = DRRec2("accountid").ToString
                        DLPlatform.Items.Add(LI)
                    End While
                End If
                DRRec2.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Open Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub

    Protected Sub DLPlatform_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLPlatform.SelectedIndexChanged
        If DLPlatform.SelectedValue = "" Then
            TextBox1.Text = 0
            TextBox2.Text = 0
            TextBox3.Text = 0
            TextBox4.Text = 0
            TextBox5.Text = 0
            TextBox6.Text = 0

        Else
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()

                Dim SQLCmd2 As New SqlCommand("Select * from tblweightage where PlatActID = '" & DLPlatform.SelectedValue & "' ", oConn)

                Dim DRRec2 As SqlDataReader = SQLCmd2.ExecuteReader()
                If DRRec2.HasRows = True Then
                    If DRRec2.Read Then

                        TextBox1.Text = DRRec2("MTLines").ToString
                        TextBox2.Text = DRRec2("MTPLines").ToString
                        TextBox3.Text = DRRec2("QALines").ToString
                        TextBox4.Text = DRRec2("QABLines").ToString
                        TextBox5.Text = DRRec2("QABSELines").ToString
                        TextBox6.Text = DRRec2("PPQALines").ToString
                    End If
                Else
                    TextBox1.Text = 0
                    TextBox2.Text = 0
                    TextBox3.Text = 0
                    TextBox4.Text = 0
                    TextBox5.Text = 0
                    TextBox6.Text = 0
                End If
                DRRec2.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Open Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub
End Class
