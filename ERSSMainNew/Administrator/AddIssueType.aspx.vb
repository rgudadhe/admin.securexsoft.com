Imports MainModule
Partial Class ERSSMain_AddIssueType
    Inherits BasePage
    Dim objMainModule As New MainModule
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
                clsERSSIT.ContractorID = Session("ContractorID").ToString
                DS = clsERSSIT.getIssueTypeList
                DV = New Data.DataView(DS.Tables(0))
                DV.RowFilter = "IssueName='" & varStrIssueName & "' AND (IsDeleted IS NULL OR IsDeleted=0) "

                If DV.ToTable().Rows.Count > 0 Then
                    Response.Write("<script type=""text/javascript"" language=javascript> alert(""Issue Type already exists"");window.location.href='AddIssueType.aspx?CID=" & Request.QueryString("CID") & "';</script>")
                    Exit Sub
                Else
                    varBolCheckPrefix = True
                End If

                Dim RetVal As Integer
                If varBolCheckPrefix Then
                    With clsERSSIT
                        .CategoryID = ViewState("CID").ToString
                        .IssueName = varStrIssueName.ToString
                        .Description = varStrIssueDesc.ToString
                        .DtCreated = Now
                    End With
                End If
                RetVal = clsERSSIT.InsertIssueType()
                If RetVal = 1 Then
                    Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Issue Type added sucessfully !!!</font></center>")
                    Response.Write("<center><a href=""CloseWindow.aspx"">Close Window</a></center>")
                    Response.End()
                Else
                    Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">Addition of Issue Type failed</font></center>")
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ViewState("CID") = Request.QueryString("CID")
    End Sub
End Class
