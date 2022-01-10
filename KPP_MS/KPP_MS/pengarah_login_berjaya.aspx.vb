Imports System.Data.SqlClient

Public Class pengarah_login_berjaya
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

                Dim data As String = oCommon.Staff_securityLogin(Request.QueryString("pengarah_ID"))

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                End If

                State_list()
                LoadPage()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub State_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='State' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlState.DataSource = ds
            ddlState.DataTextField = "Parameter"
            ddlState.DataValueField = "Parameter"
            ddlState.DataBind()
            ddlState.Items.Insert(0, New ListItem("Select State", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub LoadPage()

        Dim data_ID As String = oCommon.Staff_securityLogin(Request.QueryString("pengarah_ID"))

        strSQL = "  select * from staff_info where stf_ID = '" & data_ID & "' and staff_Status = 'Access'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Name")) Then
                txtstaffName.Text = ds.Tables(0).Rows(0).Item("staff_Name")
            Else
                txtstaffName.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_ID")) Then
                txtstaffID.Text = ds.Tables(0).Rows(0).Item("staff_ID")
            Else
                txtstaffID.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Mykad")) Then
                txtstaffMykad.Text = ds.Tables(0).Rows(0).Item("staff_Mykad")
            Else
                txtstaffMykad.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Email")) Then
                txtstaffEmail.Text = ds.Tables(0).Rows(0).Item("staff_Email")
            Else
                txtstaffEmail.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Sex")) Then
                Dim staff_gender As String = ds.Tables(0).Rows(0).Item("staff_Sex")

                If staff_gender = "Male" Or staff_gender = "MALE" Then
                    rbtn_Male.Checked = True
                ElseIf staff_gender = "Female" Or staff_gender = "FEMALE" Then
                    rbtn_Female.Checked = True
                End If
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_MobileNo")) Then
                txtstaffPhone.Text = ds.Tables(0).Rows(0).Item("staff_MobileNo")
            Else
                txtstaffPhone.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Address")) Then
                txtstaffAddress.Text = ds.Tables(0).Rows(0).Item("staff_Address")
            Else
                txtstaffAddress.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_City")) Then
                txtstaffCity.Text = ds.Tables(0).Rows(0).Item("staff_City")
            Else
                txtstaffCity.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_State")) Then
                ddlState.SelectedValue = ds.Tables(0).Rows(0).Item("staff_State")
            Else
                ddlState.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Posscode")) Then
                txtstaffPostcode.Text = ds.Tables(0).Rows(0).Item("staff_Posscode")
            Else
                txtstaffPostcode.Text = ""
            End If
        End If

        ''Position 1
        strSQL = "  select staff_Access from staff_Login where stf_ID = '" & data_ID & "' and staff_Status = 'Access' and staff_PositionNO = 'Position 1'"
        Dim sqlDP1 As New SqlDataAdapter(strSQL, objConn)

        Dim dsP1 As DataSet = New DataSet
        sqlDP1.Fill(dsP1, "AnyTable")

        Dim nRowsP1 As Integer = 0
        Dim nCountP1 As Integer = 1
        Dim MyTableP1 As DataTable = New DataTable
        MyTableP1 = dsP1.Tables(0)
        If MyTableP1.Rows.Count > 0 Then
            If Not IsDBNull(dsP1.Tables(0).Rows(0).Item("staff_Access")) Then
                txtPosition1.Text = dsP1.Tables(0).Rows(0).Item("staff_Access")
            Else
                txtPosition1.Text = ""
            End If
        End If

        ''Position 2
        strSQL = "  select staff_Access from staff_Login where stf_ID = '" & data_ID & "' and staff_Status = 'Access' and staff_PositionNO = 'Position 2'"
        Dim sqlDP2 As New SqlDataAdapter(strSQL, objConn)

        Dim dsP2 As DataSet = New DataSet
        sqlDP2.Fill(dsP2, "AnyTable")

        Dim nRowsP2 As Integer = 0
        Dim nCountP2 As Integer = 1
        Dim MyTableP2 As DataTable = New DataTable
        MyTableP2 = dsP2.Tables(0)
        If MyTableP2.Rows.Count > 0 Then
            If Not IsDBNull(dsP2.Tables(0).Rows(0).Item("staff_Access")) Then
                txtPosition2.Text = dsP2.Tables(0).Rows(0).Item("staff_Access")
            Else
                txtPosition2.Text = ""
            End If
        End If

        ''Position 3
        strSQL = "  select staff_Access from staff_Login where stf_ID = '" & data_ID & "' and staff_Status = 'Access' and staff_PositionNO = 'Position 3'"
        Dim sqlDP3 As New SqlDataAdapter(strSQL, objConn)

        Dim dsP3 As DataSet = New DataSet
        sqlDP3.Fill(dsP3, "AnyTable")

        Dim nRowsP3 As Integer = 0
        Dim nCountP3 As Integer = 1
        Dim MyTableP3 As DataTable = New DataTable
        MyTableP3 = dsP3.Tables(0)
        If MyTableP3.Rows.Count > 0 Then
            If Not IsDBNull(dsP3.Tables(0).Rows(0).Item("staff_Access")) Then
                txtPosition3.Text = dsP3.Tables(0).Rows(0).Item("staff_Access")
            Else
                txtPosition3.Text = ""
            End If
        End If

    End Sub

    Private Sub btnUpdateDirectorInfo_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateDirectorInfo.ServerClick
        Dim errorCount As Integer = 0

        Dim strgender As String = ""

        If rbtn_Male.Checked = True Then
            strgender = "Male"
        End If
        If rbtn_Female.Checked = True Then
            strgender = "Female"
        End If

        Dim host As String = Net.Dns.GetHostName()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objNewConn As SqlConnection = New SqlConnection(strConn)

        If Not IsNumeric(txtstaffName.Text) And txtstaffName.Text <> "" Then

            If txtstaffMykad.Text <> "" And txtstaffMykad.Text.Length < 20 Then

                If txtstaffEmail.Text <> "" Then

                    If strgender <> "" Then

                        If txtstaffPhone.Text <> "" And Regex.IsMatch(txtstaffPhone.Text, "^[0-9]+$") Then

                            If txtstaffCity.Text = "" Or txtstaffCity.Text.Length > 0 Then

                                If ddlState.Text = "" Or Not IsNumeric(ddlState.SelectedValue) And Regex.IsMatch(ddlState.SelectedValue, "^[A-Za-z]+$") Then

                                    If txtstaffPostcode.Text = "" Or IsNumeric(txtstaffPostcode.Text) And Regex.IsMatch(txtstaffPostcode.Text, "^[0-9]+$") Then

                                        Dim imgPath As String = "~/staff_Image/user.png"

                                        'UPDATE STAFF INFO DATA
                                        strSQL = "  UPDATE staff_Info set staff_Name ='" & txtstaffName.Text & "',staff_Mykad='" & txtstaffMykad.Text & "',
                                                    staff_Sex='" & strgender & "',staff_Email='" & txtstaffEmail.Text & "',staff_MobileNo='" & txtstaffPhone.Text & "',staff_Address='" & txtstaffAddress.Text & "',
                                                    staff_City='" & txtstaffCity.Text & "',staff_State='" & ddlState.SelectedValue & "',staff_Posscode='" & txtstaffPostcode.Text & "',staff_Photo='" & imgPath & "'
                                                    WHERE stf_ID ='" & oCommon.Staff_securityLogin(Request.QueryString("pengarah_ID")) & "'"
                                        strRet = oCommon.ExecuteSQL(strSQL)

                                        Dim find_value As String = ""
                                        Dim get_value As String = ""

                                        Dim strStaffLogin As String = Split(txtstaffName.Text, " ")(0) & "@UKM"
                                        Dim insertFunction As String = ""
                                        Dim updateFunction As String = ""

                                        If strRet = "0" Then
                                            ShowMessage(" Update Staff Information ", MessageType.Success)
                                        Else
                                            ShowMessage(" Unsuccessful Update Staff Information ", MessageType.Error)
                                        End If

                                    Else
                                        ShowMessage(" Please Fill In Zip Code ", MessageType.Error)
                                    End If

                                Else
                                    ShowMessage(" Please Select State ", MessageType.Error)
                                End If
                            Else
                                ShowMessage(" Please Fill In City ", MessageType.Error)
                            End If
                        Else
                            ShowMessage(" Please Fill In Phone Mobile ", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Please Select Gender ", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Please Fill In Email Address ", MessageType.Error)
                End If
            Else
                ShowMessage(" Please FIll In Staff NRIC / MYKAD ", MessageType.Error)
            End If
        Else
            ShowMessage(" Please Fill In Staff Name ", MessageType.Error)
        End If
    End Sub


    Private Function InsertPosition(ByVal STFLOGIN As String, ByVal STFPOS As String, ByVal STFACCESS As String) As String
        Dim strStaffPasswordP3 As String = oCommon.pswrd_random()

        strSQL = "INSERT INTO staff_Login(stf_ID,staff_Login,staff_PositionNo,staff_Access,staff_Status,staff_LoginAttempt,staff_Password)
                  VALUES('" & oCommon.Staff_securityLogin(Request.QueryString("pengarah_ID")) & "','" & STFLOGIN.ToUpper & "','" & STFPOS & "','" & STFACCESS & "','Access','0', '" & strStaffPasswordP3 & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        Return strRet
    End Function

    Private Function UpdatePosition(ByVal STFPOS As String, ByVal STFACCESS As String, ByVal id_login As String) As String
        strSQL = "UPDATE staff_Login set staff_Access = '" & STFACCESS & "', staff_PositionNo = '" & STFPOS & "' where login_ID = '" & id_login & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        Return strRet
    End Function

    Private Function checkPosition(ByVal data As String, ByVal pos As String)
        strSQL = "SELECT Value from setting where Value = '" & data & "' and Type ='" & pos & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub btnUpdateDirectorPassword_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateDirectorPassword.ServerClick

        Dim getOldPassword As String = "Select staff_Password from staff_Login where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("pengarah_ID")) & "' and staff_Password = '" & txtOldPassword.Text & "'"
        Dim dataOldPassword As String = oCommon.getFieldValue(getOldPassword)

        If txtOldPassword.Text = dataOldPassword Then

            If txtNewPassword.Text <> "" And Regex.IsMatch(txtNewPassword.Text, "^[A-Za-z0-9]+$") Then

                strSQL = "Update staff_Login set staff_Password = '" & txtNewPassword.Text & "' where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("pengarah_ID")) & "' and staff_Status = 'Access' and staff_Password = '" & txtOldPassword.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Update Staff Password ", MessageType.Success)

                txtOldPassword.Text = ""
                txtNewPassword.Text = ""

            Else
                ShowMessage(" Invalid New Password", MessageType.Error)
            End If

        Else
            ShowMessage(" Invalid Old Password", MessageType.Error)
        End If
    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum

End Class