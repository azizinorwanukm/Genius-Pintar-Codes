Imports System.Web
Imports System.Web.Services
Imports System
Imports System.Configuration
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class ShowImage
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim strstudentid As String
        Try
            If Not context.Request.QueryString("studentid") Is Nothing Then
                strstudentid = context.Request.QueryString("studentid")
            Else
                Throw New ArgumentException("No parameter specified")
            End If

            context.Response.ContentType = "image/jpeg"
            Dim strm As Stream = ShowEmpImage(strstudentid)
            Dim buffer As Byte() = New Byte(4095) {}
            Dim byteSeq As Integer = strm.Read(buffer, 0, 4096)

            Do While byteSeq > 0
                context.Response.OutputStream.Write(buffer, 0, byteSeq)
                byteSeq = strm.Read(buffer, 0, 4096)
            Loop
            'context.Response.BinaryWrite(buffer);
        Catch ex As Exception

        End Try

    End Sub

    Public Function ShowEmpImage(ByVal strstudentid As String) As Stream
        Dim conn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim connection As SqlConnection = New SqlConnection(conn)
        Dim sql As String = "SELECT top 1 SmallPhoto FROM StudentPhoto WHERE StudentID = @StudentID"
        Dim cmd As SqlCommand = New SqlCommand(sql, connection)

        Try
            cmd.CommandType = CommandType.Text
            cmd.Parameters.AddWithValue("@StudentID", strstudentid)
            connection.Open()

            Dim img As Object = cmd.ExecuteScalar()
            Return New MemoryStream(CType(img, Byte()))
        Catch
            Return Nothing
        Finally
            connection.Close()
        End Try
    End Function


    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class