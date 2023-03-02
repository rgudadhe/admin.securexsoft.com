
Partial Class TrainingLogResult
    Inherits System.Web.UI.Page
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
            'Response.Write(ex.Message & "hh")
        End Try
    End Sub
    Protected Sub DBind()
        Dim clsOLT As ETS.BL.OnlineTraining
        Dim Ds As New Data.DataSet
        Dim oRec As Data.DataTableReader
        Try

            clsOLT = New ETS.BL.OnlineTraining

            Dim UserName As String = String.Empty
            Dim StartDate As String = String.Empty
            Dim LastDate As String = String.Empty



            UserName = Request("txtUName")
            StartDate = Request("txtStartDate")
            LastDate = Request("txtEndDate")


            
            Ds = clsOLT.GetTraningLogBySearch(StartDate, LastDate, UserName, Session("WorkGroupID"))
            If Ds.Tables.Count > 0 Then
                If Ds.Tables(0).Rows.Count > 0 Then
                    oRec = Ds.Tables(0).CreateDataReader
                    If oRec.HasRows Then
                        Dim varTblRowMain As New TableRow
                        Dim vartblCellJobNoM As New TableCell
                        Dim vartblCellAccNameM As New TableCell
                        Dim vartblCellDicNameM As New TableCell
                        Dim vartblCellMTQAM As New TableCell
                        Dim vartblCellDtDictationM As New TableCell


                        vartblCellJobNoM.Text = "Job Number"
                        vartblCellJobNoM.HorizontalAlign = HorizontalAlign.Center
                        vartblCellAccNameM.Text = "Account Name"
                        vartblCellAccNameM.HorizontalAlign = HorizontalAlign.Center
                        vartblCellDicNameM.Text = "Dictator Name"
                        vartblCellDicNameM.HorizontalAlign = HorizontalAlign.Center
                        vartblCellMTQAM.Text = "User Name"
                        vartblCellMTQAM.HorizontalAlign = HorizontalAlign.Center
                        vartblCellDtDictationM.Text = "Date Of Training"
                        vartblCellDtDictationM.HorizontalAlign = HorizontalAlign.Center



                        varTblRowMain.Cells.Add(vartblCellJobNoM)
                        varTblRowMain.Cells.Add(vartblCellAccNameM)
                        varTblRowMain.Cells.Add(vartblCellDicNameM)
                        varTblRowMain.Cells.Add(vartblCellMTQAM)
                        varTblRowMain.Cells.Add(vartblCellDtDictationM)

                        varTblRowMain.CssClass = "SMSelected"
                        tblResult.Rows.Add(varTblRowMain)
                        'Response.Write("RowAdded")
                        While oRec.Read
                            Dim varStrAccName As String = String.Empty
                            Dim varStrJobNumber As String = String.Empty
                            Dim varStrPhyName As String = String.Empty
                            Dim varStrMTQA As String = String.Empty
                            Dim varStrDtDictation As String = String.Empty
                            Dim varStrDtFinished As String = String.Empty

                            If Not oRec.IsDBNull(oRec.GetOrdinal("JobNumber")) Then
                                varStrJobNumber = oRec("JobNumber")
                            End If
                            If Not oRec.IsDBNull(oRec.GetOrdinal("AccountName")) Then
                                varStrAccName = oRec("AccountName")
                            End If
                            If Not oRec.IsDBNull(oRec.GetOrdinal("PhyName")) Then
                                varStrPhyName = oRec("PhyName")
                            End If
                            If Not oRec.IsDBNull(oRec.GetOrdinal("UserName")) Then
                                varStrMTQA = oRec("UserName")
                            End If
                            If Not oRec.IsDBNull(oRec.GetOrdinal("DateEdited")) Then
                                varStrDtDictation = oRec("DateEdited")
                            End If


                            Dim varTblRow As New TableRow
                            Dim vartblCellJobNo As New TableCell
                            Dim vartblCellAccName As New TableCell
                            Dim vartblCellPhyName As New TableCell
                            Dim vartblCellMTQA As New TableCell
                            Dim vartblCellDtDictation As New TableCell
                            Dim vartblCellDtFinished As New TableCell

                            vartblCellAccName.Text = varStrAccName
                            vartblCellJobNo.Text = varStrJobNumber
                            vartblCellPhyName.Text = varStrPhyName
                            vartblCellMTQA.Text = varStrMTQA
                            vartblCellDtDictation.Text = varStrDtDictation
                            vartblCellDtFinished.Text = varStrDtFinished

                            varTblRow.Cells.Add(vartblCellJobNo)
                            varTblRow.Cells.Add(vartblCellAccName)
                            varTblRow.Cells.Add(vartblCellPhyName)
                            varTblRow.Cells.Add(vartblCellMTQA)
                            varTblRow.Cells.Add(vartblCellDtDictation)
                            varTblRow.Cells.Add(vartblCellDtFinished)

                            tblResult.Rows.Add(varTblRow)

                        End While
                    End If
                    oRec.Close()
                End If
            End If

            

        Catch ex As Exception
            Response.Write("Error : " & ex.Message)
        Finally
            oRec = Nothing
            Ds = Nothing
            clsOLT = Nothing
        End Try
    End Sub
End Class
