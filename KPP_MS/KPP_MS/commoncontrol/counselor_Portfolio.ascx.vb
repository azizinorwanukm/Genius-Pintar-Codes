Imports System.Data.SqlClient

Public Class counselor_Portfolio
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

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    Year_List_Info()
                    Program_List_Info()
                    Exam_List_Info()
                    Level_List_Info()

                    strRet = BindData(datRespondent)

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Year_List_Info()
        strSQL = "SELECT * from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Value"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Program_List_Info()
        strSQL = "SELECT * from setting where Type = 'Stream'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlProgram.DataSource = ds
            ddlProgram.DataTextField = "Parameter"
            ddlProgram.DataValueField = "Value"
            ddlProgram.DataBind()
            ddlProgram.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Exam_List_Info()

        strSQL = ""

        If ddlLevelnaming.SelectedValue = "Foundation 1" Then
            strSQL = "SELECT Parameter, Value from setting where Type = 'Exam' and (Parameter = 'Exam 1' or Parameter = 'Exam 2' or Parameter = 'Exam 3' or Parameter = 'Exam 4') "
        ElseIf ddlLevelnaming.SelectedValue = "Foundation 2" Then
            strSQL = "SELECT Parameter, Value from setting where Type = 'Exam' and (Parameter = 'Exam 5' or Parameter = 'Exam 6' or Parameter = 'Exam 7' or Parameter = 'Exam 8') "
        ElseIf ddlLevelnaming.SelectedValue = "Foundation 3" Then
            strSQL = "SELECT Parameter, Value from setting where Type = 'Exam' and (Parameter = 'Exam 9' or Parameter = 'Exam 10' or Parameter = 'Exam 11' or Parameter = 'Exam 12') "
        ElseIf ddlLevelnaming.SelectedValue = "Level 1" Then
            strSQL = "SELECT Parameter, Value from setting where Type = 'Exam' and (Parameter = 'Exam 1' or Parameter = 'Exam 2' or Parameter = 'Exam 3' or Parameter = 'Exam 4') "
        ElseIf ddlLevelnaming.SelectedValue = "Level 2" Then
            strSQL = "SELECT Parameter, Value from setting where Type = 'Exam' and (Parameter = 'Exam 5' or Parameter = 'Exam 6' or Parameter = 'Exam 7' or Parameter = 'Exam 8') "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamnaming.DataSource = ds
            ddlExamnaming.DataTextField = "Value"
            ddlExamnaming.DataValueField = "Parameter"
            ddlExamnaming.DataBind()
            ddlExamnaming.Items.Insert(0, New ListItem("Select Examination", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Level_List_Info()
        strSQL = "SELECT * from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevelnaming.DataSource = ds
            ddlLevelnaming.DataTextField = "Parameter"
            ddlLevelnaming.DataValueField = "Parameter"
            ddlLevelnaming.DataBind()
            ddlLevelnaming.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Class_List_Info()
        strSQL = "SELECT * from class_info where class_year = '" & ddlYear.SelectedValue & "' and class_Level = '" & ddlLevelnaming.SelectedValue & "' and class_Type = 'Compulsory' and course_Program = '" & ddlProgram.SelectedValue & "' and class_Campus = 'PGPN' order by class_name asc"
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
        Dim strOrderby As String = " ORDER BY F.class_Name, E.student_Name ASC"

        tmpSQL = "  SELECT B.ID, E.student_Name, E.student_ID, G.subject_StudentYear, G.subject_sem, F.class_Name, B.marks, B.grade from exam_result B
                    LEFT JOIN course C on B.course_id = C.course_id
                    LEFT JOIN exam_info D on B.exam_id = D.exam_id
                    LEFT JOIN student_info E ON C.std_ID = E.std_ID
                    LEFT JOIN class_info F ON C.class_ID = F.class_ID
                    LEFT JOIN subject_info G ON C.subject_ID = G.subject_ID

                    WHERE C.year = '" & ddlYear.SelectedValue & "' AND F.class_year = '" & ddlYear.SelectedValue & "' AND G.subject_year = '" & ddlYear.SelectedValue & "'
                    AND F.class_level = '" & ddlLevelnaming.SelectedValue & "' AND G.subject_StudentYear = '" & ddlLevelnaming.SelectedValue & "'

                    AND G.subject_Type = 'Compulsory' AND F.class_Type = 'Compulsory' AND (E.student_status = 'Access' or E.student_status = 'Graduate') AND E.student_Stream = '" & ddlProgram.SelectedValue & "'
                    AND D.exam_Name = '" & ddlExamnaming.SelectedValue & "' AND E.student_Campus = 'PGPN'
                    And (G.subject_Name = 'Portfolio' OR G.subject_NameBM = 'Portfolio')"

        If ddlClassnaming.SelectedIndex > 0 Then
            strWhere += " AND F.class_ID = '" & ddlClassnaming.SelectedValue & "' "
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            Class_List_Info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlProgram_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProgram.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevelnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevelnaming.SelectedIndexChanged
        Try
            Class_List_Info()
            Exam_List_Info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlClassnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassnaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlExamnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExamnaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSave_ServerClick(sender As Object, e As EventArgs) Handles btnSave.ServerClick
        Dim i As Integer = 0
        Dim value As String = ""

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    Dim txt_totalark As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txttotal_mark"), TextBox)

                    Dim ResultGrades As String = "select grade_Name from grade_info where grade_min_range <= '" & txt_totalark.Text & "' and grade_max_range >= '" & txt_totalark.Text & "'"
                    Dim get_grade As String = oCommon.getFieldValue(ResultGrades)

                    strSQL = "UPDATE exam_result SET
                              marks = '" & txt_totalark.Text & "', grade = '" & get_grade & "'
                              WHERE ID = '" & strKey & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = "0" Then
                        ShowMessage(" Update Student Portfolio Result ", MessageType.Success)
                    Else
                        ShowMessage(" Unsuccessful Update Student Portfolio Result ", MessageType.Error)
                    End If

                End If
            End If
        Next

        strRet = BindData(datRespondent)
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class