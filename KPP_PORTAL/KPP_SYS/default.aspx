<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="default.aspx.vb" Inherits="KPP_SYS.WebForm1" %>

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

    <!-- Carousel Slide - For Student Login -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>

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
                position: fixed;
                z-index: 1;
                 top: 200px;
                left: 60px;
                width: 350px;
            }

            #pengumuman_page {
                position: fixed;
                z-index: 1;
                top: 75px;
                left: 450px;
                width: 500px;
            }

            #pengumuman_perician_page {
                position: fixed;
                z-index: 1;
                top: 75px;
                left: 960px;
                width: 550px;
            }

            ::-webkit-scrollbar {
                width: 10px;
            }

            ::-webkit-scrollbar-track {
                background: #f1f1f1;
            }

            ::-webkit-scrollbar-thumb {
                background: #888;
            }


            #btnLogin {
                position: center;
            }

            #btnLogin_APP {
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

            #btnLogin_APP {
                position: center;
            }
        }
    </style>

</head>

<body>
    <div class="backImage">
        <form id="form1" runat="server">

            <div class="container" id="front_page" style="display: inline-block">
                <div id="myCarousel" class="carousel slide" data-interval="100000000">

                    <!-- Wrapper for slides -->
                    <div class="carousel-inner">
                        <div class="item active">

                            <!-- Login Slide For Pusat GENIUS@Pintar Negara, UKM -->
                            <div class="w3-card-4" >
                                <div class="row" style="background-color: #FFFFFF; text-align: center;">
                                    <table style="border: hidden; width: 100%">
                                        <tr>
                                            <td>
                                                <button id="BtnLoginTop" runat="server" style="display: inline-block; font-size: 0.8vw; width: 100%">Log In</button></td>
                                            <td>
                                                <button id="BtnSignupTop" runat="server" style="display: inline-block; font-size: 0.8vw; width: 100%">Sign Up</button></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <img src="img/logo genius pintar.png" width="230" height="140" />
                                    <br />
                                    <br />
                                    <div style="padding-left: 23px">
                                        <asp:TextBox class="form-control w3-text-black" ID="txtloginUsername" Placeholder="MYKAD : " Style="width: 90%;" runat="server"></asp:TextBox>
                                        <p></p>
                                        <asp:TextBox type="password" class="form-control w3-text-black" ID="txtloginPassword" Placeholder="Password : " Style="width: 90%;" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row" style="background-color: #FFFFFF; text-align: center" id="login_inf_btn" runat="server">
                                    <p></p>
                                    <asp:Button ID="btnLogin" runat="server" Text="Log In" Style="text-align: center" CssClass="btn btn-success" />
                                    <p></p>
                                </div>

                                <div class="row" style="background-color: #FFFFFF; text-align: center" id="signup_inf_btn" runat="server">
                                    <p></p>
                                    <asp:Button ID="btnSignup" runat="server" Text="Sign Up" Style="text-align: center" CssClass="btn btn-success" />
                                    <p></p>
                                </div>
                            </div>
                        </div>

                        <div class="item">
                            <!-- Login Slide For Akademi Pintar Pendang, Kedah -->
                            <div class="w3-card-4">
                                <div class="row" style="background-color: #FFFFFF; text-align: center;">
                                    <table style="border: hidden; width: 100%">
                                        <tr>
                                            <td>
                                                <button id="BtnLoginTop_APP" runat="server" style="display: inline-block; font-size: 0.8vw; width: 100%">Log In</button></td>
                                            <td>
                                                <button id="BtnSignupTop_APP" runat="server" style="display: inline-block; font-size: 0.8vw; width: 100%">Sign Up</button></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <br />
                                    <img src="img/logo kpm.jpg" width="300" height="80" />
                                    <br />
                                    <br />
                                    <br />
                                    <div style="padding-left: 23px">
                                        <asp:TextBox class="form-control w3-text-black" ID="txtloginUsername_APP" Placeholder="MYKAD : " Style="width: 90%;" runat="server"></asp:TextBox>
                                        <p></p>
                                        <asp:TextBox type="password" class="form-control w3-text-black" ID="txtloginPassword_APP" Placeholder="Password : " Style="width: 90%;" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row" style="background-color: #FFFFFF; text-align: center" id="login_inf_btn_APP" runat="server">
                                    <p></p>
                                    <asp:Button ID="btnLogin_APP" runat="server" Text="Log In" Style="text-align: center" CssClass="btn btn-success" />
                                    <p></p>
                                </div>

                                <div class="row" style="background-color: #FFFFFF; text-align: center" id="signup_inf_btn_APP" runat="server">
                                    <p></p>
                                    <asp:Button ID="btnSignup_APP" runat="server" Text="Sign Up" Style="text-align: center" CssClass="btn btn-success" />
                                    <p></p>
                                </div>
                            </div>
                        </div>

                    </div>

                    <!-- Left and right controls -->
                    <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                        <span class="sr-only">Previous</span>
                    </a>
                    <a class="right carousel-control" href="#myCarousel" data-slide="next">
                        <span class="glyphicon glyphicon-chevron-right"></span>
                        <span class="sr-only">Next</span>
                    </a>
                </div>
            </div>



            <div id="pengumuman_page" style="padding-top: 20px; padding-left: 15px; padding-bottom: 10px; background-color: #F2F2F2;">

                <div style="padding-top: 10px; padding-left: 10px; padding-right: 15px; padding-bottom: 10px; border-bottom: 5px solid #567572FF;">
                    <asp:Label runat="server"> <b> Announcement Lists </b> </asp:Label>
                </div>

                <div style="padding-top: 20px; padding-left: 10px; padding-bottom: 10px; overflow-y: scroll; height: 70vh; font-size: 0.7vw">
                    <div class="table-responsive" style="display: inline-block">
                        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                            BackColor="#FFFAFA" DataKeyNames="PengumumanID" BorderStyle="None" GridLines="None"
                            Width="97%" HeaderStyle-HorizontalAlign="Left">
                            <RowStyle HorizontalAlign="Left" />
                            <Columns>
                                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Announcement Title" ItemStyle-Width="100%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRead" runat="server" OnClick="lnkRead_Click" Text='<%# Bind("Title")%>'></asp:LinkButton>
                                        <br />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label runat="server" Class="id1 w3-text-black"><b> No Announcement Information Are Recorded </b> </asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                        </asp:GridView>
                    </div>
                </div>

            </div>

            <div id="pengumuman_perician_page" style="padding-top: 20px; padding-left: 15px; padding-bottom: 10px; background-color: #F2F2F2;">

                <div style="padding-top: 10px; padding-left: 10px; padding-right: 15px; padding-bottom: 10px; border-bottom: 5px solid #567572FF;">
                    <asp:Label runat="server"> <b> Announcement Details </b> </asp:Label>
                </div>

                <div style="padding-top: 20px; padding-left: 10px; padding-right: 10px; padding-bottom: 10px; overflow-y: scroll; height: 70vh; font-size: 11px">

                    <table class="w3-text-black" style="text-align: left; padding-left: 10px; border: hidden; margin-left: 10px">
                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Date </asp:Label>
                            </td>
                            <td style="vertical-align: top;">
                                <p></p>
                                : 
                            </td>
                            <td>
                                <p></p>
                                <asp:Label ID="lblDateCreated" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Title</asp:Label>
                            </td>
                            <td style="vertical-align: top;">
                                <p></p>
                                :  
                            </td>
                            <td>
                                <p></p>
                                <asp:Label ID="lblTitle" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50px; vertical-align: top;">
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Content </asp:Label>
                            </td>
                            <td style="vertical-align: top;">
                                <p></p>
                                : 
                            </td>
                            <td>
                                <p></p>
                                <asp:Literal ID="ltBody" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </div>

            </div>


            <div class="messagealert" id="alert_container" style="text-align: center"></div>
        </form>
    </div>
</body>

</html>
