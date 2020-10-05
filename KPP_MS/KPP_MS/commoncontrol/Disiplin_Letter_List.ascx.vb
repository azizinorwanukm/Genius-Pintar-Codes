Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports System.Net.Mail
Imports System.Linq
Imports System.Net
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports System.Text
Imports System.Web
Imports iTextSharp.text.html.simpleparser
Imports iTextSharp.tool.xml

Public Class Disiplin_Letter_List
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")
    Dim page_view As New Integer
    Dim tempSTDID As String
    Dim tempDISPID As String
    Dim cmd As SqlCommand
    Dim errCount As Integer
    Dim stdID As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            stdID = Request.QueryString("stdID")
            setDataLbl()
            strRet = BindData(datRespondent)
        End If
    End Sub

    Protected Sub setDataLbl()
        If Not IsNumeric(stdID) Then
            Return
        End If
        Dim query As String = "SELECT 
	        student_info.student_Name,
	        class_info.class_Name
        FROM 
	        dicipline_info
	        JOIN student_info
	        ON student_info.std_ID = dicipline_info.std_ID 
	        JOIN class_info
	        ON class_info.class_ID = dicipline_info.class_ID 
        WHERE dicipline_info.std_ID ='" & stdID & "'"
        Dim sqlDA As New SqlDataAdapter(query, objConn)
        Try
            Dim ds As New DataSet
            sqlDA.Fill(ds, "AnyTable")

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                wlListStudentName.Text = ds.Tables(0).Rows(0).Item("student_Name")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Name")) Then
                wlClassName.Text = ds.Tables(0).Rows(0).Item("class_Name")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function getSQL() As String
        Dim mainQuery As String = "SELECT 
	                                dicipline_info.disiplin_id,
	                                student_info.student_Name,
                                    student_info.student_ID,
	                                student_info.student_Mykad,
	                                class_info.class_Name,
	                                case_info.case_Name,
	                                dicipline_info.Dicipline_Date,
	                                counseling_info.kslr_status
                                FROM 
	                                kolejadmin.dbo.dicipline_info
	                                LEFT JOIN student_info
	                                ON student_info.std_ID = dicipline_info.std_ID
	                                LEFT JOIN class_info
	                                ON class_info.class_ID = dicipline_info.class_ID
	                                LEFT JOIN case_info
	                                ON case_info.case_ID = dicipline_info.case_ID
	                                LEFT JOIN counseling_info
	                                ON counseling_info.disiplin_id = dicipline_info.disiplin_id"
        Dim whereQuery As String = " WHERE dicipline_info.std_ID ='" & stdID & "'"
        Dim orderByQuery As String = " ORDER BY dicipline_info.Dicipline_Date ASC"
        getSQL = mainQuery & whereQuery & orderByQuery
        Return getSQL
    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim mydataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(mydataSet, "myaccount")
            gvTable.DataSource = mydataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Dim dispIDKey = datRespondent.DataKeys(e.NewEditIndex).Value.ToString

        Response.Redirect("admin_detail_disiplin.aspx?dispID=" & dispIDKey & "&v=2" & "&admin_ID=" & Request.QueryString("admin_ID"))
    End Sub

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim dispIDKey As String = datRespondent.DataKeys(e.RowIndex).Value.ToString
        Dim removeLetter As String = "UPDATE dicipline_info SET warning_letter = ' ' WHERE disiplin_id='" & dispIDKey & "'"
        Try
            Dim i = oCommon.ExecuteSQL(removeLetter)
            If i = "0" Then
                errCount = 8
            Else
                errCount = 9
            End If
        Catch ex As Exception
        Finally
            Response.Redirect("admin_detail_disiplin.aspx?stdID=" & Request.QueryString("stdID") & "&v=1" & "&admin_ID=" & Request.QueryString("admin_ID") & "&result=" & errCount)
        End Try
    End Sub

    Private Sub BtnBack_ServerClick(sender As Object, e As EventArgs) Handles BtnBack.ServerClick
        Response.Redirect("admin_view_disiplin.aspx?admin_ID=" & Request.QueryString("admin_ID"))
    End Sub

    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand

        If e.CommandName = "Print" Then

            ''find name to replace
            Dim get_Name As String = "select student_Name from student_info
                                      left join dicipline_info on student_info.std_ID = dicipline_info.std_ID
                                      where disiplin_id = '" & e.CommandArgument & "'"
            Dim find_Name As String = oCommon.getFieldValue(get_Name)

            ''find mykad to replace
            Dim get_Mykad As String = "select student_Mykad from student_info
                                      left join dicipline_info on student_info.std_ID = dicipline_info.std_ID
                                      where disiplin_id = '" & e.CommandArgument & "'"
            Dim find_Mykad As String = oCommon.getFieldValue(get_Mykad)

            ''find id to replace
            Dim get_ID As String = "select student_ID from student_info
                                      left join dicipline_info on student_info.std_ID = dicipline_info.std_ID
                                      where disiplin_id = '" & e.CommandArgument & "'"
            Dim find_ID As String = oCommon.getFieldValue(get_ID)

            ''find class to replace
            Dim get_class As String = "select class_info.class_Name from class_info
                                      left join dicipline_info on class_info.class_ID = dicipline_info.class_ID
                                      where disiplin_id = '" & e.CommandArgument & "'"
            Dim find_class As String = oCommon.getFieldValue(get_class)

            Dim query As String = "select warning_letters_table.letter_content from warning_letters_table 
                                   left join dicipline_info on warning_letters_table.id = dicipline_info.warning_id
                                   where dicipline_info.disiplin_id ='" & e.CommandArgument & "'"
            Dim letter As String = Server.HtmlDecode(oCommon.getFieldValue(query))

            letter = letter.Replace("{NAME}", find_Name)
            letter = letter.Replace("{IC}", find_Mykad)
            letter = letter.Replace("{ID}", find_ID)
            letter = letter.Replace("{CLASS}", find_class)

            Dim letterName As String = String.Format("{0}_{1}", Date.Today.ToShortDateString, wlListStudentName.Text)
            Try
                If letter.Length > 0 Then
                    Dim strWriter As New StringWriter
                    Dim htmlWriter As New HtmlTextWriter(strWriter)
                    Dim sr As New StringReader(letter)
                    Dim pdfDoc As New Document(PageSize.A4)
                    Dim writer As PdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream)
                    pdfDoc.Open()
                    XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr)
                    pdfDoc.Close()
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("content-disposition", "attachment;filename=" + letterName + ".pdf")
                    Response.Cache.SetCacheability(HttpCacheability.NoCache)
                    Response.Write(pdfDoc)
                    Response.End()
                Else
                    ShowMessage("Warning letter not exist OR counseling session not yet completed", MessageType.Error)
                End If
            Catch ex As Exception

            End Try

        End If

    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

    Protected Sub datRespondent_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
End Class