Imports System.Globalization
Partial Class ViewNewsArt
    Inherits BasePage
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        GetData()
    End Sub
    Protected Sub GetData()
        Try
            Dim SubDate1 As String
            Dim SubDate2 As String
            If TxtDate1.Text <> "" And IsDate(TxtDate1.Text) Then
                SubDate1 = TxtDate1.Text
            Else
                SubDate1 = "1/1/2006"
            End If

            If TxtDate2.Text <> "" And IsDate(TxtDate2.Text) Then
                SubDate2 = TxtDate2.Text
            Else
                SubDate2 = Now()
            End If

            If Not String.IsNullOrEmpty(SubDate2) Then
                SubDate2 = DateAdd(DateInterval.Day, 1, CDate(SubDate2))
            End If


            Dim clsNews As New ETS.BL.News
            Dim DS As New Data.DataSet
            DS = clsNews.getNewsList

            Dim DV As Data.DataView = New Data.DataView(DS.Tables(0))
            Dim varStrTemp As String = String.Empty
            varStrTemp = String.Format(New CultureInfo("en-us").DateTimeFormat, "DateDisp >= #{0:d}# and DateDisp <= #{1:d}#", CDate(SubDate1), CDate(SubDate2))
            DV.RowFilter = varStrTemp

            MyDataGrid.DataSource = DV

            MyDataGrid.DataBind()
            clsNews = Nothing

            If MyDataGrid.Rows.Count > 0 Then
                MyDataGrid.ShowFooter = True
                MyDataGrid.UseAccessibleHeader = True
                MyDataGrid.HeaderRow.TableSection = TableRowSection.TableHeader
                MyDataGrid.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub MyDataGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles MyDataGrid.RowCommand
        If Trim(UCase(e.CommandName)) = Trim(UCase("Delete")) Then
            Dim varTrackID As String = String.Empty
            varTrackID = e.CommandArgument.ToString

            Dim clsNews As New ETS.BL.News
            clsNews.trackID = varTrackID
            Dim RetVal As Integer = 0
            RetVal = clsNews.DeleteNewsDetails()
            clsNews = Nothing
            If RetVal = 1 Then
                GetData()
            End If
        End If
    End Sub
    Protected Sub MyDataGrid_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles MyDataGrid.RowDeleting
    End Sub
    Protected Sub MyDataGrid_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles MyDataGrid.Sorting
    End Sub
End Class
