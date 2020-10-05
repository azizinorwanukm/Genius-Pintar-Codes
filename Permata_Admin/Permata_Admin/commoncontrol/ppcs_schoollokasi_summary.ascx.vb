''Imports System.Data
''Imports System.Data.OleDb
Imports System.Data.SqlClient
''Imports System.IO
''Imports System.Globalization

Partial Public Class ppcs_schoollokasi_summary1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                PPCSStatus_list()
                ddlPPCSStatus.Text = "LAYAK"
                statusTawaran_list()
            End If

        Catch ex As Exception

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

    Private Sub PPCSStatus_list()
        strSQL = "SELECT PPCSStatus FROM master_PPCSStatus ORDER BY PPCSStatus"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--ddlPPCSStatus
            ddlPPCSStatus.DataSource = ds
            ddlPPCSStatus.DataTextField = "PPCSStatus"
            ddlPPCSStatus.DataValueField = "PPCSStatus"
            ddlPPCSStatus.DataBind()

            ddlPPCSStatus.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod dijumpai."
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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim strToday As String = Now.Year & oCommon.DoPadZeroLeft(Now.Month.ToString, 2) & oCommon.DoPadZeroLeft(Now.Day.ToString, 2)

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strGroupby As String = " GROUP BY SchoolProfile.SchoolLokasi"
        Dim strOrder As String = " ORDER BY Jumlah DESC"

        'tmpSQL = "SELECT c.SchoolLokasi, COUNT(*) as nTotal FROM PPCS a, StudentSchool b, SchoolProfile c"
        'strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND b.SchoolID=c.SchoolID AND PPCSStatus='LAYAK'"

        tmpSQL = "SELECT SchoolProfile.SchoolLokasi, COUNT(*) as Jumlah FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON PPCS.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON PPCS.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID AND SchoolProfile.IsDeleted='N'"
        strWhere = " WHERE PPCS.PPCSDate ='" & ddlPPCSDate.Text & "'"

        '--PPCSStatus
        If Not ddlPPCSStatus.Text = "ALL" And ddlTawaran.Text = "ALL" Then
            strWhere += " AND PPCS.PPCSStatus='" & ddlPPCSStatus.Text & "'"
        End If

        If Not ddlTawaran.Text = "ALL" Then
            strWhere += " AND PPCS.StatusTawaran='" & ddlTawaran.Text & "'"
        End If

        strSQL = tmpSQL & strWhere & strGroupby & strOrder
        Return strSQL

    End Function

    Private Sub displayDebug(ByVal strMsg As String)
        If oCommon.getAppsettings("isDebug") = "Y" Then
            lblDebug.Text = strMsg
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("ppcs.schoollokasi.list.aspx?schoollokasi=" & strKeyID & "&ppcsdate=" & ddlPPCSDate.Text & "&ppcsstatus=" & ddlPPCSStatus.Text)
            Case "ADMINOP"
                Response.Redirect("ppcs.schoollokasi.list.aspx?schoollokasi=" & strKeyID & "&ppcsdate=" & ddlPPCSDate.Text & "&ppcsstatus=" & ddlPPCSStatus.Text)
            Case "SUBADMIN"
            Case Else
        End Select

    End Sub

    Private Sub statusTawaran_list()
        ddlTawaran.Items.Add(New ListItem("ALL", "ALL"))
        ddlTawaran.Items.Add(New ListItem("TERIMA", "TERIMA"))
        ddlTawaran.Items.Add(New ListItem("TOLAK", "TOLAK"))
    End Sub

    Private Sub ddlTawaran_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTawaran.TextChanged
        If Not ddlTawaran.SelectedValue = "ALL" Then
            ddlPPCSStatus.Enabled = False
        Else
            ddlPPCSStatus.Enabled = True
        End If

    End Sub
End Class