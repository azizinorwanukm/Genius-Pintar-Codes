Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class studentprofile_view1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strSchoolID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                LoadPage()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadPage()
        strSQL = "SELECT * FROM StudentProfile WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "'"
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentForm")) Then
                    lblStudentForm.Text = ds.Tables(0).Rows(0).Item("StudentForm")
                Else
                    lblStudentForm.Text = ""
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

                ''--Kebolehan Berbahasa
                If Not IsDBNull(MyTable.Rows(nRows).Item("TalkBM")) Then
                    chkTalkBM.Checked = MyTable.Rows(nRows).Item("TalkBM").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("TalkBI")) Then
                    chkTalkBI.Checked = MyTable.Rows(nRows).Item("TalkBI").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("TalkMan")) Then
                    chkTalkMan.Checked = MyTable.Rows(nRows).Item("TalkMan").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("TalkTamil")) Then
                    chkTalkTamil.Checked = MyTable.Rows(nRows).Item("TalkTamil").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("TalkArab")) Then
                    chkTalkArab.Checked = MyTable.Rows(nRows).Item("TalkArab").ToString
                End If

                ''--Kebolehan Berbahasa
                If Not IsDBNull(MyTable.Rows(nRows).Item("WriteBM")) Then
                    chkWriteBM.Checked = MyTable.Rows(nRows).Item("WriteBM").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("WriteBI")) Then
                    chkWriteBI.Checked = MyTable.Rows(nRows).Item("WriteBI").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("WriteMan")) Then
                    chkWriteMan.Checked = MyTable.Rows(nRows).Item("WriteMan").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("WriteTamil")) Then
                    chkWriteTamil.Checked = MyTable.Rows(nRows).Item("WriteTamil").ToString
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("WriteArab")) Then
                    chkWriteArab.Checked = MyTable.Rows(nRows).Item("WriteArab").ToString
                End If


                ''--continue
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentAddress1")) Then
                    lblStudentAddress1.Text = ds.Tables(0).Rows(0).Item("StudentAddress1")
                Else
                    lblStudentAddress1.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentAddress2")) Then
                    lblStudentAddress2.Text = ds.Tables(0).Rows(0).Item("StudentAddress2")
                Else
                    lblStudentAddress2.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentPostcode")) Then
                    lblStudentPostcode.Text = ds.Tables(0).Rows(0).Item("StudentPostcode")
                Else
                    lblStudentPostcode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentCity")) Then
                    lblStudentCity.Text = ds.Tables(0).Rows(0).Item("StudentCity")
                Else
                    lblStudentCity.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentState")) Then
                    lblStudentState.Text = ds.Tables(0).Rows(0).Item("StudentState")
                Else
                    lblStudentState.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentCountry")) Then
                    lblStudentCountry.Text = ds.Tables(0).Rows(0).Item("StudentCountry")
                Else
                    lblStudentCountry.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentEmail")) Then
                    lblStudentEmail.Text = ds.Tables(0).Rows(0).Item("StudentEmail")
                Else
                    lblStudentEmail.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pwd")) Then
                    lblPwd.Text = ds.Tables(0).Rows(0).Item("Pwd")
                Else
                    lblPwd.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentContactNo")) Then
                    lblStudentContactNo.Text = ds.Tables(0).Rows(0).Item("StudentContactNo")
                Else
                    lblStudentContactNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kecenderungan")) Then
                    lblKecenderungan.Text = ds.Tables(0).Rows(0).Item("Kecenderungan")
                Else
                    lblKecenderungan.Text = ""
                End If

                '--for admin only
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StudentType")) Then
                    lblStudentType.Text = ds.Tables(0).Rows(0).Item("StudentType")
                Else
                    lblStudentType.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("AlumniID")) Then
                    lblAlumniID.Text = ds.Tables(0).Rows(0).Item("AlumniID")
                Else
                    lblAlumniID.Text = ""
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
            ''--display on screen
            'lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Sub lnkEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEdit.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.update.mykad.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.studentprofile.update.mykad.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
            Case "ADMINOP"
                Response.Redirect("studentprofile.update.mykad.aspx?lang=" & Request.QueryString("lang") & "&studentid=" & Request.QueryString("studentid"))
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