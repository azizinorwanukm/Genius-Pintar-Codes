<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="KPP_MS.WebForm1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Login Form</title>

    <link rel="stylesheet" href="css/side_bar_frame.css">
    <link rel="stylesheet" href="css/font_montserrat.css">
    <link rel="stylesheet" href="css/font_awesome.css">
    <link rel="stylesheet" href="css/plugin_export.css" type="text/css" media="all" />
    <link rel="stylesheet" href="css/jquerytable.min.css">
    <script src="js/jquery.js"></script>
    <script src="js/jquerytable.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.10.0/js/bootstrap-select.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.10.0/css/bootstrap-select.min.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <link href='https://fonts.googleapis.com/css?family=Roboto+Slab:400,100,300,700|Lato:400,100,300,700,900' rel='stylesheet' type='text/css'>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <link rel="stylesheet" href="css/w3.css">

    <link rel="stylesheet" href="css/animate.css">
    <!-- Custom Stylesheet -->
    <link rel="stylesheet" href="css/style.css">

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

    <style>
        body, html {
            height: 100%;
            margin: 0;
        }

        .backImage {
            background-image: url("img/front.jpg");
            height: 100%;
            /* Center and scale the image nicely */
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }

       

        .messagealert {
            width: 40%;
            position: fixed;
            bottom: 25px;
            right: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }

        @media screen and (min-device-width: 801px) {

            #front_page {
                position: absolute;
                width: 300px;
                height: 200px;
                top: 35%;
                left: 50%;
                margin: -100px 0 0 -150px;
                opacity: 0.75;
            }

            #btnLogin {
                position: center;
            }
        }

        @media screen and (max-device-width: 800px) {

            #front_page {
                position: absolute;
                width: 300px;
                height: 170px;
                top: 40%;
                left: 50%;
                margin: -100px 0 0 -150px;
                opacity: 0.75;
            }

            #btnLogin {
                position: center;
            }
        }
    </style>

</head>

<body>
    <div class="backImage">
        <form id="form1" runat="server">
            <div id="front_page" style="background-color: #b3b3b3">
                <div class="row" style="background-color: #b3b3b3; text-align: left; padding-left: 23px">
                    <br />
                    <img src="img/permatapintar2.jpg" width="270" height="140" class="w3-circle" />
                    <br />
                    <asp:Label runat="server"> Username : </asp:Label>
                    <asp:TextBox class="form-control w3-text-black" ID="txtloginUsername" Style="width: 90%" runat="server"></asp:TextBox>
                    <p></p>
                    <asp:Label runat="server"> Password : </asp:Label>
                    <asp:TextBox type="password" class="form-control w3-text-black" ID="txtloginPassword" Style="width: 90%" runat="server"></asp:TextBox>
                </div>
                <div class="row" style="background-color: #b3b3b3; text-align: center">
                    <p></p>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" Style="text-align: center" CssClass="btn btn-success" />
                </div>
            </div>
            <div class="messagealert" id="alert_container" style="text-align: center"></div>
        </form>
    </div>
</body>

</html>
