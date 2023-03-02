<%@ Page Language="VB" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>

<!------------------------------------------------------------------>
<!--                                                              -->
<!-- one-click.htm - sample page for                              -->
<!--                 One-Click Install trigger                    -->
<!--                                                              -->
<!-- Copyright © 2001-2002 InstallShield Software Corporation     -->
<!--                                                              -->

<title>One-Click&trade; Install - SecureXSoft SecureMT Client Setup</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252;"/>
<%--<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/nav4-0">
--%>
<style media="all" type="text/css">
BODY {
    font-family: "Verdana", "Arial", sans-serif;
    font-size: xx-small;
    color: black;
    background-color: white;
    margin: 0em;
}

IMG {
    border: none;
}

P {
    font-size: xx-small;
}

A {
    color: #002B55;
}

HR {
    width: 100%;
    height: 1px;
    color: #002B55;
}

TD {
    text-align: left;
    vertical-align: top;
}

.upperleft {
    background-image: url(UL.gif);
    background-position: top left;
    background-repeat: no-repeat;
}

.upperright {
    background-image: url(UR.gif);
    background-position: top right;
    background-repeat: no-repeat;
}

.lowerleft {
    background-image: url(LL.gif);
    background-position: bottom left;
    background-repeat: no-repeat;
}

.lowerright {
    background-image: url(LR.gif);
    background-position: bottom right;
    background-repeat: no-repeat;
}
</style>

</head>
<body>
    <form id="form1" runat="server">
    
        <%--<area coords="470,65,535,90" href="http://support.installshield.com/help/oneclick/defaulthelp.htm"--%>
            <%--shape="RECT" target="_blank" />--%>
        <table border="0" cellpadding="0" cellspacing="0" style="background-image: url(header_fill.jpg);
            width: 100%; background-repeat: repeat-x; height: 120px">
            <tr>
                <td style="width: 100%" align="center">
                    <img border="0" src="logo.jpg"/></td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <center>
            <table border="0" cellpadding="10" cellspacing="0" style="margin-top: 1em; width: 60%;
                background-color: #edf1f4">
                <tr>
                    <td class="upperleft">
                        <span style="font-size: 13pt; color: #002b55">SecureXSoft SecureMT Client Setup</span>
                    </td>
                    <td class="upperright" style="width: 142px">
                        <a href="<%=iif(Bit=32,"SecureXSoft MTClient-32bit.exe","SecureXSoft MTClient-64bit.exe")%>">
                            <img id="imgOCI" border="0" height="25" src="b_install.gif" width="142" /></a></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr noshade="noshade" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <p style="color: #002b55">
                            <b><span id="txtSize">43 MB</span></b>
                            <br />
                            <b><span id="txtModemTime">2 hours 18 minutes 54 seconds</span></b> over a dial-up
                            connection
                            <br />
                            <b><span id="txtBroadbandTime">15 minutes 55 seconds</span></b> over a broadband
                            connection
                        </p>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="lowerleft">
                        <p style="margin-top: 3em">
                            Click <a href="<%=iif(Bit=32,"SecureXSoft MTClient-32bit.exe","SecureXSoft MTClient-64bit.exe")%>"><span style="color: #002b55">Install</span></a>
                            to start this setup now.
                        </p>
                    </td>
                    <td class="lowerright">
                        &nbsp;</td>
                </tr>
            </table>
        </center>
    
    
    </form>
</body>
</html>
