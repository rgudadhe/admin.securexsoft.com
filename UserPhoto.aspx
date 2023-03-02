<%@ Page Language="VB"%>


<script  type="text/VB" runat="server" >
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd As New System.Data.SqlClient.SqlCommand("Select * from tblUsers where UserID='" & Request("UserId") & "'", New System.Data.SqlClient.SqlConnection(strConn))
        SQLCmd.Connection.Open()
        Dim dr As System.Data.SqlClient.SqlDataReader = SQLCmd.ExecuteReader()
        If dr.Read = True Then
            If Not IsDBNull(dr("photo")) Then
                Response.BinaryWrite(dr("photo"))
                Response.ContentType = "Image/Jpeg"
            Else
                Response.Redirect("~/Images/images.jpg")

            End If
        End If
        dr.Close()
        SQLCmd.Connection.Close()
    End Sub
   
    </script>  
    
