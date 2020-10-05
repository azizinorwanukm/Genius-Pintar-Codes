Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class kpp_studentlist_checklist
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim sqlCommd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim kpp_check_ans As String = Request.QueryString("kpp_update")
        Dim kpp_check As String = "Y"
        If kpp_check = kpp_check_ans Then
            lblMsg.Text = "Penilaian Pelajar Ini Sudah Dinilai"
            btnSimpan.Text = "Kembali"


        End If

        Try
            If Not IsPostBack Then

                strRet = BindData(datRespondent)
                year_list()
                getData()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean

        Dim kpp_check_ans As String = Request.QueryString("kpp_update")
        If kpp_check_ans = "Y" Then
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
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString

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

        Else
            Dim myDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
            myDataAdapter.SelectCommand.CommandTimeout = 120


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

    Private Function getSQL() As String

        Dim kpp_check_ans As String = Request.QueryString("kpp_update")
        Dim decrypt_studentid As String = Request.QueryString("studentid")

        Dim tmpSQL As String = ""
        Dim strwhere As String = ""
        Dim strwhere1 As String = ""
        '' Dim strOrder As String = ""
        Dim SQLdata As String

        If kpp_check_ans = "Y" Then
            tmpSQL = "select instruktor_marklist.insMarkList_id,instruktor_marklist.ukm3id,instruktor_marklist.marks
                    ,config_question.Question,config_question.Ppcs_type,instruktor_marklist.Ques_id from instruktor_marklist
                    left join config_question on instruktor_marklist.Ques_id = config_question.Ques_id 
                    left join instruktorExam_result_kpp on instruktor_marklist.ukm3id = instruktorExam_result_kpp.ukm3id
                    where instruktor_marklist.insMarkList_id is not null and instruktor_marklist.ukm3id ='" & decrypt_studentid & "'
                    and config_question.ppcs_type='Instruktor KPP'"

            If Not ddlYear.SelectedValue = "" Then
                strwhere += "and config_question.Question_year = '" & ddlYear.SelectedValue & "'"
            End If
            SQLdata = tmpSQL + strwhere
            getSQL = SQLdata

            Return getSQL
        Else
            tmpSQL = "select Ques_id,Question from config_question where Ppcs_type = 'Instruktor KPP' "
            If Not ddlYear.SelectedValue = "" Then
                strwhere += "and Question_year = '" & ddlYear.SelectedValue & "'"
            End If
            strwhere1 += " and Question_type= 'pilihan' "
            SQLdata = tmpSQL & strwhere
            getSQL = SQLdata

            Return getSQL
        End If
    End Function

    Private Sub getData()
        Dim decrypt_studentid As String = Request.QueryString("studentid")

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

        '        Dim sokong As String
        '        strSQL = "select top 1 * from instruktorExam_result_kpp where std_id = '" & decrypt_studentid & "'"

        '        Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
        '        Dim objConn As SqlConnection = New SqlConnection(strConn)
        '        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        '        Try
        '            Dim ds As DataSet = New DataSet
        '            sqlDA.Fill(ds, "AnyTable")

        '#Disable Warning S1481 ' Unused local variables should be removed
        '            Dim nRows As Integer = 0
        '#Enable Warning S1481 ' Unused local variables should be removed
        '#Disable Warning S1481 ' Unused local variables should be removed
        '            Dim nCount As Integer = 1
        '#Enable Warning S1481 ' Unused local variables should be removed
        '            Dim MyTable As DataTable = New DataTable
        '            MyTable = ds.Tables(0)

        '            If MyTable.Rows.Count > 0 Then

        '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("instruktorExam_Komen_kpp")) Then
        '                    txt_Comment.Text = "instruktorExam_Komen_kpp"
        '                Else
        '                    txt_Comment.Text = ""
        '                End If

        '                If Not IsDBNull(ds.Tables(0).Rows(0).Item("instruktorExam_Pencapaian_kpp")) Then
        '                    txtPencapaian.Text = "instruktorExam_Pencapaian"
        '                Else
        '                    txtPencapaian.Text = ""
        '                End If

        '                If Not IsDBNull(ds.Tables(0).Rows(3).Item("isSokong_kpp")) Then
        '                    rbl_sokong.SelectedValue = "Y"
        '                Else
        '                    sokong = ""
        '                End If

        '                If sokong = "" Then
        '                    rbl_sokong.SelectedIndex = 1
        '#Disable Warning S1940 ' Boolean checks should not be inverted
        '                ElseIf Not sokong = "" Then
        '#Enable Warning S1940 ' Boolean checks should not be inverted
        '                    rbl_sokong.SelectedIndex = 0
        '                End If

        'End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        Dim decryptid As String = Request.QueryString("studentid")
        lblMsg.Text = ""
        Dim SqlTotalMarks As String
        lblMsg.Text = ""
        Dim year As String = Now.Year.ToString()

        Dim i As Integer
        Dim j As Integer
        Dim l As Integer

        Dim totalvalue As Integer
        Dim value As Integer

        If btnSimpan.Text = "Kembali" Then
            Response.Redirect("ukm3_kpp.markintake.aspx")
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
                        Dim rbtn_value6 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio5"), RadioButton)
                        Dim rbtn_value7 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio6"), RadioButton)
                        Dim rbtn_value8 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio7"), RadioButton)
                        Dim rbtn_value9 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio8"), RadioButton)
                        Dim rbtn_value10 As RadioButton = CType(datRespondent.Rows(i).Cells(2).FindControl("radio9"), RadioButton)
                        Dim rbtn_value11 As RadioButton = CType(datRespondent.Rows(i).Cells(3).FindControl("radioDK"), RadioButton)

                        If rbtn_value1.Checked = True Then
                            value = 10
                        ElseIf rbtn_value2.Checked = True Then
                            value = 9
                        ElseIf rbtn_value3.Checked = True Then
                            value = 8
                        ElseIf rbtn_value4.Checked = True Then
                            value = 7
                        ElseIf rbtn_value5.Checked = True Then
                            value = 6
                        ElseIf rbtn_value6.Checked = True Then
                            value = 5
                        ElseIf rbtn_value7.Checked = True Then
                            value = 4
                        ElseIf rbtn_value8.Checked = True Then
                            value = 3
                        ElseIf rbtn_value9.Checked = True Then
                            value = 2
                        ElseIf rbtn_value10.Checked = True Then
                            value = 1
                        ElseIf rbtn_value11.Checked = True Then
                            value = 0
                        End If

                        Using STDDATA As New SqlCommand("Insert into instruktor_marklist(Ques_id,ukm3id,marks,stf_id) values ('" & strID & "','" & decryptid & "','" & value & "','" & getDAtaStaff() & "')", objConn)
                            objConn.Open()
                            Dim k = STDDATA.ExecuteNonQuery()
                            objConn.Close()
                            If k = 0 Then
                                lblMsg.Text = "Masalah Kemasukan Data, Sila Hubungi HelpDesk" + k
                            End If
                        End Using

                        totalvalue += value
                    Next
                    SqlTotalMarks = "insert into instruktorExam_result_kpp (instruktorExam_marks,instruktorExam_Komen_kpp,instruktorExam_Pencapaian
                                        ,isSokong_kpp,ukm3id,instruktorExam_type_kpp,instruktorExam_year,stf_id) values ('" & totalvalue & "','" & txt_Comment.Text & "',
                                        '" & txtPencapaian.Text & "','" & rbl_sokong.SelectedValue & "','" & decryptid & "','" & getUserProfile_UserType() & "'
                                         ,'" & year & "','" & getDAtaStaff() & "')"
                    strRet = oCommon.ExecuteSQL(SqlTotalMarks)
                    ''Debug.WriteLine(strRet)
                    If strRet = 0 Then

                        If upadateStudentStatus() = True Then
                            lblMsg.Text = "Kemasukan Berjaya!"
                            updateSuccess()
                            btnSimpan.Text = "Kembali"
                        End If

                    ElseIf Not strRet = 0 Then

                        lblMsg.Text = "Update error, Sila hubungi system admin =" & strRet
                    End If

                ElseIf validate() = False Then

                    lblMsg.Text = "Sila pilih sama ada anda sokong atau tidak kemasukan pelajar ke Permata Pintar"

                End If
            End If
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

        strSQL = "Update UKM3 set Kpp_update = 'Y' where ukm3id = '" & decryptid & "'"
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


End Class