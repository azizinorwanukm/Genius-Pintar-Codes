Imports System.Data.SqlClient

Public Class pelajar_CloseLogout
    Inherits System.Web.UI.Page

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oCommon As New Commonfunction

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        logout_activity(Request.QueryString("std_ID"))

    End Sub

    Private Sub logout_activity(ByVal text As String)

        Dim accessID As String = "select MAX(security_ID) from student_SecurityID where loginID_Number = '" & text & "'"
        Dim accessData As String = oCommon.getFieldValue(accessID)

        Dim get_userID As String = "select std_ID from student_SecurityID where security_ID = '" & accessData & "'"
        Dim data_userID As String = oCommon.getFieldValue(get_userID)

        Dim search_Data As String = "select student_Mykad from student_info where std_ID = '" & data_userID & "'"
        Dim find_getData As String = oCommon.getFieldValue(search_Data)

        Session.Abandon()
        Session.RemoveAll()
        Session.Clear()
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1))
        Response.Cache.SetNoStore()
        HttpContext.Current.Session.Abandon()

        ''get ipv4 address
        Dim host As String = Net.Dns.GetHostName()

        Using LoginData As New SqlCommand("INSERT into security_LoginTrail(Log_Date,Activity,Login_ID,User_HostAddress) 
                                           values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','LOGOUT','" & find_getData & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "')", objConn)
            objConn.Open()
            Dim i = LoginData.ExecuteNonQuery()
            objConn.Close()
        End Using

        Response.Redirect("default.aspx")

    End Sub

End Class