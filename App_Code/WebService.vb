Imports System
Imports System.Web
Imports System.Collections
Imports System.Web.Services
Imports System.Data.SqlClient
Imports System.Web.Services.Protocols
Imports System.Web.Script.Services
Imports AjaxControlToolkit
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports sasmtp_dotnetproxy
Imports System.Net.Mail
Imports System.Net




<WebService(Namespace:="http://secureit.edictate.com/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<System.Web.Script.Services.ScriptService()> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WebService
    Inherits System.Web.Services.WebService
    <WebMethod(EnableSession:=True)> _
 Public Function GetCompletionList(ByVal prefixText As String) As String()

        Dim strphyList As ArrayList = New ArrayList
        Dim strConn As String
        Dim SQLstring As String

        If Session("WorkGroupID").ToString <> "" Then
            SQLstring = "Select Firstname + ' ' + Lastname as 'uname' from DBO.tblUsers U INNER JOIN tblworkgroupDepartments AS WGA ON U.DepartmentID = WGA.DepartmentID AND WGA.WorkGroupID = '" & Session("WorkGroupID").ToString & "' where Firstname + ' ' + Lastname like '" & prefixText & "%' and (isdeleted is null or isdeleted=0) and ContractorID='" & Session("ContractorID").ToString & "' order by Firstname + ' ' + Lastname"
        Else
            SQLstring = "Select Firstname + ' ' + Lastname as 'uname' from DBO.tblUsers where Firstname + ' ' + Lastname like '" & prefixText & "%' and (isdeleted is null or isdeleted=0) and ContractorID='" & Session("ContractorID").ToString & "' order by Firstname + ' ' + Lastname"
        End If

        'strphyList.Add(prefixText)

        'strphyList.Add("Vishal Raut")

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand(SQLstring, New SqlConnection(strConn))
        cmdIns.Connection.Open()
        Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)

                strphyList.Add(DRRec("uname").ToString)

            End While
        End If
        DRRec.Close()

        strphyList.TrimToSize()

        cmdIns.Connection.Close()
        Return strphyList.ToArray(GetType(String))
        'Return DirectCast(strphyList.ToArray(GetType(String)), String())
    End Function
    <WebMethod(EnableSession:=True)> _
 Public Function GetCompletionAllList(ByVal prefixText As String) As String()

        Dim strphyList As ArrayList = New ArrayList
        Dim strConn As String
        Dim SQLstring As String

        If Session("WorkGroupID").ToString <> "" Then
            SQLstring = "Select Firstname + ' ' + Lastname as 'uname' from DBO.tblUsers U INNER JOIN tblworkgroupDepartments AS WGA ON U.DepartmentID = WGA.DepartmentID AND WGA.WorkGroupID = '" & Session("WorkGroupID").ToString & "' where Firstname + ' ' + Lastname like '" & prefixText & "%' and ContractorID='" & Session("ContractorID").ToString & "' order by Firstname + ' ' + Lastname"
        Else
            SQLstring = "Select Firstname + ' ' + Lastname as 'uname' from DBO.tblUsers where Firstname + ' ' + Lastname like '" & prefixText & "%' and ContractorID='" & Session("ContractorID").ToString & "' order by Firstname + ' ' + Lastname"
        End If

        'strphyList.Add(prefixText)

        'strphyList.Add("Vishal Raut")

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand(SQLstring, New SqlConnection(strConn))
        cmdIns.Connection.Open()
        Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)

                strphyList.Add(DRRec("uname").ToString)

            End While
        End If
        DRRec.Close()

        strphyList.TrimToSize()

        cmdIns.Connection.Close()
        Return strphyList.ToArray(GetType(String))
        'Return DirectCast(strphyList.ToArray(GetType(String)), String())
    End Function

    <WebMethod()> _
    Public Function GetPhyNames(ByVal prefixText As String) As String()

        'Dim strphyList As New ArrayList
        Dim strphyList As ArrayList = New ArrayList
        Dim strConn As String
        'strphyList.Add(prefixText)

        'strphyList.Add("Vishal Raut")

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand("SELECT P.FirstName+' '+P.LastName+'('+convert(varchar(10),A.AccountNo,4)+')' as PhyName FROM tblPhysicians AS P INNER JOIN tblAccounts AS A ON P.AccountID = A.AccountID where (P.IsDeleted is null or P.IsDeleted=0) and (A.IsDeleted is null or A.IsDeleted=0) and P.FirstName+' '+P.LastName like '" & prefixText & "%' order by P.FirstName+' '+P.LastName", New SqlConnection(strConn))
        cmdIns.Connection.Open()
        Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)

                strphyList.Add(DRRec("PhyName").ToString)

            End While
        End If
        DRRec.Close()

        strphyList.TrimToSize()

        cmdIns.Connection.Close()
        Return strphyList.ToArray(GetType(String))
        'Return DirectCast(strphyList.ToArray(GetType(String)), String())
    End Function
    Public Function GetUserName(ByVal prefixText As String, ByVal Level As Long, ByVal ContractorID As String) As String()
        Dim strphyList As ArrayList = New ArrayList
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand("SELECT U.Firstname + ' ' + U.Lastname + '(' + U.UserName + ')' as UserName FROM tblUsers AS U INNER JOIN tblUsersLevels AS UL ON U.UserID = UL.UserID where dbo.chkLevel(UL.ProductionLevel," & Level & ")='true' and U.Firstname + ' ' + U.Lastname like '" & prefixText & "%' and (U.isdeleted is null or U.isdeleted=0) and U.ContractorID='" & ContractorID & "'", New SqlConnection(strConn))
        cmdIns.Connection.Open()
        Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)

                strphyList.Add(DRRec("UserName").ToString)


            End While
        End If
        DRRec.Close()

        strphyList.TrimToSize()

        cmdIns.Connection.Close()


        Return strphyList.ToArray(GetType(String))

    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUsersAllLevel(ByVal prefixText As String) As String()
        Dim strphyList As ArrayList = New ArrayList
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand("SELECT U.Firstname + ' ' + U.Lastname + '(' + U.UserName + ')' as UserName FROM tblUsers AS U INNER JOIN tblUsersLevels AS UL ON U.UserID = UL.UserID where U.Firstname + ' ' + U.Lastname like '" & prefixText & "%' and (U.isdeleted is null or U.isdeleted=0) and U.ContractorID='" & Session("ContractorID").ToString & "'", New SqlConnection(strConn))
        cmdIns.Connection.Open()
        Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)
                strphyList.Add(DRRec("UserName").ToString)
            End While
        End If
        DRRec.Close()

        strphyList.TrimToSize()

        cmdIns.Connection.Close()


        Return strphyList.ToArray(GetType(String))
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function GetUserName1(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 1, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName2(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 2, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName4(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 4, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName8(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 8, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName16(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 16, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName32(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 32, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName64(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 64, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
            Public Function GetUserName128(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 128, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName256(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 256, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName512(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 512, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName1024(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 1024, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName2048(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 2048, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName4096(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 4096, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName8192(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 8192, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName16384(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 16384, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName65536(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 65536, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetUserName131072(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 131072, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
            Public Function GetUserName262144(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 262144, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
            Public Function GetUserName524288(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 524288, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
                Public Function GetUserName1048576(ByVal prefixText As String) As String()
        Return GetUserName(prefixText, 1048576, Session("ContractorID").ToString)
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function GetUserID(ByVal prefixText As String) As String()

        'Dim strphyList As New ArrayList
        Dim strphyList As ArrayList = New ArrayList
        Dim strConn As String
        Dim SQLString As String
        'strphyList.Add(prefixText)
        If Session("WorkGroupID").ToString <> "" Then
            SQLString = "Select username from DBO.tblUsers U INNER JOIN tblworkgroupDepartments AS WGA ON U.DepartmentID = WGA.DepartmentID AND WGA.WorkGroupID = '" & Session("WorkGroupID").ToString & "' where username like '" & prefixText & "%' and ContractorID='" & Session("ContractorID").ToString & "' order by username"
        Else
            SQLString = "Select username from DBO.tblUsers where username like '" & prefixText & "%' and ContractorID='" & Session("ContractorID").ToString & "' order by username"
        End If
        'strphyList.Add("Vishal Raut")
        'and ContractorID='" & Session("ContractorID") & "'
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand(SQLString, New SqlConnection(strConn))
        cmdIns.Connection.Open()
        Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)

                strphyList.Add(DRRec("username").ToString)

            End While
        End If
        DRRec.Close()

        strphyList.TrimToSize()

        cmdIns.Connection.Close()


        Return strphyList.ToArray(GetType(String))





        'Return DirectCast(strphyList.ToArray(GetType(String)), String())










    End Function
    <WebMethod(EnableSession:=True)> _
        Public Function GetAuditorID(ByVal prefixText As String) As String()

        'Dim strphyList As New ArrayList
        Dim strphyList As ArrayList = New ArrayList
        Dim strConn As String
        'strphyList.Add(prefixText)

        'strphyList.Add("Vishal Raut")

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand("SELECT U.UserName FROM tblUsers AS U INNER JOIN tblUsersLevels AS UL ON U.UserID = UL.UserID,(select audit from tblRSSStatus where iscontractor=1) as AL where username like '" & prefixText & "%' and dbo.chklevel(UL.ProductionLevel,AL.Audit)=1 and U.ContractorID='" & Session("ContractorID").ToString & "' order by username", New SqlConnection(strConn))

        cmdIns.Connection.Open()
        Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)

                strphyList.Add(DRRec("username").ToString)

            End While
        End If
        DRRec.Close()

        strphyList.TrimToSize()

        cmdIns.Connection.Close()


        Return strphyList.ToArray(GetType(String))





        'Return DirectCast(strphyList.ToArray(GetType(String)), String())










    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function GetDepartment(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()



        Dim strDeptList As ArrayList = New ArrayList
        Dim strConn As String
        Dim values As New List(Of CascadingDropDownNameValue)


        'strphyList.Add(prefixText)

        'strphyList.Add("Vishal Raut")

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand("Select name, departmentid from DBO.tblDepartments where ContractorID='" & Session("ContractorID").ToString & "'", New SqlConnection(strConn))
        cmdIns.Connection.Open()
        Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)

                values.Add(New CascadingDropDownNameValue(DRRec("name"), DRRec("departmentid").ToString))



            End While
        End If
        DRRec.Close()

        'strDeptList.TrimToSize()

        cmdIns.Connection.Close()


        Return values.ToArray()







        'Return DirectCast(strphyList.ToArray(GetType(String)), String())







    End Function

    Public Function GetUsersCategory(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()



        Dim strCatList As ArrayList = New ArrayList
        Dim strConn As String
        Dim values As New List(Of CascadingDropDownNameValue)


        'strphyList.Add(prefixText)

        'strphyList.Add("Vishal Raut")

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand("Select name, categoryid from DBO.tblUsersCategory", New SqlConnection(strConn))
        cmdIns.Connection.Open()
        Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)
                values.Add(New CascadingDropDownNameValue(DRRec("name"), DRRec("Categoryid").ToString))
            End While
        End If
        DRRec.Close()

        'strDeptList.TrimToSize()

        cmdIns.Connection.Close()


        Return values.ToArray()







        'Return DirectCast(strphyList.ToArray(GetType(String)), String())







    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function GetDepartmentDesn(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()


        Dim kv As StringDictionary = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)


        Dim strDeptList As ArrayList = New ArrayList
        Dim strConn As String
        Dim values As New List(Of CascadingDropDownNameValue)


        'strphyList.Add(prefixText)

        'strphyList.Add("Vishal Raut")

        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand("SELECT DD.Name, DD.DesignationID FROM tblDeptDesignations AS DD INNER JOIN tblDepartments AS D ON DD.DepartmentID = D.DepartmentID where DD.departmentid = '" & kv("Make") & "' and D.ContractorID='" & Session("ContractorID").ToString & "'", New SqlConnection(strConn))
        cmdIns.Connection.Open()
        Dim DRRec As SqlDataReader = cmdIns.ExecuteReader()
        If DRRec.HasRows Then
            While (DRRec.Read)

                values.Add(New CascadingDropDownNameValue(DRRec("name"), DRRec("designationid").ToString))



            End While
        End If
        DRRec.Close()

        'strDeptList.TrimToSize()

        cmdIns.Connection.Close()


        Return values.ToArray()







        'Return DirectCast(strphyList.ToArray(GetType(String)), String())







    End Function
    <WebMethod()> _
    Public Function SendMailAlerts(ByVal strFrom As String, ByVal strTo As String, ByVal strSubject As String, ByVal strBody As String) As String

        Try
            
			Dim message As New MailMessage()
            Dim fromName As String = "Do Not Reply"
            Dim from As String = "donotreply@medofficepro.com"
            Dim toAddress As String = strTo.ToString
            'Dim bccaddress As String = "sdoxreg@edictate.com"
            Dim smtpadd As String = "email-smtp.us-west-2.amazonaws.com"
            Dim smtpuname As String = "AKIA44IE6PBA24MEZW5P"
            Dim smtppass As String = "BLZJ9U1M6AILVx4FRA8E1CdRvOoRV9rx7/HBXEcNaeJ6"
            Dim port As Integer = 587
            Dim subject As String = "MModal - " & strSubject
            Dim configset As String = "ConfigSet"
			
			message.IsBodyHtml = True
            message.Body = strBody.ToString
            message.From = New MailAddress(from, fromName)
            message.To.Add(New MailAddress(toAddress))

            message.Subject = subject
            'message.Headers.Add("X-SES-CONFIGURATION-SET", configset)
            Dim client As New System.Net.Mail.SmtpClient(smtpadd, port)

            client.Credentials = New NetworkCredential(smtpuname, smtppass)
            client.EnableSsl = True
            client.Send(message)
            Return True

			'Dim iMail As New SASMTPLib.CoSMTPMail()
            'iMail.RemoteHost = "email-smtp.us-west-2.amazonaws.com"
            'iMail.UserName = "AKIA44IE6PBA24MEZW5P"
            'iMail.Password = "BLZJ9U1M6AILVx4FRA8E1CdRvOoRV9rx7/HBXEcNaeJ6"
            ''iMail.Port = 25
            'iMail.FromAddress = strFrom
            'iMail.AddRecipient(strTo, strTo)
            'iMail.ReturnReceipt = False
            'iMail.Subject = "MModal - " & strSubject
            'iMail.BodyText = strBody
            'iMail.SendMail()
            'Return iMail.Response

        Catch ex As Exception
            Return ex.Message
            Exit Function

        End Try

    End Function
    <WebMethod(EnableSession:=True)> _
            Public Function SetContSession(ByVal ContractorID As String) As Boolean
        Try
            Session("ContractorID") = ContractorID
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Public Function getContSession() As String
        Try
            Return Session("ContractorID").ToString
        Catch ex As Exception
            Return ""
        End Try


    End Function

End Class
