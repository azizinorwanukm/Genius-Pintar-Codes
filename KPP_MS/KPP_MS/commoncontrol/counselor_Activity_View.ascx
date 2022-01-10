<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="counselor_Activity_View.ascx.vb" Inherits="KPP_MS.counselor_Activity_View" %>


<script type="text/javascript">
    function ShowMessage(message, messagetype) {
        var cssclass;
        switch (messagetype) {
            case 'Success':
                cssclass = 'alert-success'
                break;
            case 'Error':
                cssclass = 'alert-danger'
                break;
            default:
                cssclass = 'alert-info'
        }
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; text-align:left -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        setTimeout(function () {
            $("#alert_div").fadeTo(5000, 500).slideUp(500, function () {
                $("#alert_div").remove();
            });
        }, 3000);
    }
</script>

<style>
    .messagealert {
        width: 40%;
        position: fixed;
        bottom: 25px;
        right: 0px;
        z-index: 100000;
        padding: 0;
        font-size: 15px;
    }

    .sc3::-webkit-scrollbar {
        height: 10px;
    }

    .sc3::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc3::-webkit-scrollbar-thumb {
        background-color: #929B9E;
        border-radius: 3px;
    }

    .sc4::-webkit-scrollbar {
        width: 10px;
    }

    .sc4::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc4::-webkit-scrollbar-thumb {
        background-color: #929B9E;
    }
</style>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp Counselor  &nbsp / &nbsp Counselling Activity &nbsp / &nbsp 
        <asp:HyperLink runat="server" ID="previousPage"> Student Counselor </asp:HyperLink> &nbsp / &nbsp I Need Counselling
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 76vh" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Student Name : </asp:Label>
            <asp:TextBox runat="server" ID="txtstudentName_SCINC" Style="width: 35vw" CssClass="textboxcss font" Enabled="false"></asp:TextBox>
        </div>

        <br />
        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Class : </asp:Label>
            <asp:TextBox runat="server" ID="txtClass_SCINC" Style="width: 35vw" CssClass="textboxcss font" Enabled="false"></asp:TextBox>
        </div>

        <br />
        <br />
        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselor Name : </asp:Label>
            <asp:DropDownList ID="ddl_INC_CN" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />
        <br />

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <%--adjust column width--%>
                <td style="width: 13vw">
                    <p></p>
                    <asp:CheckBox ID="CB_MT" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Motivation </asp:Label>
                </td>
                <td style="width: 9vw">
                    <p></p>
                    <asp:CheckBox ID="CB_PL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Plagiarism </asp:Label>
                </td>
                <td style="width: 17vw">
                    <p></p>
                    <asp:CheckBox ID="CB_CA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Class Attendance </asp:Label>
                </td>
                <td style="width: 15vw">
                    <p></p>
                    <asp:CheckBox ID="CB_IA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Inattentive </asp:Label>
                </td>
                <td style="width: 13vw">
                    <p></p>
                    <asp:CheckBox ID="CB_AP" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Academic Performance </asp:Label>
                </td>
                <td style="width: 10vw">
                    <p></p>
                    <asp:CheckBox ID="CB_TR" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Tardy </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_TM" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Time Management </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_CG" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Career Guide </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Vandelisme </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_ACWL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Apetite Change & Weight Loss </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_AD" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Adjustment </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_DA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Disrespectful </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_CL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Cheating & Lying </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_IS" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Impulsive </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_BY" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Bully </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_AY" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Apathy </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SS" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Social Skill </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SH" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Sexual Harrasment </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_RWF" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Relationship With Friend </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Stealling </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_HS" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Homesick </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_OC" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Over Confidence </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_HA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Hyperactive </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_EP" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Easy Panic </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_WD" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Withdrawn </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_AX" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Anxiety </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_RG" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Rage </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_DP" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Depress </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_FGW" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Feeling Guilty / Worthless </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SD" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Sadness </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_RL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Restless </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_PN" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Paranoid </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_PH" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Personal Hygience </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SCD" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Suicidal Desires </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_MF" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Missing Functionality </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_PNM" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Perfectionism </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Very Aggresive </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_IM" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Insomnia </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_ADSAI" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Addiction (Drug,Sex,Alcohol,Internet) </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_BC" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Behavioral Changes </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_DAS" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Drug Abuse </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SHA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Self-harm Attitude </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_AT" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Attachment </asp:Label>
                </td>
                <td colspan="5">
                    <p></p>
                    <asp:CheckBox ID="CB_Other" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Others : </asp:Label>
                    <asp:TextBox runat="server" ID="txt_CBOther" Style="width: 38vw" CssClass="textboxcss font"></asp:TextBox>
                </td>
            </tr>
        </table>

        <br />

        <table id="Table_CAD_INC_SUG" runat="server" class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw;">
            <tr>
                <td rowspan="3">
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Suggestion </asp:Label>
                </td>
                <td  rowspan="3">
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRINC_CallingSession" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Client will be called back for </asp:Label>
                    <asp:TextBox runat="server" ID="txt_CallingSession_INC" Style="width: 3vw" CssClass="textboxcss"></asp:TextBox>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> session </asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRINC_NewCounselor" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Client will be referred to </asp:Label>
                    <asp:DropDownList ID="ddl_NewCounselor_INC" runat="server" CssClass=" btn btn-default font " Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRINC_CounsellingEnd" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselling session for this client is ended </asp:Label>
                </td>
            </tr>
        </table>

        <br />
        <br />

        <button id="btn_updatecounseling_VCRCGPA" runat="server" class="btn btn-success" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Update Counseling Session </button>
        <button id="btn_Exportcounseling_VCRCGPA" runat="server" class="btn btn-info" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Print Counseling Session </button>

        <br />
        <br />
    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
