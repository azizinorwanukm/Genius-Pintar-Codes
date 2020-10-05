Imports System.Data.OleDb

Public Class import_class
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strBil As String = ""
    Dim strClassName As String = ""
    Dim strClassYear As String = ""
    Dim strClassLevel As String = ""
    Dim strClassType As String = ""
    Dim strStudentNumber As String = ""
    Dim strStaffID As String = ""
    Dim strSubjectID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/class_import/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom() + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [class$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then
                    SaveSiteData(ds)

                Else
                    'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kesalahan Kemasukkan Maklumat Kelas:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblMsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")
                refreshVar()
                strMsg = ""

                'Bil
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Bil")) Then
                    strBil = SiteData.Tables(0).Rows(i).Item("Bil")
                End If

                'Class_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Class_Name")) Then
                    strClassName = SiteData.Tables(0).Rows(i).Item("Class_Name")
                Else
                    strMsg += " Please Enter Class_Name |"
                End If

                'Class_Year
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Class_Year")) Then
                    strClassYear = SiteData.Tables(0).Rows(i).Item("Class_Year")
                Else
                    strMsg += " Please Enter Class_Year |"
                End If

                'Class_Level
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Class_Level")) Then
                    strClassLevel = SiteData.Tables(0).Rows(i).Item("Class_Level")
                Else
                    strMsg += " Please Enter Class_Level |"
                End If

                'Class_Type
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Class_Type")) Then
                    strClassType = SiteData.Tables(0).Rows(i).Item("Class_Type")
                Else
                    strMsg += " Please Enter Class_Type |"
                End If

                'Student_Number
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Student_Number")) Then
                    strStudentNumber = SiteData.Tables(0).Rows(i).Item("Student_Number")
                Else
                    strMsg += " Please Enter Student_Number |"
                End If


                If strMsg.Length = 0 Then

                Else
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim countInsert As Integer = 0
        Dim countUpdate As Integer = 0


        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("Bil")

            strBil = SiteData.Tables(0).Rows(i).Item("Bil")
            strClassName = SiteData.Tables(0).Rows(i).Item("Class_Name")
            strClassYear = SiteData.Tables(0).Rows(i).Item("Class_Year")
            strClassLevel = SiteData.Tables(0).Rows(i).Item("Class_Level")
            strClassType = SiteData.Tables(0).Rows(i).Item("Class_Type")
            strStudentNumber = SiteData.Tables(0).Rows(i).Item("Student_Number")

            strSQL = ""
            strRet = ""

            strSQL = "SELECT class_ID FROM class_info WHERE class_Name = '" & strClassName & "' AND class_year = '" & strClassYear & "' AND class_Level = '" & strClassLevel & "' AND class_type = '" & strClassType & "'"
            Dim classID As String = oCommon.getFieldValue(strSQL)
            If oCommon.isExist(strSQL) = True Then

                'UPDATE SUBJECT INFO
                strSQL = "  UPDATE class_info SET
                            class_Name = '" & strClassName & "',
                            class_year = '" & strClassYear & "',
                            class_Level = '" & strClassLevel & "',
                            class_type = '" & strClassType & "',
                            std_number = '" & strStudentNumber & "'
                            WHERE class_ID = '" & classID & "'"

                countUpdate = countUpdate + 1
                strRet = oCommon.ExecuteSQL(strSQL)

                If strClassType = "Compulsory" Then
                    strSQL = "UPDATE koko_kelas SET Kelas = '" & strClassName & "',Tahun = '" & strClassYear & "' where Kelas = '" & strClassName & "'"
                    strRet = oCommon.ExecuteSQLPermata(strSQL)
                End If

            Else

                'INSERT NEW SUBJECT
                strSQL = "  INSERT INTO class_info
                            (class_Name,
                            class_year,
                            class_Level,
                            class_type,
                            std_number)"

                strSQL += " VALUES 
                            ('" & strClassName & "', 
                            '" & strClassYear & "', 
                            '" & strClassLevel & "', 
                            '" & strClassType & "',
                            '" & strStudentNumber & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then
                    errorData = 0
                    countInsert = countInsert + 1

                    If strClassType = "Compulsory" Then
                        strSQL = "INSERT INTO koko_kelas(Kelas,Tahun) Values('" & strClassName & "','" & strClassYear & "')"
                        strRet = oCommon.ExecuteSQLPermata(strSQL)
                    End If
                Else
                    errorData = 1
                End If

            End If

        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsert & " rows inserted and " & countUpdate & " rows already exist in database ", MessageType.Success)
            value = True

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Success)
            value = False

        End If

        Return value

    End Function

    Private Sub refreshVar()

        strBil = ""
        strClassName = ""
        strClassYear = ""
        strClassLevel = ""
        strClassType = ""
        strStudentNumber = ""
        strStaffID = ""
        strSubjectID = ""

    End Sub

    Private Sub BtnUploaded_ServerClick(sender As Object, e As EventArgs) Handles BtnUploaded.ServerClick
        lblMsg.Text = ""
        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("class") = "info"
            Else
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try
    End Sub

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick
        Response.Redirect("download/ClassProfileBatch.xlsx")
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class