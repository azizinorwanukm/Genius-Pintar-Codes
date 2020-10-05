Imports System.Data.OleDb

Public Class Import_Student_Data_Temporary
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strBil As String = ""
    Dim strNAMA As String = ""
    Dim strKP As String = ""
    Dim strMATRIK As String = ""
    Dim strSTDID As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub BtnUploaded_ServerClick(sender As Object, e As EventArgs) Handles BtnUploaded.ServerClick
        lblMsg.Text = ""
        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("exam") = "info"
            Else
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try
    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/import/class_import/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [Sheet1$]", connection)
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
                    divMsg.Attributes("exam") = "error"
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
            divMsg.Attributes("exam") = "error"
            lblMsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String

        Dim strMsg As String = ""
        Dim sb As StringBuilder = New StringBuilder()

        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")
            refreshVar()
            strMsg = ""

            'Bil
            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("BIL")) Then
                strBil = SiteData.Tables(0).Rows(i).Item("BIL")
            End If

            'NAMA
            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("NAMA_STUDENT")) Then
                strNAMA = SiteData.Tables(0).Rows(i).Item("NAMA_STUDENT")
            End If

            'NO KP
            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("NO_KP")) Then
                strKP = SiteData.Tables(0).Rows(i).Item("NO_KP")
            End If

            'NO MATRIK
            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("NO_MATRIK")) Then
                strMATRIK = SiteData.Tables(0).Rows(i).Item("NO_MATRIK")
            End If

            'STD ID
            If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("STD_ID")) Then
                strSTDID = SiteData.Tables(0).Rows(i).Item("STD_ID")
            End If

            If strMsg.Length = 0 Then

            Else
                strMsg += "<br/>"
            End If

            sb.Append(strMsg)

        Next

        Return sb.ToString()


    End Function

    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""

        Dim display As String = ""
        Dim errorData As Integer = 0

        Dim countInsert As Integer = 0
        Dim countUpdate As Integer = 0


        Dim sb As StringBuilder = New StringBuilder()
        For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("BIL")

            strBil = SiteData.Tables(0).Rows(i).Item("BIL")
            strNAMA = SiteData.Tables(0).Rows(i).Item("NAMA_STUDENT")
            strKP = SiteData.Tables(0).Rows(i).Item("NO_KP")
            strMATRIK = SiteData.Tables(0).Rows(i).Item("NO_MATRIK")
            strSTDID = SiteData.Tables(0).Rows(i).Item("STD_ID")

            'UPDATE STUDENT DATA

            strSQL = "UPDATE student_info set student_Name = '" & oCommon.FixSingleQuotes(strNAMA) & "' ,student_Mykad = '" & strKP & "' ,student_ID = '" & strMATRIK & "' where std_ID = '" & strSTDID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = 0 Then
                errorData = 0
                countInsert = countInsert + 1
            Else
                errorData = 1
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
        strNAMA = ""
        strKP = ""
        strMATRIK = ""
        strSTDID = ""

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class