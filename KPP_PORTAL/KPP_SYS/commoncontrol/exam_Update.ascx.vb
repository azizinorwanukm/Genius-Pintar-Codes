Imports System.Data.SqlClient

Public Class exam_Update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Exam_info_Load(Request.QueryString("exam_ID"))
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub Exam_info_Load(ByVal strExam_Code As String)
        strSQL = "SELECT * from exam_Info WHERE exam_ID ='" & strExam_Code & "'"
        '--debug
        ' Response.Write(strSQL)

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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_Name")) Then
                    exam_Name.Text = ds.Tables(0).Rows(0).Item("exam_Name")
                Else
                    exam_Name.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_Code")) Then
                    exam_Code.Text = ds.Tables(0).Rows(0).Item("exam_Code")
                Else
                    exam_Code.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_Year")) Then
                    exam_year.Text = ds.Tables(0).Rows(0).Item("exam_Year")
                Else
                    exam_year.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_StartDate")) Then
                    exam_StartDate.Text = ds.Tables(0).Rows(0).Item("exam_StartDate")
                Else
                    exam_StartDate.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_EndDate")) Then
                    exam_EndDate.Text = ds.Tables(0).Rows(0).Item("exam_EndDate")
                Else
                    exam_EndDate.Text = ""
                End If

            End If

        Catch ex As Exception
            ''lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        If exam_Name.Text <> "" And Regex.IsMatch(exam_Name.Text, "^[A-Za-z0-9 ]+$") Then

            If exam_Code.Text <> "" And Regex.IsMatch(exam_Code.Text, "^[A-Za-z0-9]+$") Then

                If exam_year.Text <> "" And Regex.IsMatch(exam_year.Text, "^[0-9]+$") And IsNumeric(exam_year.Text) Then

                    If exam_StartDate.Text <> "" And Regex.IsMatch(exam_StartDate.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$") Then

                        If exam_EndDate.Text <> "" And Regex.IsMatch(exam_EndDate.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$") Then

                            'UPDATE
                            strSQL = "UPDATE exam_Info SET exam_Name='" & exam_Name.Text & "',exam_Code='" & exam_Code.Text & "',exam_Year='" & exam_year.Text & "',exam_StartDate='" & exam_StartDate.Text & "',exam_EndDate='" & exam_EndDate.Text & "' WHERE exam_ID ='" & Request.QueryString("exam_ID") & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If strRet = "0" Then
                                ShowMessage("Successfull update exam", MessageType.Success)
                            Else
                                ShowMessage("Unsuccessfull update exam", MessageType.Error)
                            End If
                        Else
                            ShowMessage("Please enter a valid exam end date", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please enter a valid exam start date", MessageType.Error)
                    End If
                Else
                    ShowMessage("Please enter a valid exam year", MessageType.Error)
                End If
            Else
                ShowMessage("Please enter a valid exam code", MessageType.Error)
            End If
        Else
            ShowMessage("Please enter a valid exam name", MessageType.Error)
        End If
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class