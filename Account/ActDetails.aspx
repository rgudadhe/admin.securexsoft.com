<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ActDetails.aspx.vb" Inherits="Account_ActDetails" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href= "../styles/styles.css" type="text/css" rel="stylesheet" />
    <title>Account Details</title>
</head>
<body style="height:100%; ">
    <form id="form1" runat="server">
    <div id="body">
    <div id="cap"></div>
    <div id="main">
    <h1>View/Edit Account Data</h1>
      <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial" Font-Size="Small" ForeColor="Red"></asp:Label><span
          style="font-size: 10pt; font-family: Arial"> </span>
          <table   cellspacing="0" width="100%" id="mytable">
               <tr>
                <td colspan="4" class="HeaderDiv" style="text-align: center">
                                  Account Form       </td>
            </tr>
                <tr >
                <td style="text-align: right">
                   *Account Name</td>
                <td>
                    <asp:TextBox ID="TxtActName"  runat="server"></asp:TextBox></td>
                <td style="text-align: right" >
                   *Description </td>
                <td>
                    <asp:TextBox ID="TxtDescr" runat="server"></asp:TextBox></td>
                </tr>
                   <tr style="color: gray; " class="alt">
                <td style="text-align: right; text-align: right; ">
                <span style="font-family: 'Arial', Cursive">*Processing Folder Name</span></td>
                <td style=" text-align: left; ">
                    <asp:TextBox ID="TxtFolder" runat="server"></asp:TextBox></td>
                <td style="text-align: right; text-align: right; ">
                <span style="font-family: 'Arial', Cursive">*Billing Account Number</span></td>
                <td style="text-align: left; text-align: left; ">
                    <asp:TextBox ID="TxtBillNumber" runat="server"></asp:TextBox></td>
            </tr> 
               <tr>
                <td  style="text-align: right">
                    Cateogory</td>
                <td >
                    <asp:DropDownList ID="DrpCategory" runat="server" Width="130px">
                       
                </asp:DropDownList>
                    </td>
                <td  style="text-align: right" >
                    RSS Type</td>
                <td >
                    <asp:DropDownList ID="DrpRSSType" runat="server" Width="130px">
                    <asp:ListItem Value="0" Text="Select RSS Type" ></asp:ListItem>                
                    <asp:ListItem Value="1" Text="Pocket PC" ></asp:ListItem>
                    <asp:ListItem Value="2" Text="DVR"></asp:ListItem>
                    <asp:ListItem Value="3" Text ="Custom"></asp:ListItem>
                    </asp:DropDownList></td>
                </tr>
                
              
            
                <tr>
                <td  style="text-align: right">
                    Address</td>
                <td >
                    <asp:TextBox ID="TXTADD" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    City</td>
                <td >
                    <asp:TextBox ID="TXTCity" runat="server"></asp:TextBox></td>
                </tr>
            
                <tr>
                <td  style="text-align: right; height: 30px;">
                    State</td>
                <td  style="height: 30px">
                    <asp:TextBox ID="TXTState" runat="server"></asp:TextBox></td>
                <td  style="text-align: right; height: 30px;" >
                    Country</td>
                <td  style="height: 30px">
                     <asp:DropDownList ID="DDLCntry" runat="server" Width="130px">
                    <asp:ListItem Value="" Text="Select Country" ></asp:ListItem>                
                    <asp:ListItem Value="US" Text="US"></asp:ListItem>
                    <asp:ListItem Value="Canada" Text="Canada"></asp:ListItem>
                    <asp:ListItem Value="UK" Text ="UK"></asp:ListItem>
                    <asp:ListItem Value="India" Text="India" ></asp:ListItem>
                    </asp:DropDownList></td>
                </tr>
            
                <tr>
                <td  style="text-align: right">
                    Zip</td>
                <td >
                    <asp:TextBox ID="TXTZip" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    Official Website</td>
                <td >
                    <asp:TextBox ID="TXTOS" runat="server"></asp:TextBox></td>
                </tr>
                
               <tr>
                <td  style="text-align: right">
                    Expected Daily Minutes</td>
                <td >
                    <asp:TextBox ID="TxtPMins" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    Billing Mode</td>
                <td >
                     <asp:DropDownList ID="DLMode" runat="server" Width="130px">
                         <asp:ListItem Value="S">Standard</asp:ListItem>
                         <asp:ListItem Value="LC">By Location/Department</asp:ListItem>
                        <asp:ListItem Value="DC">By Dictator</asp:ListItem> 
                        <asp:ListItem Text="DeviceWise" Value="DV"></asp:ListItem> 
                        <asp:ListItem Text="TemplateWise" Value="TW"></asp:ListItem> 
                        <asp:ListItem Text="TATWise" Value="TT"></asp:ListItem> 
                     </asp:DropDownList></td>
                </tr>
               
               <tr>
                <td  style="text-align: right">
                    Default TAT</td>
                <td >
                    <asp:DropDownList ID="DLTAT" runat="server" Width="130px">
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>48</asp:ListItem>
                        <asp:ListItem>72</asp:ListItem>
                        <asp:ListItem>96</asp:ListItem>
                        <asp:ListItem>120</asp:ListItem>
                    </asp:DropDownList></td>
                <td  style="text-align: right" >
                   Default STAT TAT
                    </td>
                <td >
                    <asp:DropDownList ID="DLSTAT" runat="server" Width="130px">
                    <asp:ListItem>1</asp:ListItem>
                         <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList></td>
                </tr> 
              <tr>
                <td  style="text-align: right" >
                   TimeZone
                </td>
                <td>
                                    <asp:DropDownList ID="DDLTZ" runat="server">
                                       <asp:ListItem Value="0" Text="Eastern Time (EST - Default)"></asp:ListItem>
                                       <asp:ListItem value="-1" text="Central Time (CST)"></asp:ListItem>
                                       <asp:ListItem value="-2" Text="Mountain Time (MST)"></asp:ListItem>
                                       <asp:ListItem value="-3" Text="Pacific Time (PST)"></asp:ListItem>
                                       <asp:ListItem value="-4" Text="Alaska Time (AKST)"></asp:ListItem>
                                       <asp:ListItem value="-6" Text="Hawaii Time"></asp:ListItem>
                                    </asp:DropDownList>
                                    
                  </td>
                  <td  style="text-align: right" >
                   DueTime(24Hrs)
                  </td>
                  <td>
                    <asp:TextBox ID="txtTime" runat="server" Text="" Width="20" MaxLength="2"></asp:TextBox>                                    
                  </td>
              </tr>                
              <tr>
                  <td  style="text-align: right">
                      AutoFax</td>
                  <td >
                      <asp:DropDownList ID="DLFaxPlus" runat="server" Width="130px" AutoPostBack="True">
                          <asp:ListItem Value="0">No</asp:ListItem>
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                      </asp:DropDownList></td>
                  <td  style="text-align: right">
                      AutoFax Mode</td>
                  <td >
                      <asp:DropDownList ID="DLFMode" runat="server" Width="130px">
                          <asp:ListItem  Value="0" >Pending Signature</asp:ListItem>
                          <asp:ListItem  Value="1">Signed Report</asp:ListItem>
                           <asp:ListItem  Value="2">CC Only</asp:ListItem>
                          
                      </asp:DropDownList></td>
              </tr>
                <tr>
                  <td  style="text-align: right">
                      AutoFax - Transcription Exceptions</td>
                  <td >
                      <asp:DropDownList ID="DLFaxExcp" runat="server" Width="130px" AutoPostBack="True">
                          <asp:ListItem Value="0">Ignore</asp:ListItem>
                          <asp:ListItem Value="1">On Hold</asp:ListItem>
                      </asp:DropDownList></td>
                      <td  style="text-align: right">
                      Fax Interface</td>
                  <td >
                  <asp:DropDownList ID="DLFaxInterFace" runat="server" Width="130px">
                    <asp:ListItem Value="B">GFI</asp:ListItem>
                    <asp:ListItem Value="E">EasyLink</asp:ListItem>
                   
                </asp:DropDownList></td>
               
                 
              </tr>
              <tr style="color: gray; ">
                  <td style="height: 26px; text-align: right">
                      Map Demo Account</td>
                  <td style="height: 26px; text-align: left">
                  <asp:DropDownList ID="MapActID" runat="server" Width="130px"  >
                    </asp:DropDownList>
                  </td>
                  <td style="height: 26px; text-align: right">
                  Map Ref Physician Account</td>
                  <td style="text-align: left">
                  <asp:DropDownList ID="MapRActID" runat="server" Width="130px"  >
                    </asp:DropDownList>
                  </td>
              </tr>
              <tr style="color: gray; ">
              
              
                          <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Dictation Source</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLVoice" runat="server" Width="130px">
                    <asp:ListItem Value="0">Default</asp:ListItem>
                    <asp:ListItem Value="1">Custom</asp:ListItem>
                </asp:DropDownList></td>
                <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Process outside SecureXFlow</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLIndirect" runat="server" Width="130px">
                    <asp:ListItem Value="0">No</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                </asp:DropDownList></td>
                <%--<td style="text-align: right; text-align: right; ">
                   *Instance
                </td>
                <td style="text-align: left; text-align: left; ">
                    <asp:DropDownList ID="DLInstance" runat="server" Width="130px">
                        <asp:ListItem Text="Please Select" Value="" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
                
                
                      
            </tr> 
            <tr style="color: gray; ">
              
              
                          <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Is Image Signature Account</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLSignature" runat="server" Width="130px">
                    <asp:ListItem Value="0">No</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                </asp:DropDownList></td>
                <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Is EMR Account</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLEMR" runat="server" Width="130px">
                    <asp:ListItem Value="0">No</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                </asp:DropDownList></td>
                </tr> 
                 <tr style="color: gray; ">
              
              
                          <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Website</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLWebsite" runat="server" Width="130px">
                    <asp:ListItem Value="SXF">SecureXFlow</asp:ListItem>
                    <asp:ListItem Value="NMBS">NMBS</asp:ListItem>
                </asp:DropDownList></td>
                 <td  style="text-align: right">
                      SecureXFlow PC - Stop Uploading Dictation</td>
                  <td >
                      <asp:DropDownList ID="DDLStopDict" runat="server" Width="130px" AutoPostBack="False">
                          <asp:ListItem Value="0">No</asp:ListItem>
                          <asp:ListItem Value="1">Yes</asp:ListItem>
                      </asp:DropDownList></td></tr> 
              <tr>
              <tr style="color: gray; ">
                     <td  style="text-align: right">
                          Is Archive Enabled</td>
                      <td >
                          <asp:DropDownList ID="DLArchive" runat="server" Width="130px" AutoPostBack="False">
                              <asp:ListItem Value="0">No</asp:ListItem>
                              <asp:ListItem Value="1">Yes</asp:ListItem>
                          </asp:DropDownList></td>
                            <td  style="text-align: right">
                          Hide UnSigned Reports (Secure-UNO)</td>
                      <td >
                          <asp:DropDownList ID="DLHideUnSignedRep" runat="server" Width="130px" AutoPostBack="False">
                              <asp:ListItem Value="0">No</asp:ListItem>
                              <asp:ListItem Value="1">Yes</asp:ListItem>
                          </asp:DropDownList></td>
                 </tr> 
             
                <td colspan="4" >
                </td>
                </tr>
           
                        <tr>
                           <td  class="alt" colspan="4" style="text-align: center" >
                               Secure-Dox
                            </td>
                        </tr>
                      
              <tr style="color: gray; ">
              
              
                          <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Is Interface</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DDLSDOXInterface" runat="server" AutoPostBack="true" Width="130px">
                    <asp:ListItem Value="0">No</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                </asp:DropDownList></td>
                <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Office ID</span></td>
                      <td style="text-align: left; text-align: left; ">
                   <asp:TextBox ID="txtOfficeID" runat="server"></asp:TextBox>
                        </td>
                        
               
                      
            </tr>  
            <tr>
            <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Effective Date</span></td>
                      <td style="text-align: left; text-align: left; ">
                   <asp:TextBox ID="txtEffectiveDate" runat="server"></asp:TextBox>
                        </td>
               
                        
                      
            </tr> 
              <tr>
                <td colspan="4" >
                </td>
                </tr>
           
                        <tr>
                           <td  class="alt" colspan="4" style="text-align: center" >
                               Interface Details
                            </td>
                        </tr>
                <tr style="color: gray; ">
              
              
                          <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Is Interface Account</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLInterface" runat="server" AutoPostBack="true" Width="130px">
                    <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                </asp:DropDownList></td>
                <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Is HL7 Account</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLHL7" runat="server" Width="130px">
                    <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                </asp:DropDownList></td>
               
                      
            </tr>  
            <tr style="color: gray; ">
              
              
                          <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Mode</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLSigned" runat="server" Width="130px">
                    <asp:ListItem Value="0" Selected="True">Pending Signature</asp:ListItem>
                    <asp:ListItem Value="1">Signed</asp:ListItem>
                </asp:DropDownList></td>
                  <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">EMR</span></td>
                      <td style="text-align: left; text-align: left; ">
                          <asp:TextBox ID="txtEMR" runat="server"></asp:TextBox></td>
               
               
                      
            </tr>    
                <tr>
                <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Group</span></td>
                      <td style="text-align: left; text-align: left; ">
                          <asp:TextBox ID="txtIntGroup" runat="server"></asp:TextBox></td>
                            <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">Show ReProcess for Finished HL7</span></td>
                      <td style="text-align: left; text-align: left; ">
                <asp:DropDownList ID="DLReProcess" runat="server" AutoPostBack="true" Width="130px">
                    <asp:ListItem Value="0">No</asp:ListItem>
                    <asp:ListItem Value="1">Yes</asp:ListItem>
                </asp:DropDownList></td></tr>
             <tr>
                 
                      <td  style="text-align: right">
                      FacilityID</td>
                  <td >
                  <asp:TextBox ID="TxtFacilityID" runat="server" ></asp:TextBox> 
                  </td> 
                 
              </tr>
             <tr style="color: gray; ">
                <td style="height: 60px; text-align: right;">
                    Comments</td>
                <td colspan="3" style="height: 60px; text-align: left">
                    <asp:TextBox ID="txtComments" runat="server" Height="80px" TextMode="MultiLine" Width="392px"></asp:TextBox></td>
            </tr>
                
              
                     <tr style="color: gray; ">
                <td colspan="2" style="height: 21px; text-align: center" class="alt">
                    
                    <strong>Primary Contact Details
                </td>
                <td colspan="2" style="height: 21px; text-align: center"  class="alt">
                    Secondary Contact Details</td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; text-align: right; height: 21px;">
                <span style="font-family: 'Arial', Cursive">
                    Name</span></td>
                <td style=" text-align: left; height: 21px;">
                    <asp:TextBox ID="TxtPCN" runat="server"></asp:TextBox></td>
                <td style="text-align: right; text-align: right; height: 21px;">
                <span style="font-family: 'Arial', Cursive">Name</span></td>
                <td style="text-align: left; text-align: left; height: 21px;">
                    <asp:TextBox ID="TxtSCN" runat="server"></asp:TextBox></td>
            </tr>
             <tr style="color: gray; ">
                <td style="text-align: right; text-align: right; height: 21px;">
                <span style="font-family: 'Arial', Cursive">
                    Title</span></td>
                <td style=" text-align: left; height: 21px;">
                    <asp:TextBox ID="TxtPriTitle" runat="server"></asp:TextBox></td>
                <td style="text-align: right; text-align: right; height: 21px;">
                <span style="font-family: 'Arial', Cursive">Title</span></td>
                <td style="text-align: left; text-align: left; height: 21px;">
                    <asp:TextBox ID="TxtSecTitle" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; text-align: right;">
                <span style="font-family: 'Arial', Cursive"> EMail</span></td>
                <td style="height: 22px; text-align: left; ">
                    <asp:TextBox ID="TxtPE" runat="server"></asp:TextBox>
                </td>
                <td style="height: 22px; text-align: right;">
                    <span style="font-family: 'Arial', Cursive">Email</span></td>
                <td style="height: 22px; text-align: left;">
                    <asp:TextBox ID="TxtSE" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; text-align: right;">
                    <span style="font-family: 'Arial', Cursive">Phone Number</span></td>
                <td style="height: 22px; text-align: left; ">
                    <asp:TextBox ID="txtPPh" runat="server"></asp:TextBox></td>
                <td style="height: 22px; text-align: right;">
                    <span style="font-family: 'Arial', Cursive">Phone Number</span></td>
                <td style="height: 22px; text-align: left;">
                    <asp:TextBox ID="txtSPh" runat="server"></asp:TextBox></td>
            </tr>
            <tr style="color: gray; ">
                <td style="text-align: right; height: 30px; text-align: right;">
                <span style="font-family: 'Arial', Cursive">Fax</span></td>
                <td style="height: 21px; height: 30px; text-align: left; ">
                    <asp:TextBox ID="txtPFax" runat="server"></asp:TextBox></td>
                <td style="height: 21px; text-align: right; height: 30px;">
                <span style="font-family: 'Arial', Cursive">Fax</span>        </td>
                <td style="height: 21px; height: 30px; text-align: left;">
                    <asp:TextBox ID="txtSFax" runat="server"></asp:TextBox></td>
            </tr>              
               
           
              
                        <tr>
                           <td  class="alt" colspan="4" style="text-align: center"    >
                              Billing Department Details
                            </td>
                        </tr>
                 
                
                <tr>
                <td  style="text-align: right">
                    Contact Name  </td>
                <td >
                    <asp:TextBox ID="TXTBCNM" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    Contact Number  </td>
                <td >
                    <asp:TextBox ID="TXTBCNO" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td  style="text-align: right">
                    Address</td>
                <td >
                    <asp:TextBox ID="TXTBADD" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    City</td>
                <td >
                    <asp:TextBox ID="TXTBCity" runat="server"></asp:TextBox></td>
                </tr>
            
                <tr>
                <td  style="text-align: right">
                    State</td>
                <td >
                    <asp:TextBox ID="TXTBState" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    Country</td>
                <td >
                    <asp:TextBox ID="TXTBCntry" runat="server"></asp:TextBox></td>
                </tr>
            
                <tr>
                <td  style="text-align: right">
                    Zip</td>
                <td >
                    <asp:TextBox ID="TXTBZip" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    Fax</td>
                <td >
                    <asp:TextBox ID="TXTBFax" runat="server"></asp:TextBox></td>
                </tr>
            
                <tr>
                <td  style="text-align: right">
                    E-Mail Address  </td>
                <td >
                    <asp:TextBox ID="TXTBEmail" runat="server"></asp:TextBox></td>
                          
                <td  style="text-align: right" >
                    Official Site</td>
                <td >
                    <asp:TextBox ID="TXTBOS" runat="server"></asp:TextBox></td>
                </tr>
            
                <tr>
                <td colspan="4" >
                </td>
                </tr>
           
                        <tr>
                           <td  class="alt" colspan="4" style="text-align: center" >
                               Technical Department Details
                            </td>
                        </tr>
                   
                <tr>
                <td  style="text-align: right">
                    Contact Name  </td>
                <td >
                    <asp:TextBox ID="TXTTCNM" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    Contact Number  </td>
                <td >
                    <asp:TextBox ID="TXTTCNO" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                <td  style="text-align: right">
                    Address</td>
                <td >
                    <asp:TextBox ID="TXTTADD" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    City</td>
                <td >
                    <asp:TextBox ID="TXTTCity" runat="server"></asp:TextBox></td>
                </tr>
            
                <tr>
                <td  style="text-align: right">
                    State</td>
                <td >
                    <asp:TextBox ID="TXTTState" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    Country</td>
                <td >
                    <asp:TextBox ID="TXTTCntry" runat="server"></asp:TextBox></td>
                </tr>
            
                <tr>
                <td  style="text-align: right">
                    Zip</td>
                <td >
                    <asp:TextBox ID="TXTTZip" runat="server"></asp:TextBox></td>
                <td  style="text-align: right" >
                    Fax</td>
                <td >
                    <asp:TextBox ID="TXTTFax" runat="server"></asp:TextBox></td>
                </tr>
            
                <tr>
                <td  style="text-align: right">
                    E-Mail Address  </td>
                <td >
                    <asp:TextBox ID="TXTTEMail" runat="server"></asp:TextBox></td>
                          
                <td  style="text-align: right" >
                    Official Site</td>
                <td >
                    <asp:TextBox ID="TXTTOS" runat="server"></asp:TextBox></td>
                </tr>
                 <tr>
                <td  style="text-align: right">
                    Account Status  </td>
                <td >
                    <asp:DropDownList ID="DLStatus" runat="server">
                    <asp:ListItem Text="Active" Value="False">                   </asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="True">                   </asp:ListItem>
                    </asp:DropDownList>
                    </td>
                     <asp:HiddenField ID="hdnastatus" runat="server" />
                    <td style="height: 26px; text-align: right">
                    <span style="font-family: Arial">MIS Report</span>
                </td>
                <td>
                    <asp:DropDownList ID="ddlMIS" runat="server" Width="130px">
                        <asp:ListItem Value="True">Yes</asp:ListItem>
                        <asp:ListItem Value="False">No</asp:ListItem>
                    </asp:DropDownList>
                </td>
                          
                
                </tr>  
            <tr>
            <td style="text-align: right">
            Password Reset (days)
            </td>
            <td >
                    <asp:DropDownList ID="DLPReset" runat="server">
                    <asp:ListItem Text="90" Value="90">                   </asp:ListItem>
                    <asp:ListItem Text="180" Value="180">                   </asp:ListItem>
                    </asp:DropDownList>
                    </td>
                     <asp:HiddenField ID="hdnpresetvalue" runat="server" />
            </tr>
                <tr>
                <td colspan="4" class ="ADACCESS">
                </td>
                </tr>
           
               
        <tr>
                <td style="text-align: center;" colspan="4" >
                    <asp:Button ID="ImageButton1" cssclass="button"  runat="server" Text="Submit" /></td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ControlToValidate="TxtActName"  ID="RequiredFieldValidator1"  runat="server" ErrorMessage="Please enter Account Name" Font-Bold="True" Font-Italic="True" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator><br />
                    <asp:RequiredFieldValidator ControlToValidate="Txtdescr"  ID="RequiredFieldValidator4"  runat="server" ErrorMessage="Please enter Account Description" Font-Bold="True" Font-Italic="True" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator><br />
                               <asp:RequiredFieldValidator ControlToValidate="TxtFolder"  ID="RequiredFieldValidator2"  runat="server" ErrorMessage="Please enter Folder Name" Font-Bold="True" Font-Italic="True" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator><br />
                                         <asp:RequiredFieldValidator ControlToValidate="TxtBillNumber"  ID="RequiredFieldValidator3"  runat="server" ErrorMessage="Please enter Billing Account Number" Font-Bold="True" Font-Italic="True" Font-Names="Arial" Font-Size="Small"></asp:RequiredFieldValidator> <br />
                                         
        <asp:HiddenField ID="AccID" runat="server" /> 
        </div>
    </div>
 <div style="text-align:left"> 
 <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="False" /> 
               <asp:RegularExpressionValidator  Display="None"
    id="RegTxtAccountName"  
    runat="server"  
   
    ControlToValidate="TxtActName"  
    ValidationExpression="^[0-9a-zA-Z-. ,]+$"
    ErrorMessage="Account Name - Please enter valid input."
   />
    <asp:RegularExpressionValidator  Display="None"
    id="RegTxtDescription"  
    runat="server" 
    ControlToValidate="TxtDescr" 
    ValidationExpression="^[0-9a-zA-Z-. ,]+$"
    ErrorMessage="Description - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtFolder"  
    runat="server" 
    ControlToValidate="TxtFolder" 
    ValidationExpression="^[0-9a-zA-Z- ,]+$"
    ErrorMessage="Processing Folder Name - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtBillNumber"  
    runat="server" 
    ControlToValidate="TxtBillNumber" 
    ValidationExpression="^[0-9a-zA-Z-]+$"
    ErrorMessage="Billing Account Number - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegCity"  
    runat="server" 
    ControlToValidate="TXTCity" 
    ValidationExpression="^[0-9a-zA-Z. ]+$"
    ErrorMessage="City - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTXTState"  
    runat="server" 
    ControlToValidate="TXTState" 
    ValidationExpression="^[0-9a-zA-Z]+$"
    ErrorMessage="State - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTXTZip"  
    runat="server" 
    ControlToValidate="TXTZip" 
    ValidationExpression="^[0-9-]+$"
    ErrorMessage="Zip - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtOfficialSite"  
    runat="server" 
    ControlToValidate="TXTOS" 
    ValidationExpression="^[a-zA-Z, ]+$"
    ErrorMessage="Official Website - Please enter valid input."
   />
 <asp:RegularExpressionValidator  Display="None"
    id="RegTxtProtocolMins"  
    runat="server" 
    ControlToValidate="TxtPMins" 
    ValidationExpression="^[0-9]+$"
    ErrorMessage="Expected Daily Minutes - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegtxtTime"  
    runat="server" 
    ControlToValidate="txtTime" 
    ValidationExpression="^[0-9]+$"
    ErrorMessage="DueTime(24Hrs) - Please enter valid input."
   /> 
<asp:RegularExpressionValidator  Display="None"
    id="RegTxtPriContact"  
    runat="server" 
    ControlToValidate="TxtPCN" 
    ValidationExpression="^[a-zA-Z, ]+$"
    ErrorMessage="Primary Contact Name - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegTxtSecContact"  
    runat="server" 
    ControlToValidate="TxtSCN" 
    ValidationExpression="^[a-zA-Z, ]+$"
    ErrorMessage="Secondary Contact Name - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegTxtPriTitle"  
    runat="server" 
    ControlToValidate="TxtPriTitle" 
    ValidationExpression="^[a-zA-Z, ]+$"
    ErrorMessage="Primary Contact Title - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegTxtSecTitle"  
    runat="server" 
    ControlToValidate="TxtSecTitle" 
    ValidationExpression="^[a-zA-Z, ]+$"
    ErrorMessage="Secondary Contact Title - Please enter valid input."
   />

<asp:RegularExpressionValidator  Display="None"
    id="RegTxtPriEmail"  
    runat="server" 
    ControlToValidate="TxtPE" 
    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    ErrorMessage="Primary Contact E-Mail - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegTxtSecEmail"  
    runat="server" 
    ControlToValidate="TxtSE" 
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
    ErrorMessage="Secondary Contact E-Mail - Please enter valid input."
   />

<asp:RegularExpressionValidator  Display="None"
    id="RegtxtPriPhone"  
    runat="server" 
    ControlToValidate="txtPPh" 
    ValidationExpression="^[0-9-() ]+$"
    ErrorMessage="Primary Contact Phone Number - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegtxtSecPhone"  
    runat="server" 
    ControlToValidate="txtSPh" 
    ValidationExpression="^[0-9-() ]+$"
    ErrorMessage="Secondary Contact Phone Number - Please enter valid input."
   />

<asp:RegularExpressionValidator  Display="None"
    id="RegtxtPriFaxNo"  
    runat="server" 
    ControlToValidate="txtPFax" 
    ValidationExpression="^[0-9-() ]+$"
    ErrorMessage="Primary Contact Fax Number - Please enter valid input."
   />
<asp:RegularExpressionValidator  Display="None"
    id="RegtxtSecFaxNo"  
    runat="server" 
    ControlToValidate="txtSFax" 
    ValidationExpression="^[0-9-() ]+$"
    ErrorMessage="Secondary Contact Fax Number - Please enter valid input."
   />
</div>
   
    </form>
</body>
</html>
