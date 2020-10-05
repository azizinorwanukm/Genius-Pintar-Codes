Imports System.Data.SqlClient

Public Class student_Ppcs_History
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                strRet = BindData(datRespondent)
            End If

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

    Private Function getTotal() As Integer
        strSQL = "SELECT COUNT(DISTINCT StudentID) as nTotal FROM PPCS"
        getTotal = oCommon.getFieldValue(strSQL)

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT A.PPCSID,A.PPCSDate,A.PPCSStatus,A.StatusTawaran,A.StatusReason,A.NamaAsrama,A.NoBilik,B.CourseCode,C.ClassCode FROM PPCS A
                  LEFT JOIN PPCS_Course B ON A.CourseID = B.CourseID
                  LEFT JOIN PPCS_Class C ON A.ClassID = C.ClassID
                  LEFT JOIN StudentProfile D on A.StudentID = D.StudentID
                  LEFT JOIN kolejadmin.dbo.student_info E on D.MYKAD = E.student_Mykad"
        strWhere = " WHERE E.std_ID ='" & Request.QueryString("std_ID") & "'"
        strOrder = " ORDER BY A.PPCSDate"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        '--Response.Write(getSQL)

        Return getSQL

    End Function

End Class