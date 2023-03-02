<%@ Page Language="VB" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 1.0 Transitional/EN">

<script runat="server">
    Sub SaveIt()
        MsgBox(File1.PostedFile)
        '    dim DocName
        '    DocName = Month(Now) & day(Now) & year(Now) & Hour(now) & Minute(Now) & Second(now)    
        '    document.all.SAXFile.AddFile "C:\edictate\temp\" & DocName & ".doc", "File"	     
        '    document.all.SAXFile.AddFormElement "KeyWords", document.all.txtKeyWords.value
        '    document.all.SAXFile.AddFormElement "TransID", document.all.hdnTransID.value
        '    document.all.SAXFile.CurrentURL = "https://ets.edictate.com/Samples/SaveSample.aspx"
        '    document.all.SAXFile.Start
        '    if document.all.SAXFile.response = "1" then
        '        MsgBox "Sample has been set successfully!"
        '    else
        '        MsgBox document.all.SAXFile.response           
        '    end if
        '    window.navigate("SetSamples.aspx")    
    End Sub
    Protected Sub btnGO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGO.Click
        If Not String.IsNullOrEmpty(DDLAccounts.SelectedItem.Text) Then
            lblAccName.Text = "Account Instructions for: " & DDLAccounts.SelectedItem.Text
            MultiView1.ActiveViewIndex = 1
            Dim oConn As New Data.SqlClient.SqlConnection
            Dim ConString, SQLString As String
            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                SQLString = "SELECT Format, DateModified,IsDeleted FROM tblAccountInstructions where AccountID='" & DDLAccounts.SelectedItem.Value & "'"
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
                If oRec.HasRows Then
                    oRec.Read()
                    lblType.Text = oRec("Format")
                    chkDelete.Checked = IIf(oRec("IsDeleted") = 1, True, False)
                    btnAddNew.Visible = False
                    btnSave.Visible = False
                Else
                    TR.Visible = True
                    lblResponse.Visible = True
                    lblResponse.Text = "No Account Instructions found for " & DDLAccounts.SelectedItem.Text
                    lblType.Visible = False
                    chkDelete.Enabled = False
                    btnEdit.Visible = False
                    btnSave.Visible = False
                    btnAddNew.Visible = True
                End If
                oRec.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TR.Visible = False
        lblResponse.Visible = False
        TRFile.Visible = False
        If Not IsPostBack Then
            MultiView1.ActiveViewIndex = 0
            Dim oConn As New Data.SqlClient.SqlConnection
            Dim ConString, SQLString As String
            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                SQLString = "SELECT newid() as AccountID,'' as AccountName union SELECT AccountID,AccountName FROM tblAccounts order by AccountName"
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
                DDLAccounts.DataSource = oRec
                DDLAccounts.DataValueField = "AccountID"
                DDLAccounts.DataTextField = "AccountName"
                DDLAccounts.DataBind()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub
    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        btnEdit.Enabled = False
    End Sub
    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            btnAddNew.Visible = False
            TRFile.Visible = True
            btnSave.Visible = True
            'Dim oConn As New Data.SqlClient.SqlConnection
            'Dim ConString, SQLString As String
            'ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            'oConn.ConnectionString = ConString
            'oConn.Open()
            'SQLString = "INSERT INTO tblAccountInstructions (AccountID,Format,DateModified,UserID)" & _
            '            "VALUES('" & DDLAccounts.SelectedItem.Value & "','','" & Now() & "','" & Session("UserID") & "')"
            'Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            'If oCommand.ExecuteNonQuery() > 0 Then
            '    TR.Visible = True
            '    lblResponse.Visible = True
            '    lblResponse.Text = "Added"
            'End If
        Catch ex As Exception
            TR.Visible = True
            lblResponse.Visible = True
            lblResponse.Text = ex.Message
        End Try
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
    function btnOnClick()
    {        
        var fso = new ActiveXObject( 'Scripting.FileSystemObject' );
        if( fso.FileExists('C:\\Program Files\\ETS\\EditTemplate.exe') ) {
            if(fso.GetFileVersion('C:\\Program Files\\ETS\\EditTemplate.exe')=='1.0.0.3') {     
            }else{
                window.location='../TemplateEditor/Default.htm';
            }           
        
        }else {        
           window.location='../TemplateEditor/Default.htm';
        }
        
    }
    </script>
</head>
<body>
       <form id="form1" runat="server" EncType="Multipart/Form-Data" >
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true" EnableScriptLocalization="true" runat="Server" />
    <%--<asp:UpdatePanel ID="pnlInstr" runat="server">
    <ContentTemplate>--%>
    <asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="vSearch" runat="server">
    <div>
    <p>  
    <br />
    <br />  
                  <asp:DropDownList ID="DDLAccounts" runat="server" Width="264px" TabIndex="1" >                      
                  </asp:DropDownList>            
                  <asp:Button ID="btnGO" runat="server" Text="Search" OnClientClick="btnOnClick()"/>
    </p>    
    </div>  
    </asp:View>
    <asp:View ID="vDetails" runat="server">
        <div>
        <p>  
        <br />
        <br /> 
        <table border="1">
            <TR bgcolor="#3399cc" class="HeaderDiv">
                <th align="center">
                    <asp:Label ID="lblAccName" runat="server"></asp:Label>
                </th>                
            </tr>
            <tr id="TR" runat="server" visible="false" class="TableBlock">
            <th><asp:Label ID="lblResponse" runat="server" Visible="false"></asp:Label></th>
            </tr>             
            
        </table> 
        </p>    
        </div>      
        <table border="1"> 
            <TR bgcolor="#3399cc">
            <TH><div class="HeaderDiv" align="center">File Format</div></TH>            
            <TH><div class="HeaderDiv" align="center">Delete</div></th>            
            <TH><div class="HeaderDiv" align="center">Action</div></th>
            </TR>        
            <tr bgcolor="#cccccc"> 
            <TH><asp:Label ID="lblType" runat="server" Font-Names="Verdana" Font-Size="10"></asp:label>                                                                 
            </TH>            
            <TH><asp:CheckBox ID="chkDelete" runat="server"/></th>            
            <TH>               
                <cc0:eximagebutton id="btnEdit" runat="server" DisableImageURL="../images/toolbar/i_filterP.gif" Text="View Instruction" ImageUrl="../images/toolbar/i_filter.gif" OnClick="btnEdit_Click"></cc0:eximagebutton>
                <cc0:eximagebutton id="btnAddNew" runat="server" DisableImageURL="../images/toolbar/i_newP.gif" Text="Add New Instructions" ImageUrl="../images/toolbar/i_new.gif" onclick="btnAddNew_Click"></cc0:eximagebutton>                
                <INPUT id="btnSave" runat="server" TYPE="image" SRC="../Images/Toolbar/i_save.gif" title="Save Instructions" onclick="SaveIt()" language="VBScript" width="37" height="37">
            </th>
            </TR>
            <tr id="TRFile" runat="server"><td colspan="3"><input type="file" id="File1" runat="server" /> </td>            
            </tr>                               
        </table>        
    </asp:View>
     </asp:MultiView>                     
        <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server"
                TargetControlID="DDLAccounts" PromptCssClass="ListSearchExtenderPrompt">
        </ajaxToolkit:ListSearchExtender>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>    
    <object classid="clsid:C3A57B60-C117-11D2-BD9B-00105A0A7E89" id="SAXFile">
	</object>	
    </form>
</body>
</html>
