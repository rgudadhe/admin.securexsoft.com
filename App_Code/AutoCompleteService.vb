Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports System.Data.SqlClient


<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class AutoCompleteService
     Inherits System.Web.Services.WebService

    <WebMethod()> _
   Public Function GetAutoCompleteData(ByVal accname As String) As List(Of String)
        Dim result As New List(Of String)()
        Dim strcon As String = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        Using con As New SqlConnection(strcon)
            Using cmd As New SqlCommand("Select distinct accname, custid from tbltollfree where accname LIKE '%'+@SearchText+'%'", con)
                con.Open()
                cmd.Parameters.AddWithValue("@SearchText", accname)
                Dim dr As SqlDataReader = cmd.ExecuteReader()
                While dr.Read()
                    result.Add(dr("accname").ToString())
                End While
                Return result
            End Using
        End Using
    End Function


End Class
