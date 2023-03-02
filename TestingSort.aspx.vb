
Partial Class TestingSort
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim mailer As Object
        mailer = CreateObject("SoftArtisans.SMTPMail")
        'mailer.RemoteHost = "smtp.edictateindia.com"
        'mailer.UserName = "edictatemail"
        'mailer.Password = "ed1ctatema!l"
        mailer.RemoteHost = "secure.emailsrvr.com"
        mailer.UserName = "alert@edictate.com"
        mailer.Password = "Welcome@medofficepro2011"
        'Dim smtp1 As New SASMTPLib.CoSMTPMail
        'smtp1.Port = ""
        mailer.port = 465
        mailer.FromAddress = "techsupport@edictate.com"
        mailer.AddRecipient("", "vraut@edictate.com")
        mailer.HtmlText = "<b><font color=#646D7E>Dear Colleagues</font></b>"
        mailer.Subject = "Testing"
        Response.Write(mailer.SendMail())
        mailer = Nothing
        'Else
        '    mailer.HtmlText = "<b><font color=#646D7E>Dear Colleagues</font></b>,<br><br></font><table border=0 cellspacing=1 bordercolor=#111111 width=100%><tr>    <td width=50%><font color=#000080 face=" & "Trebuchet MS" & "size=2><b>Account Name : - </font><font face=Trebuchet MS size=2>" & ClientName & "</font></b></td>    <td width=50%>&nbsp;</td>  </tr>  <tr>    <td width=50%><font color=#000080 face=" & "Trebuchet MS" & "size=2><b>Received Date: -</b></font><b><font face=Trebuchet MS size=2>" & Now() & "</font></b></td>    <td width=50%>&nbsp;</td>  </tr>  <tr>    <td width=50%><font color=#000080 face=" & "Trebuchet MS" & "size=2><b>Demographic Filename: -</b></font><b><font face=Trebuchet MS size=2>" & DEMOFILENAME & "</font></b></td>    <td width=50%>&nbsp;</td>  </tr>  <tr>    <td width=50%><font color=#000080 face=" & "Trebuchet MS" & "size=2><b>" & _
        '"Folder Name: -</b></font><b><font face=Trebuchet MS size=2>" & FolderName & "</font></b></td>    <td width=50%>&nbsp;</td>  </tr> </table><p><font color=#000080 face=" & "Trebuchet MS" & "size=2><b>Thanks,</b></font></p><p><font color=#000080 face=" & "Trebuchet MS" & "size=2><BR><BR>e-Dictate Support Department</b></font></p>"
        'End If

        'If Not Page.IsPostBack Then
        '    Dim clsNews As New ETS.BL.News
        '    Dim DS As New Data.DataSet
        '    DS = clsNews.getNewsList


        '    gv1.DataSource = DS

        '    gv1.DataBind()
        '    clsNews = Nothing
        '    If gv1.Rows.Count > 0 Then
        '        gv1.UseAccessibleHeader = True
        '        gv1.HeaderRow.TableSection = TableRowSection.TableHeader
        '        gv1.FooterRow.TableSection = TableRowSection.TableFooter
        '    End If
        'End If
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Confirms that an HtmlForm control is rendered for the specified ASP.NET
        '     server control at run time. 
    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Clear()
        Response.AddHeader("content-disposition", "attachment;filename=FileName.xls")
        Response.Charset = ""

        Response.ContentType = "application/vnd.xls"
        Response.AddHeader("content-disposition", "attachment;filename=" & "Export Data")
        Dim StringWriter As System.IO.StringWriter = New System.IO.StringWriter()
        Dim HtmlTextWriter As New HtmlTextWriter(StringWriter)
        gv1.RenderControl(HtmlTextWriter)

        Response.Write(StringWriter.ToString())
        Response.[End]()
    End Sub
End Class
