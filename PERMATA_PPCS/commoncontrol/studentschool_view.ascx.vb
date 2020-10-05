Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class studentschool_view
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        schoolprofile_view()
        getStudentSchool_Start_End()

        setAccessRight()
    End Sub

    Private Sub setAccessRight()

        Select Case Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
            Case "ADMIN"
                lnkEdit.Visible = True
            Case "SUBADMIN"
                lnkEdit.Visible = False
            Case Else
                lnkEdit.Visible = False
        End Select

    End Sub

    Private Sub schoolprofile_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT * FROM SchoolProfile WHERE SchoolID='" & getSchoolID() & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolName")) Then
                    lblSchoolName.Text = MyTable.Rows(nRows).Item("SchoolName").ToString
                Else
                    lblSchoolName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCode")) Then
                    lblSchoolCode.Text = MyTable.Rows(nRows).Item("SchoolCode").ToString
                Else
                    lblSchoolCode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolAddress")) Then
                    lblSchoolAddress.Text = MyTable.Rows(nRows).Item("SchoolAddress").ToString
                Else
                    lblSchoolAddress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolPostcode")) Then
                    lblSchoolPostcode.Text = MyTable.Rows(nRows).Item("SchoolPostcode").ToString
                Else
                    lblSchoolPostcode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCity")) Then
                    lblSchoolCity.Text = MyTable.Rows(nRows).Item("SchoolCity").ToString
                Else
                    lblSchoolCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolState")) Then
                    lblSchoolState.Text = MyTable.Rows(nRows).Item("SchoolState").ToString
                Else
                    lblSchoolState.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolType")) Then
                    lblSchoolType.Text = MyTable.Rows(nRows).Item("SchoolType").ToString
                Else
                    lblSchoolType.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub getStudentSchool_Start_End()

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT * FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "' Order By StudentSchoolID DESC"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then

                If Not IsDBNull(MyTable.Rows(nRows).Item("StartDate")) Then
                    lblStartDate.Text = MyTable.Rows(nRows).Item("StartDate").ToString
                Else
                    lblStartDate.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("EndDate")) Then
                    lblEndDate.Text = MyTable.Rows(nRows).Item("EndDate").ToString
                Else
                    lblEndDate.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try


    End Sub

    Private Function getSchoolID() As String
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE StudentID='" & Request.QueryString("studentid") & "' Order By StudentSchoolID DESC"
        strRet = oCommon.getFieldValue(strSQL)
        lblSchoolID.Text = strRet

        Return strRet
    End Function


    Private Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEdit.Click
        Response.Redirect("schoolprofile.search.change.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))

    End Sub
End Class