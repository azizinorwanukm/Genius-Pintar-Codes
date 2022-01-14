<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Tukar_KataLaluan.ascx.vb" Inherits="KPP_SYS.Tukar_KataLaluan" %>

<div class="row" style="background-color: #b3b3b3; text-align: left; padding-left: 23px">
    <br />
    <img src="img/permatapintar2.jpg" width="270" height="140" class="w3-circle" />
    <br />
    <asp:Label runat="server"> Username : </asp:Label>
    <asp:TextBox class="form-control w3-text-black" ID="txtloginUsername" Style="width: 90%" runat="server"></asp:TextBox>
    <p></p>
    <asp:Label runat="server"> Password : </asp:Label>
    <asp:TextBox type="password" class="form-control w3-text-black" ID="txtloginPassword" Style="width: 90%" runat="server" placeholder="Enter Old Password"></asp:TextBox>
    <p></p>
    <asp:Label runat="server"> Password : </asp:Label>
    <asp:TextBox type="password" class="form-control w3-text-black" ID="txtnewPassword" Style="width: 90%" runat="server" placeholder="Enter New Password"></asp:TextBox>
</div>
<div class="row" style="background-color: #b3b3b3; text-align: center">
    <p></p>
    <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
    <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
</div>
