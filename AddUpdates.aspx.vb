Imports System.Data.SqlClient
Partial Class AddUpdates
    Inherits BasePage

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim RowsAfected As Integer = 0
        If TxtSub.Text = "" Then
            iresponse.Text = "Subject Field is required. "
            TxtSub.Focus()
            Exit Sub
        End If
        If TxtDesc.Text = "" Then
            iresponse.Text = "Details Field is required. "
            TxtDesc.Focus()
            Exit Sub
        End If
        Try
            Dim clsUp As New ETS.BL.Updates
            With clsUp
                .Details = TxtDesc.Text.ToString
                .Dateupdated = Now
                .DateDisp = Now
                .userid = Session("userid").ToString
                .SubText = TxtSub.Text.ToString
                If Not String.IsNullOrEmpty(DLDept.SelectedValue) Then
                    .DepartmentID = DLDept.SelectedValue.ToString
                End If
                .contractorid = Session("Contractorid").ToString
            End With
            RowsAfected = clsUp.InsertUpdateDetails()
            Response.Write(" RowsAfected: " & RowsAfected)
            If RowsAfected = 1 Then
                iresponse.Text = "Updates has been added successfully."
            Else
                iresponse.Text = "Failed adding details"
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim clsDept As New ETS.BL.Department
            'clsDept.ContractorID = Session("Contractorid").ToString
            clsDept._WhereString.Append(" WHERE ContractorID='" & Session("Contractorid").ToString & "' ")
            BindDept(clsDept.getDepartmentList)
        End If
    End Sub
    Protected Sub BindDept(ByVal DS As Data.DataSet)
        Try
            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then
                    DLDept.DataSource = DS
                    DLDept.DataTextField = "Name"
                    DLDept.DataValueField = "Departmentid"
                    DLDept.DataBind()
                End If
            End If
            Dim LI1 As New ListItem
            LI1.Text = "All Departements"
            LI1.Value = ""
            LI1.Selected = True
            DLDept.Items.Insert(0, LI1)

        Catch ex As Exception
            'Response.Write(ex.Message)
        End Try
    End Sub
End Class
