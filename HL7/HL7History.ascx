<%@ Control Language="VB" AutoEventWireup="false" CodeFile="HL7History.ascx.vb" Inherits="FaxPlus_FaxHistory" %>
<asp:Repeater ID="rptHistory" runat="server">
<HeaderTemplate>
<table border="1">
            <TR bgcolor="#3399cc">
            <TH><div class="SMSelected">Status</div></TH>            
            <TH><div class="SMSelected">Date Modified</div></th>                        
            <TH><div class="SMSelected">User</div></th>            
            </TR>
</HeaderTemplate>

<ItemTemplate>
<tr>
            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("StatusDesc")%></td>
            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("DateModified")%></td>                        
            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("UserName")%></td>
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr bgcolor="#cccccc">

            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("StatusDesc")%></td>
            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("DateModified")%></td>                        
            <td style="font-size: 8pt; font-family: Verdana;"><%#Container.DataItem("UserName")%></td>
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
<asp:HiddenField ID="hdnTransID" runat="server" Value='<%#DataBinder.Eval(CType(Container, DataGridItem).DataItem, "TranscriptionID") %>'/>

