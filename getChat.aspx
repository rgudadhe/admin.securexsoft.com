<%@ Page Language="VB" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%
    '/*
    'This is the ASP.NET backend file for the AJAX Driven Chat application.

    'You may use this code in your own projects as long as this copyright is left
    'in place.  All code is provided AS-IS.
    'This code is distributed in the hope that it will be useful,
    'but WITHOUT ANY WARRANTY; without even the implied warranty of
    'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

    'For the rest of the code visit http://www.DynamicAJAX.com
    'Copyright 2007 Ryan Smith / NextView Inc.
    '*/
    
    'Add some headers to keep the response from getting cached.
    Response.AddHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT")
    Response.AddHeader("Last-Modified", Date.UtcNow)
    Response.AddHeader("Cache-Control", "no-cache, must-revalidate")
    Response.AddHeader("Pragma", "no-cache")
    Response.AddHeader("Content-Type", "text/xml; charset=utf-8")

    'Enter you database connection string here.
    Dim conn As New SqlConnection("Data Source=WIN11619;Initial Catalog=ETS;Persist Security Info=True;User ID=sa;Password=c4t!ar0und")
    conn.Open()
    
    'Add any new messages that may have gotten sent.
    If Not Request.Form("message") Is Nothing Then
        'Create a new insert command using a parameterized query.  
        'This helps eliminate the risk of SQL injection attacks.
        Dim insCommand As New SqlCommand("INSERT INTO message(chat_id, user_id, user_name, message, post_time) VALUES (@chat_id, 1, @name, @message, GETDATE())", conn)
        'Set our parameter values.  There is probably a prettier way of doing this.
        Dim paramChatID As New SqlParameter("chat_id", Data.SqlDbType.Int)
        paramChatID.Value = CInt(Request.QueryString("chat"))
        insCommand.Parameters.Add(paramChatID)
        Dim paramName As New SqlParameter("name", Data.SqlDbType.VarChar)
        paramName.Value = Request.Form("name")
        insCommand.Parameters.Add(paramName)
        Dim paramMsg As New SqlParameter("message", Data.SqlDbType.VarChar)
        paramMsg.Value = Request.Form("message")
        insCommand.Parameters.Add(paramMsg)
        'Execute the query to add the message to the database.
        insCommand.ExecuteNonQuery()
    End If
    
    '//Check to see if a reset request was sent.
    If Not Request.Form("action") Is Nothing AndAlso Request.Form("action") = "reset" Then
        Dim delCommand As New SqlCommand("DELETE FROM message WHERE chat_id = @chat_id")
        Dim paramChatID As New SqlParameter("chat_id", Data.SqlDbType.Int)
        paramChatID.Value = CInt(Request.QueryString("chat"))
        delCommand.Parameters.Add(paramChatID)
        delCommand.ExecuteNonQuery()
    End If
    
    'Create the acutal response.
    Dim xml As String = "<?xml version=""1.0"" ?><root>"
    'Check to make sure a chat room was passed.  If not alert them of their mistake
    If Request.QueryString("chat") Is Nothing Then
        xml &= "You are not currently in a chat session"
        xml &= "<message id=""0"">"
        xml &= "<user>Admin</user>"
        xml &= "<text>You are not currently in a chat session.</text>"
        xml &= "<time>" & Date.Now.Hour & " " & Date.Now.Minute & "</time>"
        xml &= "</message>"
    Else
        'Figure out the last message sent
        Dim last As Integer = 0
        If Not Request.QueryString("last") Is Nothing Then
            'I don't believe that we need to set last as a parameter.
            'Afterall, if someone tries to hack it, either the value will get set to zero,
            'or an exception will be thrown, the query will never get executed with anything
            'other than an Integer value.
            last = CInt(Request.QueryString("last"))
        End If
        'Create our message select command.
        Dim msgCommand As New SqlCommand("SELECT message_id, user_name, message, post_time FROM message WHERE chat_id = @chat_id and message_id > " & last, conn)
        Dim paramChatID As New SqlParameter("chat_id", Data.SqlDbType.Int)
        paramChatID.Value = CInt(Request.QueryString("chat"))
        msgCommand.Parameters.Add(paramChatID)
        Dim reader As SqlDataReader = msgCommand.ExecuteReader(Data.CommandBehavior.CloseConnection)
        '//Loop through each message and create an XML message node for each.
        'Notice that we're making use of Server.HtmlEncode to avoid script injection attacks.
        While reader.Read
            xml &= "<message id=""" & reader.Item("message_id") & """>"
            xml &= "<user>" & Server.HtmlEncode(reader.Item("user_name")) & "</user>"
            xml &= "<text>" & Server.HtmlEncode(reader.Item("message")) & "</text>"
            xml &= "<time>" & reader.Item("post_time") & "</time>"
            xml &= "</message>"
        End While
        xml &= "</root>"
        conn.Close()
        'Send out the AJAX response.
        Response.Write(xml)
    End If
 %>