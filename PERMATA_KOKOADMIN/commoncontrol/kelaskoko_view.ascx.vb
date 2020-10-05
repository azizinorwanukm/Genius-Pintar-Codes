Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class kelaskoko_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        Try
            If Not IsPostBack Then
                getKOKODetail()
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
                    lblKelas.Text = ds.Tables(0).Rows(0).Item("Kelas")
                Else
                    lblKelas.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    lblInstruktor.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    lblInstruktor.Text = ""
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

        Dim checking_tahun As String = "select tahun from koko_kolejpermata 
                                        left join koko_kelaskoko on koko_kolejpermata.kokoid = koko_kelaskoko.kokoid
                                        left join koko_kumpulan on koko_kelaskoko.KelasKokoID = koko_kumpulan.KelasKokoid where koko_kumpulan.kelaskokoid = '" & Request.QueryString("kelaskokoid") & "' "
        Dim get_tahun As String = oCommon.getFieldValue(checking_tahun)

        Dim strQuery As String = ""

        If get_koko = "Y" Then
            strQuery = "select koko_pelajar.StudentID, StudentProfile.StudentFullname, StudentProfile.MYKAD, koko_kelas.Kelas from koko_pelajar
                        left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                        left join koko_kelas on koko_pelajar.KelasID = koko_kelas.KelasID
                        left join koko_kumpulan on koko_pelajar.StudentID = koko_kumpulan.StudentID
                        where koko_kumpulan.KelasKokoID = '" & Request.QueryString("kelaskokoid") & "'
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

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.koko.kelaskoko.list.aspx?kokoid=" & lblKokoID.Text & "&admin_ID=" & Request.QueryString("admin_ID"))

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        '--if student exist inside the group. DO NOT delete
        If CheckDelete() = False Then
            Exit Sub
        End If

        strSQL = "DELETE koko_kelaskoko WHERE KelasKokoID=" & Request.QueryString("kelaskokoid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Rekod berjaya dihapuskan."
        Else
            lblMsg.Text = "system error:" & strRet
        End If

    End Sub

    Private Function CheckDelete() As Boolean
        strSQL = "SELECT KelasKokoID FROM koko_kelaskokopelajar WHERE KelasKokoID=" & Request.QueryString("kelaskokoid")
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Terdapat pelajar dalam kumpulan ini. Keluarkan pelajar dari kumpulan ini terlebih dahulu."
            Return False
        End If

        Return True
    End Function

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Response.Redirect("admin.kelaskoko.update.aspx?kelaskokoid=" & Request.QueryString("kelaskokoid") & "&admin_ID=" & Request.QueryString("admin_ID"))

    End Sub



End Class