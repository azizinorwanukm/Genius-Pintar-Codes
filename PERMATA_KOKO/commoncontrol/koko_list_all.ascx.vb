Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_list_all
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--default
                strRet = BindDataSQL(datUniform, "koko_uniform", "Uniform")
                strRet = BindDataSQL(datPersatuan, "koko_persatuan", "Persatuan")
                strRet = BindDataSQL(datSukan, "koko_sukan", "Sukan")

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function BindDataSQL(ByVal gvTable As GridView, ByVal strTablename As String, ByVal strField As String) As Boolean
        Dim myDataSet As New DataSet

        Dim tmpSQL As String = "SELECT *,(SELECT COUNT(*) FROM koko_pelajar WHERE koko_pelajar." & strField & "=" & strTablename & "." & strField & ") as JumlahPelajar FROM " & strTablename & ""
        Dim strWhere As String = " WHERE Tahun='" & Request.QueryString("tahun") & "'"
        Dim strOrderby As String = " ORDER BY " & strField

        Dim strQuery As String = tmpSQL & strWhere & strOrderby

        '--debug
        'Response.Write(strQuery)

        Dim myDataAdapter As New SqlDataAdapter(strQuery, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod ditemui."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
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

End Class