Imports System.Data.SqlClient

Public Class student_Pcis_History
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConnMaster As String = ConfigurationManager.AppSettings("ConnectionMaster")
    Dim objConnMaster As SqlConnection = New SqlConnection(strConnMaster)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, objConnMaster)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            If myDataSet.Tables(0).Rows.Count = 0 Then
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConnMaster.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "select B.UKM1ID,B.ExamYear,B.ExamStart,B.ExamEnd,B.Status,B.LastPage,B.ModA,B.ModB,B.ModC,B.TotalScore,B.TotalPercentage from kolejadmin.dbo.student_info C
                  left join StudentProfile A on C.student_Mykad = A.MYKAD
                  left join UKM1 B on A.StudentID = B.StudentID"
        strWhere = " where C.std_ID = '" & Request.QueryString("std_ID") & "'"
        strOrder = " ORDER BY B.ExamYear"

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function
End Class