<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_homepage.ascx.vb" Inherits="KPP_MS.lecturer_homepage" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }
</style>
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pengajar_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="staff_info()" value="0">Staff Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none; font-size: 14px" id="staff_info">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-6 w3-text-black image-upload" style="text-align: center; padding-left: 23px">
                <label for="ctl00_ContentPlaceHolder1_lecturer_homepage_uploadPhoto">
                    <asp:Image ID="staff_Photo" runat="server" class="w3-circle blah" />
                </label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Name : </asp:Label>
                <asp:Label CssClass="Label " ID="staff_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> IC :</asp:Label>
                <asp:Label CssClass="Label" ID="staff_MyKad" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> ID : </asp:Label>
                <asp:Label CssClass="Label" ID="staff_ID" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Staff Email Address : </asp:Label>
                <asp:Label CssClass="Label" ID="staff_Email" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Gender : </asp:Label>
                <asp:Label CssClass="Label" ID="staff_Sex" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Phone No : </asp:Label>
                <asp:Label CssClass="Label " ID="staff_MobileNo" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Address : </asp:Label>
                <asp:Label CssClass="Label " ID="staff_Address" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> City : </asp:Label>
                <asp:Label CssClass="Label " ID="staff_City" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> State : </asp:Label>
                <asp:Label CssClass="Label " ID="staff_State" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> PostalCode : </asp:Label>
                <asp:Label CssClass="Label" ID="staff_Posscode" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Position 1 : </asp:Label>
                <asp:Label CssClass="Label " ID="staff_Position_P1" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Position 2 : </asp:Label>
                <asp:Label CssClass="Label " ID="staff_Position_P2" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Position 3 : </asp:Label>
                <asp:Label CssClass="Label " ID="staff_Position_P3" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
        </div>
        <p></p>
    </div>
</div>
<br />
