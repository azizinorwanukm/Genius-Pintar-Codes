Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class esurvey_page03
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
        strSQL = "SELECT Q026,Q027,Q028,Q029,Q030,Q031,Q032,Q033,Q034,Q035,Q036,Q037,Q038,Q039,Q040,Q041,Q042,Q043,Q044,Q045 FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q026")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q026").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q027")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q027").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q028")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q028").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q029")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q029").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q030")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q030").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q031")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q031").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q032")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q032").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q033")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q033").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q034")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q034").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q035")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q035").ToString
                Else
                    Q10.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q036")) Then
                    Q11.Text = MyTable.Rows(nRows).Item("Q036").ToString
                Else
                    Q11.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q037")) Then
                    Q12.Text = MyTable.Rows(nRows).Item("Q037").ToString
                Else
                    Q12.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q038")) Then
                    Q13.Text = MyTable.Rows(nRows).Item("Q038").ToString
                Else
                    Q13.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q039")) Then
                    Q14.Text = MyTable.Rows(nRows).Item("Q039").ToString
                Else
                    Q14.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q040")) Then
                    Q15.Text = MyTable.Rows(nRows).Item("Q040").ToString
                Else
                    Q15.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q041")) Then
                    Q16.Text = MyTable.Rows(nRows).Item("Q041").ToString
                Else
                    Q16.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q042")) Then
                    Q17.Text = MyTable.Rows(nRows).Item("Q042").ToString
                Else
                    Q17.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q043")) Then
                    Q18.Text = MyTable.Rows(nRows).Item("Q043").ToString
                Else
                    Q18.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q044")) Then
                    Q19.Text = MyTable.Rows(nRows).Item("Q044").ToString
                Else
                    Q19.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q045")) Then
                    Q20.Text = MyTable.Rows(nRows).Item("Q045").ToString
                Else
                    Q20.SelectedValue = ""
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

        lblLevel1.Text = rm.GetString("lblD1Level1", ci)
        lblLevel2.Text = rm.GetString("lblD1Level2", ci)
        lblLevel3.Text = rm.GetString("lblD1Level3", ci)
        lblLevel4.Text = rm.GetString("lblD1Level4", ci)
        lblLevel5.Text = rm.GetString("lblD1Level5", ci)

        lblQ001.Text = rm.GetString("lblQ026", ci)
        lblQ002.Text = rm.GetString("lblQ027", ci)
        lblQ003.Text = rm.GetString("lblQ028", ci)
        lblQ004.Text = rm.GetString("lblQ029", ci)
        lblQ005.Text = rm.GetString("lblQ030", ci)
        lblQ006.Text = rm.GetString("lblQ031", ci)
        lblQ007.Text = rm.GetString("lblQ032", ci)
        lblQ008.Text = rm.GetString("lblQ033", ci)
        lblQ009.Text = rm.GetString("lblQ034", ci)
        lblQ010.Text = rm.GetString("lblQ035", ci)
        lblQ011.Text = rm.GetString("lblQ036", ci)
        lblQ012.Text = rm.GetString("lblQ037", ci)
        lblQ013.Text = rm.GetString("lblQ038", ci)
        lblQ014.Text = rm.GetString("lblQ039", ci)
        lblQ015.Text = rm.GetString("lblQ040", ci)
        lblQ016.Text = rm.GetString("lblQ041", ci)
        lblQ017.Text = rm.GetString("lblQ042", ci)
        lblQ018.Text = rm.GetString("lblQ043", ci)
        lblQ019.Text = rm.GetString("lblQ044", ci)
        lblQ020.Text = rm.GetString("lblQ045", ci)

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim strNextpage As String = "esurvey.page04.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")

        Try
            If validateform() = False Then
                lblMsgTop.Text = lblMsg.Text
                Exit Sub
            End If

            '--update record
            strSQL = "UPDATE EQTest SET LastPage='esurvey.page04.aspx',LastUpdate='" & oCommon.getNow & "',Q026=" & Q1.SelectedValue & ",Q027=" & Q2.SelectedValue & ",Q028=" & Q3.SelectedValue & ",Q029=" & Q4.SelectedValue & ",Q030=" & Q5.SelectedValue & ",Q031=" & Q6.SelectedValue & ",Q032=" & Q7.SelectedValue & ",Q033=" & Q8.SelectedValue & ",Q034=" & Q9.SelectedValue & ",Q035=" & Q10.SelectedValue & ",Q036=" & Q11.SelectedValue & ",Q037=" & Q12.SelectedValue & ",Q038=" & Q13.SelectedValue & ",Q039=" & Q14.SelectedValue & ",Q040=" & Q15.SelectedValue & ",Q041=" & Q16.SelectedValue & ",Q042=" & Q17.SelectedValue & ",Q043=" & Q18.SelectedValue & ",Q044=" & Q19.SelectedValue & ",Q045=" & Q20.SelectedValue & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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
            lblMsg.Text = "Please answer all the questions. #26"
            Return False
        End If
        If Q2.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #27"
            Return False
        End If
        If Q3.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #28"
            Return False
        End If
        If Q4.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #29"
            Return False
        End If
        If Q5.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #30"
            Return False
        End If

        If Q6.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #31"
            Return False
        End If
        If Q7.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #32"
            Return False
        End If
        If Q8.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #33"
            Return False
        End If
        If Q9.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #34"
            Return False
        End If
        If Q10.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #35"
            Return False
        End If

        If Q11.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #36"
            Return False
        End If
        If Q12.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #37"
            Return False
        End If
        If Q13.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #38"
            Return False
        End If
        If Q14.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #39"
            Return False
        End If
        If Q15.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #40"
            Return False
        End If
        If Q16.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #41"
            Return False
        End If
        If Q17.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #42"
            Return False
        End If
        If Q18.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #43"
            Return False
        End If
        If Q19.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #44"
            Return False
        End If
        If Q20.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #45"
            Return False
        End If

        Return True

    End Function

    Private Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Dim strPrevpage As String = "esurvey.page02.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")
        Response.Redirect(strPrevpage)

    End Sub

End Class