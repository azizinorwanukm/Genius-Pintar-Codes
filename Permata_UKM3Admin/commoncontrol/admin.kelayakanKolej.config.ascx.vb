Imports System.Data.SqlClient

Public Class admin_kelayakanKolej_config1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

    'Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strconn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging

        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Select Case getUserProfile_UserType()
            Case "Admin"
                Response.Redirect("admin.config_session_edit.aspx?session=" & strKeyID)
            Case Else

        End Select

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Response.Redirect("admin.config_session_add.aspx")
    End Sub


    Private Sub btnEnable_Click(sender As Object, e As EventArgs) Handles btnEnable.Click
        Try
            Dim sql As String = "UPDATE general_config SET parameter = " & txtkolejDateStart.Text & " WHERE config = 'kolejStartDate' "

            sql += "UPDATE general_config SET parameter = " & txtkolejDateEnd.Text & " WHERE config = 'kolejEndDate' "

            'sql += "UPDATE general_config SET parameter = " & ddlSesiKolej.SelectedValue & " WHERE config = 'kolejSesi' "

            oCommon.ExecuteSQL(sql)

        Catch ex As Exception
            lblMsgTop.Text = "Kemaskini gagal."
        End Try

        lblMsgTop.Text = "Kemaskini berjaya."

        ''Response.Redirect("admin.session_config.aspx")
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

    'Private Function BindData(ByVal gvTable As GridView) As Boolean
    '    Dim myDataSet As New DataSet
    '    Dim myDataAdapter As New SqlDataAdapter(getSQL, strconn)
    '    myDataAdapter.SelectCommand.CommandTimeout = 1200

    '    Try
    '        myDataAdapter.Fill(myDataSet, "myaccount")

    '        If myDataSet.Tables(0).Rows.Count = 0 Then
    '            lblMsg.Text = "Rekod tidak dijumpai!"
    '        Else
    '            lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
    '        End If

    '        gvTable.DataSource = myDataSet
    '        gvTable.DataBind()
    '        objConn.Close()
    '    Catch ex As Exception
    '        lblMsg.Text = "Error:" & ex.Message
    '        Return False
    '    End Try

    '    Return True

    'End Function

    'Private Function getSQL() As String
    '    Dim tmpSQL As String
    '    Dim strWhere As String = ""
    '    Dim strOrder As String = " ORDER BY isCurrent desc"

    '    tmpSQL = "SELECT A.id,A.sessionName,A.ukm3Year,A.ppcsdate, CASE WHEN A.isCurrent = 1 THEN 'Sesi terkini' ELSE '' END catatan, B.exam_name "
    '    tmpSQL += "FROM UKM3SESSION A LEFT JOIN Exams B ON A.exam_id = B.id"

    '    getSQL = tmpSQL & strOrder
    '    ''--debug
    '    'Response.Write(getSQL)

    '    Return getSQL

    'End Function


    'Private Sub populateData()

    '    Dim variable As String = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'kolejStartDate'")
    '    txtkolejDateStart.Text = variable

    '    variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'kolejEndDate'")
    '    txtkolejDateEnd.Text = variable

    '    ''from old database
    '    variable = Commonfunction.getSingleCellValue("SELECT configString FROM master_Config WHERE configCode = 'kolejSesi'")
    '    ddlSesiKolej.SelectedValue = variable
    'End Sub

End Class