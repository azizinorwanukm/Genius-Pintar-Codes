Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class lecturer_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0
    Dim Data_Print As String = ""

    '' connection to kolejadmin databasse
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Dim strBil As String = ""
    Dim strStaffID As String = ""
    Dim strStaffName As String = ""
    Dim strStaffMykad As String = ""
    Dim strStaffGender As String = ""
    Dim strStaffEmail As String = ""
    Dim strStaffMobileNo As String = ""
    Dim strStaffTelNo As String = ""
    Dim strStaffLogin As String = ""
    Dim strStaffPasswordP1 As String = ""
    Dim strStaffPasswordP2 As String = ""
    Dim strStaffPasswordP3 As String = ""
    Dim strStaffYear As String = ""
    Dim strStaffPositionP1 As String = ""
    Dim strStaffPositionP2 As String = ""
    Dim strStaffPositionP3 As String = ""
    Dim strStaffAddress As String = ""
    Dim strStaffCity As String = ""
    Dim strStaffPostalCode As String = ""
    Dim strStaffState As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            previousPage.NavigateUrl = String.Format("~/admin_carian_pengajar.aspx?admin_ID=" + Request.QueryString("admin_ID"))

            Dim getStatus As String = Request.QueryString("status")

            If getStatus = "AS" Then ''Add Staff Information
                txtbreadcrum1.Text = "Register Staff Information"

                RegisterStaff.Visible = True
                ImportStaff.Visible = False

                btnRegisterStaff.Attributes("class") = "btn btn-info"
                btnImportStaff.Attributes("class") = "btn btn-default font"

                fillDDL("State", staff_State)
                accessLevel_List()
                positionLevel_List()
                adminLevel_List()

            ElseIf getStatus = "IS" Then ''Import Staff Information
                txtbreadcrum1.Text = "Import Staff Information"

                RegisterStaff.Visible = False
                ImportStaff.Visible = True

                btnRegisterStaff.Attributes("class") = "btn btn-default font"
                btnImportStaff.Attributes("class") = "btn btn-info"

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRegisterStaff_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterStaff.ServerClick
        Response.Redirect("admin_daftar_pengajar_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=AS")
    End Sub

    Private Sub btnImportStaff_ServerClick(sender As Object, e As EventArgs) Handles btnImportStaff.ServerClick
        Response.Redirect("admin_daftar_pengajar_baru.aspx?admin_ID=" + Request.QueryString("admin_ID") + "&status=IS")
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim id As Object = Request.QueryString("admin_ID")
        Dim MyConnection As SqlConnection = New SqlConnection(strConn)

        Dim P1 As String = "False"
        Dim P2 As String = "False"
        Dim P3 As String = "Fasle"

        If staff_P1_Position.SelectedIndex > 0 Then
            P1 = "True"
        End If

        If staff_P2_Position.SelectedIndex > 0 Then
            P2 = "True"
        End If

        If staff_P3_Position.SelectedIndex > 0 Then
            P3 = "True"
        End If

        Dim strgender As String = ""

        If rbtn_Male.Checked = True Then
            strgender = "Male"
        End If
        If rbtn_Female.Checked = True Then
            strgender = "Female"
        End If

        Dim checking_data As String = "SELECT stf_ID from staff_Info where staff_Status = 'Access' and staff_Mykad= '" & staff_Mykad.Text & "'"
        Dim collecting_data As String = oCommon.getFieldValue(checking_data)

        If collecting_data = "" Then

            If Not IsNumeric(staff_Name.Text) And staff_Name.Text <> "" Then

                If IsNumeric(staff_Mykad.Text) And staff_Mykad.Text <> "" And staff_Mykad.Text.Length < 14 Then

                    If staff_ID.Text <> "" Then

                        If staff_Email.Text <> "" Then

                            If staff_MobileNo.Text = "" Or IsNumeric(staff_MobileNo.Text) And Regex.IsMatch(staff_MobileNo.Text, "^[0-9]+$") Then

                                If txtCity.Text = "" Or txtCity.Text.Length > 0 Then

                                    If staff_State.Text = "" Or Not IsNumeric(staff_State.SelectedValue) And Regex.IsMatch(staff_State.SelectedValue, "^[A-Za-z]+$") Then

                                        If staff_Posscode.Text = "" Or IsNumeric(staff_Posscode.Text) And Regex.IsMatch(staff_Posscode.Text, "^[0-9]+$") Then

                                            Dim imgPath As String = "~/staff_Image/user.png"

                                            If P1 = "True" And P2 = "True" And P3 = "True" Then

                                                If staff_P1_Position.SelectedValue <> staff_P2_Position.SelectedValue And staff_P1_Position.SelectedValue <> staff_P3_Position.SelectedValue And staff_P2_Position.SelectedValue <> staff_P3_Position.SelectedValue Then

                                                    ' register staff login id and staff password save into database staff_Login for Admin
                                                    Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3) 
                                                                                        values ('" & staff_ID.Text & "','" & oCommon.FixSingleQuotes(staff_Name.Text) & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "',
                                                                                        '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "')", MyConnection)
                                                        MyConnection.Open()
                                                        Dim i = STDDATA.ExecuteNonQuery()
                                                        MyConnection.Close()
                                                        If i <> 0 Then
                                                            ShowMessage(" Add New Staff ", MessageType.Success)

                                                            capture_user_position()
                                                        Else
                                                            ShowMessage(" Unsuccessful Add New Staff ", MessageType.Error)
                                                        End If

                                                    End Using
                                                Else
                                                    ShowMessage(" Please Select Different Staff Position ", MessageType.Error)
                                                End If

                                            ElseIf P1 = "True" And P2 = "True" Then

                                                If staff_P1_Position.SelectedValue <> staff_P2_Position.SelectedValue Then

                                                    ' register staff login id and staff password save into database staff_Login for Admin
                                                    Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3) 
                                                                                        values ('" & staff_ID.Text & "','" & oCommon.FixSingleQuotes(staff_Name.Text) & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "',
                                                                                        '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "')", MyConnection)
                                                        MyConnection.Open()
                                                        Dim i = STDDATA.ExecuteNonQuery()
                                                        MyConnection.Close()
                                                        If i <> 0 Then
                                                            ShowMessage(" Add New Staff ", MessageType.Success)

                                                            capture_user_position()
                                                        Else
                                                            ShowMessage(" Unsuccessful Add New Staff ", MessageType.Error)
                                                        End If

                                                    End Using
                                                Else
                                                    ShowMessage(" Please Select Different Staff Position ", MessageType.Error)
                                                End If

                                            ElseIf P1 = "True" And P3 = "True" Then

                                                If staff_P1_Position.SelectedValue <> staff_P3_Position.SelectedValue Then

                                                    ' register staff login id and staff password save into database staff_Login for Admin
                                                    Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3) 
                                                                                        values ('" & staff_ID.Text & "','" & oCommon.FixSingleQuotes(staff_Name.Text) & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "',
                                                                                        '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "')", MyConnection)
                                                        MyConnection.Open()
                                                        Dim i = STDDATA.ExecuteNonQuery()
                                                        MyConnection.Close()
                                                        If i <> 0 Then
                                                            ShowMessage(" Add New Staff ", MessageType.Success)

                                                            capture_user_position()
                                                        Else
                                                            ShowMessage(" Unsuccessful Add New Staff ", MessageType.Error)
                                                        End If

                                                    End Using
                                                Else
                                                    ShowMessage(" Please Select Different Staff Position ", MessageType.Error)
                                                End If

                                            ElseIf P3 = "True" And P2 = "True" Then

                                                If staff_P2_Position.SelectedValue <> staff_P3_Position.SelectedValue Then

                                                    ' register staff login id and staff password save into database staff_Login for Admin
                                                    Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3) 
                                                                                        values ('" & staff_ID.Text & "','" & oCommon.FixSingleQuotes(staff_Name.Text) & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "',
                                                                                        '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "')", MyConnection)
                                                        MyConnection.Open()
                                                        Dim i = STDDATA.ExecuteNonQuery()
                                                        MyConnection.Close()
                                                        If i <> 0 Then
                                                            ShowMessage(" Add New Staff ", MessageType.Success)

                                                            capture_user_position()
                                                        Else
                                                            ShowMessage(" Unsuccessful Add New Staff ", MessageType.Error)
                                                        End If

                                                    End Using
                                                Else
                                                    ShowMessage(" Please Select Different Staff Position ", MessageType.Error)
                                                End If

                                            Else

                                                '' register staff login id and staff password save into database staff_Login for Admin
                                                Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3) 
                                                                                     values ('" & staff_ID.Text & "','" & oCommon.FixSingleQuotes(staff_Name.Text) & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "',
                                                                                     '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "')", MyConnection)
                                                    MyConnection.Open()
                                                    Dim i = STDDATA.ExecuteNonQuery()
                                                    MyConnection.Close()
                                                    If i <> 0 Then
                                                        ShowMessage(" Add New Staff ", MessageType.Success)

                                                        capture_user_position()
                                                    Else
                                                        ShowMessage(" Unsuccessful Add New Staff ", MessageType.Error)
                                                    End If
                                                End Using

                                            End If
                                        Else
                                            ShowMessage(" Please Enter The Valid Zip Code ", MessageType.Error)
                                        End If
                                    Else
                                        ShowMessage(" Please Enter The Valid State ", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage(" Please Select City ", MessageType.Error)
                                End If
                            Else
                                ShowMessage(" Please Enter The Valid Phone No ", MessageType.Error)
                            End If

                        Else
                            ShowMessage(" Please Enter The Valid Email Address ", MessageType.Error)
                        End If
                    Else
                        ShowMessage(" Please Enter The Valid Staff ID ", MessageType.Error)
                    End If
                Else
                    ShowMessage(" Please Enter The Valid Staff NRIC / MYKAD ", MessageType.Error)
                End If
            Else
                ShowMessage(" Please Enter The Valid STaff Name ", MessageType.Error)
            End If

        End If
    End Sub

    Private Sub capture_user_position()

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim staffLogin As String = Split(staff_Name.Text, " ")(0) & "@UKM"
        Dim staff_Password_P1 As String = ""
        Dim staff_Password_P2 As String = ""
        Dim staff_Password_P3 As String = ""

        Dim find_value As String = ""
        Dim get_value As String = ""

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        staff_Password_P1 = oCommon.pswrd_random()

        Dim find_stf_ID As String = "select stf_ID from staff_Info where staff_Mykad = '" & staff_Mykad.Text & "' and staff_Status = 'Access'"
        Dim get_stf_ID = oCommon.getFieldValue(find_stf_ID)

        find_value = "select Value from setting where Value = '" & staff_P1_Position.SelectedValue & "' and Type = 'Level Access'"
        get_value = oCommon.getFieldValue(find_value)

        ''insert staff for position 1
        Using STDDATA As New SqlCommand("INSERT INTO staff_Login(stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status) 
                                         values ('" & get_stf_ID & "','" & staffLogin & "','" & staff_Password_P1 & "','" & get_value & "','Position 1','Access')", objConn)
            objConn.Open()
            Dim i = STDDATA.ExecuteNonQuery()
            objConn.Close()
        End Using

        staff_Password_P2 = oCommon.pswrd_random()

        find_value = "select Value from setting where Value = '" & staff_P2_Position.SelectedValue & "' and Type = 'Level Access'"
        get_value = oCommon.getFieldValue(find_value)

        ''insert staff for position 2
        Using STDDATA As New SqlCommand("INSERT INTO staff_Login(stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status) 
                                         values ('" & get_stf_ID & "','" & staffLogin & "','" & staff_Password_P2 & "'," & get_value & "',Position 2,'Access')", objConn)
            objConn.Open()
            Dim i = STDDATA.ExecuteNonQuery()
            objConn.Close()
        End Using

        staff_Password_P3 = oCommon.pswrd_random()

        find_value = "select Value from setting where Value = '" & staff_P3_Position.SelectedValue & "' and Type = 'Level Access'"
        get_value = oCommon.getFieldValue(find_value)

        ''insert staff for position 3
        Using STDDATA As New SqlCommand("INSERT INTO staff_Login(stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status) 
                                         values ('" & get_stf_ID & "','" & staffLogin & "','" & staff_Password_P3 & "','" & get_value & "',Position 3,'Access')", objConn)
            objConn.Open()
            Dim i = STDDATA.ExecuteNonQuery()
            objConn.Close()
        End Using
    End Sub

    Private Sub accessLevel_List()
        strSQL = "select Parameter, Value from setting where Type = 'Level Access' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_P2_Position.DataSource = ds
            staff_P2_Position.DataTextField = "Parameter"
            staff_P2_Position.DataValueField = "Value"
            staff_P2_Position.DataBind()
            staff_P2_Position.Items.Insert(0, New ListItem("None", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub positionLevel_List()
        strSQL = "select Parameter, Value from setting where Type = 'Level Access' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_P3_Position.DataSource = ds
            staff_P3_Position.DataTextField = "Parameter"
            staff_P3_Position.DataValueField = "Value"
            staff_P3_Position.DataBind()
            staff_P3_Position.Items.Insert(0, New ListItem("None", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub adminLevel_List()
        strSQL = "select Parameter, Value from setting where Type = 'Level Access' order by Parameter ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_P1_Position.DataSource = ds
            staff_P1_Position.DataTextField = "Parameter"
            staff_P1_Position.DataValueField = "Value"
            staff_P1_Position.DataBind()
            staff_P1_Position.Items.Insert(0, New ListItem("None", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub fillDDL(type As String, ddl As DropDownList)
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = String.Format("SELECT Parameter FROM setting Where Type = '{0}' AND Parameter IS NOT NULL order by Parameter ASC", type)
            Dim sqlDA As New SqlDataAdapter(query, objConn)


            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl.DataSource = ds.Tables(0)
            ddl.DataTextField = "Parameter"
            ddl.DataValueField = "Parameter"
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("Select " & type, String.Empty))

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Protected Sub fillRadioList(parameter As String, rb As RadioButtonList)
        Dim Query As String = "SELECT Parameter FROM setting WHERE Type = '" & parameter & "' order by Parameter ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(Query, objConn)

        Dim ds As New DataSet
        sqlDA.Fill(ds)
        rb.DataSource = ds
        rb.DataTextField = "Parameter"
        rb.DataValueField = "Parameter"
        rb.DataBind()

        For Each item As ListItem In rb.Items
            item.Attributes.Add("class", "radio-inline")
            item.Attributes.Add("Style", "display:inline-block; margin: 0px 25px 0px 25px;")
        Next

    End Sub


    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/staff_import/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [staff$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then
                    SaveSiteData(ds)

                Else
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")
                refreshVar()
                strMsg = ""

                'Staff_ID
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_ID")) Then
                    strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_ID")
                Else
                    strMsg += " Please Enter Staff_ID |"
                End If

                'Staff_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_Name")) Then
                    strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_Name")
                Else
                    strMsg += " Please Enter Staff_Name |"
                End If

                'Staff_Mykad
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_Mykad")) Then
                    strStaffMykad = SiteData.Tables(0).Rows(i).Item("Staff_Mykad")
                Else
                    strMsg += " Please Enter Staff_Mykad |"
                End If

                'Staff_Year
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_Year")) Then
                    strStaffGender = SiteData.Tables(0).Rows(i).Item("Staff_Year")
                Else
                    strMsg += " Please Enter Staff_Year |"
                End If

                If strMsg.Length = 0 Then

                Else
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message & "Here 2"
        End Try

    End Function

    Private Function SaveSiteData(ByVal SiteData As DataSet) As String

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim countInsert As Integer = 0
        Dim countUpdate As Integer = 0

        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")

            strBil = SiteData.Tables(0).Rows(i).Item("Bil")
            strStaffID = SiteData.Tables(0).Rows(i).Item("Staff_ID")
            strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_Name")
            strStaffMykad = SiteData.Tables(0).Rows(i).Item("Staff_Mykad")
            strStaffGender = SiteData.Tables(0).Rows(i).Item("Staff_Gender")
            strStaffEmail = SiteData.Tables(0).Rows(i).Item("Staff_Email")
            strStaffMobileNo = SiteData.Tables(0).Rows(i).Item("Staff_MobileNo")
            strStaffTelNo = SiteData.Tables(0).Rows(i).Item("Staff_TelNo")

            strStaffLogin = Split(strStaffName, " ")(0) & "@UKM"

            strStaffPositionP1 = SiteData.Tables(0).Rows(i).Item("Staff_Position1")
            strStaffPositionP2 = SiteData.Tables(0).Rows(i).Item("Staff_Position2")
            strStaffPositionP3 = SiteData.Tables(0).Rows(i).Item("Staff_Position3")

            strStaffAddress = SiteData.Tables(0).Rows(i).Item("Staff_Address")
            strStaffCity = SiteData.Tables(0).Rows(i).Item("Staff_City")
            strStaffState = SiteData.Tables(0).Rows(i).Item("Staff_State")
            strStaffPostalCode = SiteData.Tables(0).Rows(i).Item("Staff_PostalCode")

            Dim staffAccess_data_pos1 As String = ""
            Dim staffAccess_data_pos2 As String = ""
            Dim staffAccess_data_pos3 As String = ""

            If strStaffPositionP1 = "ADMIN" Then
                strStaffPasswordP1 = oCommon.pswrd_random()

                strSQL = "select Parameter from setting where Value = '" & strStaffPositionP1 & "' and Type = 'Admin Access'"
                staffAccess_data_pos1 = oCommon.getFieldValue(strSQL)
            Else
                strStaffPositionP1 = ""
            End If

            If strStaffPositionP2 = "KOKURIKULUM" Or strStaffPositionP2 = "PPE" Or strStaffPositionP2 = "HEA" Or strStaffPositionP2 = "HEP" Or strStaffPositionP2 = "KSLR" Or strStaffPositionP2 = "SUP" Or strStaffPositionP2 = "SUD" Or strStaffPositionP2 = "PENGARAH" Or strStaffPositionP2 = "TIMBALAN PENGARAH" Then
                strStaffPasswordP2 = oCommon.pswrd_random()
                staffAccess_data_pos2 = oCommon.getFieldValue(strSQL)
            Else
                strStaffPositionP2 = ""
            End If

            If strStaffPositionP3 = "INSTRUKTOR KPP" Or strStaffPositionP3 = "INSTRUKTOR KPP - SEMENTARA" Or strStaffPositionP3 = "PENSYARAH" Then
                strStaffPasswordP3 = oCommon.pswrd_random()
                staffAccess_data_pos3 = oCommon.getFieldValue(strSQL)
            Else
                strStaffPositionP3 = ""
            End If

            strSQL = "SELECT stf_ID FROM staff_Info WHERE staff_Mykad = '" & strStaffMykad & "' AND staff_Status = 'Access'"
            If oCommon.isExist(strSQL) = True Then
                'UPDATE STAFF INFO
                strSQL = "SELECT stf_ID FROM staff_Info WHERE staff_Mykad = '" & strStaffMykad & "' AND staff_Status = 'Access'"
                Dim id As String = oCommon.getFieldValue(strSQL)

                If id.Length > 0 Then

                    strSQL = "  UPDATE staff_Info SET
                                    staff_ID = '" & strStaffID & "',
                                    staff_Name = UPPER('" & strStaffName & "'),
                                    staff_Email = '" & strStaffEmail & "',
                                    staff_Sex = UPPER('" & strStaffGender & "'),
                                    staff_MobileNo = '" & oCommon.FixSingleQuotes(strStaffMobileNo) & "',
                                    staff_TelNo = '" & oCommon.FixSingleQuotes(strStaffTelNo) & "',
                                    staff_Address = UPPER('" & oCommon.FixSingleQuotes(strStaffAddress) & "'),
                                    staff_City = UPPER('" & strStaffCity & "'),
                                    staff_Posscode = '" & strStaffPostalCode & "',
                                    staff_Position1 = '" & staffAccess_data_pos1 & "',
                                    staff_Position2 = '" & staffAccess_data_pos2 & "',
                                    staff_Position3 = '" & staffAccess_data_pos3 & "',
                                    staff_State = UPPER('" & strStaffState & "')"

                    strSQL += " WHERE stf_ID = '" & id & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)


                    If strRet = 0 Then
                        errorData = 0
                        countUpdate = countUpdate + 1
                    Else
                        errorData = 1
                    End If

                End If

            Else
                'INSERT NEW STAFF
                strSQL = "  INSERT INTO staff_Info(staff_ID, staff_Name, staff_Mykad, staff_Email, staff_Sex, staff_MobileNo, staff_TelNo,
                            staff_Address, staff_City, staff_Posscode, staff_State, staff_Photo, staff_Status, staff_LoginAttempt,staff_Position1,staff_Position2,staff_Postion3)"

                strSQL += " VALUES 
                                ('" & strStaffID & "', 
                                UPPER('" & oCommon.FixSingleQuotes(strStaffName) & "'), 
                                '" & strStaffMykad & "', 
                                '" & oCommon.FixSingleQuotes(strStaffEmail) & "',
                                UPPER('" & strStaffGender & "'),
                                '" & oCommon.FixSingleQuotes(strStaffMobileNo) & "', 
                                '" & oCommon.FixSingleQuotes(strStaffTelNo) & "',
                                UPPER('" & oCommon.FixSingleQuotes(strStaffAddress) & "'), 
                                UPPER('" & strStaffCity & "'), 
                                UPPER('" & strStaffPostalCode & "'), 
                                UPPER('" & strStaffState & "'), 
                                '~/staff_Image/user.png', 
                                'Access',  
                                '0','" & staffAccess_data_pos1 & "','" & staffAccess_data_pos2 & "','" & staffAccess_data_pos3 & "')"

                strRet = oCommon.ExecuteSQL(strSQL)

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''INSERT STAFF LOGIN

                Dim find_stfID As String = "select stf_ID from staff_info where staff_Mykad = '" & strStaffMykad & "' and staff_Status = 'Access'"
                Dim get_sftfID As String = oCommon.getFieldValue(find_stfID)

                If strStaffPositionP1 <> "" Then
                    strSQL = "INSERT INTO staff_Login (stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status,staff_LoginAttempt)
                              VALUES ('" & get_sftfID & "','" & strStaffLogin & "','" & strStaffPasswordP1 & "','- None -','Position 1','Access','0')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

                If strStaffPositionP2 <> "" Then
                    strSQL = "INSERT INTO staff_Login (stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status,staff_LoginAttempt)
                              VALUES ('" & get_sftfID & "','" & strStaffLogin & "','" & strStaffPasswordP2 & "','- None -','Position 2','Access','0')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

                If strStaffPositionP3 <> "" Then
                    strSQL = "INSERT INTO staff_Login (stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status,staff_LoginAttempt)
                              VALUES ('" & get_sftfID & "','" & strStaffLogin & "','" & strStaffPasswordP3 & "','- None -','Position 3','Access','0')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

                If strRet = 0 Then
                    errorData = 0
                    countInsert = countInsert + 1
                Else
                    errorData = 1
                End If

            End If
        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsert & " rows inserted and " & countUpdate & " rows already exist in database", MessageType.Success)
            value = True

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Error)
            value = False

        End If

        Return value

    End Function

    Private Sub refreshVar()

        strBil = ""
        strStaffID = ""
        strStaffName = ""
        strStaffMykad = ""
        strStaffGender = ""
        strStaffEmail = ""
        strStaffMobileNo = ""
        strStaffTelNo = ""
        strStaffLogin = ""
        strStaffPasswordP1 = ""
        strStaffPasswordP2 = ""
        strStaffPasswordP3 = ""
        strStaffYear = ""
        strStaffPositionP1 = ""
        strStaffPositionP2 = ""
        strStaffPositionP3 = ""
        strStaffAddress = ""
        strStaffCity = ""
        strStaffPostalCode = ""
        strStaffState = ""

    End Sub

    Private Sub BtnUploaded_ServerClick(sender As Object, e As EventArgs) Handles BtnUploaded.ServerClick
        Try
            '--upload excel
            If ImportExcel() = True Then
            Else
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick
        Response.Redirect("download/staff_info.xlsx")
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class