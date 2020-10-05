Imports System.Data.SqlClient

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillRadioList("Gender", staff_Sex)
            fillDDL("State", staff_State)
            accessLevel_List()
            positionLevel_List()
            adminLevel_List()

        Catch ex As Exception

        End Try
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

        Dim checking_data As String = "SELECT stf_ID from staff_Info where staff_Status = 'Access' and staff_Mykad= '" & staff_Mykad.Text & "'"
        Dim collecting_data As String = oCommon.getFieldValue(checking_data)

        If collecting_data = "" Then

            If Not IsNumeric(staff_Name.Text) And staff_Name.Text <> "" And Regex.IsMatch(staff_Name.Text, "^[A-Za-z ]+$") Then

                If IsNumeric(staff_Mykad.Text) And staff_Mykad.Text <> "" And staff_Mykad.Text.Length < 14 Then

                    If staff_ID.Text <> "" And Regex.IsMatch(staff_ID.Text, "^[A-Za-z0-9 ]+$") Then

                        If staff_Email.Text = "" Or Regex.IsMatch(staff_Email.Text, "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$") Then

                            If staff_Sex.Text = "" Or Not IsNumeric(staff_Sex.SelectedValue) And Regex.IsMatch(staff_Sex.SelectedValue, "^[A-Za-z]+$") Then

                                If staff_MobileNo.Text = "" Or IsNumeric(staff_MobileNo.Text) And Regex.IsMatch(staff_MobileNo.Text, "^[0-9]+$") Then

                                    If txtCity.Text = "" Or txtCity.Text.Length > 0 Then

                                        If staff_State.Text = "" Or Not IsNumeric(staff_State.SelectedValue) And Regex.IsMatch(staff_State.SelectedValue, "^[A-Za-z]+$") Then

                                            If staff_Posscode.Text = "" Or IsNumeric(staff_Posscode.Text) And Regex.IsMatch(staff_Posscode.Text, "^[0-9]+$") Then

                                                Dim imgPath As String = "~/staff_Image/user.png"

                                                If P1 = "True" And P2 = "True" And P3 = "True" Then

                                                    If staff_P1_Position.SelectedValue <> staff_P2_Position.SelectedValue And staff_P1_Position.SelectedValue <> staff_P3_Position.SelectedValue And staff_P2_Position.SelectedValue <> staff_P3_Position.SelectedValue Then

                                                        ' register staff login id and staff password save into database staff_Login for Admin
                                                        Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3) 
                                                                                        values ('" & staff_ID.Text & "','" & staff_Name.Text & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "',
                                                                                        '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "')", MyConnection)
                                                            MyConnection.Open()
                                                            Dim i = STDDATA.ExecuteNonQuery()
                                                            MyConnection.Close()
                                                            If i <> 0 Then
                                                                errorCount = 0

                                                                capture_user_position()
                                                            Else
                                                                errorCount = 1
                                                            End If

                                                        End Using
                                                    Else
                                                        errorCount = 12 ''have similar position selected
                                                    End If

                                                ElseIf P1 = "True" And P2 = "True" Then

                                                    If staff_P1_Position.SelectedValue <> staff_P2_Position.SelectedValue Then

                                                        ' register staff login id and staff password save into database staff_Login for Admin
                                                        Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3) 
                                                                                        values ('" & staff_ID.Text & "','" & staff_Name.Text & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "',
                                                                                        '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "')", MyConnection)
                                                            MyConnection.Open()
                                                            Dim i = STDDATA.ExecuteNonQuery()
                                                            MyConnection.Close()
                                                            If i <> 0 Then
                                                                errorCount = 0

                                                                capture_user_position()
                                                            Else
                                                                errorCount = 1
                                                            End If

                                                        End Using
                                                    Else
                                                        errorCount = 12 ''have similar position selected
                                                    End If

                                                ElseIf P1 = "True" And P3 = "True" Then

                                                    If staff_P1_Position.SelectedValue <> staff_P3_Position.SelectedValue Then

                                                        ' register staff login id and staff password save into database staff_Login for Admin
                                                        Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3) 
                                                                                        values ('" & staff_ID.Text & "','" & staff_Name.Text & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "',
                                                                                        '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "')", MyConnection)
                                                            MyConnection.Open()
                                                            Dim i = STDDATA.ExecuteNonQuery()
                                                            MyConnection.Close()
                                                            If i <> 0 Then
                                                                errorCount = 0

                                                                capture_user_position()
                                                            Else
                                                                errorCount = 1
                                                            End If

                                                        End Using
                                                    Else
                                                        errorCount = 12 ''have similar position selected
                                                    End If

                                                ElseIf P3 = "True" And P2 = "True" Then

                                                    If staff_P2_Position.SelectedValue <> staff_P3_Position.SelectedValue Then

                                                        ' register staff login id and staff password save into database staff_Login for Admin
                                                        Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3) 
                                                                                        values ('" & staff_ID.Text & "','" & staff_Name.Text & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "',
                                                                                        '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "')", MyConnection)
                                                            MyConnection.Open()
                                                            Dim i = STDDATA.ExecuteNonQuery()
                                                            MyConnection.Close()
                                                            If i <> 0 Then
                                                                errorCount = 0

                                                                capture_user_position()
                                                            Else
                                                                errorCount = 1
                                                            End If

                                                        End Using
                                                    Else
                                                        errorCount = 12 ''have similar position selected
                                                    End If

                                                Else

                                                    '' register staff login id and staff password save into database staff_Login for Admin
                                                    Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo,staff_Status,staff_Position1,staff_Position2,staff_Position3) 
                                                                                     values ('" & staff_ID.Text & "','" & staff_Name.Text & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "',
                                                                                     '" & staff_Address.Text & "','" & txtCity.Text & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "','Access','" & staff_P1_Position.SelectedValue & "','" & staff_P2_Position.SelectedValue & "','" & staff_P3_Position.SelectedValue & "')", MyConnection)
                                                        MyConnection.Open()
                                                        Dim i = STDDATA.ExecuteNonQuery()
                                                        MyConnection.Close()
                                                        If i <> 0 Then
                                                            errorCount = 0

                                                            capture_user_position()
                                                        Else
                                                            errorCount = 1
                                                        End If
                                                    End Using

                                                End If
                                            Else
                                                errorCount = 10
                                            End If
                                        Else
                                            errorCount = 9
                                        End If
                                    Else
                                        errorCount = 8
                                    End If
                                Else
                                    errorCount = 7
                                End If
                            Else
                                errorCount = 6
                            End If
                        Else
                            errorCount = 5
                        End If
                    Else
                        errorCount = 4
                    End If
                Else
                    errorCount = 3
                End If
            Else
                errorCount = 2
            End If

        End If

        If errorCount = 1 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 0 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 3 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 4 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 5 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 6 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 7 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=7&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 8 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=8&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 9 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=9&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 10 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=10&admin_ID=" + Request.QueryString("admin_ID") + "")
        ElseIf errorCount = 11 Then
            Response.Redirect("admin_daftar_pengajar_baru.aspx?result=11&admin_ID=" + Request.QueryString("admin_ID") + "")
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

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
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
            staff_P2_Position.Items.Insert(0, New ListItem("- None -", "- None -"))

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
            staff_P3_Position.Items.Insert(0, New ListItem("- None -", "- None -"))

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
            staff_P1_Position.Items.Insert(0, New ListItem("- None -", "- None -"))

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
            ddl.Items.Insert(0, New ListItem("Select " & type & "...", String.Empty))

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

End Class