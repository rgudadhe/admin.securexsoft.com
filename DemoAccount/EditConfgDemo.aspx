<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditConfgDemo.aspx.vb" Inherits="DemoAccount_ConfgDemo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
    <title>Edit Configured Demo</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />

   <script type="text/javascript" language="javascript">
var newwindow;
function poptastic(inpt, inpt1)
{
    url="removea.aspx?RecordID="+ inpt + "&ActID="+ inpt1;
    //alert(inpt);
    
	newwindow=window.open(url,'name','height=100,width=400, left=300, top=100');
	if (window.focus) {newwindow.focus()}
}

</script>   
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:Table ID="Table5" runat="server" Width="100%">
            <asp:TableRow runat="server" Style="text-align: center">
                <asp:TableCell runat="server" ColumnSpan="3" CssClass="HeaderDiv">Account Details</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" CssClass="alt1">Account Name</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1">Description</asp:TableCell>
                <asp:TableCell runat="server" CssClass="alt1">Account Number</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
         <asp:Table ID="Table2" runat="server" Width="100%" >
             <asp:TableRow ID="TableRow37" runat="server" Style="text-align: center">
                <asp:TableCell ID="TableCell71" CssClass="HeaderDiv" runat="server" ColumnSpan="3">Assigned Attribute Details</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow38" runat="server">
                <asp:TableCell ID="TableCell72" runat="server" Width="60%" CssClass="alt1" >Attribute Name</asp:TableCell>
                <asp:TableCell ID="TableCell73" runat="server" Width="20%" CssClass="alt1">Size</asp:TableCell>
                 <asp:TableCell ID="TableCell74" runat="server" Width="20%" CssClass="alt1">Remove</asp:TableCell>
                  
            </asp:TableRow>
            </asp:Table>
            <br />
            <br />
            
        <asp:Table ID="Table1" runat="server" Width="100%" >
            <asp:TableRow ID="TableRow1" runat="server" Style="text-align: center">
                <asp:TableCell ID="TableCell1" runat="server" ColumnSpan="3" CssClass="HeaderDiv">Add New Attribute</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell2" runat="server" Width="70%" CssClass="alt1" >Attribute Name</asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server" Width="20%" CssClass="alt1">Size</asp:TableCell>
            </asp:TableRow>
             <asp:TableRow ID="TableRow36" runat="server" >
                <asp:TableCell ID="TableCell70" runat="server" ColumnSpan="2">
              
             
                </asp:TableCell>
                
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat="server">
                <asp:TableCell ID="TableCell5" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList1" runat="server" >
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell7" runat="server">
                    <asp:TextBox Width="50" ID="TextBox1" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                   <asp:TableRow ID="TableRow4" runat="server">
                <asp:TableCell ID="TableCell3" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList2" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell6" runat="server">
                    <asp:TextBox Width="50" ID="TextBox2" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow5" runat="server">
                <asp:TableCell ID="TableCell8" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList3" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell9" runat="server">
                    <asp:TextBox Width="50" ID="TextBox3" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow6" runat="server">
                <asp:TableCell ID="TableCell10" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList4" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell11" runat="server">
                    <asp:TextBox Width="50" ID="TextBox4" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow7" runat="server">
                <asp:TableCell ID="TableCell12" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList5" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell13" runat="server">
                    <asp:TextBox Width="50" ID="TextBox5" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow8" runat="server">
                <asp:TableCell ID="TableCell14" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList6" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell15" runat="server">
                    <asp:TextBox Width="50" ID="TextBox6" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow9" runat="server">
                <asp:TableCell ID="TableCell16" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList7" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell17" runat="server">
                    <asp:TextBox Width="50" ID="TextBox7" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow10" runat="server">
                <asp:TableCell ID="TableCell18" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList8" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell19" runat="server">
                    <asp:TextBox Width="50" ID="TextBox8" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow11" runat="server">
                <asp:TableCell ID="TableCell20" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList9" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell21" runat="server">
                    <asp:TextBox Width="50" ID="TextBox9" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow12" runat="server">
                <asp:TableCell ID="TableCell22" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList10" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell23" runat="server">
                    <asp:TextBox Width="50" ID="TextBox10" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow13" runat="server">
                <asp:TableCell ID="TableCell24" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList11" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell25" runat="server">
                    <asp:TextBox Width="50" ID="TextBox11" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow14" runat="server">
                <asp:TableCell ID="TableCell26" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList12" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell27" runat="server">
                    <asp:TextBox Width="50" ID="TextBox12" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow15" runat="server">
                <asp:TableCell ID="TableCell28" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList13" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell29" runat="server">
                    <asp:TextBox Width="50" ID="TextBox13" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow16" runat="server">
                <asp:TableCell ID="TableCell30" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList14" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell31" runat="server">
                    <asp:TextBox Width="50" ID="TextBox14" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow17" runat="server">
                <asp:TableCell ID="TableCell32" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList15" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell33" runat="server">
                    <asp:TextBox Width="50" ID="TextBox15" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow18" runat="server">
                <asp:TableCell ID="TableCell34" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList16" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell35" runat="server">
                    <asp:TextBox Width="50" ID="TextBox16" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow19" runat="server">
                <asp:TableCell ID="TableCell36" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList17" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell37" runat="server">
                    <asp:TextBox Width="50" ID="TextBox17" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow20" runat="server">
                <asp:TableCell ID="TableCell38" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList18" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell39" runat="server">
                    <asp:TextBox Width="50" ID="TextBox18" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow21" runat="server">
                <asp:TableCell ID="TableCell40" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList19" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell41" runat="server">
                    <asp:TextBox Width="50" ID="TextBox19" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow22" runat="server">
                <asp:TableCell ID="TableCell42" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList20" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell43" runat="server">
                    <asp:TextBox Width="50" ID="TextBox20" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow23" runat="server">
                <asp:TableCell ID="TableCell44" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList21" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell45" runat="server">
                    <asp:TextBox Width="50" ID="TextBox21" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow24" runat="server">
                <asp:TableCell ID="TableCell46" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList22" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell47" runat="server">
                    <asp:TextBox Width="50" ID="TextBox22" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow25" runat="server">
                <asp:TableCell ID="TableCell48" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList23" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell49" runat="server">
                    <asp:TextBox Width="50" ID="TextBox23" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow26" runat="server">
                <asp:TableCell ID="TableCell50" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList24" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell51" runat="server">
                    <asp:TextBox Width="50" ID="TextBox24" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow27" runat="server">
                <asp:TableCell ID="TableCell52" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList25" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell53" runat="server">
                    <asp:TextBox Width="50" ID="TextBox25" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow28" runat="server">
                <asp:TableCell ID="TableCell54" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList26" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell55" runat="server">
                    <asp:TextBox Width="50" ID="TextBox26" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow29" runat="server">
                <asp:TableCell ID="TableCell56" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList27" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell57" runat="server">
                    <asp:TextBox Width="50" ID="TextBox27" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow30" runat="server">
                <asp:TableCell ID="TableCell58" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList28" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell59" runat="server">
                    <asp:TextBox Width="50" ID="TextBox28" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow31" runat="server">
                <asp:TableCell ID="TableCell60" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList29" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell61" runat="server">
                    <asp:TextBox Width="50" ID="TextBox29" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow32" runat="server">
                <asp:TableCell ID="TableCell62" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList30" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell63" runat="server">
                    <asp:TextBox Width="50" ID="TextBox30" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow33" runat="server">
                <asp:TableCell ID="TableCell64" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList31" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell65" runat="server">
                    <asp:TextBox Width="50" ID="TextBox31" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow34" runat="server">
                <asp:TableCell ID="TableCell66" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList32" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell67" runat="server">
                    <asp:TextBox Width="50" ID="TextBox32" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
                    <asp:TableRow ID="TableRow35" runat="server">
                <asp:TableCell ID="TableCell68" runat="server">
                    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="AnyClicked" ID="DropDownList33" runat="server">
                    </asp:DropDownList></asp:TableCell>
                <asp:TableCell ID="TableCell69" runat="server">
                    <asp:TextBox Width="50" ID="TextBox33" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
             
            
            
        </asp:Table>
        &nbsp;
        <div style="text-align:right">
            <asp:Button ID="btnAddAttribute" runat="server" Text="Add Attribute" CssClass="button" />
        </div>
        <br />
        <br />
        <div style="text-align:center">
            <asp:Button ID="btnConfgDemo" runat="server" Text="Configure Demo" CssClass="button" />
        </div>
        <br />
        <br />
        <asp:HiddenField ID="ACount" runat="server" />
        <asp:HiddenField ID="HActID" runat="server" />
        <asp:HiddenField ID="HFoldName" runat="server" />
        <asp:HiddenField ID="hdnDemoConfg" runat="server" />
    </div> 
    </form>
</body>
</html>
