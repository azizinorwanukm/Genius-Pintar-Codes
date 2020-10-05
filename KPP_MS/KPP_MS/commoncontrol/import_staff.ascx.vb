Imports System.Data.OleDb

Public Class import_staff
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strBil As String = ""
    Dim strStaffID As String = ""
    Dim strStaffName As String = ""
    Dim strStaffMykad As String = ""
    Dim strStaffGender As String = ""
    Dim strStaffEmail As String = ""
    Dim strStaffMobileNo As String = ""
    Dim strStaffTelNo As String = ""
    Dim strStaffLogin As String = ""
    Dim strStaffPasswordP1 As String = ""
    Dim strStaffPasswordP2 As String = ""
    Dim strStaffPasswordP3 As String = ""
    Dim strStaffYear As String = ""
    Dim strStaffPositionP1 As String = ""
    Dim strStaffPositionP2 As String = ""
    Dim strStaffPositionP3 As String = ""
    Dim strStaffAddress As String = ""
    Dim strStaffCity As String = ""
    Dim strStaffPostalCode As String = ""
    Dim strStaffState As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/staff_import/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [staff$]", connection)
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
                lblMsg.Text = "System Error:" & ex.Message & " Here 1"
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

                'Staff_ID
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_ID")) Then
                    strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_ID")
                Else
                    strMsg += " Please Enter Staff_ID |"
                End If

                'Staff_Name
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_Name")) Then
                    strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_Name")
                Else
                    strMsg += " Please Enter Staff_Name |"
                End If

                'Staff_Mykad
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_Mykad")) Then
                    strStaffMykad = SiteData.Tables(0).Rows(i).Item("Staff_Mykad")
                Else
                    strMsg += " Please Enter Staff_Mykad |"
                End If

                'Staff_Year
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Staff_Year")) Then
                    strStaffGender = SiteData.Tables(0).Rows(i).Item("Staff_Year")
                Else
                    strMsg += " Please Enter Staff_Year |"
                End If

                If strMsg.Length = 0 Then

                Else
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message & "Here 2"
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
            strStaffID = SiteData.Tables(0).Rows(i).Item("Staff_ID")
            strStaffName = SiteData.Tables(0).Rows(i).Item("Staff_Name")
            strStaffMykad = SiteData.Tables(0).Rows(i).Item("Staff_Mykad")
            strStaffGender = SiteData.Tables(0).Rows(i).Item("Staff_Gender")
            strStaffEmail = SiteData.Tables(0).Rows(i).Item("Staff_Email")
            strStaffMobileNo = SiteData.Tables(0).Rows(i).Item("Staff_MobileNo")
            strStaffTelNo = SiteData.Tables(0).Rows(i).Item("Staff_TelNo")

            strStaffLogin = Split(strStaffName, " ")(0) & "@UKM"

            strStaffPositionP1 = SiteData.Tables(0).Rows(i).Item("Staff_Position1")
            strStaffPositionP2 = SiteData.Tables(0).Rows(i).Item("Staff_Position2")
            strStaffPositionP3 = SiteData.Tables(0).Rows(i).Item("Staff_Position3")

            strStaffAddress = SiteData.Tables(0).Rows(i).Item("Staff_Address")
            strStaffCity = SiteData.Tables(0).Rows(i).Item("Staff_City")
            strStaffState = SiteData.Tables(0).Rows(i).Item("Staff_State")
            strStaffPostalCode = SiteData.Tables(0).Rows(i).Item("Staff_PostalCode")

            Dim staffAccess_data_pos1 As String = ""
            Dim staffAccess_data_pos2 As String = ""
            Dim staffAccess_data_pos3 As String = ""

            If strStaffPositionP1 = "ADMIN" Then
                strStaffPasswordP1 = oCommon.pswrd_random()

                strSQL = "select Parameter from setting where Value = '" & strStaffPositionP1 & "' and Type = 'Admin Access'"
                staffAccess_data_pos1 = oCommon.getFieldValue(strSQL)
            Else
                strStaffPositionP1 = "- None -"
            End If

            If strStaffPositionP2 = "KOKURIKULUM" Or strStaffPositionP2 = "PPE" Or strStaffPositionP2 = "HEA" Or strStaffPositionP2 = "HEP" Or strStaffPositionP2 = "KSLR" Or strStaffPositionP2 = "SUP" Or strStaffPositionP2 = "SUD" Or strStaffPositionP2 = "PENGARAH" Or strStaffPositionP2 = "TIMBALAN PENGARAH" Then
                strStaffPasswordP2 = oCommon.pswrd_random()
                staffAccess_data_pos2 = oCommon.getFieldValue(strSQL)
            Else
                strStaffPositionP2 = "- None -"
            End If

            If strStaffPositionP3 = "INSTRUKTOR KPP" Or strStaffPositionP3 = "INSTRUKTOR KPP - SEMENTARA" Or strStaffPositionP3 = "PENSYARAH" Then
                strStaffPasswordP3 = oCommon.pswrd_random()
                staffAccess_data_pos3 = oCommon.getFieldValue(strSQL)
            Else
                strStaffPositionP3 = "- None -"
            End If

            strSQL = "SELECT stf_ID FROM staff_Info WHERE staff_Mykad = '" & strStaffMykad & "' AND staff_Status = 'Access'"
            If oCommon.isExist(strSQL) = True Then
                'UPDATE STAFF INFO
                strSQL = "SELECT stf_ID FROM staff_Info WHERE staff_Mykad = '" & strStaffMykad & "' AND staff_Status = 'Access'"
                Dim id As String = oCommon.getFieldValue(strSQL)

                If id.Length > 0 Then

                    strSQL = "  UPDATE staff_Info SET
                                    staff_ID = '" & strStaffID & "',
                                    staff_Name = UPPER('" & strStaffName & "'),
                                    staff_Email = '" & strStaffEmail & "',
                                    staff_Sex = UPPER('" & strStaffGender & "'),
                                    staff_MobileNo = '" & oCommon.FixSingleQuotes(strStaffMobileNo) & "',
                                    staff_TelNo = '" & oCommon.FixSingleQuotes(strStaffTelNo) & "',
                                    staff_Address = UPPER('" & oCommon.FixSingleQuotes(strStaffAddress) & "'),
                                    staff_City = UPPER('" & strStaffCity & "'),
                                    staff_Posscode = '" & strStaffPostalCode & "',
                                    staff_Position1 = '" & staffAccess_data_pos1 & "',
                                    staff_Position2 = '" & staffAccess_data_pos2 & "',
                                    staff_Position3 = '" & staffAccess_data_pos3 & "',
                                    staff_State = UPPER('" & strStaffState & "')"

                    strSQL += " WHERE stf_ID = '" & id & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)


                    If strRet = 0 Then
                        errorData = 0
                        countUpdate = countUpdate + 1
                    Else
                        errorData = 1
                    End If

                End If

            Else
                'INSERT NEW STAFF
                strSQL = "  INSERT INTO staff_Info(staff_ID, staff_Name, staff_Mykad, staff_Email, staff_Sex, staff_MobileNo, staff_TelNo,
                            staff_Address, staff_City, staff_Posscode, staff_State, staff_Photo, staff_Status, staff_LoginAttempt,staff_Position1,staff_Position2,staff_Postion3)"

                strSQL += " VALUES 
                                ('" & strStaffID & "', 
                                UPPER('" & oCommon.FixSingleQuotes(strStaffName) & "'), 
                                '" & strStaffMykad & "', 
                                '" & oCommon.FixSingleQuotes(strStaffEmail) & "',
                                UPPER('" & strStaffGender & "'),
                                '" & oCommon.FixSingleQuotes(strStaffMobileNo) & "', 
                                '" & oCommon.FixSingleQuotes(strStaffTelNo) & "',
                                UPPER('" & oCommon.FixSingleQuotes(strStaffAddress) & "'), 
                                UPPER('" & strStaffCity & "'), 
                                UPPER('" & strStaffPostalCode & "'), 
                                UPPER('" & strStaffState & "'), 
                                '~/staff_Image/user.png', 
                                'Access',  
                                '0','" & staffAccess_data_pos1 & "','" & staffAccess_data_pos2 & "','" & staffAccess_data_pos3 & "')"

                strRet = oCommon.ExecuteSQL(strSQL)

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ''INSERT STAFF LOGIN

                Dim find_stfID As String = "select stf_ID from staff_info where staff_Mykad = '" & strStaffMykad & "' and staff_Status = 'Access'"
                Dim get_sftfID As String = oCommon.getFieldValue(find_stfID)

                If strStaffPositionP1 <> "- None -" Then
                    strSQL = "INSERT INTO staff_Login (stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status,staff_LoginAttempt)
                              VALUES ('" & get_sftfID & "','" & strStaffLogin & "','" & strStaffPasswordP1 & "','- None -','Position 1','Access','0')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

                If strStaffPositionP2 <> "- None -" Then
                    strSQL = "INSERT INTO staff_Login (stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status,staff_LoginAttempt)
                              VALUES ('" & get_sftfID & "','" & strStaffLogin & "','" & strStaffPasswordP2 & "','- None -','Position 2','Access','0')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

                If strStaffPositionP3 <> "- None -" Then
                    strSQL = "INSERT INTO staff_Login (stf_ID,staff_Login,staff_Password,staff_Access,staff_PositionNo,staff_Status,staff_LoginAttempt)
                              VALUES ('" & get_sftfID & "','" & strStaffLogin & "','" & strStaffPasswordP3 & "','- None -','Position 3','Access','0')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

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
        strStaffID = ""
        strStaffName = ""
        strStaffMykad = ""
        strStaffGender = ""
        strStaffEmail = ""
        strStaffMobileNo = ""
        strStaffTelNo = ""
        strStaffLogin = ""
        strStaffPasswordP1 = ""
        strStaffPasswordP2 = ""
        strStaffPasswordP3 = ""
        strStaffYear = ""
        strStaffPositionP1 = ""
        strStaffPositionP2 = ""
        strStaffPositionP3 = ""
        strStaffAddress = ""
        strStaffCity = ""
        strStaffPostalCode = ""
        strStaffState = ""

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
            lblMsg.Text = "System Error:" & ex.Message & " Here Upload "

        End Try
    End Sub

    Private Sub BtnDownload_ServerClick(sender As Object, e As EventArgs) Handles BtnDownload.ServerClick
        Response.Redirect("download/staff_info.xlsx")
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum
End Class