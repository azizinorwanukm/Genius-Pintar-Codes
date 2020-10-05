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

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then

                    scholarship_type_list()
                    scholarship_status_list()

                    load_page()
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT * from scholarship where scholarship_id ='" & Request.QueryString("scholarship_id") & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("scholarship_name")) Then
                scholarship_name.Text = ds.Tables(0).Rows(0).Item("scholarship_name")
            Else
                scholarship_name.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("scholarship_sponsar")) Then
                scholarship_sponsar.Text = ds.Tables(0).Rows(0).Item("scholarship_sponsar")
            Else
                scholarship_sponsar.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("scholarship_type")) Then
                scholarship_type.SelectedValue = ds.Tables(0).Rows(0).Item("scholarship_type")
            Else
                scholarship_type.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("scholarship_status")) Then
                scholarship_status.SelectedValue = ds.Tables(0).Rows(0).Item("scholarship_status")
            Else
                scholarship_status.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub scholarship_type_list()
        strSQL = "SELECT * from setting where idx = 'Scholarship'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            scholarship_type.DataSource = ds
            scholarship_type.DataTextField = "Parameter"
            scholarship_type.DataValueField = "Parameter"
            scholarship_type.DataBind()
            scholarship_type.Items.Insert(0, New ListItem("Select Type", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub scholarship_status_list()
        strSQL = "SELECT * from setting where idx = 'GOD'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            scholarship_status.DataSource = ds
            scholarship_status.DataTextField = "Parameter"
            scholarship_status.DataValueField = "Parameter"
            scholarship_status.DataBind()
            scholarship_status.Items.Insert(0, New ListItem("Active", "Active"))
            scholarship_status.Items.Insert(1, New ListItem("Inactive", "Inactive"))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Try
            Response.Redirect("admin_kaunselor_pengurusanbiasiswa.aspx?admin_ID=" + Request.QueryString("admin_ID"))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Try
            If scholarship_name.Text.Length > 0 Then
                If scholarship_sponsar.Text.Length > 0 Then
                    If scholarship_type.SelectedIndex > 0 Then

                        strSQL = "UPDATE scholarship SET
                                    scholarship_name = '" & scholarship_name.Text & "', scholarship_sponsar = '" & scholarship_sponsar.Text & "',
                                    scholarship_type = '" & scholarship_type.SelectedValue & "', scholarship_status = '" & scholarship_status.SelectedValue & "'
                                  WHERE scholarship_id = '" & Request.QueryString("scholarship_id") & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        If strRet = "0" Then
                            ShowMessage("Register scholarship", MessageType.Success)
                        Else
                            ShowMessage("Register scholarship", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please select scholarship type", MessageType.Error)
                    End If
                Else
                    ShowMessage("Please fill in scholarship sponsor", MessageType.Error)
                End If
            Else
                ShowMessage("Please fill in scholarship name", MessageType.Error)
            End If

            load_page()

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