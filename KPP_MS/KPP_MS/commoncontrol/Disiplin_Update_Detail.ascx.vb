Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing

Public Class Disiplin_Update_Detail
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim staffSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                txtbreadcrum1.Text = "View Case Detail"
                previousPage.NavigateUrl = String.Format("~/admin_edit_disiplin.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=VC")

                ViewDetail_CaseCategory_List()
                ViewDetail_CaseName_List()

                txtStudent_Name.Enabled = False
                txtClass_Name.Enabled = False
                txtStudent_Level.Enabled = False

                Preload_Data()

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ViewDetail_CaseCategory_List()
        strSQL = "SELECT * from setting where Type = 'Discipline' order by ID DESC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCase_Category.DataSource = ds
            ddlCase_Category.DataTextField = "Parameter"
            ddlCase_Category.DataValueField = "Value"
            ddlCase_Category.DataBind()
            ddlCase_Category.Items.Insert(0, New ListItem("Select Case Category", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ViewDetail_CaseName_List()
        strSQL = "Select case_id from dicipline_info where disiplin_id = '" & Request.QueryString("disiplin_id") & "'"
        strRet = oCommon.getFieldValue(strSQL)


        strSQL = "SELECT * from case_info  where case_ID = '" & strRet & "'order by case_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCase_Name.DataSource = ds
            ddlCase_Name.DataTextField = "case_Name"
            ddlCase_Name.DataValueField = "case_ID"
            ddlCase_Name.DataBind()
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ViewDetail_CaseName_List_Update()
        strSQL = "SELECT * from case_info where case_Category = '" & ddlCase_Category.SelectedValue & "' order by case_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCase_Name.DataSource = ds
            ddlCase_Name.DataTextField = "case_Name"
            ddlCase_Name.DataValueField = "case_ID"
            ddlCase_Name.DataBind()
            ddlCase_Name.Items.Insert(0, New ListItem("Select Case Name", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlCase_Category_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCase_Category.SelectedIndexChanged
        Try
            ViewDetail_CaseName_List_Update()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Preload_Data()

        ''Get all Student Case Information
        strSQL = "  Select B.student_Name, C.class_Level, C.class_Name, A.Dicipline_Date, D.case_Category, D.case_Name, A.Detail_Case, A.need_Counseling from dicipline_info A
                    left join student_info B on A.std_ID = B.std_ID
                    left join class_info C on A.class_ID = C.class_ID
                    left join case_info D on A.case_ID = D.case_ID
                    where A.disiplin_id = '" & Request.QueryString("disiplin_id") & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                txtStudent_Name.Text = ds.Tables(0).Rows(0).Item("student_Name")
            Else
                txtStudent_Name.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Level")) Then
                txtStudent_Level.Text = ds.Tables(0).Rows(0).Item("class_Level")
            Else
                txtStudent_Level.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Name")) Then
                txtClass_Name.Text = ds.Tables(0).Rows(0).Item("class_Name")
            Else
                txtClass_Name.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Dicipline_Date")) Then
                txtDate.Text = ds.Tables(0).Rows(0).Item("Dicipline_Date")
            Else
                txtDate.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("case_Category")) Then
                ddlCase_Category.SelectedValue = ds.Tables(0).Rows(0).Item("case_Category")
            Else
                ddlCase_Category.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("case_Name")) Then
                ddlCase_Name.SelectedValue = ds.Tables(0).Rows(0).Item("case_Name")
            Else
                ddlCase_Name.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Detail_Case")) Then
                txtCase_Detail.Text = ds.Tables(0).Rows(0).Item("Detail_Case")
            Else
                txtCase_Detail.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("need_Counseling")) Then
                Dim Get_Check As String = ds.Tables(0).Rows(0).Item("need_Counseling")

                If Get_Check = "True" Then
                    Check_NeedCounseling.Checked = True
                ElseIf Get_Check = "False" Then
                    Check_NeedCounseling.Checked = False
                End If
            Else
                Check_NeedCounseling.Checked = False
            End If

        End If
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Try

            ''Get Demerit Point
            Dim Find_DemeritPoint As String = "Select case_MeritDemerit_Point from case_info where case_ID = '" & ddlCase_Name.SelectedValue & "'"
            Dim Get_DemeritPoint As String = oCommon.getFieldValue(Find_DemeritPoint)

            Dim getCheck_Counseling As String = ""

            If Check_NeedCounseling.Checked = True Then
                getCheck_Counseling = "True"
            Else
                getCheck_Counseling = "False"
            End If

            Dim Get_Date_SS As String = txtDate.Text.Substring(0, 2)
            Dim Get_Year_SS As String = txtDate.Text.Substring(txtDate.Text.Length - 4, 4)
            Dim Get_Month_SS As String = txtDate.Text.Remove(0, 3)
            Get_Month_SS = Get_Month_SS.Remove(Get_Month_SS.Length - 5, 5)

            If Get_Month_SS = "January" Then
                Get_Month_SS = "01"
            ElseIf Get_Month_SS = "February" Then
                Get_Month_SS = "02"
            ElseIf Get_Month_SS = "March" Then
                Get_Month_SS = "03"
            ElseIf Get_Month_SS = "April" Then
                Get_Month_SS = "04"
            ElseIf Get_Month_SS = "May" Then
                Get_Month_SS = "05"
            ElseIf Get_Month_SS = "June" Then
                Get_Month_SS = "06"
            ElseIf Get_Month_SS = "July" Then
                Get_Month_SS = "07"
            ElseIf Get_Month_SS = "August" Then
                Get_Month_SS = "08"
            ElseIf Get_Month_SS = "September" Then
                Get_Month_SS = "09"
            ElseIf Get_Month_SS = "October" Then
                Get_Month_SS = "10"
            ElseIf Get_Month_SS = "November" Then
                Get_Month_SS = "11"
            ElseIf Get_Month_SS = "December" Then
                Get_Month_SS = "12"
            End If

            Dim Final_Date_Data As String = Get_Date_SS & "/" & Get_Month_SS & "/" & Get_Year_SS

            strSQL = "Update dicipline_info set case_id = '" & ddlCase_Name.SelectedValue & "', Detail_Case = '" & txtCase_Detail.Text & "', Dicipline_Date = '" & Final_Date_Data & "', meritdemerit_point = '" & Get_DemeritPoint & "', need_Counseling = '" & getCheck_Counseling & "' where disiplin_id = '" & Request.QueryString("disiplin_id") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
                ShowMessage(" Update Student Case Detail ", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Update Student Data ", MessageType.Success)
            End If

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