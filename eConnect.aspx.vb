Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class testets_eConnect
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ShowData()

    End Sub


    Sub ShowData()

        Dim RecFound As String
        RecFound = "No"
        Dim strConn As String
        Dim inpstr() As String
        Dim HrefPath As String

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd As New SqlCommand("Select filename from tblNewsLetter where contractorid='" & Session("contractorid").ToString & "'  order by eDate DESC", New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows = True Then
                While (DRRec.Read)
                    HrefPath = Server.MapPath("ETs_Files") & "\NewsLetter\" & DRRec("Filename").ToString & ".pdf"
                    If System.IO.File.Exists(HrefPath) Then
                        Label1.Text = Label1.Text & "<ul><li><a href='/ets_files/newsletter/" & DRRec("Filename").ToString & ".pdf'>" & DRRec("Filename").ToString & "</li></ul>"
                    End If

                End While

            End If
            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try
    End Sub
End Class
