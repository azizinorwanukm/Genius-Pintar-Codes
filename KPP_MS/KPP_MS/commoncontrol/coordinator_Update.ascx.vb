Imports System.Data.SqlClient

Public Class coordinator_Update
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oCommon As New Commonfunction
    Dim result As Integer = 0
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                yearList()
                campusList()
                programList()

                load_page()

                courseList()
                staffList()
                studentlevel()

                load_page()

                previousPage.NavigateUrl = String.Format("~/admin_daftar_koordinator.aspx?admin_ID=" + Request.QueryString("admin_ID"))
                txtbreadcrum1.Text = "Edit Coordinator"

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT * from coordinator where coordinator_ID = '" & Request.QueryString("coordinator_ID") & "'"

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
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
            Else
                ddlyear.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("stf_ID")) Then
                ddlstaff.SelectedValue = ds.Tables(0).Rows(0).Item("stf_ID")
            Else
                ddlstaff.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("coordinator_Level")) Then
                ddllevel.SelectedValue = ds.Tables(0).Rows(0).Item("coordinator_Level")
            Else
                ddllevel.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("course_Name")) Then
                ddlcourse.SelectedValue = ds.Tables(0).Rows(0).Item("course_Name")

                subjectList()
            Else
                ddlcourse.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("subject_Name")) Then
                ddlsubject.SelectedValue = ds.Tables(0).Rows(0).Item("subject_Name")
            Else
                ddlsubject.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("program")) Then
                ddlStream.SelectedValue = ds.Tables(0).Rows(0).Item("program")
            Else
                ddlStream.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("campus")) Then
                ddlCampus.SelectedValue = ds.Tables(0).Rows(0).Item("campus")
            Else
                ddlCampus.SelectedValue = ""
            End If
        End If
    End Sub

    Private Sub studentlevel()
        strSQL = "SELECT Parameter from setting where Type = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddllevel.DataSource = ds
            ddllevel.DataTextField = "Parameter"
            ddllevel.DataValueField = "Parameter"
            ddllevel.DataBind()
            ddllevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub yearList()
        strSQL = "SELECT Parameter from setting where Type = 'Year'"
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

        Catch ex As Exception
        End Try
    End Sub

    Private Sub campusList()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            If Session("SchoolCampus") = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' and Value = 'APP'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Pusat Campus' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlCampus.DataSource = levds
            ddlCampus.DataValueField = "Value"
            ddlCampus.DataTextField = "Parameter"
            ddlCampus.DataBind()
            ddlCampus.Items.Insert(0, New ListItem("Select Institutions", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub programList()
        Try
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            If ddlCampus.SelectedValue = "APP" Then
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
            Else
                strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
            End If

            Dim sqlLevelDA As New SqlDataAdapter(strSQL, objConn)

            Dim levds As DataSet = New DataSet
            sqlLevelDA.Fill(levds, "LevTable")

            ddlStream.DataSource = levds
            ddlStream.DataValueField = "Value"
            ddlStream.DataTextField = "Parameter"
            ddlStream.DataBind()
            ddlStream.Items.Insert(0, New ListItem("Select Program", String.Empty))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub staffList()
        strSQL = "SELECT staff_Name,stf_ID from staff_Info where staff_Status = 'Access' and staff_Name NOT LIKE '%araken%' and staff_Campus = '" & ddlCampus.SelectedValue & "' order by staff_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstaff.DataSource = ds
            ddlstaff.DataTextField = "staff_Name"
            ddlstaff.DataValueField = "stf_ID"
            ddlstaff.DataBind()
            ddlstaff.Items.Insert(0, New ListItem("Select Staff", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub courseList()
        strSQL = "SELECT distinct course_Name from subject_info where subject_year = '" & ddlyear.SelectedValue & "' and course_Program = '" & ddlStream.SelectedValue & "' and subject_Campus = '" & ddlCampus.SelectedValue & "' order by course_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlcourse.DataSource = ds
            ddlcourse.DataTextField = "course_Name"
            ddlcourse.DataValueField = "course_Name"
            ddlcourse.DataBind()
            ddlcourse.Items.Insert(0, New ListItem("Select Course", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub subjectList()

        Dim checkSubjectType As String = "SELECT distinct subject_type from subject_info where course_Name = '" & ddlcourse.SelectedValue & "' and subject_year = '" & ddlyear.SelectedValue & "' and course_Program = '" & ddlStream.SelectedValue & "' and subject_Campus = '" & ddlCampus.SelectedValue & "'"
        strRet = oCommon.getFieldValue(checkSubjectType)

        If strRet = "Compulsory" Then
            ddlsubject.Enabled = False
        Else
            ddlsubject.Enabled = True
        End If

        strSQL = "SELECT distinct subject_Name from subject_info where course_Name = '" & ddlcourse.SelectedValue & "' and subject_year = '" & ddlyear.SelectedValue & "' and subject_StudentYear = '" & ddllevel.SelectedValue & "' and course_Program = '" & ddlStream.SelectedValue & "' and subject_Campus = '" & ddlCampus.SelectedValue & "' order by subject_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlsubject.DataSource = ds
            ddlsubject.DataTextField = "subject_Name"
            ddlsubject.DataValueField = "subject_Name"
            ddlsubject.DataBind()
            ddlsubject.Items.Insert(0, New ListItem("Select Subject", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlcourse_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcourse.SelectedIndexChanged
        subjectList()
    End Sub

    Protected Sub ddlCampus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCampus.SelectedIndexChanged
        Try
            courseList()
            staffList()
            programList()
            subjectList()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlStream_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStream.SelectedIndexChanged
        Try
            courseList()
            subjectList()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        courseList()
        subjectList()
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        If ddlcourse.SelectedValue <> "" And ddlcourse.SelectedValue <> "Bahasa Antarabangsa" And ddlcourse.SelectedValue <> "AP Courses" Then

            'UPDATE
            strSQL = "UPDATE coordinator SET stf_ID='" & ddlstaff.SelectedValue & "',course_Name ='" & ddlcourse.SelectedValue & "',subject_Name ='" & ddlcourse.SelectedValue & "',year ='" & ddlyear.SelectedValue & "',coordinator_Level ='" & ddllevel.SelectedValue & "',program ='" & ddlStream.SelectedValue & "',campus ='" & ddlCampus.SelectedValue & "' WHERE coordinator_ID ='" & Request.QueryString("coordinator_ID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        ElseIf ddlcourse.SelectedValue = "Bahasa Antarabangsa" Or ddlcourse.SelectedValue > "AP Courses" Then

            'UPDATE
            strSQL = "UPDATE coordinator SET stf_ID='" & ddlstaff.SelectedValue & "',course_Name ='" & ddlcourse.SelectedValue & "',subject_Name ='" & ddlsubject.SelectedValue & "',year ='" & ddlyear.SelectedValue & "',coordinator_Level ='" & ddllevel.SelectedValue & "',program ='" & ddlStream.SelectedValue & "',campus ='" & ddlCampus.SelectedValue & "' WHERE coordinator_ID ='" & Request.QueryString("coordinator_ID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If

        If strRet = "0" Then
            ShowMessage(" Update Coordinator", MessageType.Success)
        Else
            ShowMessage(" Unsuccessful Update Coordinator", MessageType.Error)
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