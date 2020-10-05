Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class subject_List
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getFieldValue("SELECT A.configString FROM permatapintar.dbo.master_Config A WHERE A.configCode='DefaultPPCSDate'")

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Sub ppcsdate_list()
        Dim strSQL As String = "SELECT A.PPCSDate FROM permatapintar.dbo.ppcs A GROUP BY A.PPCSDate"

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

            'ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 1200
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod ditemui!"
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

    Private Function getSQL() As String
        strSQL = "select staff_name,staff_login,staff_position from staff_info where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'"
        Dim strSQL2 As String = "select staff_name,staff_login,staff_position from staff_info where staff_login = '" & CType(Session.Item("permata_adminE"), String) & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim sqlDA2 As New SqlDataAdapter(strSQL2, objConn)
        Dim name As String
        Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")
            sqlDA2.Fill(ds, "AnyTable")


            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            '--Account Details 
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_name")) Then
                name = ds.Tables(0).Rows(0).Item("staff_name")
                Dim tmpSQL As String
                Dim strWhere As String = ""
                Dim strOrder As String = " ORDER BY CourseType,CourseCode,CourseNameBM ASC"

                tmpSQL = "SELECT b.CourseCode,b.CourseNameBM,a.ClassID,a.ClassCode,a.ClassNameBM,a.Tempat FROM PPCS_Class a, PPCS_Course b"

                strWhere = " WHERE a.CourseID=b.CourseID AND a.PPCSDate='" & ddlPPCSDate.Text & "' and (b.NamaKetuaModul like '%" & name & "%'
                     or b.NamaKetuaModulBI like '%" & name & "%' or a.NamaPengajar like '%" & name & "%'
                     or a.NamaPembantuPengajar like '%" & name & "%' or a.NamaPembantuPelajar like '%" & name & "%')
                    "


                getSQL = tmpSQL & strWhere & strOrder
                ''--debug
                'Response.Write(getSQL)

                Return getSQL
            End If
        End If




    End Function


    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        '--Response.Write(strKeyID)

        Response.Redirect("admin.laporan.keseluruhan.class.select.aspx?courseid=" & strKeyID & "&ppcsdate=" & ddlPPCSDate.Text)

    End Sub

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function


End Class