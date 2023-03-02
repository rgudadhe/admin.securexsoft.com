<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForceRouting4UserResultNew.aspx.vb" Inherits="FRUResult" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href= "../styles/Default.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div >    
    <br />
        <br />
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>               
                <asp:Panel ID="iMain" runat="server" Visible="false" Width="100%" Wrap="False" >
                    
                    <table width="100%" >
                    
                        <tr>
                            <td style="height: 197px">                                <br />
                                
<asp:Repeater ID="dlist" runat="server">
<HeaderTemplate>
<table border="1">
            <tr class="SMSelected">
            <th>*</th>
            <th>User Details</th>                        
            <th>User Level</th>                        
            </TR>
</HeaderTemplate>

<ItemTemplate>
<tr>            
            <td style="font-size: 8pt; font-family: Trebuchet MS ;">
            <asp:HiddenField ID="hdnUser" Value='<%#Container.DataItem("UserID")%>' runat="server" />                               
            <asp:HiddenField ID="hdnLevels" Value='<%#Container.DataItem("Levels")%>' runat="server" />
            <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
            </td>
            <td style="font-size: 8pt; font-family: Trebuchet MS;">
                <asp:Label ID="lblUser" runat="server" Text='<%#Container.DataItem("UserName")%>'></asp:Label>
            </td>
            <td style="font-size: 8pt; font-family: Trebuchet MS;">    
                <asp:Label ID="lblType" runat="server" Text='<%#Container.DataItem("UserLevel")%>'></asp:Label>
            </td>                       
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr bgcolor="#cccccc">
           <td style="font-size: 8pt; font-family: Trebuchet MS;">
            <asp:HiddenField ID="hdnUser" Value='<%#Container.DataItem("UserID")%>' runat="server" />                               
            <asp:HiddenField ID="hdnLevels" Value='<%#Container.DataItem("Levels")%>' runat="server" />
            <asp:LinkButton ID="lnkEdit" runat="server" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
            </td>
            <td style="font-size: 8pt; font-family: Trebuchet MS;">
                <asp:Label ID="lblUser" runat="server" Text='<%#Container.DataItem("UserName")%>'></asp:Label>
            </td>
            <td style="font-size: 8pt; font-family: Trebuchet MS;">    
                <asp:Label ID="lblType" runat="server" Text='<%#Container.DataItem("UserLevel")%>'></asp:Label>
            </td>           
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
</table>
</FooterTemplate>
</asp:Repeater>
                            </td>
                        </tr>
                    </table>                 
                  </asp:Panel>
<asp:Panel ID="idetails" runat="server" Height="" Visible="false" Width="100%" Wrap="False">
    
<asp:Repeater ID="rptHistory" runat="server">
<HeaderTemplate>
<table border="1">
            <TR bgcolor="#3399cc">
            <TH><div class="SMSelected">Force Routing Levels for: <asp:Label ID="lblTempName" runat="server" text='<%#strUserName%>'></asp:Label></div>
                </TH>                        
            </TR>
</HeaderTemplate>

<ItemTemplate>
<tr>
            <td style="font-size: 8pt; font-family: Trebuchet MS;">
            <asp:CheckBox ID="ckSelected" runat="server" Checked='<%#chkLevel(HDNSelUserFRLvl.value.tostring, Container.DataItem("LevelNo"))%>' Text='<%#Container.DataItem("LevelNameNew")%>'  />                
            <asp:HiddenField ID="hdnLvlNO" runat="server" Value='<%#Container.DataItem("LevelNo")%>'/>
            </td>
            
            
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr bgcolor="#cccccc">
            <td style="font-size: 8pt; font-family: Trebuchet MS;">
            <asp:CheckBox ID="ckSelected" runat="server" Checked='<%#chkLevel(HDNSelUserFRLvl.value.tostring, Container.DataItem("LevelNo"))%>' Text='<%#Container.DataItem("LevelNameNew")%>'/>               
            <asp:HiddenField ID="hdnLvlNO" runat="server" Value='<%#Container.DataItem("LevelNo")%>'/>
            </td>            
     
</tr>
</AlternatingItemTemplate>
<FooterTemplate>
<tr>
<td>
<asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />  
<asp:Button ID="btnBack" runat="server" Text="<< Back To Result" OnClick="btnBack_Click" />  
</td>
</tr>
</table>

</FooterTemplate>
</asp:Repeater>
    <asp:HiddenField ID="HDNSelUserFRLvl" runat="server" />
    <asp:HiddenField ID="hdnSelUserID" runat="server" />
    <asp:HiddenField ID="hdnSelUserLevel" runat="server" />
</asp:Panel>                              
              
    </div>
    </form>
</body>
</html>
