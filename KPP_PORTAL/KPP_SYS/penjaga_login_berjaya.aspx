<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penjaga.Master" CodeBehind="penjaga_login_berjaya.aspx.vb" Inherits="KPP_SYS.penjaga_login_berjaya" %>

<%@ Register Src="~/commoncontrol/parent_studenthomepage.ascx" TagPrefix="uc1" TagName="parent_studenthomepage" %>
<%--<%@ Register Src="~/commoncontrol/parent_guardianOne.ascx" TagPrefix="uc1" TagName="parent_guardianOne" %>
<%@ Register Src="~/commoncontrol/parent_guardianTwo.ascx" TagPrefix="uc1" TagName="parent_guardianTwo" %>
<%@ Register Src="~/commoncontrol/parent_studentCourse.ascx" TagPrefix="uc1" TagName="parent_studentCourse" %>
<%@ Register Src="~/commoncontrol/parent_studentHostel.ascx" TagPrefix="uc1" TagName="parent_studentHostel" %>
<%@ Register Src="~/commoncontrol/parent_studentExam.ascx" TagPrefix="uc1" TagName="parent_studentExam" %>--%>
<%--<%@ Register Src="~/commoncontrol/parent_student_paymentInformation.ascx" TagPrefix="uc1" TagName="parent_student_paymentInformation" %>--%>
<%--<%@ Register Src="~/commoncontrol/parent_studentDicipline.ascx" TagPrefix="uc1" TagName="parent_studentDicipline" %>
<%@ Register Src="~/commoncontrol/parent_studentCocurricular.ascx" TagPrefix="uc1" TagName="parent_studentCocurricular" %>--%>


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

        function payment_info() {
            if (document.getElementById("bayaran_info").value == 0) {
                document.getElementById("payment_info").style.display = "block";
                document.getElementById("bayaran_info").value = 1;
            }

            else if (document.getElementById("bayaran_info").value == 1) {
                document.getElementById("payment_info").style.display = "none";
                document.getElementById("bayaran_info").value = 0;
            }
        }

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
            var parent = document.getElementById('<%=Hidden_Parent.ClientID %>').value;

            if (data == "OFF") {
                document.getElementById("Exam").style.display = "none";
            }

            else if (data == "ON") {
                document.getElementById("Exam").style.display = "block";
            }


            if (parent == "P1") {
                document.getElementById("P1").style.display = "block";
                document.getElementById("P2").style.display = "none";
            }

            else if (parent == "P2") {
                document.getElementById("P2").style.display = "block";
                document.getElementById("P1").style.display = "none";
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

    <uc1:parent_studenthomepage runat="server" ID="parent_studenthomepage" />

<%--    <div id="P1" style="display: none">
        <uc1:parent_guardianOne runat="server" ID="parent_guardianOne" />
    </div>

    <div id="P2" style="display: none">
        <uc1:parent_guardianTwo runat="server" ID="parent_guardianTwo" />
    </div>

    <uc1:parent_studentCourse runat="server" ID="parent_studentCourse" />

    <uc1:parent_studentCocurricular runat="server" id="parent_studentCocurricular" />

    <uc1:parent_studentHostel runat="server" ID="parent_studentHostel" />

        <uc1:parent_studentExam runat="server" ID="parent_studentExam" />--%>

<%--    <uc1:parent_student_paymentInformation runat="server" ID="parent_student_paymentInformation" />--%>

    <%--<uc1:parent_studentDicipline runat="server" ID="parent_studentDicipline" />--%>

    <asp:HiddenField ID="Hidden_Data" runat="server" />
    <asp:HiddenField ID="Hidden_Parent" runat="server" />

</asp:Content>
