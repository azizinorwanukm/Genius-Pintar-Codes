Imports System.Data.SqlClient

Public Class Disiplin_config
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

                Dim getStatus As String = Request.QueryString("status")

                If getStatus = "VD" Then ''View Discipline
                    txtbreadcrum1.Text = "View Discipline"
                    ViewDiscipline.Visible = True
                    RegisterDiscipline.Visible = False

                    btnViewDiscipline.Attributes("class") = "btn btn-info"
                    btnRegisterDiscipline.Attributes("class") = "btn btn-default font"

                    category_list()
                    strRet = BindData(datRespondent)

                ElseIf getStatus = "RD" Then ''Register Discipline
                    txtbreadcrum1.Text = "Register Discipline"
                    ViewDiscipline.Visible = False
                    RegisterDiscipline.Visible = True

                    btnViewDiscipline.Attributes("class") = "btn btn-default font"
                    btnRegisterDiscipline.Attributes("class") = "btn btn-info"

                    category_list()

                End If

            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub btnViewDiscipline_ServerClick(sender As Object, e As EventArgs) Handles btnViewDiscipline.ServerClick
        Response.Redirect("admin_config_disiplin.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=VD")
    End Sub

    Private Sub btnRegisterDiscipline_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterDiscipline.ServerClick
        Response.Redirect("admin_config_disiplin.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=RD")
    End Sub

    Private Sub category_list()
        strSQL = "SELECT * from setting where Type = 'Discipline' order by ID DESC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCaseCategory.DataSource = ds
            ddlCaseCategory.DataTextField = "Parameter"
            ddlCaseCategory.DataValueField = "Value"
            ddlCaseCategory.DataBind()
            ddlCaseCategory.Items.Insert(0, New ListItem("Select Category", String.Empty))

            ddlCategory.DataSource = ds
            ddlCategory.DataTextField = "Parameter"
            ddlCategory.DataValueField = "Value"
            ddlCategory.DataBind()
            ddlCategory.Items.Insert(0, New ListItem("Select Category", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim mydataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(mydataSet, "myaccount")

            gvTable.DataSource = mydataSet
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
        Dim strOrderby As String = " ORDER by case_info.case_MeritDemerit_Point ASC"

        tmpSQL = "select * from case_info where case_Category = '" & ddlCaseCategory.SelectedValue & "'"

        getSQL = tmpSQL & strOrderby
        Return getSQL
    End Function


    Protected Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyName As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_ClassData As New SqlDataAdapter()
            Dim dlt_Class As String

            Dlt_ClassData.SelectCommand = New SqlCommand()
            Dlt_ClassData.SelectCommand.Connection = MyConnection
            Dlt_ClassData.SelectCommand.CommandText = "delete case_info where case_id = '" & strKeyName & "'"
            MyConnection.Open()
            dlt_Class = Dlt_ClassData.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim strKeyCode As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString
        Try
            Response.Redirect("admin_config_disiplin_kemaskini.aspx?case_ID=" + strKeyCode + "&admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub ddlCaseCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCaseCategory.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick

        If txtCase_Name.Text.Length > 0 Then

            If ddlCategory.SelectedIndex > 0 Then

                If txtDemerit_Point.Text.Length > 0 Then

                    Using STDDATA As New SqlCommand("INSERT INTO case_info(case_Name,case_Category,case_MeritDemerit_Point) values ('" & oCommon.FixSingleQuotes(txtCase_Name.Text) & "','" & ddlCategory.SelectedValue & "','" & txtDemerit_Point.Text & "')", objConn)
                        objConn.Open()
                        Dim i = STDDATA.ExecuteNonQuery()
                        objConn.Close()

                        If i <> 0 Then
                            ShowMessage("Add New Discipline", MessageType.Success)
                        Else
                            ShowMessage("Unsuccessful Add New Discipline", MessageType.Error)
                        End If
                    End Using

                Else
                    ShowMessage("Please Fill In Demerit Point", MessageType.Error)
                End If
            Else
                ShowMessage("Please Select Category", MessageType.Error)
            End If
        Else
            ShowMessage("Please Fill In Case Name", MessageType.Error)
        End If
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class

