Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Partial Public Class esurvey_page01
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
        strSQL = "SELECT Q001,Q002,Q003,Q004,Q005,Q006,Q007,Q008,Q009,Q010,Q011,Q012,Q013,Q014,Q015,Q016,Q017,Q018,Q019,Q020 FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q001")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q001").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q002")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q002").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q003")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q003").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q004")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q004").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q005")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q005").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q006")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q006").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q007")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q007").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q008")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q008").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q009")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q009").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q010")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q010").ToString
                Else
                    Q10.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q011")) Then
                    Q11.Text = MyTable.Rows(nRows).Item("Q011").ToString
                Else
                    Q11.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q012")) Then
                    Q12.Text = MyTable.Rows(nRows).Item("Q012").ToString
                Else
                    Q12.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q013")) Then
                    Q13.Text = MyTable.Rows(nRows).Item("Q013").ToString
                Else
                    Q13.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q014")) Then
                    Q14.Text = MyTable.Rows(nRows).Item("Q014").ToString
                Else
                    Q14.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q015")) Then
                    Q15.Text = MyTable.Rows(nRows).Item("Q015").ToString
                Else
                    Q15.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q016")) Then
                    Q16.Text = MyTable.Rows(nRows).Item("Q016").ToString
                Else
                    Q16.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q017")) Then
                    Q17.Text = MyTable.Rows(nRows).Item("Q017").ToString
                Else
                    Q17.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q018")) Then
                    Q18.Text = MyTable.Rows(nRows).Item("Q018").ToString
                Else
                    Q18.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q019")) Then
                    Q19.Text = MyTable.Rows(nRows).Item("Q019").ToString
                Else
                    Q19.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q020")) Then
                    Q20.Text = MyTable.Rows(nRows).Item("Q020").ToString
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

        lblDomain01.Text = rm.GetString("lblDomain01", ci)
        lblRating.Text = rm.GetString("lblRating", ci)

        lblLevel1.Text = rm.GetString("lblD1Level1", ci)
        lblLevel2.Text = rm.GetString("lblD1Level2", ci)
        lblLevel3.Text = rm.GetString("lblD1Level3", ci)
        lblLevel4.Text = rm.GetString("lblD1Level4", ci)
        lblLevel5.Text = rm.GetString("lblD1Level5", ci)

        lblQ001.Text = rm.GetString("lblQ001", ci)
        lblQ002.Text = rm.GetString("lblQ002", ci)
        lblQ003.Text = rm.GetString("lblQ003", ci)
        lblQ004.Text = rm.GetString("lblQ004", ci)
        lblQ005.Text = rm.GetString("lblQ005", ci)
        lblQ006.Text = rm.GetString("lblQ006", ci)
        lblQ007.Text = rm.GetString("lblQ007", ci)
        lblQ008.Text = rm.GetString("lblQ008", ci)
        lblQ009.Text = rm.GetString("lblQ009", ci)
        lblQ010.Text = rm.GetString("lblQ010", ci)
        lblQ011.Text = rm.GetString("lblQ011", ci)
        lblQ012.Text = rm.GetString("lblQ012", ci)
        lblQ013.Text = rm.GetString("lblQ013", ci)
        lblQ014.Text = rm.GetString("lblQ014", ci)
        lblQ015.Text = rm.GetString("lblQ015", ci)
        lblQ016.Text = rm.GetString("lblQ016", ci)
        lblQ017.Text = rm.GetString("lblQ017", ci)
        lblQ018.Text = rm.GetString("lblQ018", ci)
        lblQ019.Text = rm.GetString("lblQ019", ci)
        lblQ020.Text = rm.GetString("lblQ020", ci)

    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim strNextpage As String = "esurvey.page02.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")

        Try
            If validateform() = False Then
                lblMsgTop.Text = lblMsg.Text
                Exit Sub
            End If

            '--update record
            strSQL = "UPDATE EQTest SET LastPage='esurvey.page02.aspx',LastUpdate='" & oCommon.getNow & "',Q001=" & Q1.SelectedValue & ",Q002=" & Q2.SelectedValue & ",Q003=" & Q3.SelectedValue & ",Q004=" & Q4.SelectedValue & ",Q005=" & Q5.SelectedValue & ",Q006=" & Q6.SelectedValue & ",Q007=" & Q7.SelectedValue & ",Q008=" & Q8.SelectedValue & ",Q009=" & Q9.SelectedValue & ",Q010=" & Q10.SelectedValue & ",Q011=" & Q11.SelectedValue & ",Q012=" & Q12.SelectedValue & ",Q013=" & Q13.SelectedValue & ",Q014=" & Q14.SelectedValue & ",Q015=" & Q15.SelectedValue & ",Q016=" & Q16.SelectedValue & ",Q017=" & Q17.SelectedValue & ",Q018=" & Q18.SelectedValue & ",Q019=" & Q19.SelectedValue & ",Q020=" & Q20.SelectedValue & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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
            lblMsg.Text = "Please answer all the questions. #1"
            Return False
        End If
        If Q2.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #2"
            Return False
        End If
        If Q3.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #3"
            Return False
        End If
        If Q4.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #4"
            Return False
        End If
        If Q5.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #5"
            Return False
        End If

        If Q6.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #6"
            Return False
        End If
        If Q7.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #7"
            Return False
        End If
        If Q8.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #8"
            Return False
        End If
        If Q9.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #9"
            Return False
        End If
        If Q10.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #10"
            Return False
        End If

        If Q11.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #11"
            Return False
        End If
        If Q12.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #12"
            Return False
        End If
        If Q13.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #13"
            Return False
        End If
        If Q14.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #14"
            Return False
        End If
        If Q15.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #15"
            Return False
        End If
        If Q16.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #16"
            Return False
        End If
        If Q17.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #17"
            Return False
        End If
        If Q18.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #18"
            Return False
        End If
        If Q19.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #19"
            Return False
        End If
        If Q20.SelectedValue = "" Then
            lblMsg.Text = "Please answer all the questions. #20"
            Return False
        End If

        Return True

    End Function

    Private Sub lnkSave_Click(sender As Object, e As EventArgs) Handles lnkSave.Click
        Try
            '--update record
            'If(Q6.SelectedValue = "", "0", Q6.SelectedValue)
            strSQL = "UPDATE EQTest SET LastPage='esurvey.page01.aspx',LastUpdate='" & oCommon.getNow & "',Q001=" & Q1.SelectedValue & ",Q002=" & Q2.SelectedValue & ",Q003=" & Q3.SelectedValue & ",Q004=" & Q4.SelectedValue & ",Q005=" & Q5.SelectedValue & ",Q006=" & Q6.SelectedValue & ",Q007=" & Q7.SelectedValue & ",Q008=" & Q8.SelectedValue & ",Q009=" & Q9.SelectedValue & ",Q010=" & Q10.SelectedValue & ",Q011=" & Q11.SelectedValue & ",Q012=" & Q12.SelectedValue & ",Q013=" & Q13.SelectedValue & ",Q014=" & Q14.SelectedValue & ",Q015=" & Q15.SelectedValue & ",Q016=" & Q16.SelectedValue & ",Q017=" & Q17.SelectedValue & ",Q018=" & Q18.SelectedValue & ",Q019=" & Q19.SelectedValue & ",Q020=" & Q20.SelectedValue & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsgTop.Text = "Rekod berjaya disimpan."
                lblMsg.Text = lblMsgTop.Text
            Else
                lblMsgTop.Text = "Err:" & strRet
                lblMsg.Text = lblMsgTop.Text
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub
End Class