Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class delete_Course
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strCourseCode As String
    Dim strCourseid As String
    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btndelete.Attributes.Add("onclick", "return confirm('Pasti hendak menghapuskan kursus tersebut?');")

        Try
            strCourseid = Request.QueryString("courseid")
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                Load_courseDetails(strCourseid)
                ClearScreen()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

        End Try
    End Sub

    Private Sub ppcsdate_list()
        '--base on usertype. admin only allow all
        strSQL = oCommon.PPCSDate_Query(Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value))


        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

            'ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ClearScreen()
        ''lblMsg.Text = ""

        lblTotal.Text = ""

        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            lblTotal.Text = myDataSet.Tables(0).Rows.Count
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Record not found!" & ex.Message
            Return False
        End Try

        Return True
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        ClearScreen()

        nPageno = e.NewPageIndex + 1


    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        '--Response.Write(strKeyID)
        Response.Redirect("delete.Course.aspx?courseid=" & strKeyID)

    End Sub

    Private Sub Load_courseDetails(ByVal strValue As String)
        strSQL = "SELECT * FROM ppcs_course WHERE CourseID='" & strValue & "'"
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
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseCode")) Then
                    txtCourseCode.Text = ds.Tables(0).Rows(0).Item("CourseCode")
                Else
                    txtCourseCode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseNameBM")) Then
                    txtCourseNameBM.Text = ds.Tables(0).Rows(0).Item("CourseNameBM")
                Else
                    txtCourseNameBM.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseNameBI")) Then
                    txtCourseNameBI.Text = ds.Tables(0).Rows(0).Item("CourseNameBI")
                Else
                    txtCourseNameBI.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSDate")) Then
                    ddlPPCSDate.Text = ds.Tables(0).Rows(0).Item("PPCSDate")
                Else
                    ddlPPCSDate.Text = Now.Year
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseType")) Then
                    selCourseType.Value = ds.Tables(0).Rows(0).Item("CourseType")
                Else
                    selCourseType.Value = "PRIMARY"
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btndelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btndelete.Click

        Try
            ''--check if pelajr already used it.2009
            strSQL = "SELECT CourseID FROM PPCS WHERE CourseID=" & Request.QueryString("courseid")
            If oCommon.isExist(strSQL) Then
                lblMsg.Text = "Terdapat pelajar menggunakan kod kursus tersebut. Sila HAPUSKAN senarai pelajar tersebut terlebih dahulu."
                Exit Sub
            End If

            'DELETE FROM ppcs_course
            strSQL = "DELETE FROM PPCS_Course WHERE CourseID=" & strCourseid
            strRet = oCommon.ExecuteSQL(strSQL)
            lblMsg.Text = "Kod Kursus Berjaya dihapuskan."

            ClearScreen()

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub


    Protected Sub btnkemaskini_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnkemaskini.Click
        Try

            strSQL = "UPDATE PPCS_Course SET CourseCode='" & oCommon.FixSingleQuotes(txtCourseCode.Text.ToUpper) & "',CourseNameBI='" & oCommon.FixSingleQuotes(txtCourseNameBI.Text.ToUpper) & "',CourseNameBM='" & oCommon.FixSingleQuotes(txtCourseNameBM.Text.ToUpper) & "',PPCSDate='" & ddlPPCSDate.Text & "',CourseType='" & selCourseType.Value & "' WHERE CourseID=" & strCourseid
            strRet = oCommon.ExecuteSQL(strSQL)
            lblMsg.Text = "Berjaya kemaskini kursus."

            ''--load gridview
            ClearScreen()

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY  PPCSDate,CourseType,CourseCode,CourseNameBM ASC"

        tmpSQL = "SELECT * FROM PPCS_Course"
        strWhere = " WITH (NOLOCK) WHERE PPCSDate='" & ddlPPCSDate.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        strRet = BindData(datRespondent)

    End Sub
End Class