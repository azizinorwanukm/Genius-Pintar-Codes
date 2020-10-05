''Imports System.Data
''Imports System.Data.OleDb
Imports System.Data.SqlClient
''Imports System.IO
''Imports System.Globalization

Public Class config_examStudentQuestionList
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
                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try

    End Sub


    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY quest_no"

        tmpSQL = "SELECT A.id,A.quest_no,A.question,B.subjectName, A.difficulty"
        tmpSQL += " ,CASE WHEN A.answer = 1 THEN 'A' WHEN A.answer = 2 THEN 'B' WHEN A.answer = 3 THEN 'C' WHEN A.answer = 4 THEN 'D' ELSE '0' END answer"
        tmpSQL += " from Questions A LEFT JOIN StemSubject B ON A.subject_id=B.id"
        strWhere += " WHERE A.exam_id='" & Request.QueryString("exam_id") & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 1200

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tiada Soalan Dalam Pangakalan Data"
            Else
                lblMsg.Text = "Jumlah Soalan : " & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsgTop.Text = "Error : " & ex.Message
            Return False
        End Try
        Return True
    End Function

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
#Disable Warning S1481 ' Unused local variables should be removed
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Dim quest_no As String = oCommon.getFieldValue("SELECT TOP 1 quest_no FROM QUESTIONS WHERE id =" & strKeyID)

        Try
            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("config_examStudentQuestionUpload.aspx?exam_id=" & Request.QueryString("exam_id") & "&question=" & quest_no)
                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable

        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionMaster")
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


    Protected Sub btn_Update_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btn_delete_Click(sender As Object, e As EventArgs)

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


End Class