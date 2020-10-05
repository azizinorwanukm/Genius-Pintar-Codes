Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class pengkomputeran_view_01
    Inherits System.Web.UI.UserControl

    Private rm As ResourceManager

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim nPageno As Integer = 0
    Dim strDateCreated As String
    Dim strcourseCode As String

    Dim strLoginID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not Request.Cookies("ppcs_loginid") Is Nothing Then
                strLoginID = Server.HtmlEncode(Request.Cookies("ppcs_loginid").Value)
            End If

            Dim ci As CultureInfo
            Dim strBasename As String = "Resources.komputer2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture

            If Not IsPostBack Then
                ''--load question text
                LoadStrings(ci)

                '--load answers
                ppcs_komputer_load(Request.QueryString("tokenid"))
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
            Response.Cookies("ppcs_culture").Value = "ms-MY"
        End Try
    End Sub

    Private Sub ppcs_komputer_load(ByVal strValue As String)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM ppcs_komputer WHERE Tokenid='" & Request.QueryString("tokenid") & "' AND PPCSYear='" & Request.QueryString("year") & "'"
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

                ''--display checkbox list
                Dim strQ007 As String
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q007")) Then
                    strQ007 = MyTable.Rows(nRows).Item("Q007").ToString
                Else
                    strQ007 = ""
                End If
                dispCheckbox(strQ007, cblQ007)

                Dim strQ008 As String
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q008")) Then
                    strQ008 = MyTable.Rows(nRows).Item("Q008").ToString
                Else
                    strQ008 = ""
                End If
                dispCheckbox(strQ008, cblQ008)

                Dim strQ009 As String
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q009")) Then
                    strQ009 = MyTable.Rows(nRows).Item("Q009").ToString
                Else
                    strQ009 = ""
                End If
                dispCheckbox(strQ009, cblQ009)

                Dim strQ010 As String
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q010")) Then
                    strQ010 = MyTable.Rows(nRows).Item("Q010").ToString
                Else
                    strQ010 = ""
                End If
                dispCheckbox(strQ010, cblQ010)
                ''--end checkboxlist

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

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub dispCheckbox(ByVal strValue As String, ByVal cblCheckbox As CheckBoxList)
        Dim i As Integer = 0

        For i = 1 To strValue.Length
            If Mid(strValue, i, 1) = "1" Then
                cblCheckbox.Items(i - 1).Selected = True
            Else
                cblCheckbox.Items(i - 1).Selected = False
            End If
        Next

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        '--debug
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
        lblQ011.Text = rm.GetString("Q011", ci)
        lblQ012.Text = rm.GetString("Q012", ci)

        '--answer string
        Q1.Items(0).Text = rm.GetString("Q001_1", ci)
        Q1.Items(1).Text = rm.GetString("Q001_2", ci)


        Q2.Items(0).Text = rm.GetString("Q002_1", ci)
        Q2.Items(1).Text = rm.GetString("Q002_2", ci)
        Q2.Items(2).Text = rm.GetString("Q002_3", ci)


        Q3.Items(0).Text = rm.GetString("Q003_1", ci)
        Q3.Items(1).Text = rm.GetString("Q003_2", ci)
        Q3.Items(2).Text = rm.GetString("Q003_3", ci)


        Q4.Items(0).Text = rm.GetString("Q004_1", ci)
        Q4.Items(1).Text = rm.GetString("Q004_2", ci)

        Q5.Items(0).Text = rm.GetString("Q005_1", ci)
        Q5.Items(1).Text = rm.GetString("Q005_2", ci)


        Q6.Items(0).Text = rm.GetString("Q006_1", ci)
        Q6.Items(1).Text = rm.GetString("Q006_2", ci)

        '--answer string
        cblQ007.Items.Add(rm.GetString("Q007_1", ci))
        cblQ007.Items.Add(rm.GetString("Q007_2", ci))
        cblQ007.Items.Add(rm.GetString("Q007_3", ci))
        cblQ007.Items.Add(rm.GetString("Q007_4", ci))
        cblQ007.Items.Add(rm.GetString("Q007_5", ci))

        '--answer string
        cblQ008.Items.Add(rm.GetString("Q008_1", ci))
        cblQ008.Items.Add(rm.GetString("Q008_2", ci))
        cblQ008.Items.Add(rm.GetString("Q008_3", ci))
        cblQ008.Items.Add(rm.GetString("Q008_4", ci))
        cblQ008.Items.Add(rm.GetString("Q008_5", ci))

        '--answer string
        cblQ009.Items.Add(rm.GetString("Q009_1", ci))
        cblQ009.Items.Add(rm.GetString("Q009_2", ci))
        cblQ009.Items.Add(rm.GetString("Q009_3", ci))
        cblQ009.Items.Add(rm.GetString("Q009_4", ci))
        cblQ009.Items.Add(rm.GetString("Q009_5", ci))
        cblQ009.Items.Add(rm.GetString("Q009_6", ci))
        cblQ009.Items.Add(rm.GetString("Q009_7", ci))
        cblQ009.Items.Add(rm.GetString("Q009_8", ci))
        cblQ009.Items.Add(rm.GetString("Q009_9", ci))
        cblQ009.Items.Add(rm.GetString("Q009_10", ci))

        '--answer string
        cblQ010.Items.Add(rm.GetString("Q010_1", ci))
        cblQ010.Items.Add(rm.GetString("Q010_2", ci))
        cblQ010.Items.Add(rm.GetString("Q010_3", ci))
        cblQ010.Items.Add(rm.GetString("Q010_4", ci))
        cblQ010.Items.Add(rm.GetString("Q010_5", ci))

        Q11.Items(0).Text = rm.GetString("Q011_1", ci)
        Q11.Items(1).Text = rm.GetString("Q011_2", ci)

        Q12.Items(0).Text = rm.GetString("Q012_1", ci)
        Q12.Items(1).Text = rm.GetString("Q012_2", ci)
        Q12.Items(2).Text = rm.GetString("Q012_3", ci)

    End Sub

    Private Function ppcs_komputer_insert() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If


        ''--get checkboxlist value start
        Dim i As Integer

        Dim strCheckedQ007 As String = ""
        For i = 0 To cblQ007.Items.Count - 1
            If cblQ007.Items(i).Selected Then
                strCheckedQ007 += "1"
            Else
                strCheckedQ007 += "0"
            End If
        Next

        Dim strCheckedQ008 As String = ""
        For i = 0 To cblQ008.Items.Count - 1
            If cblQ008.Items(i).Selected Then
                strCheckedQ008 += "1"
            Else
                strCheckedQ008 += "0"
            End If
        Next

        Dim strCheckedQ009 As String = ""
        For i = 0 To cblQ009.Items.Count - 1
            If cblQ009.Items(i).Selected Then
                strCheckedQ009 += "1"
            Else
                strCheckedQ009 += "0"
            End If
        Next

        Dim strCheckedQ010 As String = ""
        For i = 0 To cblQ010.Items.Count - 1
            If cblQ010.Items(i).Selected Then
                strCheckedQ010 += "1"
            Else
                strCheckedQ010 += "0"
            End If
        Next

        strSQL = "INSERT INTO ppcs_komputer (Tokenid,PPCSYear,DateCreated,CreateBy,ClassCode,LastUpdate,Q001,Q002,Q003,Q004,Q005,Q006,Q007,Q008,Q009,Q010,Q011,Q012) Values ('" & Request.QueryString("tokenid") & "','" & Request.QueryString("year") & "','" & oCommon.getToday & "','" & strLoginID & "','" & Server.HtmlEncode(Request.Cookies("ppcs_classcode").Value) & "','" & oCommon.getNow & "'," & Q1.SelectedValue & "," & Q2.SelectedValue & "," & Q3.SelectedValue & "," & Q4.SelectedValue & "," & Q5.SelectedValue & "," & Q6.SelectedValue & ",'" & strCheckedQ007 & "','" & strCheckedQ008 & "','" & strCheckedQ009 & "','" & strCheckedQ010 & "'," & Q11.SelectedValue & "," & Q12.SelectedValue & ")"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya kemaskini soalselidik ini."
        Else
            lblMsg.Text = strRet
            Return False
        End If

        Return True
    End Function

    Private Function ppcs_komputer_update() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If

        ''--get checkboxlist value start
        Dim i As Integer

        Dim strCheckedQ007 As String = ""
        For i = 0 To cblQ007.Items.Count - 1
            If cblQ007.Items(i).Selected Then
                strCheckedQ007 += "1"
            Else
                strCheckedQ007 += "0"
            End If
        Next

        Dim strCheckedQ008 As String = ""
        For i = 0 To cblQ008.Items.Count - 1
            If cblQ008.Items(i).Selected Then
                strCheckedQ008 += "1"
            Else
                strCheckedQ008 += "0"
            End If
        Next

        Dim strCheckedQ009 As String = ""
        For i = 0 To cblQ009.Items.Count - 1
            If cblQ009.Items(i).Selected Then
                strCheckedQ009 += "1"
            Else
                strCheckedQ009 += "0"
            End If
        Next

        Dim strCheckedQ010 As String = ""
        For i = 0 To cblQ010.Items.Count - 1
            If cblQ010.Items(i).Selected Then
                strCheckedQ010 += "1"
            Else
                strCheckedQ010 += "0"
            End If
        Next

        strSQL = "UPDATE ppcs_komputer SET LastUpdate='" & oCommon.getNow & "',Q001=" & Q1.SelectedValue & ",Q002=" & Q2.SelectedValue & ",Q003=" & Q3.SelectedValue & ",Q004=" & Q4.SelectedValue & ",Q005=" & Q5.SelectedValue & ",Q006=" & Q6.SelectedValue & ",Q007='" & strCheckedQ007 & "',Q008='" & strCheckedQ008 & "',Q009='" & strCheckedQ009 & "',Q010='" & strCheckedQ010 & "',Q011=" & Q11.SelectedValue & ",Q012=" & Q12.SelectedValue & " WHERE Tokenid='" & Request.QueryString("tokenid") & "' AND PPCSYear='" & Request.QueryString("year") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya kemaskini soalselidik ini."
        Else
            lblMsg.Text = strRet
        End If

        Return True
    End Function

    Private Function ValidatePage() As Boolean
        If Q1.Text.Length = 0 Then
            lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
            Q1.Focus()
            Return False
        End If

        If Q2.Text.Length = 0 Then
            lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
            Q2.Focus()
            Return False
        End If

        If Q3.Text.Length = 0 Then
            lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
            Q3.Focus()
            Return False
        End If

        If Q4.Text.Length = 0 Then
            lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
            Q4.Focus()
            Return False
        End If
        If Q5.Text.Length = 0 Then
            lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
            Q5.Focus()
            Return False
        End If
        If Q6.Text.Length = 0 Then
            lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
            Q6.Focus()
            Return False
        End If
        ''If Q7.Text.Length = 0 Then
        ''    lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
        ''    Q7.Focus()
        ''    Return False
        ''End If
        ''If Q8.Text.Length = 0 Then
        ''    lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
        ''    Q8.Focus()
        ''    Return False
        ''End If
        ''If Q9.Text.Length = 0 Then
        ''    lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
        ''    Q9.Focus()
        ''    Return False
        ''End If
        ''If Q10.Text.Length = 0 Then
        ''    lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
        ''    Q10.Focus()
        ''    Return False
        ''End If
        If Q11.Text.Length = 0 Then
            lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
            Q11.Focus()
            Return False
        End If
        If Q12.Text.Length = 0 Then
            lblMsg.Text = "Sila pilih jawapan untuk setiap soalan."
            Q12.Focus()
            Return False
        End If

        Return True
    End Function

End Class