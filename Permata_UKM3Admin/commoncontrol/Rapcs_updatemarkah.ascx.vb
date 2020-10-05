Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class Rapcs_updatemarkah
    Inherits System.Web.UI.UserControl


    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim sqlCommd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnReset.Attributes.Add("onclick", "return confirm('Pengesahan pelajar ini akan dibatalkan.Ianya akan membenarkan Penilai membuat penilaian sekali lagi tanpa kehilangan rekod markah.');")
        btnResetPenilaian.Attributes.Add("onclick", "return confirm('Pengesahan dan penilaian pelajar ini akan di reset, semua markah,sokong pelajar,jawapan penilai akan di RESET semula!!');")

        Try
            If Not IsPostBack Then
                strRet = BindData(datRespondent)
                getData()

            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myTable As DataTable = New DataTable
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Dim value As Integer
        Dim i As Integer

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If


            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                myTable = myDataSet.Tables(0)
                ''Debug.WriteLine(myTable)
                Console.WriteLine(myTable)

                Dim rdb0 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
                Dim rdb1 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
                Dim rdb2 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
                Dim rdb3 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
                Dim rdb4 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio4"), RadioButton)
                If Not IsDBNull(myTable.Rows(i).Item("marks")) Then
                    value = myTable.Rows(i).Item("marks").ToString

                    If value = 1 Then
                        rdb0.Checked = True
                    ElseIf value = 2 Then
                        rdb1.Checked = True
                    ElseIf value = 3 Then
                        rdb2.Checked = True
                    ElseIf value = 4 Then
                        rdb3.Checked = True
                    ElseIf value = 5 Then
                        rdb4.Checked = True
                    End If
                End If
            Next

            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function
    Private Function getSQL() As String
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        Dim tmpSQL As String = ""
        Dim strwhere As String = ""
        Dim SQLdata As String

        tmpSQL = "select instruktor_marklist.insMarkList_id,instruktor_marklist.stf_id,instruktor_marklist.ukm3id,instruktor_marklist.marks
                    ,config_question.Question,config_question.Ppcs_type,instruktor_marklist.Ques_id from instruktor_marklist
                    left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id 
                    left join instruktorExam_result_kpp on instruktor_marklist.ukm3id = instruktorExam_result_kpp.ukm3id
                    where instruktor_marklist.insMarkList_id is not null and instruktor_marklist.ukm3id ='" & decrypt_studentid & "'
                    and config_question.ppcs_type='Instruktor Ra PPCS'"
        SQLdata = tmpSQL + strwhere
        getSQL = SQLdata

        Return getSQL


    End Function
    Private Sub getData()
        Dim decrypt_studentid As String = Request.QueryString("studentid")


        Dim get_komen As String = "select instruktorExam_Komen from instruktorExam_result_raPcs where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_komen As String = oCommon.getFieldValue(get_komen)

        txtComment.Text = find_komen

        Dim get_pencapaian As String = "select instruktorExam_Pencapaian from instruktorExam_result_raPcs where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_pencapaian As String = oCommon.getFieldValue(get_pencapaian)

        txtPencapaian.Text = find_pencapaian

        Dim get_sokong As String = "select isSokong from instruktorExam_result_raPcs where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_sokong As String = oCommon.getFieldValue(get_sokong)

        If find_sokong = "Y" Then
            rbl_sokong.SelectedValue = "Y"
        Else
            rbl_sokong.SelectedValue = "N"
        End If


    End Sub


    Private Function validate() As Boolean
        ''to validate data
        If rbl_sokong.SelectedValue = "Y" Or rbl_sokong.SelectedValue = "N" Then
            validate = True
        Else
            validate = False
        End If

        Return validate
    End Function
    Private Function upadateStudentStatus() As Boolean
        ''Update student status untuk menunjukkan data dah masuk ke dalam system
        Dim decryptid As String = Request.QueryString("studentid")

        strSQL = "Update student_info set Rapcs_update = 'Y' where ukm3id = '" & decryptid & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            upadateStudentStatus = True

        ElseIf Not strRet = "0" Then
            upadateStudentStatus = False
        End If


    End Function

    Protected Sub updateSuccess()
        ''Refresh balik data
        Dim i As Integer


        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1

            Dim rbtn_value1 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
            Dim rbtn_value2 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
            Dim rbtn_value3 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
            Dim rbtn_value4 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
            Dim rbtn_value5 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio4"), RadioButton)

            rbtn_value1.Checked = False
            rbtn_value2.Checked = False
            rbtn_value3.Checked = False
            rbtn_value4.Checked = False
            rbtn_value5.Checked = False


        Next
        txtComment.Text = ""
        txtPencapaian.Text = ""
        rbl_sokong.SelectedIndex = -1

    End Sub

    Private Function checkAllchecked() As Boolean
        ''validate untuk radio button soalan
        Dim i As Integer
        Dim j As Integer = 1
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1

            Dim rbtn_value1 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
            Dim rbtn_value2 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
            Dim rbtn_value3 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
            Dim rbtn_value4 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
            Dim rbtn_value5 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio4"), RadioButton)


            If rbtn_value1.Checked = False And rbtn_value2.Checked = False And rbtn_value3.Checked = False And rbtn_value4.Checked = False And rbtn_value5.Checked = False Then

                lblMsg.Text = "Sila semak row:" & j
                checkAllchecked = False

                Exit For

            Else
                checkAllchecked = True
                j = j + 1
            End If

        Next

    End Function
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

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        strSQL = "UPDATE ukm3.dbo.UKM3 SET rapcs_pengesahan = '',rapcs_datePengesahan='' where id = '" & Request.QueryString("studentid") & "'"
        Debug.WriteLine(strSQL)
        Try
            strRet = oCommon.ExecuteSQL(strSQL)

            If Not strRet = True Then
                lblMsg.Text = "Error : " & strRet
            Else
                lblMsg.Text = "Reset Pengesahan Pelajar Berjaya!!"
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnResetPenilaian_Click(sender As Object, e As EventArgs) Handles btnResetPenilaian.Click
        strSQL = "UPDATE ukm3.dbo.UKM3 SET rapcs_pengesahan = '',rapcs_datePengesahan='' where id = '" & Request.QueryString("studentid") & "'"
        Dim strSQL1 As String = "UPDATE ukm3.dbo.instruktorExam_result SET isSokong = 'null',instruktorExam_Pencapaian = 'null'
                ,instruktorExam_Komen='null',instruktorExam_marks = 'null' where ukm3id = '" & Request.QueryString("studentid") & "'"
        Debug.WriteLine(strSQL)
        Try
            strRet = oCommon.ExecuteSQL(strSQL)
            Dim strRet1 = oCommon.ExecuteSQL(strSQL1)

            Dim result = strRet And strRet1

            If Not result = True Then
                lblMsg.Text = "Error : " & result
            Else
                lblMsg.Text = "Reset Penilaian dan Pengesahan Berjaya Pelajar Berjaya!!"
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("ukm3_admin_ppcs_markUpdate.aspx")
    End Sub


End Class
