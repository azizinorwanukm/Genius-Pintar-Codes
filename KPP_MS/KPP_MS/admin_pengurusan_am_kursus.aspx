<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_pengurusan_am_kursus.aspx.vb" Inherits="KPP_MS.admin_pengurusan_am_kursus" %>

<%@ Register Src="commoncontrol/course_List_Table.ascx" TagName="course_List_Table" TagPrefix="uc2" %>

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

    <div id="editCourse_info" style="width: 100%; background-color: #f2f2f2; text-align: center">
        <uc2:course_List_Table ID="course_List_Table" runat="server" />

        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; text-align: left; padding-left: 23px">
            <button id="btnRegNewCourse" runat="server" type="button" class="btn btn-info" style="background-color: #009900; border-radius: 25px;"> Add Course &#160;<i class="fa fa-plus-circle w3-large w3-text-white"></i></button>
        </div>
    </div>
    </div>


    <div class="messagealert" id="alert_container" style="text-align: center"></div>
</asp:Content>
