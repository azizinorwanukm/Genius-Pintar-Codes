Imports System.Data.SqlClient
Imports System.Drawing

Public Class student_Create
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
                Dim id As String = Request.QueryString("admin_ID")

                Dim data As String = oCommon.securityLogin(id)

                If data = "FALSE" Then
                    Response.Redirect("default.aspx")
                ElseIf data = "TRUE" Then
                    student_year()
                    City()
                    State()
                    Race_List()
                    Religion_List()
                    Level()
                    CityP1()
                    StateP1()
                    CityP2()
                    StateP2()
                    ''Generate_Table()
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Race_List()
        strSQL = "SELECT Parameter from setting where Type = 'Race' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlRace.DataSource = ds
            ddlRace.DataTextField = "Parameter"
            ddlRace.DataValueField = "Parameter"
            ddlRace.DataBind()
            ddlRace.Items.Insert(0, New ListItem("Select Race", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Religion_List()
        strSQL = "SELECT Parameter from setting where Type = 'Religion' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlReligion.DataSource = ds
            ddlReligion.DataTextField = "Parameter"
            ddlReligion.DataValueField = "Parameter"
            ddlReligion.DataBind()
            ddlReligion.Items.Insert(0, New ListItem("Select Religion", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub student_year()
        strSQL = "SELECT Parameter from setting where Type = 'Year' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear.DataSource = ds
            ddlYear.DataTextField = "Parameter"
            ddlYear.DataValueField = "Parameter"
            ddlYear.DataBind()
            ddlYear.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub City()
        strSQL = "SELECT Parameter FROM setting WHERE Type='City' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCity.DataSource = ds
            ddlCity.DataTextField = "Parameter"
            ddlCity.DataValueField = "Parameter"
            ddlCity.DataBind()
            ddlCity.Items.Insert(0, New ListItem("City", String.Empty))
            ddlCity.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub CityP1()
        strSQL = "SELECT Parameter FROM setting WHERE Type='City' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCityP1.DataSource = ds
            ddlCityP1.DataTextField = "Parameter"
            ddlCityP1.DataValueField = "Parameter"
            ddlCityP1.DataBind()
            ddlCityP1.Items.Insert(0, New ListItem("City", String.Empty))
            ddlCityP1.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub CityP2()
        strSQL = "SELECT Parameter FROM setting WHERE Type='City' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlCityP2.DataSource = ds
            ddlCityP2.DataTextField = "Parameter"
            ddlCityP2.DataValueField = "Parameter"
            ddlCityP2.DataBind()
            ddlCityP2.Items.Insert(0, New ListItem("City", String.Empty))
            ddlCityP2.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub State()
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
            ddlState.Items.Insert(0, New ListItem("State", String.Empty))
            ddlState.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub StateP1()
        strSQL = "SELECT Parameter FROM setting WHERE Type='State' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStateP1.DataSource = ds
            ddlStateP1.DataTextField = "Parameter"
            ddlStateP1.DataValueField = "Parameter"
            ddlStateP1.DataBind()
            ddlStateP1.Items.Insert(0, New ListItem("State", String.Empty))
            ddlStateP1.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub StateP2()
        strSQL = "SELECT Parameter FROM setting WHERE Type='State' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStateP2.DataSource = ds
            ddlStateP2.DataTextField = "Parameter"
            ddlStateP2.DataValueField = "Parameter"
            ddlStateP2.DataBind()
            ddlStateP2.Items.Insert(0, New ListItem("State", String.Empty))
            ddlStateP2.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Level()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlLevel.DataSource = ds
            ddlLevel.DataTextField = "Parameter"
            ddlLevel.DataValueField = "Parameter"
            ddlLevel.DataBind()
            ddlLevel.Items.Insert(0, New ListItem("Level", String.Empty))
            ddlLevel.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlState_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlState.SelectedValue <> "" Then
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim STDLEVEL As New SqlDataAdapter()

            ''get parameter city

            strSQL = "select Parameter from setting where Value = '" & ddlState.SelectedValue & "'"
            Dim sqlDA1 As New SqlDataAdapter(strSQL, objConn)
            Dim ds1 As DataSet = New DataSet
            sqlDA1.Fill(ds1, "AnyTable")

            ddlCity.DataSource = ds1
            ddlCity.DataTextField = "Parameter"
            ddlCity.DataValueField = "Parameter"
            ddlCity.DataBind()
        Else
            ddlCity.Items.Clear()
        End If
    End Sub

    Private Sub BtnSimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim strgender As String = ""

        If rbtn_Male.Checked = True Then
            strgender = "Male"
        End If
        If rbtn_Female.Checked = True Then
            strgender = "Female"
        End If

        If ddlLevel.SelectedValue > "0" Then

            If IsNumeric(student_Mykad.Text) And student_Mykad.Text <> "" And student_Mykad.Text.Length < 14 Then

                If student_PostalCode.Text = "" Or IsNumeric(student_PostalCode.Text) Then

                    If student_Name.Text <> "" And Not IsNothing(student_Name.Text) And Regex.IsMatch(student_Name.Text, "^[A-Za-z ]+$") And Not IsNumeric(student_Name.Text) Then

                        If IsNumeric(student_FonNo.Text) Or student_FonNo.Text = "" Then

                            If ddlCity.SelectedValue <> "0" Then

                                If ddlState.SelectedValue <> "0" Then

                                    If strgender <> "" And Regex.IsMatch(strgender, "^[A-Za-z]+$") Then

                                        If student_Email.Text = "" Or Regex.IsMatch(student_Email.Text, "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$") Then

                                            If student_ID.Text <> "" And Regex.IsMatch(student_ID.Text, "^[A-Za-z0-9]+$") Then

                                                If ddlYear.SelectedValue > "0" Then
                                                    Dim imgPath As String = "~/student_Image/user.png"

                                                    If ddlRace.SelectedValue > "0" Or ddlRace.SelectedValue = "0" Then

                                                        If ddlReligion.SelectedValue > "0" Or ddlReligion.SelectedValue = "0" Then

                                                            Using STDDATA As New SqlCommand("INSERT INTO student_info(student_ID,student_Mykad,student_Name,student_Email,student_FonNo,student_Address,
                                                             student_Password,student_PostalCode,student_State,student_City,student_Photo,student_Sex,student_Race,student_Religion) values 
                                                           ('" & student_ID.Text & "','" & student_Mykad.Text & "','" & oCommon.FixSingleQuotes(student_Name.Text.ToUpper) & "',
                                                            '" & student_Email.Text & "','" & student_FonNo.Text & "','" & student_Address.Text & "',
                                                           '" & student_Mykad.Text & "','" & student_PostalCode.Text & "','" & ddlState.SelectedValue & "',
                                                           '" & ddlCity.SelectedValue & "','" & imgPath & "','" & strgender & "','" & ddlRace.SelectedValue & "','" & ddlReligion.SelectedValue & "')", objConn)
                                                                objConn.Open()
                                                                Dim i = STDDATA.ExecuteNonQuery()
                                                                objConn.Close()
                                                                If i <> 0 Then
                                                                    errorCount = 0
                                                                Else
                                                                    errorCount = 1
                                                                End If
                                                            End Using

                                                            Dim stdID As String = "select std_ID from student_info where student_Mykad = '" & student_Mykad.Text & "'"
                                                            Dim dataStdID As String = getFieldValue(stdID, strConn)

                                                            Using STDDATA As New SqlCommand("INSERT INTO student_level(std_ID,student_Sem,student_Level,year,month,day) values 
                                                            ('" & dataStdID & "','Sem 1','" & ddlLevel.SelectedValue & "','" & ddlYear.SelectedValue & "','" & Now.Month & "','" & Now.Day & "')", objConn)
                                                                objConn.Open()
                                                                Dim i = STDDATA.ExecuteNonQuery()
                                                                objConn.Close()
                                                                If i <> 0 Then
                                                                    errorCount = 0
                                                                Else
                                                                    errorCount = 1
                                                                End If
                                                            End Using

                                                            If validateParent1() = 0 Then

                                                                Dim exist_fatherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent1_IC.Text & "'"
                                                                Dim data_fatherID As String = getFieldValue(exist_fatherID, strConn)

                                                                If data_fatherID = "" Or data_fatherID = "NULL" Or data_fatherID = "0" Then

                                                                    'Validate parent1 input
                                                                    Using PJGDATA As New SqlCommand("INSERT INTO parent_Info(parent_No,parent_Name,parent_IC,parent_Email,parent_MobileNo,parent_Status,
                                                                 parent_HomeAddress,parent_WorkAddress,parent_OfficeNo,parent_Postcode,parent_City,parent_State,
                                                                 parent_Work,parent_Salary,parent_Work_Email) values 
                                                                ('1' ,'" & Parent1_Name.Text & "','" & Parent1_IC.Text & "','" & Parent1_Email.Text & "',
                                                                '" & Parent1_MobileNo.Text & "','" & Parent1_Status.Text & "','" & Parent1_HomeAddress.Text & "',
                                                                '" & Parent1_WorkAddress.Text & "','" & Parent1_OfficeNo.Text & "','" & Parent1_Postalcode.Text & "',
                                                                '" & ddlCityP1.SelectedValue & "','" & ddlStateP1.SelectedValue & "','" & Parent1_Work.Text & "',
                                                                '" & Parent1_Salary.Text & "','" & Parent1_Work_Email.Text & "')", objConn)
                                                                        objConn.Open()
                                                                        Dim i = PJGDATA.ExecuteNonQuery()
                                                                        objConn.Close()
                                                                        If i <> 0 Then
                                                                            errorCount = 0
                                                                        Else
                                                                            errorCount = 1
                                                                        End If
                                                                    End Using

                                                                End If

                                                            Else
                                                                errorCount = validateParent1()
                                                            End If

                                                            If validateParent2() = 0 Then

                                                                Dim exist_motherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent2_IC.Text & "'"
                                                                Dim data_motherID As String = getFieldValue(exist_motherID, strConn)

                                                                If data_motherID = "" Or data_motherID = "NULL" Or data_motherID = "0" Then

                                                                    Using PJGDATA As New SqlCommand("INSERT INTO parent_Info(parent_No,parent_Name,parent_IC,parent_Email,parent_MobileNo,parent_Status,
                                                             parent_HomeAddress,parent_WorkAddress,parent_OfficeNo,parent_Postcode,parent_City,parent_State,
                                                             parent_Work,parent_Salary,parent_Work_Email) values 
                                                            ('2','" & Parent2_Name.Text & "','" & Parent2_IC.Text & "','" & Parent2_Email.Text & "',
                                                            '" & Parent2_MobileNo.Text & "','" & Parent2_Status.Text & "','" & Parent2_HomeAddress.Text & "',
                                                            '" & Parent2_WorkAddress.Text & "','" & Parent2_OfficeNo.Text & "','" & Parent2_PostalCode.Text & "',
                                                            '" & ddlCityP2.SelectedValue & "','" & ddlCityP2.SelectedValue & "','" & Parent2_Work.Text & "',
                                                            '" & Parent2_Salary.Text & "','" & Parent2_Work_Email.Text & "')", objConn)
                                                                        objConn.Open()
                                                                        Dim i = PJGDATA.ExecuteNonQuery()
                                                                        objConn.Close()
                                                                        If i <> 0 Then
                                                                            errorCount = 0
                                                                        Else
                                                                            errorCount = 1
                                                                        End If
                                                                    End Using

                                                                End If

                                                            Else
                                                                errorCount = validateParent2()
                                                            End If

                                                            Dim fatherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent1_IC.Text & "'"
                                                            Dim ExistFatherID As String = getFieldValue(fatherID, strConn)

                                                            Dim motherID As String = "select parent_ID from parent_Info where parent_IC = '" & Parent2_IC.Text & "'"
                                                            Dim ExistMotherID As String = getFieldValue(motherID, strConn)

                                                            strSQL = "UPDATE student_info set parent_fatherID ='" & ExistFatherID & "',parent_motherID='" & ExistMotherID & "' WHERE std_ID ='" & dataStdID & "'"
                                                            strRet = oCommon.ExecuteSQL(strSQL)

                                                        End If

                                                    End If

                                                Else
                                                    errorCount = 12
                                                End If
                                            Else
                                                errorCount = 11
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
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 0 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 3 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 4 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=4&admin_ID = " + Request.QueryString("admin_ID"))
        ElseIf errorCount = 5 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 6 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=6&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 7 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=7&admin_ID = " + Request.QueryString("admin_ID"))
        ElseIf errorCount = 8 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=8&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 9 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=9&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 10 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=10&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 11 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=11&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 12 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=12&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 20 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=20&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 21 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=21&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 22 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=22&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 23 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=23&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 24 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=24&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 25 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=25&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 26 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=26&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 27 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=27&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 28 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=28&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 29 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=29&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 40 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=40&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 41 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=41&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 42 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=42&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 43 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=43&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 44 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=44&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 45 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=45&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 46 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=46&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 47 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=47&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 48 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=48&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 49 Then
            Response.Redirect("admin_daftar_pelajar_baru.aspx?result=49&admin_ID=" + Request.QueryString("admin_ID"))
        Else

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

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Public Function getValue(ByVal sqlA_plus As String, ByVal MyConnection As String) As String
        If sqlA_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlA_plus, conn)
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

    Private Function validateParent1() As Integer

        If Parent1_Name.Text = "" Or Regex.IsMatch(Parent1_Name.Text, "^[A-Za-z ]+$") Then

            If Parent1_IC.Text <> "" And Regex.IsMatch(Parent1_IC.Text, "^[A-Za-z0-9]+$") And Not Parent1_IC.Text.Length < 14 Then

                If Parent1_Email.Text = "" Or Regex.IsMatch(Parent1_Email.Text, "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$") Then

                    If IsNumeric(Parent1_MobileNo.Text) Or Parent1_MobileNo.Text = "" Then

                        If Parent1_Status.Text = "" Or Regex.IsMatch(Parent1_Status.Text, "^[A-Za-z ]+$") Then

                            If ddlCityP1.SelectedValue <> "0" Then

                                If ddlStateP1.SelectedValue <> "0" Then

                                    If Parent1_Postalcode.Text = "" Or IsNumeric(Parent1_Postalcode.Text) Then

                                        If Parent1_Work.Text = "" Or Regex.IsMatch(Parent1_Work.Text, "^[A-Za-z ]+$") Then

                                            If IsNumeric(Parent1_OfficeNo.Text) Or Parent1_OfficeNo.Text = "" Then
                                                Return 0
                                            Else
                                                Return 29
                                            End If

                                        Else
                                            Return 28
                                        End If

                                    Else
                                        Return 27
                                    End If

                                Else
                                    Return 26
                                End If

                            Else
                                Return 25
                            End If

                        Else
                            Return 24
                        End If

                    Else
                        Return 23
                    End If

                Else
                    Return 22
                End If

            Else
                Return 21
            End If

        Else
            Return 20
        End If

    End Function


    Private Function validateParent2() As Integer

        If Parent2_Name.Text = "" Or Regex.IsMatch(Parent2_Name.Text, "^[A-Za-z ]+$") Then

            If Parent2_IC.Text <> "" And Regex.IsMatch(Parent2_IC.Text, "^[A-Za-z0-9]+$") And Not Parent2_IC.Text.Length < 14 Then

                If Parent2_Email.Text = "" Or Regex.IsMatch(Parent2_Email.Text, "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$") Then

                    If IsNumeric(Parent2_MobileNo.Text) Or Parent2_MobileNo.Text = "" Then

                        If Parent2_Status.Text = "" Or Regex.IsMatch(Parent2_Status.Text, "^[A-Za-z ]+$") Then

                            If ddlCityP2.SelectedValue <> "0" Then

                                If ddlStateP2.SelectedValue <> "0" Then

                                    If Parent2_PostalCode.Text = "" Or IsNumeric(Parent2_PostalCode.Text) Then

                                        If Parent2_Work.Text = "" Or Regex.IsMatch(Parent2_Work.Text, "^[A-Za-z ]+$") Then

                                            If IsNumeric(Parent2_OfficeNo.Text) Or Parent2_OfficeNo.Text = "" Then
                                                Return 0
                                            Else
                                                Return 49
                                            End If

                                        Else
                                            Return 48
                                        End If

                                    Else
                                        Return 47
                                    End If

                                Else
                                    Return 46
                                End If

                            Else
                                Return 45
                            End If

                        Else
                            Return 44
                        End If

                    Else
                        Return 43
                    End If

                Else
                    Return 42
                End If

            Else
                Return 41
            End If

        Else
            Return 40
        End If

    End Function
End Class