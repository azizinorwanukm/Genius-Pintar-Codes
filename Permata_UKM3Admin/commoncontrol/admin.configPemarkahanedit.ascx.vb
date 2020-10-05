Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class admin_configPemarkahanedit
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim sqlCommd As SqlCommand

    Dim idn As String = Request.QueryString("id")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSubmitMark.Attributes.Add("onclick", "return confirm('Format pengiraan markah komposit baru akan dimasukkan ');")
        getSQL()
    End Sub

    Private Sub btnSubmitMark_Click(sender As Object, e As EventArgs) Handles btnSubmitMark.Click

        strSQL = "UPDATE ukm3_compoMark SET 
                 stem=" & Convert.ToDecimal(txtMarkStem.Text) & ",eq=" & Convert.ToDecimal(txtMarkEQ.Text) & ",insKPP=" & Convert.ToDecimal(txtMarkKPP.Text) & "
                ,insPPCS=" & Convert.ToDecimal(txtMarkPPCS.Text) & ",raPPCS=" & Convert.ToDecimal(txtMarkRAPPCS.Text) & ",UKM2=" & Convert.ToDecimal(txtUKM2.Text) & "
                ,postTest=" & Convert.ToDecimal(txtPostTest.Text) & " WHERE id='" & idn & "'"
        Try
            strRet = oCommon.ExecuteSQL(strSQL)
            Debug.WriteLine(strRet)
            If strRet = True Then

                lblmsgSubmit.Text = "Kemaskini Format Markah Berjaya!!"
            Else

                lblmsgSubmit.Text = "Kemaskini Format Markah Gagal!! Error = " & strRet
            End If
        Catch ex As Exception

        End Try
    End Sub

    Function getSQL()
        strSQL = "Select stem from ukm3_compoMark where id='" & idn & "'"
        txtMarkStem.Text = oCommon.getFieldValue(strSQL)

        strSQL = "Select eq from ukm3_compoMark where id='" & idn & "'"
        txtMarkEQ.Text = oCommon.getFieldValue(strSQL)

        strSQL = "Select insKPP from ukm3_compoMark where id='" & idn & "'"
        txtMarkKPP.Text = oCommon.getFieldValue(strSQL)

        strSQL = "Select insPPCS from ukm3_compoMark where id='" & idn & "'"
        txtMarkPPCS.Text = oCommon.getFieldValue(strSQL)

        strSQL = "Select raPPCS from ukm3_compoMark where id='" & idn & "'"
        txtMarkRAPPCS.Text = oCommon.getFieldValue(strSQL)

        strSQL = "Select UKM2 from ukm3_compoMark where id='" & idn & "'"
        txtUKM2.Text = oCommon.getFieldValue(strSQL)

        strSQL = "Select postTest from ukm3_compoMark where id='" & idn & "'"
        txtPostTest.Text = oCommon.getFieldValue(strSQL)
    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("admin.formatPemarkahanKeseluruhan.aspx")
    End Sub
End Class