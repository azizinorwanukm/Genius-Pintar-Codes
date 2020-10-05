﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class PPCS_Eval_Daily_create1
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
    Dim strStudentid As String = ""

    ''--selected value
    Dim nQ1, nQ2, nQ3, nQ4, nQ5, nQ6, nQ7, nQ8, nQ9, nQ10 As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strStudentid = Request.QueryString("studentid")

        Dim strSelectedDdate As String = "01/12/" & Request.QueryString("examyear")

        Try
            Dim ci As CultureInfo
            Dim strBasename As String = "Resources.eval2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            If Not IsPostBack Then
                ''--default date
                calToday.SelectedDate = CDate(strSelectedDdate)
                lblSelectedDate.Text = calToday.SelectedDate.ToString("yyyyMMdd")
                calToday.VisibleDate = CDate(strSelectedDdate)

                '--load answers
                ppcs_eval_load(Request.QueryString("studentid"))
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
            Response.Cookies("ppcs_culture").Value = "ms-MY"
        End Try
    End Sub

    Private Sub resetall_radiobutton()
        For Each li As ListItem In Q1.Items
            li.Selected = False
        Next
        For Each li As ListItem In Q2.Items
            li.Selected = False
        Next
        For Each li As ListItem In Q3.Items
            li.Selected = False
        Next
        For Each li As ListItem In Q4.Items
            li.Selected = False
        Next
        For Each li As ListItem In Q5.Items
            li.Selected = False
        Next
        For Each li As ListItem In Q6.Items
            li.Selected = False
        Next
        For Each li As ListItem In Q7.Items
            li.Selected = False
        Next
        For Each li As ListItem In Q8.Items
            li.Selected = False
        Next
        For Each li As ListItem In Q9.Items
            li.Selected = False
        Next
        For Each li As ListItem In Q10.Items
            li.Selected = False
        Next

    End Sub

    Private Sub resetall_remarks()
        Q001Remarks.Text = ""
        Q002Remarks.Text = ""
        Q003Remarks.Text = ""
        Q004Remarks.Text = ""
        Q005Remarks.Text = ""
        Q006Remarks.Text = ""
        Q007Remarks.Text = ""
        Q008Remarks.Text = ""
        Q009Remarks.Text = ""
        Q010Remarks.Text = ""

    End Sub

    Private Sub ppcs_eval_load(ByVal strValue As String)
        ''RESET AAL
        resetall_radiobutton()
        resetall_remarks()
        lblMsg.Text = "System message..."
        lblMsgTop.Text = "System message..."

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM ppcs_eval_daily WHERE StudentID='" & Request.QueryString("studentid") & "' AND DateCreated='" & lblSelectedDate.Text & "'"
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
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q001Remarks")) Then
                    Q001Remarks.Text = MyTable.Rows(nRows).Item("Q001Remarks").ToString
                Else
                    Q001Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q002")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q002").ToString
                Else
                    Q2.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q002Remarks")) Then
                    Q002Remarks.Text = MyTable.Rows(nRows).Item("Q002Remarks").ToString
                Else
                    Q002Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q003")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q003").ToString
                Else
                    Q3.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q003Remarks")) Then
                    Q003Remarks.Text = MyTable.Rows(nRows).Item("Q003Remarks").ToString
                Else
                    Q003Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q004")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q004").ToString
                Else
                    Q4.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q004Remarks")) Then
                    Q004Remarks.Text = MyTable.Rows(nRows).Item("Q004Remarks").ToString
                Else
                    Q004Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q005")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q005").ToString
                Else
                    Q5.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q005Remarks")) Then
                    Q005Remarks.Text = MyTable.Rows(nRows).Item("Q005Remarks").ToString
                Else
                    Q005Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q006")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q006").ToString
                Else
                    Q6.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q006Remarks")) Then
                    Q006Remarks.Text = MyTable.Rows(nRows).Item("Q006Remarks").ToString
                Else
                    Q006Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q007")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q007").ToString
                Else
                    Q7.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q007Remarks")) Then
                    Q007Remarks.Text = MyTable.Rows(nRows).Item("Q007Remarks").ToString
                Else
                    Q007Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q008")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q008").ToString
                Else
                    Q8.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q008Remarks")) Then
                    Q008Remarks.Text = MyTable.Rows(nRows).Item("Q008Remarks").ToString
                Else
                    Q008Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q009")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q009").ToString
                Else
                    Q9.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q009Remarks")) Then
                    Q009Remarks.Text = MyTable.Rows(nRows).Item("Q009Remarks").ToString
                Else
                    Q009Remarks.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q010")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q010").ToString
                Else
                    Q10.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q010Remarks")) Then
                    Q010Remarks.Text = MyTable.Rows(nRows).Item("Q010Remarks").ToString
                Else
                    Q010Remarks.Text = ""
                End If
            Else


            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try



    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        '--debug
        btnUpdate.Text = rm.GetString("btnUpdate", ci)
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

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        ''--insert for new entry
        strSQL = "SELECT DateCreated FROM ppcs_eval_daily WITH (NOLOCK) WHERE StudentID='" & Request.QueryString("studentid") & "' AND DateCreated='" & lblSelectedDate.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            ''--update if already exist
            ppcs_eval_update()
        Else
            ''--create new if not exist for the day
            ppcs_eval_insert()
        End If

    End Sub

    Private Function ppcs_eval_update() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If

        '--sum ALL
        Dim nTotalMark As Integer = nQ1 + nQ2 + nQ3 + nQ4 + nQ5 + nQ6 + nQ7 + nQ8 + nQ9 + nQ10

        strSQL = "UPDATE ppcs_eval_daily WITH (UPDLOCK) SET TotalMark=" & nTotalMark & ",LastUpdate='" & oCommon.getNow & "',Q001=" & nQ1 & ",Q002=" & nQ2 & ",Q003=" & nQ3 & ",Q004=" & nQ4 & ",Q005=" & nQ5 & ",Q006=" & nQ6 & ",Q007=" & nQ7 & ",Q008=" & nQ8 & ",Q009=" & nQ9 & ",Q010=" & nQ10 & ",Q001Remarks='" & oCommon.FixSingleQuotes(Q001Remarks.Text) & "',Q002Remarks='" & oCommon.FixSingleQuotes(Q002Remarks.Text) & "',Q003Remarks='" & oCommon.FixSingleQuotes(Q003Remarks.Text) & "',Q004Remarks='" & oCommon.FixSingleQuotes(Q004Remarks.Text) & "',Q005Remarks='" & oCommon.FixSingleQuotes(Q005Remarks.Text) & "',Q006Remarks='" & oCommon.FixSingleQuotes(Q006Remarks.Text) & "',Q007Remarks='" & oCommon.FixSingleQuotes(Q007Remarks.Text) & "',Q008Remarks='" & oCommon.FixSingleQuotes(Q008Remarks.Text) & "',Q009Remarks='" & oCommon.FixSingleQuotes(Q009Remarks.Text) & "',Q010Remarks='" & oCommon.FixSingleQuotes(Q010Remarks.Text) & "' WHERE StudentID='" & Request.QueryString("studentid") & "' AND DateCreated='" & lblSelectedDate.Text & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsgTop.Text = "Rekod berjaya dikemaskini."
            lblMsg.Text = "Rekod berjaya dikemaskini."
        Else
            lblMsgTop.Text = strRet
            lblMsg.Text = strRet
        End If

        Return True
    End Function

    Private Function ppcs_eval_insert() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If

        '--sum ALL(tak guna 2010-10-12)
        Dim nTotalMark As Integer = nQ1 + nQ2 + nQ3 + nQ4 + nQ5 + nQ6 + nQ7 + nQ8 + nQ9 + nQ10

        strSQL = "INSERT INTO ppcs_eval_daily (StudentID,DateCreated,CreateBy,PPCSYear,PPCSDate,ClassCode,ClassID,LastUpdate,Q001,Q002,Q003,Q004,Q005,Q006,Q007,Q008,Q009,Q010,Q001Remarks,Q002Remarks,Q003Remarks,Q004Remarks,Q005Remarks,Q006Remarks,Q007Remarks,Q008Remarks,Q009Remarks,Q010Remarks,TotalMark) Values ('" & Request.QueryString("studentid") & "','" & lblSelectedDate.Text & "','" & Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value) & "','" & calToday.SelectedDate.Year & "','" & Request.QueryString("ppcsdate") & "','" & Request.QueryString("classcode") & "'," & Request.QueryString("classid") & ",'" & oCommon.getNow & "'," & nQ1 & "," & nQ2 & "," & nQ3 & "," & nQ4 & "," & nQ5 & "," & nQ6 & "," & nQ7 & "," & nQ8 & "," & nQ9 & "," & nQ10 & ",'" & oCommon.FixSingleQuotes(Q001Remarks.Text) & "','" & oCommon.FixSingleQuotes(Q002Remarks.Text) & "','" & oCommon.FixSingleQuotes(Q003Remarks.Text) & "','" & oCommon.FixSingleQuotes(Q004Remarks.Text) & "','" & oCommon.FixSingleQuotes(Q005Remarks.Text) & "','" & oCommon.FixSingleQuotes(Q006Remarks.Text) & "','" & oCommon.FixSingleQuotes(Q007Remarks.Text) & "','" & oCommon.FixSingleQuotes(Q008Remarks.Text) & "','" & oCommon.FixSingleQuotes(Q009Remarks.Text) & "','" & oCommon.FixSingleQuotes(Q010Remarks.Text) & "'," & nTotalMark & ")"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsgTop.Text = "Rekod berjaya dikemaskini."
            lblMsg.Text = "Rekod berjaya dikemaskini."
        Else
            lblMsgTop.Text = strRet
            lblMsg.Text = strRet
            Return False
        End If

        Return True
    End Function


    Private Function ValidatePage() As Boolean
        ''--seelct radio buttons
        If Q1.Text.Length = 0 Then
            nQ1 = 0
        Else
            nQ1 = Q1.SelectedValue
        End If

        If Q2.Text.Length = 0 Then
            nQ2 = 0
        Else
            nQ2 = Q2.SelectedValue
        End If

        If Q3.Text.Length = 0 Then
            nQ3 = 0
        Else
            nQ3 = Q3.SelectedValue
        End If

        If Q4.Text.Length = 0 Then
            nQ4 = 0
        Else
            nQ4 = Q4.SelectedValue
        End If

        If Q5.Text.Length = 0 Then
            nQ5 = 0
        Else
            nQ5 = Q5.SelectedValue
        End If

        If Q6.Text.Length = 0 Then
            nQ6 = 0
        Else
            nQ6 = Q6.SelectedValue
        End If
        If Q7.Text.Length = 0 Then
            nQ7 = 0
        Else
            nQ7 = Q7.SelectedValue
        End If
        If Q8.Text.Length = 0 Then
            nQ8 = 0
        Else
            nQ8 = Q8.SelectedValue
        End If
        If Q9.Text.Length = 0 Then
            nQ9 = 0
        Else
            nQ9 = Q9.SelectedValue
        End If
        If Q10.Text.Length = 0 Then
            nQ10 = 0
        Else
            nQ10 = Q10.SelectedValue
        End If

        ''--remarks set to 500 chars only
        If Q001Remarks.Text.Length > 500 Then
            Q001Remarks.Text = Q001Remarks.Text.Substring(0, 498).ToString()
        End If
        If Q002Remarks.Text.Length > 500 Then
            Q002Remarks.Text = Q002Remarks.Text.Substring(0, 498).ToString()
        End If
        If Q003Remarks.Text.Length > 500 Then
            Q003Remarks.Text = Q003Remarks.Text.Substring(0, 498).ToString()
        End If
        If Q004Remarks.Text.Length > 500 Then
            Q004Remarks.Text = Q004Remarks.Text.Substring(0, 498).ToString()
        End If
        If Q005Remarks.Text.Length > 500 Then
            Q005Remarks.Text = Q005Remarks.Text.Substring(0, 498).ToString()
        End If
        If Q006Remarks.Text.Length > 500 Then
            Q006Remarks.Text = Q006Remarks.Text.Substring(0, 498).ToString()
        End If
        If Q007Remarks.Text.Length > 500 Then
            Q007Remarks.Text = Q007Remarks.Text.Substring(0, 498).ToString()
        End If
        If Q008Remarks.Text.Length > 500 Then
            Q008Remarks.Text = Q008Remarks.Text.Substring(0, 498).ToString()
        End If
        If Q009Remarks.Text.Length > 500 Then
            Q009Remarks.Text = Q009Remarks.Text.Substring(0, 498).ToString()
        End If
        If Q010Remarks.Text.Length > 500 Then
            Q010Remarks.Text = Q010Remarks.Text.Substring(0, 498).ToString()
        End If

        Return True
    End Function

    Private Sub calToday_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calToday.SelectionChanged
        lblSelectedDate.Text = calToday.SelectedDate.ToString("yyyyMMdd")

        '--load answers
        ppcs_eval_load(Request.QueryString("studentid"))

    End Sub

    Protected Sub lnkStudentProfileView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkStudentProfileView.Click
        Response.Redirect("ppcs.alumni.studentprofile.aspx?studentid=" & Request.QueryString("studentid"))


    End Sub
End Class