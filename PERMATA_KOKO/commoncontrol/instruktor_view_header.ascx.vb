Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class instruktor_view_header
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                getPeperiksaan()
                koko_instruktor_load()
                setAccessRight()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub getPeperiksaan()
        Select Case Request.QueryString("peperiksaan")
            Case "1"
                lblPeperiksaan.Text = "PEPERIKSAAN 1"
            Case "2"
                lblPeperiksaan.Text = "PEPERIKSAAN 2"
            Case "3"
                lblPeperiksaan.Text = "PEPERIKSAAN 3"
            Case "4"
                lblPeperiksaan.Text = "PEPERIKSAAN 4"
            Case Else
                lblPeperiksaan.Text = "NA"
        End Select
    End Sub


    Private Sub setAccessRight()
        If lblKetuaUniform.Text = "False" Then
            lnkUniform.Enabled = False
        End If
        If lblKetuaPersatuan.Text = "False" Then
            lnkPersatuan.Enabled = False
        End If
        If lblKetuaSukan.Text = "False" Then
            lnkSukan.Enabled = False
        End If
        If lblKetuaRumahsukan.Text = "False" Then
            lnkRumahsukan.Enabled = False
        End If

    End Sub

    Private Sub koko_instruktor_load()
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        '--PROFILE INSTRUKTOR
        tmpSQL = "SELECT koko_instruktor.InstruktorID,koko_instruktor.UniformID,koko_instruktor.PersatuanID,koko_instruktor.SukanID,koko_instruktor.RumahSukanID,koko_instruktor.Fullname,koko_instruktor.MYKAD,koko_instruktor.ContactNo,koko_instruktor.Email,koko_instruktor.Tahun,koko_instruktor.BankName,koko_instruktor.AcctNo,koko_instruktor.KetuaUniform,koko_instruktor.KetuaPersatuan,koko_instruktor.KetuaSukan,koko_instruktor.KetuaRumahsukan,koko_kelas.Kelas,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.UniformID=koko_kolejpermata.KokoID) as Uniform,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.SukanID=koko_kolejpermata.KokoID) as Sukan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan"
        tmpSQL += " FROM koko_instruktor"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_instruktor.KelasID=koko_kelas.KelasID"
        strWhere = " WHERE koko_instruktor.Tahun='" & Request.QueryString("tahun") & "'"
        strWhere += " AND koko_instruktor.InstruktorID='" & Request.QueryString("instruktorid") & "'"


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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    lblFullname.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    lblFullname.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MYKAD")) Then
                    lblMYKAD.Text = ds.Tables(0).Rows(0).Item("MYKAD")
                Else
                    lblMYKAD.Text = ""
                End If

                '--KOKURIKULUM
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kelas")) Then
                    lblKelas.Text = ds.Tables(0).Rows(0).Item("Kelas")
                Else
                    lblKelas.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Uniform")) Then
                    lnkUniform.Text = ds.Tables(0).Rows(0).Item("Uniform")
                    lblUniformID.Text = ds.Tables(0).Rows(0).Item("UniformID")
                Else
                    lnkUniform.Text = ""
                    lblUniformID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Persatuan")) Then
                    lnkPersatuan.Text = ds.Tables(0).Rows(0).Item("Persatuan")
                    lblPersatuanID.Text = ds.Tables(0).Rows(0).Item("PersatuanID")
                Else
                    lnkPersatuan.Text = ""
                    lblPersatuanID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sukan")) Then
                    lnkSukan.Text = ds.Tables(0).Rows(0).Item("Sukan")
                    lblSukanID.Text = ds.Tables(0).Rows(0).Item("SukanID")
                Else
                    lnkSukan.Text = ""
                    lblSukanID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Rumahsukan")) Then
                    lnkRumahsukan.Text = ds.Tables(0).Rows(0).Item("Rumahsukan")
                    lblRumahsukanID.Text = ds.Tables(0).Rows(0).Item("RumahsukanID")
                Else
                    lnkRumahsukan.Text = ""
                    lblRumahsukanID.Text = ""
                End If

                '--KETUA
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KetuaUniform")) Then
                    lblKetuaUniform.Text = ds.Tables(0).Rows(0).Item("KetuaUniform")
                Else
                    lblKetuaUniform.Text = "False"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KetuaPersatuan")) Then
                    lblKetuaPersatuan.Text = ds.Tables(0).Rows(0).Item("KetuaPersatuan")
                Else
                    lblKetuaPersatuan.Text = "False"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KetuaSukan")) Then
                    lblKetuaSukan.Text = ds.Tables(0).Rows(0).Item("KetuaSukan")
                Else
                    lblKetuaSukan.Text = "False"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KetuaRumahsukan")) Then
                    lblKetuaRumahsukan.Text = ds.Tables(0).Rows(0).Item("KetuaRumahsukan")
                Else
                    lblKetuaRumahsukan.Text = "False"
                End If

            End If

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub lnkUniform_Click(sender As Object, e As EventArgs) Handles lnkUniform.Click
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                Select Case Request.QueryString("set")
                    Case "markah"
                        Select Case Request.QueryString("peperiksaan")
                            Case "1"
                                Response.Redirect("admin.uniform.update.p1.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=UniformID&value=" & lblUniformID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "2"
                                Response.Redirect("admin.uniform.update.p2.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=UniformID&value=" & lblUniformID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "3"
                                Response.Redirect("admin.uniform.update.p3.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=UniformID&value=" & lblUniformID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "4"
                                Response.Redirect("admin.uniform.update.p4.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=UniformID&value=" & lblUniformID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case Else
                        End Select

                    Case "jawatan"
                        Response.Redirect("admin.uniform.jawatan.update.aspx?set=jawatan&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=UniformID&value=" & lblUniformID.Text)
                    Case Else
                End Select

            Case "INSTRUKTOR"
                Select Case Request.QueryString("set")
                    Case "markah"
                        Select Case Request.QueryString("peperiksaan")
                            Case "1"
                                Response.Redirect("instruktor.uniform.update.p1.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=UniformID&value=" & lblUniformID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "2"
                                Response.Redirect("instruktor.uniform.update.p2.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=UniformID&value=" & lblUniformID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "3"
                                Response.Redirect("instruktor.uniform.update.p3.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=UniformID&value=" & lblUniformID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "4"
                                Response.Redirect("instruktor.uniform.update.p4.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=UniformID&value=" & lblUniformID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case Else
                        End Select

                    Case "jawatan"
                        Response.Redirect("instruktor.uniform.jawatan.update.aspx?set=jawatan&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=UniformID&value=" & lblUniformID.Text)
                    Case Else
                End Select

            Case "PENSYARAH"
            Case "PENGARAH"
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select

    End Sub

    Protected Sub lnkPersatuan_Click(sender As Object, e As EventArgs) Handles lnkPersatuan.Click
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                Select Case Request.QueryString("set")
                    Case "markah"
                        Select Case Request.QueryString("peperiksaan")
                            Case "1"
                                Response.Redirect("admin.persatuan.update.p1.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=PersatuanID&value=" & lblPersatuanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "2"
                                Response.Redirect("admin.persatuan.update.p2.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=PersatuanID&value=" & lblPersatuanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "3"
                                Response.Redirect("admin.persatuan.update.p3.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=PersatuanID&value=" & lblPersatuanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "4"
                                Response.Redirect("admin.persatuan.update.p4.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=PersatuanID&value=" & lblPersatuanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case Else
                        End Select
                    Case "jawatan"
                        Response.Redirect("admin.persatuan.jawatan.update.aspx?set=jawatan&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=PersatuanID&value=" & lblPersatuanID.Text)
                    Case Else
                End Select
            Case "INSTRUKTOR"
                Select Case Request.QueryString("set")
                    Case "markah"
                        Select Case Request.QueryString("peperiksaan")
                            Case "1"
                                Response.Redirect("instruktor.persatuan.update.p1.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=PersatuanID&value=" & lblPersatuanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "2"
                                Response.Redirect("instruktor.persatuan.update.p2.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=PersatuanID&value=" & lblPersatuanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "3"
                                Response.Redirect("instruktor.persatuan.update.p3.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=PersatuanID&value=" & lblPersatuanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "4"
                                Response.Redirect("instruktor.persatuan.update.p4.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=PersatuanID&value=" & lblPersatuanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case Else
                        End Select
                    Case "jawatan"
                        Response.Redirect("instruktor.persatuan.jawatan.update.aspx?set=jawatan&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=PersatuanID&value=" & lblPersatuanID.Text)
                    Case Else
                End Select
            Case "PENSYARAH"
            Case "PENGARAH"
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select


    End Sub

    Protected Sub lnkSukan_Click(sender As Object, e As EventArgs) Handles lnkSukan.Click
        Select Case Server.HtmlEncode(Request.Cookies("koko_usertype").Value)
            Case "ADMIN"
                Select Case Request.QueryString("set")
                    Case "markah"
                        Select Case Request.QueryString("peperiksaan")
                            Case "1"
                                Response.Redirect("admin.sukan.update.p1.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "2"
                                Response.Redirect("admin.sukan.update.p2.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "3"
                                Response.Redirect("admin.sukan.update.p3.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case "4"
                                Response.Redirect("admin.sukan.update.p4.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                            Case Else
                        End Select

                    Case "jawatan"
                        Response.Redirect("admin.sukan.jawatan.update.aspx?set=jawatan&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text)
                    Case Else
                End Select

            Case "INSTRUKTOR"
                Select Case Request.QueryString("set")
                    Case "markah"
                        Select Case lblSukanID.Text
                            Case "161"
                                Select Case Request.QueryString("peperiksaan")
                                    Case "1"
                                        Response.Redirect("instruktor.sukanmandatory.update.p1.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                                    Case "2"
                                        Response.Redirect("instruktor.sukanmandatory.update.p2.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                                    Case Else
                                End Select
                            Case Else
                                Select Case Request.QueryString("peperiksaan")
                                    Case "1"
                                        Response.Redirect("instruktor.sukan.update.p1.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                                    Case "2"
                                        Response.Redirect("instruktor.sukan.update.p2.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                                    Case "3"
                                        Response.Redirect("instruktor.sukan.update.p3.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                                    Case "4"
                                        Response.Redirect("instruktor.sukan.update.p4.aspx?set=markah&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text & "&peperiksaan=" & Request.QueryString("peperiksaan"))
                                    Case Else
                                End Select
                        End Select


                    Case "jawatan"
                        Response.Redirect("instruktor.sukan.jawatan.update.aspx?set=jawatan&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&field=SukanID&value=" & lblSukanID.Text)
                    Case Else
                End Select

            Case "PENSYARAH"
            Case "PENGARAH"
            Case Else
                lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
        End Select


    End Sub

    Protected Sub lnkRumahsukan_Click(sender As Object, e As EventArgs) Handles lnkRumahsukan.Click
        lblMsg.Text = "Tiada kemasukkan markah bagi Rumah Sukan."

    End Sub

End Class