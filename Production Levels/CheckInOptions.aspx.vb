
Partial Class Admin_Levels_UsersAdminLevels
    Inherits BasePage
    Public CheckInOption As Long
    Public CheckInOptionIndirect As Long
    Public LevelName As String
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        
        Dim hdn As HiddenField
        Dim ConType As Integer
        Try
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

                Dim clsPLUp As New ETS.BL.ProductionLevels
                With clsPLUp
                    .ContractorID = Session("ContractorID")
                    .LevelNo = Request("LevelNo")
                    .CheckInOptions = UpdatedLevel
                    .IndirectOptions = UpdatedIndirectLevel
                End With

                If clsPLUp.UpdateLevelDetails() = 1 Then

                End If
                clsPLUp = Nothing
            End If



            Dim clsPL As New ETS.BL.ProductionLevels
            With clsPL
                .ContractorID = Session("ContractorID")
            End With
            Dim DS As Data.DataSet = clsPL.getPLevelList
            Dim DR() As Data.DataRow = DS.Tables(0).Select("LevelNo=" & CInt(Request("LevelNo")) & "")

            If DR(0).Item("LevelNo") = CInt(Request("LevelNo")) And DR.LongLength > 0 Then
                With DR(0)
                    LevelName = .Item("LevelName")
                    If Not IsDBNull(.Item("CheckInOptions")) Then
                        CheckInOption = .Item("CheckInOptions")
                    Else
                        CheckInOption = 0
                    End If
                    If Not IsDBNull(.Item("IndirectOptions")) Then
                        CheckInOptionIndirect = .Item("IndirectOptions")
                    Else
                        CheckInOptionIndirect = 0
                    End If
                    If Not IsDBNull(.Item("Type")) Then
                        ConType = .Item("Type")
                    End If

                End With
            End If

            DR = DS.Tables(0).Select(" Type=" & CBool(ConType) & " AND ContractorID='" & Session("ContractorID") & "' AND (LevelNo <> 2147483647 and LevelNo<>5 and LevelNo<>3 AND LevelNo <> " & CInt(Request("LevelNo")) & ")")
            
            If DR.LongLength > 0 Then
                rptLevels.DataSource = DR
                rptLevels.DataBind()
            Else
                Response.Write("No Records Found!")
            End If
            clsPL = Nothing
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
