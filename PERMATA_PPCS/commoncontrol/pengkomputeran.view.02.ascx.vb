Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Resources

Partial Public Class pengkomputeran_view_02
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
            Dim strBasename As String = "Resources.komputer2010"

            Thread.CurrentThread.CurrentCulture = New CultureInfo(Server.HtmlEncode(Request.Cookies("ppcs_culture").Value))
            'get the culture info to set the language
            rm = New ResourceManager(strBasename, System.Reflection.Assembly.Load("App_GlobalResources"))
            ci = Thread.CurrentThread.CurrentCulture
            LoadStrings(ci)

            If Not IsPostBack Then
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

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q013")) Then
                    Q1.Text = MyTable.Rows(nRows).Item("Q013").ToString
                Else
                    Q1.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q014")) Then
                    Q2.Text = MyTable.Rows(nRows).Item("Q014").ToString
                Else
                    Q2.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q015")) Then
                    Q3.Text = MyTable.Rows(nRows).Item("Q015").ToString
                Else
                    Q3.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q016")) Then
                    Q4.Text = MyTable.Rows(nRows).Item("Q016").ToString
                Else
                    Q4.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q017")) Then
                    Q5.Text = MyTable.Rows(nRows).Item("Q017").ToString
                Else
                    Q5.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q018")) Then
                    Q6.Text = MyTable.Rows(nRows).Item("Q018").ToString
                Else
                    Q6.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q019")) Then
                    Q7.Text = MyTable.Rows(nRows).Item("Q019").ToString
                Else
                    Q7.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q020")) Then
                    Q8.Text = MyTable.Rows(nRows).Item("Q020").ToString
                Else
                    Q8.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q021")) Then
                    Q9.Text = MyTable.Rows(nRows).Item("Q021").ToString
                Else
                    Q9.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q022")) Then
                    Q10.Text = MyTable.Rows(nRows).Item("Q022").ToString
                Else
                    Q10.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q023")) Then
                    Q11.Text = MyTable.Rows(nRows).Item("Q023").ToString
                Else
                    Q11.SelectedValue = ""
                End If
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q024")) Then
                    Q12.Text = MyTable.Rows(nRows).Item("Q024").ToString
                Else
                    Q12.SelectedValue = ""
                End If

                ''------------------
                If Not IsDBNull(MyTable.Rows(nRows).Item("Q025")) Then
                    Q13.Text = MyTable.Rows(nRows).Item("Q025").ToString
                Else
                    Q13.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q026")) Then
                    Q14.Text = MyTable.Rows(nRows).Item("Q026").ToString
                Else
                    Q14.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q027")) Then
                    Q15.Text = MyTable.Rows(nRows).Item("Q027").ToString
                Else
                    Q15.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q028")) Then
                    Q16.Text = MyTable.Rows(nRows).Item("Q028").ToString
                Else
                    Q16.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q029")) Then
                    Q17.Text = MyTable.Rows(nRows).Item("Q029").ToString
                Else
                    Q17.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q030")) Then
                    Q18.Text = MyTable.Rows(nRows).Item("Q030").ToString
                Else
                    Q18.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q031")) Then
                    Q19.Text = MyTable.Rows(nRows).Item("Q031").ToString
                Else
                    Q19.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q032")) Then
                    Q20.Text = MyTable.Rows(nRows).Item("Q032").ToString
                Else
                    Q20.SelectedValue = ""
                End If

                If Not IsDBNull(MyTable.Rows(nRows).Item("Q033")) Then
                    Q21.Text = MyTable.Rows(nRows).Item("Q033").ToString
                Else
                    Q21.SelectedValue = ""
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
        lblQPage02.Text = rm.GetString("lblQPage02", ci)
        lblAPage02.Text = rm.GetString("lblAPage02", ci)

        lblQ001.Text = rm.GetString("Q013", ci)
        lblQ002.Text = rm.GetString("Q014", ci)
        lblQ003.Text = rm.GetString("Q015", ci)
        lblQ004.Text = rm.GetString("Q016", ci)
        lblQ005.Text = rm.GetString("Q017", ci)
        lblQ006.Text = rm.GetString("Q018", ci)
        lblQ007.Text = rm.GetString("Q019", ci)
        lblQ008.Text = rm.GetString("Q020", ci)
        lblQ009.Text = rm.GetString("Q021", ci)
        lblQ010.Text = rm.GetString("Q022", ci)
        lblQ011.Text = rm.GetString("Q023", ci)
        lblQ012.Text = rm.GetString("Q024", ci)
        ''--
        lblQ013.Text = rm.GetString("Q025", ci)
        lblQ014.Text = rm.GetString("Q026", ci)
        lblQ015.Text = rm.GetString("Q027", ci)
        lblQ016.Text = rm.GetString("Q028", ci)
        lblQ017.Text = rm.GetString("Q029", ci)
        lblQ018.Text = rm.GetString("Q030", ci)
        lblQ019.Text = rm.GetString("Q031", ci)
        lblQ020.Text = rm.GetString("Q032", ci)
        lblQ021.Text = rm.GetString("Q033", ci)

        '--answer string
        Q1.Items(0).Text = rm.GetString("Q013_1", ci)
        Q1.Items(1).Text = rm.GetString("Q013_2", ci)

        Q2.Items(0).Text = rm.GetString("Q013_1", ci)
        Q2.Items(1).Text = rm.GetString("Q013_2", ci)

        Q3.Items(0).Text = rm.GetString("Q013_1", ci)
        Q3.Items(1).Text = rm.GetString("Q013_2", ci)

        Q4.Items(0).Text = rm.GetString("Q013_1", ci)
        Q4.Items(1).Text = rm.GetString("Q013_2", ci)

        Q5.Items(0).Text = rm.GetString("Q013_1", ci)
        Q5.Items(1).Text = rm.GetString("Q013_2", ci)

        Q6.Items(0).Text = rm.GetString("Q013_1", ci)
        Q6.Items(1).Text = rm.GetString("Q013_2", ci)

        Q7.Items(0).Text = rm.GetString("Q013_1", ci)
        Q7.Items(1).Text = rm.GetString("Q013_2", ci)

        Q8.Items(0).Text = rm.GetString("Q013_1", ci)
        Q8.Items(1).Text = rm.GetString("Q013_2", ci)

        Q9.Items(0).Text = rm.GetString("Q013_1", ci)
        Q9.Items(1).Text = rm.GetString("Q013_2", ci)

        Q10.Items(0).Text = rm.GetString("Q013_1", ci)
        Q10.Items(1).Text = rm.GetString("Q013_2", ci)

        Q11.Items(0).Text = rm.GetString("Q013_1", ci)
        Q11.Items(1).Text = rm.GetString("Q013_2", ci)

        Q12.Items(0).Text = rm.GetString("Q013_1", ci)
        Q12.Items(1).Text = rm.GetString("Q013_2", ci)

        ''------
        Q13.Items(0).Text = rm.GetString("Q013_1", ci)
        Q13.Items(1).Text = rm.GetString("Q013_2", ci)

        Q14.Items(0).Text = rm.GetString("Q013_1", ci)
        Q14.Items(1).Text = rm.GetString("Q013_2", ci)

        Q15.Items(0).Text = rm.GetString("Q013_1", ci)
        Q15.Items(1).Text = rm.GetString("Q013_2", ci)

        Q16.Items(0).Text = rm.GetString("Q013_1", ci)
        Q16.Items(1).Text = rm.GetString("Q013_2", ci)

        Q17.Items(0).Text = rm.GetString("Q013_1", ci)
        Q17.Items(1).Text = rm.GetString("Q013_2", ci)

        Q18.Items(0).Text = rm.GetString("Q013_1", ci)
        Q18.Items(1).Text = rm.GetString("Q013_2", ci)

        Q19.Items(0).Text = rm.GetString("Q013_1", ci)
        Q19.Items(1).Text = rm.GetString("Q013_2", ci)

        Q20.Items(0).Text = rm.GetString("Q013_1", ci)
        Q20.Items(1).Text = rm.GetString("Q013_2", ci)

        Q21.Items(0).Text = rm.GetString("Q013_1", ci)
        Q21.Items(1).Text = rm.GetString("Q013_2", ci)

    End Sub


    

End Class