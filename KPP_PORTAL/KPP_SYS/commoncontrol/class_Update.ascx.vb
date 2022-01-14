Imports System.Data.SqlClient

Public Class class_Update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    Dim idClass As String = Request.QueryString("class_ID")

                    class_info_search()
                    staff_info_search()
                    class_info_Load(idClass)
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub class_info_Load(ByVal strclass_ID As String)
        strSQL = "SELECT * FROM class_info left join staff_Info on class_info.stf_ID = staff_Info.stf_ID WHERE class_ID ='" & strclass_ID & "'"
        '--debug
        ''Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Name")) Then
                    class_Name.Text = ds.Tables(0).Rows(0).Item("class_Name")
                Else
                    class_Name.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_year")) Then
                    class_year.Text = ds.Tables(0).Rows(0).Item("class_year")
                Else
                    class_year.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Level")) Then
                    ddlclass_Level.SelectedValue = ds.Tables(0).Rows(0).Item("class_Level")
                Else
                    ddlclass_Level.Items.FindByValue("").Selected = True
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_ID")) Then
                    ddlstaff_ID.SelectedValue = ds.Tables(0).Rows(0).Item("staff_ID")
                Else
                    ddlstaff_ID.Items.FindByValue("").Selected = True
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("std_number")) Then
                    txtstd_number.Text = ds.Tables(0).Rows(0).Item("std_number")
                Else
                    txtstd_number.Text = ""
                End If
            Else
                'Response.Write("Table count < 0")
            End If

        Catch ex As Exception
            ''lblMsg.Text = "System error:" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub BtnSimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        'UPDATE
        strSQL = "UPDATE class_info SET class_Name='" & class_Name.Text & "',class_year='" & class_year.Text & "',class_Level='" & ddlclass_Level.Text & "',stf_ID='" & ddlstaff_ID.Text & "',std_number='" & txtstd_number.Text & "' WHERE class_ID ='" & Request.QueryString("class_ID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            Response.Redirect("admin_pengurusan_am_kelas.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        Else
            Response.Redirect("admin_pengurusan_am_kelas.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        End If
    End Sub

    Private Sub class_info_search()
        Try

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            strSQL = "select distinct class_Level from class_info where class_ID is not null"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)


            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlclass_Level.DataSource = ds
            ddlclass_Level.DataTextField = "class_Level"
            ddlclass_Level.DataValueField = "class_Level"
            ddlclass_Level.DataBind()
            ddlclass_Level.Items.Insert(0, New ListItem("Select Class Level", ""))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staff_info_search()
        Try

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            strSQL = "select * from staff_Info where staff_Year = '" & Now.Year & "'"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)


            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstaff_ID.DataSource = ds
            ddlstaff_ID.DataTextField = "staff_Name"
            ddlstaff_ID.DataValueField = "stf_ID"
            ddlstaff_ID.DataBind()
            ddlstaff_ID.Items.Insert(0, New ListItem("Select Staff Name", ""))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_pengurusan_am_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

End Class