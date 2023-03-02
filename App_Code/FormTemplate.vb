Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data
Imports System.Data.SqlClient
Imports EncryPass
Imports SASMTPLib
Imports HandleWordControl




<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class FormTemplate
    Inherits System.Web.Services.WebService
    'Public WebAddress As String = System.Configuration.ConfigurationManager.AppSettings("URL")
    Dim WebAddress As String = "https://admin.securexsoft.com"
    'Dim strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    Dim strConn As String = "Server=sqlone;Database=AdminETS;UID=usersqlbkp;Pwd=y0u4@209#"

    <WebMethod()> _
    Public Function getTemplateList() As DataSet
        Try

            Dim DS As New DataSet
            Dim DT As New DataTable
            Dim cmdIns As New SqlCommand("select Distinct T.TemplateID , T.TemplateName  from ADMINETS.dbo.tblTemplates T  WHERE  IsDictTemplate=1 ORDER by T.TemplateName ", New SqlConnection(strConn))
            cmdIns.Connection.Open()
            Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
            DT.Load(DRRec)
            DRRec.Close()
            If cmdIns.Connection.State = ConnectionState.Open Then
                cmdIns.Connection.Close()
            End If
            DS.Tables.Add(DT)
            Return DS

        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    <WebMethod()> _
    Public Function getCustomFields(ByVal TemplateUD As String) As DataSet
        Try

            Dim DS As New DataSet
            Dim DT As New DataTable
            Dim cmdIns As New SqlCommand("select C.AttributeID, C.Name, C.Caption, C.ControlType, C.ControlSize from adminets.dbo.tblCustomAttributes C INNER JOIN adminets.dbo.tblCustomerTemplateAttributes CA ON C.AttributeID = CA.AttributeId WHERE CA.TemplateID='" & TemplateUD & "'  Order By Name", New SqlConnection(strConn))
            cmdIns.Connection.Open()
            Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
            DT.Load(DRRec)
            DRRec.Close()
            If cmdIns.Connection.State = ConnectionState.Open Then
                cmdIns.Connection.Close()
            End If
            DS.Tables.Add(DT)
            Return DS

        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    <WebMethod()> _
    Public Function getPatientFields(ByVal TemplateID As String) As DataSet
        Try

            Dim DS As New DataSet
            Dim DT As New DataTable
            Dim cmdIns As New SqlCommand("select Ta.AttributeID, A.Name, caption   from ADMINETS.dbo.tblTemplates T INNER JOIN adminets.dbo.tblTemplateAttributes TA ON TA.TemplateID = T.TemplateID INNER JOIN (select attributeID, Name, caption from ADMINETS.dbo.tblAttributes UNION select attributeID, Name, caption FROM ADMINETS.dbo.tblAttributes_default) AS A ON A.AttributeID = TA.AttributeID WHERE TA.TemplateID='" & TemplateID & "'  Order By Name", New SqlConnection(strConn))
            cmdIns.Connection.Open()
            Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
            DT.Load(DRRec)
            DRRec.Close()
            If cmdIns.Connection.State = ConnectionState.Open Then
                cmdIns.Connection.Close()
            End If
            DS.Tables.Add(DT)
            Return DS

        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    <WebMethod()> _
    Public Function getSystemFields() As DataSet
        Try
            Dim DS As New DataSet

            Dim DT As New DataTable
            Dim cmdIns As New SqlCommand("select Name from [ADMINETS].[dbo].[tblSystemAttributes] Order By Name ", New SqlConnection(strConn))
            cmdIns.Connection.Open()
            Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
            DT.Load(DRRec)
            DRRec.Close()
            If cmdIns.Connection.State = ConnectionState.Open Then
                cmdIns.Connection.Close()
            End If
            DS.Tables.Add(DT)
            Return DS

        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    <WebMethod()> _
    Public Function getAccounts() As DataSet
        Try
            Dim DS As New DataSet
            'Dim strConn As String

            'strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim DT As New DataTable
            Dim cmdIns As New SqlCommand("select * from adminets.dbo.tblaccounts order by accountname ", New SqlConnection(strConn))
            cmdIns.Connection.Open()
            Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
            DT.Load(DRRec)
            DRRec.Close()
            If cmdIns.Connection.State = ConnectionState.Open Then
                cmdIns.Connection.Close()
            End If
            DS.Tables.Add(DT)
            Return DS

        Catch ex As Exception
            Return Nothing

        End Try
    End Function
    <WebMethod()> _
    Public Function LoginCheck(ByVal xusername As String, ByVal xpassword As String) As Boolean
        Dim oConn As New SqlConnection(strConn)

        Try
            Dim SQLSTR As String
            Dim userpass As String
            Dim EPass As New EncryPass.Encry
            userpass = EPass.encrypt(xusername, xpassword)
            SQLSTR = "select UserID, Password from AdminETS.dbo.tblUsers  WHERE username = '" & xusername & "'"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = New Data.SqlClient.SqlCommand(SQLSTR, oConn)
            Dim myDs As New Data.DataSet
            Adapter.Fill(myDs, "LoginInfo")
            For Each DR As DataRow In myDs.Tables("LoginInfo").Rows
                If DR("Password") = userpass Then
                    Return True
                    ''End If
                    Exit For
                Else
                    Return False
                End If
            Next

        Catch ex As Exception

            Return False
        Finally
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        End Try
    End Function

End Class
