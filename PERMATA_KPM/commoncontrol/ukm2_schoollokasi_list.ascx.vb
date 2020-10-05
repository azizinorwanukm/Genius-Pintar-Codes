Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Partial Public Class ukm2_schoollokasi_list1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            strRet = BindData(datRespondent)

        End If

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Senarai sekolah tidak diperolehi."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Senarai sekolah berdasarkan Lokasi. Jumlah#" & myDataSet.Tables(0).Rows.Count
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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strSQL = getSQL()
        strRet = BindData(datRespondent)
    End Sub

    Private Function Schoolprofile_Count(ByVal strValue As String)
        strSQL = "SELECT COUNT(*) FROM StudentSchool WHERE SchoolID='" & strValue & "'"
        strRet = oCommon.getFieldValue(strSQL)
        ''--debug
        'Response.Write(strSQL)

        Return strRet
    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY b.SchoolState,b.SchoolName"

        tmpSQL = "SELECT a.SchoolID,b.SchoolCode,b.SchoolName,b.SchoolAddress,b.SchoolPostcode,b.SchoolCity,b.SchoolState,b.SchoolType,b.SchoolNoTel,b.SchoolPPD,a.SchoolLokasi FROM UKM2 a,SchoolProfile b"
        strWhere = " WITH (NOLOCK) WHERE a.SchoolID=b.SchoolID AND b.IsDELETED='N' AND a.ExamYear='" & Request.QueryString("examyear") & "'"

        Dim strSchoolLokasi As String = Request.QueryString("schoollokasi")
        strWhere += " AND a.SchoolLokasi='" & strSchoolLokasi & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try
            ''jumlah pelajar berdaftar
            'Response.Redirect("ukm1.school.students.aspx?schoolid=" & strKeyID)
            Select Case getUserProfile_UserType()
                Case "ADMIN"
                    Response.Redirect("kpm.schoolprofile.update.aspx?schoolid=" & strKeyID)
                Case Else
                    Response.Redirect("system.error.aspx?usertype=" & getUserProfile_UserType())
            End Select

        Catch ex As Exception

        End Try

    End Sub
End Class