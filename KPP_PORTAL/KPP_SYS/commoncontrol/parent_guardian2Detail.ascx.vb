Imports System.Data.SqlClient
Imports System.IO

Public Class parent_guardian2Detail
    Inherits System.Web.UI.UserControl
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                parent_state_load()
                parent_salary_load()

                Dim data_ID = oCommon.Student_securityLogin(Request.QueryString("parent_ID"))
                ''parent_info
                strSQL = "SELECT * from parent_Info 
                          WHERE parent_ID ='" & data_ID & "' and parent_info.parent_No = '2'"
                '--debug
                ''Response.Write(strSQLprnt)

                Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

                Dim dset As DataSet = New DataSet
                sqlDB.Fill(dset, "AnyTable")

                Dim Rows As Integer = 0
                Dim Count As Integer = 1
                Dim Table As DataTable = New DataTable
                Table = dset.Tables(0)
                If Table.Rows.Count > 0 Then
                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Name")) Then
                        parent_Name.Text = dset.Tables(0).Rows(0).Item("parent_Name")
                    Else
                        parent_Name.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_IC")) Then
                        parent_IC.Text = dset.Tables(0).Rows(0).Item("parent_IC")
                    Else
                        parent_IC.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Status")) Then
                        Parent_relationship.Text = dset.Tables(0).Rows(0).Item("parent_Status")
                    Else
                        Parent_relationship.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Email")) Then
                        parent_Email.Text = dset.Tables(0).Rows(0).Item("parent_Email")
                    Else
                        parent_Email.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_MobileNo")) Then
                        parent_MobileNo.Text = dset.Tables(0).Rows(0).Item("parent_MobileNo")
                    Else
                        parent_MobileNo.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_HomeAddress")) Then
                        parent_Address.Text = dset.Tables(0).Rows(0).Item("parent_HomeAddress")
                    Else
                        parent_Address.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_City")) Then
                        P2_txtCity.Text = dset.Tables(0).Rows(0).Item("parent_City")
                    Else
                        P2_txtCity.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_State")) Then
                        ddlparent_State.SelectedValue = dset.Tables(0).Rows(0).Item("parent_State")
                    Else
                        ddlparent_State.SelectedValue = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Postcode")) Then
                        parent_Postcode.Text = dset.Tables(0).Rows(0).Item("parent_Postcode")
                    Else
                        parent_Postcode.Text = ""
                    End If


                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_work")) Then
                        parent_work.Text = dset.Tables(0).Rows(0).Item("parent_work")
                    Else
                        parent_work.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Salary")) Then
                        ddlsalary.SelectedValue = dset.Tables(0).Rows(0).Item("parent_Salary")
                    Else
                        ddlsalary.SelectedValue = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_OfficeNo")) Then
                        parent_OfficeNo.Text = dset.Tables(0).Rows(0).Item("parent_OfficeNo")
                    Else
                        parent_OfficeNo.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_WorkAddress")) Then
                        parent_WorkAddress.Text = dset.Tables(0).Rows(0).Item("parent_WorkAddress")
                    Else
                        parent_WorkAddress.Text = ""
                    End If

                    If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Work_Email")) Then
                        parent_Work_Email.Text = dset.Tables(0).Rows(0).Item("parent_Work_Email")
                    Else
                        parent_Work_Email.Text = ""
                    End If

                End If
            End If
        Catch ex As Exception

        End Try
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

    Private Sub parent_salary_load()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Salary' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlsalary.DataSource = ds
            ddlsalary.DataTextField = "Parameter"
            ddlsalary.DataValueField = "Parameter"
            ddlsalary.DataBind()
            ddlsalary.Items.Insert(0, New ListItem("Select Salary", String.Empty))
            ddlsalary.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub parent_state_load()
        strSQL = "SELECT Parameter FROM setting WHERE Type='State' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlparent_State.DataSource = ds
            ddlparent_State.DataTextField = "Parameter"
            ddlparent_State.DataValueField = "Parameter"
            ddlparent_State.DataBind()
            ddlparent_State.Items.Insert(0, New ListItem("Select State", String.Empty))
            ddlparent_State.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnGuardianUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnGuardianUpdate.ServerClick

        Dim data_ID = oCommon.Student_securityLogin(Request.QueryString("parent_ID"))

        Dim errorCount As Integer = 0
        If parent_Name.Text = "" Or Not IsNumeric(parent_Name.Text) And Regex.IsMatch(parent_Name.Text, "^[A-Za-z ]+$") Then

            If parent_IC.Text = "" Or IsNumeric(parent_IC.Text) And parent_IC.Text.Length < 14 Then

                If parent_work.Text = "" Or Not IsNumeric(parent_work.Text) And Regex.IsMatch(parent_work.Text, "^[A-Za-z ]+$") Then

                    If parent_Email.Text = "" Or Regex.IsMatch(parent_Email.Text, "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$") Then

                        If IsNumeric(parent_MobileNo.Text) Or parent_MobileNo.Text = "" Then

                            If P2_txtCity.Text = "" Or P2_txtCity.Text.Length > 0 Then

                                If ddlparent_State.SelectedValue <> "0" Then

                                    If parent_Postcode.Text = "" Or IsNumeric(parent_Postcode.Text) Then

                                        'UPDATE PARENT DATA
                                        strSQL = "UPDATE parent_Info set parent_Name='" & parent_Name.Text & "',parent_IC='" & parent_IC.Text & "',parent_State='" & ddlparent_State.SelectedValue & "',
                                            parent_Email='" & parent_Email.Text & "',parent_MobileNo='" & parent_MobileNo.Text & "',parent_HomeAddress='" & parent_Address.Text & "',
                                            parent_City='" & P2_txtCity.Text & "',parent_Postcode='" & parent_Postcode.Text & "',parent_Work='" & parent_work.Text & "',
                                            parent_Salary='" & ddlsalary.SelectedValue & "',parent_WorkAddress='" & parent_WorkAddress.Text & "',
                                            parent_Work_Email='" & parent_Work_Email.Text & "',parent_OfficeNo='" & parent_OfficeNo.Text & "' 
                                            FROM parent_Info where parent_ID ='" & data_ID & "' and parent_info.parent_No = '2'"
                                        strRet = oCommon.ExecuteSQL(strSQL)

                                        If strRet = 0 Then
                                            errorCount = 20

                                            ''get ipv4 address
                                            Dim host As String = Net.Dns.GetHostName()

                                            ''get user name
                                            Dim data_parentID As String = oCommon.Student_securityLogin(Request.QueryString("parent_ID"))

                                            Dim userName As String = "Select parent_Name from parent_Info where parent_ID = '" & data_parentID & "'"
                                            Dim data_userName As String = oCommon.getFieldValue(userName)

                                            'Insert activity trail image into ActivityTrail_BtmLvl DB
                                            Using PJGDATA As New SqlCommand("INSERT into ActivityTrail_BtmLvl(Log_Date,Activity,Login_ID,User_HostAddress,Page,Name_Matters) 
                                                                                         values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','Update Student Profile Data','" & data_ID & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "',' penjaga_update_profile.aspx','" & data_userName & "')", objConn)
                                                objConn.Open()
                                                Dim k = PJGDATA.ExecuteNonQuery()
                                                objConn.Close()
                                                If k <> 0 Then
                                                    errorCount = 0
                                                Else
                                                    errorCount = 1
                                                End If
                                            End Using

                                        Else
                                            errorCount = 21
                                        End If
                                    Else
                                        errorCount = 29
                                    End If
                                Else
                                    errorCount = 28
                                End If
                            Else
                                errorCount = 27
                            End If
                        Else
                            errorCount = 26
                        End If
                    Else
                        errorCount = 25
                    End If
                Else
                    errorCount = 24
                End If
            Else
                errorCount = 23
            End If
        Else
            errorCount = 22
        End If

        If errorCount = 20 Then
            Response.Redirect("penjaga_update_profile.aspx?result=20&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 21 Then
            Response.Redirect("penjaga_update_profile.aspx?result=21&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 22 Then
            Response.Redirect("penjaga_update_profile.aspx?result=22&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 23 Then
            Response.Redirect("penjaga_update_profile.aspx?result=23&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 24 Then
            Response.Redirect("penjaga_update_profile.aspx?result=24&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 25 Then
            Response.Redirect("penjaga_update_profile.aspx?result=25&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 26 Then
            Response.Redirect("penjaga_update_profile.aspx?result=26&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 27 Then
            Response.Redirect("penjaga_update_profile.aspx?result=27&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 28 Then
            Response.Redirect("penjaga_update_profile.aspx?result=28&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 29 Then
            Response.Redirect("penjaga_update_profile.aspx?result=29&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        ElseIf errorCount = 30 Then
            Response.Redirect("penjaga_update_profile.aspx?result=30&parent_ID=" + Request.QueryString("parent_ID") + "&std_ID = " + Request.QueryString("std_ID"))
        End If
    End Sub
End Class