Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class main5
    Inherits System.Web.UI.MasterPage

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strLoginID As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value) = "" Then
                Response.Redirect("../default.aspx")
            Else
                lblUsername.Text = Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value)
            End If
            lblDate.Text = Now.Date.ToString("dddd dd-MM-yyyy")

            If Not IsPostBack Then
                getHeader()
            End If

        Catch ex As Exception
            lblFooterMsg.Text = "Page_Load:" & ex.Message
        End Try

    End Sub

    Private Sub getHeader()
        ''the base is class
        'Response.Write("ClassID:" & Server.HtmlEncode(Request.Cookies("ppcs_classid").Value))
        Dim strClassID As String = ""
        strClassID = Server.HtmlEncode(Request.Cookies("ppcs_classid").Value)

        ''get class code
        Dim strClassCode As String = ""
        strSQL = "SELECT ClassCode FROM PPCS_Class WHERE ClassID=" & strClassID
        strClassCode = oCommon.getFieldValue(strSQL)
        'Response.Write("strClassCode:" & strClassCode)
        lblClassCode.Text = strClassCode

        ''get CourseID
        Dim strCourseID As String = ""
        strSQL = "SELECT CourseID FROM PPCS_Class WHERE ClassID=" & strClassID
        strCourseID = oCommon.getFieldValue(strSQL)
        'Response.Write("strCourseID:" & strCourseID)

        ' ''get CourseCode
        Dim strCourseCode As String = ""
        strSQL = "SELECT CourseCode FROM PPCS_Course WHERE CourseID=" & strCourseID
        strCourseCode = oCommon.getFieldValue(strSQL)
        'Response.Write("strCourseCode:" & strCourseCode)
        lblCourseCode.Text = strCourseCode

    End Sub

End Class