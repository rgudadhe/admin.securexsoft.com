<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SetSamples.aspx.vb" Inherits="Samples_SetSamples" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Set Samples</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Routing.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>Set Dictation Samples</h1>
            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
                <DBWC:HierarGrid id="dlist" DataKeyField="TranscriptionID" allowsorting="True" autogeneratecolumns="False" style="Z-INDEX: 101" runat="server" TemplateCachingBase="Tablename" horizontalalign="Left" LoadControlMode="UserControl" PageSize="20">
				    <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#008A8C"></SelectedItemStyle>
				    <HeaderStyle ForeColor="Black" Font-Bold="true" />
				        <AlternatingItemStyle BackColor="Gainsboro">		
				        </AlternatingItemStyle>
				        <ItemStyle  BackColor="Beige" CssClass="common" Wrap="False" ></ItemStyle>
				        <Columns>
				            <asp:templatecolumn HeaderStyle-CssClass="GrdHeader">				                
									<itemtemplate>
            <asp:LinkButton id="LinkButton1" onclick="LinkButton1_Click" runat="server">Edit</asp:LinkButton>
        <asp:HiddenField id="hdnID" runat="server" Value='<%#Container.DataItem("TranscriptionID")%>'></asp:HiddenField> 
        <asp:HiddenField id="PhyName" runat="server" Value='<%#Container.DataItem("PhysicianName")%>'></asp:HiddenField> 
</itemtemplate>
									<headerstyle width="20px" />
									</asp:templatecolumn>						
									
				<asp:boundcolumn HeaderStyle-CssClass="GrdHeader" datafield="JobNumber" sortexpression="JobNumber" headertext="Job Number">
                    <itemstyle horizontalalign="Center" />
                </asp:boundcolumn>				
				<asp:boundcolumn HeaderStyle-CssClass="GrdHeader" datafield="CustJobID"	sortexpression="CustJobID" headertext="Cust. Job#">
                    <itemstyle horizontalalign="Center" />
                </asp:boundcolumn>
				<asp:boundcolumn HeaderStyle-CssClass="GrdHeader" datafield="DateAvailable"	sortexpression="DateAvailable" headertext="Date Available"  >
                    <itemstyle horizontalalign="Center" />
                </asp:boundcolumn>               
                <asp:boundcolumn HeaderStyle-CssClass="GrdHeader" datafield="AccountNumber"	sortexpression="AccountNumber" headertext="Account#" >
                    <itemstyle horizontalalign="Center" />
                </asp:boundcolumn>
<asp:boundcolumn HeaderStyle-CssClass="GrdHeader" datafield="AccountName"	sortexpression="AccountName" headertext="Account Name"   >
    <itemstyle horizontalalign="Center" />
</asp:boundcolumn>
<asp:boundcolumn HeaderStyle-CssClass="GrdHeader" datafield="PhysicianName"	sortexpression="PhysicianName" headertext="Physician Name"   >
    <itemstyle horizontalalign="Center" />
</asp:boundcolumn>
<asp:boundcolumn HeaderStyle-CssClass="GrdHeader" datafield="SuggestedBy"	sortexpression="SuggestedBy" headertext="Suggested By"   >
    <itemstyle horizontalalign="Center" />
</asp:boundcolumn>
				</Columns>				
                        <PagerStyle Mode="NumericPages" />
			</DBWC:HierarGrid>
            </asp:Panel>
    </div> 
    </div> 
    </form>
    
</body>
</html>
