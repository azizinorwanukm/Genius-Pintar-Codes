Imports System.Data.SqlClient

Public Class lecturer_RegClass
    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                
                subject_sem_list()
                subject_type_list()
                subject_level_list()

                fillClassDDL()  'Fill class ddl
                fillStaffDDL()  'Fill staff ddl

                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub subject_sem_list()
        Try

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Sem'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlFilterSems.DataSource = levds
            ddlFilterSems.DataValueField = "Parameter"
            ddlFilterSems.DataTextField = "Parameter"
            ddlFilterSems.DataBind()
            ddlFilterSems.Items.Insert(0, New ListItem("Select Semester", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub subject_type_list()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Subject Type'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlFilterType.DataSource = levds
            ddlFilterType.DataValueField = "Parameter"
            ddlFilterType.DataTextField = "Parameter"
            ddlFilterType.DataBind()
            ddlFilterType.Items.Insert(0, New ListItem("Select Subject Type", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub subject_level_list()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            Dim strLevelSql As String = "Select Parameter from setting where Type = 'Level'"
            Dim sqlLevelDA As New SqlDataAdapter(strLevelSql, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlFilterStdntYear.DataSource = levds
            ddlFilterStdntYear.DataValueField = "Parameter"
            ddlFilterStdntYear.DataTextField = "Parameter"
            ddlFilterStdntYear.DataBind()
            ddlFilterStdntYear.Items.Insert(0, New ListItem("Select Subject Level", String.Empty))
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
        Dim strOrderby As String = " ORDER BY subject_Name ASC"

        tmpSQL = "select * from subject_info"
        strWhere = " where subject_year = '" & Now.Year & "'"

        If ddlFilterSems.SelectedIndex > 0 Then
            strWhere += " And subject_sem = '" & ddlFilterSems.SelectedValue & "'"
        End If

        If ddlFilterType.SelectedIndex > 0 Then
            strWhere += " And subject_type = '" & ddlFilterType.SelectedValue & "'"
        End If

        If ddlFilterStdntYear.SelectedIndex > 0 Then
            strWhere += " And subject_StudentYear = '" & ddlFilterStdntYear.SelectedValue & "'"
        End If

        If Not searchTextBox.Text.Length = 0 Then
            strWhere += " And subject_Name Like '%" & searchTextBox.Text & "%'"
        End If

        If Not searchTextBox.Text.Length = 0 Then
            strWhere += " OR subject_code LIKE '%" & searchTextBox.Text & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then
                    Dim class_Level As String = ""
                    Dim subject_StudentYear As String = ""

                    class_Level = "select class_Level from class_info where class_ID ='" & ddlclassChoose.SelectedValue & "' and class_year = '" & Now.Year & "'"
                    Dim dataClass As String = getFieldValue(class_Level, strConn)

                    subject_StudentYear = "select subject_StudentYear from subject_info where subject_ID='" & strKey & "' and subject_year = '" & Now.Year & "'"
                    Dim dataSubject As String = getFieldValue(subject_StudentYear, strConn)

                    If dataClass = dataSubject Then

                        Using STDDATA As New SqlCommand("INSERT INTO lecturer(stf_ID, class_ID, subject_ID, lecturer_year) 
                                                         VALUES('" & ddlstaffChoose.SelectedValue & "','" & ddlclassChoose.SelectedValue & "','" & strKey & "','" & Now.Year & "')", objConn)
                            objConn.Open()
                            Dim j = STDDATA.ExecuteNonQuery()
                            objConn.Close()
                            If j <> 0 Then
                                errorCount = 0
                            Else
                                errorCount = 1
                            End If
                        End Using

                    Else
                        errorCount = 2
                    End If
                End If
            End If
        Next

        If errorCount = 0 Then
            Response.Redirect("admin_pengajar_penempatan_kelas.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 1 Then
            Response.Redirect("admin_pengajar_penempatan_kelas.aspx?result=-1admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_pengajar_penempatan_kelas.aspx?result=2admin_ID=" + Request.QueryString("admin_ID"))
        End If
    End Sub

    Public Function getFieldValue(ByVal data As String, ByVal MyConnection As String) As String
        If data.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(data, conn)
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

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()

            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete subject_Info where subject_ID ='" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub fillClassDDL()

        Try
            Dim data As String = ""

            If ddlFilterType.SelectedValue = "Compulsory" Then
                data = "Compulsory"
            Else
                data = "Electives"
            End If

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            strSQL = "SELECT class_ID,class_Name FROM class_info WHERE class_year='" & Now.Year & "' and class_type = '" & data & "' ORDER BY class_Name ASC"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)


            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlclassChoose.DataSource = ds
            ddlclassChoose.DataTextField = "class_Name"
            ddlclassChoose.DataValueField = "class_ID"
            ddlclassChoose.DataBind()
            ddlclassChoose.Items.Insert(0, New ListItem("Select class...", ""))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub fillStaffDDL()
        Try

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            strSQL = "SELECT stf_ID,staff_Name FROM staff_Info where staff_Status = 'Access'"
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstaffChoose.DataSource = ds
            ddlstaffChoose.DataTextField = "staff_Name"
            ddlstaffChoose.DataValueField = "stf_ID"
            ddlstaffChoose.DataBind()
            ddlstaffChoose.Items.Insert(0, New ListItem("Select staff...", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub searchBtn_Click(sender As Object, e As EventArgs) Handles searchBtn.Click
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlFilterSems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterSems.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlFilterType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterType.SelectedIndexChanged
        Try
            fillClassDDL()
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlFilterStdntYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilterStdntYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub
End Class