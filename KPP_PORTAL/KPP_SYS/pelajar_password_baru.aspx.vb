Imports System.Data.SqlClient

Public Class pelajar_password_baru
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then

                Dim id As String = Request.QueryString("std_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")

                ElseIf data = "TRUE" Then

                    result = Request.QueryString("result")

                    '' student detail error
                    If result = 1 Then
                        ShowMessage(" Update Password", MessageType.Success)
                    ElseIf result = -1 Then
                        ShowMessage("Error", MessageType.Error)
                    ElseIf result = 2 Then
                        ShowMessage("Please enter a valid old password", MessageType.Error)
                    Else
                    End If

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick

        Response.Redirect("pelajar_login_berjaya.aspx?std_ID=" + Request.QueryString("std_ID"))

    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick

        Dim errorCount As Integer

        Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("std_ID"))

        Dim std_Name As String = "Select student_Name from student_info where std_ID = '" & data_ID & "'"
        Dim data_stdName As String = oCommon.getFieldValue(std_Name)

        Dim userIDMYKAD As String = ""
        Dim userIDPSWD As String = ""

        Dim CheckMYKAD As String = ""
        CheckMYKAD = "select student_Mykad from student_info where student_Mykad = '" & txtloginUsername.Text & "'"
        userIDMYKAD = getFieldValue(CheckMYKAD, strConn)

        Dim CheckPSWD As String = ""
        CheckPSWD = "select student_Mykad from student_info where student_Password = '" & txtloginPassword.Text & "'"
        userIDPSWD = getFieldValue(CheckPSWD, strConn)

        If userIDMYKAD = txtloginUsername.Text And userIDPSWD = txtloginPassword.Text Then
            strSQL = "UPDATE student_info SET student_Password ='" & txtnewPassword.Text & "' WHERE std_ID ='" & data_ID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then

                ''get ipv4 address
                Dim host As String = Net.Dns.GetHostName()

                'Insert activity trail image into ActivityTrail_BtmLvl DB
                Using PJGDATA As New SqlCommand("INSERT into ActivityTrail_BtmLvl(Log_Date,Activity,Login_ID,User_HostAddress,Page,Name_Matters) 
                                                     values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','Update Password','" & data_ID & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "','pelajar_pasword_baru.aspx','" & data_stdName & "')", objConn)
                    objConn.Open()
                    Dim k = PJGDATA.ExecuteNonQuery()
                    objConn.Close()
                    If k <> 0 Then
                        errorCount = 0
                    Else
                        errorCount = 1
                    End If
                End Using

                Response.Redirect("pelajar_password_baru.aspx?std_ID=" + Request.QueryString("std_ID") + "&result=1")
            Else
                Response.Redirect("pelajar_password_baru.aspx?std_ID=" + Request.QueryString("std_ID") + "&result=-1")
            End If
        Else
            Response.Redirect("pelajar_password_baru.aspx?std_ID=" + Request.QueryString("std_ID") + "&result=2")
        End If


    End Sub

    Public Function getFieldValue(ByVal data As String, ByVal MyConnection As String) As String
        If data.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(data, conn)
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
End Class