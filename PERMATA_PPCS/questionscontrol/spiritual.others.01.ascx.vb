Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class spiritual_others_01
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim ci As CultureInfo
            Dim strBasename As String = "Resources.spiritual_others2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            If Not IsPostBack Then
                '--load answers
                ppcs_spiritual_load(Request.QueryString("tokenid"))

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
            Response.Cookies("ppcs_culture").Value = "ms-MY"
        End Try

    End Sub

    Private Sub ppcs_spiritual_load(ByVal strValue As String)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM ppcs_spiritual WHERE Tokenid='" & Request.QueryString("tokenid") & "' AND PPCSYear='" & Request.QueryString("year") & "'"
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

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        '--debug
        ''--btnNext.Text = rm.GetString("btnNext", ci)
        lblSoalan.Text = rm.GetString("lblSoalan", ci)
        lblJawapan.Text = rm.GetString("lblJawapan", ci)

        lblQ001.Text = rm.GetString("Q001", ci)
        lblQ002.Text = rm.GetString("Q002", ci)
        lblQ003.Text = rm.GetString("Q003", ci)
        lblQ004.Text = rm.GetString("Q004", ci)
        lblQ005.Text = rm.GetString("Q005", ci)


        '--answer string
        Q1.Items(0).Text = rm.GetString("Q001_1", ci)
        Q1.Items(1).Text = rm.GetString("Q001_2", ci)
        Q1.Items(2).Text = rm.GetString("Q001_3", ci)
        Q1.Items(3).Text = rm.GetString("Q001_4", ci)

        Q2.Items(0).Text = rm.GetString("Q002_1", ci)
        Q2.Items(1).Text = rm.GetString("Q002_2", ci)
        Q2.Items(2).Text = rm.GetString("Q002_3", ci)
        Q2.Items(3).Text = rm.GetString("Q002_4", ci)

        Q3.Items(0).Text = rm.GetString("Q003_1", ci)
        Q3.Items(1).Text = rm.GetString("Q003_2", ci)
        Q3.Items(2).Text = rm.GetString("Q003_3", ci)
        Q3.Items(3).Text = rm.GetString("Q003_4", ci)

        Q4.Items(0).Text = rm.GetString("Q004_1", ci)
        Q4.Items(1).Text = rm.GetString("Q004_2", ci)
        Q4.Items(2).Text = rm.GetString("Q004_3", ci)
        Q4.Items(3).Text = rm.GetString("Q004_4", ci)

        Q5.Items(0).Text = rm.GetString("Q005_1", ci)
        Q5.Items(1).Text = rm.GetString("Q005_2", ci)
        Q5.Items(2).Text = rm.GetString("Q005_3", ci)
        Q5.Items(3).Text = rm.GetString("Q005_4", ci)

    End Sub


    Private Function ppcs_spiritual_update() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If

        strSQL = "UPDATE ppcs_spiritual SET LastUpdate='" & oCommon.getNow & "',Q001=" & Q1.SelectedValue & ",Q002=" & Q2.SelectedValue & ",Q003=" & Q3.SelectedValue & ",Q004=" & Q4.SelectedValue & ",Q005=" & Q5.SelectedValue & " WHERE Tokenid='" & Request.QueryString("tokenid") & "' AND PPCSYear='" & Request.QueryString("year") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya kemaskini soalselidik spiritual pelajar."
        Else
            lblMsg.Text = strRet
        End If

        Return True
    End Function

    Private Function ppcs_spiritual_insert() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If

        strSQL = "INSERT INTO ppcs_spiritual (Tokenid,PPCSYear,DateCreated,CreateBy,ClassCode,LastUpdate,Q001,Q002,Q003,Q004,Q005) Values ('" & Request.QueryString("tokenid") & "','" & Request.QueryString("year") & "','" & oCommon.getToday & "','" & Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value) & "','" & Server.HtmlEncode(Request.Cookies("ppcs_classcode").Value) & "','" & oCommon.getNow & "'," & Q1.SelectedValue & "," & Q2.SelectedValue & "," & Q3.SelectedValue & "," & Q4.SelectedValue & "," & Q5.SelectedValue & ")"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya kemaskini penaksiran pelajar."
        Else
            lblMsg.Text = strRet
            Return False
        End If

        Return True
    End Function


    Private Function ValidatePage() As Boolean
        If Q1.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan yang disediakan."
            Q1.Focus()
            Return False
        End If

        If Q2.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan yang disediakan."
            Q2.Focus()
            Return False
        End If

        If Q3.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan yang disediakan."
            Q3.Focus()
            Return False
        End If

        If Q4.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan yang disediakan."
            Q4.Focus()
            Return False
        End If
        If Q5.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan yang disediakan."
            Q5.Focus()
            Return False
        End If

        Return True
    End Function

End Class