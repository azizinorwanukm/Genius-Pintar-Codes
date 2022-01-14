Imports System.Data.SqlClient

Public Class student_update_coursePlacement
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

                Dim getStatus As String = Request.QueryString("status")

                If getStatus = "VCC" Then ''View Class & Course

                    ViewClassCourse.Visible = True
                    RegisterClassCourse.Visible = False

                    btnViewClassCourse.Attributes("class") = "btn btn-info"
                    btnRegisterClassCourse.Attributes("class") = "btn btn-default font"

                    ViewYear_List()
                    ViewYear_Load()

                    ViewLevel_List()
                    ViewSemester_List()

                    strRet = BindData(datRespondent)

                ElseIf getStatus = "RCC" Then ''Register Class & Course

                    ViewClassCourse.Visible = False
                    RegisterClassCourse.Visible = True

                    btnViewClassCourse.Attributes("class") = "btn btn-default font"
                    btnRegisterClassCourse.Attributes("class") = "btn btn-info"

                    RegisterYear_List()
                    RegisterLevel_List()
                    RegisterSemester_List()
                    RegisterCourseType_List()
                    RegisterCourse_List()
                    RegisterClass_List()
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnViewClassCourse_ServerClick(sender As Object, e As EventArgs) Handles btnViewClassCourse.ServerClick
        Response.Redirect("pelajar_pilih_kursus.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=VCC")
    End Sub

    Private Sub btnRegisterClassCourse_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterClassCourse.ServerClick
        Response.Redirect("pelajar_pilih_kursus.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=RCC")
    End Sub

    Private Sub ViewYear_List()
        strSQL = "Select distinct A.year from student_Level A left join student_info B on A.std_ID = B.std_ID where B.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' order by A.year asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlViewYear.DataSource = ds
            ddlViewYear.DataTextField = "year"
            ddlViewYear.DataValueField = "year"
            ddlViewYear.DataBind()
            ddlViewYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ViewLevel_List()
        strSQL = "Select distinct A.student_Level from student_Level A left join student_info B on A.std_ID = B.std_ID where B.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and A.year = '" & ddlViewYear.SelectedValue & "' order by A.student_Level asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlViewLevel.DataSource = ds
            ddlViewLevel.DataTextField = "student_Level"
            ddlViewLevel.DataValueField = "student_Level"
            ddlViewLevel.DataBind()
            ddlViewLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ViewSemester_List()
        strSQL = "Select * from setting where type = 'Sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlViewSemester.DataSource = ds
            ddlViewSemester.DataTextField = "Parameter"
            ddlViewSemester.DataValueField = "Value"
            ddlViewSemester.DataBind()
            ddlViewSemester.Items.Insert(0, New ListItem("Select Semester", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ViewYear_Load()
        strSQL = "Select MAX(Parameter) from setting where Type = 'year'"
        strRet = oCommon.getFieldValue(strSQL)

        ddlViewYear.SelectedValue = strRet
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

        Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("std_ID"))

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY B.subject_Name ASC"

        tmpSQL = "  Select A.course_ID, B.subject_Name, B.subject_code, B.subject_StudentYear, B.subject_CreditHour, C.class_Name, A.year from course A
                    left join subject_info B on A.subject_ID = B.subject_ID
                    left join class_info C on A.class_ID = C.class_ID
                    where A.year = '" & ddlViewYear.SelectedValue & "' and B.subject_year = '" & ddlViewYear.SelectedValue & "' and C.class_year = '" & ddlViewYear.SelectedValue & "'
                    and B.subject_StudentYear = '" & ddlViewLevel.SelectedValue & "' and C.class_Level = '" & ddlViewLevel.SelectedValue & "'
                    and B.subject_sem = '" & ddlViewSemester.SelectedValue & "'
                    and A.std_ID = '" & data_ID & "'"

        getSQL = tmpSQL & strOrderby

        Return getSQL
    End Function

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            strSQL = "Select year from course where course_ID = '" & strKeyName & "'"
            strRet = oCommon.getFieldValue(strSQL)

            If strRet <> Now.Year Then
                ShowMessage(" Unable To Delete Previuos Year Data", MessageType.Error)
            Else

                Dim MyConnection As SqlConnection = New SqlConnection(strConn)
                Dim Dlt_ClassData As New SqlDataAdapter()

                Dim dlt_Class As String

                Dlt_ClassData.SelectCommand = New SqlCommand()
                Dlt_ClassData.SelectCommand.Connection = MyConnection
                Dlt_ClassData.SelectCommand.CommandText = "delete course where course_ID ='" & strKeyName & "'"
                MyConnection.Open()
                dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
                MyConnection.Close()

                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlViewYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlViewYear.SelectedIndexChanged
        Try
            ViewLevel_List()
            ViewSemester_List()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlViewLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlViewLevel.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlViewSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlViewSemester.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RegisterYear_List()
        strSQL = "Select distinct A.year from student_Level A left join student_info B on A.std_ID = B.std_ID where B.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' order by A.year asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "year"
            ddlYear.DataValueField = "year"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub RegisterLevel_List()
        strSQL = "Select distinct A.student_Level from student_Level A left join student_info B on A.std_ID = B.std_ID where B.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and A.year = '" & ddlYear.SelectedValue & "' order by A.student_Level asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel.DataSource = ds
            ddlLevel.DataTextField = "student_Level"
            ddlLevel.DataValueField = "student_Level"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub RegisterSemester_List()
        strSQL = "Select * from setting where type = 'Sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester.DataSource = ds
            ddlSemester.DataTextField = "Parameter"
            ddlSemester.DataValueField = "Value"
            ddlSemester.DataBind()
            ddlSemester.Items.Insert(0, New ListItem("Select Semester", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub RegisterCourseType_List()

        strSQL = "Select Parameter, Value from setting where Type = 'Subject Type' and Value <> 'Compulsory'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCourseType.DataSource = ds
            ddlCourseType.DataTextField = "Parameter"
            ddlCourseType.DataValueField = "Value"
            ddlCourseType.DataBind()
            ddlCourseType.Items.Insert(0, New ListItem("Select Course type", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub


    Private Sub RegisterCourse_List()

        Dim find_CourseRegisterd As String = "  Select distinct A.subject_ID from subject_info A left join course B on A.subject_ID = B.subject_ID 
                                                where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & ddlYear.SelectedValue & "' and subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_sem = '" & ddlSemester.SelectedValue & "' and A.subject_type = '" & ddlCourseType.SelectedValue & "'"
        Dim get_CourseRegistered As String = oCommon.getFieldValue(find_CourseRegisterd)

        If get_CourseRegistered.Length = 0 Then

            Dim find_CourseProgram As String = "Select student_Stream from student_info where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
            Dim get_CourseProgram As String = oCommon.getFieldValue(find_CourseProgram)

            Dim find_CourseCampus As String = "Select student_Campus from student_info where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
            Dim get_CourseCampus As String = oCommon.getFieldValue(find_CourseCampus)

            strSQL = "Select subject_Name, subject_ID from subject_info where subject_year = '" & ddlYear.SelectedValue & "' and subject_StudentYear = '" & ddlLevel.SelectedValue & "' and subject_sem = '" & ddlSemester.SelectedValue & "' and subject_type = '" & ddlCourseType.SelectedValue & "' and course_Program = '" & get_CourseProgram & "' and subject_Campus = '" & get_CourseCampus & "' order by subject_Name ASC"
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Try
                Dim ds As DataSet = New DataSet
                sqlDA.Fill(ds, "AnyTable")

                ddlCourse.DataSource = ds
                ddlCourse.DataTextField = "subject_Name"
                ddlCourse.DataValueField = "subject_ID"
                ddlCourse.DataBind()
                ddlCourse.Items.Insert(0, New ListItem("Select Course", String.Empty))

            Catch ex As Exception
            Finally
                objConn.Dispose()
            End Try

        End If
    End Sub

    Private Sub RegisterClass_List()
        Dim find_CourseProgram As String = "Select student_Stream from student_info where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        Dim get_CourseProgram As String = oCommon.getFieldValue(find_CourseProgram)

        Dim find_CourseCampus As String = "Select student_Campus from student_info where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        Dim get_CourseCampus As String = oCommon.getFieldValue(find_CourseCampus)

        strSQL = "Select class_Name, class_ID from class_info where subject_ID = '" & ddlCourse.SelectedValue & "' and class_year = '" & ddlYear.SelectedValue & "' and class_Level = '" & ddlLevel.SelectedValue & "' and class_sem = '" & ddlSemester.SelectedValue & "' and course_Program = '" & get_CourseProgram & "' and class_Campus = '" & get_CourseCampus & "' order by class_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClass.DataSource = ds
            ddlClass.DataTextField = "class_Name"
            ddlClass.DataValueField = "class_ID"
            ddlClass.DataBind()
            ddlClass.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnRegisterCourse_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterCourse.ServerClick

        If ddlYear.SelectedValue <> Now.Year Then
            ShowMessage(" Unable To Register Previous Year Course", MessageType.Error)
        Else
            If ddlClass.SelectedIndex <> 0 Then

                Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("std_ID"))

                Dim errorCount As Integer = 0

                ''check subject exist in course
                Dim subjectExist As String = ""
                subjectExist = "    Select course_ID from course where year = '" & ddlYear.SelectedValue & "' and subject_ID = '" & ddlCourse.SelectedValue & "' and class_ID = '" & ddlClass.SelectedValue & "' and std_ID = '" & data_ID & "'"
                Dim getSubjectExist As String = oCommon.getFieldValue(subjectExist)

                If getSubjectExist = "" Then

                    strSQL = "Insert into course(year,subject_ID,class_ID,std_ID) values('" & ddlYear.SelectedValue & "','" & ddlCourse.SelectedValue & "','" & ddlClass.SelectedValue & "','" & data_ID & "')"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = "0" Then
                        ShowMessage(" Register Class & Course", MessageType.Success)
                    Else
                        ShowMessage(" Unsuccessful Register Class & Course", MessageType.Error)
                    End If
                Else
                    ShowMessage(" The Course Had Been Registered", MessageType.Error)
                End If
            Else
                ShowMessage(" Please Select Class", MessageType.Error)
            End If
        End If
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            RegisterLevel_List()
            RegisterCourseType_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevel.SelectedIndexChanged
        Try
            RegisterCourseType_List()
            RegisterCourse_List()
            RegisterClass_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged
        Try
            RegisterCourseType_List()
            RegisterCourse_List()
            RegisterClass_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlCourseType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourseType.SelectedIndexChanged
        Try
            RegisterCourse_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlCourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCourse.SelectedIndexChanged
        Try
            RegisterClass_List()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub btnRegisterCourse_PreRender(sender As Object, e As EventArgs) Handles btnRegisterCourse.PreRender

    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum

End Class