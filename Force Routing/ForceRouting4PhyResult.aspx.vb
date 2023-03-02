
Partial Class FRPResult
    Inherits BasePage
    Public strphyName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If String.IsNullOrEmpty(Request.QueryString("PIndex")) = False Or String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
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

            If Not String.IsNullOrEmpty(Request("PFirst").ToString) Then
                Session("WhereClause") = Session("WhereClause") & " and P.FirstName Like '" & Request("PFirst").ToString & "'"
            End If
            If Not String.IsNullOrEmpty(Request("PLast").ToString) Then
                Session("WhereClause") = Session("WhereClause") & " and P.LastName Like '" & Request("PLast").ToString & "'"
            End If
        End If
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            Dim SQLString As String = "SELECT P.PhysicianID, P.FirstName + ' ' + P.LastName AS PhyName,A.AccountName, isnull(FR_P.Levels,0) as Levels " & _
            "FROM tblPhysicians as P LEFT OUTER JOIN tblForceRouting4Physicians AS FR_P ON P.PhysicianID = FR_P.PhysicianID " & _
            "INNER JOIN tblAccounts AS A ON P.AccountID = A.AccountID " & _
            " Where A.ContractorID='" & Session("ContractorID") & "' and (A.IsDeleted=0 or A.IsDeleted is null)"
            SQLString = SQLString & Session("WhereClause") & " order by PhyName"

            'Response.Write(SQLString)

            Dim objDA As New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)

            Dim objDS As New System.Data.DataSet()

            Dim dc1 As New System.Data.DataColumn
            Dim dc2 As New System.Data.DataColumn
            'dlist.CurrentPageIndex = ViewState("PIndex").ToString
            objDA.Fill(objDS, "FRP")
            objDA.Fill(objDS, "FRPDetails")
            dc1 = objDS.Tables(0).Columns("PhysicianID")
            dc2 = objDS.Tables(1).Columns("PhysicianID")
            Dim dr As System.Data.DataRelation = New System.Data.DataRelation("Dic", dc1, dc2, False)
            objDS.Relations.Add(dr)
            'dlist.TemplateControl.LoadTemplate("FRDetails4Phy.ascx")
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
        Dim hdn As HiddenField = lnk.Parent.FindControl("hdnPhy")
        hdnSelPhyID.Value = hdn.Value.ToString
        hdn = lnk.Parent.FindControl("hdnLevels")
        HDNSelPhyLvl.Value = CLng(hdn.Value.ToString)
        Dim lbl As Label = lnk.Parent.FindControl("lblPhy")
        strphyName = lbl.Text
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
                SQLString = "delete from tblForceRouting4Physicians where PhysicianID='" & hdnSelPhyID.Value.ToString & "'"
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.ExecuteNonQuery()
                SQLString = "Insert into tblForceRouting4Physicians(PhysicianID,Levels) " & _
                            "Values('" & hdnSelPhyID.Value.ToString & "'," & UpdatedLevel & ")"
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
