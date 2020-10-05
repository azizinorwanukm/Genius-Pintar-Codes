Imports System.Data.SqlClient

Partial Public Class studentschool_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strRet = BindData(datRespondent)

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
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            '--debug
            Response.Write("BindData Error:" & ex.Message)
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim decryptid As String = oDes.DecryptData(Request.QueryString("studentid"))
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT student_school.studentschool_id,config_school.school_name,config_school.school_code,config_school.school_city,config_school.school_state,config_school.school_type,student_school.startdate,student_school.enddate,student_school.IsLatest FROM student_school"
        tmpSQL += " LEFT JOIN config_school ON student_school.school_id = config_school.school_id"
        strWhere = " WHERE std_id='" & decryptid & "'"
        strOrder = " ORDER BY student_school.studentschool_id DESC"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Dim encryptdataId As String = oDes.EncryptData(strKeyID)
        Try
            ''jumlah pelajar berdaftar
            'Response.Redirect("ukm1.school.students.aspx?schoolid=" & strKeyID)
            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("ukm3_admin.sekolahupdatedata.aspx?studentschoolid=" & encryptdataId & "&studentid=" & Request.QueryString("studentid"))

                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub lnkCreate_Click(sender As Object, e As EventArgs) Handles lnkCreate.Click
        Select Case getUserProfile_UserType()
            Case "Admin"
                Response.Redirect("ukm3_admin.sekolahupdate.aspx?studentid=" & Request.QueryString("studentid"))
            Case Else
                lblMsg.Text = "Invalid user type!"
        End Select

    End Sub

End Class