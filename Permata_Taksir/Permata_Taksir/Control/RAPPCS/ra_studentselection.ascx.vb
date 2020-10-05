Imports System.Data.SqlClient

Public Class ra_studentselection1
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

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub CheckUncheckAll(sender As Object, e As System.EventArgs)

        Dim chk1 As CheckBox
        chk1 = DirectCast(datRespondent.HeaderRow.Cells(0).FindControl("chkAll"), CheckBox)
        For Each row As GridViewRow In datRespondent.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("chkSelect"), CheckBox)
            chk.Checked = chk1.Checked
        Next

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 1200

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada Rekod Pelajar"
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

        tmpSQL = "  SELECT B.id, A.std_ID, A.student_Name, A.student_Mykad, CASE WHEN A.student_sex = 1 THEN 'Lelaki' ELSE 'Perempuan' END student_sex, C.AlumniID, Y.ClassCode, STAFFINFO.staff_name
                    from ukm3.dbo.ukm3 B
                    LEFT JOIN ukm3.dbo.student_info A on B.student_id=A.std_ID
                    LEFT JOIN permatapintar.dbo.StudentProfile C on C.MYKAD=A.student_mykad
                    LEFT JOIN permatapintar.dbo.PPCS J on A.guid = J.StudentID AND J.PPCSDate = 'PPCS DIS 2018 (UKM)'
                    LEFT JOIN permatapintar.dbo.PPCS_Class z on z.ClassCode = J.PPCSClass 
                    LEFT JOIN permatapintar.dbo.PPCS_Course x ON J.CourseID=x.CourseID
                    LEFT JOIN permatapintar.dbo.PPCS_Class y on J.ClassID = y.ClassID
                    LEFT JOIN ukm3.dbo.staff_info STAFFINFO ON STAFFINFO.staff_id = B.rapcs_stf"
        strWhere += " WHERE B.active = '1'"

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
        Dim rapcs_status As Label = DirectCast(datRespondent.Rows(e.NewSelectedIndex).FindControl("record"), Label)
        Dim encyptid As String = strKeyID
        Try
            Select Case getUserProfile_UserType()
                Case "Ra PPCS"
                    Response.Redirect("rappcs.masukmarkah.aspx?studentid=" & encyptid & "&rapcs_update=" & rapcs_status.Text)
                Case "Instruktor Ra PPCS"
                    Response.Redirect("rappcs.masukmarkah.aspx?studentid=" & encyptid & "&rapcs_update=" & rapcs_status.Text)
                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        strRet = BindData(datRespondent)

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

        strSQL = "SELECT parameter FROM general_config WHERE config = 'currentSession'"
        Dim currentSession As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT id FROM UKM3Session WHERE isCurrent = '" & currentSession & "'"
        Dim id As String = oCommon.getFieldValue(strSQL)

        ddlSession.SelectedValue = id

    End Sub

    Private Sub setDdlKodKelas()

        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT M.ClassID, G.ClassCode FROM( "
        query += " SELECT A.ClassID FROM permatapintar.dbo.PPCS_Class A "
        query += " JOIN permatapintar.dbo.PPCS B ON B.ClassID = A.ClassID AND B.PPCSDate = '" & TaksirCommon.getPpcsFromUKM3(ddlSession.SelectedValue) & "' "
        query += " JOIN student_info C ON C.guid = B.StudentID "
        query += " JOIN UKM3 D ON D.student_id = C.std_ID "
        query += " WHERE D.active = 1 AND D.session_id = " & ddlSession.SelectedValue & " GROUP BY A.ClassID) M  "
        query += " LEFT JOIN permatapintar.dbo.PPCS_Class G ON G.ClassID = M.ClassID "

        Dim strconnPermataPintar As String = ConfigurationManager.AppSettings("ConnectionUkm")

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

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function getDAtaStaff() As String
        strSQL = "SELECT top 1 stf_id from staff_info where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click

        Dim stf_ID As String = getDAtaStaff()

        ''get staff ID
        strSQL = " SELECT staff_id FROM staff_info WHERE stf_id = '" & stf_ID & "'"
        Dim staff_id As String = oCommon.getFieldValue(strSQL)

        For i = 0 To datRespondent.Rows.Count - 1

            Dim chkselect As CheckBox = CType(datRespondent.Rows(i).FindControl("chkSelect"), CheckBox)

            If chkselect.Checked = True Then

                Dim strkey As String = datRespondent.DataKeys(i).Value.ToString
                Dim strMykad As Label = CType(datRespondent.Rows(i).FindControl("MYKAD"), Label)

                strSQL = " UPDATE UKM3 SET rapcs_stf = '" & staff_id & "' WHERE id = '" & strkey & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

        Next

        If Not strRet = 0 Then

            lblMsg.Text = "Tidak Berjaya Menyimpan Maklumat"

        Else

            lblMsg.Text = "Maklumat Berjaya Disimpan!"

        End If


    End Sub

    Protected Sub ddlSession_onselectedindexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSession.SelectedIndexChanged
        setDdlKodKelas()
    End Sub

End Class