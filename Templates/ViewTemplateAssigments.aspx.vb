Partial Class Templates_ViewTemplateAssigments
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim varOrgPhyID As String = String.Empty
            Dim hdnTemp As HiddenField
            hdnTemp = DirectCast(PreviousPage.FindControl("hdnPhyID"), HiddenField)

            If Not hdnTemp Is Nothing Then
                varOrgPhyID = hdnTemp.Value.ToString
            End If

            hdnPhyIDs.Value = varOrgPhyID
            ShowDataNew()
        End If
    End Sub
    Protected Sub ShowDataNew()
        Dim DTTemp As New Data.DataTable
        DTTemp = CreateTempDataTable()
        Dim objPds As New PagedDataSource
        objPds.DataSource = DTTemp.DefaultView
        objPds.AllowPaging = True
        objPds.PageSize = 1

        objPds.CurrentPageIndex = CurrentPage

        lblCurrentPage.Text = "Page: " + (CurrentPage + 1).ToString() + " of " + objPds.PageCount.ToString()

        'Disable Prev or Next buttons if necessary
        cmdPrev.Enabled = Not objPds.IsFirstPage
        cmdNext.Enabled = Not objPds.IsLastPage


        rptParent.DataSource = objPds
        rptParent.DataBind()
    End Sub
    Protected Function CreateTempDataTable() As Data.DataTable
        Dim DTRet As New Data.DataTable

        Dim varOrgPhyID As String = String.Empty

        varOrgPhyID = hdnPhyIDs.Value.ToString
        
        If Not String.IsNullOrEmpty(varOrgPhyID.ToString) Then

            DTRet.Columns.Add(New Data.DataColumn("PhyID"))
            DTRet.Columns.Add(New Data.DataColumn("PhyName"))

            Dim varStr() As String
            varStr = Split(varOrgPhyID.ToString, "|")

            For i As Integer = 0 To UBound(varStr)
                Dim DR As Data.DataRow = DTRet.NewRow

                Dim clsPhy As ETS.BL.Physicians
                Try
                    clsPhy = New ETS.BL.Physicians
                    clsPhy.PhysicianID = varStr(i).ToString
                    clsPhy.getPhysicianDetails()
                    'varTempMTbl.Append("<tr class=""alt1""><td style=""border:0;font-weight:bold; font-size:10pt"">Template Assignments for " & clsPhy.FirstName.ToString & " " & clsPhy.LastName.ToString & "</td></tr>")
                    DR("PhyID") = varStr(i).ToString
                    DR("PhyName") = "Template Assignment for " & clsPhy.FirstName.ToString & " " & clsPhy.LastName.ToString

                    DTRet.Rows.Add(DR)
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsPhy = Nothing
                End Try
            Next
        End If


        Return DTRet


    End Function
    'Protected Sub ShowData()
    '    Dim varOrgPhyID As String = String.Empty
    '    Dim hdnTemp As HiddenField
    '    hdnTemp = DirectCast(PreviousPage.FindControl("hdnPhyID"), HiddenField)

    '    If Not hdnTemp Is Nothing Then
    '        varOrgPhyID = hdnTemp.Value.ToString
    '    End If

    '    If Not String.IsNullOrEmpty(varOrgPhyID.ToString) Then
    '        Dim varTempMTbl As New StringBuilder
    '        varTempMTbl.Append("<table width=""70%"" style=""text-align:left;font-family:Trebuchet MS; font-size:8pt"">")
    '        Dim varStr() As String
    '        varStr = Split(varOrgPhyID.ToString, "|")
    '        For i As Integer = 0 To UBound(varStr)
    '            Dim clsPhy As ETS.BL.Physicians
    '            Try
    '                clsPhy = New ETS.BL.Physicians
    '                clsPhy.PhysicianID = varStr(i).ToString
    '                clsPhy.getPhysicianDetails()
    '                varTempMTbl.Append("<tr class=""alt1""><td style=""border:0;font-weight:bold; font-size:10pt"">Template Assignments for " & clsPhy.FirstName.ToString & " " & clsPhy.LastName.ToString & "</td></tr>")
    '            Catch ex As Exception
    '                Response.Write(ex.Message)
    '            Finally
    '                clsPhy = Nothing
    '            End Try

    '            Dim clsPT As ETS.BL.PhysiciansTempaltes
    '            Dim DSPT As New Data.DataSet
    '            Dim oRec As Data.DataTableReader
    '            Try
    '                clsPT = New ETS.BL.PhysiciansTempaltes
    '                DSPT = clsPT.getPhysiciansTemplatesList(varStr(i).ToString)

    '                If DSPT.Tables.Count > 0 Then
    '                    If DSPT.Tables(0).Rows.Count > 0 Then
    '                        oRec = DSPT.Tables(0).CreateDataReader
    '                        If oRec.HasRows Then
    '                            Dim varTempTbl As New StringBuilder
    '                            varTempTbl.Append("<table border=""0"" width=""100%"" style=""text-align:left;font-family:Trebuchet MS; font-size:8pt""><tr><td class=""SMSelected"">Template Name</td><td class=""SMSelected"">TAT</td><td class=""SMSelected"">STAT</td><td class=""SMSelected"">Sequence</td></tr>")
    '                            While oRec.Read
    '                                varTempTbl.Append("<tr><td>" & oRec("TemplateName").ToString & "</td><td>" & oRec("TAT").ToString & "</td><td>" & oRec("STAT").ToString & "</td><td>" & oRec("WorkType").ToString & "</td></tr>")
    '                            End While

    '                            If Not String.IsNullOrEmpty(varTempTbl.ToString) Then
    '                                varTempTbl.Append("</table><BR>")
    '                                varTempMTbl.Append("<tr><td style=""border:0;"">" & varTempTbl.ToString & "</td></tr>")
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            Catch ex As Exception
    '                Response.Write(ex.Message)
    '            Finally
    '                clsPT = Nothing
    '                DSPT = Nothing
    '                oRec = Nothing
    '            End Try
    '        Next

    '        If Not String.IsNullOrEmpty(varTempMTbl.ToString) Then
    '            varTempMTbl.Append("</table>")
    '            lblDisplay.Text = varTempMTbl.ToString
    '        End If
    '    End If
    'End Sub

    Protected Sub rptParent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptParent.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rptTemp As Repeater
            rptTemp = DirectCast(e.Item.FindControl("rptChild"), Repeater)

            Dim hdnTempPID As HiddenField
            hdnTempPID = DirectCast(e.Item.FindControl("RhdnPhyID"), HiddenField)

            If Not rptTemp Is Nothing And Not hdnTempPID Is Nothing Then
                Dim clsPT As ETS.BL.PhysiciansTempaltes
                Dim DSPT As New Data.DataSet
                Try
                    clsPT = New ETS.BL.PhysiciansTempaltes
                    DSPT = clsPT.getPhysiciansTemplatesList(hdnTempPID.Value.ToString)

                    If DSPT.Tables.Count > 0 Then
                        If DSPT.Tables(0).Rows.Count > 0 Then
                            rptTemp.DataSource = DSPT.Tables(0)
                            rptTemp.DataBind()
                        End If
                    End If
                    For Each DR As System.Data.DataRow In DSPT.Tables(0).Rows
                        For Each ctl As RepeaterItem In rptTemp.Items
                            Dim ddl As DropDownList
                            Dim hdn As HiddenField = ctl.FindControl("hdnTemplateID")
                            If hdn.Value = DR("TemplateID").ToString Then
                                ddl = New DropDownList
                                ddl = hdn.Parent.FindControl("DDLTAT")
                                ddl.Items.FindByValue(DR("TAT").ToString).Selected = True
                                If Not IsDBNull(DR("TZDifference")) Then
                                    ddl = New DropDownList
                                    ddl = hdn.Parent.FindControl("DDLTZ")
                                    ddl.Items.FindByValue(DR("TZDifference").ToString).Selected = True
                                End If
                            End If
                        Next
                    Next
                    DSPT.Dispose()
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsPT = Nothing
                    DSPT = Nothing
                End Try
            End If
        End If
    End Sub
    Public Property CurrentPage() As Integer
        Get
            ' look for current page in ViewState
            Dim o As Object = Me.ViewState("_CurrentPage")
            If o Is Nothing Then
                Return 0
            Else
                ' default page index of 0
                Return CInt(o)
            End If
        End Get

        Set(ByVal value As Integer)
            Me.ViewState("_CurrentPage") = value
        End Set
    End Property

    Protected Sub cmdNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNext.Click
        ' Set viewstate variable to the next page
        CurrentPage += 1

        'Reload control
        ShowDataNew()
    End Sub


    
    Protected Sub cmdPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
        'Set viewstate variable to the previous page
        CurrentPage -= 1

        ' Reload control
        ShowDataNew()

    End Sub
    Protected Sub btnSave_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btnSave As Button
        btnSave = CType(sender, Button)

        If Not btnSave Is Nothing Then
            Dim varPhyID As String = String.Empty
            varPhyID = btnSave.CommandArgument

            Dim rptTempChild As Repeater
            rptTempChild = btnSave.Parent.FindControl("rptChild")
            If Not rptTempChild Is Nothing Then
                'Response.Write(rptTempChild.Items.Count & "<BR>" & varPhyID)

                For Each rItem As RepeaterItem In rptTempChild.Items
                    Dim ddl As DropDownList
                    Dim txt As TextBox
                    Dim strTempID As String
                    Dim intTime As Integer
                    Dim intTZDeference As Integer
                    Dim intTAT As Integer
                    Dim intSTAT As Integer

                    Dim intWT As Integer
                    Dim hdn As HiddenField = rItem.FindControl("hdnTemplateID")
                    strTempID = hdn.Value
                    ddl = hdn.Parent.FindControl("DDLTAT")
                    If Not String.IsNullOrEmpty(ddl.SelectedValue) Then
                        If IsNumeric(ddl.SelectedValue) Then
                            intTAT = CInt(ddl.SelectedValue)
                        End If
                    End If
                    txt = hdn.Parent.FindControl("txtTime")
                    If Not String.IsNullOrEmpty(txt.Text) Then
                        If IsNumeric(txt.Text) Then
                            intTime = CInt(txt.Text)
                        End If
                    End If
                    txt = hdn.Parent.FindControl("txtSTAT")
                    If Not String.IsNullOrEmpty(txt.Text) Then
                        If IsNumeric(txt.Text) Then
                            intSTAT = CInt(txt.Text)
                        End If
                    End If
                    ddl = hdn.Parent.FindControl("DDLTZ")
                    If Not String.IsNullOrEmpty(ddl.SelectedValue) Then
                        If IsNumeric(ddl.SelectedValue) Then
                            intTZDeference = CInt(ddl.SelectedValue)
                        End If
                    End If
                    txt = hdn.Parent.FindControl("txtSeq")
                    If Not String.IsNullOrEmpty(txt.Text) Then
                        If IsNumeric(txt.Text) Then
                            intWT = CInt(txt.Text)
                        End If
                    End If
                    If Len(strTempID) = 36 Then
                        Dim clsPT As New ETS.BL.PhysiciansTempaltes
                        With clsPT
                            .PhysicianID = varPhyID.ToString
                            .TemplateID = strTempID
                            .TAT = intTAT
                            .Time = intTime
                            .STAT = intSTAT
                            .TZDifference = intTZDeference
                            .WorkType = intWT
                            .UpdatePhysicianTemplates()
                        End With
                        clsPT = Nothing
                    End If
                Next
                Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Changes have been saved Successfully');</SCRIPT>")
                ShowDataNew()
            End If
        End If
    End Sub
    Protected Sub btnRemove_click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btnRemove As Button
        btnRemove = CType(sender, Button)

        If Not btnRemove Is Nothing Then
            Dim varTemplateID As String = String.Empty
            varTemplateID = btnRemove.CommandArgument
            Dim TemphdnPID As HiddenField
            TemphdnPID = DirectCast(btnRemove.Parent.Parent.Parent.FindControl("RhdnPhyID"), HiddenField)

            If Not TemphdnPID Is Nothing Then
                Dim clsPT As ETS.BL.PhysiciansTempaltes
                Try
                    clsPT = New ETS.BL.PhysiciansTempaltes
                    With clsPT
                        .PhysicianID = TemphdnPID.Value.ToString
                        .TemplateID = varTemplateID.ToString
                        If .DeletePhysicianTemplate() > 0 Then
                            Response.Write("<SCRIPT LANGUAGE=""JavaScript"">alert('Template removed Successfully');</SCRIPT>")
                            ShowDataNew()
                        End If
                    End With
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsPT = Nothing
                End Try
            End If
        End If
    End Sub
End Class
