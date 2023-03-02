Imports System.Data.Sqlclient
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.Security
Partial Class MIS_DailyMins
    Inherits BasePage
    Protected Sub ShowActDetails()
        Dim strConn As String
        Dim strDate As String
        Dim strCategory As String
        strCategory = ""
        Dim InpDate As Date
        If TxtDate.Text = "" Then
            strDate = Now.ToShortDateString
        Else
            strDate = TxtDate.Text
        End If
        InpDate = Date.Parse(strDate)
        Lbldate.Text = InpDate.ToShortDateString
        Dim CurrSDate As Date
        Dim CurrEDate As Date
        'Dim PrvWDays As Integer
        Dim CurrWDays As Integer
        CurrSDate = Month(InpDate) & "/1/" & Year(InpDate)

        'If Month(CurrSDate) = Month(Now) Then
        '    CurrEDate = Now.ToShortDateString
        'Else
        CurrEDate = DateAdd(DateInterval.Day, 1, InpDate)
        'End If


        CurrWDays = WorkingDays(CurrSDate, CurrEDate)
        'Dim PrvSDate As Date
        'Dim PrvEDate As Date

        'PrvSDate = DateAdd(DateInterval.Month, -1, CurrSDate)
        'PrvEDate = DateAdd(DateInterval.Day, -1, CurrSDate)
        'PrvWDays = WorkingDays(PrvSDate, PrvEDate)


        ' Response.Write(CurrWDays & "#" & PrvWDays)
        '  Response.Write(CurrSDate & "#" & CurrEDate & "#")
        '  Response.Write(PrvSDate & "#" & PrvEDate)
        Dim t2 As New Table
        t2.Style("width") = "100%"
        t2.BorderWidth = 2
        t2.GridLines = GridLines.Both
        Dim I As Integer
        I = 0
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim strQuery As String
        Dim STMTLines As Integer
        Dim STMTPLines As Integer
        Dim STQALines As Integer
        Dim STPPQALines As Integer
        Dim STMTBLines As Integer
        Dim STQABLines As Integer
        Dim STQABSELines As Integer
        Dim SDTotalLines As Integer
        Dim SMTotalLines As Integer
        Dim SMTotalTrg As Integer
        Dim SDTotalTrg As Integer
        STMTLines = 0
        STMTPLines = 0
        STQALines = 0
        STPPQALines = 0
        STMTBLines = 0
        STQABLines = 0
        STQABSELines = 0
        SDTotalLines = 0
        SMTotalLines = 0
        SMTotalTrg = 0
        SDTotalTrg = 0
        Dim TAvgPreMins As Integer
        Dim TAvgCurrMins As Integer
        Dim TDayMins1 As Integer
        Dim TDayMins2 As Integer
        Dim TDayMins3 As Integer
        Dim TDayMins4 As Integer
        Dim TDayMins5 As Integer
        Dim TDayMins6 As Integer
        Dim TDayMins7 As Integer
        Dim DailyTLines As Long
        Dim MonTLines As Long
        Dim DailyTrg As Long
        Dim MonTrg As Long
        DailyTrg = 0
        MonTrg = 0
        DailyTLines = 0
        MonTLines = 0
        TAvgPreMins = 0
        TAvgCurrMins = 0
        TDayMins1 = 0
        TDayMins2 = 0
        TDayMins3 = 0
        TDayMins4 = 0
        TDayMins5 = 0
        TDayMins6 = 0
        TDayMins7 = 0
        strQuery = "SELECT UNAME, USERNAME, TRGLINES, DNAME"
        strQuery = strQuery & " ,ISNULL(SUM(MTLINES),0) AS MTLINES "
        strQuery = strQuery & " ,ISNULL(SUM(MTPLINES),0) AS MTPLINES "
        strQuery = strQuery & " ,ISNULL(SUM(QALINES),0) AS QALINES "
        strQuery = strQuery & " ,ISNULL(SUM(BBLINES),0) AS BBLINES "
        strQuery = strQuery & " ,ISNULL(SUM(PPQALINES),0) AS PPQALINES "
        strQuery = strQuery & " ,ISNULL(SUM(MTBLINES),0) AS MTBLINES "
        strQuery = strQuery & " ,ISNULL(SUM(QABSELINES),0) AS QABSELINES "
        strQuery = strQuery & " ,ISNULL(SUM(TMTLINES),0) AS TMTLINES "
        strQuery = strQuery & " ,ISNULL(SUM(TMTPLINES),0) AS TMTPLINES "
        strQuery = strQuery & " ,ISNULL(SUM(TQALINES),0) AS TQALINES "
        strQuery = strQuery & " ,ISNULL(SUM(TBBLINES),0) AS TBBLINES "
        strQuery = strQuery & " ,ISNULL(SUM(TPPQALINES),0) AS TPPQALINES "
        strQuery = strQuery & " ,ISNULL(SUM(TMTBLINES),0) AS TMTBLINES "
       
        strQuery = strQuery & " ,ISNULL(SUM(TQABSELINES),0) AS TQABSELINES, ACCOUNTNAME FROM ( "
        strQuery = strQuery & "SELECT U.FIRSTNAME + ' ' + U.LASTNAME AS UNAME, U.USERNAME, U.TRGLINES, D.NAME AS DNAME, ISNULL(A.ACCOUNTNAME, 'Direct Accounts') AS ACCOUNTNAME "
        strQuery = strQuery & " ,ISNULL(TS1.TSLINES,0)+ISNULL(TS6.TSLINES,0) AS MTLINES"
        strQuery = strQuery & " ,ISNULL(TS2.TSLINES,0) AS MTPLINES"
        strQuery = strQuery & " ,ISNULL(TS3.TSLINES,0) + ISNULL(TS10.TSLINES,0) AS QALINES"
        strQuery = strQuery & " ,ISNULL(TS4.TSLINES,0) AS BBLINES"
        strQuery = strQuery & " ,ISNULL(TS5.TSLINES,0) AS PPQALINES"
        strQuery = strQuery & " ,ISNULL(TS7.TSLINES,0) AS MTBLINES"
        strQuery = strQuery & " ,ISNULL(TS9.TSLINES,0) AS QABSELINES"
        strQuery = strQuery & " ,ISNULL(TS11.TSLINES,0)+ISNULL(TS16.TSLINES,0) AS TMTLINES"
        strQuery = strQuery & " ,ISNULL(TS12.TSLINES,0) AS TMTPLINES"
        strQuery = strQuery & " ,ISNULL(TS13.TSLINES,0) + ISNULL(TS20.TSLINES,0) AS TQALINES"
        strQuery = strQuery & " ,ISNULL(TS14.TSLINES,0) AS TBBLINES"
        strQuery = strQuery & " ,ISNULL(TS15.TSLINES,0) AS TPPQALINES"
        strQuery = strQuery & " ,ISNULL(TS17.TSLINES,0) AS TMTBLINES"
        strQuery = strQuery & " ,ISNULL(TS19.TSLINES,0) AS TQABSELINES"
        strQuery = strQuery & " FROM TBLUSERS U"
        strQuery = strQuery & " LEFT JOIN TBLDEPTDESIGNATIONS D ON U.DESIGNATIONID = D.DESIGNATIONID"
        strQuery = strQuery & " LEFT JOIN (SELECT ACCOUNTID, ACCOUNTNAME FROM TBLACCOUNTS WHERE INDIRECT='TRUE') A ON A.ACCOUNTID = U.PLATACCID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND TS.USERLEVEL = '1' AND  TS.STATUS IN ('2') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY TS.USERID ) AS TS1 "
        strQuery = strQuery & " ON TS1.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND TS.USERLEVEL = '1' AND  TS.STATUS NOT IN ('2', '8') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY TS.USERID ) AS TS2 "
        strQuery = strQuery & " ON TS2.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND TS.USERLEVEL = '2'  AND  TS.STATUS NOT IN ('8') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY TS.USERID ) AS TS3"
        strQuery = strQuery & " ON TS3.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID NOT IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND "
        strQuery = strQuery & " TS.USERLEVEL = '8'  "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY TS.USERID ) AS TS4"
        strQuery = strQuery & " ON TS4.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND "
        strQuery = strQuery & " TS.USERLEVEL = '8'  "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY TS.USERID ) AS TS10"
        strQuery = strQuery & " ON TS10.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND TS.USERLEVEL = '8192'  "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY TS.USERID ) AS TS5"
        strQuery = strQuery & " ON TS5.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND "
        strQuery = strQuery & " TS.USERLEVEL = '1' AND  TS.STATUS IN ('8') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY TS.USERID ) AS TS6 "
        strQuery = strQuery & " ON TS6.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID NOT IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND"
        strQuery = strQuery & " TS.USERLEVEL = '1' AND  TS.STATUS IN ('8') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY TS.USERID ) AS TS7 "
        strQuery = strQuery & " ON TS7.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND "
        strQuery = strQuery & " TS.USERLEVEL = '2' AND  TS.STATUS IN ('8') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY TS.USERID ) AS TS8 "
        strQuery = strQuery & " ON TS8.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID NOT IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND"
        strQuery = strQuery & " TS.USERLEVEL = '2' AND  TS.STATUS IN ('8') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY TS.USERID ) AS TS9 "
        strQuery = strQuery & " ON TS9.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND TS.USERLEVEL = '1' AND  TS.STATUS IN ('2') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY TS.USERID ) AS TS11 "
        strQuery = strQuery & " ON TS11.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND TS.USERLEVEL = '1' AND  TS.STATUS NOT IN ('2','8') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY TS.USERID ) AS TS12 "
        strQuery = strQuery & " ON TS12.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND TS.USERLEVEL = '2'  "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY TS.USERID ) AS TS13"
        strQuery = strQuery & " ON TS13.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID NOT IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND"
        strQuery = strQuery & " TS.USERLEVEL = '8'  "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY TS.USERID ) AS TS14"
        strQuery = strQuery & " ON TS14.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND"
        strQuery = strQuery & " TS.USERLEVEL = '8' "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY TS.USERID ) AS TS20"
        strQuery = strQuery & " ON TS20.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND TS.USERLEVEL = '8192' "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY TS.USERID ) AS TS15"
        strQuery = strQuery & " ON TS15.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND"
        strQuery = strQuery & " TS.USERLEVEL = '1' AND  TS.STATUS IN ('8') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY TS.USERID ) AS TS16 "
        strQuery = strQuery & " ON TS16.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID NOT IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND"
        strQuery = strQuery & " TS.USERLEVEL = '1' AND  TS.STATUS IN ('8') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY TS.USERID ) AS TS17 "
        strQuery = strQuery & " ON TS17.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND"
        strQuery = strQuery & " TS.USERLEVEL = '2' AND  TS.STATUS IN ('8')"
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY TS.USERID ) AS TS18"
        strQuery = strQuery & " ON TS18.USERID = U.USERID"
        strQuery = strQuery & " LEFT JOIN (SELECT TS.USERID, SUM(TS.LINECOUNT) AS TSLINES FROM TBLTRANSCRIPTIONSTATUS TS, TBLTRANSCRIPTIONMAIN T"
        strQuery = strQuery & " WHERE T.TRANSCRIPTIONID NOT IN (SELECT TRANSCRIPTIONID FROM TBLAUDITEPTL) AND TS.TRANSCRIPTIONID = T.TRANSCRIPTIONID AND"
        strQuery = strQuery & " TS.USERLEVEL = '2' AND  TS.STATUS IN ('8') "
        strQuery = strQuery & " AND T.DATEMODIFIED BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY TS.USERID ) AS TS19 "
        strQuery = strQuery & " ON TS19.USERID = U.USERID"
        strQuery = strQuery & " Where D.DEPARTMENTID = '2D0892BB-BF03-4652-83E3-B517EF7BA2CE' "
        If DLUsers.SelectedValue <> "" Then
            strQuery = strQuery & " And U.USERID = '" & DLUsers.SelectedValue & "' "
        End If
        'strQuery = strQuery & " ORDER BY D.NAME"
        strQuery = strQuery & " UNION SELECT U.FIRSTNAME + ' ' + U.LASTNAME AS UNAME, U.USERNAME, U.TRGLINES, D.NAME AS DNAME, ISNULL(A.ACCOUNTNAME, 'Direct Accounts') AS ACCOUNTNAME "
        strQuery = strQuery & " ,ISNULL(TS1.TSLINES,0) AS MTLINES"
        strQuery = strQuery & " ,ISNULL(TS2.TSLINES,0) AS MTPLINES"
        strQuery = strQuery & " ,ISNULL(TS3.TSLINES,0) AS QALINES"
        strQuery = strQuery & " ,ISNULL(TS4.TSLINES,0) AS BBLINES"
        strQuery = strQuery & " ,ISNULL(TS5.TSLINES,0) AS PPQALINES"
        strQuery = strQuery & " ,ISNULL(TS7.TSLINES,0) AS MTBLINES"
        strQuery = strQuery & " ,ISNULL(TS9.TSLINES,0) AS QABSELINES"
        strQuery = strQuery & " ,ISNULL(TS11.TSLINES,0) AS TMTLINES"
        strQuery = strQuery & " ,ISNULL(TS12.TSLINES,0) AS TMTPLINES"
        strQuery = strQuery & " ,ISNULL(TS13.TSLINES,0) AS TQALINES"
        strQuery = strQuery & " ,ISNULL(TS14.TSLINES,0) AS TBBLINES"
        strQuery = strQuery & " ,ISNULL(TS15.TSLINES,0) AS TPPQALINES"
        strQuery = strQuery & " ,ISNULL(TS17.TSLINES,0) AS TMTBLINES"
        strQuery = strQuery & " ,ISNULL(TS19.TSLINES,0) AS TQABSELINES"
        strQuery = strQuery & " FROM TBLUSERS U"
        strQuery = strQuery & " LEFT JOIN (SELECT ACCOUNTID, ACCOUNTNAME FROM TBLACCOUNTS WHERE INDIRECT='TRUE') A ON A.ACCOUNTID = U.PLATACCID"
        strQuery = strQuery & " LEFT JOIN TBLDEPTDESIGNATIONS D ON U.DESIGNATIONID = D.DESIGNATIONID"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE TS.USERLEVEL = 'MT' "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY  TS.USERNAME ) AS TS1 "
        strQuery = strQuery & " ON TS1.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE  TS.USERLEVEL = 'MT PLUS'  "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY  TS.USERNAME ) AS TS2 "
        strQuery = strQuery & " ON TS2.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE  TS.USERLEVEL = 'QA'  "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY  TS.USERNAME ) AS TS3"
        strQuery = strQuery & " ON TS3.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE  TS.USERLEVEL = 'BB'   "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY  TS.USERNAME ) AS TS4"
        strQuery = strQuery & " ON TS4.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE  TS.USERLEVEL = 'PPQA'   "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY  TS.USERNAME ) AS TS5"
        strQuery = strQuery & " ON TS5.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE   TS.USERLEVEL = 'MTB'  "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY  TS.USERNAME ) AS TS7 "
        strQuery = strQuery & " ON TS7.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE TS.USERLEVEL = 'QABSE' "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN '" & InpDate & "' AND '" & CurrEDate & "' GROUP BY  TS.USERNAME ) AS TS9 "
        strQuery = strQuery & " ON TS9.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE TS.USERLEVEL = 'MT'  "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY  TS.USERNAME ) AS TS11 "
        strQuery = strQuery & " ON TS11.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE  TS.USERLEVEL = 'MT PLUS'  "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY  TS.USERNAME ) AS TS12 "
        strQuery = strQuery & " ON TS12.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE  TS.USERLEVEL = 'QA'  "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY  TS.USERNAME ) AS TS13"
        strQuery = strQuery & " ON TS13.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE   TS.USERLEVEL = 'BB'    "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY  TS.USERNAME ) AS TS14"
        strQuery = strQuery & " ON TS14.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE   TS.USERLEVEL = 'PPQA'   "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY  TS.USERNAME ) AS TS15"
        strQuery = strQuery & " ON TS15.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE   TS.USERLEVEL = 'MTB'   "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY  TS.USERNAME ) AS TS17 "
        strQuery = strQuery & " ON TS17.USERNAME = U.USERNAME"
        strQuery = strQuery & " LEFT JOIN (SELECT  TS.USERNAME, SUM(TS.LINES) AS TSLINES FROM TBLUSERPLATLINES TS "
        strQuery = strQuery & " WHERE   TS.USERLEVEL = 'QABSE'   "
        strQuery = strQuery & " AND TS.POSTDATE BETWEEN  '" & CurrSDate & "' AND '" & CurrEDate & "'  GROUP BY  TS.USERNAME ) AS TS19 "
        strQuery = strQuery & " ON TS19.USERNAME = U.USERNAME"
        strQuery = strQuery & " Where D.DEPARTMENTID = '2D0892BB-BF03-4652-83E3-B517EF7BA2CE'  and (U.Isdeleted = 'False' or U.Isdeleted is NULL) "
        If DLUsers.SelectedValue <> "" Then
            strQuery = strQuery & " And U.USERNAME = '" & DLUsers.SelectedValue & "' "
        End If
        If DLGroup.SelectedValue = "Platform" Then
            strQuery = strQuery & " ) T GROUP BY UNAME, USERNAME, TRGLINES, DNAME, ACCOUNTNAME ORDER BY ACCOUNTNAME "
        Else
            strQuery = strQuery & " ) T GROUP BY UNAME, USERNAME, TRGLINES, DNAME, ACCOUNTNAME ORDER BY DNAME "
        End If
        'Response.Write(strQuery)
        'Response.End()

        Dim SQLCmdT As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            SQLCmdT.Connection.Open()
            Dim DRRecT As SqlDataReader = SQLCmdT.ExecuteReader()
            If DRRecT.HasRows Then
                While DRRecT.Read
                    I = I + 1
                    If I = 1 Then
                        If DLGroup.SelectedValue = "Platform" Then
                            strCategory = DRRecT("ACCOUNTNAME").ToString
                        Else
                            strCategory = DRRecT("DNAME").ToString
                        End If
                        Dim Row2 As New TableRow
                        Row2.HorizontalAlign = HorizontalAlign.Center
                        ' Row2.CssClass = "tblbgbody"
                        Row2.Font.Bold = True
                        Row2.Font.Italic = True
                        Row2.BackColor = Drawing.Color.CornflowerBlue
                        Row2.ForeColor = Drawing.Color.WhiteSmoke
                        Row2.Font.Size = "10"
                        Dim CatCell As New TableCell
                        CatCell.ColumnSpan = "18"
                        If DLGroup.SelectedValue = "Platform" Then
                            CatCell.Text = DRRecT("ACCOUNTNAME").ToString
                        Else
                            CatCell.Text = DRRecT("DNAME").ToString
                        End If

                        Row2.Cells.Add(CatCell)
                        tblMins.Rows.Add(Row2)
                    Else
                        If DLGroup.SelectedValue = "Platform" Then
                            If strCategory <> DRRecT("ACCOUNTNAME").ToString Then
                                Dim ARow1 As New TableRow
                                Dim ACell1 As New TableCell
                                Dim ACell2 As New TableCell
                                Dim ACell3 As New TableCell
                                Dim ACell4 As New TableCell
                                Dim ACell5 As New TableCell
                                Dim ACell6 As New TableCell
                                Dim ACell7 As New TableCell
                                Dim ACell8 As New TableCell
                                Dim ACell9 As New TableCell
                                Dim ACell10 As New TableCell
                                Dim ACell11 As New TableCell
                                Dim ACellM1 As New TableCell
                                Dim ACellM2 As New TableCell
                                Dim ACellM3 As New TableCell
                                Dim ACellM4 As New TableCell
                                Dim ACellM5 As New TableCell
                                ARow1.Font.Size = "9.5"
                                ACellM1.ColumnSpan = 3
                                '    ACellM1.Text = "-"
                                ACellM2.Text = "-"
                                ACellM3.Text = "-"
                                ACellM4.Text = "-"
                                ACellM5.Text = "-"
                                ARow1.HorizontalAlign = HorizontalAlign.Right
                                'ARow1.CssClass = "tblbg"
                                'ARow1.Font.Bold = True
                                ACellM1.Text = "SubTotal"
                                ACell1.Text = STMTLines
                                ACell2.Text = STMTPLines
                                ACell3.Text = STQALines
                                ACell4.Text = STMTBLines
                                ACell5.Text = STQABLines
                                ACell6.Text = STQABSELines
                                ACell7.Text = STPPQALines
                                ACell8.Text = SDTotalLines
                                ACell9.Text = SDTotalTrg
                                ACell10.Text = SMTotalLines
                                ACell11.Text = SMTotalTrg

                                ARow1.Cells.Add(ACellM1)
                                ARow1.Cells.Add(ACell1)
                                ARow1.Cells.Add(ACell2)
                                ARow1.Cells.Add(ACell3)
                                ARow1.Cells.Add(ACell4)
                                ARow1.Cells.Add(ACell5)
                                ARow1.Cells.Add(ACell6)
                                ARow1.Cells.Add(ACell7)
                                ARow1.Cells.Add(ACell8)
                                ARow1.Cells.Add(ACell9)
                                ARow1.Cells.Add(ACellM2)
                                ARow1.Cells.Add(ACellM3)
                                ARow1.Cells.Add(ACell10)
                                ARow1.Cells.Add(ACell11)
                                ARow1.Cells.Add(ACellM4)
                                ARow1.Cells.Add(ACellM5)
                                tblMins.Rows.Add(ARow1)
                                If DLGroup.SelectedValue = "Platform" Then
                                    strCategory = DRRecT("ACCOUNTNAME").ToString
                                Else
                                    strCategory = DRRecT("DNAME").ToString
                                End If

                                Dim Row2 As New TableRow
                                Row2.HorizontalAlign = HorizontalAlign.Center
                                ' Row2.CssClass = "tblbgbody"
                                Row2.Font.Bold = True
                                Row2.Font.Italic = True
                                'Row2.ForeColor = Drawing.Color.White
                                Row2.BackColor = Drawing.Color.CornflowerBlue
                                Row2.ForeColor = Drawing.Color.WhiteSmoke
                                Row2.Font.Size = "9"
                                Dim CatCell As New TableCell
                                CatCell.ColumnSpan = "18"
                                If DLGroup.SelectedValue = "Platform" Then
                                    CatCell.Text = DRRecT("ACCOUNTNAME").ToString
                                Else
                                    CatCell.Text = DRRecT("DNAME").ToString
                                End If

                                Row2.Cells.Add(CatCell)
                                tblMins.Rows.Add(Row2)
                                STMTLines = 0
                                STMTPLines = 0
                                STQALines = 0
                                STPPQALines = 0
                                STMTBLines = 0
                                STQABLines = 0
                                STQABSELines = 0
                                SDTotalLines = 0
                                SMTotalLines = 0
                                SMTotalTrg = 0
                                SDTotalTrg = 0

                            End If
                        Else
                            If strCategory <> DRRecT("DNAME").ToString Then
                                Dim ARow1 As New TableRow
                                Dim ACell1 As New TableCell
                                Dim ACell2 As New TableCell
                                Dim ACell3 As New TableCell
                                Dim ACell4 As New TableCell
                                Dim ACell5 As New TableCell
                                Dim ACell6 As New TableCell
                                Dim ACell7 As New TableCell
                                Dim ACell8 As New TableCell
                                Dim ACell9 As New TableCell
                                Dim ACell10 As New TableCell
                                Dim ACell11 As New TableCell
                                Dim ACellM1 As New TableCell
                                Dim ACellM2 As New TableCell
                                Dim ACellM3 As New TableCell
                                Dim ACellM4 As New TableCell
                                Dim ACellM5 As New TableCell
                                ARow1.Font.Size = "9.5"
                                ACellM1.ColumnSpan = 3
                                '    ACellM1.Text = "-"
                                ACellM2.Text = "-"
                                ACellM3.Text = "-"
                                ACellM4.Text = "-"
                                ACellM5.Text = "-"
                                ARow1.HorizontalAlign = HorizontalAlign.Right
                                'ARow1.CssClass = "tblbg"
                                'ARow1.Font.Bold = True
                                ACellM1.Text = "SubTotal"
                                ACell1.Text = STMTLines
                                ACell2.Text = STMTPLines
                                ACell3.Text = STQALines
                                ACell4.Text = STMTBLines
                                ACell5.Text = STQABLines
                                ACell6.Text = STQABSELines
                                ACell7.Text = STPPQALines
                                ACell8.Text = SDTotalLines
                                ACell9.Text = SDTotalTrg
                                ACell10.Text = SMTotalLines
                                ACell11.Text = SMTotalTrg

                                ARow1.Cells.Add(ACellM1)
                                ARow1.Cells.Add(ACell1)
                                ARow1.Cells.Add(ACell2)
                                ARow1.Cells.Add(ACell3)
                                ARow1.Cells.Add(ACell4)
                                ARow1.Cells.Add(ACell5)
                                ARow1.Cells.Add(ACell6)
                                ARow1.Cells.Add(ACell7)
                                ARow1.Cells.Add(ACell8)
                                ARow1.Cells.Add(ACell9)
                                ARow1.Cells.Add(ACellM2)
                                ARow1.Cells.Add(ACellM3)
                                ARow1.Cells.Add(ACell10)
                                ARow1.Cells.Add(ACell11)
                                ARow1.Cells.Add(ACellM4)
                                ARow1.Cells.Add(ACellM5)
                                tblMins.Rows.Add(ARow1)
                                If DLGroup.SelectedValue = "Platform" Then
                                    strCategory = DRRecT("ACCOUNTNAME").ToString
                                Else
                                    strCategory = DRRecT("DNAME").ToString
                                End If

                                Dim Row2 As New TableRow
                                Row2.HorizontalAlign = HorizontalAlign.Center
                                ' Row2.CssClass = "tblbgbody"
                                Row2.Font.Bold = True
                                Row2.Font.Italic = True
                                'Row2.ForeColor = Drawing.Color.White
                                Row2.BackColor = Drawing.Color.CornflowerBlue
                                Row2.ForeColor = Drawing.Color.WhiteSmoke
                                Row2.Font.Size = "9"
                                Dim CatCell As New TableCell
                                CatCell.ColumnSpan = "18"
                                If DLGroup.SelectedValue = "Platform" Then
                                    CatCell.Text = DRRecT("ACCOUNTNAME").ToString
                                Else
                                    CatCell.Text = DRRecT("DNAME").ToString
                                End If

                                Row2.Cells.Add(CatCell)
                                tblMins.Rows.Add(Row2)
                                STMTLines = 0
                                STMTPLines = 0
                                STQALines = 0
                                STPPQALines = 0
                                STMTBLines = 0
                                STQABLines = 0
                                STQABSELines = 0
                                SDTotalLines = 0
                                SMTotalLines = 0
                                SMTotalTrg = 0
                                SDTotalTrg = 0

                            End If

                        End If

                    End If
                    Dim Row1 As New TableRow
                    Dim Cell1 As New TableCell
                    Dim Cell2 As New TableCell
                    Dim Cell3 As New TableCell
                    Dim Cell4 As New TableCell
                    Dim Cell5 As New TableCell
                    Dim Cell6 As New TableCell
                    Dim Cell7 As New TableCell
                    Dim Cell8 As New TableCell
                    Dim Cell9 As New TableCell
                    Dim Cell10 As New TableCell
                    Dim Cell11 As New TableCell
                    Dim Cell12 As New TableCell
                    Dim Cell13 As New TableCell
                    Dim Cell14 As New TableCell
                    Dim Cell15 As New TableCell
                    Dim Cell16 As New TableCell
                    Dim Cell17 As New TableCell
                    Dim Cell18 As New TableCell
                    Row1.HorizontalAlign = HorizontalAlign.Right
                    Cell1.Text = DRRecT(0).ToString
                    Cell2.Text = DRRecT(1).ToString

                    If IsDBNull(DRRecT(2).ToString) Then
                        DailyTrg = 500
                    Else
                        If DRRecT(2).ToString = "" Then
                            DailyTrg = 500
                        ElseIf CInt(DRRecT(2).ToString) > 0 Then
                            DailyTrg = DRRecT(2).ToString
                        Else
                            DailyTrg = 500
                        End If
                    End If
                    Cell3.Text = DailyTrg
                    If DLGroup.SelectedValue = "Platform" Then
                        R2Cell3.Text = "Level"
                        Cell4.Text = DRRecT("DName").ToString
                    Else
                        R2Cell3.Text = "Platform"
                        Cell4.Text = DRRecT("AccountName").ToString
                    End If
                    Cell5.Text = DRRecT(4).ToString
                    Cell6.Text = DRRecT(5).ToString
                    Cell7.Text = DRRecT(6).ToString
                    Cell8.Text = DRRecT(7).ToString
                    Cell9.Text = DRRecT(8).ToString
                    Cell10.Text = DRRecT(9).ToString
                    Cell11.Text = DRRecT(10).ToString
                    DailyTLines = CInt(DRRecT(4).ToString) + (CInt(DRRecT(5).ToString) * 1.75) + CInt(DRRecT(6).ToString) + (CInt(DRRecT(7).ToString) * 1.5) + (CInt(DRRecT(8).ToString) * 0.5) + (CInt(DRRecT(9).ToString) * 1.5) + CInt(DRRecT(10).ToString)
                    MonTLines = CInt(DRRecT(11).ToString) + (CInt(DRRecT(12).ToString) * 1.75) + CInt(DRRecT(13).ToString) + (CInt(DRRecT(14).ToString) * 1.5) + (CInt(DRRecT(15).ToString) * 0.5) + (CInt(DRRecT(16).ToString) * 1.5) + CInt(DRRecT(17).ToString)
                    Cell12.Text = DailyTLines
                    Cell13.Text = MonTLines
                    Cell14.Text = FormatNumber((DailyTLines / DailyTrg) * 100, 0) & "%"
                    MonTrg = DailyTrg * CurrWDays
                    Cell15.Text = MonTrg
                    Cell16.Text = FormatNumber((MonTLines / MonTrg) * 100, 0) & "%"
                    Cell17.Text = "-"
                    Cell18.Text = "-"

                    Cell1.HorizontalAlign = HorizontalAlign.Left
                    Cell2.HorizontalAlign = HorizontalAlign.Left
                    Cell4.HorizontalAlign = HorizontalAlign.Left

                    STMTLines += DRRecT(4).ToString
                    STMTPLines += DRRecT(5).ToString
                    STQALines += DRRecT(6).ToString
                    STPPQALines += DRRecT(8).ToString
                    STMTBLines += DRRecT(9).ToString
                    STQABLines += DRRecT(7).ToString
                    STQABSELines += DRRecT(10).ToString
                    SDTotalLines += DailyTLines
                    SDTotalTrg += DailyTrg
                    SMTotalLines += MonTLines
                    SMTotalTrg += MonTrg
                    'Row1.CssClass = "tblbg2"
                    Row1.ForeColor = Drawing.Color.Black
                    Row1.Cells.Add(Cell1)
                    Row1.Cells.Add(Cell2)
                    Row1.Cells.Add(Cell4)
                    Row1.Cells.Add(Cell5)
                    Row1.Cells.Add(Cell6)
                    Row1.Cells.Add(Cell7)
                    Row1.Cells.Add(Cell10)
                    Row1.Cells.Add(Cell8)
                    Row1.Cells.Add(Cell11)
                    Row1.Cells.Add(Cell9)
                    Row1.Cells.Add(Cell12)
                    Row1.Cells.Add(Cell3)
                    Row1.Cells.Add(Cell14)
                    Row1.Cells.Add(Cell17)
                    Row1.Cells.Add(Cell13)
                    Row1.Cells.Add(Cell15)
                    Row1.Cells.Add(Cell16)
                    Row1.Cells.Add(Cell18)
                    tblMins.Rows.Add(Row1)
                End While
                Dim A1Row1 As New TableRow
                Dim A1Cell1 As New TableCell
                Dim A1Cell2 As New TableCell
                Dim A1Cell3 As New TableCell
                Dim A1Cell4 As New TableCell
                Dim A1Cell5 As New TableCell
                Dim A1Cell6 As New TableCell
                Dim A1Cell7 As New TableCell
                Dim A1Cell8 As New TableCell
                Dim A1Cell9 As New TableCell
                Dim A1Cell10 As New TableCell
                Dim A1Cell11 As New TableCell
                Dim A1CellM1 As New TableCell
                Dim A1CellM2 As New TableCell
                Dim A1CellM3 As New TableCell
                Dim A1CellM4 As New TableCell
                Dim A1CellM5 As New TableCell
                A1CellM1.ColumnSpan = 3
                '    ACellM1.Text = "-"
                A1CellM2.Text = "-"
                A1CellM3.Text = "-"
                A1CellM4.Text = "-"
                A1CellM5.Text = "-"
                A1Row1.HorizontalAlign = HorizontalAlign.Right
                '   A1Row1.CssClass = "tblbg"
                ' A1Row1.Font.Bold = True
                A1CellM1.Text = "SubTotal"
                A1Cell1.Text = STMTLines
                A1Cell2.Text = STMTPLines
                A1Cell3.Text = STQALines
                A1Cell4.Text = STMTBLines
                A1Cell5.Text = STQABLines
                A1Cell6.Text = STQABSELines
                A1Cell7.Text = STPPQALines
                A1Cell8.Text = SDTotalLines
                A1Cell9.Text = SDTotalTrg
                A1Cell10.Text = SMTotalLines
                A1Cell11.Text = SMTotalTrg
                A1Row1.Cells.Add(A1CellM1)
                A1Row1.Cells.Add(A1Cell1)
                A1Row1.Cells.Add(A1Cell2)
                A1Row1.Cells.Add(A1Cell3)
                A1Row1.Cells.Add(A1Cell4)
                A1Row1.Cells.Add(A1Cell5)
                A1Row1.Cells.Add(A1Cell6)
                A1Row1.Cells.Add(A1Cell7)
                A1Row1.Cells.Add(A1Cell8)
                A1Row1.Cells.Add(A1Cell9)
                A1Row1.Cells.Add(A1CellM2)
                A1Row1.Cells.Add(A1CellM3)
                A1Row1.Cells.Add(A1Cell10)
                A1Row1.Cells.Add(A1Cell11)
                A1Row1.Cells.Add(A1CellM4)
                A1Row1.Cells.Add(A1CellM5)
                tblMins.Rows.Add(A1Row1)

            End If
            DRRecT.Close()
        Finally
            If SQLCmdT.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmdT.Connection.Close()
                SQLCmdT = Nothing
            End If
        End Try


    End Sub







    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strconn As String
            strconn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim SQLCmd As New SqlCommand("Select * from tblusers  Where DEPARTMENTID = '2D0892BB-BF03-4652-83E3-B517EF7BA2CE' and (Isdeleted = 'False' or Isdeleted is NULL) order by firstname", New SqlConnection(strconn))
            Try
                SQLCmd.Connection.Open()
                Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
                If DRRec.Read = True Then
                    While (DRRec.Read)
                        Dim LI As New ListItem
                        LI.Text = DRRec("firstname").ToString + " " + DRRec("Lastname").ToString
                        LI.Value = DRRec("UserID").ToString
                        DLUsers.Items.Add(LI)
                    End While
                End If
                DRRec.Close()
            Finally
                If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                    SQLCmd.Connection.Close()
                    SQLCmd = Nothing
                End If
            End Try

            'If Request("showDict") = "Yes" Then
            '    Dim AccountID As String
            '    Dim InpDate As Date
            '    If TxtDate.Text = "" Then
            '        InpDate = Date.Parse(Request("InpDate"))
            '        TxtDate.Text = Request("InpDate")
            '    Else
            '        InpDate = Date.Parse(TxtDate.Text)
            '    End If
            '    Dim i As Integer
            '    For i = 0 To DLUsers.Items.Count - 1

            '        DLUsers.Items(i).Selected = False

            '    Next
            '    For i = 1 To DLUsers.Items.Count - 1
            '        If DLUsers.Items(i).Value = Request("AccountID") Then
            '            DLUsers.Items(i).Selected = True
            '            Exit For
            '        End If
            '    Next

            '    AccountID = Request("AccountID").ToString
            '    ShowDictDetails(AccountID, InpDate)
            'End If

        End If


    End Sub

    Protected Sub tblsubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tblsubmit.Click

        Dim AccountID As String
        Dim InpDate As Date
        If TxtDate.Text = "" Then
            InpDate = Date.Parse(Now.ToShortDateString)
        Else
            InpDate = Date.Parse(TxtDate.Text)
        End If

        AccountID = DLUsers.SelectedValue.ToString
        ShowActDetails()
        If TxtDate.Text = "" Then
            TxtDate.Text = Now.ToShortDateString
        End If
    End Sub
    Function WorkingDays(ByVal StartDate As Date, ByVal EndDate As Date) As Integer

        Dim intCount As Integer
        intCount = 0
        Do While StartDate <= EndDate
            Select Case Weekday(StartDate)
                Case "1"
                    intCount = intCount
                Case "7"
                    intCount = intCount + 1
                Case "2"
                    intCount = intCount + 1
                Case "3"
                    intCount = intCount + 1
                Case "4"
                    intCount = intCount + 1
                Case "5"
                    intCount = intCount + 1
                Case "6"
                    intCount = intCount + 1
            End Select
            StartDate = DateAdd(DateInterval.Day, 1, StartDate)
        Loop
        If intCount = 0 Then
            intCount = 1
        End If
        WorkingDays = intCount
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim filename As String
        filename = "Daily Team Report " & Month(Now) & Day(Now) & Year(Now) & ".xls"
        Dim attachment As String = "attachment; filename=" & filename
        Response.ClearContent()
        Response.AddHeader("content-disposition", attachment)
        Response.ContentType = "application/ms-excel"
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        Dim TxCell As String
        ShowActDetails()
        'Dim Table1 As New Table
        'Table1.BackColor = Drawing.Color.Snow
        'Table1.ForeColor = Drawing.Color.Firebrick
        'Table1.GridLines = GridLines.Both
        'Table1.Font.Name = "Trebuchet MS"
        'Table1.Font.Italic = True
        'Table1.Font.Size = "9"
        'Table1.Font.Underline = False

        'Dim x As Integer
        ''If (Not (MyDataGrid.HeaderRow) Is Nothing) Then
        ''    Dim TRow1 As New TableRow
        ''    For x = 0 To MyDataGrid.HeaderRow.Cells.Count - 1
        ''        If MyDataGrid.Columns(x).Visible = True Then
        ''            Dim TCell1 As New TableCell
        ''            TCell1.Text = MyDataGrid.HeaderRow.Cells(x).Text
        ''            TCell1.Font.Bold = True
        ''            TCell1.BackColor = Drawing.Color.Gray
        ''            TCell1.ForeColor = Drawing.Color.White
        ''            TRow1.Cells.Add(TCell1)
        ''        End If
        ''    Next
        ''    Table1.Rows.Add(TRow1)
        ''End If
        'Dim i As Integer
        'Dim k As Integer
        'k = 0


        'For Each row As TableRow In tblMins.Rows
        '    k = k + 1
        '    Dim TRow1 As New TableRow


        '    For i = 0 To row.Cells.Count - 1
        '        '   If tblMins.Columns(i).Visible = True Then

        '        Dim TCell1 As New TableCell
        '        TxCell = row.Cells(i).Text
        '        If k = 1 Then
        '            TCell1.ColumnSpan = 10
        '            TCell1.HorizontalAlign = HorizontalAlign.Center
        '        Else
        '            If i = 0 Then
        '                Response.Write(TxCell)
        '                TCell1.HorizontalAlign = HorizontalAlign.Left
        '                TCell1.Font.Bold = True
        '                TCell1.Font.Underline = False
        '            Else
        '                TCell1.HorizontalAlign = HorizontalAlign.Center
        '            End If
        '        End If


        '        TCell1.Text = TxCell
        '        TRow1.Cells.Add(TCell1)
        '        'End If
        '    Next
        '    Table1.Rows.Add(TRow1)
        '    'If MyDataGrid.AllowPaging = True And MyDataGrid.PageSize = k Then
        '    '    Exit For
        '    'End If
        'Next
        'tblMins.Rows(0).ForeColor = Drawing.Color.DarkOrange
        'tblMins.Rows(1).ForeColor = Drawing.Color.DarkOrange
        'tblMins.Rows(2).ForeColor = Drawing.Color.DarkOrange
        Dim i As Integer
        For i = 3 To tblMins.Rows.Count - 1
            tblMins.Rows(i).Font.Size = "10"
            'tblMins.Rows(i).BackColor = Drawing.Color.WhiteSmoke
        Next
        'tblMins.Rows(0).BackColor = Drawing.Color.LightSlateGray
        'tblMins.Rows(1).BackColor = Drawing.Color.LightSlateGray
        'tblMins.Rows(2).BackColor = Drawing.Color.LightSlateGray
        'tblMins.Rows(0).ForeColor = Drawing.Color.WhiteSmoke
        'tblMins.Rows(1).ForeColor = Drawing.Color.WhiteSmoke
        'tblMins.Rows(2).ForeColor = Drawing.Color.WhiteSmoke
        'tblMins.ForeColor = Drawing.Color.Black
        'tblMins.Rows(0).Font.Bold = True
        'tblMins.Rows(1).Font.Bold = True
        'tblMins.Rows(2).Font.Bold = True
        'tblMins.Rows(0).Font.Italic = True
        'tblMins.Rows(1).Font.Italic = True
        'tblMins.Rows(2).Font.Italic = True
        'tblMins.Font.Size = "9.5"
        'tblMins.GridLines = GridLines.Both
        'tblMins.ForeColor = Drawing.Color.DarkGray
        tblMins.RenderControl(htw)
        'MyDataGrid.RenderControl(htw)
        Response.Write(sw.ToString())
        Response.[End]()

    End Sub
End Class
