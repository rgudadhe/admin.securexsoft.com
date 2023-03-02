<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DictSearch_SPN.aspx.vb" Inherits="ets.Dictation_Search_DictSearch" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet" />
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet" />

    <title>Dictation Search</title>
    <script language="javascript" type="text/javascript">
function validate()
{

var str = document.getElementById("UserID").value;
str = str.replace(/^[\s]+/,'').replace(/[\s]+$/,'').replace(/[\s]{2,}/,' ');
var str1 = document.getElementById("UserName").value;
str1 = str1.replace(/^[\s]+/,'').replace(/[\s]+$/,'').replace(/[\s]{2,}/,' ');
      if (str=="" && str1=="")
      {                
                document.getElementById("Level").disabled = true;
      } else {                
                document.getElementById("Level").disabled = false;                         
      }     
}
function CheckAllDataGridCheckBoxes(aspCheckBoxID, checkVal)
 {
  re = new RegExp(':' + aspCheckBoxID + '$')  //generated control name starts with a colon
  for(i = 0; i < form1.elements.length; i++)
  {
   elm = document.forms[0].elements[i]
   if (elm.type == 'checkbox')
   {
    //if (re.test(elm.name))
     elm.checked = checkVal
   }
  }
 }
</script>
 
</head>
<body>
    <form id="form1" method="post" target="DictationResult" runat="server"> 
    <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1">
    </ajaxToolkit:ToolkitScriptManager>
    
    <div id="body">
        <div id="cap"></div>
            <div id="main" style="text-align:left ">
                <h1>Dictation Search</h1>
                    <asp:UpdatePanel runat="server" ID="up2" >
                        <ContentTemplate>
                            <table style="text-align:left">  
                                <tr>
                                    <td class="alt1">Tracking Job#</td>
                                    <td class="alt1">Cutomer Job#</td>
                                    <td class="alt1">Phy. First</td>
                                    <td class="alt1">Phy. Last</td>
                                    <td class="alt1">
                                        <asp:TextBox ID="lblOp1" runat="server" ReadOnly="true"  BorderStyle="None" BackColor="#F5F5F5" ForeColor="#000000" CssClass="common" Font-Bold="true" style="text-align: center;"  />   
                                        <%--<asp:Label ID="lblOp1" runat="server" Text=""></asp:Label> --%>                                
                                        <asp:DropDownList ID="DDType" runat="server"  OnSelectedIndexChanged="DDType_SelectedIndexChanged" AutoPostBack="true" Visible="false">                                            
                                        </asp:DropDownList>                    
                                        <asp:Button ID="iPopUp" runat="server" Text="..." OnClick="iPopUp_Click" ToolTip="Click here to change search option" CssClass="button" Height="18px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="Track" runat="server" Width="130px" TabIndex="1"></asp:TextBox></td>
                                    <td><asp:TextBox ID="Cust" runat="server" Width="130px" TabIndex="2"></asp:TextBox></td>
                                    <td><asp:TextBox ID="PFirst" runat="server" Width="130px" TabIndex="3"></asp:TextBox></td>
                                    <td><asp:TextBox ID="PLast" runat="server" Width="130px" TabIndex="4"></asp:TextBox></td>                
                                    <td><asp:TextBox ID="valOp1" runat="server" Width="130px" TabIndex="5"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td class="alt1">Start Date</td>
                                    <td class="alt1">End Date</td>
                                    <td class="alt1">Dictation Code</td>
                                    <td class="alt1">Account Name</td>
                                    <td class="alt1">
                                        <asp:TextBox ID="lblOp2" runat="server" ReadOnly="true"  BorderStyle="None" BackColor="#F5F5F5" ForeColor="#000000" CssClass="common" Font-Bold="true" style="text-align: center; vertical-align:middle;"  />
                                        <%--<asp:Label ID="lblOp2" runat="server" Text=""></asp:Label>--%>
                                        <asp:DropDownList ID="DDType1" runat="server"  OnSelectedIndexChanged="DDType1_SelectedIndexChanged" AutoPostBack="true" Visible="false">                               
                                        </asp:DropDownList>                   
                                        <asp:Button ID="iPopUP1" runat="server" Text="..." OnClick="iPopUp1_Click" ToolTip="Click here to change search option" CssClass="button" Height="18px"  />
                                    </td>
                                </tr>  
                                <tr>
                                    <td><asp:TextBox ID="sDate" runat="server" Width="120px" TabIndex="6"></asp:TextBox>             
                                        <asp:ImageButton ID="ImgBntsDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" CausesValidation="False" />         
                                    </td>
                                    <td><asp:TextBox ID="eDate" runat="server" Width="120px" TabIndex="7"></asp:TextBox>
                                        <asp:ImageButton ID="ImgBnteDate" runat="server" ImageUrl="~/images/Calendar_scheduleHS.png" CausesValidation="False" />         
                                    </td>                
                                    <td><asp:TextBox ID="DCode" runat="server" Width="130px" TabIndex="8"></asp:TextBox></td>
                                    <td><asp:TextBox ID="AccName" runat="server" Width="130px" TabIndex="9"></asp:TextBox></td>                
                                    <td><asp:TextBox ID="valOp2" runat="server" Width="130px" TabIndex="10"></asp:TextBox></td>
                                </tr>  
                                <tr>                                
                                    <td class="alt1">Job Status</td>
                                    <td class="alt1">User ID</td>
                                    <td class="alt1">User Name</td>
                                    <td class="alt1">User Level</td>
                                    <td class="alt1">
                                        <asp:TextBox ID="lblOp3" runat="server"  ReadOnly="true"  BorderStyle="None" TabIndex="100" BackColor="#F5F5F5" ForeColor="#000000" CssClass="common" Font-Bold="true" style="text-align: center;" />                                    
                                        <%--<asp:Label ID="lblOp3" runat="server" Text=""></asp:Label>--%>
                                        <asp:DropDownList ID="DDType2" runat="server"  OnSelectedIndexChanged="DDType2_SelectedIndexChanged" AutoPostBack="true" Visible="false">                
                                        </asp:DropDownList>                   
                                        <asp:Button ID="iPopUp2" runat="server" Text="..." OnClick="iPopUp2_Click" ToolTip="Click here to change search option" CssClass="button" Height="18px" />
                                     </td>                       
                                </tr>
                                <tr>                
                                    <td>
                                        <asp:DropDownList ID="UStatus" runat="server" Width="140px" TabIndex="11" ></asp:DropDownList>
                                    </td>
                                    <td><asp:TextBox ID="UserID" runat="server" Width="130px" TabIndex="12"></asp:TextBox>
                                    </td>
                                    <td><asp:TextBox ID="UserName" runat="server" Width="130px" TabIndex="13"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="Level" runat="server" Width="130px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="valOp3" runat="server" Width="130px" TabIndex="14"></asp:TextBox>
                                    </td>
                                </tr> 
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" 
                                            MinimumPrefixLength="1" 
                                            CompletionSetCount="10" 
                                            runat="server" 
                                            TargetControlID="UserID"
                                            ServicePath="../users/autocomplete.asmx"
                                            ServiceMethod="GetUserID" EnableCaching="true"/>
                                    <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteSearch" 
                                            MinimumPrefixLength="1" 
                                            CompletionSetCount="10" 
                                            runat="server" 
                                            TargetControlID="UserName"
                                            ServicePath="../users/autocomplete.asmx"
                                            ServiceMethod="GetCompletionList" EnableCaching="true"/> 
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="sDate" PopupButtonID="ImgBntsDate" />         
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="eDate" PopupButtonID="ImgBnteDate" />
                                </table>
                            </ContentTemplate>        
                        </asp:UpdatePanel> 
                        <table style="text-align:left">   
                            <tr><td style="border:0">
                                <input type="submit" id="SEARCH" name="SEARCH" value="Search" language="javascript" onclick="return SEARCH_onclick()" class="button" />
                            </td></tr>     
                        </table>
                        </div> 
                        </div> 
                        <iframe src="DictResult.aspx" id="DictationResult" Name="DictationResult" frameborder="0" style="width: 100%; height: 60%; "></iframe>
   </form>
</body>
</html>
