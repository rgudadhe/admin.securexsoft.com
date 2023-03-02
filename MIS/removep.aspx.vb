Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("TrackID") <> "" Then
            Dim clsMTD As ETS.BL.MTDirectDictatorAssignments
            Try
                clsMTD = New ETS.BL.MTDirectDictatorAssignments
                If clsMTD.Remove_Click_From_MTDirectDictatorAssignment(Request("lvlNo"), Request("UID"), Request("PID")) Then
                    Response.Write("<script language='javascript'>window.opener.location.reload();</script>")
                    Response.Write("<script>window.close();</script>")
                End If
            Catch ex As Exception
            Finally
                clsMTD = Nothing
            End Try
        End If
    End Sub
End Class

