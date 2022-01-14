Imports System.Data.SqlClient

Public Class lecturer_Create
    Inherits System.Web.UI.UserControl

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            fillRadioList("Gender", staff_Sex)
            fillDDL("State", staff_State)
            fillDDL("City", staff_City)
            position_fill()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim id As Object = Request.QueryString("admin_ID")
        Dim MyConnection As SqlConnection = New SqlConnection(strConn)

        If Not IsNumeric(staff_Name.Text) And staff_Name.Text <> "" And Regex.IsMatch(staff_Name.Text, "^[A-Za-z ]+$") Then

            If IsNumeric(staff_Mykad.Text) And staff_Mykad.Text <> "" And staff_Mykad.Text.Length < 14 Then

                If staff_ID.Text <> "" And IsNumeric(staff_ID.Text) And Regex.IsMatch(staff_ID.Text, "^[A-Za-z0-9 ]+$") Then

                    If staff_Email.Text = "" Or Regex.IsMatch(staff_Email.Text, "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$") Then

                        If staff_Sex.Text = "" Or Not IsNumeric(staff_Sex.SelectedValue) And Regex.IsMatch(staff_Sex.SelectedValue, "^[A-Za-z]+$") Then

                            If staff_MobileNo.Text = "" Or IsNumeric(staff_MobileNo.Text) And Regex.IsMatch(staff_MobileNo.Text, "^[0-9]+$") Then

                                If staff_City.SelectedValue = "" Or Not IsNumeric(staff_City.SelectedValue) And Regex.IsMatch(staff_City.SelectedValue, "^[A-Za-z]+$") Then

                                    If staff_State.Text = "" Or Not IsNumeric(staff_State.SelectedValue) And Regex.IsMatch(staff_State.SelectedValue, "^[A-Za-z]+$") Then

                                        If staff_Posscode.Text = "" Or IsNumeric(staff_Posscode.Text) And Regex.IsMatch(staff_Posscode.Text, "^[0-9]+$") Then

                                            If staff_Position.Text = "" Or Not IsNumeric(staff_Position.SelectedValue) And Regex.IsMatch(staff_Position.SelectedValue, "^[A-Za-z]+$") Then

                                                Dim imgPath As String = "~/staff_Image/user.png"

                                                Using STDDATA As New SqlCommand("INSERT INTO staff_Info(staff_ID,staff_Name,staff_Mykad,staff_Email,staff_MobileNo,staff_Password,staff_Position,staff_Address,staff_City,staff_Posscode,staff_State,staff_Photo) values ('" & staff_ID.Text & "','" & staff_Name.Text & "','" & staff_Mykad.Text & "','" & staff_Email.Text & "','" & staff_MobileNo.Text & "','" & staff_Mykad.Text & "','" & staff_Position.Text & "','" & staff_Address.Text & "','" & staff_City.SelectedValue & "','" & staff_Posscode.Text & "','" & staff_State.SelectedValue & "','" & imgPath & "')", MyConnection)
                                                    MyConnection.Open()
                                                    Dim i = STDDATA.ExecuteNonQuery()
                                                    MyConnection.Close()
                                                    If i <> 0 Then
                                                        errorCount = 0
                                                    Else
                                                        errorCount = 1
                                                    End If
                                                End Using
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

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
    End Sub

    Private Sub position_Fill()
        Try
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            query = String.Format("SELECT Value FROM setting Where Type = 'Position' AND Parameter IS NOT NULL")
            Dim sqlDA As New SqlDataAdapter(query, objConn)


            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            staff_Position.DataSource = ds.Tables(0)
            staff_Position.DataTextField = "Value"
            staff_Position.DataValueField = "Value"
            staff_Position.DataBind()
            staff_Position.Items.Insert(0, New ListItem("Select Level", String.Empty))

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

    Protected Sub staff_State_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            Dim staffState As Integer = staff_State.SelectedIndex
            Dim query As String = ""
            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)

            If String.IsNullOrEmpty(staffState) Then

                query = String.Format("SELECT Parameter FROM setting WHERE Value = '{0}' AND Type = 'City'", staffState)
                Dim sqlDA As New SqlDataAdapter(query, objConn)

                Dim ds As DataSet = New DataSet
                sqlDA.Fill(ds)

                staff_City.DataSource = ds.Tables(0)
                staff_City.DataTextField = "Parameter"
                staff_City.DataValueField = "Parameter"
                staff_City.DataBind()
                staff_City.Items.Insert(0, New ListItem("Select city...", String.Empty))
            Else

            End If

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