
Partial Class TempPhyAssgn_TempPhyAssgn
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Dim strConn As String
        Dim strDeptValue As ArrayList = New ArrayList
        Dim strDescValue As ArrayList = New ArrayList

        'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

    End Sub
    Protected Sub displayProdLevelSearch(ByVal prdstate)
        If prdstate = 0 Then
            Response.Write("<TD VALIGN=TOP>" & vbCrLf)
            'Response.Write(prdstate)
            Response.Write("<TABLE BORDER=1 cellpadding=0 cellspacing=0 RULES=GROUPS><THEAD><TR><TH class=tableheadbox COLSPAN=2> User Search</TH></TR></THEAD><TR><TD >First Name</TD><TD><INPUT TYPE=TEXT NAME=FirstName></TD></TR><TR><TD class=tabletext>Last Name</TD><TD><INPUT TYPE=TEXT NAME=PrLastName></TD></TR><TR>	<TD class=tabletext>Username</TD>	<TD><INPUT TYPE=TEXT NAME=PrUserName></TD></TR></TABLE>")
            Response.Write("<INPUT TYPE=""hidden"" NAME=""prdstate"" VALUE=""1"">" & vbCrLf)
            Response.Write("<INPUT TYPE=""submit"" NAME=""prdstate1"" VALUE=""Submit"">" & vbCrLf)
            Response.Write("</TD>" & vbCrLf)
        End If
        If prdstate = 1 And Request("prdstate1") <> "" Then
            Response.Write("<TD VALIGN=TOP>" & vbCrLf)
            Response.Write(prdstate)
            Response.Write("<INPUT TYPE=""hidden"" NAME=""prdstate"" VALUE=""2"">" & vbCrLf)
            Response.Write("<INPUT TYPE=""submit"" NAME=""prdstate2"" VALUE=""Submit"">" & vbCrLf)
            Response.Write("</TD>" & vbCrLf)

        End If

        If prdstate = 2 And Request("prdstate2") <> "" Then
            Response.Write("<TD VALIGN=TOP>" & vbCrLf)
            Response.Write("Select Production Level" & vbCrLf)
            Response.Write(prdstate)
            Response.Write("</TD>" & vbCrLf)
        End If
    End Sub
    Protected Sub displayPhySearch(ByVal phystate)
        If phystate = 0 Then
            Response.Write("<TD VALIGN=TOP>" & vbCrLf)
            Response.Write("<TABLE BORDER=1 cellpadding=0 cellspacing=0 RULES=GROUPS><THEAD><TR><TH class=tableheadbox COLSPAN=2> Physician Search</TH></TR></THEAD><TR><TD>First Name</TD><TD><INPUT TYPE=TEXT NAME=FirstName></TD></TR><TR><TD class=tabletext>Last Name</TD><TD><INPUT TYPE=TEXT NAME=PrLastName></TD></TR><TR>	<TD class=tabletext>Username</TD>	<TD><INPUT TYPE=TEXT NAME=PrUserName></TD></TR></TABLE>")
            'Response.Write(phystate)
            Response.Write("<INPUT TYPE=""hidden"" NAME=""phystate"" VALUE=""1"">" & vbCrLf)
            Response.Write("<INPUT TYPE=""submit"" NAME=""phystate1"" VALUE=""Submit"">" & vbCrLf)
            Response.Write("</TD>" & vbCrLf)
        End If
        If phystate = 1 Then
            If Request("phystate1") <> "" Then
                Response.Write("<TD VALIGN=TOP>" & vbCrLf)
                Response.Write("<INPUT TYPE=""hidden"" NAME=""phystate"" VALUE=""2"">" & vbCrLf)
                Response.Write(phystate)
                Response.Write("<INPUT TYPE=""submit"" NAME=""phystate2"" VALUE=""Submit"">" & vbCrLf)
                Response.Write("</TD>" & vbCrLf)
            Else
                Response.Write("<TD VALIGN=TOP>" & vbCrLf)
                Response.Write("<TABLE BORDER=1 cellpadding=0 cellspacing=0 RULES=GROUPS><THEAD><TR><TH class=tableheadbox COLSPAN=2> Physician Search</TH></TR></THEAD><TR><TD>First Name</TD><TD><INPUT TYPE=TEXT NAME=FirstName></TD></TR><TR><TD class=tabletext>Last Name</TD><TD><INPUT TYPE=TEXT NAME=PrLastName></TD></TR><TR>	<TD class=tabletext>Username</TD>	<TD><INPUT TYPE=TEXT NAME=PrUserName></TD></TR></TABLE>")
                'Response.Write(phystate)
                Response.Write("<INPUT TYPE=""hidden"" NAME=""phystate"" VALUE=""1"">" & vbCrLf)
                Response.Write("<INPUT TYPE=""submit"" NAME=""phystate1"" VALUE=""Submit"">" & vbCrLf)
                Response.Write("</TD>" & vbCrLf)
            End If
        End If
        If phystate = 2 And Request("phystate2") <> "" Then
            Response.Write("<TD VALIGN=TOP>" & vbCrLf)
            Response.Write("Select Physician" & vbCrLf)
            Response.Write(phystate)
            Response.Write("</TD>" & vbCrLf)
        End If
    End Sub
   
End Class
