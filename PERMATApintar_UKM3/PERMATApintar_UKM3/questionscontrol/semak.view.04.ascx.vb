Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class semak_view_04
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
    Dim strTokenid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        strTokenid = Request.QueryString("tokenid")

        ''--todays date
        strDateCreated = oCommon.getToday

        Try

            Dim ci As CultureInfo
            Dim strBasename As String = "Resources.semak2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            If Not IsPostBack Then
                '--load answers
                ppcs_semak_load(strTokenid)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub


    Private Sub ppcs_semak_load(ByVal strValue As String)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM ppcs_semak WHERE Tokenid='" & strTokenid & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q033")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q033").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q034")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q034").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q035")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q035").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q036")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q036").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q037")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q037").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q038")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q038").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q039")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q039").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q040")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q040").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q041")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q041").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q042")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q042").ToString
                Else
                    Q10.SelectedValue = ""
                End If


                If Not IsDBNull(MyTable.Rows(nRows).Item("Q043")) Then
                    Q11.Text = MyTable.Rows(nRows).Item("Q043").ToString
                Else
                    Q11.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q044")) Then
                    Q12.Text = MyTable.Rows(nRows).Item("Q044").ToString
                Else
                    Q12.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q045")) Then
                    Q13.Text = MyTable.Rows(nRows).Item("Q045").ToString
                Else
                    Q13.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q046")) Then
                    Q14.Text = MyTable.Rows(nRows).Item("Q046").ToString
                Else
                    Q14.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q047")) Then
                    Q15.Text = MyTable.Rows(nRows).Item("Q047").ToString
                Else
                    Q15.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q048")) Then
                    Q16.Text = MyTable.Rows(nRows).Item("Q048").ToString
                Else
                    Q16.SelectedValue = ""
                End If

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub LoadStrings(ByVal ci As CultureInfo)
        '--debug
        ''--btnNext.Text = rm.GetString("btnNext", ci)
        lblSoalan.Text = rm.GetString("lblSoalan", ci)
        lblJawapan.Text = rm.GetString("lblJawapan", ci)

        lblQ001.Text = rm.GetString("Q033", ci)
        lblQ002.Text = rm.GetString("Q034", ci)
        lblQ003.Text = rm.GetString("Q035", ci)
        lblQ004.Text = rm.GetString("Q036", ci)
        lblQ005.Text = rm.GetString("Q037", ci)
        lblQ006.Text = rm.GetString("Q038", ci)
        lblQ007.Text = rm.GetString("Q039", ci)
        lblQ008.Text = rm.GetString("Q040", ci)
        lblQ009.Text = rm.GetString("Q041", ci)
        lblQ010.Text = rm.GetString("Q042", ci)
        lblQ011.Text = rm.GetString("Q043", ci)
        lblQ012.Text = rm.GetString("Q044", ci)
        lblQ013.Text = rm.GetString("Q045", ci)
        lblQ014.Text = rm.GetString("Q046", ci)
        lblQ015.Text = rm.GetString("Q047", ci)
        lblQ016.Text = rm.GetString("Q048", ci)

        '--answer string
        Q1.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q1.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q1.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q1.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q2.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q2.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q2.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q2.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q3.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q3.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q3.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q3.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q4.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q4.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q4.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q4.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q5.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q5.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q5.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q5.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q6.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q6.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q6.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q6.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q7.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q7.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q7.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q7.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q8.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q8.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q8.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q8.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q9.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q9.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q9.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q9.Items(3).Text = rm.GetString("AgreeLevel4", ci)


        Q10.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q10.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q10.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q10.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q11.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q11.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q11.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q11.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q12.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q12.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q12.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q12.Items(3).Text = rm.GetString("AgreeLevel4", ci)


        Q13.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q13.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q13.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q13.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q14.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q14.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q14.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q14.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q15.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q15.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q15.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q15.Items(3).Text = rm.GetString("AgreeLevel4", ci)

        Q16.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q16.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q16.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q16.Items(3).Text = rm.GetString("AgreeLevel4", ci)


    End Sub


    Private Function ppcs_semak_update() As Boolean
        'check form validation. if failed exit
        If ValidatePage() = False Then
            Exit Function
        End If

        strSQL = "UPDATE ppcs_semak SET LastUpdate='" & oCommon.getNow & "',Q033=" & Q1.SelectedValue & ",Q034=" & Q2.SelectedValue & ",Q035=" & Q3.SelectedValue & ",Q036=" & Q4.SelectedValue & ",Q037=" & Q5.SelectedValue & ",Q038=" & Q6.SelectedValue & ",Q039=" & Q7.SelectedValue & ",Q040=" & Q8.SelectedValue & ",Q041=" & Q9.SelectedValue & ",Q042=" & Q10.SelectedValue & ",Q043=" & Q11.SelectedValue & ",Q044=" & Q12.SelectedValue & ",Q045=" & Q13.SelectedValue & ",Q046=" & Q14.SelectedValue & ",Q047=" & Q15.SelectedValue & ",Q048=" & Q16.SelectedValue & " WHERE Tokenid='" & strTokenid & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            ''--lblMsgTop.Text = "Rekod berjaya dikemaskini."
            lblMsg.Text = "Rekod berjaya dikemaskini."
        Else
            ''--lblMsgTop.Text = strRet
            lblMsg.Text = strRet
        End If

        Return True
    End Function


    Private Function ValidatePage() As Boolean
        If Q1.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q1.Focus()
            Return False
        End If

        If Q2.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q2.Focus()
            Return False
        End If

        If Q3.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q3.Focus()
            Return False
        End If

        If Q4.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q4.Focus()
            Return False
        End If
        If Q5.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q5.Focus()
            Return False
        End If
        If Q6.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q6.Focus()
            Return False
        End If
        If Q7.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q7.Focus()
            Return False
        End If
        If Q8.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q8.Focus()
            Return False
        End If
        If Q9.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q9.Focus()
            Return False
        End If
        If Q10.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q10.Focus()
            Return False
        End If

        If Q11.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q11.Focus()
            Return False
        End If
        If Q12.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q12.Focus()
            Return False
        End If

        If Q13.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q13.Focus()
            Return False
        End If
        If Q14.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q14.Focus()
            Return False
        End If
        If Q15.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q15.Focus()
            Return False
        End If
        If Q16.Text.Length = 0 Then
            lblMsg.Text = "Pilih markah antara 1 hingga 4."
            Q16.Focus()
            Return False
        End If

        Return True
    End Function

End Class