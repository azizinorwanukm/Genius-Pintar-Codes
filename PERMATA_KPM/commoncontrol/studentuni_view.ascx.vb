Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class studentuni_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        studentuni_view()
        setAccessRight()

    End Sub

    Private Sub setAccessRight()

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                lnkEdit.Visible = True
            Case "SUBADMIN"
                lnkEdit.Visible = False
            Case Else
                lnkEdit.Visible = False
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub studentuni_view()
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
                    lblUniName.Text = MyTable.Rows(nRows).Item("UniName").ToString
                Else
                    lblUniName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("UniCountry")) Then
                    lblUniCountry.Text = MyTable.Rows(nRows).Item("UniCountry").ToString
                Else
                    lblUniCountry.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("UniCourse")) Then
                    lblUniCourse.Text = MyTable.Rows(nRows).Item("UniCourse").ToString
                Else
                    lblUniCourse.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEdit.Click
        Response.Redirect("admin.studentuni.update.aspx?studentid=" & Request.QueryString("studentid"))

    End Sub

End Class