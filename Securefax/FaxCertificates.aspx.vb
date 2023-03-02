Imports System.Data
Imports System.Data.SqlClient
Partial Class Services_FaxCertificates
    Inherits System.Web.UI.Page
    Protected Sub FillAccounts()
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)

        Try
            Dim objDataSet As DataSet
            Dim objCmd1 As Data.SqlClient.SqlCommand
            Dim objDAtaAdapter As Data.SqlClient.SqlDataAdapter
            'If Trim(UCase(str)) = Trim(UCase("Service")) Or str = "" Then
            '    objCmd1 = New Data.SqlClient.SqlCommand("SELECT DISTINCT A.AccID AS 'AccID',Description FROM MDONE.DBO.TBLACCDETAILS A INNER JOIN DEMOGRAPHICS.DBO.Users FA ON A.AccNumber=FA.AccID WHERE A.ACCID IS NOT NULL AND FA.FAXService=1 ORDER BY DESCRIPTION ", objConn)
            'ElseIf Trim(UCase(str)) = Trim(UCase("ALL")) Then
            '    objCmd1 = New Data.SqlClient.SqlCommand("SELECT AccID,Description FROM MDONE.DBO.TBLACCDETAILS WHERE ACCID IS NOT NULL ORDER BY DESCRIPTION ", objConn)
            'End If
            objCmd1 = New Data.SqlClient.SqlCommand("SELECT AccountID,AccountName FROM tblAccounts where (Isdeleted is null or Isdeleted=0) order by AccountName", objConn)

            objDAtaAdapter = New Data.SqlClient.SqlDataAdapter(objCmd1)
            objDataSet = New DataSet
            objDAtaAdapter.Fill(objDataSet)
            If objDataSet.Tables(0).Rows.Count > 0 Then
                DropDownAccount.Items.Clear()
                DropDownAccount.DataSource = objDataSet.Tables(0)
                DropDownAccount.DataTextField = objDataSet.Tables(0).Columns(1).ToString
                DropDownAccount.DataValueField = objDataSet.Tables(0).Columns(0).ToString
                DropDownAccount.DataBind()


            End If
            Dim varLstItem As New ListItem
            varLstItem.Text = "SELECT"
            varLstItem.Value = ""
            DropDownAccount.Items.Insert(0, varLstItem)
        Catch ex As Exception

        Finally
            If objConn.State <> ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
    Public Function OpenConnection(ByRef Conn As Data.SqlClient.SqlConnection) As Data.SqlClient.SqlConnection
        'Conn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Conn.ConnectionString = "Server=sqlmain\one;Database=ETS;UID=usersqlbkp;Pwd=y0u4@209#"
        Conn.Open()
        Return Conn
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FillAccounts()
        End If
    End Sub
    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = OpenConnection(objConn)
        Try
            Dim varStrAccID As String = String.Empty
            varStrAccID = DropDownAccount.Items(DropDownAccount.SelectedIndex).Value.ToString
            'Response.Write(varStrAccID)

            'If Not String.IsNullOrEmpty(varStrAccID) Then
            Dim objGet As New Data.SqlClient.SqlCommand("SELECT U.UserId,first +' '+ last AS 'UName',UC.UpdateDate FROM Secureweb.dbo.tblusers U LEFT OUTER JOIN Securefax.dbo.tblUserCertificates UC ON U.UserID=UC.UserID where  AccID = '" & varStrAccID.ToString & "' and (AccessLevel='666' OR AccessLevel='777') ", objConn)
            Dim objRec As Data.SqlClient.SqlDataReader = objGet.ExecuteReader

            'Response.Write(objGet.CommandText)
            If objRec.HasRows Then
                While objRec.Read
                    Dim vartblCellUName As New TableCell
                    Dim vartblcellLast As New TableCell
                    Dim varStrLast As String = String.Empty
                    Dim vartblCellAction As New TableCell
                    Dim vartblRow As New TableRow
                    Dim varStrUserID As String = String.Empty


                    If Not objRec.IsDBNull(objRec.GetOrdinal("UserID")) Then
                        varStrUserID = objRec.GetGuid(objRec.GetOrdinal("UserID")).ToString
                    End If

                    If Not objRec.IsDBNull(objRec.GetOrdinal("UName")) Then
                        vartblCellUName.Text = objRec.GetString(objRec.GetOrdinal("UName"))
                    Else
                        vartblCellUName.Text = "&nbsp"
                    End If

                    If Not objRec.IsDBNull(objRec.GetOrdinal("UpdateDate")) Then
                        varStrLast = objRec.GetDateTime(objRec.GetOrdinal("UpdateDate"))
                    End If

                    If Not String.IsNullOrEmpty(varStrLast) Then
                        vartblcellLast.Text = varStrLast
                    Else
                        vartblcellLast.Text = "&nbsp"
                    End If

                    vartblRow.Cells.Add(vartblCellUName)

                    If String.IsNullOrEmpty(varStrLast) Then
                        vartblCellAction.HorizontalAlign = HorizontalAlign.Center
                        vartblCellAction.Text = "<a href="""" OnClick=""window.open('UploadCertificate.aspx?UserID=" & varStrUserID & "&Action=Add','', 'width=450,height=140,status=1,scrollbars=1');return false;"" >Add</a>"
                    Else
                        vartblCellAction.HorizontalAlign = HorizontalAlign.Center
                        vartblCellAction.Text = "<a href="""" OnClick=""window.open('UploadCertificate.aspx?UserID=" & varStrUserID & "&Action=Update','', 'width=450,height=140,status=1,scrollbars=1');return false;"" >Update</a> &nbsp <a href="""" OnClick=""window.open('UploadCertificate.aspx?UserID=" & varStrUserID & "&Action=Delete','', 'width=450,height=140,status=1,scrollbars=1');return false;"" >Delete</a>"
                    End If
                    vartblRow.Cells.Add(vartblcellLast)
                    vartblRow.Cells.Add(vartblCellAction)

                    tblUsers.Rows.Add(vartblRow)
                End While
            End If

            objRec.Close()
            objRec = Nothing
            objGet = Nothing

            'End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            If objConn.State <> ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
