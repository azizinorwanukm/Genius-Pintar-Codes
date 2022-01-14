Imports System.Data.SqlClient

Public Class penjaga_laporan_peperiksaan
    Inherits System.Web.UI.Page
    Dim sqlA_plus As String
    Dim sqlA As String
    Dim sqlA_minus As String
    Dim sqlB_plus As String
    Dim sqlB As String
    Dim sqlB_minus As String
    Dim sqlC_plus As String
    Dim sqlC As String
    Dim sqlD As String
    Dim sqlE As String
    Dim sqlG As String
    Dim strConn As String = "server=DESKTOP-LRGNOU0;Database=SMS;uid=sa;pwd=p@ssw0rd1;"
    Dim MyConnection As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim id As Object = Session("penjaga")

            Using data As New SqlCommand("Select DISTINCT subject_info.subject_Name, exam_result.marks, exam_result.grade From parent_Info Left Join student_info ON student_info.parent_ID=parent_Info.parent_ID Left Join course ON course.student_ID=student_info.student_ID Left Join subject_info ON course.subject_ID=subject_info.subject_ID Left Join exam_result ON course.course_ID=exam_result.course_ID WHERE parent_IC ='" & id & "' ORDER BY subject_info.subject_Name")
                Using sda As New SqlDataAdapter()
                    data.Connection = MyConnection
                    sda.SelectCommand = data
                    Using dt As New DataTable()
                        sda.Fill(dt)

                        Dim html As New StringBuilder()

                        html.Append("<table class='w3-border w3-bsorder-black w3-text-black' style='width: 70%;background-color:#FFFFFF' id='kursus_pelajar'>")
                        html.Append("<tr><th class='id1 w3-border' style='width:50%'>Subjek Name</th><th class='id1 w3-border' style='width:10%'>Markah</th><th class='id1 w3-border' style='width:10%'>Gred</th></tr>")

                        For Each row As DataRow In dt.Rows
                            html.Append("<tr>")
                            For Each column As DataColumn In dt.Columns
                                html.Append("<td class='id1 w3-border'>")
                                html.Append(row(column.ColumnName))
                                html.Append("</td>")
                            Next
                            html.Append("</tr>")
                        Next
                        html.Append("</table>")
                        pelajar_exam.Controls.Add(New Literal() With {.Text = html.ToString()})
                    End Using
                End Using
            End Using

            sqlA_plus = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 95 And parent_IC ='" & id & "'"
            Dim dataA_plus As String = getFieldValueA_plus(sqlA_plus, strConn)
            HiddenFieldA_plus.Value = dataA_plus

            sqlA = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 85 AND exam_result.marks < 95 And parent_IC ='" & id & "'"
            Dim dataA As String = getFieldValueA(sqlA, strConn)
            HiddenFieldA.Value = dataA

            sqlA_minus = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 75 AND exam_result.marks < 85 And parent_IC ='" & id & "'"
            Dim dataA_minus As String = getFieldValueA_minus(sqlA_minus, strConn)
            HiddenFieldA_minus.Value = dataA_minus

            sqlB_plus = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 70 AND exam_result.marks < 75  And parent_IC ='" & id & "'"
            Dim dataB_plus As String = getFieldValueB_plus(sqlB_plus, strConn)
            HiddenFieldB_plus.Value = dataB_plus

            sqlB = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 65 AND exam_result.marks < 70 And parent_IC ='" & id & "'"
            Dim dataB As String = getFieldValueB(sqlB, strConn)
            HiddenFieldB.Value = dataB

            sqlB_minus = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 60 AND exam_result.marks < 65 And parent_IC ='" & id & "'"
            Dim dataB_minus As String = getFieldValueB_minus(sqlB_minus, strConn)
            HiddenFieldB_minus.Value = dataB_minus

            sqlC_plus = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 55 AND exam_result.marks < 60  And parent_IC ='" & id & "'"
            Dim dataC_plus As String = getFieldValueC_plus(sqlC_plus, strConn)
            HiddenFieldC_plus.Value = dataC_plus

            sqlC = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 50 AND exam_result.marks < 55 And parent_IC ='" & id & "'"
            Dim dataC As String = getFieldValueC(sqlC, strConn)
            HiddenFieldC.Value = dataC

            sqlD = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 45 AND exam_result.marks < 50 And parent_IC ='" & id & "'"
            Dim dataD As String = getFieldValueD(sqlD, strConn)
            HiddenFieldD.Value = dataD

            sqlE = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 40 AND exam_result.marks < 45 And parent_IC ='" & id & "'"
            Dim dataE As String = getFieldValueE(sqlE, strConn)
            HiddenFieldE.Value = dataE

            sqlG = "Select COUNT(exam_result.ID) AS count_student From exam_result Left Join course ON course.course_ID=exam_result.course_ID Left Join student_info ON student_info.student_ID=course.student_ID Left Join parent_Info ON parent_Info.parent_ID=student_info.parent_ID Where exam_result.marks >= 0 AND exam_result.marks < 40 And parent_IC ='" & id & "'"
            Dim dataG As String = getFieldValueG(sqlG, strConn)
            HiddenFieldG.Value = dataG

        End If
    End Sub

    Public Function getFieldValueA_plus(ByVal sqlA_plus As String, ByVal MyConnection As String) As String
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

    Public Function getFieldValueA(ByVal sqlA As String, ByVal MyConnection As String) As String
        If sqlA.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlA, conn)
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

    Public Function getFieldValueA_minus(ByVal sqlA_minus As String, ByVal MyConnection As String) As String
        If sqlA_minus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlA_minus, conn)
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

    Public Function getFieldValueB_plus(ByVal sqlB_plus As String, ByVal MyConnection As String) As String
        If sqlB_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlB_plus, conn)
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

    Public Function getFieldValueB(ByVal sqlB As String, ByVal MyConnection As String) As String
        If sqlB.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlB, conn)
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

    Public Function getFieldValueB_minus(ByVal sqlB_minus As String, ByVal MyConnection As String) As String
        If sqlB_minus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlB_minus, conn)
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

    Public Function getFieldValueC_plus(ByVal sqlC_plus As String, ByVal MyConnection As String) As String
        If sqlC_plus.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlC_plus, conn)
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

    Public Function getFieldValueC(ByVal sqlC As String, ByVal MyConnection As String) As String
        If sqlC.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlC, conn)
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

    Public Function getFieldValueD(ByVal sqlD As String, ByVal MyConnection As String) As String
        If sqlD.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlD, conn)
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

    Public Function getFieldValueE(ByVal sqlE As String, ByVal MyConnection As String) As String
        If sqlE.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlE, conn)
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

    Public Function getFieldValueG(ByVal sqlG As String, ByVal MyConnection As String) As String
        If sqlG.Length = 0 Then
            Return "0"
        End If
        Dim conn As SqlConnection = New SqlConnection(MyConnection)
        Dim sqlAdapter As New SqlDataAdapter(sqlG, conn)
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