Imports System.Data.SqlClient
Imports RKLib.ExportData


Public Class onlinetest_kelayakan_select
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnLayak.Attributes.Add("onclick", "return confirm('Pilih Sessi PPCS. Pasti ingin MELAYAKKAN pelajar-pelajar tersebut?');")
        btnTidakLayak.Attributes.Add("onclick", "return confirm('Pilih Sessi PPCS. Pasti TIDAK MELAYAKKAN pelajar-pelajar tersebut? Maklumat PPCS jika ada akan terhapus.');")

        Try
            If Not IsPostBack Then
                lblMsg.Text = ""
                lblMsgTop.Text = ""

                '--ddlPPCSDate
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                master_surveyid_list()
                ddlSurveyID.Text = oCommon.getAppsettings("EQTest_SurveyID")
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

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

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub master_surveyid_list()
        strSQL = "SELECT SurveyID FROM master_surveyid ORDER BY master_surveyid ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSurveyID.DataSource = ds
            ddlSurveyID.DataTextField = "SurveyID"
            ddlSurveyID.DataValueField = "SurveyID"
            ddlSurveyID.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai"
            Else
                lblMsg.Text = "Jumlah pelajar#:" & myDataSet.Tables(0).Rows.Count
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
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentReligion,StudentProfile.StudentGender,StudentProfile.StudentAddress1,StudentProfile.StudentAddress2,StudentProfile.StudentPostcode,StudentProfile.StudentCity,StudentProfile.StudentState,StudentProfile.StudentCountry,StudentProfile.StudentContactNo,ParentProfile.FatherFullname,ParentProfile.FatherJob,ParentProfile.FamilyContactNo,ParentProfile.MotherFullname,ParentProfile.FamilyContactNoIbu,SchoolProfile.SchoolName,SchoolProfile.SchoolState,SchoolProfile.SchoolPPD,SchoolProfile.SchoolCity,SchoolProfile.SchoolType,SchoolProfile.SchoolLokasi,PPCS.PPCSDate,PPCS_Course.CourseCode,PPCS_Class.ClassCode FROM PPCS"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON PPCS.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON PPCS.StudentID=ParentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Course ON PPCS.CourseID=PPCS_Course.CourseID"
        tmpSQL += " LEFT OUTER JOIN PPCS_Class ON PPCS.ClassID=PPCS_Class.ClassID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool ON PPCS.StudentID=StudentSchool.StudentID AND StudentSchool.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"

        strWhere = " WHERE PPCS.PPCSDate='" & ddlPPCSDate.Text & "' AND PPCSStatus='LAYAK' AND StatusTawaran='TERIMA'"

        If selSort.Value = "0" Then
            strOrder = " ORDER BY StudentProfile.StudentFullname ASC"
        Else
            strOrder = " ORDER BY UKM2.TotalScore DESC"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        '--Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function isLayakUKM3(ByVal strStudentID As String) As String
        ''--get UKM3
        strSQL = "SELECT StudentID FROM UKM3 WHERE StudentID='" & strStudentID & "' AND PPCSDate='" & ddlPPCSDate.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            Return "Y"
        Else
            Return "N"
        End If

    End Function

    Private Function getPPCSDate(ByVal strStudentID As String) As String
        ''--get the date
        strSQL = "SELECT PPCSDate FROM PPCS WHERE StudentID='" & strStudentID & "'"
        strRet = oCommon.getRowValue(strSQL)
        Return strRet

    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("ppcs.alumni.studentprofile.aspx?studentid=" & strKeyID & "&ppcsdate=" & ddlPPCSDate.Text)
            Case "SUBADMIN"
            Case Else
        End Select

    End Sub

    Private Sub btnLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLayak.Click
        lblMsg.Text = ""

        If chkEQTest.Checked = True Then
            strSQL = "INSERT INTO EQTest (LoginID,StudentID,PPCSDate,Fullname,AlumniID,EmailAdd,SurveyID)"
            strSQL += "SELECT PPCS.StudentID,PPCS.StudentID,PPCS.PPCSDate,StudentProfile.StudentFullname,StudentProfile.AlumniID,StudentProfile.StudentEmail,'" & ddlSurveyID.Text & "'"
            strSQL += "FROM PPCS,StudentProfile WHERE PPCS.StudentID=StudentProfile.StudentID AND PPCS.PPCSDate='" & ddlPPCSDate.Text & "' AND PPCS.PPCSStatus='LAYAK';"

            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text += "Berjaya LAYAK ke EQTest" & "<br/>"
            Else
                lblMsg.Text += "GAGAL LAYAK ke EQTest." & strRet & "<br/>"
            End If
        End If

        If chkSainsTest.Checked = True Then
            strSQL = "INSERT INTO SainsTest (LoginID,StudentID,PPCSDate,Fullname,AlumniID,EmailAdd,SurveyID)"
            strSQL += "SELECT PPCS.StudentID,PPCS.StudentID,PPCS.PPCSDate,StudentProfile.StudentFullname,StudentProfile.AlumniID,StudentProfile.StudentEmail,'" & ddlSurveyID.Text & "'"
            strSQL += "FROM PPCS,StudentProfile WHERE PPCS.StudentID=StudentProfile.StudentID AND PPCS.PPCSDate='" & ddlPPCSDate.Text & "' AND PPCS.PPCSStatus='LAYAK';"

            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text += "Berjaya LAYAK ke SainsTest" & "<br/>"
            Else
                lblMsg.Text += "GAGAL LAYAK ke SainsTest." & strRet & "<br/>"
            End If
        End If

        If chkStressTest.Checked = True Then
            strSQL = "INSERT INTO StressTest (LoginID,StudentID,PPCSDate,Fullname,AlumniID,EmailAdd,SurveyID)"
            strSQL += "SELECT PPCS.StudentID,PPCS.StudentID,PPCS.PPCSDate,StudentProfile.StudentFullname,StudentProfile.AlumniID,StudentProfile.StudentEmail,'" & ddlSurveyID.Text & "'"
            strSQL += "FROM PPCS,StudentProfile WHERE PPCS.StudentID=StudentProfile.StudentID AND PPCS.PPCSDate='" & ddlPPCSDate.Text & "' AND PPCS.PPCSStatus='LAYAK';"

            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text += "Berjaya LAYAK ke Stress Test" & "<br/>"
            Else
                lblMsg.Text += "GAGAL LAYAK ke Stress Test." & strRet & "<br/>"
            End If
        End If


    End Sub

    Private Sub btnTidakLayak_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTidakLayak.Click
        lblMsg.Text = ""

        If chkEQTest.Checked = True Then
            strSQL += "DELETE EQTest WHERE PPCSDate='" & ddlPPCSDate.Text & "' AND SurveyID='" & ddlSurveyID.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text += "Berjaya HAPUSKAN EQTest" & "<br/>"
            Else
                lblMsg.Text += "GAGAL HAPUSKAN EQTest." & strRet & "<br/>"
            End If
        End If

        If chkSainsTest.Checked = True Then
            strSQL += "DELETE SainsTest WHERE PPCSDate='" & ddlPPCSDate.Text & "' AND SurveyID='" & ddlSurveyID.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text += "Berjaya HAPUSKAN SainsTest" & "<br/>"
            Else
                lblMsg.Text += "GAGAL HAPUSKAN SainsTest." & strRet & "<br/>"
            End If
        End If

        If chkStressTest.Checked = True Then
            strSQL += "DELETE StressTest WHERE PPCSDate='" & ddlPPCSDate.Text & "' AND SurveyID='" & ddlSurveyID.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text += "Berjaya HAPUSKAN StressTest" & "<br/>"
            Else
                lblMsg.Text += "GAGAL HAPUSKAN StressTest." & strRet & "<br/>"
            End If
        End If

    End Sub


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            ExportToCSV()
            lblMsgTop.Text = lblMsg.Text
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