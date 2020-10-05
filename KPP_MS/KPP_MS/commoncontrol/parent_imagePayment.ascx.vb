Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Cryptography
Imports System.Configuration
Imports System.Data
Imports System.Drawing
Imports System.Web.UI
Imports System.Web.UI.WebControls

Public Class parent_imagePayment
    Inherits System.Web.UI.UserControl
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                student_year()
                load_page_year()
                strRet = BindData(datRespondent)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub student_year()
        strSQL = "SELECT distinct year from payment_image where std_ID = '" & Request.QueryString("std_ID") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlyear.DataSource = ds
            ddlyear.DataTextField = "year"
            ddlyear.DataValueField = "year"
            ddlyear.DataBind()
            ddlyear.Items.Insert(0, New ListItem("Select Year", ""))
            ddlyear.SelectedIndex = 0

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub load_page_year()
        strSQL = "SELECT distinct year from payment_image where year ='" & Now.Year & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim nRows As Integer = 0
        Dim nCount As Integer = 1
        Dim MyTable As DataTable = New DataTable
        MyTable = ds.Tables(0)
        If MyTable.Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("year")) Then
                ddlyear.SelectedValue = ds.Tables(0).Rows(0).Item("year")
                student_Photo.ImageUrl = "~/img/Box_Crate_With_Upwards_Arrow-512.png"
            Else
                ddlyear.SelectedValue = ""
                student_Photo.ImageUrl = "~/img/Box_Crate_With_Upwards_Arrow-512.png"
            End If
        End If
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception

            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        'A left outer join will give all rows in A, plus any common rows in B.

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " order by ID ASC"

        tmpSQL = "select * from payment_image"
        strWhere = " WHERE ID IS NOT NULL"
        strWhere += " and std_ID = '" & Request.QueryString("std_ID") & "'"

        If Not ddlyear.SelectedValue = 0 Or ddlyear.SelectedValue = "" Then
            strWhere += " and year = '" & ddlyear.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrderby

        Return getSQL
    End Function

    Private Sub btnUpdate_ServerClick(sender As Object, e As EventArgs) Handles btnUpdate.ServerClick
        Dim errorCount As Integer

        If uploadPhoto.PostedFile.FileName <> "" Then

            Dim filename As String = Path.GetFileName(uploadPhoto.PostedFile.FileName)
            ''sets the image path
            Dim imgPath As String = "~/payment_Image/" + filename
            ''then save it to the Folder
            uploadPhoto.SaveAs(Server.MapPath(imgPath))

            '' get date
            Dim regDate As Date = Date.Now()
            Dim strDate As String = regDate.ToString("MM\/dd\/yyyy")

            Dim userName As String = "Select parent_Name from parent_Info where parent_ID = '" & Request.QueryString("parent_ID") & "'"
            Dim data_userName As String = oCommon.getFieldValue(userName)

            Dim std_Name As String = "Select student_Name from student_info where std_ID = '" & Request.QueryString("std_ID") & "'"
            Dim data_stdName As String = oCommon.getFieldValue(std_Name)

            'INSERT PAYMENT RECEIPT
            Using STDDATA As New SqlCommand("INSERT INTO payment_image(std_ID,student_Name,date,log_user,year,photo) values 
                                             ('" & Request.QueryString("std_ID") & "','" & data_stdName & "','" & strDate & "','" & data_userName & "',
                                             '" & Now.Year & "','" & imgPath & "')", objConn)
                objConn.Open()
                Dim i = STDDATA.ExecuteNonQuery()
                objConn.Close()
                If i <> 0 Then
                    errorCount = 0
                Else
                    errorCount = 1
                End If
            End Using

            If errorCount = 1 Then
                Response.Redirect("penjaga_bayaran.aspx?result=-1&std_ID=" + Request.QueryString("std_ID") + "&parent_ID=" + Request.QueryString("parent_ID"))
            ElseIf errorCount = 0 Then
                Response.Redirect("penjaga_bayaran.aspx?result=1&std_ID=" + Request.QueryString("std_ID") + "&parent_ID=" + Request.QueryString("parent_ID"))
            End If

        End If
    End Sub

    Private Sub datRespondent_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles datRespondent.RowEditing
        Try
            Dim strKeyID As String = datRespondent.DataKeys(e.NewEditIndex).Value.ToString

            strSQL = "SELECT photo from payment_image where ID ='" & strKeyID & "'"

            Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
            Dim objConn As SqlConnection = New SqlConnection(strConn)
            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("photo")) Then
                    image1.ImageUrl = ds.Tables(0).Rows(0).Item("photo")
                    HiddenField1.Value = "1"
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub datRespondent_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
End Class
