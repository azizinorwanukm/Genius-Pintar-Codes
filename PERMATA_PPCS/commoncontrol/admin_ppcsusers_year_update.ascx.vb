Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class admin_ppcsusers_year_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '-load list
                ppcsdate_list()
                usertype_list()

                'lod profile
                PPCS_Users_Year_load()

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

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

            '--source
            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub usertype_list()
        strSQL = "SELECT usertype FROM master_PPCS_UserType WHERE UserType<>'ADMIN' ORDER BY UserType"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUserType.DataSource = ds
            ddlUserType.DataTextField = "usertype"
            ddlUserType.DataValueField = "usertype"
            ddlUserType.DataBind()

            ddlUserType.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub PPCS_Users_Year_load()
        strSQL = "SELECT * FROM PPCS_Users_Year WHERE ppcsuseryearid=" & Request.QueryString("ppcsuseryearid")
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PPCSDate")) Then
                    ddlPPCSDate.Text = ds.Tables(0).Rows(0).Item("PPCSDate")
                Else
                    ddlPPCSDate.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UserType")) Then
                    ddlUserType.Text = ds.Tables(0).Rows(0).Item("UserType")
                Else
                    ddlUserType.Text = ""
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

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click
        strSQL = "UPDATE PPCS_Users_Year SET Usertype='" & ddlUserType.Text & "',PPCSDate='" & ddlPPCSDate.Text & "' WHERE ppcsuseryearid=" & Request.QueryString("ppcsuseryearid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya kemaskini maklumat tersebut."
        Else
            lblMsg.Text = "Error:" & strRet
        End If

    End Sub

    Protected Sub lnkppcsuserlist_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkppcsuserlist.Click
        Response.Redirect("ppcs.user.list.aspx")

    End Sub
End Class