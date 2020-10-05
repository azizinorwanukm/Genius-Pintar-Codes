<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_daftar_kelas.aspx.vb" Inherits="KPP_MS.admin_daftar_kelas" %>

<%@ Register Src="~/commoncontrol/class_Create.ascx" TagPrefix="uc1" TagName="class_Create" %>
<%@ Register Src="~/commoncontrol/import_class.ascx" TagPrefix="uc1" TagName="import_class" %>
<%@ Register Src="~/commoncontrol/class_transfer.ascx" TagPrefix="uc1" TagName="class_transfer" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

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

    <div id="newClass_info" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c; ">
        <uc1:class_Create runat="server" ID="class_Create" />
        <p></p>
    </div>

    <div id="importClass" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c; margin-top: 10px">
        <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Transfer Class</p>
        <uc1:class_transfer runat="server" id="class_transfer" />

        <%--<p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Import Class</p>
        <uc1:import_class runat="server" ID="import_class" />--%>
    </div>

    <div class="messagealert" id="alert_container" style="text-align: center"></div>

</asp:Content>
