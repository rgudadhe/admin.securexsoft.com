Imports System.Data.SqlClient

Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Dim RB As RadioButton
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim squery As String

        If Request("RecordID") <> "" Then
            ' Response.Write(Request("RecordId"))
            ' Response.Write(Request("ActId"))
            '            Response.End()

            'Dim actdet As String()
            Dim ActID As String
            Dim RecordID As String
            'actdet = Split(Request("RecordID"), "#")
            RecordID = Request("RecordID")
            ActID = Request("ActID")

            Dim FoldName As String
            squery = "Select * from tblAccounts where AccountID =  '" & ActID & "'"
            Dim CmdDemo As New SqlCommand(squery, New SqlConnection(strConn))
            Try
                CmdDemo.Connection.Open()
                Dim DRDemo As SqlDataReader
                DRDemo = CmdDemo.ExecuteReader()
                If DRDemo.HasRows Then
                    While (DRDemo.Read)
                        FoldName = DRDemo("FolderName")
                    End While
                End If
                DRDemo.Close()

            Finally
                If CmdDemo.Connection.State = System.Data.ConnectionState.Open Then
                    CmdDemo.Connection.Close()
                End If
            End Try

            Dim SQuery1 As String
            Dim DFName As String
            SQuery1 = "Select * from tblActDemos where RecordID='" & Request("RecordID") & "'"
            Dim CmdDemo1 As New SqlCommand(SQuery1, New SqlConnection(strConn))
            Try
                CmdDemo1.Connection.Open()
                Dim DRDemo1 As SqlDataReader
                DRDemo1 = CmdDemo1.ExecuteReader()
                If DRDemo1.HasRows Then
                    While (DRDemo1.Read)
                        DFName = DRDemo1("DemoFieldName")
                    End While
                End If
                DRDemo1.Close()

            Finally
                If CmdDemo1.Connection.State = System.Data.ConnectionState.Open Then
                    CmdDemo1.Connection.Close()
                End If
            End Try


            SQuery1 = "Select * from tblActDemos where RecordID='" & Request("RecordID") & "'"
            Dim CmdDemo2 As New SqlCommand(SQuery1, New SqlConnection(strConn))
            Try
                CmdDemo2.Connection.Open()
                Dim DRDemo2 As SqlDataReader
                DRDemo2 = CmdDemo2.ExecuteReader()
                If DRDemo2.HasRows Then
                    Label1.Text = "There are records in the table " & FoldName & ". You need to remove all the records from the database before remove the field."
                Else


                    squery = "ALTER TABLE ETSDemos.dbo." & FoldName & " DROP COLUMN " & DFName
                    Dim cmdUp1 As New SqlCommand(squery, New SqlConnection(strConn))
                    cmdUp1.Connection.Open()
                    cmdUp1.ExecuteNonQuery()
                    cmdUp1.Connection.Close()

                    squery = "Delete from tblActDemos where RecordID='" & Request("RecordID") & "'"
                    Dim cmdUp As New SqlCommand(squery, New SqlConnection(strConn))
                    cmdUp.Connection.Open()
                    cmdUp.ExecuteNonQuery()
                    cmdUp.Connection.Close()
                    Label1.Text = "Field has been removed successfully."
                End If
                DRDemo2.Close()

            Finally
                If CmdDemo2.Connection.State = System.Data.ConnectionState.Open Then
                    CmdDemo2.Connection.Close()
                End If
            End Try

        End If



    End Sub






   
   
   
End Class

