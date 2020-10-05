Imports System.Data.SqlClient

Public Class admin_display_examresults
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnDelYear.Attributes.Add("onclick", "return confirm('Pasti ingin buang tahun?');")

        If Not IsPostBack Then
            showDefault()
            BindData(datRespondent)
        End If

    End Sub

    Private Sub BindData(ByVal gvTable As GridView)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim myDataSet As New DataSet

        Dim myDataAdapter As New SqlDataAdapter("SELECT * FROM master_examyear ORDER BY ExamYear DESC", strConn)
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

            'Display UKM1
            Dim ddl_UKM1 As DropDownList = TryCast(e.Row.FindControl("ddl_UKM1"), DropDownList)
            Dim lblUKM1 As Label = TryCast(e.Row.FindControl("lblUKM1"), Label)

            ddl_UKM1.Items.Add(New ListItem("Papar", 1))
            ddl_UKM1.Items.Add(New ListItem("Tidak Papar", 0))

            ddl_UKM1.SelectedValue = lblUKM1.Text

            'Display UKM2
            Dim ddl_UKM2 As DropDownList = TryCast(e.Row.FindControl("ddl_UKM2"), DropDownList)
            Dim lblUKM2 As Label = TryCast(e.Row.FindControl("lblUKM2"), Label)

            ddl_UKM2.Items.Add(New ListItem("Papar", 1))
            ddl_UKM2.Items.Add(New ListItem("Tidak Papar", 0))

            ddl_UKM2.SelectedValue = lblUKM2.Text

            'Display dropdown
            Dim ddl_Ddl As DropDownList = TryCast(e.Row.FindControl("ddl_Ddl"), DropDownList)
            Dim lblDdl As Label = TryCast(e.Row.FindControl("lblDdl"), Label)

            ddl_Ddl.Items.Add(New ListItem("Wujud", 1))
            ddl_Ddl.Items.Add(New ListItem("Tiada", 0))

            ddl_Ddl.SelectedValue = lblDdl.Text


        End If

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Dim i As Integer
        Dim strSQL As String = ""
        Dim gotQuery As Boolean = False

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1

            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            Dim ddl_UKM1 As DropDownList = CType(datRespondent.Rows(i).FindControl("ddl_UKM1"), DropDownList)
            Dim ddl_UKM2 As DropDownList = CType(datRespondent.Rows(i).FindControl("ddl_UKM2"), DropDownList)
            Dim ddl_Ddl As DropDownList = CType(datRespondent.Rows(i).FindControl("ddl_Ddl"), DropDownList)

            If chkUpdate.Checked Then

                gotQuery = True
                Dim examyearid As String = datRespondent.DataKeys(i).Value.ToString

                strSQL += " UPDATE master_Examyear SET displayUKM1 = " & ddl_UKM1.SelectedValue & ", displayUKM2 = " & ddl_UKM2.SelectedValue & ",displayDdl = " & ddl_Ddl.SelectedValue & " WHERE examyearid = " & examyearid

            End If

        Next

        If gotQuery Then
            Dim oCommon As Commonfunction = New Commonfunction
            oCommon.ExecuteSQL(strSQL)
            lblMsg.Text = "Berjaya kemaskini status paparan."
        Else
            lblMsg.Text = "Tiada row dipilih. Gagal kemaskini status paparan."
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

            Dim query As String = "SELECT examyearid FROM master_Examyear WHERE ExamYear = '" & txtYear.Text & "'"
            Dim oCommon As Commonfunction = New Commonfunction

            If oCommon.isExist(query) Then
                lblMsg.Text = "Error! Tahun yang sama tidak boleh berulang!"
                Exit Sub
            End If

            Dim mAdapter As New SqlDataAdapter

            query = "INSERT INTO master_Examyear (ExamYear) VALUES (@year)"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@year", txtYear.Text))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            lblMsg.Text = "Error! Gagal tambah tahun."
        End Try

        lblMsg.Text = "Tambah tahun berjaya!"
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

                    Dim examyearid As String = datRespondent.DataKeys(i).Value.ToString

                    Dim checkSQL As String = " SELECT COUNT(*) FROM UKM1 WHERE examyear_id = " & examyearid
                    Dim total As Integer = CType(oCommon.getFieldValue(checkSQL), Integer)

                    If total = 0 Then
                        gotQuery = True
                        strSQL += " DELETE master_Examyear WHERE examyearid = " & examyearid
                    End If

                End If

            Next

            If gotQuery Then
                oCommon.ExecuteSQL(strSQL)
                lblMsg.Text = "Berjaya buang tahun!"
            Else
                lblMsg.Text = "Tidak berjaya buang tahun!"
            End If

            BindData(datRespondent)

        Catch ex As Exception
            lblMsg.Text = "Error! Gagal buang tahun!"
        End Try

    End Sub

    Private Sub showDefault()

        Dim strSQL As String = "SELECT examyearid FROM master_Examyear WHERE ExamYear = (SELECT configString FROM master_Config WHERE configCode = 'DefaultExamYear')"
        Dim oCommon As Commonfunction = New Commonfunction
        Dim examyearid As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT examyearid,ExamYear FROM master_Examyear WHERE displayDdl = 1 ORDER BY examyearid DESC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlDefault.DataSource = ds
            ddlDefault.DataTextField = "ExamYear"
            ddlDefault.DataValueField = "examyearid"
            ddlDefault.DataBind()

            ddlDefault.SelectedValue = examyearid

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnDefault_Click(sender As Object, e As EventArgs) Handles btnDefault.Click

        Try

            Dim query As String = "UPDATE master_Config SET configString = (SELECT ExamYear FROM master_Examyear WHERE examyearid = @examyearid) WHERE configCode = 'DefaultExamYear'"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@examyearid", ddlDefault.SelectedValue))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            lblMsg.Text = "Error! Gagal tukar tahun terkini."
        End Try

        lblMsg.Text = "Berjaya tukar tahun terkini."
        BindData(datRespondent)

    End Sub

End Class