Imports System.Data.SqlClient
Partial Class Profuction_Levels_ProductionLevels
    Inherits BasePage

    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAdd.Click
        Dim newLevelNo As Long
        Dim ConString As String
        Dim SQLString As String

        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Try
            oConn.Open()
            SQLString = "SELECT MAX(LevelNo) + MAX(LevelNo) AS NewLevel,(SELECT COUNT(*) FROM tblProductionLevels " & _
                        "WHERE LevelName = '" & txtLevelName.Text & "' and Type=" & cmbType.SelectedValue & " and ContractorID='" & DLContractor.SelectedValue & "') AS RecExist FROM tblProductionLevels " & _
                        "WHERE LevelNo <> 2147483647 and LevelNo <> 1073741824  and ContractorID='" & DLContractor.SelectedValue & "'"
            Response.Write(SQLString)
            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
            oRec.Read()
            If oRec.HasRows Then
                If oRec("RecExist") > 0 Then
                    Dim strMessage = "User Role with name " & txtLevelName.Text & " is alreadt exist"
                    Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
                    GoTo lblflg
                Else

                    If IsDBNull(oRec("NewLevel")) Then
                        newLevelNo = 1
                    Else
                        newLevelNo = oRec("NewLevel")
                    End If

                End If
            Else
                newLevelNo = 1
            End If
            oRec.Close()
            SQLString = "Insert into tblProductionLevels(LevelName,LevelNo,Description,Type,ContractorID) " & _
                        "Values('" & txtLevelName.Text & "'," & newLevelNo & ",'" & txtLevelDesc.Text & "'," & cmbType.SelectedValue & ",'" & DLContractor.SelectedValue & "')"
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            If oCommand.ExecuteNonQuery() > 0 Then
                Dim strMessage = "User Role " & txtLevelName.Text & " added successfully"
                Response.Write("<script language=JavaScript>alert('" & strMessage & "');</script>")
                Response.Redirect("CheckInOptions.aspx?LevelNo=" & newLevelNo, True)
            End If
lblflg:
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> Data.ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Sub

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection(strConn)
            Try
                oConn.Open()
                Dim SQLCmd As New SqlCommand("Select * from DBO.tblcontractor", oConn)

                Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                If DRRec.HasRows = True Then
                    While DRRec.Read
                        Dim LI As New ListItem
                        LI.Text = DRRec("ContractorName").ToString
                        LI.Value = DRRec("ContractorID").ToString
                        DLContractor.Items.Add(LI)
                    End While
                End If
                DRRec.Close()


            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Open Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End If
    End Sub
End Class
