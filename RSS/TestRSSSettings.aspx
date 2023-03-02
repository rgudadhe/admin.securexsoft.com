<%@ Page Language="VB" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<%@ Register
    Assembly="HHM.RIAnimation"
    Namespace="HHM.RIAnimation"
    TagPrefix="ETSAnim" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server" type="text/VB"> 
    
   
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        
        If IsNothing(File1.PostedFile) Then Label1.Text = "Select a file to Test" : Exit Sub
        If File1.PostedFile.ContentLength = 0 Then Label1.Text = "Select a file to Test" : Exit Sub
        Button2.Visible = True
        Button1.Visible = False
        File1.Visible = False
        Label1.ForeColor = Drawing.Color.DarkRed
        Label1.Font.Name = "verdhana"
        Label1.Text = "<B>Selected File</B>   <->   <B>" & Mid(File1.PostedFile.FileName, InStrRev(File1.PostedFile.FileName, "\") + 1) & "</B><br><br>"
            
        Dim FileEx As String = IO.Path.GetExtension(File1.PostedFile.FileName)
        'Dim ConString As String
        'Dim SQLString As String
        'ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        'Dim oConn As New Data.SqlClient.SqlConnection
        'Dim oCommand As New Data.SqlClient.SqlCommand
        'Dim oRec As Data.SqlClient.SqlDataReader
        'oConn.ConnectionString = ConString
        Try
            'oConn.Open()
            'SQLString = "SELECT rss.[Order],rss.Type as AType, rss.Format as AFormat, rss.Deleminator as ADelr, rss.AttributeID as ANameID, A.Caption as AName " & _
            '                       "FROM tblRSSSettings AS rss INNER JOIN " & _
            '                       "tblAttributes AS A ON rss.AttributeID = A.AttributeID where rss.SettingID='" & hdnSetting.Value & "' " & _
            '                       "union " & _
            '                       "SELECT rss.[Order],rss.Type as AType, rss.Format as AFormat, rss.Deleminator as ADelr, rss.AttributeID as ANameID, tblRSSAttributes.Caption as AName " & _
            '                       "FROM         tblRSSSettings AS rss INNER JOIN " & _
            '                       "tblRSSAttributes ON rss.AttributeID = tblRSSAttributes.AttributeID where rss.SettingID='" & hdnSetting.Value & "' " & _
            '                       "order by rss.[Order]"
            
            'oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            'oRec = oCommand.ExecuteReader
            'oRec.Read()
            Dim clsRSSS As New ETS.BL.RSSSettings
            Dim DSRSSS As Data.DataSet = clsRSSS.getRSSSettings(hdnSetting.Value.ToString)
            clsRSSS = Nothing
            
            If DSRSSS.Tables(0).Rows.Count > 0 Then
                Dim eleLocation As Integer = 1
                Dim eleValue As String = String.Empty
                Dim lenRemain As Integer
                Dim strRemain As String = IO.Path.GetFileNameWithoutExtension(File1.PostedFile.FileName).ToString
                'If InStr(strRemain, oRec("ADelr")) - 1 >= 0 Then
                '    lenRemain = InStr(strRemain, oRec("ADelr")) - 1
                'Else
                '    lenRemain = Len(strRemain)
                'End If
                'If UCase(oRec("AType")) = "TIME" Or UCase(oRec("AType")) = "DATE" Then
                '    eleValue = DoFormat(oRec("AType"), oRec("AFormat"), Mid(strRemain, 1, lenRemain))
                'Else
                '    eleValue = Mid(strRemain, 1, lenRemain)
                'End If
                'Label1.Text = Label1.Text & " <b>" & oRec("AName") & "</B>   <->   <B>" & eleValue & "</B><br/>"
                'strRemain = Mid(strRemain, InStr(strRemain, oRec("ADelr")) + 1)
                For Each oRec As Data.DataRow In DSRSSS.Tables(0).Rows
                    'Do While oRec.Read
                    If InStr(strRemain, oRec("ADelr")) - 1 >= 0 Then
                        lenRemain = InStr(strRemain, oRec("ADelr")) - 1
                    Else
                        lenRemain = Len(strRemain)
                    End If
                    If UCase(oRec("AType")) = "TIME" Or UCase(oRec("AType")) = "DATE" Then
                        eleValue = DoFormat(oRec("AType"), oRec("AFormat"), Mid(strRemain, 1, lenRemain))
                    Else
                        eleValue = Mid(strRemain, 1, lenRemain)
                    End If
                    Label1.Text = Label1.Text & " <b>" & oRec("AName") & "</B>   <->   <B>" & eleValue & "</B><br/>"
                    strRemain = Mid(strRemain, InStr(strRemain, oRec("ADelr")) + 1)
                    eleLocation = eleLocation + 1
                Next
                'Loop
                DSRSSS.Dispose()
                Label1.Text = Label1.Text & "<br><B>File Extention</B> <-> <B>" & FileEx & "</B>"
            Else
                Label1.Text = "No saved settings found"
            End If
        Catch ex As Exception
            Label1.Text = ex.Message & " Failed parsing selected file using current settings"
        Finally
            
            'If oConn.State <> Data.ConnectionState.Closed Then
            '    oConn.Close()
            '    oConn = Nothing
            'End If
        End Try
    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Button2.Visible = False
        Button1.Visible = True
        File1.Visible = True
        Label1.Text = ""
    End Sub
    Private Function DoFormat(ByVal strType As String, ByVal strFormat As String, ByVal strInput As String)
        Dim strCaseValue As String
        Dim i As Integer
        If strType = "Time" Then
            Dim strHour As String = String.Empty
            Dim strMinute As String = String.Empty
            Dim strSeconds As String = String.Empty
            For i = 1 To Len(strFormat)
                strCaseValue = Mid(strFormat, i, 1)
                Select Case strCaseValue
                    Case "H"
                        strHour = strHour & Mid(strInput, i, 1)
                    Case "M"
                        strMinute = strMinute & Mid(strInput, i, 1)
                    Case "S"
                        strSeconds = strSeconds & Mid(strInput, i, 1)
                End Select
            Next i
            DoFormat = strHour & ":" & strMinute & ":" & strSeconds
        Else
            Dim strYear As String = String.Empty
            Dim strMonth As String = String.Empty
            Dim strDay As String = String.Empty
            Dim strHour As String = String.Empty
            Dim strMinute As String = String.Empty
            Dim strSeconds As String = String.Empty
            Dim strTime As String = String.Empty
            For i = 1 To Len(strFormat)
                strCaseValue = Mid(strFormat, i, 1)
                Select Case strCaseValue
                    Case "M"
                        strMonth = strMonth & Mid(strInput, i, 1)
                    Case "D"
                        strDay = strDay & Mid(strInput, i, 1)
                    Case "Y"
                        strYear = strYear & Mid(strInput, i, 1)
                    Case "H"
                        strHour = strHour & Mid(strInput, i, 1)
                    Case "N"
                        strMinute = strMinute & Mid(strInput, i, 1)
                    Case "S"
                        strSeconds = strSeconds & Mid(strInput, i, 1)
                End Select
            Next i
            If Not Trim(strHour) & Trim(strMinute) & Trim(strSeconds) = "" Then
                If Trim(strHour) = "" Then
                    strHour = "00"
                End If
                If Trim(strMinute) = "" Then
                    strMinute = "00"
                End If
                If Trim(strSeconds) = "" Then
                    strSeconds = "00"
                End If
                strTime = " " & strHour & ":" & strMinute & ":" & strSeconds
            End If
            DoFormat = strMonth & "/" & strDay & "/" & strYear & strTime
            End If
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            hdnSetting.Value = Request.QueryString("SettingID").ToString
            lblHeader.Text = "File Import Settings For - <b>" & Request.QueryString("SettingName").ToString
        End If
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Response.Redirect("EditFileImportProcess.aspx", True)
        Catch ex As Exception

        End Try
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>RSS Settings</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script type='text/javascript'>
    function cancelClick() {
        var label = $get('ctl00_SampleContent_Label1');
        
    }
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />           
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>File Import Process</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table >
        <tr>
            <td colspan="3" valign="top" class="alt1">
                <asp:Label ID="lblHeader" runat="server" CssClass="common"></asp:Label>
            </td>
        </tr>
            <tr><td colspan="3" style="position: relative; top: 0px" valign="top">                                                                            
                                    <input id="File1" type="file" class="common" runat="server"/><br />
                                    <asp:Label ID="Label1" runat="server" Text="Select one file to Test" CssClass="common" ></asp:Label><br />                                                                          
                                
           </td></tr>
           <tr>
           <td><asp:Button CssClass="button" ID="Button1" runat="server" Text="Test" /></td>           
           <td><asp:Button CssClass="button" ID="Button2" runat="server" Text="Reset Test" OnClick="Button2_Click" /></td>
           <td><asp:Button CssClass="button" ID="btnBack" runat="server" Text="<< Go to Main List" OnClick="btnBack_Click" /></td>           
           </tr>
            
    </table>
        <asp:HiddenField ID="hdnSetting" runat="server" />                
        </asp:Panel>
        </div> 
        </div> 
</form>        
</body>
</html>


