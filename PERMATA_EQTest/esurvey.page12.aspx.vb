Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class esurvey_page12
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
        strSQL = "SELECT Q162,Q163,Q164,Q165,Q166,Q167,Q168,Q169,Q170,Q171,Q172,Q173,Q174,Q175,Q176 FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q162")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q162").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q163")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q163").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q164")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q164").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q165")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q165").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q166")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q166").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q167")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q167").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q168")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q168").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q169")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q169").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q170")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q170").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q171")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q171").ToString
                Else
                    Q10.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q172")) Then
                    Q11.Text = MyTable.Rows(nRows).Item("Q172").ToString
                Else
                    Q11.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q173")) Then
                    Q12.Text = MyTable.Rows(nRows).Item("Q173").ToString
                Else
                    Q12.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q174")) Then
                    Q13.Text = MyTable.Rows(nRows).Item("Q174").ToString
                Else
                    Q13.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q175")) Then
                    Q14.Text = MyTable.Rows(nRows).Item("Q175").ToString
                Else
                    Q14.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q176")) Then
                    Q15.Text = MyTable.Rows(nRows).Item("Q176").ToString
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

        lblQ001.Text = rm.GetString("lblQ162", ci)
        lblQ002.Text = rm.GetString("lblQ163", ci)
        lblQ003.Text = rm.GetString("lblQ164", ci)
        lblQ004.Text = rm.GetString("lblQ165", ci)
        lblQ005.Text = rm.GetString("lblQ166", ci)
        lblQ006.Text = rm.GetString("lblQ167", ci)
        lblQ007.Text = rm.GetString("lblQ168", ci)
        lblQ008.Text = rm.GetString("lblQ169", ci)
        lblQ009.Text = rm.GetString("lblQ170", ci)
        lblQ010.Text = rm.GetString("lblQ171", ci)
        lblQ011.Text = rm.GetString("lblQ172", ci)
        lblQ012.Text = rm.GetString("lblQ173", ci)
        lblQ013.Text = rm.GetString("lblQ174", ci)
        lblQ014.Text = rm.GetString("lblQ175", ci)
        lblQ015.Text = rm.GetString("lblQ176", ci)

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim strNextpage As String = "esurvey.page13.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")

        Try
            If validateform() = False Then
                lblMsgTop.Text = lblMsg.Text
                Exit Sub
            End If

            '--update record
            strSQL = "UPDATE EQTest SET LastPage='esurvey.page13.aspx',LastUpdate='" & oCommon.getNow & "',Q162=" & Q1.SelectedValue & ",Q163=" & Q2.SelectedValue & ",Q164=" & Q3.SelectedValue & ",Q165=" & Q4.SelectedValue & ",Q166=" & Q5.SelectedValue & ",Q167=" & Q6.SelectedValue & ",Q168=" & Q7.SelectedValue & ",Q169=" & Q8.SelectedValue & ",Q170=" & Q9.SelectedValue & ",Q171=" & Q10.SelectedValue & ",Q172=" & Q11.SelectedValue & ",Q173=" & Q12.SelectedValue & ",Q174=" & Q13.SelectedValue & ",Q175=" & Q14.SelectedValue & ",Q176=" & Q15.SelectedValue & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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
            lblMsg.Text = "Please answer all the questions. #162"
            Return False
        End If
        If Q2.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #163"
            Return False
        End If
        If Q3.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #164"
            Return False
        End If
        If Q4.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #165"
            Return False
        End If
        If Q5.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #166"
            Return False
        End If

        If Q6.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #167"
            Return False
        End If
        If Q7.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #168"
            Return False
        End If
        If Q8.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #169"
            Return False
        End If
        If Q9.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #170"
            Return False
        End If
        If Q10.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #171"
            Return False
        End If

        If Q11.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #172"
            Return False
        End If
        If Q12.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #173"
            Return False
        End If
        If Q13.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #174"
            Return False
        End If
        If Q14.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #175"
            Return False
        End If
        If Q15.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #176"
            Return False
        End If

        Return True

    End Function

    Private Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Dim strPrevpage As String = "esurvey.page11.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")
        Response.Redirect(strPrevpage)

    End Sub

End Class