<%@ Page Language="VB" ValidateRequest="false" Trace="false" %>

<%@ Import Namespace="System.Data" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<script runat="server">
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        
    End Sub

    Protected Sub SaveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        
        Try
            Using sw As New System.IO.StreamWriter(Server.MapPath("/ETS_Files") & "/Instructions/" & DDLTemplates.SelectedItem.Value & ".htm", False)
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
    Private Function LoadInstructions(ByVal TempID As String) As Boolean
        Try
            Dim line As String = "<HTML></head><body style='margin-right: 7cm;'></body></HTML>"
            'If Not IsPostBack Then           
            If IO.File.Exists(Server.MapPath("/ETS_Files") & "/Instructions/" & TempID & ".htm") Then
                Using sr As New System.IO.StreamReader(Server.MapPath("/ETS_Files") & "/Instructions/" & TempID & ".htm")
                    line = sr.ReadToEnd()
                End Using
            End If
           
            ' End If
            FreeTextBox1.Text = line
            lblResponse.Text = ""
        Catch ex As Exception
            lblResponse.Text = ex.Message
        End Try
    End Function
   
    
    Protected Sub btnUnlockList1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        btnUnlockList1.Visible = False
        DDLTemplates.Enabled = True
    End Sub
    
    Protected Sub DDLTemplates_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If DDLTemplates.SelectedIndex = 0 Then
            SaveButton.Enabled = False
        Else
            LoadInstructions(DDLTemplates.SelectedItem.Value)
            SaveButton.Enabled = True
            DDLTemplates.Enabled = False
            btnUnlockList1.Visible = True
        End If
       
    End Sub
    Protected Sub btnTempSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        loadTemplates()
    End Sub
    Private Function loadTemplates() As Boolean
        Try
            Dim DSTemp As New DataSet
            Dim clsAcc As New ETS.BL.Templates
            With clsAcc
                .ContractorID = Session("ContractorID").ToString
                ._WhereString.Append(" and TemplateName like '" & txtTemplate.Text & "'")
                
                DSTemp = .getTemplateList
                DDLTemplates.DataSource = DSTemp
                DDLTemplates.DataValueField = "TemplateID"
                DDLTemplates.DataTextField = "TemplateName"
                DDLTemplates.DataBind()
                DSTemp.Dispose()
                Dim LI As New ListItem("", Guid.NewGuid().ToString)
                DDLTemplates.Items.Insert(0, LI)
                LI.Selected = True
                LI = Nothing
                DDLTemplates.Enabled = True
            End With
            clsAcc = Nothing
        Catch ex As Exception

        End Try
    End Function
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
	<title>Template Instructions</title>
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
                <asp:Label ID="Label1" runat="server" Text="Search Templates:"></asp:Label>
                <asp:TextBox ID="txtTemplate" runat="server"></asp:TextBox>
                <asp:Button ID="btnTempSearch" runat="server" Text="Search" OnClick="btnTempSearch_Click" Visible="True" ToolTip="Click here to search templates"/>
            </asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow >
            <asp:TableHeaderCell ColumnSpan="2" BorderStyle="Double">
                <asp:Label ID="Label2" runat="server" Text="Select Template:"></asp:Label>
                <asp:DropDownList ID="DDLTemplates" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLTemplates_SelectedIndexChanged">
                </asp:DropDownList><asp:Button ID="btnUnlockList1" runat="server" Text="..." OnClick="btnUnlockList1_Click" Visible="false" ToolTip="Click here to change the template"/>
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
