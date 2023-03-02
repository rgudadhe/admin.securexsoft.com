Imports MainModule
Partial Class ERSSMain_EditIssueType
    Inherits BasePage
    Dim objMainModule As New MainModule
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim clsERSSIT As ETS.BL.ERSSIssueType
            Try
                clsERSSIT = New ETS.BL.ERSSIssueType
                clsERSSIT.IssueID = Request.QueryString("ID")
                clsERSSIT.getIssueTypeDetails()
                ViewState("CID") = Request.QueryString("ID")
                txtIssueName.Text = clsERSSIT.IssueName
                txtIssueDesc.InnerText = clsERSSIT.Description
            Catch ex As Exception
            Finally
                clsERSSIT = Nothing
            End Try
        End If
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        Dim varBolCheckPrefix As Boolean
        Dim DS As Data.DataSet
        Dim DV As Data.DataView
        Dim clsERSSIT As ETS.BL.ERSSIssueType
        Try

            Dim varStrIssueName As String
            Dim varStrIssueDesc As String

            varStrIssueName = Replace(Request.Form("txtIssueName"), "'", "''")
            varStrIssueDesc = Replace(Request.Form("txtIssueDesc"), "'", "''")

            If varStrIssueDesc <> "" Then
                clsERSSIT = New ETS.BL.ERSSIssueType
                clsERSSIT.ContractorID = Session("ContractorID")

                DS = clsERSSIT.getIssueTypeList()
                DV = New Data.DataView(DS.Tables(0))
                DV.RowFilter = "IssueName='" & varStrIssueName & "' AND IssueID <>'" & ViewState("CID") & "' AND (IsDeleted IS NULL OR IsDeleted=0) "


                If DV.ToTable().Rows.Count > 0 Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Issue type already exists"");window.location.href='EditIssueType.aspx?ID=" & Request.QueryString("ID").ToString & "';</script>")
                    Exit Sub
                Else
                    varBolCheckPrefix = True
                End If
                Dim RetVal As Integer

                If varBolCheckPrefix = True Then
                    With clsERSSIT
                        .IssueID = ViewState("CID").ToString
                        .IssueName = varStrIssueName.ToString
                        .Description = varStrIssueDesc
                        .DtCreated = Now
                    End With
                End If

                RetVal = clsERSSIT.UpdateIssueTypeDetails()

                If RetVal = 1 Then
                    Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Issue Type updated sucessfully !!!</font></center>")
                    Response.Write("<center><a href=""CloseWindow.aspx"">Close Window</a></center>")
                    Response.End()
                Else
                    Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Issue Type updation failed</font></center>")
                    Response.Write("<center><a href=""CloseWindow.aspx"">Close Window</a></center>")
                    Response.End()
                End If
            Else
                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Please enter issue description</font></center>")
                Response.Write("<center><a href=""CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            End If
        Catch ex As Exception
        Finally
            clsERSSIT = Nothing
            DS = Nothing
            DV = Nothing
        End Try
    End Sub
    Protected Sub BtnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Dim clsERSSIT As ETS.BL.ERSSIssueType
        Dim RetVal As Integer
        Try
            clsERSSIT = New ETS.BL.ERSSIssueType
            With clsERSSIT
                .IssueID = ViewState("CID").ToString
                .IsDeleted = True
            End With
            RetVal = clsERSSIT.UpdateIssueTypeDetails()
            If RetVal = 1 Then
                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Issue Type updated sucessfully !!!</font></center>")
                Response.Write("<center><a href=""CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            Else
                Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Issue Type updation failed</font></center>")
                Response.Write("<center><a href=""CloseWindow.aspx"">Close Window</a></center>")
                Response.End()
            End If
        Catch ex As Exception
        Finally
            clsERSSIT = Nothing
        End Try
    End Sub
End Class
