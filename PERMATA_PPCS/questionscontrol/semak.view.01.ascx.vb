Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class semak_view_01
    Inherits System.Web.UI.UserControl

    Private rm As ResourceManager

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer = 0
    Dim strDateCreated As String
    Dim strcourseCode As String
    Dim strTokenid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTokenid = Request.QueryString("tokenid")

        ''--todays date
        strDateCreated = oCommon.getToday

        Try
            Dim ci As CultureInfo
            Dim strBasename As String = "Resources.semak2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            If Not IsPostBack Then
                '--load answers
                ppcs_semak_load(strTokenid)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub


    Private Sub ppcs_semak_load(ByVal strValue As String)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM ppcs_semak WHERE Tokenid='" & strTokenid & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q001")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q001").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q002")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q002").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q003")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q003").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q004")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q004").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q005")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q005").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q006")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q006").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q007")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q007").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q008")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q008").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q009")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q009").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q010")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q010").ToString
                Else
                    Q10.SelectedValue = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        '--debug
        ''btnNext.Text = rm.GetString("btnNext", ci)
        lblSoalan.Text = rm.GetString("lblSoalan", ci)
        lblJawapan.Text = rm.GetString("lblJawapan", ci)

        lblQ001.Text = rm.GetString("Q001", ci)
        lblQ002.Text = rm.GetString("Q002", ci)
        lblQ003.Text = rm.GetString("Q003", ci)
        lblQ004.Text = rm.GetString("Q004", ci)
        lblQ005.Text = rm.GetString("Q005", ci)
        lblQ006.Text = rm.GetString("Q006", ci)
        lblQ007.Text = rm.GetString("Q007", ci)
        lblQ008.Text = rm.GetString("Q008", ci)
        lblQ009.Text = rm.GetString("Q009", ci)
        lblQ010.Text = rm.GetString("Q010", ci)

        '--answer string
        Q1.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q1.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q1.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q1.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q2.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q2.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q2.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q2.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q3.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q3.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q3.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q3.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q4.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q4.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q4.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q4.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q5.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q5.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q5.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q5.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q6.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q6.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q6.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q6.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q7.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q7.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q7.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q7.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q8.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q8.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q8.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q8.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q9.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q9.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q9.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q9.Items(3).Text = rm.GetString("AgreeLevel4", ci)


        Q10.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q10.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q10.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q10.Items(3).Text = rm.GetString("AgreeLevel4", ci)

    End Sub


    Private Function ppcs_semak_update() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If

        strSQL = "UPDATE ppcs_semak SET LastUpdate='" & oCommon.getNow & "',Q001=" & Q1.SelectedValue & ",Q002=" & Q2.SelectedValue & ",Q003=" & Q3.SelectedValue & ",Q004=" & Q4.SelectedValue & ",Q005=" & Q5.SelectedValue & ",Q006=" & Q6.SelectedValue & ",Q007=" & Q7.SelectedValue & ",Q008=" & Q8.SelectedValue & ",Q009=" & Q9.SelectedValue & ",Q010=" & Q10.SelectedValue & " WHERE Tokenid='" & strTokenid & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            ''lblMsgTop.Text = "Rekod berjaya dikemaskini."
            lblMsg.Text = "Rekod berjaya dikemaskini."
        Else
            ''lblMsgTop.Text = strRet
            lblMsg.Text = strRet
        End If

        Return True
    End Function

    Private Function ppcs_semak_insert() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If

        strSQL = "INSERT INTO ppcs_semak (Tokenid,DateCreated,CreateBy,ClassCode,LastUpdate,Q001,Q002,Q003,Q004,Q005,Q006,Q007,Q008,Q009,Q010) Values ('" & strTokenid & "','" & oCommon.getToday & "','" & Server.HtmlEncode(Request.Cookies("ppcs_myguid").Value) & "','" & Server.HtmlEncode(Request.Cookies("ppcs_classcode").Value) & "','" & oCommon.getNow & "'," & Q1.SelectedValue & "," & Q2.SelectedValue & "," & Q3.SelectedValue & "," & Q4.SelectedValue & "," & Q5.SelectedValue & "," & Q6.SelectedValue & "," & Q7.SelectedValue & "," & Q8.SelectedValue & "," & Q9.SelectedValue & "," & Q10.SelectedValue & ")"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            ''lblMsgTop.Text = "Rekod berjaya dikemaskini."
            lblMsg.Text = "Rekod berjaya dikemaskini."
        Else
            ''lblMsgTop.Text = strRet
            lblMsg.Text = strRet
            Return False
        End If

        Return True
    End Function


    Private Function ValidatePage() As Boolean
        If Q1.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q1.Focus()
            Return False
        End If

        If Q2.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q2.Focus()
            Return False
        End If

        If Q3.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q3.Focus()
            Return False
        End If

        If Q4.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q4.Focus()
            Return False
        End If
        If Q5.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q5.Focus()
            Return False
        End If
        If Q6.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q6.Focus()
            Return False
        End If
        If Q7.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q7.Focus()
            Return False
        End If
        If Q8.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q8.Focus()
            Return False
        End If
        If Q9.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q9.Focus()
            Return False
        End If
        If Q10.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q10.Focus()
            Return False
        End If

        Return True
    End Function

    ''Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click

    ''    Response.Redirect("pengajar.pelajar.semak.view.02.aspx?tokenid=" & strTokenid)

    ''End Sub

End Class