Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class kelaskoko_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--refresh
                ClearScreen()
                getKOKODetail()
                getInstruktor()

                strRet = BindDataSQL(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

        End Try

    End Sub

    '--getKOKODetail
    Private Sub getKOKODetail()
        strSQL = "SELECT Nama FROM koko_kolejpermata WHERE KokoID=" & Request.QueryString("kokoid")
        lblKOKOName.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Tahun FROM koko_kolejpermata WHERE KokoID=" & Request.QueryString("kokoid")
        lblTahun.Text = oCommon.getFieldValue(strSQL)
    End Sub

    Private Sub getInstruktor()
        strSQL = "  select koko_instruktor.kokoinstruktorid, koko_instruktor.Fullname from koko_instruktor
                    left join koko_kolejpermata on koko_instruktor.SukanID = koko_kolejpermata.KokoID
                    where KetuaSukan = 'True'
                    and SukanID = '" & Request.QueryString("kokoid") & "'
                    order by koko_instruktor.Fullname asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlInstruktor.DataSource = ds
            ddlInstruktor.DataTextField = "Fullname"
            ddlInstruktor.DataValueField = "kokoinstruktorid"
            ddlInstruktor.DataBind()
            ddlInstruktor.Items.Insert(0, New ListItem("Pilih Instruktor", String.Empty))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function BindDataSQL(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet

        Dim checking_koko As String = "select IsMandatory from koko_kolejpermata where kokoid = '" & Request.QueryString("kokoid") & "' "
        Dim get_koko As String = oCommon.getFieldValue(checking_koko)

        Dim checking_tahun As String = "select tahun from koko_kolejpermata where kokoid = '" & Request.QueryString("kokoid") & "'"
        Dim get_tahun As String = oCommon.getFieldValue(checking_tahun)

        Dim strQuery As String = ""

        If get_koko = "Y" Then
            strQuery = "select koko_pelajar.StudentID, StudentProfile.StudentFullname, StudentProfile.MYKAD, koko_kelas.Kelas from koko_pelajar
                        left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                        left join koko_kelas on koko_pelajar.KelasID = koko_kelas.KelasID
                        where koko_pelajar.KelasID is not null
                        and koko_kelas.tahun = '" & get_tahun & "'
                        order by koko_kelas.Kelas, StudentProfile.StudentFullname asc"
        Else

            lbldatrespondant.visible = False

            strQuery = "select koko_pelajar.StudentID, StudentProfile.StudentFullname, StudentProfile.MYKAD, koko_kelas.Kelas from koko_pelajar
                        left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                        left join koko_kelas on koko_pelajar.KelasID = koko_kelas.KelasID
                        where koko_pelajar.KelasID is not null
                        and (SukanID = '" & Request.QueryString("kokoid") & "' or UniformID = '" & Request.QueryString("kokoid") & "' or PersatuanID = '" & Request.QueryString("kokoid") & "')
                        order by koko_kelas.Kelas, StudentProfile.StudentFullname asc"
        End If

        Dim myDataAdapter As New SqlDataAdapter(strQuery, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod ditemui."
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True
    End Function

    Private Sub ClearScreen()
        lblMsg.Text = ""
        txtKelas.Text = ""

    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            'insert into course list
            strSQL = "INSERT INTO koko_kelaskoko(KokoID,kokoinstruktorid,Kelas) VALUES (" & Request.QueryString("kokoid") & ",'" & ddlInstruktor.SelectedValue & "','" & oCommon.FixSingleQuotes(txtKelas.Text.ToUpper) & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

            Dim checking_koko As String = "select IsMandatory from koko_kolejpermata where kokoid = '" & Request.QueryString("kokoid") & "' "
            Dim get_koko As String = oCommon.getFieldValue(checking_koko)

            If get_koko = "Y" Then

                If strRet = "0" Then

                    Dim i As Integer = 0
                    Dim value As String = ""

                    Dim get_kokokelasid As String = "select KelasKokoID from koko_kelaskoko where KokoID = '" & Request.QueryString("kokoid") & "'"
                    Dim data_kokokelasid As String = oCommon.getFieldValue(get_kokokelasid)

                    Dim checking_tahun As String = "select tahun from koko_kolejpermata where kokoid = '" & Request.QueryString("kokoid") & "'"
                    Dim get_tahun As String = oCommon.getFieldValue(checking_tahun)

                    For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                        Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(3).FindControl("chkSelect"), CheckBox)
                        If Not chkUpdate Is Nothing Then
                            ' Get the values of textboxes using findControl
                            Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                            If chkUpdate.Checked = True Then

                                strSQL = "INSERT INTO koko_kumpulan(KelasKokoID,Tahun,StudentID) VALUES ('" & data_kokokelasid & "','" & get_tahun & "','" & strKey & "')"
                                strRet = oCommon.ExecuteSQL(strSQL)

                                If strRet = "0" Then
                                    lblMsg.Text = "Penambahan berjaya!"
                                Else
                                    lblMsg.Text = "Gagal. " & strRet
                                End If

                            End If
                        End If
                    Next
                Else
                    lblMsg.Text = "Gagal. " & strRet
                End If

            Else

                If strRet = "0" Then
                    lblMsg.Text = "Penambahan berjaya!"
                Else
                    lblMsg.Text = "Gagal. " & strRet
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message
        End Try

    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtKelas.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtKelas.Focus()
            Return False
        End If

        ''--check if already exist
        strSQL = "SELECT Kelas FROM koko_kelaskoko WHERE Kelas='" & oCommon.FixSingleQuotes(txtKelas.Text) & "' AND KokoID=" & Request.QueryString("kokoid")
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Telah digunakan."
            Return False
        End If

        Return True
    End Function

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.koko.kelaskoko.list.aspx?kokoid=" & Request.QueryString("kokoid") & "&admin_ID=" & Request.QueryString("admin_ID"))

    End Sub

End Class