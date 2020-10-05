Imports System.Data.SqlClient

Public Class ukm3_studentLayak1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                setDdlUkm3Session()
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
        Dim strOrder As String = " ORDER BY E.StudentFullname"

        tmpSQL = "SELECT D.id,D.student_id,E.alumniID, E.StudentFullname, E.MYKAD,C.sessionName,E.StudentGender,G.ClassCode,H.CourseCode,I.PPMT,I.Program,I.StatusTawaran, J.FatherJob, J.MotherJob "
        tmpSQL += " FROM ukm3.dbo.UKM3 D "
        ''tmpSQL += "LEFT JOIN UKM3 A ON A.student_id = B.std_id "
        tmpSQL += "LEFT JOIN ukm3.dbo.UKM3Session C ON D.session_id = C.id "
        tmpSQL += " LEFT JOIN permatapintar.dbo.StudentProfile E ON D.std_guid = E.StudentID "
        tmpSQL += " LEFT JOIN permatapintar.dbo.PPCS F ON F.StudentID = D.std_guid AND F.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "' "
        tmpSQL += " LEFT JOIN permatapintar.dbo.PPCS_Class G ON G.ClassID = F.ClassID "
        tmpSQL += " LEFT JOIN permatapintar.dbo.PPCS_Course H on H.CourseID = F.CourseID "
        tmpSQL += " LEFT JOIN permatapintar.dbo.ukm3 I on I.StudentID = F.StudentID  and I.PPCSDate ='" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "' "
        tmpSQL += " LEFT JOIN permatapintar.dbo.ParentProfile J ON J.StudentID = I.StudentID "
        tmpSQL += " LEFT JOIN permatapintar.dbo.StudentSchool K ON K.StudentID = I.StudentID AND K.IsLatest = 'Y' "
        tmpSQL += " LEFT JOIN permatapintar.dbo.SchoolProfile L ON L.SchoolID = K.SchoolID "

        strWhere += "WHERE D.active = 1 AND D.session_id = " & ddlSession.SelectedValue

        '''search
        If Not txt_nama.Text.Length = 0 Then
            strWhere += " AND (E.StudentFullname LIKE '%" & txt_nama.Text & "%' "
            strWhere += "  OR E.alumniID LIKE '%" & txt_nama.Text & "%' "
            strWhere += " OR E.MYKAD LIKE '%" & txt_nama.Text & "%')"
        End If

        If Not ddlKelas.SelectedValue = 0 Then
            strWhere += " AND F.ClassID = '" & ddlKelas.SelectedValue & "' "
        End If

        If ddlProgram.SelectedValue = "1" Then
            strWhere += " AND F.ClassID = 'ASAS 1' "
        ElseIf ddlProgram.SelectedValue = "2" Then
            strWhere += " AND F.ClassID = 'TAHAP 1' "
        End If

        If ddlJantina.SelectedValue = "1" Then
            strWhere += " AND E.StudentGender = 'LELAKI' "
        ElseIf ddlJantina.SelectedValue = "2" Then
            strWhere += " AND E.StudentGender = 'PEREMPUAN'"
        End If

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL
    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim encyptid As String = datRespondent.DataKeys(e.NewSelectedIndex).Value(1).ToString
        Dim strKeyID As String = oDes.EncryptData(encyptid)
        Try
            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)

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


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        strRet = BindData(datRespondent)

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

    Private Sub setDdlUkm3Session()
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

    Private Sub ddlSession_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSession.TextChanged
        setDdlClass()
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

        ddlKelas.Items.Clear()

        ddlKelas.Items.Add(New ListItem("-- Semua Kelas --", 0))

        For k = 0 To quantity - 1
            ddlKelas.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        Dim message As String = ""

        For i = 0 To datRespondent.Rows.Count - 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).FindControl("chkSelect"), CheckBox)
            Dim std_name As Label = CType(datRespondent.Rows(i).FindControl("StudentFullname"), Label)
            If Not chkUpdate Is Nothing Then
                Try
                    If chkUpdate.Checked = True Then
                        Dim ppcsid As String = datRespondent.DataKeys(i).Value.ToString
                        strSQL = "UPDATE ukm3.dbo.UKM3 SET ukm3.dbo.UKM3.active = '0' WHERE ukm3.dbo.UKM3.id = '" & ppcsid & "'"
                        oCommon.ExecuteSQL(strSQL)
                        BindData(datRespondent)
                        lblMsgTop.Text = "Pelajar " & std_name.Text & " dibuang dari program UKM3"
                    End If
                Catch ex As Exception

                End Try
            End If
        Next

    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ExportToCSV()
    End Sub
End Class