Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography

Public Class alumni_Student_Status
    Inherits System.Web.UI.UserControl
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then



            getddlstatus()

        End If
    End Sub

    Private Function getddlstatus()
        strSQL = "SELECT Parameter from setting where Type = 'Marital Status' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStatus.DataSource = ds
            ddlStatus.DataTextField = "Parameter"
            ddlStatus.DataValueField = "Parameter"
            ddlStatus.DataBind()
            ddlStatus.Items.Insert(0, New ListItem("Select Status", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Function

    Private Sub LoadPage(ByVal Access As String)

        strSQL = "select * from alumni_studentData where std_id = '" + Access + "')"


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


            If Not IsDBNull(ds.Tables(0).Rows(0).Item("maritalStatus")) Then
                ddlStatus.SelectedValue = ds.Tables(0).Rows(0).Item("maritalStatus")
            Else
                ddlStatus.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("ageMarried")) Then
                txtAgeMarried.Text = ds.Tables(0).Rows(0).Item("ageMarried")
            Else
                txtAgeMarried.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("yearMarried")) Then
                txtYearMarried.Text = ds.Tables(0).Rows(0).Item("yearMarried")
            Else
                txtYearMarried.Text = ""
            End If

        End If
    End Sub

    Private Sub update_ServerClick(sender As Object, e As EventArgs) Handles update.ServerClick
        Dim Access As String = Request.QueryString("std_ID")
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        If oCommon.isExist(strSQL) = True Then
            strSQL = "update alumni_studentData set maritalStatus = '" & ddlStatus.SelectedValue & "' ,ageMarried = '" & txtAgeMarried.Text & "' ,yearMarried = '" & txtYearMarried.Text & "'
                    where std_id = '" + Access + "' "
            oCommon.ExecuteSQL(strSQL)

        Else
            strSQL = "insert into alumni_studentData std_id,maritalStatus,ageMarried,yearMarried values '" & Access & "','" & ddlStatus.SelectedValue & "','" & txtAgeMarried.Text & "',
                    '" & txtYearMarried.Text & "'"
            oCommon.ExecuteSQL(strSQL)
        End If
    End Sub
End Class