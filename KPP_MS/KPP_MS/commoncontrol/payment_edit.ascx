<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_edit.ascx.vb" Inherits="KPP_MS.payment_edit" %>

<style>
    .sc3::-webkit-scrollbar {
        height: 8px;
    }

    .sc3::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc3::-webkit-scrollbar-thumb {
        background-color: #929B9E;
    }

    .sc4::-webkit-scrollbar {
        width: 10px;
        height: 8px;
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

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Payment &nbsp; / &nbsp; Payment Management &nbsp; / &nbsp;
        <asp:HyperLink runat="server" ID="previousPage"> View Payment Items </asp:HyperLink>
        &nbsp; / &nbsp;  Edit Payment Information
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 76vh" id="RegisterPayment" runat="server" class="sc4">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year  </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="ddlYear" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level  </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:CheckBox ID="CB_F1" Text="&nbsp; Foundation 1 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" />
                    <asp:CheckBox ID="CB_F2" Text="&nbsp; Foundation 2 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" />
                    <asp:CheckBox ID="CB_F3" Text="&nbsp; Foundation 3 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" />
                    <asp:CheckBox ID="CB_L1" Text="&nbsp; Level 1 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" />
                    <asp:CheckBox ID="CB_L2" Text="&nbsp; Level 2 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" runat="server" />
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Fee Type </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlType" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Fee Item Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="Inv_Name" Style="width: 30vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Fee Item Quantity </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="Inv_Quantity" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Gender </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:RadioButton ID="rbtn_Male" Text="&nbsp; Male &nbsp;&nbsp;&nbsp; " runat="server" GroupName="gender" />
                    <asp:RadioButton ID="rbtn_Female" Text="&nbsp; Female &nbsp;&nbsp;&nbsp;" runat="server" GroupName="gender" />
                    <asp:RadioButton ID="rbtn_Both" Text="&nbsp; Both" runat="server" GroupName="gender" />
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Religion </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:RadioButton ID="rbtn_Muslim" Text="&nbsp; Muslim &nbsp;&nbsp;&nbsp; " runat="server" GroupName="religon" />
                    <asp:RadioButton ID="rbtn_NonMuslim" Text="&nbsp; Non-Muslim  &nbsp;&nbsp;&nbsp;" runat="server" GroupName="religon" />
                    <asp:RadioButton ID="rbtn_All" Text="&nbsp; Both" runat="server" GroupName="religon" />
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Price (RM) </asp:Label>
                </td>
                <td colspan="5">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Std_Price" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Remark </asp:Label>
                </td>
                <td colspan="5">
                    <p></p>
                    &nbsp : &nbsp
                      <asp:TextBox runat="server" ID="Inv_Remark" Style="width: 50vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

        </table>

        <br />
        <br />

        <button id="btnAddPayment" runat="server" class="btn btn-success" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Update Payment Items</button>
    </div>


</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
