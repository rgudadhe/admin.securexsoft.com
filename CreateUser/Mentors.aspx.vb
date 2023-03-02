Imports System.Data
Partial Class CreateUser_Mentors
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = String.Empty
        If Not Page.IsPostBack Then
            FillGrid()
        End If
    End Sub
    Protected Sub FillGrid()
        Dim DSPDC As New DataSet
        Dim clsPDC As New ETS.BL.Mentors
        With clsPDC
            DSPDC = .getMentorsListByWrkGrpID(Session("ContractorID").ToString, Session("WorkGroupID").ToString)
        End With
        clsPDC = Nothing
        Dim identity As DataColumn = New DataColumn("SRNo", GetType(System.Int32))
        identity.AutoIncrement = True
        identity.AutoIncrementSeed = 1
        identity.AutoIncrementStep = 1


        DSPDC.Tables(0).Columns.Add(identity)


        If DSPDC.Tables(0).Rows.Count = 0 Then
            DSPDC.Tables(0).Constraints.Clear()
            For Each DC As Data.DataColumn In DSPDC.Tables(0).Columns
                DC.AllowDBNull = True
            Next
            'Add blank row
            DSPDC.Tables(0).Columns(0).AllowDBNull = True
            DSPDC.Tables(0).Rows.Add(DSPDC.Tables(0).NewRow)

            GridViewMain.DataSource = DSPDC.Tables(0)
            GridViewMain.DataBind()
            GridViewMain.Rows(0).Visible = False
        Else
            GridViewMain.DataSource = DSPDC.Tables(0)
            GridViewMain.DataBind()

            'GridView1.DataSource = DSPDC.Tables(0)
            'GridView1.DataBind()
        End If
        DSPDC.Dispose()

    End Sub

    Protected Sub GridViewMain_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewMain.RowCommand
        If Trim(UCase(e.CommandSource.ToString)) = Trim(UCase("System.Web.UI.WebControls.LinkButton")) Then
            If Trim(UCase(e.CommandName)) = Trim(UCase("AddUsr")) Then
                Try
                    Dim varStrUsrID As String = String.Empty
                    Dim objDDLTemp As DropDownList
                    objDDLTemp = DirectCast(GridViewMain.FooterRow.FindControl("ddlUsrs"), DropDownList)
                    If Not objDDLTemp Is Nothing Then
                        varStrUsrID = objDDLTemp.SelectedValue.ToString

                        If Not String.IsNullOrEmpty(varStrUsrID.ToString) Then
                            Dim clsM As ETS.BL.Mentors
                            Try
                                clsM = New ETS.BL.Mentors
                                clsM.UserID = varStrUsrID.ToString
                                clsM.UpdatedBy = Session("UserID").ToString
                                clsM.UpdatedOn = Now
                                If clsM.InsertMentorDetails() = 1 Then
                                    lblMsg.Text = "User added to mentor list.."
                                    FillGrid()
                                End If
                            Catch ex As Exception
                                Response.Write(ex.Message)
                            Finally
                                clsM = Nothing
                            End Try
                        End If
                    End If


                    
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try
            ElseIf Trim(UCase(e.CommandName)) = Trim(UCase("DeleteM")) Then
                Dim varStrUsrID As String = String.Empty
                Dim objTempButton As LinkButton
                Dim varGridViewRow As GridViewRow
                objTempButton = DirectCast(e.CommandSource, LinkButton)
                varGridViewRow = DirectCast(objTempButton.NamingContainer, GridViewRow)

                varStrUsrID = DirectCast(varGridViewRow.FindControl("UserId"), HiddenField).Value.ToString

                If Not String.IsNullOrEmpty(varStrUsrID.ToString) Then
                    Dim clsM As ETS.BL.Mentors
                    Try
                        clsM = New ETS.BL.Mentors
                        clsM.UserID = varStrUsrID.ToString
                        If clsM.DeleteMentorDetails() = 1 Then
                            lblMsg.Text = "User removed from mentor list.."
                            FillGrid()
                        End If
                    Catch ex As Exception
                        Response.Write(ex.Message)
                    Finally
                        clsM = Nothing
                    End Try
                End If
            End If
        End If
    End Sub

    Protected Sub GridViewMain_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewMain.RowDataBound
        If e.Row.RowType = DataControlRowType.Footer Then
            Dim objDDL As DropDownList
            objDDL = DirectCast(e.Row.FindControl("ddlUsrs"), DropDownList)
            If Not objDDL Is Nothing Then
                Dim clsM As ETS.BL.Mentors
                Dim DS As New DataSet
                Try
                    clsM = New ETS.BL.Mentors
                    DS = clsM.getMentorsLstByWorkgroupIDForSet(Session("ContractorID").ToString, Session("WorkGroupID").ToString)
                    If DS.Tables.Count > 0 Then
                        If DS.Tables(0).Rows.Count > 0 Then
                            objDDL.DataSource = DS.Tables(0)
                            objDDL.DataValueField = "UserId"
                            objDDL.DataTextField = "Mentor"
                            objDDL.DataBind()
                        End If
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsM = Nothing
                    DS = Nothing
                End Try
                objDDL.Items.Insert(0, New ListItem("--- Select ---", String.Empty))
            End If
        End If
    End Sub
End Class
