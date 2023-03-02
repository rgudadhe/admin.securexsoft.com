<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DutyRosterFormNew.aspx.vb" Inherits="TechReports_DutyRosterFormNew" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">
<script type="text/javascript" language="JavaScript">
    function Test()
    {
        alert('Test');
        alert(window.event.srcElement.value);
        
        //alert(document.getElementById("Submit").)
        if (window.event.srcElement.value == "Get Weeks")
        {
            alert('Test');
            return true;
        }
        else
        {
            document.form2.target="bottom"
            document.form2.submit 
            document.form2.action="DutyRosterResultNew.aspx";
            return true;
        }

        //document.form2.action="DutyRosterResultNew.aspx";
        //document.form2.submit(); 
        //document.form2.target="bottom";
        
        //return false
    }
    
    function ChangeAll(formObj) 
	{
	    var elval
		if (document.form2.All.checked) 
		{
			elval = true;
		} 
		else 
		{
			elval = false;
		}
		for (var i=0;i < formObj.length;i++) 
		{
			fldObj = formObj.elements[i];
			if (fldObj.type == 'checkbox')
			{ 
				fldObj.checked =  elval
			}
		}
	}
	
	function ChkValidation()
	{
	    var varCount 
	    varCount=0
	   if (GetVal()!= null )
	   {
	        for (var i=0;i < form2.length;i++) 
		    {
			    fldObj = form2.elements[i];
			    if (fldObj.type == 'checkbox')
			    { 
				    if (fldObj.checked)
				        varCount = varCount + 1    
			    }
		    }  
		    if (varCount == 0)
		    {
		        alert('Please select employee for updation')
		        return false;
		    }
	   }
	   else
	   {
	        alert('Please select shift for updation')
	        return false;
	   }
	   return true;
	}
	
	function GetVal()
    {
        var a = null;
        var f = document.forms[0];
        var e = f.elements["RadioShift"];

        for (var i=0; i < e.length; i++)
        {
            if (e[i].checked)
            {
                a = e[i].value;
                break;
            }
        }
        return a;
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <LINK href= "../../styles/Report.css" type="text/css" rel="stylesheet">
    <LINK href= "../../styles/Default.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form2" runat="server">
    <div>
        <asp:Table ID="Table2" runat="server" HorizontalAlign=Center>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:RequiredFieldValidator ID="RequiredFieldMonth" runat="server" ErrorMessage="Please select month" Font-Names="Trebuchet MS" Font-Italic=true Font-Size=Small ControlToValidate="DropDownMonth"></asp:RequiredFieldValidator>
                    <BR><asp:RequiredFieldValidator ID="RequiredFieldYear" runat="server" ErrorMessage="Please select year" Font-Names="Trebuchet MS" Font-Italic=true Font-Size=Small ControlToValidate="DropDownYear"></asp:RequiredFieldValidator>
                    <asp:Table ID="Table1" runat="server" Font-Names="Trebuchet MS" Font-Italic=true ForeColor=Gray Font-Size=Small GridLines=Both>
                        <asp:TableRow CssClass="SMSelected">
                            <asp:TableCell HorizontalAlign=Center>
                                Select Month Year
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                Month
                                <asp:DropDownList ID="DropDownMonth" Font-Names="Trebuchet MS" Font-Size=Small runat="server" Width=100>
                                </asp:DropDownList>
                                Year
                                <asp:DropDownList ID="DropDownYear" Font-Names="Trebuchet MS" Font-Size=Small runat="server" Width=100>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign=Center >
                                <asp:Button ID="Submit" runat="server" Text="Get Weeks" Font-Names="Trebuchet MS" UseSubmitBehavior=false Font-Size=Small />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
                <asp:TableCell>
                    
                </asp:TableCell>
                <asp:TableCell>
                
                </asp:TableCell>
                <asp:TableCell>
                
                </asp:TableCell>
                <asp:TableCell>
                    <BR><BR>
                    
                        <asp:Table ID="tblWeeks" runat="server" Font-Names="Trebuchet MS" Font-Italic=true ForeColor=Gray Font-Size=Small GridLines=Both>
                            <asp:TableRow CssClass="SMSelected">
                                <asp:TableCell HorizontalAlign=Center>
                                    Select Weeks 
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>
                                    StartWeek
                                    <asp:DropDownList ID="DropDownStartWeek" Font-Names="Trebuchet MS" Font-Size=Small runat="server" Width=100>
                                    </asp:DropDownList>
                                    EndWeek
                                    <asp:DropDownList ID="DropDownEndWeek" Font-Names="Trebuchet MS" Font-Size=Small runat="server" Width=100>
                                    </asp:DropDownList>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell HorizontalAlign=Center>
                                    <asp:Button ID="BtnSubmit" Font-Names="Trebuchet MS" Font-Size=Small runat="server" UseSubmitBehavior=true Enabled=false Text="Submit" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>   
        <BR>
        <hr>            
        <BR>
        <center><asp:Label ID="lblStatus" ForeColor=red runat="server" Font-Names="Trebuchet MS" Font-Size=Small Text=""></asp:Label></center>
        <asp:Table ID="tblMainResult" runat="server" HorizontalAlign=Center Font-Names="Trebuchet MS" Font-Size=Small GridLines=Both Visible=false Width=80%>
            <asp:TableRow CssClass="HeaderDiv">
                <asp:TableCell HorizontalAlign=Center>
                    Duty Roster
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign=Left>
                    <asp:RadioButtonList ID="RadioShift" runat="server">
                        
                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="tblRowShift" CssClass="SMSelected">
                <asp:TableCell>
                    <asp:CheckBox ID="All" onclick="javascript:ChangeAll(form2);" runat="server" />    
                </asp:TableCell>
                <asp:TableCell>
                    Employee Name
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table><BR>
        <center><asp:Button ID="BtnUpdateShift" Visible=false runat="server" Font-Names="Trebuchet MS" Text="Submit" OnClientClick="return ChkValidation();" /></center>
    </div>
    </form>
</body>
</html>
