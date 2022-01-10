<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengarah.Master" CodeBehind="pengarah_login_berjaya.aspx.vb" Inherits="KPP_MS.pengarah_login_berjaya" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

    <div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
        <%--Breadcrum--%>
        <div style="padding-top: 1vh; padding-left: 1vw; padding-bottom: 1vh" class="w3-text-black">
            Menu &nbsp; : &nbsp; Home &nbsp; / &nbsp; Director Information
        </div>
    </div>

    <div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
        <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 76vh; overflow-y: scroll;" class="sc4" runat="server">

            <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">

                <tr>
                    <td>
                        <p></p>
                        <asp:Label CssClass="Label" runat="server" Style="width: 100%"> ID </asp:Label>
                    </td>
                    <td colspan="4">
                        <p></p>
                        &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtstaffID" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstaffName" Style="width: 40vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstaffMykad" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstaffEmail" Style="width: 15vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstaffPhone" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstaffAddress" Style="width: 40vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstaffCity" Style="width: 15vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstaffPostcode" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <p></p>
                        <asp:Label CssClass="Label" runat="server" Style="width: 100%"> State </asp:Label>
                    </td>
                    <td colspan="4">
                        <p></p>
                        &nbsp : &nbsp
                    <asp:DropDownList ID="ddlState" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <p></p>
                        <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Position 1 </asp:Label>
                    </td>
                    <td colspan="4">
                        <p></p>
                        &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtPosition1" Style="width: 18vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <p></p>
                        <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Position 2 </asp:Label>
                    </td>
                    <td colspan="4">
                        <p></p>
                        &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtPosition2" Style="width: 18vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <p></p>
                        <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Position 3 </asp:Label>
                    </td>
                    <td colspan="4">
                        <p></p>
                        &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="txtPosition3" Style="width: 18vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>

            <br />


            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                <button id="btnUpdateDirectorInfo" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Update Information</button>
            </div>

            <br />
            <br />

            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
                <p></p>
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Old Password &nbsp : &nbsp </asp:Label>
                <asp:TextBox runat="server" ID="txtOldPassword" Style="width: 200px; font-size: 0.8vw" CssClass="textboxcss"></asp:TextBox>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
                <p></p>
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> New Password &nbsp : &nbsp </asp:Label>
                <asp:TextBox runat="server" ID="txtNewPassword" Style="width: 200px; font-size: 0.8vw" CssClass="textboxcss"></asp:TextBox>
            </div>

            <br />

            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                <button id="btnUpdateDirectorPassword" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Update Password</button>
            </div>

            <br />

        </div>
    </div>

    <div class="messagealert" id="alert_container" style="text-align: center"></div>

</asp:Content>
