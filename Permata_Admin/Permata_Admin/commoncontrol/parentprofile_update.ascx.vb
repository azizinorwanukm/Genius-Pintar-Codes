Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class Parentprofile_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                LoadPage()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoadPage()
        strSQL = "SELECT * FROM ParentProfile Where StudentID='" & Request.QueryString("studentid") & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FatherMYKADNo")) Then
                    txtFatherMYKADNo.Text = ds.Tables(0).Rows(0).Item("FatherMYKADNo")
                Else
                    txtFatherMYKADNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FatherFullname")) Then
                    txtFatherFullname.Text = ds.Tables(0).Rows(0).Item("FatherFullname")
                Else
                    txtFatherFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FatherJob")) Then
                    txtFatherJob.Text = ds.Tables(0).Rows(0).Item("FatherJob")
                Else
                    txtFatherJob.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FatherEducation")) Then
                    selFatherEducation.Value = ds.Tables(0).Rows(0).Item("FatherEducation")
                Else
                    selFatherEducation.Value = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MotherMYKADNo")) Then
                    txtMotherMYKADNo.Text = ds.Tables(0).Rows(0).Item("MotherMYKADNo")
                Else
                    txtMotherMYKADNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MotherFullname")) Then
                    txtMotherFullname.Text = ds.Tables(0).Rows(0).Item("MotherFullname")
                Else
                    txtMotherFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MotherJob")) Then
                    txtMotherJob.Text = ds.Tables(0).Rows(0).Item("MotherJob")
                Else
                    txtMotherJob.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MotherEducation")) Then
                    selMotherEducation.Value = ds.Tables(0).Rows(0).Item("MotherEducation")
                Else
                    selMotherEducation.Value = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FamilyIncome")) Then
                    selFamilyIncome.Value = ds.Tables(0).Rows(0).Item("FamilyIncome")
                Else
                    selFamilyIncome.Value = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("JumlahAnak")) Then
                    selJumlahAnak.Value = ds.Tables(0).Rows(0).Item("JumlahAnak")
                Else
                    selJumlahAnak.Value = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("FamilyContactNo")) Then
                    txtFamilyContactNo.Text = ds.Tables(0).Rows(0).Item("FamilyContactNo")
                Else
                    txtFamilyContactNo.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("FamilyContactNoIbu")) Then
                    txtFamilyContactNoIbu.Text = MyTable.Rows(nRows).Item("FamilyContactNoIbu").ToString
                Else
                    txtFamilyContactNoIbu.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ParentID")) Then
                    lblParentID.Text = ds.Tables(0).Rows(0).Item("ParentID")
                Else
                    lblParentID.Text = ""
                End If

            End If

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If ValidatePage() = False Then
                Exit Sub
            End If

            strSQL = "SELECT ParentID FROM ParentProfile WHERE StudentID='" & Request.QueryString("studentid") & "'"
            If oCommon.isExist(strSQL) = True Then
                ''--update only
                strRet = Parentprofile_update()
            Else
                ''parentprofile not created during creation. not mandatory anymore
                strRet = ParentProfile_create()
            End If

            If strRet = "0" Then
                lblMsg.Text = "Berjaya mengemaskini maklumat ibubapa/penjaga."
            Else
                lblMsg.Text = "Gagal mengemaskini maklumat ibubapa/penjaga. " & strRet
            End If

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

        End Try

    End Sub

    Private Function Parentprofile_update() As String
        strSQL = "UPDATE ParentProfile WITH (UPDLOCK) SET IsUpdated='Y',FatherMYKADNo='" & oCommon.FixSingleQuotes(txtFatherMYKADNo.Text) & "',FatherFullname='" & oCommon.FixSingleQuotes(txtFatherFullname.Text.ToUpper) & "',FatherJob='" & oCommon.FixSingleQuotes(txtFatherJob.Text.ToUpper) & "',FatherEducation='" & selFatherEducation.Value & "',MotherMYKADNo='" & oCommon.FixSingleQuotes(txtMotherMYKADNo.Text) & "',MotherFullname='" & oCommon.FixSingleQuotes(txtMotherFullname.Text.ToUpper) & "',MotherJob='" & oCommon.FixSingleQuotes(txtMotherJob.Text.ToUpper) & "',MotherEducation='" & selMotherEducation.Value & "',FamilyIncome='" & selFamilyIncome.Value & "',JumlahAnak=" & selJumlahAnak.Value & ",FamilyContactNo='" & oCommon.FixSingleQuotes(txtFamilyContactNo.Text) & "',FamilyContactNoIbu='" & oCommon.FixSingleQuotes(txtFamilyContactNoIbu.Text) & "' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''Response.Write("debug:" & strRet)
        ''lblMsg.Text += "debug:" & strRet & ":" & strSQL

        ''log
        oCommon.TransactionLog("parentprofile_update", oCommon.FixSingleQuotes(strSQL), Request.UserHostAddress, CType(Session.Item("permata_admin"), String))

        Return strRet
    End Function

    Private Function ParentProfile_create() As String
        Dim strParentID As String
        strParentID = System.Guid.NewGuid.ToString

        strSQL = "INSERT INTO ParentProfile (StudentID,ParentID,FatherMYKADNo,FatherFullname,FatherJob,FatherEducation,MotherMYKADNo,MotherFullname,MotherJob,MotherEducation,FamilyIncome,JumlahAnak,FamilyContactNo,FamilyContactNoIbu,IsUpdated) " &
        "VALUES ('" & Request.QueryString("studentid") & "','" & strParentID & "','" & oCommon.FixSingleQuotes(txtFatherMYKADNo.Text) & "','" & oCommon.FixSingleQuotes(txtFatherFullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtFatherJob.Text.ToUpper) & "','" & selFatherEducation.Value & "','" & oCommon.FixSingleQuotes(txtMotherMYKADNo.Text) & "','" & oCommon.FixSingleQuotes(txtMotherFullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtMotherJob.Text.ToUpper) & "','" & selMotherEducation.Value & "','" & selFamilyIncome.Value & "'," & selJumlahAnak.Value & ",'" & oCommon.FixSingleQuotes(txtFamilyContactNo.Text) & "','" & oCommon.FixSingleQuotes(txtFamilyContactNoIbu.Text) & "','Y')"

        strRet = oCommon.ExecuteSQL(strSQL)

        Return strRet
    End Function


    Private Function ValidatePage() As Boolean

        ''--father
        If txtFatherFullname.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Nama Bapa/Penjaga!"
            txtFatherFullname.Focus()
            Return False
        End If

        If txtFatherJob.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Pekerjaan Bapa/Penjaga!"
            txtFatherJob.Focus()
            Return False
        End If

        If selFatherEducation.Value.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Tahap Pendidikan Bapa/Penjaga!"
            selFatherEducation.Focus()
            Return False
        End If

        ''--mother
        If txtMotherMYKADNo.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. MYKAD Ibu!"
            txtMotherMYKADNo.Focus()
            Return False
        End If

        ''--to allow non malaysian mothers
        'If oCommon.isNumeric(txtMotherMYKADNo.Text) = False Then
        '    lblMsg.Text = "Invalid MYKAD format. Fill in numbers only! [0 - 9]"
        '    txtMotherMYKADNo.Focus()
        '    Return False
        'End If

        If txtMotherFullname.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Nama Ibu!"
            txtMotherFullname.Focus()
            Return False
        End If

        If txtMotherJob.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Pekerjaan  Ibu!"
            txtMotherJob.Focus()
            Return False
        End If

        If selMotherEducation.Value.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Tahap Pendidikan Ibu!"
            selMotherEducation.Focus()
            Return False
        End If

        ''--family
        'If selFamilyIncome.Value.Length = 0 Then
        '    lblMsg.Text = "Sila masukkan medan ini. Pendapatan Sekeluarga!"
        '    selFamilyIncome.Focus()
        '    Return False
        'End If

        If txtFamilyContactNo.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Nombor Talipon Bapa!"
            txtFamilyContactNo.Focus()
            Return False
        End If

        If txtFamilyContactNoIbu.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan medan ini. Nombor Talipon Ibu!"
            txtFamilyContactNoIbu.Focus()
            Return False
        End If

        Return True
    End Function


    Protected Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkStudentProfileView.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "ADMINOP"
                Response.Redirect("studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case Else
                lblMsg.Text = "Invalid user type:" & getUserProfile_UserType()
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class