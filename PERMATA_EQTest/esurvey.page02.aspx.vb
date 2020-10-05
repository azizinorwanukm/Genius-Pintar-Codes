Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class esurvey_page02
    Inherits System.Web.UI.Page

    Private rm As ResourceManager
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                UserMark_load()
            End If

            ''--debug
            ''Response.Write(Request.QueryString("culture"))

            Dim ci As CultureInfo
            Thread.CurrentThread.CurrentCulture = New CultureInfo(Request.QueryString("culture"))
            'get the culture info to set the language
            rm = New ResourceManager("Resources.eqtest_2014", System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

        Catch ex As Exception
            ''lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Sub UserMark_load()
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT Q021,Q022,Q023,Q024,Q025 FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
        ''--debug
        'Response.Write(strSQL)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q021")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q021").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q022")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q022").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q023")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q023").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q024")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q024").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q025")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q025").ToString
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
        lblInstruction.Text = rm.GetString("lblInstruction", ci)
        lblNext.Text = rm.GetString("lblNext", ci)
        btnNext.Text = rm.GetString("btnNext", ci)
        btnPrev.Text = rm.GetString("btnPrev", ci)

        lblDomain01.Text = rm.GetString("lblDomain01", ci)
        lblRating.Text = rm.GetString("lblRating", ci)

        lblQ000.Text = rm.GetString("lblQ021_0", ci)
        lblQ001.Text = rm.GetString("lblQ021", ci)
        lblQ002.Text = rm.GetString("lblQ022", ci)
        lblQ003.Text = rm.GetString("lblQ023", ci)
        lblQ004.Text = rm.GetString("lblQ024", ci)
        lblQ005.Text = rm.GetString("lblQ025", ci)
        

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim strNextpage As String = "esurvey.page03.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")

        Try
            If validateform() = False Then
                lblMsgTop.Text = lblMsg.Text
                Exit Sub
            End If

            '--update record
            strSQL = "UPDATE EQTest SET LastPage='esurvey.page03.aspx',LastUpdate='" & oCommon.getNow & "',Q021=" & Q1.SelectedValue & ",Q022=" & Q2.SelectedValue & ",Q023=" & Q3.SelectedValue & ",Q024=" & Q4.SelectedValue & ",Q025=" & Q5.SelectedValue & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            ''--debug
            ''Response.Write(strSQL)
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                Response.Redirect(strNextpage)
                ''lblMsg.Text = "Success!"
            Else
                lblMsg.Text = strRet
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function validateform() As Boolean
        ''--debug
        ''Response.Write("Q1:" & Q1.SelectedValue)

        If Q1.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #21"
            Return False
        End If
        If Q2.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #22"
            Return False
        End If
        If Q3.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #23"
            Return False
        End If
        If Q4.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #24"
            Return False
        End If
        If Q5.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #25"
            Return False
        End If

        Return True

    End Function

    Private Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Dim strPrevpage As String = "esurvey.page01.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")
        Response.Redirect(strPrevpage)

    End Sub

End Class