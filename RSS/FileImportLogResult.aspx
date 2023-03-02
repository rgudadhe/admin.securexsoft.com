<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FileImportLogResult.aspx.vb" Inherits="ets.FileImportResult" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<LINK href= "../styles/Default.css" type="text/css" rel="stylesheet">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Untitled Page</title>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:Panel ID="Panel1" runat="server" Height="100%" Width="100%">
        <asp:Repeater ID="rptPhy" runat="server">
         <HeaderTemplate>
<table border="1">
            <TR bgcolor="#3399cc">            
            <TH class="HeaderDiv" align="center">
            </TH>
            <TH class="HeaderDiv" align="center">Customer Job#</TH>            
            <TH class="HeaderDiv" align="center">Client</TH>
            <TH class="HeaderDiv" align="center">MD5 Value</th>
            <TH class="HeaderDiv" align="center">Status</th>
            <TH class="HeaderDiv" align="center">Error</th>            
            <TH class="HeaderDiv" align="center">User Name</th>
            <TH class="HeaderDiv" align="center">Process Name</th>
            <TH class="HeaderDiv" align="center">Date Processed</th>
            <TH class="HeaderDiv" align="center">Version</th>
            </TR>
</HeaderTemplate>

<ItemTemplate>
<tr style="font-family:Verdana;font-size:x-small">        
            <td><asp:LinkButton ID="LinkButton1" runat="server" Font-Size="Small" Font-Names="vardhana" OnClick="LinkButton1_Click" OnClientClick="return confirm('Are you sure you want to Reimport this job?');" Enabled='<%#iif(isdbnull(Container.DataItem("Status")),False,True)%>'>Re-Import</asp:LinkButton></td>    
            <td><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("CJobNumber")%>' ></asp:label>
            <td><asp:Label ID="txtClient" runat="server" Text='<%#mid(Container.DataItem("FileName"),1,InStr(Container.DataItem("FileName"),"_")-1)%>' ></asp:label>
            <asp:HiddenField ID="RecID" runat="server" Value='<%#Container.DataItem("RecordID")%>'/></td>                        
            <td><asp:Label ID="lblMD5" runat="server" Text='<%#Container.DataItem("MD5Value")%>'></asp:Label></td>
            <td><%#getStatus(Container.DataItem("Status").ToString)%></td>
            <td><%#Container.DataItem("Error").ToString%></td>
            <td><%#Container.DataItem("UserName").ToString%></td>
            <td><%#Container.DataItem("SettingName").ToString%></td>
            <td><%#Container.DataItem("DateProcessed").ToString%></td>
            <td><%#Container.DataItem("version").ToString%></td>            
</tr>   
    </ItemTemplate>
<AlternatingItemTemplate>
<tr bgcolor="#cccccc" style="font-family:Verdana;font-size:x-small">
            <td><asp:LinkButton ID="LinkButton1" runat="server" Font-Size="Small" Font-Names="vardhana" OnClick="LinkButton1_Click" OnClientClick="return confirm('Are you sure you want to Reimport this job?');" Enabled='<%#iif(isdbnull(Container.DataItem("Status")),False,True)%>'>Re-Import</asp:LinkButton></td>
            <td ><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("CJobNumber")%>' ></asp:label>
            <td><asp:Label ID="txtClient" runat="server" Text='<%#mid(Container.DataItem("FileName"),1,InStr(Container.DataItem("FileName"),"_")-1)%>' ></asp:label>
            <asp:HiddenField ID="RecID" runat="server" Value='<%#Container.DataItem("RecordID")%>'/></td>                        
            <td><asp:Label ID="lblMD5" runat="server" Text='<%#Container.DataItem("MD5Value")%>'></asp:Label></td>
            <td><%#getStatus(Container.DataItem("Status").ToString)%></td>
            <td><%#Container.DataItem("Error").ToString%></td>
            <td><%#Container.DataItem("UserName").ToString%></td>
            <td><%#Container.DataItem("SettingName").ToString%></td>
            <td><%#Container.DataItem("DateProcessed").ToString%></td>
            <td><%#Container.DataItem("version").ToString%></td>
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>  
        </asp:Panel>        
        <asp:Literal ID="Literal1" runat="server"></asp:Literal></div>    
    </form>
</body>
</html>
