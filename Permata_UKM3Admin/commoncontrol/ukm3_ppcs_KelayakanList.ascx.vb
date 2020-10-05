Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class ukm3_ppcs_KelayakanList
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Private birthday As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        getAge()

        Dim i As Integer
        Try
            If Not IsPostBack Then

                lblMsg.Text = ""
                lblMsgTop.Text = ""
                strRet = BindData(datRespondent)
                year_list()
                ''Tukar value of Jantina
                For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                    Dim simpanan As Label = CType(datRespondent.Rows(i).Cells(4).FindControl("Simpanan"), Label)
                    If simpanan.Text = "Y" Then
                        simpanan.Text = "Ada"
                    End If
                Next

            End If
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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY student_info.student_name"

        tmpSQL = "select distinct instruktorExam_result.instruktorExam_id,student_info.std_ID,student_info.student_Name,student_info.student_ID,student_info.student_Mykad,student_info.student_FonNo,config_question.Question_year,student_info.Rapcs_update from student_info 
                  left join instruktor_marklist on student_info.std_ID = instruktor_marklist.std_id
                  left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id
				  left join instruktorExam_result on student_info.std_ID = instruktorExam_result.std_id"
        strWhere += " where student_info.std_ID is not null and student_info.Rapcs_update = 'Y' and instruktorExam_result.instruktorExam_type = '" & getUserProfile_UserType() & "'"

        ''search
        If Not txtsearch.Text.Length = 0 Then
            strWhere += " and (student_info.student_name like'%" & txtsearch.Text & "%'"
            strWhere += " OR student_Mykad='%" & txtsearch.Text & "%'"
            strWhere += " OR student_id='%" & txtsearch.Text & "%')"
        End If

        If Not ddlyear.SelectedValue = "" Then
            strWhere += "and config_question.Question_year = '" & ddlyear.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim totalmarks_id As String = datRespondent.DataKeys(e.NewSelectedIndex).Values("instruktorExam_id").ToString
        Dim student_id As String = datRespondent.DataKeys(e.NewSelectedIndex).Values("std_id").ToString
        Dim encrypt_marksid As String = oDes.EncryptData(totalmarks_id)
        Dim encrypt_stdid As String = oDes.EncryptData(student_id)
        Try
            Select Case getUserProfile_UserType()
                Case "Instruktor PPCS"
                    Response.Redirect("ukm3_ppcs.updatemarkah.aspx?studentid=" & encrypt_stdid & "&totalmarksid=" & encrypt_marksid)
                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function getAge() As String
        Dim tmpSQL As String

        tmpSQL = "Select permatapintar.dbo.StudentProfile.DOB_year from permatapintar"

        strRet = oCommon.getFieldValue(strSQL)
        Response.Write(getSQL)

        Dim age As Integer = (Int32.Parse(DateTime.Today.ToString("yyyy")) - Int32.Parse(strRet.ToString("yyyy"))) / 10000

        Response.Write(age)


        lblMsgTop.Text = age

        Return getAge()
    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        strRet = BindData(datRespondent)

    End Sub

    Private Sub year_list()
        Dim year_now As String = Now.Year - 3
        strSQL = "SELECT description FROM master where type ='year' and description > '" & year_now & "' ORDER BY description"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "description"
            ddlyear.DataValueField = "description"
            ddlyear.DataBind()
            ddlyear.Items.Insert("0", New ListItem("Tahun", ""))


        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

End Class