Imports System.Data.SqlClient

Public Class counselor_Personality_Development
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

                    load_page()

                    Exam_List_Info()
                    Level_List_Info()

                    strRet = BindData(datRespondent)

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT * from setting where Type = 'Year' and Parameter = '" & Now.Year & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlYear.SelectedValue = Now.Year
            End If
        End If
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

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Exam_List_Info()
        strSQL = "SELECT * from exam_info where exam_year = '" & ddlYear.SelectedValue & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamnaming.DataSource = ds
            ddlExamnaming.DataTextField = "exam_Name"
            ddlExamnaming.DataValueField = "exam_ID"
            ddlExamnaming.DataBind()
            ddlExamnaming.Items.Insert(0, New ListItem("Select Exam", String.Empty))

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
        strSQL = "SELECT * from class_info where class_year = '" & ddlYear.SelectedValue & "' and class_Level = '" & ddlLevelnaming.SelectedValue & "' and class_Type = 'Compulsory'"
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
        Dim strOrderby As String = " ORDER BY A.pd_total, E.student_Name ASC"

        tmpSQL = " SELECT * from personality_developmet_mark A
                   LEFT JOIN exam_result B on A.examresult_id = B.id
                   LEFT JOIN course C on B.course_id = C.course_id
                   LEFT JOIN exam_info D on C.exam_id = D.exam_id
                   LEFT JOIN student_info E ON C.std_ID = E.std_ID
                   LEFT JOIN class_info F ON C.class_ID = F.class_ID
                   LEFT JOIN subject_info G ON C.subject_ID = G.subject_ID

                   WHERE A.year = '" & ddlYear.SelectedValue & "' AND G.subject_Type = 'Compulsory' AND F.class_Type = 'Compulsory' AND E.student_status = 'Access'"

        If ddlClassnaming.SelectedIndex > 0 Then
            strWhere += " AND F.class_ID = '" & ddlClassnaming.SelectedValue & "' "
        End If

        If ddlExamnaming.SelectedIndex > 0 Then
            strWhere += " AND D.exam_ID = '" & ddlExamnaming.SelectedValue & "'"
        End If

        If txtstudent.Text.Length > 0 Then
            strWhere += " AND (E.student_Name LIKE '%" & txtstudent.Text & "%' OR E.student_Mykad = '" & txtstudent.Text & "' OR student_ID = '" & txtstudent.Text & "')"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Try
            Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            Exam_List_Info()
            Level_List_Info()

            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlLevelnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevelnaming.SelectedIndexChanged
        Try
            Class_List_Info()

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

        Dim find_leadership As String = "select Value from setting where idx = 'Self Personality Development' and Parameter = 'Leadership'"
        Dim percen_leadership As Integer = Integer.Parse(oCommon.getFieldValue(find_leadership))

        Dim find_communityservice As String = "select Value from setting where idx = 'Self Personality Development' and Parameter = 'Community Service'"
        Dim percen_communityservice As Integer = Integer.Parse(oCommon.getFieldValue(find_communityservice))

        Dim find_reflection As String = "select Value from setting where idx = 'Self Personality Development' and Parameter = 'Reflection'"
        Dim percen_reflection As Integer = Integer.Parse(oCommon.getFieldValue(find_reflection))

        Dim find_assignment As String = "select Value from setting where idx = 'Self Personality Development' and Parameter = 'Assignment'"
        Dim percen_assignment As Integer = Integer.Parse(oCommon.getFieldValue(find_assignment))

        Dim find_apperance As String = "select Value from setting where idx = 'Self Personality Development' and Parameter = 'Appearance'"
        Dim percen_appearance As Integer = Integer.Parse(oCommon.getFieldValue(find_apperance))

        Dim find_roomtidiness As String = "select Value from setting where idx = 'Self Personality Development' and Parameter = 'Room Tidiness'"
        Dim percen_romtidiness As Integer = Integer.Parse(oCommon.getFieldValue(find_roomtidiness))

        Dim find_attitude As String = "select Value from setting where idx = 'Self Personality Development' and Parameter = 'Attitude'"
        Dim percen_attitude As Integer = Integer.Parse(oCommon.getFieldValue(find_attitude))

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    ''get student id
                    Dim find_stdid As String = "select std_ID from course
                                                left join exam_result on course.course_ID = exam_result.course_id
                                                left join personality_development_mark on exam_result.id = personality_development_mark.examresult_id where pd_id = '" & strKey & "'"
                    Dim get_stdid As String = oCommon.getFieldValue(find_stdid)

                    ''sum the demerit point
                    Dim find_sumpoint As String = "select sum(meritdemerit_point) from dicipline_info where std_ID = '" & get_stdid & "'"
                    Dim get_sumponint As Integer = Integer.Parse(oCommon.getFieldValue(find_sumpoint))

                    Dim txt_leadership As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtleadership_mrk"), TextBox)
                    Dim txt_communityservice As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtcommunityservice_mark"), TextBox)
                    Dim txt_reflection As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtreflection_mark"), TextBox)
                    Dim txt_assignment As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtassignment_mark"), TextBox)
                    Dim txt_appearance As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtappearance_mark"), TextBox)
                    Dim txt_roomtidiness As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtroomtidiness_mark"), TextBox)
                    Dim txt_attitude As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txtattitude_mark"), TextBox)

                    Dim find_total_mark As String = "select pd_total from personaltiy_development_mark where pd_id = '" & strKey & "'"
                    Dim data_total_mark As String = oCommon.getFieldValue(find_total_mark)

                    Dim total_attitude As Integer = txt_attitude.Text - get_sumponint
                    Dim total_mark As Integer = txt_leadership.Text + txt_communityservice.Text + txt_reflection.Text + txt_assignment.Text + txt_appearance.Text + txt_roomtidiness.Text + total_attitude

                    If data_total_mark.Length > 0 Then
                        If txt_leadership.Text > percen_leadership Then
                            ShowMessage(" Please fill in leadership mark according to setting ", MessageType.Error)
                            Exit For
                        End If

                        If txt_communityservice.Text > percen_communityservice Then
                            ShowMessage(" Please fill in coomunity service mark according to setting ", MessageType.Error)
                            Exit For
                        End If

                        If txt_reflection.Text > percen_reflection Then
                            ShowMessage(" Please fill in reflection mark according to setting ", MessageType.Error)
                            Exit For
                        End If

                        If txt_assignment.Text > percen_assignment Then
                            ShowMessage(" Please fill in assignment mark according to setting ", MessageType.Error)
                            Exit For
                        End If

                        If txt_appearance.Text > percen_appearance Then
                            ShowMessage(" Please fill in appearance mark according to setting ", MessageType.Error)
                            Exit For
                        End If

                        If txt_roomtidiness.Text > percen_romtidiness Then
                            ShowMessage(" Please fill in room tidiness mark according to setting ", MessageType.Error)
                            Exit For
                        End If

                        If txt_attitude.Text > percen_attitude Then
                            ShowMessage(" Please fill in attitude mark according to setting ", MessageType.Error)
                            Exit For
                        End If

                        ''update to database
                        strSQL = "UPDATE personality_development_mark
                                  SET atitude_percen = '" & percen_attitude & "', attitude_mark = '" & txt_attitude.Text & "',
                                  leadership_percen = '" & percen_leadership & "', leadership_mark = '" & txt_leadership.Text & "',
                                  communityservice_percen = '" & percen_communityservice & "', community_mark = '" & txt_communityservice.Text & "',
                                  reflection_percen = '" & percen_reflection & "', reflection_mark = '" & txt_reflection.Text & "',
                                  assignment_percen = '" & percen_assignment & "', assignment_mark = '" & txt_assignment.Text & "',
                                  appearance_percen = '" & percen_appearance & "', appearance_mark = '" & txt_appearance.Text & "',
                                  roomtidiness_percen = '" & percen_romtidiness & "', roomtidiness_mark = '" & txt_roomtidiness.Text & "',
                                  pd_total_demerit = '" & get_sumponint & "', pd_total_attitude ='" & total_attitude & "',
                                  pd_total = '" & total_mark & "'
                                  Where pd_id = '" & strKey & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        Dim select_examresultid As String = "select examresult_id from personality_development_mark where pd_id = '" & strKey & "'"
                        Dim get_examresultid As String = oCommon.getFieldValue(select_examresultid)

                        Dim ResultGrades As String = "select grade_Name from grade_info where grade_min_range <= '" & total_mark & "' and grade_max_range >= '" & total_mark & "'"
                        Dim get_grade As String = oCommon.getFieldValue(ResultGrades)

                        strSQL = "UPDATE exam_result SET
                                  marks = '" & total_mark & "', grade = '" & get_grade & "'
                                  WHERE ID = '" & get_examresultid & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        Dim resilt As String = percen_checking(txt_leadership.Text, txt_communityservice.Text, txt_reflection.Text, txt_assignment.Text, txt_appearance.Text, txt_roomtidiness.Text, txt_attitude.Text, strKey)

                        If result = "True" Then
                            ''update to database
                            strSQL = "UPDATE personality_development_mark SET
                                      attitude_mark = '" & txt_attitude.Text & "',
                                      leadership_mark = '" & txt_leadership.Text & "',
                                      community_mark = '" & txt_communityservice.Text & "',
                                      reflection_mark = '" & txt_reflection.Text & "',
                                      assignment_mark = '" & txt_assignment.Text & "',
                                      appearance_mark = '" & txt_appearance.Text & "',
                                      roomtidiness_mark = '" & txt_roomtidiness.Text & "',
                                      pd_total_attitude ='" & total_attitude & "',
                                      pd_total = '" & total_mark & "'
                                      Where pd_id = '" & strKey & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            Dim select_examresultid As String = "select examresult_id from personality_development_mark where pd_id = '" & strKey & "'"
                            Dim get_examresultid As String = oCommon.getFieldValue(select_examresultid)

                            Dim ResultGrades As String = "select grade_Name from grade_info where grade_min_range <= '" & total_mark & "' and grade_max_range >= '" & total_mark & "'"
                            Dim get_grade As String = oCommon.getFieldValue(ResultGrades)

                            strSQL = "UPDATE exam_result SET
                                      marks = '" & total_mark & "', grade = '" & get_grade & "'
                                      WHERE ID = '" & get_examresultid & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        End If
                    End If
                End If
            End If
        Next
    End Sub

    Private Function percen_checking(leadership As String, communityservice As String, reflection As String, assignment As String, appearance As String, roomtidiness As String, attitude As String, pd_id As String)

        ''checking leadership percen
        Dim find_checking_leadership As String = "select leadership_percen from personality_development_mark where pd_id = '" & pd_id & "'"
        Dim get_checking_leadership As String = oCommon.getFieldValue(find_checking_leadership)

        ''checking coomunityservice percen
        Dim find_checking_communityservice As String = "select communityservice_percen from personality_development_mark where pd_id = '" & pd_id & "'"
        Dim get_checking_communityservice As String = oCommon.getFieldValue(find_checking_communityservice)

        ''checking reflection percen
        Dim find_checking_reflection As String = "select reflection_percen from personality_development_mark where pd_id = '" & pd_id & "'"
        Dim get_checking_reflection As String = oCommon.getFieldValue(find_checking_reflection)

        ''checking assignment percen
        Dim find_checking_assignment As String = "select assignment_percen from personality_development_mark where pd_id = '" & pd_id & "'"
        Dim get_checking_assignment As String = oCommon.getFieldValue(find_checking_assignment)

        ''checking appearance percen
        Dim find_checking_appearance As String = "select appearance_percen from personality_development_mark where pd_id = '" & pd_id & "'"
        Dim get_checking_appearance As String = oCommon.getFieldValue(find_checking_appearance)

        ''checking roomtidiness percen
        Dim find_checking_roomtidiness As String = "select roomtidiness_percen from personality_development_mark where pd_id = '" & pd_id & "'"
        Dim get_checking_roomtidiness As String = oCommon.getFieldValue(find_checking_roomtidiness)

        ''checking attitude percen
        Dim find_checking_attitude As String = "select attitude_percen from personality_development_mark where pd_id = '" & pd_id & "'"
        Dim get_checking_attitude As String = oCommon.getFieldValue(find_checking_attitude)

        If get_checking_leadership.Length = 0 Or get_checking_leadership > leadership Then
            ShowMessage(" Please fill in leadership mark according to setting ", MessageType.Error)
            Return "False"
        End If

        If get_checking_communityservice.Length = 0 Or get_checking_communityservice > communityservice Then
            ShowMessage(" Please fill in community service mark according to setting ", MessageType.Error)
            Return "False"
        End If

        If get_checking_reflection.Length = 0 Or get_checking_reflection > reflection Then
            ShowMessage(" Please fill in reflection mark according to setting ", MessageType.Error)
            Return "False"
        End If

        If get_checking_assignment.Length = 0 Or get_checking_assignment > assignment Then
            ShowMessage(" Please fill in assignment mark according to setting ", MessageType.Error)
            Return "False"
        End If

        If get_checking_appearance.Length = 0 Or get_checking_appearance > appearance Then
            ShowMessage(" Please fill in appearance mark according to setting ", MessageType.Error)
            Return "False"
        End If

        If get_checking_roomtidiness.Length = 0 Or get_checking_roomtidiness > roomtidiness Then
            ShowMessage(" Please fill in room tidiness mark according to setting ", MessageType.Error)
            Return "False"
        End If

        If get_checking_attitude.Length = 0 Or get_checking_attitude > attitude Then
            ShowMessage(" Please fill in attitude mark according to setting ", MessageType.Error)
            Return "False"
        End If

        Return "True"
    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class