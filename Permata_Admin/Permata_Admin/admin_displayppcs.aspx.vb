Imports System.Data.SqlClient

Public Class admin_displayppcs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnDelYear.Attributes.Add("onclick", "return confirm('Pasti ingin buang tarikh yang dipilih?');")

        If Not IsPostBack Then
            showDefault()
            BindData(datRespondent)
        End If

    End Sub

    Private Sub BindData(ByVal gvTable As GridView)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim myDataSet As New DataSet

        Dim myDataAdapter As New SqlDataAdapter("SELECT * FROM master_PPCSDate ORDER BY ppcsid DESC", strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub datRespondent_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles datRespondent.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlPpcs As DropDownList = TryCast(e.Row.FindControl("ddlPpcs"), DropDownList)
            Dim lblPpcs As Label = TryCast(e.Row.FindControl("lblPpcs"), Label)

            ddlPpcs.Items.Add(New ListItem("Papar", 1))
            ddlPpcs.Items.Add(New ListItem("Tidak Papar", 0))

            ddlPpcs.SelectedValue = lblPpcs.Text

            'Display Ddl
            Dim ddlDdl As DropDownList = TryCast(e.Row.FindControl("ddlDdl"), DropDownList)
            Dim lblDdl As Label = TryCast(e.Row.FindControl("lblDdl"), Label)

            ddlDdl.Items.Add(New ListItem("Wujud", 1))
            ddlDdl.Items.Add(New ListItem("Tiada", 0))

            ddlDdl.SelectedValue = lblDdl.Text

        End If

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Dim i As Integer
        Dim strSQL As String = ""
        Dim gotQuery As Boolean = False

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1

            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            Dim ddlPpcs As DropDownList = CType(datRespondent.Rows(i).FindControl("ddlPpcs"), DropDownList)
            Dim ddlDdl As DropDownList = CType(datRespondent.Rows(i).FindControl("ddlDdl"), DropDownList)

            If chkUpdate.Checked Then

                gotQuery = True
                Dim ppcsid As String = datRespondent.DataKeys(i).Value.ToString

                strSQL += " UPDATE master_PPCSDate SET displayHistory = " & ddlPpcs.SelectedValue & ", displayDdl = " & ddlDdl.SelectedValue & " WHERE ppcsid = " & ppcsid

            End If

        Next

        If gotQuery Then
            Dim oCommon As Commonfunction = New Commonfunction
            oCommon.ExecuteSQL(strSQL)
            lblMsg.Text = "Berjaya kemaskini status paparan PPCS."
        Else
            lblMsg.Text = "Tiada row dipilih. Kemaskini gagal."
        End If

        BindData(datRespondent)

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("settings.aspx")

    End Sub

    Private Sub btnDisplayAdd_Click(sender As Object, e As EventArgs) Handles btnDisplayAdd.Click

        btnDisplayAdd.Visible = False
        txtYear.Visible = True
        btnAddYear.Visible = True
        btnCancelAdd.Visible = True

    End Sub

    Private Sub btnCancelAdd_Click(sender As Object, e As EventArgs) Handles btnCancelAdd.Click

        btnDisplayAdd.Visible = True
        txtYear.Visible = False
        btnAddYear.Visible = False
        btnCancelAdd.Visible = False

    End Sub

    Private Sub btnAddYear_Click(sender As Object, e As EventArgs) Handles btnAddYear.Click

        Try

            Dim query As String = "SELECT PPCSDate FROM master_PPCSDate WHERE PPCSDate = '" & txtYear.Text & "'"
            Dim oCommon As Commonfunction = New Commonfunction

            If oCommon.isExist(query) Then
                lblMsg.Text = "Error! Tarikh yang sama tidak boleh berulang!"
                Exit Sub
            End If

            Dim mAdapter As New SqlDataAdapter

            query = "INSERT INTO master_PPCSDate (PPCSDate) VALUES (@year)"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@year", txtYear.Text))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            lblMsg.Text = "Error! Gagal tambah tarikh PPCS."
        End Try

        lblMsg.Text = "Tambah tarikh PPCS berjaya!"
        BindData(datRespondent)

    End Sub

    Private Sub btnDelYear_Click(sender As Object, e As EventArgs) Handles btnDelYear.Click

        Try
            Dim i As Integer
            Dim strSQL As String = ""
            Dim gotQuery As Boolean = False
            Dim oCommon As Commonfunction = New Commonfunction

            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1

                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)

                If chkUpdate.Checked Then

                    Dim ppcsid As String = datRespondent.DataKeys(i).Value.ToString
                    Dim sqlPPcs As String = "SELECT PPCSDate FROM master_PPCSDate WHERE ppcsid = " & ppcsid

                    Dim checkSQL As String = " SELECT COUNT(*) FROM PPCS WHERE PPCSDate = '" & oCommon.getFieldValue(sqlPPcs) & "'"
                    Dim total As Integer = CType(oCommon.getFieldValue(checkSQL), Integer)

                    If total = 0 Then
                        gotQuery = True
                        strSQL += " DELETE master_PPCSDate WHERE ppcsid = " & ppcsid
                    End If

                End If

            Next

            If gotQuery Then
                oCommon.ExecuteSQL(strSQL)
                lblMsg.Text = "Berjaya buang tarikh PPCS!"
            Else
                lblMsg.Text = "Tiada row dipilih. Gagal buang tarikh PPCS!"
            End If

            BindData(datRespondent)

        Catch ex As Exception
            lblMsg.Text = "Error! Gagal buang tarikh PPCS!"
        End Try

    End Sub

    Private Sub showDefault()

        Dim strSQL As String = "SELECT ppcsid FROM master_PPCSDate WHERE PPCSDate = (SELECT configString FROM master_Config WHERE configCode = 'DefaultPPCSDate')"
        Dim oCommon As Commonfunction = New Commonfunction
        Dim ppcsid As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT ppcsid,PPCSDate FROM master_PPCSDate WHERE displayDdl = 1 ORDER BY ppcsid DESC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlDefault.DataSource = ds
            ddlDefault.DataTextField = "PPCSDate"
            ddlDefault.DataValueField = "ppcsid"
            ddlDefault.DataBind()

            ddlDefault.SelectedValue = ppcsid

            'ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnDefault_Click(sender As Object, e As EventArgs) Handles btnDefault.Click

        Try

            Dim query As String = "UPDATE master_Config SET configString = (SELECT PPCSDate FROM master_PPCSDate WHERE ppcsid = @ppcsid) WHERE configCode = 'DefaultPPCSDate'"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@ppcsid", ddlDefault.SelectedValue))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            lblMsg.Text = "Error! Gagal tukar tarikh terkini PPCS."
        End Try

        lblMsg.Text = "Berjaya tukar tarikh terkini PPCS."
        BindData(datRespondent)

    End Sub

End Class