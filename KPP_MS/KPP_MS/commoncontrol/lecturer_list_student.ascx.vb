Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class lecturer_list_student
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                Dim id As String = Request.QueryString("stf_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then

                    year_list()
                    student_program()
                    student_Level()
                    student_Sem()
                    class_info_list()

                    strRet = BindData(datRespondent)

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlClassnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassnaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlSemnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemnaming.SelectedIndexChanged
        Try
            class_info_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            student_Level()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlLevelnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevelnaming.SelectedIndexChanged
        Try
            class_info_list()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlProgramnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProgramnaming.SelectedIndexChanged
        Try
            class_info_list()
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

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY student_Name ASC"

        tmpSQL = "  Select distinct student_info.student_ID, student_info.std_ID,
                    student_info.student_Mykad, UPPER(student_info.student_Name) student_Name,
                    student_info.student_Photo, class_info.class_Name, class_info.class_Level From student_info
                    left join student_Level on student_info.std_ID=student_level.std_ID
                    left join course on student_info.std_ID= course.std_ID 
                    left join class_info on course.class_ID= class_info.class_ID
                    left join staff_info on class_info.stf_ID = staff_info.stf_ID"

        strWhere = "    WHERE student_info.std_ID IS NOT NULL and student_Status = 'Access' and student_Level.year = '" & ddlYear.SelectedValue & "'"
        strWhere += "   and course.year = '" & ddlYear.SelectedValue & "' and class_info.class_Campus = '" & Session("SchoolCampus") & "' and class_info.course_Program = '" & ddlProgramnaming.SelectedValue & "'"
        strWhere += "   and staff_info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and student_info.student_Campus = '" & Session("SchoolCampus") & "' and student_info.student_Stream = '" & ddlProgramnaming.SelectedValue & "'"

        If ddlLevelnaming.SelectedIndex > 0 Then
            strWhere += " and class_info.class_Level = '" & ddlLevelnaming.SelectedValue & "'"
        End If

        If ddlClassnaming.SelectedIndex > 0 Then
            strWhere += " and class_info.class_ID = '" & ddlClassnaming.SelectedValue & "'"
        End If

        If ddlSemnaming.SelectedIndex > 0 Then
            strWhere += " and student_Level.student_Level = '" & ddlSemnaming.SelectedValue & "')"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Debug.WriteLine(getSQL)
        ''--debug
        Return getSQL
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("pengajar_pelajar_detail.aspx?std_ID=" + strKeyID + "&stf_ID=" + Request.QueryString("stf_ID") + "&status=SI")
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub class_info_list()

        strSQL = "  select class_info.class_ID, class_info.class_Name from class_info
                    where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'
                    and class_year = '" & ddlYear.SelectedValue & "'
                    and class_Level = '" & ddlLevelnaming.SelectedValue & "' and course_Program = '" & ddlProgramnaming.SelectedValue & "' and class_Campus = '" & Session("SchoolCampus") & "'
                    order by class_Name asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassnaming.DataSource = ds
            ddlClassnaming.DataTextField = "class_Name"
            ddlClassnaming.DataValueField = "class_ID"
            ddlClassnaming.DataBind()
            ddlClassnaming.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Level()
        strSQL = "SELECT class_Level from class_info where class_year = '" & ddlYear.SelectedValue & "' and stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and class_Campus = '" & Session("SchoolCampus") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevelnaming.DataSource = ds
            ddlLevelnaming.DataTextField = "class_Level"
            ddlLevelnaming.DataValueField = "class_Level"
            ddlLevelnaming.DataBind()
            ddlLevelnaming.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Sem()
        strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemnaming.DataSource = ds
            ddlSemnaming.DataTextField = "Parameter"
            ddlSemnaming.DataValueField = "Value"
            ddlSemnaming.DataBind()
            ddlSemnaming.Items.Insert(0, New ListItem("Select Semester", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_list()
        strSQL = "select distinct class_year from class_info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and class_Campus = '" & Session("SchoolCampus") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "class_year"
            ddlYear.DataValueField = "class_year"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_program()
        If Session("SchoolCampus") = "APP" Then
            strSQL = "select Parameter, Value from setting where type = 'Stream' and Value = 'PS'"
        Else
            strSQL = "select Parameter, Value from setting where type = 'Stream' and Value <> 'Temp'"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlProgramnaming.DataSource = ds
            ddlProgramnaming.DataTextField = "Parameter"
            ddlProgramnaming.DataValueField = "Value"
            ddlProgramnaming.DataBind()
            ddlProgramnaming.Items.Insert(0, New ListItem("Select Program", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

End Class