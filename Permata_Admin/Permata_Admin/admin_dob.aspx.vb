Imports System.Data.SqlClient

Public Class admin_dob
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnDelYear.Attributes.Add("onclick", "return confirm('Pasti ingin buang tahun lahir yang dipilih?');")

        If Not IsPostBack Then
            BindData(datRespondent)
        End If

    End Sub

    Private Sub BindData(ByVal gvTable As GridView)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim myDataSet As New DataSet

        Dim myDataAdapter As New SqlDataAdapter("SELECT dobyearid,DOB_Year,display FROM master_Dobyear ORDER BY DOB_Year", strConn)
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
            Dim ddlDob As DropDownList = TryCast(e.Row.FindControl("ddlDob"), DropDownList)
            Dim lblDisplay As Label = TryCast(e.Row.FindControl("lblDisplay"), Label)

            ddlDob.Items.Add(New ListItem("Papar", 1))
            ddlDob.Items.Add(New ListItem("Tidak Papar", 0))

            ddlDob.SelectedValue = lblDisplay.Text

        End If

    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        Dim i As Integer
        Dim strSQL As String = ""
        Dim gotQuery As Boolean = False

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1

            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            Dim ddlDob As DropDownList = CType(datRespondent.Rows(i).FindControl("ddlDob"), DropDownList)

            If chkUpdate.Checked Then

                gotQuery = True
                Dim dobyearid As String = datRespondent.DataKeys(i).Value.ToString

                strSQL += " UPDATE master_Dobyear SET display = " & ddlDob.SelectedValue & " WHERE dobyearid = " & dobyearid

            End If

        Next

        If gotQuery Then
            Dim oCommon As Commonfunction = New Commonfunction
            oCommon.ExecuteSQL(strSQL)
        End If

        lblMsg.Text = "Berjaya kemaskini status paparan tahun lahir."
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

            Dim query As String = "SELECT dobyearid FROM master_Dobyear WHERE DOB_Year = '" & txtYear.Text & "'"
            Dim oCommon As Commonfunction = New Commonfunction

            If oCommon.isExist(query) Then
                lblMsg.Text = "Error! Tahun yang sama tidak boleh berulang!"
                Exit Sub
            End If

            Dim mAdapter As New SqlDataAdapter

            query = "INSERT INTO master_Dobyear (DOB_Year) VALUES (@year)"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@year", txtYear.Text))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            lblMsg.Text = "Error! Gagal tambah tahun lahir."
        End Try

        lblMsg.Text = "Tambah tahun lahir berjaya!"
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
                    gotQuery = True
                    Dim dobyearid As String = datRespondent.DataKeys(i).Value.ToString
                    strSQL += " DELETE master_Dobyear WHERE dobyearid = " & dobyearid
                End If

            Next

            If gotQuery Then
                oCommon.ExecuteSQL(strSQL)
                lblMsg.Text = "Berjaya buang tahun lahir!"
            Else
                lblMsg.Text = "Tidak berjaya buang tahun lahir!"
            End If

            BindData(datRespondent)

        Catch ex As Exception
            lblMsg.Text = "Error! Gagal buang tahun lahir!"
        End Try

    End Sub

End Class