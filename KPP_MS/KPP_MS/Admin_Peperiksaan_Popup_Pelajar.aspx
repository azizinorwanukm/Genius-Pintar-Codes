<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Admin_Peperiksaan_Popup_Pelajar.aspx.vb" Inherits="KPP_MS.Admin_Peperiksaan_Popup_Pelajar" %>

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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.0.272/jspdf.debug.js"></script>
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
        .ddl {
            border-radius: 25px;
        }

        #testExam {
            background-image: url("img/exam_transkrip_v13_2.jpg");
            height: 100%;
            /* Center and scale the image nicely */
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
            display: none;
        }

        #testExamOfficial {
            background-image: url("img/exam_official_v1.jpg");
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
                width: 15%;
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

        video {
            width: 75%;
            height: 50%;
        }

        #searchBTN {
            display: inline-block;
            float: right;
        }

        #chartdivLPE1, #chartdivLPE2, #chartdivLPE3, #chartdivLPE4, #chartdivLPPT3, #chartdivLPSPM {
            width: 100%;
            height: 300px;
            align-items: center;
            margin: 0 auto;
        }

        #chartdivMalayLanguage, #chartdivEnglishLanguage, #chartdivMathematic, #chartdivIslamicStudies_MoralValues, #chartdivHistory, #chartdivBiology,
        #chartdivPhysics, #chartdivChemistry, #chartdivPhilosophy, #chartdivPhysicalEducation, #chartdivResearchSkill, #chartdivMusic, #chartdivArabLanguage,
        #chartdivSelfDevelopment, #chartdivPerformingArts, #chartdivVisualArts, #chartdivEnterprenuership, #chartdivComputerProgramming, #chartdivEnglishLiterature,
        #chartdivPsychology, #chartdivCitizenship, #chartdivIslamicStudies_MoralValues_AlquranSunnah, #chartdivEnglishLiterature1, #chartdivEnglishLiterature2,
        #chartdivLinearAlgebra, #chartdivCalculus, #chartdivStatistic1, #chartdivStatistic2, #chartdivFranceLanguage, #chartdivMandarinLanguage {
            width: 100%;
            height: 300px;
            align-items: center;
            margin: 0 auto;
        }
    </style>

    <script type="text/javascript">

        function MyMenu_One() {
            var x = document.getElementById("MENU_SUB_GENERALMANAGEMENT");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += " w3-hover-black";

            } else {
                x.className = x.className.replace(" w3-show", "w3-hide");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Two() {
            var x = document.getElementById("MENU_SUB_STUDENT");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += " w3-hover-black";

            } else {
                x
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Three() {
            var x = document.getElementById("MENU_SUB_STAFF");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += " w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Four() {
            var x = document.getElementById("MENU_SUB_COORDINATOR");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Five() {
            var x = document.getElementById("MENU_SUB_DISCIPLINE");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Six() {
            var x = document.getElementById("MENU_SUB_COUNSELOR");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Seven() {
            var x = document.getElementById("MENU_SUB_RESEARCH");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }
        function MyMenu_Eight() {
            var x = document.getElementById("MENU_SUB_EXAMINATION");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Nine() {
            var x = document.getElementById("MENU_SUB_PAYMENT");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Ten() {
            var x = document.getElementById("MENU_SUB_HOSTEL");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Eleven() {
            var x = document.getElementById("MENU_SUB_COCURRICULAR");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Twelve() {
            var x = document.getElementById("MENU_SUB_REPORT");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Thirdteen() {
            var x = document.getElementById("MENU_SUB_ALUMNI");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

        function MyMenu_Fourteen() {
            var x = document.getElementById("MENU_SUB_SETTING");
            if (x.className.indexOf("w3-show") == -1) {
                x.className += " w3-show";
                x.previousElementSibling.className += "w3-hover-black";
            } else {
                x.className = x.className.replace(" w3-show", "");
                x.previousElementSibling.className =
                    x.previousElementSibling.className.replace(" w3-hover-black", "");
            }
        }

    </script>

    <script>
        function openSideBar() {
            document.getElementById("mySidenav").style.float = "left";
            document.getElementById("mySidenav").style.width = "175px";
        }

        function closeNav() {
            document.getElementById("mySidenav").style.width = "0";
            document.getElementById("main").style.marginLeft = "0";
        }
    </script>

</head>

<body>
    <form id="form1" runat="server">

        <br />

        <div id="run_table" runat="server">
        </div>

    </form>
</body>
</html>
