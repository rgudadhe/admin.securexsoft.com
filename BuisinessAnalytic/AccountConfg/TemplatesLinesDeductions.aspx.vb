Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType




Partial Class EditUser
    Inherits BasePage

    Public strConn As String = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = String.Empty
        If Not IsPostBack Then
            BindData()
            Dim objAct As New ETS.BL.Templates
            objAct.ContractorID = Session("contractorid").ToString
            Dim DTSet1 As System.Data.DataSet = objAct.getTemplateList

            If DTSet1.Tables.Count > 0 Then

                Dim dv As DataView
                dv = New DataView
                With dv
                    .Table = DTSet1.Tables(0)
                    .AllowDelete = True
                    .AllowEdit = True
                    .AllowNew = True
                    .RowFilter = " contractorid ='" & Session("contractorid").ToString & "' "
                    .Sort = "TemplateName Asc"
                End With

                'Dim DTView As New DataView(DTSet1.Tables(0), "Indirect='True'", "TemplateName Asc", DataViewRowState.ModifiedCurrent)
                DLAct.DataSource = dv
                DLAct.DataTextField = "TemplateName"
                DLAct.DataValueField = "TemplateID"
                DLAct.DataBind()
            End If
            objAct = Nothing

        End If
    End Sub


    Sub ShowData()
        Panel1.Visible = True
        '  DLMode.SelectedIndex = -1
        TXTValue.Text = String.Empty

        Dim clsUsr As ETS.BL.TemplatesLinesDeductionsForBilling
        Try
            clsUsr = New ETS.BL.TemplatesLinesDeductionsForBilling
            clsUsr.TemplateID = Request("DLAct").ToString
            'Response.Write(" WHERE TemplateID = '" & Request("DLAct").ToString & "'")

            Dim DS As DataSet = clsUsr.gettblTemplatesLinesDeductionsForBillingList
            'Response.Write(DS.Tables.Count)

            If DS.Tables.Count > 0 Then
                If DS.Tables(0).Rows.Count > 0 Then



                    'For i As Integer = 0 To DLMode.Items.Count - 1
                    '    DLMode.Items(i).Selected = False
                    '    If DLMode.Items(i).Value = DS.Tables(0).Rows(0).Item("Mode").ToString.Trim Then
                    '        DLMode.Items(i).Selected = True
                    '    End If
                    'Next
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
        Dim obj As New ETS.BL.TemplatesLinesDeductionsForBilling

        Dim RowAffected As Integer
        With obj
            obj.TemplateID = DLAct.SelectedValue
            obj.DeletetblTemplatesLinesDeductionsForBilling(oConn, thisTransaction)
            RowAffected = obj.InserttblTemplatesLinesDeductionsForBilling(oConn, thisTransaction, "[TemplateID],[Value],[updatedate]", "'" & DLAct.SelectedValue & "','" & TXTValue.Text & "','" & Now & "'")
            If RowAffected > 0 Then
                thisTransaction.Commit()
            Else
                thisTransaction.Rollback()
            End If

        End With
        obj = Nothing


        ShowData()
        BindData()
    End Sub

    Protected Sub BindData()
        Dim ConString As String
        Dim SQLString As String
        ConString = System.Configuration.ConfigurationManager.AppSettings("ETSCon")
        Dim oConn As New Data.SqlClient.SqlConnection
        oConn.ConnectionString = ConString
        Dim myDs As New Data.DataSet
        Try
            oConn.Open()
            SQLString = "SF_gettemplatesLinesDeductionsForBilling"
            Dim Adapter As New Data.SqlClient.SqlDataAdapter
            Dim oCommand As New Data.SqlClient.SqlCommand
            oCommand = New Data.SqlClient.SqlCommand(SQLString, oConn)
            oCommand.CommandType = Data.CommandType.StoredProcedure
            Adapter.SelectCommand = oCommand
            Adapter.Fill(myDs, "templatesLinesDeductions")

        Catch ex As Exception

        Finally
            myDs.Dispose()
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
                oConn = Nothing
            End If
        End Try
        If myDs.Tables.Count > 0 Then
            MyDataGrid.DataSource = myDs.Tables(0)
            MyDataGrid.DataBind()
        End If

    End Sub
End Class
