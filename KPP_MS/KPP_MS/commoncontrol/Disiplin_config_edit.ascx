<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_config_edit.ascx.vb" Inherits="KPP_MS.Disiplin_config_edit" %>


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
        Menu &nbsp; : &nbsp; Discipline &nbsp; / &nbsp; Discipline Management &nbsp; / &nbsp;
        <asp:HyperLink runat="server" ID="previousPage"> View Discipline </asp:HyperLink>
        &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 76vh">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">

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
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtCase_Name" Columns="100" Rows="5" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Category </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Demerit Point </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:TextBox runat="server" ID="txtDemerit_Point" Style="width: 8vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

        </table>

        <br />

        <button id="btnUpdate" runat="server" class="btn btn-success" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Update Course</button>

    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
