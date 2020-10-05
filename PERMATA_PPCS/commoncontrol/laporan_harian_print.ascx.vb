Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class laporan_harian_print
    Inherits System.Web.UI.UserControl

    Private rm As ResourceManager

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer = 0
    Dim strcourseCode As String
    Dim strStudentID As String
    Dim strDateCreated As String
    Dim strppcsevalid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strStudentID = Request.QueryString("studentid")
        strppcsevalid = Request.QueryString("ppcsevalid")

        Try
            Dim ci As CultureInfo
            Dim strBasename As String = "Resources.eval2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            If Not IsPostBack Then
                '--load answers
                ppcs_eval_load(strppcsevalid)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub


    Private Sub ppcs_eval_load(ByVal strValue As String)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM PPCS_Eval_Daily WHERE ppcsevalid='" & strValue & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("DateCreated")) Then
                    strDateCreated = MyTable.Rows(nRows).Item("DateCreated").ToString
                Else
                    strDateCreated = ""
                End If
                lblMsgTop.Text = "Tarikh: " & strDateCreated

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q001")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q001").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q001Remarks")) Then
                    lblQ001Remarks.Text = MyTable.Rows(nRows).Item("Q001Remarks").ToString
                Else
                    lblQ001Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q002")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q002").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q002Remarks")) Then
                    lblQ002Remarks.Text = MyTable.Rows(nRows).Item("Q002Remarks").ToString
                Else
                    lblQ002Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q003")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q003").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q003Remarks")) Then
                    lblQ003Remarks.Text = MyTable.Rows(nRows).Item("Q003Remarks").ToString
                Else
                    lblQ003Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q004")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q004").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q004Remarks")) Then
                    lblQ004Remarks.Text = MyTable.Rows(nRows).Item("Q004Remarks").ToString
                Else
                    lblQ004Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q005")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q005").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q005Remarks")) Then
                    lblQ005Remarks.Text = MyTable.Rows(nRows).Item("Q005Remarks").ToString
                Else
                    lblQ005Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q006")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q006").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q006Remarks")) Then
                    lblQ006Remarks.Text = MyTable.Rows(nRows).Item("Q006Remarks").ToString
                Else
                    lblQ006Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q007")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q007").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q007Remarks")) Then
                    lblQ007Remarks.Text = MyTable.Rows(nRows).Item("Q007Remarks").ToString
                Else
                    lblQ007Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q008")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q008").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q008Remarks")) Then
                    lblQ008Remarks.Text = MyTable.Rows(nRows).Item("Q008Remarks").ToString
                Else
                    lblQ008Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q009")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q009").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q009Remarks")) Then
                    lblQ009Remarks.Text = MyTable.Rows(nRows).Item("Q009Remarks").ToString
                Else
                    lblQ009Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q010")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q010").ToString
                Else
                    Q10.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q010Remarks")) Then
                    lblQ010Remarks.Text = MyTable.Rows(nRows).Item("Q010Remarks").ToString
                Else
                    lblQ010Remarks.Text = ""
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
        Q1.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q1.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q1.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q1.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q1.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q1.Items(9).Text = rm.GetString("AgreeLevel10", ci)

        Q2.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q2.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q2.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q2.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q2.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q2.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q2.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q2.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q2.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q2.Items(9).Text = rm.GetString("AgreeLevel10", ci)

        Q3.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q3.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q3.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q3.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q3.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q3.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q3.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q3.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q3.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q3.Items(9).Text = rm.GetString("AgreeLevel10", ci)

        Q4.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q4.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q4.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q4.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q4.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q4.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q4.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q4.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q4.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q4.Items(9).Text = rm.GetString("AgreeLevel10", ci)

        Q5.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q5.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q5.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q5.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q5.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q5.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q5.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q5.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q5.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q5.Items(9).Text = rm.GetString("AgreeLevel10", ci)

        Q6.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q6.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q6.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q6.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q6.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q6.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q6.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q6.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q6.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q6.Items(9).Text = rm.GetString("AgreeLevel10", ci)

        Q7.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q7.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q7.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q7.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q7.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q7.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q7.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q7.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q7.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q7.Items(9).Text = rm.GetString("AgreeLevel10", ci)

        Q8.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q8.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q8.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q8.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q8.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q8.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q8.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q8.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q8.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q8.Items(9).Text = rm.GetString("AgreeLevel10", ci)

        Q9.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q9.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q9.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q9.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q9.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q9.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q9.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q9.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q9.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q9.Items(9).Text = rm.GetString("AgreeLevel10", ci)


        Q10.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q10.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q10.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q10.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q10.Items(4).Text = rm.GetString("AgreeLevel5", ci)
        Q10.Items(5).Text = rm.GetString("AgreeLevel6", ci)
        Q10.Items(6).Text = rm.GetString("AgreeLevel7", ci)
        Q10.Items(7).Text = rm.GetString("AgreeLevel8", ci)
        Q10.Items(8).Text = rm.GetString("AgreeLevel9", ci)
        Q10.Items(9).Text = rm.GetString("AgreeLevel10", ci)

    End Sub


    Public Shared Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String, ByVal windowName As String, ByVal width As Integer, ByVal height As Integer)
        Dim clientScript As String
        Dim windowAttribs As String

        'Building Client side window attributes with width and height.
        'Also the the window will be positioned to the middle of the screen
        windowAttribs = "width=" & width & "px," & _
                        "height=" & height & "px," & _
                        "left='+((screen.width -" & width & ") / 2)+'," & _
                        "top='+ (screen.height - " & height & ") / 2+'"


        'Building the client script- window.open, with additional parameters
        clientScript = "window.open('" & PagePath & "','" & windowName & "','" & windowAttribs & "');return false;"
        'regiter the script to the clientside click event of the 'opener' control
        opener.Attributes.Add("onClick", clientScript)
    End Sub


End Class