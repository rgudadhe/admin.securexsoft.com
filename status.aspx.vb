Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class status
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim RecFound As String
        RecFound = "No"
        Dim strConn As String
        Dim LblText As String
        Dim strQuery As String
        If Session("DeptID").ToString <> "" Then
            strQuery = "Select Top 5  N.*, U.firstname + ' ' + U.LastName as uname   from tblupdates  N LEFT OUTER JOIN TBLUSERS U ON N.USERID = U.USERID  where (N.DepartmentID in ('" & Session("DeptID").ToString & "') or N.DepartmentID is NULL ) order by DateDisp DESC"
        Else
            strQuery = "Select  Top 5  N.*, U.firstname + ' ' + U.LastName as uname   from tblupdates  N LEFT OUTER JOIN TBLUSERS U ON N.USERID = U.USERID  order by DateDisp DESC"
        End If
        '  Response.Write(strQuery)
        LblText = ""
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd As New SqlCommand(strQuery, New SqlConnection(strConn))
        SQLCmd.Connection.Open()
        Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
        If DRRec.HasRows = True Then
            While (DRRec.Read)
                LblText = LblText & "<a href=# onclick=poptastic('" & DRRec("trackID").ToString & "')> <img src='images/flag.gif' border=0>  " & DRRec("SubText").ToString & " - " & DRRec("uname").ToString & "</a>   "
            End While
        End If
        DRRec.Close()
        SQLCmd.Connection.Close()
        Label1.Text = "<marquee id='marclient' onmouseover='javascript:marclient.stop()' onmouseout='javascript:marclient.start()' atomicselection='true' direction='left' behavior='scroll' height='22' loop='infinite' scrollamount='4' class='marqueebodytext' style='width: 100%;' ><span style='color: #ff9933; font-size: 10pt;'> " & LblText & "</span></marquee>  "
    End Sub
End Class
