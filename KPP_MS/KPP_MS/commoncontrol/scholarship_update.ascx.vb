Imports System.Data.SqlClient

Public Class scholarship_update
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

                    Session("getStatus") = "SS"
                    previousPage.NavigateUrl = String.Format("~/admin_kaunselor_pengurusanbiasiswa.aspx?admin_ID=" + Request.QueryString("admin_ID"))

                    ASS_Year()
                    ASS_Level()
                    ASS_Scholarship()

                    strRet = BindData(datRespondent)
                End If

            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub ASS_Year()
        strSQL = "select Parameter from setting where Type = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Year.DataSource = ds
            ddl_Year.DataTextField = "Parameter"
            ddl_Year.DataValueField = "Parameter"
            ddl_Year.DataBind()
            ddl_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddl_Year.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ASS_Level()
        strSQL = "Select Parameter from setting where Type = 'Level' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_Level.DataSource = ds
            ddl_Level.DataTextField = "Parameter"
            ddl_Level.DataValueField = "Parameter"
            ddl_Level.DataBind()
            ddl_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))
            ddl_Level.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ASS_Scholarship()
        strSQL = "Select UPPER(scholarship_name) scholarship_name, scholarship_id from scholarship where scholarship_view = 'Active' and scholarship_status = 'Active' order by scholarship_name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlScholarship.DataSource = ds
            ddlScholarship.DataTextField = "scholarship_name"
            ddlScholarship.DataValueField = "scholarship_id"
            ddlScholarship.DataBind()
            ddlScholarship.Items.Insert(0, New ListItem("Select Scholarship", String.Empty))
            ddlScholarship.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddl_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddl_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_Level.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by student_Name, B.student_Level, D.class_Name asc"

        tmpSQL = "  Select distinct A.std_id, UPPER(A.student_Name) student_Name, A.student_ID, B.student_Level, D.class_Name, C.year from student_info A
                    Left Join student_Level B on A.std_ID = B.std_ID
                    Left Join course C on A.std_ID = C.std_ID
                    Left Join class_info D on C.class_ID = D.class_ID"

        strWhere = "    where A.student_ID like '%M%' and A.student_Campus = 'PGPN' and (A.student_status = 'Access' or A.student_status = 'Graduate')
                        and B.Registered = 'Yes' and B.year = '" & ddl_Year.SelectedValue & "' 
                        and C.year = '" & ddl_Year.SelectedValue & "'
                        and D.class_year = '" & ddl_Year.SelectedValue & "' and D.class_type = 'Compulsory' and D.class_Campus = 'PGPN'"

        If ddl_Level.SelectedIndex > 0 Then
            strWhere += " and B.student_Level = '" & ddl_Level.SelectedValue & "' and D.class_Level = '" & ddl_Level.SelectedValue & "'"
        End If

        If txt_studentData.Text.Length > 0 Then
            strWhere += " and A.student_Name like '%" & txt_studentData.Text & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        strRet = BindData(datRespondent)
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer

        Dim find_religion As String = ""
        Dim get_religion As String = ""

        If ddlScholarship.SelectedIndex > 0 Then

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString

                    If chkUpdate.Checked = True Then

                        strSQL = "insert into scholarship_student(scholarship_id,std_id,year) values('" & ddlScholarship.SelectedValue & "','" & strKey & "','" & ddl_Year.SelectedValue & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If
                End If
                '--execute SQL
            Next

            If strRet = "0" Then
                ShowMessage(" Register Student Scholaship", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Register Student Scholaship", MessageType.Error)
            End If

        Else
            ShowMessage(" Please Select Scholarship ", MessageType.Error)
        End If

        strRet = BindData(datRespondent)
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class