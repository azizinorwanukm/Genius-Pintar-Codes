Imports System.Data.SqlClient

Public Class course_Update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim result As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    subject_sem_Load()
                    subject_type_Load()
                    subject_StudentYear_Load()
                    Subject_info_Load(Request.QueryString("subject_ID"))

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Subject_info_Load(ByVal strcourse_ID As String)
        strSQL = "SELECT * from subject_info WHERE subject_ID ='" & strcourse_ID & "'"
        '--debug
        'Response.Write(strSQL)

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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_Name")) Then
                    subject_Name.Text = ds.Tables(0).Rows(0).Item("subject_Name")
                Else
                    subject_Name.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_NameBM")) Then
                    subject_NameBM.Text = ds.Tables(0).Rows(0).Item("subject_NameBM")
                Else
                    subject_NameBM.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("std_number")) Then
                    txtStdNumber.Text = ds.Tables(0).Rows(0).Item("std_number")
                Else
                    txtStdNumber.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_year")) Then
                    subject_year.Text = ds.Tables(0).Rows(0).Item("subject_year")
                Else
                    subject_year.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_StudentYear")) Then
                    ddlsubject_StudentYear.SelectedValue = ds.Tables(0).Rows(0).Item("subject_StudentYear")
                Else
                    ddlsubject_StudentYear.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_code")) Then
                    subject_Code.Text = ds.Tables(0).Rows(0).Item("subject_code")
                Else
                    subject_Code.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_type")) Then
                    ddlsubjectType.SelectedValue = ds.Tables(0).Rows(0).Item("subject_type")
                Else
                    ddlsubjectType.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_sem")) Then
                    ddlsubject_Sem.SelectedValue = ds.Tables(0).Rows(0).Item("subject_sem")
                Else
                    ddlsubject_Sem.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_CreditHour")) Then
                    subject_credithour.Text = ds.Tables(0).Rows(0).Item("subject_CreditHour")
                Else
                    subject_credithour.Text = ""
                End If

            End If

        Catch ex As Exception
            ''lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0

        'UPDATE
        strSQL = "UPDATE subject_info SET subject_Name='" & subject_Name.Text & "',subject_NameBM='" & subject_NameBM.Text & "',subject_code='" & subject_Code.Text & "',subject_year='" & subject_year.Text & "',subject_type='" & ddlsubjectType.SelectedValue & "',subject_StudentYear='" & ddlsubject_StudentYear.SelectedValue & "',subject_sem='" & ddlsubject_Sem.SelectedValue & "',subject_CreditHour='" & subject_credithour.Text & "',std_number='" & txtStdNumber.Text & "' WHERE subject_ID ='" & Request.QueryString("subject_ID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            errorCount = 0
        Else
            errorCount = 1
        End If

        If errorCount = 1 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 0 Then
            Response.Redirect("admin_pengurusan_am_kursus.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        End If
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_pengurusan_am_kursus.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Protected Function fillDDL(columnName As String) As DataTable

        Dim query As String = ""
        Dim dt As New DataTable
        query += "SELECT DISTINCT " & columnName & " FROM subject_info WHERE subject_ID IS NOT NULL AND subject_Name is not null"
        Dim sqlAdapter As New SqlDataAdapter(query, strConn)
        sqlAdapter.Fill(dt)

        Return dt
    End Function

    Private Sub subject_type_Load()
        ddlsubjectType.DataSource = Me.fillDDL("subject_type")
        ddlsubjectType.DataTextField = "subject_type"
        ddlsubjectType.DataValueField = "subject_type"
        ddlsubjectType.DataBind()
        ddlsubjectType.Items.Insert(0, New ListItem("Select Subject Type", String.Empty))
        ddlsubjectType.SelectedIndex = 0
    End Sub

    Private Sub subject_StudentYear_Load()
        ddlsubject_StudentYear.DataSource = Me.fillDDL("subject_StudentYear")
        ddlsubject_StudentYear.DataTextField = "subject_StudentYear"
        ddlsubject_StudentYear.DataValueField = "subject_StudentYear"
        ddlsubject_StudentYear.DataBind()
        ddlsubject_StudentYear.Items.Insert(0, New ListItem("Select Level", String.Empty))
        ddlsubject_StudentYear.SelectedIndex = 0
    End Sub

    Private Sub subject_sem_Load()
        ddlsubject_Sem.DataSource = Me.fillDDL("subject_sem")
        ddlsubject_Sem.DataTextField = "subject_sem"
        ddlsubject_Sem.DataValueField = "subject_sem"
        ddlsubject_Sem.DataBind()
        ddlsubject_Sem.Items.Insert(0, New ListItem("Select Sem", String.Empty))
        ddlsubject_Sem.SelectedIndex = 0

    End Sub

End Class