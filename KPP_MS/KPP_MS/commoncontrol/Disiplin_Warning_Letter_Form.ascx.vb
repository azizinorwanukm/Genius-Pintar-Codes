Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Net.Mail

Public Class Disiplin_Warning_Letter_Form
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim page_view As New Integer
    Dim cmd As SqlCommand
    Dim errCount As Integer
    Dim dispID As String
    Dim stdID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dispID = Request.QueryString("dispID")
        If Not IsPostBack Then
            WarnignLetterDDL()
            GetWarningLetterDetail()
        End If
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

    Protected Sub GetWarningLetterDetail()
        If Not IsNumeric(dispID) Then
            Return
        End If
        Dim query As String = "SELECT 
                                student_info.std_ID, dicipline_info.disiplin_id,
	                            student_info.student_Name, student_info.student_ID,
	                            student_info.student_Mykad, class_info.class_Name,
	                            case_info.case_Name, case_info.case_Category,
	                            dicipline_info.Dicipline_Date,counseling_info.kslr_status,
                                counseling_info.kslr_session, warning_letters_table.letter_content
                            FROM dicipline_info 
	                            LEFT JOIN student_info ON student_info.std_ID = dicipline_info.std_ID 
	                            LEFT JOIN class_info ON class_info.class_ID = dicipline_info.class_ID 
	                            LEFT JOIN case_info ON case_info.case_ID = dicipline_info.case_ID 
	                            LEFT JOIN counseling_info ON counseling_info.disiplin_id = dicipline_info.disiplin_id
                                LEFT JOIN warning_letters_table ON dicipline_info.warning_id = warning_letters_table.id
                            WHERE dicipline_info.disiplin_id ='" & dispID & "'"

        Dim sqlDA As New SqlDataAdapter(query, objConn)

        Try
            Dim ds As New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim getklsrid As String = "select warning_letters_table.id from warning_letters_table 
                                       left join dicipline_info on warning_letters_table.id = dicipline_info.warning_id
                                       where dicipline_info.disiplin_id ='" & dispID & "'"
            Dim klsrid As String = oCommon.getFieldValue(getklsrid)


            stdID = ds.Tables(0).Rows(0).Item("std_ID")
            wlStudentNameLbl.Text = ds.Tables(0).Rows(0).Item("student_Name")
            wlStudentICLbl.Text = ds.Tables(0).Rows(0).Item("student_MyKad")
            wlStudentClassLbl.Text = ds.Tables(0).Rows(0).Item("class_Name")
            wlCaseLbl.Text = ds.Tables(0).Rows(0).Item("case_Name")
            wlCaseDate.Text = ds.Tables(0).Rows(0).Item("Dicipline_Date")
            wlCounselingStatus.Text = ds.Tables(0).Rows(0).Item("kslr_status")
            ddlLetterType.SelectedValue = klsrid

            If String.Compare(ds.Tables(0).Rows(0).Item("kslr_status"), "Uncompleted") = 0 Then
                wlContentDiv.Visible = False
                wlNoteDiv.Visible = True
            Else
                wlContentDiv.Visible = True
                wlNoteDiv.Visible = False
            End If

            If IsDBNull(ds.Tables(0).Rows(0).Item("kslr_session")) Then
                wlCounselingSession.Text = ""
            Else
                wlCounselingSession.Text = ds.Tables(0).Rows(0).Item("kslr_session")
            End If

            ''find name to replace
            Dim get_Name As String = "select student_Name from student_info
                                      left join dicipline_info on student_info.std_ID = dicipline_info.std_ID
                                      where disiplin_id = '" & dispID & "'"
            Dim find_Name As String = oCommon.getFieldValue(get_Name)

            ''find mykad to replace
            Dim get_Mykad As String = "select student_Mykad from student_info
                                      left join dicipline_info on student_info.std_ID = dicipline_info.std_ID
                                      where disiplin_id = '" & dispID & "'"
            Dim find_Mykad As String = oCommon.getFieldValue(get_Mykad)

            ''find id to replace
            Dim get_ID As String = "select student_ID from student_info
                                      left join dicipline_info on student_info.std_ID = dicipline_info.std_ID
                                      where disiplin_id = '" & dispID & "'"
            Dim find_ID As String = oCommon.getFieldValue(get_ID)

            ''find class to replace
            Dim get_class As String = "select class_info.class_Name from class_info
                                      left join dicipline_info on class_info.class_ID = dicipline_info.class_ID
                                      where disiplin_id = '" & dispID & "'"
            Dim find_class As String = oCommon.getFieldValue(get_class)

            Dim content As String = Server.HtmlDecode(ds.Tables(0).Rows(0).Item("letter_content").ToString)

            content = content.Replace("{NAME}", find_Name)
            content = content.Replace("{IC}", find_Mykad)
            content = content.Replace("{ID}", find_ID)
            content = content.Replace("{CLASS}", find_class)

            letterContent.Content = content

        Catch ex As Exception

        End Try
    End Sub

    Private Sub wlCancelBtn_ServerClick(sender As Object, e As EventArgs) Handles wlCancelBtn.ServerClick

        Dim query As String = "SELECT std_ID FROM dicipline_info WHERE disiplin_id='" & Request.QueryString("dispID") & "'"
        Dim stdID = oCommon.getFieldValue(query)
        If stdID = "" Then

        Else
            Response.Redirect("admin_detail_disiplin.aspx?v=1&stdID=" & stdID & "&admin_ID=" & Request.QueryString("admin_ID"))
        End If
    End Sub

    Private Sub ddlLetterType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLetterType.SelectedIndexChanged
        Dim query As String = "SELECT letter_content FROM warning_letters_table WHERE id='" & ddlLetterType.SelectedValue & "'"
        Dim letter = oCommon.getFieldValue(query)
        If letter.Equals("") Then
            Return
        Else
            letterContent.Content = Server.HtmlDecode(letter)
        End If
    End Sub
End Class