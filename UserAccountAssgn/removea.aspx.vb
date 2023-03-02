Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim squery
        If Request("TrackID") <> "" Then
            squery = "Delete from tblAccountUserAssgn where TrackID='" & Request("TrackID") & "'"
            Dim cmdUp As New SqlCommand(squery, New SqlConnection(strConn))
            Try
                cmdUp.Connection.Open()
                cmdUp.ExecuteNonQuery()
            Finally
                If cmdUp.Connection.State = System.Data.ConnectionState.Closed Then
                    cmdUp.Connection.Close()
                    cmdUp = Nothing
                End If
            End Try
        End If



    End Sub






   
   
   
End Class

