<%@ Application Language="VB" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim objErr As Exception = Server.GetLastError().GetBaseException()
        If InStr(LCase(objErr.Message), "does not exist.") <> 0 Then
            'Response.StatusCode = 404
            Server.ClearError()
            Server.Transfer("~/ErrorPages/404.aspx")
        ElseIf InStr(objErr.Message, "A potentially dangerous Request.QueryString") <> 0 Then
            Server.ClearError()
            'Response.StatusCode = 500
            Server.Transfer("~/ErrorPages/Oops.aspx")
        Else
            Try
                Dim conn As New System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings("ETSConnectionString").ToString())
                Dim cmd As New System.Data.SqlClient.SqlCommand("insert_error", conn)
                cmd.CommandType = System.Data.CommandType.StoredProcedure
                cmd.Parameters.Add("@code", System.Data.SqlDbType.Int)
                cmd.Parameters.Add("@http_referer", System.Data.SqlDbType.NVarChar)
                cmd.Parameters.Add("@urlrequested", System.Data.SqlDbType.NVarChar)
                cmd.Parameters.Add("@errormsg", System.Data.SqlDbType.NVarChar)
                cmd.Parameters.Add("@errortrace", System.Data.SqlDbType.NVarChar)
                cmd.Parameters.Add("@userid", System.Data.SqlDbType.NVarChar)
                cmd.Parameters("@code").Value = 500
                If String.IsNullOrEmpty(Request.ServerVariables("HTTP_REFERER")) Then
                    cmd.Parameters("@http_referer").Value = objErr.Source
                Else
                    cmd.Parameters("@http_referer").Value = Request.ServerVariables("HTTP_REFERER")
                End If
                cmd.Parameters("@urlrequested").Value = Request.Url.ToString()
                cmd.Parameters("@errormsg").Value = objErr.Message.ToString()
                cmd.Parameters("@errortrace").Value = objErr.StackTrace.ToString()
                cmd.Parameters("@userid").Value = System.Web.HttpContext.Current.Session("UserID").ToString()
                conn.Open()
                cmd.ExecuteNonQuery()
                conn.Close()
            Catch ex As Exception

            End Try
            Server.ClearError()
            'Response.StatusCode = 500
            Server.Transfer("~/ErrorPages/Oops.aspx")
        End If
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
</script>