Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class event_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                koko_event_load()
                koko_instruktor_load()
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub koko_event_load()
        strSQL = "SELECT koko_event.EventID,koko_event.Tahun,koko_event.InstruktorID,koko_event.EventDate,koko_event.Title,koko_event.KokoID,koko_event.Agenda,koko_kolejpermata.Nama FROM koko_event,koko_kolejpermata WHERE EventID=" & Request.QueryString("eventid")
        strSQL += " AND koko_event.KokoID=koko_kolejpermata.KokoID"
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
                Dim strEventDate As String = ""
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EventDate")) Then
                    strEventDate = ds.Tables(0).Rows(0).Item("EventDate")
                    lblEventDate.Text = oCommon.DateDisplay(strEventDate)
                Else
                    strEventDate = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Title")) Then
                    lblTitle.Text = ds.Tables(0).Rows(0).Item("Title")
                Else
                    lblTitle.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Nama")) Then
                    lblNama.Text = ds.Tables(0).Rows(0).Item("Nama")
                Else
                    lblNama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Agenda")) Then
                    ltAgenda.Text = ds.Tables(0).Rows(0).Item("Agenda").ToString.Replace(Environment.NewLine, "<br />")
                Else
                    ltAgenda.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("InstruktorID")) Then
                    lblInstruktorID.Text = ds.Tables(0).Rows(0).Item("InstruktorID")
                Else
                    lblInstruktorID.Text = ""
                End If
            End If

        Catch ex As Exception
            'lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub koko_instruktor_load()
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        '--PROFILE INSTRUKTOR
        tmpSQL = "SELECT koko_instruktor.InstruktorID,koko_instruktor.KelasID,koko_instruktor.UniformID,koko_instruktor.PersatuanID,koko_instruktor.SukanID,koko_instruktor.RumahSukanID,koko_instruktor.Fullname,koko_instruktor.MYKAD,koko_instruktor.ContactNo,koko_instruktor.Email,koko_instruktor.Address1,koko_instruktor.Address2,koko_instruktor.PostCode,koko_instruktor.City,koko_instruktor.State,koko_instruktor.LoginID,koko_instruktor.Pwd,koko_instruktor.BankName,koko_instruktor.AcctNo,koko_instruktor.Tahun,koko_instruktor.BankName,koko_instruktor.AcctNo,koko_instruktor.KetuaUniform,koko_instruktor.KetuaPersatuan,koko_instruktor.KetuaSukan,koko_instruktor.KetuaRumahsukan,koko_kelas.Kelas,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.UniformID=koko_kolejpermata.KokoID) as Uniform,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.SukanID=koko_kolejpermata.KokoID) as Sukan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_instruktor.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan"
        tmpSQL += " FROM koko_instruktor"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_instruktor.KelasID=koko_kelas.KelasID"
        strWhere = " WHERE koko_instruktor.Tahun='" & Request.QueryString("tahun") & "'"
        strWhere += " AND koko_instruktor.InstruktorID='" & lblInstruktorID.Text & "'"

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

                '--KETUA
            End If

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub lnkUpdate_Click(sender As Object, e As EventArgs) Handles lnkUpdate.Click
        Try
            Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
                Case "ADMIN"
                    Response.Redirect("admin.event.update.aspx?eventid=" & Request.QueryString("eventid") & "&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&admin_ID=" & Request.QueryString("admin_ID"))
                Case "INSTRUKTOR"
                    Response.Redirect("instruktor.event.update.aspx?eventid=" & Request.QueryString("eventid") & "&instruktorid=" & Request.QueryString("instruktorid") & "&tahun=" & Request.QueryString("tahun") & "&admin_ID=" & Request.QueryString("admin_ID"))
                Case "PENSYARAH"
                Case "PENGARAH"
                Case Else
            End Select

        Catch ex As Exception
        End Try
    End Sub
End Class