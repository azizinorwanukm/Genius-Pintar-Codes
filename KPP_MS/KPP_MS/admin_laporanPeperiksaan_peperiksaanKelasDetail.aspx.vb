Imports System.Data.SqlClient

Public Class admin_laporanPeperiksaan_peperiksaanKelasDetail
    Inherits System.Web.UI.Page
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Dim examName As String
    Dim examYear As String
    Dim className As String
    Dim subjectName As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    examName = "select exam_Name from exam_Info where exam_Name = '" & Request.QueryString("exam_Name") & "' And exam_Year='" & Now.Year & "'"
                    Dim examinationName As String = getFieldValueA_plus(examName, strConn)
                    txtexam_Name.Text = examinationName

                    examYear = "select exam_Year from exam_Info where exam_Year = '" & Now.Year & "'"
                    Dim studentID As String = getFieldValueA_plus(examYear, strConn)
                    txtexam_Year.Text = studentID

                    className = "select class_Name from class_info where class_Name = '" & Request.QueryString("class_Name") & "'"
                    Dim clssName As String = getFieldValueA_plus(className, strConn)
                    txtclass_Name.Text = clssName

                    subjectName = "select subject_Name from subject_info where subject_Name = '" & Request.QueryString("subject_Name") & "'"
                    Dim exmName As String = getFieldValueA_plus(examName, strConn)
                    txtsubject_Name.Text = exmName

                    strRet = BindData(datRespondent)
                End If

            End If
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
        Dim strOrder As String = ""

        tmpSQL = "select student_info.student_Name,student_info.student_ID,student_info.student_Mykad,exam_result.grade from student_info left join course on student_info.student_ID = course.student_ID left join subject_info on course.subject_ID = subject_info.subject_ID left join exam_result on course.course_ID = exam_result.course_ID left join class_info on course.class_ID = class_info.class_ID left join exam_Info on exam_result.exam_ID = exam_Info.exam_ID"
        strWhere += " where class_info.class_Name ='" & Request.QueryString("class_Name") & "' And subject_info.subject_Name='" & Request.QueryString("subject_Name") & "' And exam_Info.exam_Name='" & Request.QueryString("exam_Name") & "' and exam_Info.exam_Year='" & Now.Year & "'"
        strOrder = " ORDER BY exam_result.marks ASC"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug

        Return getSQL

    End Function

    Public Function getFieldValueA_plus(ByVal sqlA_plus As String, ByVal MyConnection As String) As String
        If sqlA_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlA_plus, conn)
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

End Class