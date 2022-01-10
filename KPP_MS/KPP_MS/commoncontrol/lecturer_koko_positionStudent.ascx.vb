Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System
Imports System.Net
Imports System.Globalization

Public Class lecturer_koko_positionStudent
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
    Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)

    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Dim stfID As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                koko_YearList()
                koko_ProgramList()
                koko_NameList()
                koko_jawatan_list()

                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub koko_jawatan_list()
        strSQL = "SELECT UPPER(Jawatan) Jawatan, JawatanID FROM koko_jawatan ORDER BY JawatanID ASC"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_KokoPosition.DataSource = ds
            ddl_KokoPosition.DataTextField = "Jawatan"
            ddl_KokoPosition.DataValueField = "Jawatan"
            ddl_KokoPosition.DataBind()

        Catch ex As Exception
        End Try

    End Sub

    Private Sub koko_YearList()
        strSQL = "SELECT Tahun FROM koko_tahun ORDER BY Tahun ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Year.DataSource = ds
            ddlKoko_Year.DataTextField = "Tahun"
            ddlKoko_Year.DataValueField = "Tahun"
            ddlKoko_Year.DataBind()
            ddlKoko_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlKoko_Year.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub koko_ProgramList()

        strSQL = "Select staff_Campus from staff_Info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet = "APP" Then
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' and Value = 'PS'"
        Else
            strSQL = "SELECT Parameter, Value FROM setting WHERE Type='Stream' "
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Program.DataSource = ds
            ddlKoko_Program.DataTextField = "Parameter"
            ddlKoko_Program.DataValueField = "Value"
            ddlKoko_Program.DataBind()
            ddlKoko_Program.Items.Insert(0, New ListItem("Select Program", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub koko_TypeList()

        ''Ketua Uniform
        Dim find_KetuaUniform As String = "Select koko_instruktor.KetuaUniform from koko_instruktor left join kolejadmin.dbo.staff_info A on A.staff_Mykad = koko_instruktor.MYKAD where A.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        Dim get_KetuaUniform As String = oCommon.getFieldValue_Permata(find_KetuaUniform)

        ''Ketua Persatuan
        Dim find_KetuaPersatuan As String = "Select koko_instruktor.KetuaPersatuan from koko_instruktor left join kolejadmin.dbo.staff_info A on A.staff_Mykad = koko_instruktor.MYKAD where A.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        Dim get_KetuaPersatuan As String = oCommon.getFieldValue_Permata(find_KetuaPersatuan)

        ''Ketua Sukan
        Dim find_KetuaSukan As String = "Select koko_instruktor.KetuaSukan from koko_instruktor left join kolejadmin.dbo.staff_info A on A.staff_Mykad = koko_instruktor.MYKAD where A.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        Dim get_KetuaSukan As String = oCommon.getFieldValue_Permata(find_KetuaSukan)

        ''Ketua Rumah Sukan
        Dim find_KetuaRumahSukan As String = "Select koko_instruktor.KetuaRumahSukan from koko_instruktor left join kolejadmin.dbo.staff_info A on A.staff_Mykad = koko_instruktor.MYKAD where A.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        Dim get_KetuaRumahSukan As String = oCommon.getFieldValue_Permata(find_KetuaRumahSukan)


        If get_KetuaUniform = "True" Or get_KetuaPersatuan = "True" Or get_KetuaSukan = "True" Or get_KetuaRumahSukan = "True" Then

            strSQL = "Select Distinct Jenis from koko_kolejpermata where Tahun = '" & ddlKoko_Year.SelectedValue & "' and ("

            If get_KetuaUniform = "True" Then
                strSQL += " Jenis = 'UNIFORM' "
            End If

            If get_KetuaPersatuan = "True" And get_KetuaUniform = "False" Then
                strSQL += " Jenis = 'PERSATUAN' "
            ElseIf get_KetuaPersatuan <> "False" Then
                strSQL += " or Jenis = 'PERSATUAN' "
            End If

            If get_KetuaSukan = "True" And get_KetuaPersatuan = "False" And get_KetuaUniform = "False" Then
                strSQL += " Jenis = 'SUKAN' "
            ElseIf get_KetuaSukan <> "False" Then
                strSQL += " or Jenis = 'SUKAN' "
            End If

            If get_KetuaRumahSukan = "True" And get_KetuaSukan = "False" And get_KetuaPersatuan = "False" And get_KetuaUniform = "False" Then
                strSQL += " Jenis = 'RUMAHSUKAN' "
            ElseIf get_KetuaRumahSukan <> "False" Then
                strSQL += " or Jenis = 'RUMAHSUKAN' "
            End If

            strSQL += ") and IsMandatory = 'N' "
        Else
            strSQL = " "
        End If

        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Type.DataSource = ds
            ddlKoko_Type.DataTextField = "Jenis"
            ddlKoko_Type.DataValueField = "Jenis"
            ddlKoko_Type.DataBind()
            ddlKoko_Type.Items.Insert(0, New ListItem("Select Type", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub koko_NameList()
        strSQL = "SELECT Distinct NamaBI from koko_kolejpermata where Jenis = '" & ddlKoko_Type.SelectedValue & "' and Tahun = '" & ddlKoko_Year.SelectedValue & "' "

        If ddlKoko_Type.SelectedValue = "UNIFORM" Then
            strSQL = "  Select Distinct NamaBI, KokoID from koko_kolejpermata 
                        left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.UniformID
                        left join kolejadmin.dbo.staff_Info on koko_instruktor.MYKAD = kolejadmin.dbo.staff_Info.staff_Mykad
                        Where kolejadmin.dbo.staff_Info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and koko_kolejpermata.Tahun = '" & ddlKoko_Year.SelectedValue & "' and koko_instruktor.Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        ElseIf ddlKoko_Type.SelectedValue = "PERSATUAN" Then
            strSQL = "  Select Distinct NamaBI, KokoID from koko_kolejpermata 
                        left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.PersatuanID
                        left join kolejadmin.dbo.staff_Info on koko_instruktor.MYKAD = kolejadmin.dbo.staff_Info.staff_Mykad
                        Where kolejadmin.dbo.staff_Info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and koko_kolejpermata.Tahun = '" & ddlKoko_Year.SelectedValue & "' and koko_instruktor.Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        ElseIf ddlKoko_Type.SelectedValue = "SUKAN" Then
            strSQL = "  Select Distinct NamaBI, KokoID from koko_kolejpermata 
                        left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.SukanID
                        left join kolejadmin.dbo.staff_Info on koko_instruktor.MYKAD = kolejadmin.dbo.staff_Info.staff_Mykad
                        Where kolejadmin.dbo.staff_Info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and koko_kolejpermata.Tahun = '" & ddlKoko_Year.SelectedValue & "' and koko_instruktor.Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        ElseIf ddlKoko_Type.SelectedValue = "RUMAHSUKAN" Then
            strSQL = "  Select Distinct NamaBI, KokoID from koko_kolejpermata 
                        left join koko_instruktor on koko_kolejpermata.KokoID = koko_instruktor.RumahsukanID
                        left join kolejadmin.dbo.staff_Info on koko_instruktor.MYKAD = kolejadmin.dbo.staff_Info.staff_Mykad
                        Where kolejadmin.dbo.staff_Info.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "' and koko_kolejpermata.Tahun = '" & ddlKoko_Year.SelectedValue & "' and koko_instruktor.Tahun = '" & ddlKoko_Year.SelectedValue & "'"
        End If

        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Name.DataSource = ds
            ddlKoko_Name.DataTextField = "NamaBI"
            ddlKoko_Name.DataValueField = "KokoID"
            ddlKoko_Name.DataBind()
            ddlKoko_Name.Items.Insert(0, New ListItem("Select Name", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlKoko_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Program.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKoko_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Year.SelectedIndexChanged
        Try
            koko_TypeList()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKoko_Type_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Type.SelectedIndexChanged
        Try
            koko_NameList()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKoko_Name_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Name.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConnPermata)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")
            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConnPermata.Close()

            If ddlKoko_Type.SelectedValue = "UNIFORM" Then
                gvTable.Columns(6).Visible = False
                gvTable.Columns(7).Visible = True
                gvTable.Columns(8).Visible = False
                gvTable.Columns(9).Visible = False

            ElseIf ddlKoko_Type.SelectedValue = "PERSATUAN" Then
                gvTable.Columns(6).Visible = False
                gvTable.Columns(7).Visible = False
                gvTable.Columns(8).Visible = True
                gvTable.Columns(9).Visible = False

            ElseIf ddlKoko_Type.SelectedValue = "SUKAN" Then
                gvTable.Columns(6).Visible = False
                gvTable.Columns(7).Visible = False
                gvTable.Columns(8).Visible = False
                gvTable.Columns(9).Visible = True

            ElseIf ddlKoko_Type.SelectedValue = "RUMAHSUKAN" Then
                gvTable.Columns(6).Visible = True
                gvTable.Columns(7).Visible = False
                gvTable.Columns(8).Visible = False
                gvTable.Columns(9).Visible = False

            Else
                gvTable.Columns(6).Visible = False
                gvTable.Columns(7).Visible = False
                gvTable.Columns(8).Visible = False
                gvTable.Columns(9).Visible = False
            End If

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function getSQL() As String

        strSQL = "Select staff_Campus from staff_Info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " order by student_Name ASC"

        tmpSQL = "  SELECT distinct A.std_ID , UPPER(A.student_Name) student_Name, A.student_Mykad, A.student_ID, koko_kelas.Kelas, UPPER(koko_pelajar.Jawatan_Sukan) Jawatan_Sukan,
                    UPPER(koko_pelajar.Jawatan_Uniform) Jawatan_Uniform, UPPER(koko_pelajar.Jawatan_Persatuan) Jawatan_Persatuan, UPPER(koko_pelajar.Jawatan_Rumahsukan) Jawatan_Rumahsukan
                    FROM koko_pelajar
                    LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                    LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                    LEFT OUTER JOIN kolejadmin.dbo.student_info A ON StudentProfile.MYKAD = A.student_Mykad
                    LEFT OUTER JOIN koko_kolejpermata ON koko_pelajar.UniformID=koko_kolejpermata.KokoID OR koko_pelajar.PersatuanID=koko_kolejpermata.KokoID OR koko_pelajar.SukanID=koko_kolejpermata.KokoID OR koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID"

        strWhere = " WHERE koko_pelajar.Tahun ='" & ddlKoko_Year.SelectedValue & "' AND A.student_Status = 'Access' and A.student_ID is not null and A.student_ID <> '' and (A.student_ID like '%M%' or A.student_ID like '%P%')"

        strWhere += " AND A.student_Stream = '" & ddlKoko_Program.SelectedValue & "' and koko_kelas.Tahun = '" & ddlKoko_Year.SelectedValue & "' and A.student_Campus = '" & strRet & "'"

        strWhere += " AND koko_kolejpermata.KokoID = '" & ddlKoko_Name.SelectedValue & "' AND koko_kolejpermata.Tahun = '" & ddlKoko_Year.SelectedValue & "' "

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL
    End Function

    Private Sub BtnUpdate_ServerClick(sender As Object, e As EventArgs) Handles BtnUpdate.ServerClick

        strSQL = ""
        Dim i As Integer = 0

        For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
            Dim chkUpdate As CheckBox = CType(datRespondent.Rows(i).Cells(5).FindControl("chkSelect"), CheckBox)

            If Not chkUpdate Is Nothing Then
                ' Get the values of textboxes using findControl
                Dim strKey As String = datRespondent.DataKeys(i).Value.ToString
                If chkUpdate.Checked = True Then

                    ''Get Student MYKAD
                    Dim find_StdMykad As String = "Select student_Mykad from student_info where std_ID = '" & strKey & "'"
                    Dim get_StdMykad As String = oCommon.getFieldValue(strSQL)

                    ''Get StudentID From Table Permatapintar
                    Dim find_StdID As String = "Select StudentID from StudentProfile where MYKAD = '" & get_StdMykad & "'"
                    Dim get_StdID As String = oCommon.getFieldValue_Permata(find_StdID)


                    If ddlKoko_Type.SelectedValue = "UNIFORM" Then
                        strSQL = "UPDATE koko_pelajar SET Jawatan_Uniform='" & ddl_KokoPosition.SelectedValue & "' WHERE StudentID='" & get_StdID & "' AND Tahun='" & ddlKoko_Year.SelectedValue & "'"
                    ElseIf ddlKoko_Type.SelectedValue = "PERSATUAN" Then
                        strSQL = "UPDATE koko_pelajar SET Jawatan_Persatuan='" & ddl_KokoPosition.SelectedValue & "' WHERE StudentID='" & get_StdID & "' AND Tahun='" & ddlKoko_Year.SelectedValue & "'"
                    ElseIf ddlKoko_Type.SelectedValue = "SUKAN" Then
                        strSQL = "UPDATE koko_pelajar SET Jawatan_Sukan='" & ddl_KokoPosition.SelectedValue & "' WHERE StudentID='" & get_StdID & "' AND Tahun='" & ddlKoko_Year.SelectedValue & "'"
                    ElseIf ddlKoko_Type.SelectedValue = "RUMAHSUKAN" Then
                        strSQL = "UPDATE koko_pelajar SET Jawatan_Rumahsukan='" & ddl_KokoPosition.SelectedValue & "' WHERE StudentID='" & get_StdID & "' AND Tahun='" & ddlKoko_Year.SelectedValue & "'"
                    End If

                    strRet = oCommon.ExecuteSQLPermata(strSQL)

                End If
            End If
        Next

        If strRet = "0" Then
            ShowMessage(" Update Student Position", MessageType.Success)
        Else
            ShowMessage(" Unsuccessful Update Student Position", MessageType.Error)
        End If

        strRet = BindData(datRespondent)
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class