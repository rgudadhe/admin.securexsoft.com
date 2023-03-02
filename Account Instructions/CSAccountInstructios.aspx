<%@ Page Language="VB" ValidateRequest="false" Trace="false" %>

<%@ Import Namespace="System.Data" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not IsPostBack Then
            Dim DSAcc As New DataSet
            Dim clsAcc As New ETS.BL.Accounts
            With clsAcc
                '.ContractorID = Session("ContractorID").ToString
                DSAcc = .getAccountList(Session("WorkGroupID").ToString, Session("ContractorID").ToString, String.Empty)
                DDLAccounts.DataSource = DSAcc
                DDLAccounts.DataValueField = "AccountID"
                DDLAccounts.DataTextField = "AccountName"
                DDLAccounts.DataBind()
                DSAcc.Dispose()
                Dim LI As New ListItem("", Guid.NewGuid().ToString)
                DDLAccounts.Items.Insert(0, LI)
                LI.Selected = True
                LI = Nothing
            End With
            clsAcc = Nothing
            fnOnLoad
        End If
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Index As Integer
        Try
            Index = lstIndex.SelectedIndex
            Using sw As New System.IO.StreamWriter(Server.MapPath("/ETS_Files") & "/Instructions/" & DDLAccounts.SelectedItem.Value & Index & ".htm", False)
                sw.Write(FreeTextBox1.Text)
                sw.Close()
            End Using
            dim format as string
            if chkSetForMTC.checked=true then 
                format=".htm"
             else
                format=".xls"
             end if
            Dim clsAI As New ETS.BL.AccountInstructions
            
             clsAI = New ETS.BL.AccountInstructions
             with clsAI       
                .AccountID = DDLAccounts.SelectedItem.Value
                
                
                .DateModified = Now
                .Format =  format
                .UserID = Session("UserID").ToString
                .IsDeleted = False                
                If .SetAccountInstructions_btnClicked() Then
                    lblResponse.Text = "Changes have been saved successfully!"
                End If
            End With
           
        Catch ex As Exception
            lblResponse.Text = "Saving changes failed!"
            Response.Write(ex.Message)
            Response.End()
        End Try
               
    End Sub
    Private Function LoadInstructions(ByVal index As Integer) As Boolean
        Try
        dim format as string
        Dim clsAI As New ETS.BL.AccountInstructions
            With clsAI 
                    .AccountID = DDLAccounts.SelectedItem.Value           
                    .getAIDetails
                   format=.Format
             end with
             if format=".htm" then 
                chkSetForMTC.checked=true
             else
                chkSetForMTC.checked=false
             end if
            Dim line As String = "<HTML></head><body style='margin-right: 7cm;'></body></HTML>"
            'If Not IsPostBack Then           
            If IO.File.Exists(Server.MapPath("/ETS_Files") & "/Instructions/" & DDLAccounts.SelectedItem.Value & index & ".htm") Then
                Using sr As New System.IO.StreamReader(Server.MapPath("/ETS_Files") & "/Instructions/" & DDLAccounts.SelectedItem.Value & index & ".htm")
                    line = sr.ReadToEnd()
                End Using
            End If
           
            ' End If
            FreeTextBox1.Text = line
            lblResponse.Text = ""
        Catch ex As Exception

        End Try
    End Function
    Protected Sub lstIndex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        
        DDLAccounts.Enabled = False
        btnUnlockList.Visible = True
        
        LoadInstructions(lstIndex.SelectedIndex)
    End Sub

    Protected Sub btnUnlockList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        btnUnlockList.Visible = False
        DDLAccounts.Enabled = True
    End Sub

    Protected Sub DDLAccounts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        fnOnLoad()
    End Sub
    Protected Function fnOnLoad() As Boolean
        Try
            If DDLAccounts.SelectedIndex = 0 Then
                lstIndex.Visible = False
                SaveButton.Enabled = False
            Else
                lstIndex.Visible = True
                SaveButton.Enabled = True
                DDLAccounts.Enabled = False
                btnUnlockList.Visible = True
                lstIndex.SelectedIndex = 0
                LoadInstructions(0)
            End If
        
        Catch ex As Exception

        End Try
    End Function
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
	<title>Account Instructions</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
     
<script>

</script>
</head>
<body>

    <form id="Form1" runat="server">
    <asp:Table ID="Table1" runat="server" Width="100%">
        <asp:TableHeaderRow >
            <asp:TableHeaderCell ColumnSpan="2" BorderStyle="Double">
                <asp:Label ID="Label1" runat="server" Text="Account Name:"></asp:Label>
                <asp:DropDownList ID="DDLAccounts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLAccounts_SelectedIndexChanged">
                </asp:DropDownList><asp:Button ID="btnUnlockList" runat="server" Text="..." OnClick="btnUnlockList_Click" Visible="false" ToolTip="Click here to change the Account"/>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow  >
            <asp:TableCell VerticalAlign="Top">
                <div>
                <asp:RadioButtonList ID="lstIndex" runat="server" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="lstIndex_SelectedIndexChanged">
                <asp:ListItem Text="Special Info" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Text Formatting" Value="1"></asp:ListItem>
                <asp:ListItem Text="Headings" Value="2"></asp:ListItem>
                <asp:ListItem Text="Frequently Used Terms" Value="3"></asp:ListItem>
                <asp:ListItem Text="Templates" Value="4"></asp:ListItem>
                <asp:ListItem Text="Extended Info" Value="5"></asp:ListItem>
                <asp:ListItem Text="Comments" Value="6"></asp:ListItem>                                
                <asp:ListItem Text="Previous Reports" Value="7"></asp:ListItem>
                <asp:ListItem Text="Addressee/CC" Value="8"></asp:ListItem> 
                <asp:ListItem Text="CheckIn" Value="9"></asp:ListItem>
                <asp:ListItem Text="Pulled Notes" Value="10"></asp:ListItem>            
                <asp:ListItem Text="Macros" Value="11"></asp:ListItem>
                <asp:ListItem Text="General Instructions" Value="12"></asp:ListItem>                 
                </asp:RadioButtonList>
                </div>
            </asp:TableCell>
            <asp:TableCell >
                   <div >   	
    	    	    		
		        <FTB:FreeTextBox id="FreeTextBox1" Focus="true" OnSaveClick="SaveButton_Click" 
			        toolbarlayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertForm,InsertTextBox,InsertTextArea,InsertRadioButton,InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell"
			        runat="Server"
			        DesignModeCss="designmode.css" 
			        SupportFolder="FreeTextBox/"
			        JavaScriptLocation="ExternalFile" ButtonImagesLocation="ExternalFile"
			        ToolbarImagesLocation="ExternalFile"
                    ToolbarStyleConfiguration="OfficeXP"
                    ButtonSet="Office2000"
                    GutterBackColor="red"                
			        />
			        <asp:CheckBox ID="chkSetForMTC" Text="<b>Activate C. S. Instructions in SecureMT Client.</b>" runat="server"/>
			        <span></span>
        	         <asp:Button id="SaveButton" Text="Save" onclick="SaveButton_Click" runat="server"  />		
                      <span></span>                       <asp:Label ID="lblResponse" runat="server" Text=""></asp:Label>
		        </div>
        		
		        <div>		 
			        <asp:Literal id="Output" runat="server" />			
		        </div>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
    

		
		

	</form>
	
</body>
</html>
