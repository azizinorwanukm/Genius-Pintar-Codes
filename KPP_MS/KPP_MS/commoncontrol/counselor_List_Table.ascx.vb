Imports System.Data.SqlClient

Public Class counselor_List_Table
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim sqlCommd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    year_list()
                    counselorstatus_list()
                    load_page()

                    strRet = BindData(datRespondent)

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT distinct class_year from class_info where class_year ='" & Now.Year & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_year")) Then
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("class_year")
            Else
                ddlyear.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
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
            ddlyear.Items.Insert(0, New ListItem("Select Year", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub counselorstatus_list()
        strSQL = "SELECT Parameter , Value FROM setting WHERE Type='Counselor'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlcounselorstatus.DataSource = ds
            ddlcounselorstatus.DataTextField = "Value"
            ddlcounselorstatus.DataValueField = "Parameter"
            ddlcounselorstatus.DataBind()
            ddlcounselorstatus.Items.Insert(0, New ListItem("Select Counselor Status", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlcounselorstatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcounselorstatus.SelectedIndexChanged
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

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY dicipline_info.Dicipline_Date DESC, student_info.student_Name, case_info.case_Name ASC"

        tmpSQL = "select counseling_info.klsr_id, student_info.student_Name, class_info.class_Name, case_info.case_Name, dicipline_info.Dicipline_Date, counseling_info.kslr_status from counseling_info
                  left join student_info on counseling_info.std_ID = student_info.std_ID
                  left join dicipline_info on counseling_info.disiplin_ID = dicipline_info.disiplin_id
                  left join class_info on dicipline_info.class_ID = class_info.class_ID
                  left join case_info on dicipline_info.case_ID = case_info.case_ID"

        strWhere = " where counseling_info.klsr_id is not null"

        strWhere += " and dicipline_info.Dicipline_Date like '%" & ddlyear.SelectedValue & "%'"

        If ddlcounselorstatus.SelectedIndex > 0 Then
            strWhere += " and counseling_info.kslr_status = '" & ddlcounselorstatus.SelectedValue & "'"
        End If

        If Not txtstudent.Text.Length = 0 Then
            Dim student_ID As String = "Select student_ID from student_info where student_ID = '" & txtstudent.Text & "'"
            Dim get_student_ID As String = oCommon.getFieldValue(student_ID)

            Dim student_Name As String = "Select student_Name from student_info where student_Name like '%" & txtstudent.Text & "%'"
            Dim get_student_Name As String = oCommon.getFieldValue(student_Name)

            Dim student_Mykad As String = "Select student_Mykad from student_info where student_Mykad = '" & txtstudent.Text & "'"
            Dim get_student_Mykad As String = oCommon.getFieldValue(student_Mykad)

            If get_student_ID = "" Then

                If get_student_Name = "" Then

                    If get_student_Mykad = "" Then

                    ElseIf get_student_Mykad <> "" Then
                        strWhere += " AND student_info.student_Mykad = '" & txtstudent.Text & "'"

                    End If
                ElseIf get_student_Name <> "" Then
                    strWhere += " AND student_info.student_Name like '%" & txtstudent.Text & "%'"

                End If
            ElseIf get_student_ID <> "" Then
                strWhere += " AND student_info.student_ID = '" & txtstudent.Text & "'"

            End If
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_detail_kaunselor.aspx?klsr_id=" + strKeyID + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class