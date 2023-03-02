Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Xml
Imports System.Data
Imports EncryPass
Imports System.Diagnostics
Imports System.Diagnostics.FileVersionInfo
Imports System.Security.Cryptography
Imports System

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class MTClientService
    Inherits System.Web.Services.WebService
    Dim ofileVersion As FileVersionInfo
    Private Function GetChecksum(ByVal file__1 As String) As String
        Using stream As FileStream = File.OpenRead(file__1)
            Dim sha As New SHA256Managed()
            Dim checksum As Byte() = sha.ComputeHash(stream)
            Return BitConverter.ToString(checksum).Replace("-", [String].Empty)
        End Using
    End Function
    <WebMethod()> _
    Public Function getDictationVRS_CS(ByVal strTransID As String, ByVal FileType As String, ByVal IsVRSJob As Boolean) As String
        'Response:
        'Audio File Exist:Response(First Column)=1
        'Audio File Not Exist:Response(First Column)=0

        'Transcription Exist:Response(Second Column)=?1
        'Transcription Not Exist:Response(Second Column)=?0
        Dim Response As String = String.Empty
        Dim ResponseSize As String = String.Empty
        Dim DictPath As String = String.Empty
        Dim TransPath As String = String.Empty
        Try
            DictPath = Path.Combine(Server.MapPath("../ETS_Files").ToString, "Dictations")
            TransPath = Path.Combine(Server.MapPath("../ETS_Files").ToString, "Transcriptions")
            If Directory.Exists(DictPath) Then
                If File.Exists(DictPath & "\" & strTransID & FileType) Then
                    Response = "1"
                    Dim FI As FileInfo = New FileInfo(DictPath & "\" & strTransID & FileType)
                    ResponseSize = GetChecksum(FI.FullName)

                Else
                    Response = "0"
                End If
            Else
                Response = "0"
            End If
            If Directory.Exists(TransPath) Then
                If File.Exists(TransPath & "\" & strTransID & IIf(IsVRSJob, ".xml", ".doc")) Then
                    Response = Response & "1"
                    Dim FI As FileInfo = New FileInfo(TransPath & "\" & strTransID & IIf(IsVRSJob, ".xml", ".doc"))
                    ResponseSize = ResponseSize & "|" & GetChecksum(FI.FullName)

                Else
                    Response = Response & "0"
                End If
            Else
                Response = Response & "0"
            End If
            Return Response & "#" & ResponseSize
        Catch ex As exception
            Return ""
        End Try
    End Function
    <WebMethod()> _
    Public Function MTCJobList(ByVal UserID As String, ByVal Level As Long, ByVal isAuditor As Boolean) As DataSet
        Dim ConString, SQLString As String

        Dim checkedOutQuery As String = String.Empty
        Dim AvailJobQuery As String = String.Empty
        Dim strUserID As String = String.Empty
        Dim intLevel As Long = 1
        strUserID = UserID
        If String.IsNullOrEmpty(strUserID) Then
            Return Nothing
            Exit Function
        End If

        intLevel = Level
        If intLevel = 0 Then
            Return Nothing
            Exit Function
        End If
        Dim myDs As New Data.DataSet
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim SelectClause As String = String.Empty
            Dim oCommand As New Data.SqlClient.SqlCommand
            SQLString = "MTC_JobList"
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@UserID", Data.SqlDbType.VarChar, 36)
            oParam.Value = UserID
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@Level", Data.SqlDbType.Int, 8)
            oParam.Value = Level
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@IsAuditor ", Data.SqlDbType.Bit, 1)
            oParam.Value = IIf(isAuditor, 1, 0)
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            'Return SQLString
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand


            Adapter.Fill(myDs, "TestData")

            Return myDs
            oConn.Close()

        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
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
        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function chkLoginVRS(ByVal UserName As String, ByVal Password As String) As DataSet
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
                ofileVersion = FileVersionInfo.GetVersionInfo(Server.MapPath("\MTClient\Setup\VRS\MTClient.exe"))

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
        Catch ex As exception
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
        Catch ex As exception
            chkUser = False
        End Try
    End Function
    <WebMethod()> _
    Public Function getLevelsbyUserID(ByVal UserID As String) As DataSet
        Dim ConString As String
        Dim txtUserName As String = String.Empty
        Dim txtPassword As String = String.Empty
        Dim myDs As New Data.DataSet
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim oCommand As New Data.SqlClient.SqlCommand("MTC_getLevelsByUserID", oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@UserID", Data.SqlDbType.VarChar, 38)
            oParam.Value = UserID
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(myDs, "LevelsInfo")
            Return myDs
        Catch ex As exception
            Return Nothing
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
            myDs.Dispose()
        End Try
    End Function
    <WebMethod()> _
    Public Function getAttributeOptions(ByVal attributeID As String) As DataTable
        Dim ConString, SQLString As String
        Dim txtUserName As String = String.Empty
        Dim txtPassword As String = String.Empty
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim myDT As New Data.DataTable("Options")
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "select ROW_NUMBER() over(order by sequence) as ID,optionvalue from tblAttributeOptions where attributeID='" & attributeID & "'"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Adapter.SelectCommand = oCommand
            Adapter.Fill(myDT)
            Return myDT
        Catch ex As exception
            Return Nothing
        Finally
            myDT.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function getExtended(ByVal strTransID As String, ByVal strTempID As String) As DataSet
        Dim ConString, SQLString As String
        Dim txtUserName As String = String.Empty
        Dim txtPassword As String = String.Empty
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim myDs As New Data.DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "MTC_getExtended"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@TransID", Data.SqlDbType.VarChar, 36)
            oParam.Value = strTransID
            oCommand.Parameters.Add(oParam)
            oParam = New Data.SqlClient.SqlParameter("@TempID", Data.SqlDbType.VarChar, 36)
            oParam.Value = strTempID
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Adapter.SelectCommand = oCommand
            Adapter.Fill(myDs, "DictationAttributes")
            Return myDs
        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
     Public Function getDictation(ByVal strTransID As String, ByVal FileType As String, ByVal IsVRSJob As Boolean) As String
        'Response:
        'Audio File Exist:Response(First Column)=1
        'Audio File Not Exist:Response(First Column)=0

        'Transcription Exist:Response(Second Column)=?1
        'Transcription Not Exist:Response(Second Column)=?0
        Dim Response As String = String.Empty
        Dim DictPath As String = String.Empty
        Dim TransPath As String = String.Empty
        Try
            DictPath = Path.Combine(Server.MapPath("../ETS_Files").ToString, "Dictations")
            TransPath = Path.Combine(Server.MapPath("../ETS_Files").ToString, "Transcriptions")
            If Directory.Exists(DictPath) Then
                If File.Exists(DictPath & "\" & strTransID & FileType) Then
                    Response = "1"
                Else
                    Response = "0"
                End If
            Else
                Response = "0"
            End If
            If Directory.Exists(TransPath) Then
                If File.Exists(TransPath & "\" & strTransID & IIf(IsVRSJob, ".xml", ".doc")) Then
                    Response = Response & "1"
                Else
                    Response = Response & "0"
                End If
            Else
                Response = Response & "0"
            End If
            Return Response
        Catch ex As exception
            Return ""
        End Try
    End Function
    <WebMethod()> _
       Public Function getDictationVRS(ByVal strTransID As String, ByVal FileType As String, ByVal IsVRSJob As Boolean) As String
        'Response:
        'Audio File Exist:Response(First Column)=1
        'Audio File Not Exist:Response(First Column)=0

        'Transcription Exist:Response(Second Column)=?1
        'Transcription Not Exist:Response(Second Column)=?0
        Dim Response As String = String.Empty
        Dim ResponseSize As String = String.Empty
        Dim DictPath As String = String.Empty
        Dim TransPath As String = String.Empty
        Try
            DictPath = Path.Combine(Server.MapPath("../ETS_Files").ToString, "Dictations")
            TransPath = Path.Combine(Server.MapPath("../ETS_Files").ToString, "Transcriptions")
            If Directory.Exists(DictPath) Then
                If File.Exists(DictPath & "\" & strTransID & FileType) Then
                    Response = "1"
                    Dim FI As FileInfo = New FileInfo(DictPath & "\" & strTransID & FileType)
                    ResponseSize = FI.Length

                Else
                    Response = "0"
                End If
            Else
                Response = "0"
            End If
            If Directory.Exists(TransPath) Then
                If File.Exists(TransPath & "\" & strTransID & IIf(IsVRSJob, ".xml", ".doc")) Then
                    Response = Response & "1"
                    Dim FI As FileInfo = New FileInfo(TransPath & "\" & strTransID & IIf(IsVRSJob, ".xml", ".doc"))
                    ResponseSize = ResponseSize & "|" & FI.Length

                Else
                    Response = Response & "0"
                End If
            Else
                Response = Response & "0"
            End If
            Return Response & "#" & ResponseSize
        Catch ex As exception
            Return ""
        End Try
    End Function
    <WebMethod()> _
    Public Function CheckOutAuditDictation(ByVal TransID As String, ByVal UserID As String, ByVal Level As Long) As Boolean
        Dim clsDic As New ETS.BL.Dictations
        With clsDic
            If Not .IsAuditJobAlreadyCheckedOut(UserID, TransID) Then
                CheckOutAuditDictation = .SetAuditRecordStatus(TransID, UserID)
            Else
                CheckOutAuditDictation = True
            End If
        End With
        clsDic = Nothing
    End Function
    <WebMethod()> _
    Public Function CheckOutDictation(ByVal TransID As String, ByVal UserID As String, ByVal Level As Long) As Boolean
        Dim clsDic As New ETS.BL.Dictations
        With clsDic
            If Not .IsJobAlreadyCheckedOut(UserID, TransID, Level + 100) Then
                CheckOutDictation = .AssignDictations(UserID, Level + 100, "", True, Me.Context.Request.UserHostAddress, TransID, Level)
            Else
                CheckOutDictation = True
            End If
        End With
        clsDic = Nothing
    End Function
    <WebMethod()> _
    Public Function getDictationInfo(ByVal PhyID As String, ByVal UserID As String, ByVal Level As Long) As DataSet
        Dim ConString, SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim myDs As New Data.DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "MTC_getDictationInfo"
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@PhysicianID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(PhyID)
            oCommand.Parameters.Add(oParam)
            oParam = New Data.SqlClient.SqlParameter("@UserID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(UserID)
            oCommand.Parameters.Add(oParam)
            oParam = New Data.SqlClient.SqlParameter("@Level", Data.SqlDbType.Int, 8)
            oParam.Value = Level
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = oCommand
            Adapter.Fill(myDs, "DictationInfo")
            Return myDs
        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function AuditTemplates(ByVal TemplateID As String, ByVal UserID As String, ByVal IsTemplateMod As Boolean) As Integer
        Dim ConString As String
        Dim SQLString As String
        Dim RecAffected As Integer
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection

        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "INSERT INTO tblAudit_Templates(UserID,TemplateID,IsTemplateMod,DateModified) VALUES('" & UserID & "','" & TemplateID & "','" & IsTemplateMod & "','" & Now() & "')"
            Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            RecAffected = oCommand.ExecuteNonQuery
            oConn.Close()
            AuditTemplates = RecAffected
        Catch ex As exception
            Return 0
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function getInstructions(ByVal AccID As String) As DataSet
        Dim myds As New DataSet
        Try
            Dim clsAI As New ETS.BL.AccountInstructions
            With clsAI
                .AccountID = AccID
                ._WhereString.Append(" and (IsDeleted=0 or IsDeleted is null)")
                myds = .getAIInfo()
            End With
            clsAI = Nothing
            Return myds
        Catch ex As exception
            Return Nothing
        Finally
            myds.Dispose()
        End Try
    End Function
    <WebMethod()> _
    Public Function getPhysicianTemplatesVRS(ByVal PhyID As String) As DataSet
        Dim ConString As String
        Dim SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim myDs As New Data.DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "MTC_getPhysicianTemplatesVRS"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@PhyID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(PhyID)
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Adapter.SelectCommand = oCommand
            Adapter.Fill(myDs, "TemplateInfo")
            Return myDs
        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function getPhysicianTemplates(ByVal PhyID As String) As DataSet
        Dim ConString As String
        Dim SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim myDs As New Data.DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "MTC_getPhysicianTemplates"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@PhyID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(PhyID)
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Adapter.SelectCommand = oCommand
            Adapter.Fill(myDs, "TemplateInfo")
            Return myDs
        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
       Public Function getTemplateAttributes(ByVal TempID As String) As DataSet
        Dim ConString As String
        Dim SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Dim myDs As New Data.DataSet
        Try
            oConn.Open()
            SQLString = "MTC_getTemplateAttributes"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@TempID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(TempID)
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Adapter.SelectCommand = oCommand
            Adapter.Fill(myDs, "DictationAttributes")
            Return myDs
        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    Private Function getDemoAccountDetails(ByVal DemoAccName As String, ByVal AccountID As String) As Boolean
        Try
            Dim clsAct As New ETS.BL.Accounts
            With clsAct
                .AccountID = AccountID
                .getAccountDetails()
                If Not IsDBNull(.MapDemoAccID) Then
                    AccountID = .MapDemoAccID
                    ._WhereString.Append("")
                    .AccountID = AccountID
                    .getAccountDetails()
                    DemoAccName = .AccountName
                End If
            End With
            clsAct = Nothing
            Return True
        Catch ex As exception
            Return False
        End Try
    End Function
    <WebMethod()> _
    Public Function DemoLookUp(ByVal DemoAccName As String, ByVal AccountID As String, ByVal SDate As String, ByVal eDate As String, ByVal PFName As String, ByVal PLName As String, ByVal MEDRN As String, ByVal APName As String) As DataSet

        Dim DisplayFiels As String = String.Empty
        Dim myDs As New Data.DataSet
        Try
            getDemoAccountDetails(DemoAccName, AccountID)
            Dim clsDemo As New ETS.BL.Demographics
            With clsDemo
                DisplayFiels = .getAcctDemoFields(AccountID)
                DisplayFiels = DisplayFiels.Replace("DtOfServ", "cast(dtofserv as datetime) as DtOfServ")
                DisplayFiels = DisplayFiels.Replace("PDOB", "cast(PDOB as datetime) as PDOB")
                If Not String.IsNullOrEmpty(DisplayFiels.ToString) Then
                    myDs = .getAcctDemos(DisplayFiels.ToString, DemoAccName, SDate, eDate, PFName, PLName, MEDRN, APName)
                End If
            End With
            clsDemo = Nothing
            Return myDs
        Catch ex As exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Function getClientComments(ByVal TransID As String) As DataSet
        Dim ConString As String
        Dim SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Dim myDs As New Data.DataSet
        Try
            oConn.Open()
            SQLString = "SF_getClientComments"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@TransID", Data.SqlDbType.VarChar, 36)
            oParam.Value = TransID
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Adapter.SelectCommand = oCommand
            Adapter.Fill(myDs, "ClientsComments")
            Return myDs
        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
   Public Function getPhySamples(ByVal PhyID As String, ByVal sName As String, ByVal sKeyword As String) As DataSet

        Dim myDs As New Data.DataSet
        Try
            Dim clsSamples As New ETS.BL.Samples
            With clsSamples
                .PhyID = PhyID
                If Not String.IsNullOrEmpty(sName) Then
                    ._WhereString.Append(" and Name like '" & sName & "'")
                End If
                If Not String.IsNullOrEmpty(sKeyword) Then
                    ._WhereString.Append(" and KeyWords like '" & sKeyword & "'")
                End If
                myDs = .getSampleList()
            End With
            clsSamples = Nothing
            Return myDs
        Catch ex As exception
            Return Nothing

        End Try
    End Function
    <WebMethod()> _
   Public Function getRefPhy(ByVal AccID As String, ByVal WhereClause As String) As DataSet
        Dim MapAccID As String = String.Empty
        Dim myDs As New Data.DataSet
        Try
            Dim clsAct As New ETS.BL.Accounts
            With clsAct
                .AccountID = AccID
                .getAccountDetails()
                If Not IsDBNull(.MapRefAccID) Then
                    MapAccID = IIf(IsDBNull(.MapRefAccID), String.Empty, .MapRefAccID)
                End If
            End With
            clsAct = Nothing

            Dim clsRP As New ETS.BL.RefPhysician
            With clsRP
                .AccID = IIf(String.IsNullOrEmpty(MapAccID), AccID, MapAccID)
                ._WhereString.Append(" AND " & WhereClause)
                myDs = .getRPList()
            End With
            clsRP = Nothing
            Dim TempDS As New DataSet
            Dim clsAB As New ETS.BL.AddressBlock
            With clsAB
                TempDS = .getAccountsAB(AccID)
            End With
            clsAB = Nothing
            Dim DT As New DataTable
            DT.TableName = "addBlock"
            DT = TempDS.Tables(0).Clone
            TempDS.Dispose()
            myDs.Tables.Add(DT)
            Return myDs

        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
        End Try
    End Function
    <WebMethod()> _
    Public Function getComments(ByVal TransID As String) As DataSet
        Dim ConString As String
        Dim SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Dim myDs As New Data.DataSet
        Try
            oConn.Open()
            SQLString = "SF_getComments"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@TransID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(TransID)
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Adapter.SelectCommand = oCommand

            Adapter.Fill(myDs, "Comments")
            Return myDs
        Catch ex As exception
            Return Nothing
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
            myDs.Dispose()
        End Try
    End Function
    <WebMethod()> _
        Public Function CheckInAuditDictation(ByVal UserID As String, ByVal TransID As String, ByVal DS As DataSet)
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        Dim SQLString As String = String.Empty
        Try
            Dim ErrCri, ErrMaj, ErrMin, ErrPMI, ErrTemp As Integer
            Dim TransComments As String = String.Empty
            For Each row As DataRow In DS.Tables("DictationDetails").Rows
                For Each col As DataColumn In DS.Tables("DictationDetails").Columns
                    Select Case col.ColumnName.ToString
                        Case "TransComments"
                            TransComments = row(col.ColumnName.ToString)
                        Case "ErrCri"
                            ErrCri = row(col.ColumnName.ToString)
                        Case "ErrMaj"
                            ErrMaj = row(col.ColumnName.ToString)
                        Case "ErrMin"
                            ErrMin = row(col.ColumnName.ToString)
                        Case "ErrPMI"
                            ErrPMI = row(col.ColumnName.ToString)
                        Case "ErrTemp"
                            ErrTemp = row(col.ColumnName.ToString)
                    End Select
                Next
            Next
            If Not Directory.Exists(Server.MapPath("../ETS_Files/AuditData")) Then
                Directory.CreateDirectory(Server.MapPath("../ETS_Files/AuditData"))
            End If
            Dim TempFile As String = Server.MapPath("../ETS_Files/Temp") & "/" & TransID & ".doc"
            Dim TransFile As String = Server.MapPath("../ETS_Files/AuditData") & "/" & TransID & ".doc"

            If Not Len(UserID) = 36 Then
                Return "You are not authorised to CheckIn dictation!"
                Exit Function
            End If
            If Not Len(TransID) = 36 Then
                Return "Please, refresh list and try to send dictation again!"
                Exit Function
            End If
            If Not File.Exists(TempFile) Then
                Return "Transcription not recieved!"
                Exit Function
            End If

            oConn = OpenConn()
            If Not oConn.State = ConnectionState.Open Then
                Return "Connection with server failed"
                Exit Function
            End If
            thisTransaction = oConn.BeginTransaction()
            Dim oCommand As New Data.SqlClient.SqlCommand
            SQLString = "SF_setAuditRecordStatus"
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@Status", Data.SqlDbType.Int)
            oParam.Value = 200
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@CurrentStatus", Data.SqlDbType.Int)
            oParam.Value = 100
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@TranscriptionID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(TransID)
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@UserID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(UserID)
            oCommand.Parameters.Add(oParam)

            oCommand.CommandType = Data.CommandType.StoredProcedure
            oCommand.Transaction = thisTransaction
            If oCommand.ExecuteNonQuery > 0 Then
                If UpdateAuditEPTL(TransID, UserID, ErrCri, ErrMaj, ErrMin, ErrPMI, ErrTemp, TransComments, oConn, thisTransaction) Then
                    If IO.File.Exists(TempFile) Then
                        IO.File.Copy(TempFile, TransFile, True)
                        If IO.File.Exists(TransFile) Then
                            IO.File.Delete(TempFile)
                            thisTransaction.Commit()
                            Return "Success"
                        Else
                            thisTransaction.Rollback()
                            Return "Error while saving Transcription on server"
                        End If
                    Else
                        thisTransaction.Rollback()
                        Return "Transcription Not found"
                    End If
                Else
                    thisTransaction.Rollback()
                    Return "Error while saving EPTL"
                End If
            Else
                thisTransaction.Rollback()
                Return "You are not authorised to check in this dictation."
            End If
        Catch ex As exception
            thisTransaction.Rollback()
            Return "Error occured while check in dictation."
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    Private Function UpdateAuditEPTL(ByVal TransID As String, ByVal UserID As String, ByVal ErrCri As Long, ByVal ErrMaj As Long, ByVal ErrMin As Long, ByVal ErrPMI As Long, ByVal ErrTemp As Long, ByVal Comment As String, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As Boolean
        Try
            Dim SQLString As String = String.Empty
            Dim oCommand As New Data.SqlClient.SqlCommand
            SQLString = "SF_UpdateAuditEPTL"
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@TransID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(TransID)
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@UserID", Data.SqlDbType.VarChar, 36)
            oParam.Value = New Guid(UserID)
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@ErrCri", Data.SqlDbType.Int, 8)
            oParam.Value = ErrCri
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@ErrMaj", Data.SqlDbType.Int, 8)
            oParam.Value = ErrMaj
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@ErrMin", Data.SqlDbType.Int, 8)
            oParam.Value = ErrMin
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@ErrPMI", Data.SqlDbType.Int, 8)
            oParam.Value = ErrPMI
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@ErrTemp", Data.SqlDbType.Int, 8)
            oParam.Value = ErrTemp
            oCommand.Parameters.Add(oParam)

            oParam = New Data.SqlClient.SqlParameter("@Comment", Data.SqlDbType.VarChar, 255)
            oParam.Value = Comment
            oCommand.Parameters.Add(oParam)

            oCommand.CommandType = Data.CommandType.StoredProcedure

            oCommand.Transaction = ThisTransaction
            If oCommand.ExecuteNonQuery() > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As exception
            Return False
        End Try
    End Function
    <WebMethod()> _
        Public Function CheckInDictation(ByVal UserID As String, ByVal TransID As String, ByVal DS As DataSet)
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        Dim SQLString As String = String.Empty
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            Dim CheckInLevel, UserLevel As Long
            Dim LineCount As String = String.Empty
            Dim TemplateID As String = String.Empty
            Dim ISFaxPlus As Boolean
            Dim TransComments As String = String.Empty
            Dim CSComments As String = String.Empty
            Dim PhysicianID As String = String.Empty
            Dim AccountID As String = String.Empty
            Dim ErrCri, ErrMaj, ErrMin, ErrPMI, ErrTemp, Version, Location As Integer
            Dim CanSetSamples, CanMarkErrors, SetAsSample, ForEvaluation As Boolean
            For Each row As DataRow In DS.Tables("DictationDetails").Rows
                For Each col As DataColumn In DS.Tables("DictationDetails").Columns
                    Select Case col.ColumnName.ToString
                        Case "ISFaxPlus"
                            ISFaxPlus = row(col.ColumnName.ToString)
                        Case "TemplateID"
                            TemplateID = row(col.ColumnName.ToString)
                        Case "CheckInLevel"
                            CheckInLevel = row(col.ColumnName.ToString)
                        Case "TransComments"
                            TransComments = row(col.ColumnName.ToString)
                        Case "CSComments"
                            CSComments = row(col.ColumnName.ToString)
                        Case "CanMarkErrors"
                            CanMarkErrors = row(col.ColumnName.ToString)
                        Case "ErrCri"
                            ErrCri = row(col.ColumnName.ToString)
                        Case "ErrMaj"
                            ErrMaj = row(col.ColumnName.ToString)
                        Case "ErrMin"
                            ErrMin = row(col.ColumnName.ToString)
                        Case "ErrPMI"
                            ErrPMI = row(col.ColumnName.ToString)
                        Case "ErrTemp"
                            ErrTemp = row(col.ColumnName.ToString)
                        Case "CanSetSamples"
                            CanSetSamples = row(col.ColumnName.ToString)
                        Case "SetAsSample"
                            SetAsSample = row(col.ColumnName.ToString)
                        Case "ForEvaluation"
                            ForEvaluation = IIf(CanMarkErrors, False, row(col.ColumnName.ToString))
                        Case "LineCount"
                            LineCount = row(col.ColumnName.ToString)
                        Case "Version"
                            Version = row(col.ColumnName.ToString) + 1
                        Case "UserLevel"
                            UserLevel = row(col.ColumnName.ToString)
                        Case "Location"
                            Location = row(col.ColumnName.ToString)
                        Case "AccountID"
                            AccountID = row(col.ColumnName.ToString)
                        Case "PhysicianID"
                            PhysicianID = row(col.ColumnName.ToString)
                    End Select
                Next
            Next
            Dim TempFile As String = Server.MapPath("../ETS_Files/Temp") & "/" & TransID & ".doc"
            Dim TransFile As String = Server.MapPath("../ETS_Files/Transcriptions") & "/" & TransID & ".doc"
            Dim VersionFile As String = Server.MapPath("../ETS_Files/Transcriptions") & "/" & TransID & ".doc." & Version
            Dim LCFile As String = Server.MapPath("../ETS_Files/LineCounter") & "/" & TransID & ".doc"
            If Not Len(UserID) = 36 Then
                Return "You are not authorised to CheckIn dictation!"
                Exit Function
            End If
            If Not Len(TransID) = 36 Then
                Return "Please, refresh list and try to send dictation again!"
                Exit Function
            End If
            If Not CheckInLevel > 0 Then
                Return "Please, Edit and re-send dictation!"
                Exit Function
            End If
            If Not File.Exists(TempFile) Then
                Return "Transcription not recieved!"
                Exit Function
            End If

            oConn = OpenConn()
            If Not oConn.State = ConnectionState.Open Then
                Return "Connection with server failed"
                Exit Function
            End If
            thisTransaction = oConn.BeginTransaction()
            If CheckInLevel = 1073741824 Then
                Dim FRLevel As Long = chkForceRouting(UserID, AccountID, TemplateID, PhysicianID, UserLevel, CheckInLevel, thisTransaction, oConn)
                If Not FRLevel = 0 Then
                    CheckInLevel = FRLevel
                Else
                    thisTransaction.Rollback()
                    Return "ForceRouting"
                    Exit Function
                End If
            End If

            'Dim tempRes As String
            'Dim clsDic As New ETS.BL.Dictations
            'With clsDic
            '    tempRes = .UpdateTranscriptionLog(UserID, CheckInLevel, "", HttpContext.Current.Request.UserHostAddress, UserLevel + 100, TransID, LineCount, Version, TemplateID, thisTransaction, oConn)
            'End With
            'clsDic = Nothing
            'thisTransaction.Rollback()
            'Return tempRes

            If UpdateTranscriptionStatus(UserID, TransID, CheckInLevel, TemplateID, UserLevel, Version, Location, LineCount, oConn, thisTransaction) Then
                If UpdateTranscriptionAttributes(TransID, DS.Tables("DictationAttributes"), oConn, thisTransaction) Then
                    If setSample(SetAsSample, TransID, UserID, oConn, thisTransaction) Then
                        If IIf(ISFaxPlus, UpdateFaxPlusInfo(DS.Tables("FaxPlusInfo"), UserID, TransID, oConn, thisTransaction), True) Then
                            If UpdateTranscriptionComments(TransID, UserID, CheckInLevel, Version, TransComments, CSComments, oConn, thisTransaction) Then
                                If UpdateClientsComments(TransID, UserID, DS.Tables("ClientsComments"), oConn, thisTransaction) Then
                                    If UpdateEPTL(CanMarkErrors, TransID, UserID, UserLevel, CheckInLevel, ErrCri, ErrMaj, ErrMin, ErrPMI, ErrTemp, oConn, thisTransaction) Then
                                        If UpdateEvaluationRequest(TransID, UserID, UserLevel, CheckInLevel, ForEvaluation, oConn, thisTransaction) Then
                                            If IO.File.Exists(TempFile) Then
                                                IO.File.Copy(TempFile, TransFile, True)
                                                If IO.File.Exists(TransFile) Then
                                                    IO.File.Copy(TempFile, VersionFile)
                                                    IO.File.Copy(TempFile, LCFile)
                                                    IO.File.Delete(TempFile)
                                                    thisTransaction.Commit()
                                                    Return "Success"
                                                Else
                                                    thisTransaction.Rollback()
                                                    Return "Error while saving Transcription on server"
                                                End If
                                            Else
                                                thisTransaction.Rollback()
                                                Return "Transcription Not found"
                                            End If

                                        Else
                                            thisTransaction.Rollback()
                                            Return "Evaluation"
                                        End If
                                    Else
                                        thisTransaction.Rollback()
                                        Return "EPTL"
                                    End If
                                Else
                                    thisTransaction.Rollback()
                                    Return "ClientsComments"
                                End If
                            Else
                                thisTransaction.Rollback()
                                Return "TransComments"
                            End If
                        Else
                            thisTransaction.Rollback()
                            Return "FaxPlus"
                        End If
                    Else
                        thisTransaction.Rollback()
                        Return "Sample"
                    End If
                Else
                    thisTransaction.Rollback()
                    Return "UpdateTranscriptionAttributes"
                End If
            Else
                thisTransaction.Rollback()
                Return "UpdateTranscriptionStatus"
            End If
            oConn.Close()
        Catch ex As exception
            If Not thisTransaction Is Nothing Then
                thisTransaction.Rollback()
                Return ex.Message
            End If
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function

    Private Function chkForceRouting(ByVal UserID As String, ByVal AccountID As String, ByVal TemplateID As String, ByVal PhysicianID As String, ByVal UserLevel As Long, ByVal CheckInLevel As Long, ByVal ThisTransaction As Data.SqlClient.SqlTransaction, ByVal oConn As Data.SqlClient.SqlConnection) As Long
        Dim FRLevel As Long
        Dim oRec As SqlClient.SqlDataReader
        Dim oCommand As New Data.SqlClient.SqlCommand
        Dim SQLString As String = "SF_getForceRoutingLevel"

        oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
        Dim oParam As New Data.SqlClient.SqlParameter("@AccountID", Data.SqlDbType.UniqueIdentifier)
        oParam.Value = New Guid(AccountID)
        oCommand.Parameters.Add(oParam)

        oParam = New Data.SqlClient.SqlParameter("@PhysicianID", Data.SqlDbType.UniqueIdentifier)
        oParam.Value = New Guid(PhysicianID)
        oCommand.Parameters.Add(oParam)

        oParam = New Data.SqlClient.SqlParameter("@TemplateID", Data.SqlDbType.UniqueIdentifier)
        oParam.Value = New Guid(TemplateID)
        oCommand.Parameters.Add(oParam)

        oParam = New Data.SqlClient.SqlParameter("@UserID", Data.SqlDbType.UniqueIdentifier)
        oParam.Value = New Guid(UserID)
        oCommand.Parameters.Add(oParam)

        oParam = New Data.SqlClient.SqlParameter("@UserLevel", Data.SqlDbType.Int)
        oParam.Value = UserLevel
        oCommand.Parameters.Add(oParam)
        oCommand.CommandType = Data.CommandType.StoredProcedure
        oCommand.Transaction = ThisTransaction
        Try
            oRec = oCommand.ExecuteReader
            oRec.Read()
            If oRec.HasRows Then
                If Not IsDBNull(oRec("LevelNo")) Then
                    FRLevel = CLng(oRec("LevelNo"))
                    oRec.Close()
                    Return FRLevel
                Else
                    oRec.Close()
                    Return CheckInLevel
                End If
            Else
                oRec.Close()
                Return CheckInLevel
            End If

        Catch ex As exception
            If Not oRec Is Nothing Then
                oRec.Close()
            End If
            Return 0
        End Try
    End Function
    Private Function OpenConn() As Data.SqlClient.SqlConnection
        Try
            Dim ConString As String = String.Empty
            ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            oConn.Open()
            Return oConn
        Catch ex As exception
            Err.Clear()
        End Try
    End Function
    Private Function UpdateTranscriptionStatus(ByVal UserID As String, ByVal TransID As String, ByVal CheckInLevel As Long, ByVal TemplateID As String, ByVal UserLevel As Long, ByVal Version As Integer, ByVal Location As Integer, ByVal LineCount As Integer, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As Boolean
        Try
            Dim clsDic As New ETS.BL.Dictations
            With clsDic
                If .IsJobAlreadyCheckedOut(UserID, TransID, UserLevel + 100) Then
                    'If .UpdateTranscriptionLog(UserID, CheckInLevel, "", HttpContext.Current.Request.UserHostAddress, UserLevel + 100, TransID, LineCount, Version, TemplateID, ThisTransaction, oConn) Then
                    '    Return True
                    'Else
                    '    Return False
                    'End If
                Else
                    Return False
                End If
            End With
            clsDic = Nothing
        Catch ex As exception
            Return False
        End Try
    End Function
    Private Function UpdateTranscriptionAttributes(ByVal TransID As String, ByVal AttribTable As DataTable, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As String
        Dim SQLString As String
        Try
            Dim Res As Boolean = False
            Dim oCommand As New Data.SqlClient.SqlCommand
            SQLString = "SF_DeleteAttrib"
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand.Transaction = ThisTransaction
            Dim oParam As New Data.SqlClient.SqlParameter("@RC", Data.SqlDbType.Int, 8)
            oParam.Value = 0
            oCommand.Parameters.Add(oParam)
            oParam = New Data.SqlClient.SqlParameter("@TransID", Data.SqlDbType.VarChar, 36)
            oParam.Value = TransID
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            oCommand.ExecuteNonQuery()
            For Each DR As DataRow In AttribTable.Rows

                SQLString = "SF_InsertAttrib"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = ThisTransaction
                oParam = New Data.SqlClient.SqlParameter("@RC", Data.SqlDbType.Int, 8)
                oParam.Value = 0
                oCommand.Parameters.Add(oParam)
                oParam = New Data.SqlClient.SqlParameter("@TransID", Data.SqlDbType.VarChar, 36)
                oParam.Value = TransID
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@AttribID", Data.SqlDbType.VarChar, 36)
                oParam.Value = DR("AttributeID").ToString
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@Value", Data.SqlDbType.VarChar, 255)
                oParam.Value = DR("Value").ToString
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@Sequal", Data.SqlDbType.Int, 8)
                oParam.Value = DR("Sequal")
                oCommand.Parameters.Add(oParam)

                oCommand.CommandType = Data.CommandType.StoredProcedure

                If oCommand.ExecuteNonQuery() > 0 Then
                    Res = True
                Else
                    Res = False
                    Exit For
                End If
            Next
            Return Res
        Catch ex As exception
            Return False
        End Try
    End Function

    Private Function UpdateTranscriptionLog(ByVal TransID As String, ByVal UserID As String, ByVal UserLevel As Long, ByVal CheckInLevel As Long, ByVal Version As Integer, ByVal LineCount As Integer, ByVal TemplateID As String, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As Boolean
        Try
            Dim oCommand As New Data.SqlClient.SqlCommand
            Dim SQLString As String = "INSERT INTO tblTranscriptionLog(TranscriptionID,UserID,UserLevel,Status,LineCount,DateModified,version,TemplateID,IP) " & _
                                      "Values('" & TransID & "','" & UserID & "'," & UserLevel & "," & CheckInLevel & "," & LineCount & ",'" & Now() & "'," & Version & ",'" & TemplateID & "','" & HttpContext.Current.Request.UserHostAddress & "')"
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand.Transaction = ThisTransaction
            If oCommand.ExecuteNonQuery() > 0 Then
                SQLString = "DELETE from tblTranscriptionStatus where TranscriptionID='" & TransID & "' and (dbo.getPrLvlSeqCon(Status,'" & UserID & "')>=dbo.getPrLvlSeqCon(" & CheckInLevel & ",'" & UserID & "') or (userID='" & UserID & "' and userlevel=" & UserLevel & "))"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = ThisTransaction
                oCommand.ExecuteNonQuery()

                SQLString = "INSERT INTO tblTranscriptionStatus(TranscriptionID,UserID,UserLevel,Status,LineCount,DateModified,Version) " & _
                            "Values('" & TransID & "','" & UserID & "'," & UserLevel & "," & CheckInLevel & "," & LineCount & ",'" & Now() & "'," & Version & ")"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = ThisTransaction
                If oCommand.ExecuteNonQuery() > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As exception
            Return False
        End Try
    End Function
    Private Function setSample(ByVal CanSetSample As Boolean, ByVal TransID As String, ByVal UserID As String, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As Boolean
        Try
            If CanSetSample Then
                Dim oCommand As New Data.SqlClient.SqlCommand
                Dim SQLString As String = "delete from tblSetSamples where TranscriptionID='" & TransID & "'"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = ThisTransaction
                oCommand.ExecuteNonQuery()
                SQLString = "INSERT INTO tblSetSamples(TranscriptionID,SuggestedBy,DateAvailable,Status) " & _
                                          "Values('" & TransID & "','" & UserID & "','" & Now() & "',0)"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = ThisTransaction
                If oCommand.ExecuteNonQuery() > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As exception
            Return False
        End Try
    End Function

    Private Function UpdateFaxPlusInfo(ByVal DTFaxPlus As DataTable, ByVal UserID As String, ByVal TransID As String, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As Boolean
        Try
            If Not DTFaxPlus Is Nothing Then
                Dim clsFPEx As New ETS.BL.FaxPlusExceptions
                With clsFPEx
                    .TranscriptionID = TransID
                    .DeleteFPExForDictation(oConn, ThisTransaction)
                End With
                Dim clsFP As New ETS.BL.FaxPlus
                With clsFP
                    .TranscriptionID = TransID
                    .DeleteFaxPlusDetails(oConn, ThisTransaction)
                End With
                For Each DR As DataRow In DTFaxPlus.Rows
                    If DR("RPType") = "1" Then
                        clsFPEx = New ETS.BL.FaxPlusExceptions
                        With clsFPEx
                            .TranscriptionID = TransID
                            .RPID = DR("RPID").ToString
                            .RPName = DR("RPName").ToString.Replace("'", "''")
                            .RPADD = DR("RPAdd").ToString.Replace("'", "''")
                            .Comments = DR("RPComm").ToString
                            .UserID = UserID
                            .DateAvaialble = Now()
                            If Not .InsertFPExDetails(oConn, ThisTransaction) > 0 Then
                                Return False
                                Exit Function
                            End If
                        End With

                    Else
                        clsFP = New ETS.BL.FaxPlus
                        With clsFP
                            .TranscriptionID = TransID
                            .RPID = DR("RPID").ToString
                            .Status = 0
                            .DateAvailable = Now()
                            If Not .InsertFaxPlusDetails(oConn, ThisTransaction) > 0 Then
                                Return False
                                Exit Function
                            End If
                        End With
                    End If
                Next
                clsFPEx = Nothing
                clsFP = Nothing
                Return True
            Else
                Return True
            End If
        Catch ex As exception
            Return False
        End Try
    End Function
    Private Function UpdateTranscriptionComments(ByVal TransID As String, ByVal UserID As String, ByVal CheckInLevel As Long, ByVal Version As Integer, ByVal Comment As String, ByVal CSComment As String, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As Boolean
        Try
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand("SF_InsertComment", oConn)
            oCommand.CommandType = CommandType.StoredProcedure
            oCommand.Parameters.Add("@TransID", SqlDbType.UniqueIdentifier).Value = New Guid(TransID)
            oCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = New Guid(UserID)
            oCommand.Parameters.Add("@CheckInLevel", SqlDbType.Int).Value = CheckInLevel
            oCommand.Parameters.Add("@Comment", SqlDbType.VarChar).Value = Comment
            oCommand.Parameters.Add("@Version", SqlDbType.Int).Value = Version
            oCommand.Transaction = ThisTransaction
            If oCommand.ExecuteNonQuery() > 0 Then
                oCommand = New Data.SqlClient.SqlCommand("SF_InsertCSComment", oConn)
                oCommand.CommandType = CommandType.StoredProcedure
                oCommand.Parameters.Add("@TransID", SqlDbType.UniqueIdentifier).Value = New Guid(TransID)
                oCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = New Guid(UserID)
                oCommand.Parameters.Add("@CheckInLevel", SqlDbType.Int).Value = CheckInLevel
                oCommand.Parameters.Add("@Comment", SqlDbType.VarChar).Value = CSComment
                oCommand.Parameters.Add("@Version", SqlDbType.Int).Value = Version
                oCommand.Transaction = ThisTransaction
                If oCommand.ExecuteNonQuery() > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As exception
            Return False
        End Try
    End Function
    Private Function UpdateClientsComments(ByVal TransID As String, ByVal UserID As String, ByVal DTClientComment As DataTable, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As Boolean
        Try
            Dim oCommand As New Data.SqlClient.SqlCommand
            For Each DR As DataRow In DTClientComment.Rows
                oCommand = New Data.SqlClient.SqlCommand("SF_InsertClientComment", oConn)
                oCommand.CommandType = CommandType.StoredProcedure
                oCommand.Parameters.Add("@TransID", SqlDbType.UniqueIdentifier).Value = New Guid(TransID)
                oCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = New Guid(UserID)
                oCommand.Parameters.Add("@PreID", SqlDbType.UniqueIdentifier).Value = New Guid(DR("PreID").ToString)
                oCommand.Transaction = ThisTransaction
                If Not oCommand.ExecuteNonQuery() > 0 Then
                    Return False
                    Exit Function
                End If
            Next
            Return True
        Catch ex As exception
            Return False
        End Try
    End Function
    Private Function UpdateEPTL(ByVal CanMarkErrors As Boolean, ByVal TransID As String, ByVal UserID As String, ByVal UserLevel As Long, ByVal CheckInLevel As Long, ByVal ErrCri As Long, ByVal ErrMaj As Long, ByVal ErrMin As Long, ByVal ErrPMI As Long, ByVal ErrTemp As Long, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As Boolean
        Dim SQLString As String = String.Empty
        Try
            If CanMarkErrors Then
                Dim oCommand As New Data.SqlClient.SqlCommand
                SQLString = "SF_UpdateEPTL"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                Dim oParam As New Data.SqlClient.SqlParameter("@TransID", Data.SqlDbType.VarChar, 36)
                oParam.Value = TransID
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@UserID", Data.SqlDbType.VarChar, 36)
                oParam.Value = UserID
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@UserLevel", Data.SqlDbType.Int, 8)
                oParam.Value = UserLevel
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@CheckInLevel", Data.SqlDbType.Int, 8)
                oParam.Value = CheckInLevel
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@ErrCri", Data.SqlDbType.Int, 8)
                oParam.Value = ErrCri
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@ErrMaj", Data.SqlDbType.Int, 8)
                oParam.Value = ErrMaj
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@ErrMin", Data.SqlDbType.Int, 8)
                oParam.Value = ErrMin
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@ErrPMI", Data.SqlDbType.Int, 8)
                oParam.Value = ErrPMI
                oCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@ErrTemp", Data.SqlDbType.Int, 8)
                oParam.Value = ErrTemp
                oCommand.Parameters.Add(oParam)

                oCommand.CommandType = Data.CommandType.StoredProcedure

                oCommand.Transaction = ThisTransaction
                If oCommand.ExecuteNonQuery() > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As exception
            Return False
        End Try
    End Function
    Private Function UpdateEvaluationRequest(ByVal TransID As String, ByVal UserID As String, ByVal UserLevel As Long, ByVal CheckInLevel As Long, ByVal ForEval As Boolean, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As Boolean
        Try
            If ForEval Then
                Dim SQLString As String
                Dim oCommand As New Data.SqlClient.SqlCommand
                'Dim SQLString As String = "DELETE from tblForEvaluation where TranscriptionID='" & TransID & "'"
                'oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                'oCommand.Transaction = ThisTransaction
                'oCommand.ExecuteNonQuery()
                SQLString = "INSERT INTO tblForEvaluation(TranscriptionID,UserID,ForEvaluation,UserLevel,CheckInLevel) " & _
                                          "Values('" & TransID & "','" & UserID & "',1," & UserLevel & "," & CheckInLevel & ")"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = ThisTransaction
                If oCommand.ExecuteNonQuery() > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If
        Catch ex As exception
            Return False
        End Try
    End Function
    <WebMethod()> _
    Public Function AddNewPatient(ByVal OriTransID As String, ByVal UserID As String, ByVal UserLevel As Long) As String
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCommand As New Data.SqlClient.SqlCommand
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        Try
            Dim DT As DataTable = GetTranscriptionInfo(OriTransID)
            If DT.Rows.Count = 0 Then
                Return "0"
                Exit Function
            End If

            Dim DR As DataRow = DT.Rows(0)
            Dim DictationPath As String = Server.MapPath("../ETS_Files/Dictations")
            Dim OldDictation As String = OriTransID & DR("Type").ToString
            If File.Exists(IO.Path.Combine(DictationPath, OldDictation)) Then
                Dim intJobNumber As Integer
                oConn = OpenConn()
                If Not oConn.State = ConnectionState.Open Then
                    Return "0"
                    Exit Function
                End If
                thisTransaction = oConn.BeginTransaction()
                Dim SQLString As String = "update tblJobNumber set jobNumber = jobNumber + 1"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                oCommand.ExecuteNonQuery()
                SQLString = "select jobNumber from tblJobNumber"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                Dim oRec As SqlClient.SqlDataReader = oCommand.ExecuteReader
                oRec.Read()
                If oRec.HasRows Then
                    intJobNumber = oRec("jobNumber")
                End If
                oRec.Close()
                Dim TranscriptionID As String = Guid.NewGuid.ToString
                Dim NewDictation As String = TranscriptionID & DR("Type").ToString
                If String.IsNullOrEmpty(DR("TemplateID").ToString) Then
                    SQLString = "Insert into tblTranscriptionMain(TranscriptionID,JobNumber,CustJobID,Status,PinNumber,Duration,SubmitDate,TAT,DueDate,DateCreated,DateDictated,Priority,TemplateID,AccountNumber,Location,DictatorID,AccountID,ContractorID,Type) " & _
                                      "values('" & TranscriptionID & "'," & intJobNumber & ",'" & DR("CustJobID") & "'," & UserLevel + 100 & ",'" & DR("PinNumber").ToString & "','00:00:00','" & DR("SubmitDate").ToString & "'," & DR("TAT") & ",'" & DR("DueDate") & "','" & DR("DateCreated") & "','" & DR("DateDictated") & "','" & DR("Priority") & "',NULL," & DR("AccountNumber") & "," & DR("Location") & ",'" & DR("DictatorID").ToString & "','" & DR("AccountID").ToString & "','" & DR("ContractorID").ToString & "','" & DR("Type").ToString & "')"
                Else
                    SQLString = "Insert into tblTranscriptionMain(TranscriptionID,JobNumber,CustJobID,Status,PinNumber,Duration,SubmitDate,TAT,DueDate,DateCreated,DateDictated,Priority,TemplateID,AccountNumber,Location,DictatorID,AccountID,ContractorID, Type) " & _
                                "values('" & TranscriptionID & "'," & intJobNumber & ",'" & DR("CustJobID") & "'," & UserLevel + 100 & ",'" & DR("PinNumber").ToString & "','00:00:00','" & DR("SubmitDate").ToString & "'," & DR("TAT") & ",'" & DR("DueDate") & "','" & DR("DateCreated") & "','" & DR("DateDictated") & "','" & DR("Priority") & "','" & DR("TemplateID").ToString & "'," & DR("AccountNumber") & "," & DR("Location") & ",'" & DR("DictatorID").ToString & "','" & DR("AccountID").ToString & "','" & DR("ContractorID").ToString & "','" & DR("Type").ToString & "')"
                End If
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                oCommand.ExecuteNonQuery()

                SQLString = "insert into  tblTranscriptionLog(TranscriptionID,UserID,UserLevel,Status,DateModified,IP) " & _
                    "values('" & TranscriptionID & "','" & UserID & "'," & UserLevel & "," & UserLevel + 100 & ",'" & Now() & "','" & Me.Context.Request.UserHostAddress & "')"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                oCommand.ExecuteNonQuery()

                SQLString = "insert into  tblNewPatientLog(ParentID,ChildID,UserID,UserLevel,DateAdded) " & _
                                    "values('" & OriTransID & "','" & TranscriptionID & "','" & UserID & "'," & UserLevel + 100 & ",'" & Now() & "')"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction

                If oCommand.ExecuteNonQuery > 0 Then

                    If UpdateExtendedInfo(OriTransID, TranscriptionID, oConn, thisTransaction) Then
                        IO.File.Copy(IO.Path.Combine(DictationPath, OldDictation), IO.Path.Combine(DictationPath, NewDictation), True)
                        If IO.File.Exists(IO.Path.Combine(DictationPath, NewDictation)) Then
                            thisTransaction.Commit()
                            Return intJobNumber
                        Else
                            thisTransaction.Rollback()
                            Return "0"
                        End If
                    Else
                        thisTransaction.Rollback()
                        Return "0"
                    End If
                Else
                    thisTransaction.Rollback()
                    Return "0"
                End If
                oConn.Close()
            End If
        Catch ex As exception
            If Not thisTransaction Is Nothing Then
                thisTransaction.Rollback()
            End If
            Return "0"
            Exit Function
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try


    End Function
    <WebMethod()> _
   Public Function AddNewPatientVRS(ByVal OriTransID As String, ByVal UserID As String, ByVal UserLevel As Long, ByVal Sequence As Integer) As String
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCommand As New Data.SqlClient.SqlCommand
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        Try
            Dim DT As DataTable = GetTranscriptionInfo(OriTransID)
            If DT.Rows.Count = 0 Then
                Return "0"
                Exit Function
            End If

            Dim DR As DataRow = DT.Rows(0)
            Dim DictationPath As String = Server.MapPath("../ETS_Files/Dictations")
            Dim OldDictation As String = OriTransID & DR("Type").ToString
            If File.Exists(IO.Path.Combine(DictationPath, OldDictation)) Then
                Dim intJobNumber As Integer
                oConn = OpenConn()
                If Not oConn.State = ConnectionState.Open Then
                    Return "0"
                    Exit Function
                End If
                thisTransaction = oConn.BeginTransaction()
                Dim SQLString As String = "update tblJobNumber set jobNumber = jobNumber + 1"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                oCommand.ExecuteNonQuery()
                SQLString = "select jobNumber from tblJobNumber"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                Dim oRec As SqlClient.SqlDataReader = oCommand.ExecuteReader
                oRec.Read()
                If oRec.HasRows Then
                    intJobNumber = oRec("jobNumber")
                End If
                oRec.Close()
                Dim TranscriptionID As String = Guid.NewGuid.ToString
                Dim NewDictation As String = TranscriptionID & DR("Type").ToString
                If String.IsNullOrEmpty(DR("TemplateID").ToString) Then
                    SQLString = "Insert into tblTranscriptionMain(TranscriptionID,JobNumber,CustJobID,Status,PinNumber,Duration,SubmitDate,TAT,DueDate,DateCreated,DateDictated,Priority,TemplateID,AccountNumber,Location,DictatorID,AccountID,ContractorID,Type) " & _
                                      "values('" & TranscriptionID & "'," & intJobNumber & ",'" & DR("CustJobID") & "'," & UserLevel & ",'" & DR("PinNumber").ToString & "','00:00:00','" & DR("SubmitDate").ToString & "'," & DR("TAT") & ",'" & DR("DueDate") & "','" & DR("DateCreated") & "','" & DR("DateDictated") & "','" & DR("Priority") & "',NULL," & DR("AccountNumber") & "," & DR("Location") & ",'" & DR("DictatorID").ToString & "','" & DR("AccountID").ToString & "','" & DR("ContractorID").ToString & "','" & DR("Type").ToString & "')"
                Else
                    SQLString = "Insert into tblTranscriptionMain(TranscriptionID,JobNumber,CustJobID,Status,PinNumber,Duration,SubmitDate,TAT,DueDate,DateCreated,DateDictated,Priority,TemplateID,AccountNumber,Location,DictatorID,AccountID,ContractorID, Type) " & _
                                "values('" & TranscriptionID & "'," & intJobNumber & ",'" & DR("CustJobID") & "'," & UserLevel & ",'" & DR("PinNumber").ToString & "','00:00:00','" & DR("SubmitDate").ToString & "'," & DR("TAT") & ",'" & DR("DueDate") & "','" & DR("DateCreated") & "','" & DR("DateDictated") & "','" & DR("Priority") & "','" & DR("TemplateID").ToString & "'," & DR("AccountNumber") & "," & DR("Location") & ",'" & DR("DictatorID").ToString & "','" & DR("AccountID").ToString & "','" & DR("ContractorID").ToString & "','" & DR("Type").ToString & "')"
                End If

                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction
                oCommand.ExecuteNonQuery()
                Dim CheckOutDictation As Boolean = False
                Dim clsDic As New ETS.BL.Dictations
                With clsDic
                    CheckOutDictation = .AssignDictations(UserID, UserLevel + 100, "", True, Me.Context.Request.UserHostAddress, TranscriptionID, UserLevel, oConn, thisTransaction)
                    'Return .AssignDictations(UserID, UserLevel + 100, "", True, Me.Context.Request.UserHostAddress, TranscriptionID, UserLevel, oConn, thisTransaction)
                End With
                clsDic = Nothing

                If CheckOutDictation = False Then
                    thisTransaction.Rollback()
                    Return "A0"
                End If

                'SQLString = "insert into  tblTranscriptionLog(TranscriptionID,UserID,UserLevel,Status,DateModified,IP) " & _
                '    "values('" & TranscriptionID & "','" & UserID & "'," & UserLevel & "," & UserLevel + 100 & ",'" & Now() & "','" & Me.Context.Request.UserHostAddress & "')"
                'oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                'oCommand.Transaction = thisTransaction
                'oCommand.ExecuteNonQuery()

                SQLString = "insert into  tblNewPatientLog(ParentID,ChildID,UserID,UserLevel,DateAdded,IsVRSJob,Sequence) " & _
                                    "values('" & OriTransID & "','" & TranscriptionID & "','" & UserID & "'," & UserLevel + 100 & ",'" & Now() & "',1," & Sequence & ")"
                oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
                oCommand.Transaction = thisTransaction

                If oCommand.ExecuteNonQuery > 0 Then
                    If UpdateExtendedInfo(OriTransID, TranscriptionID, oConn, thisTransaction) Then
                        IO.File.Copy(IO.Path.Combine(DictationPath, OldDictation), IO.Path.Combine(DictationPath, NewDictation), True)
                        If IO.File.Exists(IO.Path.Combine(DictationPath, NewDictation)) Then
                            thisTransaction.Commit()
                            Return TranscriptionID
                        Else
                            thisTransaction.Rollback()
                            Return "0"
                        End If
                    Else
                        thisTransaction.Rollback()
                        Return "0"
                    End If
                Else
                    thisTransaction.Rollback()
                    Return "0"
                End If
                oConn.Close()
            End If
        Catch ex As exception
            If Not thisTransaction Is Nothing Then
                thisTransaction.Rollback()
            End If
            Return ex.StackTrace
            Exit Function
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try


    End Function
    Private Function UpdateExtendedInfo(ByVal OldTransID As String, ByVal TransID As String, ByVal oConn As SqlClient.SqlConnection, ByVal ThisTransaction As SqlClient.SqlTransaction) As Boolean
        Try
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim OCommand As New Data.SqlClient.SqlCommand("SF_getTranscriptionAttrValuesByTransID", oConn)
            OCommand.CommandType = CommandType.StoredProcedure
            OCommand.Parameters.Add("@TranscriptionID", SqlDbType.UniqueIdentifier).Value = New Guid(OldTransID)
            Adapter.SelectCommand = OCommand
            Adapter.SelectCommand.Transaction = ThisTransaction
            Dim myDs As New Data.DataSet
            Adapter.Fill(myDs, "Extended")
            For Each DR As DataRow In myDs.Tables("Extended").Rows
                OCommand = New Data.SqlClient.SqlCommand("SF_InsertAttrib", oConn)
                OCommand.Transaction = ThisTransaction
                Dim oParam As SqlClient.SqlParameter
                oParam = New Data.SqlClient.SqlParameter("@RC", Data.SqlDbType.Int, 8)
                oParam.Value = 0
                OCommand.Parameters.Add(oParam)
                oParam = New Data.SqlClient.SqlParameter("@TransID", Data.SqlDbType.UniqueIdentifier)
                oParam.Value = New Guid(TransID)
                OCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@AttribID", Data.SqlDbType.UniqueIdentifier)
                oParam.Value = New Guid(DR("AttributeID").ToString)
                OCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@Value", Data.SqlDbType.VarChar, 255)
                oParam.Value = DR("Value").ToString
                OCommand.Parameters.Add(oParam)

                oParam = New Data.SqlClient.SqlParameter("@Sequal", Data.SqlDbType.Int, 8)
                oParam.Value = DR("Sequal")
                OCommand.Parameters.Add(oParam)
                OCommand.ExecuteNonQuery()
            Next
            Return True
        Catch ex As exception
            Return False
        End Try
    End Function
    Private Function GetTranscriptionInfo(ByVal TransID As String) As DataTable
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            oConn = OpenConn()
            If Not oConn.State = ConnectionState.Open Then
                Return Nothing
                Exit Function
            End If
            Dim SQLString As String = "SF_GetTranscriptionInfo"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@TransID", Data.SqlDbType.VarChar, 36)
            oParam.Value = TransID
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Adapter.SelectCommand = oCommand
            Dim myDs As New Data.DataSet
            Adapter.Fill(myDs, "TransInfo")
            Return myDs.Tables("TransInfo")
        Catch ex As exception
            Err.Clear()
            Return Nothing
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function CompletedJobsList(ByVal JobNumber As Integer, ByVal PhyFirst As String, ByVal PhyLast As String, ByVal sDate As String, ByVal eDate As String, ByVal UserID As String, ByVal UserLevel As Long) As DataSet

        Dim ConString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim myDs As New Data.DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New SqlClient.SqlCommand("MTC_CompletedJobsList", oConn)
            oCommand.CommandType = CommandType.StoredProcedure
            oCommand.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = New Guid(UserID)
            oCommand.Parameters.Add("@Level", SqlDbType.Int).Value = UserLevel
            oCommand.Parameters.Add("@JobNumber", SqlDbType.Int).Value = IIf(JobNumber <= 0, DBNull.Value, JobNumber)
            oCommand.Parameters.Add("@PhyFirst", SqlDbType.VarChar).Value = IIf(String.IsNullOrEmpty(PhyFirst), DBNull.Value, PhyFirst)
            oCommand.Parameters.Add("@PhyLast", SqlDbType.VarChar).Value = IIf(String.IsNullOrEmpty(PhyLast), DBNull.Value, PhyLast)
            oCommand.Parameters.Add("@sDate", SqlDbType.VarChar).Value = sDate
            oCommand.Parameters.Add("@eDate", SqlDbType.VarChar).Value = eDate

            Adapter.SelectCommand = oCommand


            Adapter.Fill(myDs, "CJobList")

            Return myDs

        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
   Public Function AuditedJobsList(ByVal JobNumber As String, ByVal PhyFirst As String, ByVal PhyLast As String, ByVal sDate As String, ByVal eDate As String, ByVal UserID As String, ByVal UserLevel As Long, ByVal isauditor As Boolean)
        Dim ConString, SQLString As String
        Dim eDateField As String = IIf(isauditor, "AR.DateFinished", "TL.CheckInDate")
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim SelectClause As String = "SELECT DISTINCT TM.TranscriptionID, TM.JobNumber, TM.CustJobID, CONVERT(char, TM.SubmitDate, 101) AS SubmitDate, "
            If Not isauditor Then
                SelectClause = SelectClause & "CONVERT(char, TL.CheckInDate, 101) AS CheckInDate,"
            Else
                SelectClause = SelectClause & "CONVERT(char, AR.DateFinished, 101) AS CheckInDate,"
            End If

            SelectClause = SelectClause & "TL.LineCount, TM.duration, P.FirstName + ' ' + P.LastName AS PhyName, " & _
"A.AccountName, TL.version as Myversion,isnull(EPTL.ErrCri,0) as ErrCri, isnull(EPTL.ErrMaj,0) as ErrMaj, isnull(EPTL.ErrMin,0) as ErrMin, isnull(EPTL.ErrPMI,0) as ErrPMI, isnull(EPTL.ErrTemp,0) as ErrTemp, isnull(EPTL.Comment,'') as Comment "

            Dim FromClause As String = "FROM tblTranscriptionMain AS TM INNER JOIN tblPhysicians AS P ON TM.DictatorID = P.PhysicianID INNER JOIN " & _
"tblAccounts AS A ON TM.AccountID = A.AccountID LEFT OUTER JOIN tblAuditEPTL AS EPTL " & _
"ON TM.TranscriptionID = EPTL.TranscriptionID " & _
"INNER JOIN (select TranscriptionID ,DateFinished, userid, AuditFor from tblAuditRecords "
            If Not isauditor Then
                FromClause = FromClause & "where UserID = '" & UserID & "' AND UserLevel = " & UserLevel & " and Status = 200) AS AR ON TM.TranscriptionID = AR.TranscriptionID "
            Else
                FromClause = FromClause & "where AuditorID = '" & UserID & "' and Status = 200) AS AR ON TM.TranscriptionID = AR.TranscriptionID "
            End If
            Dim FromClause1 As String = " INNER JOIN (select TranscriptionID,DateModified AS CheckInDate, version, LineCount,UserID from tblTranscriptionStatus "
            'Dim FromClauseU As String = FromClause & " INNER JOIN (SELECT TranscriptionID, DateModified AS CheckInDate, Version, LineCount, UserID FROM tblTranscriptionStatus TS where TS.Version=(select max(Version) from tblTranscriptionStatus where TranscriptionID=TS.TranscriptionID )) AS TL ON TL.TranscriptionID = TM.TranscriptionID "

            If Not isauditor Then
                FromClause1 = FromClause1 & " WHERE (UserID = '" & UserID & "') And (UserLevel = " & UserLevel & ")"
            End If
            FromClause1 = FromClause1 & " )AS TL ON TL.TranscriptionID = TM.TranscriptionID and TL.UserID=AR.UserID "
            Dim FromClause2 As String = " inner JOIN (select TranscriptionID,DateModified AS CheckInDate, version, LineCount,UserID from tblTranscriptionStatus as TS where version=(select max(version) from tblTranscriptionStatus where transcriptionid=TS.transcriptionid ) )AS TL ON TL.TranscriptionID = TM.TranscriptionID And AR.UserID = P.PhysicianID "


            Dim whereClause As String = String.Empty
            If Not String.IsNullOrEmpty(JobNumber) Then
                whereClause = whereClause & "AND (TM.JobNumber LIKE '" & JobNumber & "') "
            End If
            If Not String.IsNullOrEmpty(PhyFirst) Then
                whereClause = whereClause & "AND (P.FirstName LIKE '" & PhyFirst & "') "
            End If
            If Not String.IsNullOrEmpty(PhyLast) Then
                whereClause = whereClause & "AND (P.LastName LIKE '" & PhyLast & "') "
            End If
            If String.IsNullOrEmpty(eDate) = True And String.IsNullOrEmpty(sDate) = False Then
                whereClause = whereClause & "AND (" & eDateField & " = '" & sDate & "') "
            End If
            If String.IsNullOrEmpty(eDate) = False And String.IsNullOrEmpty(sDate) = True Then
                whereClause = whereClause & "AND (" & eDateField & "= '" & eDate & "') "
            End If
            If String.IsNullOrEmpty(eDate) = False And String.IsNullOrEmpty(sDate) = False Then
                whereClause = whereClause & "AND (" & eDateField & " between '" & sDate & "' and  '" & eDate & "' ) "
            End If
            SQLString = SelectClause & FromClause & FromClause1 & " WHERE TM.Status = 1073741824 " & whereClause & IIf(isauditor, " UNION " & SelectClause & FromClause & FromClause2 & " WHERE TM.Status = 1073741824 " & whereClause, "")

            SQLString = SQLString & "ORDER BY TM.JobNumber"

            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim myDs As New Data.DataSet
            Adapter.Fill(myDs, "CJobList")
            Dim sw As New IO.StringWriter
            myDs.WriteXml(sw)
            Return sw.ToString
        Catch ex As exception
            Return ex.Message
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
    End Function
    <WebMethod()> _
    Public Function ETS_Files() As String
        Try
            Return Replace(Server.MapPath("../ETS_Files").ToString, ":", "$")
        Catch ex As exception
            Return "0"
        End Try
    End Function
    <WebMethod()> _
    Public Function SplitAudio(ByVal CommandArg As String) As Boolean
        Try
            Dim secPass As New System.Security.SecureString()
            For Each c As Char In "paheli"
                secPass.AppendChar(c)
            Next

            ' Create An instance of the Process class responsible for starting the newly process. 
            Dim process1 As New System.Diagnostics.Process()
            ' Set the directory where the file resides 
            process1.StartInfo.WorkingDirectory = Server.MapPath("../ETS_Files").ToString

            ' Set the filename name of the file you want to open 
            'Response.Write(Server.MapPath("../ETS_Files").ToString & "\DSS_Sample.exe" & " " & Server.MapPath("../ETS_Files").ToString & "\test.dss$0.50$1.00$test.wav")
            process1.StartInfo.FileName = Server.MapPath("../ETS_Files").ToString & "\DSS_Sample.exe"
            process1.StartInfo.Arguments = Server.MapPath("../ETS_Files").ToString & "\Dictations\" & CommandArg
            ' Start the process 

            process1.Start()
            process1.WaitForExit()
            process1.Close()
            If IO.File.Exists(Server.MapPath("../ETS_Files").ToString & "\Dictations\" & "\test.wav") Then
                Return True
            Else
                Return False
            End If
        Catch ex As exception
            Return False
        End Try
    End Function
    <WebMethod()> _
    Public Function getActLocations(ByVal ActID As String) As DataSet
        Dim dsAL As New DataSet
        Try

            Dim clsAL As New ETS.BL.AccountsLocations
            With clsAL
                .AccountID = ActID
                dsAL = .getAcountsLocationList()
            End With
            clsAL = Nothing
            Return dsAL
        Catch ex As exception
            Return Nothing
        Finally
            dsAL.Dispose()
        End Try
    End Function
    <WebMethod()> _
    Public Function getNotes(ByVal strTransID As String, ByVal FileType As String) As String
        'Response:
        'Audio File Exist:Response(First Column)=1
        'Audio File Not Exist:Response(First Column)=0

        'Transcription Exist:Response(Second Column)=?1
        'Transcription Not Exist:Response(Second Column)=?0
        Dim Response As String = String.Empty
        Dim TransPath As String = String.Empty
        Try
            TransPath = Path.Combine(Server.MapPath("../ETS_Files").ToString, "Notes")
            If Directory.Exists(TransPath) Then
                If File.Exists(TransPath & "\" & strTransID & ".doc") Then
                    Response = Response & "1"
                Else
                    Response = Response & "0"
                End If
            Else
                Response = Response & "2"
            End If
            Return Response
        Catch ex As exception
            Return ""
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

        Catch ex As exception
            Return ex.Message
            Exit Function

        End Try

    End Function
    <WebMethod()> _
    Public Function MTC_DemoLookUp(ByVal DemoAccName As String, ByVal AccountID As String, ByVal SDate As String, ByVal eDate As String, ByVal PFName As String, ByVal PLName As String, ByVal MEDRN As String, ByVal APName As String, ByVal DOB As String, ByVal PatID As String) As DataSet

        Dim DisplayFiels As String = String.Empty
        Dim myDs As New Data.DataSet
        Try
            getDemoAccountDetails(DemoAccName, AccountID)
            Dim clsDemo As New ETS.BL.Demographics
            With clsDemo
                DisplayFiels = .getAcctDemoFields(AccountID)
                DisplayFiels = DisplayFiels.Replace("DtOfServ", "cast(dtofserv as datetime) as DtOfServ")
                DisplayFiels = DisplayFiels.Replace("PDOB", "cast(PDOB as datetime) as PDOB")
                If Not String.IsNullOrEmpty(DisplayFiels.ToString) Then
                    myDs = .DemoLookUp(DisplayFiels.ToString, DemoAccName, SDate, eDate, PFName, PLName, MEDRN, APName, DOB, PatID)
                End If
            End With
            clsDemo = Nothing
            Return myDs
        Catch ex As exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
   Public Function getDictatorNormals(ByVal phyID As String) As DataSet
        Dim ConString As String
        Dim SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim myDs As New Data.DataSet
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            SQLString = "SF_getNormalsByPhyID"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim oParam As New Data.SqlClient.SqlParameter("@PhyID", Data.SqlDbType.UniqueIdentifier)
            oParam.Value = New Guid(phyID)
            oCommand.Parameters.Add(oParam)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Adapter.SelectCommand = oCommand
            Adapter.Fill(myDs, "NormalInfo")
            Return myDs
        Catch ex As exception
            Return Nothing
        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try

    End Function

    <WebMethod()> _
   Public Function getXMLDoc(ByVal TranscriptionID As String) As String
        Try
            Dim xmlFile As String = Path.Combine(Server.MapPath("ETS_Files").ToString, "transcriptions")
            If File.Exists(Path.Combine(xmlFile, TranscriptionID & ".xml")) Then
                Dim xDoc As XmlDocument = New XmlDocument()
                xDoc.PreserveWhitespace = True
                xDoc.Load(Path.Combine(xmlFile, TranscriptionID & ".xml"))
                Return xDoc.OuterXml
            Else
                Return Nothing
            End If
        Catch ex As exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
   Public Function setXMLDoc(ByVal TranscriptionID As String, ByVal TranscriptionData As String) As Boolean
        Try
            Dim xmlFile As String = Path.Combine(Server.MapPath("ETS_Files").ToString, "Macros")
            Dim xDoc As XmlDocument = New XmlDocument()
            xDoc.PreserveWhitespace = True
            xDoc.LoadXml(TranscriptionData)
            xDoc.Save(Path.Combine(xmlFile, TranscriptionID & ".xml"))
            If File.Exists(Path.Combine(xmlFile, TranscriptionID & ".xml")) Then
                Return True
            Else
                Return False
            End If
        Catch ex As exception
            Return Nothing
        End Try
    End Function
    <WebMethod()> _
    Public Function setCDADoc(ByVal TranscriptionID As String, ByVal TranscriptionData As String) As String
        Dim xmlFile As String = Path.Combine(Server.MapPath("../ETS_Files").ToString, "Transcriptions")
        Dim bkupCDAfile As String = Path.Combine(xmlFile, TranscriptionID & "_" & Now().ToString("MMddyyyyhhmmss"))
        Try

            Dim xDoc As XmlDocument = New XmlDocument()
            xDoc.PreserveWhitespace = True
            xDoc.LoadXml(TranscriptionData)

            If Not File.Exists(bkupCDAfile) Then
                File.Move(Path.Combine(xmlFile, TranscriptionID), bkupCDAfile)
            End If

            xDoc.Save(Path.Combine(xmlFile, TranscriptionID))
            If File.Exists(Path.Combine(xmlFile, TranscriptionID)) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If Not File.Exists(xmlFile) Then
                File.Move(Path.Combine(bkupCDAfile, TranscriptionID), xmlFile)
            End If
            Return xmlFile
        End Try
    End Function
End Class

