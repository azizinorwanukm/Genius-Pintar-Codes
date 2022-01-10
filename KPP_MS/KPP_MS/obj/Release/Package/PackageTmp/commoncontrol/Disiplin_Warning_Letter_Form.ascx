<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_Warning_Letter_Form.ascx.vb" Inherits="KPP_MS.Disiplin_Warning_Letter_Form" %>
<%@ Register Assembly="TextboxioControl" Namespace="TextboxioControl" TagPrefix="textboxio" %>


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
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Warning Letters</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px;">
            <!-- Student Name -->
            <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Name : </asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="wlStudentNameLbl" Style="width: 80%; font-weight: bold;"></asp:Label>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
            <!--student ID-->
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student IC :</asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="wlStudentICLbl" Style="width: 80%; font-weight: bold;"></asp:Label>
            </div>

            <!-- Student Class -->
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Class : </asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="wlStudentClassLbl" Style="width: 80%; font-weight: bold;"></asp:Label>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px">
            <!-- Case Name -->
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Case Name : </asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="wlCaseLbl" Style="width: 80%; font-weight: bold;"></asp:Label>
            </div>

            <!-- Case Date -->
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Case Date : </asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="wlCaseDate" Style="width: 80%; font-weight: bold;"></asp:Label>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
            <div class="col-md-8 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Counseling Status : </asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="wlCounselingStatus" Style="width: 80%; font-weight: bold;"></asp:Label>
                <br />
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Counseling Session : </asp:Label>
                <asp:TextBox ID="wlCounselingSession" runat="server" TextMode="Multiline" class="form-control" Height="90" Style="width: 100%; border-radius: 25px" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div id="wlContentDiv" class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;" runat="server">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Title : </asp:Label>
                <asp:DropDownList ID="ddlLetterType" runat="server" AutoPostBack="true" CssClass="btn btn-default font ddl" Style="width: 80%"></asp:DropDownList>
            </div>
            <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Content : </asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <textboxio:Textboxio runat="server" ID="letterContent" Content="Insert letter content here" Width="200%" />
            </div>
        </div>

        <div id="wlNoteDiv" class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;" runat="server">
            <h4 style="text-align: center; padding: inherit; color: black;"><b>Please complete the counseling session to proceed with warning letter</b></h4>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
            <button id="wlCancelBtn" value="<%= stdID %>" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Cancel &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
        </div>


    </div>
</div>
