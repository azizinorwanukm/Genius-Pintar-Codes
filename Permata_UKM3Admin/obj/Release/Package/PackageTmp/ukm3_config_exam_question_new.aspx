<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_config_exam_question_new.aspx.vb" Inherits="permatapintar.ukm3_config_exam_question_new" %>

<%@ Register Src="~/commoncontrol/ukm3_config_exam_question_create.ascx" TagPrefix="uc1" TagName="ukm3_config_exam_question_create" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
  

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

    <uc1:ukm3_config_exam_question_create runat="server" id="ukm3_config_exam_question_create" />
    <div class="messagealert" id="alert_container" style="text-align: center"></div> 

</asp:Content>
