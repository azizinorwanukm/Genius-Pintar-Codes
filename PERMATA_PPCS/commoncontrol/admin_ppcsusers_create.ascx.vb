Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class admin_ppcsusers_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer
    Dim strDomainName As String = ConfigurationManager.AppSettings("DomainName")
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblUserType.Text = Request.QueryString("usertype")
            lblUserType01.Text = lblUserType.Text

            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub ppcsdate_list()
        '--base on usertype. admin only allow all
        strSQL = oCommon.PPCSDate_Query(Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value))

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

            'ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Try
            Dim strmyGUID As String = oCommon.getGUID

            'check form validation. if failed exit
            If ValidateForm() = False Then
                Exit Sub
            End If

            '--insert into PPCS_Users
            strSQL = "INSERT INTO PPCS_Users (myGUID,LoginID,Pwd,Fullname,ICNo,ContactNo,Address,Postcode,City,State,Country,isAllow) VALUES('" & strmyGUID & "','" & oCommon.FixSingleQuotes(txtLoginID.Text) & "','" & oDes.EncryptData(oCommon.FixSingleQuotes(txtPwd.Text)) & "','" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtICNo.Text) & "','" & oCommon.FixSingleQuotes(txtContactNo.Text) & "','" & oCommon.FixSingleQuotes(txtAddress.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtPostcode.Text) & "','" & oCommon.FixSingleQuotes(txtCity.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtState.Text.ToUpper) & "','MALAYSIA','Y')"
            strRet = oCommon.ExecuteSQL(strSQL)

            '--insert into PPCS_Users_Year
            strSQL = "INSERT INTO PPCS_Users_Year(myGUID,Usertype,PPCSDate) VALUES('" & strmyGUID & "','" & lblUserType.Text.ToUpper & "','" & ddlPPCSDate.Text & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

            '--refresh list
            strRet = BindData(datRespondent)

        Catch ex As Exception

        End Try
    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean
        strSQL = "SELECT LoginID FROM ppcs_users WHERE LoginID='" & txtLoginID.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Login ID/Email already exist. Please use different Login ID."
            Exit Function
        End If

        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "Email perlu di isi."
            txtLoginID.Focus()
            Return False
        End If

        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nama."
            txtFullname.Focus()
            Return False
        End If

        If txtICNo.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nombor IC."
            txtICNo.Focus()
            Return False
        End If

        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "Masukkan password."
            txtPwd.Focus()
            Return False
        End If

        If Not txtPwd.Text = txtPwdVerify.Text Then
            lblMsg.Text = "Password dimasukkan tidak sama."
            txtPwd.Focus()
            Return False

        End If

        If txtContactNo.Text.Length = 0 Then
            lblMsg.Text = "Masukkan nombor telefon."
            txtContactNo.Focus()
            Return False
        End If

        If txtAddress.Text.Length = 0 Then
            lblMsg.Text = "Masukkan alamat terkini."
            txtAddress.Focus()
            Return False
        End If

        Return True
    End Function


    Protected Sub lnkppcsuserlist_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkppcsuserlist.Click
        Response.Redirect("ppcs.user.list.aspx")

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
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY a.Fullname ASC"

        tmpSQL = "SELECT a.myGUID,a.Fullname,a.ContactNo,a.LoginID,a.Pwd,a.IsAllow,b.UserType,b.PPCSDate FROM PPCS_Users a,PPCS_Users_Year b"
        strWhere = " WITH (NOLOCK) WHERE a.myGUID=b.myGUID AND b.UserType<>'ADMIN' AND b.Usertype='" & Request.QueryString("usertype") & "'"
        strWhere += " AND b.PPCSDate='" & ddlPPCSDate.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Redirect("ppcs.users.update.aspx?myguid=" & strKeyID)

    End Sub

End Class