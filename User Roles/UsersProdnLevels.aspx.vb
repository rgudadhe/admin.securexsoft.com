
Partial Class Admin_Levels_UsersAdminLevels
    Inherits BasePage
    Public SelectedUserLevel As Long
    Public CanSetSamples As Boolean
    Public uName As String
    Public DSLimits As New System.Data.DataSet()

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        Dim ConString As String
        Dim SQLString As String
        Dim hdn As HiddenField
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCommand As New Data.SqlClient.SqlCommand
        Dim oRec As Data.SqlClient.SqlDataReader
        oConn.ConnectionString = ConString
        Try
            oConn.Open()

            If IsPostBack Then
                Dim UpdatedLevel As Long
                Dim flg As Boolean = False
                SQLString = "DELETE from tblUsersLimits where UserID='" & Request("UserID") & "'"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.ExecuteNonQuery()
                For Each rptitem As RepeaterItem In rptLevels.Items
                    Dim chk As CheckBox = DirectCast(rptitem.FindControl("ckSelected"), CheckBox)
                    If chk.Checked Then
                        hdn = chk.Parent.FindControl("LevelNo")
                        If IsNumeric(hdn.Value) Then
                            UpdatedLevel = UpdatedLevel + CLng(hdn.Value)
                        End If
                        If Not flg Then
                            Dim chkSamples As CheckBox = rptLevels.Controls(rptLevels.Controls.Count - 1).FindControl("chkSetSamples")
                            If chkSamples.Visible AndAlso chkSamples.Checked Then

                                flg = True
                            Else
                                flg = False
                            End If
                        End If


                        Dim txt As TextBox = chk.Parent.FindControl("txtViewLimit")
                        Dim ViewLimit As Long = IIf(IsNumeric(txt.Text), CLng(txt.Text), 0)
                        txt = chk.Parent.FindControl("txtChkOutLimit")
                        Dim ChkOutLimit As Long = IIf(IsNumeric(txt.Text), CLng(txt.Text), 0)
                        Dim Level As Long = IIf(IsNumeric(hdn.Value), CLng(hdn.Value), 0)
                        If Level <> 0 Then
                            SQLString = "insert into tblUsersLimits(UserID,LevelNo,ViewLimit,CheckOutLimit) values('" & Request("UserID") & "'," & Level & "," & ViewLimit & "," & ChkOutLimit & ")"
                            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                            oCommand.ExecuteNonQuery()
                        End If
                        'Response.Write(SQLString)

                    End If
                Next

                CanSetSamples = flg
                SQLString = "update tblUsersLevels set ProductionLevel=" & UpdatedLevel & ", CanSetSamples='" & CanSetSamples & "' where UserID='" & Request("UserID") & "'"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)

                If oCommand.ExecuteNonQuery() = 0 Then
                    SQLString = "insert into tblUsersLevels(UserID,ProductionLevel,CanSetSamples) values('" & Request("UserID") & "'," & UpdatedLevel & ",'" & CanSetSamples & "')"
                    'Response.Write(SQLString)
                    'Response.End()
                    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                    oCommand.ExecuteNonQuery()
                End If
            End If
            SQLString = "SELECT UL.ProductionLevel, U.FirstName + ' ' + U.LastName + ' (' + U.UserName + ')' AS uName, UL.CanSetSamples " & _
                        "FROM tblUsersLevels UL INNER JOIN tblUsers U ON UL.UserID = U.UserID where UL.UserID='" & Request("UserID") & "'"
            'Response.Write(SQLString)
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oRec = oCommand.ExecuteReader()
            oRec.Read()
            If oRec.HasRows Then

                uName = "Production Levels for " & oRec("uName")
                If Not IsDBNull(oRec("ProductionLevel")) Then
                    SelectedUserLevel = oRec("ProductionLevel")
                Else
                    SelectedUserLevel = 0
                End If
                If Not IsDBNull(oRec("CanSetSamples")) Then
                    CanSetSamples = oRec("CanSetSamples")
                Else
                    CanSetSamples = False
                End If
            End If
            oRec.Close()
            SQLString = "SELECT LevelNo,ViewLimit,CheckOutLimit FROM tblUsersLimits where UserID='" & Request("UserID") & "'"
            Dim objDA As New System.Data.SqlClient.SqlDataAdapter(SQLString, oConn)
            objDA.Fill(DSLimits, "tblLimits")
            objDA = Nothing
            SQLString = "select LevelName,LevelNo,ErrMarking from tblProductionLevels where Type=" & Session("IsContractor") & " and (LevelNo<>1073741824 and LevelNo<>5 and LevelNo<>3) and isdeleted=0 and  ContractorID='" & IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString) & "'"
            'Response.Write(SQLString)
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oRec = oCommand.ExecuteReader()
            If oRec.HasRows Then
                rptLevels.DataSource = oRec
                rptLevels.DataBind()
            Else
                Response.Write("No Records Found!")
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
    Protected Function chkLevel(ByVal AdminLevel As Long, ByVal Level As Long) As Boolean
        If (AdminLevel And Level) = Level Then
            chkLevel = True
        Else
            chkLevel = False
        End If
    End Function
    Protected Function getViewLimit(ByVal RowLevel As Long) As Long
        Dim response As Long = 0
        For Each DR As System.Data.DataRow In DSLimits.Tables("tblLimits").Rows
            If RowLevel = DR("LevelNo") Then
                response = DR("ViewLimit")
                Exit For
            End If
        Next
        Return response
    End Function
    Protected Function getChkOutLimit(ByVal RowLevel As Long) As Long
        Dim response As Long = 0
        For Each DR As System.Data.DataRow In DSLimits.Tables("tblLimits").Rows
            If RowLevel = DR("LevelNo") Then
                response = CLng(DR("CheckOutLimit"))
                Exit For
            End If
        Next
        Return response
    End Function
    

    
    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim ConString As String
    '    Dim SQLString As String
    '    Dim hdn As HiddenField
    '    ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Dim oConn As New Data.SqlClient.SqlConnection
    '    Dim oCommand As New Data.SqlClient.SqlCommand
    '    Dim oRec As Data.SqlClient.SqlDataReader
    '    oConn.ConnectionString = ConString
    '    oConn.Open()
    '    Dim UpdatedLevel As Long
    '    Dim flg As Boolean = False
    '    SQLString = "DELETE from tblUsersLimits where UserID='" & Request("UserID") & "'"
    '    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
    '    oCommand.ExecuteNonQuery()
    '    For Each rptitem As RepeaterItem In rptLevels.Items
    '        Dim chk As CheckBox = DirectCast(rptitem.FindControl("ckSelected"), CheckBox)
    '        If chk.Checked Then
    '            hdn = chk.Parent.FindControl("LevelNo")
    '            If IsNumeric(hdn.Value) Then
    '                UpdatedLevel = UpdatedLevel + CLng(hdn.Value)
    '            End If
    '            If Not flg Then
    '                Dim chkSamples As CheckBox = chk.Parent.FindControl("chkSetSamples")
    '                If chkSamples.Checked Then
    '                    flg = True
    '                Else
    '                    flg = False
    '                End If
    '            End If
    '            CanSetSamples = flg
    '            Dim txt As TextBox = chk.Parent.FindControl("txtViewLimit")
    '            Dim ViewLimit As Long = IIf(IsNumeric(txt.Text), CLng(txt.Text), 0)
    '            txt = chk.Parent.FindControl("txtChkOutLimit")
    '            Dim ChkOutLimit As Long = IIf(IsNumeric(txt.Text), CLng(txt.Text), 0)
    '            Dim Level As Long = IIf(IsNumeric(hdn.Value), CLng(hdn.Value), 0)
    '            If Level <> 0 Then
    '                SQLString = "insert into tblUsersLimits(UserID,LevelNo,ViewLimit,CheckOutLimit) values('" & Request("UserID") & "'," & Level & "," & ViewLimit & "," & ChkOutLimit & ")"
    '                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
    '                oCommand.ExecuteNonQuery()
    '            End If
    '            Response.Write(SQLString)
    '            Response.End()
    '        End If
    '    Next
    '    SQLString = "update tblUsersLevels set ProductionLevel=" & UpdatedLevel & ", CanSetSamples='" & CanSetSamples & "' where UserID='" & Request("UserID") & "'"
    '    oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)

    '    If oCommand.ExecuteNonQuery() = 0 Then
    '        SQLString = "insert into tblUsersLevels(UserID,ProductionLevel,CanSetSamples) values('" & Request("UserID") & "'," & UpdatedLevel & "," & CanSetSamples & ")"
    '        oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
    '        oCommand.ExecuteNonQuery()
    '    End If
    'End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("P_LevelAssignments.aspx?CriUser=" & Request("CriUser") & "&CriOption=" & Request("CriOption"), True)
    End Sub
End Class
