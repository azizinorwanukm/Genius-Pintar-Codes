<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_update_profile.aspx.vb" Inherits="KPP_SYS.pelajar_update_profile" %>

<%@ Register Src="~/commoncontrol/student_Detail.ascx" TagPrefix="uc1" TagName="student_Detail" %>
<%@ Register Src="~/commoncontrol/guardian1_DetailStd.ascx" TagPrefix="uc1" TagName="guardian1_DetailStd" %>
<%@ Register Src="~/commoncontrol/guardian2_DetailStd.ascx" TagPrefix="uc1" TagName="guardian2_DetailStd" %>



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

        function examination_info() {
            if (document.getElementById("exam_info").value == 0) {
                document.getElementById("exm_info").style.display = "block";
                document.getElementById("exam_info").value = 1;
            }

            else if (document.getElementById("exam_info").value == 1) {
                document.getElementById("exm_info").style.display = "none";
                document.getElementById("exam_info").value = 0;
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

    <uc1:student_Detail runat="server" ID="student_Detail" />
    <uc1:guardian1_DetailStd runat="server" id="guardian1_DetailStd" />
    <uc1:guardian2_DetailStd runat="server" id="guardian2_DetailStd" />
</asp:Content>
