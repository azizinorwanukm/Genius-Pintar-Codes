Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class student_exam
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then
                setDdlSession()
                setDdlClass()
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception

        End Try
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

    Protected Sub btsearch_click(ByVal sender As Object, e As EventArgs) Handles btnSearch.Click

        strRet = BindData(datRespondent)

    End Sub

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strwhere As String = ""
        Dim strOrder As String = " ORDER BY A.student_Name"
        Dim SQLdata As String

        tmpSQL = " SELECT D.id,A.student_Name,A.student_Mykad,B.AlumniID,E.ClassCode,D.marks_80  ,D.marks_100  
                from ukm3.dbo.UKM3 D 
				LEFT JOIN ukm3.dbo.student_info A ON A.std_ID = D.student_id  
                LEFT JOIN permatapintar.dbo.StudentProfile B on A.guid = B.StudentID
                LEFT JOIN permatapintar.dbo.PPCS C ON C.StudentID = A.guid AND C.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'
                LEFT JOIN permatapintar.dbo.PPCS_Class E ON E.ClassID = C.ClassID
				 WHERE D.active = 1 AND D.session_id = '" & ddlSession.SelectedValue & "' "

        If Not txtsearch.Text.Length = 0 Then
            strwhere += " AND (A.student_name LIKE '%" & txtsearch.Text & "%'"
            strwhere += " OR A.student_Mykad LIKE '%" & txtsearch.Text & "%')"
        End If

        If Not ddlClass.SelectedValue = "0" Then
            strwhere += " AND C.ClassID = '" & ddlClass.SelectedValue & "' "
        End If
        SQLdata = tmpSQL + strwhere + strOrder
        getSQL = SQLdata

        Return getSQL
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Dim encryptid As String = strKeyID

    End Sub

    Private Sub setDdlSession()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT id, sessionName FROM UKM3Session ORDER BY isCurrent DESC, id ASC"

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
            ddlSession.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

    End Sub


    Private Sub setDdlClass()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT M.ClassID, G.ClassCode FROM( "
        query += " SELECT A.ClassID FROM permatapintar.dbo.PPCS_Class A "
        query += " JOIN permatapintar.dbo.PPCS B ON B.ClassID = A.ClassID AND B.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "' "
        query += " JOIN student_info C ON C.guid = B.StudentID "
        query += " JOIN UKM3 D ON D.student_id = C.std_ID "
        query += " WHERE D.active = 1 AND D.session_id = " & ddlSession.SelectedValue & " GROUP BY A.ClassID) M  "
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

        ddlClass.Items.Clear()

        ddlClass.Items.Add(New ListItem("-- Pilih Kelas --", 0))

        For k = 0 To quantity - 1
            ddlClass.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next
    End Sub

    Private Sub ddlSession_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSession.SelectedIndexChanged
        setDdlClass()
    End Sub
End Class