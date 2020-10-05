Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class ukm3_transfer
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnTransfer.Attributes.Add("onclick", "return confirm('Pasti ingin transfer ke UKM3?');")

        Try
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = ConfigurationManager.AppSettings("DefaultPPCSDate")

                koko_tahun_list()
                ddlTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")

                lblIsTransfered.Text = isTransfered.ToString

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Function isTransfered() As Boolean
        '--transfer if only koko_pelajar record not exist
        strSQL = "SELECT kokopelajarid FROM koko_pelajar WHERE PPCSDate='" & ddlPPCSDate.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub koko_tahun_list()
        strSQL = "SELECT Tahun FROM koko_tahun ORDER BY Tahun ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

            'ddlTahun.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ppcsdate_list()
        strSQL = "SELECT PPCSDate FROM master_PPCSDate ORDER BY ppcsid ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

            '--ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)
        lblIsTransfered.Text = isTransfered.ToString

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
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

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        tmpSQL = "SELECT UKM3.StudentID,UKM3.TotalPercentage,UKM3.PPMT,UKM3.Program,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.NoPelajar,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentGender,StudentProfile.StudentReligion FROM UKM3"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM3.StudentID=StudentProfile.StudentID"
        strWhere = " WHERE UKM3.PPMT='Y' AND UKM3.PPCSDate='" & ddlPPCSDate.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        '--transfer if only koko_pelajar record not exist
        If isTransfered() = False Then
            koko_pelajar_insert()
        Else
            lblMsg.Text = "Rekod UKM3 sudah ditransfer ke kokurikulum pelajar sebelum ini."
        End If

    End Sub

    Private Sub koko_pelajar_insert()
        ' Create source connection
        Dim source As New SqlConnection(strConn)
        ' Create destination connection
        Dim destination As New SqlConnection(strConn)
        ' Clean up destination table. Your destination database must have the 
        ' table with schema which you are copying data to. 
        ' Before executing this code, you must create a table BulkDataTable 
        ' in your database where you are trying to copy data to.
        Dim cmd As New SqlCommand("DELETE FROM koko_pelajar WHERE PPCSDate='" & ddlPPCSDate.Text & "'", destination)    ''dont have to cleanup
        ''dont have to cleanup
        ' Open source and destination connections.
        Try
            source.Open()
            destination.Open()
            cmd.ExecuteNonQuery()

            ' Select data from Products table
            cmd = New SqlCommand(SQLTransfer, source)    '--sama dengan search
            ' Execute reader
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            ' Create SqlBulkCopy
            Dim bulkData As New SqlBulkCopy(destination)
            ' Set destination table name
            bulkData.DestinationTableName = "koko_pelajar"
            ' Write data
            bulkData.WriteToServer(reader)
            ' Close objects
            bulkData.Close()
            destination.Close()
            source.Close()

            lblMsg.Text = "Transfer completed!"
        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        End Try

    End Sub

    Private Function SQLTransfer() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentID"

        tmpSQL = "SELECT UKM3ID,StudentID,PPCSDate,'" & ddlTahun.Text & "',Program FROM UKM3"
        strWhere = " WITH (NOLOCK) WHERE PPMT='Y' AND PPCSDate ='" & ddlPPCSDate.Text & "'"

        SQLTransfer = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(SQLTransfer)

        Return SQLTransfer

    End Function

End Class