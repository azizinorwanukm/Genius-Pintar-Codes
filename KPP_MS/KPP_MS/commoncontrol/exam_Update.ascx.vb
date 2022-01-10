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
                examName_List()
                examYear_List()
                InstitutionsList()

                Exam_info_Load(Request.QueryString("exam_ID"))

                Session("getStatus") = "VE"
                previousPage.NavigateUrl = String.Format("~/admin_peperiksaan_pengurusan_peperiksaan.aspx?admin_ID=" + Request.QueryString("admin_ID"))
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub examName_List()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Exam' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamName.DataSource = ds
            ddlExamName.DataTextField = "Parameter"
            ddlExamName.DataValueField = "Parameter"
            ddlExamName.DataBind()
            ddlExamName.Items.Insert(0, New ListItem("Select Examination", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub examYear_List()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "Parameter"
            ddlExamYear.DataValueField = "Parameter"
            ddlExamYear.DataBind()
            ddlExamYear.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub InstitutionsList()
        Try
            Dim stryear As String = "select Parameter, Value from setting where Type = 'Pusat Campus' order by parameter asc"
            Dim sqlYearDA As New SqlDataAdapter(stryear, objConn)

            Dim yrds As DataSet = New DataSet
            sqlYearDA.Fill(yrds, "YrTable")

            ddlExamInstitutions.DataSource = yrds
            ddlExamInstitutions.DataValueField = "Value"
            ddlExamInstitutions.DataTextField = "Parameter"
            ddlExamInstitutions.DataBind()
            ddlExamInstitutions.Items.Insert(0, New ListItem("Select Institutions", String.Empty))

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
                    ddlExamName.SelectedValue = ds.Tables(0).Rows(0).Item("exam_Name")
                Else
                    ddlExamName.SelectedIndex = 0
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_Code")) Then
                    txtExamCode.Text = ds.Tables(0).Rows(0).Item("exam_Code")
                Else
                    txtExamCode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_Year")) Then
                    ddlExamYear.SelectedValue = ds.Tables(0).Rows(0).Item("exam_Year")
                Else
                    ddlExamYear.SelectedIndex = 0
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_StartDate")) Then
                    txtStartDate.Text = ds.Tables(0).Rows(0).Item("exam_StartDate")
                Else
                    txtStartDate.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_EndDate")) Then
                    txtEndDate.Text = ds.Tables(0).Rows(0).Item("exam_EndDate")
                Else
                    txtEndDate.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("exam_Institutions")) Then
                    ddlExamInstitutions.SelectedValue = ds.Tables(0).Rows(0).Item("exam_Institutions")
                Else
                    ddlExamInstitutions.SelectedIndex = 0
                End If

            End If

        Catch ex As Exception
            ''lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick

        If ddlExamName.SelectedIndex > 0 Then

            If txtExamCode.Text.Length > 0 Then

                If ddlExamYear.SelectedIndex > 0 Then

                    If txtStartDate.Text <> "" And Regex.IsMatch(txtStartDate.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$") Then

                        If txtEndDate.Text <> "" And Regex.IsMatch(txtEndDate.Text, "(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$") Then

                            If ddlExamInstitutions.SelectedIndex > 0 Then

                                'UPDATE
                                strSQL = "UPDATE exam_Info SET exam_Name='" & ddlExamName.SelectedValue & "',exam_Code='" & txtExamCode.Text & "',exam_Year='" & ddlExamYear.SelectedValue & "',exam_StartDate='" & txtStartDate.Text & "',exam_EndDate='" & txtEndDate.Text & "',exam_Institutions = '" & ddlExamInstitutions.SelectedValue & "'  WHERE exam_ID ='" & Request.QueryString("exam_ID") & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                                If strRet = "0" Then
                                    ShowMessage("Successfull update exam", MessageType.Success)
                                Else
                                    ShowMessage("Unsuccessfull update exam", MessageType.Error)
                                End If
                            Else
                                ShowMessage("Please select institutions", MessageType.Error)
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

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class