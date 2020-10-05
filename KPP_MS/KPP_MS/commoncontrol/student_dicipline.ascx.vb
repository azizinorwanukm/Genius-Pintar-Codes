Imports System.Data.SqlClient
Imports System.IO

Public Class student_dicipline
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
                Student_name()
                student_class()
                total_compound()
                CurrentDate.Text = DateTime.Now.ToString("dd MMMM yyyy")


                Dim id As String = " "
                id = Request.QueryString("std_ID")
                load_page()
                strRet = BindData(datRespondent)


                case_box.Enabled = False
                action_box.Enabled = False

            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub load_page()



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



    Private Function getSQL() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""

        tmpSQL = " select distinct dicipline_info.disiplin_id,student_info.std_ID,dicipline_info.Case_Name,dicipline_info.Staff_Name,dicipline_info.Dicipline_Merit,dicipline_info.Dicipline_compound,dicipline_info.Dicipline_Merit,dicipline_info.Dicipline_date
                  from dicipline_info 
                  left join student_info on dicipline_info.student_id = student_info.student_ID"
        strWhere = " where student_info.std_ID = '" & Request.QueryString("std_ID") & "'"

        getSQL = tmpSQL & strWhere
        Return getSQL
    End Function

    Protected Sub validate_dis()

        strSQL = "select std_ID from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("connectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)


        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            If ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
                ''dataset is really empty

            Else
                ''ada data


            End If


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Student_name()
        strSQL = "select student_name from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)


        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                Name.Text = ds.Tables(0).Rows(0)("student_name")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub student_class()


        strSQL = "select distinct student_info.student_Name, student_info.std_ID,class_info.class_name from class_info 
                    left join course on course.class_ID = class_info.class_ID
                    left join student_info on student_info.std_ID = course.std_ID
                    where student_info.std_ID = '" & Request.QueryString("std_ID") & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            If ds.Tables(0).Rows.Count > 0 Then
                [class].Text = ds.Tables(0).Rows(0)("class_name")

            End If
        Catch ex As Exception

        End Try

    End Sub


    Protected Sub total_compound()

        Dim zero As String = "0"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)


        strSQL = "select  sum(dicipline_info.Dicipline_Compound) from dicipline_info
        Left join student_info on student_info.student_ID = dicipline_info.student_ID
        where student_info.std_ID =  '" & Request.QueryString("std_ID") & "'
		group by student_info.std_Id"

        Dim data As String = getFieldValue(strSQL, strConn)
        If data.Length = 0 Then
            Compound.Text = zero
        Else
            Compound.Text = data
        End If


    End Sub

    Protected Sub datrespondent_rowediting(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing

        Dim strkey As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString

        strSQL = "select dicipline_date,staff_name,detail_case,dicipline_action from dicipline_info where disiplin_id = '" & strkey & "' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDADicipline As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDADicipline.Fill(ds, "anytable")

            If ds.Tables(0) Is Nothing OrElse ds.Tables(0).Rows.Count = 0 Then
                dic_view.Attributes.Add("style", "display:block")
                dic_date.Text = "No Available Detail"
                person_charge.Text = "No Available Detail"
                case_box.Text = "No Available Detail"
                action_box.Text = "No Available Detail"
            Else
                dic_view.Attributes.Add("style", "display:block")
                dic_date.Text = ds.Tables(0).Rows(0)("dicipline_date").ToString()
                person_charge.Text = ds.Tables(0).Rows(0)("staff_name").ToString()
                case_box.Text = ds.Tables(0).Rows(0)("detail_case").ToString()
                action_box.Text = ds.Tables(0).Rows(0)("dicipline_action").ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
