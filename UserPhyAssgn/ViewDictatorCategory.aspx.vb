Imports System
Imports System.Data
Partial Class UserPhyAssgn_Default
    Inherits BasePage
    Sub ViewDetails()
        Dim strAccountID As String = String.Empty
        'If Not DropDownList2.SelectedValue = "11111111-1111-1111-1111-111111111111" Then
        strAccountID = DropDownList2.SelectedValue
        ' End If
        Dim clsPhy As New ETS.BL.Physicians
        Dim DSPhy As DataSet = clsPhy.getPhywithActDetails(Session("contractorid").ToString, strAccountID)
        clsPhy = Nothing
        DSPhy.Tables(0).Columns.Add("Physician Name", GetType(System.String), "FirstName + ' ' + LastName")

        Dim DV As DataView = DSPhy.Tables(0).DefaultView
        DSPhy.Dispose()
        If DropDownList1.SelectedValue = "A" Then
            DV.RowFilter = "Category='A'"
        Else
            DV.RowFilter = "Category<>'A'"
        End If

        GridView1.DataSource = DV
        GridView1.DataBind()
        DV.Dispose()

        If GridView1.Rows.Count > 0 Then
            GridView1.ShowFooter = True
            GridView1.UseAccessibleHeader = True
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader
            GridView1.FooterRow.TableSection = TableRowSection.TableFooter
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        ViewDetails()
    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Not IsPostBack Then


            Dim DSAct As New DataSet
            Dim clsAcc As New ETS.BL.Accounts
            With clsAcc
                .ContractorID = Session("ContractorID")
                ._WhereString.Append(" and (Isdeleted is null or IsDeleted=0)")
                DSAct = .getAccountList()
            End With
            clsAcc = Nothing
            DropDownList2.DataSource = DSAct
            DropDownList2.DataTextField = "AccountName"
            DropDownList2.DataValueField = "AccountID"
            DropDownList2.DataBind()
            DSAct.Dispose()
            Dim LI As New ListItem
            LI.Text = "All Accounts"
            LI.Value = ""
            DropDownList2.Items.Insert(0, LI)
            LI = Nothing
        End If
    End Sub
End Class


