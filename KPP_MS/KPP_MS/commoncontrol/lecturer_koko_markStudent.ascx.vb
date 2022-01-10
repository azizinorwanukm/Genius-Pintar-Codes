Imports System.Data.SqlClient
Imports System.Globalization

Public Class lecturer_koko_markStudent
    Inherits System.Web.UI.UserControl

    Dim result As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim permataConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim masterConn As String = ConfigurationManager.AppSettings("ConnectionMaster")

    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim PermataObjCon As SqlConnection = New SqlConnection(permataConn)
    Dim masterObjConn As SqlConnection = New SqlConnection(masterConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                ddlYear()
                ddlProgram()
                ddl_KokoName()
                ddl_KokoSemester()

                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlYear()

        strSQL = "select staff_Mykad from staff_info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        strSQL = "select tahun from koko_instruktor where MYKAD = '" & strRet & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Year.DataSource = ds
            ddlKoko_Year.DataTextField = "Tahun"
            ddlKoko_Year.DataValueField = "Tahun"
            ddlKoko_Year.DataBind()
            ddlKoko_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlKoko_Year.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlProgram()

        strSQL = "Select staff_Campus from staff_Info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value <> 'Temp'"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Program.DataSource = ds
            ddlKoko_Program.DataTextField = "Parameter"
            ddlKoko_Program.DataValueField = "Value"
            ddlKoko_Program.DataBind()
            ddlKoko_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddl_KokoSemester()

        strSQL = "Select UPPER(Parameter) Parameter from setting where idx = 'Courses' and type = 'Sem'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Semester.DataSource = ds
            ddlKoko_Semester.DataTextField = "Parameter"
            ddlKoko_Semester.DataValueField = "Parameter"
            ddlKoko_Semester.DataBind()
            ddlKoko_Semester.Items.Insert(0, New ListItem("Select Semester", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddl_kokoType()

        ''Ketua Uniform
        Dim find_KetuaUniform As String = "Select koko_instruktor.KetuaUniform from koko_instruktor left join kolejadmin.dbo.staff_info A on A.staff_Mykad = koko_instruktor.MYKAD where A.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        Dim get_KetuaUniform As String = oCommon.getFieldValue_Permata(find_KetuaUniform)

        ''Ketua Persatuan
        Dim find_KetuaPersatuan As String = "Select koko_instruktor.KetuaPersatuan from koko_instruktor left join kolejadmin.dbo.staff_info A on A.staff_Mykad = koko_instruktor.MYKAD where A.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        Dim get_KetuaPersatuan As String = oCommon.getFieldValue_Permata(find_KetuaPersatuan)

        ''Ketua Sukan
        Dim find_KetuaSukan As String = "Select koko_instruktor.KetuaSukan from koko_instruktor left join kolejadmin.dbo.staff_info A on A.staff_Mykad = koko_instruktor.MYKAD where A.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        Dim get_KetuaSukan As String = oCommon.getFieldValue_Permata(find_KetuaSukan)

        ''Ketua Rumah Sukan
        Dim find_KetuaRumahSukan As String = "Select koko_instruktor.KetuaRumahSukan from koko_instruktor left join kolejadmin.dbo.staff_info A on A.staff_Mykad = koko_instruktor.MYKAD where A.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        Dim get_KetuaRumahSukan As String = oCommon.getFieldValue_Permata(find_KetuaRumahSukan)


        If get_KetuaUniform = "True" Or get_KetuaPersatuan = "True" Or get_KetuaSukan = "True" Or get_KetuaRumahSukan = "True" Then

            strSQL = "Select Distinct Jenis from koko_kolejpermata where Tahun = '" & ddlKoko_Year.SelectedValue & "' and ("

            If get_KetuaUniform = "True" Then
                strSQL += " Jenis = 'UNIFORM' "
            End If

            If get_KetuaPersatuan = "True" And get_KetuaUniform = "False" Then
                strSQL += " Jenis = 'PERSATUAN' "
            ElseIf get_KetuaPersatuan <> "False" Then
                strSQL += " or Jenis = 'PERSATUAN' "
            End If

            If get_KetuaSukan = "True" And get_KetuaPersatuan = "False" And get_KetuaUniform = "False" Then
                strSQL += " Jenis = 'SUKAN' "
            ElseIf get_KetuaSukan <> "False" Then
                strSQL += " or Jenis = 'SUKAN' "
            End If

            If get_KetuaRumahSukan = "True" And get_KetuaSukan = "False" And get_KetuaPersatuan = "False" And get_KetuaUniform = "False" Then
                strSQL += " Jenis = 'RUMAHSUKAN' "
            ElseIf get_KetuaRumahSukan <> "False" Then
                strSQL += " or Jenis = 'RUMAHSUKAN' "
            End If

            strSQL += ") and IsMandatory = 'N' "
        Else
            strSQL = " "
        End If

        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Type.DataSource = ds
            ddlKoko_Type.DataTextField = "Jenis"
            ddlKoko_Type.DataValueField = "Jenis"
            ddlKoko_Type.DataBind()
            ddlKoko_Type.Items.Insert(0, New ListItem("Select Type", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddl_KokoName()
        strSQL = "SELECT Distinct NamaBI from koko_kolejpermata where Jenis = '" & ddlKoko_Type.SelectedValue & "' and Tahun = '" & ddlKoko_Year.SelectedValue & "' "

        If ddlKoko_Type.SelectedValue = "UNIFORM" Then
            strSQL = "  Select Distinct NamaBI, KokoID from koko_kolejpermata 
                        left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.UniformID
                        left join kolejadmin.dbo.staff_Info on koko_instruktor.MYKAD = kolejadmin.dbo.staff_Info.staff_Mykad
                        Where kolejadmin.dbo.staff_Info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and koko_kolejpermata.Tahun = '" & ddlKoko_Year.SelectedValue & "' and koko_instruktor.Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        ElseIf ddlKoko_Type.SelectedValue = "PERSATUAN" Then
            strSQL = "  Select Distinct NamaBI, KokoID from koko_kolejpermata 
                        left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.PersatuanID
                        left join kolejadmin.dbo.staff_Info on koko_instruktor.MYKAD = kolejadmin.dbo.staff_Info.staff_Mykad
                        Where kolejadmin.dbo.staff_Info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and koko_kolejpermata.Tahun = '" & ddlKoko_Year.SelectedValue & "' and koko_instruktor.Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        ElseIf ddlKoko_Type.SelectedValue = "SUKAN" Then
            strSQL = "  Select Distinct NamaBI, KokoID from koko_kolejpermata 
                        left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.SukanID
                        left join kolejadmin.dbo.staff_Info on koko_instruktor.MYKAD = kolejadmin.dbo.staff_Info.staff_Mykad
                        Where kolejadmin.dbo.staff_Info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and koko_kolejpermata.Tahun = '" & ddlKoko_Year.SelectedValue & "' and koko_instruktor.Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        ElseIf ddlKoko_Type.SelectedValue = "RUMAHSUKAN" Then
            strSQL = "  Select Distinct NamaBI, KokoID from koko_kolejpermata 
                        left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.RumahsukanID
                        left join kolejadmin.dbo.staff_Info on koko_instruktor.MYKAD = kolejadmin.dbo.staff_Info.staff_Mykad
                        Where kolejadmin.dbo.staff_Info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and koko_kolejpermata.Tahun = '" & ddlKoko_Year.SelectedValue & "' and koko_instruktor.Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        End If

        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Name.DataSource = ds
            ddlKoko_Name.DataTextField = "NamaBI"
            ddlKoko_Name.DataValueField = "KokoID"
            ddlKoko_Name.DataBind()
            ddlKoko_Name.Items.Insert(0, New ListItem("Select Name", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlKoko_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Program.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKoko_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Year.SelectedIndexChanged
        Try
            ddl_kokoType()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKoko_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Type.SelectedIndexChanged
        Try
            ddl_KokoName()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKoko_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Name.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKoko_Semester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Semester.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, permataConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            PermataObjCon.Close()

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function getSQL() As String

        strSQL = "Select staff_Campus from staff_Info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim get_Semster As String = ""
        Dim get_kokoType As String = ""
        Dim strOrder As String = " order by student_Name ASC"

        If ddlKoko_Semester.SelectedValue = "SEMESTER 1" Then
            get_Semster = "P1"
        ElseIf ddlKoko_Semester.SelectedValue = "SEMESTER 2" Then
            get_Semster = "P2"
        End If

        If ddlKoko_Type.SelectedValue = "SUKAN" Then
            get_kokoType = "Sukan"
        ElseIf ddlKoko_Type.SelectedValue = "PERSATUAN" Then
            get_kokoType = "Persatuan"
        ElseIf ddlKoko_Type.SelectedValue = "UNIFORM" Then
            get_kokoType = "Uniform"
        End If

        tmpSQL = "  SELECT distinct A.std_ID , UPPER(A.student_Name) student_Name, A.student_Mykad, A.student_ID, koko_kelas.Kelas, " & get_kokoType & "_Pencapaian" & get_Semster & " as Pencapaian, " & get_kokoType & "_Penglibatan" & get_Semster & " as Penglibatan, " & get_kokoType & "_Jawatan" & get_Semster & " as Jawatan, " & get_kokoType & "_Kehadiran" & get_Semster & " as Kehadiran
                    FROM koko_pelajar
                    LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                    LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                    LEFT OUTER JOIN kolejadmin.dbo.student_info A ON StudentProfile.MYKAD = A.student_Mykad
                    LEFT OUTER JOIN koko_kolejpermata ON koko_pelajar.UniformID=koko_kolejpermata.KokoID OR koko_pelajar.PersatuanID=koko_kolejpermata.KokoID OR koko_pelajar.SukanID=koko_kolejpermata.KokoID OR koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID"

        strWhere = " WHERE koko_pelajar.Tahun ='" & ddlKoko_Year.SelectedValue & "' AND A.student_Status = 'Access' and A.student_ID is not null and A.student_ID <> '' and (A.student_ID like '%M%' or A.student_ID like '%P%')"

        strWhere += " AND A.student_Stream = '" & ddlKoko_Program.SelectedValue & "' and koko_kelas.Tahun = '" & ddlKoko_Year.SelectedValue & "' and A.student_Campus = '" & strRet & "'"

        strWhere += " AND koko_kolejpermata.KokoID = '" & ddlKoko_Name.SelectedValue & "' AND koko_kolejpermata.Tahun = '" & ddlKoko_Year.SelectedValue & "' "

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL
    End Function

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Try

            If ValidateForm() = True Then

                Dim get_Semster As String = ""
                Dim get_kokoType As String = ""

                If ddlKoko_Semester.SelectedValue = "SEMESTER 1" Then
                    get_Semster = "P1"
                ElseIf ddlKoko_Semester.SelectedValue = "SEMESTER 2" Then
                    get_Semster = "P2"
                End If

                If ddlKoko_Type.SelectedValue = "SUKAN" Then
                    get_kokoType = "Sukan"
                ElseIf ddlKoko_Type.SelectedValue = "PERSATUAN" Then
                    get_kokoType = "Persatuan"
                ElseIf ddlKoko_Type.SelectedValue = "UNIFORM" Then
                    get_kokoType = "Uniform"
                End If

                For i As Integer = 0 To datRespondent.Rows.Count - 1

                    Dim strKeyID As String = datRespondent.DataKeys(i).Value.ToString

                    Dim find_StdID As String = "select student_mykad from student_info where std_ID = '" & strKeyID & "'"
                    Dim get_StdID As String = oCommon.getFieldValue(find_StdID)

                    Dim find_StdMykad As String = "select StudentID from StudentProfile where MYKAD = '" & get_StdID & "'"
                    Dim get_StdMykad As String = oCommon.getFieldValue_Permata(find_StdMykad)

                    Dim txtPencapaian As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txt_Pencapaian"), TextBox)
                    Dim txtPenglibatan As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txt_Penglibatan"), TextBox)
                    Dim txtJawatan As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txt_Jawatan"), TextBox)
                    Dim txtKehadiran As TextBox = DirectCast(datRespondent.Rows(i).FindControl("txt_Kehadiran"), TextBox)

                    strSQL = "  UPDATE koko_pelajar SET " & get_kokoType & "_Pencapaian" & get_Semster & "=" & txtPencapaian.Text & "," & get_kokoType & "_Penglibatan" & get_Semster & "=" & txtPenglibatan.Text & "," & get_kokoType & "_Jawatan" & get_Semster & "=" & txtJawatan.Text & "," & get_kokoType & "_Kehadiran" & get_Semster & "=" & txtKehadiran.Text & " 
                                WHERE StudentID='" & get_StdMykad & "' AND Tahun='" & ddlKoko_Year.SelectedValue & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                Next

                ''CALCULATE SUKAN & UNIFORM & PERSATUAN JUMLAH
                strRet = oCommon.set_pelajar_Jumlah(ddlKoko_Year.SelectedValue, get_Semster)

                ''CALCULATE SUKAN & UNIFORM & PERSATUAN GRED 
                strRet = oCommon.set_pelajar_Gred(ddlKoko_Year.SelectedValue, get_Semster)

                If strRet = 0 Then
                    ShowMessage(" Update Student Result", MessageType.Success)
                Else
                    ShowMessage(" Unsuccessful Update Student Result", MessageType.Error)
                End If

            Else
                ShowMessage(" Please Enter Reult From 0-100", MessageType.Error)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim txtPencapaian As TextBox = DirectCast(row.FindControl("txtPencapaian"), TextBox)
            Dim txtPenglibatan As TextBox = DirectCast(row.FindControl("txtPenglibatan"), TextBox)
            Dim txtJawatan As TextBox = DirectCast(row.FindControl("txtJawatan"), TextBox)
            Dim txtKehadiran As TextBox = DirectCast(row.FindControl("txtKehadiran"), TextBox)

            '--validate NUMBER and less than 100
            '--txtKehadiran
            If Not txtKehadiran.Text.Length = 0 Then
                If oCommon.IsCurrency(txtKehadiran.Text) = False Then
                    Return False
                End If
                If CInt(txtKehadiran.Text) > 100 And CInt(txtJawatan.Text) < 0 Then
                    Return False
                End If
            Else
                txtKehadiran.Text = "0"
            End If

            '--txtJawatan
            If Not txtJawatan.Text.Length = 0 Then
                If oCommon.IsCurrency(txtJawatan.Text) = False Then
                    Return False
                End If
                If CInt(txtJawatan.Text) > 100 And CInt(txtJawatan.Text) < 0 Then
                    Return False
                End If
            Else
                txtJawatan.Text = "0"
            End If

            '--txtPenglibatan
            If Not txtPenglibatan.Text.Length = 0 Then
                If oCommon.IsCurrency(txtPenglibatan.Text) = False Then
                    Return False
                End If
                If CInt(txtPenglibatan.Text) > 100 And CInt(txtJawatan.Text) < 0 Then
                    Return False
                End If
            Else
                txtPenglibatan.Text = "0"
            End If

            '--txtPencapaian
            If Not txtPencapaian.Text.Length = 0 Then
                If oCommon.IsCurrency(txtPencapaian.Text) = False Then
                    Return False
                End If
                If CInt(txtPencapaian.Text) > 100 And CInt(txtJawatan.Text) < 0 Then
                    Return False
                End If
            Else
                txtPencapaian.Text = "0"
            End If
        Next

        Return True
    End Function

    Private Sub BtnExport_ServerClick(sender As Object, e As EventArgs) Handles BtnExport.ServerClick
        Try
            ExportToCSV(getSQL)
        Catch ex As Exception
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

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class