Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System
Imports System.Net
Imports System.Globalization

Public Class lecturer_koko_searchStudent
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

                strSQL = "  Select InstruktorID from koko_instruktor A 
                            left join kolejadmin.dbo.staff_Info B on A.MYKAD = B.staff_Mykad
                            where A.Tahun = '" & ddlKoko_Year.SelectedValue & "' and B.stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
                strRet = oCommon.getFieldValue_Permata(strSQL)

                koko_YearList()
                koko_ProgramList()
                koko_LeveList()
                koko_ClassList()

                koko_TypeList()
                koko_NameList()

                strRet = BindData(datRespondent)

            End If
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

    Private Sub koko_LeveList()
        strSQL = "SELECT Parameter FROM setting WHERE Type='Level' "
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Level.DataSource = ds
            ddlKoko_Level.DataTextField = "Parameter"
            ddlKoko_Level.DataValueField = "Parameter"
            ddlKoko_Level.DataBind()
            ddlKoko_Level.Items.Insert(0, New ListItem("Select Level", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub koko_ClassList()

        strSQL = "Select staff_Campus from staff_Info where stf_ID = '" & oCommon.Staff_securityLogin(Request.QueryString("stf_ID")) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        strSQL = "Select class_ID, class_Name from class_info where class_year = '" & ddlKoko_Year.SelectedValue & "' and class_Campus = '" & strRet & "' and course_Program = '" & ddlKoko_Program.SelectedValue & "' and class_Level = '" & ddlKoko_Level.SelectedValue & "' and class_Type = 'Compulsory' order by class_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Class.DataSource = ds
            ddlKoko_Class.DataTextField = "class_Name"
            ddlKoko_Class.DataValueField = "class_Name"
            ddlKoko_Class.DataBind()
            ddlKoko_Class.Items.Insert(0, New ListItem("Select Class", String.Empty))

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub koko_TypeList()
        strSQL = "Select Distinct Jenis from koko_kolejpermata where Tahun = '" & ddlKoko_Year.SelectedValue & "'"
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
        strSQL = "SELECT Distinct NamaBI from koko_kolejpermata where Jenis = '" & ddlKoko_Type.SelectedValue & "'and Tahun = '" & ddlKoko_Year.SelectedValue & "' and Kampus = '" & Session("SchoolCampus") & "'"
        Dim strConnPermata As String = ConfigurationManager.AppSettings("ConnectionPermata")
        Dim objConnPermata As SqlConnection = New SqlConnection(strConnPermata)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConnPermata)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKoko_Name.DataSource = ds
            ddlKoko_Name.DataTextField = "NamaBI"
            ddlKoko_Name.DataValueField = "NamaBI"
            ddlKoko_Name.DataBind()
            ddlKoko_Name.Items.Insert(0, New ListItem("Select Name", String.Empty))
        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Protected Sub ddlKoko_Program_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Program.SelectedIndexChanged
        Try
            koko_ClassList()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKoko_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Year.SelectedIndexChanged
        Try
            koko_TypeList()
            koko_ClassList()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKoko_Level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Level.SelectedIndexChanged
        Try
            koko_ClassList()
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ddlKoko_Class_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKoko_Class.SelectedIndexChanged
        Try
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

            If ddlKoko_Type.SelectedIndex = 0 Or ddlKoko_Type.SelectedValue = "UNIFORM" Then
                gvTable.Columns(5).Visible = True
                gvTable.Columns(6).Visible = False
                gvTable.Columns(7).Visible = False

            ElseIf ddlKoko_Type.SelectedValue = "PERSATUAN" Then
                gvTable.Columns(5).Visible = False
                gvTable.Columns(6).Visible = True
                gvTable.Columns(7).Visible = False

            ElseIf ddlKoko_Type.SelectedValue = "SUKAN" Then
                gvTable.Columns(5).Visible = False
                gvTable.Columns(6).Visible = False
                gvTable.Columns(7).Visible = True
            End If

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " order by student_Name ASC"

        tmpSQL = "SELECT distinct A.std_ID , UPPER(A.student_Name) student_Name, A.student_ID, koko_kelas.Kelas,
                  (SELECT NamaBI FROM koko_kolejpermata WHERE koko_pelajar.UniformID=koko_kolejpermata.KokoID) as Uniform,
                  (SELECT NamaBI FROM koko_kolejpermata WHERE koko_pelajar.PersatuanID=koko_kolejpermata.KokoID) as Persatuan,
                  (SELECT NamaBI FROM koko_kolejpermata WHERE koko_pelajar.SukanID=koko_kolejpermata.KokoID) as Sukan,
                  (SELECT NamaBI FROM koko_kolejpermata WHERE koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID) as RumahSukan
                  FROM koko_pelajar
                  LEFT OUTER JOIN StudentProfile ON koko_pelajar.StudentID=StudentProfile.StudentID
                  LEFT OUTER JOIN koko_kelas ON koko_pelajar.KelasID=koko_kelas.KelasID
                  LEFT OUTER JOIN kolejadmin.dbo.student_info A ON StudentProfile.MYKAD = A.student_Mykad
                  LEFT OUTER JOIN koko_kolejpermata ON koko_pelajar.UniformID=koko_kolejpermata.KokoID OR koko_pelajar.PersatuanID=koko_kolejpermata.KokoID OR koko_pelajar.SukanID=koko_kolejpermata.KokoID OR koko_pelajar.RumahSukanID=koko_kolejpermata.KokoID"

        strWhere = " WHERE koko_pelajar.Tahun ='" & ddlKoko_Year.SelectedValue & "' AND A.student_Status = 'Access' and A.student_ID is not null and A.student_ID <> '' and (A.student_ID like '%M%' or A.student_ID like '%P%')"

        strWhere += " and A.student_Campus = '" & Session("SchoolCampus") & "' AND A.student_Stream = '" & ddlKoko_Program.SelectedValue & "' and koko_kelas.Tahun = '" & ddlKoko_Year.SelectedValue & "'"

        If ddlKoko_Type.SelectedIndex > 0 Then
            If ddlKoko_Name.SelectedIndex > 0 Then
                strWhere += " AND koko_kolejpermata.NamaBI = '" & ddlKoko_Name.SelectedValue & "' "
            End If
        End If

        If ddlKoko_Class.SelectedIndex > 0 Then
            strWhere += " and koko_kelas.Kelas = '" & ddlKoko_Class.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL
    End Function

End Class