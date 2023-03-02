
Partial Class Force_Routing_ForceRoutingReportResult
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Request.QueryString("Opr") <> "" And Request.QueryString("ID") <> "" Then
                Dim varStrOpr As String = String.Empty
                Dim varStrID As String = String.Empty
                Dim varStrULevel As String = String.Empty

                varStrOpr = Request.QueryString("Opr")
                varStrID = Request.QueryString("ID")
                varStrULevel = Request.QueryString("UserLevel")

                Dim varStrQuery As String = String.Empty

                If Trim(UCase(varStrOpr)) = Trim(UCase("Account")) Then
                    varStrQuery = "DELETE FROM tblForceRouting4Accounts WHERE AccountID='" & varStrID & "'"
                ElseIf Trim(UCase(varStrOpr)) = Trim(UCase("Dictator")) Then
                    varStrQuery = "DELETE FROM tblForceRouting4Physicians WHERE PhysicianID='" & varStrID & "'"
                ElseIf Trim(UCase(varStrOpr)) = Trim(UCase("Template")) Then
                    varStrQuery = "DELETE FROM tblForceRouting4Templates WHERE TemplateID='" & varStrID & "'"
                ElseIf Trim(UCase(varStrOpr)) = Trim(UCase("User")) Then
                    If Not String.IsNullOrEmpty(varStrULevel) Then
                        varStrQuery = "DELETE FROM tblForceRouting4Users WHERE UserID='" & varStrID & "' AND UserLevel=" & varStrULevel & ""
                    End If
                End If
                Response.Write(varStrQuery)
                If Not String.IsNullOrEmpty(varStrQuery) Then
                    Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                    Dim oConn As New Data.SqlClient.SqlConnection
                    oConn.ConnectionString = ConString
                    Try
                        oConn.Open()
                        Dim objCmd As New Data.SqlClient.SqlCommand(varStrQuery, oConn)
                        If objCmd.ExecuteNonQuery() > 0 Then
                            DBind(varStrOpr, varStrULevel)
                        End If
                    Catch ex As Exception
                        Response.Write(ex.Message)
                    Finally
                        If oConn.State <> Data.ConnectionState.Closed Then
                            oConn.Close()
                            oConn = Nothing
                        End If
                    End Try
                End If
            End If

            If Not Page.IsPostBack Then
                DBind(Request.Form("ddlRouting"), Request.Form("ULevel"))
            End If
        Catch ex As Exception
            Response.Write(ex.Message & "hh")
        End Try
    End Sub
    Private Sub DBind(ByVal Routing As String, ByVal ULevel As String)
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Dim varflag As Boolean = True
        If Routing <> "" Then
            tblResult.Rows.Clear()
            lblMsg.Text = ""
            Dim varUserLevelNo As String = String.Empty
            varUserLevelNo = ULevel
            Dim varStrQueryForceRouting As String = String.Empty

            Try
                oConn.Open()
                Dim varArrCellName As New ArrayList
                Dim oCommand As New Data.SqlClient.SqlCommand("SELECT LevelName,LevelNo FROM DBO.tblProductionLevels WHERE ForcedRouting =1", oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader

                If oRec.HasRows Then
                    While oRec.Read
                        If Not oRec.IsDBNull(oRec.GetOrdinal("LevelNo")) Then
                            If String.IsNullOrEmpty(varStrQueryForceRouting) Then
                                varStrQueryForceRouting = ",(SELECT [ETS].[dbo].[chkLevel] (F.Levels," & oRec("LevelNo") & ")) as " & oRec("LevelName") & ""
                            Else
                                varStrQueryForceRouting = varStrQueryForceRouting & ",(SELECT [ETS].[dbo].[chkLevel] (F.Levels," & oRec("LevelNo") & ")) as " & oRec("LevelName") & ""
                            End If
                            varArrCellName.Add(oRec("LevelName"))
                        End If
                    End While
                End If

                oRec.Close()
                oRec = Nothing
                oCommand = Nothing

                Response.Write(varStrQueryForceRouting)
                Dim vartbl As String = String.Empty
                Dim varStrQuery As String = String.Empty

                If Trim(UCase(Routing)) = Trim(UCase("Account")) Then
                    varStrQuery = "SELECT A.AccountID AS ID ,A.AccountName AS Name " & varStrQueryForceRouting & _
                                  " FROM tblAccounts AS A INNER JOIN" & _
                                  " tblForceRouting4Accounts AS F ON A.AccountID = F.AccountID INNER JOIN" & _
                                  " tblProductionLevels AS PL ON F.Levels = PL.LevelNo " & _
                                  " WHERE F.Levels > 0 and A.contractorID='" & Session("ContractorID") & "' "
                    vartbl = "Account"
                ElseIf Trim(UCase(Routing)) = Trim(UCase("Dictator")) Then
                    varStrQuery = "SELECT P.PhysicianID AS ID, P.FirstName + ' ' + P.LastName AS Name " & varStrQueryForceRouting & _
                                  " FROM tblProductionLevels AS PL INNER JOIN " & _
                                  " tblForceRouting4Physicians AS F ON PL.LevelNo = F.Levels INNER JOIN " & _
                                  " tblPhysicians AS P ON F.PhysicianID = P.PhysicianID " & _
                                   "INNER JOIN tblAccounts AS A ON P.AccountID = A.AccountID " & _
                                    " Where F.Levels > 0 and A.ContractorID='" & Session("ContractorID") & "' and (A.IsDeleted=0 or A.IsDeleted is null)"

                    vartbl = "Dictator"
                ElseIf Trim(UCase(Routing)) = Trim(UCase("Template")) Then
                    varStrQuery = "SELECT T.TemplateID AS ID, T.TemplateName AS Name " & varStrQueryForceRouting & _
                                  " FROM tblForceRouting4Templates AS F INNER JOIN " & _
                                  " tblTemplates AS T ON F.TemplateID = T.TemplateID INNER JOIN " & _
                                  " tblProductionLevels AS PL ON F.Levels = PL.LevelNo " & _
                                  " WHERE F.Levels > 0 and T.ContractorID='" & Session("ContractorID") & "' "
                    vartbl = "Template"
                ElseIf Trim(UCase(Routing)) = Trim(UCase("User")) Then
                    If Not String.IsNullOrEmpty(varUserLevelNo) Then
                        varStrQuery = "SELECT U.UserID AS ID , F.UserLevel, PL.LevelName, U.FirstName +' '+ U.LastName + '(' + U.UserName + ')' as Name " & varStrQueryForceRouting & _
                                      " FROM tblForceRouting4Users AS F INNER JOIN " & _
                                      " tblUsers AS U ON F.UserID = U.UserID INNER JOIN " & _
                                      " tblProductionLevels AS PL ON F.UserLevel = PL.LevelNo WHERE F.UserLevel=" & varUserLevelNo & " AND F.Levels > 0 and U.ContractorID='" & Session("ContractorID") & "'"
                    Else
                        'Response.Write("<font face=""Trebuchet MS"" color=""red"" size=""2px"">Please select user level</font>")
                        lblMsg.Text = "Please select user level"
                        lblMsg.ForeColor = Drawing.Color.Red
                        varflag = False
                    End If
                    vartbl = "User"
                End If

                'Response.Write(varStrQuery)
                Response.Clear()
                If Not String.IsNullOrEmpty(varStrQuery) And varflag Then
                    Dim oCommandM As New Data.SqlClient.SqlCommand(varStrQuery, oConn)
                    Dim oRecM As Data.SqlClient.SqlDataReader = oCommandM.ExecuteReader

                    If oRecM.HasRows Then
                        Dim vartblRowM As New TableRow
                        Dim vartblCellnameM As New TableCell
                        vartblCellnameM.Text = "Name"
                        vartblRowM.Cells.Add(vartblCellnameM)
                        vartblCellnameM.HorizontalAlign = HorizontalAlign.Center

                        For i As Integer = 0 To varArrCellName.Count - 1
                            Dim varCell As New TableCell
                            varCell.Text = varArrCellName(i)
                            varCell.HorizontalAlign = HorizontalAlign.Center
                            vartblRowM.Cells.Add(varCell)
                        Next

                        Dim vartblCellRemoveM As New TableCell
                        vartblCellRemoveM.Text = ""
                        vartblRowM.Cells.Add(vartblCellRemoveM)

                        vartblRowM.CssClass = "SMSelected"
                        tblResult.Rows.Add(vartblRowM)


                        While oRecM.Read
                            Dim vartblRow As New TableRow
                            Dim vartblCellname As New TableCell
                            vartblCellname.Text = oRecM("Name")
                            vartblRow.Cells.Add(vartblCellname)

                            For j As Integer = 0 To varArrCellName.Count - 1
                                Dim varCell As New TableCell
                                Dim varValue As Boolean

                                Dim varChkBox As New CheckBox

                                varValue = oRecM(varArrCellName(j))
                                If varValue Then
                                    varChkBox.Checked = True
                                End If
                                varChkBox.Enabled = False
                                varCell.Controls.Add(varChkBox)


                                varCell.HorizontalAlign = HorizontalAlign.Center

                                vartblRow.Cells.Add(varCell)
                            Next

                            Dim vartblCellRemove As New TableCell

                            If Trim(UCase(Routing)) <> Trim(UCase("User")) Then
                                vartblCellRemove.Text = "<a href=ForceRoutingReportResult.aspx?Opr=" & vartbl & "&ID=" & oRecM("ID").ToString & ">Remove</a>"
                            Else
                                vartblCellRemove.Text = "<a href=ForceRoutingReportResult.aspx?Opr=" & vartbl & "&ID=" & oRecM("ID").ToString & "&UserLevel=" & varUserLevelNo & ">Remove</a>"
                            End If


                            'Dim varctrllnkbtn As New LinkButton

                            'varctrllnkbtn.EnableViewState = True
                            'varctrllnkbtn.ID = oRecM("ID").ToString
                            'varctrllnkbtn.Text = "Remove"
                            'varctrllnkbtn.CommandArgument = oRecM("ID").ToString & "#" & vartbl
                            'vartblCellRemove.Controls.Add(varctrllnkbtn)

                            'AddHandler varctrllnkbtn.Click, AddressOf varctrllnkbtn_click

                            vartblRow.Cells.Add(vartblCellRemove)

                            tblResult.Rows.Add(vartblRow)
                        End While
                    Else
                        'Response.Write("<font face=""Trebuchet MS"" color=""red"" size=""2px"">No records founds</font>")
                        lblMsg.Text = "No records founds"
                    End If

                    oRecM.Close()
                    oRecM = Nothing
                    oCommandM = Nothing
                End If
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub
End Class
