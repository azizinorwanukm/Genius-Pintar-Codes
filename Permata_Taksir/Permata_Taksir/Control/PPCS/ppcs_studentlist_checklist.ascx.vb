Imports System.Data.SqlClient

Public Class ppcs_studentlist_checklist
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim sqlCommd As SqlCommand
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnSahkan.Attributes.Add("onclick", "return confirm('Perhatian!! Setelah Menekan BUTANG SAHKAN. Penilaian tidak boleh diubah.');")

        If isSaved() Then
            btnSimpan.Text = "Kembali"
        End If

        'Dim ppcs_check_ans As String = Request.QueryString("ppcs_update")
        'Dim ppcs_check As String = "Y"
        'If ppcs_check = ppcs_check_ans Then
        '    lblMsg.Text = "Penilaian Pelajar Ini Sudah Dinilai"
        '    btnSimpan.Text = "Kembali"
        'End If

        Try

            If Not IsPostBack Then
                strRet = BindData(datrespondent_kecekapan, datrespondent_komunikasi, datrespondent_kepimpinan)
                getData()
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Function BindData(ByVal kecekapan As GridView, ByVal komunikasi As GridView, ByVal kepimpinan As GridView) As Boolean
        Dim ppcs_check_ans As String = Request.QueryString("ppcs_update")

        If Not ppcs_check_ans = "Y" Then
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
        Else
            Dim dataset_kecekapan As New DataSet
            Dim dataset_kepimpinan As New DataSet
            Dim dataset_komunikasi As New DataSet

            Dim myDataAdapter_kecekapan As New SqlDataAdapter(getSQL_kecekapan, strConn)
            myDataAdapter_kecekapan.SelectCommand.CommandTimeout = 1200

            Dim myDataAdapter_komunikasi As New SqlDataAdapter(getSQL_komunikasi, strConn)
            myDataAdapter_komunikasi.SelectCommand.CommandTimeout = 1200

            Dim myDataAdapter_kepimpinan As New SqlDataAdapter(getSQL_kepimpinan, strConn)
            myDataAdapter_kepimpinan.SelectCommand.CommandTimeout = 1200

            Try
                myDataAdapter_kecekapan.Fill(dataset_kecekapan, "kecekapan")
                myDataAdapter_kepimpinan.Fill(dataset_kepimpinan, "kepimpinan")
                myDataAdapter_komunikasi.Fill(dataset_komunikasi, "komunikasi")

                If dataset_kecekapan.Tables(0).Rows.Count = 0 Then
                    lblMsg.Text = "Tiada set soalan disediakan untuk tahun yang dipilih"
                Else
                    lblMsg.Text = "Jumlah Rekod#:" & dataset_kecekapan.Tables(0).Rows.Count
                End If

                If dataset_kepimpinan.Tables(0).Rows.Count = 0 Then
                    lblMsg.Text = "Tiada set soalan disediakan untuk tahun yang dipilih"
                Else
                    lblMsg.Text = "Jumlah Rekod#:" & dataset_kepimpinan.Tables(0).Rows.Count
                End If

                If dataset_komunikasi.Tables(0).Rows.Count = 0 Then
                    lblMsg.Text = "Tiada set soalan disediakan untuk tahun yang dipilih"
                Else
                    lblMsg.Text = "Jumlah Rekod#:" & dataset_komunikasi.Tables(0).Rows.Count
                End If


                kecekapan.DataSource = dataset_kecekapan
                kecekapan.DataBind()

                komunikasi.DataSource = dataset_komunikasi
                komunikasi.DataBind()

                kepimpinan.DataSource = dataset_kepimpinan
                kepimpinan.DataBind()
                objConn.Close()
            Catch ex As Exception
                lblMsg.Text = "Error:" & ex.Message
                Return False
            End Try

            Return True
        End If
    End Function

    Protected Sub ddlyear_onselectedindexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlYear.SelectedIndexChanged
        ''check soalan ada ke tak dalam database
        strRet = BindData(datrespondent_kecekapan, datrespondent_komunikasi, datrespondent_kepimpinan)


        If datrespondent_kepimpinan.Rows.Count = 0 And datrespondent_kecekapan.Rows.Count = 0 And datrespondent_komunikasi.Rows.Count = 0 Then
            InfoTable.Visible = False
            lblMsg_invi.Text = "Tiada set soalan disediakan untuk tahun yang dipilih"
        Else
            InfoTable.Visible = True
            table_msg.Visible = False

        End If
    End Sub

    Private Function getSQL_kepimpinan() As String
        Dim ppcs_check_ans As String = ""
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        If ppcs_check_ans = "Y" Then

            Dim tmpSQL As String = ""
            Dim strwhere As String = ""
            Dim strOrder As String = ""
            Dim SQLdata As String

            tmpSQL = "select instruktor_marklist.insMarkList_id,config_question.Question,instruktor_marklist.marks,config_question.Ppcs_type,instruktor_marklist.Ques_id from instruktor_marklist
                    left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id where instruktor_marklist.insMarkList_id is not null  and ukm3id = '" & decrypt_studentid & "'
                    and config_question.Ppcs_type= 'Instruktor PPCS' and config_question.Question_year = '" & Now.Year() & "' and config_question.Question_type = 'BORANG PENILAIAN KETRAMPILAN KEPIMPINAN'"

            SQLdata = tmpSQL + strOrder
            getSQL_kepimpinan = SQLdata

            Return getSQL_kepimpinan
        Else

            Dim tmpSQL As String = ""
            Dim strwhere As String = ""
            Dim strOrder As String = ""
            Dim SQLdata As String

            tmpSQL = "SELECT A.Ques_id, A.Question, B.marks FROM config_question A "
            tmpSQL += " LEFT JOIN instruktor_marklist B ON B.Ques_id = A.Ques_id AND B.ukm3id = " & decrypt_studentid
            tmpSQL += " WHERE A.Ppcs_type = '" & getUserProfile_UserType() & "' AND A.Question_type = 'BORANG PENILAIAN KETRAMPILAN KEPIMPINAN' "

            If Not ddlYear.SelectedValue = "" Then
                strwhere += "and Question_year = '" & ddlYear.SelectedValue & "'"
            End If

            SQLdata = tmpSQL + strwhere + strOrder
            getSQL_kepimpinan = SQLdata

            Return getSQL_kepimpinan

        End If
    End Function

    Private Function getSQL_komunikasi() As String
        Dim ppcs_check_ans As String = ""
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        If ppcs_check_ans = "Y" Then
            Dim tmpSQL As String = ""
            Dim strwhere As String = ""
            Dim strOrder As String = ""
            Dim SQLdata As String

            tmpSQL = "select instruktor_marklist.insMarkList_id,instruktor_marklist.marks,config_question.Question,config_question.Ppcs_type,instruktor_marklist.Ques_id from instruktor_marklist
                    left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id where instruktor_marklist.insMarkList_id is not null"
            strwhere = " and ukm3id = '" & decrypt_studentid & "' and config_question.Ppcs_type= 'Instruktor PPCS' and config_question.Question_year = '" & Now.Year() & "' and config_question.Question_type = 'BORANG PENILAIAN BAHAGIAN KETRAMPILAN BERKOMUNIKASI'"
            SQLdata = tmpSQL + strwhere + strOrder
            getSQL_komunikasi = SQLdata

            Return getSQL_komunikasi
        Else
            Dim tmpSQL As String = ""
            Dim strwhere As String = ""
            Dim strOrder As String = ""
            Dim SQLdata As String

            tmpSQL = "SELECT A.Ques_id, A.Question, B.marks FROM config_question A "
            tmpSQL += " LEFT JOIN instruktor_marklist B ON B.Ques_id = A.Ques_id AND B.ukm3id = " & decrypt_studentid
            tmpSQL += " WHERE A.Ppcs_type = '" & getUserProfile_UserType() & "' AND A.Question_type = 'BORANG PENILAIAN BAHAGIAN KETRAMPILAN BERKOMUNIKASI' "

            If Not ddlYear.SelectedValue = "" Then
                strwhere += "and Question_year = '" & ddlYear.SelectedValue & "'"
            End If

            SQLdata = tmpSQL + strwhere + strOrder
            getSQL_komunikasi = SQLdata

            Return getSQL_komunikasi

        End If
    End Function
    Private Function getSQL_kecekapan() As String
        Dim ppcs_check_ans As String = ""
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        If ppcs_check_ans = "Y" Then
            Dim tmpSQL As String = ""
            Dim strwhere As String = ""
            Dim strOrder As String = ""
            Dim SQLdata As String

            tmpSQL = "select instruktor_marklist.insMarkList_id,config_question.Question,instruktor_marklist.marks,config_question.Ppcs_type,instruktor_marklist.Ques_id from instruktor_marklist
                    left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id where instruktor_marklist.insMarkList_id is not null"
            strwhere += " and ukm3id = '" & decrypt_studentid & "' and config_question.Ppcs_type= 'Instruktor PPCS' and config_question.Question_year = '" & Now.Year() & "' and config_question.Question_type= 'BORANG PENILAIAN BAHAGIAN KETRAMPILAN KECEKAPAN BELAJAR'"

            SQLdata = tmpSQL + strwhere + strOrder
            getSQL_kecekapan = SQLdata

            Return getSQL_kecekapan
        Else
            Dim tmpSQL As String = ""
            Dim strwhere As String = ""
            Dim strOrder As String = ""
            Dim SQLdata As String

            tmpSQL = "SELECT A.Ques_id, A.Question, B.marks FROM config_question A "
            tmpSQL += " LEFT JOIN instruktor_marklist B ON B.Ques_id = A.Ques_id AND B.ukm3id = " & decrypt_studentid
            tmpSQL += " WHERE A.Ppcs_type = '" & getUserProfile_UserType() & "' AND A.Question_type = 'BORANG PENILAIAN BAHAGIAN KETRAMPILAN KECEKAPAN BELAJAR' "
            If Not ddlYear.SelectedValue = "" Then
                strwhere += " AND Question_year = '" & ddlYear.SelectedValue & "'"
            End If

            SQLdata = tmpSQL + strwhere + strOrder
            getSQL_kecekapan = SQLdata

            Return getSQL_kecekapan

        End If
    End Function

    Private Sub getData()
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        strSQL = "select top 1 * from instruktorExam_result where ukm3id = '" & decrypt_studentid & "'"

        Dim get_komenpPcs As String = "select instruktorExam_Komen from instruktorExam_result where ukm3id = " & decrypt_studentid
        Dim find_komenpPcs As String = oCommon.getFieldValue(get_komenpPcs)

        txtComment.Text = find_komenpPcs

        Dim get_pencapaianpPcs As String = "select instruktorExam_Pencapaian from instruktorExam_result where ukm3id = " & decrypt_studentid
        Dim find_pencapaianpPcs As String = oCommon.getFieldValue(get_pencapaianpPcs)

        txtPencapaian.Text = find_pencapaianpPcs

        Dim get_sokongpPcs As String = "select isSokong from instruktorExam_result where ukm3id = " & decrypt_studentid
        Dim find_sokongpPcs As String = oCommon.getFieldValue(get_sokongpPcs)

        If find_sokongpPcs = "Y" Then
            rbl_sokong.SelectedValue = "Y"
        ElseIf find_sokongpPcs = "N" Then
            rbl_sokong.SelectedValue = "N"
        End If
    End Sub

    Private Sub btnSimpan_click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        If isSaved() Then
            Response.Redirect("ppcs.studentlist.aspx")
        End If

        Dim complete As Integer = 1

        If rbl_sokong.SelectedValue = "Y" Then

        ElseIf rbl_sokong.SelectedValue = "N" Then

        Else
            complete = 0
        End If

        'If txtComment.Text = "" Then
        '    complete = 1
        'End If

        'If txtPencapaian.Text = "" Then
        '    complete = 1
        'End If

        Dim decryptid As String = Request.QueryString("studentid")

        'Dim sudahSah As String = oCommon.getFieldValue("SELECT dateValid FROM instruktorExam_result WHERE ukm3id = " & decryptid)

        'If Not sudahSah = "" Then
        '    lblMsg.Text = "Kemaskini gagal. Penilaian pelajar ini sudah disahkan sebelum ini."
        '    Exit Sub
        'End If

        Dim SqlTotalMarks As String
        lblMsg.Text = ""
        Dim year As String = Now.Year.ToString()

        Dim i As Integer
        Dim j As Integer
        Dim l As Integer

        Dim totalvalue As Integer

        If btnSimpan.Text = "Kembali" Then
            Response.Redirect("ppcs.studentlist.aspx")

        Else
            Dim query123 As String = ""
            For i = 0 To datrespondent_kecekapan.Rows.Count - 1 Step i + 1
                Dim value As Integer = 0
                Dim strID As String = datrespondent_kecekapan.DataKeys(i).Value.ToString
                Dim rbtn_value1 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
                Dim rbtn_value2 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
                Dim rbtn_value3 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
                Dim rbtn_value4 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
                Dim rbtn_value5 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio4"), RadioButton)

                If rbtn_value1.Checked = True Then
                    value = 1
                ElseIf rbtn_value2.Checked = True Then
                    value = 2
                ElseIf rbtn_value3.Checked = True Then
                    value = 3
                ElseIf rbtn_value4.Checked = True Then
                    value = 4
                ElseIf rbtn_value5.Checked = True Then
                    value = 5
                Else
                    complete = 0
                End If

                ''insert to table instruktormark_list to be display later by admin
                Try
                    TaksirCommon.insertMarklist(strID, decryptid, value, getDAtaStaff())
                Catch ex As Exception
                    lblMsg.Text = "Error!!"
                    Return
                End Try

                totalvalue += value
            Next

            For j = 0 To datrespondent_komunikasi.Rows.Count - 1 Step j + 1
                Dim value As Integer = 0
                Dim strID As String = datrespondent_komunikasi.DataKeys(j).Value.ToString
                Dim rbtn_value1 As RadioButton = CType(datrespondent_komunikasi.Rows(j).Cells(2).FindControl("radio0"), RadioButton)
                Dim rbtn_value2 As RadioButton = CType(datrespondent_komunikasi.Rows(j).Cells(2).FindControl("radio1"), RadioButton)
                Dim rbtn_value3 As RadioButton = CType(datrespondent_komunikasi.Rows(j).Cells(2).FindControl("radio2"), RadioButton)
                Dim rbtn_value4 As RadioButton = CType(datrespondent_komunikasi.Rows(j).Cells(2).FindControl("radio3"), RadioButton)
                Dim rbtn_value5 As RadioButton = CType(datrespondent_komunikasi.Rows(j).Cells(2).FindControl("radio4"), RadioButton)

                If rbtn_value1.Checked = True Then
                    value = 1
                ElseIf rbtn_value2.Checked = True Then
                    value = 2
                ElseIf rbtn_value3.Checked = True Then
                    value = 3
                ElseIf rbtn_value4.Checked = True Then
                    value = 4
                ElseIf rbtn_value5.Checked = True Then
                    value = 5
                Else
                    complete = 0
                End If

                ''insert to table instruktormark_list to be display later by admin
                Try
                    TaksirCommon.insertMarklist(strID, decryptid, value, getDAtaStaff())
                Catch ex As Exception
                    lblMsg.Text = "Error!!"
                    Return
                End Try

                totalvalue += value
            Next

            For l = 0 To datrespondent_kepimpinan.Rows.Count - 1 Step l + 1
                Dim value As Integer = 0
                Dim strID As String = datrespondent_kepimpinan.DataKeys(l).Value.ToString
                Dim rbtn_value1 As RadioButton = CType(datrespondent_kepimpinan.Rows(l).Cells(2).FindControl("radio0"), RadioButton)
                Dim rbtn_value2 As RadioButton = CType(datrespondent_kepimpinan.Rows(l).Cells(2).FindControl("radio1"), RadioButton)
                Dim rbtn_value3 As RadioButton = CType(datrespondent_kepimpinan.Rows(l).Cells(2).FindControl("radio2"), RadioButton)
                Dim rbtn_value4 As RadioButton = CType(datrespondent_kepimpinan.Rows(l).Cells(2).FindControl("radio3"), RadioButton)
                Dim rbtn_value5 As RadioButton = CType(datrespondent_kepimpinan.Rows(l).Cells(2).FindControl("radio4"), RadioButton)

                If rbtn_value1.Checked = True Then
                    value = 1
                ElseIf rbtn_value2.Checked = True Then
                    value = 2
                ElseIf rbtn_value3.Checked = True Then
                    value = 3
                ElseIf rbtn_value4.Checked = True Then
                    value = 4
                ElseIf rbtn_value5.Checked = True Then
                    value = 5
                Else
                    complete = 0
                End If

                ''insert to table instruktormark_list to be display later by admin
                Try
                    TaksirCommon.insertMarklist(strID, decryptid, value, getDAtaStaff())
                Catch ex As Exception
                    lblMsg.Text = "Error!!"
                    Return
                End Try

                totalvalue += value
            Next

            Dim strSQl1 As String = "SELECT ukm3id FROM instruktorExam_result where ukm3id='" & decryptid & "'"

            If Not oCommon.isExist(strSQl1) Then

                SqlTotalMarks = "INSERT INTO instruktorExam_result(instruktorExam_marks,instruktorExam_komen,instruktorExam_Pencapaian,isSokong,ukm3id,instruktorExam_type,instruktorExam_year,stf_id,saved) "
                SqlTotalMarks += "VALUES (@instruktorExam_marks,@instruktorExam_komen,@instruktorExam_Pencapaian,@isSokong,@ukm3id,@instruktorExam_type,@instruktorExam_year,@stf_id,@saved)"

            Else
                SqlTotalMarks = "UPDATE instruktorExam_result SET instruktorExam_marks = @instruktorExam_marks,instruktorExam_komen = @instruktorExam_komen,"
                SqlTotalMarks += "instruktorExam_Pencapaian = @instruktorExam_Pencapaian,isSokong = @isSokong,saved=@saved,"
                SqlTotalMarks += "instruktorExam_type = @instruktorExam_type,instruktorExam_year = @instruktorExam_year,stf_id = @stf_id WHERE ukm3id = @ukm3id"
            End If

            Try
                Dim mAdapter As New SqlDataAdapter
                Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

                Using mConn As New SqlConnection(strconn)
                    Using mCmd As New SqlCommand(SqlTotalMarks, mConn)
                        mCmd.Parameters.Add(New SqlParameter("@instruktorExam_marks", totalvalue))
                        mCmd.Parameters.Add(New SqlParameter("@instruktorExam_komen", txtComment.Text))
                        mCmd.Parameters.Add(New SqlParameter("@instruktorExam_Pencapaian", txtPencapaian.Text))
                        mCmd.Parameters.Add(New SqlParameter("@isSokong", rbl_sokong.SelectedValue))
                        mCmd.Parameters.Add(New SqlParameter("@ukm3id", decryptid))
                        mCmd.Parameters.Add(New SqlParameter("@instruktorExam_type", getUserProfile_UserType()))
                        mCmd.Parameters.Add(New SqlParameter("@instruktorExam_year", year))
                        mCmd.Parameters.Add(New SqlParameter("@stf_id", getDAtaStaff()))
                        mCmd.Parameters.Add(New SqlParameter("@saved", complete))
                        mConn.Open()
                        mCmd.ExecuteNonQuery()
                    End Using
                End Using
            Catch ex As Exception
                lblMsg.Text = "Error!!"
            End Try

            Response.Redirect("ppcs.masukmarkah.aspx?studentid=" & decryptid)

        End If
    End Sub

    Private Function upadateStudentStatus() As Boolean
        ''Update student status untuk menunjukkan data dah masuk ke dalam system
        Dim decryptid As String = Request.QueryString("studentid")

        strSQL = "Update UKM3 set Ppcs_update = 'Y' where id = '" & decryptid & "'"
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
        Dim j As Integer
        Dim k As Integer

        For i = 0 To datrespondent_kecekapan.Rows.Count - 1 Step i + 1

            Dim rbtn_value1 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
            Dim rbtn_value2 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
            Dim rbtn_value3 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
            Dim rbtn_value4 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
            Dim rbtn_value5 As RadioButton = CType(datrespondent_kecekapan.Rows(i).Cells(2).FindControl("radio4"), RadioButton)

            rbtn_value1.Checked = False
            rbtn_value2.Checked = False
            rbtn_value3.Checked = False
            rbtn_value4.Checked = False
            rbtn_value5.Checked = False


        Next

        For j = 0 To datrespondent_komunikasi.Rows.Count - 1 Step j + 1

            Dim rbtn_value1 As RadioButton = CType(datrespondent_komunikasi.Rows(j).Cells(2).FindControl("radio0"), RadioButton)
            Dim rbtn_value2 As RadioButton = CType(datrespondent_komunikasi.Rows(j).Cells(2).FindControl("radio1"), RadioButton)
            Dim rbtn_value3 As RadioButton = CType(datrespondent_komunikasi.Rows(j).Cells(2).FindControl("radio2"), RadioButton)
            Dim rbtn_value4 As RadioButton = CType(datrespondent_komunikasi.Rows(j).Cells(2).FindControl("radio3"), RadioButton)
            Dim rbtn_value5 As RadioButton = CType(datrespondent_komunikasi.Rows(j).Cells(2).FindControl("radio4"), RadioButton)

            rbtn_value1.Checked = False
            rbtn_value2.Checked = False
            rbtn_value3.Checked = False
            rbtn_value4.Checked = False
            rbtn_value5.Checked = False


        Next


        For k = 0 To datrespondent_kepimpinan.Rows.Count - 1 Step k + 1

            Dim rbtn_value1 As RadioButton = CType(datrespondent_kepimpinan.Rows(k).Cells(2).FindControl("radio0"), RadioButton)
            Dim rbtn_value2 As RadioButton = CType(datrespondent_kepimpinan.Rows(k).Cells(2).FindControl("radio1"), RadioButton)
            Dim rbtn_value3 As RadioButton = CType(datrespondent_kepimpinan.Rows(k).Cells(2).FindControl("radio2"), RadioButton)
            Dim rbtn_value4 As RadioButton = CType(datrespondent_kepimpinan.Rows(k).Cells(2).FindControl("radio3"), RadioButton)
            Dim rbtn_value5 As RadioButton = CType(datrespondent_kepimpinan.Rows(k).Cells(2).FindControl("radio4"), RadioButton)

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
    Private Function getDAtaStaff() As String
        strSQL = "SELECT top 1 stf_id from staff_info where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function
    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function
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

    Private Sub btnSahkan_Click(sender As Object, e As EventArgs) Handles btnSahkan.Click
        Dim decryptid As String = Request.QueryString("studentid")
        lblMsg.Text = ""

        Try
            Dim attachmentsTable = New DataTable
            Dim mAdapter As New SqlDataAdapter

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

            Dim queryString As String = "SELECT ukm3id FROM instruktor_marklist WHERE ukm3id= @ukm3id"

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(queryString, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@ukm3id", decryptid))
                    mConn.Open()
                    mAdapter.SelectCommand = mCmd
                    mAdapter.Fill(attachmentsTable)
                End Using
            End Using

            If attachmentsTable.Rows.Count = 0 Then
                lblMsg.Text = "Sila kemaskini dahulu"
                Exit Sub
            End If

            queryString = "SELECT ukm3id FROM instruktor_marklist WHERE ukm3id= @ukm3id AND marks = 0"

            attachmentsTable = New DataTable

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(queryString, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@ukm3id", decryptid))
                    mConn.Open()
                    mAdapter.SelectCommand = mCmd
                    mAdapter.Fill(attachmentsTable)
                End Using
            End Using

            If attachmentsTable.Rows.Count > 0 Then
                lblMsg.Text = "Sila lengkapkan dan kemaskini dahulu"
                Exit Sub
            End If

            queryString = "SELECT ISNULL(isSokong,'') isSokong, ISNULL(instruktorExam_Komen,'') instruktorExam_Komen, ISNULL(instruktorExam_Pencapaian,'') instruktorExam_Pencapaian, dateValid FROM instruktorExam_result WHERE ukm3id = @ukm3id"

            attachmentsTable = New DataTable

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(queryString, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@ukm3id", decryptid))
                    mConn.Open()
                    mAdapter.SelectCommand = mCmd
                    mAdapter.Fill(attachmentsTable)
                End Using
            End Using

            If Not attachmentsTable.Rows(0).Item(3).ToString = "" Then
                lblMsg.Text = "Pengesahan gagal. Penilaian pelajar ini sudah disahkan sebelum ini."
                Exit Sub
            End If

            If attachmentsTable.Rows(0).Item(0).ToString = "" Then
                lblMsg.Text = "Sila lengkapkan ruangan komen dan kemaskini sebelum mengesahkan"
                Exit Sub
            End If

            If attachmentsTable.Rows(0).Item(1).ToString = "" Then
                lblMsg.Text = "Sila lengkapkan ruangan penilaian dan kemaskini sebelum mengesahkan"
                Exit Sub
            End If

            If attachmentsTable.Rows(0).Item(2).ToString = "" Then
                lblMsg.Text = "Sila nyatakan sokong atau tidak sokong dan kemaskini sebelum mengesahkan"
                Exit Sub
            End If

            queryString = "UPDATE instruktorExam_result SET dateValid = GETDATE() WHERE ukm3id = @ukm3id "
            queryString += "UPDATE UKM3 SET Ppcs_update = 'Y' WHERE id = @ukm3id"

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(queryString, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@ukm3id", decryptid))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As Exception
            lblMsg.Text = "Error!"
        End Try

        Response.Redirect("ppcs.studentlist.aspx")

    End Sub

    Private Function isSaved() As Boolean

        Dim ukm3id As String = Request.QueryString("studentid")

        Try
            Dim saved As String = oCommon.getFieldValue("SELECT Ppcs_update FROM UKM3 WHERE id = " & ukm3id)

            If saved = "Y" Then
                Return True
            End If

        Catch ex As Exception
            Return True
        End Try

        Return False

    End Function

End Class