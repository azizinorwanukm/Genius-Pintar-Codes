Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class spiritual_others_02
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q006")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q006").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q007")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q007").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q008")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q008").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q009")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q009").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q010")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q010").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q011")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q011").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q012")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q012").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q013")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q013").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q014")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q014").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q015")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q015").ToString
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
        ''--btnNext.Text = rm.GetString("btnNext", ci)
        lblSoalan.Text = rm.GetString("lblSoalan", ci)
        lblJawapan.Text = rm.GetString("lblJawapan", ci)

        lblQ001.Text = rm.GetString("Q006", ci)
        lblQ002.Text = rm.GetString("Q007", ci)
        lblQ003.Text = rm.GetString("Q008", ci)
        lblQ004.Text = rm.GetString("Q009", ci)
        lblQ005.Text = rm.GetString("Q010", ci)
        lblQ006.Text = rm.GetString("Q011", ci)
        lblQ007.Text = rm.GetString("Q012", ci)
        lblQ008.Text = rm.GetString("Q013", ci)
        lblQ009.Text = rm.GetString("Q014", ci)
        lblQ010.Text = rm.GetString("Q015", ci)

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


    Private Function ppcs_spiritual_update() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If

        strSQL = "UPDATE ppcs_spiritual SET LastUpdate='" & oCommon.getNow & "',Q006=" & Q1.SelectedValue & ",Q007=" & Q2.SelectedValue & ",Q008=" & Q3.SelectedValue & ",Q009=" & Q4.SelectedValue & ",Q010=" & Q5.SelectedValue & ",Q011=" & Q6.SelectedValue & ",Q012=" & Q7.SelectedValue & ",Q013=" & Q8.SelectedValue & ",Q014=" & Q9.SelectedValue & ",Q015=" & Q10.SelectedValue & " WHERE Tokenid='" & Request.QueryString("tokenid") & "' AND PPCSYear='" & Request.QueryString("year") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya kemaskini soalselidik spiritual pelajar."
        Else
            lblMsg.Text = strRet
        End If

        Return True
    End Function

    Private Function ValidatePage() As Boolean
        If Q1.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan antara 1 hingga 4."
            Q1.Focus()
            Return False
        End If

        If Q2.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan antara 1 hingga 4."
            Q2.Focus()
            Return False
        End If

        If Q3.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan antara 1 hingga 4."
            Q3.Focus()
            Return False
        End If

        If Q4.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan antara 1 hingga 4."
            Q4.Focus()
            Return False
        End If
        If Q5.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan antara 1 hingga 4."
            Q5.Focus()
            Return False
        End If
        If Q6.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan antara 1 hingga 4."
            Q6.Focus()
            Return False
        End If
        If Q7.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan antara 1 hingga 4."
            Q7.Focus()
            Return False
        End If
        If Q8.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan antara 1 hingga 4."
            Q8.Focus()
            Return False
        End If
        If Q9.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan antara 1 hingga 4."
            Q9.Focus()
            Return False
        End If
        If Q10.Text.Length = 0 Then
            lblMsg.Text = "Pilih jawapan antara 1 hingga 4."
            Q10.Focus()
            Return False
        End If

        Return True
    End Function

End Class