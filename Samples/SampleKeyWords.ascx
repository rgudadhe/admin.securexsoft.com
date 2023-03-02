<%@ Control Language="VB" AutoEventWireup="false" CodeFile="SampleKeyWords.ascx.vb" Inherits="Samples_SampleKeyWords" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table>
    <tr>
        <td rowspan=2>
            <asp:TextBox ID="txtKeawords" runat="server" Rows="3" TextMode="MultiLine" Width="304px" Text='<%#DataBinder.Eval(CType(Me.BindingContainer, DataGridItem).DataItem, "KeyWords").ToString%>' Enabled="false"></asp:TextBox>            
        </td>
        <td>
            <asp:TextBox ID="txtSampleName" runat="server" Text='<%#DataBinder.Eval(CType(Me.BindingContainer, DataGridItem).DataItem, "SampleName").ToString%>' Enabled="false" Width="280px"></asp:TextBox>            
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSave" runat="server" Text="Edit" />
            <%--<asp:Button ID="btnEditDoc" runat="server" Text="Edit Sample" />--%>
        </td>
    </tr>
</table>


</ContentTemplate>
<Triggers> 
<asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
</Triggers>
</asp:UpdatePanel>

<asp:HiddenField ID="hdnSampleID" runat="server" Value=<%#DataBinder.Eval(CType(Me.BindingContainer, DataGridItem).DataItem, "SampleID").ToString%>/>