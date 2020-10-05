<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_pengajar_penempatan_kelas.aspx.vb" Inherits="KPP_MS.admin_pengajar_penempatan_kelas" %>

<%@ Register Src="commoncontrol/lecturer_RegClass.ascx" TagName="lecturer_RegClass" TagPrefix="uc1" %>
<%@ Register Src="~/commoncontrol/import_lecturerPlacement.ascx" TagPrefix="uc1" TagName="import_lecturerPlacement" %>


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
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; text-align:left; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

            setTimeout(function () {
                $("#alert_div").fadeTo(5000, 500).slideUp(500, function () {
                    $("#alert_div").remove();
                });
            }, 3000);
        }
    </script>

    <div>
        <uc1:lecturer_RegClass ID="lecturer_RegClass" runat="server" />
    </div>

    <div id="importPenempatanKelas" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c; margin-top:10px">
        <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Import Lecturer Class Placement</p>
        <uc1:import_lecturerPlacement runat="server" id="import_lecturerPlacement" />
    </div>
    </div>

    <div class="messagealert" id="alert_container" style="text-align: center"></div>
</asp:Content>
