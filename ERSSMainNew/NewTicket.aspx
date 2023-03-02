<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NewTicket.aspx.vb" Inherits="ERSSMainNew_NewTicket" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>New Ticket</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />
    <script language=javascript type="text/javascript" >

    var xmlHttp
    function GetData()
    {
        var varSelIndex = document.getElementById("DropDownCate").selectedIndex;
        var varValue=document.getElementById("DropDownCate").options(varSelIndex).value;
        RemoveSelectItems()
        
        xmlHttp=GetXmlHttpObject()

        if (xmlHttp==null)
        {
            alert("Browser does not suupport HTTP Request")
            return;
        }

        var url="GetIssueTypes.aspx"
        url=url+"?Cate='"+varValue+"'&IssueID=true";
       
        xmlHttp.onreadystatechange=stateChanged;
        xmlHttp.open("GET",url,true)
        xmlHttp.send(null)
    }
    
    function stateChanged()
    {
        if (xmlHttp.readyState==4 ||xmlHttp.readyState=="complete")
        {
            var response=xmlHttp.responseText;
            response=response.substring(0,response.indexOf("<!DOCTYPE")-4);
            
            if(response=="Empty")
            {
                alert("No Record Found !!!");
            }
            else if(response=='Error')
            {
                alert("An Error occured in accessing the DataBase !!!");
            }
            else
            {   
                //RemoveItems()
                DeleteAllRows()
                AddAllRows(response)
            }
        }
    }
    function DeleteAllRows()
    {
        if (tblIssues.rows.length > 2)
        {
            for(i=tblIssues.rows.length-1; i > 1; i--)
            {
                tblIssues.deleteRow(i)
            }   
        }
    }
    function AddAllRows(Str)
    {   
        var tbl = document.getElementById("tblIssues").getElementsByTagName("tbody")[0];
        var Issues=new Array()
        var TempIssue = new Array()
        var IssueType 
        var IssueDescription 
        var IssueID
        var Issues=Str.split("$")
        
        for (i=0;i<Issues.length-1;i++)
        {
            TempIssue=Issues[i].split("#")
            IssueType=TempIssue[0]
            IssueDescription=TempIssue[1]
            IssueID=TempIssue[2]
            
            var varTblRow =document.createElement("TR")
            var varTblCol1,varTblCol2,varInput,varnewInput,varnewInput1
            
            varTblRow.style.cssText="font-family: 'Arial';color:Gray;font-size=12;"
            varTblCol1=document.createElement("TD")
            varTblCol1.width=100
            
            varTblCol1.setAttribute("align","left");
            varTblCol1.setAttribute("style","font-family: 'Arial';color:Gray;")
			varstr="<label>"+ IssueType +"</label>";
			varTblCol1.innerHTML = varstr;
            varTblRow.appendChild(varTblCol1)
            
            varTblCol2=document.createElement("TD")
            varTblCol2.setAttribute("align","left");
            varTblCol2.setAttribute("style","font-family: 'Arial';color:Gray;")
			varstr="<label>"+ IssueDescription +"</label>";
			varTblCol2.innerHTML = varstr;
            varTblRow.appendChild(varTblCol2)

            tbl.appendChild(varTblRow)
            
            //document.getElementById("DropDownIssueType").options[i] = new Option(IssueType,IssueID);

        }
    }
    function RemoveItems()
    {
        while(document.getElementById("DropDownIssueType").length > 0)
        {
            document.getElementById("DropDownIssueType").remove(0);
        }
    }
    function RemoveSelectItems()
    {
        if (document.getElementById("DropDownCate").length > 0)
        {   
            if (document.getElementById("DropDownCate").item(0).value =='')
            {
                document.getElementById("DropDownCate").remove(0);
            }
        }
    }
    
    function RemoveSelectItemsIssues()
    {
        if (document.getElementById("DropDownList2").length > 0)
        {   
            if (document.getElementById("DropDownList2").item(0).value =='')
            {
                document.getElementById("DropDownList2").remove(0);
            }
        }
    }
    function RemoveSelectItemsPriority()
    {
        if (document.getElementById("DropDownPriority").length > 0)
        {   
            if (document.getElementById("DropDownPriority").item(0).value =='')
            {
                document.getElementById("DropDownPriority").remove(0);
            }
        }
    }
    function GetXmlHttpObject()
    {
        var objXMLHttp = null
        if (window.XMLHttpRequest)
        {
            objXMLHttp=new XMLHttpRequest()
        }
        else if(window.ActiveXObject)
        {
            objXMLHttp=new ActiveXObject("Microsoft.XMLHTTP")
        }
        return objXMLHttp
    }

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:Table ID="Table1" runat="server">
                <asp:TableRow ID="TableRow1" runat="server">
                    <asp:TableCell ID="TableCell1" ColumnSpan="2" runat="server" cssClass="HeaderDiv">
                        Raise Ticket
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server" >
                    <asp:TableCell ID="TableCell2" runat="server" Width="150px" HorizontalAlign="right">
                        <asp:Label ID="Label1" CssClass="common" runat="server" Text="Issue Category "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell3" runat="server">                                            
                        <asp:DropDownList runat="server" ID="DropDownCate" Width="200" AutoPostBack="true" OnSelectedIndexChanged="DropDownCate_SelectedIndexChanged" OnChange="javascript:GetData();" CssClass="common">
                        </asp:DropDownList>&nbsp &nbsp
                        <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldDropDownCate" runat="server" ErrorMessage="Please select Issue Category" ControlToValidate="DropDownCate"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow3" runat="server">
                    <asp:TableCell ID="TableCell4" runat="server" HorizontalAlign="Right">
                        <asp:Label ID="Label2" runat="server" Text="Issue Type "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell5" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
                            <ContentTemplate>
                                <asp:DropDownList ID="DropDownList2" Width="200" runat="server" onChange="RemoveSelectItemsIssues()" CssClass="common">
                                </asp:DropDownList>&nbsp;&nbsp;
                                <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldDropDownList2" ControlToValidate="DropDownList2" runat="server"  SetFocusOnError="true" ErrorMessage="Please select Issue Type"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DropDownCate" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow4" runat="server">
                    <asp:TableCell ID="TableCell6" runat="server" HorizontalAlign="right">
                        <asp:Label ID="Label3" runat="server" Text="Issue Details " CssClass="common"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell7" HorizontalAlign="Left" runat="server">
                        <textarea id="TextAreaIssueDetails" cols="70" rows="6" class="common" runat="server"></textarea>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Right">
                        <asp:Label ID="Label4" runat="server" Text="Priority "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="Left">
                        <asp:DropDownList ID="DropDownPriority" runat="server" Width="150" OnChange="RemoveSelectItemsPriority()" CssClass="common">
                            <asp:ListItem Selected="True" Text="Please select" Value=""></asp:ListItem>
                            <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                            <asp:ListItem Text="High" Value="High"></asp:ListItem>
                            <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                        </asp:DropDownList>&nbsp &nbsp
                        <asp:RequiredFieldValidator  Display="None" ID="RequireFieldDropDownPriority" ControlToValidate="DropDownPriority" SetFocusOnError="true" runat="server" ErrorMessage="Please select Priority"></asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center"  >
                        <center>
                            <asp:Button ID="BtnSubmit" CssClass="button" UseSubmitBehavior="true" runat="server" Text="Submit" />
                        </center>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <BR>
            <asp:RequiredFieldValidator  Display="None" ID="RequiredFieldTextAreaIssueDetails" ControlToValidate="TextAreaIssueDetails" runat="server" SetFocusOnError="true"  ErrorMessage="Please enter Issue Details"></asp:RequiredFieldValidator>
            <BR> 
            <asp:Table ID="tblIssues" runat="server" Width="100%">
                <asp:TableRow ID="TableRow5"  runat="server">
                    <asp:TableCell ID="TableCell8" HorizontalAlign="Center" CssClass="HeaderDiv" ColumnSpan="2" runat="server">
                        Issues Details
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow6" runat="server">
                    <asp:TableCell ID="TableCell9" runat="server" CssClass="alt1" HorizontalAlign="Center" Width="100px">
                        Issue Type
                    </asp:TableCell>
                    <asp:TableCell ID="TableCell10" runat="server" CssClass="alt1" HorizontalAlign="Center">
                        Description
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> </form>
</body>
</html>
