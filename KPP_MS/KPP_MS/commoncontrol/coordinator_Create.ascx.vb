Imports System.Data.SqlClient

Public Class coordinator_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oCommon As New Commonfunction
    Dim result As Integer = 0
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                ddlsubject.Enabled = False

                yearList()
                courseList()
                studentlevel()

                load_page()
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT Parameter from setting where Value ='" & Now.Year & "' and Type = 'Year'"

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
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
                staffList()
            Else
                ddlyear.SelectedValue = ""
            End If
        End If

    End Sub

    Private Sub yearList()
        strSQL = "SELECT Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataBind()

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub studentlevel()
        strSQL = "SELECT Parameter from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddllevel.DataSource = ds
            ddllevel.DataTextField = "Parameter"
            ddllevel.DataValueField = "Parameter"
            ddllevel.DataBind()
            ddllevel.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddllevel.Items.Insert(1, New ListItem("ALL", "ALL"))
            ddllevel.Items.Insert(2, New ListItem("Foundation (1-3)", "F1"))
            ddllevel.Items.Insert(3, New ListItem("Level (1-2)", "L1"))
            ddllevel.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staffList()
        strSQL = "Select staff_Name, stf_ID from staff_Info where staff_Status = 'Access' order by staff_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstaff.DataSource = ds
            ddlstaff.DataTextField = "staff_Name"
            ddlstaff.DataValueField = "stf_ID"
            ddlstaff.DataBind()
            ddlstaff.Items.Insert(0, New ListItem("Select Staff", String.Empty))
            ddlstaff.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub courseList()
        strSQL = "SELECT distinct course_Name from subject_info where subject_year = '" & ddlyear.SelectedValue & "' order by course_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlcourse.DataSource = ds
            ddlcourse.DataTextField = "course_Name"
            ddlcourse.DataValueField = "course_Name"
            ddlcourse.DataBind()
            ddlcourse.Items.Insert(0, New ListItem("Select Course", String.Empty))
            ddlcourse.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub subjectList()
        strSQL = "SELECT distinct subject_Name from subject_info where course_Name = '" & ddlcourse.SelectedValue & "' and subject_year = '" & ddlyear.SelectedValue & "' order by subject_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlsubject.DataSource = ds
            ddlsubject.DataTextField = "subject_Name"
            ddlsubject.DataValueField = "subject_Name"
            ddlsubject.DataBind()
            ddlsubject.Items.Insert(0, New ListItem("Select Subject", String.Empty))
            ddlsubject.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlcourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcourse.SelectedIndexChanged

        If ddlcourse.SelectedValue = "Bahasa Antarabangsa" Or ddlcourse.SelectedValue = "AP Courses" Then
            ddlsubject.Enabled = True
            subjectList()

        Else
            strRet = BindData(datRespondent)
            ddlsubject.Enabled = False
        End If

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
        Dim strOrderby As String = " ORDER BY staff_Info.staff_Name ASC"

        tmpSQL = "select coordinator_ID, staff_Name, coordinator_Level, subject_Name, year from coordinator left join staff_Info on coordinator.stf_ID = staff_Info.stf_ID"
        strWhere = " where year = '" & ddlyear.SelectedValue & "'"

        If ddlstaff.SelectedValue <> "" Then
            strWhere += " and coordinator.stf_ID = '" & ddlstaff.SelectedValue & "'"
        End If

        If ddlcourse.SelectedValue <> "" Then
            strWhere += " and course_Name = '" & ddlcourse.SelectedValue & "'"
        End If

        If ddlsubject.SelectedValue <> "" Then
            strWhere += " and subject_Name = '" & ddlsubject.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        staffList()
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0

        If ddlyear.SelectedIndex > 0 Then

            If ddlstaff.SelectedValue > 0 And ddlstaff.SelectedValue <> "" Then

                If ddllevel.SelectedIndex > 0 And ddllevel.SelectedValue <> "" Then

                    If ddlcourse.SelectedValue <> "" And ddlcourse.SelectedValue <> "Bahasa Antarabangsa" And ddlcourse.SelectedValue <> "AP Courses" Then

                        If ddllevel.SelectedValue = "ALL" Then

                            ''insert Foundation
                            For value As Integer = 1 To 3
                                Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level) 
                                                                 values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlyear.SelectedValue & "','Foundation " & value & "')", objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        errorCount = 0
                                    Else
                                        errorCount = 1
                                    End If
                                End Using
                            Next

                            ''insert Level
                            For value As Integer = 1 To 2
                                Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level) 
                                                                 values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlyear.SelectedValue & "','Level " & value & "')", objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        errorCount = 0
                                    Else
                                        errorCount = 1
                                    End If
                                End Using
                            Next

                        ElseIf ddllevel.SelectedValue = "F1" Then

                            ''insert Foundation
                            For value As Integer = 1 To 3
                                Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlyear.SelectedValue & "','Foundation " & value & "')", objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        errorCount = 0
                                    Else
                                        errorCount = 1
                                    End If
                                End Using
                            Next

                        ElseIf ddllevel.SelectedValue = "L1" Then

                            ''insert Level
                            For value As Integer = 1 To 2
                                Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlyear.SelectedValue & "','Level " & value & "')", objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        errorCount = 0
                                    Else
                                        errorCount = 1
                                    End If
                                End Using
                            Next

                        Else

                            Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & ddlyear.SelectedValue & "','" & ddllevel.SelectedValue & "')", objConn)
                                objConn.Open()
                                Dim i = STDDATA.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    errorCount = 0
                                Else
                                    errorCount = 1
                                End If
                            End Using

                        End If

                    ElseIf ddlcourse.SelectedValue <> "" And (ddlcourse.SelectedValue = "Bahasa Antarabangsa" Or ddlcourse.SelectedValue = "AP Courses") Then

                        If ddlsubject.SelectedValue <> "" Then

                            Dim get_subject_name As String = "select distinct subject_NameBM from subject_info where subject_Name = '" & ddlsubject.SelectedValue & "' and subject_year = '" & ddlyear.SelectedValue & "' "
                            Dim data_subject_name As String = oCommon.getFieldValue(get_subject_name)

                            If ddllevel.SelectedValue = "ALL" Then

                                ''insert Foundation
                                For value As Integer = 1 To 3
                                    Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & data_subject_name & "','" & ddlyear.SelectedValue & "','Level " & value & "')", objConn)
                                        objConn.Open()
                                        Dim i = STDDATA.ExecuteNonQuery()
                                        objConn.Close()

                                        If i <> 0 Then
                                            errorCount = 0
                                        Else
                                            errorCount = 1
                                        End If
                                    End Using
                                Next

                                ''insert Level
                                For value As Integer = 1 To 2
                                    Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & data_subject_name & "','" & ddlyear.SelectedValue & "','Level " & value & "')", objConn)
                                        objConn.Open()
                                        Dim i = STDDATA.ExecuteNonQuery()
                                        objConn.Close()

                                        If i <> 0 Then
                                            errorCount = 0
                                        Else
                                            errorCount = 1
                                        End If
                                    End Using
                                Next

                            ElseIf ddllevel.SelectedValue = "F1" Then

                                ''insert Foundation
                                For value As Integer = 1 To 3
                                    Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & data_subject_name & "','" & ddlyear.SelectedValue & "','Level " & value & "')", objConn)
                                        objConn.Open()
                                        Dim i = STDDATA.ExecuteNonQuery()
                                        objConn.Close()

                                        If i <> 0 Then
                                            errorCount = 0
                                        Else
                                            errorCount = 1
                                        End If
                                    End Using
                                Next

                            ElseIf ddllevel.SelectedValue = "L1" Then

                                ''insert Level
                                For value As Integer = 1 To 2
                                    Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & data_subject_name & "','" & ddlyear.SelectedValue & "','Level " & value & "')", objConn)
                                        objConn.Open()
                                        Dim i = STDDATA.ExecuteNonQuery()
                                        objConn.Close()

                                        If i <> 0 Then
                                            errorCount = 0
                                        Else
                                            errorCount = 1
                                        End If
                                    End Using
                                Next

                            Else

                                Using STDDATA As New SqlCommand("INSERT INTO coordinator(stf_ID, course_Name, subject_Name, year, coordinator_Level) values ('" & ddlstaff.SelectedValue & "','" & ddlcourse.SelectedValue & "','" & data_subject_name & "','" & ddlyear.SelectedValue & "','" & ddllevel.SelectedValue & "')", objConn)
                                    objConn.Open()
                                    Dim i = STDDATA.ExecuteNonQuery()
                                    objConn.Close()

                                    If i <> 0 Then
                                        errorCount = 0
                                    Else
                                        errorCount = 1
                                    End If
                                End Using

                            End If

                        Else
                            '' error subject (please select subject)
                            errorCount = 5
                        End If
                    Else
                        '' error course (please select course)
                        errorCount = 4
                    End If

                Else
                    '' error level (please select level)
                    errorCount = 6
                End If
            Else
                '' error staff (please select staff)
                errorCount = 3
            End If
        Else
            '' error year (please select year)
            errorCount = 2
        End If

        If errorCount = 1 Then
            Response.Redirect("admin_daftar_koordinator.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 0 Then
            Response.Redirect("admin_daftar_koordinator.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_daftar_koordinator.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 3 Then
            Response.Redirect("admin_daftar_koordinator.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 4 Then
            Response.Redirect("admin_daftar_koordinator.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 5 Then
            Response.Redirect("admin_daftar_koordinator.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 6 Then
            Response.Redirect("admin_daftar_koordinator.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID"))
        Else

        End If
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?&admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Protected Sub ddlstaff_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlstaff.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub ddlsubject_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsubject.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddllevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddllevel.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub
End Class