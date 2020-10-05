Public Partial Class laporan_keseluruhan_view10
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Set Button2 to open sized window
            imgPrint.ToolTip = "Cetak"
            OpenPopUp(imgPrint, "laporan.keseluruhan.print.aspx?studentid=" & Request.QueryString("studentid") & "&year=" & Request.QueryString("year"), "", 850, 650)

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub OpenPopUp(ByVal opener As System.Web.UI.WebControls.WebControl, ByVal PagePath As String, ByVal windowName As String, ByVal width As Integer, ByVal height As Integer)
        Dim clientScript As String
        Dim windowAttribs As String

        'Building Client side window attributes with width and height.
        'Also the the window will be positioned to the middle of the screen
        windowAttribs = "width=" & width & "px," & _
                        "height=" & height & "px," & _
                        "left='+((screen.width -" & width & ") / 2)+'," & _
                        "top='+ (screen.height - " & height & ") / 2+'"


        'Building the client script- window.open, with additional parameters
        clientScript = "window.open('" & PagePath & "','" & windowName & "','" & windowAttribs & "');return false;"
        'regiter the script to the clientside click event of the 'opener' control
        opener.Attributes.Add("onClick", clientScript)
    End Sub

    
End Class