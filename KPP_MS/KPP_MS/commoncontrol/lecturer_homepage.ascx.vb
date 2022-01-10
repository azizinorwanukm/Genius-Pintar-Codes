Imports System.Data.SqlClient
Imports System.IO

Public Class lecturer_homepage
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

                Dim getStatus As String = Request.QueryString("status")

                If getStatus = "SI" Then ''staff information
                    txtbreadcrum1.Text = "Staff Information"
                    StaffInformation.Visible = True
                    CourseInformation.Visible = False

                    btnStaffInfo.Attributes("class") = "btn btn-info"
                    btnCourseInfo.Attributes("class") = "btn btn-default font"

                    Row_P2.Visible = False
                    Row_P3.Visible = False

                    State_list()
                    campus_List()
                    StaffLoadPage()

                ElseIf getStatus = "CI" Then ''course information
                    txtbreadcrum1.Text = "Course Information"
                    StaffInformation.Visible = False
                    CourseInformation.Visible = True

                    btnStaffInfo.Attributes("class") = "btn btn-default font"
                    btnCourseInfo.Attributes("class") = "btn btn-info"

                    course_year_list()
                    course_sem_list()

                    strRet = BindData(datRespondent)
                End If

                If Session("SchoolCampus") = "APP" Then
                    txtLoginID_Status.Text = "@APP"
                ElseIf Session("SchoolCampus") = "PGPN" Then
                    txtLoginID_Status.Text = "@UKM"
                End If

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

    Private Sub campus_List()

        If Session("SchoolCampus") = "APP" Then
            strSQL = "select UPPER(Parameter) Parameter, Value from setting where Type = 'Pusat Campus' and Value  = 'APP' order by Parameter ASC"
        Else
            strSQL = "select UPPER(Parameter) Parameter, Value from setting where Type = 'Pusat Campus' order by Parameter ASC"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlstaff_Campus.DataSource = ds
            ddlstaff_Campus.DataTextField = "Parameter"
            ddlstaff_Campus.DataValueField = "Value"
            ddlstaff_Campus.DataBind()
            ddlstaff_Campus.Items.Insert(0, New ListItem("Select Campus", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub StaffLoadPage()

        Dim data_ID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        strSQL = "  select * from staff_info where stf_ID = '" & data_ID & "' and staff_Status = 'Access'"

        strSQL = "  select distinct UPPER(staff_Name) staff_Name, UPPER(staff_info.staff_ID) staff_ID, UPPER(staff_Mykad) staff_Mykad, UPPER(staff_Email) staff_Email, UPPER(staff_Sex) staff_Sex, staff_MobileNo, UPPER(staff_Address) staff_Address, UPPER(staff_City) staff_City, 
                    staff_State, Value, staff_Position1, staff_Position2, staff_Position3, staff_Posscode, staff_Photo
                    from staff_info left join setting on staff_info.staff_Campus = setting.Value where staff_info.stf_ID = '" & data_ID & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_ID")) Then
                txtstaffID.Text = ds.Tables(0).Rows(0).Item("staff_ID")
            Else
                txtstaffID.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Name")) Then
                txtstaffName.Text = ds.Tables(0).Rows(0).Item("staff_Name")
            Else
                txtstaffName.Text = ""
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

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("value")) Then
                ddlstaff_Campus.SelectedValue = ds.Tables(0).Rows(0).Item("value")
            Else
                ddlstaff_Campus.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position1")) Then
                txtPosition1.Text = ds.Tables(0).Rows(0).Item("staff_Position1")
            Else
                txtPosition1.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position2")) Then
                Row_P2.Visible = True
                txtPosition2.Text = ds.Tables(0).Rows(0).Item("staff_Position2")
            Else
                Row_P2.Visible = False
                txtPosition2.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position3")) Then
                Row_P3.Visible = True
                txtPosition3.Text = ds.Tables(0).Rows(0).Item("staff_Position3")
            Else
                Row_P3.Visible = False
                txtPosition3.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Photo")) Then
                staff_Photo.ImageUrl = ds.Tables(0).Rows(0).Item("staff_Photo")

            Else
                staff_Photo.ImageUrl = "~/staff_Image/user.png"
            End If
        End If

        strSQL = "select SUBSTRING (staff_Login,0,PATINDEX('%@%',staff_Login)) from staff_Login where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        txtLoginID.Text = strRet

    End Sub

    Private Sub btnUpdateStaffInfo_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateStaffInfo.ServerClick

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

            If txtstaffID.Text <> "" Then

                If txtstaffMykad.Text <> "" And txtstaffMykad.Text.Length < 20 Then

                    If txtstaffEmail.Text <> "" Then

                        If strgender <> "" Then

                            If txtstaffAddress.Text <> "" Then

                                If txtstaffPhone.Text <> "" And Regex.IsMatch(txtstaffPhone.Text, "^[0-9]+$") Then

                                    If txtstaffCity.Text <> "" Or txtstaffCity.Text.Length > 0 Then

                                        If ddlState.SelectedIndex > 0 Then

                                            If ddlstaff_Campus.SelectedIndex > 0 Then

                                                If txtstaffPostcode.Text <> "" And Regex.IsMatch(txtstaffPostcode.Text, "^[0-9]+$") Then

                                                    If txtLoginID.Text <> "" And Regex.IsMatch(txtLoginID.Text, "^[A-Za-z0-9]+$") Then

                                                        Dim imgPath As String = "~/staff_Image/user.png"

                                                        If uploadPhoto.PostedFile.FileName <> "" Then

                                                            Dim filename As String = Path.GetFileName(uploadPhoto.PostedFile.FileName)

                                                            ''sets the image path
                                                            imgPath = "~/staff_Image/" + filename

                                                            ''then save it to the Folder
                                                            uploadPhoto.SaveAs(Server.MapPath(imgPath))
                                                        End If

                                                        'UPDATE STAFF INFO DATA
                                                        strSQL = "UPDATE staff_Info set staff_Name = UPPER('" & txtstaffName.Text & "'), staff_Mykad = '" & txtstaffMykad.Text & "',
                                                                      staff_ID = UPPER('" & txtstaffID.Text & "'), staff_Sex = UPPER('" & strgender & "'), staff_Email = UPPER('" & txtstaffEmail.Text & "'), staff_MobileNo = '" & txtstaffPhone.Text & "', staff_Address = UPPER('" & txtstaffAddress.Text & "'),
                                                                      staff_City = UPPER('" & txtstaffCity.Text & "'), staff_State = '" & ddlState.SelectedValue & "', staff_Posscode = '" & txtstaffPostcode.Text & "', staff_Photo = '" & imgPath & "', staff_Campus = '" & ddlstaff_Campus.SelectedValue & "' 
                                                                      WHERE stf_ID ='" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
                                                        strRet = oCommon.ExecuteSQL(strSQL)

                                                        Dim find_value As String = ""
                                                        Dim get_value As String = ""
                                                        Dim strStaffLogin As String = ""

                                                        If Session("SchoolCampus") = "APP" Then
                                                            strStaffLogin = txtLoginID.Text & "@APP"
                                                        ElseIf Session("SchoolCampus") = "PGPN" Then
                                                             strStaffLogin  = txtLoginID.Text & "@UKM"
                                                        End If

                                                        strSQL = "UPDATE staff_Login set staff_Login = '" & strStaffLogin & "' where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
                                                        strRet = oCommon.ExecuteSQL(strSQL)

                                                        If strRet = "0" Then
                                                            ShowMessage(" Update Staff Information ", MessageType.Success)
                                                        Else
                                                            ShowMessage(" Unsuccessful Update Staff Information ", MessageType.Error)
                                                        End If

                                                    Else
                                                        ShowMessage(" Please Fill In Login ID Correctly [A-Z,a-z,0-9] ", MessageType.Error)
                                                    End If
                                                Else
                                                    ShowMessage(" Please Fill In Zip Code ", MessageType.Error)
                                                End If
                                            Else
                                                ShowMessage(" Please Select Institutions ", MessageType.Error)
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
                                ShowMessage(" Please Fill In Home Address ", MessageType.Error)
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
                ShowMessage(" Please Fill In Staff ID", MessageType.Error)
            End If
        Else
            ShowMessage(" Please Fill In Staff Name ", MessageType.Error)
        End If
    End Sub

    Private Sub btnUpdateStaffPassword_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateStaffPassword.ServerClick

        Dim getOldPassword As String = "Select staff_Password from staff_Login where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and staff_Password = '" & txtOldPassword.Text & "'"
        Dim dataOldPassword As String = oCommon.getFieldValue(getOldPassword)

        If txtOldPassword.Text = dataOldPassword Then

            If txtNewPassword.Text <> "" And Regex.IsMatch(txtNewPassword.Text, "^[A-Za-z0-9]+$") Then

                strSQL = "Update staff_Login set staff_Password = '" & txtNewPassword.Text & "' where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and staff_Status = 'Access' and staff_Password = '" & txtOldPassword.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Update Staff Password ", MessageType.Success)

                txtOldPassword.Text = ""
                txtNewPassword.Text = ""

            Else
                ShowMessage(" Invalid New Password, Please Fill In New Password Correctly [A-Z,a-z,0-9] ", MessageType.Error)
            End If

        Else
            ShowMessage(" Invalid Old Password", MessageType.Error)
        End If
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Private Sub btnStaffInfo_ServerClick(sender As Object, e As EventArgs) Handles btnStaffInfo.ServerClick
        Response.Redirect("pengajar_login_berjaya.aspx?stf_ID=" + Request.QueryString("stf_ID") + "&status=SI")
    End Sub

    Private Sub btnCourseInfo_ServerClick(sender As Object, e As EventArgs) Handles btnCourseInfo.ServerClick
        Response.Redirect("pengajar_login_berjaya.aspx?stf_ID=" + Request.QueryString("stf_ID") + "&status=CI")
    End Sub

    Private Sub course_year_list()
        Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

        strSQL = "select distinct lecturer_year from lecturer where stf_ID = '" & DATA_STAFFID & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "lecturer_year"
            ddlYear.DataValueField = "lecturer_year"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub course_sem_list()
        strSQL = "select * From setting where Type = 'Sem'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester.DataSource = ds
            ddlSemester.DataTextField = "Parameter"
            ddlSemester.DataValueField = "Value"
            ddlSemester.DataBind()
            ddlSemester.Items.Insert(0, New ListItem("Select Semester", String.Empty))
            ddlSemester.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY class_info.class_Level, class_info.class_Name ASC"

        tmpSQL = "  Select lecturer.ID, subject_info.subject_Name, subject_info.subject_code, class_info.class_Name, subject_info.subject_StudentYear, subject_info.subject_sem, lecturer.lecturer_year, subject_info.course_Program
                    From lecturer left join subject_info on lecturer.subject_ID = subject_info.subject_ID 
                    left join class_info on lecturer.class_ID = class_info.class_ID"
        strWhere = " WHERE lecturer.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and subject_info.subject_Campus = '" & Session("SchoolCampus") & "'"

        strWhere += " and lecturer.lecturer_year = '" & ddlYear.SelectedValue & "'"

        If ddlSemester.SelectedIndex > 0 Then
            strWhere += " and subject_info.subject_sem = '" & ddlSemester.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Protected Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum

End Class
