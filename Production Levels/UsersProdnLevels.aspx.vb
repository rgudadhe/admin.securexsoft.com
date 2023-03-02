Imports System
Imports System.Data
Partial Class Admin_Levels_UsersAdminLevels
    Inherits BasePage
    Public SelectedUserLevel As Long
    Public CanSetSamples As Boolean
    Public uName As String = String.Empty
    Public DSLimits As New System.Data.DataSet()

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
       
        Dim hdn As HiddenField
        Dim DT As New DataTable
        DT.Columns.Add("LevelNo", GetType(System.Int32))
        DT.Columns.Add("ViewLimit", GetType(System.Int32))
        DT.Columns.Add("CheckOutLimit", GetType(System.Int32))
        Dim DR As DataRow
       
        Try

            If IsPostBack Then
                Dim UpdatedLevel As Long
                Dim flg As Boolean = False
                
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
                            DR = DT.NewRow
                            DR("LevelNo") = Level
                            DR("ViewLimit") = ViewLimit
                            DR("CheckOutLimit") = ChkOutLimit
                            DT.Rows.Add(DR)
                        End If
                    End If
                Next

                CanSetSamples = flg
                Dim clsUL As New ETS.BL.UserLevels
                With clsUL
                    .UserID = Request("UserID")
                    .ProductionLevel = UpdatedLevel
                    .CanSetSamples = CanSetSamples
                    If .setUserLevels(DT) Then

                    End If

                End With
                
            End If
            Dim clsUPL As New ETS.BL.UserLevels
            With clsUPL
                .UserID = Request("UserID")
                .getUserLevelDetails()
                SelectedUserLevel = IIf(IsDBNull(.ProductionLevel), 0, .ProductionLevel)
                CanSetSamples = .CanSetSamples
            End With
            clsUPL = Nothing
           Dim clsULim As New ETS.BL.UsersLimits
            With clsULim
                .UserID = Request("UserID")
                DSLimits = .getUserLimits
            End With
            clsULim = Nothing
            Dim clsPL As New ETS.BL.ProductionLevels
            With clsPL
                .ContractorID = IIf(Session("IsContractor") = 0, Session("ParentID").ToString, Session("ContractorID").ToString)
                .Type = Session("IsContractor")
                ._WhereString.Append(" and LevelNo not in(1073741824,5,3)")
                Dim DSPL As DataSet = .getPLevelList
                If DSPL.Tables.Count > 0 Then
                    rptLevels.DataSource = DSPL
                    rptLevels.DataBind()
                Else
                    Response.Write("No Records Found!")
                End If
                DSPL.Dispose()
            End With
            clsPL = Nothing

            Dim clsUser As ETS.BL.Users
            Dim varStrTemp As String = String.Empty
            Try
                clsUser = New ETS.BL.Users(Request.QueryString("UserID").ToString)
                If Not String.IsNullOrEmpty(clsUser.FirstName) Then
                    varStrTemp = clsUser.FirstName
                End If

                If Not String.IsNullOrEmpty(clsUser.LastName) Then
                    varStrTemp = varStrTemp & " " & clsUser.LastName.ToString
                End If

                If Not String.IsNullOrEmpty(clsUser.UserName) Then
                    varStrTemp = varStrTemp & " (" & clsUser.UserName.ToString & ")"
                End If
                uName = "Production Levels for " & varStrTemp.ToString
                Label1.Text = uName.ToString
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                clsUser = Nothing
            End Try
        Catch ex As Exception
            Response.Write(ex.Message)
       
        End Try
    End Sub
    Protected Function getViewLimit(ByVal RowLevel As Long) As Long
        Dim response As Long = 0
        For Each DR As System.Data.DataRow In DSLimits.Tables(0).Rows
            If RowLevel = DR("LevelNo") Then
                response = DR("ViewLimit")
                Exit For
            End If
        Next
        Return response
    End Function
    Protected Function getChkOutLimit(ByVal RowLevel As Long) As Long
        Dim response As Long = 0
        For Each DR As System.Data.DataRow In DSLimits.Tables(0).Rows
            If RowLevel = DR("LevelNo") Then
                response = CLng(DR("CheckOutLimit"))
                Exit For
            End If
        Next
        Return response
    End Function
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("P_LevelAssignments.aspx?CriUser=" & Request("CriUser") & "&CriOption=" & Request("CriOption"), True)
    End Sub
End Class
