<%@ Page Language="VB" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  
<script language="javascript">
function Done() {
window.close();
}
function doInit() {
var ParmA = "Aparm";
var ParmB = "Bparm";
var ParmC = "Cparm";
var MyArgs = new Array(ParmA, ParmB, ParmC);
MyArgs = window.dialogArguments;
tbParamA.value = MyArgs[0].toString();
tbParamB.value = MyArgs[1].toString();
tbParamC.value = MyArgs[2].toString();
}
</script>
</HEAD>
<BODY onload="doInit()">
<asp:HiddenField ID="tbParamA" runat="server" />
<asp:HiddenField ID="tbParamB" runat="server" />
<asp:HiddenField ID="tbParamC" runat="server" />
<BUTTON onclick="Done()" type="button">OK</BUTTON>
</BODY>
</HTML>
