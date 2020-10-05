Public Class upsi
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            If IsNothing(Session("UserICNo")) Then Response.Redirect("~/default.aspx")

            lblName.InnerText = Session("UserName").ToString()

            Dim Path() As String = Request.Url.OriginalString.Split("/")

            UpdateLabelMaster(Session("Language").ToString)
            UpdateLabelContent(Path(Path.Length - 1), Session("Language").ToString)

            If Session("RoleId").ToString <> "1" Then
                module_list.Visible = False
                about.Visible = False
                report.Visible = False
            End If
            
            If Session("RoleId").ToString <> "1" Then

                'hide timer countdown
                Dim btn As HtmlButton = DirectCast(ContentPlaceHolder1.FindControl("timer_progress"), HtmlButton)
                If Not IsNothing(btn) Then
                    btn.Attributes.Add("class", "invisible")
                End If

            End If

            about.InnerHtml = Session("ZeroMark").ToString

        End If

    End Sub

    Private Sub UpdateLabelContent(PageName As String, LanguageId As String)

        Dim dt As DataTable = modFunction.GetLabel(PageName, LanguageId)
        Dim lbl As Label
        Dim btn As Button
        Dim html As HtmlGenericControl
        Dim hf As HiddenField
        Dim hbtn As HtmlButton
        Dim hlink As HyperLink
        Dim txt As TextBox

        For Each drow As DataRow In dt.Rows

            Select Case drow("ControlType").ToString

                Case "Label"
                    lbl = DirectCast(ContentPlaceHolder1.FindControl(drow("ControlName").ToString), Label)
                    If Not IsNothing(lbl) Then
                        lbl.Text = drow("Value").ToString
                    End If

                Case "Button"
                    btn = DirectCast(ContentPlaceHolder1.FindControl(drow("ControlName").ToString), Button)
                    If Not IsNothing(btn) Then
                        btn.Text = drow("Value").ToString
                    End If

                Case "Html"
                    html = DirectCast(ContentPlaceHolder1.FindControl(drow("ControlName").ToString), HtmlGenericControl)
                    If Not IsNothing(html) Then
                        html.InnerHtml = drow("Value").ToString
                    End If

                Case "Hidden"
                    hf = DirectCast(ContentPlaceHolder1.FindControl(drow("ControlName").ToString), HiddenField)
                    If Not IsNothing(hf) Then
                        hf.Value = drow("Value").ToString
                    End If

                Case "HtmlButton"
                    hbtn = DirectCast(ContentPlaceHolder1.FindControl(drow("ControlName").ToString), HtmlButton)
                    If Not IsNothing(hbtn) Then
                        hbtn.InnerText = drow("Value").ToString
                    End If

                Case "Hyperlink"
                    hlink = DirectCast(ContentPlaceHolder1.FindControl(drow("ControlName").ToString), HyperLink)
                    If Not IsNothing(hlink) Then
                        hlink.Text = drow("Value").ToString
                    End If

                Case "Textbox"
                    txt = DirectCast(ContentPlaceHolder1.FindControl(drow("ControlName").ToString), TextBox)
                    If Not IsNothing(txt) Then
                        txt.Text = drow("Value").ToString
                    End If

            End Select

        Next

    End Sub

    Private Sub UpdateLabelMaster(LanguageId As String)

        Dim dt As DataTable = modFunction.GetLabel("masterpage", LanguageId)
        Dim lbl As Label
        Dim btn As Button
        Dim html As HtmlGenericControl
        Dim hf As HiddenField
        Dim achor As HtmlAnchor
        Dim txt As TextBox

        For Each drow As DataRow In dt.Rows

            Select Case drow("ControlType").ToString

                Case "Label"
                    lbl = DirectCast(FindControl(drow("ControlName").ToString), Label)
                    If Not IsNothing(lbl) Then
                        lbl.Text = drow("Value").ToString
                    End If

                Case "Button"
                    btn = DirectCast(FindControl(drow("ControlName").ToString), Button)
                    If Not IsNothing(btn) Then
                        btn.Text = drow("Value").ToString
                    End If

                Case "Html"
                    html = DirectCast(ContentPlaceHolder1.FindControl(drow("ControlName").ToString), HtmlGenericControl)
                    If Not IsNothing(html) Then
                        html.InnerHtml = drow("Value").ToString
                    End If

                Case "Achor"
                    achor = DirectCast(FindControl(drow("ControlName").ToString), HtmlAnchor)
                    If Not IsNothing(achor) Then
                        achor.InnerHtml = drow("Value").ToString
                    End If

                Case "Hidden"
                    hf = DirectCast(FindControl(drow("ControlName").ToString), HiddenField)
                    If Not IsNothing(hf) Then
                        hf.Value = drow("Value").ToString
                    End If

                Case "Textbox"
                    txt = DirectCast(FindControl(drow("ControlName").ToString), TextBox)
                    If Not IsNothing(txt) Then
                        txt.Text = drow("Value").ToString
                    End If

            End Select

        Next

    End Sub

    Protected Sub LinkButton_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click, LinkButton2.Click, LinkButton3.Click, LinkButton4.Click, LinkButton5.Click, LinkButton6.Click, LinkButton7.Click, LinkButton8.Click, LinkButton9.Click, LinkButton10.Click

        Dim lnk As LinkButton = DirectCast(sender, LinkButton)
        Dim NextQuestion As String = ""
        Dim NextQuestionInterval As Integer = 0

        Select Case lnk.CommandArgument

            Case "1"
                NextQuestionInterval = 30
                NextQuestion = "~/mod01/upsi.mod01.01.aspx"
            Case "2"
                NextQuestionInterval = 0
                NextQuestion = "~/mod02/upsi.mod02.01.aspx"
            Case "3"
                NextQuestionInterval = 0
                NextQuestion = "~/mod03/upsi.mod03.01.aspx"
            Case "4"
                NextQuestionInterval = 120
                NextQuestion = "~/mod04/upsi.mod04.01.aspx"
            Case "5"
                NextQuestionInterval = 3
                NextQuestion = "~/mod05/upsi.mod05.01.aspx"
            Case "6"
                NextQuestionInterval = 0
                NextQuestion = "~/mod06/upsi.mod06.00.aspx"
            Case "7"
                NextQuestionInterval = 0
                NextQuestion = "~/mod07/upsi.mod07.01.aspx"
            Case "8"
                NextQuestionInterval = 20
                NextQuestion = "~/mod08/upsi.mod08.01.aspx"
            Case "9"
                NextQuestionInterval = 5
                NextQuestion = "~/mod09/upsi.mod09.01.aspx"
            Case "10"
                NextQuestionInterval = 90
                NextQuestion = "~/mod10/upsi.mod10.01.aspx"

        End Select

        Session("TimeLeft") = NextQuestionInterval
        Session("UserPage") = NextQuestion
        Response.Redirect(NextQuestion)

    End Sub

End Class