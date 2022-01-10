Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class lectuer_koko_attendanceStudent_Detail
    Inherits System.Web.UI.UserControl


    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Dim stfID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                koko_YearRegister()
                Koko_NameRegister()

                PreLoad_Data()

                strRet = BindDataAdd_Restatus(addRespondent)

                Session("getStatus") = "VA"
                previousPage.NavigateUrl = String.Format("~/pengajar_koko_kehadiranPelajar.aspx?stf_ID=" + Request.QueryString("stf_ID"))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub koko_YearRegister()
        strSQL = "SELECT Tahun FROM koko_tahun ORDER BY Tahun ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_YearRegister.DataSource = ds
            ddlKoko_YearRegister.DataTextField = "Tahun"
            ddlKoko_YearRegister.DataValueField = "Tahun"
            ddlKoko_YearRegister.DataBind()
            ddlKoko_YearRegister.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlKoko_YearRegister.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub Koko_NameRegister()

        Dim find_EventYear As String = "Select Tahun from koko_event where EventID = '" & Request.QueryString("EventID") & "'"
        Dim get_EventYear As String = oCommon.getFieldValue_Permata(find_EventYear)

        Dim find_instruktorid As String = " select kokoinstruktorid from koko_instruktor 
                                            left join kolejadmin.dbo.staff_Info on koko_instruktor.MYKAD = kolejadmin.dbo.staff_Info.staff_Mykad
                                            where kolejadmin.dbo.staff_Info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and Tahun = '" & get_EventYear & "'"
        Dim get_instruktorid As String = oCommon.getFieldValue_Permata(find_instruktorid)

        strSQL = "  select koko_kolejpermata.KokoID, koko_kolejpermata.NamaBI from koko_kolejpermata
                    left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.SukanID
                    where koko_kolejpermata.Tahun = '" & get_EventYear & "'
                    and KetuaSukan = 'True'
                    and koko_instruktor.kokoinstruktorid = '" & get_instruktorid & "'
                    Union
                    select koko_kolejpermata.KokoID, koko_kolejpermata.NamaBI from koko_kolejpermata
                    left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.PersatuanID
                    where koko_kolejpermata.Tahun = '" & get_EventYear & "'
                    and KetuaPersatuan = 'True'
                    and koko_instruktor.kokoinstruktorid = '" & get_instruktorid & "'
                    Union
                    select koko_kolejpermata.KokoID, koko_kolejpermata.NamaBI from koko_kolejpermata
                    left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.UniformID
                    where koko_kolejpermata.Tahun = '" & get_EventYear & "'
                    and KetuaUniform = 'True'
                    and koko_instruktor.kokoinstruktorid = '" & get_instruktorid & "'
                    Union
                    select koko_kolejpermata.KokoID, koko_kolejpermata.NamaBI from koko_kolejpermata
                    left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.RumahsukanID
                    where koko_kolejpermata.Tahun = '" & get_EventYear & "'
                    and KetuaRumahsukan = 'True'
                    and koko_instruktor.kokoinstruktorid = '" & get_instruktorid & "'
                    order by koko_kolejpermata.NamaBI asc"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Cocurricular.DataSource = ds
            ddlKoko_Cocurricular.DataTextField = "NamaBI"
            ddlKoko_Cocurricular.DataValueField = "KokoID"
            ddlKoko_Cocurricular.DataBind()
            ddlKoko_Cocurricular.Items.Insert(0, New ListItem("Select Co-Curricular", String.Empty))
            ddlKoko_Cocurricular.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub PreLoad_Data()
        Try

            strSQL = "  Select * from koko_event where EventID = '" & Request.QueryString("EventID") & "' "

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    ddlKoko_YearRegister.SelectedValue = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    ddlKoko_YearRegister.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("EventDate")) Then
                    Dim data_substring As Date = ds.Tables(0).Rows(0).Item("EventDate")

                    txtKoko_DateRegister.Text = data_substring.ToString("dddd dd-MM-yyyy")
                Else
                    txtKoko_DateRegister.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Title")) Then
                    txtKoko_TitleRegister.Text = ds.Tables(0).Rows(0).Item("Title")
                Else
                    txtKoko_TitleRegister.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Agenda")) Then
                    txtKoko_AgendaRegister.Text = ds.Tables(0).Rows(0).Item("Agenda")
                Else
                    txtKoko_AgendaRegister.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KokoID")) Then
                    ddlKoko_Cocurricular.SelectedValue = ds.Tables(0).Rows(0).Item("KokoID")
                Else
                    ddlKoko_Cocurricular.SelectedValue = ""
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Function BindDataAdd_Restatus(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLAdd_Restatus, strConnPermata)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

            run_color()
        Catch ex As Exception
        End Try
        Return True
    End Function

    Private Function getSQLAdd_Restatus() As String

        Dim strField As String = ""

        strSQL = "SELECT Jenis FROM koko_kolejpermata WHERE KokoID = '" & ddlKoko_Cocurricular.SelectedValue & "'"
        Dim strJenis As String = oCommon.getFieldValue_Permata(strSQL)

        Select Case strJenis
            Case "UNIFORM"
                strField = "UniformID"
            Case "PERSATUAN"
                strField = "PersatuanID"
            Case "SUKAN"
                strField = "SukanID"
            Case "RUMAHSUKAN"
                strField = "RumahsukanID"
        End Select

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strGroup As String = ""
        Dim strOrder As String = " ORDER BY kelas, student_Name ASC"

        tmpSQL = "      SELECT
                        koko_pelajar.StudentID,
                        UPPER(kolejadmin.dbo.student_info.student_Name) student_Name,
                        kolejadmin.dbo.student_info.student_Mykad,kolejadmin.dbo.student_info.student_ID, koko_kelas.Kelas,
                        CASE WHEN EXISTS (SELECT koko_eventdetail.EventDetailID FROM koko_eventdetail LEFT OUTER JOIN koko_event ON koko_eventdetail.EventID = koko_event.EventID WHERE koko_eventdetail.StudentID = koko_pelajar.StudentID AND koko_event.KokoID = '" & ddlKoko_Cocurricular.SelectedValue & "' and koko_event.Tahun = '" & ddlKoko_YearRegister.SelectedValue & "' AND koko_eventdetail.EventID = '" & Request.QueryString("EventID") & "') THEN 'Attend' ELSE 'Absent' END AS 'status',
                        CASE WHEN EXISTS (SELECT koko_eventdetail.EventDetailID FROM koko_eventdetail LEFT OUTER JOIN koko_event ON koko_eventdetail.EventID = koko_event.EventID WHERE koko_eventdetail.StudentID = koko_pelajar.StudentID AND koko_event.KokoID = '" & ddlKoko_Cocurricular.SelectedValue & "' and koko_event.Tahun = '" & ddlKoko_YearRegister.SelectedValue & "' AND koko_eventdetail.EventID = '" & Request.QueryString("EventID") & "') THEN 'Attend' ELSE 'Absent' END AS 'statusColor'
                        FROM koko_pelajar
                        LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                        LEFT OUTER JOIN kolejadmin.dbo.student_info ON StudentProfile.MYKAD = kolejadmin.dbo.student_info.student_Mykad
                        LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                        LEFT OUTER JOIN koko_eventdetail ON koko_pelajar.StudentID = koko_eventdetail.StudentID"

        strWhere = "    WHERE
                        koko_pelajar.Tahun = '" & ddlKoko_YearRegister.SelectedValue & "'
                        And koko_pelajar." & strField & " = '" & ddlKoko_Cocurricular.SelectedValue & "'
                        AND kolejadmin.dbo.student_info.student_Status = 'Access'"

        strGroup = "    GROUP BY
                        koko_pelajar.StudentID,
                        kolejadmin.dbo.student_info.student_Name,
                        kolejadmin.dbo.student_info.student_Mykad,kolejadmin.dbo.student_info.student_ID,
                        koko_kelas.Kelas"

        getSQLAdd_Restatus = tmpSQL & strWhere & strGroup & strOrder

        Return getSQLAdd_Restatus
    End Function

    Private Sub run_color()
        Dim col As Integer = 0
        Dim row As Integer = 0
        Dim lblDay As Label

        For row = 0 To addRespondent.Rows.Count - 1 Step row + 1
            lblDay = addRespondent.Rows(row).Cells(6).FindControl("statusColor")
            If lblDay.Text = "Absent" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Orange
                lblDay.ForeColor = Drawing.Color.Orange
                lblDay.CssClass = "lblAbsent"

            End If

            If lblDay.Text = "Attend" Then

                lblDay.Text = "OO"
                lblDay.BackColor = Drawing.Color.Green
                lblDay.ForeColor = Drawing.Color.Green
                lblDay.CssClass = "lblAttend"

            End If
        Next
    End Sub

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Try

            If ValidateForm() = False Then
                Exit Sub
            End If

            Dim find_instruktorid As String = " select InstruktorID from koko_instruktor 
                                                left join kolejadmin.dbo.staff_Info on koko_instruktor.MYKAD = kolejadmin.dbo.staff_Info.staff_Mykad
                                                where kolejadmin.dbo.staff_Info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and Tahun = '" & ddlKoko_YearRegister.SelectedValue & "'"
            Dim get_instruktorid As String = oCommon.getFieldValue_Permata(find_instruktorid)

            Dim Get_Date_SS As String = txtKoko_DateRegister.Text.Substring(txtKoko_DateRegister.Text.Length - 10, 2)
            Dim Get_Year_SS As String = txtKoko_DateRegister.Text.Substring(txtKoko_DateRegister.Text.Length - 4, 4)
            Dim Get_Month_SS As String = txtKoko_DateRegister.Text.Substring(txtKoko_DateRegister.Text.Length - 7, 2)

            If Get_Month_SS = "January" Then
                Get_Month_SS = "01"
            ElseIf Get_Month_SS = "February" Then
                Get_Month_SS = "02"
            ElseIf Get_Month_SS = "March" Then
                Get_Month_SS = "03"
            ElseIf Get_Month_SS = "April" Then
                Get_Month_SS = "04"
            ElseIf Get_Month_SS = "May" Then
                Get_Month_SS = "05"
            ElseIf Get_Month_SS = "June" Then
                Get_Month_SS = "06"
            ElseIf Get_Month_SS = "July" Then
                Get_Month_SS = "07"
            ElseIf Get_Month_SS = "August" Then
                Get_Month_SS = "08"
            ElseIf Get_Month_SS = "September" Then
                Get_Month_SS = "09"
            ElseIf Get_Month_SS = "October" Then
                Get_Month_SS = "10"
            ElseIf Get_Month_SS = "November" Then
                Get_Month_SS = "11"
            ElseIf Get_Month_SS = "December" Then
                Get_Month_SS = "12"
            End If

            Dim Final_Date_Data As String = Get_Year_SS & "-" & Get_Month_SS & "-" & Get_Date_SS

            Dim find_EventID As String = "Select EventID from koko_event where Tahun = '" & ddlKoko_YearRegister.SelectedValue & "' and InstruktorID = '" & get_instruktorid & "' and EventDate = '" & Final_Date_Data & "' and KokoID = '" & ddlKoko_Cocurricular.SelectedValue & "'"
            Dim get_EventID As String = oCommon.getFieldValue_Permata(find_EventID)

            If get_EventID.Length <> 0 Then

                strSQL = "  Update koko_event set Tahun = '" & ddlKoko_YearRegister.SelectedValue & "', InstruktorID = '" & get_instruktorid & "', EventDate = '" & Final_Date_Data & "', 
                            Title = '" & oCommon.FixSingleQuotes(txtKoko_TitleRegister.Text.ToUpper) & "', KokoID = '" & ddlKoko_Cocurricular.SelectedValue & "', Agenda = '" & oCommon.FixSingleQuotes(txtKoko_AgendaRegister.Text) & "' 
                            where EventID = '" & get_EventID & "'"
                strRet = oCommon.ExecuteSQLPermata(strSQL)

                If strRet = "0" Then

                    Dim i As Integer

                    For i = 0 To addRespondent.Rows.Count - 1 Step i + 1
                        Dim chkUpdate As CheckBox = CType(addRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
                        If Not chkUpdate Is Nothing Then
                            ' Get the values of textboxes using findControl
                            Dim strKey As String = addRespondent.DataKeys(i).Value.ToString

                            Dim find_EventDetailID As String = "SELECT EventDetailID from koko_eventdetail where EventID = '" & get_EventID & "' and StudentID = '" & strKey & "' "
                            Dim get_EventDetailID As String = oCommon.getFieldValue_Permata(find_EventDetailID)

                            If chkUpdate.Checked = True Then
                                If get_EventDetailID = "" And get_EventDetailID.Length = 0 Then
                                    If get_EventDetailID.Length = 0 Then
                                        strSQL = "INSERT INTO koko_eventdetail (EventID,StudentID) VALUES('" & get_EventID & "','" & strKey & "')"
                                        strRet = oCommon.ExecuteSQLPermata(strSQL)
                                    End If
                                End If
                            Else
                                If get_EventDetailID <> "" And get_EventDetailID.Length > 0 Then
                                    strSQL = "DELETE koko_eventdetail WHERE EventID='" & get_EventID & "' AND StudentID='" & strKey & "'"
                                    strRet = oCommon.ExecuteSQLPermata(strSQL)
                                End If
                            End If

                        End If
                    Next

                    If strRet = "0" Then
                        ShowMessage(" Successful Update Event", MessageType.Success)
                        strRet = BindDataAdd_Restatus(addRespondent)
                    Else
                        ShowMessage(" Unsuccessful Update Event", MessageType.Error)
                        Exit Sub
                    End If

                Else
                    ShowMessage(" Unsuccessful Update Event", MessageType.Error)
                    Exit Sub
                End If
            Else
                ShowMessage(" The Event Had Not Been Registered", MessageType.Error)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Function ValidateForm() As Boolean

        If ddlKoko_YearRegister.SelectedIndex = 0 Then
            ShowMessage(" Please Select Year", MessageType.Error)
            Return False
        End If

        If txtKoko_DateRegister.Text = "" Then
            ShowMessage(" Please Fill In Event Date", MessageType.Error)
            Return False
        End If

        If ddlKoko_Cocurricular.SelectedIndex = 0 Then
            ShowMessage(" Please Select Cocurricular", MessageType.Error)
            Return False
        End If

        If txtKoko_TitleRegister.Text = "" Then
            ShowMessage(" Please Fill In Event Title", MessageType.Error)
            Return False
        End If

        If txtKoko_AgendaRegister.Text = "" Then
            ShowMessage(" Please Fill In Event Agenda", MessageType.Error)
            Return False
        End If

        Return True
    End Function

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class