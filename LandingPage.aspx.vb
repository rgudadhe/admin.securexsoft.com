Imports System.Data.Sqlclient
Imports System.Drawing
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.IO
Imports WebChart

Partial Class LandingPage
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

      
            GetSystemAlerts()
            GetTipOftheWeek()
            GetEnhancement()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub GetSystemAlerts()
        Lblupdate.Text = String.Empty
        Dim clsHome3 As ETS.BL.HomePage
        Dim DSUpdates As New Data.DataSet
        Dim DRRec3 As Data.DataTableReader
        Dim updatedate As Date
        Try
            clsHome3 = New ETS.BL.HomePage
            DSUpdates = clsHome3.GetSystemAlertsListByContractorID(Session("ContractorID").ToString)
            If DSUpdates.Tables.Count > 0 Then
                If DSUpdates.Tables(0).Rows.Count > 0 Then
                    DRRec3 = DSUpdates.Tables(0).CreateDataReader
                    If DRRec3.HasRows = True Then
                        Lblupdate.Text = "<table  width='100%'>"
                        While (DRRec3.Read)
                            updatedate = DRRec3("DateUpdated").ToString
                            Lblupdate.Text = Lblupdate.Text & "<TR> <TD class='HeaderDiv' style='text-align:left;'><img src='images/flag.gif'><b><font color='Red'>" & updatedate.ToString("MM-dd-yyyy") & "</font></b></TD></TR><TR><TD>" & DRRec3("Alert").ToString & "</TD></TR>"
                        End While
                        Lblupdate.Text = Lblupdate.Text & "</table>"
                    End If
                    DRRec3.Close()
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsHome3 = Nothing
            DSUpdates = Nothing
            DRRec3 = Nothing
        End Try
    End Sub
    Protected Sub GetTipOftheWeek()
        lblTip.Text = String.Empty
        Dim clsHome3 As ETS.BL.HomePage
        Dim DSUpdates As New Data.DataSet
        Dim DRRec3 As Data.DataTableReader
        Try
            clsHome3 = New ETS.BL.HomePage
            DSUpdates = clsHome3.GetTipOftheWeekListByContractorID(Session("ContractorID").ToString)
            If DSUpdates.Tables.Count > 0 Then

                If DSUpdates.Tables(0).Rows.Count > 0 Then

                    DRRec3 = DSUpdates.Tables(0).CreateDataReader
                    If DRRec3.HasRows = True Then
                        lblTip.Text = "<table  width='100%'>"
                        While (DRRec3.Read)
                            'Response.Write("<img src='images/flag.gif'><b><font color='Red'>" & DRRec3("Tip").ToString & "</font></b><br />" & DRRec3("Description").ToString & "<br />")
                            lblTip.Text = lblTip.Text & "<TR> <TD class='HeaderDiv' style='text-align:left;'> <img src='images/flag.gif'><b><font color='Red'>" & DRRec3("Tip").ToString & "</font></b></TD></TR><TR><TD>" & DRRec3("Description").ToString & "</TD></TR>"
                        End While
                        lblTip.Text = lblTip.Text & "</table>"
                    End If
                    DRRec3.Close()
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsHome3 = Nothing
            DSUpdates = Nothing
            DRRec3 = Nothing
        End Try
    End Sub
    Protected Sub GetEnhancement()
        LblEnhancement.Text = String.Empty
        Dim clsHome3 As ETS.BL.HomePage
        Dim DSUpdates As New Data.DataSet
        Dim DRRec3 As Data.DataTableReader
        Dim ReleaseDate As Date
        Try
            clsHome3 = New ETS.BL.HomePage
            DSUpdates = clsHome3.GetEnhancementListByContractorID(Session("ContractorID").ToString)
            LblEnhancement.Text = "<table  width='100%'> <tr class='tblbg2'><td class='HeaderDiv'> Release Date</td><td class='HeaderDiv'>Enhancement</td><td class='HeaderDiv'>Description</td></tr>"
            If DSUpdates.Tables.Count > 0 Then
                If DSUpdates.Tables(0).Rows.Count > 0 Then
                    DRRec3 = DSUpdates.Tables(0).CreateDataReader
                    If DRRec3.HasRows = True Then
                        While (DRRec3.Read)
                            ReleaseDate = DRRec3("ReleaseDate").ToString
                            LblEnhancement.Text = LblEnhancement.Text & "<TR ><TD><font color='Red'><b>" & ReleaseDate.ToString("MM-dd-yyyy") & "</font></b></TD><TD>" & DRRec3("Enhancement").ToString & "</TD><TD>" & DRRec3("Description").ToString & "</TD></TR>"
                        End While

                    End If
                    DRRec3.Close()
                End If
            End If
            LblEnhancement.Text = LblEnhancement.Text & "</TD>"
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsHome3 = Nothing
            DSUpdates = Nothing
            DRRec3 = Nothing
        End Try
    End Sub

End Class
