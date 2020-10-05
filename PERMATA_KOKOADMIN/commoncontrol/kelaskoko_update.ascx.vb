Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class kelaskoko_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                getKOKODetail()
                getInstruktor()
                koko_sukan_load()

                strRet = BindDataSQL(datRespondent)
            End If

        Catch ex As Exception

        End Try

    End Sub

    '--getKOKODetail
    Private Sub getKOKODetail()
        strSQL = "SELECT KokoID FROM koko_kelaskoko WHERE kelaskokoid=" & Request.QueryString("kelaskokoid")
        lblKokoID.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Nama FROM koko_kolejpermata WHERE KokoID=" & lblKokoID.Text
        lblKOKOName.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Tahun FROM koko_kolejpermata WHERE KokoID=" & lblKokoID.Text
        lblTahun.Text = oCommon.getFieldValue(strSQL)
    End Sub

    Private Sub getInstruktor()
        strSQL = "  select koko_instruktor.kokoinstruktorid, koko_instruktor.Fullname from koko_instruktor
                    left join koko_kolejpermata on koko_instruktor.SukanID = koko_kolejpermata.KokoID
                    left join koko_kelaskoko on koko_kolejpermata.kokoid = koko_kelaskoko.kokoid
                    where KetuaSukan = 'True'
                    and koko_kelaskoko.KelasKokoID = '" & Request.QueryString("kelaskokoid") & "'
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

    Private Sub koko_sukan_load()
        strSQL = "SELECT * FROM koko_kelaskoko
                  LEFT JOIN koko_kumpulan ON koko_kelaskoko.KelasKokoID = koko_kumpulan.KelasKokoID
                  LEFT JOIN  koko_instruktor on koko_kelaskoko.kokoinstruktorid = koko_instruktor.kokoinstruktorid
                  WHERE koko_kumpulan.KelasKokoID= '" & Request.QueryString("kelaskokoid") & "'"

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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kelas")) Then
                    txtKelas.Text = ds.Tables(0).Rows(0).Item("Kelas")
                Else
                    txtKelas.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("kokoinstruktorid")) Then
                    ddlInstruktor.SelectedValue = ds.Tables(0).Rows(0).Item("kokoinstruktorid")
                Else
                    ddlInstruktor.SelectedValue = ""
                End If

            End If

        Catch ex As Exception
            'lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindDataSQL(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet

        Dim checking_koko As String = "select distinct IsMandatory from koko_kolejpermata 
                                       left join koko_kelaskoko on koko_kolejpermata.kokoid = koko_kelaskoko.kokoid
                                       left join koko_kumpulan on koko_kelaskoko.KelasKokoID = koko_kumpulan.KelasKokoid where koko_kumpulan.kelaskokoid = '" & Request.QueryString("kelaskokoid") & "' "
        Dim get_koko As String = oCommon.getFieldValue(checking_koko)

        Dim checking_tahun As String = "select distinct koko_kolejpermata.tahun from koko_kolejpermata 
                                        left join koko_kelaskoko on koko_kolejpermata.kokoid = koko_kelaskoko.kokoid
                                        left join koko_kumpulan on koko_kelaskoko.KelasKokoID = koko_kumpulan.KelasKokoid where koko_kumpulan.kelaskokoid = '" & Request.QueryString("kelaskokoid") & "' "
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

            lbldatrespondant.Visible = False

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

    Private Sub datRespondent_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datRespondent.RowDataBound
        '--set checked
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chkSelect As CheckBox

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString  '--StudentID
            chkSelect = e.Row.FindControl("chkSelect")

            Dim get_checkstudent As String = "select kumpulanID from koko_kumpulan where StudentID = '" & strKeyID & "'"
            Dim find_checkstudent As String = oCommon.getFieldValue(get_checkstudent)

            If find_checkstudent.Length > 0 Then
                chkSelect.Checked = True
            End If

        End If

    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.koko.kelaskoko.list.aspx?kokoid=" & lblKokoID.Text & "&admin_ID=" & Request.QueryString("admin_ID"))

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try

            'insert into course list
            strSQL = "UPDATE koko_kelaskoko SET Kelas='" & oCommon.FixSingleQuotes(txtKelas.Text.ToUpper) & "', kokoinstruktorid = '" & ddlInstruktor.SelectedValue & "' 
                      WHERE KelasKokoID = " & Request.QueryString("kelaskokoid")
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then

                strSQL = "delete koko_kumpulan where KelasKokoID = '" & Request.QueryString("kelaskokoid") & "'"
                strRet = oCommon.getFieldValue(strSQL)

                Dim i As Integer = 0
                Dim value As String = ""

                For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                    Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(3).FindControl("chkSelect"), CheckBox)
                    If Not chkUpdate Is Nothing Then
                        ' Get the values of textboxes using findControl
                        Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                        If chkUpdate.Checked = True Then

                            strSQL = "INSERT INTO koko_kumpulan(KelasKokoID,Tahun,StudentID) VALUES ('" & Request.QueryString("kelaskokoid") & "','" & Now.Year & "','" & strKey & "')"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If strRet = "0" Then
                                lblMsg.Text = "Kemaskini berjaya!"
                            Else
                                lblMsg.Text = "Gagal. " & strRet
                            End If

                        End If
                    End If
                Next



            Else
                lblMsg.Text = "Gagal. " & strRet
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

        If Not lblKelasOld.Text = txtKelas.Text Then
            ''--check if already exist
            strSQL = "SELECT Kelas FROM koko_kelaskoko WHERE Kelas='" & oCommon.FixSingleQuotes(txtKelas.Text) & "' AND KokoID=" & lblKokoID.Text
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Telah digunakan."
                Return False
            End If
        End If

        Return True
    End Function



End Class