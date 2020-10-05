Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class ppcs_class_select_session
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
            If Not IsPostBack Then
                strRet = BindData(datRespondent)

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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            lblTotal.Text = myDataSet.Tables(0).Rows.Count
            If lblTotal.Text = "0" Then
                lblTotal.Text = "TIADA KELAS DITETAPKAN!"
            End If

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

    Private Function getSQL() As String
        'Dim strClassID As String = Server.HtmlEncode(Request.Cookies("ppcs_classid").Value)

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY ClassCode"

        tmpSQL = "SELECT * FROM PPCS_Class"
        strWhere = " WITH (NOLOCK) WHERE PPCSDate='" & oCommon.getAppsettings("DefaultPPCSDate") & "'"

        Dim strGUID As String = Server.HtmlEncode(Request.Cookies("ppcs_myguid").Value)
        Dim strUserType As String = Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
        Select Case strUserType
            Case "KETUA MODUL"

            Case "PEMBANTU PELAJAR"
                strWhere += " AND PembantuPelajar='" & strGUID & "'"

            Case Else

        End Select

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Cookies("ppcs_classid").Value = strKeyID

        '--Response.Write(strKeyID)

        '--Response.Write(strKeyID)
        Dim strMod As String = Request.QueryString("mod")
        Select Case strMod
            Case "01"
            Case "02"
            Case "03"
            Case "04"
            Case "05"
                Response.Redirect("laporan.harian.aspx?classid=" & strKeyID & "&mod=" & strMod)

            Case Else
                lblMsg.Text = "Invalid page module! " & strMod
        End Select


    End Sub

End Class