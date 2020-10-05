Imports System.Data.SqlClient
Imports System.Text
Public Class ukm3_config_exam_question
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strSQL1 As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim sqlCommd As SqlCommand
    Dim qID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim i As Integer

        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")
        Try
            If Not IsPostBack Then
                gen_listJenis()
                gen_listYear()

                'strRet = BindData(datRespondent)
                For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                    Dim name As Label = CType(datRespondent.Rows(i).Cells(1).FindControl("Ques_id"), Label)
                Next

            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdate.Click

        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkupdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            If Not chkupdate Is Nothing Then

                If chkupdate.Checked = True Then

                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    Response.Redirect("ukm3_config_exam_question_update.aspx?strID=" + strID)
                End If

            End If

        Next

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)

            If chkUpdate IsNot Nothing Then

                If chkUpdate.Checked = True Then

                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString

                    '--DELETE 
                    strSQL = "DELETE FROM config_question WHERE Ques_id ='" & strID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "Delete error:" & strID
                    End If
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            strRet = BindData(datRespondent)
            lblMsg.Text = "Berjaya Buang Soalan."
            lblMsgTop.Text = lblMsg.Text
        End If

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Ques_id"

        tmpSQL = "SELECT Ques_id,Question,Question_type,Question_year,Ppcs_type FROM config_question"

        strWhere += " WHERE Ques_id IS NOT NULL"
        If Not ddlYearselect.SelectedValue = "" Then
            strWhere += " and Question_year = '" & ddlYearselect.SelectedValue & "'"
        End If
        If Not ddlJenissoalan.SelectedValue = "" Then
            strWhere += "and Question_type = '" & ddlJenissoalan.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        Return getSQL
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub gen_listJenis()
        strSQL = "select distinct Question_type from config_question"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDB.Fill(ds, "Anytable")

            ddlJenissoalan.DataSource = ds
            ddlJenissoalan.DataTextField = "Question_type"
            ddlJenissoalan.DataValueField = "Question_type"
            ddlJenissoalan.DataBind()
            ddlJenissoalan.Items.Insert(0, New ListItem("Pilih Jenis Soalan", String.Empty))
            ddlJenissoalan.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub gen_listYear()
        strSQL = "select distinct Question_year from config_question"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "Anytable")

            ddlYearselect.DataSource = ds
            ddlYearselect.DataTextField = "Question_year"
            ddlYearselect.DataValueField = "Question_year"
            ddlYearselect.DataBind()
            ddlYearselect.Items.Insert(0, New ListItem("Pilih Tahun", String.Empty))
            ddlYearselect.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 1200

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Public Sub btn_Search_Click() Handles btn_Search.Click
        strRet = BindData(datRespondent)
    End Sub
End Class
