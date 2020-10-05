<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_konfigurasi.aspx.vb" Inherits="KPP_MS.admin_konfigurasi" %>

<%@ Register Src="commoncontrol/configuration_setting.ascx" TagName="configuration_setting" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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

    <script type="text/javascript">
        function management_info() {
            if (document.getElementById("manage_info").value == 0) {
                document.getElementById("setting_info").style.display = "block";
                document.getElementById("manage_info").value = 1;
            }

            else if (document.getElementById("manage_info").value == 1) {
                document.getElementById("setting_info").style.display = "none";
                document.getElementById("manage_info").value = 0;
            }
        }
    </script>

    <uc1:configuration_setting ID="configuration_setting1" runat="server" />

    <div class="messagealert" id="alert_container" style="text-align: center"></div>
</asp:Content>
