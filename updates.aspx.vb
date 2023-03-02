Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class updates
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = ""
        If Not IsPostBack Then
            ShowData()
        End If


    End Sub


    Sub ShowData()

        Dim SQLConn As New SqlConnection
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        SQLConn.ConnectionString = strConn

        SQLConn.Open()
        Try


            Dim RecFound As String
            RecFound = "No"


            Dim SQLCmd As New SqlCommand("Select Top 5 N.*, U.firstname + ' ' + U.LastName as uname   from tblupdates  N LEFT OUTER JOIN TBLUSERS U ON N.USERID = U.USERID order by DateDisp DESC", SQLConn)
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows = True Then
                While (DRRec.Read)
                    Label1.Text = Label1.Text & "<h1><span style='color: #ff9933; font-size: 10pt;'>" & DRRec("SubText").ToString & "</span></h1><p class='post-by'>Posted By <a href='profile.aspx?userid=" & DRRec("userid").ToString & "' target=_blank>" & DRRec("uname").ToString & "</a></p><p>" & DRRec("Details").ToString & "</p><br />"
                End While

            End If
            DRRec.Close()


            Dim SQLCmd1 As New SqlCommand("Select month(DateDisp) as MonthDisp, Year(DateDisp) as YearDisp from tblupdates Group by month(DateDisp), Year(DateDisp)  ", SQLConn)
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then
                While DRRec1.Read
                    Dim LI As New ListItem
                    LI.Text = DRRec1("MonthDisp").ToString
                    LI.Value = DRRec1("MonthDisp").ToString & "#" & DRRec1("Yeardisp").ToString

                    If DRRec1("MonthDisp").ToString = "1" Then
                        LI.Text = "January " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "2" Then
                        LI.Text = "February " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "3" Then
                        LI.Text = "March " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "4" Then
                        LI.Text = "April " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "5" Then
                        LI.Text = "May " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "6" Then
                        LI.Text = "June " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "7" Then
                        LI.Text = "July " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "8" Then
                        LI.Text = "August " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "September " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "10" Then
                        LI.Text = "October " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "11" Then
                        LI.Text = "November " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "12" Then
                        LI.Text = "December " & DRRec1("YearDisp").ToString
                    End If
                    DLDate.Items.Add(LI)

                End While

            End If

            'Dim dateTimeInfo As DateTime = "9"
            'Dim strMonth As String = dateTimeInfo.ToString("m")
            'Response.Write(strMonth)

            DRRec1.Close()

        Finally
            SQLConn.Close()
        End Try
    End Sub

    Protected Sub DLDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLDate.SelectedIndexChanged
        SelData()
    End Sub

    Sub SelData()
        Dim SQLConn As New SqlConnection
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        SQLConn.ConnectionString = strConn
        SQLConn.Open()
        Try

            Dim RecFound As String
            RecFound = "No"
            Dim inpstr() As String
            inpstr = Split(DLDate.SelectedValue, "#")
            Dim SQLCmd As New SqlCommand("Select N.*, U.firstname + ' ' + U.LastName as uname   from tblupdates  N LEFT OUTER JOIN TBLUSERS U ON N.USERID = U.USERID  where  N.contractorid='" & Session("contractorid").ToString & "' and month(DateDisp) = '" & inpstr(0) & "' and  Year(DateDisp) = '" & inpstr(1) & "' order by DateDisp DESC", SQLConn)
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows = True Then
                While (DRRec.Read)
                    Label1.Text = Label1.Text & "<h1><span style='color: #ff9933; font-size: 10pt;'>" & DRRec("SubText").ToString & "</span></h1><p class='post-by'>Posted By <a href='profile.aspx?userid=" & DRRec("userid").ToString & "' target=_blank>" & DRRec("uname").ToString & "</a></p><p>" & DRRec("Details").ToString & "</p><br />"
                End While
            End If
            DRRec.Close()

        Finally
            SQLConn.Close()
        End Try

    End Sub
End Class
