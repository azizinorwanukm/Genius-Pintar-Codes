Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class esurvey_page10
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
        strSQL = "SELECT Q141,Q142,Q143,Q144,Q145,Q146,Q147,Q148,Q149,Q150,Q151,Q152,Q153 FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q141")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q141").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q142")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q142").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q143")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q143").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q144")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q144").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q145")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q145").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q146")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q146").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q147")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q147").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q148")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q148").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q149")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q149").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q150")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q150").ToString
                Else
                    Q10.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q151")) Then
                    Q11.Text = MyTable.Rows(nRows).Item("Q151").ToString
                Else
                    Q11.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q152")) Then
                    Q12.Text = MyTable.Rows(nRows).Item("Q152").ToString
                Else
                    Q12.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q153")) Then
                    Q13.Text = MyTable.Rows(nRows).Item("Q153").ToString
                Else
                    Q13.SelectedValue = ""
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

        lblQ001.Text = rm.GetString("lblQ141", ci)
        lblQ002.Text = rm.GetString("lblQ142", ci)
        lblQ003.Text = rm.GetString("lblQ143", ci)
        lblQ004.Text = rm.GetString("lblQ144", ci)
        lblQ005.Text = rm.GetString("lblQ145", ci)
        lblQ006.Text = rm.GetString("lblQ146", ci)
        lblQ007.Text = rm.GetString("lblQ147", ci)
        lblQ008.Text = rm.GetString("lblQ148", ci)
        lblQ009.Text = rm.GetString("lblQ149", ci)
        lblQ010.Text = rm.GetString("lblQ150", ci)
        lblQ011.Text = rm.GetString("lblQ151", ci)
        lblQ012.Text = rm.GetString("lblQ152", ci)
        lblQ013.Text = rm.GetString("lblQ153", ci)
        

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim strNextpage As String = "esurvey.page11.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")

        Try
            If validateform() = False Then
                lblMsgTop.Text = lblMsg.Text
                Exit Sub
            End If

            '--update record
            strSQL = "UPDATE EQTest SET LastPage='esurvey.page11.aspx',LastUpdate='" & oCommon.getNow & "',Q141=" & Q1.SelectedValue & ",Q142=" & Q2.SelectedValue & ",Q143=" & Q3.SelectedValue & ",Q144=" & Q4.SelectedValue & ",Q145=" & Q5.SelectedValue & ",Q146=" & Q6.SelectedValue & ",Q147=" & Q7.SelectedValue & ",Q148=" & Q8.SelectedValue & ",Q149=" & Q9.SelectedValue & ",Q150=" & Q10.SelectedValue & ",Q151=" & Q11.SelectedValue & ",Q152=" & Q12.SelectedValue & ",Q153=" & Q13.SelectedValue & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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
            lblMsg.Text = "Please answer all the questions. #141"
            Return False
        End If
        If Q2.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #142"
            Return False
        End If
        If Q3.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #143"
            Return False
        End If
        If Q4.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #144"
            Return False
        End If
        If Q5.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #145"
            Return False
        End If

        If Q6.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #146"
            Return False
        End If
        If Q7.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #147"
            Return False
        End If
        If Q8.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #148"
            Return False
        End If
        If Q9.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #149"
            Return False
        End If
        If Q10.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #150"
            Return False
        End If

        If Q11.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #151"
            Return False
        End If
        If Q12.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #152"
            Return False
        End If
        If Q13.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #153"
            Return False
        End If
        

        Return True

    End Function

    Private Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Dim strPrevpage As String = "esurvey.page09.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")
        Response.Redirect(strPrevpage)

    End Sub

End Class