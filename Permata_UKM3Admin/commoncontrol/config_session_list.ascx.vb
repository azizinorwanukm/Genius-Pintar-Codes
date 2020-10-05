Imports System.Data.SqlClient

Public Class config_session_list
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

    'Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strconn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        If Not IsPostBack Then
            populateData()
            lblMsg.Text = ""
            lblMsgTop.Text = ""
            strRet = BindData(datRespondent)

        End If
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

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim i As Integer

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    'UPDATE
                    strSQL = "DELETE UKM3SESSION WHERE id = '" & strKey & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
            End If
        Next

        Response.Redirect("admin.session_config.aspx")

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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strconn)
        myDataAdapter.SelectCommand.CommandTimeout = 1200

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " ORDER BY isCurrent desc"

        tmpSQL = "SELECT A.id,A.sessionName,A.ukm3Year,A.ppcsdate, CASE WHEN A.isCurrent = 1 THEN 'Sesi terkini' ELSE '' END catatan, B.exam_name "
        tmpSQL += "FROM UKM3SESSION A LEFT JOIN Exams B ON A.exam_id = B.id"

        getSQL = tmpSQL & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub btnActivate(sender As Object, e As EventArgs) Handles btnEnable.Click

        Try
            Dim ukm3Status As String = radioStem.SelectedValue
            Dim sql As String = "UPDATE general_config SET parameter = " & ukm3Status & " WHERE config = 'Stem Test open' "

            sql += "UPDATE general_config SET parameter = " & txtUkm3End.Text & " WHERE config = 'stemEndDate' "

            sql += "UPDATE permatapintar.dbo.master_Config SET configString = " & txtEqEnd.Text & " WHERE configCode = 'EQTest_EndDate' "

            sql += "UPDATE general_config SET parameter = " & radioEq.SelectedValue & " WHERE config = 'eqStatus' "

            sql += "UPDATE general_config SET parameter = " & txtKpp.Text & " WHERE config = 'kppEndDate' "

            sql += "UPDATE general_config SET parameter = " & radioKpp.SelectedValue & " WHERE config = 'kppStatus' "

            sql += "UPDATE general_config SET parameter = " & txtPpcs.Text & " WHERE config = 'ppcsEndDate' "

            sql += "UPDATE general_config SET parameter = " & radioPpcs.SelectedValue & " WHERE config = 'ppcsStatus' "

            sql += "UPDATE general_config SET parameter = " & txtRappcs.Text & " WHERE config = 'rappcaEndDate' "

            sql += "UPDATE general_config SET parameter = " & radioRappcs.SelectedValue & " WHERE config = 'rappcsStatus' "

            sql += "UPDATE general_config SET parameter = " & txtTappcs.Text & " WHERE config = 'tappcaEndDate' "

            sql += "UPDATE general_config SET parameter = " & radioTappcs.SelectedValue & " WHERE config = 'tappcsStatus' "
            oCommon.ExecuteSQL(sql)

        Catch ex As Exception
            lblMsgTop.Text = "Kemaskini gagal."
        End Try

        lblMsgTop.Text = "Kemaskini berjaya."

        ''Response.Redirect("admin.session_config.aspx")

    End Sub

    Private Sub populateData()

        Dim variable As String = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'Stem Test open'")
        radioStem.SelectedValue = variable

        variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'stemEndDate'")
        txtUkm3End.Text = variable

        ''from old database
        variable = Commonfunction.getSingleCellValue("SELECT configString FROM master_Config WHERE configCode = 'EQTest_EndDate'")
        txtEqEnd.Text = variable

        variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'eqStatus'")
        radioEq.SelectedValue = variable

        variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'kppEndDate'")
        txtKpp.Text = variable
        variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'kppStatus'")
        radioKpp.SelectedValue = variable
        variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'ppcsEndDate'")
        txtPpcs.Text = variable
        variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'ppcsStatus'")
        radioPpcs.SelectedValue = variable
        variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'rappcaEndDate'")
        txtRappcs.Text = variable
        variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'rappcsStatus'")
        radioRappcs.SelectedValue = variable
        variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'tappcaEndDate'")
        txtTappcs.Text = variable
        variable = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'tappcsStatus'")
        radioTappcs.SelectedValue = variable

    End Sub

End Class