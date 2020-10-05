Imports System.Data.SqlClient

Public Class cocurricular_Detail
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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
            If myDataSet.Tables(0).Rows.Count = 0 Then
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConnPermata.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT distinct A.std_ID, koko_pelajar.Tahun, koko_kelas.Kelas,
                 (SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,
                 (SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,
                 (SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,
                 (SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan
                 FROM koko_pelajar
                 LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                 LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                 LEFT OUTER JOIN kolejadmin.dbo.student_info A ON StudentProfile.MYKAD = A.student_Mykad
                 LEFT OUTER JOIN koko_kolejpermata ON koko_pelajar.UniformID=koko_kolejpermata.KokoID OR koko_pelajar.PersatuanID=koko_kolejpermata.KokoID OR koko_pelajar.SukanID=koko_kolejpermata.KokoID OR koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID"
        strWhere = " WHERE A.student_Status = 'Access' AND A.std_ID = '" & Request.QueryString("std_ID") & "'"
        strOrder = " ORDER BY koko_pelajar.Tahun DESC"

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

End Class