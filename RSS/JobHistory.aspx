<%@ Page Language="VB" AutoEventWireup="false" CodeFile="JobHistory.aspx.vb" Inherits="RSS_JobHistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function OpenPopUp(str,str1,str2)
        {
            //alert(document.getElementById('hdnMD5Value'))
        
            //var str2 = document.all("<%= hdnMD5Value.ClientID %>").value
            //alert(str2)
            //window.open('EditFIL.aspx?RecID='+ str +'&MD5='+ str2 +'&FileName='+str1 ,"",'width=700,height=700',false)
            str1=str1.replace(/#/gi, "@");
            window.location.href = 'EditFIL.aspx?RecID='+ str +'&MD5='+ str2 +'&FileName='+str1
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Repeater ID="rptHistory" runat="server" > 
    <HeaderTemplate>  
    
        <table id="tbl1" style="font: 10pt verdana" cellpadding="1" cellspacing="1" border="1">
        <tr><td colspan="9" style="font-family: Verdana; font-size: 10pt;"> File Import Log</td></tr>
        <TR>
        <TD class="SMSelected">Edit</TD>
        <TD class="SMSelected">Client Job#</TD>
        <TD class="SMSelected">Client</TD>        
        <TD class="SMSelected">Status</TD>
        <TD class="SMSelected">Error</TD>
        <TD class="SMSelected">User Name</TD>
        <TD class="SMSelected">Process</TD>
        <TD class="SMSelected">Date</TD>
        <TD class="SMSelected">Version</TD>
        </TR>
          
    </HeaderTemplate>
    
    <ItemTemplate>
        
        <tr>
        <td style="font-family: Verdana; font-size: 8pt;"><asp:Button ID="btnEdit" runat="server" Text="Edit" Enabled='<%#iif(isdbnull(Container.DataItem("Status")),False,True)%>' OnClientClick=<%# "javascript:return OpenPopUp('" + Container.DataItem("RecordID").ToString() + "','"+ Container.DataItem("FileName").ToString()  +"','"+ Container.DataItem("MD5Value").ToString() +"');" %> CssClass="button" /></td>
        <%--<td style="font-family: Verdana; font-size: 8pt;"><asp:Button ID="Button1" runat="server" Text="Edit" OnClientClick="<%#javascript:window.open('EditFIL.aspx?RecID='+ Container.DataItem("RecordID").ToString%>);return false;"/></td>--%>
        <%--<td style="font-family: Verdana; font-size: 8pt;"><asp:Button ID="btnEdit" runat="server" Text="Edit"/></td>--%>
        <td style="font-family: Verdana; font-size: 8pt;"><%#Container.DataItem("CJobNumber")%></td>        
        <td style="font-family: Verdana; font-size: 8pt;"><%#Container.DataItem("FileName")%>  </td>
         <%--<td style="font-family: Verdana; font-size: 8pt;"><%#mid(Container.DataItem("FileName"),1,InStr(Container.DataItem("FileName"),"_")-1)%>  </td>--%>
        <td style="font-family: Verdana; font-size: 8pt;"><%#getStatus(Container.DataItem("Status").ToString)%></td>        
        <td style="font-family: Verdana; font-size: 8pt;"><%#Container.DataItem("Error")%></td>        
        <td style="font-family: Verdana; font-size: 8pt;"><%#Container.DataItem("UserName")%></td>        
        <td style="font-family: Verdana; font-size: 8pt;"><%#Container.DataItem("SettingName")%></td>        
        <td style="font-family: Verdana; font-size: 8pt;"><%#Container.DataItem("DateProcessed")%></td>        
        <td style="font-family: Verdana; font-size: 8pt;"><%#Container.DataItem("Version")%></td>        
        </tr>
    </ItemTemplate>
     
    <FooterTemplate>    
     </Table> 
    </FooterTemplate>   
    </asp:Repeater>
    </div>
    <asp:HiddenField ID="hdnMD5Value" runat="server" />
    <asp:HiddenField ID="hdnFileName" runat="server" />
    </form>
</body>
</html>
