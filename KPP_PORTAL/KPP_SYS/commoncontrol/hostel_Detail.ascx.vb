Imports System.Data.SqlClient
Imports System.IO

Public Class hostel_Detail
    Inherits System.Web.UI.UserControl
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Dim id As String = ""
                id = Request.QueryString("admin_ID")

                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""

        tmpSQL = "select hostel_info.hostel_Name,hostel_info.block_Name,hostel_info.block_Level,room_info.room_Name,room_info.year,student_info.std_ID from hostel_info
                  left join room_info on hostel_info.hostel_ID = room_info.hostel_ID
                  left join student_info on room_info.std_ID = student_info.std_ID"
        strWhere = " where hostel_info.year = '" & Now.Year & "' and student_info.std_ID = '" & Request.QueryString("std_ID") & "'"

        getSQL = tmpSQL & strWhere
        Debug.WriteLine(getSQL)
        ''--debug
        Return getSQL
    End Function


End Class