Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class userprofile_view
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strSchoolID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lnkDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod pelajar tersebut?');")

        If Not IsPostBack Then
            studentprofile_view()

        End If

    End Sub

    Private Sub studentprofile_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""

        tmpSQL = "select pcis_user.fullname,pcis_user.icno,pcis_user.[address],phoneno,email,mothername,motheroccupation,fathername,fatheroccupation,pcis_user.learningcentrename,pcis_user.learningcentreaddress,[state].statename as learningcentrestatename,learningcentrephoneno,pcis_user.assistantname,pcis_user.assistantphoneno,pcis_exam.test_start,pcis_exam.test_end,pcis_exam.lastpage from pcis_user"
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
                If Not IsDBNull(MyTable.Rows(nRows).Item("learningcentrestatename")) Then
                    lbllearningcentrestatename.Text = MyTable.Rows(nRows).Item("learningcentrestatename").ToString
                Else
                    lbllearningcentrestatename.Text = ""
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

    Protected Sub lnkEdit_Click(sender As Object, e As EventArgs) Handles lnkEdit.Click
        Response.Redirect("admin.studentprofile.update.aspx?id=" & Request.QueryString("id"))

    End Sub

    Protected Sub lnkDelete_Click(sender As Object, e As EventArgs) Handles lnkDelete.Click
        strSQL = "DELETE pcis_user WHERE id='" & Request.QueryString("id") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "BERJAYA menghapuskan rekod pelajar"
        Else
            lblMsg.Text = "GAGAL! " & strRet
        End If

    End Sub
End Class