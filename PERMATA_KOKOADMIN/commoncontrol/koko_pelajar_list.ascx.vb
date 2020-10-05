Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class koko_pelajar_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnReset.Attributes.Add("onclick", "return confirm('Pasti ingin RESET koko pelajar tersebut?');")
        Try
            If Not IsPostBack Then
                setAccessRight
                getKOKODetail()
                '--default list
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub setAccessRight()
        Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
            Case "ADMIN"
                btnReset.Visible = True
            Case Else
                btnReset.Visible = False
        End Select

    End Sub

    '--getKOKODetail
    Private Sub getKOKODetail()
        strSQL = "SELECT Jenis FROM koko_kolejpermata WHERE KokoID=" & Request.QueryString("kokoid")
        lblJenis.Text = oCommon.getFieldValue(strSQL)
        Select Case lblJenis.Text
            Case "PERSATUAN"
                lblFieldname.Text = "PersatuanID"
            Case "RUMAHSUKAN"
                lblFieldname.Text = "RumahsukanID"
            Case "SUKAN"
                lblFieldname.Text = "SukanID"
            Case "UNIFORM"
                lblFieldname.Text = "UniformID"
        End Select

        strSQL = "SELECT Nama FROM koko_kolejpermata WHERE KokoID=" & Request.QueryString("kokoid")
        lblKOKOName.Text = oCommon.getFieldValue(strSQL)
        lblKOKONameReset.Text = lblKOKOName.Text

        strSQL = "SELECT Tahun FROM koko_kolejpermata WHERE KokoID=" & Request.QueryString("kokoid")
        lblTahun.Text = oCommon.getFieldValue(strSQL)

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
                
                lblMsg.Text = "Tiada rekod pelajar."
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

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        tmpSQL = "SELECT koko_pelajar.kokopelajarid,koko_pelajar.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.NoPelajar,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentGender,StudentProfile.StudentReligion,koko_pelajar.Tahun,koko_pelajar.StatusTawaran,koko_kelas.Kelas,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan"
        tmpSQL += " FROM koko_pelajar"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID"
        strWhere = " WHERE koko_pelajar.Tahun='" & lblTahun.Text & "'"
        strWhere += " AND koko_pelajar." & lblFieldname.Text & "=" & Request.QueryString("kokoid")

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case Server.HtmlEncode(Request.Cookies("kokoadmin_usertype").Value)
                Case "ADMIN"
                    Response.Redirect("admin.pelajar.view.aspx?studentid=" & strKeyID & "&admin_ID=" & Request.QueryString("admin_ID"))
                Case "INSTRUKTOR"
                    Response.Redirect("instruktor.pelajar.view.aspx?studentid=" & strKeyID & "&admin_ID=" & Request.QueryString("admin_ID"))
                Case "PENSYARAH"
                Case "PENGARAH"
                Case Else
                    lblMsg.Text = "Anda tiada kebenaran untuk meneruskan fungsi ini. Sila berhubung dengan admin jika ini satu kesilapan."
            End Select

        Catch ex As Exception
            lblMsg.Text = "System Error: " & ex.Message
        End Try

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            ExportToCSV(getSQL)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try
    End Sub

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

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        Try
            'Loop through gridview rows to find checkbox 
            'and check whether it is checked or not 
            Dim i As Integer
            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(12).FindControl("chkSelect"), CheckBox)
                If Not chkUpdate Is Nothing Then
                    ' Get the values of textboxes using findControl
                    Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                    If chkUpdate.Checked = True Then
                        strSQL = "UPDATE koko_pelajar SET " & lblFieldname.Text & "=NULL WHERE StudentID='" & strKey & "' AND Tahun='" & lblTahun.Text & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
                        End If
                    End If
                End If
            Next

            If lblMsg.Text.Length = 0 Then
                lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
            End If

            '--default list
            strRet = BindData(datRespondent)

        Catch ex As Exception
            lblMsgTop.Text = "System error:" & ex.Message
        End Try
    End Sub
End Class