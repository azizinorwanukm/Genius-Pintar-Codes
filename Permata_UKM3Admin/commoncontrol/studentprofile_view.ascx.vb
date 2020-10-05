Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class studentprofile_view1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strSchoolID As String
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim newname As String = Request.QueryString("studentid") & ".jpg"

        imgStudent.ImageUrl = "~/stdnt_img/" & newname
        Try
            If Not IsPostBack Then
                LoadPage()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadPage()
        Dim decryptid As String = Request.QueryString("studentid")
        Dim gender As Integer
        Dim status As String
        strSQL = "SELECT top 1 * FROM student_profile WHERE std_id ='" & decryptid & "'"
        ''debug
        ''Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Mykad")) Then
                    lblMYKAD.Text = ds.Tables(0).Rows(0).Item("student_Mykad")
                Else
                    lblMYKAD.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                    lblStudentFullname.Text = ds.Tables(0).Rows(0).Item("student_Name")
                Else
                    lblStudentFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_sex")) Then
                    gender = ds.Tables(0).Rows(0).Item("student_sex")
                    If gender = 1 Then
                        lblStudentGender.Text = "Lelaki"
                    ElseIf gender = 0 Then
                        lblStudentGender.Text = "Perempuan"
                    End If
                Else
                    lblStudentGender.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_day")) Then
                    lblStudentDOB.Text = ds.Tables(0).Rows(0).Item("student_day")
                Else
                    lblStudentDOB.Text = "--"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Month")) Then
                    lblStudentDOB.Text += "-" & ds.Tables(0).Rows(0).Item("student_Month")
                Else
                    lblStudentDOB.Text = "--"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Year")) Then
                    lblStudentDOB.Text += "-" & ds.Tables(0).Rows(0).Item("student_Year")
                Else
                    lblStudentDOB.Text = "--"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_form")) Then
                    lblStudentForm.Text = ds.Tables(0).Rows(0).Item("student_form")
                Else
                    lblStudentForm.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Race")) Then
                    lblStudentRace.Text = ds.Tables(0).Rows(0).Item("student_Race")
                Else
                    lblStudentRace.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Religion")) Then
                    lblStudentReligion.Text = ds.Tables(0).Rows(0).Item("student_Religion")
                Else
                    lblStudentReligion.Text = ""
                End If

                ''--continue
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Address")) Then
                    lblStudentAddress1.Text = ds.Tables(0).Rows(0).Item("student_Address")
                Else
                    lblStudentAddress1.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_PostalCode")) Then
                    lblStudentPostcode.Text = ds.Tables(0).Rows(0).Item("student_PostalCode")
                Else
                    lblStudentPostcode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_City")) Then
                    lblStudentCity.Text = ds.Tables(0).Rows(0).Item("student_City")
                Else
                    lblStudentCity.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_State")) Then
                    lblStudentState.Text = ds.Tables(0).Rows(0).Item("student_State")
                Else
                    lblStudentState.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Country")) Then
                    lblStudentCountry.Text = ds.Tables(0).Rows(0).Item("student_Country")
                Else
                    lblStudentCountry.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Email")) Then
                    lblStudentEmail.Text = ds.Tables(0).Rows(0).Item("student_Email")
                Else
                    lblStudentEmail.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_FonNo")) Then
                    lblStudentContactNo.Text = ds.Tables(0).Rows(0).Item("student_FonNo")
                Else
                    lblStudentContactNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Rapcs_update")) Then
                    status = ds.Tables(0).Rows(0).Item("Rapcs_update")
                    If status = "Y" Then
                        lbl_insRa.Text = "Ya"
                    ElseIf status = "N" Then
                        lbl_insRa.Text = "Tidak"
                    Else
                        lbl_insRa.Text = "Tiada Maklumat"
                    End If
                End If

                status = ""

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kpp_update")) Then
                    status = ds.Tables(0).Rows(0).Item("Kpp_update")
                    If status = "Y" Then
                        lbl_insKpp.Text = "Ya"
                    ElseIf status = "N" Then
                        lbl_insKpp.Text = "Tidak"

                    Else
                        lbl_insKpp.Text = "Tiada Maklumat"
                    End If
                End If
                status = ""

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Ppcs_update")) Then
                    status = ds.Tables(0).Rows(0).Item("Ppcs_update")
                    If status = "Y" Then
                        lbl_insPpcs.Text = "Ya"
                    ElseIf status = "N" Then
                        lbl_insPpcs.Text = "Tidak"

                    Else
                        lbl_insPpcs.Text = "Tiada Maklumat"
                    End If
                End If

                ''--load initial photo here
                ''imgStudent.ImageUrl = "~/ShowImage.ashx?studentid=" & Request.QueryString("studentid")

            End If

        Catch ex As Exception
            ''--display on screen
            'lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEdit.Click
        Select Case getUserProfile_UserType()
            Case "Admin"
                Response.Redirect("ukm3_admin.studentupdate.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
            Case Else
                '--lblMsg.Text = "Invalid user type:" & getUserProfile_UserType()
        End Select
    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class