<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pelajar_daftar_baru.aspx.vb" Inherits="KPP_SYS.pelajar_daftar_baru" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link rel="stylesheet" href="css/side_bar_frame.css">
    <link rel="stylesheet" href="css/font_montserrat.css">
    <link rel="stylesheet" href="css/font_awesome.css">
    <link rel="stylesheet" href="css/plugin_export.css" type="text/css" media="all" />
    <link rel="stylesheet" href="css/jquerytable.min.css">
    <script src="js/jquery.js"></script>
    <script src="js/jquerytable.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <script type="text/javascript" src="http://www.google.com/jsapi"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.10.0/js/bootstrap-select.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.10.0/css/bootstrap-select.min.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>

    <style>
        #testExam {
            background-image: url("img/exam_transkrip_v13_2.jpg");
            height: 100%;
            /* Center and scale the image nicely */
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
            display: none;
        }

        .w3-sidebar {
            width: 180px;
            background: #222;
            color: white;
            text-align: left
        }

        table {
            margin-left: auto;
            margin-right: auto;
        }

        .bootstrap-select .btn {
            background-color: #ffffff;
            border-style: solid;
            border-width: 2px;
            border-color: #b3b3ff;
            color: black;
            font-weight: 200;
            font-size: 14px;
            -webkit-appearance: none;
            -moz-appearance: none;
        }

        .bootstrap-select .dropdown-menu {
            margin: 10px 0 0;
        }

        select::-ms-expand {
            display: none;
        }

        .table-striped > tbody > tr:nth-child(odd) > td {
            background-color: #dee4e5;
        }

        .senarai_pelajar {
            text-align: left;
            padding-left: 2%;
        }

        .mySlides {
            display: none;
            width: 100%;
            align-items: center;
            margin: 0 auto;
        }

        .permata {
            text-align: center;
            width: 40%;
        }

        .textboxcss {
            padding: 5px 15px;
            display: inline-block;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        @media screen and (min-width: 801px) {
            .messagealert {
                width: 40%;
                position: fixed;
                bottom: 25px;
                right: 0px;
                z-index: 100000;
                padding: 0;
                font-size: 15px;
            }

            .sidenav {
                height: 100%;
                width: 15%;
                position: fixed;
                z-index: 1;
                top: 0;
                left: 0;
                background-color: #00203FFF;
                overflow-x: scroll;
                transition: 0.5s;
            }

                .sidenav a {
                    font-size: 0.8vw;
                }

            #myTopnav {
                width: 100%;
                /*height: 50px;*/
                height: 6.7vh;
                background-color: #00203FFF;
                position: fixed;
                z-index: 1;
                top: 0;
                left: 13vw;
                transition: 0.5s;
            }

            .font {
                font-size: 0.8vw
            }

            #tutupMenu {
                display: none;
            }

            #header {
                display: none
            }

            #mySidenav {
                width: 13vw;
                height: 100%
            }

            #menu_smallScreen {
                display: none
            }

            sidebar1 {
                display: block
            }

            sidebar2 {
                display: none
            }

            #mySidenav::-webkit-scrollbar {
                display: none;
            }


            .GridPager {
                padding: 2px;
                margin: 2% auto;
            }

                .GridPager a {
                    margin: auto 1%;
                    border-radius: 50%;
                    background-color: #444;
                    padding: 5px 10px 5px 10px;
                    color: #fff;
                    text-decoration: none;
                    -moz-box-shadow: 1px 1px 1px #111;
                    -webkit-box-shadow: 1px 1px 1px #111;
                    box-shadow: 1px 1px 1px #111;
                }

                    .GridPager a:hover {
                        background-color: #b1c9af;
                        color: #fff;
                    }

                .GridPager span {
                    background-color: #c6033e;
                    color: #fff;
                    -moz-box-shadow: 1px 1px 1px #111;
                    -webkit-box-shadow: 1px 1px 1px #111;
                    box-shadow: 1px 1px 1px #111;
                    border-radius: 50%;
                    padding: 5px 10px 5px 10px;
                }

            .blah {
                Width: 200px;
                Height: 150px;
            }
        }

        @media screen and (max-width: 800px) {

            .sidenav {
                height: 100%;
                width: 0%;
                position: fixed;
                z-index: 1;
                top: 0;
                left: 0;
                background-color: #00203FFF;
                overflow-x: scroll;
                transition: 0.5s;
            }

            .messagealert {
                width: 80%;
                position: fixed;
                bottom: 25px;
                right: 0px;
                z-index: 100000;
                padding: 0;
                font-size: 15px;
            }

            .sidenav a {
                font-size: 2vw;
            }

            #main {
                transition: margin-left .5s;
                margin-left: 0;
            }

            #header {
                display: block
            }

            #mySidenav {
                height: 100%;
                top: 15px;
                left: -30px;
                z-index: 100000;
            }

            #menu_smallScreen {
                display: block
            }

            body::-webkit-scrollbar {
                display: none;
            }

            .GridPager {
                padding: 2px;
                margin: 2% auto;
            }

                .GridPager a {
                    margin: auto 1%;
                    border-radius: 50%;
                    background-color: #444;
                    padding: 5px 10px 5px 10px;
                    color: #fff;
                    text-decoration: none;
                    -moz-box-shadow: 1px 1px 1px #111;
                    -webkit-box-shadow: 1px 1px 1px #111;
                    box-shadow: 1px 1px 1px #111;
                }

                    .GridPager a:hover {
                        background-color: #b1c9af;
                        color: #fff;
                    }

                .GridPager span {
                    background-color: #c6033e;
                    color: #fff;
                    -moz-box-shadow: 1px 1px 1px #111;
                    -webkit-box-shadow: 1px 1px 1px #111;
                    box-shadow: 1px 1px 1px #111;
                    border-radius: 50%;
                    padding: 5px 10px 5px 10px;
                }

            .gridViewRespond {
                font-size: x-small;
            }

            .font {
                font-size: 2vw
            }

            #tutupMenu {
                display: block;
            }

            .blah {
                Width: 100px;
                Height: 50px;
            }

            i {
                font-size: 2vw
            }

            sidebar1 {
                display: none
            }

            sidebar2 {
                display: block;
            }
        }

        #searchBTN {
            display: inline-block;
            float: right;
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

    <style>
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

        .sc3::-webkit-scrollbar {
            height: 10px;
        }

        .sc3::-webkit-scrollbar-track {
            background-color: transparent;
        }

        .sc3::-webkit-scrollbar-thumb {
            background-color: #929B9E;
            border-radius: 3px;
        }

        .sc4::-webkit-scrollbar {
            width: 10px;
        }

        .sc4::-webkit-scrollbar-track {
            background-color: transparent;
        }

        .sc4::-webkit-scrollbar-thumb {
            background-color: #929B9E;
        }
    </style>

</head>

<body style="background-color: #00203FFF;" onunload="bodyUnload();" onclick="clicked=true;">
    <form id="form1" runat="server">

        <script>
            function readURL(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('#student_Photo').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(input.files[0]);
                }
            }
            $("#uploadPhoto").change(function () {
                readURL(this);
            });
        </script>

        <div class="w3-text-white" style="position: fixed; z-index: 1; top: 9vh; left: 8.75vw; right: 8.75vw; transition: 0.5s;">

            <div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">

                <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
                    <button id="btnTutorial" runat="server" style="display: inline-block; font-size: 0.8vw">Student Registration Tutorial</button>
                    <button id="btnStudentInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Student Information</button>
                    <button id="btnParentInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Family Information</button>
                    <button id="btnLogout" runat="server" style="display: inline-block; font-size: 0.8vw" class="btn btn-danger w3-right">Log Out</button>
                </div>

                 <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="StudentTutorial" runat="server" class="sc4">
                    <div class="w3-text-black " style="text-align: left; padding-left: 1vw;">
                        <p>1. Student Registration Manual </p>
                        <video width="400px" src="video/Manual Pendaftaran.mp4" controls>
                        </video>
                    </div>

                    &nbsp;
                </div>

                <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="StudentInformation" runat="server" class="sc4">
                    <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Name </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtstudentName" Style="width: 40vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                            <td>&nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp   &nbsp &nbsp  &nbsp &nbsp  &nbsp   &nbsp &nbsp  &nbsp &nbsp  &nbsp
                            </td>
                            <td rowspan="6" class="Label" style="text-align: center;">
                                <asp:Image ID="student_Photo" runat="server" Width="200px" Height="200px" border="2px balck solid" />
                                <br />
                                <br />
                                <asp:FileUpload ID="uploadPhoto" runat="server" class="w3-text-black btn btn-default font" onchange="readURL(this)" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> NRIC / MYKAD </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtstudentMykad" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Race </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:DropDownList ID="ddlRace" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Religion </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:DropDownList ID="ddlReligion" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Gender </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                                <asp:RadioButton ID="rbtn_Male" Text="&nbsp; Male &nbsp;&nbsp;&nbsp; " runat="server" GroupName="gender" />
                                <asp:RadioButton ID="rbtn_Female" Text="&nbsp; Female" runat="server" GroupName="gender" />
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtstudentEmail" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Phone No </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtstudentPhone" Style="width: 8vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Address </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtstudentAddress" Style="width: 40vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> City </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtstudentCity" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Zip Code </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtstudentPostcode" Style="width: 8vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> State </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>

                    <br />

                    <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                        <p></p>
                        <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Student Year &nbsp : &nbsp </asp:Label>
                        <asp:TextBox runat="server" ID="txtstudentYear" Style="width: 10vw" Enabled="false" CssClass="textboxcss"></asp:TextBox>
                    </div>
                    <div class="w3-text-black font" style="text-align: left; padding-left: 1.2vw; display: inline-block">
                        <p></p>
                        <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Student Level &nbsp : &nbsp </asp:Label>
                        <asp:TextBox runat="server" ID="txtstudentLevel" Style="width: 10vw" Enabled="false" CssClass="textboxcss"></asp:TextBox>
                    </div>
                    <div class="w3-text-black font" style="text-align: left; padding-left: 1.2vw; display: inline-block">
                        <p></p>
                        <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Student Sem &nbsp : &nbsp </asp:Label>
                        <asp:TextBox runat="server" ID="txtstudentSem" Style="width: 10vw" Enabled="false" CssClass="textboxcss"></asp:TextBox>
                    </div>

                    <br />
                    <br />
                    <br />

                    <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                        <p></p>
                        <button id="btnUpdateStudentInfo" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Register Student Information</button>
                    </div>
                </div>

                <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="FamilyInformation" runat="server" class="sc4">
                    <table class="w3-text-black font" style="text-align: left; padding-left: 10px; border: hidden; margin-left: 1vw">

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Guardian 1 Name </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent1_Name" Style="width: 28vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp  &nbsp &nbsp  &nbsp
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Guardian 2 Name </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent2_Name" Style="width: 28vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Mykad / Passport No </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent1_IC" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp 
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Mykad / Passport No </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent2_IC" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Phone No </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent1_MobileNo" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp 
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Phone No </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent2_MobileNo" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email Address </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent1_Email" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp 
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email Address </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent2_Email" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Connection </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent1_Connection" Style="width: 20vw" CssClass="textboxcss" placeholder="Father / Mother / Uncle / Grandmother "></asp:TextBox>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp 
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Connection </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent2_Connection" Style="width: 20vw" CssClass="textboxcss" placeholder="Father / Mother / Uncle / Grandmother "></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Occupation </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent1_Work" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp 
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Occupation </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="Parent2_Work" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Salary (RM) </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:DropDownList ID="ddlsalaryP1" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp 
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Salary (RM) </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:DropDownList ID="ddlsalaryP2" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Status </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:RadioButton ID="Parent1_RadioStatusAlive" Text="&nbsp; Alive &nbsp;&nbsp;&nbsp; " runat="server" GroupName="P1_Status" />
                                <asp:RadioButton ID="Parent1_RadioStatusPassAway" Text="&nbsp; Passed Away" runat="server" GroupName="P1_Status" />
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp 
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Status </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:RadioButton ID="Parent2_RadioStatusAlive" Text="&nbsp; Alive &nbsp;&nbsp;&nbsp; " runat="server" GroupName="P2_Status" />
                                <asp:RadioButton ID="Parent2_RadioStatusPassAway" Text="&nbsp; Passed Away" runat="server" GroupName="P2_Status" />
                            </td>
                        </tr>

                    </table>

                    <br />
                    <br />

                    <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                        <button id="Btnsimpan" runat="server" class="btn btn-success" style="top: 8px; display: inline-block; font-size: 0.8vw">Register Family Information</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="messagealert" id="alert_container" style="text-align: center"></div>
    </form>
</body>
</html>
