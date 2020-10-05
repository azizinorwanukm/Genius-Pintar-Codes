Imports System.Data.SqlClient
Imports Microsoft.Win32
Imports System.Globalization

Public Class alumni_student_workStatusEdit
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
            checkSQL()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub checkSQL()
        strSQL = "select * from alumni_workStatus where std_id ='" & Request.QueryString("std_ID") & ""
        Try
            Dim z = oCommon.isExist(strSQL)
            If z = 0 Then
                strSQL = "select * from alumni_workStatus where std_id ='" & Request.QueryString("std_ID") & ""
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
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("employerName")) Then
                        txt_empName.Text = ds.Tables(0).Rows(0).Item("employerName")
                    Else
                        txt_empName.Text = 0
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("position")) Then
                        txt_position.Text = ds.Tables(0).Rows(0).Item("position")
                    Else
                        txt_position.Text = 0
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("dateStart")) Then
                        txt_dateFrom.Text = ds.Tables(0).Rows(0).Item("dateStart")
                    Else
                        txt_dateFrom.Text = 0
                    End If
                    If Not IsDBNull(ds.Tables(0).Rows(0).Item("dateEnd")) Then
                        txt_dateTo.Text = ds.Tables(0).Rows(0).Item("dateEnd")
                    Else
                        txt_dateTo.Text = 0
                    End If


                End If
            End If


        Catch e As Exception

        End Try

    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        strSQL = "select * from alumni_workStatus where std_id ='" & Request.QueryString("std_ID") & ""
        Try
            Dim z = oCommon.isExist(strSQL)
            If z = 0 Then

                strSQL = "UPDATE alumni_workStatus SET employerName = '" & txt_empName.Text & "',position = '" & txt_position.Text & "',
                          dateStart = '" & txt_dateFrom.Text & "',dateEnd = '" & txt_dateTo.Text & "' WHERE std_idn = '" & Request.QueryString("std_ID") & "'"
            ElseIf z = 1 Then
                strSQL = "INSERT INTO alumni_workStatus (std_id,employerName,position,dateStart,dateEnd)
                            VALUES 
                            ('" + Request.QueryString("std_ID") + "','" + txt_empName.Text + "','" + txt_position.Text + "','" + txt_dateFrom.Text + "',
                                '" + txt_dateTo.Text + "')"
            End If
            oCommon.ExecuteSQL(strSQL)

        Catch ez As Exception

        End Try
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("alumni_History_List.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub
End Class