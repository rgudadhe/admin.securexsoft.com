Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing.Imaging
Imports system.drawing

Partial Class CreateUser_UserPhoto
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'get original bitmap
        Dim strPath As String
        strPath = Request("phphoto").ToString
        Dim myBitmap As New Bitmap(Server.MapPath(strPath))

        'create thumbnail from the orginal bitmap
        Dim myThumbNail As New Bitmap(myBitmap, 220, 180)

        'Set the content type
        Response.ContentType = "image/jpeg"

        'send the thumbnail bitmap to the outputstream
        myThumbNail.save(Response.OutputStream, ImageFormat.Jpeg)

        'Response.BinaryWrite(Request("phphoto").ToString)
        'Response.ContentType = "Image/Jpeg"

        'Response.Redirect("~/Images/images.jpg")

    End Sub
End Class
