Imports System.Data.SqlClient
'Imports SASMTPLib
Partial Class Templates_ViewAssignment
    Inherits BasePage
    Protected Sub TemplateStatus(ByVal PhyID As String)

        Dim TemplateID As String
        Dim TemplateName As String
        Dim clsTemp As ETS.BL.PhysiciansTempaltes
        Dim DsTemp As New Data.DataSet
        Dim oRec As Data.DataTableReader

        Try
            clsTemp = New ETS.BL.PhysiciansTempaltes
            DsTemp = clsTemp.getTemplatesByPhyID(PhyID, Session("ContractorID").ToString)
            If DsTemp.Tables.Count > 0 Then
                If DsTemp.Tables(0).Rows.Count > 0 Then
                    oRec = DsTemp.Tables(0).CreateDataReader
                    If oRec.HasRows Then
                        While oRec.Read()
                            TemplateID = oRec("TemplateID").ToString
                            TemplateName = oRec("TemplateName").ToString
                            Dim li As New ListItem
                            li.Text = TemplateName
                            li.Value = TemplateID
                            If IsDBNull(oRec("Physicianid")) Then
                                'res
                                lstAvailTmps.Items.Add(li)
                            Else
                                lstAssignTmps.Items.Add(li)
                            End If
                        End While
                    End If
                    oRec.Close()
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsTemp = Nothing
            DsTemp = Nothing
            oRec = Nothing
        End Try
    End Sub
    Protected Sub BtnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAssign.Click
        ' Dim DT As New Data.DataTable
        'DT.Columns.Add(New Data.DataColumn("TemplateID"))
        Dim varTempStr As String = String.Empty
        For i As Integer = 0 To lstAssignTmps.Items.Count - 1
            'Dim DR As Data.DataRow = DT.NewRow
            'DR("TemplateID") = lstAssignTmps.Items(i).Value
            'DT.Rows.Add(DR)
            If String.IsNullOrEmpty(varTempStr) Then
                varTempStr = lstAssignTmps.Items(i).Value
            Else
                varTempStr = Trim(varTempStr) & "|" & lstAssignTmps.Items(i).Value
            End If
        Next

        'If DT.Rows.Count > 0 Then
        '    Dim clsTemp As ETS.BL.PhysiciansTempaltes
        '    Try
        '        clsTemp = New ETS.BL.PhysiciansTempaltes
        '        If clsTemp.btn_AssignedTemplatestoPhy(HPhyID.Value.ToString, DT) = True Then
        '            Response.Redirect("EditTemplateAssignments.aspx?phyid=" & HPhyID.Value)
        '        Else
        '            Response.Write("Assignment failed")
        '        End If
        '    Catch ex As Exception
        '        Response.Write(ex.Message)
        '    Finally
        '        clsTemp = Nothing
        '    End Try
        'End If
        Dim clsPT As New ETS.BL.PhysiciansTempaltes
        With clsPT
            .PhysicianID = HPhyID.Value
            ' Response.Write(varTempStr)
            If .btn_Assign_Click_From_Multiple(varTempStr) Then
                Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & HPhyID.Value & "&PhyName=" & Request("PhyName") & "&iRes=" & "Templates have been assigned Successfully", True)
            Else
                Response.Redirect("EditTemplateAssignments.aspx?PhyID=" & HPhyID.Value & "&PhyName=" & Request("PhyName") & "&iRes=" & "Assigning Templates Failed", True)
            End If
        End With
        clsPT = Nothing
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("phyid") <> "" Then
                HPhyID.Value = Request("phyid")
                Dim clsPhy As ETS.BL.Physicians
                Dim varPhyName As String = String.Empty
                Try
                    clsPhy = New ETS.BL.Physicians()
                    clsPhy.PhysicianID = HPhyID.Value.ToString
                    clsPhy.getPhysicianDetails()
                    If Not String.IsNullOrEmpty(clsPhy.FirstName) Then
                        varPhyName = clsPhy.FirstName
                    End If

                    If Not String.IsNullOrEmpty(clsPhy.LastName) Then
                        varPhyName = varPhyName & clsPhy.LastName
                    End If

                    lblCaption.Text = "Template Assignments for " & varPhyName.ToString
                Catch ex As Exception
                    Response.Write(ex.Message)
                Finally
                    clsPhy = Nothing
                End Try
            End If
            TemplateStatus(HPhyID.Value)
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
        Dim intCt As Integer
        For intCt = lstAvailTmps.Items.Count - 1 To 0 Step -1 ' Looping Backwards
            If lstAvailTmps.Items(intCt).Selected Then
                Dim LI As New ListItem
                LI.Text = lstAvailTmps.Items(intCt).Text
                LI.Value = lstAvailTmps.Items(intCt).Value
                lstAssignTmps.Items.Add(LI)
                lstAvailTmps.Items.Remove(lstAvailTmps.Items(intCt))
            End If
        Next
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove.Click
        Dim intCt As Integer
        For intCt = lstAssignTmps.Items.Count - 1 To 0 Step -1 ' Looping Backwards
            If lstAssignTmps.Items(intCt).Selected Then
                If Len(lstAssignTmps.Items(intCt).Value) = 36 Then
                    Dim clsPT As New ETS.BL.PhysiciansTempaltes
                    With clsPT
                        .PhysicianID = HPhyID.Value
                        .TemplateID = lstAssignTmps.Items(intCt).Value
                        If .DeletePhysicianTemplate() > 0 Then
                            Dim LI As New ListItem
                            LI.Text = lstAssignTmps.Items(intCt).Text
                            LI.Value = lstAssignTmps.Items(intCt).Value
                            lstAvailTmps.Items.Add(LI)
                            lstAssignTmps.Items.Remove(lstAssignTmps.Items(intCt))
                        End If
                    End With
                    clsPT = Nothing
                End If

            End If
        Next
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Dim Index As Integer = lstAssignTmps.SelectedIndex    'Index of selected item
        Dim Swap As Object = lstAssignTmps.SelectedItem       'Selected Item
        If Not (Swap Is Nothing) Then               'If something is selected...
            lstAssignTmps.Items.RemoveAt(Index)                   'Remove it
            lstAssignTmps.Items.Insert(Index - 1, Swap)           'Add it back in one spot up
            'lstAssignTmps.SelectedItem = Swap                     'Keep this item selected
        End If

    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Dim Index As Integer = lstAssignTmps.SelectedIndex    'Index of selected item       
        Dim Swap As Object = lstAssignTmps.SelectedItem       'Selected Item        
        If (Index <> -1) AndAlso (Index + 1 < lstAssignTmps.Items.Count) Then
            lstAssignTmps.Items.RemoveAt(Index) 'Remove it            
            lstAssignTmps.Items.Insert(Index + 1, Swap)           'Add it back in one spot up            
            'lstAssignTmps.SelectedItem = Swap                     'Keep this item selected        
        End If
    End Sub
End Class
