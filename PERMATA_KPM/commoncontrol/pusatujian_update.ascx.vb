Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization


Partial Public Class pusatujian_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SchoolState_list()
            examyear_list()
            ddlExamYear.Text = oCommon.getAppsettings("DefaultExamYear")

            pusatujian_view()
            setAccessRight()
        End If

    End Sub

    Private Sub setAccessRight()
        Select Case getUserProfile_UserType()
            Case "ADMIN"

            Case "SUBADMIN"

            Case "UKM"
                txtPusatName.Enabled = False
                txtPusatAddress.Enabled = False
                txtPusatPostcode.Enabled = False
                txtPusatCity.Enabled = False
                ddlSchoolState.Enabled = False
                txtPusatType.Enabled = False
                txtPusatPPD.Enabled = False
                txtPusatNoTel.Enabled = False
                txtPusatNoFax.Enabled = False
                txtPusatEmail.Enabled = False
                ddlExamYear.Enabled = False
                txtPusatJumlahKomp.Enabled = False
                txtPusatJumlahLab.Enabled = False

            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk fungsi ini. " & getUserProfile_UserType()
        End Select

    End Sub

    Private Sub SchoolState_list()
        strSQL = "SELECT SchoolState FROM SchoolState WITH (NOLOCK) WHERE SchoolState<>'UKM2-KPT' AND SchoolState <>'UKM2-ASASI'  ORDER BY SchoolStateID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSchoolState.DataSource = ds
            ddlSchoolState.DataTextField = "schoolstate"
            ddlSchoolState.DataValueField = "schoolstate"
            ddlSchoolState.DataBind()

            ddlSchoolState.Items.Add(New ListItem("ALL", "ALL"))
            strRet = getUserProfile_State()
            ddlSchoolState.SelectedValue = getUserProfile_State()
            If Not strRet = "ALL" Then
                ddlSchoolState.Enabled = False
            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":SchoolState_list:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub examyear_list()
        '--Limit examyear access
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "SUBADMIN"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"
            Case "KPT"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%KPT%' ORDER BY ExamYear"
            Case "ASASI"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '%ASASI%' ORDER BY ExamYear"
            Case "UKM"
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear LIKE '" & oCommon.getAppsettings("DefaultExamYear") & "%'  ORDER BY ExamYear"
            Case Else
                strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) WHERE ExamYear='" & oCommon.getAppsettings("DefaultExamYear") & "' ORDER BY ExamYear"
        End Select

        '--debug
        'Response.Write("examyear_list:" & strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "ExamYear"
            ddlExamYear.DataValueField = "ExamYear"
            ddlExamYear.DataBind()

            ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub pusatujian_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        ''--display PusatUjian profile
        strSQL = "SELECT * FROM PusatUjian WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatName")) Then
                    txtPusatName.Text = MyTable.Rows(nRows).Item("PusatName").ToString
                Else
                    txtPusatName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatAddress")) Then
                    txtPusatAddress.Text = MyTable.Rows(nRows).Item("PusatAddress").ToString
                Else
                    txtPusatAddress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatPostcode")) Then
                    txtPusatPostcode.Text = MyTable.Rows(nRows).Item("PusatPostcode").ToString
                Else
                    txtPusatPostcode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatCity")) Then
                    txtPusatCity.Text = MyTable.Rows(nRows).Item("PusatCity").ToString
                Else
                    txtPusatCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatState")) Then
                    ddlSchoolState.Text = MyTable.Rows(nRows).Item("PusatState").ToString
                Else
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatType")) Then
                    txtPusatType.Text = MyTable.Rows(nRows).Item("PusatType").ToString
                Else
                    txtPusatType.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatPPD")) Then
                    txtPusatPPD.Text = MyTable.Rows(nRows).Item("PusatPPD").ToString
                Else
                    txtPusatPPD.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatNoTel")) Then
                    txtPusatNoTel.Text = MyTable.Rows(nRows).Item("PusatNoTel").ToString
                Else
                    txtPusatNoTel.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatNoFax")) Then
                    txtPusatNoFax.Text = MyTable.Rows(nRows).Item("PusatNoFax").ToString
                Else
                    txtPusatNoFax.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatEmail")) Then
                    txtPusatEmail.Text = MyTable.Rows(nRows).Item("PusatEmail").ToString
                Else
                    txtPusatEmail.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatTahun")) Then
                    ddlExamYear.Text = MyTable.Rows(nRows).Item("PusatTahun").ToString
                Else

                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatJumlahLab")) Then
                    txtPusatJumlahLab.Text = MyTable.Rows(nRows).Item("PusatJumlahLab").ToString
                Else
                    txtPusatJumlahLab.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("PusatJumlahKomp")) Then
                    txtPusatJumlahKomp.Text = MyTable.Rows(nRows).Item("PusatJumlahKomp").ToString
                Else
                    txtPusatJumlahKomp.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Komen")) Then
                    txtKomen.Text = MyTable.Rows(nRows).Item("Komen").ToString
                Else
                    txtKomen.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        ''--validate screen
        If ValidatePage() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        End If

        strSQL = "UPDATE PusatUjian WITH (UPDLOCK) SET PusatName='" & oCommon.FixSingleQuotes(txtPusatName.Text) & "',PusatAddress='" & oCommon.FixSingleQuotes(txtPusatAddress.Text) & "',PusatPostcode='" & oCommon.FixSingleQuotes(txtPusatPostcode.Text) & "',PusatCity='" & oCommon.FixSingleQuotes(txtPusatCity.Text) & "',PusatState='" & ddlSchoolState.Text & "',PusatType='" & oCommon.FixSingleQuotes(txtPusatType.Text) & "',PusatPPD='" & oCommon.FixSingleQuotes(txtPusatPPD.Text) & "',PusatNoTel='" & oCommon.FixSingleQuotes(txtPusatNoTel.Text) & "',PusatNoFax='" & oCommon.FixSingleQuotes(txtPusatNoFax.Text) & "',PusatEmail='" & oCommon.FixSingleQuotes(txtPusatEmail.Text) & "',PusatTahun='" & ddlExamYear.Text & "',PusatJumlahLab='" & oCommon.FixSingleQuotes(txtPusatJumlahLab.Text) & "',PusatJumlahKomp='" & oCommon.FixSingleQuotes(txtPusatJumlahKomp.Text) & "',Komen='" & oCommon.FixSingleQuotes(txtKomen.Text) & "' WHERE PusatCode='" & Request.QueryString("pusatcode") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini Maklumat Pusat Ujian UKM2."
        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = strRet
        End If

    End Sub

    Private Sub ClearScreen()
        txtPusatName.Text = ""
        txtPusatAddress.Text = ""
        txtPusatPostcode.Text = ""
        txtPusatCity.Text = ""
        txtPusatType.Text = ""
        txtPusatNoTel.Text = ""
        txtPusatNoFax.Text = ""
        txtPusatJumlahLab.Text = 0
        txtPusatJumlahKomp.Text = ""

    End Sub

    Private Function ValidatePage() As Boolean
        If txtPusatName.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatName.Focus()
            Return False
        End If

        If txtPusatAddress.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatAddress.Focus()
            Return False
        End If

        If txtPusatPostcode.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatPostcode.Focus()
            Return False
        End If

        If IsNumeric(txtPusatPostcode.Text) = False Then
            lblMsg.Text = "Masukkan nombor sahaja."
            txtPusatPostcode.Focus()
            Return False
        End If

        If txtPusatCity.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatCity.Focus()
            Return False
        End If
        If txtPusatJumlahLab.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatJumlahLab.Focus()
            Return False
        End If
        If IsNumeric(txtPusatJumlahLab.Text) = False Then
            lblMsg.Text = "Masukkan nombor sahaja."
            txtPusatJumlahLab.Focus()
            Return False
        End If

        If txtPusatJumlahKomp.Text.Length = 0 Then
            lblMsg.Text = "Sila penuhkan medan yang wajib diisi. [Bertanda *]"
            txtPusatJumlahKomp.Focus()
            Return False
        End If
        If IsNumeric(txtPusatJumlahKomp.Text) = False Then
            lblMsg.Text = "Masukkan nombor sahaja."
            txtPusatJumlahKomp.Focus()
            Return False
        End If


        Return True
    End Function

    Protected Sub lnkPusatUjian_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkPusatUjian.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.list.aspx")
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.list.aspx")
            Case "UKM"
                Response.Redirect("ukm.pusatujian.list.aspx?func=0")
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk fungsi ini. " & getUserProfile_UserType()
        End Select


    End Sub
End Class