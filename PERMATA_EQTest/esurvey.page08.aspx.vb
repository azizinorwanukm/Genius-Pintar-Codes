Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class esurvey_page08
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
        strSQL = "SELECT Q101,Q102,Q103,Q104,Q105,Q106,Q107,Q108,Q109,Q110,Q111,Q112,Q113,Q114,Q115,Q116,Q117,Q118,Q119,Q120 FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q101")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q101").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q102")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q102").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q103")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q103").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q104")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q104").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q105")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q105").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q106")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q106").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q107")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q107").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q108")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q108").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q109")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q109").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q110")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q110").ToString
                Else
                    Q10.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q111")) Then
                    Q11.Text = MyTable.Rows(nRows).Item("Q111").ToString
                Else
                    Q11.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q112")) Then
                    Q12.Text = MyTable.Rows(nRows).Item("Q112").ToString
                Else
                    Q12.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q113")) Then
                    Q13.Text = MyTable.Rows(nRows).Item("Q113").ToString
                Else
                    Q13.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q114")) Then
                    Q14.Text = MyTable.Rows(nRows).Item("Q114").ToString
                Else
                    Q14.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q115")) Then
                    Q15.Text = MyTable.Rows(nRows).Item("Q115").ToString
                Else
                    Q15.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q116")) Then
                    Q16.Text = MyTable.Rows(nRows).Item("Q116").ToString
                Else
                    Q16.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q117")) Then
                    Q17.Text = MyTable.Rows(nRows).Item("Q117").ToString
                Else
                    Q17.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q118")) Then
                    Q18.Text = MyTable.Rows(nRows).Item("Q118").ToString
                Else
                    Q18.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q119")) Then
                    Q19.Text = MyTable.Rows(nRows).Item("Q119").ToString
                Else
                    Q19.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q120")) Then
                    Q20.Text = MyTable.Rows(nRows).Item("Q120").ToString
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

        lblQ001.Text = rm.GetString("lblQ101", ci)
        lblQ002.Text = rm.GetString("lblQ102", ci)
        lblQ003.Text = rm.GetString("lblQ103", ci)
        lblQ004.Text = rm.GetString("lblQ104", ci)
        lblQ005.Text = rm.GetString("lblQ105", ci)
        lblQ006.Text = rm.GetString("lblQ106", ci)
        lblQ007.Text = rm.GetString("lblQ107", ci)
        lblQ008.Text = rm.GetString("lblQ108", ci)
        lblQ009.Text = rm.GetString("lblQ109", ci)
        lblQ010.Text = rm.GetString("lblQ110", ci)
        lblQ011.Text = rm.GetString("lblQ111", ci)
        lblQ012.Text = rm.GetString("lblQ112", ci)
        lblQ013.Text = rm.GetString("lblQ113", ci)
        lblQ014.Text = rm.GetString("lblQ114", ci)
        lblQ015.Text = rm.GetString("lblQ115", ci)
        lblQ016.Text = rm.GetString("lblQ116", ci)
        lblQ017.Text = rm.GetString("lblQ117", ci)
        lblQ018.Text = rm.GetString("lblQ118", ci)
        lblQ019.Text = rm.GetString("lblQ119", ci)
        lblQ020.Text = rm.GetString("lblQ120", ci)

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim strNextpage As String = "esurvey.page09.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")

        Try
            If validateform() = False Then
                lblMsgTop.Text = lblMsg.Text
                Exit Sub
            End If

            '--update record
            strSQL = "UPDATE EQTest SET LastPage='esurvey.page09.aspx',LastUpdate='" & oCommon.getNow & "',Q101=" & Q1.SelectedValue & ",Q102=" & Q2.SelectedValue & ",Q103=" & Q3.SelectedValue & ",Q104=" & Q4.SelectedValue & ",Q105=" & Q5.SelectedValue & ",Q106=" & Q6.SelectedValue & ",Q107=" & Q7.SelectedValue & ",Q108=" & Q8.SelectedValue & ",Q109=" & Q9.SelectedValue & ",Q110=" & Q10.SelectedValue & ",Q111=" & Q11.SelectedValue & ",Q112=" & Q12.SelectedValue & ",Q113=" & Q13.SelectedValue & ",Q114=" & Q14.SelectedValue & ",Q115=" & Q15.SelectedValue & ",Q116=" & Q16.SelectedValue & ",Q117=" & Q17.SelectedValue & ",Q118=" & Q18.SelectedValue & ",Q119=" & Q19.SelectedValue & ",Q120=" & Q20.SelectedValue & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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
            lblMsg.Text = "Please answer all the questions. #101"
            Return False
        End If
        If Q2.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #102"
            Return False
        End If
        If Q3.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #103"
            Return False
        End If
        If Q4.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #104"
            Return False
        End If
        If Q5.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #105"
            Return False
        End If

        If Q6.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #106"
            Return False
        End If
        If Q7.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #107"
            Return False
        End If
        If Q8.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #108"
            Return False
        End If
        If Q9.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #109"
            Return False
        End If
        If Q10.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #110"
            Return False
        End If

        If Q11.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #111"
            Return False
        End If
        If Q12.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #112"
            Return False
        End If
        If Q13.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #113"
            Return False
        End If
        If Q14.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #114"
            Return False
        End If
        If Q15.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #115"
            Return False
        End If
        If Q16.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #116"
            Return False
        End If
        If Q17.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #117"
            Return False
        End If
        If Q18.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #118"
            Return False
        End If
        If Q19.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #119"
            Return False
        End If
        If Q20.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #120"
            Return False
        End If

        Return True

    End Function

    Private Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Dim strPrevpage As String = "esurvey.page07.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")
        Response.Redirect(strPrevpage)

    End Sub

End Class