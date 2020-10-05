﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="Import_Student_Data_Temporary.aspx.vb" Inherits="KPP_MS.Import_Student_Data_Temporary" %>
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

    <div id="importClass" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c; margin-top: 10px">
        <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Import Class</p>
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">

            <div class="w3-text-black" style="text-align: left; padding-left: 23px; margin-bottom: 10px">
                <asp:label cssclass="Label" runat="server"> Select Excel File to Import:</asp:label>
                <asp:fileupload id="FlUploadcsv" runat="server" class="btn btn-info  ddl" />
                <asp:regularexpressionvalidator id="regexValidator" runat="server" errormessage="Only XLSX file are allowed" validationexpression="(.*\.([Xx][Ll][Ss][Xx])$)" controltovalidate="FlUploadcsv"></asp:regularexpressionvalidator>
            </div>

            <div class="w3-text-black" style="text-align: left; padding-left: 23px; margin-bottom: 10px">
                <button id="BtnUploaded" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Import File">Import &#160; <i class="fa fa-plus-circle w3-large w3-text-white"></i></button>
                <asp:label id="lblMsg" runat="server" text="" forecolor="Red"></asp:label>
                <div class="info" id="divMsg" runat="server"></div>
            </div>
        </div>
    </div>

    <div class="messagealert" id="alert_container" style="text-align: center"></div>

</asp:Content>
