Imports System.Data.SqlClient
Imports System.IO

Public Class lecturer_Detail
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fillStateDDL()
                staff_State.SelectedIndex = 0
                fillPositionP1DDL()
                fillPositionP2DDL()
                fillPositionP3DDL()

                LoadPage()

            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadPage()
        strSQL = "select * from staff_info where staff_info.stf_ID = '" & Request.QueryString("stf_ID") & "'"
        '--debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim STAFFPHOTO As New SqlDataAdapter()

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Name")) Then
                staff_Name.Text = ds.Tables(0).Rows(0).Item("staff_Name")
            Else
                staff_Name.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_ID")) Then
                staff_ID.Text = ds.Tables(0).Rows(0).Item("staff_ID")
            Else
                staff_ID.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Mykad")) Then
                staff_MyKad.Text = ds.Tables(0).Rows(0).Item("staff_Mykad")
            Else
                staff_MyKad.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Email")) Then
                staff_Email.Text = ds.Tables(0).Rows(0).Item("staff_Email")
            Else
                staff_Email.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Sex")) Then
                staff_Sex.Text = ds.Tables(0).Rows(0).Item("staff_Sex")
            Else
                staff_Sex.Text = "0"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_MobileNo")) Then
                staff_MobileNo.Text = ds.Tables(0).Rows(0).Item("staff_MobileNo")
            Else
                staff_MobileNo.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Address")) Then
                staff_Address.Text = ds.Tables(0).Rows(0).Item("staff_Address")
            Else
                staff_Address.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_City")) Then
                txtCity.Text = ds.Tables(0).Rows(0).Item("staff_City")
            Else
                txtCity.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_State")) Then
                staff_State.SelectedValue = ds.Tables(0).Rows(0).Item("staff_State")
            Else
                staff_State.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position1")) Then
                staff_Position_P1.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Position1")
            Else
                staff_Position_P1.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position2")) Then
                staff_Position_P2.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Position2")
            Else
                staff_Position_P2.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position3")) Then
                staff_Position_P3.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Position3")
            Else
                staff_Position_P3.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Posscode")) Then
                staff_Posscode.Text = ds.Tables(0).Rows(0).Item("staff_Posscode")
            Else
                staff_Posscode.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Photo")) Then
                staff_Photo.ImageUrl = ds.Tables(0).Rows(0).Item("staff_Photo")
            Else
                staff_Photo.ImageUrl = "~/staff_Image/user.png"
            End If

        End If
    End Sub

    Private Function checkPosition(ByVal data As String, ByVal pos As String)

        strSQL = "SELECT Value from setting where Value = '" & data & "' and Type ='" & pos & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub btnLecturerUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnLecturerUpdate.ServerClick
        Dim errorCount As Integer = 0

        Dim host As String = Net.Dns.GetHostName()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objNewConn As SqlConnection = New SqlConnection(strConn)

        Dim pos1_list As String = checkPosition(staff_Position_P1.SelectedValue, "Level Access")
        Dim pos2_list As String = checkPosition(staff_Position_P2.SelectedValue, "Level Access")
        Dim pos3_list As String = checkPosition(staff_Position_P3.SelectedValue, "Level Access")

        Dim str_user_position As String = CType(Session.Item("user_position"), String)

        If Not IsNumeric(staff_Name.Text) And staff_Name.Text <> "" And Regex.IsMatch(staff_Name.Text, "^[A-Za-z ]+$") Then

            If staff_MyKad.Text <> "" And staff_MyKad.Text.Length < 20 Then

                If staff_ID.Text <> "" And Regex.IsMatch(staff_ID.Text, "^[A-Za-z0-9 ]+$") Then

                    If staff_Email.Text = "" Or Regex.IsMatch(staff_Email.Text, "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$") Then

                        If staff_Sex.Text = "" Or Not IsNumeric(staff_Sex.Text) And Regex.IsMatch(staff_Sex.Text, "^[A-Za-z]+$") Then

                            If staff_MobileNo.Text = "" Or IsNumeric(staff_MobileNo.Text) And Regex.IsMatch(staff_MobileNo.Text, "^[0-9]+$") Then

                                If txtCity.Text = "" Or txtCity.Text.Length > 0 Then

                                    If staff_State.Text = "" Or Not IsNumeric(staff_State.SelectedValue) And Regex.IsMatch(staff_State.SelectedValue, "^[A-Za-z]+$") Then

                                        If staff_Posscode.Text = "" Or IsNumeric(staff_Posscode.Text) And Regex.IsMatch(staff_Posscode.Text, "^[0-9]+$") Then

                                            Dim imgPath As String = "~/staff_Image/user.png"

                                            If uploadPhoto.PostedFile.FileName <> "" Then

                                                Dim filename As String = Path.GetFileName(uploadPhoto.PostedFile.FileName)

                                                ''sets the image path
                                                imgPath = "~/staff_Image/" + filename

                                                ''then save it to the Folder
                                                uploadPhoto.SaveAs(Server.MapPath(imgPath))
                                            End If

                                            'If pos1_list <> pos2_list Or pos1_list <> pos3_list Or pos2_list <> pos3_list Then
                                            'UPDATE STUDENT INFO DATA
                                            strSQL = "UPDATE staff_Info set staff_Name ='" & staff_Name.Text & "',staff_Mykad='" & staff_MyKad.Text & "',
                                                      staff_ID='" & staff_ID.Text & "',staff_Sex='" & staff_Sex.Text & "',staff_Email='" & staff_Email.Text & "',staff_MobileNo='" & staff_MobileNo.Text & "',staff_Address='" & staff_Address.Text & "',
                                                      staff_City='" & txtCity.Text & "',staff_State='" & staff_State.SelectedValue & "',staff_Posscode='" & staff_Posscode.Text & "',staff_Photo='" & imgPath & "',
                                                      staff_Position1 = '" & pos1_list & "', staff_Position2 = '" & pos2_list & "', staff_Position3 = '" & pos3_list & "' WHERE stf_ID ='" & Request.QueryString("stf_ID") & "'"
                                            strRet = oCommon.ExecuteSQL(strSQL)

                                            Dim find_value As String = ""
                                            Dim get_value As String = ""

                                            Dim strStaffLogin As String = Split(staff_Name.Text, " ")(0) & "@UKM"
                                            Dim insertFunction As String = ""
                                            Dim updateFunction As String = ""

                                            Dim strStaffPasswordP1 As String = oCommon.pswrd_random()

                                            strSQL = "select login_ID from staff_Login where stf_ID = '" & Request.QueryString("stf_ID") & "' and staff_PositionNo = 'Position 1'"
                                            strRet = oCommon.getFieldValue(strSQL)

                                            find_value = "select Value from setting where Value = '" & staff_Position_P1.SelectedValue & "' and Type = 'Level Access'"
                                            get_value = oCommon.getFieldValue(find_value)

                                            If strRet = "" Then
                                                insertFunction = InsertPosition(strStaffLogin, strStaffPasswordP1, "Position 1", get_value)
                                            Else
                                                updateFunction = UpdatePosition("Position 1", get_value, strRet)
                                            End If


                                            Dim strStaffPasswordP2 As String = oCommon.pswrd_random()

                                            strSQL = "select login_ID from staff_Login where stf_ID = '" & Request.QueryString("stf_ID") & "' and staff_PositionNo = 'Position 2'"
                                            strRet = oCommon.getFieldValue(strSQL)

                                            find_value = "select Value from setting where Value = '" & staff_Position_P2.SelectedValue & "' and Type = 'Level Access'"
                                            get_value = oCommon.getFieldValue(find_value)

                                            If strRet = "" Then
                                                insertFunction = InsertPosition(strStaffLogin, strStaffPasswordP2, "Position 2", get_value)
                                            Else
                                                updateFunction = UpdatePosition("Position 2", get_value, strRet)
                                            End If

                                            Dim strStaffPasswordP3 As String = oCommon.pswrd_random()

                                            strSQL = "select login_ID from staff_Login where stf_ID = '" & Request.QueryString("stf_ID") & "' and staff_PositionNo = 'Position 3'"
                                            strRet = oCommon.getFieldValue(strSQL)

                                            find_value = "select Value from setting where Value = '" & staff_Position_P3.SelectedValue & "' and Type = 'Level Access'"
                                            get_value = oCommon.getFieldValue(find_value)

                                            If strRet = "" Then
                                                insertFunction = InsertPosition(strStaffLogin, strStaffPasswordP3, "Position 3", get_value)
                                            Else
                                                updateFunction = UpdatePosition("Position 3", get_value, strRet)
                                            End If

                                            If strRet = "0" Then
                                                errorCount = 0

                                                Using ActivityTrail As New SqlCommand("INSERT INTO ActivityTrail_Upperlvl(log_Date,Activity,Login_ID,User_HostAddress,Name_Matters,page) values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','UPDATE STAFF','" & Request.QueryString("stf_ID") & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "','" & staff_Name.Text & "','pengajar_kemaskini_profile.aspx')", objConn)
                                                    objConn.Open()
                                                    Dim j = ActivityTrail.ExecuteNonQuery()
                                                    objConn.Close()
                                                End Using
                                            Else
                                                errorCount = 1
                                            End If
                                        Else
                                            errorCount = 12
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

        If errorCount = 1 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 0 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 3 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 4 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 5 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 6 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 7 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=7&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 8 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=8&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 9 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=9&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 10 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=10&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 11 Then
            Response.Redirect("admin_edit_pengajar_data.aspx?result=11&admin_ID=" + Request.QueryString("admin_ID") + "&stf_ID=" + Request.QueryString("stf_ID") + "")
        End If
    End Sub

    Private Function InsertPosition(ByVal STFLOGIN As String, ByVal STFPSWRD As String, ByVal STFPOS As String, ByVal STFACCESS As String) As String

        strSQL = "INSERT INTO staff_Login(stf_ID,staff_Login,staff_Password,staff_PositionNo,staff_Access,staff_Status,staff_LoginAttempt)
                  VALUES('" & Request.QueryString("stf_ID") & "','" & STFLOGIN & "','" & STFPSWRD & "','" & STFPOS & "','" & STFACCESS & "','Access','0')"
        strRet = oCommon.ExecuteSQL(strSQL)

        Return strRet
    End Function

    Private Function UpdatePosition(ByVal STFPOS As String, ByVal STFACCESS As String, ByVal id_login As String) As String

        Dim new_pswrd = oCommon.pswrd_random()

        strSQL = "UPDATE staff_Login set staff_Access = '" & STFACCESS & "', staff_PositionNo = '" & STFPOS & "', staff_Password = '" & new_pswrd & "' where login_ID = '" & id_login & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        Return strRet
    End Function

    Protected Sub fillRadioList(parameter As String, rb As RadioButtonList)
        Dim Query As String = "SELECT Parameter FROM setting WHERE Type = '" & parameter & "'"

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

    Protected Sub fillStateDDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT Parameter FROM setting Where Type = 'State' AND Parameter IS NOT NULL order by Parameter ASC"
            Dim sqlDA As New SqlDataAdapter(query, objConn)


            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            staff_State.DataSource = ds.Tables(0)
            staff_State.DataTextField = "Parameter"
            staff_State.DataValueField = "Parameter"
            staff_State.DataBind()
            staff_State.Items.Insert(0, New ListItem("Select State...", String.Empty))

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub fillPositionP2DDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT Parameter, Value FROM setting Where Type = 'Level Access' AND Parameter IS NOT NULL order by Parameter ASC"
            Dim sqlDA As New SqlDataAdapter(query, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)
            staff_Position_P2.DataSource = ds.Tables(0)
            staff_Position_P2.DataTextField = "Parameter"
            staff_Position_P2.DataValueField = "Value"
            staff_Position_P2.DataBind()
            staff_Position_P2.Items.Insert(0, New ListItem("- None -", "- None -"))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub fillPositionP1DDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT Parameter, Value FROM setting Where Type = 'Level Access' AND Parameter IS NOT NULL order by Parameter ASC"
            Dim sqlDA As New SqlDataAdapter(query, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)
            staff_Position_P1.DataSource = ds.Tables(0)
            staff_Position_P1.DataTextField = "Parameter"
            staff_Position_P1.DataValueField = "Value"
            staff_Position_P1.DataBind()
            staff_Position_P1.Items.Insert(0, New ListItem("- None -", "- None -"))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub fillPositionP3DDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT Parameter, Value FROM setting Where Type = 'Level Access' AND Parameter IS NOT NULL order by Parameter ASC"
            Dim sqlDA As New SqlDataAdapter(query, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)
            staff_Position_P3.DataSource = ds.Tables(0)
            staff_Position_P3.DataTextField = "Parameter"
            staff_Position_P3.DataValueField = "Value"
            staff_Position_P3.DataBind()
            staff_Position_P3.Items.Insert(0, New ListItem("- None -", "- None -"))

        Catch ex As Exception
        End Try
    End Sub

End Class