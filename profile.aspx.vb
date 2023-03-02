Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.SqlDbType

Partial Class testets_profile
    Inherits BasePage
    Dim UserID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strURL As String
        If Request("userid") <> "" Then
            pnl.Visible = False
            UserID = Request("userid")
        Else
            pnl.Visible = True
            UserID = Session("userid").ToString
        End If

        strURL = "userphoto.aspx?UserID=" & UserID

        Image1.ImageUrl = strURL
        ShowData()
        If IsPostBack Then

            'Response.Write(Request("DropDownList1"))
            'Response.End()


        End If

    End Sub


    Sub ShowData()

        Dim RecFound As String


        RecFound = "No"
        Dim strConn As String
        strConn = System.Configuration.ConfigurationManager.AppSettings("ETSCon")

        Dim SQLCmd As New SqlCommand("Select U.*, D.Name as Deptname, D1.Name as Desnname, C.Name as Category  from tblUsers U LEFT OUTER JOIN tblDepartments D ON D.DepartmentID = U.DepartmentID LEFT OUTER JOIN tblusersCategory C ON C.CategoryID = U.CategoryID  LEFT OUTER JOIN tblDeptDesignations D1 ON D1.DesignationID = U.DesignationID where UserID='" & UserID & "'", New SqlConnection(strConn))
        SQLCmd.Connection.Open()
        Dim DRRec As SqlDataReader = SQLCmd.ExecuteReader()
        If DRRec.Read = True Then


            LblName.Text = UCase(DRRec("firstname").ToString & " " & DRRec("lastname").ToString)
            TxtFirstName.Text = UCase(DRRec("firstname").ToString)
            TxtLastName.Text = UCase(DRRec("lastname").ToString)
            TxtEmail.Text = UCase(DRRec("OfficialMailID").ToString)
            TxtNonOEmail.Text = UCase(DRRec("OtherMailID").ToString)
            TxtChatID.Text = UCase(DRRec("ChatID").ToString)
            TxtAdd.Text = DRRec("Address").ToString
            TxtCity.Text = DRRec("City").ToString
            TxtState.Text = DRRec("State").ToString
            TxtCountry.Text = Trim(DRRec("Country").ToString)
            TxtpAdd.Text = DRRec("pAddress").ToString
            TxtpCity.Text = DRRec("pCity").ToString
            TxtpState.Text = DRRec("pState").ToString
            TxtpCountry.Text = Trim(DRRec("pCountry").ToString)
            If Not IsDate(DRRec("DatOfBirth").ToString) Then
                TxtDOB.Text = ""
            Else
                TxtDOB.Text = FormatDateTime(DRRec("DatOfBirth").ToString, DateFormat.ShortDate)
            End If
            If Not IsDate(DRRec("DateJoined").ToString) Then
                TxtJoin.Text = ""
            Else
                TxtJoin.Text = FormatDateTime(DRRec("DateJoined").ToString, DateFormat.ShortDate)
            End If

            TxtCell.Text = DRRec("CellNo").ToString
            TxtTel.Text = DRRec("PhoneNo").ToString
            TxtDept.Text = DRRec("Deptname").ToString
            TxtDesn.Text = DRRec("Desnname").ToString
            TxtCategory.Text = DRRec("Category").ToString
            'Dim DepartmentID = DRRec("DepartmentID").ToString
            'Dim DesignationID = DRRec("DesignationID").ToString
            DRRec.Close()
            SQLCmd.Connection.Close()
        End If

    End Sub
End Class
