Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Net.Mail

Public Class Disiplin_Case_Detail
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim dispID As String
    Dim errCount As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            dispID = Request.QueryString("dispID")
            StaffDDL()
            CaseDDL()
            getCaseDetail()
        End If
    End Sub

    Protected Sub StaffDDL()
        Dim staffSQL = "SELECT staff_Name, stf_ID FROM staff_info WHERE staff_Status = 'Access'"
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

    Protected Sub getCaseDetail()
        Dim caseQuery As String = " SELECT 
	                                student_info.student_Name, student_info.student_ID,
	                                student_info.student_Mykad, class_info.class_Name,
	                                dicipline_info.Dicipline_Date, dicipline_info.case_ID,
	                                dicipline_info.Detail_Case, warning_letters_table.title,
	                                dicipline_info.disiplin_id, dicipline_info.stf_id, 
                                    counseling_info.kslr_status, counseling_info.kslr_session,
                                    counseling_info.kslr_date
                                    FROM dicipline_info
	                                LEFT JOIN student_info ON student_info.std_ID = dicipline_info.std_ID
	                                LEFT JOIN class_info ON class_info.class_ID = dicipline_info.class_ID
	                                LEFT JOIN counseling_info  ON counseling_info.disiplin_id = dicipline_info.disiplin_id
                                    LEFT JOIN staff_info on dicipline_info.stf_ID = staff_info.stf_ID
                                    LEFT JOIN warning_letters_table ON dicipline_info.warning_id = warning_letters_table.id
                                    WHERE dicipline_info.disiplin_id = '" & dispID & "'"
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

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("title")) Then
                Action_box.Text = ds.Tables(0).Rows(0).Item("title")
            Else
                Action_box.Text = " "
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dicipline_Date")) Then
                CurrentDate.Text = ds.Tables(0).Rows(0).Item("Dicipline_Date")
            Else
                CurrentDate.Text = " "
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("kslr_date")) Then
                CounselingDate.Text = ds.Tables(0).Rows(0).Item("kslr_date")
            Else
                CounselingDate.Text = " "
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnSimpanCase_ServerClick(sender As Object, e As EventArgs) Handles BtnSimpanCase.ServerClick

        Try
            If ddlReporter.SelectedIndex > 0 Then
                If Detail_case.Text.Length > 0 Then
                    If ddlCaseType.SelectedIndex > 0 Then
                        If Action_box.Text.Length > 0 Then

                            If CurrentDate.Text.Length > 0 And IsDate(CurrentDate.Text) Then
                                If Request.QueryString("dispID").Length > 0 And Regex.IsMatch(Request.QueryString("dispID"), "^[0-9 ]+$") Then

                                    Dim get_point As String = "select case_MeritDemerit_Point from case_info where case_ID = '" & ddlCaseType.SelectedValue & "'"
                                    Dim data_point As String = oCommon.getFieldValue(get_point)

                                    Dim query As String = "UPDATE dicipline_info 
                                                               SET 
                                                                    stf_ID = '" & ddlReporter.SelectedValue & "',
                                                                    Detail_case = '" & Detail_case.Text & "',
                                                                    case_ID = '" & ddlCaseType.SelectedValue & "',
                                                                    Dicipline_Action = '" & Action_box.Text & "',
                                                                    Dicipline_Date = '" & CurrentDate.Text & "',
                                                                    meritdemerit_point = '" & data_point & "'
                                                               WHERE disiplin_id = '" & Request.QueryString("dispID") & "'"
                                    Dim i = oCommon.ExecuteSQL(query)

                                    query = ""

                                    query = "update counseling_info set klsr_date = '" & CounselingDate.Text & "' where disiplin_id = '" & Request.QueryString("dispID") & "' "
                                    i = oCommon.ExecuteSQL(query)

                                    If i = "0" Then
                                        errCount = 10
                                    Else
                                        errCount = 12
                                    End If
                                Else
                                    errCount = 7
                                End If
                            Else
                                errCount = 6
                            End If

                        Else
                            errCount = 4
                        End If
                    Else
                        errCount = 3
                    End If
                Else
                    errCount = 2
                End If
            Else
                errCount = 1
            End If
        Catch ex As Exception
            errCount = 13
        Finally
            If errCount <> 10 Then
                Response.Redirect(String.Format("admin_detail_disiplin.aspx?dispID={0}&v=0&admin_ID={1}&result={2}", Request.QueryString("dispID"), Request.QueryString("admin_ID"), errCount))
            Else
                Response.Redirect(String.Format("admin_view_disiplin.aspx?admin_ID={0}&result={1}", Request.QueryString("admin_ID"), errCount))
            End If
        End Try
    End Sub

    Private Sub BtnBackCase_ServerClick(sender As Object, e As EventArgs) Handles BtnBackCase.ServerClick
        Response.Redirect("admin_view_disiplin.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub
End Class