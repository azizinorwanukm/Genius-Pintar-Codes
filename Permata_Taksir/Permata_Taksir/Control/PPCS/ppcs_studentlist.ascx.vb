Imports System.Data.SqlClient

Public Class ppcs_studentlist
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
                setDdlExams()
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
                lblMsg.Text = "Semua pelajar telah di semak!"
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
        Dim strOrder As String = " ORDER BY A.student_Name"


        tmpSQL = " SELECT B.id, A.student_Name as studentName,CASE WHEN B.Ppcs_update = 'Y' THEN W.dateValid ELSE W.dateValid END Ppcs_update
                       , A.student_Mykad, C.AlumniID, Y.ClassCode,
                       CASE WHEN W.saved = 1 THEN 'Lengkap' ELSE 'Belum dinilai' END saved
                       , Y.NamaPengajar ,Y.NamaPembantuPengajar ,Y.NamaPembantuPelajar 
                       from ukm3.dbo.ukm3 B
                       LEFT JOIN ukm3.dbo.student_info A on B.student_id=A.std_ID
                       LEFT JOIN permatapintar.dbo.StudentProfile C on C.MYKAD=A.student_mykad
                       LEFT JOIN permatapintar.dbo.PPCS J on A.guid = J.StudentID AND J.PPCSDate = '" & TaksirCommon.getPpcsFromUKM3(ddlSession.SelectedValue) & "'
                       LEFT JOIN permatapintar.dbo.PPCS_Class z on z.ClassCode = J.PPCSClass 
                       LEFT JOIN permatapintar.dbo.PPCS_Course x ON J.CourseID=x.CourseID
                       LEFT JOIN permatapintar.dbo.PPCS_Class y on J.ClassID = y.ClassID
                       LEFT JOIN ukm3.dbo.instruktorExam_result W ON W.ukm3id = B.id
                       where (Y.NamaPengajar like '%" & getUserName_Usertype() & "%' or Y.NamaPembantuPengajar like '%" & getUserName_Usertype() & "%' or Y.NamaPengurusPelajar like '%" & getUserName_Usertype() & "%' 
                       or Y.NamaPembantuPelajar like '%" & getUserName_Usertype() & "%') and  B.active = 1 AND B.session_id = " & ddlSession.SelectedValue

        ''search
        If Not txtsearch.Text.Length = 0 Then
            strWhere += " AND (A.student_Name LIKE'%" & txtsearch.Text & "%'"
            strWhere += " OR A.student_Mykad LIKE '%" & txtsearch.Text & "%')"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getUserName_Usertype() As String
        strSQL = "SELECT staff_name FROM staff_info where staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Dim encyptid As String = strKeyID

        Dim saved As String = oCommon.getFieldValue("SELECT saved FROM instruktorExam_result where ukm3id = " & encyptid)

        'If saved = "1" Then
        '    lblMsgTop.Text = "Penilian pelajar ini sudah dilakukan!"
        '    Return
        'End If

        Dim ppcs_status As Label = DirectCast(datRespondent.Rows(e.NewSelectedIndex).FindControl("record"), Label)
        Try
            Select Case getUserProfile_UserType()
                Case "Instruktor PPCS"
                    Response.Redirect("ppcs.masukmarkah.aspx?studentid=" & encyptid)
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
    Private Function getDAtaStaff() As String
        strSQL = "SELECT top 1 stf_id from staff_info where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        strRet = BindData(datRespondent)

    End Sub

    Private Sub setDdlExams()
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

        Dim currentSession As String = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'currentSession'")

        ddlSession.SelectedValue = currentSession

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

    Private Sub btnSah_Click(sender As Object, e As EventArgs) Handles btnSah.Click
        Dim stf_ID As String = getDAtaStaff()

        ''get staff ID
        'strSQL = " SELECT staff_id FROM staff_info WHERE stf_id = '" & stf_ID & "'"
        'Dim staff_id As String = oCommon.getFieldValue(strSQL)

        lblMsg.Text = ""

        Dim countRow As Integer = datRespondent.Rows.Count

        Dim success As Integer = 0
        Dim fail As Integer = 0

        For i = 0 To countRow - 1

            Dim chkselect As CheckBox = CType(datRespondent.Rows(i).FindControl("chkSelect"), CheckBox)

            If chkselect.Checked = True Then
                Dim ukm3id As String = datRespondent.DataKeys(i).Value.ToString
                Dim saved As String = ""

                Try
                    saved = oCommon.getFieldValue("SELECT saved FROM instruktorExam_result WHERE ukm3id= " & ukm3id)
                Catch ex As Exception
                    fail = fail + 1
                    Continue For
                End Try

                If saved = "1" Then

                    Try
                        Dim queryString As String = "UPDATE instruktorExam_result SET dateValid = GETDATE() WHERE ukm3id = " & ukm3id
                        queryString += " UPDATE UKM3 SET Ppcs_update = 'Y', ppcs_id = '" & stf_ID & "' WHERE id = " & ukm3id

                        oCommon.ExecuteSQL(queryString)
                    Catch ex As Exception
                        fail = fail + 1
                        Continue For
                    End Try
                    success = success + 1
        Else
                    fail = fail + 1
                End If
            End If

        Next

        Dim message As String = ""

        If success > 0 Then
            message += "Berjaya sahkan " & success & " pelajar<br> "
        End If

        If fail > 0 Then
            message += "Gagal sahkan " & fail & " pelajar<br> "
        End If

        lblMsg.Text = message

    End Sub
End Class