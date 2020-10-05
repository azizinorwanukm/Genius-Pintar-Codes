<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_pelajar_detail.aspx.vb" Inherits="KPP_MS.pengajar_pelajar_detail" %>

<%--<%@ Register Src="~/commoncontrol/student_homepage.ascx" TagPrefix="uc1" TagName="student_homepage" %>
<%@ Register Src="~/commoncontrol/student_guardian_one.ascx" TagPrefix="uc1" TagName="student_guardian_one" %>
<%@ Register Src="~/commoncontrol/student_guardian_two.ascx" TagPrefix="uc1" TagName="student_guardian_two" %>
<%@ Register Src="~/commoncontrol/student_course_information.ascx" TagPrefix="uc1" TagName="student_course_information" %>
<%@ Register Src="~/commoncontrol/student_hostel_information.ascx" TagPrefix="uc1" TagName="student_hostel_information" %>
<%@ Register Src="~/commoncontrol/student_examData.ascx" TagPrefix="uc1" TagName="student_examData" %>--%>

<%@ Register Src="commoncontrol/hostel_Detail.ascx" TagName="hostel_Detail" TagPrefix="uc3" %>

<%@ Register Src="commoncontrol/student_CourseList.ascx" TagName="student_CourseList" TagPrefix="uc4" %>
<%@ Register Src="~/commoncontrol/guardian1_Detail.ascx" TagPrefix="uc2" TagName="guardian1_Detail" %>
<%@ Register Src="~/commoncontrol/guardian2_Detail.ascx" TagPrefix="uc3" TagName="guardian2_Detail" %>
<%@ Register Src="~/commoncontrol/student_ExamList.ascx" TagPrefix="uc1" TagName="student_ExamList" %>
<%@ Register Src="~/commoncontrol/student_DetailAdmin.ascx" TagPrefix="uc1" TagName="student_DetailAdmin" %>
<%@ Register Src="~/commoncontrol/student_Ukm1_History.ascx" TagPrefix="uc1" TagName="student_Ukm1_History" %>
<%@ Register Src="~/commoncontrol/student_Ukm2_History.ascx" TagPrefix="uc1" TagName="student_Ukm2_History" %>
<%@ Register Src="~/commoncontrol/student_Ppcs_History.ascx" TagPrefix="uc1" TagName="student_Ppcs_History" %>
<%@ Register Src="~/commoncontrol/student_Ukm3_History.ascx" TagPrefix="uc1" TagName="student_Ukm3_History" %>
<%@ Register Src="~/commoncontrol/student_Pcis_History.ascx" TagPrefix="uc1" TagName="student_Pcis_History" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function student_info() {
            if (document.getElementById("pelajar_info").value == 0) {
                document.getElementById("std_info").style.display = "block";
                document.getElementById("pelajar_info").value = 1;
            }

            else if (document.getElementById("pelajar_info").value == 1) {
                document.getElementById("std_info").style.display = "none";
                document.getElementById("pelajar_info").value = 0;
            }
        }

        function guardian1_info() {
            if (document.getElementById("penjaga1_info").value == 0) {
                document.getElementById("guard1_info").style.display = "block";
                document.getElementById("penjaga1_info").value = 1;
            }

            else if (document.getElementById("penjaga1_info").value == 1) {
                document.getElementById("guard1_info").style.display = "none";
                document.getElementById("penjaga1_info").value = 0;
            }
        }

        function guardian2_info() {
            if (document.getElementById("penjaga2_info").value == 0) {
                document.getElementById("guard2_info").style.display = "block";
                document.getElementById("penjaga2_info").value = 1;
            }

            else if (document.getElementById("penjaga2_info").value == 1) {
                document.getElementById("guard2_info").style.display = "none";
                document.getElementById("penjaga2_info").value = 0;
            }
        }

        function remarks_info() {
            if (document.getElementById("markah_info").value == 0) {
                document.getElementById("remarks_info").style.display = "block";
                document.getElementById("markah_info").value = 1;
            }

            else if (document.getElementById("markah_info").value == 1) {
                document.getElementById("remarks_info").style.display = "none";
                document.getElementById("markah_info").value = 0;
            }
        }

        function hostel_info() {
            if (document.getElementById("asrama_info").value == 0) {
                document.getElementById("hostel_info").style.display = "block";
                document.getElementById("asrama_info").value = 1;
            }

            else if (document.getElementById("asrama_info").value == 1) {
                document.getElementById("hostel_info").style.display = "none";
                document.getElementById("asrama_info").value = 0;
            }
        }

        function course_info() {
            if (document.getElementById("kursus_info").value == 0) {
                document.getElementById("course_info").style.display = "block";
                document.getElementById("kursus_info").value = 1;
            }

            else if (document.getElementById("kursus_info").value == 1) {
                document.getElementById("course_info").style.display = "none";
                document.getElementById("kursus_info").value = 0;
            }
        }

        function exam_info() {
            if (document.getElementById("periksa_info").value == 0) {
                document.getElementById("exam_info").style.display = "block";
                document.getElementById("periksa_info").value = 1;
            }

            else if (document.getElementById("periksa_info").value == 1) {
                document.getElementById("exam_info").style.display = "none";
                document.getElementById("periksa_info").value = 0;
            }
        }

        function student_Ukm1() {
            if (document.getElementById("UKM1_history").value == 0) {
                document.getElementById("std_ukm1").style.display = "block";
                document.getElementById("UKM1_history").value = 1;
            }

            else if (document.getElementById("UKM1_history").value == 1) {
                document.getElementById("std_ukm1").style.display = "none";
                document.getElementById("UKM1_history").value = 0;
            }
        }

        function student_Ukm2() {
            if (document.getElementById("UKM2_history").value == 0) {
                document.getElementById("std_ukm2").style.display = "block";
                document.getElementById("UKM2_history").value = 1;
            }

            else if (document.getElementById("UKM2_history").value == 1) {
                document.getElementById("std_ukm2").style.display = "none";
                document.getElementById("UKM2_history").value = 0;
            }
        }

        function student_Ppcs() {
            if (document.getElementById("PPCS_history").value == 0) {
                document.getElementById("std_ppcs").style.display = "block";
                document.getElementById("PPCS_history").value = 1;
            }

            else if (document.getElementById("PPCS_history").value == 1) {
                document.getElementById("std_ppcs").style.display = "none";
                document.getElementById("PPCS_history").value = 0;
            }
        }

        function student_Ukm3() {
            if (document.getElementById("UKM3_history").value == 0) {
                document.getElementById("std_ukm3").style.display = "block";
                document.getElementById("UKM3_history").value = 1;
            }

            else if (document.getElementById("UKM3_history").value == 1) {
                document.getElementById("std_ukm3").style.display = "none";
                document.getElementById("UKM3_history").value = 0;
            }
        }

        function student_Pcis() {
            if (document.getElementById("PCIS_history").value == 0) {
                document.getElementById("std_pcis").style.display = "block";
                document.getElementById("PCIS_history").value = 1;
            }

            else if (document.getElementById("PCIS_history").value == 1) {
                document.getElementById("std_pcis").style.display = "none";
                document.getElementById("PCIS_history").value = 0;
            }
        }
    </script>

    <script>
        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#blah').attr('src', e.target.result);
                }

                reader.readAsDataURL(input.files[0]);
            }
        }
        $("#uploadPhoto").change(function () {
            readURL(this);
        });
    </script>

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
    <uc1:student_DetailAdmin runat="server" ID="student_DetailAdmin" />

    <uc2:guardian1_Detail runat="server" ID="guardian1_Detail" />

    <uc3:guardian2_Detail runat="server" ID="guardian2_Detail" />

    <uc4:student_CourseList ID="student_CourseList" runat="server" />

    <uc1:student_ExamList runat="server" ID="student_ExamList" />

    <div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
        <button id="markah_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="remarks_info()" value="0">Student Remarks<i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
        <div style="display: none;" id="remarks_info">
            <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
                <table class="w3-text-black" style="border: 5px solid black">
                    <tr>
                        <td style="border: 5px solid black">2017 </td>
                        <td style="border: 5px solid black">Overall Remarks</td>
                    </tr>
                </table>
                <p class="w3-text-black">OR </p>
                <table class="w3-text-black" style="border: 5px solid black">
                    <tr>
                        <td style="border: 5px solid black">2016 </td>
                        <td style="border: 5px solid black">Remarks 1</td>
                        <td style="border: 5px solid black">Remarks 2</td>
                        <td style="border: 5px solid black">Remarks 3</td>
                        <td style="border: 5px solid black">Remarks 4</td>
                    </tr>
                    <tr>
                        <td style="border: 5px solid black">2015 </td>
                        <td style="border: 5px solid black">Remarks 1</td>
                        <td style="border: 5px solid black">Remarks 2</td>
                        <td style="border: 5px solid black">Remarks 3</td>
                        <td style="border: 5px solid black">Remarks 4</td>
                    </tr>
                </table>
            </div>
            <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
                <button id="btnRemarksUpdate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;"><i class="fa fa-upload w3-large w3-text-white"></i></button>
            </div>
            <p></p>
        </div>
    </div>
    <br />

    <uc3:hostel_Detail ID="hostel_Detail" runat="server" />

    <uc1:student_Ukm1_History runat="server" ID="student_Ukm1_History" />

    <uc1:student_Ukm2_History runat="server" ID="student_Ukm2_History" />

    <uc1:student_Ppcs_History runat="server" ID="student_Ppcs_History" />

    <uc1:student_Ukm3_History runat="server" id="student_Ukm3_History" />

    <uc1:student_Pcis_History runat="server" id="student_Pcis_History" />

    <div class="messagealert" id="alert_container" style="text-align: center"></div>

    <asp:HiddenField ID="Hidden_Data" runat="server" />
</asp:Content>
