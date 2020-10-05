Imports System.Data.SqlClient

Public Class admin_survey_online
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnDelYear.Attributes.Add("onclick", "return confirm('Pasti ingin buang Survey ID yang dipilih?');")

        If Not IsPostBack Then
            showDefault()
            BindData(datRespondent)
        End If

    End Sub

    Private Sub BindData(ByVal gvTable As GridView)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim myDataSet As New DataSet

        Dim myDataAdapter As New SqlDataAdapter("SELECT master_surveyid, SurveyID, displayDdl FROM master_surveyid ORDER BY master_surveyid DESC", strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()

        Catch ex As Exception

        End Try

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

            Dim query As String = "SELECT master_surveyid FROM master_surveyid WHERE SurveyID = '" & txtYear.Text & "'"
            Dim oCommon As Commonfunction = New Commonfunction

            If oCommon.isExist(query) Then
                lblMsg.Text = "Error! Survey ID yang sama tidak boleh berulang!"
                Exit Sub
            End If

            Dim mAdapter As New SqlDataAdapter

            query = "INSERT INTO master_surveyid (SurveyID) VALUES (@surveyid)"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@surveyid", txtYear.Text))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            lblMsg.Text = "Error! Gagal tambah Survey ID."
        End Try

        lblMsg.Text = "Tambah Survey ID berjaya!"
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

                    Dim master_surveyid As String = datRespondent.DataKeys(i).Value.ToString
                    Dim sqlSurvey As String = "SELECT SurveyID FROM master_surveyid WHERE master_surveyid = " & master_surveyid

                    Dim checkSQL As String = " SELECT COUNT(*) FROM EQTest WHERE SurveyID = '" & oCommon.getFieldValue(sqlSurvey) & "'"
                    Dim total As Integer = CType(oCommon.getFieldValue(checkSQL), Integer)

                    If total = 0 Then
                        checkSQL = " SELECT COUNT(*) FROM SainsTest WHERE SurveyID = '" & oCommon.getFieldValue(sqlSurvey) & "'"
                        total = CType(oCommon.getFieldValue(checkSQL), Integer)

                        If total = 0 Then
                            checkSQL = " SELECT COUNT(*) FROM StressTest WHERE SurveyID = '" & oCommon.getFieldValue(sqlSurvey) & "'"
                            total = CType(oCommon.getFieldValue(checkSQL), Integer)

                            If total = 0 Then
                                gotQuery = True
                                strSQL += " DELETE master_surveyid WHERE master_surveyid = " & master_surveyid
                            End If
                        End If
                    End If
                End If

            Next

            If gotQuery Then
                oCommon.ExecuteSQL(strSQL)
                lblMsg.Text = "Berjaya buang Survey ID!"
            Else
                lblMsg.Text = "Tidak berjaya buang Survey ID!"
            End If

            BindData(datRespondent)

        Catch ex As Exception
            lblMsg.Text = "Error! Gagal buang Survey ID!"
        End Try

    End Sub

    Private Sub showDefault()

        Dim oCommon As Commonfunction = New Commonfunction

        Dim strSQL As String = "SELECT master_surveyid FROM master_surveyid WHERE SurveyID = (SELECT configString FROM master_Config WHERE configCode = 'EQTest_SurveyID')"
        Dim master_surveyid As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT master_surveyid FROM master_surveyid WHERE SurveyID = (SELECT configString FROM master_Config WHERE configCode = 'SainsTest_SurveyID')"
        Dim sains_surveyid As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT master_surveyid FROM master_surveyid WHERE SurveyID = (SELECT configString FROM master_Config WHERE configCode = 'StressTest_SurveyID')"
        Dim stress_surveyid As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT master_surveyid,SurveyID FROM master_surveyid WHERE displayDdl = 1 ORDER BY master_surveyid DESC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlDefault.DataSource = ds
            ddlDefault.DataTextField = "SurveyID"
            ddlDefault.DataValueField = "master_surveyid"
            ddlDefault.DataBind()

            ddlDefault.SelectedValue = master_surveyid

            ddlSains.DataSource = ds
            ddlSains.DataTextField = "SurveyID"
            ddlSains.DataValueField = "master_surveyid"
            ddlSains.DataBind()

            ddlSains.SelectedValue = sains_surveyid

            ddlStress.DataSource = ds
            ddlStress.DataTextField = "SurveyID"
            ddlStress.DataValueField = "master_surveyid"
            ddlStress.DataBind()

            ddlStress.SelectedValue = stress_surveyid

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnEQ_Click(sender As Object, e As EventArgs) Handles btnEQ.Click

        Try

            Dim query As String = "UPDATE master_Config SET configString = (SELECT SurveyID FROM master_surveyid WHERE master_surveyid = @master_surveyid) WHERE configCode = 'EQTest_SurveyID'"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@master_surveyid", ddlDefault.SelectedValue))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            lblMsg.Text = "Error! Gagal tukar Survey ID EQTest terkini."
        End Try

        lblMsg.Text = "Berjaya tukar Survey ID EQTest terkini."
        BindData(datRespondent)

    End Sub

    Private Sub btnSains_Click(sender As Object, e As EventArgs) Handles btnSains.Click

        Try

            Dim query As String = "UPDATE master_Config SET configString = (SELECT SurveyID FROM master_surveyid WHERE master_surveyid = @master_surveyid) WHERE configCode = 'SainsTest_SurveyID'"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@master_surveyid", ddlSains.SelectedValue))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            lblMsg.Text = "Error! Gagal tukar Survey ID Sains Test terkini."
        End Try

        lblMsg.Text = "Berjaya tukar Survey ID Sains Test terkini."
        BindData(datRespondent)

    End Sub

    Private Sub btnStress_Click(sender As Object, e As EventArgs) Handles btnStress.Click

        Try

            Dim query As String = "UPDATE master_Config SET configString = (SELECT SurveyID FROM master_surveyid WHERE master_surveyid = @master_surveyid) WHERE configCode = 'StressTest_SurveyID'"

            Dim strconn As String = ConfigurationManager.AppSettings("ConnectionString")

            Using mConn As New SqlConnection(strconn)
                Using mCmd As New SqlCommand(query, mConn)
                    mCmd.Parameters.Add(New SqlParameter("@master_surveyid", ddlStress.SelectedValue))
                    mConn.Open()
                    mCmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            lblMsg.Text = "Error! Gagal tukar Survey ID Stress Test terkini."
        End Try

        lblMsg.Text = "Berjaya tukar Survey ID Stress Test terkini."
        BindData(datRespondent)

    End Sub

    Protected Sub datRespondent_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles datRespondent.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then

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
            Dim ddlDdl As DropDownList = CType(datRespondent.Rows(i).FindControl("ddlDdl"), DropDownList)

            If chkUpdate.Checked Then

                gotQuery = True
                Dim master_surveyid As String = datRespondent.DataKeys(i).Value.ToString

                strSQL += " UPDATE master_surveyid SET  displayDdl = " & ddlDdl.SelectedValue & " WHERE master_surveyid = " & master_surveyid

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

End Class