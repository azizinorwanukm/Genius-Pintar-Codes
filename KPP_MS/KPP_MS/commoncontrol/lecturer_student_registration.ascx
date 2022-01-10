<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_student_registration.ascx.vb" Inherits="KPP_MS.lecturer_student_registration" %>

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

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Home &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnStudentRegistration" runat="server" style="display: inline-block; font-size: 0.8vw">Student Registration</button>
        <button id="btnHostelRegistration" runat="server" style="display: inline-block; font-size: 0.8vw">Hostel Registration</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh; overflow-y: scroll;" class="sc4" id="StudentRegistration" runat="server">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Enter Student Mykad </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="TextBox1" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                     &nbsp &nbsp
                    <button id="btnStudentSearch" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Search Student</button>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student ID </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtstudentID" Style="width: 8vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstudentName" Style="width: 40vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstudentMykad" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Race </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlRace" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Religion </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlReligion" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
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
                    <asp:TextBox runat="server" ID="txtstudentGender" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstudentEmail" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstudentPhone" Style="width: 8vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstudentAddress" Style="width: 40vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstudentCity" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtstudentPostcode" Style="width: 8vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
        </table>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <p></p>
            <button id="btnUpdateStudentInfo" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Update Student Information</button>
        </div>
    </div>


    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh;" class="sc4" id="HostelRegistration" runat="server">


    </div>
</div>


<div class="messagealert" id="alert_container" style="text-align: center"></div>
