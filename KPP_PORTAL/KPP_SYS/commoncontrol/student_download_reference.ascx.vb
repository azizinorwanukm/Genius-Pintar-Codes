Imports System.Data.SqlClient
Imports System.IO
Imports System.Data.OleDb

Public Class student_download_reference
    Inherits System.Web.UI.UserControl

    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim result As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim oCommon As New Commonfunction

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                Dim getStatus As String = Request.QueryString("status")

                If getStatus = "DR" Then ''Download Reference

                    txtbreadcrum1.Text = "Download Reference"
                    DownloadReference.Visible = True
                    UploadReference.Visible = False
                    ViewInformation.Visible = False

                    btnDownloadReferences.Attributes("class") = "btn btn-info"
                    btnUploadReferences.Attributes("class") = "btn btn-default font"
                    btnViewInformation.Attributes("class") = "btn btn-default font"

                    CheckLevel_Function()

                ElseIf getStatus = "UR" Then ''Upload Reference

                    txtbreadcrum1.Text = "Upload Reference"
                    DownloadReference.Visible = False
                    UploadReference.Visible = True
                    ViewInformation.Visible = False

                    R1.Visible = False
                    R2.Visible = False
                    R3.Visible = False
                    R4.Visible = False
                    R5.Visible = False
                    R6.Visible = False
                    R7.Visible = False

                    F1_R1.Visible = False
                    F1_R2.Visible = False
                    F1_R3.Visible = False
                    F1_R4.Visible = False
                    F1_R5.Visible = False
                    F1_R6.Visible = False
                    F1_R7.Visible = False

                    btnDownloadReferences.Attributes("class") = "btn btn-default font"
                    btnUploadReferences.Attributes("class") = "btn btn-info"
                    btnViewInformation.Attributes("class") = "btn btn-default font"

                ElseIf getStatus = "VI" Then ''View Information

                    txtbreadcrum1.Text = "View Information"
                    DownloadReference.Visible = False
                    UploadReference.Visible = False
                    ViewInformation.Visible = True

                    R1.Visible = False
                    R2.Visible = False
                    R3.Visible = False
                    R4.Visible = False
                    R5.Visible = False
                    R6.Visible = False
                    R7.Visible = False

                    btnDownloadReferences.Attributes("class") = "btn btn-default font"
                    btnUploadReferences.Attributes("class") = "btn btn-default font"
                    btnViewInformation.Attributes("class") = "btn btn-info"

                    Year_Info_list()
                    strRet = BindData(datRespondent)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnDownloadReferences_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadReferences.ServerClick
        Response.Redirect("pelajar_muat_turun_dokumen.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=DR")
    End Sub

    Private Sub btnUploadReferences_ServerClick(sender As Object, e As EventArgs) Handles btnUploadReferences.ServerClick
        Response.Redirect("pelajar_muat_turun_dokumen.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=UR")
    End Sub

    Private Sub btnViewInformation_ServerClick(sender As Object, e As EventArgs) Handles btnViewInformation.ServerClick
        Response.Redirect("pelajar_muat_turun_dokumen.aspx?std_ID=" + Request.QueryString("std_ID") + "&status=VI")
    End Sub

    Private Sub CheckLevel_Function()
        Dim check_ES As String = "select FDES_id from fee_documentList_ES where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and FDES_Year = '" & Now.Year & "'"
        Dim get_ES As String = oCommon.getFieldValue(check_ES)

        Dim find_LA As String = ""
        Dim get_LA As String = ""
        Dim find_LA1 As String = ""
        Dim get_LA1 As String = ""
        Dim find_LA2 As String = ""
        Dim get_LA2 As String = ""
        Dim find_LA3 As String = ""
        Dim get_LA3 As String = ""
        Dim find_LB As String = ""
        Dim get_LB As String = ""
        Dim find_LB1 As String = ""
        Dim get_LB1 As String = ""
        Dim find_LC As String = ""
        Dim get_LC As String = ""

        Dim find_F1_LA As String = ""
        Dim get_F1_LA As String = ""
        Dim find_F1_LB As String = ""
        Dim get_F1_LB As String = ""
        Dim find_F1_LC As String = ""
        Dim get_F1_LC As String = ""
        Dim find_F1_LD As String = ""
        Dim get_F1_LD As String = ""
        Dim find_F1_LD1 As String = ""
        Dim get_F1_LD1 As String = ""
        Dim find_F1_LE As String = ""
        Dim get_F1_LE As String = ""
        Dim find_F1_LE1 As String = ""
        Dim get_F1_LE1 As String = ""
        Dim find_F1_LE2 As String = ""
        Dim get_F1_LE2 As String = ""
        Dim find_F1_LE3 As String = ""
        Dim get_F1_LE3 As String = ""
        Dim find_F1_LF As String = ""
        Dim get_F1_LF As String = ""
        Dim find_F1_LF1 As String = ""
        Dim get_F1_LF1 As String = ""
        Dim find_F1_LSS As String = ""
        Dim get_F1_LSS As String = ""

        If get_ES.Length > 0 Then

            strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
            strRet = oCommon.getFieldValue(strSQL)

            If strRet.Length > 0 Then

                LA.Visible = True
                LA1.Visible = True
                LA2.Visible = True
                LA3.Visible = True
                LB.Visible = True
                LB1.Visible = True
                LC.Visible = True

                F1_LA.Visible = False
                F1_LB.Visible = False
                F1_LC.Visible = False
                F1_LD.Visible = False
                F1_LD1.Visible = False
                F1_LE.Visible = False
                F1_LE1.Visible = False
                F1_LE2.Visible = False
                F1_LE3.Visible = False
                F1_LF.Visible = False
                F1_LF1.Visible = False
                F1_LSS.Visible = False

                find_LA = "Select FDES_Name from fee_documentList_ES where FDES_Year = '" & Now.Year & "' and std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and FDES_Ref = 'LAMPIRAN A'"
                get_LA = oCommon.getFieldValue(find_LA)

                find_LA1 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A1'"
                get_LA1 = oCommon.getFieldValue(find_LA1)

                find_LA2 = "Select FDES_Name from fee_documentList_ES where FDES_Year = '" & Now.Year & "' and std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and FDES_Ref = 'LAMPIRAN A2'"
                get_LA2 = oCommon.getFieldValue(find_LA2)

                find_LA3 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A3'"
                get_LA3 = oCommon.getFieldValue(find_LA3)

                find_LB = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN B'"
                get_LB = oCommon.getFieldValue(find_LB)

                find_LB1 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN B1'"
                get_LB1 = oCommon.getFieldValue(find_LB1)

                find_LC = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN C'"
                get_LC = oCommon.getFieldValue(find_LC)
            End If

        Else
            strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
            strRet = oCommon.getFieldValue(strSQL)

            If strRet.Length > 0 And strRet <> "Foundation 1" Then

                LA.Visible = True
                LA1.Visible = True
                LA2.Visible = True
                LA3.Visible = True
                LB.Visible = True
                LB1.Visible = True
                LC.Visible = True

                F1_LA.Visible = False
                F1_LB.Visible = False
                F1_LC.Visible = False
                F1_LD.Visible = False
                F1_LD1.Visible = False
                F1_LE.Visible = False
                F1_LE1.Visible = False
                F1_LE2.Visible = False
                F1_LE3.Visible = False
                F1_LF.Visible = False
                F1_LF1.Visible = False
                F1_LSS.Visible = False


                find_LA = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A'"
                get_LA = oCommon.getFieldValue(find_LA)

                find_LA1 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A1'"
                get_LA1 = oCommon.getFieldValue(find_LA1)

                find_LA2 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A2'"
                get_LA2 = oCommon.getFieldValue(find_LA2)

                find_LA3 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A3'"
                get_LA3 = oCommon.getFieldValue(find_LA3)

                find_LB = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN B'"
                get_LB = oCommon.getFieldValue(find_LB)

                find_LB1 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN B1'"
                get_LB1 = oCommon.getFieldValue(find_LB1)

                find_LC = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN C'"
                get_LC = oCommon.getFieldValue(find_LC)

            ElseIf strRet.Length > 0 And strRet = "Foundation 1" Then

                LA.Visible = False
                LA1.Visible = False
                LA2.Visible = False
                LA3.Visible = False
                LB.Visible = False
                LB1.Visible = False
                LC.Visible = False

                F1_LA.Visible = True
                F1_LB.Visible = True
                F1_LC.Visible = True
                F1_LD.Visible = True
                F1_LD1.Visible = True
                F1_LE.Visible = True
                F1_LE1.Visible = True
                F1_LE2.Visible = True
                F1_LE3.Visible = True
                F1_LF.Visible = True
                F1_LF1.Visible = True
                F1_LSS.Visible = True

                find_F1_LA = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A'"
                get_F1_LA = oCommon.getFieldValue(find_F1_LA)

                find_F1_LB = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN B'"
                get_F1_LB = oCommon.getFieldValue(find_F1_LB)

                find_F1_LC = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN C'"
                get_F1_LC = oCommon.getFieldValue(find_F1_LC)

                find_F1_LD = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN D'"
                get_F1_LD = oCommon.getFieldValue(find_F1_LD)

                find_F1_LD1 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN D1'"
                get_F1_LD1 = oCommon.getFieldValue(find_F1_LD1)

                find_F1_LE = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN E'"
                get_F1_LE = oCommon.getFieldValue(find_F1_LE)

                find_F1_LE1 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN E1'"
                get_F1_LE1 = oCommon.getFieldValue(find_F1_LE1)

                find_F1_LE2 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN E2'"
                get_F1_LE2 = oCommon.getFieldValue(find_F1_LE2)

                find_F1_LE3 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN E3'"
                get_F1_LE3 = oCommon.getFieldValue(find_F1_LE3)

                find_F1_LF = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN F'"
                get_F1_LF = oCommon.getFieldValue(find_F1_LF)

                find_F1_LF1 = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN F1'"
                get_F1_LF1 = oCommon.getFieldValue(find_F1_LF1)

                find_F1_LSS = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'SENARAI SEMAK'"
                get_F1_LSS = oCommon.getFieldValue(find_F1_LSS)

            End If
        End If

        btnDownloadLA.InnerText = get_LA
        btnDownloadLA1.InnerText = get_LA1
        btnDownloadLA2.InnerText = get_LA2
        btnDownloadLA3.InnerText = get_LA3
        btnDownloadLB.InnerText = get_LB
        btnDownloadLB1.InnerText = get_LB1
        btnDownloadLC.InnerText = get_LC

        btnDownloadF1_LA.InnerText = get_F1_LA
        btnDownloadF1_LB.InnerText = get_F1_LB
        btnDownloadF1_LC.InnerText = get_F1_LC
        btnDownloadF1_LD.InnerText = get_F1_LD
        btnDownloadF1_LD1.InnerText = get_F1_LD1
        btnDownloadF1_LE.InnerText = get_F1_LE
        btnDownloadF1_LE1.InnerText = get_F1_LE1
        btnDownloadF1_LE2.InnerText = get_F1_LE2
        btnDownloadF1_LE3.InnerText = get_F1_LE3
        btnDownloadF1_LF.InnerText = get_F1_LF
        btnDownloadF1_LF1.InnerText = get_F1_LF1
        btnDownloadF1_LSS.InnerText = get_F1_LSS
    End Sub

    Private Sub btnDownloadLA_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadLA.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LA As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A'"
        Dim get_LA As String = oCommon.getFieldValue(find_LA)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LA & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LA & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadLA1_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadLA1.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LA1 As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A1'"
        Dim get_LA1 As String = oCommon.getFieldValue(find_LA1)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LA1 & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LA1 & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadLA2_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadLA2.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LA2 As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A2'"
        Dim get_LA2 As String = oCommon.getFieldValue(find_LA2)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LA2 & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LA2 & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadLA3_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadLA3.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LA3 As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A3'"
        Dim get_LA3 As String = oCommon.getFieldValue(find_LA3)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LA3 & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LA3 & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadLB_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadLB.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LB As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN B'"
        Dim get_LB As String = oCommon.getFieldValue(find_LB)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LB & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LB & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadLB1_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadLB1.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LB1 As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN B1'"
        Dim get_LB1 As String = oCommon.getFieldValue(find_LB1)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LB1 & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LB1 & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadLC_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadLC.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN C'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LA_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LA.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN A'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LB_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LB.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN B'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LC_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LC.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN C'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LD_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LD.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN D'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LD1_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LD1.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN D1'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LE_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LE.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN E'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LE1_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LE1.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN E1'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LE2_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LE2.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN E2'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LE3_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LE3.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN E3'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LF_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LF.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN F'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LF1_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LF1.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'LAMPIRAN F1'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub btnDownloadF1_LSS_ServerClick(sender As Object, e As EventArgs) Handles btnDownloadF1_LSS.ServerClick
        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Dim find_LC As String = "Select FD_Name from fee_documentList where FD_Year = '" & Now.Year & "' and FD_Level = '" & strRet & "' and FD_Ref = 'SENARAI SEMAK'"
        Dim get_LC As String = oCommon.getFieldValue(find_LC)

        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & get_LC & ".pdf")
        Response.TransmitFile(Server.MapPath("reference download/" & get_LC & ".pdf"))
        Response.End()
    End Sub

    Private Sub rbtn_FullPayment_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_FullPayment.CheckedChanged

        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet.Length > 0 And strRet <> "Foundation 1" Then
            R1.Visible = True
            R2.Visible = True
            R3.Visible = True
            R4.Visible = False
            R5.Visible = False
            R6.Visible = True
            R7.Visible = True

            F1_R1.Visible = False
            F1_R2.Visible = False
            F1_R3.Visible = False
            F1_R4.Visible = False
            F1_R5.Visible = False
            F1_R6.Visible = False
            F1_R7.Visible = False

        ElseIf strRet.Length > 0 And strRet = "Foundation 1" Then
            R1.Visible = False
            R2.Visible = False
            R3.Visible = False
            R4.Visible = False
            R5.Visible = False
            R6.Visible = False
            R7.Visible = False

            F1_R1.Visible = True
            F1_R2.Visible = True
            F1_R3.Visible = True
            F1_R4.Visible = False
            F1_R5.Visible = False
            F1_R6.Visible = True
            F1_R7.Visible = True

        End If
    End Sub

    Private Sub rbtn_Installment_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_Installment.CheckedChanged

        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet.Length > 0 And strRet <> "Foundation 1" Then
            R1.Visible = True
            R2.Visible = True
            R3.Visible = True
            R4.Visible = True
            R5.Visible = True
            R6.Visible = True
            R7.Visible = True

            F1_R1.Visible = False
            F1_R2.Visible = False
            F1_R3.Visible = False
            F1_R4.Visible = False
            F1_R5.Visible = False
            F1_R6.Visible = False
            F1_R7.Visible = False

        ElseIf strRet.Length > 0 And strRet = "Foundation 1" Then
            R1.Visible = False
            R2.Visible = False
            R3.Visible = False
            R4.Visible = False
            R5.Visible = False
            R6.Visible = False
            R7.Visible = False

            F1_R1.Visible = True
            F1_R2.Visible = True
            F1_R3.Visible = True
            F1_R4.Visible = True
            F1_R5.Visible = True
            F1_R6.Visible = True
            F1_R7.Visible = True

        End If
    End Sub

    Private Sub rbtn_Scholarship_CheckedChanged(sender As Object, e As EventArgs) Handles rbtn_Scholarship.CheckedChanged

        strSQL = "select MAX(student_level) from student_level where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "' and year = '" & Now.Year & "'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet.Length > 0 And strRet <> "Foundation 1" Then
            R1.Visible = False
            R2.Visible = False
            R3.Visible = True
            R4.Visible = False
            R5.Visible = False
            R6.Visible = True
            R7.Visible = True

            F1_R1.Visible = False
            F1_R2.Visible = False
            F1_R3.Visible = False
            F1_R4.Visible = False
            F1_R5.Visible = False
            F1_R6.Visible = False
            F1_R7.Visible = False

        ElseIf strRet.Length > 0 And strRet = "Foundation 1" Then
            R1.Visible = False
            R2.Visible = False
            R3.Visible = False
            R4.Visible = False
            R5.Visible = False
            R6.Visible = False
            R7.Visible = False

            F1_R1.Visible = False
            F1_R2.Visible = False
            F1_R3.Visible = True
            F1_R4.Visible = False
            F1_R5.Visible = False
            F1_R6.Visible = True
            F1_R7.Visible = True

        End If
    End Sub

    Private Sub btnUploadDocuments_ServerClick(sender As Object, e As EventArgs) Handles btnUploadDocuments.ServerClick
        Dim imgPath As String = ""

        Dim find_DateTime As String = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
        Dim find_Date As String = DateTime.Now.ToString("yyyyMMdd")

        Dim find_stdMykad As String = "Select student_Mykad from student_info where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        Dim get_stdMykad As String = oCommon.getFieldValue(find_stdMykad)

        Dim filename As String = ""


        If FileUploadImportRAF.HasFile Then
            If FileUploadImportRAF.PostedFile.ContentLength <> 0 And FileUploadImportRAF.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImportRAF.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImportRAF.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Academic Fee Receipt", MessageType.Success)
            End If
        End If

        If FileUploadImportRMF.HasFile Then
            If FileUploadImportRMF.PostedFile.ContentLength <> 0 And FileUploadImportRMF.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImportRMF.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImportRMF.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Muafakat Fee Receipt", MessageType.Success)
            End If
        End If

        If FileUploadImportAA.HasFile Then
            If FileUploadImportAA.PostedFile.ContentLength <> 0 And FileUploadImportAA.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImportAA.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImportAA.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Attachment A", MessageType.Success)
            End If
        End If

        If FileUploadImportAA3.HasFile Then
            If FileUploadImportAA3.PostedFile.ContentLength <> 0 And FileUploadImportAA3.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImportAA3.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImportAA3.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Attachment A3", MessageType.Success)
            End If
        End If

        If FileUploadImportPAL.HasFile Then
            If FileUploadImportPAL.PostedFile.ContentLength <> 0 And FileUploadImportPAL.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImportPAL.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImportPAL.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Parents Request Letter", MessageType.Success)
            End If
        End If

        If FileUploadImportFP.HasFile Then
            If FileUploadImportFP.PostedFile.ContentLength <> 0 And FileUploadImportFP.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImportFP.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImportFP.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Fathers Payslip", MessageType.Success)
            End If
        End If

        If FileUploadImportMP.HasFile Then
            If FileUploadImportMP.PostedFile.ContentLength <> 0 And FileUploadImportMP.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImportMP.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImportMP.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Mothers Payslip", MessageType.Success)
            End If
        End If

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        If FileUploadImport_F1RAF.HasFile Then
            If FileUploadImport_F1RAF.PostedFile.ContentLength <> 0 And FileUploadImport_F1RAF.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImport_F1RAF.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImport_F1RAF.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Academic Fee Receipt", MessageType.Success)
            End If
        End If

        If FileUploadImport_F1RMF.HasFile Then
            If FileUploadImport_F1RMF.PostedFile.ContentLength <> 0 And FileUploadImport_F1RMF.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImport_F1RMF.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImport_F1RMF.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Muafakat Fee Receipt", MessageType.Success)
            End If
        End If

        If FileUploadImport_F1AE.HasFile Then
            If FileUploadImport_F1AE.PostedFile.ContentLength <> 0 And FileUploadImport_F1AE.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImport_F1AE.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImport_F1AE.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Attachment E", MessageType.Success)
            End If
        End If

        If FileUploadImport_F1AE3.HasFile Then
            If FileUploadImport_F1AE3.PostedFile.ContentLength <> 0 And FileUploadImport_F1AE3.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImport_F1AE3.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImport_F1AE3.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Attachment E3", MessageType.Success)
            End If
        End If

        If FileUploadImport_F1PAL.HasFile Then
            If FileUploadImport_F1PAL.PostedFile.ContentLength <> 0 And FileUploadImport_F1PAL.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImport_F1PAL.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImport_F1PAL.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Parents Request Letter", MessageType.Success)
            End If
        End If

        If FileUploadImport_F1FP.HasFile Then
            If FileUploadImport_F1FP.PostedFile.ContentLength <> 0 And FileUploadImport_F1FP.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImport_F1FP.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImport_F1FP.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Fathers Payslip", MessageType.Success)
            End If
        End If

        If FileUploadImport_F1MP.HasFile Then
            If FileUploadImport_F1MP.PostedFile.ContentLength <> 0 And FileUploadImport_F1MP.PostedFile.FileName <> "" Then

                Dim filename_Doc As String = Path.GetFileName(FileUploadImport_F1MP.PostedFile.FileName)

                filename = get_stdMykad & "_" & find_Date & "_" & filename_Doc
                ''sets the image path
                imgPath = "~/reference_upload/" + filename

                ''then save it to the Folder
                FileUploadImport_F1MP.SaveAs(Server.MapPath(imgPath))

                strSQL = "insert into fee_documentList_Student(FDStudent_Year,FDStudent_Name,FDStudent_DateTime,std_id) values('" & Now.Year & "','" & filename_Doc & "','" & find_DateTime & "','" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

                ShowMessage(" Upload Mothers Payslip", MessageType.Success)
            End If
        End If
    End Sub

    Private Sub Year_Info_list()
        strSQL = "select distinct FDStudent_Year from fee_documentList_Student where std_ID = '" & oCommon.Student_securityLogin(Request.QueryString("std_ID")) & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlView_Year.DataSource = ds
            ddlView_Year.DataTextField = "FDStudent_Year"
            ddlView_Year.DataValueField = "FDStudent_Year"
            ddlView_Year.DataBind()
            ddlView_Year.Items.Insert(0, New ListItem("Select Year", String.Empty))
            ddlView_Year.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
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

        Dim data_ID As String = oCommon.Student_securityLogin(Request.QueryString("std_ID"))

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrderby As String = " ORDER BY FDStudent_Year DESC, FDStudent_DateTime DESC, FDStudent_Name ASC"

        tmpSQL = "Select * from fee_documentList_Student"
        strWhere = " WHERE std_ID = '" & data_ID & "'"
        strWhere += " AND FDStudent_Year = '" & ddlView_Year.SelectedValue & "'"

        getSQL = tmpSQL & strWhere & strOrderby

        ''--debug
        Return getSQL
    End Function

    Protected Sub ddlView_Year_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlView_Year.SelectedIndexChanged
        Try
            strRet = BindData(datRespondent)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        Warning
        [Error]
    End Enum

End Class