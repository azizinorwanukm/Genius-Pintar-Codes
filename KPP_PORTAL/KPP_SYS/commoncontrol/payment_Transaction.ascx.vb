Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class payment_Transaction
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = ""
                id = Request.QueryString("admin_ID")

                txtstudent.Text = ""

                student_Level()
                year_list()

                ''get a user access
                Dim userAccess As String = ""
                userAccess = "select staff_Position from staff_Info where stf_ID = '" & id & "'"
                Dim access As String = getFieldValue(userAccess, strConn)
                hiddenAccess.Value = access

                load_page()
                class_info_list()
                strRet = BindData(datRespondent)
                ''Generate_Table()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT year from student_Level where year ='" & Now.Year & "'"

        '--debug
        ''Response.Write(strSQLstd) 

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlYear.SelectedValue = ""
            End If
        End If
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

    Protected Sub ddlClassnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClassnaming.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlLevelnaming_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLevelnaming.SelectedIndexChanged
        ''Dim class As String = ""
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

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
        Dim strOrderby As String = " ORDER BY student_info.student_Name ASC"

        tmpSQL = "Select distinct student_info.student_ID, student_info.std_ID,
                  student_info.student_Mykad,
                  student_info.student_Name,
                  student_Level.student_Level,
                  student_info.student_Photo,
                  class_info.class_Name 
                  From student_info 
                  left join student_Level on student_info.std_ID=student_level.std_ID
                  left join course on student_info.std_ID= course.std_ID 
                  left join class_info on course.class_ID= class_info.class_ID"
        strWhere = " WHERE student_info.std_ID IS NOT NULL"
        strWhere += " and course.year = '" & ddlYear.SelectedValue & "'"
        strWhere += " and student_level.year = '" & ddlYear.SelectedValue & "'"
        strWhere += " and class_info.class_type = 'Compulsory'"

        If Not txtstudent.Text.Length = 0 Then
            Dim student_ID As String = "Select student_ID from student_info where student_ID = '" & txtstudent.Text & "'"
            Dim get_student_ID As String = oCommon.getFieldValue(student_ID)

            Dim student_Name As String = "Select student_Name from student_info where student_Name = '" & txtstudent.Text & "'"
            Dim get_student_Name As String = oCommon.getFieldValue(student_Name)

            Dim student_Mykad As String = "Select student_Mykad from student_info where student_Mykad = '" & txtstudent.Text & "'"
            Dim get_student_Mykad As String = oCommon.getFieldValue(student_Mykad)

            If get_student_ID <> txtstudent.Text Then

                If get_student_Name <> txtstudent.Text Then

                    If get_student_Mykad <> txtstudent.Text Then
                        strWhere += " AND student_info.student_Mykad = '" & txtstudent.Text & "'"
                    ElseIf get_student_Mykad = txtstudent.Text Then
                        strWhere += " AND student_info.student_Mykad = '" & txtstudent.Text & "'"

                    End If
                ElseIf get_student_Name = txtstudent.Text Then
                    strWhere += " AND student_info.student_Name like '%" & txtstudent.Text & "%'"

                End If
            ElseIf get_student_ID = txtstudent.Text Then
                strWhere += " AND student_info.student_ID = '" & txtstudent.Text & "'"

            End If
        End If

        If ddlLevelnaming.SelectedIndex > 0 Then
            strWhere += " and student_level.student_Level = '" & ddlLevelnaming.SelectedValue & "'"
        End If

        If ddlClassnaming.SelectedIndex > 0 Then
            strWhere += " and class_info.class_ID = '" & ddlClassnaming.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        Debug.WriteLine(getSQL)
        ''--debug
        Return getSQL
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_transaksi_yuran_gambar.aspx?std_ID=" + strKeyID + "&admin_ID=" + Request.QueryString("admin_ID") + "&year=" + ddlYear.SelectedValue)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub class_info_list()
        strSQL = "SELECT class_Name,class_ID FROM class_info where class_year = '" & Now.Year & "' and class_type = 'Compulsory' order by class_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClassnaming.DataSource = ds
            ddlClassnaming.DataTextField = "class_Name"
            ddlClassnaming.DataValueField = "class_ID"
            ddlClassnaming.DataBind()
            ddlClassnaming.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_Level()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevelnaming.DataSource = ds
            ddlLevelnaming.DataTextField = "Parameter"
            ddlLevelnaming.DataValueField = "Parameter"
            ddlLevelnaming.DataBind()
            ddlLevelnaming.Items.Insert(0, New ListItem("Select Level", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub


    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
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

End Class