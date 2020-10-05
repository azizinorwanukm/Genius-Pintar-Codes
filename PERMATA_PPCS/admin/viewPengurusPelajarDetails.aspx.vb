Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class viewPengurusPelajarDetails
    Inherits System.Web.UI.Page
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strCourseCode As String
    Dim strIC As String
    Dim strloginid As String
    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strloginid = Request.QueryString("loginid")
            If Not IsPostBack Then
                '--GET DETAILS
                strSQL = "SELECT * FROM ukm2_course WHERE loginid='" & strloginid & "'"
                strRet = oCommon.getFieldValue(strSQL)
                '--GET USER DETAILS
                Load_tutorDetails(strloginid)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)

        End Try
    End Sub

    Private Sub Load_tutorDetails(ByVal strloginid As String)
        strSQL = "SELECT * FROM ukm2_login WHERE loginid='" & strloginid & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("loginid")) Then
                    txtemail.Text = ds.Tables(0).Rows(0).Item("loginid")
                Else
                    txtemail.Text = "NA"
                End If

                'If Not IsDBNull(ds.Tables(0).Rows(0).Item("pwd")) Then
                '    txtpwd.Text = ds.Tables(0).Rows(0).Item("pwd")
                'Else
                '    txtpwd.Text = "NA"
                'End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("fullname")) Then
                    txtfullname.Text = ds.Tables(0).Rows(0).Item("fullname")
                Else
                    txtfullname.Text = "NA"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("address")) Then
                    txtaddress.Text = ds.Tables(0).Rows(0).Item("address")
                Else
                    txtaddress.Text = "NA"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("contactno")) Then
                    txtcontactno.Text = ds.Tables(0).Rows(0).Item("contactno")
                Else
                    txtcontactno.Text = "NA"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("ICnumber")) Then
                    txtIC.Text = ds.Tables(0).Rows(0).Item("ICnumber")
                Else
                    txtIC.Text = "NA"
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            'insert in to user, not active
            strSQL = "UPDATE ukm2_login WITH (UPDLOCK) SET loginid='" & txtemail.Text & "',fullname='" & txtfullname.Text & "',ICnumber='" & txtIC.Text & "',contactno='" & txtcontactno.Text & "',address='" & txtaddress.Text & "' WHERE loginid='" & strloginid & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "error:" & strRet

            Else
                lblMsg.Text = "Berjaya dikemaskini."
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub

    Protected Sub btndelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btndelete.Click
        Try
            'DELETE PENGURUS
            strSQL = "DELETE FROM ukm2_login WHERE ICnumber='" & txtIC.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            lblMsg.Text = "Berjaya dihapuskan."
            btndelete.Enabled = False
        Catch ex As Exception
            lblMsg.Text = ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
        End Try
    End Sub
End Class