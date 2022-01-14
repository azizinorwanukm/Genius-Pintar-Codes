<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penjaga.Master" CodeBehind="penjaga_update_profile.aspx.vb" Inherits="KPP_SYS.penjaga_update_profile" %>

<%@ Register Src="~/commoncontrol/parent_studentDetail.ascx" TagPrefix="uc1" TagName="parent_studentDetail" %>
<%@ Register Src="~/commoncontrol/parent_guardian1Detail.ascx" TagPrefix="uc1" TagName="parent_guardian1Detail" %>
<%@ Register Src="~/commoncontrol/parent_guardian2Detail.ascx" TagPrefix="uc1" TagName="parent_guardian2Detail" %>



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

         $(document).ready(function () {
            var parent = document.getElementById('<%=Hidden_Parent.ClientID %>').value;

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

    <uc1:parent_studentDetail runat="server" id="parent_studentDetail" />

    <div id="P1" style="display: none">
    <uc1:parent_guardian1Detail runat="server" id="parent_guardian1Detail" />
        </div>

    <div id="P2" style="display: none">
    <uc1:parent_guardian2Detail runat="server" id="parent_guardian2Detail" />
        </div>

    <asp:HiddenField ID="Hidden_Parent" runat="server" />
</asp:Content>
