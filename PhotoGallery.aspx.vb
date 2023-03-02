Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType
Imports System.IO


Partial Class ets_profile
    Inherits BasePage

    Private Const ROOT_DIRECTORY As String = "Photo Albums"
    Private relativePath As String
    Private fullPath As String


    Private Sub displayCurrentLocation()
        ' end Page_Load 
        Dim path As String() = relativePath.Split(New Char() {"\"c})
        Dim cumulativePath As String = path(0)
        Dim strSpace As String
        strSpace = ""
        CurrentLocation.Text = "<table>"
        For i As Integer = 0 To path.Length - 2
            CurrentLocation.Text += "<tr><td>" + strSpace + "<a href=""photogallery.aspx?dir=" + cumulativePath + """><img src='images/Folder.png'>" + path(i) + "</a></td></tr>"
            cumulativePath += "\" + path(i + 1)
            strSpace += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
        Next
        CurrentLocation.Text += "<tr><td>" + strSpace + "<img src='images/Folder_share.png'> <b>" + path(path.Length - 1) + "</b></td></tr></table>"
    End Sub
    Private Sub displaySubDirectories()
        ' end displayCurrentLocation 
        Dim directories As String() = Directory.GetDirectories(fullPath)
        If directories.Length = 0 Then
            Return
        End If
        ' don't bother with remainder of method if no sub-directories 
        Dim directoryName As String
        Dim currentPath As String = relativePath + "\"
        DirectoryList.Text = "<table>"
        For Each dir As String In directories
            directoryName = shortName(dir)
            If Not directoryName.StartsWith("_") Then
                DirectoryList.Text += "<tr><td valign=middle><a href=""photogallery.aspx?dir=" + currentPath + directoryName + """><img src='images/Folder.png'> " + directoryName + "</a></td></tr>"
            End If
        Next
        DirectoryList.Text += "</table>"
        ' end for 
        DirectoryList.Text = "Photo Gallery: <ul>" + DirectoryList.Text + "</ul>"
    End Sub
    Private Sub displayPictures()
        ' end displaySubDirectories() 
        Dim pictures As String() = Directory.GetFiles(fullPath, "*.jpg")

        If pictures.Length = 0 Then
            If HRoot.Value = "" Then
                PictureList.Text = "There are no pictures in this folder."
            End If

            ' if no pictures, skip remainder of method 
            Return
        End If

        Dim pictureName As String
        PictureList.Text = "Pictures: <br />"
        PictureList.Text = "<table width=440 cellPadding=2 cellspacing=2>"
        Dim i As Integer
        Dim Cphotos As Integer
        Cphotos = 0

        i = 0
        For Each picture As String In pictures



            If i = 0 Then
                PictureList.Text = PictureList.Text & "<tr>"
            End If
            PictureList.Text = PictureList.Text & "<td><table border='2' cellpadding='2' style='margin-top:0px; margin-left:0px;'><tr><td style='background-color: gainsboro;' ><a href='photoGallery.aspx?dir=" & relativePath & "&imgindex=" & Cphotos & "&ShowImg=True'><img id='Image1' src='phphoto.aspx?phphoto=" & relativePath & "/" & shortName(picture) & "' style='border-width:4px;border-style:Groove;height:90px;width:110px;' /></a></td></tr></table></td>"
            Cphotos = Cphotos + 1

            i = i + 1
            'Dim Cell1 As New TableCell
            'Dim Img As New Image

            'Img.Width = "110"
            'Img.Height = "90"
            'Img.Style("background") = "#FAFAFA"
            'Img.Style("margin") = "5px 0px 10px 10px"
            ''Img.Style("background") = "#FAFAFA"

            'Img.BorderColor = Drawing.Color.LightGray
            'Img.BorderStyle = BorderStyle.Solid
            'Img.BorderWidth = "1"


            'Img.ImageUrl = relativePath & "/" & shortName(picture)
            'Cell1.Controls.Add(Img)
            'Row1.Cells.Add(Cell1)
            If i = 3 Then
                PictureList.Text = PictureList.Text & "</tr>"
                i = 0
            End If
            ' Response.Write(relativePath & "#" & shortName(picture))

            'pictureName = shortName(picture)
            'PictureList.Text += "<a href=""displayPicture.aspx?path=" + relativePath + "&picture=" + pictureName + """ target=""main"">" + pictureName + "</a><br />"
        Next
        PictureList.Text = PictureList.Text & "</table>"
        lblCount.Text = "Total Photos: " & Cphotos

    End Sub
    ' end displayPictures() 
    ' returns JUST the folder/file name (ie, not previous path information) 
    Private Function shortName(ByVal path As String) As String
        Return path.Substring(path.LastIndexOf("\") + 1)
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HRoot.Value = ""
        'Response.Write(Request("dir"))

        If Request.QueryString.[Get]("dir") IsNot Nothing Then
            relativePath = Request.QueryString.[Get]("dir")
        Else

            relativePath = ROOT_DIRECTORY
        End If
        If relativePath = ROOT_DIRECTORY Then
            HRoot.Value = "Yes"
        End If
        fullPath = Server.MapPath(relativePath)

        'Response.Write(fullPath)


        ' security precaution, do allow the possiblity of travelling higher than present folder 
        ' this prevents knowledgable users from using this application to browse your directory structure and JPG's 
        ' you can either stop loading with "return", redirect to an ERROR/WARNING page, or redirect to ROOT_DIRECTORY 
        ' if (relativePath.IndexOf("..") != -1 || !relativePath.StartsWith(ROOT_DIRECTORY)) Response.Redirect("/stopHacking.aspx"); 
        ' if (relativePath.IndexOf("..") != -1 || !relativePath.StartsWith(ROOT_DIRECTORY)) Response.Redirect(Request.Path); 
        If relativePath.IndexOf("..") <> -1 OrElse Not relativePath.StartsWith(ROOT_DIRECTORY) Then
            Return
        End If
        If Request("ShowImg") = "True" Then
            lblShow.Text = "<a href='photoGallery.aspx?dir=" & relativePath & "'>Show All Photos</a>"
            displayCurrentLocation()
            Dim Photos As String() = Directory.GetFiles(fullPath, "*.jpg")
            Dim j As Integer = Photos.Length
            lblCount.Text = "Total Photos: " & j
            Dim FileIndex As Integer
            FileIndex = j - 1
            Dim imgindex As Integer
            imgindex = Request("imgindex")
            If imgindex = 0 Then
                LPrevious.Visible = False
                LNext.Visible = True
                LNext.Text = "<a href='photoGallery.aspx?dir=" & relativePath & "&imgindex=" & imgindex + 1 & "&ShowImg=True'>Next</a>"
            ElseIf imgindex = FileIndex Then
                LNext.Visible = False
                LPrevious.Visible = True
                LPrevious.Text = "<a href='photoGallery.aspx?dir=" & relativePath & "&imgindex=" & imgindex - 1 & "&ShowImg=True'>Previous</a>"
            Else
                LPrevious.Visible = True
                LNext.Visible = True
                LPrevious.Text = "<a href='photoGallery.aspx?dir=" & relativePath & "&imgindex=" & imgindex - 1 & "&ShowImg=True'>Previous</a>"
                LNext.Text = "<a href='photoGallery.aspx?dir=" & relativePath & "&imgindex=" & imgindex + 1 & "&ShowImg=True'>Next</a>"

            End If
            'For Each picture As String In Photos

            iphoto.ImageUrl = relativePath & "/" & shortName(Photos(imgindex))

            'iphoto.ImageUrl = "phphoto.aspx?phphoto=" & relativePath & "/" & shortName(picture)
            'Response.Write("<script language=JavaScript> alert(" & imgindex & "); <" + "/script>")
            'System.Threading.Thread.Sleep(3000)
            'If imgindex + 1 < j Then

            '    Response.Redirect("photoGallery.aspx?dir=" & relativePath & "&imgindex=" & imgindex + 1 & "&ShowImg=True")
            'End If


            'Next


            Panel2.Visible = True
            Panel1.Visible = False
        Else
            Panel1.Visible = True
            Panel2.Visible = False
            displayCurrentLocation()
            displaySubDirectories()
            displayPictures()
        End If


    End Sub
End Class
