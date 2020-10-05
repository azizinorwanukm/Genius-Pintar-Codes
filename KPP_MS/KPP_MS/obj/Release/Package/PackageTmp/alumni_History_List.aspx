<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="alumni_History_List.aspx.vb" Inherits="KPP_MS.alumni_History_List1" %>


<%@ Register Src="~/commoncontrol/alumni_student_Detail.ascx" TagPrefix="uc1" TagName="student_DetailAdmin" %>
<%@ Register Src="~/commoncontrol/student_Ukm1_History.ascx" TagPrefix="uc1" TagName="student_Ukm1_History" %>
<%@ Register Src="~/commoncontrol/student_Ukm2_History.ascx" TagPrefix="uc1" TagName="student_Ukm2_History" %>
<%@ Register Src="~/commoncontrol/student_Ppcs_History.ascx" TagPrefix="uc1" TagName="student_Ppcs_History" %>
<%@ Register Src="~/commoncontrol/student_Ukm3_History.ascx" TagPrefix="uc1" TagName="student_Ukm3_History" %>
<%@ Register Src="~/commoncontrol/student_Pcis_History.ascx" TagPrefix="uc1" TagName="student_Pcis_History" %>
<%@ Register Src="~/commoncontrol/alumni_Student_Status.ascx" TagPrefix="uc1" TagName="alumni_Student_Status" %>
<%@ Register Src="~/commoncontrol/alumni_Student_EduBack.ascx" TagPrefix="uc1" TagName="alumni_Student_EduBack" %>
<%@ Register Src="~/commoncontrol/alumni_student_workStatus.ascx" TagPrefix="uc1" TagName="alumni_student_workStatus" %>






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

        function student_status() {
            if (document.getElementById("pelajar_status").value == 0) {
                document.getElementById("std_status").style.display = "block";
                document.getElementById("pelajar_status").value = 1;
            }

            else if (document.getElementById("pelajar_status").value == 1) {
                document.getElementById("std_status").style.display = "none";
                document.getElementById("pelajar_status").value = 0;
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
        function student_status() {
             if (document.getElementById("pelajar_personalStatus").value == 0) {
                document.getElementById("personal_status").style.display = "block";
                document.getElementById("pelajar_personalStatus").value = 1;
            }

            else if (document.getElementById("pelajar_personalStatus").value == 1) {
                document.getElementById("personal_status").style.display = "none";
                document.getElementById("pelajar_personalStatus").value = 0;
            }
        }
        function student_workStatus() {
             if (document.getElementById("pelajar_kerja").value == 0) {
                document.getElementById("work_status").style.display = "block";
                document.getElementById("pelajar_kerja").value = 1;
            }

            else if (document.getElementById("pelajar_kerja").value == 1) {
                document.getElementById("work_status").style.display = "none";
                document.getElementById("pelajar_kerja").value = 0;
            }
        }
        function student_eduback() {
             if (document.getElementById("pelajar_EduBack").value == 0) {
                document.getElementById("education_Back").style.display = "block";
                document.getElementById("pelajar_EduBack").value = 1;
            }

            else if (document.getElementById("pelajar_EduBack").value == 1) {
                document.getElementById("education_Back").style.display = "none";
                document.getElementById("pelajar_EduBack").value = 0;
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
    
    <uc1:alumni_Student_Status runat="server" ID="alumni_Student_Status" />

    <uc1:student_Ukm1_History runat="server" ID="student_Ukm1_History" />

    <uc1:student_Ukm2_History runat="server" ID="student_Ukm2_History" />

    <uc1:student_Ppcs_History runat="server" ID="student_Ppcs_History" />

    <uc1:student_Ukm3_History runat="server" id="student_Ukm3_History" />

    <uc1:student_Pcis_History runat="server" id="student_Pcis_History" />

    <uc1:alumni_Student_EduBack runat="server" id="alumni_Student_EduBack" />
    
    <uc1:alumni_student_workStatus runat="server" id="alumni_student_workStatus" />

    <div class="messagealert" id="alert_container" style="text-align: center"></div>
</asp:Content>
