Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class ukm3_config_exam_question_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim sqlCommd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        getUserProfile_UserType()
        getDAtaStaff()

        txttahun.Text = Now.Year

        Try
            If Not IsPostBack Then
                insruktor_load()
                questionType_load()
                ''countdown Perkataan Komen use
                txtsoalan.Attributes.Add("onkeypress", "return tbLimit();")
                txtsoalan.Attributes.Add("onkeyup", "return tbCount(" & countdown_comment.ClientID & ");")
                txtsoalan.Attributes.Add("maxLength", "200")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub insruktor_load()
        strSQL = "SELECT Distinct Ppcs_type FROM config_question "
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            instruktor_Selec.DataSource = ds
            instruktor_Selec.DataTextField = "Ppcs_type"
            instruktor_Selec.DataValueField = "Ppcs_type"
            instruktor_Selec.DataBind()
            instruktor_Selec.Items.Insert(0, New ListItem("Select Instruktor", String.Empty))
            instruktor_Selec.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub questionType_load()
        strSQL = "SELECT Distinct Question_type FROM config_question"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDb As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDb.Fill(ds, "AnyTable")

            question_Select.DataSource = ds
            question_Select.DataTextField = "Question_type"
            question_Select.DataValueField = "Question_type"
            question_Select.DataBind()
            question_Select.Items.Insert(0, New ListItem("Pilih Soalan", String.Empty))
            question_Select.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub clearAll()
        Dim a As String = ""

        txtsoalan.Text = a
        instruktor_Selec.SelectedIndex = 0
        txttahun.Text = Now.Year
        question_Select.SelectedIndex = 0
    End Sub

    Private Sub btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles btnsimpan.ServerClick

        If validate() = True Then
            strSQL = "INSERT into config_question(Question,Ppcs_type,Question_year,Question_type) values ('" & txtsoalan.Text & "','" & instruktor_Selec.SelectedValue & "','" & txttahun.Text & "','" & question_Select.SelectedValue & "' )"
            strRet = oCommon.ExecuteSQL(strSQL)

            lblMsg.Text = "Kemasukan Berjaya!"
            clearAll()
        End If


    End Sub

    Private Function validate() As Boolean
        Dim a As Boolean = False
        Dim b As Boolean = False
        Dim c As Boolean = False
        Dim d As Boolean = False


        ''to validate data
        If txtsoalan.Text <> "" Then
            a = True

            If instruktor_Selec.SelectedIndex > 0 Then
                b = True

                If txttahun.Text <> "" Then
                    c = True

                    If question_Select.SelectedIndex > 0 Then
                        d = True

                        If a = True AndAlso b = True AndAlso c = True AndAlso d = True Then
                            validate = True
                        Else
                            validate = False
                        End If

                    Else
                        lblMsg.Text = "Sila Pilih Jenis Soalan"
                    End If
                Else

                    lblMsg.Text = "Sila Masukkan Tahun Soalan"
                End If
            Else

                lblMsg.Text = "Sila Pilih Jenis Instruktor"
            End If

        Else

            lblMsg.Text = "Sila Masukkan Soalan "
        End If
        Return validate
    End Function

    Private Sub btnreset_ServerClick(sender As Object, e As EventArgs) Handles btnreset.ServerClick
        clearAll()
    End Sub

    Private Sub btnback_ServerClick(sender As Object, e As EventArgs) Handles btnback.ServerClick
        Response.Redirect("ukm3_config_exam_question.aspx")

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getDAtaStaff() As String
        strSQL = "SELECT top 1 stf_id from staff_info where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function



End Class