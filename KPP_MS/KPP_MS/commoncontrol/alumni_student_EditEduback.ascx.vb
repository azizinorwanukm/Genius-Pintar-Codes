Imports System.Data.SqlClient
Imports Microsoft.Win32
Imports System.Globalization

Public Class alumni_student_EditEduback
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0
    Dim Data_Print As String = ""
    Dim Access As String
    '' connection to kolejadmin databasse
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try


            fill_ddlscholarship()
            fill_ddlqualification()
            fill_ddlcountry()
            checkSQL()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub checkSQL()
        strSQL = "select * from alumni_educationBackground where std_id ='" & Request.QueryString("std_ID") & ""
        Try
            Dim z = oCommon.isExist(strSQL)
            If z = 0 Then
                strSQL = "select * from alumni_educationBackground where std_id ='" & Request.QueryString("std_ID") & ""
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
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("qualification")) Then
                        ddl_qualificationType.SelectedValue = ds.Tables(0).Rows(0).Item("qualification")
                    Else
                        ddl_qualificationType.SelectedIndex = 0
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("institute_university")) Then
                        txt_insUniName.Text = ds.Tables(0).Rows(0).Item("institute_university")
                    Else
                        txt_insUniName.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("location")) Then
                        txt_location.Text = ds.Tables(0).Rows(0).Item("location")
                    Else
                        txt_location.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("country")) Then
                        ddl_Country.SelectedValue = ds.Tables(0).Rows(0).Item("country")
                    Else
                        ddl_Country.SelectedIndex = 0
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("course")) Then
                        txt_course.Text = ds.Tables(0).Rows(0).Item("course")
                    Else
                        txt_course.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("field")) Then
                        txt_field.Text = ds.Tables(0).Rows(0).Item("field")
                    Else
                        txt_field.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("year_Grad")) Then
                        txt_yearGrad.Text = ds.Tables(0).Rows(0).Item("year_Grad")
                    Else
                        txt_yearGrad.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("age_Grad")) Then
                        txt_ageGrad.Text = ds.Tables(0).Rows(0).Item("age_Grad")
                    Else
                        txt_ageGrad.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("result")) Then
                        txt_result.Text = ds.Tables(0).Rows(0).Item("result")
                    Else
                        txt_result.Text = ""
                    End If

                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("scholarship")) Then
                        ddl_scholarship.SelectedValue = ds.Tables(0).Rows(0).Item("scholarship")
                    Else
                        ddl_scholarship.SelectedIndex = 0
                    End If

                End If
            End If
        Catch e As Exception

        End Try

    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        strSQL = "select * from alumni_educationBackground where std_id ='" & Request.QueryString("std_ID") & ""
        Try
            Dim z = oCommon.isExist(strSQL)
            If z = 0 Then

                strSQL = "UPDATE alumni_educationBackground SET qualification = '" & ddl_qualificationType.SelectedValue & "',institute_university= '" & txt_insUniName.Text & "',
                          location= '" & txt_location.Text & "',country= '" & ddl_Country.SelectedValue & "',course= '" & txt_course.Text & "',field= '" & txt_field.Text & "'
                          ,year_Grad= '" & txt_yearGrad.Text & "',age_Grad= '" & txt_ageGrad.Text & "',result= '" & txt_result.Text & "' ,
                          scholarship= '" & ddl_qualificationType.SelectedValue & "' WHERE std_idn = '" & Request.QueryString("std_ID") & "'"
            ElseIf z = 1 Then
                strSQL = "INSERT INTO alumni_educationBackground (std_id,qualification,institute_university,location,country,course,field,year_Grad,age_Grad,result,scholarship)
                            VALUES 
                            ('" + Request.QueryString("std_ID") + "','" + ddl_qualificationType.SelectedValue + "','" + txt_insUniName.Text + "','" + txt_location.Text + "',
                                '" + ddl_Country.SelectedValue + "','" + txt_course.Text + "','" + txt_field.Text + "','" + txt_yearGrad.Text + "','" + txt_ageGrad.Text + "','" + txt_result.Text + "'
                            ,'" + ddl_qualificationType.SelectedValue + "')"
            End If

            oCommon.ExecuteSQL(strSQL)

        Catch ez As Exception

        End Try

    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_Student_EduBack.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub fill_ddlcountry()
        Dim Allcountries As RegistryKey = Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Telephony\Country List")
        If Allcountries IsNot Nothing Then
            Try

                ddl_Country.Items.Clear()
                Dim AllCountriesSubkeys As String() = Allcountries.GetSubKeyNames()
                For Each CountryCode As String In AllCountriesSubkeys

                    Dim country As RegistryKey = Allcountries.OpenSubKey(CountryCode)
                    Dim countryName As String = country.GetValue("Name").ToString()
                    ddl_Country.Items.Add(countryName)
                    country.Close()
                Next
                Allcountries.Close()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub fill_ddlqualification()
        strSQL = "select Parameter, Value from setting where Type = 'Qualification' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_qualificationType.DataSource = ds
            ddl_qualificationType.DataTextField = "Parameter"
            ddl_qualificationType.DataValueField = "Value"
            ddl_qualificationType.DataBind()
            ddl_qualificationType.Items.Insert(0, New ListItem("- None -", "- None -"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub fill_ddlscholarship()
        strSQL = "select * from scholarship A
                    where A.scholarship_status = 'Active'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_scholarship.DataSource = ds
            ddl_scholarship.DataTextField = "scholarship_name"
            ddl_scholarship.DataValueField = "scholarship_id"
            ddl_scholarship.DataBind()
            ddl_scholarship.Items.Insert(0, New ListItem("- None -", "- None -"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub
End Class
