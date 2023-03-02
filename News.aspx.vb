Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class testets_News
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ShowData()
        End If


    End Sub


    Sub ShowData()

        Dim RecFound As String


        RecFound = "No"
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim SQLCmd As New SqlCommand("Select Top 1 * from tblCOO order by DateDisp DESC", New SqlConnection(strConn))
        SQLCmd.Connection.Open()
        Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
        If DRRec.Read = True Then
            Label1.Text = Replace(DRRec("Details").ToString, vbCrLf, "<br>")
            LblDate.Text = FormatDateTime(DRRec("DateDisp").ToString, DateFormat.ShortDate)
            ' Label1.Text = Label1.Text & "<BR>Break"

        End If
        DRRec.Close()
        SQLCmd.Connection.Close()

        Dim SQLCmd1 As New SqlCommand("Select  C.*, U.firstname + ' ' + U.LastName as uname  from tblCOO C LEFT OUTER JOIN TBLUSERS U ON C.USERID = U.USERID order by DateDisp DESC", New SqlConnection(strConn))
        SQLCmd1.Connection.Open()
        Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
        If DRRec1.HasRows Then
            While DRRec1.Read
                Dim LI As New ListItem
                LI.Text = FormatDateTime(DRRec1("DateDisp").ToString, DateFormat.ShortDate)
                LI.Value = DRRec1("TrackID").ToString
                DLDate.Items.Add(LI)
                lblName.Text = "<a href='profile.aspx?userid=" & DRRec1("userid").ToString & "' target=_blank>" & DRRec1("uname").ToString & "</a>"
            End While

        End If
        DRRec1.Close()
        SQLCmd1.Connection.Close()
    End Sub

    Protected Sub DLDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLDate.SelectedIndexChanged
        SelData()
    End Sub

    Sub SelData()

        Dim RecFound As String
        RecFound = "No"
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd As New SqlCommand("Select * from tblCOO where TrackID='" & DLDate.SelectedValue & "' order by DateDisp DESC", New SqlConnection(strConn))
        SQLCmd.Connection.Open()
        Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
        If DRRec.Read = True Then
            Label1.Text = Replace(DRRec("Details").ToString, vbCrLf, "<br>")
            LblDate.Text = FormatDateTime(DRRec("DateDisp").ToString, DateFormat.ShortDate)
            ' Label1.Text = Label1.Text & "<BR>Break"

        End If
        DRRec.Close()
        SQLCmd.Connection.Close()


    End Sub
End Class
