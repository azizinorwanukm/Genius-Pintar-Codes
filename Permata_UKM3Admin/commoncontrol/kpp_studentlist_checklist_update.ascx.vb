Imports System.Data.SqlClient

Public Class kpp_studentlist_checklist_update1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
        Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strRet2 As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim oDes As New Simple3Des("p@ssw0rd1")
        Dim sqlCommd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnReset.Attributes.Add("onclick", "return confirm('Pengesahan pelajar ini akan dibatalkan.Ianya akan membenarkan Penilai membuat penilaian sekali lagi tanpa kehilangan rekod markah.');")
        btnResetPenilaian.Attributes.Add("onclick", "return confirm('Pengesahan dan penilaian pelajar ini akan di reset, markah penilaian,sokong pelajar dan komen akan di RESET semula.!!');")

        Try
            If Not IsPostBack Then
                strRet = BindData(datRespondent)
                getData()
                lbl_text.Text = oCommon.getFieldValue(getSQLtext())
                year_list()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim storedEva As DataTable = New DataTable
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Dim value As Integer
        Dim i As Integer


        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada set soalan disediakan untuk tahun yang dipilih"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If


            gvTable.DataSource = myDataSet
            gvTable.DataBind()

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                storedEva = myDataSet.Tables(0)
                Dim rbtn_value1 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
                Dim rbtn_value2 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
                Dim rbtn_value3 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
                Dim rbtn_value4 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
                Dim rbtn_value5 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio4"), RadioButton)
                Dim rbtn_value6 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio5"), RadioButton)
                Dim rbtn_value7 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio6"), RadioButton)
                Dim rbtn_value8 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio7"), RadioButton)
                Dim rbtn_value9 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio8"), RadioButton)
                Dim rbtn_value10 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio9"), RadioButton)
                Dim rbtn_value11 As RadioButton = CType(datRespondent.Rows(i).Cells(3).FindControl("radioDK"), RadioButton)
                If Not IsDBNull(storedEva.Rows(i).Item("marks")) Then
                    value = storedEva.Rows(i).Item("marks").ToString
                    If value = 10 Then
                        rbtn_value1.Checked = True
                    ElseIf value = 9 Then
                        rbtn_value2.Checked = True
                    ElseIf value = 8 Then
                        rbtn_value3.Checked = True
                    ElseIf value = 7 Then
                        rbtn_value4.Checked = True
                    ElseIf value = 6 Then
                        rbtn_value5.Checked = True
                    ElseIf value = 5 Then
                        rbtn_value6.Checked = True
                    ElseIf value = 4 Then
                        rbtn_value7.Checked = True
                    ElseIf value = 3 Then
                        rbtn_value8.Checked = True
                    ElseIf value = 2 Then
                        rbtn_value9.Checked = True
                    ElseIf value = 1 Then
                        rbtn_value10.Checked = True
                    ElseIf value = 0 Then
                        rbtn_value11.Checked = True
                    End If

                End If

            Next
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Error:  " & ex.Message
            Return False
        End Try

        Return True

    End Function


    Private Function getSQLtext() As String
        strSQL = "select Question from config_question where Ppcs_type = 'Instruktor KPP' and config_question.Question_type='teks'"

        Return strSQL
    End Function



    Private Function getSQL() As String

        Dim decrypt_studentid As String = Request.QueryString("studentid")

        Dim tmpSQL As String = ""
        Dim strwhere As String = ""

        Dim strorder As String = ""
        '' Dim strOrder As String = ""
        Dim SQLdata As String

        tmpSQL = "select instruktor_marklist.insMarkList_id,instruktor_marklist.stf_id,instruktor_marklist.ukm3id,instruktor_marklist.marks
                    ,config_question.Question,config_question.Ppcs_type,instruktor_marklist.Ques_id from instruktor_marklist
                    left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id 
                    left join instruktorExam_result_kpp on instruktor_marklist.ukm3id = instruktorExam_result_kpp.ukm3id
                    where instruktor_marklist.insMarkList_id is not null and instruktor_marklist.ukm3id ='" & decrypt_studentid & "'
                    and config_question.ppcs_type='Instruktor KPP' and Question_type='pilihan'"

        If Not ddlYear.SelectedValue = "" Then
            strwhere += "and config_question.Question_year = '" & ddlYear.SelectedValue & "'"
        End If
        SQLdata = tmpSQL + strwhere
        getSQL = SQLdata

        Return getSQL
    End Function

    Private Sub getData()
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        Dim get_q15KPP As String = "select q15_comment from instruktorExam_result_kpp where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_q15KPP As String = oCommon.getFieldValue(get_q15KPP)

        txtQuestion15.Text = find_q15KPP

        Dim get_komenKPP As String = "select instruktorExam_Komen_kpp from instruktorExam_result_kpp where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_komenKPP As String = oCommon.getFieldValue(get_komenKPP)

        txt_Comment.Text = find_komenKPP

        Dim get_pencapaianKPP As String = "select instruktorExam_Pencapaian from instruktorExam_result_kpp where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_pencapaianKPP As String = oCommon.getFieldValue(get_pencapaianKPP)

        txtPencapaian.Text = find_pencapaianKPP

        Dim get_sokongKPP As String = "select isSokong_KPP from instruktorExam_result_kpp where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_sokongKPP As String = oCommon.getFieldValue(get_sokongKPP)

        If find_sokongKPP = "Y" Then
            rbl_sokong.SelectedValue = "Y"
        Else
            rbl_sokong.SelectedValue = "N"
        End If

    End Sub

    Protected Sub ddlyear_onselectedindexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlYear.SelectedIndexChanged
        ''check soalan ada ke tak dalam database
        strRet = BindData(datRespondent)

        If datRespondent.Rows.Count = 0 Then
            InfoTable.Visible = False
            lblMsg_invi.Text = "Tiada set soalan disediakan untuk tahun yang dipilih"
        Else
            InfoTable.Visible = True
            table_msg.Visible = False

        End If
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

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "description"
            ddlYear.DataValueField = "description"
            ddlYear.DataBind()
            ddlYear.Items.Insert("0", New ListItem("Tahun", ""))
            ddlYear.SelectedValue = Now.Year

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
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

    Private Function upadateStudentStatus() As Boolean
        ''Update student status untuk menunjukkan data dah masuk ke dalam system
        Dim decryptid As String = Request.QueryString("studentid")

        strSQL = "Update ukm3 set Kpp_update = 'Y' where ukm3id = '" & decryptid & "'"
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


        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1 <= 14

            Dim rbtn_value1 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
            Dim rbtn_value2 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
            Dim rbtn_value3 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
            Dim rbtn_value4 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
            Dim rbtn_value5 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio4"), RadioButton)
            Dim rbtn_value6 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio5"), RadioButton)
            Dim rbtn_value7 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio6"), RadioButton)
            Dim rbtn_value8 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio7"), RadioButton)
            Dim rbtn_value9 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio8"), RadioButton)
            Dim rbtn_value10 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio9"), RadioButton)
            Dim rbtn_value11 As RadioButton = CType(datRespondent.Rows(i).Cells(3).FindControl("radioDK"), RadioButton)


            rbtn_value1.Checked = False
            rbtn_value2.Checked = False
            rbtn_value3.Checked = False
            rbtn_value4.Checked = False
            rbtn_value5.Checked = False
            rbtn_value6.Checked = False
            rbtn_value7.Checked = False
            rbtn_value8.Checked = False
            rbtn_value9.Checked = False
            rbtn_value10.Checked = False
            rbtn_value11.Checked = False

        Next
        txt_Comment.Text = ""
        txtPencapaian.Text = ""
        rbl_sokong.SelectedIndex = -1

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
            Dim rbtn_value6 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio5"), RadioButton)
            Dim rbtn_value7 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio6"), RadioButton)
            Dim rbtn_value8 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio7"), RadioButton)
            Dim rbtn_value9 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio8"), RadioButton)
            Dim rbtn_value10 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio9"), RadioButton)
            Dim rbtn_value11 As RadioButton = CType(datRespondent.Rows(i).Cells(3).FindControl("radioDK"), RadioButton)


            If rbtn_value1.Checked = False And rbtn_value2.Checked = False And rbtn_value3.Checked = False And rbtn_value4.Checked = False And rbtn_value5.Checked = False And
                    rbtn_value6.Checked = False And rbtn_value7.Checked = False And rbtn_value8.Checked = False And rbtn_value9.Checked = False And rbtn_value10.Checked = False And rbtn_value11.Checked = False Then

                lblMsg.Text = "Sila semak row:" & j & ""
                checkAllchecked = False

#Disable Warning S3385 ' "Exit" statements should not be used
                Exit For
#Enable Warning S3385 ' "Exit" statements should not be used

            Else
                checkAllchecked = True

            End If

        Next
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        strSQL = "UPDATE ukm3.dbo.UKM3 SET kpp_pengesahan = null, kpp_datePengesahan = null where id = '" & Request.QueryString("studentid") & "'"
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
        strSQL = "UPDATE ukm3.dbo.UKM3 SET kpp_pengesahan = null, kpp_datePengesahan = null where id = '" & Request.QueryString("studentid") & "'"
        Dim strSQL1 As String = "UPDATE ukm3.dbo.instruktorExam_result_kpp SET isSokong_kpp = null,instruktorExam_Pencapaian = null
                ,instruktorExam_Komen_kpp = null,instruktorExam_marks = null where ukm3id = '" & Request.QueryString("studentid") & "'"
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