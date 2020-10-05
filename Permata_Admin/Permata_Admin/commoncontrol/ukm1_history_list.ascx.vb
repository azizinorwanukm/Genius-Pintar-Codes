Imports System.Data.SqlClient

Partial Public Class ukm1_history_list
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
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT UKM1ID,StudentID,ExamYear,ExamStart,ExamEnd,Status,LastPage,ModA,ModB,ModC,TotalScore,TotalPercentage FROM UKM1"
        strWhere = " WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strOrder = " ORDER BY ExamYear"

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
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("admin.ukm1.view.aspx?studentid=" & Request.QueryString("studentid") & "&ukm1id=" & strKeyID)
                Case "ADMINOP"
                    '--no action
                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datRespondent.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                '--security
                Select Case getUserProfile_UserType()
                    Case "ADMIN"
                        '--dont hide
                    Case "ADMINOP"
                        hideColumn("11")
                    Case Else
                        hideColumn("6,7,8,9,10,11")
                End Select

            End If

        Catch ex As Exception
            lblDebug.Text = "datRespondent_RowDataBound:" & ex.Message
        End Try

    End Sub

    Private Sub hideColumn(ByVal strIndex As String)
        '--admin dont send anything to hide
        If strIndex.Length = 0 Then
            Exit Sub
        End If

        ' Split string based on spaces.
        Dim arColumn As String() = strIndex.Split(",")

        ' Use For Each loop over words and display them.
        Dim nColumn As String
        For Each nColumn In arColumn
            datRespondent.Columns(nColumn).Visible = False
        Next

        ' datRespondent.Columns(6).Visible = False
    End Sub

End Class