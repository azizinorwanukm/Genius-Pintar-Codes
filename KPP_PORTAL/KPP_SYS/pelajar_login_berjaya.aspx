<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_login_berjaya.aspx.vb" Inherits="KPP_SYS.pelajar_login_berjaya" %>

<%@ Register Src="~/commoncontrol/student_homepage.ascx" TagPrefix="uc1" TagName="student_homepage" %>
<%--<%@ Register Src="~/commoncontrol/student_guardian_one.ascx" TagPrefix="uc1" TagName="student_guardian_one" %>
<%@ Register Src="~/commoncontrol/student_course_information.ascx" TagPrefix="uc1" TagName="student_course_information" %>
<%@ Register Src="~/commoncontrol/student_guardian_two.ascx" TagPrefix="uc1" TagName="student_guardian_two" %>
<%@ Register Src="~/commoncontrol/student_hostel_information.ascx" TagPrefix="uc1" TagName="student_hostel_information" %>--%>
<%--<%@ Register Src="~/commoncontrol/student_payment_information.ascx" TagPrefix="uc1" TagName="student_payment_information" %>--%>
<%--<%@ Register Src="~/commoncontrol/student_examData.ascx" TagPrefix="uc1" TagName="student_examData" %>
<%@ Register Src="~/commoncontrol/student_dicipline.ascx" TagPrefix="uc1" TagName="student_dicipline" %>
<%@ Register Src="~/commoncontrol/student_cocurricular_information.ascx" TagPrefix="uc1" TagName="student_cocurricular_information" %>--%>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


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

        //function payment_info() {
        //    if (document.getElementById("bayaran_info").value == 0) {
        //        document.getElementById("payment_info").style.display = "block";
        //        document.getElementById("bayaran_info").value = 1;
        //    }

        //    else if (document.getElementById("bayaran_info").value == 1) {
        //        document.getElementById("payment_info").style.display = "none";
        //        document.getElementById("bayaran_info").value = 0;
        //    }
        //}

        function dicipline_info() {
            if (document.getElementById("dicHidden").value == "0") {
                document.getElementById("dic_info").style.display = "block";
                document.getElementById("dicHidden").value = "1";

            }

            else if (document.getElementById("dicHidden").value == "1") {
                document.getElementById("dic_info").style.display = "none";
                document.getElementById("dicHidden").value = "0";

            }
        }

        function cocurricular_info() {
            if (document.getElementById("cocurricular_data").value == "0") {
                document.getElementById("koko_info").style.display = "block";
                document.getElementById("cocurricular_data").value = "1";

            }

            else if (document.getElementById("cocurricular_data").value == "1") {
                document.getElementById("koko_info").style.display = "none";
                document.getElementById("cocurricular_data").value = "0";

            }
        }

        $(document).ready(function () {
            var data = document.getElementById('<%=Hidden_Data.ClientID %>').value;

            if (data == "OFF") {
                document.getElementById("Exam").style.display = "none";
            }

            else if (data == "ON") {
                document.getElementById("Exam").style.display = "block";
            }

        });
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

    <uc1:student_homepage runat="server" ID="student_homepage" />

<%--    <uc1:student_guardian_one runat="server" ID="student_guardian_one" />

    <uc1:student_guardian_two runat="server" ID="student_guardian_two" />

    <uc1:student_course_information runat="server" ID="student_course_information" />

    <uc1:student_cocurricular_information runat="server" ID="student_cocurricular_information" />

    <uc1:student_hostel_information runat="server" ID="student_hostel_information" />

    <uc1:student_examData runat="server" ID="student_examData" />--%>

    <%--    <uc1:student_payment_information runat="server" ID="student_payment_information" />--%>

    <%--<uc1:student_dicipline runat="server" ID="student_dicipline" />--%>

    <asp:HiddenField ID="Hidden_Data" runat="server" />
</asp:Content>
