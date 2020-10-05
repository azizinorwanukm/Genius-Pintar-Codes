Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class ukm3_transfer
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnTransfer.Attributes.Add("onclick", "return confirm('Pasti ingin transfer ke UKM3?');")
        btnTransferSelect.Attributes.Add("onclick", "return confirm('Pasti ingin transfer pelajar pilihan ke UKM3?');")

        Try
            If Not IsPostBack Then
                ppcsdate_list()
                ddlPPCSDate.Text = oCommon.getAppsettings("DefaultPPCSDate")
                lblPPCSDateSearch.Text = ddlPPCSDate.Text

                koko_tahun_list()
                ddlTahun.Text = oCommon.getAppsettings("DefaultKOKOYear")

                lblIsTransfered.Text = isTransfered.ToString

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Function isTransfered() As Boolean
        '--transfer if only koko_pelajar record not exist
        strSQL = "SELECT kokopelajarid FROM koko_pelajar WHERE PPCSDate='" & ddlPPCSDate.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub koko_tahun_list()
        strSQL = "SELECT Tahun FROM koko_tahun ORDER BY Tahun ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

            'ddlTahun.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ppcsdate_list()
        strSQL = "SELECT PPCSDate FROM master_PPCSDate ORDER BY ppcsid ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPPCSDate.DataSource = ds
            ddlPPCSDate.DataTextField = "PPCSDate"
            ddlPPCSDate.DataValueField = "PPCSDate"
            ddlPPCSDate.DataBind()

            '--ddlPPCSDate.Items.Add(New ListItem("ALL", "ALL"))

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex

        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        strRet = BindData(datRespondent)
        lblIsTransfered.Text = isTransfered.ToString

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
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY StudentProfile.StudentFullname"

        tmpSQL = "SELECT UKM3.StudentID,UKM3.TotalPercentage,UKM3.PPMT,UKM3.Program,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.NoPelajar,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentGender,StudentProfile.StudentReligion FROM UKM3"
        tmpSQL += " LEFT OUTER JOIN StudentProfile ON UKM3.StudentID=StudentProfile.StudentID"
        strWhere = " WHERE UKM3.PPMT='Y' AND UKM3.PPCSDate='" & ddlPPCSDate.Text & "' AND UKM3.StatusTawaran = 'TERIMA'"

        '--StudentFullname
        If Not txtStudentFullname.Text.Length = 0 Then
            strWhere += " AND StudentProfile.StudentFullname LIKE '%" & oCommon.FixSingleQuotes(txtStudentFullname.Text) & "%'"
        End If

        '--MYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND StudentProfile.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub btnTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        '--transfer if only koko_pelajar record not exist
        If isTransfered() = False Then
            koko_pelajar_insert()
        Else
            lblMsg.Text = "Rekod UKM3 sudah ditransfer ke kokurikulum pelajar sebelum ini."
        End If

    End Sub

    Private Sub koko_pelajar_insert()
        Dim tmpSQL As String = ""

        Try
            tmpSQL = "INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, StatusTawaran)"
            tmpSQL += " SELECT StudentID,PPCSDate,'" & ddlTahun.Text & "',Program,StatusTawaran FROM UKM3"
            tmpSQL += " WHERE UKM3.PPMT='Y' AND UKM3.PPCSDate ='" & ddlPPCSDate.Text & "' ORDER BY StudentID"

            strRet = oCommon.ExecuteSQL(tmpSQL)
            If Not strRet = "0" Then
                lblMsg.Text = "System error: " & strRet
            Else
                lblMsg.Text = "Berjaya memindahkan pelajar ke koko_pelajar!"
            End If

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub btnTransferSelect_Click(sender As Object, e As EventArgs) Handles btnTransferSelect.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""
        Dim tmpSQL As String = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(8).FindControl("chkSelect"), CheckBox)
            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString

                If chkUpdate.Checked = True Then
                    tmpSQL = "INSERT INTO koko_pelajar(StudentID, PPCSDate, Tahun, Program, StatusTawaran)"
                    tmpSQL += " SELECT StudentID,PPCSDate,'" & ddlTahun.Text & "',Program,StatusTawaran FROM UKM3"
                    tmpSQL += " WHERE UKM3.PPMT='Y' AND UKM3.PPCSDate ='" & ddlPPCSDate.Text & "' AND StudentID='" & strKey & "' "

                    If oCommon.getAppsettings("isDebug") = "Y" Then
                        lblDebug.Text = "Debug strSQL:" & strSQL
                    End If
                    strRet = oCommon.ExecuteSQL(tmpSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += ":" & datRespondent.DataKeys(i).Value.ToString & ":" & strRet
                    End If
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            lblMsg.Text = "Berjaya mengemaskini kehadiran pelajar."
        End If
        lblMsgTop.Text = lblMsg.Text
        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddlPPCSDate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPPCSDate.SelectedIndexChanged
        lblPPCSDateSearch.Text = ddlPPCSDate.Text

    End Sub
End Class