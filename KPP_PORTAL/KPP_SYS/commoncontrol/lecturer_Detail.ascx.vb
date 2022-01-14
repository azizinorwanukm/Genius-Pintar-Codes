Imports System.Data.SqlClient
Imports System.IO

Public Class lecturer_Detail
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                fillCityDDL()
                staff_City.SelectedIndex = 0
                fillStateDDL()
                staff_State.SelectedIndex = 0
                fillPositionDDL()
                staff_Position.SelectedIndex = 0

                LoadPage()

            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub LoadPage()
        strSQL = "SELECT * from staff_Info WHERE stf_ID ='" & Request.QueryString("stf_ID") & "'"
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
                staff_City.SelectedValue = ds.Tables(0).Rows(0).Item("staff_City")
            Else
                staff_City.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_State")) Then
                staff_State.SelectedValue = ds.Tables(0).Rows(0).Item("staff_State")
            Else
                staff_State.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position")) Then
                staff_Position.SelectedValue = ds.Tables(0).Rows(0).Item("staff_Position")
            Else
                staff_Position.SelectedIndex = 0
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

    Private Sub btnLecturerUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnLecturerUpdate.ServerClick
        Dim errorCount As Integer = 0
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objNewConn As SqlConnection = New SqlConnection(strConn)

        If Not IsNumeric(staff_Name.Text) And staff_Name.Text <> "" And Regex.IsMatch(staff_Name.Text, "^[A-Za-z ]+$") Then

            If staff_MyKad.Text <> "" And staff_MyKad.Text.Length < 20 Then

                If staff_ID.Text <> "" And Regex.IsMatch(staff_ID.Text, "^[A-Za-z0-9 ]+$") Then

                    If staff_Email.Text = "" Or Regex.IsMatch(staff_Email.Text, "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$") Then

                        If staff_Sex.Text = "" Or Not IsNumeric(staff_Sex.Text) And Regex.IsMatch(staff_Sex.Text, "^[A-Za-z]+$") Then

                            If staff_MobileNo.Text = "" Or IsNumeric(staff_MobileNo.Text) And Regex.IsMatch(staff_MobileNo.Text, "^[0-9]+$") Then

                                If staff_City.Text = "" Or Not IsNumeric(staff_City.Text) And Regex.IsMatch(staff_City.Text, "^[A-Za-z]+$") Then

                                    If staff_State.Text = "" Or Not IsNumeric(staff_State.SelectedValue) And Regex.IsMatch(staff_State.SelectedValue, "^[A-Za-z]+$") Then

                                        If staff_Posscode.Text = "" Or IsNumeric(staff_Posscode.Text) And Regex.IsMatch(staff_Posscode.Text, "^[0-9]+$") Then

                                            If staff_Position.Text = "" Or Not IsNumeric(staff_Position.Text) And Regex.IsMatch(staff_Position.Text, "^[A-Za-z]+$") Then

                                                If uploadPhoto.PostedFile.FileName <> "" Then

                                                    Dim filename As String = Path.GetFileName(uploadPhoto.PostedFile.FileName)

                                                    ''sets the image path
                                                    Dim imgPath As String = "~/staff_Image/" + filename

                                                    ''then save it to the Folder
                                                    uploadPhoto.SaveAs(Server.MapPath(imgPath))

                                                    'UPDATE DATA
                                                    strSQL = "UPDATE staff_Info set staff_Name='" & staff_Name.Text & "',staff_Mykad='" & staff_MyKad.Text & "',staff_Sex='" & staff_Sex.Text & "',staff_ID='" & staff_ID.Text & "',staff_Email='" & staff_Email.Text & "',staff_MobileNo='" & staff_MobileNo.Text & "',staff_Position='" & staff_Position.SelectedValue & "',staff_Address='" & staff_Address.Text & "',staff_City='" & staff_City.SelectedValue & "',staff_State='" & staff_State.SelectedValue & "',staff_Posscode='" & staff_Posscode.Text & "',staff_Photo='" & imgPath & "' WHERE stf_ID ='" & Request.QueryString("stf_ID") & "'"
                                                    strRet = oCommon.ExecuteSQL(strSQL)
                                                Else
                                                    Dim imgPath As String = "~/staff_Image/user.png"

                                                    'UPDATE DATA
                                                    strSQL = "UPDATE staff_Info set staff_Name='" & staff_Name.Text & "',staff_Mykad='" & staff_MyKad.Text & "',staff_Sex='" & staff_Sex.Text & "',staff_ID='" & staff_ID.Text & "',staff_Email='" & staff_Email.Text & "',staff_MobileNo='" & staff_MobileNo.Text & "',staff_Position='" & staff_Position.SelectedValue & "',staff_Address='" & staff_Address.Text & "',staff_City='" & staff_City.SelectedValue & "',staff_State='" & staff_State.SelectedValue & "',staff_Posscode='" & staff_Posscode.Text & "',staff_Photo='" & imgPath & "' WHERE stf_ID ='" & Request.QueryString("stf_ID") & "'"
                                                    strRet = oCommon.ExecuteSQL(strSQL)
                                                End If
                                                If strRet = "0" Then
                                                    errorCount = 0
                                                Else
                                                    errorCount = 1
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

    Protected Sub fillCityDDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT Parameter FROM setting Where Type = 'City' AND Parameter IS NOT NULL"
            Dim sqlDA As New SqlDataAdapter(query, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            staff_City.DataSource = ds.Tables(0)
            staff_City.DataTextField = "Parameter"
            staff_City.DataValueField = "Parameter"
            staff_City.DataBind()
            staff_City.Items.Insert(0, New ListItem("Select City...", String.Empty))

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Protected Sub fillStateDDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT Parameter FROM setting Where Type = 'State' AND Parameter IS NOT NULL"
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


    Protected Sub fillPositionDDL()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = "SELECT Value FROM setting Where Type = 'Position' AND Parameter IS NOT NULL"
            Dim sqlDA As New SqlDataAdapter(query, objConn)


            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds)

            staff_Position.DataSource = ds.Tables(0)
            staff_Position.DataTextField = "Value"
            staff_Position.DataValueField = "Value"
            staff_Position.DataBind()
            staff_Position.Items.Insert(0, New ListItem("Select Position...", String.Empty))

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Private Sub resetButton_Click(sender As Object, e As EventArgs) Handles resetButton.Click

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "UPDATE staff_Info SET staff_Password = staff_Mykad WHERE stf_ID = '" & Request.QueryString("admin_ID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub
End Class