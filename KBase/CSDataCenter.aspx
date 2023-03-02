<%@ Page Language="C#" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="mb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
<script runat="server" type="text/C#">
		protected void Page_Load(object sender, EventArgs e)
		{
			object sessionObject = Session["UserID"];
			if (sessionObject == null) { 
			Response.Write ("Invalid session,please login again!!!");
			Response.End();
			}
		}
	</script>
    <script runat="server">

    /// <summary>
    /// Keep track of the value path to the currently
    /// selected node so we can look it up as needed
    /// </summary>
    private string CurrentNodeValuePath
    {
        get { return this.ViewState["CurrentNodeValuePath"] as string; }
        set { this.ViewState["CurrentNodeValuePath"] = value; }
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void GvFolderItems_RowDataBound(object sender, GridViewRowEventArgs args)
    {
        if (args.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imageButton = (ImageButton)args.Row.FindControl("btnItemIcon");
            LinkButton linkButton = (LinkButton)args.Row.FindControl("btnItemName");

            if (args.Row.DataItem is System.IO.DirectoryInfo)
            {
                imageButton.ImageUrl = @"Img/folder.gif";
                imageButton.CommandName = "OpenFolder";
                linkButton.CommandName = "OpenFolder";
            }
            else
            {
                System.IO.FileInfo oFile = (System.IO.FileInfo)args.Row.DataItem;

                string fName = oFile.Extension.ToLower();
                switch (fName)
                {
                    case ".doc":
                        imageButton.ImageUrl = @"Img/word.jpg";
                        break;
                    case ".docx":
                        imageButton.ImageUrl = @"Img/word.jpg";
                        break;
                    case ".pdf":
                        imageButton.ImageUrl = @"Img/pdf.gif";
                        break;
                    case ".wav":
                        imageButton.ImageUrl = @"Img/Audio.jpg";
                        break;
                    case ".dss":
                        imageButton.ImageUrl = @"Img/Audio.jpg";
                        break;
		case ".zip":
                        imageButton.ImageUrl = @"Img/txt.jpg";
                        break;

                    default:
                        imageButton.ImageUrl = @"Img/txt.jpg";
                        
                        break;
                }               
                
                
                
                
                imageButton.CommandName = "OpenFile";
                linkButton.CommandName = "OpenFile";                

                // register both the linkbutton and imagebutton as full postback
                // controls
                ScriptManager scriptManager = ScriptManager.GetCurrent(this);
                scriptManager.RegisterPostBackControl(linkButton);
                scriptManager.RegisterPostBackControl(imageButton);

                System.IO.FileInfo fileInfo = (System.IO.FileInfo)args.Row.DataItem;
                Label lblSize = (Label)args.Row.FindControl("lblSize");
                lblSize.Text = string.Format("{0:N0} KB", fileInfo.Length / 1000);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary> 
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void GvFolderItems_RowCommand(object sender, GridViewCommandEventArgs args)
    {
        // handle either opening the item or rebinding the grid
        if (args.CommandName == "OpenFile")
        {
            //  they clicked on a file, download it
            string name = (string)args.CommandArgument;
            string fullFilePath = System.IO.Path.Combine(this.BuildFullFilePath(this.CurrentNodeValuePath), name);

            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fullFilePath);
            
            this.Response.Clear();
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileInfo.Name);
            this.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            this.Response.ContentType = "application/octet-stream";
            this.Response.WriteFile(fileInfo.FullName);
            this.Response.End();
        }
        else if (args.CommandName == "OpenFolder")
        {
            string name = (string)args.CommandArgument;

            TreeNode node = this.tvFolders.FindNode(string.Format("{0}{1}{2}", this.CurrentNodeValuePath, this.tvFolders.PathSeparator, name));
            node.Selected = true;
            this.CurrentNodeValuePath = node.ValuePath;

            // expand the parents
            TreeNode parentNode = node.Parent;
            while (parentNode != null)
            {
                parentNode.Expanded = true;
                parentNode = parentNode.Parent;
            }

            // bind the gridview to the datasource
            BindDirsContents(node);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void TvFolders_SelectedNodeChanged(object sender, EventArgs args)
    {
        this.CurrentNodeValuePath = this.tvFolders.SelectedNode.ValuePath;
        BindDirsContents(this.tvFolders.SelectedNode);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    private void BindDirsContents(TreeNode node)
    {
        // bind the gridview to the datasource
        this.gvFolderItems.DataSource = new System.IO.DirectoryInfo(this.BuildFullFilePath(node.ValuePath)).GetFileSystemInfos();
        this.gvFolderItems.DataBind();
    }

    /// <summary>
    /// Converts a nodes ValuePath into the full file/folder path
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private string BuildFullFilePath(string valuePath)
    {
        string rootPath = HttpContext.Current.Server.MapPath(System.IO.Path.Combine(this.Request.ApplicationPath, this.fileSystemDataSource.RootPath));
        return System.IO.Path.Combine(rootPath, valuePath);
    }
    
    </script>
</head>
<body>
    <form id="frm" runat="server">
        <asp:ScriptManager ID="scriptManager" runat="server" />
        <mb:FileSystemDataSource ID="fileSystemDataSource" runat="server" RootPath="CSDataCenter" FoldersOnly="true" />
        
        <div>            
            
            
 
            <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="tvFolders" EventName="SelectedNodeChanged" />
                </Triggers>                            
                <ContentTemplate>   
                    <table id="tbl" cellpadding="0px" cellspacing="0px">            
                        <tr>
                            <td style="border:solid 1px black" valign="top">
                                <div style="overflow:auto;width:300px;height:350px;">
                                    <asp:TreeView ID="tvFolders" runat="server" DataSourceID="fileSystemDataSource"
                                        OnSelectedNodeChanged="TvFolders_SelectedNodeChanged">
                                        <NodeStyle ImageUrl="Img/folder.gif" HorizontalPadding="3px" />
                                        <SelectedNodeStyle Font-Underline="true" />
                                    </asp:TreeView> 
                                </div>
                            </td>
                            <td style="border:solid 1px black" valign="top">                            
                                <div style="overflow:auto;width:450px;height:350px;">
                                    <mb:GridView 
                                        ID="gvFolderItems" runat="server" 
                                        EmptyDataText="This folder is empty." 
                                        GridLines="None" Width="100%" BorderStyle="None" 
                                        OnRowDataBound="GvFolderItems_RowDataBound" 
                                        OnRowCommand="GvFolderItems_RowCommand" 
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                        <HeaderStyle HorizontalAlign="Left" BackColor="BurlyWood" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <%--The image is set when row is databound--%>
                                                    <asp:ImageButton 
                                                        ID="btnItemIcon" runat="server" 
                                                        ImageAlign="AbsMiddle" 
                                                        CommandArgument='<%# Eval("Name") %>' />
                                                    <asp:LinkButton 
                                                        ID="btnItemName" runat="server" 
                                                        Text='<%# Eval("Name") %>' 
                                                        CommandArgument='<%# Eval("Name") %>' 
                                                        Font-Underline="false" 
                                                        ForeColor="black" /> 
                                                </ItemTemplate>     
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>
                                                    <%--The image is set when row is databound--%>
                                                    <asp:Label ID="lblSize" runat="server" />                                                
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                       </Columns>
                                    </mb:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>  
                </ContentTemplate>
            </asp:UpdatePanel>            
            <br /> 
        </div>        
    </form>    
</body>
</html>
