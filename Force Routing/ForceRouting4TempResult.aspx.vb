
Partial Class FRTResult
    Inherits BasePage
    Public strTempName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
                iMain.Visible = True
                DBind()
            ElseIf IsPostBack Then
                iMain.Visible = True
            Else
                iMain.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message & "hh")
        End Try
    End Sub
    Private Sub DBind()


        If IsPostBack = False And String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
            iMain.Visible = True
            Session("WhereClause") = String.Empty

            If Not String.IsNullOrEmpty(Request("TName").ToString) Then
                Session("WhereClause") = Session("WhereClause") & " and T.TemplateName Like '" & Request("TName").ToString & "'"
            End If
        End If
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            Dim SQLString As String = "SELECT isnull(FR_T.Levels,0) as Levels, T.TemplateName, T.TypeDesc as TypeName,T.TemplateID " & _
            "FROM tblTemplates AS T " & _
            "LEFT OUTER JOIN tblForceRouting4Templates AS FR_T ON T.TemplateID = FR_T.TemplateID where T.TemplateID is not null and T.contractorID='" & Session("ContractorID") & "'"
            SQLString = SQLString & Session("WhereClause") & " order by T.TemplateName"

            Dim objDA As New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)
            Dim objDS As New System.Data.DataSet()
            objDA.Fill(objDS, "FRT")
            dlist.DataSource = objDS
            dlist.DataBind()
            'dlist.RowExpanded.CollapseAll()
            oConn.Close()
            If objDS.Tables(0).Rows.Count <= 0 Then
                iMain.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub



    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        iMain.Visible = False
        idetails.Visible = True
        rptHistory.DataSource = Nothing
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Dim hdn As HiddenField = lnk.Parent.FindControl("hdnTemp")
        hdnSelTempID.Value = hdn.Value.ToString
        hdn = lnk.Parent.FindControl("hdnLevels")
        HDNSelTempLvl.Value = CLng(hdn.Value.ToString)
        Dim lbl As Label = lnk.Parent.FindControl("lblTemp")
        strTempName = lbl.Text
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            Dim SQLString As String = "SELECT LevelName+' ('+ Description+')' as LevelName , LevelNo FROM tblProductionLevels WHERE Type =" & Session("IsContractor") & " AND ForcedRouting = 1 order by Sequence"

            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
            rptHistory.DataSource = oRec
            rptHistory.DataBind()
            oRec.Close()
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try

    End Sub
    Protected Function chkLevel(ByVal AdminLevel As Long, ByVal Level As Long) As Boolean
        If (AdminLevel And Level) = Level Then
            chkLevel = True
        Else
            chkLevel = False
        End If
    End Function

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim UpdatedLevel As Long
        Dim SQLString As String
        Dim PhyName As String = String.Empty
        Try
            Dim btn As Button = CType(sender, Button)
            For Each item As RepeaterItem In rptHistory.Items
                Dim chk As CheckBox = DirectCast(item.FindControl("ckSelected"), CheckBox)
                If chk.Checked Then
                    Dim hdn As HiddenField = chk.Parent.FindControl("hdnLvlNO")
                    If IsNumeric(hdn.Value) Then
                        UpdatedLevel = UpdatedLevel + CLng(hdn.Value)
                    End If
                End If
            Next
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                SQLString = "delete from tblForceRouting4Templates where TemplateID='" & hdnSelTempID.Value.ToString & "'"
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.ExecuteNonQuery()
                SQLString = "Insert into tblForceRouting4Templates(TemplateID,Levels) " & _
                            "Values('" & hdnSelTempID.Value.ToString & "'," & UpdatedLevel & ")"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.ExecuteNonQuery()
                iMain.Visible = True
                idetails.Visible = False
                Response.Write("<script>alert('Force Routing Levels have been successfully set');</script>")
                DBind()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        iMain.Visible = True
        idetails.Visible = False
        DBind()
    End Sub
End Class
