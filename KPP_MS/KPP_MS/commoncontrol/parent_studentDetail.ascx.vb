Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography

Public Class parent_studentDetail
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
                student_year_list()
                City_list()
                State_list()
                Level_list()
                Sem_list()
                LoadPage()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Function getFieldValue(ByVal sql_plus As String, ByVal MyConnection As String) As String
        If sql_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sql_plus, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function

    Private Sub LoadPage()
        ''student_info
        strSQL = "select * from student_info
                  where std_ID = '" & Request.QueryString("std_ID") & "'"

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
                student_Name.Text = ds.Tables(0).Rows(0).Item("student_Name")
            Else
                student_Name.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Mykad")) Then
                student_Mykad.Text = ds.Tables(0).Rows(0).Item("student_Mykad")
            Else
                student_Mykad.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Email")) Then
                student_Email.Text = ds.Tables(0).Rows(0).Item("student_Email")
            Else
                student_Email.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Sex")) Then
                student_Sex.Text = ds.Tables(0).Rows(0).Item("student_Sex")
            Else
                student_Sex.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_FonNo")) Then
                student_FonNo.Text = ds.Tables(0).Rows(0).Item("student_FonNo")
            Else
                student_FonNo.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Address")) Then
                student_Address.Text = ds.Tables(0).Rows(0).Item("student_Address")
            Else
                student_Address.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_City")) Then
                ddlCity.SelectedValue = ds.Tables(0).Rows(0).Item("student_City")
            Else
                ddlCity.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_State")) Then
                ddlState.SelectedValue = ds.Tables(0).Rows(0).Item("student_State")
            Else
                ddlState.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_PostalCode")) Then
                student_PostCode.Text = ds.Tables(0).Rows(0).Item("student_PostalCode")
            Else
                student_PostCode.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Level")) Then
                ddlLevel.SelectedValue = ds.Tables(0).Rows(0).Item("student_Level")
            Else
                ddlLevel.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Sem")) Then
                ddlSem.SelectedValue = ds.Tables(0).Rows(0).Item("student_Sem")
            Else
                ddlSem.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Year")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("student_Year")
            Else
                ddlYear.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Photo")) Then
                student_Photo.ImageUrl = ds.Tables(0).Rows(0).Item("student_Photo")
            Else
                student_Photo.ImageUrl = "~/student_Image/user.png"
            End If

            '--password encrypted
            Dim strPwd As String = ""
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_ID")) Then
                student_ID.Text = ds.Tables(0).Rows(0).Item("student_ID")
            Else
                student_ID.Text = ""
            End If
        End If
    End Sub

    Private Sub student_year_list()
        strSQL = "SELECT Parameter from setting where Type = 'Year' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub City_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='City' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCity.DataSource = ds
            ddlCity.DataTextField = "Parameter"
            ddlCity.DataValueField = "Parameter"
            ddlCity.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
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

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Level_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel.DataSource = ds
            ddlLevel.DataTextField = "Parameter"
            ddlLevel.DataValueField = "Parameter"
            ddlLevel.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Sem_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSem.DataSource = ds
            ddlSem.DataTextField = "Parameter"
            ddlSem.DataValueField = "Parameter"
            ddlSem.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnStudentUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnStudentUpdate.ServerClick
        Dim errorCount As Integer = 0
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objNewConn As SqlConnection = New SqlConnection(strConn)

        If ddlLevel.SelectedValue <> "0" Then

            If IsNumeric(student_Mykad.Text) And student_Mykad.Text <> "" And student_Mykad.Text.Length < 14 Then

                If student_PostCode.Text = "" Or IsNumeric(student_PostCode.Text) Then

                    If student_Name.Text <> "" And Not IsNothing(student_Name.Text) And Regex.IsMatch(student_Name.Text, "^[A-Za-z ]+$") Then

                        If IsNumeric(student_FonNo.Text) Or student_FonNo.Text = "" Then

                            If ddlCity.SelectedValue <> "0" Then

                                If ddlState.SelectedValue <> "0" Then

                                    If student_Sex.Text <> "" And Regex.IsMatch(student_Sex.Text, "^[A-Za-z]+$") Then

                                        If student_Email.Text = "" Or Regex.IsMatch(student_Email.Text, "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$") Then

                                            If ddlRace.SelectedValue <> "0" Or ddlRace.SelectedValue = "0" Then

                                                If ddlReligion.SelectedValue <> "0" Or ddlReligion.SelectedValue = "0" Then

                                                    If uploadPhoto.PostedFile.FileName <> "" Then

                                                        Dim filename As String = Path.GetFileName(uploadPhoto.PostedFile.FileName)

                                                        ''sets the image path
                                                        Dim imgPath As String = "~/student_Image/" + filename

                                                        ''then save it to the Folder
                                                        uploadPhoto.SaveAs(Server.MapPath(imgPath))

                                                        'UPDATE STUDENT DATA
                                                        strSQL = "UPDATE student_info set student_ID='" & student_ID.Text & "',student_Mykad='" & student_Mykad.Text & "',
                                                          student_Sex='" & student_Sex.Text & "',student_Name='" & student_Name.Text & "',student_Email='" & student_Email.Text & "',
                                                          student_FonNo='" & student_FonNo.Text & "',student_Address='" & student_Address.Text & "',
                                                          student_City='" & ddlCity.SelectedValue & "',student_State='" & ddlState.SelectedValue & "',student_Race='" & ddlRace.SelectedValue & "',student_Religion='" & ddlReligion.SelectedValue & "',
                                                          student_PostCode='" & student_PostCode.Text & "',
                                                          student_Photo='" & imgPath & "' WHERE std_ID ='" & Request.QueryString("std_ID") & "'"
                                                        strRet = oCommon.ExecuteSQL(strSQL)
                                                    Else
                                                        Dim imgPath As String = "~/student_Image/user.png"

                                                        'UPDATE STUDENT DATA
                                                        strSQL = "UPDATE student_info set student_ID='" & student_ID.Text & "',student_Mykad='" & student_Mykad.Text & "',
                                                          student_Sex='" & student_Sex.Text & "',student_Name='" & student_Name.Text & "',
                                                          student_Email='" & student_Email.Text & "',student_FonNo='" & student_FonNo.Text & "',
                                                          student_Address='" & student_Address.Text & "',student_City='" & ddlCity.SelectedValue & "',
                                                          student_State='" & ddlState.SelectedValue & "',student_PostalCode='" & student_PostCode.Text & "',student_Race='" & ddlRace.SelectedValue & "',student_Religion='" & ddlReligion.SelectedValue & "',
                                                          student_Photo='" & imgPath & "' 
                                                          WHERE std_ID ='" & Request.QueryString("std_ID") & "'"
                                                        strRet = oCommon.ExecuteSQL(strSQL)
                                                    End If

                                                    If strRet = "0" Then
                                                        errorCount = 0
                                                    Else
                                                        errorCount = 1
                                                    End If

                                                End If

                                            End If

                                        Else
                                            errorCount = 10
                                        End If
                                    Else
                                        errorCount = 9
                                    End If
                                Else
                                    errorCount = 8
                                End If
                            Else
                                errorCount = 7
                            End If
                        Else
                            errorCount = 6
                        End If
                    Else
                        errorCount = 5
                    End If
                Else
                    errorCount = 4
                End If
            Else
                errorCount = 3
            End If
        Else
            errorCount = 2
        End If

        If errorCount = 1 Then
            Response.Redirect("penjaga_update_profile.aspx?result=-1&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 0 Then
            Response.Redirect("penjaga_update_profile.aspx?result=1&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 2 Then
            Response.Redirect("penjaga_update_profile.aspx?result=2&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 3 Then
            Response.Redirect("penjaga_update_profile.aspx?result=3&sparent_ID=" + Request.QueryString("parent_ID") + "&std_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 4 Then
            Response.Redirect("penjaga_update_profile.aspx?result=4&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID= " + Request.QueryString("std_ID"))
        ElseIf errorCount = 5 Then
            Response.Redirect("penjaga_update_profile.aspx?result=5&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 6 Then
            Response.Redirect("penjaga_update_profile.aspx?result=6&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID= " + Request.QueryString("std_ID"))
        ElseIf errorCount = 7 Then
            Response.Redirect("penjaga_update_profile.aspx?result=7&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 8 Then
            Response.Redirect("penjaga_update_profile.aspx?result=8&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID= " + Request.QueryString("std_ID"))
        ElseIf errorCount = 9 Then
            Response.Redirect("penjaga_update_profile.aspx?result=9&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 10 Then
            Response.Redirect("penjaga_update_profile.aspx?result=10&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID= " + Request.QueryString("std_ID"))
        End If
    End Sub

    Private Sub resetButton_Click(sender As Object, e As EventArgs) Handles resetButton.Click

        strSQL = "UPDATE student_Info SET student_Password = student_Mykad WHERE std_ID = '" & Request.QueryString("std_ID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub
End Class