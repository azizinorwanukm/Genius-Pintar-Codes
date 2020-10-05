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
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
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
                
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
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

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        tmpSQL = "SELECT koko_pelajar.kokopelajarid,koko_pelajar.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.NoPelajar,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentGender,StudentProfile.StudentReligion,koko_pelajar.Tahun,koko_pelajar.StatusTawaran,koko_kelas.Kelas,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,"
        tmpSQL += "(SELECT Nama FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan"
        tmpSQL += " FROM koko_pelajar"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID"
        tmpSQL += " LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID"
        strWhere = " WHERE koko_pelajar.Tahun='" & Request.QueryString("tahun") & "'"
        strWhere += " AND koko_pelajar." & strField & "=" & strKokoID


        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Protected Sub btnAssign_Click(sender As Object, e As EventArgs) Handles btnAssign.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(8).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then
                    strSQL = "INSERT INTO koko_eventdetail (EventID,StudentID) VALUES(" & Request.QueryString("eventid") & ",'" & strKey & "')"
                Else
                    strSQL = "DELETE koko_eventdetail WHERE EventID=" & Request.QueryString("eventid") & " AND StudentID='" & strKey & "'"
                End If
                '--execute SQL
                strRet = oCommon.ExecuteSQL(strSQL)
                If Not strRet = "0" Then
                    lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If

        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

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

End Class