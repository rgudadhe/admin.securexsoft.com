<%@ WebService Language="VB" Class="WebService" %>

Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports AjaxControlToolkit
Imports System.Collections
Imports System.Collections.Generic
<WebService(Namespace:="HI")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<System.Web.Script.Services.ScriptService()> _
Public Class WebService
    Inherits System.Web.Services.WebService
    
    <WebMethod()> _
    Public Function GetDropDownContents(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()

        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCommand As New Data.SqlClient.SqlCommand
        oConn.ConnectionString = ConString
        oConn.Open()
        Dim SQLString As String = "SELECT U.UserID, U.UserName FROM tblUsers AS U INNER JOIN tblUsersLevels AS UL ON U.UserID = UL.UserID"

        oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
        Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader()
        oRec.Read()
        If oRec.HasRows Then
            Dim values As New List(Of CascadingDropDownNameValue)()
            While oRec.Read
                values.Add(New CascadingDropDownNameValue(oRec("UserName").ToString, oRec("UserID").ToString))
            End While
            Return values.ToArray()
        Else
            Return Nothing
        End If
    End Function

End Class
