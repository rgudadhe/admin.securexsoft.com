Imports System
Imports System.Data
Namespace ets
    Partial Class Templates_TemplateAttributes
        Inherits BasePage
        Public TempName As String
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                lstBind()
            End If
        End Sub
        Private Function lstBind()
            Try
                lstAvailLAtrib.Items.Clear()
                lstAssignAtrib.Items.Clear()
                tdHeader.Text = "Attribute assignments for Template: " & Request("Name")
                TempName = Request("Name")
                TemplateID.Value = Request("TempID")
                TemplateName.Value = TempName
                UserID.Value = Session("UserID")
                Dim clsAtt As New ets.BL.Attributes
                Dim DSAtt As Data.DataSet = clsAtt.getEditAttributes(Session("ContractorID"))
                clsAtt = Nothing
                Dim clstempAtt As New ets.BL.TemplateAttributes
                clstempAtt.TemplateID = Request("TempID")
                clstempAtt._WhereString.Append(" order by Sequal")
                Dim DSTempAtt As Data.DataSet = clstempAtt.getTemplateAttributeList


                clstempAtt = Nothing
                If DSAtt.Tables.Count > 0 Then
                    Dim DV As New Data.DataView
                    DV = New Data.DataView(DSAtt.Tables(0), "", " Name ", DataViewRowState.CurrentRows)
                    For Each oRec As DataRow In DV.ToTable.Rows
                        Dim DR() As DataRow = DSTempAtt.Tables(0).Select("AttributeID='" & oRec("AttributeID").ToString & "'")
                        Dim LI As New ListItem
                        LI.Text = oRec("Name")
                        LI.Value = oRec("AttributeID").ToString
                        If UBound(DR) < 0 Then
                            lstAvailLAtrib.Items.Add(LI)
                        End If
                    Next
                    DV = Nothing
                End If

                If DSTempAtt.Tables.Count > 0 Then
                    For Each oRec As DataRow In DSTempAtt.Tables(0).Rows
                        Dim DR() As DataRow = DSAtt.Tables(0).Select("AttributeID='" & oRec("AttributeID").ToString & "'")
                        Dim LI As New ListItem
                        If UBound(DR) >= 0 Then
                            LI.Text = DR(0).Item("Name").ToString
                            LI.Value = oRec("AttributeID").ToString
                            lstAssignAtrib.Items.Add(LI)
                        End If
                    Next

                End If

                DSAtt.Dispose()
                DSTempAtt.Dispose()
                If lstAssignAtrib.Items.Count <= 0 Then
                    btnAddDefault.Visible = True
                End If


            Catch ex As exception
                Response.Write(ex.message)
            End Try
        End Function

        Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
            Dim intCt As Integer
            For intCt = lstAvailLAtrib.Items.Count - 1 To 0 Step -1 ' Looping Backwards
                If lstAvailLAtrib.Items(intCt).Selected Then
                    Dim LI As New ListItem
                    LI.Text = lstAvailLAtrib.Items(intCt).Text
                    LI.Value = lstAvailLAtrib.Items(intCt).Value
                    lstAssignAtrib.Items.Add(LI)
                    lstAvailLAtrib.Items.Remove(lstAvailLAtrib.Items(intCt))
                End If
            Next
        End Sub

        Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRemove.Click
            Dim intCt As Integer
            For intCt = lstAssignAtrib.Items.Count - 1 To 0 Step -1 ' Looping Backwards
                If lstAssignAtrib.Items(intCt).Selected Then
                    Dim LI As New ListItem
                    LI.Text = lstAssignAtrib.Items(intCt).Text
                    LI.Value = lstAssignAtrib.Items(intCt).Value
                    lstAvailLAtrib.Items.Add(LI)
                    lstAssignAtrib.Items.Remove(lstAssignAtrib.Items(intCt))
                End If
            Next
        End Sub

        
        Private Function SaveChanges() As Boolean
           
            Dim DT As New DataTable
            DT.Columns.Add("AttributeID", GetType(System.String))
            DT.Columns.Add("Sequal", GetType(System.String))

            For i As Integer = 0 To lstAssignAtrib.Items.Count - 1
                Dim DR As DataRow = DT.NewRow
                DR("AttributeID") = lstAssignAtrib.Items(i).Value.ToString
                DR("Sequal") = i
                DT.Rows.Add(DR)
            Next
            Dim clsTA As New ets.BL.TemplateAttributes
            With clsTA
                .TemplateID = TemplateID.Value
                If .setTemplateAttributes(DT) Then
                    Dim MTC As New MTClientService
                    Dim RecAffected As Integer = MTC.AuditTemplates(TemplateID.Value, Session("UserID").ToString, True)
                    If RecAffected > 0 Then
                        lblMessage.Text = "Changes have been saved successfully!"
                    Else
                        lblMessage.Text = "Failed saving changes!"
                    End If
                Else
                    lblMessage.Text = "Failed saving changes!"
                End If
            End With

            

            clsTA = Nothing
            DT.Dispose()
        End Function

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            SaveChanges()
        End Sub

        Protected Sub imgUp_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUp.Click
            Dim intCurrentIndex As Integer
            With lstAssignAtrib
                If .SelectedIndex > 0 Then
                    intCurrentIndex = .SelectedIndex
                    Dim lstItem As ListItem = .SelectedItem
                    .Items.RemoveAt(.SelectedIndex)
                    .Items.Insert(intCurrentIndex - 1, lstItem)
                    .SelectedIndex = intCurrentIndex - 1
                End If
            End With
        End Sub

        Protected Sub imgDown_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDown.Click
            Dim intCurrentIndex As Integer
            With lstAssignAtrib
                If .SelectedIndex < (.Items.Count - 1) And Not .SelectedIndex = -1 Then
                    intCurrentIndex = .SelectedIndex
                    Dim lstItem As ListItem = .SelectedItem
                    .Items.RemoveAt(.SelectedIndex)
                    .Items.Insert(intCurrentIndex + 1, lstItem)
                    .SelectedIndex = intCurrentIndex + 1
                End If
            End With
        End Sub

        Protected Sub btnAddDefault_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
            Dim clsAtt As New ets.BL.Attributes
            Dim DSAtt As Data.DataSet = clsAtt.getEditAttributes(Session("ContractorID"))
            clsAtt = Nothing

            If DSAtt.Tables.Count > 0 Then
                For Each oRec As DataRow In DSAtt.Tables(0).Rows
                    If oRec("IsDefault") Then
                        Dim LI As New ListItem
                        LI.Text = oRec("Name")
                        LI.Value = oRec("AttributeID").ToString
                        lstAssignAtrib.Items.Add(LI)
                        lstAvailLAtrib.Items.Remove(LI)
                    End If
                Next
            End If
            DSAtt.Dispose()
            btnAddDefault.Visible = False
           
        End Sub
    End Class
End Namespace