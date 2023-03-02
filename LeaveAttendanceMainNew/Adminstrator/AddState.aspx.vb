Imports System.Data
Imports System.Data.SqlClient
Imports MainModule
Partial Class AddState
    Inherits BasePage
    Dim objMainModule As New MainModule
    Dim varStrDeptID As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        varStrDeptID = Request.QueryString("DeptID")
        If Not Page.IsPostBack Then
            Dim objConn As New Data.SqlClient.SqlConnection
            objConn = objMainModule.OpenConnection(objConn)
            Try
                'Dim objSQLAdapter As Data.SqlClient.SqlDataAdapter = New Data.SqlClient.SqlDataAdapter("SELECT DISTINCT State FROM DBO.tblUsers WHERE State NOT IN (SELECT DISTINCT State FROM DBO.tblOffDays WHERE Department='" & varStrDeptID & "') AND (State IS NOT NULL OR State <>'' OR State<>' ') AND DepartmentID='" & varStrDeptID & "'", objConn)
                'Dim objDataSet As New DataSet
                'objSQLAdapter.Fill(objDataSet, "tblUsers")
                'DropDownState.DataSource = objDataSet
                'DropDownState.DataTextField = "State"
                'DropDownState.DataValueField = "State"
                'DropDownState.DataBind()

                Dim objCmd As New Data.SqlClient.SqlCommand("SELECT DISTINCT State FROM DBO.tblUsers WHERE State NOT IN (SELECT DISTINCT State FROM DBO.tblOffDays WHERE Department='" & varStrDeptID & "') AND (State IS NOT NULL OR State <>'' OR State<>' ') AND DepartmentID='" & varStrDeptID & "'", objConn)
                Dim objRec As Data.SqlClient.SqlDataReader = objCmd.ExecuteReader

                If objRec.HasRows Then
                    While objRec.Read
                        Dim varstr As String = String.Empty
                        If Not objRec.IsDBNull(objRec.GetOrdinal("State")) Then
                            varstr = objRec.GetString(objRec.GetOrdinal("State"))
                        End If

                        If Not String.IsNullOrEmpty(varstr) Then
                            Dim varlst As New ListItem
                            varlst.Text = varstr
                            varlst.Value = varstr

                            DropDownState.Items.Add(varlst)
                        End If
                    End While
                End If

                objRec.Close()
                objRec = Nothing
                objCmd = Nothing

            Catch ex As Exception
            Finally
                If objConn.State <> ConnectionState.Closed Then
                    objConn.Close()
                    objConn = Nothing
                End If
            End Try
        End If
    End Sub
    Protected Sub BtnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSubmit.Click
        If String.IsNullOrEmpty(Request.Form("DropDownState")) Then
            Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">State should not be blank</font></center>")
            Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
            Response.End()
        End If
        Dim varStrInsert As String
        varStrInsert = "INSERT INTO DBO.tblOffDays (State,Department) VALUES('" & Request.Form("DropDownState") & "','" & varStrDeptID & "')"
        Dim objConn As New Data.SqlClient.SqlConnection
        objConn = objMainModule.OpenConnection(objConn)
        Try
            Dim InsertCmd As New Data.SqlClient.SqlCommand
            InsertCmd.CommandType = Data.CommandType.Text
            InsertCmd.CommandText = varStrInsert
            InsertCmd.Connection = objConn
            InsertCmd.ExecuteNonQuery()
            InsertCmd = Nothing

            Response.Write("<center><font face=""Arial"" size=""2"" color=""#000080"">State added sucessfully !!!</font></center>")
            Response.Write("<center><a href=""../CloseWindow.aspx"">Close Window</a></center>")
            Response.End()
        Catch ex As Exception
        Finally
            If objConn.State <> ConnectionState.Closed Then
                objConn.Close()
                objConn = Nothing
            End If
        End Try
    End Sub
End Class
