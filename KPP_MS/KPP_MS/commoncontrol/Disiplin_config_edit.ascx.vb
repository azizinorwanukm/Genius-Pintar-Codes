Imports System.Data.SqlClient

Public Class Disiplin_config_edit
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

                    txtbreadcrum1.Text = "Edit Discipline Information"

                    category_list()

                    previousPage.NavigateUrl = String.Format("~/admin_config_disiplin.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=VD")

                    case_info_load()

                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub category_list()
        strSQL = "SELECT * from setting where Type = 'Discipline' order by ID DESC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

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

    Private Sub case_info_load()
        strSQL = "SELECT * from case_info WHERE case_ID ='" & Request.QueryString("case_ID") & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("case_Name")) Then
                    txtCase_Name.Text = ds.Tables(0).Rows(0).Item("case_Name")
                Else
                    txtCase_Name.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("case_MeritDemerit_Point")) Then
                    txtDemerit_Point.Text = ds.Tables(0).Rows(0).Item("case_MeritDemerit_Point")
                Else
                    txtDemerit_Point.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("case_Category")) Then
                    ddlCategory.SelectedValue = ds.Tables(0).Rows(0).Item("case_Category")
                Else
                    ddlCategory.SelectedValue = ""
                End If

            End If

        Catch ex As Exception
            ''lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Dim errorCount As Integer = 0

        'UPDATE
        strSQL = "  UPDATE case_info SET case_Name ='" & txtCase_Name.Text & "',
                    case_Category ='" & ddlCategory.SelectedValue & "',case_MeritDemerit_Point='" & txtDemerit_Point.Text & "' WHERE case_ID ='" & Request.QueryString("case_ID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then
            ShowMessage(" Update Discipline Information", MessageType.Success)
        Else
            ShowMessage("Unsuccessfull Update Discipline Information", MessageType.Error)
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