Imports System
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml
Imports System.Data
Imports System.IO

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class DataShare
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function InsertPatientInfo(ByVal Password As String, ByVal XmlDocument As XmlDocument) As Integer
        
        Try
            If Not Password = "liadf!80z18hoif8dh8s3hos7uh9f!" Then
                Return -1
            Else
                Dim xmlFile As String = Date.Now.Year & Date.Now.Month & Date.Now.Day & Date.Now.Hour & Date.Now.Minute & Date.Now.Second & Date.Now.Millisecond & ".xml"
                Dim ds As New DataSet
                ds.ReadXml(New XmlNodeReader(XmlDocument))

                ds.WriteXml(Path.Combine("\\hl7server\d$\ETS HL7\SCA Receiver", xmlFile))
                ds.Dispose()
                If File.Exists(Path.Combine("\\hl7server\d$\ETS HL7\SCA Receiver", xmlFile)) Then
                    Return ValidateXML(XmlDocument)
                Else
                    Return -5
                End If
            End If
        Catch ex As System.Exception
            Return -10
        End Try
    End Function
    Private Function ValidateXML(ByVal xmlDoc As XmlDocument) As Integer
        Try
            Dim ndMessage As XmlDocument = New XmlDocument()
            ndMessage = xmlDoc
            Dim oNode As XmlNode = ndMessage.DocumentElement
            For Each chNode As XmlNode In oNode.ChildNodes
                If chNode.Name = "PatientData" Then
                    For Each item As XmlNode In chNode.ChildNodes
                        If item.Name = "Patient" Then
                            For Each Attribute As XmlAttribute In item.Attributes
                                If Attribute.Name = "Accountnumber" Then
                                    If Attribute.InnerXml.Trim = "" Then
                                        Return -3
                                    End If
                                End If
                            Next
                            For Each chItem As XmlNode In item.ChildNodes
                                If chItem.Name = "Addresses" Then
                                    'For Each ch As XmlNode In chItem.ChildNodes
                                    '    If ch.Name = "Address" Then
                                    '    End If
                                    'Next
                                ElseIf chItem.Name = "PatientVisit" Then
                                    For Each ch As XmlNode In chItem.ChildNodes
                                        If ch.Name = "VisitInformation" Then
                                            For Each Attribute As XmlAttribute In ch.Attributes
                                                'If Attribute.Name = "VisitNumber" Then
                                                'If Attribute.InnerXml.Trim = "" Then
                                                '    Return -2
                                                'End If
                                                'Else
                                                If Attribute.Name = "ScheduledDate" Then
                                                    If Attribute.InnerXml.Trim = "" Then
                                                        Return -4
                                                    End If
                                                End If
                                            Next
                                            'ElseIf ch.Name = "PatientProcedures" Then
                                            'ElseIf ch.Name = "PatientEquipment" Then
                                            'ElseIf ch.Name = "PatientGuarantor" Then
                                            'ElseIf ch.Name = "PatientInsurances" Then
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    Next
                End If
            Next
            Return 1
        Catch ex As Exception
            Return -10
        End Try
    End Function

    <WebMethod()> _
    Public Function GetReturnCodeStr(ByVal Password As String, ByVal ReturnCode As Integer) As String
        If Not Password = "liadf!80z18hoif8dh8s3hos7uh9f!" Then
            Return -1
        Else
            Select Case ReturnCode
                Case 1
                    Return "Transaction Successful."
                Case -20
                    Return "Invalid Login or Password"
                Case -2
                    Return "Missing Visit Number"
                Case -3
                    Return "Missing Patient Account Number"
                Case -4
                    Return "Missing Scheduled Date"
                Case -5
                    Return "Web Service Error - XMLDocument not found"
                Case -10
                    Return "Web Service Error - Coding Error"
            End Select
        End If
        Return ""
    End Function


End Class
