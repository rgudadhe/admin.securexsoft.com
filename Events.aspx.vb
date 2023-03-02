Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class testets_Events

    Inherits BasePage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'ShowData()

    End Sub


    'Sub ShowData()

    '    Dim RecFound As String


    '    RecFound = "No"
    '    Dim strConn As String
    '    strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

    '    Dim SQLCmd As New SqlCommand("Select * from tblUsers where UserID='" & Session("userid").ToString & "'", New SqlConnection(strConn))
    '    SQLCmd.Connection.Open()
    '    Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
    '    If DRRec.Read = True Then


    '        LblName.Text = UCase(DRRec("firstname").ToString & " " & DRRec("lastname").ToString)
    '        TxtFirstName.Text = UCase(DRRec("firstname").ToString)
    '        TxtLastName.Text = UCase(DRRec("lastname").ToString)
    '        TxtEmail.Text = UCase(DRRec("OfficialMailID").ToString)
    '        TxtNonOEmail.Text = UCase(DRRec("OtherMailID").ToString)
    '        TxtChatID.Text = UCase(DRRec("ChatID").ToString)
    '        TxtAdd.Text = DRRec("Address").ToString
    '        TxtCity.Text = DRRec("City").ToString
    '        TxtState.Text = DRRec("State").ToString
    '        'txtCountry.Text = DRRec("Country").ToString
    '        TxtDOB.Text = DRRec("DatOfBirth").ToString
    '        TxtJoin.Text = DRRec("DateJoined").ToString
    '        TxtCell.Text = DRRec("CellNo").ToString
    '        TxtTel.Text = DRRec("PhoneNo").ToString
    '        'Dim DepartmentID = DRRec("DepartmentID").ToString
    '        'Dim DesignationID = DRRec("DesignationID").ToString
    '        DRRec.Close()
    '        SQLCmd.Connection.Close()
    '    End If

    'End Sub
End Class
