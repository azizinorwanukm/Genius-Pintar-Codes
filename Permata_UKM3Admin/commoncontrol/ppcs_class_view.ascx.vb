Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class ppcs_class_view
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")
    Dim strClassID As String
    Dim strTestID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'Query string
                strClassID = Request.QueryString("classid")
                Load_classdetail(strClassID)
            End If

        Catch ex As Exception
            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            '  oCommon.WriteLogFile(strPath, strMsg)

        End Try

    End Sub

    Private Sub Load_classdetail(ByVal strValue As String)
        ''--strSQL = "SELECT * FROM ppcs_class WHERE ClassID='" & strClassID & "'"
        strSQL = "SELECT a.CourseID,a.ClassCode,a.ClassNameBM,a.ClassNameBI,b.CourseCode,b.CourseNameBM,b.CourseNameBI FROM ppcs_class a,ppcs_course b WHERE ClassID=" & strValue & " and a.CourseID=b.courseid"
        '--debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseCode")) Then
                    txtPPCSCourse.Text = ds.Tables(0).Rows(0).Item("CourseCode")
                Else
                    txtPPCSCourse.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseNameBM")) Then
                    txtCourseNameBM.Text = ds.Tables(0).Rows(0).Item("CourseNameBM")
                Else
                    txtCourseNameBM.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ClassCode")) Then
                    txtClassCode.Text = ds.Tables(0).Rows(0).Item("ClassCode")
                Else
                    txtClassCode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ClassNameBM")) Then
                    txtClassNameBM.Text = ds.Tables(0).Rows(0).Item("ClassNameBM")
                Else
                    txtClassNameBM.Text = ""
                End If
            End If

        Catch ex As Exception
            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            '  oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

End Class