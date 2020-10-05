Imports System.Data.SqlClient

Public Class userprofile_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            state_list()
            studentprofile_view()

        End If

    End Sub

    Private Sub state_list()
        strSQL = "SELECT * FROM State ORDER BY statename"
        '--debug
        'Response.Write(strSQL)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddllearningcentrestatename.DataSource = ds
            ddllearningcentrestatename.DataTextField = "statename"
            ddllearningcentrestatename.DataValueField = "stateid"
            ddllearningcentrestatename.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub studentprofile_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        tmpSQL = "select pcis_user.fullname,pcis_user.icno,pcis_user.[address],phoneno,email,mothername,motheroccupation,fathername,fatheroccupation,pcis_user.learningcentrename,pcis_user.learningcentreaddress,[state].statename as learningcentrestatename,learningcentrestateid,learningcentrephoneno,pcis_user.assistantname,pcis_user.assistantphoneno,pcis_exam.test_start,pcis_exam.test_end,pcis_exam.lastpage from pcis_user"
        tmpSQL += " left join pcis_exam on pcis_exam.userid = pcis_user.id"
        tmpSQL += " left join [state] on [state].stateid = pcis_user.learningcentrestateid"
        strWhere += " WHERE pcis_user.id='" & oCommon.FixSingleQuotes(Request.QueryString("id")) & "'"
        strSQL = tmpSQL + strWhere

        '--debug
        'Response.Write(strSQL)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                ''--parent info
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("icno")) Then
                    lblicno.Text = ds.Tables(0).Rows(0).Item("icno")
                Else
                    lblicno.Text = ""
                End If
                lblicno_bak.Text = lblicno.Text

                If Not IsDBNull(MyTable.Rows(nRows).Item("fullname")) Then
                    lblfullname.Text = MyTable.Rows(nRows).Item("fullname").ToString
                Else
                    lblfullname.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("address")) Then
                    lbladdress.Text = MyTable.Rows(nRows).Item("address").ToString
                Else
                    lbladdress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("phoneno")) Then
                    lblphoneno.Text = MyTable.Rows(nRows).Item("phoneno").ToString
                Else
                    lblphoneno.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("email")) Then
                    lblemail.Text = MyTable.Rows(nRows).Item("email").ToString
                Else
                    lblemail.Text = ""
                End If

                ''--family info
                If Not IsDBNull(MyTable.Rows(nRows).Item("mothername")) Then
                    lblmothername.Text = MyTable.Rows(nRows).Item("mothername").ToString
                Else
                    lblmothername.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("motheroccupation")) Then
                    lblmotheroccupation.Text = MyTable.Rows(nRows).Item("motheroccupation").ToString
                Else
                    lblmotheroccupation.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("fathername")) Then
                    lblfathername.Text = MyTable.Rows(nRows).Item("fathername").ToString
                Else
                    lblfathername.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("fatheroccupation")) Then
                    lblfatheroccupation.Text = MyTable.Rows(nRows).Item("fatheroccupation").ToString
                Else
                    lblfatheroccupation.Text = ""
                End If

                '--PAPN info
                If Not IsDBNull(MyTable.Rows(nRows).Item("learningcentrename")) Then
                    lbllearningcentrename.Text = MyTable.Rows(nRows).Item("learningcentrename").ToString
                Else
                    lbllearningcentrename.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("learningcentreaddress")) Then
                    lbllearningcentreaddress.Text = MyTable.Rows(nRows).Item("learningcentreaddress").ToString
                Else
                    lbllearningcentreaddress.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("learningcentrestateid")) Then
                    ddllearningcentrestatename.SelectedValue = MyTable.Rows(nRows).Item("learningcentrestateid").ToString
                Else
                    ddllearningcentrestatename.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("learningcentrephoneno")) Then
                    lbllearningcentrephoneno.Text = MyTable.Rows(nRows).Item("learningcentrephoneno").ToString
                Else
                    lbllearningcentrephoneno.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("assistantname")) Then
                    lblassistantname.Text = MyTable.Rows(nRows).Item("assistantname").ToString
                Else
                    lblassistantname.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("assistantphoneno")) Then
                    lblassistantphoneno.Text = MyTable.Rows(nRows).Item("assistantphoneno").ToString
                Else
                    lblassistantphoneno.Text = ""
                End If

            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""
        If studentprofile_update() = True Then
        End If

    End Sub

    Private Function studentprofile_update() As Boolean
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        '--insert into UserProfile
        If ValidatePage() = False Then
            Return False
        End If

        ''--update user
        tmpSQL = "UPDATE pcis_user SET icno='" & oCommon.FixSingleQuotes(lblicno.Text.ToUpper) & "',fullname='" & oCommon.FixSingleQuotes(lblfullname.Text.ToUpper) & "',email='" & oCommon.FixSingleQuotes(lblemail.Text) & "',address='" & oCommon.FixSingleQuotes(lbladdress.Text.ToUpper) & "',phoneno='" & oCommon.FixSingleQuotes(lblphoneno.Text.ToUpper) & "',learningcentrename='" & oCommon.FixSingleQuotes(lbllearningcentrename.Text.ToUpper) & "',mothername='" & oCommon.FixSingleQuotes(lblmothername.Text.ToUpper) & "',motheroccupation='" & oCommon.FixSingleQuotes(lblmotheroccupation.Text.ToUpper) & "',fathername='" & oCommon.FixSingleQuotes(lblfathername.Text.ToUpper) & "',fatheroccupation='" & oCommon.FixSingleQuotes(lblfatheroccupation.Text.ToUpper) & "',learningcentreaddress='" & oCommon.FixSingleQuotes(lbllearningcentreaddress.Text.ToUpper) & "',learningcentrephoneno='" & oCommon.FixSingleQuotes(lbllearningcentrephoneno.Text.ToUpper) & "',assistantname='" & oCommon.FixSingleQuotes(lblassistantname.Text.ToUpper) & "',assistantphoneno='" & oCommon.FixSingleQuotes(lblassistantphoneno.Text.ToUpper) & "',learningcentrestateid='" & ddllearningcentrestatename.SelectedValue & "'"
        strWhere = " WHERE id='" & Request.QueryString("id") & "'"
        strSQL = tmpSQL & strWhere

        strRet = oCommon.ExecuteSQL(strSQL)
        ''--debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA mengemaskini maklumat pelajar."
            Return True
        Else
            lblMsg.Text += "GAGAL! " & strRet
            Return False
        End If
    End Function

    Private Function ValidatePage() As Boolean
        If lblicno.Text.Length = 0 Then
            lblMsg.Text = "Masukkan maklumat medan ini. MYKID#"
            lblicno.Focus()
            Return False
        End If

        If lblfullname.Text.Length = 0 Then
            lblMsg.Text = "Masukkan maklumat medan ini. Nama Penuh"
            lblfullname.Focus()
            Return False
        End If
        Return True

    End Function

    Protected Sub lnkView_Click(sender As Object, e As EventArgs) Handles lnkView.Click
        Response.Redirect("admin.studentprofile.view.aspx?id=" & Request.QueryString("id"))

    End Sub
End Class