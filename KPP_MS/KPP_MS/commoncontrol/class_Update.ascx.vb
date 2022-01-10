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

                Dim data As String = oCommon.securityLogin(Request.QueryString("admin_ID"))

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then

                    class_info_search()
                    staff_info_search()
                    year_info_search()
                    course_program_Load()
                    class_info_Load()
                    campus_List()

                    Session("getStatus") = "VC"
                    previousPage.NavigateUrl = String.Format("~/admin_pengurusan_am_kelas.aspx?admin_ID=" + Request.QueryString("admin_ID"))

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub class_info_Load()
        strSQL = "  SELECT * FROM class_info 
                    left join staff_Info on class_info.stf_ID = staff_Info.stf_ID
                    left join setting on class_info.class_Campus = setting.Value
                    WHERE class_ID = '" & Request.QueryString("class_ID") & "'"

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
                    class_Name.text = ds.Tables(0).Rows(0).Item("class_Name")
                Else
                    class_Name.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_year")) Then
                    ddlclass_year.SelectedValue = ds.Tables(0).Rows(0).Item("class_year")
                Else
                    ddlclass_year.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Level")) Then
                    ddlclass_Level.SelectedValue = ds.Tables(0).Rows(0).Item("class_Level")
                Else
                    ddlclass_Level.Items.FindByValue("").Selected = True
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("stf_ID")) Then
                    ddlstaff_ID.SelectedValue = ds.Tables(0).Rows(0).Item("stf_ID")
                Else
                    ddlstaff_ID.Items.FindByValue("").Selected = True
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("std_number")) Then
                    txtstd_number.Text = ds.Tables(0).Rows(0).Item("std_number")
                Else
                    txtstd_number.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("course_Program")) Then
                    ddlStream.SelectedValue = ds.Tables(0).Rows(0).Item("course_Program")
                Else
                    ddlStream.Items.FindByValue("").Selected = True
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Value")) Then
                    ddlCampus.SelectedValue = ds.Tables(0).Rows(0).Item("Value")
                Else
                    ddlCampus.SelectedIndex = 0
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

    Private Sub campus_List()
        strSQL = "Select Parameter, Value from setting where type = 'Pusat Campus'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCampus.DataSource = ds
            ddlCampus.DataTextField = "Parameter"
            ddlCampus.DataValueField = "Value"
            ddlCampus.DataBind()
            ddlCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        'UPDATE
        strSQL = "UPDATE class_info SET class_Name='" & class_Name.Text & "',class_year='" & ddlclass_year.SelectedValue & "',class_Level='" & ddlclass_Level.Text & "',stf_ID='" & ddlstaff_ID.Text & "',
                  std_number='" & txtstd_number.Text & "' WHERE class_ID ='" & Request.QueryString("class_ID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage("Successfull update class", MessageType.Success)
        Else
            ShowMessage("Unsuccessfull update class", MessageType.Error)
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
            ddlclass_Level.Items.Insert(0, New ListItem("Select Class Level", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub staff_info_search()
        Try

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            If ddlCampus.SelectedValue = "APP" Then
                strSQL = "select * from staff_Info where staff_Status = 'Access' and staff_Campus = 'APP' order by staff_Name ASC"
            Else
                strSQL = "select * from staff_Info where staff_Status = 'Access' order by staff_Name ASC"
            End If

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)


            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstaff_ID.DataSource = ds
            ddlstaff_ID.DataTextField = "staff_Name"
            ddlstaff_ID.DataValueField = "stf_ID"
            ddlstaff_ID.DataBind()
            ddlstaff_ID.Items.Insert(0, New ListItem("Select Staff Name", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_info_search()
        Try

            strSQL = "select distinct class_year from class_info order by class_year asc"
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlclass_year.DataSource = ds
            ddlclass_year.DataTextField = "class_year"
            ddlclass_year.DataValueField = "class_year"
            ddlclass_year.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub course_program_Load()
        Try

            strSQL = "select * from setting where type = 'Stream'"
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStream.DataSource = ds
            ddlStream.DataTextField = "Parameter"
            ddlStream.DataValueField = "Value"
            ddlStream.DataBind()
            ddlStream.Items.Insert(0, New ListItem("All Program", "AP"))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCampus.SelectedIndexChanged
        Try
            staff_info_search()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]

    End Enum
End Class