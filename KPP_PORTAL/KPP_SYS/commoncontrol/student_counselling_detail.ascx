<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_counselling_detail.ascx.vb" Inherits="KPP_SYS.student_counselling_detail" %>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="textboxio/textboxio.js"></script>
    <script>
        $(function () {
            $('.datepicker').datepicker({ dateFormat: 'dd MM yy' }).val();
        });
    </script>
</head>

<style type="text/css">
    .CalendarCssClass {
        background-color: #990000;
        font-family: Century;
        text-transform: lowercase;
        width: 750px;
        border: 1px solid Olive;
    }
</style>

<script type="text/javascript">
    function date_Click() {
        if (divcalendar.style.display == "none")
            divcalendar.style.display = "";
        else
            divcalendar.style.display = "none";
    }
</script>

<style>
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
        height: 10px;
    }

    .sc4::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc4::-webkit-scrollbar-thumb {
        background-color: #929B9E;
    }
</style>

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

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh;" class="w3-card-2">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black font">
        Menu &nbsp; : &nbsp; Counselling &nbsp; / &nbsp; Counselling Information
         &nbsp; / &nbsp; 
        <asp:HyperLink runat="server" ID="previousPage"> View Counselling </asp:HyperLink>
        &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 76vh" class="sc4" id="V_CGPA" runat="server">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:TextBox runat="server" ID="txtstudentName_VCRCGPA" Style="width: 35vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
                </td>
                <td id="SE_Status_One" runat="server">
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Date </asp:Label>
                </td>
                <td id="SE_Status_Two" runat="server">
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td id="SE_Status_Three" runat="server">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtexamDate_VCRCGPA" Style="width: 15vw" CssClass="textboxcss datepicker" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Class </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="4">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtclassName_VCRCGPA" Style="width: 10vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr id="Exam_Show" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Examination </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="4">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtexamName_VCRCGPA" Style="width: 10vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr id="CGPA_Show" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> CGPA </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="4">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtcgpa_VCRCGPA" Style="width: 10vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr id="Case_Show" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Case </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="4">
                    <p></p>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtcase_VCRDI" Columns="100" Rows="3" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr id="Demerit_Schow" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Demerit Point </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="4">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtdp_VCRDI" Style="width: 5vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>
        </table>

        <br />

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Counseling Date </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="3">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtcounselingDate_VCRCGPA" Style="width: 10vw" CssClass="textboxcss datepicker" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Attendance </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddlattendance_VCRCGPA" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Counseling Time </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:TextBox runat="server" ID="txtstart_VCRCGPA" Style="width: 10vw" CssClass="textboxcss" placeholder=" EX : 07.00 PM"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp   - &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:TextBox runat="server" ID="txtend_VCRCGPA" Style="width: 10vw" CssClass="textboxcss" placeholder=" EX 08.00 PM"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Counseling Session </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="3">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtsession_VCRCGPA" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Client Classification </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="3">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtclientClassification_VCRCGPA" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Issues Type </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="3">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtissuesType_VCRCGPA" Style="width: 10vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>
        </table>

        <br />

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student Condition </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="3">
                    <p></p>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtstudentCondition_VCRCGPA" Columns="140" Rows="5" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student Background </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="3">
                    <p></p>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtstudentBackground_VCRCGPA" Columns="140" Rows="5" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td rowspan="3">
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Discussion Type </asp:Label>
                </td>
                <td rowspan="3">
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRCGPA_MB" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Build Relationship </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRCGPA_MT" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Taking Action </asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRCGPA_MDMM" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Exploring And Analyzing Problems </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRCGPA_MDMA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Discuss And Choose Alternatives </asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRCGPA_MPPM" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Identify The Cause Of The Problem </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRCGPA_MS" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> End Session </asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Issues Discussed </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="3">
                    <p></p>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtremark_VCRCGPA" Columns="140" Rows="5" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Action </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="3">
                    <p></p>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtaction_VCRCGPA" Columns="140" Rows="5" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td rowspan="3">
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Suggestion </asp:Label>
                </td>
                <td rowspan="3">
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRCGPA_CallingSession" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Client will be called back for </asp:Label>
                    <asp:TextBox runat="server" ID="txt_CallingSession" Style="width: 3vw" CssClass="textboxcss"></asp:TextBox>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> session </asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRCGPA_NewCounselor" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Client will be referred to </asp:Label>
                    <asp:DropDownList ID="ddl_NewCounselor" runat="server" AutoPostBack="true" CssClass=" btn btn-default font " Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VCRCGPA_CounsellingEnd" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselling session for this client is ended </asp:Label>
                </td>
            </tr>
        </table>

        <br />

        <button id="btn_updatecounseling_VCRCGPA" runat="server" class="btn btn-success" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Update Counseling Session </button>
        <button id="btn_Exportcounseling_VCRCGPA" runat="server" class="btn btn-info" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Print Counseling Session </button>

        <br />
        <br />
    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>