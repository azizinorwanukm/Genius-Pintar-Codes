Imports System.Data.SqlClient

Public Class UKM2_Trail_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                '--load examyear
                examyear_list(ddlExamYearUKM2)
                ddlExamYearUKM2.Text = oCommon.getAppsettings("DefaultExamYear")

                txtDateCreated.Text = oCommon.getToday
                lblDateCreated.Text = oCommon.getToday
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub examyear_list(ByVal ddlExamYear As DropDownList)
        strSQL = "SELECT ExamYear FROM master_examyear WITH (NOLOCK) ORDER BY ExamYear"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlExamYear.DataSource = ds
            ddlExamYear.DataTextField = "ExamYear"
            ddlExamYear.DataValueField = "ExamYear"
            ddlExamYear.DataBind()

            'ddlExamYear.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
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

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY UKM2TrailID DESC"

        tmpSQL = "SELECT StudentProfile.MYKAD,StudentProfile.StudentFullname,PusatUjian.PusatName,PusatUjian.PusatCity,UKM2_Trail.UKM2TrailID,UKM2_Trail.Browser,UKM2_Trail.ExamStart,UKM2_Trail.ExamYear,UKM2_Trail.HostAddress,UKM2_Trail.HostName FROM UKM2_Trail,StudentProfile,UKM2,PusatUjian"
        strWhere = " WHERE UKM2TrailID IS NOT NULL"
        strWhere += " AND UKM2_Trail.StudentID=StudentProfile.StudentID"
        strWhere += " AND UKM2_Trail.StudentID=UKM2.StudentID AND UKM2.ExamYear='" & ddlExamYearUKM2.Text & "'"
        strWhere += " AND UKM2.PusatCode=PusatUjian.PusatCode"

        If Not txtDateCreated.Text.Length = 0 Then
            strWhere += " AND UKM2_Trail.ExamStart LIKE '%" & oCommon.FixSingleQuotes(txtDateCreated.Text) & "%'"        'DDMMYYYY
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("ukm2.TransactionLog.view.aspx?transactionID=" & strKeyID)
            Case "SUBADMIN"
            Case Else
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

End Class