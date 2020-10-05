Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class ukm1_schoolprofile_student_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            lblExamYear.Text = Request.QueryString("examyear")
            strRet = BindData(datRespondent)
        End If

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada rekod ditemui!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        'tmpSQL = "SELECT UKM1.StudentID,StudentProfile.MYKAD,StudentProfile.StudentFullname,StudentProfile.DOB_Day,StudentProfile.DOB_Month,StudentProfile.DOB_Year,StudentProfile.StudentGender,StudentProfile.StudentRace,StudentProfile.StudentReligion,StudentProfile.StudentEmail,StudentProfile.StudentForm,StudentProfile.StudentAddress1,StudentProfile.StudentAddress2,StudentProfile.StudentPostcode,StudentProfile.StudentCity,StudentProfile.StudentState,StudentProfile.StudentContactNo,ParentProfile.FamilyContactNo,ParentProfile.FatherMYKADNo,ParentProfile.FatherFullname,ParentProfile.FatherJob,ParentProfile.MotherMYKADNo,ParentProfile.MotherFullname,ParentProfile.MotherJob,SchoolProfile.SchoolCode,SchoolProfile.SchoolName FROM UKM1"
        '--export ALL
        tmpSQL = "SELECT UKM1.StudentID,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.StudentFullname,StudentProfile.DOB_Day,StudentProfile.DOB_Month,StudentProfile.DOB_Year,StudentProfile.StudentGender,StudentProfile.StudentRace,StudentProfile.StudentReligion,StudentProfile.StudentEmail,StudentProfile.StudentForm,StudentProfile.StudentAddress1,StudentProfile.StudentAddress2,StudentProfile.StudentPostcode,StudentProfile.StudentCity,StudentProfile.StudentState,StudentProfile.StudentContactNo,ParentProfile.FamilyContactNo,ParentProfile.FatherMYKADNo,ParentProfile.FatherFullname,ParentProfile.FatherJob,ParentProfile.MotherMYKADNo,ParentProfile.MotherFullname,ParentProfile.MotherJob,SchoolProfile.SchoolCode,SchoolProfile.SchoolName,"
        tmpSQL += " HostAddress, HostName, Browser, ExamStart, ExamEnd, Duration, SelectedLang, Status, LastPage, Q001, Q002, Q003, Q004, Q005, Q006,Q007, Q008, Q009, Q010, Q011, Q012, Q013, Q014, Q015, Q016, Q017, Q018, Q019, Q020, Q021, Q022, Q023, Q024, Q025, Q026, Q027, Q028, Q029, Q030, Q031, Q032, Q033, Q034, Q035, Q036,"
        tmpSQL += " Q037, Q038, Q039, Q040, Q041, Q042, Q043, Q044, Q045, Q046, Q047, Q048, Q049, Q050, Q051, Q052, Q053, Q054, Q055, Q056, Q057, Q058, Q059, Q060, Q061, Q062, Q063, Q064, Q065, Q066,"
        tmpSQL += " Q067, Q068, Q069, Q070, Q071, Q072, Q073, Q074, Q075, Q076, Q077, Q078, Q079, Q080, Q081, Q082, Q083, Q084, Q085, Q086, Q087, Q088, Q089, Q090, Q091, Q092, Q093, Q094, Q095, Q096,"
        tmpSQL += " Q097, Q098, Q099, Q100, Q101, Q102, Q103, Q104, Q105, Q106, Q107, Q108, Q109, Q110, Q111, Q112, Q113, Q114, Q115, Q116, Q117, Q118, Q119, Q120, Q121, Q122, Q123, Q124, Q125, Q126,"
        tmpSQL += " Q127, Q128, Q129, Q130, Q131, Q132, Q133, Q134, Q135, Q136, Q137, Q138, Q139, Q140, Q141, Q142, Q143, Q144, Q145, Q146, Q147, Q148, Q149, Q150, ModA, ModB, ModC, TotalScore, Fullmark,"
        tmpSQL += " TotalPercentage, WrongCounter, UKM1.SchoolID, UKM1.SchoolState, UKM1.SchoolCity, UKM1.SchoolType, UKM1.SchoolPPD, UKM1.SchoolLokasi"
        tmpSQL += " FROM UKM1"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM1.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON UKM1.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON UKM1.SchoolID=SchoolProfile.SchoolID"
        strWhere += " WHERE UKM1.SchoolID='" & Request.QueryString("schoolid") & "'"
        strWhere += " AND UKM1.ExamYear='" & Request.QueryString("examyear") & "'"
        strOrder = " ORDER BY StudentProfile.StudentFullname"

        '--IsCount
        If Request.QueryString("iscount") = "True" Then
            strWhere += " AND UKM1.IsCount=1"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Try
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)
                Case "SUBADMIN"
                    Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & strKeyID)
                Case Else
                    lblMsg.Text = "Invalid User Type: " & getUserProfile_UserType()
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        Dim tmpSQL As String = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(tmpSQL)

        Return strRet
    End Function


    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Try
            ExportToCSV()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    Private Sub ExportToCSV()
        'Get the data from database into datatable 
        Dim strQuery As String = getSQL()
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=FileExport.csv")
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

End Class