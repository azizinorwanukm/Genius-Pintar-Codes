Imports System.Data.SqlClient

Public Class counselor_Detal_Case
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim staffSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    staff_list()
                    case_list()
                    counselor_status_list()
                    load_page()

                    capture_data_list()


                    CurrentDate.Enabled = False
                    ddlDiciplinetype.Enabled = False
                    ddlReporter.Enabled = False
                    Detail_case.Enabled = False
                    Action_box.Enabled = False
                    demerit_mark.Enabled = False
                    CounselorDate.Enabled = False


                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "select staff_ID from staff_info 
                  left join dicipline_info on staff_info.stf_ID = dicipline_info.stf_ID
                  left join counseling_info on dicipline_info.disiplin_id = counseling_info.disiplin_id
                  where klsr_id = '" & Request.QueryString("klsr_id") & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_ID")) Then
                ddlReporter.SelectedValue = ds.Tables(0).Rows(0).Item("staff_ID")
            Else
                ddlReporter.SelectedValue = ""
            End If
        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        strSQL = "select case_Name from case_info
                  left join  dicipline_info on case_info.case_ID = dicipline_info.case_ID
                  left join counseling_info on dicipline_info.disiplin_id = counseling_info.disiplin_id
                  where counseling_info.klsr_id = '" & Request.QueryString("klsr_id") & "'"

        Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

        Dim dt As DataSet = New DataSet
        sqlDB.Fill(dt, "AnyTable")

        Dim nRowt As Integer = 0
        Dim nCounts As Integer = 1
        Dim YourTable As DataTable = New DataTable
        YourTable = dt.Tables(0)
        If YourTable.Rows.Count > 0 Then
            If Not IsDBNull(dt.Tables(0).Rows(0).Item("case_Name")) Then
                ddlDiciplinetype.SelectedValue = dt.Tables(0).Rows(0).Item("case_Name")
            Else
                ddlDiciplinetype.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub counselor_status_list()
        strSQL = "SELECT Parameter , Value FROM setting WHERE Type='Counselor'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlcounselorstatus.DataSource = ds
            ddlcounselorstatus.DataTextField = "Value"
            ddlcounselorstatus.DataValueField = "Parameter"
            ddlcounselorstatus.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staff_list()
        strSQL = "Select staff_Name, stf_ID from staff_Info where staff_Status = 'Access' order by staff_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlReporter.DataSource = ds
            ddlReporter.DataTextField = "staff_Name"
            ddlReporter.DataValueField = "stf_ID"
            ddlReporter.DataBind()
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub case_list()
        strSQL = "SELECT case_Name FROM case_info "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlDiciplinetype.DataSource = ds
            ddlDiciplinetype.DataTextField = "case_Name"
            ddlDiciplinetype.DataValueField = "case_Name"
            ddlDiciplinetype.DataBind()
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub capture_data_list()
        ''student_info
        strSQL = "select student_info.student_Name, student_info.student_ID, student_info.student_Mykad, class_info.class_Name, dicipline_info.Dicipline_Date, 
	                   case_info.case_Name, staff_info.stf_ID, dicipline_info.Detail_Case, dicipline_info.meritdemerit_point,
	                   counseling_info.kslr_date, counseling_info.kslr_status, counseling_info.kslr_session
                 from counseling_info
                 left join dicipline_info on counseling_info.disiplin_id = dicipline_info.disiplin_id
                 left join student_info on counseling_info.std_ID = student_info.std_ID
                 left join class_info on dicipline_info.class_ID = class_info.class_ID
                 left join case_info on dicipline_info.case_ID = case_info.case_ID
                 left join staff_info on dicipline_info.stf_ID = staff_info.stf_ID
                 where counseling_info.klsr_id = '" & Request.QueryString("klsr_id") & "'"

        '--debug
        ''Response.Write(strSQLstd)

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                StudentNameLbl.Text = ds.Tables(0).Rows(0).Item("student_Name")
            Else
                StudentNameLbl.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_ID")) Then
                StudentIdLbl.Text = ds.Tables(0).Rows(0).Item("student_ID")
            Else
                StudentIdLbl.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Mykad")) Then
                StudentMyKadLbl.Text = ds.Tables(0).Rows(0).Item("student_Mykad")
            Else
                StudentMyKadLbl.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Name")) Then
                StudentClassLbl.Text = ds.Tables(0).Rows(0).Item("class_Name")
            Else
                StudentClassLbl.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dicipline_Date")) Then
                CurrentDate.Text = ds.Tables(0).Rows(0).Item("Dicipline_Date")
            Else
                CurrentDate.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("case_Name")) Then
                ddlDiciplinetype.SelectedValue = ds.Tables(0).Rows(0).Item("case_Name")
            Else
                ddlDiciplinetype.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("stf_ID")) Then
                ddlReporter.SelectedValue = ds.Tables(0).Rows(0).Item("stf_ID")
            Else
                ddlReporter.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Detail_Case")) Then
                Detail_case.Text = ds.Tables(0).Rows(0).Item("Detail_Case")
            Else
                Detail_case.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("meritdemerit_point")) Then
                demerit_mark.Text = ds.Tables(0).Rows(0).Item("meritdemerit_point")
            Else
                demerit_mark.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("kslr_date")) Then
                CounselorDate.Text = ds.Tables(0).Rows(0).Item("kslr_date")
            Else
                CounselorDate.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("kslr_status")) Then
                ddlcounselorstatus.SelectedValue = ds.Tables(0).Rows(0).Item("kslr_status")
            Else
                ddlcounselorstatus.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("kslr_session")) Then
                txtCounselorSession.Text = ds.Tables(0).Rows(0).Item("kslr_session")
            Else
                txtCounselorSession.Text = ""
            End If

        End If
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        strSQL = "UPDATE counseling_info SET kslr_status ='" & ddlcounselorstatus.SelectedValue & "', kslr_session = '" & txtCounselorSession.Text & "' WHERE klsr_id ='" & Request.QueryString("klsr_id") & "' "
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage(" Update Case Status", MessageType.Success)
        Else
            ShowMessage(" Update Case Status", MessageType.Error)
        End If

        capture_data_list()
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_carian_pelajar_kaunselor.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class