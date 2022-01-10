Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography

Public Class lecturer_Update
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

                Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

                fillDDL("State", staff_State)
                position_Fill()

                load_Page(DATA_STAFFID)

                staff_Photo.ImageUrl = "~/staff_Image/user.png"

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub load_Page(ByVal text As String)

        strSQL = "select * from staff_Info where stf_ID = '" & text & "' and staff_Status = 'Access'"

        '--debug
        ''Response.Write(strSQLstd)

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
                staff_Name.Text = ds.Tables(0).Rows(0).Item("staff_Name")
            Else
                staff_Name.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_ID")) Then
                staff_ID.Text = ds.Tables(0).Rows(0).Item("staff_ID")
            Else
                staff_ID.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_MyKad")) Then
                staff_Mykad.Text = ds.Tables(0).Rows(0).Item("staff_MyKad")
            Else
                staff_Mykad.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Email")) Then
                staff_Email.Text = ds.Tables(0).Rows(0).Item("staff_Email")
            Else
                staff_Email.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Sex")) Then
                staff_sex.Text = ds.Tables(0).Rows(0).Item("staff_Sex")
            Else
                staff_sex.Text = ""
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
                txt_City.Text = ds.Tables(0).Rows(0).Item("staff_City")
            Else
                txt_City.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_State")) Then
                staff_State.Text = ds.Tables(0).Rows(0).Item("staff_State")
            Else
                staff_State.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Posscode")) Then
                staff_Posscode.Text = ds.Tables(0).Rows(0).Item("staff_Posscode")
            Else
                staff_Posscode.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position1")) Then
                staff_Position_P1.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Position1")
            Else
                staff_Position_P1.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position2")) Then
                staff_Position_P2.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Position2")
            Else
                staff_Position_P2.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position3")) Then
                staff_Position_P3.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Position3")
            Else
                staff_Position_P3.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Photo")) Then
                staff_Photo.ImageUrl = ds.Tables(0).Rows(0).Item("staff_Photo")
            Else
                staff_Photo.ImageUrl = "~/staff_Image/user.png"
            End If
        End If
    End Sub

    Private Function checkPosition(ByVal data As String, ByVal pos As String)

        strSQL = "SELECT Parameter from setting where Value = '" & data & "' and Type ='" & pos & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0

        Dim host As String = Net.Dns.GetHostName()
        Dim MyConnection As SqlConnection = New SqlConnection(strConn)

        Dim pos1_list As String = checkPosition(staff_Position_P1.SelectedValue, "Level Access")
        Dim pos2_list As String = checkPosition(staff_Position_P2.SelectedValue, "Level Access")
        Dim pos3_list As String = checkPosition(staff_Position_P3.SelectedValue, "Level Access")

        Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        If Not IsNumeric(staff_Name.Text) And staff_Name.Text <> "" And Regex.IsMatch(staff_Name.Text, "^[A-Za-z ]+$") Then

            If staff_Mykad.Text <> "" And staff_Mykad.Text.Length < 14 And IsNumeric(staff_Mykad.Text) Then

                If staff_ID.Text <> "" And Regex.IsMatch(staff_ID.Text, "^[A-Za-z0-9 ]+$") Then

                    If staff_Email.Text = "" Or Regex.IsMatch(staff_Email.Text, "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$") Then

                        If staff_sex.Text = "" Or Regex.IsMatch(staff_sex.Text, "^[A-Za-z ]+$") Then

                            If (staff_MobileNo.Text = "" Or IsNumeric(staff_MobileNo.Text)) And Regex.IsMatch(staff_MobileNo.Text, "^[0-9]+$") Then

                                If txt_City.Text = "" Or txt_City.Text.Length > 0 Then

                                    If staff_State.Text = "" Or Not IsNumeric(staff_State.SelectedValue) And Regex.IsMatch(staff_State.SelectedValue, "^[A-Za-z]+$") Then

                                        If staff_Posscode.Text = "" Or IsNumeric(staff_Posscode.Text) And Regex.IsMatch(staff_Posscode.Text, "^[0-9]+$") Then

                                            Dim imgPath As String = ""

                                            If uploadPhoto.PostedFile.FileName <> "" Then

                                                Dim filename As String = Path.GetFileName(uploadPhoto.PostedFile.FileName)

                                                ''sets the image path
                                                imgPath = "~/staff_Image/" + filename

                                                ''then save it to the Folder
                                                uploadPhoto.SaveAs(Server.MapPath(imgPath))

                                            Else
                                                imgPath = "~/staff_Image/user.png"

                                            End If

                                            If pos1_list <> pos2_list Or pos1_list <> pos3_list Or pos2_list <> pos3_list Then
                                                'UPDATE STUDENT INFO DATA
                                                strSQL = "UPDATE staff_Info set staff_Name ='" & staff_Name.Text & "',staff_Mykad='" & staff_Mykad.Text & "',
                                                      staff_ID='" & staff_ID.Text & "',staff_Sex='" & staff_sex.Text & "',staff_Email='" & staff_Email.Text & "',staff_MobileNo='" & staff_MobileNo.Text & "',staff_Address='" & staff_Address.Text & "',
                                                      staff_City='" & txt_City.Text & "',staff_State='" & staff_State.SelectedValue & "',staff_Posscode='" & staff_Posscode.Text & "',staff_Photo='" & imgPath & "',
                                                      staff_Position1 = '" & pos1_list & "', staff_Position2 = '" & pos2_list & "', staff_Position3 = '" & pos3_list & "' WHERE stf_ID ='" & DATA_STAFFID & "'"
                                                strRet = oCommon.ExecuteSQL(strSQL)

                                                Dim find_value As String = ""
                                                Dim get_value As String = ""
                                                Dim strStaffLogin As String = ""

                                                If Session("SchoolCampus") = "APP" Then
                                                    strStaffLogin = Split(staff_Name.Text, " ")(0) & "@APP"
                                                ElseIf Session("SchoolCampus") = "PGPN" Then
                                                    strStaffLogin = Split(staff_Name.Text, " ")(0) & "@UKM"
                                                End If

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

                                                    Using ActivityTrail As New SqlCommand("INSERT INTO ActivityTrail_Upperlvl(log_Date,Activity,Login_ID,User_HostAddress,Name_Matters,page) values ('" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "','UPDATE STAFF','" & DATA_STAFFID & "','" & Net.Dns.GetHostByName(host).AddressList(0).ToString() & "','" & staff_Name.Text & "','pengajar_kemaskini_profile.aspx')", objConn)
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
        Else
            errorCount = 2
        End If

        If errorCount = 1 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=-1&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 0 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=1&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 2 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=2&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 3 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=3&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 4 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=4&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 5 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=5&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 6 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=6&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 7 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=7&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 8 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=8&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 9 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=9&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 10 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=10&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 11 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=11&stf_ID=" + Request.QueryString("stf_ID") + "")
        ElseIf errorCount = 12 Then
            Response.Redirect("pengajar_kemaskini_profile.aspx?result=12&stf_ID=" + Request.QueryString("stf_ID") + "")
        End If
    End Sub

    Private Function InsertPosition(ByVal STFLOGIN As String, ByVal STFPSWRD As String, ByVal STFPOS As String, ByVal STFACCESS As String) As String

        strSQL = "INSERT INTO staff_Login(stf_ID,staff_Login,staff_Password,staff_PositionNo,staf_Access,staff_Status,staff_LoginAttempt)
                  VALUS('" & Request.QueryString("stf_ID") & "','" & STFLOGIN & "','" & STFPSWRD & "','" & STFPOS & "','" & STFACCESS & "','Access','0')"
        strRet = oCommon.ExecuteSQL(strSQL)

        Return strRet
    End Function

    Private Function UpdatePosition(ByVal STFPOS As String, ByVal STFACCESS As String, ByVal id_login As String) As String

        strSQL = "UPDATE staff_Login set staff_Access = '" & STFACCESS & "' and staff_PositionNo = '" & STFPOS & "' where login_ID = '" & id_login & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        Return strRet
    End Function

    Private Sub position_Fill()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = String.Format("SELECT Parameter,Value FROM setting Where Type = 'Level Access' AND Parameter IS NOT NULL order by Parameter ASC")
            Dim sqlDA As New SqlDataAdapter(query, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_Position_P1.DataSource = ds.Tables(0)
            staff_Position_P1.DataTextField = "Parameter"
            staff_Position_P1.DataValueField = "Value"
            staff_Position_P1.DataBind()
            staff_Position_P1.Items.Insert(0, New ListItem("- None -", "- None -"))

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            query = String.Format("SELECT Parameter,Value FROM setting Where Type = 'Level Access' AND Parameter IS NOT NULL order by Parameter ASC")
            Dim sqlDb As New SqlDataAdapter(query, objConn)

            Dim db As DataSet = New DataSet
            sqlDb.Fill(db, "AnyTable")

            staff_Position_P2.DataSource = db.Tables(0)
            staff_Position_P2.DataTextField = "Parameter"
            staff_Position_P2.DataValueField = "Value"
            staff_Position_P2.DataBind()
            staff_Position_P2.Items.Insert(0, New ListItem("- None -", "- None -"))

            staff_Position_P3.DataSource = db.Tables(0)
            staff_Position_P3.DataTextField = "Parameter"
            staff_Position_P3.DataValueField = "Value"
            staff_Position_P3.DataBind()
            staff_Position_P3.Items.Insert(0, New ListItem("- None -", "- None -"))

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub fillDDL(type As String, ddl As DropDownList)
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = String.Format("SELECT Parameter FROM setting Where Type = '{0}' AND Parameter IS NOT NULL", type)
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

End Class