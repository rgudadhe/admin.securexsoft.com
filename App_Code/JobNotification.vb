Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports SXFDBStatus
Imports Microsoft.VisualBasic
Imports log4net
Imports System

<WebService([Namespace]:="com.mmodal.cds.notificationservice")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<SoapDocumentService(RoutingStyle:=SoapServiceRoutingStyle.RequestElement)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class NotificationService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function updateJobStatus(ByVal mmodalJobId As String, ByVal statusCode As Integer, ByVal qualityScore As Single) As Boolean

        Try
            mmodalJobId = mmodalJobId.Replace("\", "-")
            'SW.WriteLine(mmodalJobId & "=>statusCode: " & statusCode & "qualityScore: " & qualityScore)
            Dim clsDA As New DataAccess
            With clsDA
                .Job_ObjID = mmodalJobId
                .JobState = statusCode
                .QualityScore = qualityScore
                Return .UpdateJobState()
            End With
            clsDA = Nothing
        Catch ex As exception
            'Dim SW As IO.StreamWriter = New IO.StreamWriter(IO.Path.Combine(Server.MapPath("ETS_Files").ToString, "JobNotificationLog") & "\" & Now.Month & Now.Day & Now.Year & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".txt", True)

            'Using SW
            '    SW.WriteLine(mmodalJobId & "=>ErrorDesc: " & ex.Message)
            '    SW.Close()
            'End Using

           
            Return False
        Finally
            'If Not SW Is Nothing Then
            '    SW.Dispose()
            'Else
            '    SW.Close()
            '    SW.Dispose()
            'End If
        End Try
    End Function

End Class
