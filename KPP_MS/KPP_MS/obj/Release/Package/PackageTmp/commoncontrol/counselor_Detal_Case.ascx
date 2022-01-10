<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="counselor_Detal_Case.ascx.vb" Inherits="KPP_MS.counselor_Detal_Case" %>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="../textboxio/textboxio.js"></script>
    <script>
        $(function () {
            $('.datepicker').datepicker({ dateFormat: 'dd MM yy' }).val();
        });
    </script>
</head>

<style type="text/css">
    .ddl {
        border-radius: 25px;
    }

    .CalendarCssClass {
        background-color: #990000;
        font-family: Century;
        text-transform: lowercase;
        width: 750px;
        border: 1px solid Olive;
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


<div>
    <div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

        <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Student Case Detail</p>
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
            <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">

                <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">

                    <!-- Student Name -->
                    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                        <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Name : </asp:Label>
                        <asp:Label CssClass="Label" runat="server" ID="StudentNameLbl" Style="width: 80%; font-weight: bold;"></asp:Label>
                    </div>

                    <!--student ID-->
                    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                        <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student ID : </asp:Label>
                        <asp:Label CssClass="Label" runat="server" ID="StudentIdLbl" Style="width: 80%; font-weight: bold;"></asp:Label>
                    </div>

                    <!-- Student MyKad -->
                    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                        <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student MyKad : </asp:Label>
                        <asp:Label CssClass="Label" runat="server" ID="StudentMyKadLbl" Style="width: 80%; font-weight: bold;"></asp:Label>
                    </div>

                    <!-- Student Class -->
                    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                        <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Class : </asp:Label>
                        <asp:Label CssClass="Label" runat="server" ID="StudentClassLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
                    </div>

                    <!-- Date And Time -->
                    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px; margin-top: 10px">
                        <asp:Label CssClass="Label" runat="server"> Date : </asp:Label>
                        <asp:TextBox CssClass="ddl datepicker" ID="CurrentDate" runat="server" />
                        &#160; <i class="fa fa-calendar w3-medium w3-text-black"></i>
                    </div>

                </div>

                <div id="dis_type">
                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">

                        <!--disiplin drop down -->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Diciplinary Category : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                            <asp:DropDownList ID="ddlDiciplinetype" runat="server" AutoPostBack="false" CssClass="btn btn-default font ddl" Style="width: 100%"></asp:DropDownList>
                        </div>

                        <!--Complainant Dropdown list-->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Complainant Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                            <asp:DropDownList ID="ddlReporter" runat="server" AutoPostBack="false" CssClass="btn btn-default font ddl" Style="width: 100%"></asp:DropDownList>
                        </div>

                    </div>

                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">

                        <!-- Detail Case -->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Detail Case : </asp:Label>
                            <asp:TextBox ID="Detail_case" runat="server" TextMode="Multiline" class="form-control" Height="90" Style="width: 100%; border-radius: 25px"></asp:TextBox>
                        </div>

                        <!--Action Box -->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Action : </asp:Label>
                            <asp:TextBox ID="Action_box" runat="server" TextMode="Multiline" Class="form-control" Height="90" Style="width: 100%; border-radius: 25px;"></asp:TextBox>
                        </div>
                    </div>


                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">

                        <!-- Demerit Mark -->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Demerit Mark : </asp:Label>
                            <asp:TextBox ID="demerit_mark" runat="server" class="form-control" Style="border-radius: 25px;width: 30%"></asp:TextBox>

                        </div>
                    </div>

                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">

                        <!-- Counselo Section -->
                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Counselor Session Date : </asp:Label>
                            <asp:TextBox CssClass="ddl datepicker" ID="CounselorDate" runat="server" />
                            &#160; <i class="fa fa-calendar w3-medium w3-text-black"></i>
                        </div>

                        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Counselor Status : </asp:Label>
                            <asp:DropDownList ID="ddlcounselorstatus" runat="server" AutoPostBack="false" CssClass="btn btn-default font ddl" Style="width: 60%"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">

                        <!-- Counselor Session Talk -->
                        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Counseling Session : </asp:Label>
                            <asp:TextBox ID="txtCounselorSession" runat="server" TextMode="Multiline" class="form-control" Height="90" Style="width: 100%; border-radius: 25px"></asp:TextBox>
                        </div>

                    </div>

                    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
                        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
                        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
                    </div>

                </div>
            </div>
        </div>
    </div>

</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
