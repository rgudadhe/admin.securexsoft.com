<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MacroSearch.aspx.vb" Inherits="ets.Macros_TATempSearch" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Templates Assignments</title>
    <link href= "../App_Themes/Css/Main.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Styles.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
 function chkALL(str)
        {
            for(i = 0; i < form1.elements.length; i++)
            {
                elm = document.forms[0].elements[i]
                if (elm.type == 'checkbox')
                {
                    elm.checked = str.checked;
                }
            }
        }
 </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Multiple Macro Assignments</h1>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
            <table width="50%">
                <tr>
                    <td class="HeaderDiv">
                        <asp:Label ID="lblPhyName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td  class="alt">
                        Search Macro
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" Width="267px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search Template" CssClass="button" /></td>
                    </tr>
            </table>
        </asp:Panel>        
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
            <asp:Repeater ID="rptTemp" runat="server">
                 <HeaderTemplate>
                    <table>
                        <tr>            
                        <td class="alt1" align="center"><asp:CheckBox ID="ChkALL" runat="server" OnClick="chkALL(this);" /></td>          
                        <td class="alt1" align="center">Macro Description</td>
                        </tr>
                 </HeaderTemplate>

        <ItemTemplate>
        <tr>        
                    <td  class="alt1" align="center"><asp:CheckBox ID="chkSel" runat="server">
                    </asp:CheckBox> </td>    
                    <td width=80%><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("Description")%>' ></asp:label>
                    <asp:HiddenField ID="McID" runat="server" Value='<%#Container.DataItem("McID")%>'/></td>            

        </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
        <tr bgcolor="#cccccc" >
        <td  class="alt1" align="center"><asp:CheckBox ID="chkSel" runat="server">
                    </asp:CheckBox> </td>    
                    <td width=80%><asp:Label ID="txtName" runat="server" Text='<%#Container.DataItem("Description")%>' ></asp:label>
                    <asp:HiddenField ID="McID" runat="server" Value='<%#Container.DataItem("McID")%>'/></td>            
        </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
            <td colspan=2>
                <asp:Button ID="btnSel" CssClass="button" runat="server" Text="Assign Selected" OnClick="btnSel_Click"/>
            </td>
        </table>
        </FooterTemplate>
        </asp:Repeater>
        </asp:Panel>             
                
    
        <asp:Literal ID="iResponse" runat="server" ></asp:Literal>                    
        <asp:HiddenField ID="hdnPhyID" runat="server" />
        </div> 
        </div> 
    </form>
</body>
</html>
