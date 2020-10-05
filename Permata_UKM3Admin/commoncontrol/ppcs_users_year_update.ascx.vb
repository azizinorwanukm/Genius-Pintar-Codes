Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ppcs_users_year_update1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '-load list
                session_list()
                usertype_list()
                assign_session()
                assign_position()

                'lod profile
                'PPCS_Users_Year_load()

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

        End Try

    End Sub

    Private Sub session_list()
        strSQL = "SELECT description FROM master where type = 'session'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--source
            ddlsession.DataSource = ds
            ddlsession.DataTextField = "description"
            ddlsession.DataValueField = "description"
            ddlsession.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub usertype_list()
        strSQL = "SELECT description FROM master where type = 'userType'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlposition.DataSource = ds
            ddlposition.DataTextField = "description"
            ddlposition.DataValueField = "description"
            ddlposition.DataBind()

            ddlposition.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub assign_session()

        Dim session_id As String = oDes.DecryptData(Request.QueryString("session"))
        strSQL = "select session_ukm from session where session_id = '" & session_id & "' "
        Dim session As String = oCommon.getFieldValue(strSQL)

        ddlsession.SelectedValue = session

    End Sub
    Private Sub assign_position()
        Dim session_id As String = oDes.DecryptData(Request.QueryString("session"))
        strSQL = "select staff_position from session where session_id = '" & session_id & "' "
        Dim position As String = oCommon.getFieldValue(strSQL)

        ddlposition.SelectedValue = position
    End Sub



    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click

        Dim session_id As String = oDes.DecryptData(Request.QueryString("session"))
        Dim staff_id As String = oDes.DecryptData(Request.QueryString("myguid"))

        strSQL = "UPDATE session SET session_ukm ='" & ddlsession.SelectedValue & "',staff_position='" & ddlposition.SelectedValue & "' WHERE session_id = '" & session_id & "' and stf_id = '" & staff_id & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya kemaskini maklumat tersebut."

        Else
            lblMsg.Text = "Error:" & strRet
        End If

    End Sub

    Protected Sub lnkppcsuserlist_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkppcsuserlist.Click
        Select Case getUserProfile_UserType()
            Case "Admin"
                Response.Redirect("ukm3_admin.liststaff.aspx")
            Case Else
                lblMsg.Text = "Invalid user type!"
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String


        strSQL = "SELECT staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


End Class