Imports System.Data.SqlClient

Public Class tappcs_prepostMark

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                setDdlExams()
                populatePpcsdate()
                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY A.student_Name ASC"

        tmpSQL = "SELECT B.id as student_ID,A.student_Name as student_Name,A.student_Mykad as student_Mykad ,C.AlumniID as AlumniID,Y.ClassCode as classcode,B.markPretest as pretest,B.markPosttest as posttest   
                  ,(B.markPosttest-B.markPretest) as difference
                    from ukm3.dbo.ukm3 B
                    LEFT JOIN ukm3.dbo.student_info A on B.student_id=A.std_ID
                    LEFT JOIN permatapintar.dbo.StudentProfile C on C.MYKAD=A.student_mykad
                          
                    LEFT JOIN permatapintar.dbo.PPCS J on A.guid = J.StudentID AND J.PPCSDate = '" & ddlPpcsDate.SelectedValue & "'
                    
					LEFT JOIN permatapintar.dbo.PPCS_Class z on z.ClassCode = J.PPCSClass 
					LEFT JOIN permatapintar.dbo.PPCS_Course x ON J.CourseID=x.CourseID
					LEFT JOIN permatapintar.dbo.PPCS_Class y on J.ClassID = y.ClassID 

                    WHERE (Y.NamaPengajar like '%" & getUserName_Usertype() & "%' or Y.NamaPembantuPengajar like '%" & getUserName_Usertype() & "%' or Y.NamaPengurusPelajar like '%" & getUserName_Usertype() & "%' 
                    or Y.NamaPembantuPelajar like '%" & getUserName_Usertype() & "%') and 
                    B.active = 1 AND B.session_id = " & ddlSession.SelectedValue

        ''search
        If Not txtsearch.Text.Length = 0 Then
            strWhere += " AND (A.student_Name LIKE '%" & txtsearch.Text & "%'"
            strWhere += " OR A.student_Mykad LIKE '%" & txtsearch.Text & "%')"
        End If

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL
    End Function

    Private Function getUserName_Usertype() As String
        strSQL = "SELECT staff_name FROM staff_info where staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet

        Dim myDataAdapter As New SqlDataAdapter(getSQL(), strConn2)
        myDataAdapter.SelectCommand.CommandTimeout = 1200

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

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)
            Dim mark_pre As TextBox = CType(datRespondent.Rows(i).FindControl("txt_preTest"), TextBox)

            Dim mark_post As TextBox = CType(datRespondent.Rows(i).FindControl("txt_postTest"), TextBox)
            Dim id As Label = CType(datRespondent.Rows(i).Cells(2).FindControl("lbl_MYKAD"), Label)
            If chkUpdate IsNot Nothing Then

                If chkUpdate.Checked = True Then
                    Try
                        Dim ukm3id As String = datRespondent.DataKeys(i).Value.ToString

                        strSQL = "UPDATE ukm3.dbo.UKM3 SET ukm3.dbo.UKM3.markPretest = '" & mark_pre.Text & "',ukm3.dbo.UKM3.markPosttest ='" & mark_post.Text & "' WHERE ukm3.dbo.UKM3.student_id  =(select A.student_id from ukm3.dbo.UKM3 A
                                left join ukm3.dbo.student_info B on A.student_id =B.std_ID
                                where B.student_Mykad='" & id.Text & "')"

                        strRet = oCommon.ExecuteSQL(strSQL)

                        If strRet = 0 Then
                            lblMsg.Text = "Kemaskini Berjaya"
                        Else

                            lblMsg.Text = "Kemaskini tidak Berjaya"
                        End If

                    Catch ex As Exception
                        lblMsgTop.Text = "Error:" & strRet
                    End Try

                End If
            End If
        Next

        BindData(datRespondent)


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

    Private Sub populatePpcsdate()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT PPCSDate FROM permatapintar.dbo.master_PPCSDate"

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
            ddlPpcsDate.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(0).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

        Dim currentDate As String = oCommon.getFieldValue("SELECT configString FROM permatapintar.dbo.master_Config WHERE configCode = 'DefaultPPCSDate'")

        ddlPpcsDate.SelectedValue = currentDate
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        strRet = BindData(datRespondent)

    End Sub

End Class