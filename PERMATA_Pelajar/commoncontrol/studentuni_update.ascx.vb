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
            master_UniLevel()

            master_Dobyear_UniStart()
            ddlUniStartYear.Text = Now.Year - 5

            master_Dobyear_UniEnd()
            ddlUniEndYear.Text = Now.Year

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

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub master_UniLevel()
        strSQL = "SELECT UniLevel FROM master_UniLevel WITH (NOLOCK) ORDER BY UniLevelID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUniLevel.DataSource = ds
            ddlUniLevel.DataTextField = "UniLevel"
            ddlUniLevel.DataValueField = "UniLevel"
            ddlUniLevel.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub master_Dobyear_UniStart()
        strSQL = "SELECT DOB_Year FROM master_Dobyear WITH (NOLOCK) ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUniStartYear.DataSource = ds
            ddlUniStartYear.DataTextField = "DOB_Year"
            ddlUniStartYear.DataValueField = "DOB_Year"
            ddlUniStartYear.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub master_Dobyear_UniEnd()
        strSQL = "SELECT DOB_Year FROM master_Dobyear WITH (NOLOCK) ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUniEndYear.DataSource = ds
            ddlUniEndYear.DataTextField = "DOB_Year"
            ddlUniEndYear.DataValueField = "DOB_Year"
            ddlUniEndYear.DataBind()

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
        strSQL = "SELECT * FROM StudentUni WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "' ORDER BY StudentUniID DESC"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                '--StudentID,UniName,UniCountry,UniCourse,UniTajaan,UniLevel,UniStartYear,UniEndYear
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

                '--new field request by Dr Siti 20150521
                If Not IsDBNull(MyTable.Rows(nRows).Item("UniTajaan")) Then
                    txtUniTajaan.Text = MyTable.Rows(nRows).Item("UniTajaan").ToString
                Else
                    txtUniTajaan.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("UniLevel")) Then
                    ddlUniLevel.Text = MyTable.Rows(nRows).Item("UniLevel").ToString
                Else
                    ddlUniLevel.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("UniStartYear")) Then
                    ddlUniStartYear.Text = MyTable.Rows(nRows).Item("UniStartYear").ToString
                Else
                    ddlUniStartYear.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("UniEndYear")) Then
                    ddlUniEndYear.Text = MyTable.Rows(nRows).Item("UniEndYear").ToString
                Else
                    ddlUniEndYear.Text = ""
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
                Exit Sub
            End If

            strSQL = "SELECT StudentID FROM StudentUni WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "'"
            If oCommon.isExist(strSQL) = False Then
                strRet = StudentUni_insert()
            Else
                strRet = StudentUni_update()
            End If

            If strRet = "0" Then
                lblMsg.Text = "Berjaya mengemaskini maklumat Universiti Pelajar."
            Else
                lblMsg.Text = "System error: " & strRet
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function StudentUni_update() As String
        strSQL = "UPDATE StudentUni WITH (UPDLOCK) SET UniName='" & oCommon.FixSingleQuotes(txtUniName.Text.ToUpper) & "',UniCountry='" & ddlUniCountry.Text & "',UniCourse='" & oCommon.FixSingleQuotes(txtUniCourse.Text.ToUpper) & "',UniTajaan='" & oCommon.FixSingleQuotes(txtUniTajaan.Text.ToUpper) & "',UniLevel='" & oCommon.FixSingleQuotes(ddlUniLevel.Text) & "',UniStartYear='" & oCommon.FixSingleQuotes(ddlUniStartYear.Text) & "',UniEndYear='" & oCommon.FixSingleQuotes(ddlUniEndYear.Text) & "' WHERE StudentID='" & CType(Session.Item("permata_studentid"), String) & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''log
        oCommon.TransactionLog("studentuni_update", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress, CType(Session.Item("permata_mykad"), String))

        Return strRet
    End Function

    Private Function StudentUni_insert() As String
        strSQL = "INSERT INTO StudentUni (StudentID,UniName,UniCountry,UniCourse,UniTajaan,UniLevel,UniStartYear,UniEndYear) " &
        "VALUES ('" & CType(Session.Item("permata_studentid"), String) & "','" & oCommon.FixSingleQuotes(txtUniName.Text.ToUpper) & "','" & ddlUniCountry.Text & "','" & oCommon.FixSingleQuotes(txtUniCourse.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtUniTajaan.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(ddlUniLevel.Text) & "','" & oCommon.FixSingleQuotes(ddlUniStartYear.Text) & "','" & oCommon.FixSingleQuotes(ddlUniEndYear.Text) & "')"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''log
        oCommon.TransactionLog("studentuni_insert", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress, CType(Session.Item("permata_mykad"), String))

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
        Response.Redirect("student.studentprofile.view.aspx")

    End Sub

End Class