<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_Case_Detail.ascx.vb" Inherits="KPP_MS.Disiplin_Case_Detail" %>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="textboxio/textboxio.js"></script>
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

    function date_Click() {
        if (divcalendar.style.display == "none")
            divcalendar.style.display = "";
        else
            divcalendar.style.display = "none";
    }
</script>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Disciplin Detail</p>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px;">
        <!-- Student Name -->
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Name : </asp:Label>
            <asp:Label CssClass="Label" runat="server" ID="StudentNameLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
        </div>

        <!--student ID-->
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student ID :</asp:Label>
            <asp:Label CssClass="Label" runat="server" ID="StudentIdLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
        </div>

        <!-- Student MyKad -->
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student MyKad : </asp:Label>
            <asp:Label CssClass="Label" runat="server" ID="StudentMyKadLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
        </div>

        <!-- Student Class -->
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Class : </asp:Label>
            <asp:Label CssClass="Label" runat="server" ID="StudentClassLbl" Style="width: 20%; font-weight: bold;"></asp:Label>
        </div>
    </div>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px;">
        <!-- Date And Time -->
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Date : </asp:Label>
            <asp:TextBox CssClass="ddl datepicker" ID="CurrentDate" runat="server" />
            &#160; <i class="fa fa-calendar w3-medium w3-text-black"></i>
        </div>
    </div>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <!--disiplin drop down -->
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Diciplinary Category : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:DropDownList ID="ddlCaseType" runat="server" AutoPostBack="false" CssClass="btn btn-default font ddl" Style="width: 100%"></asp:DropDownList>
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
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Counseling Date : </asp:Label>
            <asp:TextBox CssClass="ddl datepicker" ID="CounselingDate" runat="server" />&#160; <i class="fa fa-calendar w3-medium w3-text-black"></i>
        </div>
    </div>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
        <button id="BtnSimpanCase" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;">Update &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="BtnBackCase" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>

</div>
