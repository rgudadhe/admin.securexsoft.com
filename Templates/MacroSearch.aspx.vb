Imports System
Imports System.Data
Namespace ets
    Partial Class Macros_TATempSearch
        Inherits BasePage
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            iResponse.Text = ""
            If Not Page.IsPostBack Then
                Dim hdnTemp As HiddenField
                hdnTemp = DirectCast(PreviousPage.FindControl("hdnPhysicianID"), HiddenField)
                If Not hdnTemp Is Nothing Then
                    'Response.Write("ID:" & hdnTemp.Value.ToString)
                    hdnPhyID.Value = hdnTemp.Value.ToString
                End If

                Dim hdnTempName As HiddenField
                hdnTempName = DirectCast(PreviousPage.FindControl("hdnPhysicianName"), HiddenField)
                Dim varstrTemp As String = String.Empty
                If Not hdnTempName Is Nothing Then
                    'Response.Write("Name:" & hdnTempName.Value.ToString)
                    Dim varTempSplit() As String
                    varTempSplit = hdnTempName.Value.ToString.Split("|")

                    varstrTemp = varTempSplit(0)
                End If

                iResponse.Text = ""
                lblPhyName.Text = "Macro assignments for " & varstrTemp & "<a style=""cursor:hand ;"" onMouseover=""tip_it('ToolTip','Physician Names','" & Replace(hdnTempName.Value.ToString, "|", ",") & "'); "" onMouseout=""hideIt('ToolTip');""> more</a>"
            End If
        End Sub




        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            If Not rptBind() Then
                iResponse.Text = "Error Occured"
            End If
        End Sub


        Private Function rptBind() As Boolean
            Dim DSTemp As New DataSet
            Dim clsMacros As New ets.BL.MModalDMAssignments
            With clsMacros
                DSTemp = .GetMacrosByDesc(txtDescription.Text)

            End With
            clsMacros = Nothing
            rptTemp.DataSource = DSTemp
            rptTemp.DataBind()
            rptBind = True
            DSTemp.Dispose()
        End Function


        Protected Sub btnSel_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Try

            
                Dim varTempStr As String = String.Empty
                Dim strTempIDs(rptTemp.Items.Count) As String
                'Response.Write(hdnPhyID.Value)
                Dim DT As New DataTable
                Dim DC1 As New DataColumn("PhysicianID")
                Dim DC2 As New DataColumn("MCID")
                DT.Columns.Add(DC1)
                DT.Columns.Add(DC2)
                Dim splDict() As String
                splDict = Split(hdnPhyID.Value, "|")
                For i As Integer = 0 To UBound(splDict)


                    For Each ctl As RepeaterItem In rptTemp.Items
                        Dim DR As DataRow = DT.NewRow
                        Dim chk As CheckBox = ctl.FindControl("chkSel")
                        If chk.Checked Then
                            Dim hdn As HiddenField = chk.Parent.FindControl("McID")
                            If Not String.IsNullOrEmpty(hdn.Value) Then
                                'If String.IsNullOrEmpty(varTempStr) Then
                                '    varTempStr = Trim(hdn.Value.ToString)
                                'Else
                                '    varTempStr = Trim(varTempStr) & "|" & Trim(hdn.Value.ToString)
                                'End If
                                ''strTempIDs(i) = hdn.Value
                                ''i = +1
                                DR(0) = splDict(i)
                                DR(1) = Trim(hdn.Value.ToString)
                            End If
                            DT.Rows.Add(DR)
                            'If Not String.IsNullOrEmpty(hdn.Value) Then
                            '    strTempIDs(i) = hdn.Value
                            '    i = +1
                            'End If
                        End If
                    Next
                Next
                Dim DS As New DataSet
                DS.Tables.Add(DT)
                'DS.WriteXml("c:\DS.xml")
                Dim obj As New ets.BL.MModalDMAssignments
                ' Response.Write(obj.btnSubmitPhyMacros_click(DT))
                If obj.btnSubmitPhyMacros_click(DT) Then
                    iResponse.Text = "Records submitted successfully."
                Else
                    iResponse.Text = "Error Occured"
                End If
            Catch ex As Exception
                iResponse.Text = ex.Message
            End Try
        End Sub


    End Class
End Namespace