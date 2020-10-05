Imports System.Data.SqlClient

Public Class student_CocurricularList
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                year_list_info()
                koko_type_info()
                koko_list_info()

                page_Load()

                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub year_list_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub koko_type_info()
        strSQL = "Select Distinct Jenis from koko_kolejpermata where Tahun = '" & ddlYear.SelectedValue & "'"
        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKokoType.DataSource = ds
            ddlKokoType.DataTextField = "Jenis"
            ddlKokoType.DataValueField = "Jenis"
            ddlKokoType.DataBind()
            ddlKokoType.Items.Insert(0, New ListItem("Select Course", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub koko_list_info()
        strSQL = "SELECT Distinct Nama from koko_kolejpermata where Jenis = '" & ddlKokoType.SelectedValue & "'and Tahun = '" & ddlYear.SelectedValue & "' "
        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKokoName.DataSource = ds
            ddlKokoName.DataTextField = "Nama"
            ddlKokoName.DataValueField = "Nama"
            ddlKokoName.DataBind()
            ddlKokoName.Items.Insert(0, New ListItem("Select Name", String.Empty))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub page_Load()
        strSQL = "SELECT Parameter from setting where Type ='Year' and Parameter = '" & Now.Year & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlYear.SelectedValue = ""
            End If
        End If
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            koko_type_info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConnPermata)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConnPermata.Close()

        Catch ex As Exception

            Return False
        End Try
        Return True
    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " order by A.student_Name ASC"

        tmpSQL = "SELECT distinct A.std_ID , A.student_Name, A.student_Mykad, StudentProfile.AlumniID, koko_kelas.Kelas,
                  (SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,
                  (SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,
                  (SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,
                  (SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan
                  FROM koko_pelajar
                  LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                  LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                  LEFT OUTER JOIN kolejadmin.dbo.student_info A ON StudentProfile.MYKAD = A.student_Mykad
                  LEFT OUTER JOIN koko_kolejpermata ON koko_pelajar.UniformID=koko_kolejpermata.KokoID OR koko_pelajar.PersatuanID=koko_kolejpermata.KokoID OR koko_pelajar.SukanID=koko_kolejpermata.KokoID OR koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID"

        strWhere = " WHERE koko_pelajar.Tahun ='" & ddlYear.SelectedValue & "' AND A.student_Status = 'Access'"

        If txtstudent.Text.Length > 0 And txtstudent.Text <> "" Then
            strWhere += " AND (A.student_Name like '%" & txtstudent.Text & "%'"
            strWhere += " OR A.student_Mykad = '" & txtstudent.Text & "'"
            strWhere += " OR StudentProfile.AlumniID = '" & txtstudent.Text & "')"
        End If

        If ddlKokoType.SelectedIndex > 0 Then
            If ddlKokoName.SelectedIndex > 0 Then
                strWhere += " AND koko_kolejpermata.Nama = '" & ddlKokoName.SelectedValue & "' "
            End If
        End If

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL
    End Function

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub ddlKokoType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKokoType.SelectedIndexChanged
        Try
            koko_list_info()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKokoName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKokoName.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub
End Class