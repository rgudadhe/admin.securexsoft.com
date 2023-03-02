<%@ Page Language="VB" %>


<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ConString, SQLString As String
        Dim intCheckOutLimit As Integer
        Dim intViewLimit As Integer
        Dim NumOfChkedOut As Integer
        Dim checkedOutQuery As String = String.Empty
        Dim AvailJobQuery As String = String.Empty
        Dim strUserID As String = String.Empty
        Dim intLevel As Long = 1
        strUserID = Request.Form("UserID").ToString
        If String.IsNullOrEmpty(strUserID) Then
            Response.Write("0")
            Exit Sub
        End If
        
        intLevel = CLng(Request.Form("Level").ToString)
        If intLevel = 0 Then
            Response.Write("1")
            Exit Sub
        End If
        
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        Try
            oConn.ConnectionString = ConString
            oConn.Open()
            Dim SelectClause As String = "SELECT distinct TM.TranscriptionID, TM.JobNumber, TM.CustJobID, FxStatus.Status as StatusName, TM.Duration, convert(char, TM.SubmitDate, 101) as SubmitDate , " & _
                               "convert(char, TM.DueDate, 101) as DueDate, TM.Priority, P.FirstName, P.LastName, A.AccountName"
            Dim FromClause As String = " FROM tblTranscriptionMain AS TM INNER JOIN " & _
                        "tblPhysicians AS P ON TM.DictatorID = P.PhysicianID INNER JOIN " & _
                        "tblAccounts AS A ON TM.AccountID = A.AccountID INNER JOIN " & _
                        "dbo.getStatus() AS FxStatus ON TM.Status = FxStatus.LevelNo"
            '"tblTranscriptionLog AS TL ON TL.TranscriptionID = TM.TranscriptionID INNER JOIN " & _
            checkedOutQuery = checkedOutList(SelectClause, FromClause, strUserID, intLevel)
            NumOfChkedOut = getCheckedOutCount(strUserID, intLevel, oConn)
            intCheckOutLimit = getCheckedOutLimit(strUserID, intLevel, oConn)
            intViewLimit = getViewLimit(strUserID, intLevel, oConn)
            If NumOfChkedOut < intCheckOutLimit Then
                AvailJobQuery = AvailJobList(SelectClause, FromClause, strUserID, intLevel, intViewLimit)
                SQLString = checkedOutQuery & " UNION " & AvailJobQuery
            Else
                SQLString = checkedOutQuery
            End If
            
            
            'Response.Write(SQLString)
            'Response.End()
            'Dim oCommand As New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Adapter.SelectCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            Dim myDs As New Data.DataSet
            
            Adapter.Fill(myDs, "TestData")
            Dim sw As New IO.StringWriter
            myDs.WriteXml(sw)
            Response.Write(sw.ToString)
            'Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
            'oRec.Read()
            'If oRec.HasRows Then
            '    strReponse = oRec("TranscriptionID").ToString & "|" & oRec("JobNumber") & "|" & oRec("CustJobID") & "|" & oRec("Status")
            '    Do While oRec.Read
            '        strReponse = strReponse & "^" & oRec("TranscriptionID").ToString & "|" & oRec("JobNumber") & "|" & oRec("CustJobID") & "|" & oRec("Status")
            '    Loop
            'End If
            'oRec.Close()
            oConn.Close()
            'Response.Write(strReponse.ToString)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Function checkedOutList(ByVal SelectClause As String, ByVal FromClause As String, ByVal UserID As String, ByVal Level As Integer) As String
        Dim WhereClauseCK As String = String.Empty
        WhereClauseCK = " inner join (SELECT tblLog.UserID,tblLog.UserLevel,tblLog.TranscriptionID FROM tblTranscriptionLog as tblLog LEFT OUTER JOIN tblUsers ON tblLog.UserID = tblUsers.UserID WHERE tblUsers.UserId='" & UserID & "' and tblLog.Status IN (SELECT LevelNo+100 FROM tblProductionLevels where LevelNo=" & Level & ") and tblLog.DateModified = (SELECT MAX(DateModified) AS MaxDate FROM tblTranscriptionLog where transcriptionid=tblLog.TranscriptionID)) as U on U.TranscriptionID = TM.TranscriptionID"
        checkedOutList = SelectClause & FromClause & WhereClauseCK
    End Function
    Private Function AvailJobList(ByVal SelectClause As String, ByVal FromClause As String, ByVal UserId As String, ByVal Level As Integer, ByVal limit As Integer) As String
        Dim WhereClauseAvail As String = String.Empty
        AvailJobList = "select distinct top " & limit & " * " & _
                       "from (" & _
                       DictatorJobList(SelectClause, FromClause, UserId, Level) & _
                       " UNION " & _
                       AccountsJobList(SelectClause, FromClause, UserId, Level) & _
                       ") foo"
                       
    End Function
    Private Function DictatorJobList(ByVal SelectClause As String, ByVal FromClause As String, ByVal UserId As String, ByVal Level As Integer) As String
        DictatorJobList = SelectClause & FromClause & " inner join (select PhysicianID from tblUserPrLvlMgmt where userid='" & UserId & "' and LevelNo=" & Level & ") as UP on P.PhysicianID = UP.PhysicianID where TM.Status=" & Level
    End Function
    Private Function AccountsJobList(ByVal SelectClause As String, ByVal FromClause As String, ByVal UserId As String, ByVal Level As Integer) As String
        AccountsJobList = SelectClause & FromClause & " inner join (SELECT  P.PhysicianID FROM tblAccountUserAssgn AS UA INNER JOIN tblPhysicians AS P ON UA.AccountID = P.AccountID WHERE (UA.userid = '" & UserId & "' and UA.LevelNo=" & Level & ")) as AP on P.PhysicianID = AP.PhysicianID where TM.Status=" & Level
    End Function
    Private Function getCheckedOutCount(ByVal strUserID As String, ByVal intLevel As Integer, ByVal objConn As Data.SqlClient.SqlConnection) As Integer
        Dim sqlString As String = "select DBO.getChkedOutCount('" & strUserID & "'," & intLevel & ") as rCount"    '"select count(TranscriptionID) as rCount from (" & strQuery & ") as foo"
        Dim oCommand As New Data.SqlClient.SqlCommand(sqlString, objConn)
        Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
        oRec.Read()
        If oRec.HasRows Then
            getCheckedOutCount = CInt(oRec("rCount").ToString)
        Else
            getCheckedOutCount = 0
        End If
        oRec.Close()
    End Function
    Private Function getCheckedOutLimit(ByVal strUserID As String, ByVal intLevel As Integer, ByVal objConn As Data.SqlClient.SqlConnection) As Integer
        Dim sqlString As String = "select DBO.getCheckedLimit('" & strUserID & "'," & intLevel & ") as cCount"
        Dim oCommand As New Data.SqlClient.SqlCommand(sqlString, objConn)
        Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
        oRec.Read()
        If oRec.HasRows Then
            If Not String.IsNullOrEmpty(oRec("cCount").ToString) Then
                getCheckedOutLimit = CInt(oRec("cCount").ToString)
            Else
                getCheckedOutLimit = 0
            End If
        Else
            getCheckedOutLimit = 0
        End If
            oRec.Close()
    End Function
    Private Function getViewLimit(ByVal strUserID As String, ByVal intLevel As Integer, ByVal objConn As Data.SqlClient.SqlConnection) As Integer
        Dim sqlString As String = "select DBO.getViewLimit('" & strUserID & "'," & intLevel & ") as vCount"
        Dim oCommand As New Data.SqlClient.SqlCommand(sqlString, objConn)
        Dim oRec As Data.SqlClient.SqlDataReader = oCommand.ExecuteReader
        oRec.Read()
        If oRec.HasRows Then
            If Not String.IsNullOrEmpty(oRec("vCount").ToString) Then
                getViewLimit = CInt(oRec("vCount").ToString)
            Else
                getViewLimit = 0
            End If
        Else
            getViewLimit = 0
        End If
        oRec.Close()
    End Function
</script>


