Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class esurvey_page04
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
        strSQL = "SELECT Q046,Q047,Q048,Q049,Q050,Q051,Q052,Q053,Q054,Q055,Q056,Q057,Q058,Q059,Q060,Q061,Q062,Q063,Q064,Q065 FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q046")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q046").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q047")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q047").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q048")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q048").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q049")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q049").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q050")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q050").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q051")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q051").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q052")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q052").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q053")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q053").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q054")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q054").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q055")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q055").ToString
                Else
                    Q10.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q056")) Then
                    Q11.Text = MyTable.Rows(nRows).Item("Q056").ToString
                Else
                    Q11.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q057")) Then
                    Q12.Text = MyTable.Rows(nRows).Item("Q057").ToString
                Else
                    Q12.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q058")) Then
                    Q13.Text = MyTable.Rows(nRows).Item("Q058").ToString
                Else
                    Q13.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q059")) Then
                    Q14.Text = MyTable.Rows(nRows).Item("Q059").ToString
                Else
                    Q14.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q060")) Then
                    Q15.Text = MyTable.Rows(nRows).Item("Q060").ToString
                Else
                    Q15.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q061")) Then
                    Q16.Text = MyTable.Rows(nRows).Item("Q061").ToString
                Else
                    Q16.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q062")) Then
                    Q17.Text = MyTable.Rows(nRows).Item("Q062").ToString
                Else
                    Q17.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q063")) Then
                    Q18.Text = MyTable.Rows(nRows).Item("Q063").ToString
                Else
                    Q18.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q064")) Then
                    Q19.Text = MyTable.Rows(nRows).Item("Q064").ToString
                Else
                    Q19.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q065")) Then
                    Q20.Text = MyTable.Rows(nRows).Item("Q065").ToString
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

        lblQ001.Text = rm.GetString("lblQ046", ci)
        lblQ002.Text = rm.GetString("lblQ047", ci)
        lblQ003.Text = rm.GetString("lblQ048", ci)
        lblQ004.Text = rm.GetString("lblQ049", ci)
        lblQ005.Text = rm.GetString("lblQ050", ci)
        lblQ006.Text = rm.GetString("lblQ051", ci)
        lblQ007.Text = rm.GetString("lblQ052", ci)
        lblQ008.Text = rm.GetString("lblQ053", ci)
        lblQ009.Text = rm.GetString("lblQ054", ci)
        lblQ010.Text = rm.GetString("lblQ055", ci)
        lblQ011.Text = rm.GetString("lblQ056", ci)
        lblQ012.Text = rm.GetString("lblQ057", ci)
        lblQ013.Text = rm.GetString("lblQ058", ci)
        lblQ014.Text = rm.GetString("lblQ059", ci)
        lblQ015.Text = rm.GetString("lblQ060", ci)
        lblQ016.Text = rm.GetString("lblQ061", ci)
        lblQ017.Text = rm.GetString("lblQ062", ci)
        lblQ018.Text = rm.GetString("lblQ063", ci)
        lblQ019.Text = rm.GetString("lblQ064", ci)
        lblQ020.Text = rm.GetString("lblQ065", ci)

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim strNextpage As String = "esurvey.page05.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")

        Try
            If validateform() = False Then
                lblMsgTop.Text = lblMsg.Text
                Exit Sub
            End If

            '--update record
            strSQL = "UPDATE EQTest SET LastPage='esurvey.page05.aspx',LastUpdate='" & oCommon.getNow & "',Q046=" & Q1.SelectedValue & ",Q047=" & Q2.SelectedValue & ",Q048=" & Q3.SelectedValue & ",Q049=" & Q4.SelectedValue & ",Q050=" & Q5.SelectedValue & ",Q051=" & Q6.SelectedValue & ",Q052=" & Q7.SelectedValue & ",Q053=" & Q8.SelectedValue & ",Q054=" & Q9.SelectedValue & ",Q055=" & Q10.SelectedValue & ",Q056=" & Q11.SelectedValue & ",Q057=" & Q12.SelectedValue & ",Q058=" & Q13.SelectedValue & ",Q059=" & Q14.SelectedValue & ",Q060=" & Q15.SelectedValue & ",Q061=" & Q16.SelectedValue & ",Q062=" & Q17.SelectedValue & ",Q063=" & Q18.SelectedValue & ",Q064=" & Q19.SelectedValue & ",Q065=" & Q20.SelectedValue & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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
            lblMsg.Text = "Please answer all the questions. #46"
            Return False
        End If
        If Q2.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #47"
            Return False
        End If
        If Q3.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #48"
            Return False
        End If
        If Q4.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #49"
            Return False
        End If
        If Q5.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #50"
            Return False
        End If

        If Q6.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #51"
            Return False
        End If
        If Q7.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #52"
            Return False
        End If
        If Q8.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #53"
            Return False
        End If
        If Q9.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #54"
            Return False
        End If
        If Q10.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #55"
            Return False
        End If

        If Q11.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #56"
            Return False
        End If
        If Q12.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #57"
            Return False
        End If
        If Q13.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #58"
            Return False
        End If
        If Q14.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #59"
            Return False
        End If
        If Q15.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #60"
            Return False
        End If
        If Q16.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #61"
            Return False
        End If
        If Q17.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #62"
            Return False
        End If
        If Q18.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #63"
            Return False
        End If
        If Q19.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #64"
            Return False
        End If
        If Q20.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #65"
            Return False
        End If

        Return True

    End Function

    Private Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Dim strPrevpage As String = "esurvey.page03.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")
        Response.Redirect(strPrevpage)

    End Sub


End Class