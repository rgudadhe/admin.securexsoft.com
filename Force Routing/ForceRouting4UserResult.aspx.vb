
Partial Class FRUResult
    Inherits BasePage
    Public strUserName As String
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
        Try

            If IsPostBack = False And String.IsNullOrEmpty(Request.Form("SEARCH")) = False Then
                iMain.Visible = True
                Session("SQLString") = String.Empty
                Dim WhereClause As String = String.Empty
                If Not String.IsNullOrEmpty(Request("UName").ToString) Then
                    WhereClause = WhereClause & " and U.FirstName + ' ' + U.LastName Like '" & Request("UName").ToString & "'"
                End If
                If Not String.IsNullOrEmpty(Request("UID").ToString) Then
                    WhereClause = WhereClause & " and U.UserName like '" & Request("UID").ToString & "'"
                End If
                If Not String.IsNullOrEmpty(Request("ULevel").ToString) Then
                    hdnSelUserLevel.Value = Request("ULevel").ToString
                    WhereClause = WhereClause & " and (select dbo.chkLevel(UL.ProductionLevel," & CLng(Request("ULevel").ToString) & "))=1"
                End If
                '    Dim SQLString As String = "SELECT isnull(FR_U.Levels,0) as Levels, U.UserName AS UID, U.UserID, U.FirstName + ' ' + U.LastName AS UserName, UL.ProductionLevel, (SELECT LevelName FROM tblProductionLevels where levelno=" & CLng(Request("ULevel").ToString) & ") as UserLevel " & _
                '"FROM tblUsersLevels AS UL RIGHT OUTER JOIN tblUsers AS U ON UL.UserID = U.UserID LEFT OUTER JOIN " & _
                '"tblForceRouting4Users FR_U ON U.UserID = FR_U.UserID where (U.IsDeleted=0 or U.IsDeleted is null)"
                '    Session("SQLString") = SQLString & WhereClause & " order by UserName"
                Dim SQLString As String = "SELECT isnull(FR_U.Levels,0) as Levels, U.UserName AS UID, U.UserID, U.FirstName + ' ' + U.LastName AS UserName, UL.ProductionLevel, (SELECT LevelName FROM tblProductionLevels where levelno=" & CLng(Request("ULevel").ToString) & ") as UserLevel " & _
            "FROM tblUsersLevels AS UL RIGHT OUTER JOIN tblUsers AS U ON UL.UserID = U.UserID LEFT OUTER JOIN " & _
            "(select * from tblForceRouting4Users where userlevel =" & CLng(Request("ULevel").ToString) & ") AS FR_U ON U.UserID = FR_U.UserID where (U.IsDeleted=0 or U.IsDeleted is null) and U.contractorID='" & Session("ContractorID") & "'"
                Session("SQLString") = SQLString & WhereClause & " order by UserName"
            End If
            Response.Write(Session("SQLString"))
            Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            oConn.Open()
            dlist.DataSource = Nothing
            Dim objDA As New System.Data.SqlClient.SqlDataAdapter(Session("SQLString"), oConn)
            Dim objDS As New System.Data.DataSet()
            objDA.Fill(objDS, "FRU")
            dlist.DataSource = objDS
            dlist.DataBind()

            oConn.Close()
            If objDS.Tables(0).Rows.Count <= 0 Then
                iMain.Visible = False
            End If
        Catch ex As Exception
            Response.Write(ex.Message & " bb")
        End Try
    End Sub



    Protected Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        iMain.Visible = False
        idetails.Visible = True
        rptHistory.DataSource = Nothing
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Dim hdn As HiddenField = lnk.Parent.FindControl("hdnUser")
        hdnSelUserID.Value = hdn.Value.ToString
        hdn = lnk.Parent.FindControl("hdnLevels")
        HDNSelUserFRLvl.Value = CLng(hdn.Value.ToString)
        Dim lbl As Label = lnk.Parent.FindControl("lblUser")
        strUserName = lbl.Text
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        oConn.Open()
        Dim SQLString As String = "SELECT LevelName+' ('+ Description+')' as LevelName , LevelNo FROM tblProductionLevels WHERE Type =" & Session("IsContractor") & " AND ForcedRouting = 1 order by Sequence"
        Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
        Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
        rptHistory.DataSource = oRec
        rptHistory.DataBind()
        oRec.Close()
        oConn.Close()

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
            oConn.Open()
            SQLString = "delete from tblForceRouting4Users where UserID='" & hdnSelUserID.Value.ToString & "' and UserLevel=" & CLng(hdnSelUserLevel.Value.ToString)
            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand.ExecuteNonQuery()
            SQLString = "Insert into tblForceRouting4Users(UserID,UserLevel,Levels) " & _
                        "Values('" & hdnSelUserID.Value.ToString & "'," & CLng(hdnSelUserLevel.Value.ToString) & "," & UpdatedLevel & ")"
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand.ExecuteNonQuery()
            oConn.Close()
            iMain.Visible = True
            idetails.Visible = False
            Response.Write("<script>alert('Force Routing Levels have been successfully set');</script>")
            DBind()
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
