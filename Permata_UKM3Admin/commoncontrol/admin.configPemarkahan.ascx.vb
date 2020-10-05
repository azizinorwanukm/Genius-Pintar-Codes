Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class admin_configPemarkahan
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim sqlCommd As SqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan formula tersebut?');")

        Try
            If Not IsPostBack Then
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

        End Try
    End Sub

    Private Sub btnSetActive_Click(sender As Object, e As EventArgs) Handles btnSetActive.Click
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).FindControl("chkSelect"), CheckBox)
            Try
                If Not chkUpdate Is Nothing Then
                    If chkUpdate.Checked = True Then
                        Dim strKeyID As String = datRespondent.DataKeys(i).Values(0).ToString

                        '--Set active 
                        oCommon.ExecuteSQL("update ukm3_compoMark set isActive = 0")
                        strSQL = "UPDATE ukm3_compoMark SET isActive=1 WHERE id ='" & strKeyID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text = "Delete error:" & strKeyID
                        Else
                            lblMsg.Text = "telah dipilih sebagai active formula"
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        Next
        BindData(datRespondent)
    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Try

            strSQL = "admin.configPemarkahanEdit.aspx?id='" & strKeyID & "'"

        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).FindControl("chkSelect"), CheckBox)
            Try
                If chkUpdate IsNot Nothing Then

                    If chkUpdate.Checked = True Then
                        Dim strKeyID As String = datRespondent.DataKeys(i).Values(0).ToString

                        '--DELETE 
                        strSQL = "DELETE FROM ukm3_compoMark WHERE id ='" & strKeyID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text = "Delete error:" & strKeyID
                        Else
                            lblMsg.Text = "telah dibuang"
                        End If
                    Else
                        lblMsg.Text = "Sila tandakan kotak checkbox untuk membuang formula"
                    End If
                Else
                    lblMsg.Text = "Sila tandakan kotak checkbox untuk membuang formula"
                End If


            Catch ex As Exception

            End Try
        Next
        BindData(datRespondent)
    End Sub

    Private Sub btnFormulaAdd_Click(sender As Object, e As EventArgs) Handles btnFormulaAdd.Click
        Response.Redirect("admin_configPemarkahanAdd.aspx")
    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String

        tmpSQL = "SELECT A.id as id_formula,CONCAT('Ujian Stem=',stem,space(3),'Eq Test=',eq,space(3),'Ins KPP=',insKPP,space(3),'Ins PPCS=',insPPCS,space(3),'Ra PPCS=',raPPCS,space(3),'Ukm2=',ukm2,space(3),'PostTest=',postTest ) as formula
                    ,A.year,A.session,A.isActive"
        tmpSQL += " FROM ukm3.dbo.ukm3_compoMark A"

        getSQL = tmpSQL
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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

End Class
