Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class pelajar_pencapaian_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblTahun.Text = Request.QueryString("tahun")
                koko_pencapaian_load()
            End If

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

    Private Sub koko_pencapaian_load()
        strSQL = "SELECT Pencapaian,Disahkan,DisahkanOleh FROM koko_pelajar WHERE StudentID='" & Request.QueryString("studentid") & "' AND Tahun='" & Request.QueryString("tahun") & "'"
        '--debug
        'Response.Write(strSQL)

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Pencapaian")) Then
                    txtPencapaian.Text = ds.Tables(0).Rows(0).Item("Pencapaian")
                Else
                    txtPencapaian.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Disahkan")) Then
                    selDisahkan.Value = ds.Tables(0).Rows(0).Item("Disahkan")
                Else
                    selDisahkan.Value = "N"
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("DisahkanOleh")) Then
                    lblDisahkanOleh.Text = ds.Tables(0).Rows(0).Item("DisahkanOleh")
                Else
                    lblDisahkanOleh.Text = ""
                End If

            End If

        Catch ex As Exception
            'lblMsg.Text = "System error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsgTop.Text = ""

        'check form validation. if failed exit
        If ValidateForm() = False Then
            Exit Sub
        End If

        'UPDATE
        Dim strDisahkanoleh As String = CType(Session.Item("koko_loginid"), String)
        strSQL = "UPDATE koko_pelajar Set Pencapaian='" & oCommon.FixSingleQuotes(txtPencapaian.Text) & "',Disahkan='" & selDisahkan.Value & "',DisahkanOleh='" & strDisahkanoleh & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND Tahun='" & Request.QueryString("tahun") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Kemaskini berjaya!"
        Else
            lblMsg.Text = "system error:" & strRet
        End If

        lblMsgTop.Text = lblMsg.Text

    End Sub

    '--CHECK form validation.
    Private Function ValidateForm() As Boolean

        If txtPencapaian.Text.Length = 0 Then

        End If

        Return True
    End Function


End Class