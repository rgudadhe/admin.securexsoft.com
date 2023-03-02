Namespace ets
    Public Class Main_left
        Inherits PageBase 'OboutInc.oboutAJAXPage
        Protected pro7 As OboutInc.SlideMenu.SlideMenu


        Public Sub UpdateSlideMenu(ByVal cId As String)
            Dim arrCID
            arrCID = Split(cId, "|")
            pro7.SelectedId = arrCID(0)
            ' Refresh left menu
            'UpdatePanel("cp_slidemenu")
        End Sub



        Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                Dim ConString As String
                Dim SQLString As String
                ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
                Dim oConn As New Data.SqlClient.SqlConnection
                oConn.ConnectionString = ConString
                oConn.Open()
                SQLString = "SELECT LevelNo, LevelName as Description FROM tblAdminLevels order by LevelNo"
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
                oRec.Read()
                If (CInt(Session("AdminLevel")) And oRec("LevelNo")) = oRec("LevelNo") Then
                    pro7.AddParent(oRec("LevelNo"), oRec("Description"))
                End If
                Do While oRec.Read
                    If (CInt(Session("AdminLevel")) And oRec("LevelNo")) = oRec("LevelNo") Then
                        pro7.AddParent(oRec("LevelNo"), oRec("Description"))
                    End If
                Loop
                oRec.Close()

                SQLString = "SELECT AL.LevelNo, LL.Link_Caption, LL.Link_Path FROM tblAdminLevels AL INNER JOIN tblAdminLevelLinks LL ON AL.LevelNo = LL.LevelNo order by AL.LevelNo"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oRec = oCommand.ExecuteReader()
                oRec.Read()
                If (CInt(Session("AdminLevel")) And oRec("LevelNo")) = oRec("LevelNo") Then
                    pro7.AddChildAt(oRec("Link_Caption"), oRec("LevelNo"), oRec("Link_Caption"), "", "LoadMainPage('" & oRec("Link_Caption") & "|" & oRec("Link_Path") & "')", "", "splDV.RightPanel.Content.Url")
                End If

                Do While oRec.Read

                    If (CInt(Session("AdminLevel")) And oRec("LevelNo")) = oRec("LevelNo") Then
                        pro7.AddChildAt(oRec("Link_Caption"), oRec("LevelNo"), oRec("Link_Caption"), "", "LoadMainPage('" & oRec("Link_Caption") & "|" & oRec("Link_Path") & "')", "", "")
                    End If
                Loop
                oRec.Close()
                oConn.Close()
            End If
        End Sub
    End Class
End Namespace