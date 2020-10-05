﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class keterujaan_view_03
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            Dim ci As CultureInfo
            Dim strBasename As String = "Resources.keterujaan2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            If Not IsPostBack Then
                '--load answers
                ppcs_keterujaan_load(Request.QueryString("tokenid"))
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub ppcs_keterujaan_load(ByVal strValue As String)
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        strSQL = "SELECT * FROM ppcs_keterujaan WHERE Tokenid='" & Request.QueryString("tokenid") & "' AND PPCSYear='" & Request.QueryString("year") & "'"
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q021")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q021").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q022")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q022").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q023")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q023").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q024")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q024").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q025")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q025").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q026")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q026").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q027")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q027").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q028")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q028").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q029")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q029").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q030")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q030").ToString
                Else
                    Q10.SelectedValue = ""
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
        lblSoalan.Text = rm.GetString("lblSoalan", ci)
        lblJawapan.Text = rm.GetString("lblJawapan", ci)

        lblQ001.Text = rm.GetString("Q021", ci)
        lblQ002.Text = rm.GetString("Q022", ci)
        lblQ003.Text = rm.GetString("Q023", ci)
        lblQ004.Text = rm.GetString("Q024", ci)
        lblQ005.Text = rm.GetString("Q025", ci)
        lblQ006.Text = rm.GetString("Q026", ci)
        lblQ007.Text = rm.GetString("Q027", ci)
        lblQ008.Text = rm.GetString("Q028", ci)
        lblQ009.Text = rm.GetString("Q029", ci)
        lblQ010.Text = rm.GetString("Q030", ci)

        '--answer string
        Q1.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q1.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q1.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q1.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q1.Items(4).Text = rm.GetString("AgreeLevel5", ci)

        Q2.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q2.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q2.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q2.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q2.Items(4).Text = rm.GetString("AgreeLevel5", ci)

        Q3.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q3.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q3.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q3.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q3.Items(4).Text = rm.GetString("AgreeLevel5", ci)

        Q4.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q4.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q4.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q4.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q4.Items(4).Text = rm.GetString("AgreeLevel5", ci)

        Q5.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q5.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q5.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q5.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q5.Items(4).Text = rm.GetString("AgreeLevel5", ci)

        Q6.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q6.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q6.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q6.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q6.Items(4).Text = rm.GetString("AgreeLevel5", ci)

        Q7.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q7.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q7.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q7.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q7.Items(4).Text = rm.GetString("AgreeLevel5", ci)

        Q8.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q8.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q8.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q8.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q8.Items(4).Text = rm.GetString("AgreeLevel5", ci)

        Q9.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q9.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q9.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q9.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q9.Items(4).Text = rm.GetString("AgreeLevel5", ci)

        Q10.Items(0).Text = rm.GetString("AgreeLevel1", ci)
        Q10.Items(1).Text = rm.GetString("AgreeLevel2", ci)
        Q10.Items(2).Text = rm.GetString("AgreeLevel3", ci)
        Q10.Items(3).Text = rm.GetString("AgreeLevel4", ci)
        Q10.Items(4).Text = rm.GetString("AgreeLevel5", ci)

    End Sub

End Class