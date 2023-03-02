<%@ Control Language="vb" AutoEventWireup="false" Inherits="HierarGridDemoVB.Authors" CodeFile="History.ascx.vb" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
    
<link href= "../styles/Default.css" type="text/css" rel="stylesheet" />

<script language="javascript" type="text/javascript">
    function ShowDiv(str)
    {
        //alert(str);
        document.getElementById('FileName').style.display=''
        document.getElementById('FileName').scrollTop = 0;
        
        document.getElementById('FileName').autoHide = false;
        document.getElementById('FileName').position = "top left";
        document.getElementById('FileName').constrainToScreen = false;

        return false;
    }
    function OpenPopUp(str,str1,str2)
    {
        //alert(document.getElementById('hdnMD5Value'))
        
        //var str2 = document.all("<%= hdnMD5Value.ClientID %>").value
        //alert(str2)
        window.open('EditFIL.aspx?RecID='+ str +'&FileName='+str1+'&MD5='+ str2 ,"",'width=300,height=100',false)
        return false;
    }
</script>    
<%--<div style="WIDTH: 100%; POSITION: relative; HEIGHT: 32px; left: 0px; top: 0px; border-right: navy thin solid; border-top: navy thin solid; border-left: navy thin solid; border-bottom: navy thin solid;" >--%>

<div style="border-right: navy thin solid; border-top: navy thin solid; border-left: navy thin solid; border-bottom: navy thin solid;width:100%; height:100%">
    
            <asp:Repeater ID="rptHistory" runat="server" > 
    <HeaderTemplate>  
    
        <table id="tbl1" style="font: 10pt verdana" cellpadding="1" cellspacing="1" border="1">
        <tr><td colspan="8" style="font-family: Verdana; font-size: 10pt;"> File Import Log</td></tr>
        <TR>
        <%--<TD class="SMSelected">Edit</TD>--%>
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
        <td style="font-family: Verdana; font-size: 8pt;"><asp:Button ID="btnEdit" runat="server" Text="Edit" Enabled='<%#iif(isdbnull(Container.DataItem("Status")),False,True)%>' OnClientClick=<%# "javascript:return OpenPopUp('" + Container.DataItem("RecordID").ToString() + "','"+ Container.DataItem("FileName").ToString()  +"','"+ Container.DataItem("MD5Value").ToString() +"');" %> /></td>
        <%--<td style="font-family: Verdana; font-size: 8pt;"><asp:Button ID="Button1" runat="server" Text="Edit" OnClientClick="<%#javascript:window.open('EditFIL.aspx?RecID='+ Container.DataItem("RecordID").ToString%>);return false;"/></td>--%>
        <%--<td style="font-family: Verdana; font-size: 8pt;"><asp:Button ID="btnEdit" runat="server" Text="Edit"/></td>--%>
        <td style="font-family: Verdana; font-size: 8pt;"><%#mid(Container.DataItem("FileName"),1,InStr(Container.DataItem("FileName"),"_")-1)%>  </td>
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
     
    <asp:HiddenField ID="hdnMD5Value" runat="server" Value='<%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "MD5Value") %>' />
    <asp:HiddenField ID="hdnFileName" runat="server" />
	</DIV>
	
	<div id="FileName" style="display:none; margin-top:5px; background-color:Gray; font-family:Trebuchet MS; font-size:12px; width:250px; height:65px" >
        <asp:Label ID="lblfilename" runat="server" Text="Please enter file name"></asp:Label>                	    
        <asp:TextBox ID="txtFname" runat="server" Font-Names="Trebuchet MS" Font-Size="12px" Width="238px"></asp:TextBox>
        <asp:Button ID="btnSave" runat="server" Text="Submit" Font-Names="Trebuchet MS" Font-Size="12px" /></div>
        




	
