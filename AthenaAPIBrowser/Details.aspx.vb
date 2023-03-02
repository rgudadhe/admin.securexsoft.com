Imports System.Net
Imports System.Collections.Generic
Imports System.IO
'Imports System.lin
Imports Newtonsoft.Json
Imports System.Data
Imports System.ComponentModel
Partial Class AthenaAPIBrowser_Details
    Inherits System.Web.UI.Page

   

    Private baseUrl As String = "https://api.platform.athenahealth.com/"
    Private key As String = ConfigurationSettings.AppSettings("key")
    Private secret As String = ConfigurationSettings.AppSettings("secret")
    Private version As String = ConfigurationSettings.AppSettings("version")
    Private DestinationPath As String = ConfigurationSettings.AppSettings("DestinationPath").ToString


    Protected Sub form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles form1.Load
        Try
            Response.Write("Loaded")

            GetToken()
            getAccounts()
            Dim selActID As String = Request("accountid")
            Dim selProID As String = Request("providerid")
            If Not IsPostBack Then
                If Not selActID = String.Empty Then
                    dvActs.Visible = False
                    dvPhysicians.Visible = True
                    dvAppt.Visible = False
                    Session("selActID") = selActID

                    GetPhysicians(Session("selActID"), 1)
                End If
                If Not selProID = String.Empty Then
                    dvActs.Visible = False
                    dvPhysicians.Visible = False
                    dvAppt.Visible = True
                    'Response.Write(selProID)

                    GetChangedAppointmentsMultiDepartment(selProID, Session("selActID"))
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
    Private Function getAccounts() As Boolean
        Dim oConn As Data.SqlClient.SqlConnection = Connect()
        If Not oConn.State = ConnectionState.Open Then

            Return Nothing
        End If
        Try
            Dim Adapter As Data.SqlClient.SqlDataAdapter
            Adapter = New Data.SqlClient.SqlDataAdapter("SF_getInHealthAHAccountsWithProID", oConn)
            Adapter.SelectCommand.CommandType = CommandType.StoredProcedure
            Dim DSPJN As New Data.DataSet
            Adapter.Fill(DSPJN, "tblAHAccounts")
            Adapter.Dispose()
            If DSPJN.Tables.Count > 0 Then
                If DSPJN.Tables(0).Rows.Count > 0 Then


                    MyDataGrid.DataSource = DSPJN
                    MyDataGrid.DataBind()
                End If
            End If
            Return True
        Catch ex As Exception
            Response.Write(ex.Message)
            Return False
        Finally
            If oConn.State <> ConnectionState.Closed Then
                oConn.Close()
            End If

        End Try
    End Function
    Private Function Connect() As Data.SqlClient.SqlConnection
        Try
            Dim ConString As String = ConfigurationSettings.AppSettings("ConnectionString").ToString
            Dim oConn As New Data.SqlClient.SqlConnection
            oConn.ConnectionString = ConString
            oConn.Open()
            Return oConn
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
    Private Function GetPhysicians(ByVal practiceid As Integer, ByVal AccountNo As Integer)
        Dim UTF8 As Encoding = System.Text.Encoding.GetEncoding("utf-8")
        Try

            ' Now we get to make the URL, making sure to encode the parameters and remember the "?"
            Dim url As String = PathJoin(baseUrl, version, practiceid, "/providers")

            Dim request As WebRequest
            ' Create the request, add in the auth header
            request = WebRequest.Create(url)
            request.Method = "GET"
            request.Headers("Authorization") = Convert.ToString("Bearer ") & Session("token")

            Dim response As WebResponse
            ' Get the response, read and decode
            response = request.GetResponse()
            Dim receive As Stream
            receive = response.GetResponseStream()
            Dim reader As StreamReader
            reader = New StreamReader(receive, UTF8)
            Dim providers As Linq.JObject = Linq.JObject.Parse(reader.ReadToEnd())
            Dim ds As New DataSet

            Dim doc As New System.Xml.XmlDocument
            doc = JsonConvert.DeserializeXmlNode(providers.ToString, "providers")
            Dim xmlReader As New System.Xml.XmlNodeReader(doc)
            ds.ReadXml(xmlReader)
            'For Each x As Linq.JObject In providers.SelectToken("providers")

            '    Dim doc As New System.Xml.XmlDocument
            '    doc = JsonConvert.DeserializeXmlNode(x.ToString, "root")
            '    Dim root As System.Xml.XmlNode = doc.DocumentElement
            '    'Create a new node.
            '    Dim elem As System.Xml.XmlElement = doc.CreateElement("practiceid")
            '    elem.InnerText = practiceid
            '    'Add the node to the document.
            '    root.AppendChild(elem)
            '    'Create a new node.
            '    elem = doc.CreateElement("AccountNo")
            '    elem.InnerText = AccountNo
            '    'Add the node to the document.
            '    root.AppendChild(elem)
            '    ds.ReadXmlSchema(doc.ToString)
            'doc.Save(Path.Combine("D:\", Now.Month & Now.Day & Now.Year & Now.Hour & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xml"))

            'Next
            rptComLog.DataSource = ds
            rptComLog.DataBind()


            'Console.WriteLine(appointments.ToString)

        Catch ex As Exception
            Response.Write(ex.StackTrace)
        End Try
    End Function
    Private Function GetChangedAppointmentsMultiDepartment(ByVal _ProviderIDs As String, ByVal practiceid As String) As Boolean
        Try
            '  For Each department As String In arrDepartments

            ' GET /appointments/Changed
            Dim parameters As New Dictionary(Of String, String)
            ' Since GET parameters go in the URL, we set up the parameters Dictionary first
            parameters = New Dictionary(Of String, String)()
            'Dim strDepartments As String = String.Join(",", arrDepartments)
            With parameters
                '.Add("departmentid", department)
                .Add("providerid", _ProviderIDs)
                .Add("enddate", Now.ToShortDateString)
                .Add("showcancelled", "false")
                .Add("showclaimdetail", "false")
                .Add("showcopay", "false")
                .Add("showinsurance", "false")
                .Add("showpatientdetail", "true")
                .Add("startdate", DateAdd(DateInterval.Day, -1, Now).ToShortDateString)
            End With


            ' Now we get to make the URL, making sure to encode the parameters and remember the "?"
            Dim url As String = PathJoin(baseUrl, version, practiceid, "/appointments/booked/multipledepartment?" & UrlEncode(parameters))

            Dim request As WebRequest
            ' Create the request, add in the auth header
            request = WebRequest.Create(url)
            request.Method = "GET"
            request.Headers("Authorization") = Convert.ToString("Bearer ") & Session("token")

            Dim apiresponse As WebResponse
            ' Get the response, read and decode
            apiresponse = request.GetResponse()
            Dim receive As Stream
            receive = apiresponse.GetResponseStream()

            Dim UTF8 As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim reader As StreamReader
            reader = New StreamReader(receive, UTF8)
            Dim appointments As Linq.JObject = Linq.JObject.Parse(reader.ReadToEnd())
            Dim TotalCount As Integer = CInt(appointments("totalcount"))
            Dim ds As New DataTable
            ds.TableName = "Appointments"
            ds.Columns.Add("DtOfServ")
            ds.Columns.Add("CaseNumber")
            ds.Columns.Add("PFirstName")
            ds.Columns.Add("PriDepartmentID")
            ds.Columns.Add("AppointmentType")
            ds.Columns.Add("PLastName")
            ds.Columns.Add("PtEmail")
            ds.Columns.Add("PtSuffix")
            ds.Columns.Add("DepartmentID")
            ds.Columns.Add("PtMobile")
            ds.Columns.Add("PatientID")
            ds.Columns.Add("MedRN")
            ds.Columns.Add("DtTransac")
            ds.Columns.Add("PtCountryCode")
            ds.Columns.Add("PGender")
            ds.Columns.Add("PDOB")
            ds.Columns.Add("RelationToInsured")
            ds.Columns.Add("AppointmentTime")
            ds.Columns.Add("VisitStatus")
            ds.Columns.Add("AppTime")
            ds.Columns.Add("VisitID")
            ds.Columns.Add("AppointmentTypeDesc")
            ds.Columns.Add("ProviderID")
            ds.Columns.Add("AppointmentNotes")
            ds.Columns.Add("PracticeLocationKey")
            ds.Columns.Add("AccNo")
            ds.Columns.Add("AttendingPhysician")
            ds.Columns.Add("checkindatetime")
            ds.Columns.Add("checkoutdatetime")



            If TotalCount > 0 Then
                lblAptCount.Text = "Total Appointmnts: " & TotalCount
                Dim counter As Integer = 0
                If Not appointments.SelectToken("appointments") Is Nothing Then
                    For Each Apt As Linq.JObject In appointments.SelectToken("appointments")
                        Dim DR As DataRow = ds.NewRow
                        ProcessJson(Apt, DR)
                        If Not DR Is Nothing Then ds.Rows.Add(DR)
                    Next
                End If
            End If
            ds.AcceptChanges()
            'ds.WriteXml("d:\1.xml")

            AptDataGrid.DataSource = ds
            AptDataGrid.DataBind()
            Return True
        Catch ex As Exception
            Response.Write(ex.Message & " " & ex.StackTrace)
            Return False
        End Try
    End Function
    Public Function ProcessJson(ByVal Apt As Linq.JObject, ByRef DR As DataRow) As DataRow
        Dim DS As New DataTable
        Try




            Dim Pat As Linq.JObject = Apt.SelectToken("patient")
            If Not Apt.SelectToken("date") Is Nothing Then DR("DtOfServ") = Apt.SelectToken("date")
            If Not Apt.SelectToken("appointmentid") Is Nothing Then DR("CaseNumber") = Apt.SelectToken("appointmentid")
            If Not Pat.SelectToken("firstname") Is Nothing Then DR("PFirstName") = Pat.SelectToken("firstname")
            If Not Pat.SelectToken("primarydepartmentid") Is Nothing Then DR("PriDepartmentID") = Pat.SelectToken("primarydepartmentid")
            If Not Pat.SelectToken("status") Is Nothing Then DR("AppointmentType") = Pat.SelectToken("status")
            If Not Pat.SelectToken("lastname") Is Nothing Then DR("PLastName") = Pat.SelectToken("lastname")
            If Not Pat.SelectToken("email") Is Nothing Then DR("PtEmail") = Pat.SelectToken("email")
            If Not Pat.SelectToken("suffix") Is Nothing Then DR("PtSuffix") = Pat.SelectToken("suffix")
            If Not Pat.SelectToken("departmentid") Is Nothing Then DR("DepartmentID") = Pat.SelectToken("departmentid")
            If Not Pat.SelectToken("mobilephone") Is Nothing Then DR("PtMobile") = Pat.SelectToken("mobilephone")
            If Not Pat.SelectToken("patientid") Is Nothing Then DR("PatientID") = Pat.SelectToken("patientid")

            If Not Pat.SelectToken("registrationdate") Is Nothing Then DR("DtTransac") = Pat.SelectToken("registrationdate")
            If Not Pat.SelectToken("countrycode") Is Nothing Then DR("PtCountryCode") = Pat.SelectToken("countrycode")
            If Not Pat.SelectToken("sex") Is Nothing Then DR("PGender") = Pat.SelectToken("sex")
            If Not Pat.SelectToken("dob") Is Nothing Then DR("PDOB") = Pat.SelectToken("dob")
            If Not Apt.SelectToken("guarantorrelationshiptopatient") Is Nothing Then DR("RelationToInsured") = Apt.SelectToken("guarantorrelationshiptopatient")
            If Not Apt.SelectToken("checkindatetime") Is Nothing Then DR("checkindatetime") = Apt.SelectToken("checkindatetime")
            If Not Apt.SelectToken("checkoutdatetime") Is Nothing Then DR("checkoutdatetime") = Apt.SelectToken("checkoutdatetime")
            If Not Apt.SelectToken("starttime") Is Nothing Then
                ' DR("AppointmentTime") = Apt.SelectToken("starttime")
                DR("DtOfServ") = Apt.SelectToken("date").ToString & " " & Apt.SelectToken("starttime").ToString
            End If

            If Not Apt.SelectToken("encounterstatus") Is Nothing Then
                DR("VisitStatus") = Apt.SelectToken("encounterstatus")
                'If Not VisitStatus = "2" And Not VisitStatus = "3" And Not VisitStatus = "x" Then
                '    Return True
                'End If
            End If

            If Not Apt.SelectToken("duration") Is Nothing Then DR("AppTime") = Apt.SelectToken("duration")
            If Not Apt.SelectToken("encounterid") Is Nothing Then
                DR("VisitID") = Apt.SelectToken("encounterid")
                'Else
                '    Return True
            End If

            If Not Apt.SelectToken("appointmenttype") Is Nothing Then DR("AppointmentTypeDesc") = Apt.SelectToken("appointmenttype")
            If Not Apt.SelectToken("providerid") Is Nothing Then DR("ProviderID") = Apt.SelectToken("providerid")
            If Not Apt.SelectToken("patientappointmenttypename") Is Nothing Then DR("AppointmentNotes") = Apt.SelectToken("patientappointmenttypename")
            'If Not Apt.SelectToken("PracticeLocationKey") Is Nothing then DR("PracticeLocationKey") = Apt.SelectToken("PracticeLocationKey")
            'If Not Apt.SelectToken("AccNo") Is Nothing then DR("AccNo") = Apt.SelectToken("AccNo")
            If Not Apt.SelectToken("AttendingPhysician") Is Nothing Then DR("AttendingPhysician") = Apt.SelectToken("AttendingPhysician")





            Return DR
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Function ToDataTable(Of T)(ByVal data As IList(Of T)) As DataTable
        Dim props As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim table As DataTable = New DataTable()

        For i As Integer = 0 To props.Count - 1
            Dim prop As PropertyDescriptor = props(i)
            table.Columns.Add(prop.Name, prop.PropertyType)
        Next

        Dim values As Object() = New Object(props.Count - 1) {}

        For Each item As T In data

            For i As Integer = 0 To values.Length - 1
                values(i) = props(i).GetValue(item)
            Next

            table.Rows.Add(values)
        Next

        Return table
    End Function
    Private Function GetToken() As Boolean
        Dim UTF8 As Encoding = System.Text.Encoding.GetEncoding("utf-8")
        Try


            ' Easier to keep track of OAuth prefixes
            Dim auth_prefixes As New Dictionary(Of String, String)() From {{"v1", "/oauth2/v1"},
            {"preview1", "/oauth2/v1"},
            {"openpreview1", "/oauthopenpreview"}}


            ' Basic access authentication
            Dim parameters As New Dictionary(Of String, String)() From {
            {"grant_type", "client_credentials"},
        {"scope", "athena/service/Athenanet.MDP.*"}
        }

            ' Create and set up a request
            Dim request As WebRequest = WebRequest.Create(PathJoin(baseUrl, auth_prefixes(version), "/token"))
            request.Method = "POST"
            request.ContentType = "application/x-www-form-urlencoded"

            ' Make sure to add the Authorization header
            Dim auth As String = System.Convert.ToBase64String(UTF8.GetBytes(Convert.ToString(key & Convert.ToString(":")) & secret))
            request.Headers("Authorization") = Convert.ToString("Basic ") & auth

            ' Encode the parameters, convert it to bytes (because that's how the streams want it)
            Dim encoded As String = UrlEncode(parameters)

            Dim content As Byte() = UTF8.GetBytes(encoded)

            ' Write the parameters to the body
            Dim writer As Stream = request.GetRequestStream()
            writer.Write(content, 0, content.Length)
            writer.Close()

            ' Get the response, read it out, and decode it
            Dim webResponse As WebResponse = request.GetResponse()
            Dim receive As Stream = webResponse.GetResponseStream()
            Dim reader As New StreamReader(receive, UTF8)
            Dim authorization As Linq.JObject
            authorization = Linq.JObject.Parse(reader.ReadToEnd())

            ' Make sure to grab the token!
            Session("token") = authorization("access_token")
            'Response.Write(Session("token"))

            ' And always remember to close the readers and streams
            webResponse.Close()

        Catch ex As Exception
            Response.Write(ex.Message)

        End Try
    End Function
    Public Shared Function UrlEncode(ByVal dict As Dictionary(Of String, String)) As String
        Return String.Join("&", dict.[Select](Function(kvp) WebUtility.UrlEncode(kvp.Key) + "=" + WebUtility.UrlEncode(kvp.Value)).ToList())
    End Function

    ' A useful function for joining paths into URLs
    Public Shared Function PathJoin(ByVal ParamArray args As String()) As String
        Return String.Join("/", args.[Select](Function(arg) arg.Trim(New Char() {"/"c})).Where(Function(arg) Not [String].IsNullOrEmpty(arg)))
    End Function

End Class
