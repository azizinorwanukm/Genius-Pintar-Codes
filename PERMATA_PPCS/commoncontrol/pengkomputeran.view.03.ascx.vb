Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class pengkomputeran_view_03
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
            Dim strBasename As String = "Resources.komputer2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            If Not IsPostBack Then
                '--load answers
                ppcs_komputer_load(Request.QueryString("tokenid"))
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
            Response.Cookies("ppcs_culture").Value = "ms-MY"
        End Try
    End Sub

    Private Sub ppcs_komputer_load(ByVal strValue As String)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM ppcs_komputer WHERE Tokenid='" & Request.QueryString("tokenid") & "' AND PPCSYear='" & Request.QueryString("year") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q034")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q034").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q035")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q035").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q036")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q036").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q037")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q037").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q038")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q038").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q039")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q039").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q040")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q040").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q041")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q041").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q042")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q042").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q043")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q043").ToString
                Else
                    Q10.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q044")) Then
                    Q11.Text = MyTable.Rows(nRows).Item("Q044").ToString
                Else
                    Q11.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q045")) Then
                    Q12.Text = MyTable.Rows(nRows).Item("Q045").ToString
                Else
                    Q12.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q046")) Then
                    Q13.Text = MyTable.Rows(nRows).Item("Q046").ToString
                Else
                    Q13.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q047")) Then
                    Q14.Text = MyTable.Rows(nRows).Item("Q047").ToString
                Else
                    Q14.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q048")) Then
                    Q15.Text = MyTable.Rows(nRows).Item("Q048").ToString
                Else
                    Q15.SelectedValue = ""
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
        lblSoalan.Text = rm.GetString("lblSoalan", ci)
        lblJawapan.Text = rm.GetString("lblJawapan", ci)

        lblQ001.Text = rm.GetString("Q034", ci)
        lblQ002.Text = rm.GetString("Q035", ci)
        lblQ003.Text = rm.GetString("Q036", ci)
        lblQ004.Text = rm.GetString("Q037", ci)
        lblQ005.Text = rm.GetString("Q038", ci)
        lblQ006.Text = rm.GetString("Q039", ci)
        lblQ007.Text = rm.GetString("Q040", ci)
        lblQ008.Text = rm.GetString("Q041", ci)
        lblQ009.Text = rm.GetString("Q042", ci)
        lblQ010.Text = rm.GetString("Q043", ci)
        lblQ011.Text = rm.GetString("Q044", ci)
        lblQ012.Text = rm.GetString("Q045", ci)
        lblQ013.Text = rm.GetString("Q046", ci)
        lblQ014.Text = rm.GetString("Q047", ci)
        lblQ015.Text = rm.GetString("Q048", ci)

       
    End Sub


End Class