﻿Imports System.Data.SqlClient
Imports System.IO

Public Class guardian2_Detail
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
                parent_city_load()
                parent_state_load()

                Dim access As String = Request.QueryString("std_ID")


                ''parent_info
                strSQL = "SELECT * from parent_Info 
                  Left Join student_info ON parent_Info.parent_ID = student_info.parent_motherID  
                  WHERE student_info.std_ID ='" & access & "' and parent_info.parent_No = '2'"
                '--debug
                ''Response.Write(strSQLprnt)

                Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

                Dim dset As DataSet = New DataSet
                sqlDB.Fill(dset, "AnyTable")

                Dim Rows As Integer = 0
                Dim Count As Integer = 1
                Dim Table As DataTable = New DataTable
                Table = dset.Tables(0)
                If Table.Rows.Count > 0 Then
                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Name")) Then
                        parent_Name.Text = dset.Tables(0).Rows(0).Item("parent_Name")
                    Else
                        parent_Name.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_IC")) Then
                        parent_IC.Text = dset.Tables(0).Rows(0).Item("parent_IC")
                    Else
                        parent_IC.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Email")) Then
                        parent_Email.Text = dset.Tables(0).Rows(0).Item("parent_Email")
                    Else
                        parent_Email.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_MobileNo")) Then
                        parent_MobileNo.Text = dset.Tables(0).Rows(0).Item("parent_MobileNo")
                    Else
                        parent_MobileNo.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_HomeAddress")) Then
                        parent_Address.Text = dset.Tables(0).Rows(0).Item("parent_HomeAddress")
                    Else
                        parent_Address.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Status")) Then
                        Parent_relationship.Text = dset.Tables(0).Rows(0).Item("parent_Status")
                    Else
                        Parent_relationship.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_City")) Then
                        ddlparent_City.SelectedValue = dset.Tables(0).Rows(0).Item("parent_City")
                    Else
                        ddlparent_City.SelectedValue = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_State")) Then
                        ddlparent_State.SelectedValue = dset.Tables(0).Rows(0).Item("parent_State")
                    Else
                        ddlparent_City.SelectedValue = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Postcode")) Then
                        parent_Postcode.Text = dset.Tables(0).Rows(0).Item("parent_Postcode")
                    Else
                        parent_Postcode.Text = ""
                    End If


                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_work")) Then
                        parent_work.Text = dset.Tables(0).Rows(0).Item("parent_work")
                    Else
                        parent_work.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Salary")) Then
                        parent_Salary.Text = dset.Tables(0).Rows(0).Item("parent_Salary")
                    Else
                        parent_Salary.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_OfficeNo")) Then
                        parent_OfficeNo.Text = dset.Tables(0).Rows(0).Item("parent_OfficeNo")
                    Else
                        parent_OfficeNo.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_WorkAddress")) Then
                        parent_WorkAddress.Text = dset.Tables(0).Rows(0).Item("parent_WorkAddress")
                    Else
                        parent_WorkAddress.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Work_Email")) Then
                        parent_Work_Email.Text = dset.Tables(0).Rows(0).Item("parent_Work_Email")
                    Else
                        parent_Work_Email.Text = ""
                    End If

                End If
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

    Private Sub parent_city_load()
        strSQL = "SELECT Parameter FROM setting WHERE Type='City' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlparent_City.DataSource = ds
            ddlparent_City.DataTextField = "Parameter"
            ddlparent_City.DataValueField = "Parameter"
            ddlparent_City.DataBind()
            ddlparent_City.Items.Insert(0, New ListItem("Select City", String.Empty))
            ddlparent_City.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub parent_state_load()
        strSQL = "SELECT Parameter FROM setting WHERE Type='State' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlparent_State.DataSource = ds
            ddlparent_State.DataTextField = "Parameter"
            ddlparent_State.DataValueField = "Parameter"
            ddlparent_State.DataBind()
            ddlparent_State.Items.Insert(0, New ListItem("Select State", String.Empty))
            ddlparent_State.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnGuardianUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnGuardianUpdate.ServerClick

        Dim access As String = Request.QueryString("std_ID")


        Dim errorCount As Integer = 0

        If parent_Name.Text = "" Or Not IsNumeric(parent_Name.Text) And Regex.IsMatch(parent_Name.Text, "^[A-Za-z ]+$") Then

            If parent_IC.Text = "" Or IsNumeric(parent_IC.Text) And parent_IC.Text.Length < 14 Then

                If parent_work.Text = "" Or Not IsNumeric(parent_work.Text) And Regex.IsMatch(parent_work.Text, "^[A-Za-z ]+$") Then

                    If parent_Email.Text = "" Or Regex.IsMatch(parent_Email.Text, "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$") Then

                        If IsNumeric(parent_MobileNo.Text) Or parent_MobileNo.Text = "" Then

                            If ddlparent_City.SelectedValue <> "0" Then

                                If ddlparent_State.SelectedValue <> "0" Then

                                    If parent_Postcode.Text = "" Or IsNumeric(parent_Postcode.Text) Then

                                        'UPDATE PARENT DATA
                                        strSQL = "UPDATE parent_Info set parent_Name='" & parent_Name.Text & "',parent_IC='" & parent_IC.Text & "',parent_State='" & ddlparent_State.SelectedValue & "',
                                                parent_Email='" & parent_Email.Text & "',parent_MobileNo='" & parent_MobileNo.Text & "',
                                                parent_HomeAddress='" & parent_Address.Text & "',parent_City='" & ddlparent_City.SelectedValue & "',parent_Postcode='" & parent_Postcode.Text & "',
                                                parent_Work='" & parent_work.Text & "',parent_Salary='" & parent_Salary.Text & "',parent_WorkAddress='" & parent_WorkAddress.Text & "',
                                                parent_Work_Email='" & parent_Work_Email.Text & "',parent_OfficeNo='" & parent_OfficeNo.Text & "' 
                                                FROM parent_Info 
                                                LEFT JOIN student_info ON parent_Info.parent_ID = student_info.parent_motherID  
                                                WHERE student_info.std_ID ='" & access & "' and parent_info.parent_No = '2'"
                                        strRet = oCommon.ExecuteSQL(strSQL)

                                        If strRet = 0 Then
                                            errorCount = 20
                                        Else
                                            errorCount = 21
                                        End If
                                    Else
                                        errorCount = 29
                                    End If
                                Else
                                    errorCount = 28
                                End If
                            Else
                                errorCount = 27
                            End If
                        Else
                            errorCount = 26
                        End If
                    Else
                        errorCount = 25
                    End If
                Else
                    errorCount = 24
                End If
            Else
                errorCount = 23
            End If
        Else
            errorCount = 22
        End If

        If errorCount = 20 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=20&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 21 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=21&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 22 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=22&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 23 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=23&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 24 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=24&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 25 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=25&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 26 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=26&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 27 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=27&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 28 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=28&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 29 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=29&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        ElseIf errorCount = 30 Then
            Response.Redirect("admin_edit_pelajar_data.aspx?result=30&std_ID=" + Request.QueryString("std_ID") + "&admin_ID=" + Request.QueryString("std_ID"))
        End If
    End Sub
End Class