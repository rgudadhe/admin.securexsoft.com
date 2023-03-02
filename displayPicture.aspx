<%@ Page language="C#" %>
<%@ Import NameSpace="System.IO" %>
<%@ Import Namespace="System.Drawing" %> 
<%@ Import Namespace="System.Drawing.Imaging" %>
<script runat="server">
/* displayPicture.aspx - main section of ASP.NET Photo Gallery
   Originally Created by Waiki Lee (http://www.waiki.ca) <><
   
   uploaded to Devhood (http://www.devhood.com)
   This section of the Photo Gallery displays the image given a path
   and image parameter.  There are two constants that you can modify to
   adjust what the maximum size is for these display images.
 
   all generated images are placed under an _image sub-directory
   the original image file is not modified.
*/

  private const int MAX_WIDTH = 640;
  private const int MAX_HEIGHT = 480;
  private const int ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT; // used to determine limiting factor
  
  private void Page_Load(Object sender, EventArgs e) {
    String relativePath, fullPath;
    if (Request.QueryString.Get("path") != null) relativePath = Request.QueryString.Get("path");
	else relativePath = "";
	fullPath = Server.MapPath(relativePath);
	// NOTE:  no security precautions taken with path tampering...
	
	String pictureFileName = Request.QueryString.Get("picture");
	String imagePath = fullPath + "\\_images";
	String originalFileName = fullPath + "\\" + pictureFileName;
	String displayFileName = imagePath + "\\" + pictureFileName;
	
	if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);
	if (!File.Exists(displayFileName)) createImage(originalFileName, displayFileName);
	theImage.ImageUrl = relativePath + "\\_images\\" + pictureFileName;
	originalLink.HRef = relativePath + "\\" + pictureFileName;
  }
  
  private void createImage(String originalImage, String newImageFile) {
    System.Drawing.Image original = System.Drawing.Image.FromFile(originalImage);
	int newWidth, newHeight;

	if (original.Width > MAX_WIDTH || original.Height > MAX_HEIGHT) {
	  int factor;
      // determine the largest factor 
	  if (original.Width / original.Height > ASPECT_RATIO) {
  	    factor = original.Width / MAX_WIDTH;
		newWidth = original.Width / factor;
		newHeight = original.Width / factor;
	  } else {
	    factor = original.Height / MAX_HEIGHT;
		newWidth = original.Width / factor;
		newHeight = original.Height / factor;
	  }	  
	} else {
	  newWidth = original.Width;
	  newHeight = original.Height;
	}
	
	System.Drawing.Image displayImage = new Bitmap(original,newWidth,newHeight);
	original.Dispose();
	displayImage.Save(newImageFile, ImageFormat.Jpeg);    
  }
</script>

<html>
<head>
	<title>Main</title>
</head>

<body bgcolor="black" leftmargin="0" rightmargin="0" topmargin="0" bottommargin="0">
  <center>
    <asp:Image id="theImage" runat="server"/><br />
    [<a id="originalLink" runat="server">View Full Quality Image</a>]
  </center>
</body>
</html>
