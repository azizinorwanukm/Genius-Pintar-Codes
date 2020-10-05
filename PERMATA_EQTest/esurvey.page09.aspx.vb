Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class esurvey_page09
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
        strSQL = "SELECT Q121,Q122,Q123,Q124,Q125,Q126,Q127,Q128,Q129,Q130,Q131,Q132,Q133,Q134,Q135,Q136,Q137,Q138,Q139,Q140 FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q121")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q121").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q122")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q122").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q123")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q123").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q124")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q124").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q125")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q125").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q126")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q126").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q127")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q127").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q128")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q128").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q129")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q129").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q130")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q130").ToString
                Else
                    Q10.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q131")) Then
                    Q11.Text = MyTable.Rows(nRows).Item("Q131").ToString
                Else
                    Q11.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q132")) Then
                    Q12.Text = MyTable.Rows(nRows).Item("Q132").ToString
                Else
                    Q12.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q133")) Then
                    Q13.Text = MyTable.Rows(nRows).Item("Q133").ToString
                Else
                    Q13.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q134")) Then
                    Q14.Text = MyTable.Rows(nRows).Item("Q134").ToString
                Else
                    Q14.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q135")) Then
                    Q15.Text = MyTable.Rows(nRows).Item("Q135").ToString
                Else
                    Q15.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q136")) Then
                    Q16.Text = MyTable.Rows(nRows).Item("Q136").ToString
                Else
                    Q16.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q137")) Then
                    Q17.Text = MyTable.Rows(nRows).Item("Q137").ToString
                Else
                    Q17.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q138")) Then
                    Q18.Text = MyTable.Rows(nRows).Item("Q138").ToString
                Else
                    Q18.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q139")) Then
                    Q19.Text = MyTable.Rows(nRows).Item("Q139").ToString
                Else
                    Q19.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q140")) Then
                    Q20.Text = MyTable.Rows(nRows).Item("Q140").ToString
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

        lblQ001.Text = rm.GetString("lblQ121", ci)
        lblQ002.Text = rm.GetString("lblQ122", ci)
        lblQ003.Text = rm.GetString("lblQ123", ci)
        lblQ004.Text = rm.GetString("lblQ124", ci)
        lblQ005.Text = rm.GetString("lblQ125", ci)
        lblQ006.Text = rm.GetString("lblQ126", ci)
        lblQ007.Text = rm.GetString("lblQ127", ci)
        lblQ008.Text = rm.GetString("lblQ128", ci)
        lblQ009.Text = rm.GetString("lblQ129", ci)
        lblQ010.Text = rm.GetString("lblQ130", ci)
        lblQ011.Text = rm.GetString("lblQ131", ci)
        lblQ012.Text = rm.GetString("lblQ132", ci)
        lblQ013.Text = rm.GetString("lblQ133", ci)
        lblQ014.Text = rm.GetString("lblQ134", ci)
        lblQ015.Text = rm.GetString("lblQ135", ci)
        lblQ016.Text = rm.GetString("lblQ136", ci)
        lblQ017.Text = rm.GetString("lblQ137", ci)
        lblQ018.Text = rm.GetString("lblQ138", ci)
        lblQ019.Text = rm.GetString("lblQ139", ci)
        lblQ020.Text = rm.GetString("lblQ140", ci)

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim strNextpage As String = "esurvey.page10.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")

        Try
            If validateform() = False Then
                lblMsgTop.Text = lblMsg.Text
                Exit Sub
            End If

            '--update record
            strSQL = "UPDATE EQTest SET LastPage='esurvey.page10.aspx',LastUpdate='" & oCommon.getNow & "',Q121=" & Q1.SelectedValue & ",Q122=" & Q2.SelectedValue & ",Q123=" & Q3.SelectedValue & ",Q124=" & Q4.SelectedValue & ",Q125=" & Q5.SelectedValue & ",Q126=" & Q6.SelectedValue & ",Q127=" & Q7.SelectedValue & ",Q128=" & Q8.SelectedValue & ",Q129=" & Q9.SelectedValue & ",Q130=" & Q10.SelectedValue & ",Q131=" & Q11.SelectedValue & ",Q132=" & Q12.SelectedValue & ",Q133=" & Q13.SelectedValue & ",Q134=" & Q14.SelectedValue & ",Q135=" & Q15.SelectedValue & ",Q136=" & Q16.SelectedValue & ",Q137=" & Q17.SelectedValue & ",Q138=" & Q18.SelectedValue & ",Q139=" & Q19.SelectedValue & ",Q140=" & Q20.SelectedValue & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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
            lblMsg.Text = "Please answer all the questions. #121"
            Return False
        End If
        If Q2.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #122"
            Return False
        End If
        If Q3.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #123"
            Return False
        End If
        If Q4.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #124"
            Return False
        End If
        If Q5.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #125"
            Return False
        End If

        If Q6.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #126"
            Return False
        End If
        If Q7.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #127"
            Return False
        End If
        If Q8.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #128"
            Return False
        End If
        If Q9.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #129"
            Return False
        End If
        If Q10.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #130"
            Return False
        End If

        If Q11.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #131"
            Return False
        End If
        If Q12.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #132"
            Return False
        End If
        If Q13.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #133"
            Return False
        End If
        If Q14.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #134"
            Return False
        End If
        If Q15.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #135"
            Return False
        End If
        If Q16.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #136"
            Return False
        End If
        If Q17.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #137"
            Return False
        End If
        If Q18.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #138"
            Return False
        End If
        If Q19.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #139"
            Return False
        End If
        If Q20.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #140"
            Return False
        End If

        Return True

    End Function

    Private Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Dim strPrevpage As String = "esurvey.page08.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")
        Response.Redirect(strPrevpage)

    End Sub

End Class