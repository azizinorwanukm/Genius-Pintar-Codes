Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class parentprofile_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strSchoolID As String
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            parentprofile_view()

        End If

    End Sub

    Private Sub parentprofile_view()
        Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim decryptid As String = oDes.DecryptData(Request.QueryString("studentid"))

        strSQL = "SELECT top 1 * FROM parent_profile WHERE std_id='" & decryptid & "' "
        '--debug
        'Response.Write("strSQL:" & strSQL)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                ''--parent info
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Father_Mykad")) Then
                    lblFatherMYKADNo.Text = ds.Tables(0).Rows(0).Item("Father_Mykad")
                Else
                    lblFatherMYKADNo.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Father_Name")) Then
                    lblFatherFullname.Text = MyTable.Rows(nRows).Item("Father_Name").ToString
                Else
                    lblFatherFullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Father_Job")) Then
                    lblFatherJob.Text = MyTable.Rows(nRows).Item("Father_Job").ToString
                Else
                    lblFatherJob.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Father_education")) Then
                    lblFatherEducation.Text = MyTable.Rows(nRows).Item("Father_education").ToString
                Else
                    lblFatherEducation.Text = ""
                End If

                ''mother info
                If Not IsDBNull(MyTable.Rows(nRows).Item("Mother_Mykad")) Then
                    lblMotherMYKADNo.Text = MyTable.Rows(nRows).Item("Mother_Mykad").ToString
                Else
                    lblMotherMYKADNo.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Mother_Name")) Then
                    lblMotherFullname.Text = MyTable.Rows(nRows).Item("Mother_Name").ToString
                Else
                    lblMotherFullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Mother_Job")) Then
                    lblMotherJob.Text = MyTable.Rows(nRows).Item("Mother_Job").ToString
                Else
                    lblMotherJob.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Mother_education")) Then
                    lblMotherEducation.Text = MyTable.Rows(nRows).Item("Mother_education").ToString
                Else
                    lblMotherEducation.Text = ""
                End If

                ''--family info. remove. Dr Siti request 20131124
                If Not IsDBNull(MyTable.Rows(nRows).Item("Family_income")) Then
                    lblFamilyIncome.Text = MyTable.Rows(nRows).Item("Family_income").ToString
                Else
                    lblFamilyIncome.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("JumlahAnak")) Then
                    lblJumlahAnak.Text = MyTable.Rows(nRows).Item("JumlahAnak").ToString
                Else
                    lblJumlahAnak.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Family_ContactNo")) Then
                    lblFamilyContactNo.Text = MyTable.Rows(nRows).Item("Family_ContactNo").ToString
                Else
                    lblFamilyContactNo.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Family_ContactNoibu")) Then
                    lblFamilyContactNoIbu.Text = MyTable.Rows(nRows).Item("Family_ContactNoibu").ToString
                Else
                    lblFamilyContactNoIbu.Text = ""
                End If



            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEdit.Click
        Select Case getUserProfile_UserType()
            Case "Admin"
                Response.Redirect("ukm3_admin.parentupdate.aspx?studentid=" & Request.QueryString("studentid"))
            Case Else
                '--lblMsg.Text = "Invalid user type:" & getUserProfile_UserType()
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class