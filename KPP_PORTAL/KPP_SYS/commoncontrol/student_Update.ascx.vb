Imports System.Data.SqlClient

Public Class student_Update
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                student_sem_list()
                student_level_list()
                year_list()
                class_List()

                load_page()
                strRet = BindData(datRespondent)


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
                ddl_Year.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddl_Year.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub year_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "SELECT Parameter FROM setting WHERE Type = 'Year' and Parameter is not null "
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataBind()
            ddlyear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlyear.SelectedIndex = 0

            ddl_Year.DataSource = ds
            ddl_Year.DataTextField = "Parameter"
            ddl_Year.DataValueField = "Parameter"
            ddl_Year.DataBind()
            '' ddl_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ''ddl_Year.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub class_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "SELECT * FROM class_info WHERE class_year = '" & Now.Year & "' and class_type = 'Compulsory' "
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlclass.DataSource = ds
            ddlclass.DataTextField = "class_Name"
            ddlclass.DataValueField = "class_ID"
            ddlclass.DataBind()
            ddlclass.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlclass.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_sem_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "SELECT Parameter FROM setting WHERE Type like '%Sem%' "
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstudentSem.DataSource = ds
            ddlstudentSem.DataTextField = "Parameter"
            ddlstudentSem.DataValueField = "Parameter"
            ddlstudentSem.DataBind()
            ddlstudentSem.Items.Insert(0, New ListItem("Select Sem", String.Empty))
            ddlstudentSem.SelectedIndex = 0

            ddl_Sem.DataSource = ds
            ddl_Sem.DataTextField = "Parameter"
            ddl_Sem.DataValueField = "Parameter"
            ddl_Sem.DataBind()
            ddl_Sem.Items.Insert(0, New ListItem("Select Sem", String.Empty))
            ddl_Sem.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_level_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "SELECT Parameter FROM setting WHERE Type = 'Level' "
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstudentLevel.DataSource = ds
            ddlstudentLevel.DataTextField = "Parameter"
            ddlstudentLevel.DataValueField = "Parameter"
            ddlstudentLevel.DataBind()
            ddlstudentLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddlstudentLevel.SelectedIndex = 0

            ddl_Level.DataSource = ds
            ddl_Level.DataTextField = "Parameter"
            ddl_Level.DataValueField = "Parameter"
            ddl_Level.DataBind()
            ddl_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_Level.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlLevel_Changed(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlYear_Changed(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlSem_Changed(sender As Object, e As EventArgs) Handles ddl_Sem.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
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

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "select * FROM student_info left join student_Level on student_info.std_ID=student_Level.std_ID"
        strWhere += " WHERE student_info.std_ID IS NOT NULL"
        strWhere += " and student_Level.year = '" & ddl_Year.SelectedValue & "'"

        If Not txtstudent_data.Text.Length = 0 Then
            strWhere += " AND student_info.student_ID LIKE '%" & txtstudent_data.Text & "%'"
            strWhere += " AND ID =("
            strWhere += " SELECT max(ID) FROM student_Level left join student_info on student_Level.std_ID=student_info.std_ID"
            strWhere += " where student_info.student_ID  LIKE '%" & txtstudent_data.Text & "%')"
        End If

        If Not txtstudent_data.Text.Length = 0 Then
            strWhere += " OR student_info.student_Name LIKE '%" & txtstudent_data.Text & "%'"
            strWhere += " AND ID =("
            strWhere += " SELECT max(ID) FROM student_Level left join student_info on student_Level.std_ID=student_info.std_ID"
            strWhere += " where student_info.student_Name  LIKE '%" & txtstudent_data.Text & "%')"
        End If

        If Not txtstudent_data.Text.Length = 0 Then
            strWhere += " OR student_info.student_Mykad LIKE '%" & txtstudent_data.Text & "%'"
            strWhere += " AND ID =("
            strWhere += " SELECT max(ID) FROM student_Level left join student_info on student_Level.std_ID=student_info.std_ID"
            strWhere += " where student_info.student_Mykad LIKE '%" & txtstudent_data.Text & "%')"
        End If

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " and student_Level.student_Level = '" & ddl_Level.SelectedValue & "'"
        End If

        If ddl_Sem.SelectedIndex > 0 Then
            strWhere += " and student_Level.student_Sem = '" & ddl_Sem.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

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

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyName As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_edit_kelas_pelajar.aspx?std_ID=" + strKeyName)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Using PJGDATA As New SqlCommand("INSERT student_Level(std_ID,student_Sem,student_Level,year,month,day) values ('" & strKey & "','" & ddlstudentSem.SelectedValue & "','" & ddlstudentLevel.SelectedValue & "','" & ddlyear.SelectedValue & "','" & Now.Month & "','" & Now.Day & "')", objConn)
                        objConn.Open()
                        Dim j = PJGDATA.ExecuteNonQuery()
                        objConn.Close()
                        If j <> 0 Then
                            errorCount = 1
                        Else
                            errorCount = 2
                        End If
                    End Using

                    Dim stdID As String = "select ID from student_level where std_ID = '" & strKey & "' and year = '" & Now.Year & "' and student_Level = '" & ddlstudentLevel.SelectedValue & "' and student_Sem = '" & ddlstudentSem.SelectedValue & "'"
                    Dim dataStdID As Integer = getFieldValue(stdID, strConn)

                    ''select subject
                    Dim strsubj As String = "select subject_id from subject_info where"
                    strsubj += " subject_type = 'Compulsory'"
                    strsubj += " And subject_year = '" & ddl_Year.SelectedValue & "'"
                    strsubj += " And subject_StudentYear = '" & ddlstudentLevel.SelectedValue & "'"
                    strsubj += " And subject_sem = '" & ddlstudentSem.SelectedValue & "'"
                    Dim strsubjDA As New SqlDataAdapter(strsubj, objConn)

                    Dim subjds As DataSet = New DataSet
                    strsubjDA.Fill(subjds, "SubjTable")

                    For idx As Integer = 0 To subjds.Tables(0).Rows.Count - 1
                        Dim subj As String = subjds.Tables(0).Rows(idx).Item("subject_ID")
                        Dim strAdd As String = "INSERT INTO course(std_ID,class_ID,subject_ID,ID,year) VALUES('" & strKey & "','" & ddlclass.SelectedValue & "' , '" & subj & "', '" & dataStdID & "', '" & ddl_Year.SelectedValue & "' )"

                        Using STDDATA As New SqlCommand(strAdd, objConn)
                            objConn.Open()
                            Dim run = STDDATA.ExecuteNonQuery()
                            objConn.Close()
                        End Using
                    Next
                Else
                    errorCount = 2
                End If
            End If
            '--execute SQL
        Next

        If errorCount > 0 Then
            Response.Redirect("adminPengurusanPelajar.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        Else
            Response.Redirect("adminPengurusanPelajar.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
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

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try
    End Sub
End Class