Imports System.Data.SqlClient
Partial Class Department_Default
    Inherits BasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strConn As String
            Dim strDeptValue As ArrayList = New ArrayList
            Dim strDescValue As ArrayList = New ArrayList

            strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
            Dim cmdIns As New SqlCommand("Select * from tblDepartments where DepartmentId='" & Request.QueryString("DeptID") & "'", New SqlConnection(strConn))
            Try
                cmdIns.Connection.Open()
                Dim dataReader As System.Data.IDataReader = cmdIns.ExecuteReader()
                If dataReader.Read Then
                    TxtDeptName.Text = dataReader("Name")
                    TxtDeptDesc.Text = dataReader("Description")
                    DeptID.Value = dataReader("DepartmentID").ToString
                    'MsgDisp.Text = DeptID.Value
                End If
                dataReader.Close()
            Finally
                If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
                    cmdIns.Connection.Close()
                    cmdIns = Nothing
                End If
            End Try
            Button3.Visible = False

        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand("update tblDepartments set name='" & TxtDeptName.Text & "', Description='" & TxtDeptDesc.Text & "' where DepartmentID='" & DeptID.Value & "'", New SqlConnection(strConn))
        MsgDisp.Text = "Record has been udpated successfully."
        Try
            cmdIns.Connection.Open()
            cmdIns.ExecuteNonQuery()
        Finally
            If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
                cmdIns.Connection.Close()
                cmdIns = Nothing
            End If
        End Try


    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim cmdIns As New SqlCommand("update tblDepartments set deleted=1 where DepartmentID='" & DeptID.Value & "'", New SqlConnection(strConn))
        MsgDisp.Text = "Record has been deleted successfully."
        'TxtDeptName.Text = ""
        'TxtDeptDesc.Text = ""
        'DeptID.Value = ""
        Button1.Visible = False
        Button2.Visible = False
        Button3.Visible = True
        Try
            cmdIns.Connection.Open()
            cmdIns.ExecuteNonQuery()
        Finally
            If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
                cmdIns.Connection.Close()
                cmdIns = Nothing
            End If
        End Try
    End Sub
End Class
