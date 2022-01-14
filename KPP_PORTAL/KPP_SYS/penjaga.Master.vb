Imports System.Data.SqlClient

Public Class Penjaga
    Inherits System.Web.UI.MasterPage

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Request.IsSecureConnection Then
                Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"))
            End If

            If Not IsPostBack Then
                Dim id As String = ""
                ''id = Session("pelajar")
                id = Session("Parent_ID")

                hiddenData.Value = id

                If id Is Nothing Then
                    Response.Redirect("default.aspx")
                Else
                    loading_Page(id)
                End If

                If Session("Student_Campus") = "PGPN" Then
                    Main_Logo_PGPN.Visible = True
                    Main_Logo_APP.Visible = False

                ElseIf Session("Student_Campus") = "APP" Then
                    Main_Logo_PGPN.Visible = False
                    Main_Logo_APP.Visible = True
                End If

                Check_STDStatus()

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Check_STDStatus()

        Dim check_Stream As String = "Select student_Stream from student_info where std_ID = '" & Session("Std_ID") & "' and student_Status = 'Access'"
        Dim get_Stream As String = oCommon.getFieldValue(check_Stream)

        Dim check_Campus As String = "Select student_Campus from student_info where std_ID = '" & Session("Std_ID") & "' and student_Status = 'Access'"
        Dim get_Campus As String = oCommon.getFieldValue(check_Campus)

        If (get_Stream = "PS" Or get_Stream = "DIP") And get_Campus = "PGPN" Then
            MenuPayment.Style.Add("display", "block")
            MenuReport.Style.Add("display", "block")
        End If
    End Sub

    Public Function getFieldValue(ByVal sql_plus As String, ByVal MyConnection As String) As String
        If sql_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sql_plus, conn)
        Dim strvalue As String = ""
        Try
            Dim ds As DataSet = New DataSet
            sqlAdapter.Fill(ds, "AnyTable")

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item(0).ToString) Then
                    strvalue = ds.Tables(0).Rows(0).Item(0).ToString
                Else
                    Return "0"
                End If
            End If
        Catch ex As Exception
            Return "0"
        Finally
            conn.Dispose()
        End Try
        Return strvalue
    End Function

    Private Sub loading_Page(ByVal id As String)
        Dim std As String = Session("Std_ID")

        Home.NavigateUrl = String.Format("penjaga_login_berjaya.aspx")
        penjagaLK.NavigateUrl = String.Format("penjaga_laporan_kehadiran.aspx?parent_ID=" + id + "&std_ID=" + std)
        penjagaRP.NavigateUrl = String.Format("penjaga_bayaran.aspx")

        strSQL = "  select student_Name from student_info
                    where student_Status = 'Access'
                    and std_ID = '" & std & "'"
        txtstudentName.Text = " [ WELCOME , &nbsp;&nbsp; " & oCommon.getFieldValue(strSQL) & " ] "

        txtcurrentDate.Text = DateTime.Now.ToString("dd/MM/yyyy")
    End Sub

    Private Sub btnLogout_ServerClick(sender As Object, e As EventArgs) Handles btnLogout.ServerClick
        Response.Redirect("default.aspx?result=90&parent_ID=" + ID)
    End Sub
End Class