Imports System.Data.SqlClient

Public Class lecturer_AddClass
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
                program_list()
                student_class_list()

                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub year_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "Select distinct lecturer_year from lecturer where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' order by lecturer_year asc"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Year.DataSource = ds
            ddl_Year.DataTextField = "lecturer_year"
            ddl_Year.DataValueField = "lecturer_year"
            ddl_Year.DataBind()
            ddl_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddl_Year.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub program_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        If Session("SchoolCampus") = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type = 'Stream' and Value = 'PS'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type = 'Stream' and Value <> 'Temp'"
        End If

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Program.DataSource = ds
            ddl_Program.DataTextField = "Parameter"
            ddl_Program.DataValueField = "Value"
            ddl_Program.DataBind()
            ddl_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))
            ddl_Program.SelectedIndex = 0
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_sem_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "SELECT * FROM setting WHERE Type = 'Sem'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            ddl_Sem.DataSource = ds
            ddl_Sem.DataTextField = "Parameter"
            ddl_Sem.DataValueField = "Value"
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

        strSQL = "  select distinct subject_info.subject_StudentYear from subject_info
                    left join lecturer on subject_info.subject_ID = lecturer.subject_ID
                    where subject_info.subject_year = '" & ddl_Year.SelectedValue & "' and lecturer.lecturer_year = '" & ddl_Year.SelectedValue & "' and subject_info.subject_Campus = '" & Session("SchoolCampus") & "'
                    and lecturer.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'
                    order by subject_info.subject_StudentYear asc"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            ddl_Level.DataSource = ds
            ddl_Level.DataTextField = "subject_StudentYear"
            ddl_Level.DataValueField = "subject_StudentYear"
            ddl_Level.DataBind()
            ddl_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_Level.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub stduent_course_list()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        strSQL = "  select distinct subject_info.subject_Name, subject_info.subject_ID from subject_info
                    left join lecturer on subject_info.subject_ID = lecturer.subject_ID
                    where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'
                    and lecturer_year = '" & ddl_Year.SelectedValue & "' and course_Program = '" & ddl_Program.SelectedValue & "' and subject_Campus = '" & Session("SchoolCampus") & "'
                    and subject_info.subject_StudentYear = '" & ddl_Level.SelectedValue & "'  and subject_info.subject_sem = '" & ddl_Sem.SelectedValue & "'
                    order by subject_Name asc"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            ddl_Course.DataSource = ds
            ddl_Course.DataTextField = "subject_Name"
            ddl_Course.DataValueField = "subject_ID"
            ddl_Course.DataBind()
            ddl_Course.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddl_Course.SelectedIndex = 0

            ddlRegisterCourse.DataSource = ds
            ddlRegisterCourse.DataTextField = "subject_Name"
            ddlRegisterCourse.DataValueField = "subject_ID"
            ddlRegisterCourse.DataBind()
            ddlRegisterCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddl_Course.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_class_list()

        Dim findcourseYear As String = "Select subject_year from subject_info where subject_ID = '" & ddlRegisterCourse.SelectedValue & "'"
        Dim checkcourseYear As String = oCommon.getFieldValue(findcourseYear)

        Dim findcourseLevel As String = "Select subject_StudentYear from subject_info where subject_ID = '" & ddlRegisterCourse.SelectedValue & "'"
        Dim checkcourseLevel As String = oCommon.getFieldValue(findcourseLevel)

        Dim findcourseSem As String = "Select subject_sem from subject_info where subject_ID = '" & ddlRegisterCourse.SelectedValue & "'"
        Dim checkcourseSem As String = oCommon.getFieldValue(findcourseSem)

        Dim findcourseType As String = "Select subject_type from subject_info where subject_ID = '" & ddlRegisterCourse.SelectedValue & "'"
        Dim checkcourseType As String = oCommon.getFieldValue(findcourseType)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim STDLEVEL As New SqlDataAdapter()

        If checkcourseType = "Compulsory" Then
            strSQL = "select class_Name, class_ID from class_info where class_year = '" & checkcourseYear & "' and class_level = '" & checkcourseLevel & "' and class_type = 'Compulsory' and course_Program = '" & ddl_Program.SelectedValue & "' and class_Campus = '" & Session("SchoolCampus") & "' order by class_Name asc"
        ElseIf checkcourseType <> "Compulsory" Then
            strSQL = "select class_Name, class_ID from class_info where class_year = '" & checkcourseYear & "' and class_level = '" & checkcourseLevel & "' and class_type <> 'Compulsory' and class_sem = '" & checkcourseSem & "' and course_Program = '" & ddl_Program.SelectedValue & "' and class_Campus = '" & Session("SchoolCampus") & "' order by class_Name asc"
        End If

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            ddlRegisterClass.DataSource = ds
            ddlRegisterClass.DataTextField = "class_Name"
            ddlRegisterClass.DataValueField = "class_ID"
            ddlRegisterClass.DataBind()
            ddlRegisterClass.Items.Insert(0, New ListItem("Select Class", String.Empty))
            ddlRegisterClass.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddl_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            student_level_list()
            stduent_course_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Program.SelectedIndexChanged
        Try
            stduent_course_list()

            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            stduent_course_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Sem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Sem.SelectedIndexChanged
        Try
            stduent_course_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Course_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Course.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlRegisterCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegisterCourse.SelectedIndexChanged
        Try
            student_class_list()

            strRet = BindData(datRespondent)
        Catch ex As Exception
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

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_Name ASC"

        tmpSQL = "  select B.ID, A.subject_Name, A.subject_code, A.subject_StudentYear, A.subject_sem, C.class_Name, B.lecturer_year from subject_info A
                    left join lecturer B on A.subject_ID = B.subject_ID
                    left join class_info C on B.class_ID = C.class_ID "

        strWhere = "    where B.lecturer_year = '" & ddl_Year.SelectedValue & "' and A.subject_year = '" & ddl_Year.SelectedValue & "' and B.class_ID is not null and A.subject_Campus = '" & Session("SchoolCampus") & "' and C.class_Campus = '" & Session("SchoolCampus") & "'
                        and C.class_year = '" & ddl_Year.SelectedValue & "' and B.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'
                        and C.course_Program = '" & ddl_Program.SelectedValue & "' and A.course_Program = '" & ddl_Program.SelectedValue & "'"

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " And A.subject_StudentYear = '" & ddl_Level.SelectedValue & "'"
        End If

        If ddl_Sem.SelectedIndex > 0 Then
            strWhere += " And A.subject_sem = '" & ddl_Sem.SelectedValue & "'"
        End If

        If ddl_Course.SelectedIndex > 0 Then
            strWhere += " And A.subject_ID = '" & ddl_Course.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub btnRegisterClass_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterClass.ServerClick

        If ddl_Year.SelectedIndex > 0 And ddlRegisterClass.SelectedIndex > 0 And ddlRegisterCourse.SelectedIndex > 0 Then

            Dim checkCourseEmptyClass As String = "Select ID from lecturer where subject_ID = '" & ddlRegisterCourse.SelectedValue & "' and class_ID is null and stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
            Dim findCourseEmptyClass As String = oCommon.getFieldValue(checkCourseEmptyClass)

            If findCourseEmptyClass.Length > 0 Then
                strSQL = "Delete lecturer where subject_ID = '" & ddlRegisterCourse.SelectedValue & "' and class_ID is null and stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and lecturer_year = '" & ddl_Year.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

            Dim checkClassAvailable As String = "Select ID from lecturer where subject_ID = '" & ddlRegisterCourse.SelectedValue & "' and stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and lecturer_year = '" & ddl_Year.SelectedValue & "' and class_ID = '" & ddlRegisterClass.SelectedValue & "'"
            Dim findClassAvailable As String = oCommon.getFieldValue(checkClassAvailable)

            If findClassAvailable.Length > 0 Then
                ShowMessage(" Class Has Been Registered ", MessageType.Error)
            Else
                strSQL = "INSERT INTO lecturer(stf_ID, class_ID, subject_ID, lecturer_year) VALUES('" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "','" & ddlRegisterClass.SelectedValue & "','" & ddlRegisterCourse.SelectedValue & "','" & ddl_Year.SelectedValue & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then
                    ShowMessage(" Register Class ", MessageType.Success)
                Else
                    ShowMessage(" Unsuccessful Register Class ", MessageType.Error)
                End If

            End If
        Else
            ShowMessage(" Please Select Year, Register Course And Register Class ", MessageType.Error)
        End If

    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            strSQL = "Update lecturer set class_ID = null where ID = '" & strKeyName & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            strRet = BindData(datRespondent)

            ShowMessage(" Delete Data ", MessageType.Success)

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum

End Class