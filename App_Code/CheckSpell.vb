Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Microsoft.Office.Interop
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class CheckSpell
    Inherits System.Web.Services.WebService
    Dim objWord As Word.Application
    <WebMethod()> _
    Public Function CheckWord(ByVal Str As String) As Boolean
        'CheckWord = objWord.CheckSpelling(Str)
        Return objWord.CheckSpelling(Str)
    End Function
    'Public Function SugeestWord(ByVal str As String) As String
    '    Dim varSuggestWords As Microsoft.Office.Interop.Word.SpellingSuggestions
    '    varSuggestWords = objWord.GetSpellingSuggestions(str)
    '    Return varSuggestWords
    'End Function
End Class
