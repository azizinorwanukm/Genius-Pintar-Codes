''Imports System.Data
''Imports System.Data.OleDb
Imports System.Data.SqlClient
''Imports System.IO
''Imports System.Globalization

Public Class config_examStudentQuestion
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                strRet = BindData(datRespondent)
                yearList()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY id"

        tmpSQL = "SELECT id,exam_name,examyear,quantity from Exams"
        strWhere += " WHERE exam_name IS NOT NULL "

        If ddlYearselect.SelectedValue IsNot "" Then
            strWhere += "AND examyear='" & ddlYearselect.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function



    Private Sub yearList()

        strSQL = "select distinct examyear from Exams "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "Anytable")

            ddlYearselect.DataSource = ds
            ddlYearselect.DataTextField = "examyear"
            ddlYearselect.DataValueField = "examyear"
            ddlYearselect.DataBind()
            ddlYearselect.Items.Insert(0, New ListItem("pilih Tahun", String.Empty))
            ddlYearselect.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 1200

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada Soalan Dalam Pangakalan Data"
            Else
                lblMsg.Text = "Jumlah Soalan : " & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsgTop.Text = "Error : " & ex.Message
            Return False
        End Try
        Return True
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanged
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Dim encryptdataId As String = oDes.EncryptData(strKeyID)
        Try
            Select Case getUserProfile_UserType()
                Case "Admin"
                    ''Response.Redirect(".sekolahupdatedata.aspx?studentschoolid=" & encryptdataId & "")
                    Response.Redirect("config_examStudentQuestionList.aspx?exam_id=" & datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString)
                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable

        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionMaster")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con

        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)

            Return dt

        Catch ex As Exception

            Throw ex

        Finally

            con.Close()
            sda.Dispose()
            con.Dispose()

        End Try

    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btn_Search_Click(sender As Object, e As EventArgs) Handles btn_Search.Click
        strRet = BindData(datRespondent)

    End Sub
End Class