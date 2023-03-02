Imports System.Data.Sqlclient
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.Security

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim date1 As Date
        If Not IsPostBack Then
            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim clsAcc As ETS.BL.Accounts
            Try
                clsAcc = New ETS.BL.Accounts
                clsAcc.AccountID = Request.QueryString("AccID")
                clsAcc.getAccountDetails()
                lblAct.Text = clsAcc.AccountName
            Catch ex As Exception
            Finally
                clsAcc = Nothing
            End Try

            Dim clsMTU As ETS.BL.MTDirectUserAssignments
            Dim DsUsrInfo As New Data.DataSet
            Dim DRRec2 As Data.DataTableReader
            Dim DS As New Data.DataSet

            Try
                clsMTU = New ETS.BL.MTDirectUserAssignments
                ' DsUsrInfo = clsMTU.GetUsrLstForUsrAssignment(New Guid(Request.QueryString("UserID")))
                If DsUsrInfo.Tables.Count > 0 Then
                    If DsUsrInfo.Tables(0).Rows.Count > 0 Then
                        DRRec2 = DsUsrInfo.Tables(0).CreateDataReader
                        If DRRec2.HasRows Then
                            If DRRec2.Read Then
                                lblusername.Text = DRRec2("firstname").ToString & " " & DRRec2("lastname").ToString
                                lblID.Text = DRRec2("username").ToString
                                lblAddress.Text = DRRec2("Address").ToString
                                lblCity.Text = DRRec2("City").ToString
                                lblState.Text = DRRec2("State").ToString
                                lblChatID.Text = DRRec2("ChatID").ToString
                                lblOfficialMailID.Text = DRRec2("OfficialMailID").ToString
                                lblPhoneNo.Text = DRRec2("PhoneNo").ToString
                                lblCellNo.Text = DRRec2("CellNo").ToString
                                lblMentor.Text = DRRec2("Mentor").ToString
                                If IsDate(DRRec2("datejoined").ToString) Then
                                    date1 = DRRec2("datejoined").ToString
                                    lbljoin.Text = date1.ToShortDateString
                                Else
                                    lbljoin.Text = String.Empty
                                End If
                            End If
                        End If
                    End If
                End If

                DRRec2.Close()

                DS = clsMTU.GetAssignmentRecordsByAccByUsrID(New Guid(Request.QueryString("userid")), New Guid(Request.QueryString("accid")), Session("IntialProductionLevel").ToString, Session("ContractorID"))
                If DS.Tables.Count > 0 Then
                    If DS.Tables(0).Rows.Count > 0 Then
                        GridView1.DataSource = DS.Tables(0)
                        GridView1.DataBind()
                    End If
                End If
            Catch ex As Exception
            Finally
                clsMTU = Nothing
                DRRec2 = Nothing
                DsUsrInfo = Nothing
                DS = Nothing
            End Try
        End If
    End Sub
End Class


