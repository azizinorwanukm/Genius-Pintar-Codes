Imports System.Data.SqlClient

Public Class counselor_Activity_View
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

                previousPage.NavigateUrl = String.Format("~/admin_kaunselor_aktivitikaunselor.aspx?admin_ID=" + Request.QueryString("admin_ID"))

                INC_Counsellor()
                INC_NewCounsellor()
                INC_Load_Data()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub INC_Counsellor()

        strSQL = "  select A.stf_ID, A.staff_Name from staff_info A left join staff_Login B on A.stf_ID = B.stf_ID
                    where A.staff_Status = 'Access' and (B.staff_Access = 'KSLR' or B.staff_Access = 'KKSLR') and staff_Campus = 'PGPN'
                    order by staff_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_INC_CN.DataSource = ds
            ddl_INC_CN.DataTextField = "staff_Name"
            ddl_INC_CN.DataValueField = "stf_ID"
            ddl_INC_CN.DataBind()
            ddl_INC_CN.Items.Insert(0, New ListItem("Select Counsellor Name", String.Empty))
            ddl_INC_CN.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub INC_NewCounsellor()

        strSQL = "  select A.stf_ID, A.staff_Name from staff_info A left join staff_Login B on A.stf_ID = B.stf_ID
                    where A.staff_Status = 'Access' and (B.staff_Access = 'KSLR' or B.staff_Access = 'KKSLR') and staff_Campus = 'PGPN'
                    order by staff_Name asc"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddl_NewCounselor_INC.DataTextField = "staff_Name"
            ddl_NewCounselor_INC.DataValueField = "stf_ID"
            ddl_NewCounselor_INC.DataBind()
            ddl_NewCounselor_INC.Items.Insert(0, New ListItem("Select Counselor Name", String.Empty))
            ddl_NewCounselor_INC.SelectedIndex = 0

        Catch ex As Exception
        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub INC_Load_Data()

        ''Get Year
        strSQL = "  Select * from counseling_inc A Left Join student_info B on B.std_ID = A.std_ID Left Join staff_Info C on C.stf_ID = A.stf_ID Left Join class_info D on D.class_ID = A.class_ID
                    where A.CINC_ID = '" & Session("get_ID") & "'"

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
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("stf_ID")) Then
                ddl_INC_CN.SelectedValue = ds.Tables(0).Rows(0).Item("stf_ID")
            Else
                ddl_INC_CN.SelectedIndex = 0
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("student_Name")) Then
                txtstudentName_SCINC.Text = ds.Tables(0).Rows(0).Item("student_Name")
            Else
                txtstudentName_SCINC.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("class_Name")) Then
                txtClass_SCINC.Text = ds.Tables(0).Rows(0).Item("class_Name")
            Else
                txtClass_SCINC.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_MT")) Then
                CB_MT.Checked = ds.Tables(0).Rows(0).Item("CINC_MT")
            Else
                CB_MT.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_PL")) Then
                CB_PL.Checked = ds.Tables(0).Rows(0).Item("CINC_PL")
            Else
                CB_PL.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_CA")) Then
                CB_CA.Checked = ds.Tables(0).Rows(0).Item("CINC_CA")
            Else
                CB_CA.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_IA")) Then
                CB_IA.Checked = ds.Tables(0).Rows(0).Item("CINC_IA")
            Else
                CB_IA.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_AP")) Then
                CB_AP.Checked = ds.Tables(0).Rows(0).Item("CINC_AP")
            Else
                CB_AP.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_TR")) Then
                CB_TR.Checked = ds.Tables(0).Rows(0).Item("CINC_TR")
            Else
                CB_TR.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_TM")) Then
                CB_TM.Checked = ds.Tables(0).Rows(0).Item("CINC_TM")
            Else
                CB_TM.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_CG")) Then
                CB_CG.Checked = ds.Tables(0).Rows(0).Item("CINC_CG")
            Else
                CB_CG.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_VL")) Then
                CB_VL.Checked = ds.Tables(0).Rows(0).Item("CINC_VL")
            Else
                CB_VL.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_ACWL")) Then
                CB_ACWL.Checked = ds.Tables(0).Rows(0).Item("CINC_ACWL")
            Else
                CB_ACWL.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_AD")) Then
                CB_AD.Checked = ds.Tables(0).Rows(0).Item("CINC_AD")
            Else
                CB_AD.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_AT")) Then
                CB_AT.Checked = ds.Tables(0).Rows(0).Item("CINC_AT")
            Else
                CB_AT.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_DA")) Then
                CB_DA.Checked = ds.Tables(0).Rows(0).Item("CINC_DA")
            Else
                CB_DA.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_CAL")) Then
                CB_CL.Checked = ds.Tables(0).Rows(0).Item("CINC_CAL")
            Else
                CB_CL.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_IS")) Then
                CB_IS.Checked = ds.Tables(0).Rows(0).Item("CINC_IS")
            Else
                CB_IS.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_BY")) Then
                CB_BY.Checked = ds.Tables(0).Rows(0).Item("CINC_BY")
            Else
                CB_BY.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_AY")) Then
                CB_AY.Checked = ds.Tables(0).Rows(0).Item("CINC_AY")
            Else
                CB_AY.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_SS")) Then
                CB_SS.Checked = ds.Tables(0).Rows(0).Item("CINC_SS")
            Else
                CB_SS.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_SH")) Then
                CB_SH.Checked = ds.Tables(0).Rows(0).Item("CINC_SH")
            Else
                CB_SH.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_RWF")) Then
                CB_RWF.Checked = ds.Tables(0).Rows(0).Item("CINC_RWF")
            Else
                CB_RWF.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_SL")) Then
                CB_SL.Checked = ds.Tables(0).Rows(0).Item("CINC_SL")
            Else
                CB_SL.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_HS")) Then
                CB_HS.Checked = ds.Tables(0).Rows(0).Item("CINC_HS")
            Else
                CB_HS.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_OC")) Then
                CB_OC.Checked = ds.Tables(0).Rows(0).Item("CINC_OC")
            Else
                CB_OC.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_HA")) Then
                CB_HA.Checked = ds.Tables(0).Rows(0).Item("CINC_HA")
            Else
                CB_HA.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_EP")) Then
                CB_EP.Checked = ds.Tables(0).Rows(0).Item("CINC_EP")
            Else
                CB_EP.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_WD")) Then
                CB_WD.Checked = ds.Tables(0).Rows(0).Item("CINC_WD")
            Else
                CB_WD.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_AX")) Then
                CB_AX.Checked = ds.Tables(0).Rows(0).Item("CINC_AX")
            Else
                CB_AX.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_RG")) Then
                CB_RG.Checked = ds.Tables(0).Rows(0).Item("CINC_RG")
            Else
                CB_RG.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_DP")) Then
                CB_DP.Checked = ds.Tables(0).Rows(0).Item("CINC_DP")
            Else
                CB_DP.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_FGW")) Then
                CB_FGW.Checked = ds.Tables(0).Rows(0).Item("CINC_FGW")
            Else
                CB_FGW.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_SD")) Then
                CB_SD.Checked = ds.Tables(0).Rows(0).Item("CINC_SD")
            Else
                CB_SD.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_RL")) Then
                CB_RL.Checked = ds.Tables(0).Rows(0).Item("CINC_RL")
            Else
                CB_RL.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_PN")) Then
                CB_PN.Checked = ds.Tables(0).Rows(0).Item("CINC_PN")
            Else
                CB_PN.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_PH")) Then
                CB_PH.Checked = ds.Tables(0).Rows(0).Item("CINC_PH")
            Else
                CB_PH.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_SCD")) Then
                CB_SCD.Checked = ds.Tables(0).Rows(0).Item("CINC_SCD")
            Else
                CB_SCD.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_MF")) Then
                CB_MF.Checked = ds.Tables(0).Rows(0).Item("CINC_MF")
            Else
                CB_MF.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_PNM")) Then
                CB_PNM.Checked = ds.Tables(0).Rows(0).Item("CINC_PNM")
            Else
                CB_PNM.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_VA")) Then
                CB_VA.Checked = ds.Tables(0).Rows(0).Item("CINC_VA")
            Else
                CB_VA.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_IM")) Then
                CB_IM.Checked = ds.Tables(0).Rows(0).Item("CINC_IM")
            Else
                CB_IM.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_ADSAI")) Then
                CB_ADSAI.Checked = ds.Tables(0).Rows(0).Item("CINC_ADSAI")
            Else
                CB_ADSAI.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_BC")) Then
                CB_BC.Checked = ds.Tables(0).Rows(0).Item("CINC_BC")
            Else
                CB_BC.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_DAS")) Then
                CB_DAS.Checked = ds.Tables(0).Rows(0).Item("CINC_DAS")
            Else
                CB_DAS.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_SHA")) Then
                CB_SHA.Checked = ds.Tables(0).Rows(0).Item("CINC_SHA")
            Else
                CB_SHA.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_Other")) Then
                CB_Other.Checked = ds.Tables(0).Rows(0).Item("CINC_Other")
            Else
                CB_Other.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_Other_Details")) Then
                txt_CBOther.Text = ds.Tables(0).Rows(0).Item("CINC_Other_Details")
            Else
                txt_CBOther.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_SG_NeedNewSession")) Then
                CB_VCRINC_CallingSession.Checked = ds.Tables(0).Rows(0).Item("CINC_SG_NeedNewSession")
            Else
                CB_VCRINC_CallingSession.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_SG_NewCounselor")) Then
                CB_VCRINC_NewCounselor.Checked = ds.Tables(0).Rows(0).Item("CINC_SG_NewCounselor")
            Else
                CB_VCRINC_NewCounselor.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_SG_EndSession")) Then
                CB_VCRINC_CounsellingEnd.Checked = ds.Tables(0).Rows(0).Item("CINC_SG_EndSession")
            Else
                CB_VCRINC_CounsellingEnd.Checked = "False"
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_NeedNewSession")) Then
                txt_CallingSession_INC.Text = ds.Tables(0).Rows(0).Item("CINC_NeedNewSession")
            Else
                txt_CallingSession_INC.Text = ""
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0).Item("CINC_SG_NewCounselor_stfID")) Then
                ddl_NewCounselor_INC.SelectedValue = ds.Tables(0).Rows(0).Item("CINC_SG_NewCounselor_stfID")
            Else
                ddl_NewCounselor_INC.SelectedIndex = 0
            End If

        End If

    End Sub

    Private Sub btn_updatecounseling_VCRCGPA_ServerClick(sender As Object, e As EventArgs) Handles btn_updatecounseling_VCRCGPA.ServerClick
        Try

            Dim get_CINC_MT As String = "False"
            Dim get_CINC_PL As String = "False"
            Dim get_CINC_CA As String = "False"
            Dim get_CINC_IA As String = "False"
            Dim get_CINC_AP As String = "False"
            Dim get_CINC_TR As String = "False"
            Dim get_CINC_TM As String = "False"
            Dim get_CINC_CG As String = "False"
            Dim get_CINC_VL As String = "False"
            Dim get_CINC_ACWL As String = "False"
            Dim get_CINC_AD As String = "False"
            Dim get_CINC_AT As String = "False"
            Dim get_CINC_DA As String = "False"
            Dim get_CINC_CAL As String = "False"
            Dim get_CINC_IS As String = "False"
            Dim get_CINC_BY As String = "False"
            Dim get_CINC_AY As String = "False"
            Dim get_CINC_SS As String = "False"
            Dim get_CINC_SH As String = "False"
            Dim get_CINC_RWF As String = "False"
            Dim get_CINC_SL As String = "False"
            Dim get_CINC_HS As String = "False"
            Dim get_CINC_OC As String = "False"
            Dim get_CINC_HA As String = "False"
            Dim get_CINC_EP As String = "False"
            Dim get_CINC_WD As String = "False"
            Dim get_CINC_AX As String = "False"
            Dim get_CINC_RG As String = "False"
            Dim get_CINC_DP As String = "False"
            Dim get_CINC_FGW As String = "False"
            Dim get_CINC_SD As String = "False"
            Dim get_CINC_RL As String = "False"
            Dim get_CINC_PN As String = "False"
            Dim get_CINC_PH As String = "False"
            Dim get_CINC_SCD As String = "False"
            Dim get_CINC_MF As String = "False"
            Dim get_CINC_PNM As String = "False"
            Dim get_CINC_VA As String = "False"
            Dim get_CINC_IM As String = "False"
            Dim get_CINC_ADSAI As String = "False"
            Dim get_CINC_BC As String = "False"
            Dim get_CINC_DAS As String = "False"
            Dim get_CINC_SHA As String = "False"
            Dim get_CINC_Other As String = "False"
            Dim get_CINC_SG_NS As String = "False"
            Dim get_CINC_SG_NC As String = "False"
            Dim get_CINC_SG_SE As String = "False"

            If CB_MT.Checked = True Then
                get_CINC_MT = "True"
            End If
            If CB_PL.Checked = True Then
                get_CINC_PL = "True"
            End If
            If CB_CA.Checked = True Then
                get_CINC_CA = "True"
            End If
            If CB_IA.Checked = True Then
                get_CINC_IA = "True"
            End If
            If CB_AP.Checked = True Then
                get_CINC_AP = "True"
            End If
            If CB_TR.Checked = True Then
                get_CINC_TR = "True"
            End If
            If CB_TM.Checked = True Then
                get_CINC_TM = "True"
            End If
            If CB_CG.Checked = True Then
                get_CINC_CG = "True"
            End If
            If CB_VL.Checked = True Then
                get_CINC_VL = "True"
            End If
            If CB_ACWL.Checked = True Then
                get_CINC_ACWL = "True"
            End If
            If CB_AD.Checked = True Then
                get_CINC_AD = "True"
            End If
            If CB_AT.Checked = True Then
                get_CINC_AT = "True"
            End If
            If CB_DA.Checked = True Then
                get_CINC_DA = "True"
            End If
            If CB_CL.Checked = True Then
                get_CINC_CAL = "True"
            End If
            If CB_IS.Checked = True Then
                get_CINC_IS = "True"
            End If
            If CB_BY.Checked = True Then
                get_CINC_BY = "True"
            End If
            If CB_AY.Checked = True Then
                get_CINC_AY = "True"
            End If
            If CB_SS.Checked = True Then
                get_CINC_SS = "True"
            End If
            If CB_SH.Checked = True Then
                get_CINC_SH = "True"
            End If
            If CB_RWF.Checked = True Then
                get_CINC_RWF = "True"
            End If
            If CB_SL.Checked = True Then
                get_CINC_SL = "True"
            End If
            If CB_HS.Checked = True Then
                get_CINC_HS = "True"
            End If
            If CB_OC.Checked = True Then
                get_CINC_OC = "True"
            End If
            If CB_HA.Checked = True Then
                get_CINC_HA = "True"
            End If
            If CB_EP.Checked = True Then
                get_CINC_EP = "True"
            End If
            If CB_WD.Checked = True Then
                get_CINC_WD = "True"
            End If
            If CB_AX.Checked = True Then
                get_CINC_AX = "True"
            End If
            If CB_RG.Checked = True Then
                get_CINC_RG = "True"
            End If
            If CB_DP.Checked = True Then
                get_CINC_DP = "True"
            End If
            If CB_FGW.Checked = True Then
                get_CINC_FGW = "True"
            End If
            If CB_SD.Checked = True Then
                get_CINC_SD = "True"
            End If
            If CB_RL.Checked = True Then
                get_CINC_RL = "True"
            End If
            If CB_PN.Checked = True Then
                get_CINC_PN = "True"
            End If
            If CB_PH.Checked = True Then
                get_CINC_PH = "True"
            End If
            If CB_SCD.Checked = True Then
                get_CINC_SCD = "True"
            End If
            If CB_MF.Checked = True Then
                get_CINC_MF = "True"
            End If
            If CB_PNM.Checked = True Then
                get_CINC_PNM = "True"
            End If
            If CB_VA.Checked = True Then
                get_CINC_VA = "True"
            End If
            If CB_IM.Checked = True Then
                get_CINC_IM = "True"
            End If
            If CB_ADSAI.Checked = True Then
                get_CINC_ADSAI = "True"
            End If
            If CB_BC.Checked = True Then
                get_CINC_BC = "True"
            End If
            If CB_DAS.Checked = True Then
                get_CINC_DAS = "True"
            End If
            If CB_SHA.Checked = True Then
                get_CINC_SHA = "True"
            End If
            If CB_Other.Checked = True Then
                get_CINC_Other = "True"

                If txt_CBOther.Text.Length = 0 Then
                    ShowMessage(" Please Fill In Next Session ", MessageType.Error)
                    Exit Sub
                End If
            End If

            If CB_VCRINC_CallingSession.Checked = True Then
                get_CINC_SG_NS = "True"

                If txt_CallingSession_INC.Text.Length = 0 Then
                    ShowMessage(" Please Fill In Next Session ", MessageType.Error)
                    Exit Sub
                End If
            End If

            If CB_VCRINC_NewCounselor.Checked = True Then
                get_CINC_SG_NC = "True"

                If ddl_NewCounselor_INC.SelectedIndex = 0 Then
                    ShowMessage(" Please Selct New Counselor ", MessageType.Error)
                    Exit Sub
                End If
            End If

            If CB_VCRINC_CounsellingEnd.Checked = True Then
                get_CINC_SG_SE = "True"
            End If

            Dim find_StdID As String = "Select std_ID from counselling_inc where CINC_ID = '" & Session("get_CIID") & "'"
            Dim get_StdID As String = oCommon.getFieldValue(find_StdID)

            Dim find_StfID As String = "Select stf_ID from counselling_inc where CINC_ID = '" & Session("get_CIID") & "'"
            Dim get_StfID As String = oCommon.getFieldValue(find_StfID)

            strSQL = "      Update counselling_inc set CINC_Status = 'Done', CINC_MT = '" & get_CINC_MT & "', CINC_PL = '" & get_CINC_PL & "', CINC_CA = '" & get_CINC_CA & "', CINC_IA = '" & get_CINC_IA & "',
                            CINC_AP = '" & get_CINC_AP & "', CINC_TR = '" & get_CINC_TR & "', CINC_TM = '" & get_CINC_TM & "', CINC_CG = '" & get_CINC_CG & "', CINC_VL = '" & get_CINC_VL & "', CINC_ACWL = '" & get_CINC_ACWL & "', CINC_AD = '" & get_CINC_AD & "',
                            CINC_AT = '" & get_CINC_AT & "', CINC_DA = '" & get_CINC_DA & "', CINC_CAL = '" & get_CINC_CAL & "', CINC_IS = '" & get_CINC_IS & "', CINC_BY = '" & get_CINC_BY & "', CINC_AY = '" & get_CINC_AY & "', CINC_SS = '" & get_CINC_SS & "',
                            CINC_SH = '" & get_CINC_SH & "', CINC_RWF = '" & get_CINC_RWF & "', CINC_SL = '" & get_CINC_SL & "', CINC_HS = '" & get_CINC_HS & "', CINC_OC = '" & get_CINC_OC & "', CINC_HA = '" & get_CINC_HA & "', CINC_EP = '" & get_CINC_EP & "',
                            CINC_WD = '" & get_CINC_WD & "', CINC_AX = '" & get_CINC_AX & "', CINC_RG = '" & get_CINC_RG & "', CINC_DP = '" & get_CINC_DP & "', CINC_FGW = '" & get_CINC_FGW & "', CINC_SD = '" & get_CINC_SD & "', CINC_RL = '" & get_CINC_RL & "',
                            CINC_PN = '" & get_CINC_PN & "', CINC_PH = '" & get_CINC_PH & "', CINC_SCD = '" & get_CINC_SCD & "', CINC_MF = '" & get_CINC_MF & "', CINC_PNM = '" & get_CINC_PNM & "', CINC_VA = '" & get_CINC_VA & "', CINC_IM = '" & get_CINC_IM & "',
                            CINC_ADSAI = '" & get_CINC_ADSAI & "', CINC_BC = '" & get_CINC_BC & "', CINC_DAS = '" & get_CINC_DAS & "', CINC_SHA = '" & get_CINC_SHA & "', CINC_Other = '" & get_CINC_Other & "', CINC_Other_Details = '" & txt_CBOther.Text & "',
                             CINC_SG_NeedNewSession = '" & get_CINC_SG_NS & "', 
                            CINC_NeedNewSession = '" & txt_CallingSession_INC.Text & "', CINC_SG_NewCounselor = '" & get_CINC_SG_NC & "', CINC_SG_NewCounselor_stfID = '" & ddl_NewCounselor_INC.SelectedValue & "', CINC_SG_EndSession = '" & get_CINC_SG_SE & "' 
                            where CINC_ID = '" & Session("get_CIID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            If CB_VCRINC_CallingSession.Checked = True Then

                strSQL = "      Insert Into counseling_inc(std_ID,stf_ID,class_ID,CINC_Status,CINC_MT,CINC_PL,CINC_CA,CINC_IA,CINC_AP,CINC_TR,CINC_TM,CINC_CG,CINC_VL,CINC_ACWL,CINC_AD,CINC_AT,CINC_DA,CINC_CAL,CINC_IS,CINC_BY,CINC_AY,CINC_SS,CINC_SH,CINC_RWF,CINC_SL,CINC_HS,CINC_OC,
                                CINC_HA,CINC_EP,CINC_WD,CINC_AX,CINC_RG,CINC_DP,CINC_FGW,CINC_SD,CINC_RL,CINC_PN,CINC_PH,CINC_SCD,CINC_MF,CINC_PNM,CINC_VA,CINC_IM,CINC_ADSAI,CINC_BC,CINC_DAS,CINC_SHA,CINC_Other,CINC_Other_Details,CINC_Year)
                                Values('" & get_StdID & "','" & get_StfID & "','" & strRet & "','Requested','" & CB_MT.Checked & "','" & CB_PL.Checked & "','" & CB_CA.Checked & "','" & CB_IA.Checked & "','" & CB_AP.Checked & "','" & CB_TR.Checked & "',
                                '" & CB_TM.Checked & "','" & CB_CG.Checked & "','" & CB_VL.Checked & "','" & CB_ACWL.Checked & "','" & CB_AD.Checked & "','" & CB_AT.Checked & "','" & CB_DA.Checked & "','" & CB_CL.Checked & "','" & CB_IS.Checked & "','" & CB_BY.Checked & "',
                                '" & CB_AY.Checked & "','" & CB_SS.Checked & "','" & CB_SH.Checked & "','" & CB_RWF.Checked & "','" & CB_SL.Checked & "','" & CB_HS.Checked & "','" & CB_OC.Checked & "','" & CB_HA.Checked & "','" & CB_EP.Checked & "','" & CB_WD.Checked & "',
                                '" & CB_AX.Checked & "','" & CB_RG.Checked & "','" & CB_DP.Checked & "','" & CB_FGW.Checked & "','" & CB_SD.Checked & "','" & CB_RL.Checked & "','" & CB_PN.Checked & "','" & CB_PH.Checked & "','" & CB_SCD.Checked & "','" & CB_MF.Checked & "',
                                '" & CB_PNM.Checked & "','" & CB_VA.Checked & "','" & CB_IM.Checked & "','" & CB_ADSAI.Checked & "','" & CB_BC.Checked & "','" & CB_DAS.Checked & "','" & CB_SHA.Checked & "','" & CB_Other.Checked & "','" & txt_CBOther.Text & "','" & Now.Year & "')"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

            If CB_VCRINC_NewCounselor.Checked = True Then

                strSQL = "      Insert Into counseling_inc(std_ID,stf_ID,class_ID,CINC_Status,CINC_MT,CINC_PL,CINC_CA,CINC_IA,CINC_AP,CINC_TR,CINC_TM,CINC_CG,CINC_VL,CINC_ACWL,CINC_AD,CINC_AT,CINC_DA,CINC_CAL,CINC_IS,CINC_BY,CINC_AY,CINC_SS,CINC_SH,CINC_RWF,CINC_SL,CINC_HS,CINC_OC,
                                CINC_HA,CINC_EP,CINC_WD,CINC_AX,CINC_RG,CINC_DP,CINC_FGW,CINC_SD,CINC_RL,CINC_PN,CINC_PH,CINC_SCD,CINC_MF,CINC_PNM,CINC_VA,CINC_IM,CINC_ADSAI,CINC_BC,CINC_DAS,CINC_SHA,CINC_Other,CINC_Other_Details,CINC_Year)
                                Values('" & get_StdID & "','" & ddl_NewCounselor_INC.SelectedValue & "','" & strRet & "','Requested','" & CB_MT.Checked & "','" & CB_PL.Checked & "','" & CB_CA.Checked & "','" & CB_IA.Checked & "','" & CB_AP.Checked & "','" & CB_TR.Checked & "',
                                '" & CB_TM.Checked & "','" & CB_CG.Checked & "','" & CB_VL.Checked & "','" & CB_ACWL.Checked & "','" & CB_AD.Checked & "','" & CB_AT.Checked & "','" & CB_DA.Checked & "','" & CB_CL.Checked & "','" & CB_IS.Checked & "','" & CB_BY.Checked & "',
                                '" & CB_AY.Checked & "','" & CB_SS.Checked & "','" & CB_SH.Checked & "','" & CB_RWF.Checked & "','" & CB_SL.Checked & "','" & CB_HS.Checked & "','" & CB_OC.Checked & "','" & CB_HA.Checked & "','" & CB_EP.Checked & "','" & CB_WD.Checked & "',
                                '" & CB_AX.Checked & "','" & CB_RG.Checked & "','" & CB_DP.Checked & "','" & CB_FGW.Checked & "','" & CB_SD.Checked & "','" & CB_RL.Checked & "','" & CB_PN.Checked & "','" & CB_PH.Checked & "','" & CB_SCD.Checked & "','" & CB_MF.Checked & "',
                                '" & CB_PNM.Checked & "','" & CB_VA.Checked & "','" & CB_IM.Checked & "','" & CB_ADSAI.Checked & "','" & CB_BC.Checked & "','" & CB_DAS.Checked & "','" & CB_SHA.Checked & "','" & CB_Other.Checked & "','" & txt_CBOther.Text & "','" & Now.Year & "')"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If


            If strRet = "0" Then
                ShowMessage(" Update Counselling Session ", MessageType.Success)
            Else
                ShowMessage(" Unsuccessful Update Counselling Session ", MessageType.Success)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btn_Exportcounseling_VCRCGPA_ServerClick(sender As Object, e As EventArgs) Handles btn_Exportcounseling_VCRCGPA.ServerClick

        Dim Prints As New StringBuilder()

        Prints.AppendLine("<div id='data' style='display:none'>")
        Prints.AppendLine("<div id='dataCounselling'> ")

        If Session("getStatus") = "VCR_CGPA" Then

            Prints.AppendLine("<p style='text-align:center;font-family:BankFuturistic; font-size:0.6875em'><b> LAPORAN SESI BIMBINGAN INDIVIDU </b></p> ")

            strSQL = "Select UPPER(A.student_Name) from student_info A left join counselor_info B on A.std_ID = B.std_ID where B.CI_ID = '" & Session("get_CIID") & "'"
            Dim get_StudentName As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_Date from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_Date As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select UPPER(A.student_Sex) from student_info A left join counselor_info B on A.std_ID = B.std_ID where B.CI_ID = '" & Session("get_CIID") & "'"
            Dim get_StudentGender As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_StartTime from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_StartTime As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_EndTime from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_EndTime As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select UPPER(A.student_Race) from student_info A left join counselor_info B on A.std_ID = B.std_ID where B.CI_ID = '" & Session("get_CIID") & "'"
            Dim get_StudentRace As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select UPPER(A.staff_Name) from staff_Info A left join counselor_info B on A.stf_ID = B.stf_ID where B.CI_ID = '" & Session("get_CIID") & "'"
            Dim get_StaffName As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select UPPER(CI_Type) from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_Type As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select UPPER(CI_Attendance) from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_Attendance As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select A.class_Name from class_info A left join counselor_info B on A.class_ID = B.class_ID where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_ClassName As String = oCommon.getFieldValue(strSQL)

            Prints.AppendLine(" <div style='padding-top:5px'>
                                    <table style='width:100%;border: 1px solid black;border-collapse: collapse;'>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:15%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px;background-color:#ffcba4'><b>STUDENT NAME</b></td>
                                            <td style='width:35%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px'> &nbsp;" & get_StudentName & "</td>
                                            <td style='width:15%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px;background-color:#ffcba4'><b>DATE & TIME</b></td>
                                            <td style='width:35%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px'> &nbsp;" & get_Date & " &nbsp;&nbsp; " & get_StartTime & " - " & get_EndTime & " </td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:15%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px;background-color:#ffcba4'><b>GENDER</b></td>
                                            <td style='width:35%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px'> &nbsp;" & get_StudentGender & "</td>
                                            <td style='width:15%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px;background-color:#ffcba4'><b>CLASS</b></td>
                                            <td style='width:35%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px'> &nbsp; " & get_ClassName & "</td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:15%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px;background-color:#ffcba4'><b>RACE</b></td>
                                            <td style='width:35%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px'> &nbsp;" & get_StudentRace & "</td>
                                            <td style='width:15%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px;background-color:#ffcba4'><b>COUNSELOR NAME</b></td>
                                            <td style='width:35%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px'> &nbsp;" & get_StaffName & "</td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:15%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px;background-color:#ffcba4'><b>ISSUES TYPE</b></td>
                                            <td style='width:35%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px'> &nbsp;" & get_Type & "</td>
                                            <td style='width:15%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px;background-color:#ffcba4'><b>ATTENDANCE</b></td>
                                            <td style='width:35%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px'> &nbsp;" & get_Attendance & "</td>
                                        </tr>
                                    </table>
                                </div> <br><br>")


            strSQL = "Select CI_StudentCondition from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_StudentCondition As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_StudentBackground from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_StudentBackground As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_CounselingRemark from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_CounselingRemark As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_Action from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_Action As String = oCommon.getFieldValue(strSQL)


            strSQL = "Select CI_DT_MB from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_MB As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_DT_MT from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_MT As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_DT_MDMM from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_MDMM As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_DT_MDMA from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_MDMA As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_DT_MPPM from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_MPPM As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_DT_MS from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_MS As String = oCommon.getFieldValue(strSQL)

            If get_DT_MB = "True" Then
                get_DT_MB = "&#10004;"
            Else
                get_DT_MB = ""
            End If

            If get_DT_MT = "True" Then
                get_DT_MT = "&#10004;"
            Else
                get_DT_MT = ""
            End If

            If get_DT_MDMM = "True" Then
                get_DT_MDMM = "&#10004;"
            Else
                get_DT_MDMM = ""
            End If

            If get_DT_MDMA = "True" Then
                get_DT_MDMA = "&#10004;"
            Else
                get_DT_MDMA = ""
            End If

            If get_DT_MPPM = "True" Then
                get_DT_MPPM = "&#10004;"
            Else
                get_DT_MPPM = ""
            End If

            If get_DT_MS = "True" Then
                get_DT_MS = "&#10004;"
            Else
                get_DT_MS = ""
            End If


            strSQL = "Select CI_SuggestionNewSession from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_SNS As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_SuggestionNewCounselor from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_SNC As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_SuggestionEndSession from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_SES As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select CI_SuggestionNewSession_No from counselor_info where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_SNSN As String = oCommon.getFieldValue(strSQL)

            strSQL = "Select UPPER(A.staff_Name) from staff_info A left join counselor_info B on A.stf_ID =  B.CI_SuggestionNewCounselor_StfID where CI_ID = '" & Session("get_CIID") & "'"
            Dim get_DT_SNCS As String = oCommon.getFieldValue(strSQL)


            If get_DT_SNS = "True" Then
                get_DT_SNS = "&#10004;"
            Else
                get_DT_SNS = ""
                get_DT_SNSN = "__________"
            End If

            If get_DT_SNC = "True" Then
                get_DT_SNC = "&#10004;"
            Else
                get_DT_SNC = ""
                get_DT_SNCS = "__________"
            End If

            If get_DT_SES = "True" Then
                get_DT_SES = "&#10004;"
            Else
                get_DT_SES = ""
            End If

            Prints.AppendLine(" <div style='padding-top:10px'>
                                    <table style='width:100%;border: 1px solid black;border-collapse: collapse;'>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:100%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;background-color:#ffcba4;text-align:center;'><b>COUNSELLING REPORT</b></td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:100%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:20px'><b>Student Conditon :</b><br> " & get_StudentCondition & "</td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:100%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:20px'><b>Student Background :</b><br> " & get_StudentBackground & "</td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:100%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px'><b>Discussion Type :</b><br><br>
                                                <table style='width:90%;border: 1px solid black;border-collapse: collapse;padding-top:5px;margin-left:10px'>
                                                    <tr style='width:100%;border: 1px solid black'>
                                                        <td style='width:40%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;'>Building Rapport Or Relationship</td>
                                                        <td style='width:10%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;text-align:center'>" & get_DT_MB & "</td>
                                                        <td style='width:40%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;'>Taking Action</td>
                                                        <td style='width:10%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;text-align:center'>" & get_DT_MT & "</td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black'>
                                                        <td style='width:40%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;'>Exploring And Analyzing The Problems</td>
                                                        <td style='width:10%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;text-align:center'>" & get_DT_MDMM & "</td>
                                                        <td style='width:40%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;'>Discussing And Choosing Alternative</td>
                                                        <td style='width:10%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;text-align:center'>" & get_DT_MDMA & "</td>
                                                    </tr>
                                                    <tr style='width:100%;border: 1px solid black'>
                                                        <td style='width:40%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;'>Identifying The Cause Of Problem</td>
                                                        <td style='width:10%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;text-align:center'>" & get_DT_MPPM & "</td>
                                                        <td style='width:40%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;'>Ending Session</td>
                                                        <td style='width:10%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;text-align:center'>" & get_DT_MS & "</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:100%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:20px'><b>Issues Discussed :</b><br> " & get_CounselingRemark & "</td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:100%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:20px'><b>Action :</b><br> " & get_Action & "</td>
                                        </tr>
                                        <tr style='width:100%;border: 1px solid black'>
                                            <td style='width:100%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:10px'><b>Suggestion :</b><br><br>
                                                <table style='width:90%;padding-top:5px;margin-left:10px'>
                                                    <tr style='width:100%;'>
                                                        <td style='width:10%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;text-align:center'>" & get_DT_SNS & "</td>
                                                        <td style='width:80%;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;'>Client Will Be Called Back For <b>" & get_DT_SNSN & "</b> Session</td>
                                                    </tr>
                                                    <tr style='width:100%;'>
                                                        <td style='width:10%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;text-align:center'>" & get_DT_SNC & "</td>
                                                        <td style='width:80%;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;'>Client Will Be Referred To <b>" & get_DT_SNCS & "</b></td>
                                                    </tr>
                                                    <tr style='width:100%;'>
                                                        <td style='width:10%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;text-align:center'>" & get_DT_SES & "</td>
                                                        <td style='width:80%;font-family:BankFuturistic; font-size:0.6875em;padding-bottom:5px;'>Counselling Session For This Client Is Ended</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>")

        ElseIf Session("getStatus") = "VCR_INC" Then

            Prints.AppendLine(" <table style='width:100%;'>
                                    <tr style='width:100%;'>
                                        <td style='width:35%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;'> Psychology Support Centre & Counselling emphasizes the protection of a person's personal data.  Personal data filled in in this form will be treated as <b>CONFIDENTIAL</b> </td>
                                        <td style='width:65%;'></td>
                                    </tr>
                                </table>")

            Prints.AppendLine("<p style='text-align:center;font-family:BankFuturistic; font-size:1em;'><b> BORANG RUJUKAN KAUNSELING </b></p> ")

            strSQL = "  Select A.CINC_ID, B.student_Name, B.student_ID, D.staff_Name, A.CINC_Session, D.staff_MobileNo, C.class_Name, A.CINC_Date, A.CINC_StartTime, A.CINC_EndTime, A.CINC_Status, A.CINC_Status as CINC_Status_Color, A.CINC_SD_Reorder, A.CINC_Attendace, 
                        A.CINC_SG_NeedNewSession, A.CINC_SG_NewCounselor, A.CINC_SG_EndSession, A.CINC_SG_NewCounselor_stfID, A.CINC_NeedNewSession, A.CINC_MT, A.CINC_PL, A.CINC_CA, A.CINC_IA, A.CINC_AP, A.CINC_TR, A.CINC_TM, A.CINC_CG, A.CINC_VL, A.CINC_ACWL,
                        A.CINC_AD, A.CINC_AT, A.CINC_DA, A.CINC_CAL, A.CINC_IS, A.CINC_BY, A.CINC_AY, A.CINC_SS, A.CINC_SH, A.CINC_RWF, A.CINC_SL, A.CINC_HS, A.CINC_OC, A.CINC_HA, A.CINC_EP, A.CINC_WD, A.CINC_AX, A.CINC_RG, A.CINC_DP, A.CINC_FGW, A.CINC_SD, A.CINC_RL, 
                        A.CINC_PN, A.CINC_PH, A.CINC_SCD, A.CINC_MF, A.CINC_PNM, A.CINC_VA, A.CINC_IM, A.CINC_ADSAI, A.CINC_BC, A.CINC_DAS, A.CINC_SHA, A.CINC_Other, A.CINC_Other_Details
                        from counseling_inc A
                        Left join student_info B on A.std_ID = B.std_ID
                        Left join class_info C on A.class_ID = C.class_ID
                        left join staff_Info D on A.stf_ID = D.stf_ID
                        where A.CINC_ID = '" & Session("get_CIID") & "'"

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

                Dim C_MT As String = ds.Tables(0).Rows(0).Item("CINC_MT")
                Dim C_PL As String = ds.Tables(0).Rows(0).Item("CINC_PL")
                Dim C_CA As String = ds.Tables(0).Rows(0).Item("CINC_CA")
                Dim C_IA As String = ds.Tables(0).Rows(0).Item("CINC_IA")
                Dim C_AP As String = ds.Tables(0).Rows(0).Item("CINC_AP")
                Dim C_TR As String = ds.Tables(0).Rows(0).Item("CINC_TR")
                Dim C_TM As String = ds.Tables(0).Rows(0).Item("CINC_TM")
                Dim C_CG As String = ds.Tables(0).Rows(0).Item("CINC_CG")
                Dim C_VL As String = ds.Tables(0).Rows(0).Item("CINC_VL")
                Dim C_ACWL As String = ds.Tables(0).Rows(0).Item("CINC_ACWL")
                Dim C_AD As String = ds.Tables(0).Rows(0).Item("CINC_AD")
                Dim C_AT As String = ds.Tables(0).Rows(0).Item("CINC_AT")
                Dim C_DA As String = ds.Tables(0).Rows(0).Item("CINC_DA")
                Dim C_CAL As String = ds.Tables(0).Rows(0).Item("CINC_CAL")
                Dim C_IS As String = ds.Tables(0).Rows(0).Item("CINC_IS")
                Dim C_BY As String = ds.Tables(0).Rows(0).Item("CINC_BY")
                Dim C_AY As String = ds.Tables(0).Rows(0).Item("CINC_AY")
                Dim C_SS As String = ds.Tables(0).Rows(0).Item("CINC_SS")
                Dim C_SH As String = ds.Tables(0).Rows(0).Item("CINC_SH")
                Dim C_RWF As String = ds.Tables(0).Rows(0).Item("CINC_RWF")
                Dim C_SL As String = ds.Tables(0).Rows(0).Item("CINC_SL")
                Dim C_HS As String = ds.Tables(0).Rows(0).Item("CINC_HS")
                Dim C_OC As String = ds.Tables(0).Rows(0).Item("CINC_OC")
                Dim C_HA As String = ds.Tables(0).Rows(0).Item("CINC_HA")
                Dim C_EP As String = ds.Tables(0).Rows(0).Item("CINC_EP")
                Dim C_WD As String = ds.Tables(0).Rows(0).Item("CINC_WD")
                Dim C_AX As String = ds.Tables(0).Rows(0).Item("CINC_AX")
                Dim C_RG As String = ds.Tables(0).Rows(0).Item("CINC_RG")
                Dim C_DP As String = ds.Tables(0).Rows(0).Item("CINC_DP")
                Dim C_FGW As String = ds.Tables(0).Rows(0).Item("CINC_FGW")
                Dim C_SD As String = ds.Tables(0).Rows(0).Item("CINC_SD")
                Dim C_RL As String = ds.Tables(0).Rows(0).Item("CINC_RL")
                Dim C_PN As String = ds.Tables(0).Rows(0).Item("CINC_PN")
                Dim C_PH As String = ds.Tables(0).Rows(0).Item("CINC_PH")
                Dim C_SCD As String = ds.Tables(0).Rows(0).Item("CINC_SCD")
                Dim C_MF As String = ds.Tables(0).Rows(0).Item("CINC_MF")
                Dim C_PNM As String = ds.Tables(0).Rows(0).Item("CINC_PNM")
                Dim C_VA As String = ds.Tables(0).Rows(0).Item("CINC_VA")
                Dim C_IM As String = ds.Tables(0).Rows(0).Item("CINC_IM")
                Dim C_ADSAI As String = ds.Tables(0).Rows(0).Item("CINC_ADSAI")
                Dim C_BC As String = ds.Tables(0).Rows(0).Item("CINC_BC")
                Dim C_DAS As String = ds.Tables(0).Rows(0).Item("CINC_DAS")
                Dim C_SHA As String = ds.Tables(0).Rows(0).Item("CINC_SHA")
                Dim C_Other As String = ds.Tables(0).Rows(0).Item("CINC_Other")
                Dim C_Other_Details As String = ds.Tables(0).Rows(0).Item("CINC_Other_Details")

                If C_MT = True Then
                    C_MT = "&#10003;"
                Else
                    C_MT = " "
                End If

                If C_PL = True Then
                    C_PL = "&#10003;"
                Else
                    C_PL = " "
                End If

                If C_CA = True Then
                    C_CA = "&#10003;"
                Else
                    C_CA = " "
                End If

                If C_IA = True Then
                    C_IA = "&#10003;"
                Else
                    C_IA = " "
                End If

                If C_AP = True Then
                    C_AP = "&#10003;"
                Else
                    C_AP = " "
                End If

                If C_TR = True Then
                    C_TR = "&#10003;"
                Else
                    C_TR = " "
                End If

                If C_TM = True Then
                    C_TM = "&#10003;"
                Else
                    C_TM = " "
                End If

                If C_CG = True Then
                    C_CG = "&#10003;"
                Else
                    C_CG = " "
                End If

                If C_VL = True Then
                    C_VL = "&#10003;"
                Else
                    C_VL = " "
                End If

                If C_ACWL = True Then
                    C_ACWL = "&#10003;"
                Else
                    C_ACWL = " "
                End If

                If C_AD = True Then
                    C_AD = "&#10003;"
                Else
                    C_AD = " "
                End If

                If C_AT = True Then
                    C_AT = "&#10003;"
                Else
                    C_AT = " "
                End If

                If C_DA = True Then
                    C_DA = "&#10003;"
                Else
                    C_DA = " "
                End If

                If C_CAL = True Then
                    C_CAL = "&#10003;"
                Else
                    C_CAL = " "
                End If

                If C_IS = True Then
                    C_IS = "&#10003;"
                Else
                    C_IS = " "
                End If

                If C_BY = True Then
                    C_BY = "&#10003;"
                Else
                    C_BY = " "
                End If

                If C_AY = True Then
                    C_AY = "&#10003;"
                Else
                    C_AY = " "
                End If

                If C_SS = True Then
                    C_SS = "&#10003;"
                Else
                    C_SS = " "
                End If

                If C_SH = True Then
                    C_SH = "&#10003;"
                Else
                    C_SH = " "
                End If

                If C_RWF = True Then
                    C_RWF = "&#10003;"
                Else
                    C_RWF = " "
                End If

                If C_SL = True Then
                    C_SL = "&#10003;"
                Else
                    C_SL = " "
                End If

                If C_HS = True Then
                    C_HS = "&#10003;"
                Else
                    C_HS = " "
                End If

                If C_OC = True Then
                    C_OC = "&#10003;"
                Else
                    C_OC = " "
                End If

                If C_HA = True Then
                    C_HA = "&#10003;"
                Else
                    C_HA = " "
                End If

                If C_EP = True Then
                    C_EP = "&#10003;"
                Else
                    C_EP = " "
                End If

                If C_WD = True Then
                    C_WD = "&#10003;"
                Else
                    C_WD = " "
                End If

                If C_AX = True Then
                    C_AX = "&#10003;"
                Else
                    C_AX = " "
                End If

                If C_RG = True Then
                    C_RG = "&#10003;"
                Else
                    C_RG = " "
                End If

                If C_DP = True Then
                    C_DP = "&#10003;"
                Else
                    C_DP = " "
                End If

                If C_FGW = True Then
                    C_FGW = "&#10003;"
                Else
                    C_FGW = " "
                End If

                If C_SD = True Then
                    C_SD = "&#10003;"
                Else
                    C_SD = " "
                End If

                If C_RL = True Then
                    C_RL = "&#10003;"
                Else
                    C_RL = " "
                End If

                If C_PN = True Then
                    C_PN = "&#10003;"
                Else
                    C_PN = " "
                End If

                If C_PH = True Then
                    C_PH = "&#10003;"
                Else
                    C_PH = " "
                End If

                If C_SCD = True Then
                    C_SCD = "&#10003;"
                Else
                    C_SCD = " "
                End If

                If C_MF = True Then
                    C_MF = "&#10003;"
                Else
                    C_MF = " "
                End If

                If C_PNM = True Then
                    C_PNM = "&#10003;"
                Else
                    C_PNM = " "
                End If

                If C_VA = True Then
                    C_VA = "&#10003;"
                Else
                    C_VA = " "
                End If

                If C_IM = True Then
                    C_IM = "&#10003;"
                Else
                    C_IM = " "
                End If

                If C_ADSAI = True Then
                    C_ADSAI = "&#10003;"
                Else
                    C_ADSAI = " "
                End If

                If C_BC = True Then
                    C_BC = "&#10003;"
                Else
                    C_BC = " "
                End If

                If C_DAS = True Then
                    C_DAS = "&#10003;"
                Else
                    C_DAS = " "
                End If

                If C_SHA = True Then
                    C_SHA = "&#10003;"
                Else
                    C_SHA = " "
                End If

                If C_Other = True Then
                    C_Other = "&#10003;"
                Else
                    C_Other = " "
                End If

                If C_Other_Details.Length = 0 Then
                    C_Other_Details = " "
                End If

                Prints.AppendLine(" <div >
                                    <table style='width:100%;'>
                                        <tr style='width:100%;'>
                                            <td style='width:85%;font-family:BankFuturistic; font-size:0.6875em;'></td>
                                            <td style='width:5%;font-family:BankFuturistic; font-size:0.6875em;'> Date : </td>
                                            <td style='width:10%;border-bottom: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;'>" & ds.Tables(0).Rows(0).Item("CINC_Date") & "</td>
                                            
                                        </tr>
                                    </table>
                                    </div>

                                    <br />
                                    <div >
                                    <table style='width:100%;'>
                                        <tr style='width:100%;'>
                                            <td style='width:10%;font-family:BankFuturistic; font-size:0.6875em;'>Student Name : </td>
                                            <td style='width:50%;border-bottom: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;'>" & ds.Tables(0).Rows(0).Item("student_Name") & "</td>
                                            <td style='width:3%;font-family:BankFuturistic; font-size:0.6875em;'></td>
                                            <td style='width:8%;font-family:BankFuturistic; font-size:0.6875em;'>Student ID : </td>
                                            <td style='width:11%;border-bottom: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;'>" & ds.Tables(0).Rows(0).Item("student_ID") & "</td>
                                            <td style='width:3%;font-family:BankFuturistic; font-size:0.6875em;'></td>
                                            <td style='width:5%;font-family:BankFuturistic; font-size:0.6875em;'>Class : </td>
                                            <td style='width:10%;border-bottom: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;'>" & ds.Tables(0).Rows(0).Item("class_Name") & "</td>
                                        </tr>
                                    </table>
                                    </div>")

                Prints.AppendLine(" 
                                    <div >
                                    <table style='width:100%; '>
                                        <tr style='width:100%;'>
                                            <td style='width:12%;font-family:BankFuturistic; font-size:0.6875em;'>Counsellor Name : </td>
                                            <td style='width:40%;border-bottom: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;'>" & ds.Tables(0).Rows(0).Item("staff_Name") & "</td>
                                            <td style='width:3%;font-family:BankFuturistic; font-size:0.6875em;'></td>
                                            <td style='width:8%;font-family:BankFuturistic; font-size:0.6875em;'>Mobile No : </td>
                                            <td style='width:10%;border-bottom: 1px solid black;font-family:BankFuturistic; font-size:0.6875em;'>" & ds.Tables(0).Rows(0).Item("staff_MobileNo") & "</td>
                                            <td style='width:37%;font-family:BankFuturistic; font-size:0.6875em;'></td>
                                        </tr>
                                    </table>
                                    </div>")

                Prints.AppendLine(" <br /> <br />
                                    <div >
                                    <table style='width:100%; border: 1px solid black;border-collapse: collapse;'>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_MT & "] </b> Motivation </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_DA & "] </b> Disrespectful Attitude </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_HA & "] </b> Hyperactive </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_PH & "] </b> Personal Hygiene </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_PL & "] </b> Plagiarism </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_CAL & "] </b> Cheating and Lying </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_EP & "] </b> Easy Panic </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_SCD & "] </b> Suicidal Desire </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_CA & "] </b> Class Attendance </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_IS & "] </b> Impulsive </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_WD & "] </b> Withdrawn </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_MF & "] </b> Missing Functionality </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_IA & "] </b> Inattentive </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_BY & "] </b> Bully </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_AX & "] </b> Anxiety </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_PNM & "] </b> Perfectionism </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_AP & "] </b> Academic Performance </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_AY & "] </b> Apathy </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_RG & "] </b> Rage </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_VA & "] </b> Very Aggresive </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_TR & "] </b> Tardy </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_SS & "] </b> Social Skills </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_DP & "] </b> Depress </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_IM & "] </b> Insomnia </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_TM & "] </b> Time Management </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_SH & "] </b> Sexual Harrasment </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_FGW & "] </b> Feeling Guilty / Worthless </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_ADSAI & "] </b> Addiction (Drug, Alcohol, Internet) </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_CG & "] </b> Career Guide </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_RWF & "] </b> Relationships With Friend </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_SD & "] </b> Sadness </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_BC & "] </b> Behavioral Changes </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_VL & "] </b> Vandelism </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_SL & "] </b> Stealling </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_RL & "] </b> Restless </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_DAS & "] </b> Drug Abuse </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_ACWL & "] </b> Apetite Change & Weight Loss </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_HS & "] </b> Homesick </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_PN & "] </b> Paranoid </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_SHA & "] </b> Self-harm Attitude </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_AD & "] </b> Adjustment </td>
                                            <td style='width:25%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_OC & "] </b> Over Confidence </td>
                                            <td colspan='2' style='width:50%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_AT & "] </b> Attachment </td>
                                        </tr>
                                        <tr style='width:100%;'>
                                            <td colspan='4' style='width:100%;border: 1px solid black;font-family:BankFuturistic; font-size:0.6875em; padding-bottom:2vh'> &nbsp; <b> [" & C_Other & "] </b> Others : <p> " & C_Other_Details & " </p> </td>
                                        </tr>
                                    </table>
                                    </div>")

            End If

        End If


        Prints.AppendLine(" </div> </div>")

        Prints.AppendLine("<script type='text/javascript'>  var divToPrint=document.getElementById('dataCounselling'); newWin=window.open();newWin.document.write(divToPrint.outerHTML); newWin.print(); newWin.close()</script>")

        ''print
        Page.ClientScript.RegisterStartupScript([GetType](), "onClick", Prints.ToString())
    End Sub

    Protected Sub ShowMessage(Message As String, type As MessageType)
        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), System.Guid.NewGuid().ToString(), "ShowMessage('" & Message & "','" & type.ToString() & "');", True)
    End Sub

    Public Enum MessageType
        Success
        [Error]
    End Enum

End Class