Imports System.Data.SqlClient

Public Class pcis_config_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        If Not IsPostBack Then
            pcis_config_view()

        End If

    End Sub

    Private Sub pcis_config_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        tmpSQL = "SELECT * FROM pcis_config"
        strWhere += " WHERE pcisconfigid=" & oCommon.FixSingleQuotes(Request.QueryString("pcisconfigid"))
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
                ''--parent info
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("configCode")) Then
                    txtconfigCode.Text = ds.Tables(0).Rows(0).Item("configCode")
                Else
                    txtconfigCode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("configString")) Then
                    txtconfigString.Text = MyTable.Rows(nRows).Item("configString").ToString
                Else
                    txtconfigString.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""
        If pcis_config_update() = True Then
        End If

    End Sub

    Private Function pcis_config_update() As Boolean
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        '--insert into UserProfile
        If ValidatePage() = False Then
            Return False
        End If

        ''--update user
        tmpSQL = "UPDATE pcis_config SET configCode='" & oCommon.FixSingleQuotes(txtconfigCode.Text) & "',configString='" & oCommon.FixSingleQuotes(txtconfigString.Text) & "'"
        strWhere = " WHERE pcisconfigid=" & oCommon.FixSingleQuotes(Request.QueryString("pcisconfigid"))
        strSQL = tmpSQL & strWhere
        strRet = oCommon.ExecuteSQL(strSQL)

        Dim find_year As String = ""
        strSQL = "Select id from pcis_exam_year where description = '" & Now.Year & "'"
        find_year = oCommon.getFieldValue(strSQL)

        If txtconfigCode.Text = "PCISStartDate" Then
            tmpSQL = "UPDATE pcis_exam_year SET start_date='" & oCommon.FixSingleQuotes(txtconfigString.Text) & "'"
            strWhere = " WHERE id =" & find_year
            strSQL = tmpSQL & strWhere
            strRet = oCommon.ExecuteSQL(strSQL)

        ElseIf txtconfigCode.Text = "PCISEndDate" Then
            tmpSQL = "UPDATE pcis_exam_year SET end_date='" & oCommon.FixSingleQuotes(txtconfigString.Text) & "'"
            strWhere = " WHERE id =" & find_year
            strSQL = tmpSQL & strWhere
            strRet = oCommon.ExecuteSQL(strSQL)
        End If

        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA mengemaskini maklumat konfigurasi."
            Return True
        Else
            lblMsg.Text += "GAGAL! " & strRet
            Return False
        End If
    End Function

    Private Function ValidatePage() As Boolean
        If txtconfigCode.Text.Length = 0 Then
            lblMsg.Text = "Masukkan maklumat medan ini. configCode#"
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

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        strSQL = "DELETE FROM pcis_config WHERE pcisconfigid=" & Request.QueryString("pcisconfigid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA menghapuskan reokd tersebut."
        End If

    End Sub
End Class