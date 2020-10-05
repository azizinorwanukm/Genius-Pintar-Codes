'Dont forget it!
Imports OpenFlashChart

Public Class radar
    Inherits System.Web.UI.Page

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Chart As New OpenFlashChart.OpenFlashChart 'My chart I will work on.
        Dim data1 As New List(Of Double) 'The list of my values
        Dim label1 As New List(Of String) 'The list of my SpokeLabels
        Dim area As New Area 'An area is as display of values in a radar
        Dim radar As New RadarAxis(7) 'The kind of chart, Radar, we use. Note : the constructor need how many sectors
        Dim sp_labels As New XAxisLabels() 'We define the XAxis or in our case, the SpokeLabels

        Dim strDomain01 As String = "0"
        Dim strDomain02 As String = "0"
        Dim strDomain03 As String = "0"
        Dim strDomain04 As String = "0"
        Dim strDomain05 As String = "0"
        Dim strDomain06 As String = "0"
        Dim strDomain07 As String = "0"

        '--SelfAwareness,SelfRegulation,SelfMotivation,Empathy,SocialSkill,Spirituality,Maturity
        '--get domain value
        strSQL = "SELECT SelfAwareness FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
        strDomain01 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SelfRegulation FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
        strDomain02 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SelfMotivation FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
        strDomain03 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Empathy FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
        strDomain04 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT SocialSkill FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
        strDomain05 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Spirituality FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
        strDomain06 = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Maturity FROM EQTest WHERE LoginID='" & Request.QueryString("loginid") & "' AND surveyid='" & Request.QueryString("surveyid") & "'"
        strDomain07 = oCommon.getFieldValue(strSQL)

        Try
            'This is where you place the values inside a list of double
            'You should add values here dynamically...
            data1.Add(CDbl(strDomain01))
            data1.Add(CDbl(strDomain02))
            data1.Add(CDbl(strDomain03))
            data1.Add(CDbl(strDomain04))
            data1.Add(CDbl(strDomain05))
            data1.Add(CDbl(strDomain06))
            data1.Add(CDbl(strDomain07))

            'We define the SpokeLabels (the labels on the tips of each lines)
            'You should add values here dynamically...
            '--SelfAwareness,SelfRegulation,SelfMotivation,Empathy,SocialSkill,Spirituality,Maturity
            label1.Add("SelfAwareness")
            label1.Add("SelfRegulation")
            label1.Add("SelfMotivation")
            label1.Add("Empathy")
            label1.Add("SocialSkill")
            label1.Add("Spirituality")
            label1.Add("Maturity")

            'We define our area. In this case, we only have one area but simply 
            'define another and add it to the chart with different values.
            With area
                .Values = data1
                .Width = 1
                .DotSize = 1
                .FillColor = "#cccccc"
                .Colour = "#fe0"
                .Loop = True
            End With

            'We add our values in our chart here
            Chart.AddElement(area)

            'We define our SpokeLabels here
            sp_labels.SetLabels(label1)

            'We define how the radar will look like...
            radar.Steps = 1
            radar.SetRange(0, 100)
            radar.SpokeLabels = sp_labels

            'Now we put the finishing touch for the chart. We define the radar
            'and we place some properties for the chart itself...
            With Chart
                .Radar_Axis = radar
                .Title = New Title("PERMATAPINTAR EMOTIONAL QUOTIENT TEST.")
                .Title.Style = "radar_title"
                .Bgcolor = "#ffffff"
                .Tooltip = New ToolTip("#val#")
                .Tooltip.Shadow = True
                .Tooltip.Colour = "#e43456"
                .Tooltip.MouseStyle = ToolTipStyle.CLOSEST
            End With

            'Now, we must translate our properties in a JSON style with the command
            'ToPrettyString and our data will be bind inside our radar Chart
            With Response
                .Clear()
                .CacheControl = "no-cache"
                .Write(Chart.ToPrettyString())
                .End()
            End With
        Catch ex As Exception
            lblMsg.Text = "err:" & ex.Message
        End Try



    End Sub

End Class