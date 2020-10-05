Imports System.Data.SqlClient

Public Class kpp_studentlist1
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
                ''Tukar value of Jantina
                ''For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                ''Dim jantina As Label = CType(datRespondent.Rows(i).Cells(4).FindControl("student_sex"), Label)
                ''If jantina.Text = "1" Then
                ''jantina.Text = "Lelaki"
                ''ElseIf jantina.Text = "0" Then
                ''  jantina.Text = "Perempuan"
                ''End If

                ''Dim simpanan As Label = CType(datRespondent.Rows(i).Cells(5).FindControl("record"), Label)
                ''Dim rekod As Label = CType(datRespondent.Rows(i).Cells(5).FindControl("rekod"), Label)
                ''If simpanan.Text = "N" Then
                ''rekod.Text = "Tiada Simpanan"
                ''ElseIf simpanan.Text = "Y" Then
                ''  rekod.Text = "Penilaian Sudah Dilakukan"
                ''Else
                ''  rekod.Text = "Tiada Simpanan"
                ''End If
                ''  Next

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY A.student_name "

        tmpSQL = "SELECT A.std_ID,A.student_Name,A.student_ID,CASE WHEN A.student_sex = 1 THEN 'Lelaki' ELSE 'Perempuan' END student_sex,A.student_Mykad,A.student_FonNo, "
        tmpSQL += "CASE WHEN A.Kpp_update = 'Y' THEN 'Penilaian Sudah Dilakukan' ELSE 'Tiada Simpanan' END Kpp_update "
        tmpSQL += "FROM ukm3 D LEFT JOIN student_info A ON A.std_ID = D.student_id "
        strWhere += " WHERE D.active = 1 AND D.session_id = " & ddlSession.SelectedValue

        ''search
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
        Try
            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("ukm3_admin_kpp_markUpdate.aspx?studentid=" & encyptid)
                Case "Instruktor KPP"
                    Response.Redirect("kpp_studentlist_checklist.aspx?studentid=" & encyptid & "&kpp_update=" & kpp_status.Text)
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

End Class