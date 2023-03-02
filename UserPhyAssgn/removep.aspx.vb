Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsUPLM As New ETS.BL.UserPrLvlMgmt
        With clsUPLM
            .TrackID = Request("TrackID")
            .DeletePhyUserAsignment()
        End With
        clsUPLM = Nothing
        Response.Write("<script language='javascript'>window.opener.location.reload();</script>")
        Response.Write("<script>window.close();</script>")
    End Sub
End Class

