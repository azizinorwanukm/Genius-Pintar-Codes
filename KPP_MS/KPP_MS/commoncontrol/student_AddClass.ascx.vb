Imports System.Data.SqlClient

Public Class student_AddClass

    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                ddlYear()
                ddlLevel()
                ddlclassChoose.Enabled = False

                load_page()
                strRet = BindData(datRespondent)
                ''Generate_Table()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT year from student_Level where year ='" & Now.Year & "'"

        '--debug
        ''Response.Write(strSQLstd)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddl_year.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddl_year.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete student_info where std_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

            If gvTable.Rows.Count = 0 Then
                ddlclassChoose.Visible = False
                Btnsimpan.Visible = False
                nodatamessage.Visible = True
            Else
                nodatamessage.Visible = False
                Btnsimpan.Visible = True
                ddlclassChoose.Visible = True
            End If

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_Name ASC"

        tmpSQL = "Select distinct student_info.student_ID, student_info.std_ID,
                  student_info.student_Mykad,
                  student_info.student_Name,
                  student_Level.student_Level,
                  student_Level.student_Sem,
                  class_info.class_Name
                  From student_info 
                  left join student_Level on student_info.std_ID=student_Level.std_ID
                  left join course on student_info.std_ID= course.std_ID 
                  left join class_info on course.class_ID = class_info.class_ID"
        strWhere = " WHERE student_info.std_ID Is Not NULL"
        strWhere += " And course.year = '" & ddl_year.SelectedValue & "' "
        strWhere += " And student_level.year = '" & ddl_year.SelectedValue & "' AND ( class_info.class_type = 'Compulsory' or course.class_ID is null)"

        If ddl_level.SelectedIndex > 0 Then
            strWhere += " And student_Level.student_Level = '" & ddl_level.SelectedValue & "'"
        End If

        If ddl_sem.SelectedIndex > 0 Then
            strWhere += " And student_Level.student_Sem = '" & ddl_sem.SelectedValue & "'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " And student_info.student_ID Like '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Name LIKE '%" & txtstudent.Text & "%'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            strWhere += " OR student_info.student_Mykad LIKE '%" & txtstudent.Text & "%'"
        End If

        If ddl_class.SelectedIndex > 0 Then
            strWhere += " And class_info.class_ID = '" & ddl_class.SelectedValue & "' AND class_info.class_year = '" & ddl_year.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL

    End Function

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then
                    Dim studentLevel As String = ""
                    Dim studentID As String = ""
                    Dim classLevel As String = ""
                    Dim religion As String = ""
                    Dim id As String = ""

                    studentID = "select std_ID from student_info where std_ID ='" & strKey & "' "
                    Dim datastudentID As String = getFieldValue(studentID, strConn)

                    studentLevel = "select student_Level from student_Level where std_ID ='" & datastudentID & "' And year = '" & ddl_year.SelectedValue & "'"
                    Dim datastudent As String = getFieldValue(studentLevel, strConn)

                    id = "select ID from student_Level where std_ID ='" & datastudentID & "' And year = '" & ddl_year.SelectedValue & "'"
                    Dim dataID As String = getFieldValue(id, strConn)

                    classLevel = "Select class_Level from class_info where class_ID='" & ddlclassChoose.SelectedValue & "' And class_year = '" & ddl_year.SelectedValue & "'"
                    Dim dataclass As String = getFieldValue(classLevel, strConn)

                    religion = "select student_Religion from student_info where std_ID  ='" & datastudentID & "'"
                    Dim dataReligion = getFieldValue(religion, strConn)

                    If datastudent = dataclass Then

                        Try
                            Dim strsubj As String = ""

                            If dataReligion = "ISLAM" Or dataReligion = "islam" Or dataReligion = "Islam" Then

                                If ddl_level.SelectedValue = "Foundation 3" And ddl_sem.SelectedValue = "Sem 2" Then

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ISLAM'"

                                ElseIf ddl_level.SelectedValue = "Level 2" And ddl_sem.SelectedValue = "Sem 2" Then

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ISLAM'"

                                Else

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " And subject_Name <> 'Portfolio'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ISLAM'"

                                End If

                            Else
                                If ddl_level.SelectedValue = "Foundation 3" And ddl_sem.SelectedValue = "Sem 2" Then

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'OTHERS'"

                                ElseIf ddl_level.SelectedValue = "Level 2" And ddl_sem.SelectedValue = "Sem 2" Then

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'OTHERS'"

                                Else

                                    strsubj = "Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'ALL'"
                                    strsubj += " And subject_Name <> 'Portfolio'"
                                    strsubj += " Union"
                                    strsubj += " Select subject_id from subject_info where"
                                    strsubj += " subject_type = 'Compulsory'"
                                    strsubj += " And subject_year = '" & ddl_year.SelectedValue & "'"
                                    strsubj += " And subject_StudentYear = '" & ddl_level.SelectedValue & "'"
                                    strsubj += " And subject_sem = '" & ddl_sem.SelectedValue & "'"
                                    strsubj += " And subject_religions = 'OTHERS'"

                                End If

                            End If

                            Dim strsubjDA As New SqlDataAdapter(strsubj, objConn)
                            Dim subjds As DataSet = New DataSet
                            strsubjDA.Fill(subjds, "SubjTable")

                            Try
                                Dim MyConnection As SqlConnection = New SqlConnection(strConn)
                                Dim Dlt_ClassData As New SqlDataAdapter()

                                Dim dlt_Class As String

                                Dlt_ClassData.SelectCommand = New SqlCommand()
                                Dlt_ClassData.SelectCommand.Connection = MyConnection
                                Dlt_ClassData.SelectCommand.CommandText = "delete course where std_ID ='" & datastudentID & "' And year = '" & ddl_year.SelectedValue & "'"
                                MyConnection.Open()
                                dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
                                MyConnection.Close()

                            Catch ex As Exception

                            End Try

                            For idx As Integer = 0 To subjds.Tables(0).Rows.Count - 1
                                Dim subj As String = subjds.Tables(0).Rows(idx).Item("subject_ID")
                                Dim strAdd As String = "INSERT INTO course(std_ID,class_ID,subject_ID,ID,year) VALUES('" & datastudentID & "','" & ddlclassChoose.SelectedValue & "' , '" & subj & "', '" & dataID & "', '" & ddl_year.SelectedValue & "' )"

                                Using STDDATA As New SqlCommand(strAdd, objConn)
                                    objConn.Open()
                                    Dim run = STDDATA.ExecuteNonQuery()
                                    objConn.Close()
                                End Using
                            Next

                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' insert into kokurikulum database (koko_pelajar)
                            Dim find_Mykad As String = "select student_Mykad from student_info where std_ID = '" & datastudentID & "'"
                            Dim get_Mykad As String = oCommon.getFieldValue(find_Mykad)

                            Dim find_StudentID As String = "select StudentID from StudentProfile where MYKAD = '" & find_Mykad & "'"
                            Dim get_StudentID As String = oCommon.getFieldValue_Permata(find_StudentID)

                            Dim find_kolejKelas As String = "select class_Name from class_info where class_ID = '" & ddlclassChoose.SelectedValue & "'"
                            Dim get_kolejKelas As String = oCommon.getFieldValue(find_kolejKelas)

                            Dim find_kokoKelas As String = "select KelasID from koko_kelas where Kelas = '" & get_kolejKelas & "'"
                            Dim get_kokoKelas As String = oCommon.getFieldValue_Permata(find_kokoKelas)

                            strSQL = "UPDATE koko_kelas SET KelasID = '" & get_kokoKelas & "' where StudentID = '" & get_StudentID & "' and Tahun = '" & ddl_year.SelectedValue & "'"
                            strRet = oCommon.ExecuteSQLPermata(strSQL)

                            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                        Catch ex As Exception

                        End Try


                        errorCount = 0
                    Else
                        errorCount = 1
                    End If
                End If
                '--execute SQL
            End If
        Next

        If errorCount > 0 Then
            Response.Redirect("admin_pelajar_penempatan_kelas.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        Else
            Response.Redirect("admin_pelajar_penempatan_kelas.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        End If
    End Sub

    Public Function getFieldValue(ByVal data As String, ByVal MyConnection As String) As String
        If data.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(data, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_level.SelectedIndexChanged

        ddlClass()

        If Not ddl_level.SelectedValue = "" Then

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim STDLEVEL As New SqlDataAdapter()

            strSQL = "select class_ID,class_Name from class_info where class_Level ='" & ddl_level.SelectedValue & "'"
            strSQL += " And class_year = '" & ddl_year.SelectedValue & "' and class_type = 'Compulsory'"

            Debug.WriteLine(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlclassChoose.DataSource = ds
            ddlclassChoose.DataTextField = "class_Name"
            ddlclassChoose.DataValueField = "class_ID"
            ddlclassChoose.DataBind()
            ddlclassChoose.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlclassChoose.SelectedIndex = 0

            ddlclassChoose.Enabled = True

            ddlSem()

        Else
            ddlclassChoose.Enabled = False
        End If

        BindData(datRespondent)

    End Sub

    Protected Sub ddlClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub ddlSem()
        Try
            strSQL = "select distinct subject_sem from subject_info where subject_StudentYear ='" & ddl_level.SelectedValue & "'"
            ''strSQL += " And subject_Year = '" & ddl_year.SelectedValue & "'"

            Debug.WriteLine(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_sem.DataSource = ds
            ddl_sem.DataTextField = "subject_sem"
            ddl_sem.DataValueField = "subject_sem"
            ddl_sem.DataBind()
            ddl_sem.Items.Insert(0, New ListItem("Select Student Sem", String.Empty))
            ddl_sem.SelectedIndex = 0
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlClass()
        Try
            strSQL = "SELECT class_Name, class_ID from class_info where class_type = 'Compulsory' and class_year = '" & ddl_year.SelectedValue & "' and class_Level = '" & ddl_level.SelectedValue & "' ORDER BY class_Name ASC"
            ''strSQL += " And subject_Year = '" & ddl_year.SelectedValue & "'"

            Debug.WriteLine(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_class.DataSource = ds
            ddl_class.DataTextField = "class_Name"
            ddl_class.DataValueField = "class_ID"
            ddl_class.DataBind()
            ddl_class.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddl_class.SelectedIndex = 0

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear()
        Try
            Dim stryear As String = "Select Parameter from setting where Type = 'Year'"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddl_year.DataSource = yrds
            ddl_year.DataValueField = "Parameter"
            ddl_year.DataTextField = "Parameter"
            ddl_year.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlLevel()

        Try

            Dim strLevelSql As String = "Select Parameter,idx from setting where Type = 'Level' order by idx ASC"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddl_level.DataSource = levds
            ddl_level.DataValueField = "Parameter"
            ddl_level.DataTextField = "Parameter"
            ddl_level.DataBind()
            ddl_level.Items.Insert(0, New ListItem("Select Student Level", String.Empty))
            ddl_level.SelectedIndex = 0

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddl_class_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_class.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub
End Class