Imports System.Data.SqlClient

Public Class pcis_exam_year_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        If Not IsPostBack Then
            pcis_exam_year_view()

        End If

    End Sub

    Private Sub pcis_exam_year_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        tmpSQL = "SELECT * FROM pcis_exam_year"
        strWhere += " WHERE id=" & oCommon.FixSingleQuotes(Request.QueryString("id"))
        strSQL = tmpSQL + strWhere

        '--debug
        'Response.Write(strSQL)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("description")) Then
                    txtdescription.Text = ds.Tables(0).Rows(0).Item("description")
                Else
                    txtdescription.Text = ""
                End If

                Dim strstart_date As Date
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("start_date")) Then
                    strstart_date = ds.Tables(0).Rows(0).Item("start_date")
                Else
                    strstart_date = ""
                End If

                Dim strend_date As Date
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("end_date")) Then
                    strend_date = ds.Tables(0).Rows(0).Item("end_date")
                Else
                    strend_date = ""
                End If
                txtend_date.Text = strend_date.ToString("yyyy-MM-dd")

            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""
        If pcis_exam_year_update() = True Then
        End If

    End Sub

    Private Function pcis_exam_year_update() As Boolean
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        '--insert into UserProfile
        If ValidatePage() = False Then
            Return False
        End If

        ''--update user
        tmpSQL = "UPDATE pcis_exam_year SET description='" & oCommon.FixSingleQuotes(txtdescription.Text) & "',end_date='" & oCommon.FixSingleQuotes(txtend_date.Text) & "',start_date = '" & oCommon.FixSingleQuotes(txtstart_date.Text) & "'"
        strWhere += " WHERE id=" & oCommon.FixSingleQuotes(Request.QueryString("id"))
        strSQL = tmpSQL & strWhere

        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA mengemaskini maklumat Tahun Ujian."
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

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        strSQL = "DELETE FROM pcis_exam_year WHERE id=" & Request.QueryString("id")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA menghapuskan reokd tersebut."
        End If

    End Sub

    Protected Sub lnkExamYear_Click(sender As Object, e As EventArgs) Handles lnkExamYear.Click
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        ''--update pcis_setting
        tmpSQL = "UPDATE pcis_setting SET settingvalue='" & Request.QueryString("id") & "'"
        strWhere += " WHERE settingname='exam_year'"
        strSQL = tmpSQL & strWhere

        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA mengemaskini pcis_setting exam_year"
        Else
            lblMsg.Text += "GAGAL! " & strRet
        End If
    End Sub
End Class