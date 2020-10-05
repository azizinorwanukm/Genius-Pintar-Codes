Imports System.Data.SqlClient

Public Class studentschool_update
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim getUKM1Year As String = oCommon.getFieldValue("select configString from master_Config where configCode = 'UKM1ExamYear'")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod tersebut?');")
        Try
            If Not IsPostBack Then
                master_Dobyear_list()
                schoolprofile_view()
                getStudentSchool_view()
            End If

        Catch ex As Exception
            lblMsg.Text = "Err:" & ex.Message
        End Try

    End Sub

    Private Sub getStudentSchool_view()
        '--IsLatest
        Dim strIsLatest As String = "N"
        strSQL = "SELECT IsLatest FROM StudentSchool WHERE StudentSchoolID=" & Request.QueryString("studentschoolid")
        strIsLatest = oCommon.getFieldValue(strSQL)
        If strIsLatest = "Y" Then
            chkIsLatest.Checked = True
        Else
            chkIsLatest.Checked = False
        End If

        '--strStartDate
        Dim strStartDate As String = ""
        strSQL = "SELECT StartDate FROM StudentSchool WHERE StudentSchoolID=" & Request.QueryString("studentschoolid")
        strStartDate = oCommon.getFieldValue(strSQL)

        Dim arStartDate As Array = strStartDate.Split("-")
        If UBound(arStartDate) >= 2 Then
            selStartDate_day.Value = arStartDate(0).ToString
            selStartDate_month.Value = arStartDate(1).ToString
            ddlStartDate_year.SelectedValue = arStartDate(2).ToString
        End If

        '--EndDate
        Dim strEndDate As String = ""
        strSQL = "SELECT EndDate FROM StudentSchool WHERE StudentSchoolID=" & Request.QueryString("studentschoolid")
        strEndDate = oCommon.getFieldValue(strSQL)
        Dim arEndDate As Array = strEndDate.Split("-")
        If UBound(arEndDate) >= 2 Then
            selEndDate_day.Value = arEndDate(0).ToString
            selEndDate_month.Value = arEndDate(1).ToString
            ddlEndDate_year.SelectedValue = arEndDate(2).ToString
        End If



    End Sub

    Private Sub master_Dobyear_list()
        strSQL = "SELECT DOB_Year FROM master_Dobyear WITH (NOLOCK) ORDER BY DOB_Year"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--ddlStartDate_day
            ddlStartDate_year.DataSource = ds
            ddlStartDate_year.DataTextField = "DOB_Year"
            ddlStartDate_year.DataValueField = "DOB_Year"
            ddlStartDate_year.DataBind()

            '--ddlEndDate_year
            ddlEndDate_year.DataSource = ds
            ddlEndDate_year.DataTextField = "DOB_Year"
            ddlEndDate_year.DataValueField = "DOB_Year"
            ddlEndDate_year.DataBind()

        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT UserType FROM UserProfile WITH (NOLOCK) WHERE LoginID='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub schoolprofile_view()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)


        strSQL = "SELECT * FROM SchoolProfile WHERE SchoolID='" & getSchoolID() & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolName")) Then
                    lblSchoolName.Text = MyTable.Rows(nRows).Item("SchoolName").ToString
                Else
                    lblSchoolName.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCode")) Then
                    lblSchoolCode.Text = MyTable.Rows(nRows).Item("SchoolCode").ToString
                Else
                    lblSchoolCode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolAddress")) Then
                    lblSchoolAddress.Text = MyTable.Rows(nRows).Item("SchoolAddress").ToString
                Else
                    lblSchoolAddress.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolPostcode")) Then
                    lblSchoolPostcode.Text = MyTable.Rows(nRows).Item("SchoolPostcode").ToString
                Else
                    lblSchoolPostcode.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolCity")) Then
                    lblSchoolCity.Text = MyTable.Rows(nRows).Item("SchoolCity").ToString
                Else
                    lblSchoolCity.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolState")) Then
                    lblSchoolState.Text = MyTable.Rows(nRows).Item("SchoolState").ToString
                Else
                    lblSchoolState.Text = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("SchoolType")) Then
                    lblSchoolType.Text = MyTable.Rows(nRows).Item("SchoolType").ToString
                Else
                    lblSchoolType.Text = ""
                End If
            End If
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function getSchoolID() As String
        strSQL = "SELECT SchoolID FROM StudentSchool WHERE studentschoolid=" & Request.QueryString("studentschoolID")
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        Dim strStartDate As String = selStartDate_day.Value & "-" & selStartDate_month.Value & "-" & ddlStartDate_year.Text
        Dim strEndDate As String = selEndDate_day.Value & "-" & selEndDate_month.Value & "-" & ddlEndDate_year.Text

        Dim strIsLatest As String = "N"
        If chkIsLatest.Checked = True Then
            strIsLatest = "Y"
            setStudentSchool_N()
        End If

        strSQL = "UPDATE StudentSchool SET StartDate='" & strStartDate & "',EndDate='" & strEndDate & "',IsLatest='" & strIsLatest & "' WHERE studentschoolid=" & Request.QueryString("studentschoolid")
        strRet = oCommon.ExecuteSQL(strSQL)

        If Not strRet = "0" Then
            lblMsg.Text = "System error:" & strRet
        Else

            ''get Student ID data
            Dim get_StudentID As String = "select StudentSchool.StudentID from StudentSchool
                                           where studentschoolid='" & Request.QueryString("studentschoolid") & "'"
            Dim data_StudentID As String = oCommon.getFieldValue(get_StudentID)

            ''get School ID data 
            Dim get_SchoolID As String = "select SchoolProfile.SchoolID from SchoolProfile
                                          left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                          left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                          where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolID As String = oCommon.getFieldValue(get_SchoolID)

            ''get School State data
            Dim get_SchoolState As String = "select SchoolProfile.SchoolState from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                             where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolState As String = oCommon.getFieldValue(get_SchoolState)

            ''get School City data
            Dim get_SchoolCity As String = "select SchoolProfile.SchoolCity from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                             where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolCity As String = oCommon.getFieldValue(get_SchoolCity)

            ''get School Type data
            Dim get_SchoolType As String = "select SchoolProfile.SchoolType from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                             where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolType As String = oCommon.getFieldValue(get_SchoolType)

            ''get School PPD data
            Dim get_SchoolPPD As String = "select SchoolProfile.SchoolPPD from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                             where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolPPD As String = oCommon.getFieldValue(get_SchoolPPD)

            ''get School Lokasi data
            Dim get_SchoolLokasi As String = "select SchoolProfile.SchoolType from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM1 on UKM1.StudentID = StudentSchool.StudentID
                                             where UKM1.StudentID='" & data_StudentID & "' and StudentSchool.IsLatest = 'Y' and UKM1.ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')"
            Dim data_SchoolLokasi As String = oCommon.getFieldValue(get_SchoolLokasi)

            Dim maxExamYear As String = oCommon.getFieldValue("select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "'")

            If maxExamYear = getUKM1Year Then
                strSQL = "UPDATE UKM1_" & getUKM1Year & " SET SchoolID='" & data_SchoolID & "',SchoolState='" & data_SchoolState & "',SchoolCity='" & data_SchoolCity & "', SchoolType='" & data_SchoolType & "',SchoolPPD='" & data_SchoolPPD & "',SchoolLokasi='" & data_SchoolLokasi & "' 
                        WHERE StudentID=" & data_StudentID & "' And ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

            strSQL = "UPDATE UKM1 SET SchoolID='" & data_SchoolID & "',SchoolState='" & data_SchoolState & "',SchoolCity='" & data_SchoolCity & "', SchoolType='" & data_SchoolType & "',SchoolPPD='" & data_SchoolPPD & "',SchoolLokasi='" & data_SchoolLokasi & "' 
                        WHERE StudentID=" & data_StudentID & "' And ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID & "')'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If Not strRet = "0" Then
                lblMsg.Text = "System error:" & strRet
            Else

                ''get Student ID data
                Dim get_StudentID_UKM2 As String = "select StudentSchool.StudentID from StudentSchool
                                           where studentschoolid='" & Request.QueryString("studentschoolid") & "'"
                Dim data_StudentID_UKM2 As String = oCommon.getFieldValue(get_StudentID_UKM2)

                ''get School ID data 
                Dim get_SchoolID_UKM2 As String = "select SchoolProfile.SchoolID from SchoolProfile
                                          left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                          left join UKM2 on UKM1.StudentID = StudentSchool.StudentID
                                          where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
                Dim data_SchoolID_UKM2 As String = oCommon.getFieldValue(get_SchoolID_UKM2)

                ''get School State data
                Dim get_SchoolState_UKM2 As String = "select SchoolProfile.SchoolState from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM2 on UKM2.StudentID = StudentSchool.StudentID
                                             where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
                Dim data_SchoolState_UKM2 As String = oCommon.getFieldValue(get_SchoolState_UKM2)

                ''get School City data
                Dim get_SchoolCity_UKM2 As String = "select SchoolProfile.SchoolCity from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM2 on UKM2.StudentID = StudentSchool.StudentID
                                             where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
                Dim data_SchoolCity_UKM2 As String = oCommon.getFieldValue(get_SchoolCity_UKM2)

                ''get School Type data
                Dim get_SchoolType_UKM2 As String = "select SchoolProfile.SchoolType from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM2 on UKM2.StudentID = StudentSchool.StudentID
                                             where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
                Dim data_SchoolType_UKM2 As String = oCommon.getFieldValue(get_SchoolType_UKM2)

                ''get School PPD data
                Dim get_SchoolPPD_UKM2 As String = "select SchoolProfile.SchoolPPD from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM2 on UKM2.StudentID = StudentSchool.StudentID
                                             where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
                Dim data_SchoolPPD_UKM2 As String = oCommon.getFieldValue(get_SchoolPPD_UKM2)

                ''get School Lokasi data
                Dim get_SchoolLokasi_UKM2 As String = "select SchoolProfile.SchoolType from SchoolProfile
                                             left join StudentSchool on SchoolProfile.SchoolID = StudentSchool.SchoolID
                                             left join UKM2 on UKM2.StudentID = StudentSchool.StudentID
                                             where UKM2.StudentID='" & data_StudentID_UKM2 & "' and StudentSchool.IsLatest = 'Y' and UKM2.ExamYear in (select max(ExamYear) from UKM2 where StudentID = '" & data_StudentID_UKM2 & "')"
                Dim data_SchoolLokasi_UKM2 As String = oCommon.getFieldValue(get_SchoolLokasi_UKM2)

                strSQL = "UPDATE UKM2 SET SchoolID='" & data_SchoolID_UKM2 & "',SchoolState='" & data_SchoolState_UKM2 & "',SchoolCity='" & data_SchoolCity_UKM2 & "', SchoolType='" & data_SchoolType_UKM2 & "',SchoolPPD='" & data_SchoolPPD_UKM2 & "',SchoolLokasi='" & data_SchoolLokasi_UKM2 & "' 
                        WHERE StudentID=" & data_StudentID_UKM2 & "' And ExamYear in (select max(ExamYear) from UKM1 where StudentID = '" & data_StudentID_UKM2 & "')'"
                strRet = oCommon.ExecuteSQL(strSQL)

                If Not strRet = "0" Then
                    lblMsg.Text = "System error:" & strRet
                Else
                    lblMsg.Text = "BERJAYA mengemaskini maklumat sekolah."
                End If

            End If

        End If

    End Sub

    Private Sub setStudentSchool_N()
        strSQL = "UPDATE StudentSchool SET IsLatest='N' WHERE StudentID='" & Request.QueryString("studentid") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

    End Sub

    Protected Sub lnkStudentProfileView_Click(sender As Object, e As EventArgs) Handles lnkStudentProfileView.Click
        Select Case getUserProfile_UserType()
            Case "ADMIN"
                Response.Redirect(CType(Session.Item("pageid"), String) & "?studentid=" & Request.QueryString("studentid"))
            Case "ADMINOP"
                Response.Redirect("studentprofile.view.aspx?studentid=" & Request.QueryString("studentid"))
            Case "SUBADMIN"
            Case Else
                lblMsg.Text = "Invalid user type:" & getUserProfile_UserType()
        End Select

    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        strSQL = "DELETE FROM StudentSchool WHERE StudentSchoolID=" & Request.QueryString("studentschoolid")
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya MENGHAPUSKAN rekod tersebut."
        Else
            lblMsg.Text = "GAGAL menghapuskan rekod tersebut. " & strRet
        End If

    End Sub
End Class