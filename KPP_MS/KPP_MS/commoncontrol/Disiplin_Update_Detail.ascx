<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_Update_Detail.ascx.vb" Inherits="KPP_MS.Disiplin_Update_Detail" %>

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

<style>
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
        height: 10px;
    }

    .sc4::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc4::-webkit-scrollbar-thumb {
        background-color: #929B9E;
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

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh;" class="w3-card-2">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black font">
        Menu &nbsp; : &nbsp; Discipline &nbsp; / &nbsp; Case Management
         &nbsp; / &nbsp; 
        <asp:HyperLink runat="server" ID="previousPage"> View Case </asp:HyperLink>
        &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 76vh" class="sc4">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:TextBox runat="server" ID="txtStudent_Name" Style="width: 35vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Date </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:TextBox runat="server" ID="txtDate" Style="width: 15vw" CssClass="textboxcss datepicker"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="4">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtStudent_Level" Style="width: 15vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Class </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td colspan="4">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtClass_Name" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>
        </table>

        <br />

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Case Category </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddlCase_Category" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Case Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddlCase_Name" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Case Detail </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtCase_Detail" Columns="70" Rows="5" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>
        </table>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Need Counseling Guidance ? : </asp:Label>
            <asp:CheckBox ID="Check_NeedCounseling" runat="server" Checked="false" />
        </div>

        <br />
        <br />
        <br />

        <button id="btnUpdate" runat="server" class="btn btn-success" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Update Disciplinary Case </button>
    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
