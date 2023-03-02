Imports System.Data

Partial Class UpdateOptions
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Private Sub BindData()
        Dim clsAtt As New ETS.BL.Attributes
        Dim DTAttOp As Data.DataTable = clsAtt.getAttributeOptions(Request("AttributeID"))
        clsAtt = Nothing
        If DTAttOp.Rows.Count > 0 Then
            For Each oRec As DataRow In DTAttOp.Rows
                lstAssignOptions.Items.Add(oRec("OptionValue"))
            Next
        End If
        DTAttOp.Dispose()
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If txtNewOption.Text.Trim <> "" Then
            lstAssignOptions.Items.Add(txtNewOption.Text)
            txtNewOption.Text = ""
        End If
    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Dim intCt As Integer
        For intCt = lstAssignOptions.Items.Count - 1 To 0 Step -1 ' Looping Backwards
            If lstAssignOptions.Items(intCt).Selected Then
                lstAssignOptions.Items.Remove(lstAssignOptions.Items(intCt))
                Exit For
            End If
        Next
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim DTOp As New DataTable
        Dim DC As New DataColumn("Value", GetType(System.String))
        DTOp.Columns.Add(DC)

        DC = New DataColumn("Seq", GetType(System.Int32))
        DTOp.Columns.Add(DC)
        DTOp.AcceptChanges()

        Dim DR As DataRow
        For Each item As ListItem In lstAssignOptions.Items
            DR = DTOp.NewRow
            DR("Value") = item.Text
            DR("Seq") = lstAssignOptions.Items.IndexOf(item)
            DTOp.Rows.Add(DR)
        Next
        If DTOp.Rows.Count > 0 Then
            Dim clsAtt As New ETS.BL.Attributes
            With clsAtt
                If .SaveAttributeOptions(Request("AttributeID"), DTOp) > 0 Then
                    lblmessage.text = "Options updated successfully!"
                Else
                    lblmessage.text = "updating options failed!"
                End If
                'Response.Write(Request("AttributeID"))
            End With
        End If
    End Sub

    Protected Sub btnMoveUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click
        Dim lstItem As ListItem = lstAssignOptions.SelectedItem
        If Not lstItem Is Nothing Then
            Dim index As Integer = lstAssignOptions.Items.IndexOf(lstItem)
            If index <> 0 Then
                lstAssignOptions.Items.Remove(lstItem)
                lstAssignOptions.Items.Insert(index - 1, lstItem)
                lstAssignOptions.Items(index - 1).Selected = True
            End If
        End If
        
    End Sub

    Protected Sub btnMoveDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click
        Dim lstItem As ListItem = lstAssignOptions.SelectedItem
        If Not lstItem Is Nothing Then
            Dim index As Integer = lstAssignOptions.Items.IndexOf(lstItem)
            If index <> (lstAssignOptions.Items.Count - 1) Then
                lstAssignOptions.Items.Remove(lstItem)
                lstAssignOptions.Items.Insert(index + 1, lstItem)
                lstAssignOptions.Items(index + 1).Selected = True
            End If
        End If
    End Sub
End Class
