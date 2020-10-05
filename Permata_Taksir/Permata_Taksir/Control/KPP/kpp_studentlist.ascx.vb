Imports System.Data.SqlClient

Public Class kpp_studentlist
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
                setDdlSession()
                setDdlKodKelas()
                lblMsg.Text = ""
                lblMsgTop.Text = ""

                strRet = BindData(datRespondent)
                getDateSah()

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
                lblMsg.Text = "Tiada Rekod Pelajar!"
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

    Private Function getDAtaStaff() As String
        strSQL = "SELECT top 1 stf_id from staff_info where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getSQL() As String

        Dim stf_ID As String = getDAtaStaff()

        ''get staff ID
        strSQL = " SELECT staff_id FROM staff_info WHERE stf_id = '" & stf_ID & "'"
        Dim staff_id As String = oCommon.getFieldValue(strSQL)

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY A.student_name"

        tmpSQL = "  SELECT B.id, A.std_ID, A.student_ID, A.student_Name, A.student_Mykad, B.kpp_simpan, C.AlumniID, Y.ClassCode, B.kpp_stf,CASE WHEN A.student_sex = 1 THEN 'Lelaki' ELSE 'Perempuan' END student_sex
                    from ukm3.dbo.ukm3 B
                    LEFT JOIN ukm3.dbo.student_info A on B.student_id=A.std_ID
                    LEFT JOIN permatapintar.dbo.StudentProfile C on C.MYKAD=A.student_mykad
                    LEFT JOIN permatapintar.dbo.PPCS J on A.guid = J.StudentID AND J.PPCSDate = '" & Commonfunction.getPpcsDate & "'
                    LEFT JOIN permatapintar.dbo.PPCS_Class z on z.ClassCode = J.PPCSClass 
                    LEFT JOIN permatapintar.dbo.PPCS_Course x ON J.CourseID=x.CourseID
                    LEFT JOIN permatapintar.dbo.PPCS_Class y on J.ClassID = y.ClassID"
        strWhere += " WHERE B.active = '1' AND B.kpp_stf = '" & staff_id & "'"

        If Not ddlSession.Text = "Pilih" Then

            strWhere += " AND B.session_id = '" & ddlSession.SelectedValue & "'"

        End If

        If Not ddlKodKelas.Text = "Pilih" Then

            strWhere += " AND Y.ClassID = '" & ddlKodKelas.SelectedValue & "'"

        End If

        If Not ddlJantina.SelectedValue = 2 Then

            strWhere += " AND A.student_sex = '" & ddlJantina.SelectedValue & "'"

        End If

        If Not txtsearch.Text.Length = 0 Then
            strWhere += " AND (A.student_name LIKE '%" & txtsearch.Text & "%'"
            strWhere += " OR A.student_Mykad LIKE '%" & txtsearch.Text & "%')"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Dim encyptid As String = strKeyID
        Dim kpp_status As Label = DirectCast(datRespondent.Rows(e.NewSelectedIndex).FindControl("record"), Label)

        strSQL = " SELECT kpp_pengesahan FROM UKM3 WHERE id = '" & encyptid & "'"
        Dim statusPengesahan As String = oCommon.getFieldValue(strSQL)

        If statusPengesahan = "" Then

            Try
                Select Case getUserProfile_UserType()
                    Case "Instruktor KPP"
                        Response.Redirect("kpp.masukmarkah.aspx?studentid=" & encyptid & "&kpp_update=" & kpp_status.Text)
                    Case Else
                        lblMsg.Text = "Invalid user type!"
                End Select
            Catch ex As Exception

            End Try

        Else

            lblSah.Text = "Pengesahan telah dibuat. Tidak boleh mengubah markah pelajar."

        End If




    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        strRet = BindData(datRespondent)
        getDateSah()

    End Sub

    Private Sub setDdlSession()

        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT id, sessionName FROM UKM3Session ORDER BY id DESC"

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

        ddlSession.Items.Insert(0, "Pilih")

        Dim currentSession As String = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'currentSession'")

        ddlSession.SelectedValue = currentSession

    End Sub

    Private Sub setDdlKodKelas()

        ''get stf_id
        Dim stf_ID As String = getDAtaStaff()

        ''get staff ID
        strSQL = " SELECT staff_id FROM staff_info WHERE stf_id = '" & stf_ID & "'"
        Dim staff_id As String = oCommon.getFieldValue(strSQL)

        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = " SELECT DISTINCT A.ClassID, A.ClassCode FROM ukm3.dbo.UKM3 D 
                                LEFT JOIN ukm3.dbo.student_info C ON C.std_ID = D.student_id
								LEFT JOIN permatapintar.dbo.PPCS B ON B.StudentID = C.guid
								LEFT JOIN  permatapintar.dbo.PPCS_Class A ON A.ClassID = B.ClassID
                                WHERE D.kpp_stf = '" & staff_id & "' AND D.active = 1 
                                AND D.session_id = " & ddlSession.SelectedValue & " 
                                AND B.PPCSDate = '" & Commonfunction.getPpcsDate & "'
                               ORDER BY A.ClassCode"

        Dim strconnPermataPintar As String = ConfigurationManager.AppSettings("ConnectionString")

        Using mConn As New SqlConnection(strconnPermataPintar)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        ddlKodKelas.Items.Clear()

        For k = 0 To quantity - 1
            ddlKodKelas.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

        ddlKodKelas.Items.Insert(0, "Pilih")

        ddlKodKelas.SelectedIndex = 1

    End Sub

    Private Sub btnSah_Click(sender As Object, e As EventArgs) Handles btnSah.Click

        lblMsg.Text = ""

        Dim countNotUpdate As Integer = 0

        ''get stf_id
        Dim stf_ID As String = getDAtaStaff()

        ''get staff ID
        strSQL = " SELECT staff_id FROM staff_info WHERE stf_id = '" & stf_ID & "'"
        Dim staff_id As String = oCommon.getFieldValue(strSQL)

        If ddlKodKelas.Text = "Pilih" Then

            lblSah.Text = "Sila pilih kod kelas sebelum membuat pengesahan"

        Else

            For i = 0 To datRespondent.Rows.Count - 1

                Dim kpp_update As Label = CType(datRespondent.Rows(i).FindControl("record"), Label)

                If Not kpp_update.Text = "Y" Then

                    countNotUpdate = countNotUpdate + 1

                End If

            Next

            If countNotUpdate = 0 Then

                For i = 0 To datRespondent.Rows.Count - 1

                    Dim strkey = datRespondent.DataKeys(i).Value.ToString

                    strSQL = " UPDATE UKM3 SET kpp_pengesahan = 'Y', kpp_datePengesahan = GETDATE() WHERE id = '" & strkey & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = 0 Then

                        lblSah.Text = "Berjaya membuat pengesahan."

                        updateDateValidKPP()

                    End If

                Next

                getDateSah()

            Else

                lblSah.Text = "Sila kemaskini kemasukan markah untuk kesemua pelajar."

            End If

        End If

    End Sub

    Private Sub updateDateValidKPP()

        ''get stf_id
        Dim stf_ID As String = getDAtaStaff()

        ''get staff ID
        strSQL = " SELECT staff_id FROM staff_info WHERE stf_id = '" & stf_ID & "'"
        Dim staff_id As String = oCommon.getFieldValue(strSQL)

        For i = 0 To datRespondent.Rows.Count - 1

            Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

            ''18122018 get rapcs exam id
            strSQL = " SELECT instruktorExam_id FROM instruktorExam_result_kpp WHERE ukm3id = '" & strkey & "' AND stf_id = '" & stf_ID & "'"
            Dim examID As String = oCommon.getFieldValue(strSQL)

            strSQL = " UPDATE instruktorExam_result_kpp SET dateValid = GETDATE() WHERE instruktorExam_id = '" & examID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)


        Next



    End Sub

    Private Sub getDateSah()

        lblMsg.Text = ""
        lblSah.Text = ""

        ''get stf_id
        Dim stf_ID As String = getDAtaStaff()

        ''get staff ID
        strSQL = " SELECT staff_id FROM staff_info WHERE stf_id = '" & stf_ID & "'"
        Dim staff_id As String = oCommon.getFieldValue(strSQL)

        If Not ddlKodKelas.Text = "Pilih" Then

            strSQL = "  SELECT MAX(UKM3.kpp_datePengesahan)
                        FROM ukm3.dbo.UKM3 UKM3
                        LEFT JOIN ukm3.dbo.student_info student_info ON student_info.std_ID = UKM3.student_id
                        LEFT JOIN permatapintar.dbo.PPCS PPCS ON PPCS.StudentID = student_info.guid
                        WHERE UKM3.kpp_stf = '" & staff_id & "'"

            If Not ddlKodKelas.Text = "Pilih" Then

                strSQL += " AND PPCS.ClassID = '" & ddlKodKelas.SelectedValue & "'"

            End If

            Dim dateSah As String = oCommon.getFieldValue(strSQL)

            If Not dateSah = "" Then

                lblSah.Text = "Pengesahan dibuat pada tarikh : " & dateSah

            End If
        End If
    End Sub

    Protected Sub ddlSession_onselectedindexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSession.SelectedIndexChanged
        setDdlKodKelas()
    End Sub

End Class