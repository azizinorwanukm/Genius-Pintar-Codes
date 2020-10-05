Imports System.Data.SqlClient
Imports System.IO

Public Class parent_guardianTwo
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

                Loadpage(id)
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

    Private Sub Loadpage(ByVal Access As String)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)

        Dim parent_data_female As String = "select parent_motherID from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
        Dim dataParentIDFemale As String = getFieldValue(parent_data_female, strConn)

        strSQL = "SELECT * from parent_Info 
                  left join parent_Info on student_info.parent_motherID=parent_Info.parent_ID 
                  WHERE student_info.parent_motherID ='" & dataParentIDFemale & "'"

        Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

        Dim dset As DataSet = New DataSet
        sqlDB.Fill(dset, "AnyTable")

        Dim Rows As Integer = 0
        Dim Count As Integer = 1
        Dim Table As DataTable = New DataTable
        Table = dset.Tables(0)
        If Table.Rows.Count > 0 Then
            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Name")) Then
                parent_Name.Text = dset.Tables(0).Rows(0).Item("parent_Name")
            Else
                parent_Name.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_IC")) Then
                parent_IC.Text = dset.Tables(0).Rows(0).Item("parent_IC")
            Else
                parent_IC.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Email")) Then
                parent_Email.Text = dset.Tables(0).Rows(0).Item("parent_Email")
            Else
                parent_Email.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_MobileNo")) Then
                parent_MobileNo.Text = dset.Tables(0).Rows(0).Item("parent_MobileNo")
            Else
                parent_MobileNo.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Status")) Then
                Parent_relationship.Text = dset.Tables(0).Rows(0).Item("parent_Status")
            Else
                Parent_relationship.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_HomeAddress")) Then
                parent_Address.Text = dset.Tables(0).Rows(0).Item("parent_HomeAddress")
            Else
                parent_Address.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_City")) Then
                parent_City.Text = dset.Tables(0).Rows(0).Item("parent_City")
            Else
                parent_City.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_State")) Then
                parent_State.Text = dset.Tables(0).Rows(0).Item("parent_State")
            Else
                parent_City.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Postcode")) Then
                parent_Postcode.Text = dset.Tables(0).Rows(0).Item("parent_Postcode")
            Else
                parent_Postcode.Text = ""
            End If


            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_work")) Then
                parent_work.Text = dset.Tables(0).Rows(0).Item("parent_work")
            Else
                parent_work.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Salary")) Then
                parent_Salary.Text = dset.Tables(0).Rows(0).Item("parent_Salary")
            Else
                parent_Salary.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_OfficeNo")) Then
                parent_OfficeNo.Text = dset.Tables(0).Rows(0).Item("parent_OfficeNo")
            Else
                parent_OfficeNo.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_WorkAddress")) Then
                parent_WorkAddress.Text = dset.Tables(0).Rows(0).Item("parent_WorkAddress")
            Else
                parent_WorkAddress.Text = ""
            End If

            If Not IsDBNull(dset.Tables(0).Rows(0).Item("parent_Work_Email")) Then
                parent_Work_Email.Text = dset.Tables(0).Rows(0).Item("parent_Work_Email")
            Else
                parent_Work_Email.Text = ""
            End If

        End If
    End Sub

End Class