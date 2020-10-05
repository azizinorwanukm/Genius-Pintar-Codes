Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class ukm1_open_end
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim nMark As Integer

    Private rm As ResourceManager
    Dim ci As CultureInfo
    Dim strSystemAns As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("lang"))
            rm = New ResourceManager("Resources.UKM" & oCommon.getQuestionYear(Request.QueryString("studentid")), System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            ''--reset wrong counter
            Dim nWrongCounter As Integer = 0

            Dim ukm1Table As String = Common.getUKM1Table(oCommon.getAppsettings("UKM1ExamYear"))

            strSQL = ""

            If Not ukm1Table = "UKM1" Then
                strSQL = "UPDATE " & ukm1Table & " SET WrongCounter=" & nWrongCounter.ToString & ",LastPage='ukm1.open.end.aspx?' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
                'oCommon.ExecuteSQL(strSQL)
            End If

            strSQL += " UPDATE UKM1 SET WrongCounter=" & nWrongCounter.ToString & ",LastPage='ukm1.open.end.aspx?' WHERE StudentID='" & Request.QueryString("studentid") & "' AND ExamYear='" & oCommon.getAppsettings("UKM1ExamYear") & "'"
            'Debug.WriteLine(strSQL)
            strRet = oCommon.ExecuteSQL(strSQL)

        Catch ex As Exception
            lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        lblOpenEnd.Text = rm.GetString("lblOpenEnd", ci)
        lblOpenEndMsg.Text = rm.GetString("lblOpenEndMsg", ci)

        btnNext.Text = rm.GetString("btnNext", ci)
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        ''--start with questions number 105
        Response.Redirect("ukm1.select.page00.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid") & "&q=105")

    End Sub

End Class