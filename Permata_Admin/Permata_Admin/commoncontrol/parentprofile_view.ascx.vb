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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            parentprofile_view()

        End If

    End Sub

    Private Sub parentprofile_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        strSQL = "SELECT * FROM ParentProfile WHERE StudentID='" & Request.QueryString("studentid") & "' "
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FatherMYKADNo")) Then
                    lblFatherMYKADNo.Text = ds.Tables(0).Rows(0).Item("FatherMYKADNo")
                Else
                    lblFatherMYKADNo.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("FatherFullname")) Then
                    lblFatherFullname.Text = MyTable.Rows(nRows).Item("FatherFullname").ToString
                Else
                    lblFatherFullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("FatherJob")) Then
                    lblFatherJob.Text = MyTable.Rows(nRows).Item("FatherJob").ToString
                Else
                    lblFatherJob.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("FatherEducation")) Then
                    lblFatherEducation.Text = MyTable.Rows(nRows).Item("FatherEducation").ToString
                Else
                    lblFatherEducation.Text = ""
                End If

                ''mother info
                If Not IsDBNull(MyTable.Rows(nRows).Item("MotherMYKADNo")) Then
                    lblMotherMYKADNo.Text = MyTable.Rows(nRows).Item("MotherMYKADNo").ToString
                Else
                    lblMotherMYKADNo.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("MotherFullname")) Then
                    lblMotherFullname.Text = MyTable.Rows(nRows).Item("MotherFullname").ToString
                Else
                    lblMotherFullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("MotherJob")) Then
                    lblMotherJob.Text = MyTable.Rows(nRows).Item("MotherJob").ToString
                Else
                    lblMotherJob.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("MotherEducation")) Then
                    lblMotherEducation.Text = MyTable.Rows(nRows).Item("MotherEducation").ToString
                Else
                    lblMotherEducation.Text = ""
                End If

                ''--family info. remove. Dr Siti request 20131124
                If Not IsDBNull(MyTable.Rows(nRows).Item("FamilyIncome")) Then
                    lblFamilyIncome.Text = MyTable.Rows(nRows).Item("FamilyIncome").ToString
                Else
                    lblFamilyIncome.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("JumlahAnak")) Then
                    lblJumlahAnak.Text = MyTable.Rows(nRows).Item("JumlahAnak").ToString
                Else
                    lblJumlahAnak.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("FamilyContactNo")) Then
                    lblFamilyContactNo.Text = MyTable.Rows(nRows).Item("FamilyContactNo").ToString
                Else
                    lblFamilyContactNo.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("FamilyContactNoIbu")) Then
                    lblFamilyContactNoIbu.Text = MyTable.Rows(nRows).Item("FamilyContactNoIbu").ToString
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
            Case "ADMIN"
                Response.Redirect("admin.parentprofile.update.aspx?studentid=" & Request.QueryString("studentid"))
            Case "ADMINOP"
                Response.Redirect("parentprofile.update.aspx?studentid=" & Request.QueryString("studentid"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.parentprofile.update.aspx?studentid=" & Request.QueryString("studentid"))
            Case Else
                '--lblMsg.Text = "Invalid user type:" & getUserProfile_UserType()
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class