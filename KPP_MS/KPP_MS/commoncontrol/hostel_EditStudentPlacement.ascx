<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="hostel_EditStudentPlacement.ascx.vb" Inherits="KPP_MS.hostel_EditStudentPlacement" %>

<script type="text/javascript" lang="javascript">
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
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Student Info</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label runat="server" ID="lblStudentName"></asp:Label>
            <asp:Label runat="server" ID="lblStudentYear"></asp:Label>
            <asp:Label runat="server" ID="lblStudentSem"></asp:Label>
        </div>
    </div>
</div>
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Placement Info</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:DropDownList ID="ddlHostelYear" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" style="width:190px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlBlockName" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" style="width:190px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlBlockLevel" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" style="width:190px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlRoomName" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" style="width:190px;"></asp:DropDownList>
            <asp:Label CssClass="Label w3-text-black" ID="count_student" runat="server" Text =" " Visible ="False"/>
        </div>
        <asp:Button runat="server" ID="btnSave" Text="Save" />
        <asp:Button runat="server" ID="btnBack" Text="Back" />
    </div>
    <div class="messagealert" id="alert_container" style="text-align: center"></div> 
</div>