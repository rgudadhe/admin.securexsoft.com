Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType




Partial Class EditUser
    Inherits BasePage

    Public strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = String.Empty
        If Not IsPostBack Then
            Panel1.Visible = False
            Dim objAct As New ETS.BL.Accounts
            Dim DTSet1 As System.Data.DataSet = objAct.getAccountList

            If DTSet1.Tables.Count > 0 Then

                Dim dv As DataView
                dv = New DataView
                With dv
                    .Table = DTSet1.Tables(0)
                    .AllowDelete = True
                    .AllowEdit = True
                    .AllowNew = True
                    .RowFilter = " (Indirect='False' OR Indirect IS NULL) AND (Isdeleted='False' OR Isdeleted IS NULL) and contractorid ='" & Session("contractorid").ToString & "' "
                    .Sort = "AccountName Asc"
                End With

                'Dim DTView As New DataView(DTSet1.Tables(0), "Indirect='True'", "AccountName Asc", DataViewRowState.ModifiedCurrent)
                DLAct.DataSource = dv
                DLAct.DataTextField = "AccountName"
                DLAct.DataValueField = "AccountID"
                DLAct.DataBind()
            End If
            objAct = Nothing

        End If
    End Sub


    Sub ShowData()
        Panel1.Visible = True
        DLMode.SelectedIndex = -1
        TXTValue.Text = String.Empty

        Dim clsUsr As ETS.BL.PhysiciansLinesDeductions
        Try
            clsUsr = New ETS.BL.PhysiciansLinesDeductions
            clsUsr.PhysicianID = Request("DLPhysician").ToString
            'Response.Write(" WHERE PhysicianID = '" & Request("DLAct").ToString & "'")

            Dim DS As DataSet = clsUsr.gettblPhysiciansLinesDeductionsList
            'Response.Write(DS.Tables.Count)

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then



                    For i As Integer = 0 To DLMode.Items.Count - 1
                        DLMode.Items(i).Selected = False
                        If DLMode.Items(i).Value = DS.Tables(0).Rows(0).Item("Mode").ToString.Trim Then
                            DLMode.Items(i).Selected = True

                        End If
                    Next
                    TXTValue.Text = DS.Tables(0).Rows(0).Item("Value").ToString
                End If
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        Finally
            clsUsr = Nothing
        End Try


    End Sub
    Protected Sub DLAct_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLAct.SelectedIndexChanged
        If DLAct.SelectedValue <> "" Then
            Dim objPhy As New ETS.BL.Physicians
            objPhy.AccountID = DLAct.SelectedValue
            Dim DTSet1 As System.Data.DataSet = objPhy.getPhysiciansList

            If DTSet1.Tables.Count > 0 Then

                Dim dv As DataView
                dv = New DataView
                DTSet1.Tables(0).Columns.Add("PhyName", GetType(String), "FirstName + ' ' + LastName")
                With dv
                    .Table = DTSet1.Tables(0)
                    .AllowDelete = True
                    .AllowEdit = True
                    .AllowNew = True
                    .RowFilter = "  (Isdeleted='False' OR Isdeleted IS NULL) "
                    .Sort = "FirstName Asc"
                End With

                'Dim DTView As New DataView(DTSet1.Tables(0), "Indirect='True'", "PhysicianName Asc", DataViewRowState.ModifiedCurrent)
                DLPhysician.DataSource = dv
                DLPhysician.DataTextField = "PhyName"
                DLPhysician.DataValueField = "PhysicianID"
                DLPhysician.DataBind()
            End If
            objPhy = Nothing


            ShowData()
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub
    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim oConn As New Data.SqlClient.SqlConnection
        Dim oCommand As New Data.SqlClient.SqlCommand
        Dim ConString As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim thisTransaction As Data.SqlClient.SqlTransaction
        oConn.ConnectionString = ConString
        oConn.Open()
        thisTransaction = oConn.BeginTransaction()
        Dim obj As New ETS.BL.PhysiciansLinesDeductions

        Dim RowAffected As Integer
        With obj
            obj.PhysicianID = DLPhysician.SelectedValue
            obj.DeletetblPhysiciansLinesDeductions(oConn, thisTransaction)
            RowAffected = obj.InserttblPhysiciansLinesDeductions(oConn, thisTransaction, "[PhysicianID],[Mode],[Value],[updatedate]", "'" & DLPhysician.SelectedValue & "', '" & DLMode.SelectedValue & "','" & TXTValue.Text & "','" & Now & "'")
            If RowAffected > 0 Then
                thisTransaction.Commit()
            Else
                thisTransaction.Rollback()
            End If

        End With
        obj = Nothing


        ShowData()
    End Sub

    Protected Sub DLPhysician_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DLPhysician.SelectedIndexChanged
        ShowData()
    End Sub
End Class
