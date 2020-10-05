Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class ppcs_class_assign
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")
    Dim strClassID As String
    Dim strTestID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                '--PPCSDate
                strSQL = "SELECT PPCSDate FROM PPCS_Class WHERE ClassID=" & Request.QueryString("classid")
                lblPPCSDate.Text = oCommon.getFieldValue(strSQL)

                master_dobyear_list()
                ddlDOB_Year.Text = "ALL"

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            '  oCommon.WriteLogFile(strPath, strMsg)

        End Try
    End Sub


    Private Sub master_dobyear_list()
        strSQL = "SELECT DOB_Year FROM master_Dobyear ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlDOB_Year.DataSource = ds
            ddlDOB_Year.DataTextField = "DOB_Year"
            ddlDOB_Year.DataValueField = "DOB_Year"
            ddlDOB_Year.DataBind()

            ddlDOB_Year.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod ditemui!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = " ORDER BY StudentFullname"

        tmpSQL = "SELECT a.PPCSID,a.StudentID,a.CourseID,b.StudentFullname,b.MYKAD,b.DOB_Year,b.StudentRace,b.StudentReligion,b.StudentCity,b.StudentState,b.AlumniID,a.PPCSStatus,a.StatusTawaran,(SELECT ClassCode FROM PPCS_Class WHERE PPCS_Class.ClassID=a.ClassID) as ClassCode FROM PPCS a,StudentProfile b"
        strWhere = " WITH (NOLOCK) WHERE a.StudentID=b.StudentID AND a.CourseID=" & Request.QueryString("courseid")

        If Not ddlDOB_Year.Text = "ALL" Then
            strWhere += " AND b.DOB_Year ='" & ddlDOB_Year.Text & "'"
        End If

        If Not selStudentGender.Value = "ALL" Then
            strWhere += " AND b.StudentGender ='" & selStudentGender.Value & "'"
        End If

        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND b.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND b.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        ''--debug
        ''--lblMsg.Text = "Updating :"
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

            If isChecked Then
                strSQL = "UPDATE PPCS SET CourseID=" & Request.QueryString("courseid") & ",ClassID=" & Request.QueryString("classid") & " WHERE PPCSID=" & datRespondent.DataKeys(i).Value.ToString
                oCommon.ExecuteSQL(strSQL)
                ''--lblMsg.Text += datRespondent.DataKeys(i).Value.ToString & ":"
            End If
        Next

        strRet = BindData(datRespondent)
        lblMsg.Text = "Berjaya Assign Kelas."

    End Sub

    Private Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        ''--debug
        ''--lblMsg.Text = "Updating :"
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim isChecked As Boolean = DirectCast(row.FindControl("chkSelect"), CheckBox).Checked

            If isChecked Then
                strSQL = "UPDATE PPCS SET ClassID=NULL WHERE PPCSID=" & datRespondent.DataKeys(i).Value.ToString
                oCommon.ExecuteSQL(strSQL)
                ''--lblMsg.Text += datRespondent.DataKeys(i).Value.ToString & ":"
            End If
        Next

        strRet = BindData(datRespondent)
        lblMsg.Text = "Berjaya Remove Kelas."

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub
End Class