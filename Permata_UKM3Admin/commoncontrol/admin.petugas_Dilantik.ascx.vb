Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Resources

Public Class admin_petugas_Dilantik

    Inherits System.Web.UI.UserControl

        Dim oCommon As New Commonfunction
        Dim strSQL As String = ""
        Dim strRet As String = ""
        Dim oDes As New Simple3Des("p@ssw0rd1")

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        'Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strconn)

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            btnRemove.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

            Try
                If Not IsPostBack Then
                    strRet = BindData(datRespondent)
                    'Debug.WriteLine("try:", CType(Session.Item("permata_admin"), String))
                    ukm3_DateList()
                usertype_list()
                End If

            Catch ex As Exception
                lblMsg.Text = ex.Message

            End Try
        End Sub

    Private Sub ukm3_DateList()
        strSQL = "  SELECT description FROM master where type = 'session' order by data_id"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--destination
            ddl_Sesi.DataSource = ds
            ddl_Sesi.DataTextField = "description"
            ddl_Sesi.DataValueField = "description"
            ddl_Sesi.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub usertype_list()
            strSQL = "SELECT description FROM master where type = 'userType' order by data_id "
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Try
                Dim ds As DataSet = New DataSet
                sqlDA.Fill(ds, "AnyTable")

            ddlJawatan.DataSource = ds
            ddlJawatan.DataTextField = "description"
            ddlJawatan.DataValueField = "description"
            ddlJawatan.DataBind()
            ddlJawatan.Items.Insert(0, New ListItem("Sila Pilih", String.Empty))
            ddlJawatan.SelectedIndex = 0

        Catch ex As Exception
                lblMsg.Text = "Database error!" & ex.Message
            Finally
                objConn.Dispose()
            End Try

        End Sub


        Private Function BindData(ByVal gvTable As GridView) As Boolean
            Dim myDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(getSQL, strconn)
            myDataAdapter.SelectCommand.CommandTimeout = 1200

            Try
                myDataAdapter.Fill(myDataSet, "myaccount")

                If myDataSet.Tables(0).Rows.Count = 0 Then
                    lblMsg.Text = "Record not found!"
                Else
                    lblMsg.Text = "Total users #:" & myDataSet.Tables(0).Rows.Count
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
            strRet = BindData(datRespondent)
        End Sub

        Private Function getSQL() As String
            Dim tmpSQL As String
            Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY A.staff_Position ASC"

        ''tmpSQL = "SELECT stf_id,staff_name,staff_login,staff_Password,staff_session,staff_Position FROM staff_info"
        tmpSQL = "SELECT A.staff_id ,A.staff_name,A.staff_login,A.staff_year,A.staff_Position,A.staff_session,A.staff_Position as staff_Position FROM staff_info A
                    "
        strWhere = " WHERE A.staff_id is not null AND A.isAllow='1'"

        If Not txtFullname.Text.Length = 0 Then
            strWhere += " AND A.staff_name LIKE '%" & txtFullname.Text & "%'"
        End If

        If Not ddl_Sesi.SelectedValue = "" Then
            strWhere += " AND A.staff_session = '" & ddl_Sesi.SelectedValue & "'"
        End If
        If Not ddlJawatan.SelectedValue = "" Then
            strWhere += " AND A.staff_Position = '" & ddlJawatan.SelectedValue & "'"
        End If
        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

        End Function


    Private Function getPPCSDate(ByVal strKeyID As String) As String
            Dim strValue As String = ""
            strSQL = "SELECT PPCSDate,Usertype FROM PPCS_Users_Year WHERE myGUID='" & strKeyID & "'"
            strValue = oCommon.getRowValue(strSQL)

            Return strValue
        End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging

        Try
            Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Values(0).ToString
            Dim strUserID As String = datRespondent.DataKeys(e.NewSelectedIndex).Values(1).ToString
            Dim strLoginID As String = datRespondent.DataKeys(e.NewSelectedIndex).Values(2).ToString

            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("Ukm3.userDetail.aspx?myguid=" & strKeyID & "&fullname=" & strUserID & "&login=" & strLoginID)
                Case Else
            End Select

        Catch ex As Exception
            lblMsgTop.Text = "Error :" & ex.ToString
        End Try
    End Sub

    Private Function getUserProfile_UserType() As String
            'strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
            'strRet = oCommon.getFieldValue(strSQL)
            Try
                strSQL = "SELECT top 1 staff_position from staff_info with (NOLOCK) where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'  "
                strRet = oCommon.getFieldValue(strSQL)

            Catch ex As Exception
                lblMsg.Text = "Error:" & ex.Message
            End Try

            Return strRet
        End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemove.Click
            lblMsg.Text = ""
        lblMsgTop.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            Try
                If Not chkUpdate Is Nothing Then
                    If chkUpdate.Checked = True Then
                        ' Get the values of textboxes using findControl

                        Dim strKeyID As String = datRespondent.DataKeys(i).Values(0).ToString
                        Dim strUserID As String = datRespondent.DataKeys(i).Values(1).ToString
                        Dim strLoginID As String = datRespondent.DataKeys(i).Values(2).ToString


                        '--DELETE 
                        strSQL = $"UPDATE ukm3.dbo.staff_info SET ukm3.dbo.staff_info.isAllow = 0
                                where ukm3.dbo.staff_info.staff_name='{strUserID}' AND ukm3.dbo.staff_info.staff_id='{strKeyID}' AND ukm3.dbo.staff_info.staff_login='{strLoginID}' "
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text += "Delete error:" & strRet
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya Remove Ukm3 Users."
            lblMsgTop.Text = lblMsg.Text

            strRet = BindData(datRespondent)
        End If

    End Sub

    End Class
