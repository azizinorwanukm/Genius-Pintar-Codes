Imports System.Data.SqlClient

Public Class admin_kelayakan_Asas1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("connectionUkm")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Try
            If Not IsPostBack Then
                setDdlSession()

                lblMsg.Text = ""
                lblMsgTop.Text = ""
                strRet = BindData(datRespondent)

            End If
        Catch ex As Exception

        End Try

    End Sub
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 1200

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Function getSQL() As String
        Dim classStudent As String = Request.QueryString("ClassCode")
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT B.id,A.student_Name,A.student_Mykad ,C.AlumniID,Y.ClassCode as classcode,
                    B.marks_100,K.ScorePercentage as meqi ,E.TotalMark as ukm2Mark,CAST(I.Mental_Age_Year As decimal(6,2)) as mentalAge,
                    B.markPretest as pretest,B.markPosttest as posttest,(B.markPosttest-B.markPretest) as difference,
                    I.TotalPercentage as ukm2TotalPercentage,
                    convert(decimal(5,2),round(H.instruktorExam_marks*100/175,2)) marks_ppcs,CONCAT (H.isSokong,space(3),'|',space(3),H.instruktorExam_Komen) as komen_ppcs,
                    convert(decimal(5,2),round(F.instruktorExam_marks*100/150,2)) marks_raPcs, CONCAT (F.isSokong   ,space(3),'|',space(3),F.instruktorExam_Komen) as raPcs_komen,
                    convert(decimal(5,2),round(G.instruktorExam_marks*100/140,2)) marks_kpp,CONCAT (G.isSokong_kpp ,space(3),'|',space(3),G.instruktorExam_Komen_kpp) as kpp_komen,
                    B.isLayak as Layak
                    from ukm3.dbo.ukm3 B
                    LEFT JOIN ukm3.dbo.student_info A on B.student_id=A.std_ID
                    LEFT JOIN permatapintar.dbo.StudentProfile C on C.MYKAD=A.student_mykad
                    LEFT JOIN permatapintar.dbo.EQTest D on D.StudentID=C.StudentID and D.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'
                    LEFT JOIN (select AA.StudentID, AA.TotalMark from permatapintar.dbo.PPCS_Eval_Daily AA join (select StudentID, max(LastUpdate) LastUpdate from permatapintar.dbo.PPCS_Eval_Daily WHERE PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "' GROUP BY StudentID) BB on AA.StudentID=BB.StudentID AND AA.LastUpdate = BB.LastUpdate) E on E.StudentID=C.StudentID
                    LEFT JOIN ukm3.dbo.instruktorExam_result H on B.id =H.ukm3id
                    LEFT JOIN ukm3.dbo.instruktorExam_result_raPcs F on B.id = F.ukm3id
                    LEFT JOIN ukm3.dbo.instruktorExam_result_kpp G on B.id=G.ukm3id                  
                    LEFT JOIN (select  AA.StudentID,AA.Mental_Age_Year from permatapintar.dbo.ukm2 AA join(select  CC.StudentID, CC.ExamYear from permatapintar.dbo.ukm2 CC WHERE ExamYear ='" & Now.Year() & "')BB on AA.StudentID=BB.StudentID and AA.ExamYear =BB.ExamYear) I on I.StudentID = C.StudentID
                    LEFT JOIN permatapintar.dbo.PPCS J on A.guid = J.StudentID AND J.PPCSDate = '" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'
                    
					LEFT JOIN permatapintar.dbo.EQTest K on A.guid  = K.StudentID and k.PPCSDate ='" & Commonfunction.getPpcsDate(ddlSession.SelectedValue) & "'
					
					LEFT JOIN permatapintar.dbo.PPCS_Class z on z.ClassCode = J.PPCSClass 
					LEFT JOIN permatapintar.dbo.PPCS_Course x ON J.CourseID=x.CourseID
					LEFT JOIN permatapintar.dbo.PPCS_Class y on J.ClassID = y.ClassID 
                    "
        strWhere += " WHERE B.active = 1 AND B.session_id = '" & ddlSession.SelectedValue & "' AND J.ClassID = '" & classStudent & "'"


        ''search
        If Not txtsearch.Text.Length = 0 Then
            strWhere += " And (A.student_name Like '%" & txtsearch.Text & "%'"
            strWhere += " OR A.student_Mykad LIKE '%" & txtsearch.Text & "%')"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function


    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Dim encryptid As String = strKeyID
        Try
            Select Case getUserProfile_UserType()
                Case "Admin"
                    Response.Redirect("ukm3_ppcs.updatemarkah.aspx?studentid=" & encryptid)

                Case Else
                    lblMsg.Text = "Invalid user type!"
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Function getUserProfile_UserType() As String
        strSQL = "SELECT top 1 staff_position FROM staff_info WHERE staff_login='" & CType(Session.Item("permata_admin"), String) & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Return strRet
    End Function

    Private Sub ExportToCSV()
        'Get the data from database into datatable 
        Dim strQuery As String = getSQL()
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=FileExport.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

    End Function

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        strRet = BindData(datRespondent)
    End Sub

    Private Sub setDdlSession()
        Dim attachmentsTable = New DataTable

        Dim mAdapter As New SqlDataAdapter

        Dim query As String = "SELECT id, sessionName FROM UKM3Session ORDER BY id DESC"

        Dim strconn As String = ConfigurationManager.AppSettings("connectionUkm")

        Using mConn As New SqlConnection(strconn)
            Using mCmd As New SqlCommand(query, mConn)
                mConn.Open()
                mAdapter.SelectCommand = mCmd
                mAdapter.Fill(attachmentsTable)
            End Using
        End Using

        Dim quantity As Integer = attachmentsTable.Rows.Count

        For k = 0 To quantity - 1
            ddlSession.Items.Add(New ListItem(attachmentsTable.Rows(k).Item(1).ToString, attachmentsTable.Rows(k).Item(0).ToString))
        Next

        Dim currentSession As String = oCommon.getFieldValue("SELECT parameter FROM general_config WHERE config = 'currentSession'")

        ddlSession.SelectedValue = currentSession

    End Sub

    Private Sub datRespondent_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles datRespondent.RowDataBound
        Try
            Dim i As Integer
            For i = 0 To datRespondent.Rows.Count - 1 Step i + 1
                Dim stem As Label = CType(datRespondent.Rows(i).FindControl("lbl_stem"), Label)
                Dim eq As Label = CType(datRespondent.Rows(i).FindControl("lbl_meqi"), Label)
                Dim insKPP As Label = CType(datRespondent.Rows(i).FindControl("instrKPP"), Label)
                Dim insPPCS As Label = CType(datRespondent.Rows(i).FindControl("instrPPCS"), Label)
                Dim raPPCS As Label = CType(datRespondent.Rows(i).FindControl("instrRaPPCS"), Label)
                Dim ukm2 As Label = CType(datRespondent.Rows(i).FindControl("lbl_ukm2percentage"), Label)
                Dim postTest As Label = CType(datRespondent.Rows(i).FindControl("txt_postTest"), Label)
                Dim lbl_compoMark As Label = CType(datRespondent.Rows(i).FindControl("lbl_compoMark"), Label)

                Dim A = Decimal.Parse(stem.Text) * oCommon.getFieldValue("Select stem from ukm3_compoMark where isActive = '1'")

                Dim B = Decimal.Parse(eq.Text) * (oCommon.getFieldValue("Select eq from ukm3_compoMark where isActive = '1'"))

                Dim C = Decimal.Parse(insKPP.Text) * (oCommon.getFieldValue("Select insKPP from ukm3_compoMark where isActive = '1'"))

                Dim D = Decimal.Parse(insPPCS.Text) * (oCommon.getFieldValue("Select insPPCS from ukm3_compoMark where isActive = '1'"))
                Dim EE = Decimal.Parse(raPPCS.Text) * (oCommon.getFieldValue("Select raPPCS from ukm3_compoMark where isActive = '1'"))
                Dim F = Decimal.Parse(ukm2.Text) * (oCommon.getFieldValue("Select ukm2 from ukm3_compoMark where isActive = '1'"))
                Dim G = Decimal.Parse(postTest.Text) * (oCommon.getFieldValue("Select postTest from ukm3_compoMark where isActive = '1'"))

                Dim compoMark As String = A + B + C + D + EE + F + G

                lbl_compoMark.Text = compoMark
            Next

        Catch ex As Exception
            lblMsgTop.Text = ex.ToString
        End Try
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("admin.asas.classSelect.aspx")
    End Sub
End Class
