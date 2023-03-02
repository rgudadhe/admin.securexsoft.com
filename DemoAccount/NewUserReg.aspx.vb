Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType




Partial Class Department_Default
    Inherits BasePage


    
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Panel1.Visible = True
            'Panel2.Visible = False
            'Panel3.Visible = False
            LblUN.Visible = False
            TxtUname.Visible = False
        End If
    End Sub

   

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim RecFound As String
        Dim strQuery As String
        Dim stream As IO.Stream

        RecFound = "No"
        stream = FileUpload1.PostedFile.InputStream
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim uploadedFile(stream.Length) As Byte
        Dim UserId As String = String.Empty
        Dim cmdIns As New SqlCommand("select newID() as foo", New SqlConnection(strConn))
        Try
            cmdIns.Connection.Open()
            UserId = cmdIns.ExecuteScalar().ToString
        Finally
            If cmdIns.Connection.State = System.Data.ConnectionState.Open Then
                cmdIns.Connection.Close()
            End If
        End Try
        If FileUpload1.HasFile Then
            If UCase(Right(FileUpload1.FileName, 3)) = "JPG" Or UCase(Right(FileUpload1.FileName, 3)) = "GIF" Then
                RecFound = "Yes"
                stream.Read(uploadedFile, 0, stream.Length)
            End If
        End If

        If RecFound = "Yes" Then
            strQuery = "Insert Into TblUsers ( UserID, FirstName, LastName,OfficialMailID, OtherMailID, ChatID, Address, City, State, Country, DatOfBirth, DateJoined, CellNo, PhoneNo, DepartmentID, DesignationID, Photo) Values ( '" & UserId & "','" & TxtFirstName.Text & "', '" & TxtLastName.Text & "', '" & TxtEmail.Text & "', '" & TxtNonOEmail.Text & "', '" & TxtChatID.Text & "', '" & TxtAdd.Text & "', '" & TxtCity.Text & "', '" & TxtState.Text & "', '" & txtCountry.Text & "', '" & TxtDOB.Text & "', '" & TxtJoin.Text & "', '" & TxtCell.Text & "', '" & TxtTel.Text & "', '" & TxtDept.Text & "', '" & TxtDesi.Text & "', @DocumentFile); select newid()"
        Else
            strQuery = "Insert Into TblUsers (  UserID, FirstName, LastName,OfficialMailID, OtherMailID, ChatID, Address, City, State, Country, DatOfBirth, DateJoined, CellNo, PhoneNo, DepartmentID, DesignationID) Values ( '" & UserId & "','" & TxtFirstName.Text & "', '" & TxtLastName.Text & "', '" & TxtEmail.Text & "', '" & TxtNonOEmail.Text & "', '" & TxtChatID.Text & "', '" & TxtAdd.Text & "', '" & TxtCity.Text & "', '" & TxtState.Text & "', '" & txtCountry.Text & "', '" & TxtDOB.Text & "', '" & TxtJoin.Text & "', '" & TxtCell.Text & "', '" & TxtTel.Text & "', '" & TxtDept.Text & "', '" & TxtDesi.Text & "'); select newid()"
        End If


        Dim cmdUp As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            cmdUp.Connection.Open()
            If RecFound = "Yes" Then
                cmdUp.Parameters.Add("@DocumentFile", SqlDbType.Image, uploadedFile.Length).Value = uploadedFile
            End If

            cmdUp.ExecuteNonQuery()
        Finally
            If cmdUp.Connection.State = System.Data.ConnectionState.Open Then
                cmdUp.Connection.Close()
            End If
        End Try
        Dim i As Integer

        Dim SQLCmd As New SqlCommand("Select * from tblUsers where UserID='" & UserId & "'", New SqlConnection(strConn))
        'Response.Write("Select * from tblUsers where UserID='" & UserId & "'")
        'Response.End()
        Dim EmpNo As String
        EmpNo = ""
        Try
            SQLCmd.Connection.Open()
            Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
            If DRRec.Read = True Then
                EmpNo = DRRec("EmpNo")
            End If
            DRRec.Close()
        Finally
            If SQLCmd.Connection.State = System.Data.ConnectionState.Open Then
                SQLCmd.Connection.Close()
            End If
        End Try

        Dim uname As String
        uname = "TestUser"
        ' Response.Write(txtCate.SelectedItem.Text)

        If ChkUn.Checked Then
            If txtCate.SelectedItem.Text = "Office Employee" Then
                uname = "E" & EmpNo
            ElseIf txtCate.SelectedItem.Text = "HBE" Then
                uname = "E" & EmpNo
            ElseIf txtCate.SelectedItem.Text = "HBA" Then
                uname = "H" & EmpNo
            ElseIf txtCate.SelectedItem.Text = "Contractor" Then
                uname = "C" & EmpNo
            End If
        Else
            uname = TxtUname.Text
        End If
        strQuery = "Update tblUsers set username='" & uname & "' where userid='" & UserId & "'"
        'Response.Write(strQuery)
        'Response.End()


        Dim cmdUpUser As New SqlCommand(strQuery, New SqlConnection(strConn))
        Try
            cmdUpUser.Connection.Open()
            cmdUpUser.ExecuteNonQuery()
        Finally
            If cmdUpUser.Connection.State = System.Data.ConnectionState.Open Then
                cmdUpUser.Connection.Close()
            End If
        End Try

        For i = 1 To 4

            If i = 1 Then
                If TxtQua1.Text <> "" Then
                    strQuery = "Insert Into tblUsersAcadInfo (UserID, Qualification,Institute,YearOfPassing,Percentage) Values ('" & UserId & "', '" & TxtQua1.Text & "', '" & TxtInst1.Text & "', '" & TxtYrsPass1.Text & "', '" & TxtPerc1.Text & "')"
                    Dim cmdUp1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    Try
                        cmdUp1.Connection.Open()
                        cmdUp1.ExecuteNonQuery()

                    Finally
                        If cmdUp1.Connection.State = System.Data.ConnectionState.Open Then
                            cmdUp1.Connection.Close()
                        End If
                    End Try
                End If
            ElseIf i = 2 Then
                If TxtQua2.Text <> "" Then
                    strQuery = "Insert Into tblUsersAcadInfo (UserID, Qualification,Institute,YearOfPassing,Percentage) Values ('" & UserId & "', '" & TxtQua2.Text & "', '" & TxtInst2.Text & "', '" & TxtYrsPass2.Text & "', '" & TxtPerc2.Text & "')"
                    Dim cmdUp2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    Try
                        cmdUp2.Connection.Open()
                        cmdUp2.ExecuteNonQuery()
                    Finally
                        If cmdUp2.Connection.State = System.Data.ConnectionState.Open Then
                            cmdUp2.Connection.Close()
                        End If
                    End Try
                End If
            ElseIf i = 3 Then
                If TxtQua3.Text <> "" Then
                    strQuery = "Insert Into tblUsersAcadInfo (UserID, Qualification,Institute,YearOfPassing,Percentage) Values ('" & UserId & "', '" & TxtQua3.Text & "', '" & TxtInst3.Text & "', '" & TxtYrsPass3.Text & "', '" & TxtPerc3.Text & "')"
                    Dim cmdUp3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    cmdUp3.Connection.Open()
                    cmdUp3.ExecuteNonQuery()
                    cmdUp3.Connection.Close()
                End If
            ElseIf i = 4 Then
                If TxtQua4.Text <> "" Then
                    strQuery = "Insert Into tblUsersAcadInfo (UserID, Qualification,Institute,YearOfPassing,Percentage) Values ('" & UserId & "', '" & TxtQua4.Text & "', '" & TxtInst4.Text & "', '" & TxtYrsPass4.Text & "', '" & TxtPerc4.Text & "')"
                    Dim cmdUp4 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    cmdUp4.Connection.Open()
                    cmdUp4.ExecuteNonQuery()
                    cmdUp4.Connection.Close()
                End If
            End If
        Next


        For i = 1 To 4

            If i = 1 Then
                If TxtComp1.Text <> "" Then
                    strQuery = "Insert Into tblUsersProfInfo (UserID, CompanyName,Experience,Designation,Skills) Values ('" & UserId & "', '" & TxtComp1.Text & "', '" & TxtExp1.Text & "', '" & TxtDesi1.Text & "', '" & TxtSkill1.Text & "')"
                    Dim cmdUp1 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    cmdUp1.Connection.Open()
                    cmdUp1.ExecuteNonQuery()
                    cmdUp1.Connection.Close()
                End If
            ElseIf i = 2 Then
                If TxtComp2.Text <> "" Then
                    strQuery = "Insert Into tblUsersProfInfo (UserID, CompanyName,Experience,Designation,Skills) Values ('" & UserId & "', '" & TxtComp2.Text & "', '" & TxtExp2.Text & "', '" & TxtDesi2.Text & "', '" & TxtSkill2.Text & "')"
                    Dim cmdUp2 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    cmdUp2.Connection.Open()
                    cmdUp2.ExecuteNonQuery()
                    cmdUp2.Connection.Close()
                End If
            ElseIf i = 3 Then
                If TxtComp3.Text <> "" Then
                    strQuery = "Insert Into tblUsersProfInfo (UserID, CompanyName,Experience,Designation,Skills) Values ('" & UserId & "', '" & TxtComp3.Text & "', '" & TxtExp3.Text & "', '" & TxtDesi3.Text & "', '" & TxtSkill3.Text & "')"
                    Dim cmdUp3 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    cmdUp3.Connection.Open()
                    cmdUp3.ExecuteNonQuery()
                    cmdUp3.Connection.Close()
                End If
            ElseIf i = 4 Then
                If TxtComp4.Text <> "" Then
                    strQuery = "Insert Into tblUsersProfInfo (UserID, CompanyName,Experience,Designation,Skills) Values ('" & UserId & "', '" & TxtComp4.Text & "', '" & TxtExp4.Text & "', '" & TxtDesi4.Text & "', '" & TxtSkill4.Text & "')"
                    Dim cmdUp4 As New SqlCommand(strQuery, New SqlConnection(strConn))
                    cmdUp4.Connection.Open()
                    cmdUp4.ExecuteNonQuery()
                    cmdUp4.Connection.Close()
                End If
            End If
        Next
        Response.Write("Record has been successfully added")

        Response.End()

    End Sub

    Protected Sub ChkUn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkUn.CheckedChanged
        ' Response.Write("checked")
        If ChkUn.Checked Then
            LblUN.Visible = False
            TxtUname.Visible = False
            ' Response.Write("checked")
        Else
            LblUN.Visible = True
            TxtUname.Visible = True
            'Response.Write(" Not checked")
        End If
    End Sub
    
End Class
