Imports System.Data.SqlClient

Public Class admin_laporanPelajar_peperiksaanDetail
    Inherits System.Web.UI.Page
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Dim stdName As String
    Dim stdID As String
    Dim className As String
    Dim examName As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    stdName = "select student_Name from student_info where student_ID = '" & Request.QueryString("student_ID") & "'"
                    Dim studentName As String = getFieldValueA_plus(stdName, strConn)
                    txtstudent_Name.Text = studentName

                    stdID = "select student_ID from student_info where student_ID = '" & Request.QueryString("student_ID") & "'"
                    Dim studentID As String = getFieldValueA_plus(stdID, strConn)
                    txtstudent_id.Text = studentID

                    className = "select class_Name from class_info left join course on class_info.class_Id=course.class_ID left join student_info on course.student_ID=student_info.student_ID where student_info.student_ID = '" & Request.QueryString("student_ID") & "'"
                    Dim clssName As String = getFieldValueA_plus(className, strConn)
                    txtclass_Name.Text = clssName

                    examName = "select exam_Name from exam_info left join exam_result on exam_info.exam_ID=exam_result.exam_ID left join course on exam_result.course_ID=course.course_ID left join student_info on course.student_ID=student_info.student_ID where student_info.student_ID = '" & Request.QueryString("student_ID") & "'"
                    Dim exmName As String = getFieldValueA_plus(examName, strConn)
                    txtexam_Name.Text = exmName

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

        tmpSQL = "Select course.course_ID,subject_info.subject_Name,subject_info.subject_code,subject_info.subject_type,exam_result.grade From course Left Join subject_info ON course.subject_ID=subject_info.subject_ID Left Join exam_result ON course.course_ID=exam_result.course_ID left join student_info on course.student_ID=student_info.student_ID left join exam_Info on exam_result.exam_ID=exam_Info.exam_ID"
        strWhere += " WHERE student_info.student_ID='" & Request.QueryString("student_ID") & "' And exam_Info.exam_Name='" & Request.QueryString("exam_Name") & "'"
        strOrder = " ORDER BY subject_info.subject_Name"

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