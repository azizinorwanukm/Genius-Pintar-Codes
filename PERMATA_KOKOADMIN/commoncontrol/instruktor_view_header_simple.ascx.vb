Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class instruktor_view_header_simple
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                koko_instruktor_load()
                setAccessRight()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub setAccessRight()
        lnkUniform.Enabled = False
        lnkPersatuan.Enabled = False
        lnkSukan.Enabled = False
        lnkRumahsukan.Enabled = False

    End Sub

    Private Sub koko_instruktor_load()
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        '--PROFILE INSTRUKTOR
        tmpSQL = "SELECT koko_instruktor.InstruktorID,koko_instruktor.UniformID,koko_instruktor.PersatuanID,koko_instruktor.SukanID,koko_instruktor.RumahSukanID,koko_instruktor.KelasID,koko_instruktor.Fullname,koko_instruktor.MYKAD,koko_instruktor.ContactNo,koko_instruktor.Email,koko_instruktor.Address1,koko_instruktor.Address2,koko_instruktor.Postcode,koko_instruktor.City,koko_instruktor.State,koko_instruktor.Tahun,koko_instruktor.BankName,koko_instruktor.AcctNo,koko_instruktor.LoginID,koko_instruktor.Pwd,koko_kelas.Kelas,koko_instruktor.KetuaUniform,koko_instruktor.KetuaPersatuan,koko_instruktor.KetuaSukan,koko_instruktor.KetuaRumahsukan,"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContactNo")) Then
                    lblContactNo.Text = ds.Tables(0).Rows(0).Item("ContactNo")
                Else
                    lblContactNo.Text = ""
                End If
                '--KOKURIKULUM
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kelas")) Then
                    lblKelas.Text = ds.Tables(0).Rows(0).Item("Kelas")
                Else
                    lblKelas.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Uniform")) Then
                    lnkUniform.Text = ds.Tables(0).Rows(0).Item("Uniform")
                Else
                    lnkUniform.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Persatuan")) Then
                    lnkPersatuan.Text = ds.Tables(0).Rows(0).Item("Persatuan")
                Else
                    lnkPersatuan.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sukan")) Then
                    lnkSukan.Text = ds.Tables(0).Rows(0).Item("Sukan")
                Else
                    lnkSukan.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Rumahsukan")) Then
                    lnkRumahsukan.Text = ds.Tables(0).Rows(0).Item("Rumahsukan")
                Else
                    lnkRumahsukan.Text = ""
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
            lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

End Class