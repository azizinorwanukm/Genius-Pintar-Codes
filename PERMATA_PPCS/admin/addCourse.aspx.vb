Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class addCourse
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

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


    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert into course list
            strSQL = "INSERT INTO ppcs_course(CourseType,CourseCode,CourseNameBM,CourseNameBI,PPCSDate) VALUES ('" & selCourseType.Value & "','" & oCommon.FixSingleQuotes(txtCourseCode.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtCourseNameBM.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtCourseNameBI.Text.ToUpper) & "','" & ddlPPCSDate.Text & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsgTop.Text = "Penambahan kursus telah berjaya"
                ClearScreen()
            Else
                lblMsgTop.Text = "system error:" & strRet
            End If

        Catch ex As Exception
            lblMsgTop.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtCourseCode.Text.Length = 0 Then
            lblMsgTop.Text = "Kod Kursus perlu di isi."
            txtCourseCode.Focus()
            Return False
        End If

        If txtCourseNameBM.Text.Length = 0 Then
            lblMsgTop.Text = "Nama Kursus (BM) perlu di isi."
            txtCourseNameBM.Focus()
            Return False
        End If

        If txtCourseNameBI.Text.Length = 0 Then
            lblMsgTop.Text = "Nama Kursus (BI) perlu di isi."
            txtCourseNameBI.Focus()
            Return False
        End If

        ''if Exist
        strSQL = "SELECT courseCode FROM ppcs_course WHERE CourseCode='" & txtCourseCode.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsgTop.Text = "Kod Kursus telah digunakan! Sila guna Kod Kursus yang baru."
            Return False
        End If

        Return True
    End Function

    Private Sub ClearScreen()
        lblMsg.Text = ""

        txtCourseCode.Text = ""
        txtCourseNameBI.Text = ""
        txtCourseNameBM.Text = ""

        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod ditemui!"
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
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

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        strRet = BindData(datRespondent)

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

End Class