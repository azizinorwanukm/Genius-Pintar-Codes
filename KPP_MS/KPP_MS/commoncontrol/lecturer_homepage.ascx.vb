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

                Dim DATA_STAFFID As String = oCommon.Staff_securityLogin(Request.QueryString("stf_ID"))

                Dim userAccess As String = ""
                userAccess = "select stf_ID from staff_Info where stf_ID = '" & DATA_STAFFID & "'"
                Dim access As String = getFieldValue(userAccess, strConn)

                LoadPage(access)
            End If
        Catch ex As Exception

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
        strSQL = "select * from staff_Info where stf_ID = '" & Access & "' and staff_Status = 'Access'"

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
                staff_MyKad.Text = ds.Tables(0).Rows(0).Item("staff_MyKad")
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
                staff_Sex.Text = ""
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
                staff_City.Text = ds.Tables(0).Rows(0).Item("staff_City")
            Else
                staff_City.Text = ""
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
                staff_Position_P1.Text = ds.Tables(0).Rows(0).Item("staff_Position1")
            Else
                staff_Position_P1.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position2")) Then
                staff_Position_P2.Text = ds.Tables(0).Rows(0).Item("staff_Position2")
            Else
                staff_Position_P2.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Position3")) Then
                staff_Position_P3.Text = ds.Tables(0).Rows(0).Item("staff_Position3")
            Else
                staff_Position_P3.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("staff_Photo")) Then
                staff_Photo.ImageUrl = ds.Tables(0).Rows(0).Item("staff_Photo")
            Else
                staff_Photo.ImageUrl = "~/staff_Image/user.png"
            End If
        End If
    End Sub
End Class
