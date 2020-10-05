Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Partial Public Class pusatujian_petugas_select
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                txtTarikhSearch.Text = oCommon.getTodayFormated

                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_State() As String
        strSQL = "SELECT State FROM UserProfile WITH (NOLOCK) WHERE loginid='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function


    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Petugas belum didaftarkan bagi pusat ini."
            Else
                lblMsg.Text = "Jumlah Petugas #:" & myDataSet.Tables(0).Rows.Count
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

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Fullname"

        tmpSQL = "SELECT PetugasID,Fullname,ContactNo,Email,UserType,City,State FROM PusatUjian_Petugas"
        strWhere = " WITH (NOLOCK) WHERE PetugasID IS NOT NULL"

        If Not selUserType.Value = "ALL" Then
            strWhere += " AND UserType='" & selUserType.Value & "'"
        End If

        If Not txtFullname.Text.Length = 0 Then
            strWhere += " AND Fullname LIKE '%" & oCommon.FixSingleQuotes(txtFullname.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub btnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        lblMsg.Text = ""

        If txtTarikhSearch.Text.Length = 0 Then
            lblMsg.Text = "Sila pilih Tarikh!"
            Exit Sub
        End If

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        Dim strSesi As String = ""
        Dim strSesiPagi As String = ""
        Dim strSesiTghari As String = ""
        Dim strSesiPetang As String = ""

        If chkPagi.Checked = True Then
            strSesiPagi = "PAGI-"
        End If
        If chkTghari.Checked = True Then
            strSesiTghari = "TGHARI-"
        End If
        If chkPetang.Checked = True Then
            strSesiPetang = "PETANG"
        End If

        strSesi = strSesiPagi & strSesiTghari & strSesiPetang

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(6).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                    ''--debug
                    ''Response.Write(strID)

                    strSQL = " SELECT PusatCode FROM PusatUjian_Petugas_List WITH (NOLOCK) WHERE PusatCode='" & Request.QueryString("pusatcode") & "' AND PetugasID=" & strID & " AND PusatTahun='" & Request.QueryString("examyear") & "' AND Tarikh='" & oCommon.FixSingleQuotes(txtTarikhSearch.Text) & "'"
                    '--Response.Write(strSQL & vbCrLf)
                    '--belum ada baru insert
                    If oCommon.isExist(strSQL) = False Then
                        strSQL = "INSERT PusatUjian_Petugas_List (PusatCode,PetugasID,PusatTahun,Tarikh,SesiPagi,SesiTghari,SesiPetang,Sesi) VALUES ('" & Request.QueryString("pusatcode") & "'," & strID & ",'" & Request.QueryString("examyear") & "','" & oCommon.FixSingleQuotes(txtTarikhSearch.Text) & "','" & chkPagi.Checked.ToString & "','" & chkTghari.Checked.ToString & "','" & chkPetang.Checked.ToString & "','" & strSesi & "')"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text += "INSERT NOK:" & strRet & vbCrLf
                        Else
                            'lblMsg.Text += "OK"
                        End If
                    Else
                        strSQL = "UPDATE PusatUjian_Petugas_List SET Tarikh='" & oCommon.FixSingleQuotes(txtTarikhSearch.Text) & "',Sesi='" & strSesi & "',SesiPagi='" & chkPagi.Checked.ToString & "',SesiTghari='" & chkTghari.Checked.ToString & "',SesiPetang='" & chkPetang.Checked.ToString & "' WHERE PusatCode='" & Request.QueryString("pusatcode") & "' AND PetugasID=" & strID & " AND PusatTahun='" & Request.QueryString("examyear") & "' AND Tarikh='" & oCommon.FixSingleQuotes(txtTarikhSearch.Text) & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                        If Not strRet = "0" Then
                            lblMsg.Text += "UPDATE NOK:" & strRet & vbCrLf
                            'lblMsg.Text += "Berjaya kemaskini Petugas Pusat Ujian."
                        Else
                            'lblMsg.Text += "OK"
                        End If
                    End If
                End If
            End If
        Next

        lblMsg.Text += "Berjaya mengemaskini rekod baru bagi Petugas Pusat Ujian."

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.petugas.update.aspx?petugasid=" & strKeyID)
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.petugas.update.aspx?petugasid=" & strKeyID)
            Case "UKM"
                Response.Redirect("ukm.pusatujian.petugas.update.aspx?petugasid=" & strKeyID)
            Case Else
                lblMsg.Text = "Invalid user type!"
        End Select

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE loginid='" & CType(Session.Item("kpmadmin_loginid"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub lnkSenaraiPetugas_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkSenaraiPetugas.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect("admin.pusatujian.petugas.list.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "SUBADMIN"
                Response.Redirect("subadmin.pusatujian.petugas.list.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case "UKM"
                Response.Redirect("ukm.pusatujian.petugas.list.aspx?pusatcode=" & Request.QueryString("pusatcode") & "&examyear=" & Request.QueryString("examyear"))
            Case Else
                lblMsg.Text = "Invalid user type!"
        End Select

    End Sub


End Class