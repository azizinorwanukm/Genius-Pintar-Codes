Imports System.Data.SqlClient

Public Class student_course_information
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

                load_page()
                strRet = BindData(datRespondent)
                ''Generate_Table()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "select distinct year from student_level where year = '" & Now.Year & "'"

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
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlyear.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub Year()

        Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("std_ID"))

        strSQL = "select distinct year from student_level where std_ID = '" & data_ID & "'"
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
        strSQL = "SELECT Parameter, Value from setting where Type = 'Sem'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlsem.DataSource = ds
            ddlsem.DataTextField = "Parameter"
            ddlsem.DataValueField = "Value"
            ddlsem.DataBind()
            ddlsem.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlsem.SelectedIndex = 0

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

    Protected Sub ddlsem_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsem.SelectedIndexChanged
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

        Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("std_ID"))

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY subject_Name ASC"

        tmpSQL = "Select * From course left join subject_info on course.subject_ID=subject_info.subject_ID left join class_info on course.class_ID=class_info.class_ID left join student_info on course.std_ID=student_info.std_ID"
        strWhere = " WHERE course.std_ID = '" & data_ID & "'"
        strWhere += " AND course.year = '" & ddlyear.SelectedValue & "'"
        strWhere += " AND subject_info.subject_Sem = '" & ddlsem.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug
        Return getSQL
    End Function
End Class