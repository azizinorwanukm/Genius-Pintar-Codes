<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="scholarship_update.ascx.vb" Inherits="KPP_MS.scholarship_update" %>

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


<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Scholarship Information<i class="fa fa-fw fa fa-caret-down w3-left"></i></button>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Scholarship Name : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="scholarship_name" Style="width: 60%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 10px">
            <asp:Label CssClass="Label" runat="server"> Scholarship Sponsor : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="scholarship_sponsar" Style="width: 60%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
    </div>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Scholarship Type : </asp:Label>
            <asp:DropDownList Style="width: 190px; height: 29px; border-radius: 25px;" CssClass="w3-text-black btn btn-default ddl" ID="scholarship_type" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 10px">
            <asp:Label CssClass="Label" runat="server"> Scholarship Status : </asp:Label>
            <asp:DropDownList Style="width: 190px; height: 29px; border-radius: 25px;" CssClass="w3-text-black btn btn-default ddl" ID="scholarship_status" runat="server"></asp:DropDownList>
        </div>
    </div>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160; <i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back">Back &#160; <i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>

    <div class="messagealert" id="alert_container" style="text-align: center"></div>

</div>
