Imports System.Data.SqlClient

Public Class parent_studentCourse
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
                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Year()
        strSQL = "SELECT distinct year from student_Level where std_ID = '" & Request.QueryString("std_ID") & "'"
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

    Public Function getFieldValue(ByVal sql_plus As String, ByVal MyConnection As String) As String
        If sql_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sql_plus, conn)
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

    Private Function getSQL() As String

        Dim id As String = ""
        Dim dataSTDID As String = ""

        id = Request.QueryString("std_ID")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_ID ASC"

        tmpSQL = "Select * From course 
                  left join subject_info on course.subject_ID=subject_info.subject_ID 
                  left join class_info on course.class_ID=class_info.class_ID 
                  left join student_info on course.std_ID=student_info.std_ID"
        strWhere = " WHERE student_info.std_ID = '" & id & "'"

        If ddlyear.SelectedIndex > 0 Then
            strWhere += " AND course.year = '" & ddlyear.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere
        ''--debug
        Return getSQL
    End Function
End Class