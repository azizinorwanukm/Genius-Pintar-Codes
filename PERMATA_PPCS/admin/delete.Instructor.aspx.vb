Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class delete_Instructor
    Inherits System.Web.UI.Page
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer

    Dim strICnumber As String
    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strUsername As String

        Try
            Dim cmd As SqlCommand
            Dim dr As SqlDataReader

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim con As SqlConnection = New SqlConnection(strConn)

            'GET COURSE NAME
            cmd = New SqlCommand("select courseName from ukm2_course", con)
            con.Open()

            dr = cmd.ExecuteReader()
            While dr.Read()

                If dr(0).ToString().Length > 0 Then

                    txtcourse.Items.Add(dr(0).ToString())
                    ' Response.Write(dr(0).ToString())
                End If
            End While

            con.Close()

            'GET COURSE CODE
            cmd = New SqlCommand("select courseCode from ukm2_course", con)
            con.Open()

            dr = cmd.ExecuteReader()
            While dr.Read()

                If dr(0).ToString().Length > 0 Then

                    txtcourseCode.Items.Add(dr(0).ToString())
                    ' Response.Write(dr(0).ToString())
                End If
            End While

            con.Close()

            strICnumber = Request.QueryString("ICnumber")
            If Not IsPostBack Then
                '--get Username
                strSQL = "SELECT loginid FROM ukm2_login WHERE ICnumber='" & strICnumber & "'"
                strRet = oCommon.getFieldValue(strSQL)
                strUsername = strRet
                '--get all user account info
                Load_profile(strUsername)

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

    Private Sub Load_profile(ByVal strUsername As String)
        strSQL = "SELECT * FROM ukm2_login WHERE loginid='" & strUsername & "'"
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

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("courseName")) Then
                    txtcourse.Text = ds.Tables(0).Rows(0).Item("courseName")
                Else
                    txtcourse.Text = "NA"
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("courseCode")) Then
                    txtcourseCode.Text = ds.Tables(0).Rows(0).Item("courseCode")
                Else
                    txtcourseCode.Text = "NA"
                End If

            End If

            'GET TA and RA
            Load_TARA(txtcourseCode.Text)

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

    Private Sub Load_TARA(ByVal code As String)
        Dim cmd As SqlCommand
        Dim dr As SqlDataReader

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As SqlConnection = New SqlConnection(strConn)

        'GET TA FULLNAME
        cmd = New SqlCommand("select fullname from ukm2_login WHERE usertype='PENGAJAR (TA)' and courseCode='" & code & "'", con)
        con.Open()

        dr = cmd.ExecuteReader()
        While dr.Read()

            If dr(0).ToString().Length > 0 Then

                txtTA.Items.Add(dr(0).ToString())
                ' Response.Write(dr(0).ToString())
            End If
        End While

        con.Close()

        'GET RA FULLNAME
        cmd = New SqlCommand("select fullname from ukm2_login WHERE usertype='PENGAJAR (RA)' AND courseCode='" & code & "'", con)
        con.Open()

        dr = cmd.ExecuteReader()
        While dr.Read()

            If dr(0).ToString().Length > 0 Then

                txtRA.Items.Add(dr(0).ToString())
                ' Response.Write(dr(0).ToString())
            End If
        End While

        con.Close()
    End Sub

    Protected Sub btndelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btndelete.Click
        Try
            Dim strusername As String

            strusername = Session("username")


            strSQL = "DELETE FROM ukm2_login WHERE ICnumber='" & strICnumber & "'"
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