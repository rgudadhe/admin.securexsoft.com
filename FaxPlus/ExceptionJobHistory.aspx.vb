
'Partial Class FaxPlus_ExceptionJobHistory
'    Inherits System.Web.UI.Page
'    Protected Sub btnSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSet.Click
'        Dim clsRP As ETS.BL.RefPhysician
'        Try
'            If String.IsNullOrEmpty(txtFName.Text) Then
'                Response.Write("<script language=""javascript"">" & vbCrLf)
'                Response.Write("<!--" & vbCrLf)
'                Response.Write("alert('First Name can not be blank');")
'                Response.Write("//-->" & vbCrLf)
'                Response.Write("</script>" & vbCrLf)
'                Exit Sub
'            End If
'            If String.IsNullOrEmpty(txtFax.Text) Then
'                Response.Write("<script language=""javascript"">" & vbCrLf)
'                Response.Write("<!--" & vbCrLf)
'                Response.Write("alert('Fax# can not be blank');")
'                Response.Write("//-->" & vbCrLf)
'                Response.Write("</script>" & vbCrLf)
'                Exit Sub
'            ElseIf Len(txtFax.Text) <> 10 Then
'                Response.Write("<script language=""javascript"">" & vbCrLf)
'                Response.Write("<!--" & vbCrLf)
'                Response.Write("alert('Incorrect Fax#');")
'                Response.Write("//-->" & vbCrLf)
'                Response.Write("</script>" & vbCrLf)
'                Exit Sub
'            End If
'            clsRP = New ETS.BL.RefPhysician
'            With clsRP
'                .PhyID = hdnRP.Value.ToString
'                .PhyName = txtFName.Text
'                .PhymName = txtMName.Text
'                .PhylName = txtLName.Text
'                .PhyDegree = txtDegree.Text
'                .Address = txtadd.Text
'                .PhyState = txtState.Text
'                .PhyCode = txtZip.Text
'                .PhyCountry = txtCountry.Text
'                .PhoneNO = txtPhone.Text
'                .FaxNO = txtFax.Text
'                .Email = txtEmail.Text
'                .addModDt = Now()
'                If .setException(hdnTrans.Value, Session("UserID").ToString) Then
'                    Response.Write("<script language=""javascript"">" & vbCrLf)
'                    Response.Write("<!--" & vbCrLf)
'                    Response.Write("alert('Referring Physician has been set successfully');")
'                    Response.Write("window.location.href = 'FaxPlusExceptions.aspx?Result=1';" & vbCrLf)
'                    Response.Write("//-->" & vbCrLf)
'                    Response.Write("</script>" & vbCrLf)
'                Else
'                    Response.Write("<script language=""javascript"">" & vbCrLf)
'                    Response.Write("<!--" & vbCrLf)
'                    Response.Write("alert('Failed updating Referring Physician');")
'                    Response.Write("window.location.href = 'FaxPlusExceptions.aspx?Result=1';" & vbCrLf)
'                    Response.Write("//-->" & vbCrLf)
'                    Response.Write("</script>" & vbCrLf)
'                End If
'            End With
'        Catch ex As Exception
'            Response.Write("<script language=""javascript"">" & vbCrLf)
'            Response.Write("<!--" & vbCrLf)
'            Response.Write("alert('" & ex.Message.ToString & "');")
'            Response.Write("//-->" & vbCrLf)
'            Response.Write("</script>" & vbCrLf)
'        Finally
'            clsRP = Nothing
'        End Try
'    End Sub
'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        If Not Page.IsPostBack Then
'            hdnRP.Value = Request.QueryString("RPID").ToString
'            hdnTrans.Value = Request.QueryString("TransID").ToString
'        End If
'        Try
'            Button1.Attributes.Add("onclick", "window.open('popup.aspx?TransID=" & hdnTrans.Value.ToString & "&RPID=" & hdnRP.Value.ToString & "', 'newwindow','toolbar=yes,location=no,menubar=no,width=640,height=430,resizable=no,scrollbars=yes,top=200,left=250');return false;")
'            ShowInfo()
'        Catch ex As Exception
'        End Try
'    End Sub
'    Private Sub ShowInfo(Optional ByVal strPID As String = Nothing)
'        Dim clsRP As ETS.BL.RefPhysician
'        Try
'            If String.IsNullOrEmpty(strPID) Then
'                strPID = hdnRP.Value.ToString
'            End If
'            clsRP = New ETS.BL.RefPhysician
'            With clsRP
'                .PhyID = strPID
'                .getRPDetails()
'                If Len(.PhyID) = 36 Then
'                    txtFName.Text = .PhylName
'                    txtMName.Text = .PhymName
'                    txtLName.Text = .PhylName
'                    txtDegree.Text = .PhyDegree
'                    txtadd.Text = .Address
'                    txtCity.Text = .PhyCity
'                    txtState.Text = .PhyState
'                    txtZip.Text = .PhyCode
'                    txtCountry.Text = .PhyCountry
'                    txtPhone.Text = .PhoneNO
'                    txtFax.Text = .FaxNO
'                    txtEmail.Text = .Email
'                End If
'            End With
'        Catch ex As Exception
'        Finally
'            clsRP = Nothing
'        End Try
'    End Sub
'End Class

Partial Class FaxPlus_ExceptionJobHistory
    Inherits System.Web.UI.Page
    Protected Sub btnSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSet.Click
        Dim clsRP As ETS.BL.RefPhysician
        Try
            If String.IsNullOrEmpty(txtFName.Text) Then
                lblMsg.Text = "First Name can not be blank"
                Exit Sub
            End If
            If String.IsNullOrEmpty(txtFax.Text) Then
                lblMsg.Text = "Fax# can not be blank"
                Exit Sub
            End If
            clsRP = New ETS.BL.RefPhysician
            With clsRP
                .PhyID = hdnRP.Value.ToString
                .PhyName = txtFName.Text
                .PhymName = txtMName.Text
                .PhylName = txtLName.Text
                .PhyDegree = txtDegree.Text
                .Address = txtadd.Text
                .PhyState = txtState.Text
                .PhyCode = txtZip.Text
                .PhyCountry = txtCountry.Text
                .PhoneNO = txtPhone.Text
                .FaxNO = txtFax.Text
                .Email = txtEmail.Text
                .addModDt = Now()
                'Response.Write(.setException(hdnTrans.Value.ToString, Session("UserID").ToString))
                If Trim(UCase(hdnfrom.Value.ToString)) = Trim(UCase("OutBound")) Then
                    If .setExceptionStatusFromOutBound(hdnTrans.Value.ToString, Session("UserID").ToString, hdnRecordID.Value.ToString) Then
                        Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Referring Physician has been set successfully</font></center>")
                        Response.Write("<center><a href=""CloseWindow.aspx""><font face=""Trebuchet MS"" size=""2"">Close Window</font></a></center>")
                        Response.End()
                    Else
                        Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Failed updating Referring Physician,Please try again...</font></center>")
                        Response.Write("<center><a href=""CloseWindow.aspx""><font face=""Trebuchet MS"" size=""2"">Close Window</font></a></center>")
                        Response.End()
                    End If
                Else
                    If .setExceptionStatusFromFaxPlusExceptions(hdnTrans.Value.ToString, Session("UserID").ToString) Then
                        Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Referring Physician has been set successfully</font></center>")
                        Response.Write("<center><a href=""CloseWindow.aspx""><font face=""Trebuchet MS"" size=""2"">Close Window</font></a></center>")
                        Response.End()
                    Else
                        Response.Write("<center><font face=""Trebuchet MS"" size=""2"" color=""#000080"">Failed updating Referring Physician,Please try again...</font></center>")
                        Response.Write("<center><a href=""CloseWindow.aspx""><font face=""Trebuchet MS"" size=""2"">Close Window</font></a></center>")
                        Response.End()
                    End If
                End If
            End With
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            clsRP = Nothing
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = String.Empty
        If Not Page.IsPostBack Then
            hdnRP.Value = Request.QueryString("RPID").ToString
            hdnTrans.Value = Request.QueryString("TransID").ToString
            hdnfrom.Value = Request.QueryString("from").ToString
            hdnRecordID.Value = Request.QueryString("RecordID").ToString
            Try
                Button1.Attributes.Add("onclick", "window.open('popup.aspx?TransID=" & hdnTrans.Value.ToString & "&RPID=" & hdnRP.Value.ToString & "', 'newwindow','toolbar=yes,location=no,menubar=no,width=640,height=430,resizable=no,scrollbars=yes,top=200,left=250');return false;")
                ShowInfo()
            Catch ex As Exception
            End Try
        End If

    End Sub
    Private Sub ShowInfo(Optional ByVal strPID As String = Nothing)
        Dim clsRP As ETS.BL.RefPhysician
        Try
            If String.IsNullOrEmpty(strPID) Then
                strPID = hdnRP.Value.ToString
            End If
            clsRP = New ETS.BL.RefPhysician
            With clsRP
                .PhyID = strPID
                .getRPDetails()
                If Len(.PhyID) = 36 Then
                    txtFName.Text = .PhyName
                    txtMName.Text = .PhymName
                    txtLName.Text = .PhylName
                    txtDegree.Text = .PhyDegree
                    txtadd.Text = .Address
                    txtCity.Text = .PhyCity
                    txtState.Text = .PhyState
                    txtZip.Text = .PhyCode
                    txtCountry.Text = .PhyCountry
                    txtPhone.Text = .PhoneNO
                    txtFax.Text = .FaxNO
                    txtEmail.Text = .Email
                End If
            End With
        Catch ex As Exception
        Finally
            clsRP = Nothing
        End Try
    End Sub
End Class

