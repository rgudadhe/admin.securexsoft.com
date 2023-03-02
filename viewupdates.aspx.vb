Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class ets_updates
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = ""
        If Not IsPostBack Then
            ShowData(Request("trackid"))
        End If


    End Sub


    Sub ShowData(ByVal trackid As String)

        Dim SQLConn As New SqlConnection
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        SQLConn.ConnectionString = strConn
        Dim RecFound As String
        RecFound = "No"
        ' SQLConn.Open()
        Try
            Dim SQLCmd As New SqlCommand("Select N.*, U.firstname + ' ' + U.LastName as uname   from tblupdates  N LEFT OUTER JOIN TBLUSERS U ON N.USERID = U.USERID where N.trackid = '" & trackid & "' order by DateDisp DESC", SQLConn)
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows = True Then
                While (DRRec.Read)
                    Label1.Text = Label1.Text & "<h1><span style='color: #ff9933; font-size: 10pt;'>" & DRRec("SubText").ToString & "</span></h1><p class='post-by'>Posted By <a href='profile.aspx?userid=" & DRRec("userid").ToString & "' target=_blank>" & DRRec("uname").ToString & "</a></p><p>" & DRRec("Details").ToString & "</p><br />"
                End While
            End If
            DRRec.Close()
            SQLCmd.Connection.Close()
        Finally
            SQLConn.Close()
        End Try

    End Sub

 

    
End Class
