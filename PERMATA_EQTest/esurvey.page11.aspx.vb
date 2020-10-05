Imports System.Globalization
Imports System.Threading
Imports System.Resources
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class esurvey_page11
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
        strSQL = "SELECT Q154txt,Q155txt,Q156txt,Q157txt,Q158txt,Q159txt,Q160txt,Q161txt FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q154txt")) Then
                    txtQ1.Text = MyTable.Rows(nRows).Item("Q154txt").ToString
                Else
                    txtQ1.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q155txt")) Then
                    txtQ2.Text = MyTable.Rows(nRows).Item("Q155txt").ToString
                Else
                    txtQ2.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q156txt")) Then
                    txtQ3.Text = MyTable.Rows(nRows).Item("Q156txt").ToString
                Else
                    txtQ3.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q157txt")) Then
                    txtQ4.Text = MyTable.Rows(nRows).Item("Q157txt").ToString
                Else
                    txtQ4.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q158txt")) Then
                    txtQ5.Text = MyTable.Rows(nRows).Item("Q158txt").ToString
                Else
                    txtQ5.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q159txt")) Then
                    txtQ6.Text = MyTable.Rows(nRows).Item("Q159txt").ToString
                Else
                    txtQ6.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q160txt")) Then
                    txtQ7.Text = MyTable.Rows(nRows).Item("Q160txt").ToString
                Else
                    txtQ7.Text = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q161txt")) Then
                    txtQ8.Text = MyTable.Rows(nRows).Item("Q161txt").ToString
                Else
                    txtQ8.Text = ""
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
        lblAnswer.Text = rm.GetString("lblAnswer", ci)

        lblLevel1.Text = rm.GetString("lblD1Level1", ci)
        lblLevel2.Text = rm.GetString("lblD1Level2", ci)
        lblLevel3.Text = rm.GetString("lblD1Level3", ci)
        lblLevel4.Text = rm.GetString("lblD1Level4", ci)
        lblLevel5.Text = rm.GetString("lblD1Level5", ci)

        lblQ001.Text = rm.GetString("lblQ154", ci)
        lblQ002.Text = rm.GetString("lblQ155", ci)
        lblQ003.Text = rm.GetString("lblQ156", ci)
        lblQ004.Text = rm.GetString("lblQ157", ci)
        lblQ005.Text = rm.GetString("lblQ158", ci)
        lblQ006.Text = rm.GetString("lblQ159", ci)
        lblQ007.Text = rm.GetString("lblQ160", ci)
        lblQ008.Text = rm.GetString("lblQ161", ci)
       


    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim strNextpage As String = "esurvey.page12.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")

        Try
            If validateform() = False Then
                lblMsgTop.Text = lblMsg.Text
                Exit Sub
            End If

            '--update text
            strSQL = "UPDATE EQTest SET Q154txt='" & oCommon.FixSingleQuotes(txtQ1.Text) & "',Q155txt='" & oCommon.FixSingleQuotes(txtQ2.Text) & "',Q156txt='" & oCommon.FixSingleQuotes(txtQ3.Text) & "',Q157txt='" & oCommon.FixSingleQuotes(txtQ4.Text) & "',Q158txt='" & oCommon.FixSingleQuotes(txtQ5.Text) & "',Q159txt='" & oCommon.FixSingleQuotes(txtQ6.Text) & "',Q160txt='" & oCommon.FixSingleQuotes(txtQ7.Text) & "',Q161txt='" & oCommon.FixSingleQuotes(txtQ8.Text) & "' WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
            Else
                lblMsg.Text = strRet
            End If

            '--update mar
            strSQL = "UPDATE EQTest SET LastPage='esurvey.page12.aspx',LastUpdate='" & oCommon.getNow & "',Q154=" & getMark(txtQ1.Text, "Q154") & ",Q155=" & getMark(txtQ2.Text, "Q155") & ",Q156=" & getMark(txtQ3.Text, "Q156") & ",Q157=" & getMark(txtQ4.Text, "Q157") & ",Q158=" & getMark(txtQ5.Text, "Q158") & ",Q159=" & getMark(txtQ6.Text, "Q159") & ",Q160=" & getMark(txtQ7.Text, "Q160") & ",Q161=" & getMark(txtQ8.Text, "Q161") & " WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
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

    Private Function getMark(ByVal strAnswer As String, ByVal strQuestionID As String) As Integer
        Dim bResult As Boolean
        Dim strKeyword As String = "|"
        Dim nMark As Integer = 0
        Dim nCount As Integer
        Dim arstrKeyword

        Select Case strQuestionID
            Case "Q154"
                Select Case Request.QueryString("culture")
                    Case "ms-MY"
                        strKeyword = "saya tidak membelakangkan perasaan saya sendiri|saya boleh sesuai diri dengan suasana sekeliling|penting untuk saya bergaul dengan rakan-rakan saya|saya tahu apa yang saya inginkan|saya tahu keadaan diri saya|saya dapat mengubahnya kearah yang positive|Mencari jawaban pada setiap persoalan|Mudah untuk bertindak mengikut perasaan saya|saya dapat melakukan sesuatu kerja dengan baik atau dapat mengubah cara sesuatu kerja|saya dapat mengetahui apa yang saya inginkan|saya dapat mengetahui apa yang saya fikirkan|cuba kawal perasaan yang negative itu supaya tak berlarutan menjadi masalah|memudahkan saya membuat keputusan|saya tidak stress dan boleh mengawal perasaan|perasaan dapat membantu saya dalam sesuatu tugasan"
                    Case "en-US"
                        strKeyword = ""
                    Case "ar-SA"
                        strKeyword = ""
                    Case "zh-CN"
                        strKeyword = ""
                    Case Else
                        strKeyword = ""
                End Select

            Case "Q155"
                Select Case Request.QueryString("culture")
                    Case "ms-MY"
                        strKeyword = "Untuk memperbaiki kelemahan diri dan menggunakan kekuatan diri|Membaiki kelemahan dan menghargai diri sendiri|Kerana langkah untuk saya mengatasi hal tersebut|dapat mengubah kelemahan kepada kebaikan dan meningkatkan lagi kekuatan saya|dapat bina kejayaan berdasarkan apa yang dikurniakan untuk saya|dapat bina kejayaan berdasarkan apa yang dikurniakan untuk saya dan memperbaiki kelemahan|untuk memperbaiki diri|membina segala kelemahan ini menjadi kekuatan|Kekuatan boleh dipertingkatkan dan kelemahan boleh diperbaiki|Untuk saya mudah memperbaiki diri|dapat menggunakan kekuatan saya untuk membantu orang lain |dapat menggunakan kekuatan saya untuk membantu orang lain dan dapat memperbaiki kelemahan saya|memperbaiki diri|ketahui kelemahan serta kekuatan diri|kekuatan yang ada dapat di baiki|kekuatan dapat membantu kita berpotensi untuk tunjuk bakat yang ada dalam kepimpinan"
                    Case "en-US"
                        strKeyword = ""
                    Case "ar-SA"
                        strKeyword = ""
                    Case "zh-CN"
                        strKeyword = ""
                    Case Else
                        strKeyword = ""
                End Select

            Case "Q156"
                Select Case Request.QueryString("culture")
                    Case "ms-MY"
                        strKeyword = "memudahkan saya menerima sesuatu tugasan|menggunakan kebolehan diri sendiri|tahu di mana hala tuju|dapat mencari peluang untuk meningkatkan diri|dapat mengeksploitasi kebolehan itu untuk kejayaan diri|mencari peluang disebaliknya|tidak rasa rendah diri|mengekspresikannya dengan lebih terbuka|boleh menonjolkan kebolehan yang ada|dapat mencari kebolehan lain selain kebolehan yang sedia ada|membolehkan orang lain mengetahui kebolehan diri saya|dapat menetapkan matlamat yang sesuai dengan kemampuan diri dan dapat mencapainya|dapat yakin pada diri|orang lain tidak tersinggung dengan persaan saya|tidak menyebabkan permusuhan|menunjukkan imej yang baik kepada orang ramai|mengelak masalah yang lebih besar berlaku|tidak menyinggung perasaan orang lain|tidak bercampur aduk antara personaliti dan pelajaran|dapat bertindak rasional|tidak melukai hati orang lain|mempengaruhi tingkah laku dan pemikiran kita"
                    Case "en-US"
                        strKeyword = ""
                    Case "ar-SA"
                        strKeyword = ""
                    Case "zh-CN"
                        strKeyword = ""
                    Case Else
                        strKeyword = ""
                End Select

            Case "Q157"
                Select Case Request.QueryString("culture")
                    Case "ms-MY"
                        strKeyword = "menjaga perasaan orang lain|orang lain tidak berkecil hati|tidak timbul kekecohan dengan orang lain|menjaga hubungan rakan|saya hidup lebih tenang|tidak menyinggung perasaan orang lain|dapat membuat keputusan yang lebih tepat|tidak mengikuti perasaan|motivasi senantiasa diperingkat yang tinggi"
                    Case "en-US"
                        strKeyword = ""
                    Case "ar-SA"
                        strKeyword = ""
                    Case "zh-CN"
                        strKeyword = ""
                    Case Else
                        strKeyword = ""
                End Select

            Case "Q158"
                Select Case Request.QueryString("culture")
                    Case "ms-MY"
                        strKeyword = "Berusaha bersungguh-sungguh|Konsisten dalam kerja|Konsisten dalam pelajaran|sentiasa jelas dengan matlamat sendiri|Belajar bersungguh-sungguh|tahu apa matlamat kita yang sebenar|Berusaha dengan betul|belajar dengan betul|istiqamah dalam setiap usaha yang dilakukan|tidak mudah putus asa|berusaha tanpa mengalah|memperbaiki segala kelemahan|tidak berputus asa|kurangkan keinginan diri yang tidak bermanfaat|banyakkan pengetahuan umum|berpegang kepada konsep study hard|berpegang kepada konsep study smart|sentiasa membuat rujukan|menghormati guru|menghormati ibu bapa|menghormati rakan|menghormati orang yang hampir dengan kita|sentiasa menunaikan suruhan Allah|yakin saya boleh cemerlang|sentiasa mengulangkaji|rajin mencari ilmu "
                    Case "en-US"
                        strKeyword = ""
                    Case "ar-SA"
                        strKeyword = ""
                    Case "zh-CN"
                        strKeyword = ""
                    Case Else
                        strKeyword = ""
                End Select

            Case "Q159"
                Select Case Request.QueryString("culture")
                    Case "ms-MY"
                        strKeyword = "tak berpandangan negative|menyelesaikan tugas dengan sempurna|tidak mendapat tekanan|tidak melemahkan semangat saya untuk menyiapkan tugasan|Tugasan tersebut akan menjadi mudah jika difikirkan ianya mudah|fikiran positive menjana minda|tidak sukar melakukan sesuatu tugasan|membantu motivasi diri|tidak stress ketika menyiapkan tugasan|dapat melakukan tugasan tersebut dengan baik|memberi kepuasan dalam diri|dapat disiapkan dengan sempurna|menghasilkan hasil terbaik|Membolehkan untuk membuat keputusan|dapat di selesaikan dengan jaya|kita dapat yakin dan akan berjaya|menyelesaikan tugasan yang sukar dengan berkesan"
                    Case "en-US"
                        strKeyword = ""
                    Case "ar-SA"
                        strKeyword = ""
                    Case "zh-CN"
                        strKeyword = ""
                    Case Else
                        strKeyword = ""
                End Select

            Case "Q160"
                Select Case Request.QueryString("culture")
                    Case "ms-MY"
                        strKeyword = "Menasihati mereka|menentramkan kedua-dua belah pihak|Mendiamkan diri jika perlu berbincang dengan secara baik|cuba menyelesaikan konflik tersebut|selesaikan|Cari punca konflik|Menasehati mereka|meredakan pertelingkahan|Tanya apakah masalah antara mereka|selesaikan masalah dengan bijak|perlu bersikap terbuka|mendamaikan mereka|Mendamaikan keadaan|mencari punca konflik|menyelesaikan konflik|memberi pandangan"
                    Case "en-US"
                        strKeyword = ""
                    Case "ar-SA"
                        strKeyword = ""
                    Case "zh-CN"
                        strKeyword = ""
                    Case Else
                        strKeyword = ""
                End Select

            Case "Q161"
                Select Case Request.QueryString("culture")
                    Case "ms-MY"
                        strKeyword = "memotivasikan diri sendiri|tidak tersasar dengan matlamat tugasan|hidup bermasyarakat|memerlukan bimbingan dari saya|memudahkan urusan orang lain|membantu mencari pengalaman|dapat menyiapkan tugasan tersebut dengan sempurna|memotivasi diri sendiri|tidak melakukan kesilapan yang besar|ada orang yang mengambil berat|bersama-sama mencapai matlamat|dapat membantu dan belajar|memberi keyakinan pada diri sendiri"
                    Case "en-US"
                        strKeyword = ""
                    Case "ar-SA"
                        strKeyword = ""
                    Case "zh-CN"
                        strKeyword = ""
                    Case Else
                        strKeyword = ""
                End Select

            Case Else
                strKeyword = "|"
        End Select

        If strKeyword.Length > 1 Then
            arstrKeyword = Split(strKeyword.Trim, "|")
            For nCount = 0 To UBound(arstrKeyword)
                bResult = System.Text.RegularExpressions.Regex.IsMatch(strAnswer, arstrKeyword(nCount), System.Text.RegularExpressions.RegexOptions.IgnoreCase)
                If bResult = True Then
                    nMark += 1
                End If
            Next
        End If

        '--maximum mark is 2
        If nMark > 2 Then
            nMark = 2
        End If

        getMark = nMark

    End Function

    Private Function validateform() As Boolean
        If txtQ1.Text.Length = 0 Then
            lblMsg.Text = "Please answer all the Questions. #154"
            Return False
        End If
        If txtQ2.Text = "" Then
            lblMsg.Text = "Please answer all the questions. #155"
            Return False
        End If
        If txtQ3.Text = "" Then
            lblMsg.Text = "Please answer all the questions. #156"
            Return False
        End If
        If txtQ4.Text = "" Then
            lblMsg.Text = "Please answer all the questions. #157"
            Return False
        End If
        If txtQ5.Text = "" Then
            lblMsg.Text = "Please answer all the questions. #158"
            Return False
        End If

        If txtQ6.Text = "" Then
            lblMsg.Text = "Please answer all the questions. #159"
            Return False
        End If
        If txtQ7.Text = "" Then
            lblMsg.Text = "Please answer all the questions. #160"
            Return False
        End If
        If txtQ8.Text = "" Then
            lblMsg.Text = "Please answer all the questions. #161"
            Return False
        End If

        Return True

    End Function

    Private Sub btnPrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrev.Click
        Dim strPrevpage As String = "esurvey.page10.aspx?loginid=" & Request.QueryString("loginid") & "&surveyid=" & Request.QueryString("surveyid") & "&culture=" & Request.QueryString("culture")
        Response.Redirect(strPrevpage)

    End Sub

End Class