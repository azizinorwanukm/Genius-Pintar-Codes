Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports RKLib.ExportData

Partial Public Class admin_import
    Inherits System.Web.UI.Page

    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim strSaparator As String = ","

    Dim straction As String = ""
    Dim nPageno As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
        If myFile.Value.Length = 0 Then
            AppMsg.Text = "Please select file to import."
            Exit Sub
        End If

        If Not (myFile.PostedFile Is Nothing) Then
            Dim intFileNameLength As Integer
            Dim strFileNamePath As String
            Dim strFileNameOnly As String
            Dim strServerFilename As String
            Dim strFileType As String

            'Logic to find the FileName (excluding the path)
            strFileNamePath = myFile.PostedFile.FileName
            intFileNameLength = InStr(1, StrReverse(strFileNamePath), "\")
            If intFileNameLength > 0 Then
                strFileNameOnly = Mid(strFileNamePath, (Len(strFileNamePath) - intFileNameLength) + 2)
            Else
                strFileNameOnly = strFileNamePath
            End If
            strFileType = myFile.PostedFile.ContentType

            strServerFilename = Server.MapPath(".") & "\result\" & strFileNameOnly
            'AppMsg.Text = "strFileNamePath:" & strFileNamePath & "strServerFilename:" & strServerFilename
            'Exit Sub

            Try
                myFile.PostedFile.SaveAs(strServerFilename)
                AppMsg.Text = "File Upload Success." & strFileNameOnly

                'process the file and move it to completed folder
                strRet = admin_import(strServerFilename)
                If strRet = "0" Then
                    AppMsg.Text = "Successfully upload ukm2_respondent_mark (PKSM) file and process it. Check out process result!"
                Else
                    AppMsg.Text = strRet
                End If

            Catch ex As Exception
                '--display on screen
                lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message

            End Try

        End If
    End Sub

    Private Function admin_import(ByVal strFilename As String) As String
        admin_import = "0"
        Dim arLine

        'Const ForReading = 1, ForWriting = 2, ForAppending = 3
        'Const TristateUseDefault = -2, TristateTrue = -1, TristateFalse = 0
        Const ForReading = 1
        Const TristateUseDefault = -2

        Dim strLine As String
        Dim strALL As String = ""

        ' Create a filesystem object
        Dim FSO
        FSO = Server.CreateObject("Scripting.FileSystemObject")

        '' Map the logical path to the physical system path
        Dim Filepath
        Filepath = strFilename

        If FSO.FileExists(Filepath) Then

            ' Get a handle to the file
            Dim file
            file = FSO.GetFile(Filepath)

            ' Get some info about the file
            Dim FileSize
            FileSize = file.Size

            ' Open the file
            Dim TextStream
            TextStream = file.OpenAsTextStream(ForReading, TristateUseDefault)

            ' Read the file line by line
            Do While Not TextStream.AtEndOfStream
                strLine = TextStream.readline
                arLine = Split(strLine, strSaparator)
                If Not UBound(arLine) = 3 Then

                    ''--[TokenID],[RespFullname]
                    strSQL = "INSERT INTO ppmt_surat2011(TokenID,RespFullname,RespFilename) VALUES('" & oCommon.FixSingleQuotes(arLine(0)) & "','" & oCommon.FixSingleQuotes(arLine(1)) & "','" & oCommon.FixSingleQuotes(arLine(2)) & "')"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If strRet = "0" Then
                        strALL = strALL & "OK:" & arLine(0) & ":" & arLine(1) & vbCrLf
                    Else
                        strALL = strALL & "NOK:" & strRet & ":" & vbCrLf
                    End If
                Else
                    strALL = strALL & "INVALID (no of fields#" & UBound(arLine) & "):" & strLine & vbCrLf
                End If
            Loop

            txtRespondents.Text = strALL
            TextStream = Nothing
        Else
            admin_import = "File not found! Upload fail."
        End If

        FSO = Nothing
    End Function

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        txtRespondents.Text = ""

    End Sub

    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        LoadData()

    End Sub

    Private Sub LoadData()
        lblMsg.Text = ""
        lblPageNo.Text = ""
        lblTotal.Text = ""

        strRet = BindData(datRespondent)
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error. Email to permatapintar@araken.biz: " & ex.Message
            Return False
        End Try

        Return True
    End Function

    Private Function getSQL() As String
        'Dim tmpSQL As String
        'Dim strWhere As String = ""
        'Dim strOrder As String = ""

        'tmpSQL = "SELECT * FROM PPCS"
        'strWhere = " WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "'"
        'strOrder = " ORDER BY PPCSDate"

        'getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        getSQL = txtSQL.Text
        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        LoadData()

        nPageno = e.NewPageIndex + 1
        lblPageNo.Text = "Selected Page:" & nPageno.ToString

    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

        strSQL = txtSQL.Text

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)
        myDataAdapter.Fill(myDataSet, "myTable")
        myDataAdapter.SelectCommand.CommandTimeout = 80000

        strRet = ExportData(myDataSet)

        objConn.Close()

        lblMsg.Text = "strRet:" & strRet
        objConn.Close()

    End Sub

    Private Function ExportData(ByVal dsTable As DataSet) As String
        Dim strFilename As String = oCommon.getRandom & ".txt"

        Try
            ' Export all the details to CSV
            Dim objExport As New RKLib.ExportData.Export("Web")
            Dim dtRespondent As DataTable = dsTable.Tables("myTable").Copy()
            objExport.ExportDetails(dtRespondent, Export.ExportFormat.CSV, strFilename)

            Return strFilename
        Catch Ex As Exception
            Return Ex.Message
        End Try

    End Function

End Class