﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_password_baru.aspx.vb" Inherits="KPP_SYS.pelajar_password_baru" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>

    <style type="text/css">
        .bImage {
            background-image: url('img/front.jpg');
            height: 530px;
            /* Center and scale the image nicely */
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }

        #front_pswd {
            position: absolute;
            width: 300px;
            height: 200px;
            top: 35%;
            left: 57%;
            margin: -100px 0 0 -150px;
            opacity: 0.75;
        }
    </style>

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

    <div class="bImage" style="width: 100%; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
        <div id="front_pswd" style="background-color: #b3b3b3">
            <div class="row" style="background-color: #b3b3b3; text-align: left; padding-left: 23px">
                <br />
                <img src="img/permatapintar2.jpg" width="270" height="140" class="w3-circle" />
                <br />
                <asp:Label runat="server"> Username (MYKAD) : </asp:Label>
                <asp:TextBox class="form-control w3-text-black" ID="txtloginUsername" Style="width: 90%" runat="server"></asp:TextBox>
                <p></p>
                <asp:Label runat="server"> Password : </asp:Label>
                <asp:TextBox type="password" class="form-control w3-text-black" ID="txtloginPassword" Style="width: 90%" runat="server" placeholder="Enter Old Password"></asp:TextBox>
                <p></p>
                <asp:Label runat="server"> New Password : </asp:Label>
                <asp:TextBox type="password" class="form-control w3-text-black" ID="txtnewPassword" Style="width: 90%" runat="server" placeholder="Enter New Password"></asp:TextBox>
            </div>
            <div class="row" style="background-color: #b3b3b3; text-align: center">
                <p></p>
                <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
                <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
            </div>
        </div>
    </div>

    <div class="messagealert" id="alert_container" style="text-align: center"></div>
</asp:Content>
