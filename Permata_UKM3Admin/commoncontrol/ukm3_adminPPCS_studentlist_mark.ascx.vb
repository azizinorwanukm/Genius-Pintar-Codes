Imports System.Data.SqlClient

Public Class ukm3_adminPPCS_studentlist_mark
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Try
            If Not IsPostBack Then
                setDdlSession()
                setDdlClass()

                lblMsg.Text = ""
                lblMsgTop.Text = ""
                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 1200

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY A.student_name"

        tmpSQL = "SELECT D.id, A.student_Name, A.student_Mykad, B.AlumniID, E.ClassCode,C.ClassID,C.CourseID,H.SchoolID,E.NamaPengajar as staff_name
                ,CASE WHEN D.Ppcs_update  = 'Y' THEN 'Lengkap' ELSE 'Tidak lengkap' END Ppcs_update,
                CASE WHEN F.isSokong = 'Y' THEN 'Sokong' WHEN F.isSokong = 'N' THEN 'Tidak sokong' ELSE 'Belum disemak' END isSokong,
                convert(decimal(5,2),round(M.instruktorExam_marks*100/175,2)) instruktorExam_marks , convert(varchar, M.dateValid, 0) dateValid
                from ukm3.dbo.UKM3 D
			    LEFT JOIN ukm3.dbo.student_info A ON A.std_ID = D.student_id  
                LEFT JOIN permatapintar.dbo.StudentProfile B on A.guid = B.StudentID
                LEFT JOIN permatapintar.dbo.PPCS C ON C.StudentID = A.guid AND C.PPCSDate =  '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'
                LEFT JOIN permatapintar.dbo.PPCS_Class E ON E.ClassID = C.ClassID
				LEFT JOIN instruktorExam_result F ON F.ukm3id = D.id
                LEFT JOIN staff_info G ON G.stf_id = F.stf_id
                LEFT JOIN (SELECT ukm3id,instruktorExam_marks, dateValid FROM instruktorExam_result ) M ON M.ukm3id = D.id
                LEFT JOIN permatapintar.dbo.StudentSchool H on H.StudentID = D.std_guid and H.IsLatest='Y'"


        strWhere += " WHERE D.active = 1 AND D.session_id = " & ddlSession.SelectedValue


        ''search
        If Not txtsearch.Text.Length = 0 Then
            strWhere += " AND (A.student_name LIKE '%" & txtsearch.Text & "%'"
            strWhere += " OR A.student_Mykad LIKE '%" & txtsearch.Text & "%')"
        End If

        If Not ddlClass.SelectedValue = "0" Then
            strWhere += " AND C.ClassID = '" & ddlClass.SelectedValue & "' "
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Values("id").ToString
        Dim encryptid As String = strKeyID
        Dim strKeyClass As String = datRespondent.DataKeys(e.NewSelectedIndex).Values("ClassID").ToString
        Dim encryptClass As String = strKeyClass
        Dim strKeyCourse As String = datRespondent.DataKeys(e.NewSelectedIndex).Values("CourseID").ToString
        Dim encryptCourse As String = strKeyCourse
        Dim strKeySchool As String = datRespondent.DataKeys(e.NewSelectedIndex).Values("SchoolID").ToString
        Dim encryptSchool As String = strKeySchool
        Try
            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("ukm3_ppcs.updatemarkah.aspx?studentid=" & encryptid & "&classid=" & encryptClass & "&courseid=" & encryptCourse & "&schoolid=" & encryptSchool)
                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

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

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)
    End Sub

    Private Sub setDdlSession()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT id, sessionName FROM UKM3Session ORDER BY isCurrent DESC, id ASC"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        For k = 0 To quantity - 1
            ddlSession.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next


    End Sub

    Private Sub setDdlClass()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT M.ClassID, G.ClassCode FROM( "
        query += " SELECT A.ClassID FROM permatapintar.dbo.PPCS_Class A "
        query += " JOIN permatapintar.dbo.PPCS B ON B.ClassID = A.ClassID AND B.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "' "
        query += " JOIN student_info C ON C.guid = B.StudentID "
        query += " JOIN UKM3 D ON D.student_id = C.std_ID "
        query += " WHERE D.active = 1 AND D.session_id = " & ddlSession.SelectedValue & " GROUP BY A.ClassID) M  "
        query += " LEFT JOIN permatapintar.dbo.PPCS_Class G ON G.ClassID = M.ClassID "

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        ddlClass.Items.Clear()

        ddlClass.Items.Add(New ListItem("-- Pilih Kelas --", 0))

        For k = 0 To quantity - 1
            ddlClass.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next
    End Sub


    Private Sub btnResetDir_Click(sender As Object, e As EventArgs) Handles btnResetDir.Click
        Response.Redirect("ukm3_adminPPCS_resetPenilaian.aspx")
    End Sub
End Class
