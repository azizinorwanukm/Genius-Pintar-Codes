Imports System.Data.SqlClient

Partial Public Class ppcs_users_assign
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

    'Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strconn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnRemove.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")

        Try
            If Not IsPostBack Then
                strRet = BindData(datRespondent)
                'Debug.WriteLine("try:", CType(Session.Item("permata_admin"), String))
                ukm3_DateList()
                ukm3_session()
                ddlUkm3Assign.Text = oCommon.getAppsettings("DefaultPPCSDate")
                ddl_jawatanlist()
                usertype_list()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message

        End Try
    End Sub

    Private Sub ukm3_DateList()
        strSQL = "  SELECT description FROM master where type = 'session' order by data_id"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--destination
            ddlUkm3Assign.DataSource = ds
            ddlUkm3Assign.DataTextField = "description"
            ddlUkm3Assign.DataValueField = "description"
            ddlUkm3Assign.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ukm3_session()
        strSQL = "  SELECT description FROM master where type = 'session' order by data_id"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--destination
            ddlUKM3Session.DataSource = ds
            ddlUKM3Session.DataTextField = "description"
            ddlUKM3Session.DataValueField = "description"
            ddlUKM3Session.DataBind()
            ddlUKM3Session.Items.Insert(0, New ListItem("Sila Pilih", String.Empty))
            ddlUKM3Session.SelectedIndex = 0

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddl_jawatanlist()
        strSQL = " SELECT description FROM master where type = 'userType'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDB As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDB.Fill(ds, "Anytable")

            ddlJawatan.DataSource = ds
            ddlJawatan.DataTextField = "description"
            ddlJawatan.DataValueField = "description"
            ddlJawatan.DataBind()
            ddlJawatan.Items.Insert(0, New ListItem("Sila Pilih", String.Empty))
            ddlJawatan.SelectedIndex = 0
        Catch ex As Exception

        End Try
    End Sub

    Private Sub usertype_list()
        strSQL = "SELECT description FROM master where type = 'userType' order by data_id "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionUkm")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUserTypeAssign.DataSource = ds
            ddlUserTypeAssign.DataTextField = "description"
            ddlUserTypeAssign.DataValueField = "description"
            ddlUserTypeAssign.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub


    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strconn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Record not found!"
            Else
                lblMsg.Text = "Total users #:" & myDataSet.Tables(0).Rows.Count
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
        Dim tmpSQL1 As String
        Dim tmpSQL2 As String
        Dim strWhere As String = ""
        Dim strWhere1 As String = ""

        tmpSQL = "select DISTINCT A.ppcsuserid as staff_id,A.LoginID as LoginID,A.pwd as pwd,A.Fullname as fullname,E.staff_session,E.staff_Position As UserType, 'PPCS' fromWhere from permatapintar.dbo.PPCS_Users A 
                    left join ukm3.dbo.staff_info E on E.staff_id=A.ppcsuserid AND E.isAllow = 1
                    left join permatapintar.dbo.PPCS_Users_Year B on B.myGUID= A.myGUID 
					WHERE A.isAllow='Y' and B.PPCSDate = 'PPCS DIS " & Now.Year & " (UKM)' "
        tmpSQL1 = "union "
        tmpSQL2 = "select C.stf_ID as staff_id,D.staff_Login as LoginID,D.staff_Password as pwd,C.staff_Name as fullname,E.staff_session,E.staff_Position As UserType, 'KPP' fromWhere from kolejadmin.dbo.staff_Info C
                    left join ukm3.dbo.staff_info E on E.staff_id=C.staff_ID AND E.isAllow = 1
					left join kolejadmin.dbo.staff_Login D on D.stf_ID = C.stf_ID
					where D.staff_Status ='Access' and D.staff_Access = 'INSTRUKTOR KPP' 
                    "

        'If chk_sortName.Checked = True Then
        '    tmpSQL += " order by A.fullname ASC"
        '    tmpSQL2 += " order by B.staff_Name ASC"
        'End If
        If ddlData.SelectedValue = "" Then


            If Not txtFullname.Text.Length = 0 Then
                strWhere += " AND A.Fullname LIKE '%" & txtFullname.Text & "%'"
                strWhere1 += " AND C.staff_Name LIKE '%" & txtFullname.Text & "%'"
            End If

            If Not ddlJawatan.SelectedValue = "" Then
                strWhere += " AND E.staff_position = '" & ddlJawatan.SelectedValue & "' "
                strWhere1 += "AND E.staff_position = '" & ddlJawatan.SelectedValue & "'"

            End If

            If Not ddlUKM3Session.SelectedValue = "" Then
                strWhere += " AND E.staff_session = '" & ddlUKM3Session.SelectedValue & "'"
                strWhere1 += " AND E.staff_session = '" & ddlUKM3Session.SelectedValue & "'"
            End If

            getSQL = tmpSQL & strWhere & tmpSQL1 & tmpSQL2 & strWhere1

        ElseIf ddlData.SelectedValue = "PPCS" Then

            Dim sort As String = " order by A.Fullname ASC"

            If Not txtFullname.Text.Length = 0 Then
                strWhere += " AND A.Fullname LIKE '%" & txtFullname.Text & "%'"
            End If

            If Not ddlJawatan.SelectedValue = "" Then
                strWhere += " AND E.staff_position = '" & ddlJawatan.SelectedValue & "'"
            End If

            If Not ddlUKM3Session.SelectedValue = "" Then
                strWhere += " AND E.staff_session = '" & ddlUKM3Session.SelectedValue & "'"
            End If

            getSQL = tmpSQL & strWhere & sort
            ''--debug
            'Response.Write(getSQL)
            Return getSQL
        ElseIf ddlData.SelectedValue = "KPP" Then

            Dim sort As String = " order by C.staff_name ASC"

            If Not txtFullname.Text.Length = 0 Then
                strWhere += " AND C.staff_Name LIKE '%" & txtFullname.Text & "%'"
            End If

            If Not ddlJawatan.SelectedValue = "" Then
                strWhere += " AND E.staff_position = '" & ddlJawatan.SelectedValue & "'"
            End If

            If Not ddlUKM3Session.SelectedValue = "" Then
                strWhere += " AND E.staff_session = '" & ddlUKM3Session.SelectedValue & "'"
            End If

            getSQL = tmpSQL2 & strWhere & sort
            Return getSQL
        ElseIf ddlData.SelectedValue = "" Then
            Dim sort As String = " order by fullname ASC"
            getSQL = tmpSQL & tmpSQL1 & tmpSQL2 & sort
            Return getSQL
        End If
#Disable Warning BC42105 ' Function doesn't return a value on all code paths
    End Function
#Enable Warning BC42105 ' Function doesn't return a value on all code paths

    'Private Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles datRespondent.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        Dim lblPPCSDate As Label

    '        Dim i As Integer = e.Row.RowIndex + 1
    '        Dim strKeyID As String = datRespondent.DataKeys(e.Row.RowIndex).Value.ToString  'myGUID

    '        lblPPCSDate = e.Row.FindControl("lblPPCSDate")
    '        lblPPCSDate.Text = getPPCSDate(strKeyID)
    '    End If

    'End Sub

    Private Function getPPCSDate(ByVal strKeyID As String) As String
        Dim strValue As String = ""
        strSQL = "SELECT PPCSDate,Usertype FROM PPCS_Users_Year WHERE myGUID='" & strKeyID & "'"
        strValue = oCommon.getRowValue(strSQL)

        Return strValue
    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Response.Redirect("Ukm3.update_staff.aspx?myguid=" & strKeyID)


    End Sub

    Private Function getUserProfile_UserType() As String
        'strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        'strRet = oCommon.getFieldValue(strSQL)
        Try
            strSQL = "SELECT top 1 staff_position from staff_info with (NOLOCK) where staff_login = '" & CType(Session.Item("permata_admin"), String) & "'  "
            strRet = oCommon.getFieldValue(strSQL)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

        Return strRet
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)

    End Sub

    Private Sub btnAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssign.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        Dim mesej As Integer

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(0).FindControl("chkSelect"), CheckBox)

            If Not chkUpdate Is Nothing Then

                Dim strID As String = datRespondent.DataKeys(i).Value.ToString
                Dim fromWhere As String = CType(datRespondent.Rows(i).Cells(0).FindControl("fromWhere"), Label).Text

                If chkUpdate.Checked = True Then

                    ukm3_status(strID, fromWhere)
                    ukm3_session(strID)


                End If
            End If

        Next

        If mesej = datRespondent.Rows.Count Then

            strRet = BindData(datRespondent)
            lblMsg.Text = "Tiada Rekod dipilih."

        ElseIf lblMsg.Text.Length = 0 Then
            strRet = BindData(datRespondent)
            lblMsg.Text = "Berjaya assign Ukm3 Users."
            lblMsgTop.Text = lblMsg.Text
        Else
            '--error msg
            lblMsgTop.Text = lblMsg.Text
        End If

    End Sub

    Private Sub ukm3_status(ByVal strmyGUID As String, ByVal fromWhere As String)
        'Dim tmpSQL As String

        Dim strCheck As String = "select staff_id from staff_info where staff_id= '" & strmyGUID & "' and staff_from = '" & fromWhere & "' and staff_session = '" & ddlUkm3Assign.SelectedValue & "'"

        Try
            If oCommon.isExist(strCheck) = True Then
                strSQL = "update staff_info set staff_Position='" & ddlUserTypeAssign.SelectedValue & "',staff_session= '" & ddlUkm3Assign.SelectedValue & "',isAllow= '1' where staff_id='" & strmyGUID & "' AND staff_from = '" & fromWhere & "' "

            Else
                strSQL = "insert into staff_info(staff_id,staff_Position ,staff_session)values('" & strmyGUID & "','" & ddlUserTypeAssign.SelectedValue & "','" & ddlUkm3Assign.SelectedValue & "') "

            End If

            strRet = oCommon.ExecuteSQL(strSQL)

            'update the record
            If fromWhere = "KPP" Then

                If strRet = 0 Then
                    strSQL = "update ukm3.dbo.staff_info set ukm3.dbo.staff_info.staff_name=B.staff_Name,
                            ukm3.dbo.staff_info.staff_login=C.staff_Login,
                            ukm3.dbo.staff_info.staff_from='KPP',
                            ukm3.dbo.staff_info.isAllow=1
                From ukm3.dbo.staff_info A, kolejadmin.dbo.staff_info B, kolejadmin.dbo.staff_Login C
                    where A.staff_id = '" & strmyGUID & "' AND B.stf_ID = '" & strmyGUID & "' AND C.stf_ID = '" & strmyGUID & "' AND staff_Access = 'Instruktor KPP' AND A.staff_session = '" & ddlUkm3Assign.SelectedValue & "'"

                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text = "unable to update the data:" & strRet
                    End If
                End If

            Else
                If strRet = 0 Then
                    strSQL = "update ukm3.dbo.staff_info set 
                            ukm3.dbo.staff_info.staff_name=B.fullname,
                            ukm3.dbo.staff_info.staff_login=B.LoginID,
                             ukm3.dbo.staff_info.staff_from='PPCS',
                            ukm3.dbo.staff_info.isAllow=1
                From ukm3.dbo.staff_info A, permatapintar.dbo.PPCS_Users B
                    where A.staff_id = '" & strmyGUID & "' AND B.ppcsuserid='" & strmyGUID & "' AND A.staff_session = '" & ddlUkm3Assign.SelectedValue & "'"

                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text = "unable to update the data:" & strRet
                    End If
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub



    Private Sub ukm3_session(ByVal strmyGUID As String)

        Dim year As String = Date.Today.Year
        strSQL = "insert into session(session_ukm,staff_position,stf_id,session_year) values ('" & ddlUkm3Assign.SelectedValue & "','" & ddlUserTypeAssign.SelectedValue & "','" & strmyGUID & "', '" & year & "')"
        Debug.WriteLine(strSQL)
        Try
            strRet = oCommon.ExecuteSQL(strSQL)
            Debug.WriteLine(strRet)
            If Not strRet = "0" Then
                lblMsg.Text += "unable to update to session:" & strRet
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnRemove_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRemove.Click
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        'Loop through gridview rows to find checkbox 
        'and check whether it is checked or not 
        Dim i As Integer
        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)
            ''--debug
            'Response.Write(chkUpdate)
            If Not chkUpdate Is Nothing Then
                If chkUpdate.Checked = True Then
                    ' Get the values of textboxes using findControl
                    ''Dim strID As String = datRespondent.Rows(i).Cells(0).Text
                    Dim strID As String = datRespondent.DataKeys(i).Value.ToString

                    '--DELETE 
                    strSQL = "UPDATE ukm3.dbo.staff_info SET isAllow = 0
                                where staff_id= '" & strID & "' AND staff_from = '" & CType(datRespondent.Rows(i).Cells(0).FindControl("fromWhere"), Label).Text & "' AND A.staff_session = '" & ddlUkm3Assign.SelectedValue & "'"

                    Debug.WriteLine(strSQL)
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If Not strRet = "0" Then
                        lblMsg.Text += "Delete error:" & strID
                    End If
                End If
            End If
        Next

        If lblMsg.Text.Length = 0 Then
            strRet = BindData(datRespondent)
            lblMsg.Text = "Berjaya Remove Ukm3 Users."
            lblMsgTop.Text = lblMsg.Text
        End If

    End Sub

End Class