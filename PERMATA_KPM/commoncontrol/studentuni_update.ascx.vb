Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class studentuni_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            '--master_Country
            master_Country_list()

            StudentUni_view()
        End If

    End Sub

    Private Sub master_Country_list()
        strSQL = "SELECT Country FROM master_Country WITH (NOLOCK) ORDER BY CountryID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUniCountry.DataSource = ds
            ddlUniCountry.DataTextField = "Country"
            ddlUniCountry.DataValueField = "Country"
            ddlUniCountry.DataBind()

            '--ddlCountry.Items.Add(New ListItem("--SELECT--", "--SELECT--"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub


    Private Sub StudentUni_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)


        '--get the latest only
        strSQL = "SELECT * FROM StudentUni WHERE StudentID='" & Request.QueryString("studentid") & "' ORDER BY StudentUniID DESC"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("UniName")) Then
                    txtUniName.Text = MyTable.Rows(nRows).Item("UniName").ToString
                Else
                    txtUniName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("UniCountry")) Then
                    ddlUniCountry.Text = MyTable.Rows(nRows).Item("UniCountry").ToString
                Else
                    ddlUniCountry.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("UniCourse")) Then
                    txtUniCourse.Text = MyTable.Rows(nRows).Item("UniCourse").ToString
                Else
                    txtUniCourse.Text = ""
                End If
            Else
                ddlUniCountry.Text = "MALAYSIA"
            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            strSQL = "SELECT StudentID FROM StudentUni WHERE StudentID='" & Request.QueryString("studentid") & "'"
            If oCommon.isExist(strSQL) = False Then
                StudentUni_create()
            Else
                StudentUni_update_db()
            End If

            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini maklumat Universiti Pelajar."

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function StudentUni_update_db() As String
        strSQL = "UPDATE StudentUni WITH (UPDLOCK) SET UniName='" & oCommon.FixSingleQuotes(txtUniName.Text.ToUpper) & "',UniCountry='" & ddlUniCountry.Text & "',UniCourse='" & oCommon.FixSingleQuotes(txtUniCourse.Text.ToUpper) & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''log
        oCommon.TransactionLog("studentuni_update", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress, CType(Session.Item("kpmadmin_loginid"), String))

        Return strRet
    End Function

    Private Function StudentUni_create() As String

        strSQL = "INSERT INTO StudentUni (StudentID,UniName,UniCountry,UniCourse) " & _
        "VALUES ('" & Request.QueryString("studentid") & "','" & oCommon.FixSingleQuotes(txtUniName.Text.ToUpper) & "','" & ddlUniCountry.Text & "','" & oCommon.FixSingleQuotes(txtUniCourse.Text.ToUpper) & "')"

        strRet = oCommon.ExecuteSQL(strSQL)

        Return strRet
    End Function


    Private Function ValidatePage() As Boolean
        '--request by dr siti. 20131126. no validation required for admin only

        ''--father
        If txtUniName.Text.Length = 0 Then
            lblMsg.Text = "Please fill-in this field. Nama Universiti!"
            txtUniName.Focus()
            Return False
        End If

        Return True
    End Function


    Private Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkStudentProfileView.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "UKM"
                Response.Redirect("ukm.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "JPN"
                Response.Redirect("jpn.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "KPM"
                Response.Redirect("kpm.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "KPT"
                Response.Redirect("kpt.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "MRSM"
                Response.Redirect("mara.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "ASASI"
                Response.Redirect("asasi.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case Else
                lblMsg.Text = "Invalid usertype!"
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class