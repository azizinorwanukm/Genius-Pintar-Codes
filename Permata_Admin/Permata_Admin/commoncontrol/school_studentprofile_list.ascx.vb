Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class school_studentprofile_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            strRet = BindData(datRespondent)
        End If

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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
        Dim strOrder As String = ""

        tmpSQL = "SELECT StudentProfile.StudentID,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.StudentFullname,StudentProfile.DOB_Day,StudentProfile.DOB_Month,StudentProfile.DOB_Year,StudentProfile.StudentGender,StudentProfile.StudentRace,StudentProfile.StudentReligion,StudentProfile.StudentEmail,StudentProfile.StudentForm,StudentProfile.StudentAddress1,StudentProfile.StudentAddress2,StudentProfile.StudentPostcode,StudentProfile.StudentCity,StudentProfile.StudentState,StudentProfile.StudentContactNo,ParentProfile.FamilyContactNo,ParentProfile.FatherMYKADNo,ParentProfile.FatherFullname,ParentProfile.FatherJob,ParentProfile.MotherMYKADNo,ParentProfile.MotherFullname,ParentProfile.MotherJob,SchoolProfile.SchoolCode,SchoolProfile.SchoolName FROM StudentSchool"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON StudentSchool.Studentid=StudentProfile.Studentid"
        tmpSQL += " LEFT OUTER JOIN ParentProfile ON StudentSchool.Studentid=ParentProfile.Studentid"
        tmpSQL += " LEFT OUTER JOIN SchoolProfile ON StudentSchool.SchoolID=SchoolProfile.SchoolID"
        strWhere += " WHERE StudentSchool.SchoolID='" & Request.QueryString("schoolid") & "'"
        strOrder += " ORDER BY StudentProfile.StudentFullname"

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
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.studentprofile.view.aspx?studentid=" & strKeyID)
            Case "SUBADMIN"
            Case Else
                lblMsg.Text = "Invalid User Type! " & getUserProfile_UserType()
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

End Class