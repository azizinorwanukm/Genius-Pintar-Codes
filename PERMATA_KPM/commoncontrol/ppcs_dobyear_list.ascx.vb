Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class ppcs_dobyear_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Request.QueryString("dob_year") = "" Then
                    lbldob_year.Text = "0"
                Else
                    lbldob_year.Text = Request.QueryString("dob_year")
                End If

                lblPPCSDate.Text = Request.QueryString("ppcsdate")

                Dim nAge As Integer = Now.Year - CInt(lbldob_year.Text)
                lbldob_year.Text += " Umur: " & nAge.ToString & " tahun"

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod ditemui."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

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
        Dim strOrder As String = " ORDER BY b.Studentfullname"
        Dim strdob_year As String = ""

        tmpSQL = "SELECT b.StudentID,b.StudentFullname,b.MYKAD,b.DOB_Year,b.StudentRace,a.PPCSStatus FROM PPCS a,StudentProfile b WHERE a.StudentID=b.StudentID"

        If Not Request.QueryString("ppcsdate") = "ALL" Then
            strWhere += " AND a.PPCSDate='" & Request.QueryString("ppcsdate") & "'"
        End If

        If Not Request.QueryString("studentrace") = "ALL" Then
            strWhere += " AND b.StudentRace='" & Request.QueryString("studentrace") & "'"
        End If

        If Not Request.QueryString("dob_year") = "" Then
            strWhere += " AND b.DOB_Year='" & Request.QueryString("dob_year") & "'"
        Else
            strWhere += " AND b.DOB_Year IS NULL"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL


    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "SUBADMIN"
                Response.Redirect("subadmin.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "UKM"
                Response.Redirect("ukm.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "JPN"
                Response.Redirect("jpn.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "KPM"
                Response.Redirect("kpm.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "KPT"
                Response.Redirect("kpt.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "MRSM"
                Response.Redirect("mara.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "ASASI"
                Response.Redirect("asasi.studentprofile.view.aspx?studentid=" & strKeyID)
            Case Else
                lblMsg.Text = "Invalid usertype!"
        End Select
    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class