Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class event_pelajar_select
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strKelasID As String = ""
    Dim strTahun As String = ""
    Dim strKelas As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                getKOKOName()
                '--default
                strRet = BindData(datRespondent)

                '--title for printing
                strSQL = "SELECT EventDate,Title FROM koko_event WHERE EventID=" & Request.QueryString("eventid")
                lblTitle.Text = oCommon.getFieldValueEx(strSQL)

                lblPrintDate.Text = oCommon.getNow
                setAccessRight()

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub setAccessRight()
        Dim strInstruktorID As String = ""
        strSQL = "SELECT InstruktorID FROM koko_instruktor WHERE LoginID='" & CType(Session.Item("koko_loginid"), String) & "' AND Tahun='" & Request.QueryString("tahun") & "'"
        strInstruktorID = oCommon.getFieldValue(strSQL)

        If strInstruktorID = Request.QueryString("instruktorid") Then
            btnUpdate.Enabled = True
        Else
            btnUpdate.Enabled = False
            btnUpdate.CssClass = "fbbuttondisable"
        End If

    End Sub

    '--get kokoname
    Private Sub getKOKOName()
        Dim strField As String = ""
        Dim strEventID As String = Request.QueryString("eventid")
        '--KokoID
        strSQL = "SELECT KokoID FROM koko_event WHERE EventID=" & strEventID
        Dim strKokoID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Nama FROM koko_kolejpermata WHERE KokoID=" & strKokoID
        lblKOKOName.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then

                lblMsg.Text = "Tiada rekod pelajar."
            Else

                lblMsg.Text = "Jumlah rekod#: " & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim strField As String = ""
        Dim strEventID As String = Request.QueryString("eventid")
        '--KokoID
        strSQL = "SELECT KokoID FROM koko_event WHERE EventID=" & strEventID
        Dim strKokoID As String = oCommon.getFieldValue(strSQL)
        '--
        strSQL = "SELECT Jenis FROM koko_kolejpermata WHERE KokoID=" & strKokoID
        Dim strJenis As String = oCommon.getFieldValue(strSQL)
        '--
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

        Dim kelaskokoid As String = Request.QueryString("kelaskokoid")
        Dim instruktorid As String = Request.QueryString("instruktorid")
        Dim kokoinstruktorid As String = getkokoinstruktorid(instruktorid)
        Dim tahun As String = Request.QueryString("tahun")

        Dim kokoid As String = getKokoID(kelaskokoid)

        Dim find_IsMandatory As String = "  SELECT IsMandatory from koko_kolejpermata
                                            LEFT JOIN koko_event ON koko_kolejpermata.KokoID = koko_event.KokoID where EventId = '" & strEventID & "'"
        Dim get_IsMandatory As String = oCommon.getFieldValue(find_IsMandatory)

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strGroup As String = ""
        Dim strOrder As String = " ORDER BY koko_kelas.Kelas ASC"

        '' only for mandatory sport
        If get_IsMandatory = "Y" Then
            tmpSQL = "  SELECT 
                        StudentProfile.StudentID,kolejadmin.dbo.student_info.student_Name,
                        kolejadmin.dbo.student_info.student_Mykad,kolejadmin.dbo.student_info.student_ID,
                        koko_kelas.Kelas,koko_event.EventID,
                        CASE WHEN EXISTS (SELECT koko_eventdetail.EventDetailID FROM koko_eventdetail WHERE koko_eventdetail.StudentID = StudentProfile.StudentID AND koko_eventdetail.EventID = '" & strEventID & "') THEN 'HADIR' ELSE 'TIDAK HADIR' END AS 'Status'
                        FROM StudentProfile
                        LEFT JOIN kolejadmin.dbo.student_info ON StudentProfile.MYKAD = kolejadmin.dbo.student_info.student_Mykad
                        LEFT JOIN koko_pelajar ON StudentProfile.StudentID = koko_pelajar.StudentID
                        LEFT JOIN koko_kelas ON koko_pelajar.KelasID = koko_kelas.KelasID
                        LEFT JOIN koko_kumpulan ON StudentProfile.StudentID = koko_kumpulan.StudentID
                        LEFT JOIN koko_kelaskoko ON koko_kumpulan.KelasKokoID = koko_kelaskoko.KelasKokoID
                        LEFT JOIN koko_kolejpermata ON koko_kelaskoko.KokoID = koko_kolejpermata.KokoID
                        LEFT JOIN koko_event ON koko_kolejpermata.KokoID = koko_event.KokoID
                        WHERE koko_kumpulan.KelasKokoID = '" & kelaskokoid & "'
                        AND koko_kelaskoko.kokoinstruktorid = '" & kokoinstruktorid & "'
                        AND koko_kolejpermata.Tahun = '" & tahun & "'
                        AND koko_event.EventID = '" & strEventID & "'
                        AND kolejadmin.dbo.student_info.student_Status = 'Access'"
        Else
            tmpSQL = "SELECT
                        koko_pelajar.StudentID,
                        kolejadmin.dbo.student_info.student_Name,
                        kolejadmin.dbo.student_info.student_Mykad,kolejadmin.dbo.student_info.student_ID,
                        koko_kelas.Kelas,
                        CASE WHEN EXISTS (SELECT koko_eventdetail.EventDetailID FROM koko_eventdetail WHERE koko_eventdetail.StudentID = koko_pelajar.StudentID AND koko_eventdetail.EventID = '" & strEventID & "') THEN 'HADIR' ELSE 'TIDAK HADIR' END AS 'Status'
                        FROM koko_pelajar
                        LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                        LEFT OUTER JOIN kolejadmin.dbo.student_info ON StudentProfile.MYKAD = kolejadmin.dbo.student_info.student_Mykad
                        LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                        LEFT OUTER JOIN koko_eventdetail ON koko_pelajar.StudentID = koko_eventdetail.StudentID"
            strWhere = " WHERE
                        koko_pelajar.Tahun = '" & tahun & "'
                        And koko_pelajar." & strField & "='" & strKokoID & "'"
            strGroup = " GROUP BY
                        koko_pelajar.StudentID,
                        kolejadmin.dbo.student_info.student_Name,
                        kolejadmin.dbo.student_info.student_Mykad,kolejadmin.dbo.student_info.student_ID,
                        koko_kelas.Kelas"
        End If

        getSQL = tmpSQL & strWhere & strGroup & strOrder

        Return getSQL

    End Function

    Private Function getkokoinstruktorid(ByVal instruktorid As String) As String
        strSQL = "SELECT kokoinstruktorid FROM koko_instruktor WHERE InstruktorID = '" & instruktorid & "' AND Tahun = '" & Request.QueryString("tahun") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Private Function getKokoID(ByVal kelaskokoid As String) As String
        strSQL = "SELECT KokoID FROM koko_kelaskoko WHERE KelasKokoID = '" & kelaskokoid & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Private Sub datRespondent_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datRespondent.RowDataBound

        '--set checked
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim chkSelect As CheckBox

            Dim i As Integer = e.Row.RowIndex + 1
            Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString  '--StudentID
            chkSelect = e.Row.FindControl("chkSelect")

            strSQL = "SELECT EventDetailID FROM koko_eventdetail WHERE EventID=" & Request.QueryString("eventid") & " AND StudentID='" & strKeyID & "'"
            If oCommon.isExist(strSQL) Then
                chkSelect.Checked = True
            End If

        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        Dim strEventID As String = ""
        Dim strPPCSdate As String = ""
        Dim strDate As String = ""
        Dim strProgram As String = ""
        Dim strKelasID As String = ""
        Dim strSukanID As String = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1

            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(4).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString

                Dim find_EventDetailID As String = "SELECT EventDetailID from koko_eventdetail where EventID = '" & Request.QueryString("eventid") & "' and StudentID = '" & strKey & "' "
                Dim get_EventDetailID As String = oCommon.getFieldValue(find_EventDetailID)

                If chkUpdate.Checked = True Then
                    If chkUpdate.Checked = True Then
                        If get_EventDetailID.Length = 0 Then
                            strSQL = "INSERT INTO koko_eventdetail (EventID,StudentID) VALUES('" & Request.QueryString("eventid") & "','" & strKey & "')"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    End If
                Else
                    If get_EventDetailID <> "" And get_EventDetailID.Length > 0 Then
                        strSQL = "DELETE koko_eventdetail WHERE EventID='" & Request.QueryString("eventid") & "' AND StudentID='" & strKey & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If
            End If

            ''for mandatory sport 
            'If get_IsMandatory = "Y" Then
            '    If chkUpdate.Checked = True Then
            '        strSQL = "UPDATE koko_pelajarmandatory SET Kehadiran = 'HADIR' WHERE StudentID = '" & strKeyID & "' AND EventID = '" & Request.QueryString("eventid") & "'"
            '    ElseIf chkUpdate.Checked = False Then
            '        strSQL = "UPDATE koko_pelajarmandatory SET Kehadiran = 'TIDAK HADIR' WHERE StudentID = '" & strKeyID & "' AND EventID = '" & Request.QueryString("eventid") & "'"
            '    End If
            'Else
            '    If chkUpdate.Checked = True Then
            '        strSQL = "INSERT INTO koko_eventdetail (EventID,StudentID) VALUES('" & Request.QueryString("eventid") & "','" & strKeyID & "')"
            '    Else
            '        strSQL = "DELETE koko_eventdetail WHERE EventID='" & Request.QueryString("eventid") & "' AND StudentID='" & strKeyID & "'"
            '    End If
            'End If

            If Not strRet = "0" Then
                lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    Private Function getEventID(ByVal strKeyID As String) As String
        strSQL = "SELECT EventID FROM koko_event WHERE KokoID = '" & Request.QueryString("kokoid") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function
    Private Function getPPCSdate(ByVal strKeyID As String) As String
        strSQL = "SELECT PPCSDate FROM koko_pelajar WHERE StudentID = '" & strKeyID & "' AND Tahun = '" & Request.QueryString("tahun") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function
    Private Function getDate(ByVal strEventID As String) As String
        strSQL = "SELECT EventDate FROM koko_event WHERE EventID = '" & strEventID & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function

    Private Function getProgram(ByVal strKeyID As String) As String
        strSQL = "SELECT Program FROM koko_pelajar WHERE StudentID = '" & strKeyID & "' AND Tahun = '" & Request.QueryString("tahun") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function
    Private Function getKelasID(ByVal strKeyID As String) As String
        strSQL = "SELECT KelasID FROM koko_pelajar WHERE StudentID = '" & strKeyID & "' AND Tahun = '" & Request.QueryString("tahun") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function
    Private Function getSukanID(ByVal strKeyID As String) As String
        strSQL = "SELECT SukanID FROM koko_pelajar WHERE StudentID = '" & strKeyID & "' AND Tahun = '" & Request.QueryString("tahun") & "'"
        strRet = oCommon.getFieldValue(strSQL)
        Return strRet
    End Function
End Class