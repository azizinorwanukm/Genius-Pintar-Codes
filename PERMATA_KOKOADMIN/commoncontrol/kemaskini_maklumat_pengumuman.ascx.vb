Imports System.Data.SqlClient

Public Class kemaskini_maklumat_pengumuman
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                strRet = BindData(datRespondent)
                strRet = BindDataBadan(KokoBadan)
                strRet = BindDataSukan(KokoSukan)
                strRet = BindDokumen(DokumenView)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""

        tmpSQL = "Select * from master_pengumuman"
        strWhere = " where ID is not null and Jenis_Kokurikulum = 'Kelab Dan Persatuan'"

        getSQL = tmpSQL & strWhere

        ''Debug.WriteLine(getSQL)
        ''--debug
        Return getSQL
    End Function

    Private Function BindDataBadan(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLBadan, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQLBadan() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""

        tmpSQL = "Select * from master_pengumuman"
        strWhere = " where ID is not null and Jenis_Kokurikulum = 'Badan Beruniform'"

        getSQLBadan = tmpSQL & strWhere

        ''Debug.WriteLine(getSQL)
        ''--debug
        Return getSQLBadan
    End Function

    Private Function BindDokumen(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLDokumen, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function BindDataSukan(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLSukan, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQLSukan() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""

        tmpSQL = "Select * from master_pengumuman"
        strWhere = " where ID is not null and Jenis_Kokurikulum = 'Sukan Dan Permainan'"

        getSQLSukan = tmpSQL & strWhere

        ''Debug.WriteLine(getSQL)
        ''--debug
        Return getSQLSukan
    End Function

    Private Function getSQLDokumen() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""

        tmpSQL = "Select * from koko_content"
        strWhere = " where ContentID is not null"

        getSQLDokumen = tmpSQL & strWhere

        Return getSQLDokumen
    End Function

    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim name_Pengumuman As TextBox = DirectCast(datRespondent.Rows(e.RowIndex).FindControl("txtPengumuman"), TextBox)
        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        ''update grades
        strSQL = "UPDATE master_pengumuman SET Pengumuman='" & name_Pengumuman.Text & "' WHERE ID ='" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        datRespondent.EditIndex = e.NewEditIndex
        Me.BindData(datRespondent)

    End Sub

    Protected Sub OnRowCancelingEdit(sender As Object, e As EventArgs)
        datRespondent.EditIndex = -1
        Me.BindData(datRespondent)
    End Sub

    Protected Sub OnRowUpdatingSukan(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim name_Pengumuman As TextBox = DirectCast(KokoSukan.Rows(e.RowIndex).FindControl("txtPengumuman"), TextBox)
        Dim strKeyID As String = KokoSukan.DataKeys(e.RowIndex).Value.ToString

        ''update grades
        strSQL = "UPDATE master_pengumuman SET Pengumuman='" & name_Pengumuman.Text & "' WHERE ID ='" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        KokoSukan.EditIndex = -1
        Me.BindDataSukan(KokoSukan)
    End Sub

    Private Sub KokoSukan_RowEditingSukan(sender As Object, e As GridViewEditEventArgs) Handles KokoSukan.RowEditing
        KokoSukan.EditIndex = e.NewEditIndex
        Me.BindDataSukan(KokoSukan)

    End Sub

    Protected Sub OnRowCancelingEditSukan(sender As Object, e As EventArgs)
        KokoSukan.EditIndex = -1
        Me.BindDataSukan(KokoSukan)
    End Sub


    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyCode As String = datRespondent.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_NewCourse As New SqlDataAdapter()

            Dim dlt_Course As String

            Dlt_NewCourse.SelectCommand = New SqlCommand()
            Dlt_NewCourse.SelectCommand.Connection = MyConnection
            Dlt_NewCourse.SelectCommand.CommandText = "delete master_pengumuman where ID='" & strKeyCode & "'"
            MyConnection.Open()
            dlt_Course = Dlt_NewCourse.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindData(datRespondent)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Private Sub KokoSukan_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles KokoSukan.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyCode As String = KokoSukan.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_NewCourse As New SqlDataAdapter()

            Dim dlt_Course As String

            Dlt_NewCourse.SelectCommand = New SqlCommand()
            Dlt_NewCourse.SelectCommand.Connection = MyConnection
            Dlt_NewCourse.SelectCommand.CommandText = "delete master_pengumuman where ID='" & strKeyCode & "'"
            MyConnection.Open()
            dlt_Course = Dlt_NewCourse.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindDataSukan(KokoSukan)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub OnRowUpdatingBadan(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim name_Pengumuman As TextBox = DirectCast(KokoBadan.Rows(e.RowIndex).FindControl("txtPengumuman"), TextBox)
        Dim strKeyID As String = KokoBadan.DataKeys(e.RowIndex).Value.ToString

        ''update grades
        strSQL = "UPDATE master_pengumuman SET Pengumuman='" & name_Pengumuman.Text & "' WHERE ID ='" & strKeyID & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        KokoBadan.EditIndex = -1
        Me.BindDataBadan(KokoBadan)
    End Sub

    Private Sub KokoBadan_RowEditingSukan(sender As Object, e As GridViewEditEventArgs) Handles KokoBadan.RowEditing
        KokoBadan.EditIndex = e.NewEditIndex
        Me.BindDataBadan(KokoBadan)

    End Sub

    Protected Sub OnRowCancelingEditBadan(sender As Object, e As EventArgs)
        KokoBadan.EditIndex = -1
        Me.BindDataBadan(KokoBadan)
    End Sub

    Private Sub KokoBadan_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles KokoBadan.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyCode As String = KokoBadan.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_NewCourse As New SqlDataAdapter()

            Dim dlt_Course As String

            Dlt_NewCourse.SelectCommand = New SqlCommand()
            Dlt_NewCourse.SelectCommand.Connection = MyConnection
            Dlt_NewCourse.SelectCommand.CommandText = "delete master_pengumuman where ID='" & strKeyCode & "'"
            MyConnection.Open()
            dlt_Course = Dlt_NewCourse.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindDataBadan(KokoBadan)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub btnCreate_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
        Response.Redirect("admin.tambah.maklumat.pengumuman.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub

    Protected Sub BtnCreateUniform_Click(sender As Object, e As EventArgs) Handles BtnCreateUniform.Click
        Response.Redirect("admin.tambah.maklumat.pengumuman.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub

    Protected Sub BtnCreateSukan_Click(sender As Object, e As EventArgs) Handles BtnCreateSukan.Click
        Response.Redirect("admin.tambah.maklumat.pengumuman.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub

    Protected Sub DokumenView_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles DokumenView.RowDeleting
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim strKeyCode As String = DokumenView.DataKeys(e.RowIndex).Value.ToString

        Try
            Dim MyConnection As SqlConnection = New SqlConnection(strConn)
            Dim Dlt_NewCourse As New SqlDataAdapter()

            Dim dlt_Course As String

            Dlt_NewCourse.SelectCommand = New SqlCommand()
            Dlt_NewCourse.SelectCommand.Connection = MyConnection
            Dlt_NewCourse.SelectCommand.CommandText = "delete koko_content where ContentID='" & strKeyCode & "'"
            MyConnection.Open()
            dlt_Course = Dlt_NewCourse.SelectCommand.ExecuteScalar()
            MyConnection.Close()

            strRet = BindDokumen(DokumenView)
        Catch ex As Exception
            ''lblMsg.Text = "System Error: " & ex.Message
        End Try
    End Sub

    Protected Sub btnAddDoc_Click(sender As Object, e As EventArgs) Handles btnAddDoc.Click
        Response.Redirect("admin.tambah.maklumat.dokumen.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub



End Class