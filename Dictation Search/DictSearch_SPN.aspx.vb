Imports WebService
Namespace ets
    Partial Class Dictation_Search_DictSearch
        Inherits BasePage
        Public OpList As New Hashtable
        Public CloneList As Hashtable
        Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
            If Not String.IsNullOrEmpty(Request.Form("SEARCH")) Then
                'Server.Transfer("DictaResult_SPN.aspx")
                Server.Transfer("DictResult.aspx")
            End If
        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            ClientScript.RegisterClientScriptInclude("SelectAllCheckboxes", "JScript.js")
            Track.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            Cust.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            PFirst.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            PLast.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            valOp1.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            sDate.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            eDate.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            DCode.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            AccName.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            valOp2.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            UStatus.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            UserID.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            UserName.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            Level.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")
            valOp3.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('SEARCH').click();return false;}} else {return true}; ")

            Try
                OpList.Add("", "Please Select")
                OpList.Add("PIN", "PIN")
                OpList.Add("Account#", "Account#")
                OpList.Add("Patient First", "Patient First")
                OpList.Add("Patient Last", "Patient Last")
                OpList.Add("Date of Birth", "Date of Birth")
                OpList.Add("Date Of Service", "Date Of Service")
                OpList.Add("MRN", "MRN")
                OpList.Add("Template Type", "Template Type")
                OpList.Add("Template Name", "Template Name")
                If Not IsPostBack Then

                    lblOp1.Text = "Patient First"
                    lblOp2.Text = "Patient Last"
                    lblOp3.Text = "Date Of Service"

                    CloneList = New Hashtable
                    CloneList = OpList.Clone()

                    CloneList.Remove(lblOp1.Text)
                    CloneList.Remove(lblOp2.Text)
                    CloneList.Remove(lblOp3.Text)

                    DDType.DataSource = CloneList 'SortHashtable(CloneList)
                    DDType.DataValueField = "Key"
                    DDType.DataTextField = "Value"
                    DDType.DataBind()
                    SortDDL(DDType)
                    DDType1.DataSource = CloneList
                    DDType1.DataValueField = "Key"
                    DDType1.DataTextField = "Value"
                    DDType1.DataBind()
                    SortDDL(DDType1)
                    DDType2.DataSource = CloneList
                    DDType2.DataValueField = "Key"
                    DDType2.DataTextField = "Value"
                    DDType2.DataBind()
                    SortDDL(DDType2)
                    Dim dsPL As New Data.DataSet
                    Dim clsPL As New ets.BL.ProductionLevels
                    With clsPL
                        dsPL = .getProductionLevelsByContractorType(Session("ContractorID"), Session("ParentID"), Session("IsContractor"), IIf(Session("IsContractor"), 0, 1))
                    End With
                    clsPL = Nothing

                    If dsPL.Tables.Count > 0 Then
                        Dim LI As New ListItem
                        For Each oRec As Data.DataRow In dsPL.Tables(0).Rows
                            If oRec("LevelNo") <> 1073741824 And oRec("LevelNo") <> 3 And oRec("LevelNo") <> 5 Then
                                li = New ListItem
                                li.Value = oRec("LevelNo")
                                li.Text = oRec("LevelName")
                                Level.Items.Add(li)
                                li = New ListItem
                                li.Value = oRec("LevelNo")
                                li.Text = "Pending " & oRec("LevelName")
                                UStatus.Items.Add(li)
                                li = New ListItem
                                li.Value = oRec("LevelNo") + 100
                                li.Text = "Checked Out " & oRec("LevelName")
                                UStatus.Items.Add(li)
                            Else
                                li = New ListItem
                                li.Value = oRec("LevelNo")
                                li.Text = oRec("LevelName")
                                UStatus.Items.Add(li)
                            End If
                        Next
                        li = New ListItem
                        li.Value = ""
                        li.Text = "Any"
                        Level.Items.Insert(0, li)
                        UStatus.Items.Insert(0, li)
                        li = New ListItem
                        li.Value = "-100"
                        li.Text = "Not Finished"
                        UStatus.Items.Add(li)
                        dsPL.Dispose()
                       
                    End If
                 
                End If
                UserName.Attributes.Add("onkeyup", "validate()")
                UserID.Attributes.Add("onkeyup", "validate()")
            Catch ex As Exception

            End Try



        End Sub
        Private Sub SortDDL(ByRef objDDL As DropDownList)
            Dim textList As New ArrayList()
            Dim valueList As New ArrayList()


            For Each li As ListItem In objDDL.Items
                valueList.Add(li.Value)
            Next

            valueList.Sort()


            For Each item As Object In valueList
                Dim value As String = objDDL.Items.FindByValue(item.ToString()).Text
                textList.Add(value)
            Next
            objDDL.Items.Clear()
            For i As Integer = 0 To textList.Count - 1

                Dim objItem As New ListItem(textList(i).ToString(), valueList(i).ToString())
                objDDL.Items.Add(objItem)
            Next
        End Sub

        Protected Sub DDType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

            Dim OldVal As String = lblOp1.Text
            lblOp1.Text = DDType.SelectedItem.Text
            If lblOp1.Text = lblOp2.Text Then
                lblOp2.Text = OldVal
            ElseIf lblOp1.Text = lblOp3.Text Then
                lblOp3.Text = OldVal
            End If
            DDType.Visible = False
            lblOp1.Visible = True
        End Sub
        Protected Sub DDType1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

            Dim OldVal As String = lblOp2.Text
            lblOp2.Text = DDType1.SelectedItem.Text
            If lblOp1.Text = lblOp2.Text Then
                lblOp1.Text = OldVal
            ElseIf lblOp3.Text = lblOp2.Text Then
                lblOp3.Text = OldVal
            End If
            DDType1.Visible = False
            lblOp2.Visible = True
        End Sub
        Protected Sub DDType2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

            Dim OldVal As String = lblOp3.Text
            lblOp3.Text = DDType2.SelectedItem.Text
            If lblOp1.Text = lblOp3.Text Then
                lblOp1.Text = OldVal
            ElseIf lblOp2.Text = lblOp3.Text Then
                lblOp2.Text = OldVal
            End If
            DDType2.Visible = False
            lblOp3.Visible = True
        End Sub
        Protected Sub iPopUp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim btn As Button = CType(sender, Button)
            Dim ddlist As DropDownList = btn.Parent.FindControl("DDType")
            If Not ddlist.Visible Then
                CloneList = New Hashtable
                CloneList = OpList.Clone()

                CloneList.Remove(lblOp1.Text)
                CloneList.Remove(lblOp2.Text)
                CloneList.Remove(lblOp3.Text)
                DDType.DataSource = CloneList
                DDType.DataValueField = "Key"
                DDType.DataTextField = "Value"
                DDType.DataBind()
                SortDDL(DDType)
                DDType1.Visible = False
                lblOp2.Visible = True
                DDType2.Visible = False
                lblOp3.Visible = True
                ddlist.Visible = True
                lblOp1.Visible = False
                btn.ToolTip = "Click to reset"
            Else
                ddlist.Visible = False
                lblOp1.Visible = True
                btn.ToolTip = "Click here to change search option"
            End If
        End Sub
        Protected Sub iPopUp1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not DDType1.Visible Then
                CloneList = New Hashtable
                CloneList = OpList.Clone()

                CloneList.Remove(lblOp1.Text)
                CloneList.Remove(lblOp2.Text)
                CloneList.Remove(lblOp3.Text)
                DDType1.DataSource = CloneList
                DDType1.DataValueField = "Key"
                DDType1.DataTextField = "Value"
                DDType1.DataBind()
                SortDDL(DDType1)
                DDType.Visible = False
                lblOp1.Visible = True
                DDType2.Visible = False
                lblOp3.Visible = True
                DDType1.Visible = True
                lblOp2.Visible = False
                iPopUP1.ToolTip = "Click to reset"
            Else
                DDType1.Visible = False
                lblOp2.Visible = True
                iPopUP1.ToolTip = "Click here to change search option"
            End If
        End Sub
        Protected Sub iPopUp2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            If Not DDType2.Visible Then
                CloneList = New Hashtable
                CloneList = OpList.Clone()
                CloneList.Remove(lblOp1.Text)
                CloneList.Remove(lblOp2.Text)
                CloneList.Remove(lblOp3.Text)
                DDType2.DataSource = CloneList
                DDType2.DataValueField = "Key"
                DDType2.DataTextField = "Value"
                DDType2.DataBind()
                SortDDL(DDType2)
                DDType.Visible = False
                lblOp1.Visible = True
                DDType1.Visible = False
                lblOp2.Visible = True
                DDType2.Visible = True
                lblOp3.Visible = False
                iPopUp2.ToolTip = "Click to reset"
            Else
                DDType2.Visible = False
                lblOp3.Visible = True
                iPopUp2.ToolTip = "Click here to change search option"
            End If
        End Sub
    End Class

End Namespace