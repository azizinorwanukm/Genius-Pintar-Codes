Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography

Public Class student_DetailAdmin
    Inherits System.Web.UI.UserControl
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                student_year_list()
                State_list()
                Level_list()
                Sem_list()
                Race_List()
                Religion_List()

                Dim access As String = Request.QueryString("std_ID")

                LoadPage(access)
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

    Private Sub LoadPage(ByVal Access As String)
        ''student_info
        strSQL = "select * from student_info
                  left join student_Level on student_info.std_ID=student_level.std_ID
                  where student_Level.ID = (
							                select max(ID) from student_Level
							                left join student_info on student_level.std_ID=student_info.std_ID
							                where student_info.std_ID = '" + Access + "')"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                student_Name.Text = ds.Tables(0).Rows(0).Item("student_Name")
            Else
                student_Name.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Mykad")) Then
                student_Mykad.Text = ds.Tables(0).Rows(0).Item("student_Mykad")
            Else
                student_Mykad.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Email")) Then
                student_Email.Text = ds.Tables(0).Rows(0).Item("student_Email")
            Else
                student_Email.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Sex")) Then
                student_Sex.Text = ds.Tables(0).Rows(0).Item("student_Sex")
            Else
                student_Sex.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_FonNo")) Then
                student_FonNo.Text = ds.Tables(0).Rows(0).Item("student_FonNo")
            Else
                student_FonNo.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Address")) Then
                student_Address.Text = ds.Tables(0).Rows(0).Item("student_Address")
            Else
                student_Address.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_City")) Then
                txtCity.Text = ds.Tables(0).Rows(0).Item("student_City")
            Else
                txtCity.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_State")) Then
                ddlState.SelectedValue = ds.Tables(0).Rows(0).Item("student_State")
            Else
                ddlState.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_PostalCode")) Then
                student_PostCode.Text = ds.Tables(0).Rows(0).Item("student_PostalCode")
            Else
                student_PostCode.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Level")) Then
                ddlLevel.SelectedValue = ds.Tables(0).Rows(0).Item("student_Level")
            Else
                ddlLevel.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Sem")) Then
                ddlSem.SelectedValue = ds.Tables(0).Rows(0).Item("student_Sem")
            Else
                ddlSem.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Race")) Then
                ddlRace.SelectedValue = ds.Tables(0).Rows(0).Item("student_Race")
            Else
                ddlRace.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Religion")) Then
                ddlReligion.SelectedValue = ds.Tables(0).Rows(0).Item("student_Religion")
            Else
                ddlReligion.SelectedValue = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Photo")) Then
                student_Photo.ImageUrl = ds.Tables(0).Rows(0).Item("student_Photo")
            Else
                student_Photo.ImageUrl = "~/student_Image/user.png"
            End If

            '--password encrypted
            Dim strPwd As String = ""
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_ID")) Then
                student_ID.Text = ds.Tables(0).Rows(0).Item("student_ID")
            Else
                student_ID.Text = ""
            End If

        End If

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        strSQL = "select min(year) from student_level where std_ID = '" & Access & "'"
        strRet = oCommon.getFieldValue(strSQL)
        ddlYear_registered.SelectedValue = strRet

        strSQL = "select MAX(ID) from student_level where std_ID = '" & Access & "'"
        strRet = oCommon.getFieldValue(strSQL)

        strSQL = "select max(student_Sem) from student_level where ID = '" & strRet & "'"
        strRet = oCommon.getFieldValue(strSQL)
        ddlSem.SelectedValue = strRet

    End Sub

    Private Sub student_year_list()
        strSQL = "SELECT Parameter from setting where Type = 'Year' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear_registered.DataSource = ds
            ddlYear_registered.DataTextField = "Parameter"
            ddlYear_registered.DataValueField = "Parameter"
            ddlYear_registered.DataBind()
            ddlYear_registered.Items.Insert(0, New ListItem("Select Year", String.Empty))

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

    Private Sub Level_list()
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
            ddlLevel.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Sem_list()
        strSQL = "SELECT * FROM setting WHERE Type='Sem' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSem.DataSource = ds
            ddlSem.DataTextField = "Parameter"
            ddlSem.DataValueField = "Value"
            ddlSem.DataBind()
            ddlSem.Items.Insert(0, New ListItem("Select Sem", String.Empty))

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub btnStudentUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnStudentUpdate.ServerClick
        Dim errorCount As Integer = 0
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objNewConn As SqlConnection = New SqlConnection(strConn)

        Dim access As String = Request.QueryString("std_ID")

        If ddlLevel.SelectedIndex <> 0 Then

            If IsNumeric(student_Mykad.Text) And student_Mykad.Text <> "" And student_Mykad.Text.Length < 14 Then

                If student_Name.Text <> "" And Not IsNothing(student_Name.Text) Then

                    If student_Sex.Text <> "" And Regex.IsMatch(student_Sex.Text, "^[A-Za-z]+$") Then

                        If student_Email.Text = "" Or Regex.IsMatch(student_Email.Text, "^[_A-Za-z0-9-]+(.[A-Za-z0-9-]+)@[A-Za-z0-9-]+(.[A-Za-z0-9-]+)*(.[A-Za-z]{2,4})$") Or student_Email.Text = "NULL" Then

                            If ddlRace.SelectedValue <> "0" Or ddlRace.SelectedValue = "0" Then

                                If ddlReligion.SelectedValue <> "0" Or ddlReligion.SelectedValue = "0" Then

                                    If uploadPhoto.PostedFile.FileName <> "" Then

                                        Dim filename As String = Path.GetFileName(uploadPhoto.PostedFile.FileName)

                                        ''sets the image path
                                        Dim imgPath As String = "~/student_Image/" + filename

                                        ''then save it to the Folder
                                        uploadPhoto.SaveAs(Server.MapPath(imgPath))

                                        'UPDATE STUDENT DATA
                                        strSQL = "UPDATE student_info set student_ID='" & student_ID.Text & "',student_Mykad='" & student_Mykad.Text & "',
                                                          student_Sex='" & student_Sex.Text & "',student_Name='" & oCommon.FixSingleQuotes(student_Name.Text.ToUpper) & "',student_Email='" & student_Email.Text & "',
                                                          student_FonNo='" & student_FonNo.Text & "',student_Address='" & student_Address.Text & "',
                                                          student_City='" & txtCity.Text & "',student_State='" & ddlState.SelectedValue & "',student_Race='" & ddlRace.SelectedValue & "',student_Religion='" & ddlReligion.SelectedValue & "',
                                                          student_PostCode='" & student_PostCode.Text & "',
                                                          student_Photo='" & imgPath & "' WHERE std_ID ='" & access & "'"
                                        strRet = oCommon.ExecuteSQL(strSQL)
                                    Else
                                        Dim imgPath As String = "~/student_Image/user.png"

                                        'UPDATE STUDENT DATA
                                        strSQL = "UPDATE student_info set student_ID='" & student_ID.Text & "',student_Mykad='" & student_Mykad.Text & "',
                                                          student_Sex='" & student_Sex.Text & "',student_Name='" & oCommon.FixSingleQuotes(student_Name.Text.ToUpper) & "',
                                                          student_Email='" & student_Email.Text & "',student_FonNo='" & student_FonNo.Text & "',
                                                          student_Address='" & student_Address.Text & "',student_City='" & txtCity.Text & "',
                                                          student_State='" & ddlState.SelectedValue & "',student_PostalCode='" & student_PostCode.Text & "',
                                                          student_Photo='" & imgPath & "',student_Race='" & ddlRace.SelectedValue & "',student_Religion='" & ddlReligion.SelectedValue & "' 
                                                          WHERE std_ID ='" & access & "'"
                                        strRet = oCommon.ExecuteSQL(strSQL)
                                    End If

                                    If strRet = "0" Then
                                        ShowMessage("Please enter a valid student level", MessageType.Error)
                                    Else
                                        ShowMessage("Please enter a valid", MessageType.Error)
                                    End If
                                Else
                                    ShowMessage("Please enter a valid student religion", MessageType.Error)
                                End If

                            Else
                                ShowMessage("Please enter a valid student race", MessageType.Error)

                            End If
                        Else
                            ShowMessage("Please enter a valid student email", MessageType.Error)
                        End If
                    Else
                        ShowMessage("Please enter a valid gender", MessageType.Error)
                    End If

                Else
                    ShowMessage("Please enter a valid student name", MessageType.Error)
                End If
            Else
                ShowMessage("Please enter a valid student ic", MessageType.Error)
            End If
        Else
            ShowMessage("Please enter a valid student level", MessageType.Error)
        End If
    End Sub

    Private Sub resetButton_Click(sender As Object, e As EventArgs) Handles resetButton.Click

        Dim access As String = Request.QueryString("std_ID")

        strSQL = "UPDATE student_Info SET student_Password = student_Mykad WHERE std_ID = '" & access & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class