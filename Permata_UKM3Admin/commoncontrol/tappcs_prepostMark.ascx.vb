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
                setDdlSession()
                setDdlClass()
                strRet = BindData(datRespondent)

                End If
            Catch ex As Exception

            End Try

        End Sub

        Private Function getSQL() As String
            Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY A.student_Name ASC"

        tmpSQL = "SELECT B.id as student_ID,A.student_Name as student_Name,A.student_Mykad as student_Mykad ,C.AlumniID as AlumniID,Y.ClassCode as classcode,B.markPretest as pretest,B.markPosttest as posttest ,
                    (B.markPosttest-B.markPretest) as difference
                    from ukm3.dbo.ukm3 B
                    LEFT JOIN ukm3.dbo.student_info A on B.student_id=A.std_ID
                    LEFT JOIN permatapintar.dbo.StudentProfile C on C.MYKAD=A.student_mykad
                          
                    LEFT JOIN permatapintar.dbo.PPCS J on A.guid = J.StudentID AND J.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'
                    
					LEFT JOIN permatapintar.dbo.PPCS_Class z on z.ClassCode = J.PPCSClass 
					LEFT JOIN permatapintar.dbo.PPCS_Course x ON J.CourseID=x.CourseID
					LEFT JOIN permatapintar.dbo.PPCS_Class y on J.ClassID = y.ClassID 

                    WHERE B.active = 1 AND B.session_id = " & ddlSession.SelectedValue

        ''search
        If Not txtsearch.Text.Length = 0 Then
            strWhere += " AND (A.student_Name LIKE '%" & txtsearch.Text & "%'"
            strWhere += " OR A.student_Mykad LIKE '%" & txtsearch.Text & "%')"
        End If

        If Not ddlClass.SelectedValue = "0" Then
            strWhere += " AND J.ClassID = '" & ddlClass.SelectedValue & "' "
        End If

        getSQL = tmpSQL & strWhere & strOrder

            Return getSQL
        End Function


    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

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


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

            strRet = BindData(datRespondent)

        End Sub

    End Class