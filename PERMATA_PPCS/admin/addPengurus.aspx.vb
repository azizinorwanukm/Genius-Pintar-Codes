Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class addPengurus
    Inherits System.Web.UI.Page

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
                ClearScreen()
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

    Private Sub ClearScreen()
        lblMsg.Text = ""
        txtfullname.Text = ""
        txtaddress.Text = ""
        txtcontactno.Text = ""
        txtemail.Text = ""
        txtIC.Text = ""
        txtpwd.Text = ""

        lblMsg.Text = ""

        lblTotal.Text = ""

        strSQL = "SELECT * from ukm2_login WHERE usertype='PENGURUS PELAJAR' ORDER BY userid desc"
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            lblTotal.Text = myDataSet.Tables(0).Rows.Count
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Record not found!" & ex.Message

            '--write to file
            Dim strPagename As String = System.IO.Path.GetFileName(Request.ServerVariables("SCRIPT_NAME"))
            Dim strMsg As String = Now.ToString & ":" & strPagename & ":loadprofile:" & Request.UserHostAddress & ":" & ex.Message
            Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            oCommon.WriteLogFile(strPath, strMsg)
            Return False
        End Try

        Return True
    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        ClearScreen()

        nPageno = e.NewPageIndex + 1


    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        '--Response.Write(strKeyID)
        Response.Redirect("viewPengurusPelajarDetails.aspx?loginid=" & strKeyID)

    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            Dim strdatecreated As String = Now.ToString("yyyyMMdd HH:mm:ss.fff")
            Dim strusername As String

            strusername = Session("username")

            'insert in to user, not active
            strSQL = "INSERT INTO ukm2_login(loginid,pwd,fullname,ICnumber,contactno,usertype,address) VALUES ('" & txtemail.Text & "','" & txtpwd.Text & "','" & txtfullname.Text & "','" & txtIC.Text & "','" & txtcontactno.Text & "','PENGURUS PELAJAR','" & txtaddress.Text & "')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "error:" & strRet

            Else
                lblMsg.Text = "Berjaya."
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

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtemail.Text.Length = 0 Then
            lblMsg.Text = "Email perlu di isi."
            txtemail.Focus()
            Return False
            Exit Function
        End If

        If txtpwd.Text.Length = 0 Then
            lblMsg.Text = "Masukkan password."
            txtpwd.Focus()
            Return False
            Exit Function
        End If

        If txtfullname.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nama."
            txtfullname.Focus()
            Return False
            Exit Function
        End If

        If txtaddress.Text.Length = 0 Then
            lblMsg.Text = "Masukkan alamat terkini."
            txtaddress.Focus()
            Return False
            Exit Function
        End If

        If txtcontactno.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nombor telefon."
            txtcontactno.Focus()
            Return False
            Exit Function
        End If

        If txtIC.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nombor IC."
            txtIC.Focus()
            Return False
            Exit Function
        End If

        Return True
    End Function

End Class