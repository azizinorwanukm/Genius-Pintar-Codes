Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class userprofile_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            state_list()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub state_list()
        strSQL = "SELECT * FROM State ORDER BY statename"
        '--debug
        'Response.Write(strSQL)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddllearningcentrestatename.DataSource = ds
            ddllearningcentrestatename.DataTextField = "statename"
            ddllearningcentrestatename.DataValueField = "stateid"
            ddllearningcentrestatename.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub


    Protected Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""
        If studentprofile_create() = True Then
        End If

    End Sub

    Private Function studentprofile_create() As Boolean
        '--insert into UserProfile
        If ValidatePage() = False Then
            Return False
        End If

        ''--issert user
        strSQL = "INSERT INTO pcis_user (id,icno,fullname,email,address,phoneno,learningcentrename,mothername,motheroccupation,fathername,fatheroccupation,learningcentreaddress,learningcentrephoneno,assistantname,assistantphoneno,learningcentrestateid)"
        strSQL += " VALUES ('" & oCommon.getGUID & "','" & oCommon.FixSingleQuotes(lblicno.Text) & "','" & oCommon.FixSingleQuotes(lblfullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(lblemail.Text) & "','" & oCommon.FixSingleQuotes(lbladdress.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(lblphoneno.Text) & "','" & oCommon.FixSingleQuotes(lbllearningcentrename.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(lblmothername.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(lblmotheroccupation.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(lblfathername.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(lblfatheroccupation.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(lbllearningcentreaddress.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(lbllearningcentrephoneno.Text) & "','" & oCommon.FixSingleQuotes(lblassistantname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(lblassistantphoneno.Text) & "','" & oCommon.FixSingleQuotes(ddllearningcentrestatename.SelectedValue) & "')"

        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA mendaftar pelajar baru."
            Return True
        Else
            lblMsg.Text += "GAGAL! " & strRet
            Return False
        End If
    End Function

    Private Function ValidatePage() As Boolean
        If lblicno.Text.Length = 0 And Regex.IsMatch(lblicno.Text, "^[0-9]+$") And lblicno.Text.Length < 12 Then
            lblMsg.Text = "Masukkan maklumat medan ini. MYKID#"
            lblicno.Focus()
            Return False
        End If

        If lblfullname.Text.Length = 0 Then
            lblMsg.Text = "Masukkan maklumat medan ini. Nama Penuh"
            lblfullname.Focus()
            Return False
        End If

        Return True

    End Function
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/admin.studentprofile.create.aspx", True)
        lblMsg.Text = "Batal pendaftaran pelajar."

    End Sub

End Class