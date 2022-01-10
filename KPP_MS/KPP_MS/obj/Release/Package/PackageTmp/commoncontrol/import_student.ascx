﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="import_student.ascx.vb" Inherits="KPP_MS.import_student" %>

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

<div class="import" style="width: 100%; background-color: #f2f2f2; text-align: left; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; text-align:center">Import Data</p>

    <div class="w3-text-black" style="text-align: left; padding-left: 23px; margin-top: 10px; margin-bottom: 10px">
        <p>Please used this format before import :</p>
        <button id="BtnDownload" runat="server" class="btn btn-info" style="background-color: #087c35; border-radius: 25px;" title="Print"> Excel Format &#160; <i class="fa fa-file-excel-o w3-large w3-text-white"></i></button>
    </div>

    <div class="w3-text-black" style="text-align: left; padding-left: 23px; margin-bottom: 10px">
        <asp:Label CssClass="Label" runat="server"> Select Excel File to Import:</asp:Label>
        <asp:FileUpload ID="FlUploadcsv" runat="server" class="btn btn-info  ddl" />
        <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed" ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator>
    </div>

    <div class="w3-text-black" style="text-align: left; padding-left: 23px; margin-bottom: 10px; display:inline-block">
        <button id="BtnUploaded" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Import File"> Import Student & Subject &#160; <i class="fa fa-plus-circle w3-large w3-text-white"></i></button>
        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        <div class="info" id="divMsg" runat="server"></div>
    </div>


    <div class="w3-text-black" style="text-align: left; padding-left: 23px; margin-bottom: 10px; display:inline-block">
        <button id="BtnUploadedStudentOnly" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Import File"> Import Student Only  &#160; <i class="fa fa-plus-circle w3-large w3-text-white"></i></button>
    </div>

</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div> 