Imports System
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports EncryPass
Imports System.Diagnostics
Imports System.Diagnostics.FileVersionInfo
Imports System.Xml

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class VRSInterface
    Inherits System.Web.Services.WebService
    Dim ofileVersion As FileVersionInfo
    <WebMethod()> _
    Public Function chkLogin(ByVal UserName As String, ByVal Password As String) As DataSet
        Dim ConString As String
        Dim txtUserName As String = String.Empty
        Dim txtPassword As String = String.Empty
        Dim tblName As String
        Dim userpass As String
        Dim EPass As New EncryPass.Encry
        Dim myDs As New Data.DataSet
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            txtUserName = UserName
            txtPassword = Password
            userpass = EPass.encrypt(txtUserName.ToLower, txtPassword)
            oConn.ConnectionString = ConString
            oConn.Open()

            If chkUser(txtUserName, oConn) Then
                tblName = "LoginInfo"

                ofileVersion = FileVersionInfo.GetVersionInfo(Server.MapPath("\MTClient\Setup\MTClient.exe"))

                Dim oCommand As New Data.SqlClient.SqlCommand("MTC_chkLogin", oConn)
                Dim oParam As New Data.SqlClient.SqlParameter("@UserName", Data.SqlDbType.VarChar, 500)
                oParam.Value = txtUserName
                oCommand.Parameters.Add(oParam)
                oParam = New Data.SqlClient.SqlParameter("@UaserPass", Data.SqlDbType.VarChar, 500)
                oParam.Value = userpass
                oCommand.Parameters.Add(oParam)
                oParam = New Data.SqlClient.SqlParameter("@MTCVersion", Data.SqlDbType.VarChar, 500)
                oParam.Value = ofileVersion.FileVersion.ToString
                oCommand.Parameters.Add(oParam)
                oCommand.CommandType = Data.CommandType.StoredProcedure
                Dim Adapter As New Data.SqlClient.SqlDataAdapter
                Adapter.SelectCommand = oCommand
                Adapter.Fill(myDs, tblName)
            Else
                tblName = "UserFailed"
                myDs.Tables.Add(tblName)
                myDs.Tables(0).Columns.Add("clm", GetType(System.String))
                Dim DR As DataRow = myDs.Tables(0).NewRow
                DR(0) = "Please check username"
                myDs.Tables(0).Rows.Add(DR)
                myDs.AcceptChanges()
            End If




            If myDs.Tables.Contains("LoginInfo") = True Then
                If myDs.Tables("LoginInfo").Rows.Count > 0 Then
                    Dim DR As DataRow = myDs.Tables(0).Rows(0)
                    Dim clsU As New ETS.BL.UsersLastLogin
                    clsU._WhereString.Append(" Where userid ='" & DR("UserID").ToString & "' ")
                    Dim DSUsers As System.Data.DataSet = clsU.getUsersLastLogin()
                    If DSUsers.Tables(0).Rows.Count > 0 Then
                        clsU.UpdateLastLogin(DR("UserID").ToString)
                    Else
                        clsU.Lastlogin = Now
                        clsU.UserID = DR("UserID").ToString
                        clsU.InsertUserLastLogin()
                    End If
                    clsU = Nothing
                End If
            End If
            Return myDs
        Catch ex As Exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    Private Function chkUser(ByVal strUser As String, ByVal objCon As Data.SqlClient.SqlConnection) As Boolean
        Try
            Dim oCommand As New Data.SqlClient.SqlCommand("MTC_chkUser", objCon)
            Dim oParam As New Data.SqlClient.SqlParameter("@UserID", Data.SqlDbType.VarChar, 500)
            oParam.Value = strUser
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
            oRec.Read()
            If oRec.HasRows Then
                If CInt(oRec("uCount").ToString) > 0 Then
                    chkUser = True
                Else
                    chkUser = False
                End If
            Else
                chkUser = False
            End If
            oRec.Close()
        Catch ex As Exception
            chkUser = False
        End Try
    End Function
    <WebMethod()> _
    Public Function GetPhyNames(ByVal ActID As String) As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT P.FirstName+' '+P.LastName as PhyName FROM tblPhysicians AS P INNER JOIN tblAccounts AS A ON P.AccountID = A.AccountID where (P.IsDeleted is null or P.IsDeleted=0) and (A.IsDeleted is null or A.IsDeleted=0) and A.AccountID='" & ActID & "' order by P.FirstName+' '+P.LastName", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblPhy")
            Return DS.Tables("tblPhy")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
   Public Function GetContractors() As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("select c.ContractorID,c.ContractorName,c.Description,case LEN(isnull(mc.Con_ObjID,'')) when 0 then 'Register' else 'Remove' end as Status,mc.Con_ObjID,c.ContractorNo as Con_ExtID from tblContractor as C " & _
                                                        "LEFT OUTER join tblmmodal_customers as MC on c.ContractorID=MC.ContractorID where (C.isDeleted is null or C.isDeleted=0)", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblCon")
            Return DS.Tables("tblCon")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function SetContractors(ByVal ContractorID As String, ByVal Con_ObjID As String, ByVal Con_ExtID As String, ISDeleteRequest As Boolean) As Integer
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            Dim qRetVal As Integer
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As Data.SqlClient.SqlCommand
            If ISDeleteRequest Then
                oCommand = New Data.SqlClient.SqlCommand("DELETE FROM tblMModal_Customers  where ContractorID= '" & ContractorID & "'", oConn)
                qRetVal = oCommand.ExecuteNonQuery
            Else
                oCommand = New Data.SqlClient.SqlCommand("UPDATE tblMModal_Customers SET Con_ObjID='" & Con_ObjID & "', Con_ExtID='" & Con_ExtID & "' where ContractorID= '" & ContractorID & "'", oConn)
                qRetVal = oCommand.ExecuteNonQuery
                If qRetVal = 0 Then
                    oCommand = New Data.SqlClient.SqlCommand("INSERT INTO tblMModal_Customers(ContractorID,Con_ObjID,Con_ExtID) " & _
                                                              "VALUES('" & ContractorID & "','" & Con_ObjID & "','" & Con_ExtID & "')", oConn)
                    qRetVal = oCommand.ExecuteNonQuery
                End If
            End If
            
            Return qRetVal


        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
  Public Function SetAccounts(ByVal AccountID As String, ByVal Act_ObjID As String, ByVal Act_ExtID As String, ByVal ContractorID As String, ByVal ISDeleteRequest As Boolean) As Integer
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            Dim qRetVal As Integer
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As Data.SqlClient.SqlCommand
            If ISDeleteRequest Then
                oCommand = New Data.SqlClient.SqlCommand("DELETE FROM tblMModal_Accounts  where AccountID= '" & AccountID & "'", oConn)
                qRetVal = oCommand.ExecuteNonQuery
            Else
                oCommand = New Data.SqlClient.SqlCommand("UPDATE tblMModal_Accounts SET Act_ObjID='" & Act_ObjID & "', Act_ExtID='" & Act_ExtID & "' where AccountID= '" & AccountID & "'", oConn)
                qRetVal = oCommand.ExecuteNonQuery
                If qRetVal = 0 Then
                    oCommand = New Data.SqlClient.SqlCommand("INSERT INTO tblMModal_Accounts(AccountID,Act_ObjID,Act_ExtID,ContractorID) " & _
                                                              "VALUES('" & AccountID & "','" & Act_ObjID & "','" & Act_ExtID & "','" & ContractorID & "')", oConn)
                    qRetVal = oCommand.ExecuteNonQuery
                End If
            End If

            Return qRetVal


        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
 Public Function GetRegContractors() As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("select '' as  ContractorID, '--Select--' as ContractorName union select CONVERT(varchar(36),c.ContractorID) as ContractorID,c.ContractorName from tblContractor as C " & _
                                                        " where (C.isDeleted is null or C.isDeleted=0) Order By ContractorName ", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblCon")
            Return DS.Tables("tblCon")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
 Public Function GetRegAccounts() As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("select '' as  AccountID, '--Select--' as AccountName union select CONVERT(varchar(36),c.AccountID) as AccountID,c.AccountName from tblAccounts as C " & _
                                                        "INNER join tblmmodal_customers as MC on c.AccountID=MC.ContractorID where (C.isDeleted is null or C.isDeleted=0) Order By AccountName", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblCon")
            Return DS.Tables("tblCon")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
 Public Function GetDepartments(ByVal ContractorID As String) As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT mmD.DepartmentID,mmD.ContractorID,case LEN(isnull(mmD.Dep_ObjID,'')) when 0 then 'Register' else 'Remove' end as Status,Dep_ObjID,Dep_ExtID,Dep_Name,mmC.Con_ObjID FROM dbo.tblMModal_Depatments as mmD inner join tblmmodal_customers as mmC on mmD.ContractorID=mmC.ContractorID where mmD.ContractorID='" & ContractorID & "'", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblSpe")
            Return DS.Tables("tblSpe")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
Public Function AddDepartments(ByVal DepartmentID As String, ByVal ContractorID As String, ByVal Dep_ObjID As String, ByVal Dep_ExtID As Integer, ByVal Dep_Name As String) As Integer
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("INSERT INTO dbo.tblMModal_Depatments(DepartmentID,ContractorID,Dep_ObjID,Dep_ExtID,Dep_Name) VALUES('" & DepartmentID & "','" & ContractorID & "','" & Dep_ObjID & "'," & Dep_ExtID & ",'" & Dep_Name & "')", oConn)
            Return oCommand.ExecuteNonQuery()
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function SetDepartment(ByVal ContractorID As String, ByVal DepartmentID As String, ByVal Dep_ObjID As String, ByVal IsDeleteRequest As Boolean) As Integer
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As Data.SqlClient.SqlCommand


            Dim qRetVal As Integer
            If IsDeleteRequest Then
                oCommand = New Data.SqlClient.SqlCommand("DELETE FROM tblMModal_Depatments where ContractorID= '" & ContractorID & "' and DepartmentID='" & DepartmentID & "'", oConn)
                qRetVal = oCommand.ExecuteNonQuery
            Else
                oCommand = New Data.SqlClient.SqlCommand("UPDATE tblMModal_Depatments SET Dep_ObjID='" & Dep_ObjID & "' where ContractorID= '" & ContractorID & "' and DepartmentID='" & DepartmentID & "'", oConn)
                qRetVal = oCommand.ExecuteNonQuery
            End If
            Return qRetVal


        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function GetAuthors(ByVal AccountID As String) As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("select PhysicianID, FirstName,LastName,PINNo,Speciality as WorkSpeciality,A.AccountName, C.ContractorName,mmA.Dep_ObjID,mmA.Aut_ObjID,case LEN(isnull(mmA.Aut_ObjID,'')) when 0 then 'Register' else 'Remove' end as Status,A.AccountID AS ContractorID from tblPhysicians as P inner join tblaccounts as A on P.AccountID=A.AccountID inner join tblContractor as C on A.ContractorID=C.ContractorID left outer join tblMModal_Authors as mmA on p.PhysicianID=mmA.DictatorID where A.AccountID='" & AccountID & "'", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblAuth")
            Return DS.Tables("tblAuth")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function GetAccounts(ByVal ContractorID As String) As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("select A.AccountID, A.AccountName,AccountNo,mmF.Con_ObjID, mmF.Con_ExtID,case LEN(isnull(mmF.Con_ObjID,'')) when 0 then 'Register' else 'Remove' end as Status from tblaccounts as A INNER JOIN tblContractor as C on A.ContractorID=C.ContractorID left outer join tblMModal_Customers as mmF on A.AccountID=mmF.ContractorID where A.ContractorID='" & ContractorID & "' Order By A.AccountName ", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblAccounts")
            Return DS.Tables("tblAccounts")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function GetSXFAccounts() As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("select '' as AccountID,'-- Please Select --' as AccountName union select CONVERT(varchar(36),AccountID) as AccountID,AccountName from tblaccounts where (IsDeleted is null or IsDeleted=0) order by AccountName", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblSXFActs")
            Return DS.Tables("tblSXFActs")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function SetAuthor(ByVal DictatorID As String, ByVal Aut_ObjID As String, ByVal Dep_ObjID As String, ByVal IsDeleteRequest As Boolean) As String
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Dim oCommand As Data.SqlClient.SqlCommand
        Try
            oConn.ConnectionString = ConString
            oConn.Open()



            Dim qRetVal As Integer
            If IsDeleteRequest Then
                oCommand = New Data.SqlClient.SqlCommand("DELETE FROM tblMModal_Authors where DictatorID= '" & DictatorID & "'", oConn)
                qRetVal = oCommand.ExecuteNonQuery
            Else
                oCommand = New Data.SqlClient.SqlCommand("UPDATE tblMModal_Authors SET Dep_ObjID='" & Dep_ObjID & "', Aut_ObjID='" & Aut_ObjID & "' where DictatorID= '" & DictatorID & "'", oConn)
                qRetVal = oCommand.ExecuteNonQuery
                If qRetVal = 0 Then
                    oCommand = New Data.SqlClient.SqlCommand("INSERT INTO tblMModal_Authors(DictatorID,Dep_ObjID,Aut_ObjID) " & _
                                                              "VALUES('" & DictatorID & "','" & Dep_ObjID & "','" & Aut_ObjID & "')", oConn)
                    qRetVal = oCommand.ExecuteNonQuery
                End If
            End If
            Return qRetVal


        Catch ex As Exception
            Return ex.Message & " " & oCommand.CommandText
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function SetAccount(ByVal AccountID As String, ByVal Act_ObjID As String, ByVal Con_ObjID As String, ByVal Act_ExtID As String, ByVal IsDeleteRequest As Boolean) As String
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Dim oCommand As Data.SqlClient.SqlCommand
        Try
            oConn.ConnectionString = ConString
            oConn.Open()



            Dim qRetVal As Integer
            If IsDeleteRequest Then
                oCommand = New Data.SqlClient.SqlCommand("DELETE FROM tblMModal_Accounts where AccountID= '" & AccountID & "'", oConn)
                qRetVal = oCommand.ExecuteNonQuery
            Else
                oCommand = New Data.SqlClient.SqlCommand("UPDATE tblMModal_Accounts SET Con_ObjID='" & Con_ObjID & "', Act_ObjID='" & Act_ObjID & "', Act_ExtID='" & Act_ExtID & "' where AccountID= '" & AccountID & "'", oConn)
                qRetVal = oCommand.ExecuteNonQuery
                If qRetVal = 0 Then
                    oCommand = New Data.SqlClient.SqlCommand("INSERT INTO tblMModal_Accounts(AccountID,Con_ObjID,Act_ObjID,Act_ExtID) " & _
                                                              "VALUES('" & AccountID & "','" & Con_ObjID & "','" & Act_ObjID & "','" & Act_ExtID & "')", oConn)
                    qRetVal = oCommand.ExecuteNonQuery
                End If
            End If
            Return qRetVal


        Catch ex As Exception
            Return ex.Message & " " & oCommand.CommandText
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function getDictatorMacros(ByVal DictatorID As String) As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try

            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT McID,McName,DateModified FROM dbo.tblMModal_Macros where DictatorID='" & DictatorID & "'", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblMacros")
            Return DS.Tables("tblMacros")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
   
    <WebMethod()> _
    Public Function UpdateDictatorMacros(ByVal DictatorID As String, ByVal McID As String, ByVal McName As String, ByVal McData As String) As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        Try

            oConn.ConnectionString = ConString
            oConn.Open()
            thisTransaction = oConn.BeginTransaction()
            Dim oCommand As New Data.SqlClient.SqlCommand("UPDATE dbo.tblMModal_Macros set McName='" & McName & "' where McID='" & McID & "' and DictatorID='" & DictatorID & "'", oConn)
            oCommand.Transaction = thisTransaction
            Dim retVal As Integer = oCommand.ExecuteNonQuery
            If retVal <= 0 Then
                oCommand = New Data.SqlClient.SqlCommand("INSERT INTO dbo.tblMModal_Macros(DictatorID,McID,McName) values('" & DictatorID & "', '" & McID & "', '" & McName & "')", oConn)
                oCommand.Transaction = thisTransaction
                retVal = oCommand.ExecuteNonQuery
            End If
            If retVal > 0 Then
                Dim xmlFile As String = Path.Combine(Server.MapPath("ETS_Files").ToString, "Macros")
                Dim xDoc As XmlDocument = New XmlDocument()
                xDoc.PreserveWhitespace = True
                xDoc.LoadXml(McData)
                xDoc.Save(Path.Combine(xmlFile, McID & ".xml"))
                If File.Exists(Path.Combine(xmlFile, McID & ".xml")) Then
                    thisTransaction.Commit()
                    Return True
                Else
                    thisTransaction.Rollback()
                    Return False
                End If
            End If

        Catch ex As Exception
            Return False
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
   
    <WebMethod()> _
   Public Function DeleteDictatorMacros(ByVal DictatorID As String, ByVal McID As String) As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        Try

            oConn.ConnectionString = ConString
            oConn.Open()
            thisTransaction = oConn.BeginTransaction()
            Dim oCommand As New Data.SqlClient.SqlCommand("DELETE FROM dbo.tblMModal_Macros  where McID='" & McID & "' and DictatorID='" & DictatorID & "'", oConn)
            oCommand.Transaction = thisTransaction
            Dim retVal As Integer = oCommand.ExecuteNonQuery
            
            If retVal > 0 Then
                Dim xmlFile As String = Path.Combine(Server.MapPath("ETS_Files").ToString, "Macros")
                If File.Exists(Path.Combine(xmlFile, McID & ".xml")) Then
                    File.Delete(Path.Combine(xmlFile, McID & ".xml"))
                    thisTransaction.Commit()
                    Return True
                Else
                    thisTransaction.Rollback()
                    Return False
                End If
            Else
                thisTransaction.Rollback()
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function

    <WebMethod()> _
  Public Function getMacros() As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try

            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT McID,McName,Description,DateModified FROM dbo.tblMModal_Macros Order by McName ", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblMacros")
            Return DS.Tables("tblMacros")
        Catch ex As exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function

    <WebMethod()> _
 Public Function getMacrosByDesc(ByVal MacroName As String) As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Try

            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("SELECT McID,McName,Description,DateModified, CASE WHEN Reviewed = 1 THEN 'Yes' ELSE 'No' END AS Reviewed FROM dbo.tblMModal_Macros where description like '" & MacroName & "' Order by McName ", oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblMacros")
            Return DS.Tables("tblMacros")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function

    <WebMethod()> _
 Public Function getMacrosDetailsByDesc(ByVal MacroName As String, ByVal Reviewed As String) As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim DS As New DataSet
        Dim strQuery As String

        Try


            oConn.ConnectionString = ConString
            oConn.Open()
            If Reviewed = "Reviewed" Then
                strQuery = "SELECT McID,McName,Description,DateModified, CASE WHEN Reviewed = 1 THEN 'Yes' ELSE 'No' END AS Reviewed FROM dbo.tblMModal_Macros where Reviewed = 1 and description like '" & MacroName & "' Order by McName "
            ElseIf Reviewed = "Not Reviewed" Then
                strQuery = "SELECT McID,McName,Description,DateModified, CASE WHEN Reviewed = 1 THEN 'Yes' ELSE 'No' END AS Reviewed FROM dbo.tblMModal_Macros where (Reviewed = 0 or Reviewed is null) and description like '" & MacroName & "' Order by McName "
            Else
                strQuery = "SELECT McID,McName,Description,DateModified, CASE WHEN Reviewed = 1 THEN 'Yes' ELSE 'No' END AS Reviewed FROM dbo.tblMModal_Macros where description like '" & MacroName & "' Order by McName "
            End If

            Dim oCommand As New Data.SqlClient.SqlCommand(strQuery, oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(DS, "tblMacros")
            Return DS.Tables("tblMacros")
        Catch ex As Exception
            Return Nothing
        Finally
            DS.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function UpdateMacros(ByVal McID As String, ByVal McName As String, ByVal Description As String, ByVal McData As String) As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        Try

            oConn.ConnectionString = ConString
            oConn.Open()
            thisTransaction = oConn.BeginTransaction()
            Dim oCommand As New Data.SqlClient.SqlCommand("UPDATE dbo.tblMModal_Macros set McName='" & McName & "',Description='" & Description & "', Reviewed =1,DateModified=getDate()  where McID='" & McID & "' ", oConn)
            oCommand.Transaction = thisTransaction
            Dim retVal As Integer = oCommand.ExecuteNonQuery
            If retVal <= 0 Then
                oCommand = New Data.SqlClient.SqlCommand("INSERT INTO dbo.tblMModal_Macros(McID,McName,Description,Reviewed,DateModified) values('" & McID & "', '" & McName & "','" & Description & "',1,getDate())", oConn)
                oCommand.Transaction = thisTransaction
                retVal = oCommand.ExecuteNonQuery
            End If
            If retVal > 0 Then
                Dim xmlFile As String = Path.Combine(Server.MapPath("ETS_Files").ToString, "Macros")
                Dim xDoc As XmlDocument = New XmlDocument()
                xDoc.PreserveWhitespace = True
                xDoc.LoadXml(McData)
                xDoc.Save(Path.Combine(xmlFile, McID & ".xml"))
                If File.Exists(Path.Combine(xmlFile, McID & ".xml")) Then
                    thisTransaction.Commit()
                    Return True
                Else
                    thisTransaction.Rollback()
                    Return False
                End If
            End If
        Catch ex As Exception
            Return False
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function

    <WebMethod()> _
       Public Function DeleteMacros(ByVal McID As String) As Boolean
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        Try

            oConn.ConnectionString = ConString
            oConn.Open()
            thisTransaction = oConn.BeginTransaction()
            Dim oCommand As New Data.SqlClient.SqlCommand("DELETE FROM dbo.tblMModal_Macros  where McID='" & McID & "' ", oConn)
            oCommand.Transaction = thisTransaction
            Dim retVal As Integer = oCommand.ExecuteNonQuery

            If retVal > 0 Then
                Dim xmlFile As String = Path.Combine(Server.MapPath("ETS_Files").ToString, "Macros")
                If File.Exists(Path.Combine(xmlFile, McID & ".xml")) Then
                    File.Delete(Path.Combine(xmlFile, McID & ".xml"))
                    thisTransaction.Commit()
                    Return True
                Else
                    thisTransaction.Rollback()
                    Return False
                End If
            Else
                thisTransaction.Rollback()
                Return False
            End If
        Catch ex As Exception
            Return False
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function

    <WebMethod()> _
    Public Function getXMLDoc(ByVal McID As String) As String
        Try
            Dim xmlFile As String = Path.Combine(Server.MapPath("ETS_Files").ToString, "Macros")
            If File.Exists(Path.Combine(xmlFile, McID & ".xml")) Then
                Dim xDoc As XmlDocument = New XmlDocument()
                xDoc.PreserveWhitespace = True
                xDoc.Load(Path.Combine(xmlFile, McID & ".xml"))
                Return xDoc.OuterXml
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
   Public Function SendMailAlerts(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strBody As String) As String

        Try
            Dim iMail As New SASMTPLib.CoSMTPMail()
            iMail.RemoteHost = "secure.emailsrvr.com"
            iMail.UserName = "alert@edictate.com"
            iMail.Password = "Welcome@medofficepro2011"
            'iMail.Port = 25
            iMail.FromAddress = strFrom
            iMail.AddRecipient(strTo, strTo)
            iMail.ReturnReceipt = False
            iMail.Subject = "SecureXSoft - " & strSubject
            iMail.BodyText = strBody
            iMail.SendMail()
            Return iMail.Response

        Catch ex As Exception
            Return ex.Message
            Exit Function

        End Try

    End Function

    <WebMethod()> _
  Public Function SXFLoginCheck(ByVal xusername As String, ByVal xpassword As String) As DataSet
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        oConn.Open()
        Try
            Dim SQLSTR As String
            Dim userpass As String
            Dim EPass As New EncryPass.Encry
            userpass = EPass.encrypt(xusername, xpassword)
            SQLSTR = "select U.UserID, U.Password, U.First+' '+U.Last as Name, U.username, A.foldername as 'FolderName', A.description, U.AccID, U.EmailAddress, U.AccessLevel, isnull(A.Mode,'S') as Mode,  IsNull(U.LocCode, 0) as loccode, IsNull(U.NRStatus, '') as NRStatus, IsNull(U.NRPeriod, 30) as NRPeriod , isnull((Select Top 1 isnull(DocPassword, 'docpa55') as docpassword from ets.DBO.tblaccdocpass where AccountID = A.AccountID order by effdate desc ),'docpa55') as DocPassword from SecureWeb.dbo.tblUsers U, DBO.tblaccounts A where A.AccountID=U.AccID AND U.IsActive=1 and U.username = '" & xusername & "'"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = New Data.SqlClient.SqlCommand(SQLSTR, oConn)
            Dim myDs As New Data.DataSet
            Adapter.Fill(myDs, "LoginInfo")
            For Each DR As DataRow In myDs.Tables("LoginInfo").Rows
                If DR("Password") = userpass Then
                    Return myDs
                End If
            Next
            Return Nothing
        Catch ex As Exception
            'Dim ds As New DataSet
            'Dim DT As New DataTable
            'Dim DC As New DataColumn("ERror")
            'Dim DR As DataRow = DT.NewRow
            'DT.Columns.Add(DC)
            'DR(0) = ex.Message
            'DT.Rows.Add(DR)
            'ds.Tables.Add(DT)
            Return Nothing
        Finally
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        End Try
    End Function
    '<WebMethod()> _
    'Public Function InsertDictatorMacros(ByVal DictatorID As String, ByVal McID As String, ByVal McName As String) As Integer
    '    Dim oConn As New Data.SqlClient.SqlConnection
    '    Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    '    Try
    '        oConn.ConnectionString = ConString
    '        oConn.Open()
    '        Dim oCommand As New Data.SqlClient.SqlCommand("UPDATE dbo.tblMModal_Macros set McName='" & McName & "' where McID='" & McID & "' and DictatorID='" & DictatorID & "'", oConn)
    '        Dim retVal As Integer = oCommand.ExecuteNonQuery
    '        Return retVal
    '    Catch ex As Exception
    '        Return Nothing
    '    Finally
    '        If oConn.State <> ConnectionState.Closed Then
    '            oConn.Close()
    '            oConn = Nothing
    '        End If
    '    End Try
    'End Function
End Class
