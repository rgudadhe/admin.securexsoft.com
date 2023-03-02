Imports system.io
Imports System.Net
Imports Microsoft.Office.Interop
Imports System.Data
Imports System.Data.SqlClient
Partial Class Dictation_Search_ShowPDF
    Inherits System.Web.UI.Page
    Public MediaURL As String
    Public WebAddress As String = System.Configuration.ConfigurationManager.AppSettings("URL")
    Public MediaType As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim Val As String = Request("McID")
            'Response.Write(Val)
            Dim Str() As String = Val.Split("|")
            Dim Version As String = Str(1).Trim
            Dim McID As String = Str(0).Trim
            HMcID.Value = McID
            Dim AccID As String = Str(2).Trim
            HAccID.Value = AccID
            MediaURL = WebAddress & "/ETS_Files/Macros/Rendered/Versions/" & McID & "_" & Version & "Comp.pdf"
            LoadDictators()
            LoadComments()
            'Response.Write(MediaURL)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub LoadDictators()
        Try


            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim cmdIns1 As New SqlCommand("ADMINETS.dbo.SF_getMacrosAssignmentbyMcID", New SqlConnection(strConn))
            'Response.Write("select * from [Secureweb].[dbo].[tblTranscriptionCodingStatus] WHERE LookupId ='" & hdnLookupID.Value & "' and  TranscriptionId ='" & hdnTransID.Value & "' ")
            'Response.Write("rec" & "#" & "#")

            cmdIns1.CommandType = CommandType.StoredProcedure


            cmdIns1.Parameters.AddWithValue("@AccountID", HAccID.Value)
            cmdIns1.Parameters.AddWithValue("@McID", HMcID.Value)
            cmdIns1.Connection.Open()
            Dim DT1 As New DataTable
            Dim DRRec1 As SqlDataReader = cmdIns1.ExecuteReader()
            DT1.Load(DRRec1)

            Repeater1.DataSource = DT1
            Repeater1.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub


    Protected Sub LoadComments()
        Try


            Dim strConn As String
            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim cmdIns1 As New SqlCommand("ADMINETS.dbo.SF_getMacrosCommentsbyMcID", New SqlConnection(strConn))
            'Response.Write("select * from [Secureweb].[dbo].[tblTranscriptionCodingStatus] WHERE LookupId ='" & hdnLookupID.Value & "' and  TranscriptionId ='" & hdnTransID.Value & "' ")
            'Response.Write("rec" & "#" & "#")

            cmdIns1.CommandType = CommandType.StoredProcedure


            cmdIns1.Parameters.AddWithValue("@AccountID", HAccID.Value)
            cmdIns1.Parameters.AddWithValue("@McID", HMcID.Value)
            cmdIns1.Connection.Open()
            Dim DT1 As New DataTable
            Dim DRRec1 As SqlDataReader = cmdIns1.ExecuteReader()
            DT1.Load(DRRec1)
            ' Response.Write(Session("AccID").ToString)
            Repeater2.DataSource = DT1
            Repeater2.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Protected Sub btnProcess_Click(sender As Object, e As System.EventArgs) Handles btnProcess.Click
        Dim strConn As String
        Dim strQuery As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection(strConn)
        Try
            oConn.Open()
            strQuery = "UPDATE adminets.dbo.tblMacroFinalStatus SET Processed=1, ProcessedDate='" & Now & "', ProcessedBy='" & Session("userid").ToString & "' where McID='" & HMcID.Value & "'"
            ' Response.Write(strQuery)

            Dim SQLCmd3 As New SqlCommand(strQuery, oConn)
            SQLCmd3.ExecuteNonQuery()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>")
        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If oConn.State <> ConnectionState.Open Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
       
    End Sub
End Class
