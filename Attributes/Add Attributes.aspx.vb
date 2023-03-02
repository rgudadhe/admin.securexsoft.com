Imports System
Imports System.Data
Namespace ets
    Partial Class Attributes_Add
        Inherits BasePage
        Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
            Dim strMessage As String
            If InStr(Trim(txtAttribName.Text), " ") > 0 Then
                valAttribName.IsValid = False
                valAttribName.ErrorMessage = "Name can not have white space"
            Else
                Dim clsAtt As New ets.BL.Attributes
                With clsAtt
                    .ContractorID = Session("ContractorID")
                    ._WhereString.Append(" and Name = '" & txtAttribName.Text & "'")
                    Dim DSAtt As DataSet = .getAttributeList()
                    If Not DSAtt.Tables(0).Rows.Count > 0 Then
                        .Name = txtAttribName.Text
                        .Caption = txtAttribCaption.Text
                        .Type = ddType.SelectedValue
                        .CanDelete = ddCanDelete.SelectedValue
                        If .InsertAttribute = 1 Then
                            strMessage = "Attribute " & txtAttribName.Text & " added successfully"
                        End If
                    Else
                        strMessage = "Attribute with name " & txtAttribName.Text & " is alreadt exist"
                    End If
                    DSAtt.Dispose()
                End With
                clsAtt = Nothing
                Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
            End If
        End Sub


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then

            End If
        End Sub



    End Class
End Namespace