Imports System.Data.SqlClient

Public Class ukm1_toplist
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction

    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Dim time As DateTime = DateTime.Parse(oCommon.getFieldValue("SELECT configDate FROM master_config_date WHERE configString = 'UKM1SchoolRank'"))
            Dim format As String = "d MMMM yyyy hh:mm tt "
            lblDate.Text = time.ToString(format)

            If Not IsPostBack Then
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception
            Response.Redirect("settings.aspx")
        End Try

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter("SELECT * FROM SchoolRank ORDER BY Jumlah DESC", strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai."
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

    Private Sub btnKemaskini_Click(sender As Object, e As EventArgs) Handles btnKemaskini.Click

        Dim updateSql As String = "TRUNCATE TABLE SchoolRank "
        updateSql += " INSERT INTO SchoolRank (SchoolID,SchoolState,SchoolName,Jumlah) "
        updateSql += " SELECT TOP 10 A.SchoolID,A.SchoolState,A.SchoolName,(SELECT COUNT(*) FROM UKM1 B WHERE A.SchoolID=B.SchoolID AND B.examyear_id = " & Common.getDefaultExamYearID() & "  AND B.IsCount=1) as Jumlah FROM SchoolProfile A  WHERE A.IsDeleted='N' ORDER BY Jumlah DESC "
        updateSql += " UPDATE master_config_date SET configDate = GETDATE() WHERE configString = 'UKM1SchoolRank'"

        Try
            oCommon.ExecuteSQL(updateSql)

            Dim time As DateTime = DateTime.Parse(oCommon.getFieldValue("SELECT configDate FROM master_config_date WHERE configString = 'UKM1SchoolRank'"))
            Dim format As String = "d MMMM yyyy hh:mm tt "
            lblDate.Text = time.ToString(format)

            lblDebug.Text = "Kemaskini berjaya!"

        Catch ex As Exception
            lblDebug.Text = "Error: " & ex.Message
        End Try

    End Sub

End Class