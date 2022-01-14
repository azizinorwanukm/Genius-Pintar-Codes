Imports System.Data.SqlClient
Imports System.Drawing

Public Class hostel_Create
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oCommon As New Commonfunction
    Dim result As Integer = 0
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                block_name_list()
                block_level_list()
                hostel_name_list()
                year_list()
                ''Generate_Table()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub block_name_list()
        strSQL = "SELECT Parameter from setting where Type = 'Block_Name' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlock_Name.DataSource = ds
            ddlBlock_Name.DataTextField = "Parameter"
            ddlBlock_Name.DataValueField = "Parameter"
            ddlBlock_Name.DataBind()
            ddlBlock_Name.Items.Insert(0, New ListItem("Select Block", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub hostel_name_list()
        strSQL = "SELECT Parameter from setting where Type = 'Hostel_Name' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlHostelName.DataSource = ds
            ddlHostelName.DataTextField = "Parameter"
            ddlHostelName.DataValueField = "Parameter"
            ddlHostelName.DataBind()
            ddlHostelName.Items.Insert(0, New ListItem("Select Hostel", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub block_level_list()
        strSQL = "SELECT Parameter from setting where Type = 'Block_Level' and Parameter is not null "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBlock_Level.DataSource = ds
            ddlBlock_Level.DataTextField = "Parameter"
            ddlBlock_Level.DataValueField = "Parameter"
            ddlBlock_Level.DataBind()
            ddlBlock_Level.Items.Insert(0, New ListItem("Select Floor Level", String.Empty))
            ''ddlYear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub year_list()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Year' "
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
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Btnsimpan_ServerClick(sender As Object, e As EventArgs) Handles Btnsimpan.ServerClick
        Dim errorCount As Integer = 0
        Dim id As String = Request.QueryString("admin_ID")

        If ddlHostelName.SelectedIndex > 0 Then

            If ddlBlock_Name.SelectedIndex > 0 Then

                If ddlBlock_Level.SelectedIndex > 0 Then

                    If Not IsNothing(room_Quantity.Text) And IsNumeric(room_Quantity.Text) Then

                        ''check if the hostel is already exist 
                        Dim hostelExist As String = "select hostel_Name from hostel_info where hostel_Name = '" & ddlHostelName.SelectedValue & "' and year = '" & ddlYear.SelectedValue & "'"
                        Dim dataHostelExist As String = getFieldValue(hostelExist, strConn)

                        If dataHostelExist = "" Or dataHostelExist = "NULL" Then
                            ''hostel not exist
                            Using STDDATA As New SqlCommand("INSERT INTO hostel_info(hostel_Name,block_Name,block_Level,room_Number,year) values ('" & ddlHostelName.SelectedValue & "',
                                                        '" & ddlBlock_Name.SelectedValue & "','" & ddlBlock_Level.SelectedValue & "','" & room_Quantity.Text & "','" & ddlYear.SelectedValue & "')", objConn)
                                objConn.Open()
                                Dim i = STDDATA.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    ''success
                                    ''insert data to room_info based on room number on hostel info
                                    For index As Integer = 1 To room_Quantity.Text
                                        Using ROOMDATA As New SqlCommand("INSERT INTO room_info(hostel_ID,room_Name,room_Capacity,year,status)
                                                                      SELECT hostel_ID,'" & index & "',3,'" & ddlYear.SelectedValue & "','Current' from hostel_info
                                                                      WHERE year = '" & Now.Year & "'", objConn)
                                            objConn.Open()
                                            Dim j = ROOMDATA.ExecuteNonQuery()
                                            objConn.Close()
                                            If j <> 0 Then
                                                errorCount = 0
                                            Else
                                                errorCount = 1
                                            End If
                                        End Using
                                    Next

                                Else
                                    ''error
                                    errorCount = 1
                                End If

                            End Using

                        Else
                            '' hostel is exist
                            '' if the hotel exist and its mean that user want to add new room 
                            '' room name cannot be add separately from hostel
                            '' if the user want to add a new room.. user must create a same hostel name with a different number of room quantity
                            '' collect all the room data that relevant to the hostel namew

                            ''get hostel id
                            Dim hostelID As String = "select hostel_ID from hostel_info where hostel_Name = '" & ddlHostelName.SelectedValue & "' and year = '" & ddlYear.SelectedValue & "'"
                            Dim datahostelID As String = getFieldValue(hostelID, strConn)

                            ''update an room status from current to old
                            strSQL = "UPDATE room_info SET status='old' WHERE hostel_ID ='" & datahostelID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                            If strRet = "0" Then
                                errorCount = 0
                            Else
                                errorCount = 1
                            End If

                            Try
                                ''delete relevant data in hostel info
                                Dim MyConnection2 As SqlConnection = New SqlConnection(strConn)
                                Dim Dlt_ClassData2 As New SqlDataAdapter()
                                Dim dlt_Class2 As String
                                Dlt_ClassData2.SelectCommand = New SqlCommand()
                                Dlt_ClassData2.SelectCommand.Connection = MyConnection2
                                Dlt_ClassData2.SelectCommand.CommandText = "delete hostel_info where hostel_ID ='" & datahostelID & "'"
                                MyConnection2.Open()
                                dlt_Class2 = Dlt_ClassData2.SelectCommand.ExecuteScalar()
                                MyConnection2.Close()
                            Catch ex As Exception
                            End Try

                            ''insert new data
                            Using STDDATA As New SqlCommand("INSERT INTO hostel_info(hostel_Name,block_Name,block_Level,room_Number,year) values ('" & ddlHostelName.SelectedValue & "',
                                                        '" & ddlBlock_Name.SelectedValue & "','" & ddlBlock_Level.SelectedValue & "','" & room_Quantity.Text & "','" & ddlYear.SelectedValue & "')", objConn)
                                objConn.Open()
                                Dim i = STDDATA.ExecuteNonQuery()
                                objConn.Close()

                                If i <> 0 Then
                                    ''success
                                    ''insert data to room_info based on room number on hostel info
                                    For index As Integer = 1 To room_Quantity.Text

                                        Using ROOMDATA As New SqlCommand("INSERT INTO room_info(hostel_ID,room_Name,room_Capacity,year,status)
                                                                          SELECT hostel_ID,'" & index & "',3,'" & ddlYear.SelectedValue & "','Current' from hostel_info
                                                                          WHERE year = '" & Now.Year & "'", objConn)
                                            objConn.Open()
                                            Dim j = ROOMDATA.ExecuteNonQuery()
                                            objConn.Close()
                                            If j <> 0 Then
                                                errorCount = 0
                                            Else
                                                errorCount = 1
                                            End If
                                        End Using
                                    Next

                                Else
                                    ''error
                                    errorCount = 1
                                End If

                                ''update data in room info
                                strSQL = "WITH mydata AS (SELECT std_ID,room_Name FROM room_info WHERE hostel_ID='" & datahostelID & "' )
                                          UPDATE room_info SET std_ID = mydata.std_ID 
                                          FROM mydata WHERE room_info.status = 'Current' AND room_info.year = '" & ddlYear.SelectedValue & "' AND room_info.room_Name = mydata.room_Name"
                                strRet = oCommon.ExecuteSQL(strSQL)
                                If strRet = "0" Then
                                    errorCount = 0

                                    ''if success
                                    Try
                                        ''delete relevant previous data in hostel info
                                        Dim MyConnection1 As SqlConnection = New SqlConnection(strConn)
                                        Dim Dlt_ClassData1 As New SqlDataAdapter()
                                        Dim dlt_Class1 As String
                                        Dlt_ClassData1.SelectCommand = New SqlCommand()
                                        Dlt_ClassData1.SelectCommand.Connection = MyConnection1
                                        Dlt_ClassData1.SelectCommand.CommandText = "delete room_info where hostel_ID ='" & datahostelID & "'"
                                        MyConnection1.Open()
                                        dlt_Class1 = Dlt_ClassData1.SelectCommand.ExecuteScalar()
                                        MyConnection1.Close()
                                    Catch ex As Exception
                                    End Try

                                Else
                                    errorCount = 1
                                End If

                            End Using

                        End If
                    Else
                        errorCount = 2
                    End If
                Else
                    errorCount = 3
                End If
            Else
                errorCount = 4
            End If
        Else
            errorCount = 5
        End If

        If errorCount = 1 Then
            Response.Redirect("admin_daftar_asrama_baru.aspx?result=-1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 0 Then
            Response.Redirect("admin_daftar_asrama_baru.aspx?result=1&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 2 Then
            Response.Redirect("admin_daftar_asrama_baru.aspx?result=2&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 3 Then
            Response.Redirect("admin_daftar_asrama_baru.aspx?result=3&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 4 Then
            Response.Redirect("admin_daftar_asrama_baru.aspx?result=4&admin_ID=" + Request.QueryString("admin_ID"))
        ElseIf errorCount = 5 Then
            Response.Redirect("admin_daftar_asrama_baru.aspx?result=5&admin_ID=" + Request.QueryString("admin_ID"))
        End If
    End Sub

    Private Sub Btnback_ServerClick(sender As Object, e As EventArgs) Handles Btnback.ServerClick
        Response.Redirect("admin_login_berjaya.aspx?admin_ID=" + Request.QueryString("admin_ID"))
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
End Class