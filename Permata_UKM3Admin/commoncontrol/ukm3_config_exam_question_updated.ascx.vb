Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Data.OleDb

Public Class ukm3_config_exam_question_updated
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim sqlCommd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Try
            If Not IsPostBack Then
                insruktor_load()
                questionType_load()
                getData()
                ''countdown Perkataan Komen use
                txtsoalan.Attributes.Add("onkeypress", "return tbLimit();")
                txtsoalan.Attributes.Add("onkeyup", "return tbCount(" & countdown_comment.ClientID & ");")
                txtsoalan.Attributes.Add("maxLength", "200")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub getData()
        strSQL = "select top 1 * from Config_question where Ques_id = '" & Request.QueryString("strID") & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)

            If MyTable.Rows.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Question")) Then
                    txtsoalan.Text = ds.Tables(0).Rows(0).Item("Question")
                Else
                    txtsoalan.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Ppcs_type")) Then
                    instruktor_Selec.SelectedValue = ds.Tables(0).Rows(0).Item("Ppcs_type")
                Else
                    instruktor_Selec.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Question_year")) Then
                    txttahun.Text = ds.Tables(0).Rows(0).Item("Question_year")
                Else
                    txttahun.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Question_type")) Then
                    question_Select.SelectedValue = ds.Tables(0).Rows(0).Item("Question_type")
                Else
                    question_Select.SelectedValue = ""
                End If
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

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnupdate_ServerClick(sender As Object, e As EventArgs) Handles btnupdate.ServerClick

        Dim strWhere As String
        strSQL = "UPDATE config_question SET Question='" & txtsoalan.Text & "',Ppcs_type='" & instruktor_Selec.SelectedValue & "',Question_year='" & txttahun.Text & "',Question_type='" & question_Select.SelectedValue & "'"
        strWhere = "WHERE Ques_id='" & Request.QueryString("strID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL & strWhere)
        lblMsg.Text = ""
        If strRet = 0 Then
            lblMsg.Text = "Data sudah dikemaskini!!"
        Else
            lblMsg.Text = "Error!"
        End If

    End Sub

    Private Sub btnback_ServerClick(sender As Object, e As EventArgs) Handles btnback.ServerClick
        Response.Redirect("ukm3_config_exam_question.aspx")

    End Sub

End Class