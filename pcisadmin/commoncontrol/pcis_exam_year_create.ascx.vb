Public Class pcis_exam_year_create
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""
        If pcis_exam_year_create() = True Then
        End If

    End Sub

    Private Function pcis_exam_year_create() As Boolean
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        '--insert into UserProfile
        If ValidatePage() = False Then
            Return False
        End If

        strSQL = "SELECT description FROM pcis_exam_year WHERE description='" & oCommon.FixSingleQuotes(txtdescription.Text) & "'"
        If oCommon.isExist(strSQL) Then
            lblMsg.Text = "pcis_exam_year telah ujud!"
            Return False
        End If

        ''--update user
        tmpSQL = "INSERT INTO pcis_exam_year (description) VALUES ('" & oCommon.FixSingleQuotes(txtdescription.Text) & "')"
        strWhere += ""
        strSQL = tmpSQL & strWhere

        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA tambah Tahun Ujian."
            Return True
        Else
            lblMsg.Text += "GAGAL! " & strRet
            Return False
        End If
    End Function

    Private Function ValidatePage() As Boolean
        If txtdescription.Text.Length = 0 Then
            lblMsg.Text = "Masukkan maklumat medan ini. pcis_exam_year"
            txtdescription.Focus()
            Return False
        End If


        Return True

    End Function

    Protected Sub lnkView_Click(sender As Object, e As EventArgs) Handles lnkView.Click
        Response.Redirect("admin.pcis.exam.year.list.aspx")

    End Sub
End Class