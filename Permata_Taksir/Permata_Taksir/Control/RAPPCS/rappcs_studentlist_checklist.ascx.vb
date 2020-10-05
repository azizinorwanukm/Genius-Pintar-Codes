Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class rappcs_studentlist_checklist
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

        studentDetails()

        Dim rapcs_check_ans As String = Request.QueryString("rapcs_update")
        Dim rapcs_check As String = "Y"
        If rapcs_check = rapcs_check_ans Then
            lblMsg.Text = "Penilaian Pelajar Ini Sudah Dinilai"
            btnSimpan.Text = "Simpan"

        End If

        Try
            If Not IsPostBack Then

                strRet = BindData(datRespondent)
                getData()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub studentDetails()

        Dim ukm3id As String = Request.QueryString("studentid")

        strSQL = "SELECT student_id FROM UKM3 WHERE id = '" & ukm3id & "'"
        Dim student_id As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT student_Name FROM student_info WHERE std_ID = '" & student_id & "'"
        Dim student_Name As String = oCommon.getFieldValue(strSQL)

        lblNama.Text = student_Name

        strSQL = "SELECT student_Mykad FROM student_info WHERE std_ID = '" & student_id & "'"
        Dim student_Mykad As String = oCommon.getFieldValue(strSQL)

        lblMykad.Text = student_Mykad

        strSQL = "SELECT guid FROM student_info WHERE std_ID = '" & student_id & "'"
        Dim guid As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT ClassID FROM permatapintar.dbo.PPCS WHERE StudentID = '" & guid & "' AND PPCSDate = '" & Commonfunction.getPpcsDate & "'"
        Dim ClassID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT ClassCode FROM permatapintar.dbo.PPCS_Class WHERE ClassID = '" & ClassID & "'"
        Dim ClassCode As String = oCommon.getFieldValue(strSQL)

        lblKodKelas.Text = ClassCode

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim rapcs_check_ans As String = Request.QueryString("rapcs_update")
        Dim myTable As DataTable = New DataTable
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 1200
        Dim value As Integer
        Dim i As Integer



        If rapcs_check_ans = "Y" Then
            Try
                myDataAdapter.Fill(myDataSet, "myaccount")

                If myDataSet.Tables(0).Rows.Count = 0 Then
                    lblMsg.Text = "Rekod tidak dijumpai!"
                Else
                    lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
                End If


                gvTable.DataSource = myDataSet
                gvTable.DataBind()
                Console.WriteLine(gvTable)
                ''Debug.WriteLine(gvTable)

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
        Else


            Try
                myDataAdapter.Fill(myDataSet, "myaccount")

                If myDataSet.Tables(0).Rows.Count = 0 Then
                    lblMsg.Text = "Tiada set soalan disediakan untuk tahun yang dipilih"
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
        End If

    End Function
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

    Private Sub getData()
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        Dim get_komenrePcs As String = "select instruktorExam_Komen from instruktorExam_result_raPcs where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_komenrePcs As String = oCommon.getFieldValue(get_komenrePcs)

        txtComment.Text = find_komenrePcs

        Dim get_pencapaianrePcs As String = "select instruktorExam_Pencapaian from instruktorExam_result_raPcs where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_pencapaianrePcs As String = oCommon.getFieldValue(get_pencapaianrePcs)

        txtPencapaian.Text = find_pencapaianrePcs

        Dim get_sokongrePcs As String = "select isSokong from instruktorExam_result_raPcs where ukm3id = '" & decrypt_studentid & "' and instruktorExam_year = '" & Now.Year & "'"
        Dim find_sokongrePcs As String = oCommon.getFieldValue(get_sokongrePcs)

        If find_sokongrePcs = "Y" Then
            rbl_sokong.SelectedValue = "Y"
        Else
            rbl_sokong.SelectedValue = "N"
        End If

    End Sub

    Private Function getSQL() As String

        Dim rapcs_check_ans As String = Request.QueryString("rapcs_update")

        If rapcs_check_ans = "Y" Then
            Dim decrypt_studentid As String = Request.QueryString("studentid")

            Dim tmpSQL As String = ""
            Dim strwhere As String = ""
            Dim SQLdata As String

            tmpSQL = "select DISTINCT instruktor_marklist.insMarkList_id,instruktor_marklist.Ques_id,instruktor_marklist.marks,config_question.question,instruktor_marklist.Ques_id,instruktor_marklist.ukm3id from instruktor_marklist 
                   left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id 
                    where Ppcs_type = 'Instruktor Ra PPCS '"
            strwhere = " and ukm3id = '" & decrypt_studentid & "'"
            strwhere += " and stf_id = '" & getDAtaStaff() & "'"
            SQLdata = tmpSQL + strwhere
            getSQL = SQLdata

            Console.WriteLine(getSQL)
            ''Debug.WriteLine(getSQL)

            Return getSQL
        Else

            Dim tmpSQL As String = ""
            Dim strwhere As String = ""
            Dim strOrder As String = ""
            Dim SQLdata As String

            tmpSQL = "select Ques_id,Question from config_question where Ppcs_type = 'Instruktor Ra PPCS'"
            If Not ddlYear.SelectedValue = "" Then
                strwhere += "and Question_year = '" & ddlYear.SelectedValue & "'"
            End If

            SQLdata = tmpSQL + strwhere + strOrder
            getSQL = SQLdata

            Return getSQL
        End If
    End Function

    Private Sub btnSimpan_click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim decryptid As String = Request.QueryString("studentid")
        Dim SqlTotalMarks As String
        lblMsg.Text = ""
        Dim year As String = Now.Year.ToString()

        Dim i As Integer
        Dim totalvalue As Integer
        Dim value As Integer

        If btnSimpan.Text = "Kembali" Then
            Response.Redirect("rappcs.studentlist.aspx")

        Else
            If checkAllchecked() = True Then
                If validate() = True Then
                    For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                        Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                        Dim rbtn_value1 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
                        Dim rbtn_value2 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
                        Dim rbtn_value3 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
                        Dim rbtn_value4 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
                        Dim rbtn_value5 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio4"), RadioButton)

                        ''18122018 update skor
                        strSQL = "SELECT insMarkList_id FROM instruktor_marklist WHERE Ques_id = '" & strID & "' AND ukm3id = '" & decryptid & "' AND stf_id = '" & getDAtaStaff() & "'"
                        Dim MarkList_ID As String = oCommon.getFieldValue(strSQL)

                        If rbtn_value1.Checked = True Then
                            value = 1

                            If MarkList_ID = "" Then

                                ''insert to table instruktormark_list to be display later by admin
                                Using STDDATA As New SqlCommand("insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                                    objConn.Open()
                                    Dim k = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If k = 0 Then
                                        lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + strRet
                                    End If

                                End Using

                            Else

                                ''18122018 UPDATE SKOR

                                strSQL = " UPDATE instruktor_marklist SET marks = '" & value & "' WHERE insMarkList_id = '" & MarkList_ID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                                If Not strRet = 0 Then

                                    lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk"

                                End If

                            End If

                        ElseIf rbtn_value2.Checked = True Then
                            value = 2

                            If MarkList_ID = "" Then

                                ''insert to table instruktormark_list to be display later by admin
                                Using STDDATA As New SqlCommand("insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                                    objConn.Open()
                                    Dim k = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If k = 0 Then
                                        lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + strRet
                                    End If

                                End Using

                            Else

                                ''18122018 UPDATE SKOR

                                strSQL = " UPDATE instruktor_marklist SET marks = '" & value & "' WHERE insMarkList_id = '" & MarkList_ID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                                If Not strRet = 0 Then

                                    lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk"

                                End If

                            End If

                        ElseIf rbtn_value3.Checked = True Then
                            value = 3

                            If MarkList_ID = "" Then

                                ''insert to table instruktormark_list to be display later by admin
                                Using STDDATA As New SqlCommand("insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                                    objConn.Open()
                                    Dim k = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If k = 0 Then
                                        lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + strRet
                                    End If

                                End Using

                            Else

                                ''18122018 UPDATE SKOR

                                strSQL = " UPDATE instruktor_marklist SET marks = '" & value & "' WHERE insMarkList_id = '" & MarkList_ID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                                If Not strRet = 0 Then

                                    lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk"

                                End If

                            End If

                        ElseIf rbtn_value4.Checked = True Then
                            value = 4

                            If MarkList_ID = "" Then

                                ''insert to table instruktormark_list to be display later by admin
                                Using STDDATA As New SqlCommand("insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                                    objConn.Open()
                                    Dim k = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If k = 0 Then
                                        lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + strRet
                                    End If

                                End Using

                            Else

                                ''18122018 UPDATE SKOR

                                strSQL = " UPDATE instruktor_marklist SET marks = '" & value & "' WHERE insMarkList_id = '" & MarkList_ID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                                If Not strRet = 0 Then

                                    lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk"

                                End If

                            End If

                        ElseIf rbtn_value5.Checked = True Then
                            value = 5

                            If MarkList_ID = "" Then

                                ''insert to table instruktormark_list to be display later by admin
                                Using STDDATA As New SqlCommand("insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                                    objConn.Open()
                                    Dim k = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If k = 0 Then
                                        lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + strRet
                                    End If

                                End Using

                            Else

                                ''18122018 UPDATE SKOR

                                strSQL = " UPDATE instruktor_marklist SET marks = '" & value & "' WHERE insMarkList_id = '" & MarkList_ID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                                If Not strRet = 0 Then

                                    lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk"

                                End If

                            End If

                        End If

                        totalvalue += value
                    Next


                    ''18122018 get rapcs exam id
                    strSQL = " SELECT instruktorExam_id FROM instruktorExam_result_raPcs WHERE ukm3id = '" & decryptid & "' AND stf_id = '" & getDAtaStaff() & "'"
                    Dim examID As String = oCommon.getFieldValue(strSQL)

                    If examID = "" Then

                        SqlTotalMarks = "insert into instruktorExam_result_raPcs(instruktorExam_marks,instruktorExam_komen,instruktorExam_Pencapaian,isSokong,ukm3id,instruktorExam_type,instruktorExam_year,stf_id) 
                                    values ('" & totalvalue & "','" & txtComment.Text & "','" & txtPencapaian.Text & "','" & rbl_sokong.SelectedValue & "','" & decryptid & "','" & getUserProfile_UserType() & "','" & year & "','" & getDAtaStaff() & "')"
                        strRet = oCommon.ExecuteSQL(SqlTotalMarks)
                        ''Debug.WriteLine(strRet)
                        If strRet = 0 Then
                            lblMsg.Text = "Kemasukan Berjaya!"
                            btnSimpan.Text = "Kembali"

                            upadateStudentStatus()

                        ElseIf Not strRet = 0 Then
                            lblMsg.Text = "Update error, sila hubungi system admin =" & strRet
                        End If

                    Else

                        SqlTotalMarks = " UPDATE instruktorExam_result_raPcs SET instruktorExam_marks = '" & totalvalue & "', instruktorExam_komen = '" & txtComment.Text & "', instruktorExam_Pencapaian = '" & txtPencapaian.Text & "', isSokong = '" & rbl_sokong.SelectedValue & "', instruktorExam_type = '" & getUserProfile_UserType() & "', instruktorExam_year = '" & year & "', stf_id = '" & getDAtaStaff() & "' WHERE instruktorExam_id = '" & examID & "'"
                        strRet = oCommon.ExecuteSQL(SqlTotalMarks)

                        If strRet = 0 Then

                            If upadateStudentStatus() = True Then

                                lblMsg.Text = "Berjaya mengemaskini markah pelajar!"
                                btnSimpan.Text = "Simpan"

                            End If

                        ElseIf Not strRet = 0 Then

                            lblMsg.Text = "Update error, sila hubungi system admin =" & strRet

                        End If

                    End If

                ElseIf validate() = False Then

                    lblMsg.Text = "Sila pilih sama ada anda sokong atau tidak kemasukan pelajar ke Permata Pintar"

                End If

            End If

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

        strSQL = "Update UKM3 set rapcs_simpan = 'Y' where id = '" & decryptid & "'"
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
        Dim SqlTotalMarks As String
        lblMsg.Text = ""
        Dim year As String = Now.Year.ToString()

        Dim i As Integer
        Dim totalvalue As Integer
        Dim value As Integer

        If btnSimpan.Text = "Kembali" Then
            Response.Redirect("rappcs.studentlist.aspx")

        Else
            If checkAllchecked() = True Then
                If validate() = True Then
                    For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                        Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                        Dim rbtn_value1 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio0"), RadioButton)
                        Dim rbtn_value2 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio1"), RadioButton)
                        Dim rbtn_value3 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio2"), RadioButton)
                        Dim rbtn_value4 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio3"), RadioButton)
                        Dim rbtn_value5 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio4"), RadioButton)

                        If rbtn_value1.Checked = True Then
                            value = 1
                            ''insert to table instruktormark_list to be display later by admin
                            Using STDDATA As New SqlCommand("insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                                objConn.Open()
                                Dim k = STDDATA.ExecuteNonQuery()
                                objConn.Close()

                                If k = 0 Then
                                    lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + strRet
                                End If

                            End Using


                        ElseIf rbtn_value2.Checked = True Then
                            value = 2
                            ''insert to table instruktormark_list to be display later by admin
                            Using STDDATA As New SqlCommand("insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                                objConn.Open()
                                Dim k = STDDATA.ExecuteNonQuery()
                                objConn.Close()

                                If k = 0 Then
                                    lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + strRet
                                End If

                            End Using

                        ElseIf rbtn_value3.Checked = True Then
                            value = 3
                            ''insert to table instruktormark_list to be display later by admin
                            Using STDDATA As New SqlCommand("insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                                objConn.Open()
                                Dim k = STDDATA.ExecuteNonQuery()
                                objConn.Close()

                                If k = 0 Then
                                    lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + k
                                End If

                            End Using

                        ElseIf rbtn_value4.Checked = True Then
                            value = 4
                            ''insert to table instruktormark_list to be display later by admin
                            Using STDDATA As New SqlCommand("insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                                objConn.Open()
                                Dim k = STDDATA.ExecuteNonQuery()
                                objConn.Close()

                                If k = 0 Then
                                    lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + k
                                End If

                            End Using
                        ElseIf rbtn_value5.Checked = True Then
                            value = 5
                            ''insert to table instruktormark_list to be display later by admin
                            Using STDDATA As New SqlCommand("insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                                objConn.Open()
                                Dim k = STDDATA.ExecuteNonQuery()
                                objConn.Close()

                                If k = 0 Then
                                    lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + k
                                End If

                            End Using
                        End If
                        totalvalue += value
                    Next

                    SqlTotalMarks = "insert into instruktorExam_result_raPcs(instruktorExam_marks,instruktorExam_komen,instruktorExam_Pencapaian,isSokong,ukm3id,instruktorExam_type,instruktorExam_year,stf_id) 
                                    values ('" & totalvalue & "','" & txtComment.Text & "','" & txtPencapaian.Text & "','" & rbl_sokong.SelectedValue & "','" & decryptid & "','" & getUserProfile_UserType() & "','" & year & "','" & getDAtaStaff() & "')"
                    strRet = oCommon.ExecuteSQL(SqlTotalMarks)
                    ''Debug.WriteLine(strRet)
                    If strRet = 0 Then
                        If upadateStudentStatus() = True Then
                            lblMsg.Text = "Kemasukan Berjaya!"
                            btnSimpan.Text = "Kembali"
                            btnSahkan.Visible = False
                        End If

                    ElseIf Not strRet = 0 Then
                        lblMsg.Text = "Update error, sila hubungi system admin =" & strRet
                    End If

                ElseIf validate() = False Then
                    lblMsg.Text = "Sila pilih sama ada anda sokong atau tidak kemasukan pelajar ke Permata Pintar"
                End If

            End If

        End If


    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("rappcs.studentlist.aspx")
    End Sub
End Class