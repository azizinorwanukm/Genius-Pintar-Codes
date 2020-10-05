Imports System.IO

Public Class ImageVB

    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("exam_id") IsNot Nothing Then

            Try
                ' Read the file and convert it to Byte Array
                Dim filePath As String = ConfigurationManager.AppSettings("FolderPath")

                Dim filename As String = oCommon.getFieldValue("SELECT imgurl FROM QUESTIONS WHERE exam_id =" & Request.QueryString("exam_id") & " AND quest_no=" & Request.QueryString("question"))

                Dim contenttype As String = "image/" & Path.GetExtension(filename).Replace(".", "")

                Dim fs As FileStream = New FileStream(filePath & filename, FileMode.Open, FileAccess.Read)
                Dim br As BinaryReader = New BinaryReader(fs)
                Dim bytes As Byte() = br.ReadBytes(Convert.ToInt32(fs.Length))
                br.Close()
                fs.Close()

                'Write the file to Reponse
                Response.Buffer = True
                Response.Charset = ""
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Response.ContentType = contenttype
                Response.AddHeader("content-disposition", "attachment;filename=" & filename)
                Response.BinaryWrite(bytes)
                Response.Flush()
                Response.End()
            Catch

            End Try

        End If
    End Sub

End Class