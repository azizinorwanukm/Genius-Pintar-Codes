Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class admin_assignClass
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strClassCode As String
    Dim strClassid As String
    Dim strUserType As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strUserType = Request.QueryString("usertype")
            lblUserType.Text = strUserType

            If Not IsPostBack Then
                ppcsdate_list()


                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            '  oCommon.WriteLogFile(strPath, strMsg)

        End Try
    End Sub

    Private Sub ppcsdate_list()
        '--base on usertype. admin only allow all
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT PPCSDate FROM PPCS GROUP BY PPCSDate"

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        For k = 0 To quantity - 1
            ddlPPCSDate.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(0).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

        Dim currentPpcs As String = Commonfunction.getSingleCellValue("SELECT configString FROM master_Config WHERE configCode = 'DefaultPPCSDate'")
        ddlPPCSDate.SelectedValue = currentPpcs


    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY b.coursecode,a.ClassCode"

        tmpSQL = "SELECT a.ClassID,a.ClassCode,b.CourseCode,a.ClassNameBM,a.ClassNameBI,a.Pengajar,a.NamaPengajar,a.PembantuPengajar,a.NamaPembantuPengajar,a.PengurusPelajar,a.NamaPengurusPelajar,a.PembantuPelajar,a.NamaPembantuPelajar,b.CourseNameBM FROM PPCS_Class a,PPCS_Course b"
        strWhere = " WITH (NOLOCK) WHERE a.CourseID=b.CourseID AND b.PPCSDate='" & ddlPPCSDate.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            lblMsg.Text = myDataSet.Tables(0).Rows.Count
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Record not found!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
            Return False
        End Try

        Return True
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        '--Response.Write(strKeyID)
        Response.Redirect("admin_listPengguna.aspx?classid=" & strKeyID)

    End Sub


    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        strRet = BindData(datRespondent)

    End Sub

End Class