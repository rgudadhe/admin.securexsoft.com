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
        
        Try
            Using sw As New System.IO.StreamWriter(Server.MapPath("/ETS_Files") & "/Instructions/" & DDLPhy.SelectedItem.Value & ".htm", False)
                sw.Write(FreeTextBox1.Text)
                sw.Close()
            End Using
            'Dim clsAI As New ETS.BL.AccountInstructions
            'With clsAI
            '    .AccountID = DDLAccounts.SelectedItem.Value
            '    .DateModified = Now
            '    .Format = ".htm"
            '    .UserID = Session("UserID").ToString
            '    .IsDeleted = False
            '    If .SetAccountInstructions_btnClicked() Then
            lblResponse.Text = "Changes have been saved successfully!"
            '    End If
            'End With
        Catch ex As Exception
            lblResponse.Text = "Saving changes failed!"
            Response.Write(ex.Message)
            Response.End()
        End Try
               
    End Sub
    Private Function LoadInstructions(ByVal DictID As String) As Boolean
        Try
            Dim line As String = "<HTML></head><body style='margin-right: 7cm;'></body></HTML>"
            'If Not IsPostBack Then           
            If IO.File.Exists(Server.MapPath("/ETS_Files") & "/Instructions/" & DictID & ".htm") Then
                Using sr As New System.IO.StreamReader(Server.MapPath("/ETS_Files") & "/Instructions/" & DictID & ".htm")
                    line = sr.ReadToEnd()
                End Using
            End If
           
            ' End If
            FreeTextBox1.Text = line
            lblResponse.Text = ""
        Catch ex As Exception

        End Try
    End Function
   
    Protected Sub btnUnlockList_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        btnUnlockList.Visible = False
        DDLAccounts.Enabled = True
    End Sub
    Protected Sub btnUnlockList1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        btnUnlockList1.Visible = False
        DDLPhy.Enabled = True
    End Sub
    Protected Sub DDLAccounts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        fnOnLoad()
    End Sub
    Protected Sub DDLPhy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If DDLPhy.SelectedIndex = 0 Then
            SaveButton.Enabled = False
        Else
            LoadInstructions(DDLPhy.SelectedItem.Value)
            SaveButton.Enabled = True
            DDLPhy.Enabled = False
            btnUnlockList1.Visible = True
        End If
       
    End Sub
    Protected Function fnOnLoad() As Boolean
        Try
            If DDLAccounts.SelectedIndex = 0 Then
                DDLPhy.Items.Clear()
                DDLPhy.Enabled = False
                
            Else
                
                SaveButton.Enabled = True
                DDLAccounts.Enabled = False
                btnUnlockList.Visible = True
                loadDictators()
                
            End If
        
        Catch ex As Exception

        End Try
    End Function
    Private Function loadDictators() As Boolean
        Try
            Dim DSPhy As New DataSet
            Dim clsAcc As New ETS.BL.Physicians
            With clsAcc
                
                DSPhy = .getPhysiciansList(Session("ContractorID").ToString, Session("WorkGroupID").ToString, DDLAccounts.SelectedItem.Value)
                DDLPhy.DataSource = DSPhy
                DDLPhy.DataValueField = "PhysicianID"
                DDLPhy.DataTextField = "FullName"
                DDLPhy.DataBind()
                DSPhy.Dispose()
                Dim LI As New ListItem("", Guid.NewGuid().ToString)
                DDLPhy.Items.Insert(0, LI)
                LI.Selected = True
                LI = Nothing
                DDLPhy.Enabled = True
            End With
            clsAcc = Nothing
        Catch ex As Exception
            response.write( ex.message)
        End Try
    End Function
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
	<title>Dictator Instructions</title>
    <link href= "../App_Themes/Css/styles.css" type="text/css" rel="stylesheet"/>
    <link href= "../App_Themes/Css/Common.css" type="text/css" rel="stylesheet"/>
<script>

</script>
</head>
<body>

    <form id="Form1" runat="server">
    <asp:Table ID="Table1" runat="server" Width="100%">
        <asp:TableHeaderRow >
            <asp:TableHeaderCell BorderStyle="Double">
                <asp:Label ID="Label1" runat="server" Text="Account Name:"></asp:Label>
                <asp:DropDownList ID="DDLAccounts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLAccounts_SelectedIndexChanged">
                </asp:DropDownList><asp:Button ID="btnUnlockList" runat="server" Text="..." OnClick="btnUnlockList_Click" Visible="false" ToolTip="Click here to change the Account"/>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow >
            <asp:TableHeaderCell ColumnSpan="2" BorderStyle="Double">
                <asp:Label ID="Label2" runat="server" Text="Dictator Name:"></asp:Label>
                <asp:DropDownList ID="DDLPhy" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLPhy_SelectedIndexChanged">
                </asp:DropDownList><asp:Button ID="btnUnlockList1" runat="server" Text="..." OnClick="btnUnlockList1_Click" Visible="false" ToolTip="Click here to change the dictator"/>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow  >
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
