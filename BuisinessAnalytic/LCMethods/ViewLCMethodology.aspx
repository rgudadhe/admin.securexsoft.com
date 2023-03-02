<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewLCMethodology.aspx.vb" Inherits="ViewLCMethodology" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View UnitCount Methodology</title>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/styles.css"/>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>View UnitCount Methodology</h1>
            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
                <div>
                    <asp:Label ID="lblDisp" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>
                    <asp:Table ID="Table1" runat="server" Width="500">
                        <asp:TableRow ID="Row1" runat="server">
                            <asp:TableCell  BorderStyle="None" ID="Cell1"  HorizontalAlign="Right" runat="server" CssClass="common">
                                <div style="text-align:right">Name</div>
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell1"  BorderStyle="None" HorizontalAlign="Left" runat="server" > 
                                <asp:DropDownList ID="DLLCMethod" runat="server" CssClass="common" AutoPostBack="True" >
                                    <asp:ListItem Text="Any" value="" selected="True" ></asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow1" runat="server">
                            <asp:TableCell ID="TableCell2" BorderStyle="None" HorizontalAlign="Right" CssClass="common" runat="server" Width="20%" >
                                <div style="text-align:right">Description</div>
                            </asp:TableCell> 
                            <asp:TableCell ID="TableCell3" BorderStyle="None" HorizontalAlign="Left" runat="server" Width="80%" >
                                <asp:TextBox ID="TxtDescr" runat="server" Width="95%" CssClass="common" ></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <br />
                    <asp:Panel ID="Panel1" runat="server">                
                        <span class="common">
                            <strong>UnitCount Method</strong>
                        </span>
                        <asp:DropDownList ID="DLMethod" runat="server" CssClass="common" AutoPostBack="True" >
                            <asp:ListItem Text="Pages" Value="Pages"></asp:ListItem> 
                            <asp:ListItem Text="Characters Per Line" Value="CharsPerLine"></asp:ListItem> 
                            <asp:ListItem Text="Words" Value="Words"></asp:ListItem> 
                            <asp:ListItem Text="Gross Lines" Value="GrossLines"></asp:ListItem> 
                            <asp:ListItem Text="All Lines" Value="AllLines"></asp:ListItem> 
                            <asp:ListItem Text="Per Dictator" Value="PerDictator"></asp:ListItem>
                            <asp:ListItem Text="Per Report" Value="PerReport"></asp:ListItem>
                            <asp:ListItem Text="Minutes" Value="Minutes"></asp:ListItem> 
                        </asp:DropDownList>
                    </asp:Panel>
                    <asp:Table ID="Table2" runat="server" Width="600" CssClass="common">
                        <asp:TableRow ID="TableRow4" runat="server">
                            <asp:TableCell ID="TableCell7" CssClass="alt5" HorizontalAlign="Center" runat="server" Width="50%" >
                                <asp:CheckBox ID="CHheader" Text="Header" CssClass="common" runat="server" />
                            </asp:TableCell> 
                            <asp:TableCell ID="TableCell8" CssClass="alt5" HorizontalAlign="Center" runat="server" Width="50%" >
                                <asp:CheckBox ID="CHBIU" Text="Formatted (BIU) Characters" CssClass="common" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow6" runat="server">
                            <asp:TableCell ID="TableCell10" HorizontalAlign="Center" runat="server" Width="50%" >
                                <asp:CheckBox ID="CHfooter" CssClass="common" Text="Footer"  runat="server" />
                            </asp:TableCell> 
                            <asp:TableCell ID="TableCell11" HorizontalAlign="Center" runat="server" Width="50%" >
                                <asp:CheckBox ID="CHSchars" CssClass="common" Text="Shifted Characters"  runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow7" runat="server">
                            <asp:TableCell ID="TableCell12" HorizontalAlign="Center" runat="server" Width="50%" >
                                <asp:CheckBox ID="CHbody" CssClass="common" Text="Body Of the Document"  runat="server" />
                            </asp:TableCell> 
                            <asp:TableCell ID="TableCell13" HorizontalAlign="Center" runat="server" Width="50%" >
                                <asp:CheckBox ID="CHspaces" Text="Spaces"  runat="server" AutoPostBack="True" />
                            </asp:TableCell>
                        </asp:TableRow>
                         <asp:TableRow ID="TableRow2" HorizontalAlign="Center" runat="server" >
        <asp:TableCell ID="TableCell4"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
         <asp:CheckBox ID="CHBIUOnOff" Text="Formatted (BIU) Characters (On/Off) (InHealth)"  runat="server" />
        </asp:TableCell> 
        <asp:TableCell ID="TableCell15"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
        <asp:CheckBox ID="CHsrt" Text="Spaces, Carriage Returns, Tab" CssClass="common" runat="server" AutoPostBack="True" />
        </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow ID="TableRow3" HorizontalAlign="Center" runat="server" >
        <asp:TableCell ID="TableCell6"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
         <asp:CheckBox ID="CHScharsAll" Text="Shifted Characters (All) (InHealth)"  runat="server" />
        </asp:TableCell> 
        <asp:TableCell ID="TableCell16"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
        <asp:CheckBox ID="CHDocVariable" Text="Document Veriables (Inhealth)"  runat="server" AutoPostBack="True" />
        </asp:TableCell>
        </asp:TableRow>
                        <asp:TableRow ID="TableRow8" runat="server">
                            <asp:TableCell ID="TableCell18"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
                            Shifted Characters (All) Value (InHealth) <asp:TextBox ID="txtBIUShiftedAll" runat="server" CssClass="common" Columns="4" ></asp:TextBox>
                            </asp:TableCell> 
                            <asp:TableCell ID="TableCell5"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
                            Formatted (BIU) Value (InHealth) <asp:TextBox ID="txtBIUVal" runat="server" CssClass="common" Columns="4" ></asp:TextBox>
                            </asp:TableCell> 
                        </asp:TableRow>
                         <asp:TableRow ID="TableRow9" runat="server">
                             <asp:TableCell ID="TableCell14" HorizontalAlign="Center" runat="server" Width="50%" >
                                Characters Per Unit <asp:TextBox ID="TXcpl" runat="server" CssClass="common" Columns="4" ></asp:TextBox>
                            </asp:TableCell>  
                           
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow5" runat="server">
                            <asp:TableCell ID="TableCell9" ColumnSpan="2" runat="server" >
                                <center>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" />
                                </center>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:HiddenField ID="HUserID" runat="server" />
                </div>
            </asp:Panel>
        </div> 
        </div> 
    </form>
</body>
</html>
