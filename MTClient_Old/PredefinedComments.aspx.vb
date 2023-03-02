Imports System
Imports System.Data
Partial Class PredefinedComments
    Inherits BasePage
    Dim gvUniqueID As String
    Dim varParentGridViewRow As GridViewRow
    Dim gvNewPageIndex As Integer = 0
    Dim gvSortExpr As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FillGrid()
        End If
    End Sub
    Public Function OpenConnection(ByRef Conn As Data.SqlClient.SqlConnection) As Data.SqlClient.SqlConnection
        Conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Conn.Open()
        Return Conn
    End Function
    Protected Sub GridViewMain_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridViewMain.PageIndexChanging
        GridViewMain.PageIndex = e.NewPageIndex
        FillGrid()
    End Sub
    Protected Sub GridViewMain_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles GridViewMain.RowCancelingEdit
        GridViewMain.EditIndex = -1
        FillGrid()
    End Sub
    Protected Sub GridViewMain_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewMain.RowCommand
        If Trim(UCase(e.CommandSource.ToString)) = Trim(UCase("System.Web.UI.WebControls.LinkButton")) Then
            If Trim(UCase(e.CommandName)) = Trim(UCase("AddComment")) Then
                Try
                    Dim varStrComment As String = String.Empty
                    varStrComment = DirectCast(GridViewMain.FooterRow.FindControl("txtComment"), TextBox).Text
                    If varStrComment <> "" Then
                        Dim clsPDC As New ETS.BL.PreDefinedComments
                        With clsPDC
                            .Comment = varStrComment
                            .ContractorID = Session("ContractorID")
                            .IsDeleted = False
                            If .InsertPDC = 1 Then
                                ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Comment added successfully');window.location='PredefinedComments.aspx'</script>")
                            Else
                                ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Failed adding Comment');window.location='PredefinedComments.aspx'</script>")
                            End If
                        End With
                        clsPDC = Nothing
                        'FillGrid()
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message)
                End Try
            Else
                Dim varStrId As String = String.Empty


                Dim objTempButton As LinkButton
                Dim varGridViewRow As GridViewRow
                objTempButton = DirectCast(e.CommandSource, LinkButton)
                varGridViewRow = DirectCast(objTempButton.NamingContainer, GridViewRow)

                varStrId = DirectCast(varGridViewRow.FindControl("ID"), HiddenField).Value.ToString

                If Trim(UCase(objTempButton.CommandName)) = Trim(UCase("Deleted")) Then
                    Dim clsPDC As New ETS.BL.PreDefinedComments
                    With clsPDC
                        .IsDeleted = True
                        .ID = varStrId
                        If .UpdatePDC >= 1 Then
                            ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Comment deleted successfully');window.location='PredefinedComments.aspx'</script>")
                        Else
                            ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Failed deleting Comment');window.location='PredefinedComments.aspx'</script>")
                        End If
                    End With
                    clsPDC = Nothing

                    'FillGrid()

                End If


                If Trim(UCase(e.CommandName)) = Trim(UCase("Active")) Then
                    Dim clsPDC As New ETS.BL.PreDefinedComments
                    With clsPDC
                        .IsDeleted = False
                        .ID = varStrId
                        If .UpdatePDC >= 1 Then
                            ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Comment activted successfully');window.location='PredefinedComments.aspx'</script>")
                        Else
                            ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Failed activating Comment');window.location='PredefinedComments.aspx'</script>")
                        End If
                    End With
                    clsPDC = Nothing

                    'FillGrid()


                End If
            End If
        End If
    End Sub
    Protected Sub GridViewMain_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridViewMain.RowDeleting
        Dim varStrID As String

        Dim varStrOpr As String = String.Empty
        Try
            varStrID = DirectCast(GridViewMain.Rows(e.RowIndex).FindControl("ID"), HiddenField).Value.ToString
            Dim objbtn As New Button
            objbtn = DirectCast(sender, Button)


            If Trim(UCase(objbtn.CommandName)) = Trim(UCase("Deleted")) Then
                Dim clsPDC As New ETS.BL.PreDefinedComments
                With clsPDC
                    .IsDeleted = True
                    .ID = varStrID
                    If .UpdatePDC >= 1 Then
                        ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Comment deleted successfully');window.location='PredefinedComments.aspx'</script>")
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Failed deleting Comment');window.location='PredefinedComments.aspx'</script>")
                    End If
                End With
                clsPDC = Nothing
                'FillGrid()


            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub GridViewMain_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles GridViewMain.RowEditing
        GridViewMain.EditIndex = e.NewEditIndex
        FillGrid()
    End Sub
    Protected Sub GridViewMain_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles GridViewMain.RowUpdating
        Dim varStrComment As String
        Dim varStrID As String
        
        varStrID = DirectCast(GridViewMain.Rows(e.RowIndex).FindControl("ID"), HiddenField).Value.ToString
        varStrComment = DirectCast(GridViewMain.Rows(e.RowIndex).FindControl("txtCommentEdit"), TextBox).Text
        Try
            Dim clsPDC As New ETS.BL.PreDefinedComments
            With clsPDC
                .Comment = varStrComment
                .ID = varStrID
                .IsDeleted = False
                If .UpdatePDC >= 1 Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Comment updated successfully');window.location='PredefinedComments.aspx'</script>")
                Else
                    ClientScript.RegisterStartupScript(Me.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Failed updating Comment');window.location='PredefinedComments.aspx'</script>")
                End If
            End With
            clsPDC = Nothing

            
            'FillGrid()
            GridViewMain.EditIndex = -1

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Protected Function SetStatus(ByVal str) As String
        If IsDBNull(str) Then
            Return "Active"
        Else
            If str = True Then
                Return "Deleted"
            Else
                Return "Active"
            End If
        End If
    End Function
    Protected Sub FillGrid()
        Dim DSPDC As New DataSet
        Dim clsPDC As New ETS.BL.PreDefinedComments
        With clsPDC
            .ContractorID = Session("ContractorID")
            DSPDC = .getPDCList()
        End With
        clsPDC = Nothing
        Dim identity As DataColumn = New DataColumn("SRNo", GetType(System.Int32))
        identity.AutoIncrement = True
        identity.AutoIncrementSeed = 0
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
        End If
        DSPDC.Dispose()
       
    End Sub
    Protected Sub GridViewMain_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridViewMain.Sorting
        'Dim varStrTemp = ViewState("Dir")

        'If varStrTemp = "DESC" Then
        '    ViewState("Dir") = "ASC"
        'Else
        '    ViewState("Dir") = "DESC"
        'End If

        'Dim objConn As New Data.SqlClient.SqlConnection
        'objConn = OpenConnection(objConn)

        'Dim objCmd As New Data.SqlClient.SqlCommand("SELECT ROW_NUMBER() OVER(ORDER BY ID) AS SrNo,ID,Comment,IsDeleted FROM DBO.tblPreDefinedComments where ContractorID='" & Session("ContractorID") & "'", objConn)
        'Dim objDataSet As New Data.DataSet
        'Dim objAdapter As New Data.SqlClient.SqlDataAdapter(objCmd)
        'objAdapter.Fill(objDataSet)

        'Dim DV As New Data.DataView
        'DV = objDataSet.Tables(0).DefaultView

        'GetSortDirection()
        'DV.ApplyDefaultSort = True
        'DV.Sort = e.SortExpression & " " & ViewState("Dir")

        'GridViewMain.DataSource = objDataSet.Tables(0)
        'GridViewMain.DataBind()

    End Sub
    Public Property gvSortDir()
        Get
            Return IIf(ViewState("SortDirection") = Nothing, 1, ViewState("SortDirection"))
        End Get
        Set(ByVal value)
            ViewState("SortDirection") = value
        End Set
    End Property
    'This procedure returns the Sort Direction
    Public Function GetSortDirection() As String
        Select Case (gvSortDir())
            Case "1"
                gvSortDir = 0
            Case "0"
                gvSortDir = 1
        End Select
        Return gvSortDir()
    End Function
End Class
