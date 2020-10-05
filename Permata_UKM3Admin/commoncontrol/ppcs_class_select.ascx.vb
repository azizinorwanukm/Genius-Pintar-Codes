Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class ppcs_class_select
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
        Dim strSQL As String
        Dim strRet As String
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim straction As String = ""
        Dim nPageno As Integer
        Dim strCourseID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strCourseID = Request.QueryString("courseid")
            If Not IsPostBack Then
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

        End Try

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


    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY ClassCode"

        tmpSQL = "SELECT ClassID,ClassCode,ClassNameBM,ClassNameBI,(SELECT COUNT(*) FROM PPCS WHERE PPCS.ClassID=PPCS_Class.ClassID AND PPCS.PPCSStatus='LAYAK') as PelajarCount FROM PPCS_Class"
        strWhere = " WITH (NOLOCK) WHERE CourseID=" & strCourseID

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        '--Response.Write(strKeyID)

        '--Response.Write(strKeyID)
        'Dim strMod As String = Request.QueryString("mod")
        'Select Case strMod
        '    Case "01"
        '        Response.Redirect("laporan.pelajar.class.aspx?mod=" & strMod & "&courseid=" & Request.QueryString("courseid") & "&classid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate"))
        '    Case "02"
        Response.Redirect("admin.laporan.keseluruhan.student.list.aspx?courseid=" & Request.QueryString("courseid") & "&classid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate"))
        '    Case "03"
        '        Response.Redirect("laporan.keseluruhan.student.list.aspx?mod=" & strMod & "&courseid=" & Request.QueryString("courseid") & "&classid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate"))
        '    Case "04"
        '        Response.Redirect("pelajar.class.assign.03.aspx?mod=" & strMod & "&courseid=" & Request.QueryString("courseid") & "&classid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate"))
        '    Case "10"
        '        Response.Redirect("cetak.laporan.akhir.aspx?mod=" & strMod & "&courseid=" & Request.QueryString("courseid") & "&classid=" & strKeyID & "&ppcsdate=" & Request.QueryString("ppcsdate"))
        '    Case Else
        '        lblMsg.Text = "Invalid page module! " & strMod
        'End Select

    End Sub

End Class