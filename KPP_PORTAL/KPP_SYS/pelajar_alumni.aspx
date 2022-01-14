<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="pelajar_alumni.aspx.vb" Inherits="KPP_SYS.pelajar_alumni" %>

<%@ Register Src="~/commoncontrol/guardian2_Detail.ascx" TagPrefix="uc1" TagName="guardian2_Detail" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
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

            #myTopnav {
                width: 100%;
                /*height: 50px;*/
                height: 6.7vh;
                background-color: #00203FFF;
                position: fixed;
                z-index: 1;
                top: 0;
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

</head>

<body style="background-color: #FFFFFF;" onunload="bodyUnload();" onclick="clicked=true;">
    <form id="form1" runat="server">

        <div id="myTopnav">
            <%--    /***  HEADER  ***/    --%>
            <div style="padding-top: 2.2vh; padding-left: 2vw">
                <asp:Label runat="server" ID="txtAlumniName" class="w3-text-white font" Style="text-align: left;"></asp:Label>
                &nbsp;&nbsp;
                <asp:Label runat="server" ID="txtcurrentDate" class="w3-text-white font" Style="text-align: left;"></asp:Label>
                <button id="btnLogout" runat="server" class="btn btn-danger" style="background-color: #CC0000; position: fixed; z-index: 1; right: 2vw; top: 1.6vh; font-size: 0.8vw">Logout &nbsp;&nbsp;<i class="fa fa-sign-out w3-text-white"></i></button>
            </div>
        </div>

        <%--    /***  BODY  ***/    --%>
        <div class="w3-text-white" style="position: fixed; z-index: 1; top: 9vh; left: 8.75vw; right: 8.75vw; transition: 0.5s;">

            <div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
                <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
                    <button id="btnAlumniInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Alumni Information</button>
                    <button id="btnEducationInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Educational Information</button>
                    <button id="btnProfessionalInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Professional Information</button>
                </div>

                <%--    /***  Alumni Information  ***/    --%>
                <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 78vh" id="AlumniInformation" runat="server" class="sc4">
                    <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student ID </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtstudentID" Style="width: 7vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                            </td>
                            <td>&nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp   &nbsp &nbsp  &nbsp &nbsp  &nbsp   &nbsp &nbsp  &nbsp &nbsp  &nbsp
                            </td>
                            <td rowspan="8" class="Label" style="text-align: center;">
                                <asp:Image ID="student_Photo" runat="server" Width="200px" Height="200px" border="2px balck solid" />
                                <br />
                                <br />
                                <asp:FileUpload ID="uploadPhoto" runat="server" class="w3-text-black btn btn-default font" onchange="readURL(this)" />
                            </td>
                        </tr>

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
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> NRIC / MYKAD </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtstudentMykad" Style="width: 10vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
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
                               <asp:DropDownList ID="ddlRace" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw; text-align: left"></asp:DropDownList>
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
                                <asp:DropDownList ID="ddlReligion" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw; text-align: left"></asp:DropDownList>
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
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw; text-align: left"></asp:DropDownList>
                            </td>
                            <td>
                                <p></p>
                                &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Country </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw; text-align: left"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student Batch </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                            <asp:TextBox runat="server" ID="txtstudentBatch" Style="width: 10vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <br />
                    <br />

                    <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Spouse Name </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                     <asp:TextBox runat="server" ID="txtSpouseName" Style="width: 40vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Spouse Age </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                     <asp:TextBox runat="server" ID="txtSpouseAge" Style="width: 5vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> No Of Children </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                     <asp:TextBox runat="server" ID="txtChilderNo" Style="width: 5vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <br />

                    <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                        <p></p>
                        <button id="btnUpdateAlumniInfo" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Update Alumni Information</button>
                    </div>
                </div>

                <%--    /***  Educational Information  ***/    --%>
                <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 80vh" id="EducationalInformation" runat="server" class="sc4">
                    <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Institute Name </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                            <asp:TextBox runat="server" ID="txtUniName" Style="width: 35vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Type </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                               <asp:DropDownList ID="ddlUniType" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw; text-align: left"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                               <asp:TextBox runat="server" ID="txtUniCourse" Style="width: 35vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Location </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                               <asp:DropDownList ID="ddlUniCountry" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw; text-align: left"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year Start </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                              <asp:TextBox runat="server" ID="txtUniStart" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                            <td>
                                <p></p>
                            </td>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year End </asp:Label>
                            </td>
                            <td>
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtUniEnd" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Sponsorship </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                               <asp:TextBox runat="server" ID="txtUniSponsorship" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                        <p></p>
                        <button id="btnUpdateEducation" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Update Education Information</button>
                    </div>

                    <br />
                    <br />
                    <br />

                    <div style="overflow-y: scroll; height: 36vh" class="table-responsive sc4 font">
                        <asp:GridView ID="datRespondent_EB" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                            BackColor="#FFFAFA" DataKeyNames="AEB_ID" BorderStyle="None" GridLines="None"
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
                                <asp:TemplateField HeaderText="Institutions" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <asp:Label ID="AEB_SchoolName" class="id1" runat="server" Text='<%# Eval("AEB_SchoolName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="33%">
                                    <ItemTemplate>
                                        <asp:Label ID="AEB_SchoolCourseName" class="id1" runat="server" Text='<%# Eval("AEB_SchoolCourseName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" ItemStyle-Width="17%">
                                    <ItemTemplate>
                                        <asp:Label ID="AEB_SchoolType" class="id1" runat="server" Text='<%# Eval("AEB_SchoolType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="AEB_SchoolStartYear" class="id1" runat="server" Text='<%# Eval("AEB_SchoolStartsYear") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="AEB_SchoolEndYear" class="id1" runat="server" Text='<%# Eval("AEB_SchoolEndsYear") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton Width="25" Height="25" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label runat="server" Class="id1 w3-text-black"><b> No Alumni Education Information Are Recorded </b> </asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                        </asp:GridView>
                    </div>
                </div>

                <%--    /***  rofessional Information  ***/    --%>
                <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 80vh" id="ProfessionalInformation" runat="server" class="sc4">
                    <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Company Name </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                                <asp:TextBox runat="server" ID="txtCompanyName" Style="width: 35vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Position </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                               <asp:TextBox runat="server" ID="txtCompanyPosition" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Location </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                               <asp:DropDownList ID="ddlCompanyLocation" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw; text-align: left"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="5">
                                <p></p>
                                &nbsp &nbsp
                            </td>
                        </tr>

                        <tr>
                            <td colspan="5">
                                <p></p>
                                <asp:CheckBox ID="checkWorkHere" runat="server" AutoPostBack="true" Style="display: inline-block" />
                                &nbsp 
                               <asp:Label CssClass="Label" runat="server" Style="width: 100%; display: inline-block"><b> I Am Currently Working In This Role </b></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Start Date </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                                <asp:DropDownList ID="ddlCompanyStartMonth" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw; text-align: left; display: inline-block"></asp:DropDownList>
                                <asp:TextBox runat="server" ID="txtCompanyStartYear" Style="width: 10vw; display: inline-block" CssClass="textboxcss" placeholder="Select Year"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <p></p>
                                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> End Date </asp:Label>
                            </td>
                            <td colspan="4">
                                <p></p>
                                &nbsp : &nbsp
                                <asp:DropDownList ID="ddlCompanyEndMonth" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw; text-align: left; display: inline-block"></asp:DropDownList>
                                <asp:TextBox runat="server" ID="txtCompanyEndYear" Style="width: 10vw; display: inline-block" CssClass="textboxcss" placeholder="Select Year"></asp:TextBox>
                            </td>
                        </tr>


                    </table>

                    <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                        <p></p>
                        <button id="btnUpdateCompany" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Update Education Information</button>
                    </div>

                    <br />
                    <br />
                    <br />

                    <div style="overflow-y: scroll; height: 33vh" class="table-responsive sc4 font">
                        <asp:GridView ID="datRespondent_CB" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                            BackColor="#FFFAFA" DataKeyNames="ACB_ID" BorderStyle="None" GridLines="None"
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
                                <asp:TemplateField HeaderText="Company Name" ItemStyle-Width="40%">
                                    <ItemTemplate>
                                        <asp:Label ID="ACB_CompanyName" class="id1" runat="server" Text='<%# Eval("ACB_CompanyName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Position" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="ACB_CompanyPosition" class="id1" runat="server" Text='<%# Eval("ACB_CompanyPosition") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="ACB_CompanyLocation" class="id1" runat="server" Text='<%# Eval("ACB_CompanyLocation") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="12.5%">
                                    <ItemTemplate>
                                        <asp:Label ID="ACB_CompanyMonthStart" class="id1" runat="server" Text='<%# Eval("ACB_CompanyMonthStart") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date" ItemStyle-Width="12.5%">
                                    <ItemTemplate>
                                        <asp:Label ID="ACB_CompanyMonthEnd" class="id1" runat="server" Text='<%# Eval("ACB_CompanyMonthEnd") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <ItemTemplate>
                                        <asp:ImageButton Width="25" Height="25" ID="btnACBDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label runat="server" Class="id1 w3-text-black"><b> No Alumni Profesional Information Are Recorded </b> </asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                        </asp:GridView>
                    </div>
                </div>

            </div>
        </div>
        <uc1:guardian2_Detail runat="server" ID="guardian2_Detail" />

        <div class="messagealert" id="alert_container" style="text-align: center"></div>
    </form>
</body>
</html>
