
Partial Class Admin_Levels_UsersAdminLevels
    Inherits BasePage
    Public CheckInOption As Long
    Public CheckInOptionIndirect As Long
    Public LevelName As String
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Not IsPostBack Then
            HContractor.Value = Request("ContractorID")
        End If
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
                Dim UpdatedIndirectLevel As Long
                For Each rptitem As RepeaterItem In rptLevels.Items
                    Dim chk As CheckBox = DirectCast(rptitem.FindControl("ckSelected"), CheckBox)
                    If chk.Checked Then
                        hdn = chk.Parent.FindControl("LevelNo")
                        If IsNumeric(hdn.Value) Then
                            UpdatedLevel = UpdatedLevel + CLng(hdn.Value)
                        End If
                    End If

                    chk = DirectCast(rptitem.FindControl("ckSelIndirect"), CheckBox)
                    If chk.Checked Then
                        hdn = chk.Parent.FindControl("LevelNo")
                        If IsNumeric(hdn.Value) Then
                            UpdatedIndirectLevel = UpdatedIndirectLevel + CLng(hdn.Value)
                        End If
                    End If
                Next
                SQLString = "update tblProductionLevels set CheckInOptions=" & UpdatedLevel & ",IndirectOptions=" & UpdatedIndirectLevel & " where LevelNo='" & Request("LevelNo") & "' and ContractorID='" & HContractor.Value & "'"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.ExecuteNonQuery()
            End If
            SQLString = "select LevelName,CheckInOptions,IndirectOptions from tblProductionLevels where LevelNo='" & Request("LevelNo") & "' and ContractorID='" & HContractor.Value & "'"
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oRec = oCommand.ExecuteReader()
            oRec.Read()
            If oRec.HasRows Then
                LevelName = oRec("LevelName")
                If Not IsDBNull(oRec("CheckInOptions")) Then
                    CheckInOption = oRec("CheckInOptions")
                Else
                    CheckInOption = 0
                End If
                If Not IsDBNull(oRec("IndirectOptions")) Then
                    CheckInOptionIndirect = oRec("IndirectOptions")
                Else
                    CheckInOptionIndirect = 0
                End If
            End If
            oRec.Close()
            SQLString = "SELECT PL.LevelName, PL.LevelNo FROM tblProductionLevels PL INNER JOIN " & _
                        "(SELECT type FROM tblProductionLevels WHERE LevelNo ='" & Request("LevelNo") & "' and ContractorID='" & HContractor.Value & "') lType ON PL.Type = lType.type " & _
                        "WHERE (PL.LevelNo <> '" & Request("LevelNo") & "') AND (PL.LevelNo <> 2147483647 and LevelNo<>5 and LevelNo<>3)  and PL.ContractorID='" & HContractor.Value & "' "
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

    

   
End Class
