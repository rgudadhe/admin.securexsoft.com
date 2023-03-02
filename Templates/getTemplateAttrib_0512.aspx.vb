Namespace ets
    Partial Class Templates_Template
        Inherits System.Web.UI.Page


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim ConString As String
            Dim SQLString As String
            Dim TemplateID As String
            Dim intFileFlag As Integer = 0
            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            Try
                oConn.Open()
                TemplateID = Request("TempID")
                Dim oFol As New IO.DirectoryInfo(Server.MapPath("/ETS_Files/Templates"))
                Dim oFile As New IO.FileInfo(oFol.FullName & "/" & TemplateID & ".doc")
                If oFile.Exists Then
                    intFileFlag = 1
                Else
                    intFileFlag = 0
                End If
                SQLString = "SELECT 'SignOffBlock' as Name, newid() AttributeID union " & _
                "select 'DtTranscribe' as Name, newid() AttributeID union " & _
                "select 'DtTmTranscribe' as Name, newid() AttributeID union " & _
                "select 'DtDictated' as Name, newid() AttributeID union " & _
                "select 'DtTmDictated' as Name, newid() AttributeID union " & _
                "select 'LocDesc' as Name, newid() AttributeID union " & _
                "select 'DictatorName' as Name, newid() AttributeID union " & _
                "select 'eSignature' as Name, newid() AttributeID  union " & _
                "select 'WType' as Name, newid() AttributeID  union " & _
                "select 'CustNum' as Name, newid() AttributeID  union " & _
                "SELECT Name,AttributeID FROM tblAttributes_Default union " & _
                "SELECT A.Name, A.AttributeID FROM tblTemplateAttributes TA INNER JOIN tblAttributes A ON TA.AttributeID = A.AttributeID " & _
                "WHERE TA.TemplateID = '" & TemplateID & "'"
                Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
                oRec.Read()
                If oRec.HasRows Then
                    Dim strResult As String = oRec("AttributeID").ToString & "|" & oRec("Name").ToString
                    Do While oRec.Read
                        strResult = strResult & "^" & oRec("AttributeID").ToString & "|" & oRec("Name").ToString
                    Loop
                    Response.Write(intFileFlag.ToString & "#" & strResult)
                Else
                    Response.Write("0#")
                End If
                oRec.Close()
            Catch ex As Exception
                Response.Write(ex.Message)
            Finally
                If oConn.State <> Data.ConnectionState.Closed Then
                    oConn.Close()
                    oConn = Nothing
                End If
            End Try
        End Sub
    End Class
End Namespace