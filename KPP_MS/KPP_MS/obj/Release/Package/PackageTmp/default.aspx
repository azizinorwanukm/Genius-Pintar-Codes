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
    <link rel="stylesheet" href="css/font_awesome_470.min.css">
    <link rel="stylesheet" href="css/w3.css">
    <link rel="stylesheet" href="css/animate.css">
    <link rel="stylesheet" href="css/style.css">

    <link rel="stylesheet" href="css/jquerytable.min.css">
    <link rel="stylesheet" href="css/bootstrap_337.min.css">
    <link rel="stylesheet" href="css/bootstrap_select_1100.min.css">
    <link rel="stylesheet" href="css/bootstrap_335.min.css">

    <script src="js/jquery.js"></script>
    <script src="js/jquerytable.min.js"></script>
    <script src="js/jquery_211.min.js"></script>
    <script src="js/bootstrap_select_1100.min.js"></script>
    <script src="js/bootstrap_337.min.js"></script>
    <script src="js/jquery_321.min.js"></script>
    <script src="js/jquery_1113.min.js"></script>
    <script src="js/bootstrap_335.min.js"></script>

    <%-- <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.10.0/js/bootstrap-select.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.10.0/css/bootstrap-select.min.css" rel="stylesheet" />
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <link href='https://fonts.googleapis.com/css?family=Roboto+Slab:400,100,300,700|Lato:400,100,300,700,900' rel='stylesheet' type='text/css'>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>

    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>--%>

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
            background: linear-gradient(to bottom, #00203FFF 0%, #FFFFFF 100%);
            height: 100%;
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
                /*opacity: 0.75;*/
            }

            #btnLogin {
                position: center;
            }
        }

        @media screen and (max-device-width: 800px) {

            #front_page {
                position: absolute;
                width: 300px;
                height: 60vh;
                top: 40%;
                left: 50%;
                margin: -100px 0 0 -150px;
                overflow-y: scroll;
                /*opacity: 0.75;*/
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
            <div id="front_page">
                <div class="row" style="background-color: #FFFFFF; text-align: center;">
                    <table style="border: hidden; width: 100%">
                        <tr>
                            <td>
                                <button id="BtnLoginPGPN" runat="server" style="display: inline-block; font-size: 0.8vw; width: 100%">Genius Pintar</button></td>
                            <td>
                                <button id="BtnLoginAPP" runat="server" style="display: inline-block; font-size: 0.8vw; width: 100%">Akademik Pintar</button></td>
                        </tr>
                    </table>
                    <br />
                    <img width="150" height="80" id="logo_pgpn" runat="server" />
                    <img width="230" height="80" id="logo_app" runat="server" />
                    <br />
                    <br />
                    <div style="padding-left: 23px">
                        <asp:TextBox class="form-control w3-text-black" ID="txtloginUsername" Placeholder="Username : " Style="width: 90%;" runat="server"></asp:TextBox>
                        <p></p>
                        <asp:TextBox type="password" class="form-control w3-text-black" ID="txtloginPassword" Placeholder="Password : " Style="width: 90%;" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="row" style="background-color: #FFFFFF; text-align: center" id="login_inf_btn_PGPN" runat="server">
                    <p></p>
                    <asp:Button ID="btnLogin_PGPN" runat="server" Text="Login" Style="text-align: center" CssClass="btn btn-success" />
                    <p></p>
                </div>

                <div class="row" style="background-color: #FFFFFF; text-align: center" id="login_inf_btn_APP" runat="server">
                    <p></p>
                    <asp:Button ID="btnLogin_APP" runat="server" Text="Login" Style="text-align: center" CssClass="btn btn-success" />
                    <p></p>
                </div>
            </div>
            <div class="messagealert" id="alert_container" style="text-align: center"></div>
        </form>
    </div>
</body>

</html>
