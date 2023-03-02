
Partial Class ERSSMainNew_HBA_HBA
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Trim(UCase(Session("UserID").ToString)) = Trim(UCase("A1ABBF5E-4869-4600-907F-01B6FAEEF377")) Then
            'Response.Write("Leave application pending(HL)".Contains("LWPHL"))
            'Session("UserID") = "3DDEF488-7581-4433-B6BA-96A679F1E427"
            'Response.Write(Server.MapPath("/ETS_Files/"))
            'Response.Write(Server.MapPath("../ETS_Files"))
            'Session("UserID") = "C2B7BF61-6F76-4746-8C63-3F0A6D67D4CC"
        End If
    End Sub
End Class
