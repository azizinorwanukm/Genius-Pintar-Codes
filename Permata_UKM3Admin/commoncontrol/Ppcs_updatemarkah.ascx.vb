Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class Ppcs_updatemarkah
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
        btnResetPenilaian.Attributes.Add("onclick", "return confirm('Pengesahan dan penilaian pelajar ini akan di reset, markah penilaian,sokong pelajar dan komen akan di RESET semula!!');")

        Try
            If Not IsPostBack Then
                strRet = BindData(datrespondent_kecekapan, datrespondent_komunikasi, datrespondent_kepimpinan)
                getData()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal kecekapan As GridView, ByVal komunikasi As GridView, ByVal kepimpinan As GridView) As Boolean
        Dim myTable_kecekapan As DataTable = New DataTable
        Dim myTable_kepimpinan As DataTable = New DataTable
        Dim mytable_komunikasi As DataTable = New DataTable
        Dim value As Integer

        Dim i As Integer
        Dim j As Integer
        Dim k As Integer

        Dim total As Integer = 0

        Dim dataset_kecekapan As New DataSet
        Dim dataset_kepimpinan As New DataSet
        Dim dataset_komunikasi As New DataSet

        Dim myDataAdapter_kecekapan As New SqlDataAdapter(getSQL_kecekapan, strConn)

        Dim myDataAdapter_komunikasi As New SqlDataAdapter(getSQL_komunikasi, strConn)

        Dim myDataAdapter_kepimpinan As New SqlDataAdapter(getSQL_kepimpinan, strConn)


        Try
            myDataAdapter_kecekapan.Fill(dataset_kecekapan, "kecekapan")
            myDataAdapter_kepimpinan.Fill(dataset_kepimpinan, "kepimpinan")
            myDataAdapter_komunikasi.Fill(dataset_komunikasi, "komunikasi")

            If dataset_kecekapan.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada set soalan disediakan untuk tahun yang dipilih"
            Else
                total = total + dataset_kecekapan.Tables(0).Rows.Count
            End If

            If dataset_kepimpinan.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada set soalan disediakan untuk tahun yang dipilih"
            Else
                total = total + dataset_kepimpinan.Tables(0).Rows.Count
            End If

            If dataset_komunikasi.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada set soalan disediakan untuk tahun yang dipilih"
            Else
                total = total + dataset_komunikasi.Tables(0).Rows.Count
            End If

            lblMsg.Text = "Total Rekod " & total

            kecekapan.DataSource = dataset_kecekapan
            kecekapan.DataBind()

            komunikasi.DataSource = dataset_komunikasi
            komunikasi.DataBind()

            kepimpinan.DataSource = dataset_kepimpinan
            kepimpinan.DataBind()


            For i = 0 To datrespondent_kecekapan.Rows.Count - 1 Step i + 1
                myTable_kecekapan = dataset_kecekapan.Tables(0)

                Dim rdb0 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
                Dim rdb1 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
                Dim rdb2 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
                Dim rdb3 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
                Dim rdb4 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio4"), RadioButton)
                If Not IsDBNull(myTable_kecekapan.Rows(i).Item("marks")) Then
                    value = myTable_kecekapan.Rows(i).Item("marks").ToString

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


            For j = 0 To datrespondent_kepimpinan.Rows.Count - 1 Step j + 1
                myTable_kepimpinan = dataset_kepimpinan.Tables(0)

                Dim rdb0 As RadioButton = CType(datrespondent_kepimpinan.Rows(j).Cells(2).FindControl("radio0"), RadioButton)
                Dim rdb1 As RadioButton = CType(datrespondent_kepimpinan.Rows(j).Cells(2).FindControl("radio1"), RadioButton)
                Dim rdb2 As RadioButton = CType(datrespondent_kepimpinan.Rows(j).Cells(2).FindControl("radio2"), RadioButton)
                Dim rdb3 As RadioButton = CType(datrespondent_kepimpinan.Rows(j).Cells(2).FindControl("radio3"), RadioButton)
                Dim rdb4 As RadioButton = CType(datrespondent_kepimpinan.Rows(j).Cells(2).FindControl("radio4"), RadioButton)
                If Not IsDBNull(myTable_kepimpinan.Rows(j).Item("marks")) Then
                    value = myTable_kepimpinan.Rows(j).Item("marks").ToString

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

            For k = 0 To datrespondent_komunikasi.Rows.Count - 1 Step k + 1
                mytable_komunikasi = dataset_komunikasi.Tables(0)

                Dim rdb0 As RadioButton = CType(datrespondent_komunikasi.Rows(k).Cells(2).FindControl("radio0"), RadioButton)
                Dim rdb1 As RadioButton = CType(datrespondent_komunikasi.Rows(k).Cells(2).FindControl("radio1"), RadioButton)
                Dim rdb2 As RadioButton = CType(datrespondent_komunikasi.Rows(k).Cells(2).FindControl("radio2"), RadioButton)
                Dim rdb3 As RadioButton = CType(datrespondent_komunikasi.Rows(k).Cells(2).FindControl("radio3"), RadioButton)
                Dim rdb4 As RadioButton = CType(datrespondent_komunikasi.Rows(k).Cells(2).FindControl("radio4"), RadioButton)
                If Not IsDBNull(mytable_komunikasi.Rows(k).Item("marks")) Then
                    value = mytable_komunikasi.Rows(k).Item("marks").ToString

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
    Private Function getSQL_kepimpinan() As String
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        Dim tmpSQL As String = ""
        Dim SQLdata As String

        tmpSQL = "select instruktor_marklist.insMarkList_id,config_question.Question,instruktor_marklist.marks,config_question.Ppcs_type,instruktor_marklist.Ques_id from instruktor_marklist
                    left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id where instruktor_marklist.insMarkList_id is not null  and ukm3id = '" & decrypt_studentid & "'
                    and config_question.Ppcs_type= 'Instruktor PPCS' and config_question.Question_type = 'BORANG PENILAIAN KETRAMPILAN KEPIMPINAN'"



        SQLdata = tmpSQL
        getSQL_kepimpinan = SQLdata

        Return getSQL_kepimpinan
    End Function
    Private Function getSQL_komunikasi() As String
        ''Amik data untuk bahagian komunikasi
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        Dim tmpSQL As String = ""
        Dim strwhere As String = ""
        Dim SQLdata As String

        tmpSQL = "select instruktor_marklist.insMarkList_id,instruktor_marklist.marks,config_question.Question,config_question.Ppcs_type,instruktor_marklist.Ques_id from instruktor_marklist
                    left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id where instruktor_marklist.insMarkList_id is not null"
        strwhere = " and ukm3id = '" & decrypt_studentid & "' and config_question.Ppcs_type= 'Instruktor PPCS' and config_question.Question_type = 'BORANG PENILAIAN BAHAGIAN KETRAMPILAN BERKOMUNIKASI'"
        SQLdata = tmpSQL + strwhere
        getSQL_komunikasi = SQLdata

        Return getSQL_komunikasi
    End Function
    Private Function getSQL_kecekapan() As String
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        Dim tmpSQL As String = ""
        Dim strwhere As String = ""
        Dim SQLdata As String

        tmpSQL = "select instruktor_marklist.insMarkList_id,config_question.Question,instruktor_marklist.marks,config_question.Ppcs_type,instruktor_marklist.Ques_id from instruktor_marklist
                    left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id where instruktor_marklist.insMarkList_id is not null"
        strwhere += " and ukm3id = '" & decrypt_studentid & "' and config_question.Ppcs_type= 'Instruktor PPCS' and config_question.Question_type= 'BORANG PENILAIAN BAHAGIAN KETRAMPILAN KECEKAPAN BELAJAR'"

        SQLdata = tmpSQL + strwhere
        getSQL_kecekapan = SQLdata

        Return getSQL_kecekapan
    End Function

    Private Sub getData()
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        strSQL = "select top 1 * from instruktorExam_result where ukm3id = '" & decrypt_studentid & "'"

        Dim get_komenpPcs As String = "select instruktorExam_Komen from instruktorExam_result where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_komenpPcs As String = oCommon.getFieldValue(get_komenpPcs)

        txtComment.Text = find_komenpPcs

        Dim get_pencapaianpPcs As String = "select instruktorExam_Pencapaian from instruktorExam_result where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_pencapaianpPcs As String = oCommon.getFieldValue(get_pencapaianpPcs)

        txtPencapaian.Text = find_pencapaianpPcs

        Dim get_sokongpPcs As String = "select isSokong from instruktorExam_result where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_sokongpPcs As String = oCommon.getFieldValue(get_sokongpPcs)

        If find_sokongpPcs = "Y" Then
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

        strSQL = "Update UKM3 set Kpp_update = 'Y' where ukm3id = '" & decryptid & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            upadateStudentStatus = True

        ElseIf Not strRet = "0" Then
            upadateStudentStatus = False
        End If
    End Function

    Private Function checkAllchecked() As Boolean
        ''validate untuk radio button soalan
        Dim i As Integer
        Dim j As Integer = 1
        Dim k As Integer = 1
        Dim l As Integer = 1
        For i = 0 To datrespondent_kecekapan.Rows.Count - 1 Step i + 1

            Dim rbtn_value1 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
            Dim rbtn_value2 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
            Dim rbtn_value3 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
            Dim rbtn_value4 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
            Dim rbtn_value5 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio4"), RadioButton)


            If rbtn_value1.Checked = False And rbtn_value2.Checked = False And rbtn_value3.Checked = False And rbtn_value4.Checked = False And rbtn_value5.Checked = False Then

                lblMsg.Text = "Sila semak row: " & j & " dalam BORANG PENILAIAN BAHAGIAN KETRAMPILAN KECEKAPAN BELAJAR"
                checkAllchecked = False

                Exit Function

            Else
                checkAllchecked = True
                j = j + 1
            End If

        Next
        j = 1
        For k = 0 To datrespondent_komunikasi.Rows.Count - 1 Step +1

            Dim rbtn_value1 As RadioButton = CType(datrespondent_komunikasi.Rows(k).Cells(2).FindControl("radio0"), RadioButton)
            Dim rbtn_value2 As RadioButton = CType(datrespondent_komunikasi.Rows(k).Cells(2).FindControl("radio1"), RadioButton)
            Dim rbtn_value3 As RadioButton = CType(datrespondent_komunikasi.Rows(k).Cells(2).FindControl("radio2"), RadioButton)
            Dim rbtn_value4 As RadioButton = CType(datrespondent_komunikasi.Rows(k).Cells(2).FindControl("radio3"), RadioButton)
            Dim rbtn_value5 As RadioButton = CType(datrespondent_komunikasi.Rows(k).Cells(2).FindControl("radio4"), RadioButton)


            If rbtn_value1.Checked = False And rbtn_value2.Checked = False And rbtn_value3.Checked = False And rbtn_value4.Checked = False And rbtn_value5.Checked = False Then

                lblMsg.Text = "Sila semak row: " & j & " dalam BORANG PENILAIAN BAHAGIAN KETRAMPILAN BERKOMUNIKASI"
                checkAllchecked = False

                Exit Function

            Else
                checkAllchecked = True
                j = j + 1
            End If

        Next
        j = 1
        For l = 0 To datrespondent_kepimpinan.Rows.Count - 1 Step l + 1

            Dim rbtn_value1 As RadioButton = CType(datrespondent_kepimpinan.Rows(l).Cells(2).FindControl("radio0"), RadioButton)
            Dim rbtn_value2 As RadioButton = CType(datrespondent_kepimpinan.Rows(l).Cells(2).FindControl("radio1"), RadioButton)
            Dim rbtn_value3 As RadioButton = CType(datrespondent_kepimpinan.Rows(l).Cells(2).FindControl("radio2"), RadioButton)
            Dim rbtn_value4 As RadioButton = CType(datrespondent_kepimpinan.Rows(l).Cells(2).FindControl("radio3"), RadioButton)
            Dim rbtn_value5 As RadioButton = CType(datrespondent_kepimpinan.Rows(l).Cells(2).FindControl("radio4"), RadioButton)


            If rbtn_value1.Checked = False And rbtn_value2.Checked = False And rbtn_value3.Checked = False And rbtn_value4.Checked = False And rbtn_value5.Checked = False Then

                lblMsg.Text = "Sila semak row: " & j & " dalam BORANG PENILAIAN KETRAMPILAN KEPIMPINAN"
                checkAllchecked = False

                Exit Function

            Else
                checkAllchecked = True
                j = j + j
            End If

        Next

    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "Select top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function
    Private Function getyear() As String

        strSQL = "select top 1 instruktorExam_year from instruktorExam_result where instruktorExam_id = '" & Request.QueryString("totalmarksid") & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        strSQL = "UPDATE ukm3.dbo.UKM3 SET Ppcs_update = null where id = '" & Request.QueryString("studentid") & "'"
        Dim strSQL1 As String = "update ukm3.dbo.instruktorExam_result set saved = 0 where ukm3id = '" & Request.QueryString("studentid") & "'"
        Debug.WriteLine(strSQL)
        Try
            strRet = oCommon.ExecuteSQL(strSQL)
            Dim strRet1 = oCommon.ExecuteSQL(strSQL1)

            Dim result = strRet And strRet1

            If Not result = True Then
                lblMsg.Text = "Error : " & strRet
            Else
                lblMsg.Text = "Reset Pengesahan Pelajar Berjaya!!"
            End If

        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnResetPenilaian_Click(sender As Object, e As EventArgs) Handles btnResetPenilaian.Click
        strSQL = "UPDATE ukm3.dbo.UKM3 SET Ppcs_update = 'null' where id = '" & Request.QueryString("studentid") & "'"
        Dim strSQL1 As String = "UPDATE ukm3.dbo.instruktorExam_result SET isSokong = 'null',instruktorExam_Pencapaian = 'null'
                ,instruktorExam_Komen='null',instruktorExam_marks = 'null' where ukm3id = '" & Request.QueryString("studentid") & "'"
        Debug.WriteLine(strSQL)
        Try
            strRet = oCommon.ExecuteSQL(strSQL)
            Dim strRet1 = oCommon.ExecuteSQL(strSQL1)

            Dim result = strRet And strRet1

            If Not result = True Then
                lblMsg.Text = "Error : " & strRet
            Else
                lblMsg.Text = "Reset Penilaian dan Pengesahan Berjaya Pelajar Berjaya!!"
            End If

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.ToString
        End Try

    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("ukm3_admin_ppcs_markUpdate.aspx")
    End Sub
End Class