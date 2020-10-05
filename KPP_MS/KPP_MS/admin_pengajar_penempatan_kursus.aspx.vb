Imports System.Data.SqlClient

Public Class admin_pengajar_penempatan_kursus
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub subject_info_list()
        strSQL = "SELECT * FROM subject_info WHERE subject_year='" & Now.Year & "' ORDER BY subject_Name ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlsubject_ID.DataSource = ds
            ddlsubject_ID.DataTextField = "subject_Name"
            ddlsubject_ID.DataValueField = "subject_ID"
            ddlsubject_ID.DataBind()

            ''ddlsubject_ID.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception

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
        Dim strOrder As String = ""

        tmpSQL = "select subject_ID,subject_Name from subject_info"
        tmpSQL += " left join staff_Info on staff_Info.staff_ID=subject_info.staff_ID"
        strWhere = " where staff_Info.staff_ID='" & staff_id.Text & "'"

        getSQL = tmpSQL & strWhere
        ''--debug
        lblMsg.Text = getSQL

        Return getSQL

    End Function

    Private Sub btnSearch_ServerClick(sender As Object, e As EventArgs) Handles btnSearch.ServerClick
        strRet = BindData(datRespondent)
    End Sub

End Class