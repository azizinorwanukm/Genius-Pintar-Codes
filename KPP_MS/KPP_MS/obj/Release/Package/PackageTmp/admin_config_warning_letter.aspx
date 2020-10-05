<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_config_warning_letter.aspx.vb" Inherits="KPP_MS.admin_config_warning_letter" %>

<%@ Register Src="~/commoncontrol/Disiplin_warning_letter.ascx" TagPrefix="uc1" TagName="Disiplin_warning_letter" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type ="text/javascript">

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
    <uc1:Disiplin_warning_letter runat="server" ID="Disiplin_warning_letter" />
    <div class="messagealert" id="alert_container" style="text-align: center"></div>
</asp:Content>