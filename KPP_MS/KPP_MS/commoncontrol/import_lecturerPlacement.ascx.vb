Imports System.Data.OleDb

Public Class import_lecturerPlacement
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strBil As String = ""
    Dim strStaffName As String = ""
    Dim strStaffMykad As String = ""
    Dim strSubjectName As String = ""
    Dim strSubjectCode As String = ""
    Dim strClassName As String = ""
    Dim strYear As String = ""

    Dim staffID As String = ""
    Dim subjectID As String = ""
    Dim classID As String = ""
    Dim lecturerID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/lecturerPlacement_import/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [placement$]", connection)
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
                    lblMsg.Text = "Kesalahan Kemasukkan Maklumat :<br />" & validationMessage
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

                'Staff_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_Name")) Then
                    strSubjectName = SiteData.Tables(0).Rows(i).Item("Staff_Name")
                Else
                    strMsg += " Please Enter Staff_Name |"
                End If

                'Staff_Mykad
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_Mykad")) Then
                    strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_Mykad")
                Else
                    strMsg += " Please Enter Staff_Mykad |"
                End If

                'Subject_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Name")) Then
                    strStaffMykad = SiteData.Tables(0).Rows(i).Item("Subject_Name")
                Else
                    strMsg += " Please Enter Subject_Name |"
                End If

                'Subject_Code
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Subject_Code")) Then
                    strSubjectCode = SiteData.Tables(0).Rows(i).Item("Subject_Code")
                Else
                    strMsg += " Please Enter Subject_Code |"
                End If

                'Class_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Class_Name")) Then
                    strClassName = SiteData.Tables(0).Rows(i).Item("Class_Name")
                Else
                    strMsg += " Please Enter Class_Name |"
                End If

                'Year
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Year")) Then
                    strYear = SiteData.Tables(0).Rows(i).Item("Year")
                Else
                    strMsg += " Please Enter Year |"
                End If

                If strMsg.Length = 0 Then

                Else
                    strMsg = "BIL# :" & strBil & " Staff Name " & strSubjectName & " : Staff Mykad " & strStaffMykad & " : " & strMsg
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
            strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_Name")
            strStaffMykad = SiteData.Tables(0).Rows(i).Item("Staff_Mykad")
            strSubjectName = SiteData.Tables(0).Rows(i).Item("Subject_Name")
            strSubjectCode = SiteData.Tables(0).Rows(i).Item("Subject_Code")
            strClassName = SiteData.Tables(0).Rows(i).Item("Class_Name")
            strYear = SiteData.Tables(0).Rows(i).Item("Year")

            ''GET STAFF ID
            strSQL = "  SELECT stf_ID FROM staff_Info
                            WHERE staff_Name = '" & strStaffName & "'
                            AND staff_Mykad = '" & strStaffMykad & "'
                            AND staff_Status = 'Access'"
            staffID = oCommon.getFieldValue(strSQL)

            ''GET SUBJECT ID
            strSQL = "  SELECT subject_ID FROM subject_info
                            WHERE (subject_Name = '" & strSubjectName & "'
                            OR subject_NameBM = '" & strSubjectName & "')
                            AND subject_code = '" & strSubjectCode & "'
                            AND subject_year = '" & strYear & "'"
            subjectID = oCommon.getFieldValue(strSQL)

            ''GET CLASS ID
            strSQL = "  SELECT class_ID FROM class_info WHERE class_Name = '" & strClassName & "' AND class_year = '" & strYear & "'"
            classID = oCommon.getFieldValue(strSQL)

            ''IF DATA ALREADY EXIST
            strSQL = "  SELECT ID FROM lecturer WHERE stf_ID = '" & staffID & "' AND subject_ID = '" & subjectID & "' AND class_ID = '" & classID & "' AND lecturer_year = '" & strYear & "'"
            lecturerID = oCommon.getFieldValue(strSQL)
            If oCommon.isExist(strSQL) = True Then
                ''UPDATE
                strSQL = "  UPDATE lecturer SET stf_ID = '" & staffID & "', subject_ID = '" & subjectID & "', class_ID = '" & classID & "', lecturer_year = '" & strYear & "'
                            WHERE ID = '" & lecturerID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then
                    errorData = 0
                    countUpdate = countUpdate + 1
                Else
                    errorData = 1
                End If

            Else
                ''INSERT
                strSQL = "  INSERT INTO lecturer
                                (
                                stf_ID,
                                subject_ID,
                                class_ID,
                                lecturer_year
                                )"
                strSQL += " VALUES
                                (
                                '" & staffID & "',
                                '" & subjectID & "',
                                '" & classID & "',
                                '" & strYear & "'
                                )"
                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = 0 Then
                    errorData = 0
                    countInsert = countInsert + 1
                Else
                    errorData = 1
                End If

            End If

        Next

        Dim value As String = ""

        If errorData = 0 Then

            ShowMessage(countInsert & " rows inserted and " & countUpdate & " rows already exist in database", MessageType.Success)
            value = True

        ElseIf errorData = 1 Then

            ShowMessage("Import failed", MessageType.Success)
            value = False

        End If

        Return value

    End Function

    Private Sub refreshVar()

        strBil = ""
        strStaffName = ""
        strStaffMykad = ""
        strSubjectName = ""
        strSubjectCode = ""
        strClassName = ""
        strYear = ""

        staffID = ""
        subjectID = ""
        classID = ""
        lecturerID = ""

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
        Response.Redirect("download/lecturer_classplacement.xlsx")
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class