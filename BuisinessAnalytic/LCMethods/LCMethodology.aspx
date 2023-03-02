<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LCMethodology.aspx.vb" Inherits="Billing_LCMethods_LCMethodology" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>UnitCount Methodology</title>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/styles.css"/>
    <link rel="Stylesheet" type="text/css" href="../../App_Themes/Css/Common.css"/>
</head>
<body>
    <form id="form1" runat="server">
        
    
    
        <div id="body">
        <div id="cap"></div>
        <div id="main">
        <h1>UnitCount Methodology </h1>
        <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left">
            <div>
            <asp:Label ID="lblDisp" runat="server" CssClass="Title" ForeColor="#C00000"></asp:Label>
                    
        <asp:Table ID="Table1" runat="server" Width="500" CssClass="common">
        <asp:TableRow ID="Row1" HorizontalAlign="Center" runat="server" >
        <asp:TableCell ID="Cell1" runat="server" CssClass="alt1" >Name
        </asp:TableCell>
        <asp:TableCell ID="TableCell1"  CssClass="alt1" runat="server" >Description
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow1" HorizontalAlign="Center" runat="server">
        <asp:TableCell ID="TableCell2" HorizontalAlign="Center" runat="server" Width="50%" ><asp:TextBox ID="TxtName" CssClass="common" runat="server" Width="70%" ></asp:TextBox>
        </asp:TableCell> 
        <asp:TableCell ID="TableCell3"  HorizontalAlign="Center" runat="server" Width="50%" ><asp:TextBox ID="TxtDescr" CssClass="common" runat="server" Width="70%"></asp:TextBox>
        </asp:TableCell>
      </asp:TableRow> 
        </asp:Table>
        <br />
        <asp:Panel ID="Panel1" runat="server">                
            &nbsp;<span class="common">UnitCount Method:</span>
            <asp:DropDownList ID="DLMethod" runat="server"  AutoPostBack="True" CssClass="common">
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
       
        <asp:TableRow ID="TableRow4" HorizontalAlign="Center" runat="server">
        <asp:TableCell ID="TableCell7"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="alt5" >
            <asp:CheckBox ID="CHheader" Text="Header"  runat="server" />
        </asp:TableCell> 
        <asp:TableCell  ID="TableCell8"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="alt5">
        <asp:CheckBox ID="CHBIU" Text="Formatted (BIU) Characters (All)"  runat="server" />
        </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow ID="TableRow6" HorizontalAlign="Center" runat="server">
        <asp:TableCell ID="TableCell10"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
        <asp:CheckBox ID="CHfooter" Text="Footer"  runat="server" />
        </asp:TableCell> 
        <asp:TableCell ID="TableCell11"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
        <asp:CheckBox ID="CHSchars" Text="Shifted Characters (On/Off)"  runat="server" />
        </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow ID="TableRow7" HorizontalAlign="Center" runat="server" >
        <asp:TableCell ID="TableCell12"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
         <asp:CheckBox ID="CHbody" Text="Body Of the Document"  runat="server" />
        </asp:TableCell> 
        <asp:TableCell ID="TableCell13"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
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
        <asp:TableRow ID="TableRow8" HorizontalAlign="Center" runat="server" >
        <asp:TableCell ID="TableCell18"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
        Shifted Characters (All) Value (InHealth) <asp:TextBox ID="txtBIUShiftedAll" runat="server" CssClass="common" Columns="4" ></asp:TextBox>
        </asp:TableCell> 
         <asp:TableCell ID="TableCell5"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
        Formatted (BIU) Value (InHealth) <asp:TextBox ID="txtBIUVal" runat="server" CssClass="common" Columns="4" ></asp:TextBox>
        </asp:TableCell> 
        </asp:TableRow>
        <asp:TableRow ID="TableRow9" HorizontalAlign="Center" runat="server" >
        <asp:TableCell ID="TableCell14"  HorizontalAlign="Left" runat="server" Width="50%" CssClass="common">
        Characters Per Unit <asp:TextBox ID="TXcpl" runat="server" CssClass="common" Columns="4" ></asp:TextBox>
        </asp:TableCell>  
         
        </asp:TableRow>
        <asp:TableRow ID="TableRow5" HorizontalAlign="Center" runat="server">
        <asp:TableCell ID="TableCell9" ColumnSpan="2" runat="server" >
            <center>
        <asp:Button ID="Button2" runat="server"  CssClass="button" Text="Submit"   />
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
