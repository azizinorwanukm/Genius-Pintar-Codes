Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class admin_configPemarkahanAdd

    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim sqlCommd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSubmitMark.Attributes.Add("onclick", "return confirm('Format pengiraan markah komposit baru akan dimasukkan ');")

    End Sub

    Function calcheck()
        Dim total = Convert.ToDecimal(txtMarkStem.Text) + Convert.ToDecimal(txtMarkEQ.Text) + Convert.ToDecimal(txtMarkPPCS.Text) + Convert.ToDecimal(txtMarkKPP.Text) + Convert.ToDecimal(txtMarkRAPPCS.Text) + Convert.ToDecimal(txtUKM2.Text) + Convert.ToDecimal(txtPostTest.Text)

        Return total

    End Function

    Function getUKM3session()
        strSQL = "SELECT sessionName FROM UKM3Session WHERE ppcsdate = '" & Commonfunction.getPpcsDate.ToString & "'"
        Dim strRetname As String = oCommon.getFieldValue(strSQL)

        Return strRetname
    End Function

    Private Sub btnSubmitMark_Click(sender As Object, e As EventArgs) Handles btnSubmitMark.Click
        If calcheck() = 1.0 Then

            strSQL = "update  ukm3_compoMark set isActive='0'"
            oCommon.ExecuteSQL(strSQL)
            strSQL = "INSERT INTO ukm3_compoMark (year,stem,eq,insKPP,insPPCS,raPPCS,UKM2,postTest,session,isActive) VALUES ('" & Now.Year & "'," & Convert.ToDecimal(txtMarkStem.Text) & "," & Convert.ToDecimal(txtMarkEQ.Text) & "," & Convert.ToDecimal(txtMarkKPP.Text) & "," & Convert.ToDecimal(txtMarkPPCS.Text) & "," & Convert.ToDecimal(txtMarkRAPPCS.Text) & "," & Convert.ToDecimal(txtUKM2.Text) & "," & Convert.ToDecimal(txtPostTest.Text) & ",'" & getUKM3session.ToString & "','1')"
            Try
                strRet = oCommon.ExecuteSQL(strSQL)
                Debug.WriteLine(strRet)
                If strRet = False Then
                    lblmsgSubmit.Text = "Kemasukan Format Markah Berjaya!!"
                Else
                    lblmsgSubmit.Text = "Kemasukan Format Markah Gagal!! Error = " & strRet
                End If
            Catch ex As Exception

            End Try
        Else
            lblmsgSubmit.Text = "Error!! Markah Komposit tidak menepati 100%"
        End If

    End Sub

End Class