Imports System.Data.SqlClient

Public Class ukm3_adminPPCS_resetPenilaian
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnConfirm.Attributes.Add("onclick", "return confirm('Pengesahan akan di reset, membenarkan instruktor mengubah penilaian');")
        setDdlSession()
        setDdlClass()
        setDDLInstruktor()
    End Sub

    Private Sub setDdlSession()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT id, sessionName FROM UKM3Session ORDER BY id DESC"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        For k = 0 To quantity - 1
            ddlSesi.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

        Dim currentSession As String = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'currentSession'")

        ddlSesi.SelectedValue = currentSession

    End Sub

    Private Sub setDdlClass()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT M.ClassID, G.ClassCode FROM( "
        query += " SELECT A.ClassID FROM permatapintar.dbo.PPCS_Class A "
        query += " JOIN permatapintar.dbo.PPCS B ON B.ClassID = A.ClassID AND B.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSesi.SelectedValue) & "' "
        query += " JOIN student_info C ON C.guid = B.StudentID "
        query += " JOIN UKM3 D ON D.student_id = C.std_ID "
        query += " WHERE D.active = 1 AND D.session_id = " & ddlSesi.SelectedValue & " GROUP BY A.ClassID) M  "
        query += " LEFT JOIN permatapintar.dbo.PPCS_Class G ON G.ClassID = M.ClassID "

        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        ddlKelas.Items.Clear()

        ddlKelas.Items.Add(New ListItem("-- Pilih Kelas --", 0))

        For k = 0 To quantity - 1
            ddlKelas.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next
    End Sub

    Private Sub setDDLInstruktor()
        Dim attachmentsTable = New DataTable
        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT Distinct E.NamaPengajar as staff_name,I.stf_id 

                from ukm3.dbo.UKM3 D
			    LEFT JOIN student_info A ON A.std_ID = D.student_id  
                LEFT JOIN permatapintar.dbo.StudentProfile B on D.std_guid = B.StudentID
                LEFT JOIN permatapintar.dbo.PPCS C ON C.StudentID = A.guid AND C.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSesi.SelectedValue) & "' 
                LEFT JOIN permatapintar.dbo.PPCS_Class E ON E.ClassID = C.ClassID
				LEFT JOIN ukm3.dbo.staff_info I on I.staff_name  = E.NamaPengajar 
                LEFT JOIN instruktorExam_result_kpp F ON F.ukm3id = D.id
                LEFT JOIN staff_info G ON G.stf_id = F.stf_id
                LEFT JOIN (SELECT ukm3id,instruktorExam_marks, dateValid FROM instruktorExam_result_kpp ) M ON M.ukm3id = D.id
                LEFT JOIN permatapintar.dbo.StudentSchool H on H.StudentID = D.std_guid and H.IsLatest='Y'
				where E.NamaPengajar is not null AND D.session_id = " & ddlSesi.SelectedValue & ""
        Dim strconn As String = ConfigurationManager.AppSettings("ConnectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        ddlNamaInstruktor.Items.Clear()

        ddlNamaInstruktor.Items.Add(New ListItem("-- Pilih Instruktor --", 0))

        For k = 0 To quantity - 1
            ddlNamaInstruktor.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(0).ToString, attachmentsTable.Rows(k).Item(1).ToString))
        Next
    End Sub

    Private Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        strSQL = "UPDATE ukm3.dbo.UKM3,ukm3.dbo.instruktorExam_result SET ukm3.dbo.UKM3.Ppcs_update = null, ukm3.dbo.instruktorExam_result.saved = '0' where ukm3.dbo.UKM3.ppcs_id = '" & ddlNamaInstruktor.SelectedValue & "'"
        Debug.WriteLine(strSQL)
        Try
            strRet = oCommon.ExecuteSQL(strSQL)

            'If Not strRet = True Then
            '    lblMsg.Text = "Error : " & strRet
            'Else
            '    lblMsg.Text = "Reset Pengesahan Pelajar Berjaya!!"
            'End If

        Catch ex As Exception
        End Try
    End Sub
End Class