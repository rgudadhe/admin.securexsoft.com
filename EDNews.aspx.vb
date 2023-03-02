Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class EDNews
    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = ""
        If Not IsPostBack Then
            ShowData()
        End If


    End Sub


    Sub ShowData()

        Dim RecFound As String


        RecFound = "No"
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim SQLCmd As New SqlCommand("Select Top 5 N.*, U.firstname + ' ' + U.LastName as uname   from tblNews  N LEFT OUTER JOIN TBLUSERS U ON N.USERID = U.USERID where  N.contractorid='" & Session("contractorid").ToString & "' order by DateDisp DESC", New SqlConnection(strConn))
        SQLCmd.Connection.Open()
        Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
        If DRRec.HasRows = True Then
            While (DRRec.Read)
                Label1.Text = Label1.Text & "<h1><span style='color: #ff9933; font-size: 10pt;'>" & DRRec("SubText").ToString & "</span></h1><p class='post-by'>Posted By <a href='profile.aspx?userid=" & DRRec("userid").ToString & "' target=_blank>" & DRRec("uname").ToString & "</a></p><p>" & DRRec("Details").ToString & "</p><br />"
            End While

        End If
        DRRec.Close()
        SQLCmd.Connection.Close()

        Dim SQLCmd1 As New SqlCommand("Select month(DateDisp) as MonthDisp, Year(DateDisp) as YearDisp from tblNews Group by month(DateDisp), Year(DateDisp) ", New SqlConnection(strConn))
        Try
            SQLCmd1.Connection.Open()
            Dim DRRec1 As SqlDataReader = SQLCmd1.ExecuteReader()
            If DRRec1.HasRows Then
                While DRRec1.Read
                    Dim LI As New ListItem
                    LI.Text = DRRec1("MonthDisp").ToString
                    LI.Value = DRRec1("MonthDisp").ToString & "#" & DRRec1("Yeardisp").ToString

                    If DRRec1("MonthDisp").ToString = "1" Then
                        LI.Text = "January " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "February " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "March " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "April " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "May " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "June " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "July " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "August " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "September " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "October " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
                        LI.Text = "November " & DRRec1("YearDisp").ToString
                    ElseIf DRRec1("MonthDisp").ToString = "9" Then
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
            If SQLCmd1.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd1.Connection.Close()
                SQLCmd1 = Nothing
            End If
        End Try
    End Sub

    Protected Sub DLDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLDate.SelectedIndexChanged
        If DLDate.SelectedValue <> "" Then
            If DLDate.Items(0).Value = "" Then
                DLDate.Items.RemoveAt(0)
            End If
        End If
        SelData()
    End Sub

    Sub SelData()

        Dim RecFound As String
        RecFound = "No"
        Dim strConn As String
        Dim inpstr() As String
        inpstr = Split(DLDate.SelectedValue, "#")
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim SQLCmd As New SqlCommand("Select N.*, U.firstname + ' ' + U.LastName as uname   from tblNews  N LEFT OUTER JOIN TBLUSERS U ON N.USERID = U.USERID  where month(DateDisp) = '" & inpstr(0) & "' and  Year(DateDisp) = '" & inpstr(1) & "' order by DateDisp DESC", New SqlConnection(strConn))
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.HasRows = True Then
                While (DRRec.Read)
                    Label1.Text = Label1.Text & "<h1><span style='color: #ff9933; font-size: 10pt;'>" & DRRec("SubText").ToString & "</span></h1><p class='post-by'>Posted By <a href='profile.aspx?userid=" & DRRec("userid").ToString & "' target=_blank>" & DRRec("uname").ToString & "</a></p><p>" & DRRec("Details").ToString & "</p><br />"
                End While

            End If
            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
                SQLCmd = Nothing
            End If
        End Try


    End Sub
End Class
