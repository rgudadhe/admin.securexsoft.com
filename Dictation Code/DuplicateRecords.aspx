<%@ Page Language="VB"%>
<script  type="text/VB" runat="server" >
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error Resume Next
        Dim upl As New SoftArtisans.Net.FileUp(Context)
        Dim DictCode As String
        
        Dim DupRec As Boolean = False
        Dim DupText As String = String.Empty
        For j As Integer = 1 To upl.Form("HDictCode").ToString
            DictCode = "DictCode" & j
            Dim sQuery2 As String
            
            Dim clsDC As New ETS.BL.DictationCodes
            
            If clsDC.CheckDictationCodeExist(upl.Form("HActID").ToString, upl.Form(DictCode).ToString) = True Then
                DupRec = True
                If DupText = String.Empty Then
                    DupText = upl.Form(DictCode).ToString
                Else
                    DupText = DupText & "," & upl.Form(DictCode).ToString
                End If
            End If
            clsDC = Nothing
        Next
        Response.Write(DupRec & "#@" & DupText)
        'Response.Write(upl.Form("HDictCode").ToString)
    End Sub
   
</script>
