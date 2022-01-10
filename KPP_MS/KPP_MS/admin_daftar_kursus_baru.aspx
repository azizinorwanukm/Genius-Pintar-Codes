<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_daftar_kursus_baru.aspx.vb" Inherits="KPP_MS.admin_daftar_kursus_baru" %>

<%@ Register Src="~/commoncontrol/course_Transfer.ascx" TagPrefix="uc1" TagName="course_Transfer" %>
<%@ Register Src="~/commoncontrol/course_Create.ascx" TagPrefix="uc1" TagName="course_Create" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function student_transfer() {
            if (document.getElementById("pelajarTransfer_info").value == 0) {
                document.getElementById("transfer_info").style.display = "block";
                document.getElementById("pelajarTransfer_info").value = 1;
            }

            else if (document.getElementById("pelajarTransfer_info").value == 1) {
                document.getElementById("transfer_info").style.display = "none";
                document.getElementById("pelajarTransfer_info").value = 0;
            }
        }

        function student_create() {
            if (document.getElementById("pelajarCreate_info").value == 0) {
                document.getElementById("create_info").style.display = "block";
                document.getElementById("pelajarCreate_info").value = 1;
            }

            else if (document.getElementById("pelajarCreate_info").value == 1) {
                document.getElementById("create_info").style.display = "none";
                document.getElementById("pelajarCreate_info").value = 0;
            }
        }
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

    <uc1:course_Create runat="server" ID="course_Create" />

<%--    <br />

    <uc1:course_Transfer runat="server" ID="course_Transfer" />--%>

    <div class="messagealert" id="alert_container" style="text-align: center"></div>
</asp:Content>
