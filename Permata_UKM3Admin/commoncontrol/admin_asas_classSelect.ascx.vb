Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class admin_asas_classSelect
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim strConn2 As String = ConfigurationManager.AppSettings("ConnectionMaster")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim objConn2 As SqlConnection = New SqlConnection(strConn2)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim sqlCommd As SqlCommand
    Private this As Object
    Private i As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                setDdlExams()
                'setddlYear()
                setDdlClass()
                setJantina()
                setPilihKelayakan()
                calMean()
                'If ddlYear.SelectedValue = "" Then
                '    lblMsgTop.Text = "Sila Pilih Tahun Untuk Mengeluarkan Paparan"
                'Else
                strRet = BindData(datRespondent)
                'End If

                checkKolejDate()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub calMean()
        strSQL = "SELECT sum(A.marks_100) from ukm3.dbo.ukm3 A
                  LEFT JOIN permatapintar.dbo.StudentProfile C on C.StudentID = A.std_guid
                  LEFT JOIN permatapintar.dbo.PPCS J on A.std_guid = J.StudentID AND J.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'
                  LEFT JOIN permatapintar.dbo.PPCS_Class z on z.ClassCode = J.PPCSClass 
				  LEFT JOIN permatapintar.dbo.PPCS_Course x ON J.CourseID=x.CourseID
				  LEFT JOIN permatapintar.dbo.PPCS_Class y on J.ClassID = y.ClassID 
                  WHERE A.active = 1 AND A.session_id = " & ddlSession.SelectedValue & "
                  AND x.CourseType = 'PRIMARY'"
        Dim total_Mean As Decimal = Decimal.Parse(oCommon.getFieldValueMaster(strSQL))

        strSQL = "SELECT count(A.student_id) from ukm3.dbo.ukm3 A
                  LEFT JOIN permatapintar.dbo.StudentProfile C on C.StudentID = A.std_guid
                  LEFT JOIN permatapintar.dbo.PPCS J on A.std_guid = J.StudentID AND J.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'
                  LEFT JOIN permatapintar.dbo.PPCS_Class z on z.ClassCode = J.PPCSClass 
				  LEFT JOIN permatapintar.dbo.PPCS_Course x ON J.CourseID=x.CourseID
				  LEFT JOIN permatapintar.dbo.PPCS_Class y on J.ClassID = y.ClassID 
                  WHERE A.active = 1 AND A.session_id = " & ddlSession.SelectedValue & "
                  AND x.CourseType = 'PRIMARY'"
        Dim total_Count As Integer = Integer.Parse(oCommon.getFieldValueMaster(strSQL))

        txtMean.Text = Math.Round(total_Mean / total_Count, 2)
    End Sub

    Private Function getSQL() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        strSQL = "Select ukm3Year from UKM3Session where ppcsdate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        tmpSQL = "SELECT B.id,C.StudentFullname student_name,C.MYKAD student_Mykad , C.StudentGender as Jantina ,C.AlumniID,Y.ClassCode as classcode,
                    B.marks_100,K.ScorePercentage as meqi ,E.TotalMark as ukm2Mark,CAST(I.Mental_Age_Year As decimal(6,2)) as mentalAge,I.TotalPercentage as ukm2TotalPercentage,
                    B.markPretest as pretest,B.markPosttest as posttest,(B.markPosttest-B.markPretest) as difference,
                    convert(decimal(5,2),round(H.instruktorExam_marks*100/175,2)) marks_ppcs,CONCAT (H.isSokong,space(3),'|',space(3),H.instruktorExam_Komen) as komen_ppcs,
                    convert(decimal(5,2),round(F.instruktorExam_marks*100/150,2)) marks_raPcs, CONCAT (F.isSokong   ,space(3),'|',space(3),F.instruktorExam_Komen) as raPcs_komen,
                    convert(decimal(5,2),round(G.instruktorExam_marks*100/140,2)) marks_kpp,CONCAT (G.isSokong_kpp ,space(3),'|',space(3),G.instruktorExam_Komen_kpp) as kpp_komen,
                    convert(Decimal (5,3),((B.marks_100*MM.stem)+(B.markPosttest*MM.postTest)+(round(H.instruktorExam_marks*100/175,2)*mm.insPPCS)+(round(F.instruktorExam_marks*100/150,2)*MM.raPPCS)
                    +(round(G.instruktorExam_marks*100/140,2)*MM.insKPP)+(I.TotalPercentage*MM.ukm2)+(K.ScorePercentage *MM.eq))) as compo ,
                    B.isLayak as Layak, ZZ.TotalPercentage as UKM2
                    from ukm3.dbo.ukm3_compoMark MM,ukm3.dbo.ukm3 B
                    LEFT JOIN permatapintar.dbo.UKM2 ZZ on B.std_guid = ZZ.StudentID
                    LEFT JOIN permatapintar.dbo.StudentProfile C on C.StudentID=B.std_guid
                    LEFT JOIN permatapintar.dbo.EQTest D on D.StudentID=C.StudentID and D.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'
                    LEFT JOIN (select AA.StudentID, AA.TotalMark from permatapintar.dbo.PPCS_Eval_Daily AA join (select StudentID, max(LastUpdate) LastUpdate from permatapintar.dbo.PPCS_Eval_Daily WHERE PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "' GROUP BY StudentID) BB on AA.StudentID=BB.StudentID AND AA.LastUpdate = BB.LastUpdate) E on E.StudentID=C.StudentID
                    LEFT JOIN ukm3.dbo.instruktorExam_result H on B.id =H.ukm3id
                    LEFT JOIN ukm3.dbo.instruktorExam_result_raPcs F on B.id = F.ukm3id
                    LEFT JOIN ukm3.dbo.instruktorExam_result_kpp G on B.id=G.ukm3id
                    LEFT JOIN (select  AA.StudentID,AA.Mental_Age_Year,AA.TotalPercentage from permatapintar.dbo.ukm2 AA join(select  CC.StudentID, CC.ExamYear from permatapintar.dbo.ukm2 CC WHERE ExamYear ='" & strRet & "')BB on AA.StudentID=BB.StudentID and AA.ExamYear =BB.ExamYear) I on I.StudentID = C.StudentID
                    LEFT JOIN permatapintar.dbo.PPCS J on B.std_guid = J.StudentID AND J.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'                    
                                        
					LEFT JOIN permatapintar.dbo.EQTest K on B.std_guid  = K.StudentID and k.PPCSDate ='" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'
					
					LEFT JOIN permatapintar.dbo.PPCS_Class z on z.ClassCode = J.PPCSClass 
					LEFT JOIN permatapintar.dbo.PPCS_Course x ON J.CourseID=x.CourseID
					LEFT JOIN permatapintar.dbo.PPCS_Class y on J.ClassID = y.ClassID 

                    WHERE MM.isActive =1 AND B.active = 1 AND B.session_id = " & ddlSession.SelectedValue & "
                    AND x.CourseType = 'PRIMARY' AND ZZ.ExamYear = '" & strRet & "'"

        ''search
        If Not txtsearch.Text.Length = 0 Then
            strWhere += " And (C.StudentFullname Like '%" & txtsearch.Text & "%'"
            strWhere += " OR C.MYKAD LIKE '%" & txtsearch.Text & "%')"
        End If

        ''search gender
        If ddlJantina.SelectedValue = "1" Then
            strWhere += " AND C.StudentGender = 'LELAKI' "
        ElseIf ddlJantina.SelectedValue = "2" Then
            strWhere += " AND C.StudentGender = 'PEREMPUAN'"
        End If

        If Not ddlClass.SelectedValue = "0" Then
            strWhere += " AND J.ClassID = '" & ddlClass.SelectedValue & "' "
        End If

        ''search kelayakan
        If ddlPilihKelayakan.SelectedValue <> "" Then
            strWhere += " AND B.isLayak = '" & ddlPilihKelayakan.SelectedValue & "'"
        End If

        If ddlSortbys.SelectedValue = 0 Then
            strOrder = " ORDER BY A.student_Name ASC"
        ElseIf ddlSortbys.SelectedValue = 1 Then
            strOrder = " ORDER BY B.marks_100 ASC"
        ElseIf ddlSortbys.SelectedValue = 2 Then
            strOrder = " ORDER BY B.marks_100 DESC"
        ElseIf ddlSortbys.SelectedValue = 3 Then
            strOrder = " ORDER BY compo ASC"
        ElseIf ddlSortbys.SelectedValue = 4 Then
            strOrder = " ORDER BY compo DESC"
        End If

        'Debug.WriteLine(tmpSQL)
        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL
    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet

        Dim myDataAdapter As New SqlDataAdapter(getSQL(), strConn2)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Dim descrip As String = "select description from master where type = 'MEQI'"
        Dim meqi_ID As String = oCommon.getFieldValue(descrip)

        If meqi_ID = "on" Or meqi_ID = "On" Or meqi_ID = "ON" Or meqi_ID = "show" Then
            gvTable.Columns(7).Visible = True
        ElseIf meqi_ID = "off" Or meqi_ID = "hide" Or meqi_ID = "tutup" Then
            gvTable.Columns(7).Visible = False
        End If

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

    Private Function checkKolejDate() As String

        strSQL = "Select parameter from general_config where config = 'kolejStartDate'"
        Dim strSQL1 As String = "Select parameter from general_config = 'kolejEndDate'"
        Dim getdate As Date = DateTime.Now.ToString("YYYYMMDD")
        Dim date1 As Date = oCommon.getFieldValue(strSQL)
        Dim date2 As Date = oCommon.getFieldValue(strSQL1)


        If getdate < date2 And getdate > date1 Then
            btnKemaskini.Enabled = True
        Else
            btnKemaskini.Enabled = False
        End If
    End Function

    Protected Sub datRespondent_OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles datRespondent.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlKelayakan As DropDownList = TryCast(e.Row.FindControl("ddl_kelayakan"), DropDownList)
            Dim lblLayak As Label = TryCast(e.Row.FindControl("lbllayak"), Label)
            'Dim status As String = Convert.ToString(load_ddl)
            strSQL = "Select ukm3.dbo.master.description From ukm3.dbo.master where type = 'Kelayakan'"

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            If sqlDA IsNot Nothing Then
                Dim ds As DataSet = New DataSet
                sqlDA.Fill(ds, "Anytable")
                ddlKelayakan.DataSource = ds
                ddlKelayakan.DataTextField = "description"
                ddlKelayakan.DataValueField = "description"

                ddlKelayakan.DataBind()
                ddlKelayakan.Items.Insert(0, New ListItem("Sila Pilih", ""))

                ddlKelayakan.SelectedValue = lblLayak.Text

            End If
        End If

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Dim encyptid As String = oDes.EncryptData(strKeyID)
        Try
            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("ukm3_admin.studentprofileview.aspx?studentid=" & encyptid)
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

    Private Function GetData(query As String) As DataTable
        Dim ds As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionMaster")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        Dim cmd As New SqlCommand(query)
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(ds)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            Dim ddl_kelayakan As DropDownList = CType(datRespondent.Rows(i).FindControl("ddl_kelayakan"), DropDownList)
            If chkUpdate IsNot Nothing Then

                If chkUpdate.Checked = True Then

                    Dim ukm3id As String = datRespondent.DataKeys(i).Value.ToString

                    strSQL = " UPDATE UKM3 SET isLayak = '" & ddl_kelayakan.SelectedValue & "' WHERE id ='" & ukm3id & "' "

                    Dim oldUkm3id As String = oCommon.getFieldValue("select UKM3ID from permatapintar.dbo.ukm3 A join ukm3 B on A.StudentID = B.std_guid join UKM3Session C on C.ppcsdate = A.PPCSDate and c.id = b.session_id where b.id = '" & ukm3id & "'")

                    If ddl_kelayakan.SelectedValue = "L1" Then
                        strSQL += " UPDATE permatapintar.dbo.ukm3 SET Program = 'ASAS 1', PPMT = 'Y' WHERE UKM3ID = '" & oldUkm3id & "'"
                    Else
                        strSQL += " UPDATE permatapintar.dbo.ukm3 SET Program = null, PPMT = 'N' WHERE UKM3ID = '" & oldUkm3id & "'"
                    End If

                    Try
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Catch ex As Exception

                    End Try

                    If strRet = 0 Then
                        lblMsg.Text = "Kemaskini Berjaya"
                    Else
                        lblMsg.Text = "Kemaskini tidak Berjaya"
                    End If

                End If
                End If
        Next

        BindData(datRespondent)

    End Sub

    Private Sub setDdlExams()
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

    'Private Sub setddlYear()
    '    strSQL = "select a.description from ukm3.dbo.master a where type= 'year' order by description DESC"

    '    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    '    Dim objConn As SqlConnection = New SqlConnection(strConn)
    '    Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

    '    Try
    '        Dim ds As DataSet = New DataSet
    '        sqlDA.Fill(ds, "AnyTable")

    '        ddlYear.DataSource = ds
    '        ddlYear.DataTextField = "description"
    '        ddlYear.DataValueField = "description"
    '        ddlYear.DataBind()
    '        ddlYear.Items.Insert(0, New ListItem("Sila Pilih Tahun", String.Empty))

    '    Catch ex As Exception

    '    Finally
    '        objConn.Dispose()
    '    End Try
    'End Sub

    Private Sub setJantina()
        'strSQL = "select distinct student_sex from student_info where std_ID is not null"

        'Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
        'Dim objConn As SqlConnection = New SqlConnection(strConn)
        'Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        'Try
        '    Dim ds As DataSet = New DataSet
        '    sqlDA.Fill(ds, "AnyTable")

        '    ddlJantina.DataSource = ds
        '    ddlJantina.DataTextField = "student_sex"
        '    ddlJantina.DataValueField = "student_sex"
        '    ddlJantina.DataBind()
        '    ddlJantina.Items.Insert(0, New ListItem("Sila Pilih Jantina", String.Empty))

        'Catch ex As Exception

        'Finally
        '    objConn.Dispose()
        'End Try
        ddlJantina.Items.Add(New ListItem("--SILA PILIH--", 0))
        ddlJantina.Items.Add(New ListItem("LELAKI", 1))
        ddlJantina.Items.Add(New ListItem("PEREMPUAN", 2))


    End Sub

    Private Sub setPilihKelayakan()
        strSQL = "select description from master where type = 'Kelayakan'"

        Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPilihKelayakan.DataSource = ds
            ddlPilihKelayakan.DataTextField = "description"
            ddlPilihKelayakan.DataValueField = "description"
            ddlPilihKelayakan.DataBind()
            ddlPilihKelayakan.Items.Insert(0, New ListItem("Sila Pilih Kelayakan", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub setDdlClass()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT M.ClassID, G.ClassCode FROM( "
        query += " SELECT A.ClassID FROM permatapintar.dbo.PPCS_Class A "
        query += " JOIN permatapintar.dbo.PPCS B ON B.ClassID = A.ClassID AND B.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "' "
        query += " JOIN permatapintar.dbo.PPCS_Course N ON A.CourseID = N.CourseID"
        query += " JOIN UKM3 D ON D.std_guid = B.StudentID "
        query += " WHERE D.active = 1 AND D.session_id = " & ddlSession.SelectedValue & " AND N.CourseType = 'PRIMARY'  GROUP BY A.ClassID) M  "
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

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        strRet = BindData(datRespondent)
    End Sub

    'export 
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 

        Dim dt As DataTable = GetData(strQuery)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Kelayakan_Asas1.csv")
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

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        ExportToCSV(getSQL)
    End Sub

    Private Sub btnKemaskini_Click(sender As Object, e As EventArgs) Handles btnKemaskini.Click
        lblMsg.Text = ""
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            Dim lbl_markahKompo As Label = CType(datRespondent.Rows(i).FindControl("lbl_compoMark"), Label)
            If chkUpdate IsNot Nothing Then

                If chkUpdate.Checked = True Then

                    Dim ukm3id As String = datRespondent.DataKeys(i).Value.ToString

                    strSQL = " UPDATE UKM3 SET Compo_Mark = '" & lbl_markahKompo.Text & "' WHERE id ='" & ukm3id & "' "

                    Try
                        strRet = oCommon.ExecuteSQL(strSQL)
                        Debug.WriteLine(strRet)
                    Catch ex As Exception

                    End Try
                    If strRet = 0 Then
                        lblMsg.Text = "Kemaskini Markah Komposit Berjaya"
                    Else
                        lblMsg.Text = "Kemaskini Markah Komposit tidak Berjaya"
                    End If
                End If
            End If
        Next
        BindData(datRespondent)

    End Sub

    Private Sub ddlSession_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSession.SelectedIndexChanged
        setDdlClass()
    End Sub

    'Private Sub datRespondent_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datRespondent.RowDataBound
    '    Try
    '        Dim i As Integer
    '        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
    '            Dim stem As Label = CType(datRespondent.Rows(i).FindControl("lbl_stem"), Label)
    '            Dim eq As Label = CType(datRespondent.Rows(i).FindControl("lbl_meqi"), Label)
    '            Dim insKPP As Label = CType(datRespondent.Rows(i).FindControl("instrKPP"), Label)
    '            Dim insPPCS As Label = CType(datRespondent.Rows(i).FindControl("instrPPCS"), Label)
    '            Dim raPPCS As Label = CType(datRespondent.Rows(i).FindControl("instrRaPPCS"), Label)
    '            Dim ukm2 As Label = CType(datRespondent.Rows(i).FindControl("lbl_ukm2percentage"), Label)
    '            Dim postTest As Label = CType(datRespondent.Rows(i).FindControl("txt_postTest"), Label)
    '            Dim lbl_compoMark As Label = CType(datRespondent.Rows(i).FindControl("lbl_compoMark"), Label)

    '            Dim A = Decimal.Parse(stem.Text) * oCommon.getFieldValue("Select stem from ukm3_compoMark where isActive = '1' and session='" & Commonfunction.getPpcsDate.ToString & "'")

    '            Dim B = Decimal.Parse(eq.Text) * (oCommon.getFieldValue("Select eq from ukm3_compoMark where isActive = '1' and session='" & Commonfunction.getPpcsDate.ToString & "'"))

    '            Dim C = Decimal.Parse(insKPP.Text) * (oCommon.getFieldValue("Select insKPP from ukm3_compoMark where isActive = '1' and session='" & Commonfunction.getPpcsDate.ToString & "'"))

    '            Dim D = Decimal.Parse(insPPCS.Text) * (oCommon.getFieldValue("Select insPPCS from ukm3_compoMark where isActive = '1' and session='" & Commonfunction.getPpcsDate.ToString & "'"))
    '            Dim EE = Decimal.Parse(raPPCS.Text) * (oCommon.getFieldValue("Select raPPCS from ukm3_compoMark where isActive = '1' and session='" & Commonfunction.getPpcsDate.ToString & "'"))
    '            Dim F = Decimal.Parse(ukm2.Text) * (oCommon.getFieldValue("Select ukm2 from ukm3_compoMark where isActive = '1' and session='" & Commonfunction.getPpcsDate.ToString & "'"))
    '            Dim G = Decimal.Parse(postTest.Text) * (oCommon.getFieldValue("Select postTest from ukm3_compoMark where isActive = '1' and session='" & Commonfunction.getPpcsDate.ToString & "'"))

    '            Dim compoMark As String = A + B + C + D + EE + F + G

    '            lbl_compoMark.Text = compoMark
    '        Next

    '    Catch ex As Exception
    '        lblMsgTop.Text = ex.ToString
    '    End Try
    'End Sub
End Class