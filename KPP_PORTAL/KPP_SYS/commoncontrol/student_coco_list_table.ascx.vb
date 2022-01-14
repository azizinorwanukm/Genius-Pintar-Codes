Imports System.Data.SqlClient
Imports System.IO

Public Class student_coco_list_table
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim getStatus As String = Request.QueryString("status")

                If getStatus = "VKOKO" Then ''View Cocurricular
                    txtbreadcrum1.Text = "View Co-Curriculum Information"
                    ViewCocurricular.Visible = True
                    RegisterCocurricular.Visible = False
                    ViewAchievement.Visible = False

                    btnViewCocu.Attributes("class") = "btn btn-info"
                    btnRegisterCocu.Attributes("class") = "btn btn-default font"
                    btnViewAchievement.Attributes("class") = "btn btn-default font"

                    strRet = BindDataCocurricular(datRespondent_Cocurricular)

                ElseIf getStatus = "RKOKO" Then ''Register COcurricular
                    txtbreadcrum1.Text = "Register Co-Curriculum Information"
                    ViewCocurricular.Visible = False
                    RegisterCocurricular.Visible = True
                    ViewAchievement.Visible = False
                    HideForAPP.Visible = True

                    btnViewCocu.Attributes("class") = "btn btn-default font"
                    btnRegisterCocu.Attributes("class") = "btn btn-info"
                    btnViewAchievement.Attributes("class") = "btn btn-default font"

                    strSQL = "Select student_Campus from student_info where std_Id = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and student_ID is not null and student_Status = 'Access'"
                    strRet = oCommon.getFieldValue(strSQL)

                    If strRet = "APP" Then
                        HideForAPP.Visible = False
                    End If

                    ddlYear_Koko()
                    koko_kelas_list()
                    koko_uniform_list()
                    koko_persatuan_list()
                    koko_sukan_list()
                    koko_rumahsukan_list()

                ElseIf getStatus = "VACHIEVE" Then ''View Achievement
                    txtbreadcrum1.Text = "View Achievement Information"
                    ViewCocurricular.Visible = False
                    RegisterCocurricular.Visible = False
                    ViewAchievement.Visible = True

                    btnViewCocu.Attributes("class") = "btn btn-default font"
                    btnRegisterCocu.Attributes("class") = "btn btn-default font"
                    btnViewAchievement.Attributes("class") = "btn btn-info"

                    ddlYear_Koko()
                    Load_Achievement()
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnViewCocu_ServerClick(sender As Object, e As EventArgs) Handles btnViewCocu.ServerClick
        Response.Redirect("pelajar_pilih_koko.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=VKOKO")
    End Sub

    Private Sub btnRegisterCocu_ServerClick(sender As Object, e As EventArgs) Handles btnRegisterCocu.ServerClick
        Response.Redirect("pelajar_pilih_koko.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=RKOKO")
    End Sub

    Private Sub btnViewAchievement_ServerClick(sender As Object, e As EventArgs) Handles btnViewAchievement.ServerClick
        Response.Redirect("pelajar_pilih_koko.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=VACHIEVE")
    End Sub

    Private Function BindDataCocurricular(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLCocurricular, strConnPermata)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function getSQLCocurricular() As String

        Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("std_ID"))

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY koko_pelajar.Tahun ASC"

        tmpSQL = "SELECT distinct A.std_ID, koko_pelajar.Tahun, UPPER(koko_kelas.Kelas) Kelas,
                 (SELECT UPPER(NamaBI) Nama FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,
                 (SELECT UPPER(NamaBI) Nama FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,
                 (SELECT UPPER(NamaBI) Nama FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,
                 (SELECT UPPER(NamaBI) Nama FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan
                 FROM koko_pelajar
                 LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                 LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                 LEFT OUTER JOIN kolejadmin.dbo.student_info A ON StudentProfile.MYKAD = A.student_Mykad
                 LEFT OUTER JOIN koko_kolejpermata ON koko_pelajar.UniformID=koko_kolejpermata.KokoID OR koko_pelajar.PersatuanID=koko_kolejpermata.KokoID OR koko_pelajar.SukanID=koko_kolejpermata.KokoID OR koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID"
        strWhere = " WHERE A.student_Status = 'Access' AND A.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"

        getSQLCocurricular = tmpSQL & strWhere & strOrderby

        Return getSQLCocurricular
    End Function

    Private Sub ddlYear_Koko()
        strSQL = "  Select Tahun from koko_pelajar 
                    left join StudentProfile on koko_pelajar.StudentID = StudentProfile.StudentID
                    left join kolejadmin.dbo.student_info on kolejadmin.dbo.student_info.student_Mykad = StudentProfile.MYKAD
                    where kolejadmin.dbo.student_info.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlYear_Achievement.DataSource = ds
            ddlYear_Achievement.DataTextField = "Tahun"
            ddlYear_Achievement.DataValueField = "Tahun"
            ddlYear_Achievement.DataBind()
            ddlYear_Achievement.Items.Insert(0, New ListItem("Select Year", String.Empty))

            ddlYear_Kokurrikulum.DataSource = ds
            ddlYear_Kokurrikulum.DataTextField = "Tahun"
            ddlYear_Kokurrikulum.DataValueField = "Tahun"
            ddlYear_Kokurrikulum.DataBind()
            ddlYear_Kokurrikulum.Items.Insert(0, New ListItem("Select Year", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub ddlYear_Achievement_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_Achievement.SelectedIndexChanged
        Try
            Load_Achievement()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Load_Achievement()
        Dim findID As String = "Select StudentID from StudentProfile left join kolejadmin.dbo.student_info on  kolejadmin.dbo.student_info.student_Mykad = StudentProfile.MYKAD where kolejadmin.dbo.student_info.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        Dim getID As String = oCommon.getFieldValuePermata(findID)

        strSQL = "Select Pencapaian from koko_pelajar where Tahun = '" & ddlYear_Achievement.SelectedValue & "' and StudentID = '" & getID & "'"
        txtarea_achievement.Text = oCommon.getFieldValuePermata(strSQL)

        If txtarea_achievement.Text = "" Or ddlYear_Achievement.SelectedIndex = 0 Then
            txtarea_achievement.Text = "  No Achievement Information Are Recorded !!!"
        End If
    End Sub

    Private Sub btnUpdateAchievement_ServerClick(sender As Object, e As EventArgs) Handles btnUpdateAchievement.ServerClick
        If ddlYear_Achievement.SelectedValue = Now.Year Then

            Dim findID As String = "Select StudentID from StudentProfile left join kolejadmin.dbo.student_info on  kolejadmin.dbo.student_info.student_Mykad = StudentProfile.MYKAD where kolejadmin.dbo.student_info.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
            Dim getID As String = oCommon.getFieldValuePermata(findID)

            strSQL = "Update koko_pelajar set Pencapaian = '" & txtarea_achievement.Text & "' where StudentID = '" & getID & "' and Tahun = '" & ddlYear_Achievement.SelectedValue & "'"
            strRet = oCommon.ExecuteSQLPermata(strSQL)

            If strRet = "" Then
                ShowMessage(" Update  Achievement Information ", MessageType.Success)
            Else
                ShowMessage(" Uanble To Update Achievement Information ", MessageType.Error)
            End If
        Else
            ShowMessage(" Uanble To Update Previous Year Achievement Information ", MessageType.Error)
        End If
    End Sub

    Private Sub ddlYear_Kokurrikulum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear_Kokurrikulum.SelectedIndexChanged
        Try
            koko_kelas_list()
            koko_uniform_list()
            koko_persatuan_list()
            koko_sukan_list()
            koko_rumahsukan_list()

            koko_pelajar_koko_load()
            CountMax()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub koko_kelas_list()
        strSQL = "SELECT * FROM koko_kelas WHERE Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "' ORDER BY Kelas ASC"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClass_Kokurrikulum.DataSource = ds
            ddlClass_Kokurrikulum.DataTextField = "Kelas"
            ddlClass_Kokurrikulum.DataValueField = "KelasID"
            ddlClass_Kokurrikulum.DataBind()
            ddlClass_Kokurrikulum.Items.Add(New ListItem("Select Class", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub koko_uniform_list()

        strSQL = "Select student_Campus from student_info where std_Id = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and student_ID is not null and student_Status = 'Access'"
        strRet = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT KokoID,UPPER(Nama) Nama FROM koko_kolejpermata WHERE Jenis='UNIFORM' AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "' and Kampus = '" & strRet & "' ORDER BY Nama ASC"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlUniform_Kokurrikulum.DataSource = ds
            ddlUniform_Kokurrikulum.DataTextField = "Nama"
            ddlUniform_Kokurrikulum.DataValueField = "KokoID"
            ddlUniform_Kokurrikulum.DataBind()
            ddlUniform_Kokurrikulum.Items.Add(New ListItem("Select Uniform", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub koko_persatuan_list()

        strSQL = "Select student_Campus from student_info where std_Id = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and student_ID is not null and student_Status = 'Access'"
        strRet = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT KokoID,UPPER(Nama) Nama FROM koko_kolejpermata WHERE Jenis='PERSATUAN' AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "' and Kampus = '" & strRet & "' ORDER BY Nama ASC"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlClub_Kokurrikulum.DataSource = ds
            ddlClub_Kokurrikulum.DataTextField = "Nama"
            ddlClub_Kokurrikulum.DataValueField = "KokoID"
            ddlClub_Kokurrikulum.DataBind()
            ddlClub_Kokurrikulum.Items.Add(New ListItem("Select Club", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub koko_sukan_list()

        strSQL = "Select student_Campus from student_info where std_Id = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and student_ID is not null and student_Status = 'Access'"
        strRet = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT KokoID,UPPER(Nama) Nama FROM koko_kolejpermata WHERE Jenis='SUKAN' AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "' and Kampus = '" & strRet & "' ORDER BY Nama ASC"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSport_Kokurrikulum.DataSource = ds
            ddlSport_Kokurrikulum.DataTextField = "Nama"
            ddlSport_Kokurrikulum.DataValueField = "KokoID"
            ddlSport_Kokurrikulum.DataBind()
            ddlSport_Kokurrikulum.Items.Add(New ListItem("Select Sport", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub koko_rumahsukan_list()

        strSQL = "Select student_Campus from student_info where std_Id = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and student_ID is not null and student_Status = 'Access'"
        strRet = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT KokoID, UPPER(Nama) Nama FROM koko_kolejpermata WHERE Jenis='RUMAHSUKAN' AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "' and Kampus = '" & strRet & "' ORDER BY Nama ASC"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSportHouses_Kokurrikulum.DataSource = ds
            ddlSportHouses_Kokurrikulum.DataTextField = "Nama"
            ddlSportHouses_Kokurrikulum.DataValueField = "KokoID"
            ddlSportHouses_Kokurrikulum.DataBind()
            ddlSportHouses_Kokurrikulum.Items.Add(New ListItem("Select Sport Houses", String.Empty))

        Catch ex As Exception
        End Try
    End Sub

    Private Sub koko_pelajar_koko_load()

        Dim findID As String = "Select StudentID from StudentProfile left join kolejadmin.dbo.student_info on  kolejadmin.dbo.student_info.student_Mykad = StudentProfile.MYKAD where kolejadmin.dbo.student_info.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        Dim getID As String = oCommon.getFieldValuePermata(findID)

        strSQL = "  SELECT koko_pelajar.kokopelajarid,koko_pelajar.StudentID,StudentProfile.StudentFullname,StudentProfile.MYKAD,StudentProfile.AlumniID,StudentProfile.NoPelajar,StudentProfile.DOB_Year,StudentProfile.StudentRace,StudentProfile.StudentGender,StudentProfile.StudentReligion,koko_pelajar.Tahun,koko_pelajar.KelasID,koko_kelas.Kelas,koko_pelajar.UniformID,koko_pelajar.PersatuanID,koko_pelajar.SukanID,koko_pelajar.RumahsukanID,koko_pelajar.StatusTawaran FROM koko_pelajar"
        strSQL += " LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID"
        strSQL += " LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID"
        strSQL += " WHERE koko_pelajar.Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "'"
        strSQL += " AND koko_pelajar.StudentID='" & getID & "'"

        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KelasID")) Then
                    ddlClass_Kokurrikulum.SelectedValue = ds.Tables(0).Rows(0).Item("KelasID")
                Else
                    ddlClass_Kokurrikulum.SelectedIndex = 0
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("UniformID")) Then
                    ddlUniform_Kokurrikulum.SelectedValue = ds.Tables(0).Rows(0).Item("UniformID")
                Else
                    ddlUniform_Kokurrikulum.SelectedIndex = 0
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("PersatuanID")) Then
                    ddlClub_Kokurrikulum.SelectedValue = ds.Tables(0).Rows(0).Item("PersatuanID")
                Else
                    ddlClub_Kokurrikulum.SelectedIndex = 0
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SukanID")) Then
                    ddlSport_Kokurrikulum.SelectedValue = ds.Tables(0).Rows(0).Item("SukanID")
                Else
                    ddlSport_Kokurrikulum.SelectedIndex = 0
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("RumahsukanID")) Then
                    ddlSportHouses_Kokurrikulum.SelectedValue = ds.Tables(0).Rows(0).Item("RumahsukanID")
                Else
                    ddlSportHouses_Kokurrikulum.SelectedIndex = 0
                End If

                txtSwimming_Kokurrikulum.Text = "MANDATORY"
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub CountMax()

        lblCountUniform.Text = "0"
        lblCountClub.Text = "0"
        lblCountSport.Text = "0"
        lblCountSportHouses.Text = "0"

        '--UNIFORM
        If ddlUniform_Kokurrikulum.SelectedValue.Length > 0 Then
            strSQL = "SELECT COUNT(*) FROM koko_pelajar WHERE UniformID=" & ddlUniform_Kokurrikulum.SelectedValue & " AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "'"
            lblCountUniform.Text = oCommon.getFieldValuePermata(strSQL)
        End If
        '-max value uniform
        strSQL = "SELECT ConfigString FROM koko_config WHERE ConfigCODE='MAXUNIFORM' AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "'"
        lblMaxUniform.Text = oCommon.getFieldValuePermata(strSQL)


        '--PERSATUAN
        If ddlClub_Kokurrikulum.SelectedValue.Length > 0 Then
            strSQL = "SELECT COUNT(*) FROM koko_pelajar WHERE PersatuanID=" & ddlClub_Kokurrikulum.SelectedValue & " AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "'"
            lblCountClub.Text = oCommon.getFieldValuePermata(strSQL)
        End If
        '-max value persatuan
        strSQL = "SELECT ConfigString FROM koko_config WHERE ConfigCODE='MAXPERSATUAN' AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "'"
        lblMaxClub.Text = oCommon.getFieldValuePermata(strSQL)

        '--SUKAN
        If ddlSport_Kokurrikulum.SelectedValue.Length > 0 Then
            strSQL = "SELECT COUNT(*) FROM koko_pelajar WHERE SukanID=" & ddlSport_Kokurrikulum.SelectedValue & " AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "'"
            lblCountSport.Text = oCommon.getFieldValuePermata(strSQL)
        End If
        '-max value SUKAN
        strSQL = "SELECT ConfigString FROM koko_config WHERE ConfigCODE='MAXSUKAN' AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "'"
        lblMaxSport.Text = oCommon.getFieldValuePermata(strSQL)

        '--RUMAHSUKAN
        If ddlSportHouses_Kokurrikulum.SelectedValue.Length > 0 Then
            strSQL = "SELECT COUNT(*) FROM koko_pelajar WHERE RumahsukanID=" & ddlSportHouses_Kokurrikulum.SelectedValue & " AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "'"
            lblCountSportHouses.Text = oCommon.getFieldValuePermata(strSQL)
        End If
        '-max value persatuan
        strSQL = "SELECT ConfigString FROM koko_config WHERE ConfigCODE='MAXRUMAHSUKAN' AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "'"
        lblMaxSportHouses.Text = oCommon.getFieldValuePermata(strSQL)

    End Sub

    Private Sub btnRegister_Koko_Click(sender As Object, e As EventArgs) Handles btnRegister_Koko.ServerClick

        Dim findID As String = "Select StudentID from StudentProfile left join kolejadmin.dbo.student_info on  kolejadmin.dbo.student_info.student_Mykad = StudentProfile.MYKAD where kolejadmin.dbo.student_info.std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        Dim getID As String = oCommon.getFieldValuePermata(findID)

        If CInt(lblCountUniform.Text) >= CInt(lblMaxUniform.Text) Then
            ShowMessage(" Uniform Had Reach The Maximum Number ", MessageType.Error)
            Exit Sub
        End If

        If CInt(lblCountClub.Text) >= CInt(lblMaxClub.Text) Then
            ShowMessage(" Club Had Reach The Maximum Number ", MessageType.Error)
            Exit Sub
        End If

        If CInt(lblCountSport.Text) >= CInt(lblMaxSport.Text) Then
            ShowMessage(" Sport Had Reach The Maximum Number ", MessageType.Error)
            Exit Sub
        End If
        If CInt(lblCountSportHouses.Text) >= CInt(lblMaxSportHouses.Text) Then
            ShowMessage(" Sport Houses Had Reach The Maximum Number ", MessageType.Error)
            Exit Sub
        End If

        If ddlYear_Kokurrikulum.SelectedValue = Now.Year Then
            strSQL = "UPDATE koko_pelajar SET UniformID=" & ddlUniform_Kokurrikulum.SelectedValue & ",PersatuanID=" & ddlClub_Kokurrikulum.SelectedValue & ",SukanID=" & ddlSport_Kokurrikulum.SelectedValue & ",RumahsukanID=" & ddlSportHouses_Kokurrikulum.SelectedValue & " WHERE Studentid='" & getID & "' AND Tahun='" & ddlYear_Kokurrikulum.SelectedValue & "'"
            strRet = oCommon.ExecuteSQLPermata(strSQL)

            If strRet = "0" Then
                ShowMessage(" Update Student Co-curricular Information ", MessageType.Error)
            Else
                ShowMessage(" Uanble To Register Co-Curricular Information ", MessageType.Error)
            End If
        Else
            ShowMessage(" Uanble To Update Previous Year Co-Curricular Information ", MessageType.Error)
        End If

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum
End Class