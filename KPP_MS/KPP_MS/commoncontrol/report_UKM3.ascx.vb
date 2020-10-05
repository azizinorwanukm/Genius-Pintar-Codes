Imports System.Data.SqlClient

Public Class report_UKM3
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConnMaster As String = ConfigurationManager.AppSettings("ConnectionMaster")
    Dim objConnMaster As SqlConnection = New SqlConnection(strConnMaster)

    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionUKM3")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                year_list_info()

                Page_Load()

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
            objConnPermata.Close()

        Catch ex As Exception

            Return False
        End Try
        Return True
    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " order by F.student_Name ASC"

        tmpSQL = "select A.id, F.student_Name, F.student_Mykad ,E.AlumniID, A.marks_100, A.markPretest, A.markPosttest, A.insPPCS100mark, B.instruktorExam_Komen, 
                  A.insRAPPCS100mark, C.instruktorExam_Komen, A.insKPP100mark, D.instruktorExam_Komen_kpp, A.compo_Mark from ukm3 A
                  left join instruktorExam_result B on A.id = B.ukm3id
                  left join instruktorExam_result_raPcs C on A.id = C.ukm3id
                  left join instruktorExam_result_kpp D on A.id = D.ukm3id
                  left join student_info E on A.student_id = E.std_ID
                  left join kolejadmin.dbo.student_info F on E.student_Mykad = F.student_Mykad
                  left join UKM3Session G on A.session_id = G.id"

        strWhere = " where F.student_status = 'Access' and G.ukm3Year = '" & ddlYear.SelectedValue & "'"

        If txtstudent.Text.Length > 0 And txtstudent.Text <> "" Then
            strWhere += " AND (F.student_Name like '%" & txtstudent.Text & "%'"
            strWhere += " OR F.student_Mykad = '" & txtstudent.Text & "'"
            strWhere += " OR E.AlumniID = '" & txtstudent.Text & "')"
        End If

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL
    End Function

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        strRet = BindData(datRespondent)
    End Sub

End Class