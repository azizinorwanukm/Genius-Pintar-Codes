Public Class pelajar_koko_pencapaian_sample
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtPencapaian.Text += "1.	Participated in ICAS Mathematics ( 2013 )" & vbCrLf
            txtPencapaian.Text += "2.	9th place In Purple Comet Math Competition Malaysia ( 2014 )" & vbCrLf
            txtPencapaian.Text += "3.	Silver Medallist in Future Scientist Conference ( 2014 )" & vbCrLf
            txtPencapaian.Text += "4.	Bronze Medallist In Softball MSSD Hulu Langat Competition ( 2014 )" & vbCrLf
            txtPencapaian.Text += "5.	Bronze Medallist in Ping Pong MSSD Hulu Langat Competition ( 2014 )" & vbCrLf
            txtPencapaian.Text += "6.	Participated In Basketball MSSD Hulu Langat Competition ( 2014 )" & vbCrLf
            txtPencapaian.Text += "7.	Member of Majlis Tertinggi Kolej 2014 ( Sports Exco )" & vbCrLf
            txtPencapaian.Text += "8.	President Of Tennis Club ( 2014 )" & vbCrLf
            txtPencapaian.Text += "9.	Police Cadet ( 2013 , 2014 )" & vbCrLf
            txtPencapaian.Text += "10.	Photographer Of Robotic Club ( 2013 )" & vbCrLf
            txtPencapaian.Text += "11.	Member of Nature Club ( 2014 )" & vbCrLf
        Catch ex As Exception

        End Try
    End Sub

End Class