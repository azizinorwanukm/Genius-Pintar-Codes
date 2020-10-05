Imports System.Data.SqlClient

Public Class student_Ukm3_History
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConnUKM3 As String = ConfigurationManager.AppSettings("ConnectionUKM3")
    Dim objConnUKM3 As SqlConnection = New SqlConnection(strConnUKM3)

    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, objConnUKM3)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            If myDataSet.Tables(0).Rows.Count = 0 Then
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConnUKM3.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " order by G.ukm3Year ASC"

        tmpSQL = "select A.id, G.ukm3Year, A.marks_100, A.markPretest, A.markPosttest, A.insPPCS100mark, B.instruktorExam_Komen, 
                  A.insRAPPCS100mark, C.instruktorExam_Komen, A.insKPP100mark, D.instruktorExam_Komen_kpp, A.compo_Mark from ukm3 A
                  left join instruktorExam_result B on A.id = B.ukm3id
                  left join instruktorExam_result_raPcs C on A.id = C.ukm3id
                  left join instruktorExam_result_kpp D on A.id = D.ukm3id
                  left join student_info E on A.student_id = E.std_ID
                  left join kolejadmin.dbo.student_info F on E.student_Mykad = F.student_Mykad
                  left join UKM3Session G on A.session_id = G.id"

        strWhere = " where F.student_status = 'Access' and F.std_ID = '" & Request.QueryString("std_ID") & "'"

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function
End Class