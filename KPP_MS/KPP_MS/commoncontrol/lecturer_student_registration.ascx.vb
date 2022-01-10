Imports System.Data.SqlClient
Imports System.IO

Public Class lecturer_student_registration
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

                Dim getStatus As String = Request.QueryString("status")

                If getStatus = "SR" Then ''student registration
                    txtbreadcrum1.Text = "Student Registration"
                    StudentRegistration.Visible = True
                    HostelRegistration.Visible = False

                    btnStudentRegistration.Attributes("class") = "btn btn-info"
                    btnHostelRegistration.Attributes("class") = "btn btn-default font"

                    State_list()
                    'StaffLoadPage()

                ElseIf getStatus = "HR" Then ''hostel registration
                    txtbreadcrum1.Text = "Hostel Registration"
                    StudentRegistration.Visible = False
                    HostelRegistration.Visible = True

                    btnStudentRegistration.Attributes("class") = "btn btn-default font"
                    btnHostelRegistration.Attributes("class") = "btn btn-info"

                    'course_year_list()
                    'course_sem_list()

                    'strRet = BindData(datRespondent)
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub btnStaffInfo_ServerClick(sender As Object, e As EventArgs) Handles btnStudentRegistration.ServerClick
        Response.Redirect("penagajr_kemasukan_pelajar.aspx?stf_ID=" + Request.QueryString("stf_ID") + "&status=SR")
    End Sub

    Private Sub btnCourseInfo_ServerClick(sender As Object, e As EventArgs) Handles btnHostelRegistration.ServerClick
        Response.Redirect("penagajr_kemasukan_pelajar.aspx?stf_ID=" + Request.QueryString("stf_ID") + "&status=HR")
    End Sub

    Private Sub State_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='State' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlState.DataSource = ds
            ddlState.DataTextField = "Parameter"
            ddlState.DataValueField = "Parameter"
            ddlState.DataBind()
            ddlState.Items.Insert(0, New ListItem("Select State", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

End Class