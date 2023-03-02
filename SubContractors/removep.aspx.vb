Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim squery
        If Request("ContrID") <> "" Then
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()
                squery = "Delete from tblSubCont2Physicians where TrackID='" & Request("ContrID") & "'"
                Dim cmdUp As New SqlCommand(squery, oConn)
                cmdUp.ExecuteNonQuery()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub






   
   
   
End Class

