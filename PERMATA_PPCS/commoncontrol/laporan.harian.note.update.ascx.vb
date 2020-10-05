Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class laporan_harian_note_update
    Inherits System.Web.UI.UserControl

    Private rm As ResourceManager

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer = 0
    Dim strcourseCode As String
    Dim strTokenid As String = ""
    Dim strppcsevalid As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strppcsevalid = Request.QueryString("ppcsevalid")

            ''--close window
            WindowClose(btnClose)

            Dim strUserType As String = Server.HtmlEncode(Request.Cookies("ppcs_usertype").Value)

            Select Case strUserType
                Case "PENGURUS AKADEMIK"
                    setRemarks(txtRemarksPengurusAkademik, True)
                Case "KETUA MODUL"
                    setRemarks(txtRemarksKetuaModul, True)
                Case "PENGAJAR"
                    setRemarks(txtRemarksPengajar, True)
                Case "PEMBANTU PENGAJAR"
                    setRemarks(txtRemarksPengurusAkademik, False)
                    lblMsg.Text = "View only"
                    btnUpdate.Visible = False
                Case Else
                    setRemarks(txtRemarksPengurusAkademik, False)
                    lblMsg.Text = "View only"
                    btnUpdate.Visible = False
            End Select

            If Not IsPostBack Then
                '--load answers
                ppcs_eval_load(strppcsevalid)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ppcs_eval_load(ByVal strValue As String)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM ppcs_eval_daily WHERE ppcsevalid=" & strValue
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("RemarksPengurusAkademik")) Then
                    txtRemarksPengurusAkademik.Text = MyTable.Rows(nRows).Item("RemarksPengurusAkademik").ToString
                Else
                    txtRemarksPengurusAkademik.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("RemarksKetuaModul")) Then
                    txtRemarksKetuaModul.Text = MyTable.Rows(nRows).Item("RemarksKetuaModul").ToString
                Else
                    txtRemarksKetuaModul.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("RemarksPengajar")) Then
                    txtRemarksPengajar.Text = MyTable.Rows(nRows).Item("RemarksPengajar").ToString
                Else
                    txtRemarksPengajar.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub


    Private Sub setRemarks(ByVal txtEnable As TextBox, ByVal bEnable As Boolean)

        txtRemarksPengurusAkademik.Enabled = False
        txtRemarksPengurusAkademik.BackColor = Drawing.Color.LightGray

        txtRemarksKetuaModul.Enabled = False
        txtRemarksKetuaModul.BackColor = Drawing.Color.LightGray

        txtRemarksPengajar.Enabled = False
        txtRemarksPengajar.BackColor = Drawing.Color.LightGray

        ''--enable and set back color back
        txtEnable.Enabled = bEnable
        If bEnable = True Then
            txtEnable.BackColor = Drawing.Color.White
        Else
            txtEnable.BackColor = Drawing.Color.LightGray
        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        strSQL = "UPDATE ppcs_eval_daily WITH (UPDLOCK) SET RemarksPengurusAkademik='" & oCommon.FixSingleQuotes(txtRemarksPengurusAkademik.Text) & "',RemarksKetuaModul='" & oCommon.FixSingleQuotes(txtRemarksKetuaModul.Text) & "',RemarksPengajar='" & oCommon.FixSingleQuotes(txtRemarksPengajar.Text) & "' WHERE ppcsevalid=" & strppcsevalid
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Nota berjaya dikemaskini."
        Else
            lblMsg.Text = strRet
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click

    End Sub

    Public Shared Sub WindowClose(ByVal opener As System.Web.UI.WebControls.WebControl)
        Dim clientScript As String


        'Building the client script- window.close, with additional parameters
        clientScript = "window.close();return false;"
        'regiter the script to the clientside click event of the 'opener' control
        opener.Attributes.Add("onClick", clientScript)
    End Sub

End Class