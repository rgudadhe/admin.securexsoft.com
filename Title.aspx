<%@ Page Language="VB" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server">        
        
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        lHeader.Text = Request("lHeader")
    End Sub

    Protected Sub Logout_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Session.Clear()
        Response.Redirect("Login.aspx")
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
		<style type="text/css">
			.SMParentSelected {
				background-image:url(/styles/pro_7/background_parentselected.gif);
				background-repeat:repeat-x;
				border-top:1px solid #DADBDB;
				font:bold 8pt Tahoma;
				color:white;
				text-align:left;
				padding: 2px;
				padding-left: 12px;
				height:21px;				
			}
			.SMSelected {
			background-image:url(/styles/pro_7/MenuBg.gif);
			background-repeat:repeat-x;
			background-color:#000000;
			height:21px;
			border-top:1px solid #DADBDB;
			font:bold 8pt Tahoma;
			color:white;
			text-align:left;
			padding: 2px;
			padding-left:12px;
			)
		</style>
<script language="javascript" type="text/javascript">
// <!CDATA[

function Button1_onclick(eobj) {
if (eobj.value == 'Hide')
{
document.getElementById('disp').value='';
//eobj.value == 'Show';
window.parent.document.all.Main.cols = '0,*';
}
if (eobj.value == 'Show')
{
document.getElementById('disp').value='';
eobj.value == 'Hide';
window.parent.document.all.Main.cols = '19,81';
}


}

// ]]>
</script>
</head>
<body>
    <form id="form1" runat="server" target="_top">
                
				<table width="100%" cellpadding="1" cellspacing="1">
				<tr>
				<td   class="SMSelected" align="center">				
                    <asp:Label ID="lHeader" runat="server" Text="Label"  Width="98%" ></asp:Label></td>
				<td style="text-align: center;" width="14px" class="SMSelected">				
				                <asp:LinkButton ID="Logout" runat="server" Font-Names="Tahoma" ForeColor="White" OnClick="Logout_Click" >Logout</asp:LinkButton>
				</td>
				</tr>
				</table>
								
        
    </form>
</body>
</html>
