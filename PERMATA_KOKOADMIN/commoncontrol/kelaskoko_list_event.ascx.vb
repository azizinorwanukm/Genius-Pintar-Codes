﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class kelaskoko_list_event
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                '--default
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub




    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then

                lblMsg.Text = "Tiada rekod ditemui."
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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim kelaskokoid As String = Request.QueryString("kelaskokoid")

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY koko_kelas.Kelas"
        tmpSQL = "SELECT StudentProfile.StudentID, StudentProfile.StudentFullName, StudentProfile.MYKAD, StudentProfile.NoPelajar, koko_pelajar.Tahun, StudentProfile.StudentRace, koko_kelas.Kelas FROM StudentProfile"
        tmpSQL += " LEFT JOIN koko_pelajar ON StudentProfile.StudentID = koko_pelajar.StudentID"
        tmpSQL += " LEFT JOIN koko_kelaskokopelajar ON koko_pelajar.kokopelajarid = koko_kelaskokopelajar.kokopelajarid"
        tmpSQL += " LEFT JOIN koko_kelaskoko ON koko_kelaskokopelajar.KelasKokoID = koko_kelaskoko.KelasKokoID"
        tmpSQL += " LEFT JOIN koko_kolejpermata ON koko_kelaskoko.KokoID = koko_kolejpermata.KokoID"
        tmpSQL += " LEFT JOIN koko_kelas ON koko_pelajar.KelasID = koko_kelas.KelasID"
        strWhere = " WHERE koko_kelaskoko.KelasKokoID = '" & kelaskokoid & "'"
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=KOKO_File.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

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

    Private Sub btnKemaskini_Click(sender As Object, e As EventArgs) Handles btnKemaskini.Click

        Dim i As Integer

        For i = 0 To datRespondent.Rows.Count Step i + 1



            'Try
            'check form validation. if failed exit
            'If ValidateForm() = False Then
            'Exit Sub
            'End If

            'insert into course list
            'strSQL = "INSERT INTO koko_pelajarmandatory(StudentID, InstruktorID, EventDate, Title, KokoID, Agenda) VALUES ('" & lblTahun.Text & "','" & Request.QueryString("instruktorid") & "','" & lblEventDate.Text & "','" & oCommon.FixSingleQuotes(txtTitle.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(ddlKokoID.Text) & "','" & oCommon.FixSingleQuotes(txtAgenda.Text) & "')"
            'strRet = oCommon.ExecuteSQL(strSQL)
            'If strRet = "0" Then
            'lblMsg.Text = "Kemaskini berjaya!"
            'Else
            'lblMsg.Text = "Gagal. " & strRet
            'End If

            'Catch ex As Exception
            'lblMsg.Text = "System error:" & ex.Message
            'End Try

        Next


    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtEventDate.Text.Length = 0 Then
            lblMsg.Text = "Medan ini mesti diisi."
            txtEventDate.Focus()
            Return False
        End If

        Return True
    End Function

    Private Sub calEventDate_SelectionChanged(sender As Object, e As EventArgs) Handles calEventDate.SelectionChanged
        txtEventDate.Text = oCommon.DateDisplay(calEventDate.SelectedDate)
        calEventDate.Visible = False
    End Sub

    Private Sub btnDate_Click(sender As Object, e As ImageClickEventArgs) Handles btnDate.Click
        Dim [date] As New DateTime()
        'Flip the visibility attribute
        calEventDate.Visible = Not (calEventDate.Visible)
        'If the calendar is visible try assigning the date from the textbox
        If calEventDate.Visible Then
            'If the Conversion was successfull assign the textbox's date
            If DateTime.TryParse(txtEventDate.Text, [date]) Then
                calEventDate.SelectedDate = [date]
            End If
            calEventDate.Attributes.Add("style", "POSITION: absolute")
        End If

    End Sub
End Class