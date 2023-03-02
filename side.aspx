<%@ Page language="c#" %>
<%@ Import NameSpace="System.IO" %>
<script runat="server">
/*  SIDE.ASPX - Side frame of the Self-Resizing Photo Gallery
    Created by: Waiki Lee (http://www.waiki.ca)
    Uploaded to Devhood  http://www.devhood.com
	
    This ASP.NET page will generate a list of JPG images and sub-directories given a relative path
    to view.  All image links target the main frame of the Photo Gallery with the correct parameters.

    The default image directory is specified as the constant ROOT_DIRECTORY.  This application is
    restricted to viewing all files under this sub-directory.  The application is to be placed one
    level above this directory.
*/
  private const String ROOT_DIRECTORY = "Pictures";
  private String relativePath;
  private String fullPath;

  private void Page_Load(Object sender, EventArgs e) {
    if (Request.QueryString.Get("dir") != null) relativePath = Request.QueryString.Get("dir");
	else relativePath = ROOT_DIRECTORY;
    fullPath = Server.MapPath(relativePath);
		
    // security precaution, do allow the possiblity of travelling higher than present folder
    // this prevents knowledgable users from using this application to browse your directory structure and JPG's
    // you can either stop loading with "return", redirect to an ERROR/WARNING page, or redirect to ROOT_DIRECTORY
    // if (relativePath.IndexOf("..") != -1 || !relativePath.StartsWith(ROOT_DIRECTORY)) Response.Redirect("/stopHacking.aspx");
    // if (relativePath.IndexOf("..") != -1 || !relativePath.StartsWith(ROOT_DIRECTORY)) Response.Redirect(Request.Path);
       if (relativePath.IndexOf("..") != -1 || !relativePath.StartsWith(ROOT_DIRECTORY)) return;

    displayCurrentLocation();
    displaySubDirectories();
    displayPictures();	
  } // end Page_Load
  
  private void displayCurrentLocation() {
    String[] path = relativePath.Split(new char[] { '\\' });	
    String cumulativePath = path[0];
    for (int i=0; i < path.Length-1; i++) {    
      CurrentLocation.Text += " &gt; <a href=\"side.aspx?dir="+cumulativePath+"\">" + path[i] + "</a>";
      cumulativePath += "\\"+path[i+1];
    }
    CurrentLocation.Text += " &gt; <b>"+ path[path.Length-1] +"</b>";	
  } // end displayCurrentLocation
  
  private void displaySubDirectories() {
    String[] directories = Directory.GetDirectories(fullPath);
    if (directories.Length == 0) return;  // don't bother with remainder of method if no sub-directories
    
    String directoryName;
    String currentPath = relativePath + "\\";
    foreach (String dir in directories) {
      directoryName = shortName(dir);
      if (!directoryName.StartsWith("_"))
        DirectoryList.Text += "<li><a href=\"side.aspx?dir="+currentPath+directoryName+"\">" + directoryName + "</a></li>";
    }// end for
	
    DirectoryList.Text = "Sub-Folders: <ul>"+DirectoryList.Text+"</ul>";	
  } // end displaySubDirectories()
  
  private void displayPictures() {
    String[] pictures = Directory.GetFiles(fullPath, "*.jpg");	
    if (pictures.Length == 0) {
      PictureList.Text = "There are no pictures in this folder.";
      return;  // if no pictures, skip remainder of method
    } 

    String pictureName;
    PictureList.Text = "Pictures: <br />";
    foreach (String picture in pictures) {
      pictureName = shortName(picture);
      PictureList.Text += "<a href=\"displayPicture.aspx?path="+relativePath+"&picture="+pictureName+"\" target=\"main\">" + pictureName + "</a><br />";
    }
  } // end displayPictures()
  
  //  returns JUST the folder/file name (ie, not previous path information)
  private String shortName(String path) { return path.Substring(path.LastIndexOf("\\") + 1);  }
</script>

<html>
<head>
	<title></title>
</head>

<body bgcolor="lightgrey" leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
  <asp:label id="CurrentLocation" runat="server" />
  <br /><br />
  <asp:label id="DirectoryList" runat="server" />
  <asp:label id="PictureList" runat="server" />
</body>
</html>
