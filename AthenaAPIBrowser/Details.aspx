<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Details.aspx.vb" Inherits="AthenaAPIBrowser_Details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
       .reToolbarWrapper {
             display: none;
         }

         #flterComLog {
             background-image: url(/css/searchicon.png);
             background-position: 10px 10px;
             background-repeat: no-repeat;
             width: 32%;
             font-size: 16px;
             padding: 5px 5px 7px 40px;
             border: 1px solid #ddd;
             margin-bottom: 0px;
             vertical-align: top;
         }
     </style>

    <style type="text/css">
        .WaterMarkedTextBox {
            height: 16px;
            width: 268px;
            padding: 2px 2 2 2px;
            border: 1px solid #BEBEBE;
            background-color: #F0F8FF;
            color: gray;
            font-size: 8pt;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnselActID" runat="server" />
    <div id="dvActs" runat="server" visible="true" >
        <div class="tabs-container">
                <div class="tabsArea">
                    <div class="tabContainer">
                        <div class="viewCommunication ">
                            <table id="tblActs">
                                <thead>
                                   <%-- <th></th>--%>
                                    <th>Account Name</th>
                                    <th>Account Number</th>
                                    <th>Athena Context ID</th>
                                   
                                </thead>
                                <tbody>

                                    <asp:Repeater ID="MyDataGrid" runat="server">
                                       
                                        <ItemTemplate>
                                            <tr>
                                                 <td style="width:300px !important;">
                                                     <asp:HyperLink ID="hlnkAct" Text='<%# Eval("AccountName")%>'  runat="server" NavigateUrl='<%# "AthenaHealthAPIBrowser.aspx?accountid=" & Eval("AHAccountNo")%>'></asp:HyperLink>
                                                    
                                                </td>
                                                <td style="width:300px !important;">
                                                    <%# Eval("AccountNo")%>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("AHAccountNo")%>
                                                </td>
                                               
                                                </tr>
                                            </ItemTemplate>
                                         <FooterTemplate>
                                           
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    </tbody>
                            </table>
                            </div>
                        </div>
                    </div>
            </div>
     
    </div>
        <div id="dvPhysicians" runat="server" visible="false" >
            <div class="tabs-container">
                <div class="tabsArea">
                    <div class="tabContainer">
                        <div class="viewCommunication ">
            <div class="nextPrev">
                       
                       
                        <input type="text" id="flterComLog" onkeyup="myFunction()" placeholder="type to search.." title="Filter result" />
                        
                    </div>
            <table id="tblComLog">
                                <thead>
                                   <%-- <th></th>--%>
                                    <th>Provider ID</th>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Speciality</th>
                                    <th>Degree/Type</th>
                                </thead>
                                <tbody>

                                    <asp:Repeater ID="rptComLog" runat="server">
                                       
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width:300px !important;">
                                                     <asp:HyperLink ID="hlnkAct" Text='<%# Eval("providerid")%>'  runat="server" NavigateUrl='<%# "AthenaHealthAPIBrowser.aspx?providerid=" & Eval("providerid")%>'></asp:HyperLink>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("firstname")%>
                                                </td>
                                                <td style="width:300px !important;">
                                                    <%# Eval("lastname")%>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("specialty")%>
                                                </td>
                                                <td style="width:300px !important;">
                                                    <%# Eval("providertype")%>
                                                </td>
                                                </tr>
                                            </ItemTemplate>
                                         <FooterTemplate>
                                           
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    </tbody>
                            </table>
                            </div>
                        </div>
                    </div>
                </div>

    </div>
         <div id="dvAppt" runat="server" visible="false" >
             <div class="tabs-container">
                <div class="tabsArea">
                    <div class="tabContainer">
                        <div class="viewCommunication ">
                            <div class="nextPrev">
                       
                       
                        <input type="text" id="flterApts" onkeyup="myFunctionApts()" placeholder="type to search.." title="Filter result" />
                        
                    </div>
                            <asp:Label ID="lblAptCount" runat="server" ></asp:Label>
                            <table id="tblApt">
                                <thead>
                                   <%-- <th></th>--%>
                                   
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                   <th>Date of Birth</th>
                                    <th>Gender</th>
                                    <th>PatientID</th>
                                    <th>VisitID</th>
                                    <th>Visit Status</th>
                                    <th>Appointment Desc</th>
                                    <th>Appointment Note</th>
                                    <th>CheckIn DateTime</th>
                                </thead>
                                <tbody>

                                    <asp:Repeater ID="AptDataGrid" runat="server">
                                       
                                        <ItemTemplate>
                                            <tr>
                                                
                                                <td style="width:300px !important;">
                                                    <%# Eval("PFirstName")%>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("Plastname")%>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("PDOB")%>
                                                </td>

                                                 <td style="width:300px !important;">
                                                    <%# Eval("pgender")%>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("PatientID")%>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("VisitID")%>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("VisitStatus")%>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("AppointmentTypeDesc")%>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("AppointmentNotes")%>
                                                </td>
                                                 <td style="width:300px !important;">
                                                    <%# Eval("checkindatetime")%>
                                                </td>
                                                </tr>
                                            </ItemTemplate>
                                         <FooterTemplate>
                                           
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    </tbody>
                            </table>
                            </div>
                        </div>
                    </div>
            </div>
     <%--<asp:CompleteGridView  ID="AptDataGrid" runat="server" AutoGenerateColumns="False" 
                    AllowPaging="false" CellPadding="4" AllowSorting="false" CssClass="common" ShowFilter="true"
                    Width="100%" GridLines="Both"     
                    ForeColor="#333333"   PageSize="500" CountFormat="Displaying records <b>{0}</b> to <b>{1}</b> from <b>{2}</b>" ShowCount="False" Font-Italic="False" CaptionAlign="Bottom" ShowInsertRow="True"  SortAscendingImageUrl="" SortDescendingImageUrl="" HorizontalAlign="Left" >
                            <Columns>
                                 
                                <asp:HyperlinkField DataTextField="providerid" DataNavigateUrlFields="providerid" DataNavigateUrlFormatString="AthenaHealthAPIBrowser.aspx?providerid={0}"
  HeaderText="Provider ID" SortExpression="providerid" ItemStyle-HorizontalAlign="Left"  />
                                <asp:BoundField DataField="firstname" HeaderText="First Name" SortExpression="firstname" />
                                <asp:BoundField DataField="lastname" HeaderText="Last Name" SortExpression="lastname" />
                                <asp:BoundField DataField="specialty" HeaderText="specialty" SortExpression="specialty" />
                                <asp:BoundField DataField="providertype" HeaderText="Type" SortExpression="providertype" />
                            </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle HorizontalAlign="Center"  BackColor="#5D7B9D" cssclass="Title"   />
                            <EditRowStyle BackColor="#999999" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        
                </asp:CompleteGridView>--%>
    </div>
    </form>
     <script>
        function myFunction() {
            var input, filter, table, tr, td, i, txtValue;
            input = document.getElementById("flterComLog");
           
            filter = input.value.toUpperCase();
            table = document.getElementById("tblComLog");
            tr = table.getElementsByTagName("tr");
           
            for (i = 1; i < tr.length; i++) {
                tr[i].style.display = "none";
                td = tr[i].getElementsByTagName("td")
                for (j = 0; j < td.length; j++) {
                    if (td[j]) {
                        txtValue = td[j].textContent || td[j].innerText;
                        // alert(filter);
                        if (txtValue.toUpperCase().indexOf(filter) > -1) {
                            tr[i].style.display = "";
                        } else {
                            
                        }
                    }
                }

                
            }
        }
        function myFunctionApts() {
            var input, filter, table, tr, td, i, txtValue;
            input = document.getElementById("flterApts");

            filter = input.value.toUpperCase();
            table = document.getElementById("tblApt");
            tr = table.getElementsByTagName("tr");

            for (i = 1; i < tr.length; i++) {
                tr[i].style.display = "none";
                td = tr[i].getElementsByTagName("td")
                for (j = 0; j < td.length; j++) {
                    if (td[j]) {
                        txtValue = td[j].textContent || td[j].innerText;
                        // alert(filter);
                        if (txtValue.toUpperCase().indexOf(filter) > -1) {
                            tr[i].style.display = "";
                        } else {

                        }
                    }
                }


            }
        }
       
    </script>
     <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
</body>
</html>

