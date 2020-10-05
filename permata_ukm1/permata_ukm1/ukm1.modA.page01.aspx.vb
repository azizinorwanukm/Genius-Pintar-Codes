Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class ukm1_modA_page01
    Inherits System.Web.UI.Page

    Private rm As ResourceManager
    Dim ci As CultureInfo

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '--go to last page. user try to keyin manually
            strSQL = "SELECT Status FROM UKM1 WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            If oCommon.getFieldValue(strSQL) = "DONE" Then
                Response.Redirect("ukm1.permata.end.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
            End If

            LoadImages(oCommon.getQuestionYear(Request.QueryString("studentid")))

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("lang"))
            rm = New ResourceManager("Resources.UKM" & oCommon.getQuestionYear(Request.QueryString("studentid")), System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            '--set initial value. nWrongCounter=0
            SavePage()


        Catch ex As Exception
            lblmodAFirst.Text = ex.Message

        End Try
    End Sub


    Private Sub LoadImages(ByVal strSet As String)
        Image0.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/A1.0.gif"
        Image1.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/A1.1.gif"
        Image2.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/A1.2.gif"
        Image3.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/A1.3.gif"
        Image4.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/A1.4.gif"
        Image5.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/A1.5.gif"
        Image6.ImageUrl = strSet & "/" & Request.QueryString("lang") & "/img/A1.6.gif"

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        lblModuleA.Text = rm.GetString("lblModuleA", ci)
        lblInstructionModuleA.Text = rm.GetString("lblInstructionModuleA", ci)
        lblmodAFirst.Text = rm.GetString("lblmodAFirst", ci)

    End Sub

    Private Sub SavePage()

        strSQL = "SELECT TOP 1 UKM1ID FROM UKM1 WHERE StudentID = '" & Request.QueryString("studentid") & "' AND ExamYear = '" & oCommon.getAppsettings("UKM1ExamYear") & "'"

        Dim ukm1id As String = oCommon.getFieldValue(strSQL)

        '--student answered before
        strSQL = "SELECT ExamStart FROM UKM1 WHERE UKM1ID = " & ukm1id
        strRet = oCommon.getFieldValue(strSQL)
        If strRet.Length > 0 Then
            Exit Sub
        End If

        Dim nWrongCounter As Integer = 0

        strSQL = " UPDATE UKM1 SET ExamStart='" & oCommon.getNow & "',LastPage='ukm1.modA.page01.aspx?',ExamEnd='" & oCommon.getNow & "',WrongCounter=" & nWrongCounter.ToString & " WHERE UKM1ID = " & ukm1id
        'Debug.WriteLine(strSQL)
        strRet = oCommon.ExecuteSQL(strSQL)

        If Not strRet = "0" Then
            lblmodAFirst.Text = strRet
        End If

        Dim ukm1Table As String = Common.getUKM1Table(oCommon.getAppsettings("UKM1ExamYear"))

        strSQL = "DELETE FROM " & ukm1Table & " WHERE UKM1ID = " & ukm1id
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then

            strSQL = "INSERT INTO " & ukm1Table & " SELECT * FROM UKM1 WHERE UKM1ID = " & ukm1id
            strRet = oCommon.ExecuteSQL(strSQL)

        End If

    End Sub


End Class