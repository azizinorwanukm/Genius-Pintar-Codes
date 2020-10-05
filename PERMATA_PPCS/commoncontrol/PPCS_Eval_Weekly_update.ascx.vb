Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class PPCS_Eval_Weekly_update1
    Inherits System.Web.UI.UserControl

    Private rm As ResourceManager

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer = 0
    Dim strDateCreated As String
    Dim strcourseCode As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''--todays date
        strDateCreated = oCommon.getToday

        Try

            Dim ci As CultureInfo
            Dim strBasename As String = "Resources.eval2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            If Not IsPostBack Then
                ppcs_eval_weekly_load()

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub resetall_remarks()
        Q001Remarks.Text = ""
        Q002Remarks.Text = ""
        Q003Remarks.Text = ""

    End Sub

    Private Sub ppcs_eval_weekly_load()
        ''RESET
        resetall_remarks()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM ppcs_eval_weekly WHERE ppcsevalweekid=" & Request.QueryString("ppcsevalweekid")
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q001Remarks")) Then
                    Q001Remarks.Text = MyTable.Rows(nRows).Item("Q001Remarks").ToString
                Else
                    Q001Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q002Remarks")) Then
                    Q002Remarks.Text = MyTable.Rows(nRows).Item("Q002Remarks").ToString
                Else
                    Q002Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q003Remarks")) Then
                    Q003Remarks.Text = MyTable.Rows(nRows).Item("Q003Remarks").ToString
                Else
                    Q003Remarks.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        '--debug
        btnUpdate.Text = rm.GetString("btnUpdate", ci)

        lblQ001.Text = rm.GetString("WQ001", ci)
        lblQ002.Text = rm.GetString("WQ002", ci)
        lblQ003.Text = rm.GetString("WQ003", ci)

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        ppcs_eval_update()

    End Sub

    Private Function ppcs_eval_update() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If

        strSQL = "UPDATE ppcs_eval_weekly SET LastUpdate='" & oCommon.getNow & "',Q001Remarks='" & oCommon.FixSingleQuotes(Q001Remarks.Text) & "',Q002Remarks='" & oCommon.FixSingleQuotes(Q002Remarks.Text) & "',Q003Remarks='" & oCommon.FixSingleQuotes(Q003Remarks.Text) & "' WHERE ppcsevalweekid=" & Request.QueryString("ppcsevalweekid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsgTop.Text = "Rekod berjaya dikemaskini."
            lblMsg.Text = "Rekod berjaya dikemaskini."
        Else
            lblMsgTop.Text = strRet
            lblMsg.Text = strRet
        End If

        Return True
    End Function

    Private Function ValidatePage() As Boolean
        ''--do nothing

        Return True
    End Function

    Protected Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkStudentProfileView.Click
        Response.Redirect("ppcs.alumni.studentprofile.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub
End Class