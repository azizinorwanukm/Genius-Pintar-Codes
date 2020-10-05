Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Resources

Public Class admin_passwordUser
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Dim strconn As String = ConfigurationManager.AppSettings("ConnectionMaster")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        getData()
    End Sub

    Private Sub getData()
        Dim id As String = Request.QueryString("myGUID")
        Dim name As String = Request.QueryString("fullname")
        Dim loginid As String = Request.QueryString("login")

        Dim strSQL As String = "select top 1 A.staff_name,C.staff_Password from ukm3.dbo.staff_info A 
                                left join kolejadmin.dbo.staff_Info B on A.staff_id = B.stf_ID
                                left join kolejadmin.dbo.staff_Login C on A.staff_id = C.stf_ID
                                where B.stf_ID ='" & id & "' and B.staff_Name='" & name & "' and C.stf_ID ='" & id & "' and C.staff_Login='" & loginid & "'"
        Dim strSQL1 As String = "select top 1 A.staff_name,C.pwd from ukm3.dbo.staff_info A 
                                left join permatapintar.dbo.PPCS_Users C on A.staff_login = C.LoginID
                                where C.ppcsuserID='" & id & "' and C.Fullname='" & name & "' and C.LoginID='" & loginid & "' "

        If oCommon.isExist(strSQL) = True Then

            Dim getID As String = oCommon.getFieldValue("select top 1 A.staff_login from ukm3.dbo.staff_info A 
                                 left join kolejadmin.dbo.staff_Info B on  A.staff_id = B.stf_ID
                                left join kolejadmin.dbo.staff_Login C on A.staff_id = C.stf_ID
                                where B.staff_Name='" & name & "' and C.staff_Login='" & loginid & "' and B.stf_ID='" & id & "' and C.stf_ID ='" & id & "'")
            lblloginID.Text = getID

            Dim getpass As String = oCommon.getFieldValue("select top 1 B.staff_Password from ukm3.dbo.staff_info A 
                                left join kolejadmin.dbo.staff_Login B on A.staff_id = B.stf_ID                                
                                where B.staff_Login='" & loginid & "' and B.stf_ID='" & id & "'")
            lbluserPassword.Text = getpass

        ElseIf oCommon.isExist(strSQL1) = True Then

            Dim getID As String = oCommon.getFieldValue("select top 1 A.staff_login from ukm3.dbo.staff_info A 
                                left join permatapintar.dbo.PPCS_Users B on A.staff_login = B.LoginID
                                where B.Fullname='" & name & "' and B.LoginID='" & loginid & "' and B.ppcsuserID='" & id & "'")
            lblloginID.Text = getID
            Dim getEpass As String = oCommon.getFieldValue("select top 1 B.pwd from ukm3.dbo.staff_info A 
                                left join permatapintar.dbo.PPCS_Users B on A.staff_login = B.LoginID
                                where B.Fullname='" & name & "' and B.LoginID='" & loginid & "' and B.ppcsuserID='" & id & "' ")
            Try
                Dim getPass As String = oDes.DecryptData(getEpass)
                If IsError(getPass) = False Then
                    lbluserPassword.Text = getPass
                End If
            Catch ex As Exception
                lbluserPassword.Text = getEpass
            End Try

        End If
        
    End Sub
    Private Sub btnBack_Click() Handles btnBack.Click
        Response.Redirect("admin.petugasDilantik.aspx")
    End Sub




End Class