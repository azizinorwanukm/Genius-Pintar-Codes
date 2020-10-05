Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class updateClass
    Inherits System.Web.UI.Page
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")
    Dim strClassCode As String
    'Dim strClassid As String
    Dim strCourseID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btndelete.Attributes.Add("onclick", "return confirm('Pasti hendak menghapuskan kelas tersebut?');")

        Try
            If Not IsPostBack Then
                Load_classdetail(Request.QueryString("classid"))
                ClearScreen()
            End If
            ''btnPengajar.Attributes.Add("onclick", "ListPengguna('PENGAJAR')")

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            '  oCommon.WriteLogFile(strPath, strMsg)

        End Try
    End Sub

    Private Sub ClearScreen()
        ''--lblMsg.Text = ""

        lblTotal.Text = ""

        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY ClassCode,ClassNameBM ASC"

        tmpSQL = "SELECT * FROM PPCS_Class"
        strWhere = " WITH (NOLOCK) WHERE CourseID=" & lblCourseID.Text

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function


    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            lblTotal.Text = myDataSet.Tables(0).Rows.Count
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Record not found!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
            Return False
        End Try

        Return True
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        ClearScreen()

        nPageno = e.NewPageIndex + 1


    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        '--Response.Write(strKeyID)
        Response.Redirect("updateClass.aspx?classid=" & strKeyID)

    End Sub


    Private Sub Load_classdetail(ByVal strValue As String)

        strSQL = "SELECT * FROM ppcs_class WHERE Classid=" & Request.QueryString("classid")
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("CourseID")) Then
                    lblCourseID.Text = ds.Tables(0).Rows(0).Item("CourseID")
                Else
                    lblCourseID.Text = ""
                End If

                ''--course info
                strSQL = "SELECT CourseCode FROM ppcs_course WHERE CourseID=" & lblCourseID.Text
                txtCourseCode.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT CourseNameBM FROM ppcs_course WHERE CourseID=" & lblCourseID.Text
                txtCourseNameBM.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT CourseYear FROM ppcs_course WHERE CourseID=" & lblCourseID.Text
                txtCourseYear.Text = oCommon.getFieldValue(strSQL)

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ClassCode")) Then
                    txtClassCode.Text = ds.Tables(0).Rows(0).Item("ClassCode")
                Else
                    txtClassCode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ClassNameBM")) Then
                    txtClassNameBM.Text = ds.Tables(0).Rows(0).Item("ClassNameBM")
                Else
                    txtClassNameBM.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            '  oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btndelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btndelete.Click

        Try
            strSQL = "DELETE FROM ppcs_class WHERE Classid=" & Request.QueryString("classid")
            ''--debug
            ''--Response.Write("strSQL:" & strSQL)
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                ClearScreen()
                lblMsg.Text = "Kelas berjaya dihapuskan."
            Else
                lblMsg.Text = "Error:" & strRet
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            'oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub


    Protected Sub btnkemaskini_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnkemaskini.Click
        Try
            'Query string
            strSQL = "UPDATE ppcs_class SET ClassCode='" & oCommon.FixSingleQuotes(txtClassCode.Text.ToUpper) & "',ClassNameBM='" & oCommon.FixSingleQuotes(txtClassNameBM.Text.ToUpper) & "',ClassNameBI='" & oCommon.FixSingleQuotes(txtClassNameBM.Text.ToUpper) & "' WHERE Classid=" & Request.QueryString("classid")
            ''--debug
            ''--Response.Write("strsql:" & strSQL)
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "Berjaya dikemaskini."
                ClearScreen()
            Else
                lblMsg.Text = strRet
            End If


        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            ' oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

End Class