Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Net.Mail

Public Class Disiplin_student_detail
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim page_view As New Integer
    Dim tempSTDID As String
    Dim tempDISPID As String
    Dim cmd As SqlCommand
    Dim errCount As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub StaffDDL()
        Dim staffSQL = "SELECT staff_Name, stf_ID FROM staff_info WHERE staff_Year='" & Now.Year & "'"
        Dim sqlDA As New SqlDataAdapter(staffSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            ddlReporter.DataSource = ds
            ddlReporter.DataTextField = "staff_Name"
            ddlReporter.DataValueField = "stf_ID"
            ddlReporter.DataBind()
            ddlReporter.Items.Insert(0, New ListItem("Select reporter", String.Empty))
            ddlReporter.SelectedIndex = 0
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub CaseDDL()
        Dim caseSQL = "SELECT case_Name,case_ID FROM case_info"
        Dim sqlDA As New SqlDataAdapter(caseSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            ddlCaseType.DataSource = ds
            ddlCaseType.DataTextField = "case_Name"
            ddlCaseType.DataValueField = "case_ID"
            ddlCaseType.DataBind()
            ddlCaseType.Items.Insert(0, New ListItem("Select Case Type", String.Empty))
            ddlCaseType.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub WarnignLetterDDL()
        Dim ltrSQL = "SELECT id,title FROM warning_letters_table"
        Dim sqlDA As New SqlDataAdapter(ltrSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            ddlLetterType.DataSource = ds
            ddlLetterType.DataTextField = "title"
            ddlLetterType.DataValueField = "id"
            ddlLetterType.DataBind()
            ddlLetterType.Items.Insert(0, New ListItem("Select letter ", String.Empty))
            ddlLetterType.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LoadPage(queryID As String)
        If page_view.Equals(0) = True Then
            GetCaseDetail(queryID)
        ElseIf page_view.Equals(1) = True Then
            GetWarningLetterList(queryID)
        ElseIf page_view.Equals(2) = True Then
            GetWarningLetterDetail(queryID)
        End If
    End Sub

    'Private Function BindData(ByVal gvTable As GridView) As Boolean
    '    Dim mydataSet As New DataSet
    '    Dim myDataAdapter As New SqlDataAdapter(getSql, strConn)
    '    myDataAdapter.SelectCommand.CommandTimeout = 120

    '    Try
    '        objConn.Open()
    '        myDataAdapter.Fill(mydataSet, "myaccount")
    '        gvTable.DataSource = mydataSet
    '        gvTable.DataBind()
    '    Catch ex As Exception
    '        Return False
    '    Finally
    '        objConn.Close()
    '    End Try
    '    Return True
    'End Function

    Protected Sub GetCaseDetail(dispId As String)
        Dim caseQuery As String = "SELECT 
	                            student_info.student_Name,
	                            student_info.student_ID,
	                            student_info.student_Mykad,
	                            class_info.class_Name,
	                            dicipline_info.Dicipline_Date,
	                            dicipline_info.case_ID,
	                            dicipline_info.Detail_Case,
	                            dicipline_info.Dicipline_Action,
	                            dicipline_info.demerit_mark,
	                            dicipline_info.disiplin_id,
                                dicipline_info.stf_id,
                                counseling_info.kslr_status,
                                counseling_info.kslr_session
                            FROM dicipline_info
	                            JOIN student_info
		                            ON student_info.std_ID = dicipline_info.std_ID
	                            JOIN class_info
		                            ON class_info.class_ID = dicipline_info.class_ID
	                            FULL JOIN counseling_info
                                    ON counseling_info.disiplin_id = dicipline_info.disiplin_id
                            WHERE dicipline_info.disiplin_id = '" + dispId + "'"
        Dim sqlDA As New SqlDataAdapter(caseQuery, objConn)

        Try
            Dim ds As New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                StudentNameLbl.Text = ds.Tables(0).Rows(0).Item("student_Name")
            Else
                StudentNameLbl.Text = " "
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_ID")) Then
                StudentIdLbl.Text = ds.Tables(0).Rows(0).Item("student_ID")
            Else
                StudentIdLbl.Text = " "
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_MyKad")) Then
                StudentMyKadLbl.Text = ds.Tables(0).Rows(0).Item("Student_MyKad")
            Else
                StudentMyKadLbl.Text = " "
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Name")) Then
                StudentClassLbl.Text = ds.Tables(0).Rows(0).Item("class_Name")
            Else
                StudentClassLbl.Text = " "
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("stf_ID")) Then
                ddlReporter.SelectedValue = ds.Tables(0).Rows(0).Item("stf_ID")
            Else
                ddlReporter.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("case_ID")) Then
                ddlCaseType.SelectedValue = ds.Tables(0).Rows(0).Item("case_ID")
            Else
                ddlCaseType.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Detail_Case")) Then
                Detail_case.Text = ds.Tables(0).Rows(0).Item("Detail_Case")
            Else
                Detail_case.Text = " "
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dicipline_Action")) Then
                Action_box.Text = ds.Tables(0).Rows(0).Item("Dicipline_Action")
            Else
                Action_box.Text = " "
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dicipline_Date")) Then
                CurrentDate.Text = ds.Tables(0).Rows(0).Item("Dicipline_Date")
            Else
                CurrentDate.Text = " "
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("demerit_mark")) Then
                demerit_mark.Text = ds.Tables(0).Rows(0).Item("demerit_mark")
                If demerit_mark.Text.Length < 0 Then
                    demerit_mark.Text = "0"
                End If
            Else
                demerit_mark.Text = "0"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("kslr_status")) Or Not IsDBNull(ds.Tables(0).Rows(0).Item("kslr_session")) Then
                kslgStatusLbl.Text = ds.Tables(0).Rows(0).Item("kslr_status")
                kslgSession.Text = ds.Tables(0).Rows(0).Item("kslr_session")
                counselingDiv.Visible = True
            Else
                counselingDiv.Visible = False
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GetWarningLetterList(stdID As String)
        Dim query As String = "SELECT 
	        dicipline_info.disiplin_id,
	        student_info.student_Name,
            student_info.student_ID,
	        student_info.student_Mykad,
	        class_info.class_Name,
	        case_info.case_Name,
	        case_info.case_Category,
	        dicipline_info.Dicipline_Date,
	        counseling_info.kslr_status
        FROM 
	        kolejadmin.dbo.dicipline_info
	        JOIN student_info
	        ON student_info.std_ID = dicipline_info.std_ID
	        JOIN class_info
	        ON class_info.class_ID = dicipline_info.class_ID
	        JOIN case_info
	        ON case_info.case_ID = dicipline_info.case_ID
	        FULL JOIN counseling_info
	        ON counseling_info.disiplin_id = dicipline_info.disiplin_id
        WHERE dicipline_info.std_ID ='" & stdID & "'"
        Dim sqlDA As New SqlDataAdapter(query, objConn)
        Try
            Dim ds As New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                wlListStudentName.Text = ds.Tables(0).Rows(0).Item("student_Name")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Name")) Then
                wlClassName.Text = ds.Tables(0).Rows(0).Item("class_Name")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_ID")) Then
                wlStudentID.Text = ds.Tables(0).Rows(0).Item("student_ID")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_MyKad")) Then
                wlStudentMyKad.Text = ds.Tables(0).Rows(0).Item("student_MyKad")
            End If
            datRespondent.DataSource = ds
            datRespondent.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetWarningLetterDetail(dispID As String)
        Dim query As String = "SELECT 
                                    student_info.std_ID,
	                                dicipline_info.disiplin_id,
	                                student_info.student_Name,
                                    student_info.student_ID,
	                                student_info.student_Mykad,
	                                class_info.class_Name,
	                                case_info.case_Name,
	                                case_info.case_Category,
	                                dicipline_info.Dicipline_Date,
	                                counseling_info.kslr_status,
                                    counseling_info.kslr_session,
                                    dicipline_info.warning_letter
                                FROM 
	                                dicipline_info
	                                JOIN student_info
	                                ON student_info.std_ID = dicipline_info.std_ID
	                                JOIN class_info
	                                ON class_info.class_ID = dicipline_info.class_ID
	                                JOIN case_info
	                                ON case_info.case_ID = dicipline_info.case_ID
	                                FULL JOIN counseling_info
	                                ON counseling_info.disiplin_id = dicipline_info.disiplin_id
                                WHERE dicipline_info.disiplin_id ='" + dispID + "'"

        Dim sqlDA As New SqlDataAdapter(query, objConn)

        Try
            Dim ds As New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("std_ID")) Then
                tempSTDID = ds.Tables(0).Rows(0).Item("std_ID")
            End If

            If ds.Tables.Count > 0 Then

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("std_ID")) Then
                    tempSTDID = ds.Tables(0).Rows(0).Item("std_ID")
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_info.student_Name")) Then
                    wlStudentNameLbl.Text = ds.Tables(0).Rows(0).Item("student_info.student_Name")
                Else
                    wlStudentNameLbl.Text = "empty"
                End If
            Else
                wlStudentNameLbl.Text = "table empty"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSimpanCase_ServerClick(sender As Object, e As EventArgs) Handles BtnSimpanCase.ServerClick
        'Try
        '    Dim cmd As New SqlCommand
        '    cmd.CommandType = CommandType.Text
        '    cmd.CommandText = "UPDATE dicipline_info 
        '                       SET 
        '                            stf_ID = '@stfID',
        '                            Detail_case = '@caseDetail',
        '                            case_ID = '@caseType',
        '                            Dicipline_Action = '@caseAction',
        '                            demerit_mark = '@demeritMark',
        '                            Dicipline_Date = '@caseDate' 
        '                       WHERE disiplin_id = '@dispID'"
        '    cmd.Parameters.Add("@stfID", SqlDbType.Int).Value = ddlReporter.SelectedValue
        '    cmd.Parameters.Add("@caseDetail", SqlDbType.NVarChar, 250).Value = Detail_case.Text
        '    cmd.Parameters.Add("@caseType", SqlDbType.Int).Value = ddlCaseType.SelectedValue
        '    cmd.Parameters.Add("@caseAction", SqlDbType.NVarChar, 500).Value = Action_box.Text
        '    cmd.Parameters.Add("@demeritMark", SqlDbType.NChar, 10).Value = demerit_mark.Text
        '    cmd.Parameters.Add("@caseDate", SqlDbType.VarChar, 50).Value = CurrentDate.Text
        '    cmd.Parameters.Add("@dispID", SqlDbType.Int).Value = Request.QueryString("dispID")
        '    cmd.Connection = objConn
        '    objConn.Open()
        '    cmd.ExecuteNonQuery()
        '    errCount = 0
        'Catch ex As Exception
        '    errCount = 12
        'Finally
        '    objConn.Close()
        '    Response.Redirect("admin_view_disiplin.aspx?admin_ID=" & Request.QueryString("admin_ID") & "&result=" & errCount)
        'End Try

        Try
            Dim query As String = "UPDATE dicipline_info 
                               SET 
                                    stf_ID = '" & ddlReporter.SelectedValue & "',
                                    Detail_case = '" & Detail_case.Text & "',
                                    case_ID = '" & ddlCaseType.SelectedValue & "',
                                    Dicipline_Action = '" & Action_box.Text & "',
                                    demerit_mark = '" & demerit_mark.Text & "',
                                    Dicipline_Date = '" & CurrentDate.Text & "' 
                               WHERE disiplin_id = '" & Request.QueryString("dispID") & "'"
            Dim i = oCommon.ExecuteSQL(query)
            If i = "0" Then
                errCount = 0
            Else
                errCount = 12
            End If
        Catch ex As Exception

        Finally
            Response.Redirect("admin_view_disiplin.aspx?admin_ID=" & Request.QueryString("admin_ID") & "&result=" & errCount)
        End Try
    End Sub

    Private Sub BtnBackCase_ServerClick(sender As Object, e As EventArgs) Handles BtnBackCase.ServerClick
        Response.Redirect("admin_view_disiplin.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strkeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_detail_disiplin.aspx?dispID=" + strkeyID + "&v=2" + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            Debug.WriteLine("Error" & ex.Message)
        End Try
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

    End Sub

    Private Sub wlSaveBtn_ServerClick(sender As Object, e As EventArgs) Handles wlSaveBtn.ServerClick

    End Sub

    Private Sub wlCancelBtn_ServerClick(sender As Object, e As EventArgs) Handles wlCancelBtn.ServerClick
        Response.Redirect("admin_detail_disiplin.aspx?stdID=" + tempSTDID + "&v=1" + "&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub
End Class