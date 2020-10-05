Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class ppcs_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            strRet = BindData(datRespondent)
            Dim strQuery As String = "SELECT StudentID FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(Request.QueryString("mykad")) & "'"
            Dim strStudentID As String = oCommon.getFieldValue(strQuery)

            strSQL = "SELECT TOP 1 PPCSStatus FROM PPCS WITH (NOLOCK) WHERE StudentID='" & strStudentID & "' AND DisplayStatus='Y' ORDER BY PPCSID DESC"
            Dim strPPCSStatus As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT TOP 1 PPCSDate FROM PPCS WITH (NOLOCK) WHERE StudentID='" & strStudentID & "' AND DisplayStatus='Y' ORDER BY PPCSID DESC"
            Dim strPPCSDate As String = oCommon.getFieldValue(strSQL)

            If strPPCSDate = "PPCS MEI-JUN 2016" Then
                lblMsg.Text = "SIMPANAN: Melepasi skor kelayakan minimum, pelajar akan ditawarkan jika ada calon LAYAK menolak. Pelajar akan dimaklumkan melalui emel. Sila pastikan emel dan nombor telefon yang betul di <a href='http://pelajar.permatapintar.edu.my' target='_blank'>Laman Rasmi Pelajar PERMATApintar</a>"
            ElseIf strPPCSDate = "PPCS MEI-JUN 2016" And strPPCSStatus = "SIMPANAN"
                lblMsg.Text = "SIMPANAN: Melepasi skor kelayakan minimum, pelajar akan ditawarkan jika ada calon LAYAK menolak. Pelajar akan dimaklumkan melalui emel. Sila pastikan emel dan nombor telefon yang betul di <a href='http://pelajar.permatapintar.edu.my' target='_blank'>Laman Rasmi Pelajar PERMATApintar</a>"
            ElseIf strPPCSDate = "PPCS DIS 2015" And strPPCSStatus = "SIMPANAN"
                lblMsg.Text = "SIMPANAN: Melepasi skor kelayakan minimum, pelajar akan ditawarkan jika ada calon LAYAK menolak. Pelajar akan dimaklumkan melalui emel. Sila pastikan emel dan nombor telefon yang betul di <a href='http://pelajar.permatapintar.edu.my' target='_blank'>Laman Rasmi Pelajar PERMATApintar</a>"
            ElseIf strPPCSDate = "PPCS DIS 2015" And strPPCSStatus = "LAYAK"
                'layak
                lblMsg.Text = ""
            End If

            'DOB 2007
            strSQL = "SELECT TOP 1 DOB_Year FROM StudentProfile WHERE  MYKAD='" & oCommon.FixSingleQuotes(Request.QueryString("mykad")) & "'"
            Dim strPPCSDOB As String = oCommon.getFieldValue(strSQL)
            If strPPCSDOB = "2007" Then
                lblMsg.Text = "Bagi pelajar lahir tahun 2007, keputusan tidak dikeluarkan di laman semakan ini, tetapi pelajar akan dipanggil oleh pihak USIM untuk ujian USIM1"
            End If
        Catch ex As Exception
        End Try

    End Sub


    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Tidak Layak!"
            Else
                'lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        '--get StudentID
        Dim strQuery As String = "SELECT StudentID FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(Request.QueryString("mykad")) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strQuery)

        '--once assign pusat baru display kepada pelajar.
        'tmpSQL = "SELECT PPCSID,StudentID,PPCSDate,PPCSStatus,StatusTawaran FROM PPCS"
        'change date on 20150904
        tmpSQL = "SELECT TOP 1 PPCSID,StudentID,PPCSDate,PPCSStatus,StatusTawaran FROM PPCS"
        strWhere = " WITH (NOLOCK) WHERE StudentID='" & strStudentID & "' AND DisplayStatus='Y'"
        strOrder = " ORDER BY PPCSID DESC"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ' Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Dim strQuery As String = "SELECT StudentID FROM StudentProfile WHERE MYKAD='" & oCommon.FixSingleQuotes(Request.QueryString("mykad")) & "'"
        Dim strStudentID As String = oCommon.getFieldValue(strQuery)

        strSQL = "SELECT TOP 1 PPCSStatus FROM PPCS WITH (NOLOCK) WHERE StudentID='" & strStudentID & "' AND DisplayStatus='Y' ORDER BY PPCSID DESC"
        Dim strPPCSStatus As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT TOP 1 PPCSDate FROM PPCS WITH (NOLOCK) WHERE StudentID='" & strStudentID & "' AND DisplayStatus='Y' ORDER BY PPCSID DESC"
        Dim strPPCSDate As String = oCommon.getFieldValue(strSQL)

        If strPPCSDate = "PPCS MEI-JUN 2016" Then
        ElseIf strPPCSDate = "PPCS MEI-JUN 2016" And strPPCSStatus = "SIMPANAN"
        ElseIf strPPCSDate = "PPCS DIS 2015" And strPPCSStatus = "SIMPANAN"
        ElseIf strPPCSDate = "PPCS DIS 2015" And strPPCSStatus = "LAYAK"
            Response.Redirect("ppcs.result.aspx?mykad=" & Request.QueryString("mykad") & "&ppcsid=" & strKeyID)
        End If
    End Sub

End Class