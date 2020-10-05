Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class ppcs_user_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Load_Details()
            End If

        Catch ex As Exception
            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            ' oCommon.WriteLogFile(strPath, strMsg)

        End Try

    End Sub

    Private Sub Load_Details()
        strSQL = "SELECT * FROM ppcs_users WHERE myGUID='" & Request.QueryString("myguid") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("LoginID")) Then
                    lblLoginID.Text = ds.Tables(0).Rows(0).Item("LoginID")
                Else
                    lblLoginID.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pwd")) Then
                    lblPwd.Text = ds.Tables(0).Rows(0).Item("Pwd")
                Else
                    lblPwd.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Fullname")) Then
                    lblFullname.Text = ds.Tables(0).Rows(0).Item("Fullname")
                Else
                    lblFullname.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ContactNo")) Then
                    lblContactNo.Text = ds.Tables(0).Rows(0).Item("ContactNo")
                Else
                    lblContactNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ICNo")) Then
                    lblICNo.Text = ds.Tables(0).Rows(0).Item("ICNo")
                Else
                    lblICNo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Address")) Then
                    lblAddress.Text = ds.Tables(0).Rows(0).Item("Address")
                Else
                    lblAddress.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Postcode")) Then
                    lblPostcode.Text = ds.Tables(0).Rows(0).Item("Postcode")
                Else
                    lblPostcode.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("City")) Then
                    lblCity.Text = ds.Tables(0).Rows(0).Item("City")
                Else
                    lblCity.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("State")) Then
                    lblState.Text = ds.Tables(0).Rows(0).Item("State")
                Else
                    lblState.Text = ""
                End If

            End If

        Catch ex As Exception
            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub


End Class