Imports System.Data.SqlClient

Partial Public Class studentschool_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT StudentSchool.StudentSchoolID,SchoolProfile.SchoolName,SchoolProfile.SchoolCode,SchoolProfile.SchoolCity,SchoolProfile.SchoolState,SchoolProfile.SchoolType,StudentSchool.StartDate,StudentSchool.EndDate,StudentSchool.IsLatest FROM StudentSchool"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere = " WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strOrder = " ORDER BY StudentSchool.EndDate"

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
        Try
            ''jumlah pelajar berdaftar
            'Response.Redirect("ukm1.school.students.aspx?schoolid=" & strKeyID)
            Select Case Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
                Case "ADMIN"
                    Response.Redirect("studentschool.update.aspx?studentschoolid=" & strKeyID & "&studentid=" & Request.QueryString("studentid"))
                Case "ADMINOP"
                    Response.Redirect("studentschool.update.aspx?studentschoolid=" & strKeyID & "&studentid=" & Request.QueryString("studentid"))
                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lnkCreate_Click(sender As Object, e As EventArgs) Handles lnkCreate.Click
        Select Case Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
            Case "ADMIN"
                Response.Redirect("studentschool.select.aspx?studentid=" & Request.QueryString("studentid"))
            Case "ADMINOP"
                lblMsg.Text = "Invalid user type!" & Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
            Case Else
                lblMsg.Text = "Invalid user type!" & Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)
        End Select



    End Sub
End Class