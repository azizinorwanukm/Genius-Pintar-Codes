Imports System.Data.SqlClient
Imports System.IO

Public Class parent_studenthomepage
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
                Dim id As String = ""

                id = Request.QueryString("parent_ID")

                LoadPage(id)
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

        strSQL = "SELECT * from student_info 
                  WHERE std_ID ='" & Request.QueryString("std_ID") & "'"


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

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_ID")) Then
                student_ID.Text = ds.Tables(0).Rows(0).Item("student_ID")
            Else
                student_ID.Text = ""
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
                student_City.Text = ds.Tables(0).Rows(0).Item("student_City")
            Else
                student_City.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_State")) Then
                student_State.Text = ds.Tables(0).Rows(0).Item("student_State")
            Else
                student_State.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_PostalCode")) Then
                student_PostCode.Text = ds.Tables(0).Rows(0).Item("student_PostalCode")
            Else
                student_PostCode.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Level")) Then
                student_Level.Text = ds.Tables(0).Rows(0).Item("student_Level")
            Else
                student_Level.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Year")) Then
                student_Year.Text = ds.Tables(0).Rows(0).Item("student_Year")
            Else
                student_Year.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Race")) Then
                student_Race.Text = ds.Tables(0).Rows(0).Item("student_Race")
            Else
                student_Race.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Religion")) Then
                student_Religion.Text = ds.Tables(0).Rows(0).Item("student_Religion")
            Else
                student_Religion.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Photo")) Then
                student_Photo.ImageUrl = ds.Tables(0).Rows(0).Item("student_Photo")
            Else
                student_Photo.ImageUrl = "~/student_Image/user.png"
            End If
        End If
    End Sub
End Class