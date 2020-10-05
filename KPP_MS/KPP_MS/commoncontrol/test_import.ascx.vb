Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class test_import
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    '' connection to kolejadmin database
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strBil As String = ""
    Dim strID As String = ""
    Dim strIC As String = ""
    Dim strPNGS As String = ""
    Dim strPNGK As String = ""
    Dim strEXAM As String = ""
    Dim strYEAR As String = ""
    Dim strTYPE As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                year_info()
                exam_info_list()
                level_info()

                load_page()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub year_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Year'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "Parameter"
            ddlyear.DataValueField = "Parameter"
            ddlyear.DataBind()

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub exam_info_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Exam'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlexam_Name.DataSource = ds
            ddlexam_Name.DataTextField = "Parameter"
            ddlexam_Name.DataValueField = "Parameter"
            ddlexam_Name.DataBind()
            ddlexam_Name.Items.Insert(0, New ListItem("Select Examination", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub level_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type  = 'Level'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddllevel.DataSource = ds
            ddllevel.DataTextField = "Parameter"
            ddllevel.DataValueField = "Parameter"
            ddllevel.DataBind()
            ddllevel.DataBind()
            ddllevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub load_page()
        strSQL = "SELECT Parameter from setting where Type = 'Year' and Parameter = '" & Now.Year & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlyear.SelectedValue = ""
            End If
        End If

        strRet = BindData(datRespondent)
    End Sub

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick
        Response.Redirect("download/student_pngs_pngk.xlsx")
    End Sub

    Private Sub BtnUploaded_ServerClick(sender As Object, e As EventArgs) Handles BtnUploaded.ServerClick
        lblMsg.Text = ""
        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("exam") = "info"
            Else
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try
    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/student_gpa_cgpa/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom() + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" & fullFileName & "; Extended Properties=Excel 12.0;"
            'Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)

            Using connection As New OleDbConnection(excelConnectionString)

                Try
                    connection.Open()

                    Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [exam$]", connection)
                    Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
                    Dim ds As DataSet = New DataSet

                    da.Fill(ds)
                    Dim validationMessage As String = ValidateSiteData(ds)
                    If validationMessage = "" Then
                        SaveSiteData(ds)

                    Else
                        'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                        divMsg.Attributes("exam") = "error"
                        lblMsg.Text = "Kesalahan Kemasukkan Maklumat Kelas:<br />" & validationMessage
                        Return False
                    End If

                    da.Dispose()
                    connection.Close()
                    command.Dispose()

                Catch ex As Exception
                    lblMsg.Text = "System Error:" & ex.Message
                    Return False
                Finally
                    If connection.State = ConnectionState.Open Then
                        connection.Close()
                    End If
                End Try

            End Using
        Else
            divMsg.Attributes("exam") = "error"
            lblMsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")
                refreshVar()
                strMsg = ""

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Bil")) Then
                    strBil = SiteData.Tables(0).Rows(i).Item("Bil")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Name")) Then
                    strID = SiteData.Tables(0).Rows(i).Item("Name")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Mykad")) Then
                    strIC = SiteData.Tables(0).Rows(i).Item("Mykad")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("PNGS")) Then
                    strPNGS = SiteData.Tables(0).Rows(i).Item("PNGS")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("PNGK")) Then
                    strPNGK = SiteData.Tables(0).Rows(i).Item("PNGK")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("EXAM")) Then
                    strEXAM = SiteData.Tables(0).Rows(i).Item("EXAM")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("YEAR")) Then
                    strYEAR = SiteData.Tables(0).Rows(i).Item("YEAR")
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("TYPE")) Then
                    strTYPE = SiteData.Tables(0).Rows(i).Item("TYPE")
                End If

                If strMsg.Length = 0 Then

                Else
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim countInsert As Integer = 0
        Dim countUpdate As Integer = 0


        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")

            strBil = SiteData.Tables(0).Rows(i).Item("Bil")
            strID = SiteData.Tables(0).Rows(i).Item("Name")
            strIC = SiteData.Tables(0).Rows(i).Item("Mykad")
            strPNGS = SiteData.Tables(0).Rows(i).Item("PNGS")
            strPNGK = SiteData.Tables(0).Rows(i).Item("PNGK")
            strEXAM = SiteData.Tables(0).Rows(i).Item("EXAM")
            strYEAR = SiteData.Tables(0).Rows(i).Item("YEAR")
            strTYPE = SiteData.Tables(0).Rows(i).Item("TYPE")


            Dim get_exist As String = "select std_Id from student_info where student_Mykad = '" & strIC & "'"
            Dim data_exist As String = oCommon.getFieldValue(get_exist)

            If data_exist.Length > 0 Then

                ''update table
                strSQL = "update student_Png set png = '" & strPNGS & "', pngs = '" & strPNGK & "' where std_ID = '" & data_exist & "' and year = '" & strYEAR & "' and exam_Name = '" & strEXAM & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            Else

                ''insert new
                strSQL = "insert into student_Png(std_ID,png,pngs,exam_Name,year,student_type) values('" & data_exist & "', '" & strPNGS & "', '" & strPNGK & "', '" & strEXAM & "', '" & strYEAR & "', '" & strTYPE & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

            If strRet = 0 Then
                errorData = 0
                countInsert = countInsert + 1
            Else
                errorData = 1
            End If

        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsert & " rows inserted and " & countUpdate & " rows already exist in database ", MessageType.Success)
            value = True

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Success)
            value = False

        End If

        Return value

    End Function

    Private Sub refreshVar()

        strBil = ""
        strID = ""
        strIC = ""
        strPNGS = ""
        strPNGK = ""
        strEXAM = ""
        strYEAR = ""
        strTYPE = ""

    End Sub

    Private Sub BtnExport_ServerClick(sender As Object, e As EventArgs) Handles BtnExport.ServerClick
        ExportToCSV(getSQL)
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

    'export 
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" & Now.Year & "_StudentResult.csv")
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

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Protected Sub ddlyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlyear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by C.pngs DESC, C.png DESC, A.student_Name ASC"

        tmpSQL = "select distinct A.std_ID, A.student_Name, A.student_Mykad, B.student_Level, E.class_Name, C.exam_Name, C.year, C.png, C.pngs from student_info A
                  left join student_level B on A.std_ID = B.std_ID
                  left join student_Png C on A.std_ID = C.std_ID
                  left join course D on A.std_ID = D.std_ID
                  left join class_info E on D.class_ID = E.class_ID
                  where A.std_ID is not null
                  and A.student_Status = 'Access'
                  and E.class_type = 'Compulsory'
                  and D.year = '" & ddlyear.SelectedValue & "' and E.class_year = '" & ddlyear.SelectedValue & "'
                  and B.year = '" & ddlyear.SelectedValue & "' and C.year = '" & ddlyear.SelectedValue & "'"

        If ddlexam_Name.SelectedIndex > 0 Then
            strWhere += " and C.exam_Name = '" & ddlexam_Name.SelectedValue & "'"
        End If

        If ddllevel.SelectedIndex > 0 Then
            strWhere += " and B.student_Level = '" & ddllevel.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby
        ''--debug

        Return getSQL
    End Function

    Protected Sub ddlexam_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlexam_Name.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddllevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddllevel.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class