Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblTahun.Text = Request.QueryString("tahun")
                If ispelajar_exist() = False Then
                    lblMsg.Text = "Profil anda tiada pada tahun :" & lblTahun.Text
                    Exit Sub
                End If

                getKOKOEnd()
                setAccessRight()
                setKOKOEnd()

                koko_kelas_list()
                ddlKelas.SelectedValue = "0"

                koko_uniform_list()
                ddlUniform.SelectedValue = "0"

                koko_persatuan_list()
                ddlPersatuan.SelectedValue = "0"

                koko_sukan_list()
                ddlSukan.SelectedValue = "0"

                koko_rumahsukan_list()
                ddlRumahsukan.SelectedValue = "0"

                '--load default value if any
                koko_pelajar_koko_load()

                renang_load()

                '--inital count
                CountMax()

                getMandatory_Sukan()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Sub getMandatory_Sukan()
        'strSQL = "SELECT Nama FROM koko_kolejpermata WHERE IsMandatory='Y' AND Tahun='" & lblTahun.Text & "'"
        'lblMandatory_sukan.Text = oCommon.getRowValue(strSQL)

        lblMandatory_sukan.Text = "Sukan Renang adalah wajib : Tidak perlu mendaftar"

        '--debug 
        'Response.Write(strSQL)

    End Sub

    Private Sub renang_load()

        Dim strsql As String = "  select distinct koko_kelaskoko.kelas from koko_kumpulan
                                  left join koko_kelaskoko on koko_kumpulan.KelasKokoID = koko_kelaskoko.KelasKokoID
                                  where StudentID = '" & Request.QueryString("studentid") & "'
                                  and Tahun = '" & Now.Year & "'"
        Dim strret As String = oCommon.getFieldValue(strsql)

        lblRenang.Text = strret
    End Sub

    Private Sub setAccessRight()
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                'admin free to update
                lnkList.Visible = True
                ddlKelas.Enabled = True
            Case Else
                lnkList.Visible = False
                ddlKelas.Enabled = False
        End Select

    End Sub

    Private Sub getKOKOEnd()
        '-KOKOEND
        strSQL = "SELECT ConfigString FROM koko_config WHERE ConfigCODE='KOKOEND' AND Tahun='" & lblTahun.Text & "'"
        lblKOKOEND.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub setKOKOEnd()
        'yyyyMMdd
        Dim strToday As String = oCommon.getToday
        '--if more disable it
        If CInt(strToday) > CInt(lblKOKOEND.Text) Then
            ddlUniform.Enabled = False
            ddlPersatuan.Enabled = False
            ddlSukan.Enabled = False
            ddlRumahsukan.Enabled = False
        End If

        '--mior 20160118. sudah ada tak boleh edit RumahsukanID
        '' strSQL = "SELECT RumahsukanID FROM koko_pelajar WHERE Studentid='" & Request.QueryString("studentid") & "' AND Tahun='" & Request.QueryString("tahun") & "'"
        ''If Not oCommon.getFieldValue(strSQL) = "" Then
        ''ddlRumahsukan.Enabled = False
        ''End If

    End Sub

    Private Sub koko_kelas_list()
        strSQL = "SELECT * FROM koko_kelas WHERE Tahun='" & Request.QueryString("tahun") & "' ORDER BY Kelas ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKelas.DataSource = ds
            ddlKelas.DataTextField = "Kelas"
            ddlKelas.DataValueField = "KelasID"
            ddlKelas.DataBind()

            ddlKelas.Items.Add(New ListItem("--PILIH--", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    '----DDL KOKO
    Private Sub koko_uniform_list()
        strSQL = "SELECT KokoID,Nama FROM koko_kolejpermata WHERE Jenis='UNIFORM' AND Tahun='" & Request.QueryString("tahun") & "' ORDER BY Nama ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUniform.DataSource = ds
            ddlUniform.DataTextField = "Nama"
            ddlUniform.DataValueField = "KokoID"
            ddlUniform.DataBind()

            ddlUniform.Items.Add(New ListItem("--PILIH--", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_persatuan_list()
        strSQL = "SELECT KokoID,Nama FROM koko_kolejpermata WHERE Jenis='PERSATUAN' AND Tahun='" & Request.QueryString("tahun") & "' ORDER BY Nama ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPersatuan.DataSource = ds
            ddlPersatuan.DataTextField = "Nama"
            ddlPersatuan.DataValueField = "KokoID"
            ddlPersatuan.DataBind()

            ddlPersatuan.Items.Add(New ListItem("--PILIH--", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_sukan_list()
        strSQL = "SELECT KokoID,Nama FROM koko_kolejpermata WHERE Jenis='SUKAN' AND Tahun='" & Request.QueryString("tahun") & "' ORDER BY Nama ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSukan.DataSource = ds
            ddlSukan.DataTextField = "Nama"
            ddlSukan.DataValueField = "KokoID"
            ddlSukan.DataBind()

            ddlSukan.Items.Add(New ListItem("--PILIH--", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub koko_rumahsukan_list()
        strSQL = "SELECT KokoID,Nama FROM koko_kolejpermata WHERE Jenis='RUMAHSUKAN' AND Tahun='" & Request.QueryString("tahun") & "' ORDER BY Nama ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlRumahsukan.DataSource = ds
            ddlRumahsukan.DataTextField = "Nama"
            ddlRumahsukan.DataValueField = "KokoID"
            ddlRumahsukan.DataBind()

            ddlRumahsukan.Items.Add(New ListItem("--PILIH--", "0"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function ispelajar_exist() As Boolean
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT koko_pelajar.kokopelajarid FROM koko_pelajar"
        strWhere = " WHERE koko_pelajar.Tahun='" & Request.QueryString("tahun") & "'"
        strWhere += " AND koko_pelajar.StudentID='" & Request.QueryString("studentid") & "'"

        strSQL = tmpSQL & strWhere & strOrder
        Return oCommon.isExist(strSQL)

    End Function

    Private Sub koko_pelajar_koko_load()
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        '--PELAJAR INFO
        tmpSQL = "SELECT koko_pelajar.kokopelajarid,koko_pelajar.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.NoPelajar,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentGender,StudentProfile.StudentReligion,koko_pelajar.Tahun,koko_pelajar.KelasID,koko_kelas.Kelas,koko_pelajar.UniformID,koko_pelajar.PersatuanID,koko_pelajar.SukanID,koko_pelajar.RumahsukanID,koko_pelajar.StatusTawaran FROM koko_pelajar"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID"
        strWhere = " WHERE koko_pelajar.Tahun='" & Request.QueryString("tahun") & "'"
        strWhere += " AND koko_pelajar.StudentID='" & Request.QueryString("studentid") & "'"

        strSQL = tmpSQL & strWhere & strOrder
        '--debug
        'Response.Write(strSQL)

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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    lblTahun.Text = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    lblTahun.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KelasID")) Then
                    ddlKelas.SelectedValue = ds.Tables(0).Rows(0).Item("KelasID")
                Else
                    ddlKelas.SelectedValue = "0"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UniformID")) Then
                    ddlUniform.SelectedValue = ds.Tables(0).Rows(0).Item("UniformID")
                Else
                    ddlUniform.SelectedValue = "0"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PersatuanID")) Then
                    ddlPersatuan.SelectedValue = ds.Tables(0).Rows(0).Item("PersatuanID")
                Else
                    ddlPersatuan.SelectedValue = "0"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SukanID")) Then
                    ddlSukan.SelectedValue = ds.Tables(0).Rows(0).Item("SukanID")
                Else
                    ddlSukan.SelectedValue = "0"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("RumahsukanID")) Then
                    ddlRumahsukan.SelectedValue = ds.Tables(0).Rows(0).Item("RumahsukanID")
                Else
                    ddlRumahsukan.SelectedValue = "0"
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        '--recount again
        CountMax()

        '--check maximum
        If ValidateForm() = False Then
            Exit Sub
        End If

        strSQL = "UPDATE koko_pelajar SET KelasID=" & ddlKelas.SelectedValue & ",UniformID=" & ddlUniform.SelectedValue & ",PersatuanID=" & ddlPersatuan.SelectedValue & ",SukanID=" & ddlSukan.SelectedValue & ",RumahsukanID=" & ddlRumahsukan.SelectedValue & " WHERE Studentid='" & Request.QueryString("studentid") & "' AND Tahun='" & Request.QueryString("tahun") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya mengemaskini rekod pelajar."
        Else
            lblMsg.Text = "Gagal mengemaskini rekod pleajar. " & strRet
        End If

    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean
        '--must select all
        If ddlUniform.SelectedValue = "0" Then
            lblMsg.Text = "Medan ini mesti dipilih."
            ddlUniform.Focus()
            Return False
        End If

        '--check if exceed
        If lblUniformlock.Text = "Y" Then
            lblMsg.Text = "Badan Beruniform pilihan melebihi kouta yang ditetapkan."
            Return False
        End If

        If ddlPersatuan.SelectedValue = "0" Then
            lblMsg.Text = "Medan ini mesti dipilih."
            ddlPersatuan.Focus()
            Return False
        End If

        '--check if exceed
        If lblPersatuanlock.Text = "Y" Then
            lblMsg.Text = "Kelab & Persatuan pilihan melebihi kouta yang ditetapkan."
            Return False
        End If

        If ddlSukan.SelectedValue = "0" Then
            lblMsg.Text = "Medan ini mesti dipilih."
            ddlSukan.Focus()
            Return False
        End If
        '--check if exceed
        If lblSukanlock.Text = "Y" Then
            lblMsg.Text = "Sukan & Permainan pilihan melebihi kouta yang ditetapkan."
            Return False
        End If

        If ddlRumahsukan.SelectedValue = "0" Then
            lblMsg.Text = "Medan ini mesti dipilih."
            ddlRumahsukan.Focus()
            Return False
        End If
        '--check if exceed
        If lblRumahsukanlock.Text = "Y" Then
            lblMsg.Text = "Rumah Sukan pilihan melebihi kouta yang ditetapkan."
            Return False
        End If

        Return True
    End Function


    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                Response.Redirect("admin.pelajar.list.aspx?std_ID=" & Request.QueryString("std_ID"))
            Case Else

        End Select

    End Sub

    Private Sub ddlUniform_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUniform.SelectedIndexChanged
        'count
        CountMax()

    End Sub

    Private Sub ddlPersatuan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPersatuan.SelectedIndexChanged
        'count
        CountMax()

    End Sub

    Private Sub ddlSukan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSukan.SelectedIndexChanged
        'count
        CountMax()

    End Sub

    Private Sub ddlRumahsukan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRumahsukan.SelectedIndexChanged
        'count
        CountMax()

    End Sub

    Private Sub CountMax()
        '--UNIFORM
        If Not ddlUniform.SelectedValue.Length = 0 Then
            strSQL = "SELECT COUNT(*) FROM koko_pelajar WHERE UniformID=" & ddlUniform.SelectedValue & " AND Tahun='" & lblTahun.Text & "'"
            lblCountUniform.Text = oCommon.getFieldValue(strSQL)
        End If
        '-max value uniform
        strSQL = "SELECT ConfigString FROM koko_config WHERE ConfigCODE='MAXUNIFORM' AND Tahun='" & lblTahun.Text & "'"
        lblMaxUniform.Text = oCommon.getFieldValue(strSQL)
        '--more than MAX
        If CInt(lblCountUniform.Text) >= CInt(lblMaxUniform.Text) Then
            lblMsg.Text = "Badan Beruniform pilihan melebihi kouta yang ditetapkan."
            lblUniformlock.Text = "Y"
        Else
            lblUniformlock.Text = "N"
        End If

        '--PERSATUAN
        If Not ddlPersatuan.SelectedValue.Length = 0 Then
            strSQL = "SELECT COUNT(*) FROM koko_pelajar WHERE PersatuanID=" & ddlPersatuan.SelectedValue & " AND Tahun='" & lblTahun.Text & "'"
            lblCountPersatuan.Text = oCommon.getFieldValue(strSQL)
        End If
        '-max value persatuan
        strSQL = "SELECT ConfigString FROM koko_config WHERE ConfigCODE='MAXPERSATUAN' AND Tahun='" & lblTahun.Text & "'"
        lblMaxPersatuan.Text = oCommon.getFieldValue(strSQL)
        '--more than MAX
        If CInt(lblCountPersatuan.Text) >= CInt(lblMaxPersatuan.Text) Then
            lblMsg.Text = "Kelab & Persatuan pilihan melebihi kouta yang ditetapkan."
            lblPersatuanlock.Text = "Y"
        Else
            lblPersatuanlock.Text = "N"
        End If

        '--SUKAN
        If Not ddlSukan.SelectedValue.Length = 0 Then
            strSQL = "SELECT COUNT(*) FROM koko_pelajar WHERE SukanID=" & ddlSukan.SelectedValue & " AND Tahun='" & lblTahun.Text & "'"
            lblCountSukan.Text = oCommon.getFieldValue(strSQL)
        End If
        '-max value SUKAN
        strSQL = "SELECT ConfigString FROM koko_config WHERE ConfigCODE='MAXSUKAN' AND Tahun='" & lblTahun.Text & "'"
        lblMaxSukan.Text = oCommon.getFieldValue(strSQL)
        '--more than MAX
        If CInt(lblCountSukan.Text) >= CInt(lblMaxSukan.Text) Then
            lblMsg.Text = "Sukan & Permainan pilihan melebihi kouta yang ditetapkan."
            lblSukanlock.Text = "Y"
        Else
            lblSukanlock.Text = "N"
        End If

        '--RUMAHSUKAN
        If Not ddlRumahsukan.SelectedValue.Length = 0 Then
            strSQL = "SELECT COUNT(*) FROM koko_pelajar WHERE RumahsukanID=" & ddlRumahsukan.SelectedValue & " AND Tahun='" & lblTahun.Text & "'"
            lblCountRumahsukan.Text = oCommon.getFieldValue(strSQL)
        End If

        '-max value persatuan
        strSQL = "SELECT ConfigString FROM koko_config WHERE ConfigCODE='MAXRUMAHSUKAN' AND Tahun='" & lblTahun.Text & "'"
        lblMaxRumahsukan.Text = oCommon.getFieldValue(strSQL)

        '--more than MAX
        If CInt(lblCountRumahsukan.Text) >= CInt(lblMaxRumahsukan.Text) Then
            lblMsg.Text = "Rumah Sukan pilihan melebihi kouta yang ditetapkan."
            lblRumahsukanlock.Text = "Y"
        Else
            lblRumahsukanlock.Text = "N"
        End If

    End Sub

End Class