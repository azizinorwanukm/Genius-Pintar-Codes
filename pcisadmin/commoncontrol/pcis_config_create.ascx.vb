Public Class pcis_config_create
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click

        lblMsg.Text = ""
        If pcis_config_create() = True Then
        End If

    End Sub

    Private Function pcis_config_create() As Boolean
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        '--insert into UserProfile
        If ValidatePage() = False Then
            Return False
        End If

        strSQL = "SELECT configCode FROM pcis_config WHERE configCode='" & oCommon.FixSingleQuotes(txtconfigCode.Text) & "'"
        If oCommon.isExist(strSQL) Then
            lblMsg.Text = "configCode telah ujud!"
            Return False
        End If

        ''--update user
        tmpSQL = "INSERT INTO pcis_config (configCode,configString) VALUES ('" & oCommon.FixSingleQuotes(txtconfigCode.Text) & "','" & oCommon.FixSingleQuotes(txtconfigString.Text) & "')"
        strWhere += ""
        strSQL = tmpSQL & strWhere

        strRet = oCommon.ExecuteSQL(strSQL)

        Dim find_year As String = ""
        strSQL = "Select id from pcis_exam_year where description = '" & Now.Year & "'"
        find_year = oCommon.getFieldValue(strSQL)

        If txtconfigCode.Text = "PCISStartDate" Then
            If strRet.Length > 0 Then
                tmpSQL = "INSERT INTO pcis_exam_year (description,start_date) VALUES ('" & Now.Year & "','" & oCommon.FixSingleQuotes(txtconfigString.Text) & "')"
                strSQL = tmpSQL
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                tmpSQL = "UPDATE pcis_exam_year SET start_date='" & oCommon.FixSingleQuotes(txtconfigString.Text) & "'"
                strWhere = " WHERE id =" & find_year
                strSQL = tmpSQL & strWhere
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

        ElseIf txtconfigCode.Text = "PCISEndDate" Then
            If strRet.Length > 0 Then
                tmpSQL = "INSERT INTO pcis_exam_year (description,end_date) VALUES ('" & Now.Year & "','" & oCommon.FixSingleQuotes(txtconfigString.Text) & "')"
                strSQL = tmpSQL
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                tmpSQL = "UPDATE pcis_exam_year SET end_date='" & oCommon.FixSingleQuotes(txtconfigString.Text) & "'"
                strWhere = " WHERE id =" & find_year
                strSQL = tmpSQL & strWhere
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
        End If

        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA tambah Konfigurasi."
            Return True
        Else
            lblMsg.Text += "GAGAL! " & strRet
            Return False
        End If
    End Function

    Private Function ValidatePage() As Boolean
        If txtconfigCode.Text.Length = 0 Then
            lblMsg.Text = "Masukkan maklumat medan ini. configCode"
            txtconfigCode.Focus()
            Return False
        End If

        If txtconfigString.Text.Length = 0 Then
            lblMsg.Text = "Masukkan maklumat medan ini. configString"
            txtconfigString.Focus()
            Return False
        End If


        Return True

    End Function

    Protected Sub lnkView_Click(sender As Object, e As EventArgs) Handles lnkView.Click
        Response.Redirect("admin.pcis.config.list.aspx")

    End Sub
End Class