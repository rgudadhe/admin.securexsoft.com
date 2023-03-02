
Partial Class MenuPage
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

       
            'If IsPostBack Then
            '    Dim i As Integer
            '    LblAL.Text = i + 1
            '    Response.Write(i)
            'End If
            'Session.Abandon()

            LblDate.Text = Now
            LblAL.Text = "Welcome " & Session("UserName")

            If Not IsPostBack Then
                Dim ConString As String
                Dim SQLString As String
                Dim ALevel As Long
                Dim strCategory As String
                If Session("adminLevel").ToString = "" Then
                    ALevel = 0
                Else
                    ALevel = CLng(Session("adminLevel"))
                End If

                ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim oConn As New Data.SqlClient.SqlConnection
                oConn.ConnectionString = ConString
                oConn.Open()
                Dim LvlDscr As String
                Dim strCont As String
                Dim RecADd As Integer
                Dim TDMIncr As Integer = 6
                Dim TDSIncr As Integer = 6
                RecADd = 0

                strCont = ""
                Dim i As Integer
                Dim j As Integer
                Dim Incr As Integer = 6
                i = 0
                LvlDscr = ""
                Dim IncrImg As Integer = 0
                Dim ItemImage As String
                Dim ItemImageDD As String

                '
                SQLString = "SELECT N.Details as Category, AL.LevelNo, LL.Link_Caption, LL.Link_Path, AL.LevelName, isnull(AL.Description,'') as Description FROM tblAdminLevels AL INNER JOIN tblNavBar N ON N.NavBarID = AL.NavBarID INNER JOIN tblAdminLevelLinks LL ON AL.LevelNo = LL.LevelNo "
                SQLString = SQLString & IIf(Session("adminlevel").ToString = 2147483647, "", "INNER JOIN tblUsersLinks AS UL ON AL.LevelNo = UL.LevelNo ")
                'SQLString = SQLString & "where AL.IsDeleted=0 " & IIf(Session("adminlevel") = 2147483647, "", " and N.SAAccess = 'False' and UL.UserID='" & Session("UserID") & "' and (SELECT [dbo].[chkLevel] (UL.LinkNo,LL.LinkID))=1") & _
                SQLString = SQLString & "where AL.IsDeleted=0 " & IIf(Session("adminlevel") = 2147483647, "", "and UL.UserID='" & Session("UserID") & "' and (SELECT [dbo].[chkLevel] (UL.LinkNo,LL.LinkID))=1") & _
                "order by N.sequence, AL.LevelNo,LL.Link_Caption"
                'Exit Sub

                'Response.Write(SQLString)
                'Response.End()
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
                If oRec.HasRows Then
                    'Dim Accor As New AjaxControlToolkit.Accordion
                    Dim Accor1 As New AjaxControlToolkit.Accordion
                    Dim AccordionPane2 As AjaxControlToolkit.AccordionPane
                    Dim AccordionPane3 As AjaxControlToolkit.AccordionPane
                    'Accor.ID = "MyAccordion"
                    'Accor.Width = "178"
                    ''Accor.FadeTransitions = "true"
                    'Accor.FramesPerSecond = "50"
                    'Accor.TransitionDuration = "150"
                    ''Accor.AutoSize = "None"
                    'Accor.RequireOpenedPane = False
                    'Accor.SuppressHeaderPostbacks = "False"
                    'Accor.SelectedIndex = -1
                    'Accor1 = New AjaxControlToolkit.Accordion
                    'AccordionPane3 = New AjaxControlToolkit.AccordionPane
                    ''Accor1.SelectedIndex = -1
                    'Accor1.ID = "SubAccordion" & j
                    'Accor1.Width = "178"
                    ''Accor1.FadeTransitions = "true"
                    'Accor1.FramesPerSecond = "50"
                    'Accor1.TransitionDuration = "150"
                    ''Accor1.ContentCssClass = "Style17"
                    ''Accor1.EnableViewState = True

                    ''Accor1.
                    ''Accor.AutoSize = "None"
                    'Accor1.RequireOpenedPane = False
                    'Accor1.SuppressHeaderPostbacks = "False"
                    'strCategory = "My Home"
                    'AccordionPane3 = New AjaxControlToolkit.AccordionPane
                    ''AccordionPane3.
                    'Dim lblDef As New LiteralControl
                    'lblDef.Text = "<table width='178'><tr ><td style='text-align:middle; background-image:url(images/NavBar_SecureXSoft.jpg); height:33px;width:173;  ' ><p style='text-align:left'><span style='color: #ffffff' class='style17'>&nbsp;&nbsp;<b>" & strCategory & " </b></span> </p>                           </td>                        </tr>                    </table>"
                    ''lbl.Text = "<table width='178'><tr><td style='text-align:middle; background-image:url(images/navigationtabN.jpg); height:33px;width:173;'><p style='text-align:left'><span style='FONT-SIZE: 8pt; font-family:'Arial'; left:20; color:White; cursor:hand; '><b>" & strCategory & "</b></p></td></tr></table>"
                    'AccordionPane3.HeaderContainer.Controls.Add(lblDef)
                    'Accor.Panes.Add(AccordionPane3)
                    Do While oRec.Read

                        'Response.Write(oRec("LevelNo") & " # " & oRec("Description") & "#")
                        If (ALevel And oRec("LevelNo")) = oRec("LevelNo") Then
                            TDSIncr = TDSIncr + 1
                            ' Response.Write(oRec("LevelNo") & " # " & oRec("Link_Caption").ToString)

                            If LvlDscr <> oRec("LevelName").ToString Then
                                TDMIncr = TDMIncr + 1
                                'Response.Write(oRec("category").ToString & "#" & oRec("description").ToString & vbNewLine)
                                If IncrImg = 0 Then
                                    ItemImage = "adminaccesstab.jpg"
                                    ItemImageDD = "adminaccessdropdown.jpg"
                                ElseIf IncrImg = 1 Then
                                    ItemImage = "demographicstab.jpg"
                                    ItemImageDD = "demographicsdropdown.jpg"
                                ElseIf IncrImg = 2 Then
                                    ItemImage = "approvedreportstab.jpg"
                                    ItemImageDD = "approvedreportsdropdown.jpg"
                                ElseIf IncrImg = 3 Then
                                    ItemImage = "settingstab.jpg"
                                    ItemImageDD = "settingsdropdown.jpg"
                                ElseIf IncrImg = 4 Then
                                    ItemImage = "voicefilestab.jpg"
                                    ItemImageDD = "voicefilesdropdown.jpg"
                                End If
                                If IncrImg = 4 Then
                                    IncrImg = 0
                                Else
                                    IncrImg = IncrImg + 1
                                End If

                                If i <> 0 Then

                                    strCont = strCont & " </table>"
                                    Dim lbl1 As New LiteralControl
                                    lbl1.Text = strCont
                                    AccordionPane2.ContentContainer.Controls.Add(lbl1)
                                    strCont = ""
                                    Accor1.Panes.Add(AccordionPane2)
                                    Accor1.SelectedIndex = -1
                                    AccordionPane3.ContentContainer.Controls.Add(Accor1)
                                    RecADd = RecADd + 1
                                    'If RecADd = 6 Then
                                    '    Exit Do
                                    'End If
                                End If

                                LvlDscr = oRec("LevelName").ToString
                                AccordionPane2 = New AjaxControlToolkit.AccordionPane
                                Dim lbl As New LiteralControl
                                lbl.Text = "<table width='178'><tr onclick='ShowHide1(" & TDMIncr & ")'><td title='" & oRec("Description").ToString & "'  style='text-align:middle; background-image:url(images/" & ItemImage & "); height:20px;width:173;background-repeat:round;;  ' ><span style='color: #000000' class='style17' id='tr" & TDMIncr & "'>" & LvlDscr.Trim & " </span>                            </td>                        </tr>                    </table>"
                                AccordionPane2.HeaderContainer.Controls.Add(lbl)
                                strCont = "<table width='173' border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse; border-color:#111111' >"
                                '  = "<table width='178'><tr><td style='text-align:middle; background-image:url(secureweb/images//transcriptiontab.jpg); height:20px;width:173;  ' >                           <span class='style17'><img alt='' style='display:none;' id='Img1'   src='rot.gif'> <b>Transcription Status</b></span>                            </td>                        </tr>                    </table>"
                                'Accor.Panes.Add(AccordionPane2)
                            End If
                            If strCategory <> oRec("category").ToString Then
                                'strCategory = oRec("category").ToString
                                If j <> 0 Then
                                    Accor.Panes.Add(AccordionPane3)
                                End If
                                j = j + 1

                                Accor1 = New AjaxControlToolkit.Accordion
                                AccordionPane3 = New AjaxControlToolkit.AccordionPane
                                'Accor1.SelectedIndex = -1
                                Accor1.ID = "SubAccordion" & j
                                Accor1.Width = "178"
                                'Accor1.FadeTransitions = "true"
                                Accor1.FramesPerSecond = "50"
                                Accor1.TransitionDuration = "150"
                                'Accor1.ContentCssClass = "Style17"
                                'Accor1.EnableViewState = True

                                'Accor1.
                                'Accor.AutoSize = "None"
                                Accor1.RequireOpenedPane = False
                                Accor1.SuppressHeaderPostbacks = "False"
                                strCategory = oRec("category").ToString
                                AccordionPane3 = New AjaxControlToolkit.AccordionPane
                                'AccordionPane3.
                                Dim lbl As New LiteralControl
                                lbl.Text = "<table width='178'><tr ><td style='text-align:middle; background-image:url(images/NavBar_SecureXSoft.jpg); height:33px;width:173;background-repeat:round;' ><p style='text-align:left'><span style='color: #ffffff' class='style17'>&nbsp;&nbsp;<b>" & strCategory & " </b></span> </p>                           </td>                        </tr>                    </table>"
                                'lbl.Text = "<table width='178'><tr><td style='text-align:middle; background-image:url(images/navigationtabN.jpg); height:33px;width:173;'><p style='text-align:left'><span style='FONT-SIZE: 8pt; font-family:'Arial'; left:20; color:White; cursor:hand; '><b>" & strCategory & "</b></p></td></tr></table>"
                                AccordionPane3.HeaderContainer.Controls.Add(lbl)

                            End If
                            strCont = strCont & " <tr onclick='ShowHideS(" & TDSIncr & ")'><td style='cursor:hand; text-align:middle; width:173; background-image:url(images/" & ItemImageDD & "); height:20px;background-repeat: no-repeat;   background-position: center;cursor: pointer; '   onclick='parent.frames[""" & "right" & """].location=""" & oRec("Link_Path").ToString & """;' ><p style='text-align:left'  class='style17'><span style='color: #000000'  id='trS" & TDSIncr & "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & oRec("Link_Caption").ToString.Trim & " </span> </p></td></tr>"
                            i = i + 1
                            'If i > 0 Then
                            '    strCont = strCont & " </table>"
                            '    Dim lbl2 As New LiteralControl
                            '    lbl2.Text = strCont
                            '    AccordionPane2.ContentContainer.Controls.Add(lbl2)
                            '    strCont = ""
                            '    Accor.Panes.Add(AccordionPane2)
                            '    Cell1.Controls.Add(Accor)
                            'End If
                        End If
                    Loop
                    If j <> 0 Then
                        strCont = strCont & " </table>"
                        Dim lbl1 As New LiteralControl
                        lbl1.Text = strCont
                        AccordionPane2.ContentContainer.Controls.Add(lbl1)
                        strCont = ""
                        Accor1.Panes.Add(AccordionPane2)
                        Accor1.SelectedIndex = -1
                        AccordionPane3.ContentContainer.Controls.Add(Accor1)
                        Accor.Panes.Add(AccordionPane3)
                    End If
                    ' Cell1.Controls.Add(Accor)

                End If

                oRec.Close()
                oConn.Close()
                hTDSIncr.Value = TDSIncr
                hTDMIncr.Value = TDMIncr
            End If
        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Sub
End Class
