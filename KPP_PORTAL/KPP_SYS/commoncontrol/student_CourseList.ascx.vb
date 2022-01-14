Imports System.Data.SqlClient

Public Class student_CourseList
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
                Year()
                Sem()
                strRet = BindData(datRespondent)
                ''Generate_Table()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Year()
        strSQL = "select distinct student_level.year from student_info
                  left join student_level on student_info.std_ID=student_level.std_ID
                  where student_info.std_ID = '" + Request.QueryString("std_ID") + "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "year"
            ddlyear.DataValueField = "year"
            ddlyear.DataBind()
            ddlyear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlyear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Sem()
        strSQL = "select Parameter From setting where Type = 'Sem'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSem.DataSource = ds
            ddlSem.DataTextField = "Parameter"
            ddlSem.DataValueField = "Parameter"
            ddlSem.DataBind()
            ddlSem.Items.Insert(0, New ListItem("Select Sem", String.Empty))
            ddlSem.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlSem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSem.SelectedIndexChanged
        Try
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
        Dim strOrderby As String = " ORDER BY subject_ID ASC"

        tmpSQL = "Select * From course 
                  left join subject_info on course.subject_ID=subject_info.subject_ID 
                  left join class_info on course.class_ID=class_info.class_ID 
                  left join student_level on course.std_ID=student_level.std_ID
                  left join student_info on student_level.std_ID=student_info.std_ID "
        strWhere = " WHERE student_info.std_ID = '" & Request.QueryString("std_ID") & "'"

        If ddlyear.SelectedIndex > 0 Then
            strWhere += " AND course.year = '" & ddlyear.SelectedValue & "'"
        End If

        If ddlSem.SelectedIndex > 0 Then
            strWhere += " AND student_Level.student_Sem = '" & ddlSem.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere
        ''--debug
        Return getSQL
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing

        datRespondent.EditIndex = e.NewEditIndex
        Me.BindData(datRespondent)

    End Sub

    Protected Sub OnRowCancelingEdit(sender As Object, e As EventArgs)
        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)

        Dim txtSubjectCode As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtsubject_code"), TextBox)

        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        ''select subject code to get subject_ID
        Dim subjectID As String = "select subject_ID from subject_info where subject_code like '%" + txtSubjectCode.Text + "%'"
        Dim datasubjectID As String = getFieldValue(subjectID, strConn)

        ''update the data in table
        strSQL = "UPDATE course SET subject_ID='" & datasubjectID & "' WHERE course_ID ='" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
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
End Class