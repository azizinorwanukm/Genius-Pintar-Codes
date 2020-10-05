Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources


Partial Public Class pengkomputeran_view_04
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

            If Not IsPostBack Then
                ''--load answer string
                LoadStrings(ci)

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
                Dim strQ049 As String
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q049")) Then
                    strQ049 = MyTable.Rows(nRows).Item("Q049").ToString
                Else
                    strQ049 = ""
                End If
                dispCheckbox(strQ049, cblQ001)

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q049_1")) Then
                    txtQ002.Text = MyTable.Rows(nRows).Item("Q049_1").ToString
                Else
                    txtQ002.Text = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub dispCheckbox(ByVal strValue As String, ByVal cblCheckbox As CheckBoxList)
        Dim i As Integer = 0

        For i = 1 To strValue.Length
            If Mid(strValue, i, 1) = "1" Then
                cblCheckbox.Items(i - 1).Selected = True
            Else
                cblCheckbox.Items(i - 1).Selected = False
            End If
        Next

    End Sub


    Private Sub LoadStrings(ByVal ci As CultureInfo)
        '--debug
        lblSoalan.Text = rm.GetString("lblSoalan", ci)
        lblJawapan.Text = rm.GetString("lblJawapan", ci)

        lblQ001.Text = rm.GetString("Q049", ci)

        '--answer string
        cblQ001.Items.Clear()
        cblQ001.Items.Add(rm.GetString("Q049_1", ci))
        cblQ001.Items.Add(rm.GetString("Q049_2", ci))
        cblQ001.Items.Add(rm.GetString("Q049_3", ci))
        cblQ001.Items.Add(rm.GetString("Q049_4", ci))
        cblQ001.Items.Add(rm.GetString("Q049_5", ci))
        cblQ001.Items.Add(rm.GetString("Q049_6", ci))

    End Sub


End Class