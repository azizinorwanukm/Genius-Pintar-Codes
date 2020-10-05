Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class studentprofile_view_header
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblMsg.Text = ""
            LoadPage()
        End If

    End Sub

    Private Sub LoadPage()
        strSQL = "SELECT * FROM StudentProfile Where StudentID='" & Request.QueryString("studentid") & "'"
        ''debug
        ''Response.Write(strSQL)

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
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MYKAD")) Then
                    lblMYKAD.Text = ds.Tables(0).Rows(0).Item("MYKAD")
                Else
                    lblMYKAD.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentFullname")) Then
                    lblStudentFullname.Text = ds.Tables(0).Rows(0).Item("StudentFullname")
                Else
                    lblStudentFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentGender")) Then
                    lblStudentGender.Text = ds.Tables(0).Rows(0).Item("StudentGender")
                Else
                    lblStudentGender.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DOB_Day")) Then
                    lblStudentDOB.Text = ds.Tables(0).Rows(0).Item("DOB_Day")
                Else
                    lblStudentDOB.Text = "--"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DOB_Month")) Then
                    lblStudentDOB.Text += "-" & ds.Tables(0).Rows(0).Item("DOB_Month")
                Else
                    lblStudentDOB.Text = "--"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DOB_Year")) Then
                    lblStudentDOB.Text += "-" & ds.Tables(0).Rows(0).Item("DOB_Year")
                Else
                    lblStudentDOB.Text = "--"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentRace")) Then
                    lblStudentRace.Text = ds.Tables(0).Rows(0).Item("StudentRace")
                Else
                    lblStudentRace.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentReligion")) Then
                    lblStudentReligion.Text = ds.Tables(0).Rows(0).Item("StudentReligion")
                Else
                    lblStudentReligion.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NoPelajar")) Then
                    lblNoPelajar.Text = ds.Tables(0).Rows(0).Item("NoPelajar")
                Else
                    lblNoPelajar.Text = ""
                End If

                ''--load initial photo here
                imgStudent.ImageUrl = "~/ShowImage.ashx?studentid=" & Request.QueryString("studentid")

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

End Class