Imports System.Data.SqlClient

Public Class report_UKM1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConnMaster As String = ConfigurationManager.AppSettings("ConnectionMaster")
    Dim objConnMaster As SqlConnection = New SqlConnection(strConnMaster)

    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                year_list_info()

                page_Load()

                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub year_list_info()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub page_Load()
        strSQL = "SELECT Parameter from setting where Type ='Year' and Parameter = '" & Now.Year & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                ddlYear.SelectedValue = ds.Tables(0).Rows(0).Item("Parameter")
            Else
                ddlYear.SelectedValue = ""
            End If
        End If
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConnPermata)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False
        End Try
        Return True
    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT F.std_ID,F.student_Name,F.student_Mykad,B.AlumniID,B.DOB_Year,F.student_Religion,A.TotalScore,A.ExamStart,A.ExamEnd,A.QuestionYear,A.ModA,A.ModB,A.ModC,A.TotalPercentage, E.SchoolCode FROM UKM1_" & ddlYear.SelectedValue & " A "
        tmpSQL += " LEFT OUTER JOIN StudentProfile B ON A.StudentID = B.StudentID"
        tmpSQL += " LEFT OUTER JOIN StudentSchool D ON A.StudentID = D.StudentID AND D.IsLatest='Y'"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile E ON D.SchoolID = E.SchoolID"
        tmpSQL += " LEFT OUTER JOIN kolejadmin.dbo.student_info F ON B.MYKAD = F.student_Mykad"
        strWhere = " WHERE A.ExamYear = '" & ddlYear.SelectedValue & "'
                      AND F.student_Status = 'Access'"

        If txtstudent.Text.Length > 0 And txtstudent.Text <> "" Then
            strWhere += " AND (F.student_Name like '%" & txtstudent.Text & "%'"
            strWhere += " OR F.student_Mykad = '" & txtstudent.Text & "'"
            strWhere += " OR B.AlumniID = '" & txtstudent.Text & "')"
        End If

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL
    End Function

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        strRet = BindData(datRespondent)
    End Sub
End Class